// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

/// <summary>
/// Le panneau que les verbes de zone 57 et 58 ouvrent : <b>le coffre</b> — deposer et reprendre des
/// objets. Trois fonctions, toutes atteintes par le bit <c>0x40</c> de <c>DAT_005de02c</c> que
/// <c>AreaVerb_0053DEA0</c> et <c>AreaVerb_0053DF20</c> levent.
///
/// <para>Fichier ouvert le 2026-08-25. Il aurait pu aller dans <c>WorldMapTick.cs</c>, qui contient
/// deja le crochet appelant — mais celui-la fait 12 500 lignes et ceci est un ecran complet,
/// coherent en soi. <c>ARCHITECTURE.md</c> ne fixe pas de taille maximale ; il demande de la
/// coherence.</para>
/// </summary>
public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: 0x0053df90 — <b>le crochet <c>PTR_FUN_0056aff0</c></b>, entree 10 de
    /// <c>s_Vt_0056afc8</c>. Absent de <c>Blaze.c</c> ; releve au desassembleur (260 octets,
    /// <c>Legacy/Raw/Blaze/FUN_0053DF90.asm</c>).
    ///
    /// <para>Il encadre l'appel au panneau : il pose le rectangle de decoupe, appelle
    /// <see cref="RunItemDepot_0053E0A0"/>, puis dessine <b>le curseur d'ecran</b> par dessus.</para>
    ///
    /// <para><b>Le rectangle de decoupe a deux regimes.</b> Si <c>s_SmallTextureFlag</c>
    /// (<c>DAT_00564a38</c>) est nul <b>et</b> que le niveau de detail vaut au moins 8, les deux
    /// bornes sont mises a zero — plein ecran. Sinon la largeur est fixee a <c>0xa0</c> et la
    /// hauteur derive du niveau de detail, <c>(0xb - niveau) * 12</c>. Le <c>lea eax,[eax+eax*2]</c>
    /// suivi de <c>shl eax,2</c> est une multiplication par 12, pas une adresse.</para>
    ///
    /// <para><b>La queue est mot pour mot celle de <c>DrawWorldMapMenuCursor</c></b> — memes huit
    /// octets de descripteur montes a la main dans l'arene d'entites
    /// (<c>00 00 00 df 00 01 3f 00</c>), memes <c>0x80</c> / <c>0x70</c> / <c>0x1400</c> /
    /// <c>0x1249</c>, memes deux emissions. Seule difference : le descripteur de sprite est ici une
    /// variable locale de la pile et non <c>s_MenuSprite_005de5f8</c>. Les deux corps sont
    /// distincts dans le binaire ; ils ne sont pas factorises ici pour la meme raison.</para>
    /// </summary>
    private static uint* EntityHook_0053DF90(uint* otBase, uint* prim, byte* slotRecord, byte* pad)
    {
        s_Unknown_005de15c = 1;

        if (s_SmallTextureFlag == 0 && Mem.s_SizeSetting_005de160 >= 8)
        {
            s_Unknown_005de158 = 0;
            s_Unknown_005de154 = 0;
        }
        else
        {
            s_Unknown_005de154 = 0xa0;
            s_Unknown_005de158 = (0xb - Mem.s_SizeSetting_005de160) * 12;
        }

        prim = RunItemDepot_0053E0A0(otBase, prim, slotRecord, pad);

        // Ghidra: `mov cx,80h` sert aux deux ecritures qui suivent — la table de decodage et le
        // champ +8 du descripteur local.
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfc) = 0x80;

        short* sprite = stackalloc short[0x0a];      // 0x14 octets de descripteur
        *(short*)((byte*)sprite + 0x08) = 0x80;

        s_Unknown_005de15c = 0;
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfe) = 0;

        byte* cursorSprite = Mem.Entity(0x005eca88);
        cursorSprite[0] = 0;
        cursorSprite[1] = 0;
        cursorSprite[2] = 0;
        cursorSprite[3] = 0xdf;
        cursorSprite[4] = 0;
        cursorSprite[5] = 1;
        cursorSprite[6] = 0x3f;
        cursorSprite[7] = 0;

        *(short*)((byte*)sprite + 0x0a) = 0x70;
        *(short*)((byte*)sprite + 0x0c) = 0x1400;
        *(short*)((byte*)sprite + 0x0e) = 0x1249;
        *(short*)((byte*)sprite + 0x10) = 0;
        *(byte**)sprite = cursorSprite;

        prim = Prims.DrawSpritePrimitive(otBase, prim, (int*)sprite, 0, 0);   // Ghidra: FUN_00521f20
        return Prims.DrawFlatPrimitive(otBase, prim, (int*)sprite);           // Ghidra: FUN_0051f9c0
    }

    /// <summary>
    /// Ghidra: FUN_005412f0 @ 0x005412f0 — <b>une liste d'icones posees par rapport a un panneau</b>.
    /// Le seul appele de <see cref="RunItemDepot_0053E0A0"/> qui manquait encore.
    ///
    /// <para>La liste est un tableau de dwords termine par un dword nul, et chaque dword est en
    /// realite <b>quatre octets signes</b> : le numero du panneau de reference, l'image de l'atlas,
    /// puis un decalage en x et en y. Les coordonnees se prennent donc sur le descripteur du
    /// panneau (<c>+4</c> et <c>+6</c>), et non sur l'ecran — ce qui fait suivre les icones quand le
    /// panneau bouge.</para>
    ///
    /// <para>Ghidra rend l'indexation par <c>(char)*piVar3 * 0x8c8</c> depuis <c>&amp;DAT_00568534</c> :
    /// c'est <see cref="s_MenuDescriptors_00568530"/> au pas d'un descripteur d'interface, plus 4.
    /// Le meme tableau de quatre descripteurs que le menu de jeu emploie.</para>
    ///
    /// <para>La derniere emission remet l'image a <b>0</b> avant d'appeler l'emetteur plat : c'est ce
    /// qui referme la liste d'affichage, pas une icone de plus.</para>
    /// </summary>
    private static uint* DrawIconList_005412F0(uint* otBase, uint* prim, int* list)
    {
        UiWin.EmitViewportRect(otBase, &prim);              // Ghidra: FUN_0051be20

        short* sprite = stackalloc short[0x0a];             // 0x14 octets
        *(uint*)((byte*)sprite + 0x04) = 0x1000000;
        *(byte**)sprite = *(byte**)*(byte***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);

        for (byte* entry = (byte*)list; *(int*)entry != 0; entry += 4)
        {
            *(short*)((byte*)sprite + 0x10) = (sbyte)entry[1];

            byte* panel = MenuDescriptor((sbyte)entry[0]);
            *(short*)((byte*)sprite + 0x0a) = (short)(*(short*)(panel + 6) + (sbyte)entry[3]);
            *(short*)((byte*)sprite + 0x08) = (short)(*(short*)(panel + 4) + (sbyte)entry[2]);

            prim = Prims.DrawSpriteVariant(otBase, prim, (int*)sprite, 0, 0);   // Ghidra: FUN_00521df0
        }

        *(short*)((byte*)sprite + 0x10) = 0;
        prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)sprite);            // Ghidra: FUN_0051f9c0
        UiWin.EmitClipRect(otBase, &prim);                                     // Ghidra: FUN_0051bdb0
        return prim;
    }

    // =====================================================================================
    // L'etat du coffre. Onze globales nouvelles, toutes verifiees contre la carte des symboles le
    // 2026-08-25 : aucune ne tombe dans une allocation existante. La plus proche en dessous de la
    // plage 0x005dfe20..0x005dff40 est s_Particles_005de824, qui s'arrete a 0x005dfba4.
    //
    // **Onze autres adresses de ce corps N'ONT PAS ete declarees**, et c'est le resultat le plus
    // utile de la verification : ce sont des CHAMPS des descripteurs d'interface deja modelises.
    //   0x005633b6                                        = s_WorldMapDescriptor_005633b0 + 6
    //   0x00568563 / 0x0056856b / 0x00568ddc / 0x00568df7 = MenuDescriptor(0) + 0x33 / 0x3b / 0x8ac / 0x8c7
    //   0x00568dfc / 0x00568e00 / 0x00568e02 / 0x00568e1b = MenuDescriptor(1) + 4 / 8 / 0x0a / 0x23
    //   0x005696e3                                        = MenuDescriptor(2) + 0x23
    // Les declarer aurait donne deux noms pour une adresse — le defaut que MemoryLayoutTests
    // surveille. Et les quatre derniers offsets sont ceux que le moteur de texte nomme deja :
    // +0x33 le curseur, +0x3b les fleches, +0x8ac le curseur de texte, +0x8c7 les drapeaux.
    // =====================================================================================

    /// <summary>Ghidra: <c>&amp;DAT_005dfe20</c> — le descripteur de sprite de travail du coffre,
    /// 0x14 octets : <c>+0</c> l'atlas, <c>+4</c> les attributs, <c>+8</c> et <c>+0x0a</c> la
    /// position, <c>+0x10</c> l'image. Tous ses champs sont ecrits avant chaque emission, donc son
    /// contenu initial de <c>.data</c> n'est jamais lu — contrairement au bloc d'ecran
    /// <c>0x00566c48</c>, dont le cas a ete tranche le meme jour.</summary>
    private static readonly byte* s_DepotSprite_005dfe20 = (byte*)NativeMemory.AllocZeroed(0x14);

    /// <summary>Ghidra: DAT_005dfe40 — le brouillon a tout faire du corps : tantot la cle de
    /// comparaison <c>rangee &lt;&lt; 8 | colonne</c>, tantot le numero d'objet courant.</summary>
    private static uint s_Unknown_005dfe40;

    /// <summary>Ghidra: DAT_005dfe48 — l'emplacement d'objet vise, dans le bloc du personnage.</summary>
    private static byte* s_Unknown_005dfe48;

    /// <summary>Ghidra: DAT_005dfee8 — la fiche de l'objet designe, dans le repertoire du menu
    /// objets (<see cref="s_Unknown_005e97d0"/>), au pas de 0x140.</summary>
    private static byte* s_Unknown_005dfee8;

    /// <summary>Ghidra: DAT_005dff10 — la vue de l'emplacement du membre courant, au pas de
    /// <b>0x4c</b>. Ghidra rend <c>0x26</c> sur un tableau de <c>short</c> ; le binaire fait
    /// <c>imul eax,4Ch</c> en <c>0x0053e0e5</c>, ce qui tranche. Pose par le prologue et lu par le
    /// mode 0 seulement.</summary>
    private static byte* s_Unknown_005dff10;

    /// <summary>Ghidra: DAT_005dff14 — le bloc de 0x2000 octets du personnage courant.</summary>
    private static byte* s_Unknown_005dff14;

    /// <summary>Ghidra: DAT_005dff2c — la table de numeros d'objet de la page courante, prise dans
    /// le repertoire de zone a <c>+0xb4</c>.</summary>
    private static byte* s_Unknown_005dff2c;

    /// <summary>Ghidra: DAT_005dff34 — le compteur de boucle du corps, en <b>globale</b> et non en
    /// local. Ce n'est pas un artefact de decompilation : le binaire ecrit vraiment a cette
    /// adresse.</summary>
    private static int s_Unknown_005dff34;

    /// <summary>Ghidra: DAT_005dff38 — l'enregistrement de manette du membre courant.</summary>
    private static byte* s_Unknown_005dff38;

    /// <summary>Ghidra: DAT_005dff3c — l'emplacement de personnage du membre courant, 0x9c octets
    /// a partir de <c>0x005e93c0</c>.</summary>
    private static byte* s_Unknown_005dff3c;

    /// <summary>Ghidra: <c>&amp;DAT_0056dd34</c> — les octets <c>2a 00</c>, releves dans le binaire :
    /// la chaine <c>"*"</c> d'un caractere que la liste intercale entre le nom de l'objet et son
    /// prix. Passee comme texte a <c>FUN_0051c120</c>, donc bien une chaine et pas un nombre.</summary>
    private static readonly byte* s_DepotSeparator_0056dd34 = AllocBytes([0x2a, 0x00]);

    // =====================================================================================
    // L'etat du mode 0 — le coffre. Trente-neuf globales et huit tables de donnees, relevees dans
    // le binaire le 2026-08-25.
    //
    // **Quatre adresses n'ont PAS ete declarees**, meme verification que pour le mode 1 : ce sont
    // des vues dans des allocations existantes.
    //   0x005e8f92 = s_EntityDecodeTable_005e8f00 + 0x92   (l'allocation fait 0x1000)
    //   0x005e9322 = Mem.Arena(0x005e9322)                 (s_EquipPairs_005e9320 est deja une vue)
    //   0x005fafb0 = Mem.Entity(0x005fafb0)
    //   0x00568536 / 0x0056853a / 0x005696c4 / 0x005696c6 = champs des descripteurs d'interface
    // =====================================================================================

    /// <summary>Ghidra: DAT_005dff58 — le peripherique choisi, 0 a 3, deduit de deux mots de
    /// manette a <c>0x00604b0a</c> et <c>0x00604b2e</c>.</summary>
    private static uint s_Unknown_005dff58;

    /// <summary>Ghidra: DAT_005dff18 — le sous-etat d'attente de peripherique (1, 2, 99).</summary>
    private static int s_Unknown_005dff18;

    /// <summary>Ghidra: DAT_005dff7c, DAT_005dff80 et DAT_005dff84 — l'etat de l'automate, sa
    /// valeur de l'image precedente, et le drapeau « l'etat vient de changer » que la comparaison
    /// des deux leve. <c>DAT_005dff7c</c> est deja declaree avec les dwords de curseur.</summary>
    private static int s_Unknown_005dff80;
    private static int s_Unknown_005dff84;

    /// <summary>Ghidra: DAT_005dff04 — la poignee de fichier ouverte par <c>FUN_00504b40</c>.
    /// L'original la garde en <c>int</c> et la compare a -1 ; <c>RetryOpenCardFile</c> rend un
    /// <c>IntPtr</c>, donc c'est ce type-la qui est conserve ici et le test devient un
    /// <c>(nint) >= 0</c>.</summary>
    private static IntPtr s_Unknown_005dff04;

    /// <summary>Ghidra: DAT_005dff08 — le curseur d'ecriture du message compose.</summary>
    private static byte* s_Unknown_005dff08;

    /// <summary>Ghidra: DAT_005dff0c, DAT_005dff1c, DAT_005dff20, DAT_005dff24, DAT_005dff30 —
    /// les compteurs de travail du corps : ordonnee courante, compteur de boucle, drapeau « le bloc
    /// d'objets a ete range », le retour de <c>FUN_00504820</c>, et le nombre d'objets tenus.</summary>
    private static int s_Unknown_005dff0c, s_Unknown_005dff1c, s_Unknown_005dff24, s_Unknown_005dff30;
    private static byte s_Unknown_005dff20;

    /// <summary>Ghidra: DAT_005dfe44 — le nombre d'objets presents dans le coffre.</summary>
    private static int s_Unknown_005dfe44;

    /// <summary>Ghidra: DAT_005dfe18 — le curseur dans la table des puissances de dix.</summary>
    private static uint* s_Unknown_005dfe18;

    /// <summary>Ghidra: &amp;DAT_005dfe50 — le tampon ou le message d'erreur de peripherique est
    /// compose, trois chaines mises bout a bout en glyphes de seize bits. 0x80 octets suffisent
    /// largement aux trois libelles concatenes.</summary>
    private static readonly byte* s_DepotMessage_005dfe50 = (byte*)NativeMemory.AllocZeroed(0x80);

    /// <summary>Ghidra: &amp;DAT_005dff40 — la table des indices d'objet presents dans le coffre,
    /// jusqu'a 0x16 entrees d'un octet.</summary>
    private static readonly byte* s_DepotSlots_005dff40 = (byte*)NativeMemory.AllocZeroed(0x18);

    /// <summary>Ghidra: &amp;DAT_005dfed0 — le drapeau « garde » par emplacement, 0x18 octets.
    /// Les six dwords que le corps remet a zero d'un bloc (<c>0x005dfed0</c> a <c>0x005dfee4</c>)
    /// sont ce meme tableau.</summary>
    private static readonly byte* s_DepotKeep_005dfed0 = (byte*)NativeMemory.AllocZeroed(0x18);

    /// <summary>Ghidra: &amp;DAT_005dfef4 et &amp;DAT_005dfefc — les deux rectangles de transfert
    /// VRAM que la boucle de vignettes remplit avant chaque <c>Ordinal_674</c>, et
    /// <c>DAT_005dfef8</c> / <c>DAT_005dff00</c> les deux sources correspondantes.</summary>
    private static readonly short* s_DepotRect_005dfef4 = (short*)NativeMemory.AllocZeroed(8);
    private static readonly short* s_DepotRect_005dfefc = (short*)NativeMemory.AllocZeroed(8);
    private static byte* s_Unknown_005dfef8;
    private static byte* s_Unknown_005dff00;

    /// <summary>Ghidra: &amp;DAT_005dfe38 — le rectangle VRAM de l'icone de sauvegarde, quatre
    /// shorts que <c>Ordinal_684</c> lit.</summary>
    private static readonly short* s_DepotIconRect_005dfe38 = (short*)NativeMemory.AllocZeroed(8);

    /// <summary>
    /// Ghidra: <c>&amp;PTR_DAT_0056dc20</c> — quatre tables d'octets, relevees dans le binaire :
    /// l'identifiant de peripherique que <c>DAT_00604980</c> recoit, indexe par le peripherique
    /// choisi puis par la ligne du curseur.
    /// </summary>
    private static readonly byte*[] s_DepotDeviceIds_0056dc20 =
    [
        AllocBytes([0x00, 0x04, 0x00, 0x00]),
        AllocBytes([0x00, 0x01, 0x02, 0x03, 0x04, 0x00, 0x00, 0x00]),
        AllocBytes([0x00, 0x04, 0x05, 0x06, 0x07, 0x00, 0x00, 0x00]),
        AllocBytes([0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07]),
    ];

    /// <summary>
    /// Ghidra: la table de pointeurs de <c>0x0056dc58</c> — quatre listes dont le <b>premier octet
    /// est le compte</b>, puis autant d'indices de ligne. C'est ce compte que le corps lit par
    /// <c>**(char **)(DAT_005dff58 * 4 + 0x56dc58)</c> pour borner le curseur.
    /// </summary>
    private static readonly byte*[] s_DepotRows_0056dc58 =
    [
        AllocBytes([0x02, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00]),
        AllocBytes([0x05, 0x02, 0x03, 0x04, 0x05, 0x01, 0x00, 0x00]),
        AllocBytes([0x05, 0x00, 0x06, 0x07, 0x08, 0x09, 0x00, 0x00]),
        AllocBytes([0x08, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09]),
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056dc68</c> — dix positions d'ecran, deux <c>short</c> chacune, que le
    /// curseur de peripherique prend par l'indice de ligne.
    /// </summary>
    private static readonly short* s_DepotRowPos_0056dc68 = (short*)AllocBytes(
    [
        0x11, 0x00, 0x2c, 0x00,  0x1c, 0x00, 0x2c, 0x00,
        0xef, 0xff, 0x43, 0x00,  0xf6, 0xff, 0x49, 0x00,
        0x04, 0x00, 0x49, 0x00,  0x0b, 0x00, 0x43, 0x00,
        0x23, 0x00, 0x43, 0x00,  0x2a, 0x00, 0x49, 0x00,
        0x38, 0x00, 0x49, 0x00,  0x3f, 0x00, 0x43, 0x00,
    ]);

    /// <summary>Ghidra: <c>&amp;DAT_0056dcb0</c> — la liste d'icones du cadre principal, dix
    /// entrees de quatre octets signes plus le mot nul de fin. Voir
    /// <see cref="DrawIconList_005412F0"/>.</summary>
    private static readonly int* s_DepotIcons_0056dcb0 = (int*)AllocBytes(
    [
        0x02, 0x13, 0x10, 0x68,  0x02, 0x14, 0x4c, 0x68,
        0x02, 0x1c, 0x10, 0x78,  0x02, 0x1d, 0x34, 0x78,
        0x02, 0x1e, 0x58, 0x78,  0x02, 0x1f, 0x7c, 0x78,
        0x02, 0x20, 0x10, 0x82,  0x02, 0x21, 0x34, 0x82,
        0x02, 0x22, 0x58, 0x82,  0x02, 0x23, 0x7c, 0x82,
        0x00, 0x00, 0x00, 0x00,
    ]);

    /// <summary>Ghidra: <c>&amp;DAT_0056dca0</c> — la liste d'icones du bandeau du bas, deux
    /// entrees.</summary>
    private static readonly int* s_DepotIcons_0056dca0 = (int*)AllocBytes(
    [
        0x03, 0x06, 0x0c, 0x0c,  0x03, 0x07, 0x0c, 0x28,
        0x00, 0x00, 0x00, 0x00,
    ]);

    /// <summary>Ghidra: <c>&amp;DAT_0056dce0</c> et <c>&amp;DAT_0056dce8</c> — deux enregistrements
    /// d'atlas de huit octets : le cadre de selection et la marque « garde ».</summary>
    private static readonly byte* s_DepotFrame_0056dce0 =
        AllocBytes([0x00, 0x00, 0x18, 0x18, 0x00, 0x00, 0x3e, 0x00]);
    private static readonly byte* s_DepotKeepMark_0056dce8 =
        AllocBytes([0xf8, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x07]);

    /// <summary>Ghidra: <c>&amp;DAT_00566be8</c> — les dix puissances de dix, de 1 000 000 000 a 1,
    /// que la conversion du nombre d'heures de jeu descend.</summary>
    private static readonly uint* s_PowersOfTen_00566be8 = (uint*)AllocDwords(
    [
        1000000000, 100000000, 10000000, 1000000, 100000,
        10000, 1000, 100, 10, 1,
    ]);

    private static uint* AllocDwords(uint[] values)
    {
        uint* p = (uint*)NativeMemory.Alloc((nuint)(values.Length * 4));
        for (int i = 0; i < values.Length; i++) p[i] = values[i];
        return p;
    }

    /// <summary>
    /// Ghidra: FUN_0053e0a0 @ 0x0053e0a0 — <b>le panneau du coffre et celui de la carte
    /// memoire</b>, selon le mode que <c>DAT_005de548</c> porte. 1359 lignes de pseudo-C
    /// (<c>Blaze.c</c> 87975..89333), et ce sont <b>deux ecrans</b>, pas un.
    ///
    /// <para><b>Mode 0 — LE COFFRE DE STOCKAGE</b> (verbe 57) : 1150 lignes, un automate de
    /// sous-etats, une persistance sur fichier (<c>FUN_00504b40</c> / <c>FUN_00504c20</c>) et des
    /// recopies de blocs d'objets (<c>FUN_005352e0</c>). <b>Non transcrite</b> — elle leve en se
    /// nommant. C'est ce que le coffre noir de la chambre demande.</para>
    ///
    /// <para><b>Mode 1 — une BOUTIQUE</b> (verbe 58) : cinq rangees d'objets avec un prix contre le
    /// bord droit ; elle preleve ce prix sur l'emplacement marque <c>0x2efb</c> et rend l'objet par
    /// <see cref="GrantChestContents_00530F20"/>. C'est la branche transcrite ici.</para>
    ///
    /// <para><b>Les deux etiquettes ont ete inversees jusqu'au 2026-08-25</b>, et pour une mauvaise
    /// raison : le mode 0 appelle des primitives de fichier, j'en ai conclu « carte memoire » sans
    /// regarder ce qu'il DESSINE. La persistance sur fichier est le magasin du coffre, pas une
    /// sauvegarde de partie. C'est le jeu qui a tranche, pas la lecture.</para>
    ///
    /// <para>Ghidra ne recupere que deux parametres alors que le crochet en pousse quatre ; les
    /// deux derniers ne sont lus nulle part dans le corps.</para>
    ///
    /// <para><b>Le prologue est commun aux deux modes.</b> Il fixe le cadre d'ecran, derive quatre
    /// vues du membre courant (<c>DAT_005dff28</c>) et eteint le bit <c>0x10000</c> de son mot
    /// d'etat. Les trois pas sont pris <b>au desassemblage</b> et non a la pseudo-C, qui les rend
    /// sur des types de tableau differents : <c>imul eax,4Ch</c> pour la vue d'emplacement (Ghidra
    /// dit <c>0x26</c>, sur des <c>short</c>), <c>0x9c</c> pour l'emplacement de personnage,
    /// <c>shl edx,0Dh</c> pour le bloc du personnage.</para>
    ///
    /// <para><b>La liste des emplacements occupes est reconstruite a chaque image</b> : le bloc du
    /// personnage porte a <c>+0x2c0</c> leur nombre et a <c>+0x2c1</c> leurs indices. Celui dont la
    /// cle de <c>+0x86</c> vaut <c>0x2efb</c> est note a part dans <c>DAT_005dff6c</c> — c'est
    /// l'emplacement que la validation reprendra.</para>
    ///
    /// <para><b>Un appel que Ghidra decoupe mal.</b> Il rend
    /// <c>FUN_0051bd50(fiche, personnage, panneau, 0)</c> suivi de <c>FUN_00544350(resultat)</c>.
    /// Le binaire empile les quatre valeurs, appelle <c>FUN_0051bd50</c> avec <b>une seule</b>
    /// (<c>add esp,4</c> en <c>0x0053e323</c>), puis empile le nom rendu et appelle
    /// <c>FUN_00544350</c> avec quatre (<c>add esp,10h</c>). Les trois arguments appartiennent au
    /// second appel, pas au premier — le meme piege que le commentaire de
    /// <see cref="BuildItemDisplayName_0051BD50"/> decrit deja pour son autre site.</para>
    /// </summary>
    private static uint* RunItemDepot_0053E0A0(uint* otBase, uint* prim, byte* unused3, byte* unused4)
    {
        _ = unused3;
        _ = unused4;

        *(ushort*)Mem.Arena(0x005e9950) = 0x100;
        *(ushort*)Mem.Arena(0x005e98e0) = 0x100;
        *(ushort*)Mem.Arena(0x005e9952) = 0xe0;
        *(ushort*)Mem.Arena(0x005e98e2) = 0xe0;

        uint member = s_Unknown_005dff28;
        s_Unknown_005dff3c = Inp.s_SlotFlags_005e93c0 + member * 0x9c;
        s_Unknown_005dff10 = Inp.s_SlotArea_005e9180 + member * 0x4c;
        s_Unknown_005dff14 = GetMainMemPtr() + (member + 0x78) * 0x2000;
        s_Unknown_005dff38 = Inp.s_InputStateBase_00604a20
                           + Inp.s_PlayerSlots_005e9188[member * 0x4c] * 0x10;

        *(uint*)s_Unknown_005dff3c &= 0xfffeffff;
        s_Unknown_005dff14[0x2b8] = 0;
        MenuDescriptor(2)[0x23] = 0;                       // Ghidra: DAT_005696e3

        if (s_Unknown_005de548 == 0) return RunDepotMode0(otBase, prim);

        if (s_Unknown_005de548 != 1)
        {
            MenuDescriptor(2)[0x23] = 0;                   // Ghidra: DAT_005696e3
            return prim;
        }

        // ---------------------------------------------------------------- la liste des occupes
        s_Unknown_005dff34 = 0;
        s_Unknown_005dff14[0x2c0] = 0;
        byte* block = s_Unknown_005dff14;

        for (; s_Unknown_005dff34 < 10; s_Unknown_005dff34++)
        {
            if (block[s_Unknown_005dff34 * 0x280 + 0x300] == 0) continue;

            if (*(short*)(block + s_Unknown_005dff34 * 0x280 + 0x386) == 0x2efb)
            {
                s_Unknown_005dff6c = (uint)s_Unknown_005dff34;
            }

            block[block[0x2c0] + 0x2c1] = (byte)s_Unknown_005dff34;
            s_Unknown_005dff14[0x2c0]++;
            block = s_Unknown_005dff14;
        }

        s_Unknown_005dff2c = *(byte**)(s_Unknown_005e97cc + 0xb4) + (block[0x150] & 7) * 0x18;

        if ((int)s_Unknown_0056dc90 < 0)
        {
            // ------------------------------------------------------ navigation dans la grille
            if ((s_Unknown_005dff38[3] & 0x40) != 0)
            {
                // Ghidra: `goto LAB_0053e9fa`, qui vit dans la branche du mode 0. Trois lignes,
                // recopiees ici plutot que rendues par un saut d'un mode a l'autre.
                *(short*)(s_WorldMapDescriptor_005633b0 + 6) = unchecked((short)0xffb0);
                Mem.s_WorldFlags_005de02c &= 0xffffffbf;
                Aud.PlayUiSound39();                       // Ghidra: FUN_0053dba0
                return prim;
            }

            s_Unknown_005dff34 = 0x17;
            s_Unknown_005dfe40 = s_Unknown_005dff5c << 8 | s_Unknown_005dff64;

            // La grille fait cinq colonnes : la colonne bouge d'abord, la rangee ensuite.
            if ((s_Unknown_005dff38[6] & 0x10) != 0)
            {
                if (s_Unknown_005dff64 == 0)
                {
                    if (s_Unknown_005dff5c != 0) s_Unknown_005dff5c--;
                }
                else
                {
                    s_Unknown_005dff64--;
                }
            }

            if ((s_Unknown_005dff38[6] & 0x40) != 0)
            {
                if ((int)s_Unknown_005dff64 < 4) s_Unknown_005dff64++;
                else if ((int)s_Unknown_005dff5c < 0x12) s_Unknown_005dff5c++;
            }

            if ((*(ushort*)(s_Unknown_005dff38 + 2) & 0x8000) == 0)
            {
                if (s_Unknown_005dfe40 != (s_Unknown_005dff5c << 8 | s_Unknown_005dff64))
                {
                    Aud.PlayUiSound3A();                   // Ghidra: FUN_0053db60
                }

                s_Unknown_005dfee8 = s_Unknown_005e97d0
                    + s_Unknown_005dff2c[s_Unknown_005dff5c + s_Unknown_005dff64] * 0x140;

                byte* name = BuildItemDisplayName_0051BD50(s_Unknown_005dfee8);
                BindMenuLabels(name, (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
            }
            else
            {
                *(ushort*)(s_Unknown_005dff38 + 2) &= 0x7fff;
                BindMenuLabels(
                    *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 4),
                    (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
                Aud.PlayUiSound38();                       // Ghidra: FUN_0053db80
                s_Unknown_0056dc90 = 0;
            }
        }
        else if ((s_Unknown_005dff38[3] & 0x40) == 0)
        {
            // ------------------------------------------------------ les trois sous-etats
            if (s_Unknown_0056dc90 == 0)
            {
                if (MenuDescriptor(0)[0x8ac] == 0)         // Ghidra: DAT_00568ddc
                {
                    s_Unknown_005dfe48 = block + s_Unknown_005dff6c * 0x280 + 0x300;

                    if (Mem.s_Unknown_005e909f == 0)
                    {
                        ushort price = *(byte*)(*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8) + 4)
                                              + s_Unknown_005dff64 + s_Unknown_005dff5c);

                        if (*(ushort*)(s_Unknown_005dfe48 + 0x10) < price)
                        {
                            BindMenuLabels(
                                *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 0xc),
                                (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
                            s_Unknown_0056dc90 = 1;
                        }
                        else
                        {
                            // Le paiement exact vide l'emplacement : trois octets a zero et le
                            // compte d'occupes qui redescend.
                            if (*(ushort*)(s_Unknown_005dfe48 + 0x10) == price)
                            {
                                s_Unknown_005dfe48[0x10] = 0;
                                s_Unknown_005dfe48[0x11] = 0;
                                s_Unknown_005dfe48[0] = 0;
                                s_Unknown_005dff14[0x2c0]--;
                            }

                            s_Unknown_005dff3c[0x1b] = 0;
                            Mem.s_Unknown_005e909f = 0;

                            s_Unknown_005dfee8 = s_Unknown_005e97d0
                                + s_Unknown_005dff2c[s_Unknown_005dff5c + s_Unknown_005dff64] * 0x140;
                            s_Unknown_005dfee8[0x13] |= 0x80;

                            s_Unknown_005dff34 = GrantChestContents_00530F20(
                                (uint*)s_Unknown_005dff3c,
                                s_Unknown_005dff2c[s_Unknown_005dff5c + s_Unknown_005dff64],
                                0);
                            RestorePartyReactions_00530ED0();

                            if (s_Unknown_005dff34 == 0)
                            {
                                if (*(short*)(s_Unknown_005dfe48 + 0x10) != 0
                                    && s_Unknown_005dfe48[0] != 0)
                                {
                                    *(ushort*)(s_Unknown_005dfe48 + 0x10) =
                                        (ushort)(*(short*)(s_Unknown_005dfe48 + 0x10) - price);
                                }

                                StoreItemInInventory_005312D0((uint*)s_Unknown_005dff3c);
                                BindMenuLabels(Mem.s_Unknown_005606f8,
                                    (uint)(nint)s_Unknown_005dff3c, s_WorldMapDescriptor_005633b0, 0);
                                BindMenuLabels(
                                    *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 0x10),
                                    (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
                                s_Unknown_0056dc90 = 2;
                            }
                            else
                            {
                                BindMenuLabels(Mem.s_Unknown_005606f8,
                                    (uint)(nint)s_Unknown_005dff3c, s_WorldMapDescriptor_005633b0, 0);
                                BindMenuLabels(
                                    *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 0x14),
                                    (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
                                s_Unknown_0056dc90 = 1;
                            }
                        }
                    }
                    else
                    {
                        BindMenuLabels(
                            *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x70) + 8),
                            (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
                        s_Unknown_0056dc90 = 1;
                    }
                }
            }
            else if (s_Unknown_0056dc90 == 1)
            {
                if (MenuDescriptor(0)[0x8ac] == 0) s_Unknown_0056dc90 = -1;
            }
            else if (s_Unknown_0056dc90 == 2 && MenuDescriptor(0)[0x8ac] == 0)
            {
                Mem.s_WorldFlags_005de02c &= 0xffffffbf;
            }

            if ((Mem.s_WorldFlags_005de02c & 0x40) == 0) return prim;
        }
        else
        {
            // ------------------------------------------------------ l'annulation
            byte* panel = MenuDescriptor(0);
            panel[0x3b] = 0;                               // Ghidra: DAT_0056856b
            panel[0x8c7] &= 0xf9;                          // Ghidra: DAT_00568df7
            panel[0x33] = 0;                               // Ghidra: DAT_00568563
            panel[0x8ac] = 0;                              // Ghidra: DAT_00568ddc
            Aud.PlayUiSound39();                           // Ghidra: FUN_0053dba0
            s_Unknown_0056dc90 = -1;
        }

        // ---------------------------------------------------------------- le curseur de rangee
        byte* row = MenuDescriptor(1);
        UiWin.EmitViewportRect(otBase, &prim);             // Ghidra: FUN_0051be20

        *(uint*)(s_DepotSprite_005dfe20 + 4) = 0x1000000;
        *(byte**)s_DepotSprite_005dfe20 =
            *(byte**)*(byte***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
        *(short*)(s_DepotSprite_005dfe20 + 8) = (short)(*(short*)(row + 4) + 0x0c);
        *(short*)(s_DepotSprite_005dfe20 + 0x10) = 0x18;
        *(short*)(s_DepotSprite_005dfe20 + 0x0a) =
            (short)(((short)(s_Unknown_005dff64 << 3) - (short)s_Unknown_005dff64) * 4
                    + *(short*)(row + 6) + 0x14);

        prim = Prims.DrawSpriteVariant(otBase, prim, (int*)s_DepotSprite_005dfe20, 0, 0);
        prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20);
        UiWin.EmitClipRect(otBase, &prim);                 // Ghidra: FUN_0051bdb0

        // ---------------------------------------------------------------- les cinq rangees
        *(short*)(s_DepotSprite_005dfe20 + 0x10) = 0;
        *(uint*)(s_DepotSprite_005dfe20 + 4) = 0x1000000;
        (*(byte**)s_DepotSprite_005dfe20)[7] = 0;
        *(short*)(s_DepotSprite_005dfe20 + 0x0a) = (short)(*(short*)(row + 6) + 0x10);

        for (s_Unknown_005dff34 = (int)s_Unknown_005dff5c;
             s_Unknown_005dff34 < (int)(s_Unknown_005dff5c + 5);
             s_Unknown_005dff34++)
        {
            UiWin.EmitViewportRect(otBase, &prim);
            *(short*)(s_DepotSprite_005dfe20 + 8) = (short)(*(short*)(row + 4) + 0x14);
            *(byte**)s_DepotSprite_005dfe20 = *(byte**)(*(byte**)(s_Unknown_005e97cc + 0x94) + 4);

            s_Unknown_005dfe40 = s_Unknown_005dff2c[s_Unknown_005dff34];
            s_Unknown_005dfee8 = s_Unknown_005e97d0 + s_Unknown_005dfe40 * 0x140;

            // La vignette : le numero d'objet donne la colonne par son reste et la rangee par son
            // quotient, decalee de quatre. Ghidra rend la division par 10 avec sa correction de
            // signe ; le numero vient d'un octet, donc positif, et les deux formes coincident.
            byte* atlas = *(byte**)s_DepotSprite_005dfe20;
            atlas[0] = (byte)((s_Unknown_005dfe40 % 10) * 0x18);
            atlas[1] = (byte)((s_Unknown_005dfe40 / 10 + 4) * 0x18);

            prim = Prims.DrawSpriteVariant(otBase, prim, (int*)s_DepotSprite_005dfe20, 0,
                       s_Unknown_005dfe40 & 0xf | (s_Unknown_005dfe40 & 0xf0) << 4);
            prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20);
            UiWin.EmitClipRect(otBase, &prim);

            *(short*)(s_DepotSprite_005dfe20 + 8) += 0x1c;
            *(short*)(s_DepotSprite_005dfe20 + 0x0a) += 8;
            *(byte**)s_DepotSprite_005dfe20 = (byte*)s_GlyphAtlas_00566c28;

            // Un nom qui commence par le marqueur de devise s'affiche a partir du troisieme octet.
            byte* label = ItemDisplayName(s_Unknown_005dfee8);   // Ghidra: FUN_0051bd30
            if (*label == 0x24) label += 2;

            prim = Prims.DrawTextRun(otBase, prim, (int*)s_DepotSprite_005dfe20, label, 0, 0, 0x10);

            *(short*)(s_DepotSprite_005dfe20 + 8) += 0x88;
            prim = Prims.DrawTextRun(otBase, prim, (int*)s_DepotSprite_005dfe20,
                       s_DepotSeparator_0056dd34, 0, 0, 0x10);

            *(short*)(s_DepotSprite_005dfe20 + 8) += 8;
            *(byte**)s_DepotSprite_005dfe20 = SpriteAtlas(0x00566c30);

            prim = Prims.DrawDecimalNumber(otBase, prim, (int*)s_DepotSprite_005dfe20,
                       *(byte*)(*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8) + 4)
                                + s_Unknown_005dff34),
                       2, 0);

            *(short*)(s_DepotSprite_005dfe20 + 0x0a) += 0x14;
        }

        // ---------------------------------------------------------------- le texte et les cadres
        *(short*)(row + 8) = unchecked((short)0xe0);       // Ghidra: DAT_00568e00
        *(short*)(row + 0x0a) = 0x98;                      // Ghidra: DAT_00568e02
        row[0x23] = 0;                                     // Ghidra: DAT_00568e1b

        prim = Ui.RunTextEngine(prim, MenuDescriptor(0));  // Ghidra: FUN_005443a0

        for (s_Unknown_005dff34 = 1; s_Unknown_005dff34 >= 0; s_Unknown_005dff34--)
        {
            prim = UiWin.DrawUiPanel(otBase, prim, MenuDescriptor(s_Unknown_005dff34));
        }

        return prim;
    }

    /// <summary>
    /// Ghidra: la branche <c>DAT_005de548 == 0</c> de <c>FUN_0053e0a0</c> — <b>le coffre de
    /// stockage</b>, celui du cliche du jeu d'origine. <c>Blaze.c</c> 88221..89333.
    ///
    /// <para>Trois parties : un prologue de navigation, un automate de vingt etats, et une queue
    /// d'affichage. <b>Le prologue et la queue sont transcrits ; l'automate ne l'est pas</b> — ses
    /// etats levent en se nommant, un par un, pour que chacun se transcrive et se verifie
    /// separement. Voir <c>REVERSE_STATUS.md</c>, « suite 25 », pour l'ordre conseille.</para>
    ///
    /// <para><b>Une branche morte, etablie au desassemblage.</b> Le prologue teste
    /// <c>DAT_0056dc90 &lt; 0</c> trois fois, et le premier bloc met cette globale a zero
    /// (<c>mov [56DC90h],esi</c> en <c>0x0053e963</c>, avec <c>esi</c> a 0 — les <c>cmp</c> voisins
    /// le confirment). Les deux tests suivants sont donc faux <b>dans tous les cas</b> : si le
    /// premier bloc a tourne la valeur vaut 0, s'il n'a pas tourne elle etait deja positive. Le
    /// <c>goto switchD_0053eaef_caseD_a</c> qui en depend ne se prend jamais, et les deux tests de
    /// bouton <c>0x4000</c> / <c>0x8000</c> qu'ils gardent sont inatteignables. Ils sont signales
    /// ici en commentaire plutot que transcrits : les ecrire donnerait du code que rien n'appelle,
    /// les taire perdrait le fait.</para>
    /// </summary>
    private static uint* RunDepotMode0(uint* otBase, uint* prim)
    {
        s_Unknown_005dff34 = (int)s_Unknown_005dff64;

        // ------------------------------------------------------------------ l'entree du panneau
        // Le seul tour ou ce bloc s'execute est celui qui suit l'ouverture : le sous-etat vaut -1,
        // et il repart de zero en sortant.
        if (s_Unknown_0056dc90 < 0)
        {
            if ((s_Unknown_005dff38[6] & 0x80) != 0) s_Unknown_005dff64--;
            if ((s_Unknown_005dff38[6] & 0x20) != 0) s_Unknown_005dff64++;

            BindMenuLabels(
                *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x104),
                (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);

            s_Unknown_005dff7c = 2;
            s_Unknown_0056dc90 = 0;
        }

        // ------------------------------------------------------------------ le peripherique
        // Deux mots de manette a 0x00604b0a et 0x00604b2e ; le second ajoute 2 au choix.
        short* padA = (short*)(Inp.s_InputStateBase_00604a20 + (0x00604b0a - 0x00604a20));
        short* padB = (short*)(Inp.s_InputStateBase_00604a20 + (0x00604b2e - 0x00604a20));

        s_Unknown_005dff58 = *padA == unchecked((short)0x8000) ? 1u : 0u;
        if (*padB == unchecked((short)0x8000)) s_Unknown_005dff58 += 2;

        byte* rows = s_DepotRows_0056dc58[s_Unknown_005dff58];
        if ((int)s_Unknown_005dff64 < 0) s_Unknown_005dff64 = (uint)((sbyte)rows[0] - 1);
        if ((sbyte)rows[0] <= (int)s_Unknown_005dff64) s_Unknown_005dff64 = 0;

        if (s_Unknown_005dff34 != (int)s_Unknown_005dff64) Aud.PlayUiSound3A();   // FUN_0053db60

        // Ghidra place ici un second `if (DAT_0056dc90 < 0)` — les deux tests de bouton 0x4000 et
        // 0x8000 qu'il garde sont MORTS, voir le resume. Non transcrits, volontairement.

        if (s_Unknown_005dff80 != (int)s_Unknown_005dff7c)
        {
            s_Unknown_005dff80 = (int)s_Unknown_005dff7c;
            s_Unknown_005dff84 = 1;
        }

        // ------------------------------------------------------------------ les deux automates
        DispatchDepotDeviceWait();
        prim = DispatchDepotState(otBase, prim);

        // ------------------------------------------------------------------ la queue d'affichage
        return DrawDepotFrame(otBase, prim);
    }

    /// <summary>
    /// Ghidra: <c>switch(DAT_005dff18)</c> — le petit automate d'attente de peripherique.
    /// TODO(REVERSE) : non transcrit ; son <c>default</c> ne fait rien, comme dans l'original.
    /// </summary>
    private static void DispatchDepotDeviceWait()
    {
        int slot = (s_CardSlotIndex_00604980 & 3) + (s_CardSlotIndex_00604980 & 4) * 4;

        switch (s_Unknown_005dff18)
        {
            // Ghidra: `default: break` — c'est l'etat au repos.
            default: return;

            case 1:
                s_Unknown_005dff24 = Saves.PollCardTransfer(slot, false);
                if (s_Unknown_005dff24 < -1) s_Unknown_005dff7c = unchecked((uint)-4);
                return;

            case 2:
                // Ghidra: une boucle d'attente sur Ordinal_67. Elle ne tourne jamais dans cette
                // version de la DLL — winsys3d.dll rend toujours 1 (FUN_00a050f0, `RET 4` verifie
                // dans le binaire) — mais elle est transcrite telle quelle : c'est une attente de
                // materiel, et rien ne garantit qu'une autre version reponde pareil.
                while (Winsys3d.Ordinal_67(slot) == 0) { }
                s_Unknown_005dff24 = Saves.PollCardTransfer(slot, false);
                return;

            case 99:
                s_Unknown_005dff24 = Saves.PollCardTransfer(slot, false);
                if (s_Unknown_005dff24 < -1) s_Unknown_005dff7c = 0x60;
                return;
        }
    }

    /// <summary>
    /// Ghidra: <c>switch(DAT_005dff7c)</c> — l'automate principal du coffre, vingt etats.
    /// TODO(REVERSE) : non transcrit.
    ///
    /// <para><b>L'original n'a pas de <c>default</c></b> : une valeur hors de sa liste de cas ne
    /// fait rien du tout. La levee ci-dessous ne couvre donc que les etats qu'il traite vraiment,
    /// et elle les nomme.</para>
    /// </summary>
    private static uint* DispatchDepotState(uint* otBase, uint* prim)
    {
        switch ((int)s_Unknown_005dff7c)
        {
            case 2: DepotState2LoadStore(); return prim;

            case 3: return DepotState3Browse(otBase, prim);

            // Ghidra: `case 4` — trois affectations, rien d'autre. C'est l'aiguillage vers
            // l'ecriture : l'etat 3 y arrive en sortant, et il repart aussitot vers l'etat 8.
            // Il remet aussi a zero les deux drapeaux de transition, ce qui fait que l'etat 8
            // entrera par sa branche « premier tour » et non par celle qui ne fait que consommer
            // le changement d'etat.
            case 4:
                s_Unknown_005dff7c = 8;
                s_Unknown_005dff84 = 0;
                s_Unknown_005dff18 = 0;
                return prim;

            case 6: DepotState6Confirm(); return prim;
            case 7: DepotState7Confirm(); return prim;
            case 9: DepotState9Leave(); return prim;
            case 0x5f: DepotState5FAbort(true); return prim;
            case 0x60: DepotState5FAbort(false); return prim;
            case 99: DepotState99Retry(); return prim;

            case 0x61: case 0x62: DepotState61DeviceWait(); return prim;
            case -5: DepotStateMinus4Rearm(true); return prim;
            case -4: DepotStateMinus4Rearm(false); return prim;
            case -3: case -2: DepotStateMinus2WaitCard(); return prim;
            case -1: DepotStateMinus1Rearm(); return prim;

            case 0: DepotState0PickDevice(); return prim;
            case 1: DepotState1OpenDevice(); return prim;
            case 5: DepotState5Reopen(); return prim;

            case 8: DepotState8Write(); return prim;

            // Toute autre valeur : l'original ne fait rien, et il n'a pas de `default`.
            default: return prim;
        }
    }

    /// <summary>
    /// Ghidra: <c>case 2</c> de l'automate du coffre — <b>la lecture du magasin</b>, l'etat que le
    /// prologue pose en entrant. <c>Blaze.c</c> 88423..88543.
    ///
    /// <para>Il ouvre le fichier du coffre, le lit, <b>fusionne dans l'inventaire du joueur les
    /// piles identiques</b>, envoie les vingt-deux vignettes en VRAM, recense ce qui reste des deux
    /// cotes, et passe a l'etat 3 — sauf si le coffre est vide, auquel cas il affiche un message et
    /// saute a l'etat 9.</para>
    ///
    /// <para><b>Il commence par mettre les objets du joueur de cote</b> a
    /// <c>MainMem + 0x1bc000</c>. C'est cette copie que l'etat 9 remet en place quand on sort sans
    /// avoir rien range — le seul filet de securite du panneau.</para>
    ///
    /// <para><b>Le fichier n'est pris que si sa somme de controle correspond</b> a celle du bloc du
    /// joueur (<c>+0x1ffc</c> contre <c>MainMem + 0x1b38fc</c>). Sinon tout le corps est saute et
    /// on tombe sur le message « coffre vide ».</para>
    ///
    /// <para><b>La fusion.</b> Pour chacun des douze emplacements lus, on cherche parmi les dix du
    /// joueur un objet dont les <b>seize premiers octets</b> et la cle de <c>+0x86</c> concordent —
    /// le meme predicat d'identite que l'expertise et le transfert entre membres emploient — et
    /// dont l'octet de <c>+0x13</c> porte <c>0x30</c>. Les drapeaux de <c>+0x12</c> se combinent par
    /// <c>ou</c>, les quantites s'additionnent, et le total est <b>borne a 99</b> par deux ecritures
    /// d'octet (<c>99</c> puis <c>0</c>) et non par une saturation de <c>short</c>.</para>
    ///
    /// <para><b>Les vignettes.</b> Vingt-deux emplacements, deux transferts VRAM chacun : la
    /// planche de l'objet (<c>+0x140</c>) vers une bande de 0x10 de large a la ligne
    /// <c>0x1fc + i/16</c>, et son pictogramme (<c>+0x160</c>) vers une case de 6 x 0x18 rangee en
    /// dix colonnes. Les deux rectangles sont des <b>locales de pile</b> dans l'original, dont
    /// l'adresse est rangee dans deux globales ; ils sont ici deux blocs propres de huit octets —
    /// meme comportement observable, sans le pointeur pendant que l'original laisse derriere lui.</para>
    ///
    /// <para><b>Un croisement de listes, transcrit tel quel.</b> La derniere boucle parcourt la
    /// table des emplacements du <i>coffre</i> (<c>DAT_005dff40</c>) mais son compte de tours est
    /// celui de l'inventaire du <i>joueur</i> (<c>+0x2c0</c>). C'est ce que dit le binaire ; les
    /// deux listes n'ont aucune raison d'avoir la meme longueur, et rien ne borne la lecture.
    /// Conserve comme les autres defauts d'origine — a verser dans <c>legacy-bug.md</c> le jour ou
    /// une partie le fait sortir de ses seize octets.</para>
    /// </summary>
    private static void DepotState2LoadStore()
    {
        if (s_Unknown_005dff84 != 0)
        {
            s_Unknown_005dff84 = 0;
            s_Unknown_005dff18 = 0;
            return;
        }

        byte* mainMem = GetMainMemPtr();

        Saves.SetCardFileName(s_Unknown_005dff14 + 0x1fd0);      // Ghidra: FUN_00512900
        Saves.SetCardPathDeviceFields();                         // Ghidra: FUN_005128e0
        s_Unknown_005dff20 = 0;

        // Les objets du joueur mis de cote : c'est ce que l'etat 9 remet si on ressort a vide.
        Mem.CopyDwords((uint*)(s_Unknown_005dff14 + 0x300), (uint*)(mainMem + 0x1bc000), 0x640);

        s_Unknown_005dff04 = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
        int read = Saves.RetrySeekAndReadCardFile(
            s_Unknown_005dff04, mainMem + 0x1b1900, 0x2000, 0x2000);
        Saves.RetryCloseCardFile(s_Unknown_005dff04);

        s_Unknown_005dfe40 = (uint)read;
        s_Unknown_005dff2c = mainMem + 0x1b38fc;

        if (read >= 0 && *(int*)(s_Unknown_005dff14 + 0x1ffc) == *(int*)s_Unknown_005dff2c)
        {
            MergeStoredStacksIntoInventory(mainMem);
            UploadStoredItemThumbnails(mainMem);
            RebuildInventoryAndKeepFlags();

            if (s_Unknown_005dfe44 != 0)
            {
                s_Unknown_005dff7c++;
                return;
            }
        }

        // Coffre vide, ou somme de controle qui ne correspond pas.
        BindMenuLabels(
            *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x100),
            (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
        s_Unknown_005dff7c = 9;
    }

    /// <summary>La fusion des piles identiques — voir <see cref="DepotState2LoadStore"/>.</summary>
    private static void MergeStoredStacksIntoInventory(byte* mainMem)
    {
        byte* player = s_Unknown_005dff14;
        s_Unknown_005dfee8 = mainMem + 0x1b1900;

        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 0xc;
             s_Unknown_005dff34++, s_Unknown_005dfee8 += 0x280)
        {
            for (s_Unknown_005dff1c = 0; s_Unknown_005dff1c < 10; s_Unknown_005dff1c++)
            {
                byte* slot = player + s_Unknown_005dff1c * 0x280 + 0x300;

                int same = 0;
                while (same < 0x10 && slot[same] == s_Unknown_005dfee8[same]) same++;

                if (same != 0x10
                    || *(short*)(slot + 0x86) != *(short*)(s_Unknown_005dfee8 + 0x86)
                    || (s_Unknown_005dfee8[0x13] & 0x30) == 0)
                {
                    continue;
                }

                *(ushort*)(slot + 0x12) |= *(ushort*)(s_Unknown_005dfee8 + 0x12);
                *(short*)(slot + 0x10) += *(short*)(s_Unknown_005dfee8 + 0x10);

                // Ghidra: deux ecritures d'octet, 99 puis 0 — pas une saturation de short.
                if (*(ushort*)(slot + 0x10) > 99)
                {
                    slot[0x10] = 99;
                    slot[0x11] = 0;
                }

                *s_Unknown_005dfee8 = 0;
                break;
            }
        }

        Mem.CopyDwords((uint*)(player + 0x300), (uint*)(mainMem + 0x1b0000), 0x640);
    }

    /// <summary>Les vingt-deux vignettes envoyees en VRAM — voir <see cref="DepotState2LoadStore"/>.</summary>
    private static void UploadStoredItemThumbnails(byte* mainMem)
    {
        byte* entry = mainMem + 0x1b0000;
        s_Unknown_005dfe44 = 0;

        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 0x16;
             s_Unknown_005dff34++, entry += 0x280)
        {
            s_Unknown_005dfee8 = entry;

            if (*entry != 0)
            {
                s_DepotSlots_005dff40[s_Unknown_005dfe44] = (byte)s_Unknown_005dff34;
                s_Unknown_005dfe44++;
            }

            s_Unknown_005dfef8 = entry + 0x140;
            s_DepotRect_005dfef4[0] = (short)((s_Unknown_005dff34 & 0xf) << 4);
            s_DepotRect_005dfef4[1] = (short)((s_Unknown_005dff34 >> 4) + 0x1fc);
            s_DepotRect_005dfef4[2] = 0x10;
            s_DepotRect_005dfef4[3] = 1;
            Winsys3d.Ordinal_674(s_DepotRect_005dfef4, s_Unknown_005dfef8);

            s_Unknown_005dff00 = entry + 0x160;
            s_DepotRect_005dfefc[0] = (short)((s_Unknown_005dff34 % 10) * 0x18 / 4 + 0x3c0);
            s_DepotRect_005dfefc[1] = (short)((s_Unknown_005dff34 / 10) * 0x18 + 0x88);
            s_DepotRect_005dfefc[2] = 6;
            s_DepotRect_005dfefc[3] = 0x18;
            Winsys3d.Ordinal_674(s_DepotRect_005dfefc, s_Unknown_005dff00);
        }

        // Les deux entrees de page de la table de decodage, derivees du dernier rectangle envoye.
        // Ghidra les rend par un CONCAT11 sur un CONCAT22 dont la moitie haute est un reste de
        // registre : le masque 0x1f0 la supprime entierement, donc elle ne participe pas.
        byte* decode = Mem.s_EntityDecodeTable_005e8f00;
        *(short*)(decode + 0xfa) = 0x7f00;

        int low = (byte)(s_DepotRect_005dfefc[0] >> 2);
        int high = (byte)((ushort)s_DepotRect_005dfefc[1] >> 8);
        *(short*)(decode + 0xf8) = (short)((short)((((high << 8) | low) & 0x1f0) | 0x2000) >> 4);
    }

    /// <summary>Le recensement des deux cotes — voir <see cref="DepotState2LoadStore"/>.</summary>
    private static void RebuildInventoryAndKeepFlags()
    {
        byte* player = s_Unknown_005dff14;

        player[0x2c0] = 0;
        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 10; s_Unknown_005dff34++)
        {
            if (player[s_Unknown_005dff34 * 0x280 + 0x300] == 0) continue;
            player[player[0x2c0] + 0x2c1] = (byte)s_Unknown_005dff34;
            player[0x2c0]++;
        }

        // Ghidra: six dwords remis a zero d'un bloc — c'est le tableau des drapeaux « garde ».
        for (int i = 0; i < 6; i++) ((uint*)s_DepotKeep_005dfed0)[i] = 0;

        // Le croisement de listes decrit dans le resume : la table parcourue est celle du coffre,
        // le compte de tours celui du joueur. Transcrit tel quel.
        for (uint i = 0; i < player[0x2c0]; i++)
        {
            s_Unknown_005dff34 = (int)(i + 1);
            s_DepotKeep_005dfed0[s_DepotSlots_005dff40[i]] = 1;
        }

        s_Unknown_005dff30 = player[0x2c0];
    }

    /// <summary>Ghidra: <c>&amp;DAT_0056dc98</c> — l'ordre dans lequel les huit valeurs de la fiche
    /// sont prises dans l'objet, relevé dans le binaire : un octet d'index par case.</summary>
    private static readonly byte* s_DepotStatOrder_0056dc98 =
        AllocBytes([0x02, 0x01, 0x00, 0x03, 0x04, 0x05, 0x06, 0x07]);

    /// <summary>Ghidra: <c>&amp;DAT_0056dcf0</c> — les huit pastilles de classe, un enregistrement
    /// d'atlas de huit octets chacune, releves d'un bloc dans le binaire.</summary>
    private static readonly byte* s_DepotClassMarks_0056dcf0 = AllocBytes(
    [
        0x80, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0x90, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xa0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xb0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xc0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xd0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xe0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xf0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
    ]);

    /// <summary>Ghidra: <c>&amp;DAT_0056dd30</c> — les octets <c>14 07</c>, un glyphe de seize bits
    /// que le nom de chaque rangee se voit ajouter en suffixe.</summary>
    private static readonly byte* s_DepotNameSuffix_0056dd30 = AllocBytes([0x14, 0x07, 0x00, 0x00]);

    /// <summary>
    /// Ghidra: <c>case 3</c> de l'automate du coffre — <b>la liste des objets ranges</b>, sa fenetre
    /// de cinq lignes et la fiche de celui que le curseur vise. <c>Blaze.c</c> 88545..88889, le plus
    /// gros des vingt etats.
    ///
    /// <para>C'est lui qui remplit le cadre que <see cref="DrawDepotFrame"/> dessine : quatre lignes
    /// « pictogramme + nom », les valeurs <c>ATT</c> / <c>DEF</c>, les deux portees et la rangee des
    /// huit classes. Exactement ce que montre le cliche du jeu d'origine.</para>
    ///
    /// <para><b>La fenetre.</b> Cinq lignes au plus : la colonne bouge d'abord, la rangee du haut
    /// ensuite, et la borne haute est <c>nombre - 1</c> tant qu'il y a moins de cinq objets. Le
    /// <c>iVar14 = 4</c> que Ghidra pose <b>avant</b> le repartiteur est la valeur par defaut de
    /// cette borne : il ne descend a <c>nombre - 1</c> que si la liste est courte.</para>
    ///
    /// <para><b>Un <c>CONCAT31</c> qui n'existe pas.</b> Les quatre emissions de sprite de la boucle
    /// recoivent un <c>extra</c> que Ghidra rend par des <c>CONCAT31(...) &amp; 0xffff000f | (... &amp;
    /// 0xffff0010) &lt;&lt; 4</c>. Le desassemblage (<c>0x0053f93a</c>) montre la vraie sequence :
    /// <c>xor cx,cx</c> et <c>xor dx,dx</c> — deux effacements <b>de seize bits</b> qui laissent la
    /// moitie haute des registres intacte. Le calcul reel est
    /// <c>(emplacement &amp; 0xf) | ((emplacement &amp; 0x10) &lt;&lt; 4)</c>, et la moitie haute est un
    /// reste que <see cref="Primitives2d.DrawSpriteVariant"/> n'ouvre jamais — elle ne lit que les
    /// seize bits bas de son <c>extra</c>. Transcrit sous sa forme propre, avec ce releve pour
    /// justification.</para>
    ///
    /// <para><b>Le nom se compose sur la pile</b>, en un flux d'etiquettes
    /// <c>0c 04 &lt;x&gt; &lt;y&gt; 14 &lt;couleur&gt;</c> suivi du texte — le meme idiome que
    /// <see cref="DrawItemDetail_0052FDC0"/>. Le texte est le nom de l'objet, ou <c>« ??? »</c> s'il
    /// n'est pas identifie, et il recoit toujours le suffixe de <c>DAT_0056dd30</c>. Les trois
    /// boucles de copie de Ghidra sont les <c>strlen</c> / <c>strcpy</c> / <c>strcat</c> que MSVC a
    /// mis en ligne ; elles sont rendues par ce qu'elles font.</para>
    ///
    /// <para><b>La couleur de la ligne dit si l'objet est garde</b> : <c>7</c> quand son drapeau est
    /// leve, <c>1</c> sinon. Ghidra l'ecrit <c>(-(garde != 0) &amp; 6) + 1</c>, une facon sans
    /// branchement de choisir entre 1 et 7.</para>
    ///
    /// <para><b>La sortie.</b> Valider bascule le drapeau « garde » de la ligne visee, en refusant
    /// au-dela de dix objets tenus et en refusant de rendre ce qui est deja equipe (les cinq octets
    /// de <c>+0x2b0</c>). Annuler ne quitte que si l'echange tient en douze emplacements, et va
    /// directement a l'etat 9 si <b>rien n'a change</b> — compte identique et quantites identiques a
    /// la copie de sauvegarde. Sinon il passe a l'etat suivant.</para>
    /// </summary>
    private static uint* DepotState3Browse(uint* otBase, uint* prim)
    {
        byte* mainMem = GetMainMemPtr();
        byte* player = s_Unknown_005dff14;
        byte* store = mainMem + 0x1b0000;

        // ------------------------------------------------------------------ la navigation
        s_Unknown_005dff34 = (int)(s_Unknown_005dff68 << 8 | s_Unknown_005dff5c);

        if ((s_Unknown_005dff38[6] & 0x10) != 0)
        {
            if (s_Unknown_005dff5c == 0)
            {
                if (s_Unknown_005dff68 != 0) s_Unknown_005dff68--;
            }
            else
            {
                s_Unknown_005dff5c--;
            }
        }

        if ((s_Unknown_005dff38[6] & 0x40) != 0)
        {
            // Ghidra pose `iVar14 = 4` avant le repartiteur : c'est la borne par defaut.
            int last = 4;
            if (s_Unknown_005dfe44 < 5) last = s_Unknown_005dfe44 - 1;

            if ((int)s_Unknown_005dff5c < last) s_Unknown_005dff5c++;
            else if (s_Unknown_005dff68 < s_Unknown_005dfe44 - 5 && s_Unknown_005dfe44 > 4)
            {
                s_Unknown_005dff68++;
            }
        }

        // ------------------------------------------------------------------ le libelle du visé
        if (s_Unknown_005dff34 != (int)(s_Unknown_005dff68 << 8 | s_Unknown_005dff5c)
            || s_Unknown_005dff84 != 0)
        {
            if (s_Unknown_005dff84 == 0) Aud.PlayUiSound3A();   // Ghidra: FUN_0053db60
            else s_Unknown_005dff84 = 0;

            s_Unknown_005dfee8 = store
                + s_DepotSlots_005dff40[s_Unknown_005dff5c + s_Unknown_005dff68] * 0x280;

            byte* label = (s_Unknown_005dfee8[0x13] & 0x80) == 0
                ? *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x318)
                : BuildItemDisplayName_0051BD50(s_Unknown_005dfee8);

            BindMenuLabels(label, (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
        }

        // ------------------------------------------------------------------ le cadre et le curseur
        prim = DrawIconList_005412F0(otBase, prim, s_DepotIcons_0056dcb0);

        UiWin.EmitViewportRect(otBase, &prim);
        *(uint*)(s_DepotSprite_005dfe20 + 4) = 0x1000000;
        *(byte**)s_DepotSprite_005dfe20 =
            *(byte**)*(byte***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
        *(short*)(s_DepotSprite_005dfe20 + 0x0a) = (short)((s_Unknown_005dff5c << 4) + 0x18);
        *(short*)(s_DepotSprite_005dfe20 + 0x10) = 0x18;
        *(short*)(s_DepotSprite_005dfe20 + 8) = 0x58;
        prim = Prims.DrawSpriteVariant(otBase, prim, (int*)s_DepotSprite_005dfe20, 0, 0);
        prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20);
        UiWin.EmitClipRect(otBase, &prim);

        // ------------------------------------------------------------------ les cinq rangees
        s_Unknown_005dff1c = 0x60;
        s_Unknown_005dff0c = 0x14;
        *(short*)(s_DepotSprite_005dfe20 + 0x10) = 0;
        s_Unknown_005dfee8 = store;

        byte* stream = stackalloc byte[0x40];
        stream[0] = 0x0c;                                  // Ghidra: local_3c

        for (s_Unknown_005dff34 = (int)s_Unknown_005dff68; ; s_Unknown_005dff34++)
        {
            int end = s_Unknown_005dfe44 > 4 ? (int)s_Unknown_005dff68 + 5 : s_Unknown_005dfe44;
            if (s_Unknown_005dff34 >= end) break;

            bool aimed = s_Unknown_005dff34 == (int)(s_Unknown_005dff68 + s_Unknown_005dff5c)
                         && (int)s_Unknown_005dff78 >= 0;

            if (aimed)
            {
                *(uint*)(s_DepotSprite_005dfe20 + 0x0c) = 0x10001000;
                *(uint*)(s_DepotSprite_005dfe20 + 4) = s_Unknown_005e9814;
            }
            else
            {
                *(uint*)(s_DepotSprite_005dfe20 + 4) = 0x1000000;
                *(uint*)(s_DepotSprite_005dfe20 + 0x0c) = 0x8000800;
            }

            byte slot = s_DepotSlots_005dff40[s_Unknown_005dff34];
            byte* item = s_Unknown_005dfee8 + slot * 0x280;

            // La quantite, seulement si l'objet est empilable.
            if ((item[0x13] & 0x30) != 0)
            {
                *(byte**)s_DepotSprite_005dfe20 = SpriteAtlas(0x00566c30);
                *(short*)(s_DepotSprite_005dfe20 + 8) = (short)(s_Unknown_005dff1c + 8);
                *(short*)(s_DepotSprite_005dfe20 + 0x0a) = (short)(s_Unknown_005dff0c + 0x10);
                prim = Prims.DrawDecimalNumber(otBase, prim, (int*)s_DepotSprite_005dfe20,
                           *(ushort*)(item + 0x10), 0x82, 0);
            }

            UiWin.EmitViewportRect(otBase, &prim);
            *(short*)(s_DepotSprite_005dfe20 + 0x10) = 0;

            // La marque « equipe » : cinq octets a +0x2b0 du bloc du joueur.
            byte* equipped = player + 0x2b0;
            for (int n = 5; n != 0; n--, equipped++)
            {
                if (*equipped != slot) continue;

                *(short*)(s_DepotSprite_005dfe20 + 8) = (short)(s_Unknown_005dff1c + 0x0c);
                *(short*)(s_DepotSprite_005dfe20 + 0x0a) = (short)(s_Unknown_005dff0c + 0x0c);
                *(byte**)s_DepotSprite_005dfe20 = s_DepotKeepMark_0056dce8;
                prim = Prims.DrawSpriteVariant(otBase, prim, (int*)s_DepotSprite_005dfe20, 0, 0);
                prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20);
            }

            // Le pictogramme de l'objet. Sa case dans l'atlas se derive du numero d'emplacement :
            // la colonne par le reste de dix, la rangee par le quotient, decalee de 0x78.
            *(byte**)s_DepotSprite_005dfe20 = s_DepotFrame_0056dce0;
            s_DepotFrame_0056dce0[0] = (byte)((slot % 10) * 0x18);
            s_DepotFrame_0056dce0[1] = (byte)((slot / 10) * 0x18 - 0x78);
            *(short*)(s_DepotSprite_005dfe20 + 0x0a) = (short)(s_Unknown_005dff0c + 0x0c);
            *(short*)(s_DepotSprite_005dfe20 + 8) = (short)(s_Unknown_005dff1c + 0x0c);

            // Ghidra: l'extra des quatre emissions. Voir le resume — la forme propre est celle-ci,
            // le CONCAT31 etant un artefact des `xor cx,cx` / `xor dx,dx` de 0x0053f93a.
            uint extra = (uint)((slot & 0xf) | ((slot & 0x10) << 4));

            sbyte width = (sbyte)(s_DepotFrame_0056dce0[2] - 1);
            sbyte height = (sbyte)(s_DepotFrame_0056dce0[3] - 1);

            // La ligne visee est dessinee QUATRE fois, decalee d'un pixel a chaque tour : c'est le
            // liere qui l'entoure. Ghidra teste `DAT_005dfe2c == 0x800`, la moitie basse du mot de
            // couleur que la branche « non visee » vient de poser.
            if (*(ushort*)(s_DepotSprite_005dfe20 + 0x0c) == 0x800)
            {
                s_DepotFrame_0056dce0[2] -= 2;
                s_DepotFrame_0056dce0[3] -= 2;
                s_DepotFrame_0056dce0[0] += 1;
                prim = Prims.DrawSpritePrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20, 2, extra);

                (*(byte**)s_DepotSprite_005dfe20)[1] += 1;
                prim = Prims.DrawSpritePrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20, 2, extra);

                (*(byte**)s_DepotSprite_005dfe20)[0] -= 1;
                prim = Prims.DrawSpritePrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20, 2, extra);

                (*(byte**)s_DepotSprite_005dfe20)[1] -= 1;
                (*(byte**)s_DepotSprite_005dfe20)[2] += 1;
                (*(byte**)s_DepotSprite_005dfe20)[3] += 1;

                width = (sbyte)s_DepotFrame_0056dce0[2];
                height = (sbyte)s_DepotFrame_0056dce0[3];
            }

            s_DepotFrame_0056dce0[3] = (byte)height;
            s_DepotFrame_0056dce0[2] = (byte)width;

            prim = Prims.DrawSpritePrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20, 0, extra);
            (*(byte**)s_DepotSprite_005dfe20)[2] += 1;
            (*(byte**)s_DepotSprite_005dfe20)[3] += 1;
            UiWin.EmitClipRect(otBase, &prim);

            // ------------------------------------------------ le nom, monte sur la pile
            *(short*)(s_DepotSprite_005dfe20 + 8) = (short)(s_Unknown_005dff1c + 0x1c);
            *(byte**)s_DepotSprite_005dfe20 = (byte*)s_GlyphAtlas_00566c28;
            *(short*)(s_DepotSprite_005dfe20 + 0x0a) =
                (short)(((s_Unknown_005dff0c - 0x14) >> 4) * 3 + 0x20);

            byte* panel2 = MenuDescriptor(2);
            stream[1] = 4;
            stream[2] = (byte)(*(short*)(s_DepotSprite_005dfe20 + 8) - *(short*)(panel2 + 4));
            stream[3] = (byte)(*(short*)(s_DepotSprite_005dfe20 + 0x0a) - *(short*)(panel2 + 6) - 4);
            stream[4] = 0x14;

            // Ghidra: `(-(garde != 0) & 6) + 1` — 7 quand le drapeau est leve, 1 sinon.
            stream[5] = (byte)(s_DepotKeep_005dfed0[slot] != 0 ? 7 : 1);

            byte* text = stream + 6;
            if ((item[0x13] & 0x80) == 0)
            {
                // Les deux dwords de « ??? », copies tels quels.
                *(uint*)text = *(uint*)(s_UnknownItemText_00566b80 + 4);
                *(uint*)(text + 4) = *(uint*)(s_UnknownItemText_00566b80 + 8);
            }
            else
            {
                // Ghidra: un strcpy mis en ligne par MSVC — rendu par ce qu'il fait.
                byte* name = ItemDisplayName(item);          // Ghidra: FUN_0051bd30
                int n = 0;
                while (name[n] != 0) { text[n] = name[n]; n++; }
                text[n] = 0;
            }

            // Ghidra: un strcat mis en ligne, du suffixe de DAT_0056dd30.
            {
                int n = 0;
                while (text[n] != 0) n++;
                for (int k = 0; ; k++)
                {
                    text[n + k] = s_DepotNameSuffix_0056dd30[k];
                    if (s_DepotNameSuffix_0056dd30[k] == 0) break;
                }
            }

            panel2[0x23] = 1;                                // Ghidra: DAT_005696e3
            BindMenuLabels(stream, (uint)(nint)s_Unknown_005dff3c, panel2, 0);
            prim = Ui.RunTextEngine(prim, panel2);

            s_Unknown_005dff0c += 0x10;
            stream[0] = 0x0d;
        }

        // ------------------------------------------------------------------ la fiche de l'objet
        *(uint*)(s_DepotSprite_005dfe20 + 4) = 0x1000000;
        s_Unknown_005dfee8 = store
            + s_DepotSlots_005dff40[s_Unknown_005dff5c + s_Unknown_005dff68] * 0x280;

        // Les huit valeurs, quatre par rangee.
        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 8; s_Unknown_005dff34++)
        {
            *(short*)(s_DepotSprite_005dfe20 + 8) = (short)((s_Unknown_005dff34 & 3) * 0x24 + 0x68);
            int y = (s_Unknown_005dff34 >> 2) * 10 + 0x80;
            *(short*)(s_DepotSprite_005dfe20 + 0x0a) = (short)y;

            if ((s_Unknown_005dfee8[0x13] & 0x80) == 0)
            {
                *(byte**)s_DepotSprite_005dfe20 = (byte*)s_GlyphAtlas_00566c28;
                prim = Prims.DrawTextRun(otBase, prim, (int*)s_DepotSprite_005dfe20,
                           s_UnknownItemText_00566b80, 0, 0, -1);
            }
            else
            {
                *(byte**)s_DepotSprite_005dfe20 = SpriteAtlas(0x00566c30);
                sbyte field = (sbyte)s_DepotStatOrder_0056dc98[s_Unknown_005dff34];
                prim = Prims.DrawDecimalNumber(otBase, prim, (int*)s_DepotSprite_005dfe20,
                           (uint)(sbyte)s_Unknown_005dfee8[field + 0x18], 3, (uint)(field + 2));
            }
        }

        // Les deux portees.
        *(short*)(s_DepotSprite_005dfe20 + 8) = 0x60;
        *(short*)(s_DepotSprite_005dfe20 + 0x0a) = 0x70;

        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 2; s_Unknown_005dff34++)
        {
            *(short*)(s_DepotSprite_005dfe20 + 8) += 0x14;

            if ((*(ushort*)(s_Unknown_005dfee8 + 0x12) & 0x8000) == 0)
            {
                *(byte**)s_DepotSprite_005dfe20 = (byte*)s_GlyphAtlas_00566c28;
                prim = Prims.DrawTextRun(otBase, prim, (int*)s_DepotSprite_005dfe20,
                           s_UnknownItemText_00566b80, 0, 0, -1);
            }
            else
            {
                *(byte**)s_DepotSprite_005dfe20 = SpriteAtlas(0x00566c30);
                int value = *(short*)(s_Unknown_005dfee8 + s_Unknown_005dff34 * 2 + 0x30);
                if (value < 0) value = 0;
                s_Unknown_005dfe40 = (uint)value;
                prim = Prims.DrawDecimalNumber(otBase, prim, (int*)s_DepotSprite_005dfe20,
                           s_Unknown_005dfe40, 3, 0);
            }

            *(short*)(s_DepotSprite_005dfe20 + 8) += 0x28;
        }

        // Les huit pastilles de classe : allumee quand le bit correspondant de +0x12 est leve.
        UiWin.EmitViewportRect(otBase, &prim);
        *(short*)(s_DepotSprite_005dfe20 + 0x10) = 0;
        *(short*)(s_DepotSprite_005dfe20 + 8) = 0x60;
        *(short*)(s_DepotSprite_005dfe20 + 0x0a) = unchecked((short)0x98);
        *(byte**)s_DepotSprite_005dfe20 = s_DepotClassMarks_0056dcf0;

        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 8; s_Unknown_005dff34++)
        {
            // Ghidra: `(-(bit != 0) & 0xfffffff9) + 9` — 0 quand le bit est leve, 9 sinon.
            bool set = (1 << (s_Unknown_005dff34 & 0x1f)
                        & *(ushort*)(s_Unknown_005dfee8 + 0x12)) != 0;

            prim = Prims.DrawSpriteVariant(otBase, prim, (int*)s_DepotSprite_005dfe20, 0,
                       set ? 0u : 9u);
            prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20);

            *(short*)(s_DepotSprite_005dfe20 + 8) += 0x10;
            *(short*)(s_DepotSprite_005dfe20 + 0x10) += 1;
        }

        UiWin.EmitClipRect(otBase, &prim);

        // ------------------------------------------------------------------ valider / annuler
        ushort buttons = *(ushort*)(s_Unknown_005dff38 + 2);

        if ((buttons & 0x8000) != 0)
        {
            ToggleDepotKeepFlag(player);
            return prim;
        }

        if ((buttons & 0x4000) == 0) return prim;

        // Annuler : l'echange doit tenir en douze emplacements.
        s_Unknown_005dff34 = s_Unknown_005dfe44 - player[0x2c0];
        if (s_Unknown_005dff34 >= 0xd)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x144),
                (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
            Aud.PlayUiSound3B();                             // Ghidra: FUN_0053dbc0
            return prim;
        }

        Aud.PlayUiSound39();                                 // Ghidra: FUN_0053dba0

        // Rien n'a change ? Meme compte et memes quantites que la copie de sauvegarde.
        s_Unknown_005dff34 = 0;
        for (; s_Unknown_005dff34 < (int)s_Unknown_005dff30; s_Unknown_005dff34++)
        {
            byte slot = s_DepotSlots_005dff40[s_Unknown_005dff34];
            if (s_DepotKeep_005dfed0[slot] == 0) break;
            if (*(short*)(player + slot * 0x280 + 0x310)
                != *(short*)(mainMem + slot * 0x280 + 0x1bc010)) break;
        }

        if (player[0x2c0] == s_Unknown_005dff30 && s_Unknown_005dff34 == (int)s_Unknown_005dff30)
        {
            s_Unknown_005dff7c = 9;
            return prim;
        }

        s_Unknown_005dff7c++;                                // Ghidra: goto LAB_005402bd
        return prim;
    }

    /// <summary>
    /// Ghidra: la branche de validation de <c>case 3</c> — <b>le basculement du drapeau
    /// « garde »</b> sur la ligne visee.
    ///
    /// <para>Trois refus avant d'accepter : l'objet est deja equipe (il figure dans les cinq octets
    /// de <c>+0x2b0</c>), on tient deja dix objets, ou l'on essaie de rendre alors qu'on n'en tient
    /// aucun. Chacun se solde par le meme bip de refus.</para>
    /// </summary>
    private static void ToggleDepotKeepFlag(byte* player)
    {
        byte slot = s_DepotSlots_005dff40[s_Unknown_005dff5c + s_Unknown_005dff68];

        s_Unknown_005dff2c = player + 0x2b0;
        s_Unknown_005dff34 = 5;
        while (s_Unknown_005dff34 != 0 && *s_Unknown_005dff2c != slot)
        {
            s_Unknown_005dff34--;
            s_Unknown_005dff2c++;
        }

        if (s_Unknown_005dff34 != 0)
        {
            Aud.PlayUiSound3B();                             // deja equipe : refus
            return;
        }

        if (s_DepotKeep_005dfed0[slot] == 0)
        {
            if (player[0x2c0] < 10)
            {
                s_DepotKeep_005dfed0[slot] = 1;
                player[0x2c0]++;
                Aud.PlayUiSound38();
            }
            else
            {
                Aud.PlayUiSound3B();
                BindMenuLabels(
                    *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x140),
                    (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);
            }
            return;
        }

        if (player[0x2c0] != 0)
        {
            s_DepotKeep_005dfed0[slot] = 0;
            player[0x2c0]--;
            Aud.PlayUiSound38();
            return;
        }

        Aud.PlayUiSound3B();
    }


    /// <summary>
    /// Ghidra: <c>case 6</c> et <c>case 7</c> de l'automate du coffre — <b>les deux confirmations</b>
    /// que l'ecriture encadre. <c>Blaze.c</c> 88964..89002.
    ///
    /// <para>Les deux ont la meme forme : ils affichent un libelle, avancent d'un etat, et — une
    /// fois le message a l'ecran — attendent que le joueur annule pour tout defaire. Ghidra les
    /// rend avec trois etiquettes croisees (<c>LAB_005402b5</c>, <c>LAB_005402bd</c>,
    /// <c>joined_r0x005402d1</c>) ; ce sont deux corps distincts qui partagent trois queues, et ils
    /// sont ecrits ici comme tels.</para>
    ///
    /// <para><b>Ils se lisent sur le bit 2 des drapeaux du panneau</b> (<c>+0x8c7</c>) : tant qu'il
    /// est eteint, le message n'est pas encore pose et on le pose ; une fois leve, c'est que le
    /// moteur de texte attend une validation, et on surveille le bouton d'annulation.</para>
    ///
    /// <para><b>La difference entre les deux</b> tient au libelle et a une condition : le 6 prend le
    /// sien dans le repertoire principal (<c>+0x3c0</c>) sans rien demander ; le 7 ne parle que si
    /// <c>DAT_005e909f</c> est nul, et sinon il termine le transfert et saute a l'etat 4. Le 7 ne
    /// fait par ailleurs rien du tout tant que l'etat n'a pas change — <c>DAT_005dff84</c> a
    /// l'envers de tous les autres etats.</para>
    /// </summary>
    private static void DepotState6Confirm()
    {
        if (s_Unknown_005dff84 != 0)
        {
            s_Unknown_005dff84 = 0;
            return;
        }

        byte* panel = MenuDescriptor(0);

        if ((panel[0x8c7] & 2) == 0)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x3c0),
                (uint)(nint)s_Unknown_005dff3c, panel, 0);
            s_Unknown_005dff7c++;                        // Ghidra: LAB_005402bd
            return;
        }

        DepotCancelTransfer();                           // Ghidra: joined_r0x005402d1
    }

    /// <summary>Ghidra: <c>case 7</c> — voir <see cref="DepotState6Confirm"/>.</summary>
    private static void DepotState7Confirm()
    {
        // Le seul etat qui ne fasse rien TANT QUE l'etat n'a pas change, au lieu de l'inverse.
        if (s_Unknown_005dff84 == 0) return;

        byte* panel = MenuDescriptor(0);

        if ((panel[0x8c7] & 4) == 0)
        {
            if (Mem.s_Unknown_005e909f == 0)
            {
                BindMenuLabels(
                    *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x110),
                    (uint)(nint)s_Unknown_005dff3c, panel, 0);
                s_Unknown_005dff7c++;                    // Ghidra: LAB_005402b5 puis LAB_005402bd
                return;
            }

            Saves.PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);   // FUN_005046c0
            Saves.ResetCardWriteEvents();                                           // FUN_00504730
            s_Unknown_005dff7c = 4;                      // Ghidra: goto LAB_00540dab
            return;
        }

        DepotCancelTransfer();                           // Ghidra: joined_r0x005402d1
    }

    /// <summary>
    /// Ghidra: <c>joined_r0x005402d1</c> — la queue commune aux etats 6 et 7 : <b>l'annulation</b>.
    ///
    /// <para>Elle ne fait rien tant que le bouton n'est pas presse. Quand il l'est, elle eteint les
    /// deux bits de message du panneau, remet son curseur et ses fleches a zero, termine le
    /// transfert de carte et renvoie a l'etat 4 — donc a l'ecriture.</para>
    /// </summary>
    private static void DepotCancelTransfer()
    {
        if ((*(ushort*)(s_Unknown_005dff38 + 2) & 0x4000) == 0) return;

        byte* panel = MenuDescriptor(0);
        panel[0x8c7] &= 0xf9;
        panel[0x3b] = 0;
        panel[0x33] = 0;

        Saves.PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
        Saves.ResetCardWriteEvents();

        s_Unknown_005dff7c = 4;
        Aud.PlayUiSound39();
    }

    /// <summary>
    /// Ghidra: <c>case 9</c> de l'automate du coffre — <b>la sortie</b>. <c>Blaze.c</c> 89214..89240.
    ///
    /// <para>C'est l'etat ou l'on arrive quand le coffre est vide (etat 2) et quand rien n'a change
    /// (etat 3). Il rend la main au monde en eteignant le bit <c>0x40</c> de
    /// <see cref="GameMemory.s_WorldFlags_005de02c"/> — le meme que le verbe 57 avait leve — et pose
    /// <c>100</c> dans <c>0x005fafb0</c>, ce qui referme le couvercle.</para>
    ///
    /// <para><b>Et il remet les objets du joueur si rien n'a ete range.</b> <c>DAT_005dff20</c> est
    /// le drapeau que l'etat 8 leve apres une ecriture reussie ; tant qu'il est a zero, la copie
    /// posee par l'etat 2 a <c>MainMem + 0x1bc000</c> est recopiee par-dessus l'inventaire. C'est le
    /// filet de securite du panneau, et c'est ici qu'il se tend.</para>
    ///
    /// <para>Deux chemins pour la meme sortie : le bouton d'annulation la declenche tout de suite
    /// avec un bip et le nettoyage du panneau ; sinon elle attend que le message en cours soit
    /// consomme (bit 1 de <c>+0x8c7</c>).</para>
    /// </summary>
    private static void DepotState9Leave()
    {
        byte* mainMem = GetMainMemPtr();
        byte* panel = MenuDescriptor(0);

        if ((*(ushort*)(s_Unknown_005dff38 + 2) & 0x4000) == 0)
        {
            if ((panel[0x8c7] & 2) != 0) return;

            if (s_Unknown_005dff20 == 0)
            {
                Mem.CopyDwords((uint*)(mainMem + 0x1bc000),
                               (uint*)(s_Unknown_005dff14 + 0x300), 0x640);
            }

            *(ushort*)Mem.Entity(0x005fafb0) = 100;
            Mem.s_WorldFlags_005de02c &= 0xffffffbf;
            return;
        }

        if (s_Unknown_005dff20 == 0)
        {
            Mem.CopyDwords((uint*)(mainMem + 0x1bc000),
                           (uint*)(s_Unknown_005dff14 + 0x300), 0x640);
        }

        panel[0x8c7] &= 0xf9;
        Mem.s_WorldFlags_005de02c &= 0xffffffbf;
        panel[0x3b] = 0;
        panel[0x33] = 0;
        *(ushort*)Mem.Entity(0x005fafb0) = 100;
        Aud.PlayUiSound39();
    }

    /// <summary>
    /// Ghidra: <c>case 0x5f</c> et <c>case 0x60</c> — <b>l'abandon</b>, avec et sans fin de
    /// transfert. <c>Blaze.c</c> 89241..89252.
    ///
    /// <para>Le <c>0x5f</c> <b>tombe dans</b> le <c>0x60</c> : il termine le transfert de carte puis
    /// fait exactement ce que fait le <c>0x60</c>. Le <c>break</c> manquant est dans l'original, et
    /// c'est bien un enchainement voulu et non un oubli — les deux etats existent precisement pour
    /// distinguer « il y avait un transfert en cours » de « il n'y en avait pas ».</para>
    /// </summary>
    private static void DepotState5FAbort(bool finishTransfer)
    {
        if (finishTransfer)
        {
            Saves.PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
            Saves.ResetCardWriteEvents();
        }

        byte* panel = MenuDescriptor(0);
        panel[0x3b] = 0;
        panel[0x8c7] &= 0xf9;
        panel[0x33] = 0;

        s_Unknown_005dff7c = 4;
        s_Unknown_005dff18 = 0;
        Aud.PlayUiSound39();
    }

    /// <summary>
    /// Ghidra: <c>case 99</c> — <b>la reprise apres echec</b>. <c>Blaze.c</c> 89268..89272.
    ///
    /// <para>Il attend que le message soit consomme, remet le magasin dans l'etat ou il etait avant
    /// la tentative (<c>0x1b4000</c> vers <c>0x1b0000</c>, 0xdc0 dwords), puis repart a l'etat 4.
    /// C'est le pendant du <c>case 5</c>, qui avait fait la copie inverse.</para>
    /// </summary>
    private static void DepotState99Retry()
    {
        if ((MenuDescriptor(0)[0x8c7] & 2) != 0) return;

        byte* mainMem = GetMainMemPtr();
        Mem.CopyDwords((uint*)(mainMem + 0x1b4000), (uint*)(mainMem + 0x1b0000), 0xdc0);

        s_Unknown_005dff7c = 4;                          // Ghidra: LAB_00540dab
    }

    /// <summary>
    /// Ghidra: <c>case 0x61</c> et <c>case 0x62</c> — <b>l'attente de peripherique</b>, les deux
    /// etats ou l'on est reste avec un message a l'ecran pendant qu'une carte est sollicitee.
    /// <c>Blaze.c</c> 89248..89267. Les deux cas partagent un seul corps dans l'original.
    ///
    /// <para>Trois issues, testees dans cet ordre :</para>
    /// <list type="number">
    /// <item>le joueur annule — on part a l'etat <c>0x5f</c>, celui qui termine le transfert avant
    /// d'abandonner ;</item>
    /// <item><c>DAT_005dff24</c> vaut <b>-3</b> — le peripherique a rendu une erreur franche : on
    /// affiche le libelle <c>+0x3cc</c>, on nettoie le panneau et on repart a l'etat 5 pour
    /// reessayer ;</item>
    /// <item>sinon on attend que le message en cours soit consomme, puis on termine le transfert et
    /// on file a l'etat 4 — l'ecriture.</item>
    /// </list>
    ///
    /// <para><b>Le <c>-3</c> n'est pas un code d'erreur invente</b> : c'est ce que
    /// <c>FUN_00504820</c> rend, et l'automate d'attente de peripherique
    /// (<see cref="DispatchDepotDeviceWait"/>) le range dans <c>DAT_005dff24</c>. Les deux automates
    /// communiquent par cette seule globale.</para>
    /// </summary>
    private static void DepotState61DeviceWait()
    {
        byte* panel = MenuDescriptor(0);

        if ((*(ushort*)(s_Unknown_005dff38 + 2) & 0x4000) != 0)
        {
            s_Unknown_005dff7c = 0x5f;
            return;
        }

        if (s_Unknown_005dff24 == -3)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x3cc),
                (uint)(nint)s_Unknown_005dff3c, panel, 0);

            panel[0x8c7] &= 0xf9;
            panel[0x3b] = 0;
            panel[0x33] = 0;
            s_Unknown_005dff18 = 0;
            s_Unknown_005dff7c = 5;
            return;
        }

        if ((panel[0x8c7] & 2) != 0) return;

        Saves.PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
        Saves.ResetCardWriteEvents();
        s_Unknown_005dff18 = 0;
        s_Unknown_005dff7c = 4;                          // Ghidra: goto LAB_00540dab
    }

    /// <summary>
    /// Ghidra: <c>case -5</c> et <c>case -4</c> — <b>le retour au choix de peripherique</b>, avec et
    /// sans fin de transfert. <c>Blaze.c</c> 89273..89282.
    ///
    /// <para>Le <c>-5</c> <b>tombe dans</b> le <c>-4</c>, comme le <c>0x5f</c> tombe dans le
    /// <c>0x60</c> : meme construction, meme raison. Deux etats pour distinguer « il y avait un
    /// transfert en cours » de « il n'y en avait pas », et le premier n'ajoute que la fin de
    /// transfert.</para>
    ///
    /// <para><b>Ils reposent <c>DAT_0056dc90</c> a -1</b>, et c'est tout leur objet : le prologue de
    /// <see cref="RunDepotMode0"/> ne s'execute que lorsque cette globale est negative. La remettre
    /// a -1 rearme donc le choix de peripherique au tour suivant — c'est le seul chemin de retour
    /// vers ce choix une fois qu'on en est sorti.</para>
    /// </summary>
    private static void DepotStateMinus4Rearm(bool finishTransfer)
    {
        if (finishTransfer)
        {
            Saves.PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
            Saves.ResetCardWriteEvents();
        }

        byte* panel = MenuDescriptor(0);
        panel[0x3b] = 0;
        panel[0x8c7] &= 0xf9;
        panel[0x33] = 0;

        s_Unknown_0056dc90 = -1;
        Aud.PlayUiSound39();
    }

    /// <summary>
    /// Ghidra: <c>case -3</c> et <c>case -2</c> — <b>l'attente d'une carte absente</b>.
    /// <c>Blaze.c</c> 89283..89298. Les deux cas partagent un corps.
    ///
    /// <para>Annuler renvoie a l'etat <c>-5</c>. Sinon, une erreur franche du peripherique
    /// (<c>DAT_005dff24 == -3</c>) ramene a l'etat 0 en remettant l'attente a l'arret ; et faute
    /// d'erreur, on attend que le message soit consomme avant de terminer le transfert et de
    /// rearmer le choix de peripherique.</para>
    ///
    /// <para>Noter que cet etat rearme le choix <b>sans</b> passer par <c>-4</c> : il refait les
    /// deux appels de fin de transfert lui-meme et repose <c>DAT_0056dc90</c> directement. Le code
    /// est duplique dans l'original, pas factorise.</para>
    /// </summary>
    private static void DepotStateMinus2WaitCard()
    {
        byte* panel = MenuDescriptor(0);

        if ((*(ushort*)(s_Unknown_005dff38 + 2) & 0x4000) != 0)
        {
            s_Unknown_005dff7c = unchecked((uint)-5);
            return;
        }

        if (s_Unknown_005dff24 == -3)
        {
            s_Unknown_005dff18 = 0;
            s_Unknown_005dff7c = 0;
            return;
        }

        if ((panel[0x8c7] & 2) != 0) return;

        Saves.PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
        Saves.ResetCardWriteEvents();
        s_Unknown_0056dc90 = -1;
    }

    /// <summary>
    /// Ghidra: <c>case -1</c> — <b>le plus court de l'automate</b>, et le dernier de la liste :
    /// des que le message en cours est consomme, il rearme le choix de peripherique.
    /// <c>Blaze.c</c> 89299..89302.
    ///
    /// <para>Il n'a pas de <c>break</c> dans l'original — il est simplement le dernier cas avant
    /// l'accolade fermante du repartiteur, donc rien ne suit.</para>
    /// </summary>
    private static void DepotStateMinus1Rearm()
    {
        if ((MenuDescriptor(0)[0x8c7] & 2) == 0) s_Unknown_0056dc90 = -1;
    }

    /// <summary>
    /// Ghidra: <c>case 0</c> de l'automate du coffre — <b>le choix du peripherique est valide</b>.
    /// <c>Blaze.c</c> 88320..88331.
    ///
    /// <para>Deux chemins selon que le panneau a un message a l'ecran ou non. S'il n'en a pas, la
    /// ligne visee designe un identifiant de peripherique dans <see cref="s_DepotDeviceIds_0056dc20"/>
    /// et on passe a l'etat 1. S'il en a un, on l'eteint et on affiche celui de <c>+0x3cc</c>.</para>
    ///
    /// <para><b>L'identifiant n'ecrase qu'un octet.</b> Ghidra rend l'affectation par
    /// <c>CONCAT31(DAT_00604980._1_3_, x)</c> : les trois octets hauts sont relus et reecrits tels
    /// quels. La globale est un octet dans la reconstruction, donc l'affectation est directe.</para>
    /// </summary>
    private static void DepotState0PickDevice()
    {
        byte* panel = MenuDescriptor(0);

        if (panel[0x8ac] == 0)                            // Ghidra: DAT_00568ddc
        {
            s_Unknown_005dff7c = 1;
            s_CardSlotIndex_00604980 =
                s_DepotDeviceIds_0056dc20[s_Unknown_005dff58][s_Unknown_005dff64];
            return;
        }

        panel[0x8c7] &= 0xfd;
        panel[0x33] = 0;
        BindMenuLabels(
            *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x3cc),
            (uint)(nint)s_Unknown_005dff3c, panel, 0);
    }

    /// <summary>
    /// Ghidra: <c>case 1</c> de l'automate du coffre — <b>l'ouverture du peripherique</b>.
    /// <c>Blaze.c</c> 88332..88422.
    ///
    /// <para>Il relit le repertoire de sauvegarde et se branche sur les quatre valeurs que
    /// <c>FUN_005126c0</c> peut rendre :</para>
    ///
    /// <list type="table">
    /// <item><term>0</term><description>le repertoire est la. On tente d'ouvrir le fichier du
    /// coffre ; s'il s'ouvre, on affiche « lecture en cours », on arme l'attente de peripherique et
    /// on passe a l'etat 2. Sinon on compose le message d'absence.</description></item>
    /// <item><term>-1</term><description>meme message d'absence, sans avoir tente
    /// l'ouverture.</description></item>
    /// <item><term>-2</term><description>libelle <c>+0x3d4</c>, attente de type 2, etat
    /// <c>-3</c>.</description></item>
    /// <item><term>-3</term><description>libelle <c>+0x3d0</c>, et sortie par la queue commune qui
    /// pose l'etat <c>-1</c>.</description></item>
    /// </list>
    ///
    /// <para><b>Le message d'absence est compose sur place</b>, en collant bout a bout trois
    /// chaines de glyphes de seize bits : un prefixe du repertoire principal (<c>+0x3f4</c>), le nom
    /// du personnage (<c>+0x100</c> de son bloc) et un suffixe (<c>+0x3f8</c>). C'est la meme
    /// composition, mot pour mot, dans les cas 0 et -1 — l'original la duplique, et
    /// <see cref="ComposeDepotDeviceMessage"/> la factorise ici parce que c'est une unite
    /// reellement independante.</para>
    ///
    /// <para><b>Le <c>goto switchD_0053eaef_caseD_a</c> n'est pas un abandon</b> : il saute
    /// par-dessus la fin du repartiteur pour tomber directement sur la queue d'affichage. Rendu ici
    /// par un simple retour, la queue etant appelee par
    /// <see cref="RunDepotMode0"/> dans tous les cas.</para>
    /// </summary>
    private static void DepotState1OpenDevice()
    {
        s_CardCursorSlot_00604984 = 0;
        s_Unknown_005dff34 = LoadSaveDirectory();         // Ghidra: FUN_005126c0(0)

        byte* panel = MenuDescriptor(0);
        byte* labels = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* message;

        switch (s_Unknown_005dff34)
        {
            case 0:
                Saves.SetCardFileName(s_Unknown_005dff14 + 0x1fd0);
                Saves.SetCardPathDeviceFields();
                s_Unknown_005dff04 = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                Saves.RetryCloseCardFile(s_Unknown_005dff04);

                if ((nint)s_Unknown_005dff04 >= 0)
                {
                    BindMenuLabels(
                        *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70) + 0x108),
                        (uint)(nint)s_Unknown_005dff3c, panel, 0);
                    Saves.ResetCardTransferStatus(
                        (s_CardSlotIndex_00604980 & 3) + (s_CardSlotIndex_00604980 & 4) * 4);
                    s_Unknown_005dff7c++;
                    s_Unknown_005dff18 = 1;
                    return;                               // Ghidra: goto switchD_0053eaef_caseD_a
                }

                ComposeDepotDeviceMessage(labels);
                message = s_DepotMessage_005dfe50;
                break;

            case -1:
                ComposeDepotDeviceMessage(labels);
                message = s_DepotMessage_005dfe50;
                break;

            case -2:
                BindMenuLabels(*(byte**)(labels + 0x3d4),
                               (uint)(nint)s_Unknown_005dff3c, panel, 0);
                Saves.ResetCardTransferStatus(
                    (s_CardSlotIndex_00604980 & 3) + (s_CardSlotIndex_00604980 & 4) * 4);
                s_Unknown_005dff18 = 2;
                s_Unknown_005dff7c = unchecked((uint)-3);
                return;                                   // Ghidra: goto switchD_0053eaef_caseD_a

            case -3:
                message = *(byte**)(labels + 0x3d0);       // Ghidra: goto LAB_0053eefa
                break;

            default:
                return;                                   // Ghidra: goto switchD_0053eaef_caseD_a
        }

        // Ghidra: LAB_0053eefa — la queue commune aux trois chemins qui parlent.
        BindMenuLabels(message, (uint)(nint)s_Unknown_005dff3c, panel, 0);
        s_Unknown_005dff7c = unchecked((uint)-1);
    }

    /// <summary>
    /// Ghidra: les trois boucles de copie que les cas 0 et <c>-1</c> de
    /// <see cref="DepotState1OpenDevice"/> repetent a l'identique — <b>le message d'absence de
    /// peripherique</b>.
    ///
    /// <para>Trois chaines de <b>glyphes de seize bits</b> collees bout a bout : un prefixe pris
    /// dans le repertoire principal, le nom du personnage, un suffixe. Les boucles avancent de deux
    /// en deux et s'arretent sur un octet de tete nul, ce qui est la convention de ce jeu pour ses
    /// chaines larges — pas un NUL de seize bits.</para>
    ///
    /// <para>Le terminateur final fait <b>deux octets</b>, ecrits separement dans l'original.</para>
    /// </summary>
    private static void ComposeDepotDeviceMessage(byte* labels)
    {
        s_Unknown_005dff08 = s_DepotMessage_005dfe50;

        CopyWideGlyphs(*(byte**)(labels + 0x3f4));
        CopyWideGlyphs(s_Unknown_005dff14 + 0x100);
        CopyWideGlyphs(*(byte**)(labels + 0x3f8));

        s_Unknown_005dff08[0] = 0;
        s_Unknown_005dff08[1] = 0;
    }

    /// <summary>Une des trois copies de <see cref="ComposeDepotDeviceMessage"/>.</summary>
    private static void CopyWideGlyphs(byte* source)
    {
        while (*source != 0)
        {
            s_Unknown_005dff08[0] = source[0];
            s_Unknown_005dff08[1] = source[1];
            source += 2;
            s_Unknown_005dff08 += 2;
        }
    }

    /// <summary>
    /// Ghidra: <c>case 5</c> de l'automate du coffre — <b>la reouverture avant ecriture</b>.
    /// <c>Blaze.c</c> 88895..88963.
    ///
    /// <para>C'est le jumeau de <see cref="DepotState1OpenDevice"/>, a trois differences pres : le
    /// libelle de succes vient de <c>+0x3fc</c> et non de la ressource de zone, l'attente armee est
    /// du type <b>99</b> et non 1, et le cas <c>-1</c> pose l'etat 99 au lieu de <c>-1</c>. Le cas
    /// <c>0</c> <b>tombe dans</b> le cas <c>-1</c> quand l'ouverture echoue — <c>break</c> manquant
    /// compris, comme <c>0x5f</c> dans <c>0x60</c>.</para>
    ///
    /// <para><b>Et il ne fait rien du tout tant que l'etat vient de changer</b> : la branche
    /// <c>DAT_005dff84 != 0</c> se contente de remettre le magasin d'avant la tentative
    /// (<c>0x1b0000</c> vers <c>0x1b4000</c>, 0xdc0 dwords). C'est la copie que l'etat 99 remettra
    /// dans l'autre sens si l'ecriture echoue — les deux forment le point de reprise.</para>
    /// </summary>
    private static void DepotState5Reopen()
    {
        if (s_Unknown_005dff84 != 0)
        {
            s_Unknown_005dff84 = 0;
            byte* mm = GetMainMemPtr();
            Mem.CopyDwords((uint*)(mm + 0x1b0000), (uint*)(mm + 0x1b4000), 0xdc0);
            return;
        }

        s_CardCursorSlot_00604984 = 0;
        s_Unknown_005dff34 = LoadSaveDirectory();

        byte* panel = MenuDescriptor(0);
        byte* labels = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);

        switch (s_Unknown_005dff34)
        {
            case 0:
                Saves.SetCardFileName(s_Unknown_005dff14 + 0x1fd0);
                Saves.SetCardPathDeviceFields();
                s_Unknown_005dff04 = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                Saves.RetryCloseCardFile(s_Unknown_005dff04);

                if ((nint)s_Unknown_005dff04 >= 0)
                {
                    BindMenuLabels(*(byte**)(labels + 0x3fc),
                                   (uint)(nint)s_Unknown_005dff3c, panel, 0);
                    Saves.ResetCardTransferStatus(
                        (s_CardSlotIndex_00604980 & 3) + (s_CardSlotIndex_00604980 & 4) * 4);
                    s_Unknown_005dff7c++;
                    s_Unknown_005dff18 = 99;
                    return;
                }

                // Ghidra: pas de `break` — le cas 0 tombe dans le cas -1.
                goto case -1;

            case -1:
                ComposeDepotDeviceMessage(labels);
                BindMenuLabels(s_DepotMessage_005dfe50,
                               (uint)(nint)s_Unknown_005dff3c, panel, 0);
                s_Unknown_005dff18 = 99;
                s_Unknown_005dff7c = 99;
                return;

            case -3:
                BindMenuLabels(*(byte**)(labels + 0x3d0),
                               (uint)(nint)s_Unknown_005dff3c, panel, 0);
                s_Unknown_005dff7c = 99;
                return;

            case -2:
                BindMenuLabels(*(byte**)(labels + 0x3d4),
                               (uint)(nint)s_Unknown_005dff3c, panel, 0);
                Saves.ResetCardTransferStatus(
                    (s_CardSlotIndex_00604980 & 3) + (s_CardSlotIndex_00604980 & 4) * 4);
                s_Unknown_005dff18 = 2;
                s_Unknown_005dff7c = 0x61;
                return;

            default:
                return;                                   // l'original n'a pas de `default`
        }
    }

    /// <summary>
    /// Ghidra: <c>case 8</c> de l'automate du coffre — <b>l'ecriture</b>, et le seul etat qui touche
    /// le disque. <c>Blaze.c</c> 89002..89213, le deuxieme plus gros des vingt.
    ///
    /// <para>Cinq temps :</para>
    /// <list type="number">
    /// <item><b>compacter ce qu'on garde</b> vers l'inventaire du joueur, en corrigeant au passage
    /// les cinq octets d'equipement de <c>+0x2b0</c> qui designaient les anciens indices ;</item>
    /// <item><b>compacter le reste</b> vers le magasin, et vider la queue ;</item>
    /// <item><b>completer le bloc de sauvegarde</b> — le niveau colle au titre, huit banques
    /// globales, huit reperes de carte, l'icone capturee depuis la VRAM ;</item>
    /// <item><b>fusionner la table d'icones</b> du fichier avec les vingt-deux entrees du magasin,
    /// recalculer la somme de controle et ecrire en trois morceaux ;</item>
    /// <item>selon l'issue, afficher le libelle de succes et passer a l'etat suivant, ou celui
    /// d'echec et repartir a l'etat 99.</item>
    /// </list>
    ///
    /// <para><b>Le titre se termine par un mot sentinelle.</b> La boucle remonte depuis
    /// <c>+0x44</c> jusqu'a trouver <c>0x6b82</c> et ecrit les chiffres apres lui — les memes glyphes
    /// de seize bits que <c>BuildAndWriteSaveBlock</c> emploie, et pour cause : la moitie basse de
    /// cet etat est <b>la meme sequence que l'ecran de sauvegarde du menu</b>, emise deux fois par
    /// le compilateur. Les deux corps restent distincts ici parce qu'ils le sont dans le binaire, et
    /// que leurs globales de travail different.</para>
    ///
    /// <para><b>La somme de controle se calcule sur 0x1ffc octets signes</b> — une addition d'octets
    /// <b>signes</b>, ce que Ghidra rend par <c>(int)(char)*p</c>. Un octet a 0x80 y compte donc pour
    /// -128, pas pour 128 ; se tromper la rendrait tout fichier illisible sans que rien ne le
    /// dise.</para>
    ///
    /// <para><b>Trois ecritures, un seul verdict.</b> Les trois retours de
    /// <c>FUN_00504c20</c> sont combines par <b>ou binaire</b> et le resultat teste une seule fois :
    /// il suffit qu'un bit de signe apparaisse pour que l'ensemble soit declare en echec. Transcrit
    /// tel quel.</para>
    /// </summary>
    private static void DepotState8Write()
    {
        if (s_Unknown_005dff84 != 0)
        {
            s_Unknown_005dff84 = 0;
            return;
        }

        byte* mainMem = GetMainMemPtr();
        byte* player = s_Unknown_005dff14;
        byte* store = mainMem + 0x1b0000;

        Saves.SetCardFileName(player + 0x1fd0);
        Saves.SetCardPathDeviceFields();

        CompactKeptItemsIntoInventory(mainMem, player, store);
        CompactRemainderIntoStore(mainMem, player, store);
        FillDepotSaveBlock(player);

        byte* labels = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* message = WriteDepotSaveFile(mainMem, player, labels, out bool written);

        BindMenuLabels(message, (uint)(nint)s_Unknown_005dff3c, MenuDescriptor(0), 0);

        // **Les deux issues ne posent PAS le meme etat**, et s'y tromper est ce qui a corrompu une
        // sauvegarde le 2026-08-25 : le succes avance d'un cran (8 -> 9, la sortie), l'echec part a
        // l'etat 99, qui remet le magasin d'avant et repasse par l'ecriture. Poser 99 apres une
        // ecriture reussie boucle donc indefiniment en reecrivant le fichier a chaque tour.
        if (written)
        {
            s_Unknown_005dff7c++;
            s_Unknown_005dff20 = 1;
        }
        else
        {
            s_Unknown_005dff7c = 99;
        }

        s_Unknown_005dff18 = 0;
    }

    /// <summary>
    /// Le premier temps de <see cref="DepotState8Write"/> : <b>ce qu'on garde part dans
    /// l'inventaire</b>, dans l'ordre ou les emplacements se presentent.
    ///
    /// <para>Chaque objet garde est recopie a la place suivante de l'inventaire (0xa0 dwords), et
    /// les cinq octets d'equipement de <c>+0x2b0</c> qui designaient son ancien indice sont
    /// reecrits avec le nouveau. Sans cette correction, refermer le coffre desequiperait ce que le
    /// joueur portait.</para>
    ///
    /// <para>Les emplacements restants sont vides, puis la liste des occupes est refaite et les
    /// icones du groupe renvoyees en VRAM.</para>
    /// </summary>
    private static void CompactKeptItemsIntoInventory(byte* mainMem, byte* player, byte* store)
    {
        _ = mainMem;

        int destination = 0;
        s_Unknown_005dfe40 = 0;

        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < s_Unknown_005dfe44; s_Unknown_005dff34++)
        {
            byte slot = s_DepotSlots_005dff40[s_Unknown_005dff34];
            if (s_DepotKeep_005dfed0[slot] == 0) continue;

            Mem.CopyDwords((uint*)(store + slot * 0x280),
                           (uint*)(player + destination * 0x280 + 0x300), 0xa0);

            // Les cinq octets d'equipement suivent l'objet a son nouvel indice.
            s_Unknown_005dff2c = player + 0x2b0;
            for (s_Unknown_005dff1c = 5; s_Unknown_005dff1c != 0;
                 s_Unknown_005dff1c--, s_Unknown_005dff2c++)
            {
                if (*s_Unknown_005dff2c == slot) *s_Unknown_005dff2c = (byte)s_Unknown_005dfe40;
            }

            destination = (int)s_Unknown_005dfe40 + 1;
            s_Unknown_005dff1c = 0;
            s_Unknown_005dfe40 = (uint)destination;
        }

        while (destination < 10)
        {
            player[destination * 0x280 + 0x300] = 0;
            destination = (int)s_Unknown_005dfe40 + 1;
            s_Unknown_005dfe40 = (uint)destination;
        }

        player[0x2c0] = 0;
        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 10; s_Unknown_005dff34++)
        {
            if (player[s_Unknown_005dff34 * 0x280 + 0x300] == 0) continue;
            player[player[0x2c0] + 0x2c1] = (byte)s_Unknown_005dff34;
            player[0x2c0]++;
        }

        UploadPartyIconTextures();                        // Ghidra: FUN_00535500
    }

    /// <summary>
    /// Le deuxieme temps de <see cref="DepotState8Write"/> : <b>ce qu'on ne garde pas reste au
    /// coffre</b>, compacte au debut du magasin, et la queue est videe jusqu'a douze.
    /// </summary>
    private static void CompactRemainderIntoStore(byte* mainMem, byte* player, byte* store)
    {
        _ = player;

        int destination = 0;
        s_Unknown_005dfe40 = 0;

        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < s_Unknown_005dfe44; s_Unknown_005dff34++)
        {
            byte slot = s_DepotSlots_005dff40[s_Unknown_005dff34];
            if (s_DepotKeep_005dfed0[slot] != 0) continue;

            Mem.CopyDwords((uint*)(store + slot * 0x280),
                           (uint*)(store + destination * 0x280), 0xa0);
            destination = (int)s_Unknown_005dfe40 + 1;
            s_Unknown_005dfe40 = (uint)destination;
        }

        s_Unknown_005dfee8 = store + destination * 0x280;
        while (destination < 0xc)
        {
            *s_Unknown_005dfee8 = 0;
            s_Unknown_005dfee8 += 0x280;
            s_Unknown_005dfe40++;
            destination = (int)s_Unknown_005dfe40;
        }

        _ = mainMem;
    }

    /// <summary>
    /// Le troisieme temps de <see cref="DepotState8Write"/> : <b>le bloc de sauvegarde est
    /// complete</b> — le niveau colle au titre, huit banques globales, huit reperes, l'icone.
    ///
    /// <para>Le nombre d'heures de jeu (<c>+0x144</c>) est rendu en chiffres par descente des dix
    /// puissances de dix : un chiffre n'est emis que si son quotient est non nul <b>ou</b> si c'est
    /// le dernier, ce qui supprime les zeros de tete tout en garantissant au moins un chiffre.</para>
    ///
    /// <para>La capture de l'icone passe par <c>Ordinal_684</c>, encadree de deux
    /// <c>Ordinal_667</c> — un talon partage qui ne fait rien dans cette version de la DLL, conserve
    /// parce que rien ne dit qu'il en sera toujours ainsi.</para>
    /// </summary>
    private static void FillDepotSaveBlock(byte* player)
    {
        // Le mot sentinelle : on remonte depuis +0x44 jusqu'a le trouver.
        byte* cursor = player + 0x44;
        while (*(short*)cursor != (short)s_TitleSentinel_0056ac2c) cursor -= 2;

        s_Unknown_005dff1c = *(ushort*)(player + 0x144);
        s_Unknown_005dfe18 = s_PowersOfTen_00566be8;
        s_Unknown_005dfe40 = 0;
        s_Unknown_005dff08 = cursor;

        byte* digits = *(byte**)(*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x70) + 4);

        for (s_Unknown_005dff34 = 10; s_Unknown_005dff34 != 0;
             s_Unknown_005dff34--, s_Unknown_005dfe18++)
        {
            s_Unknown_005dff0c = (int)(s_Unknown_005dff1c / *s_Unknown_005dfe18);
            s_Unknown_005dff1c -= (int)(*s_Unknown_005dfe18 * (uint)s_Unknown_005dff0c);

            if (s_Unknown_005dff0c != 0 || s_Unknown_005dff34 == 1) s_Unknown_005dfe40++;
            if (s_Unknown_005dfe40 == 0) continue;

            s_Unknown_005dff08 = cursor + 2;
            *(ushort*)s_Unknown_005dff08 = *(ushort*)(digits + 0x20 + s_Unknown_005dff0c * 2);
            cursor = s_Unknown_005dff08;
        }

        player[0x1f00] = 0;
        player[0x1f01] = s_Unknown_00566c84;
        player[0x1f02] = s_Unknown_00566c88;
        player[0x1f03] = *(byte*)Mem.Camera(0x005de044);

        // Les huit banques globales, seize dwords chacune.
        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 0x10; s_Unknown_005dff34++)
        {
            int at = s_Unknown_005dff34 * 4;
            *(uint*)(player + at + 0x1d00) = *(uint*)(Mem.Arena(0x005e96e0) + at);
            *(uint*)(player + at + 0x1d40) = *(uint*)(Mem.Arena(0x005e9780) + at);
            *(uint*)(player + at + 0x1d80) = *(uint*)(Mem.Arena(0x005e9060) + at);
            *(uint*)(player + at + 0x1dc0) = *(uint*)(Mem.Arena(0x005e9840) + at);
            *(uint*)(player + at + 0x1e00) = *(uint*)(Mem.Arena(0x005e9720) + at);
            *(uint*)(player + at + 0x1e40) = *(uint*)(Mem.Arena(0x005e92c0) + at);
            *(uint*)(player + at + 0x1e80) = *(uint*)(Mem.Arena(0x005e9980) + at);
            *(uint*)(player + at + 0x1ec0) = *(uint*)(Mem.Arena(0x005e9360) + at);
        }

        // Les huit reperes de carte, trois shorts chacun sur un pas de huit octets.
        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 8; s_Unknown_005dff34++)
        {
            byte* marker = Mem.Arena(0x005e9320) + s_Unknown_005dff34 * 0x10;
            *(ushort*)(player + s_Unknown_005dff34 * 8 + 0x1f04) = *(ushort*)marker;
            *(ushort*)(player + s_Unknown_005dff34 * 8 + 0x1f06) = *(ushort*)(marker + 2);
            *(ushort*)(player + s_Unknown_005dff34 * 8 + 0x1f08) = *(ushort*)(marker + 4);
        }

        // L'icone, capturee depuis la VRAM. La page se derive du portrait du personnage.
        // Ghidra rend `DAT_005dff3c[1]` sur un pointeur de DWORD : c'est l'octet de +4, pas de +1.
        // Verifie au desassembleur — `movzx cx,byte [eax+4]` en 0x005407f3.
        s_DepotIconRect_005dfe38[0] = (short)((s_Unknown_005dff3c[0x0b] + s_Unknown_005dff3c[4] * 4) * 0x10);
        s_DepotIconRect_005dfe38[2] = 0x10;
        s_DepotIconRect_005dfe38[3] = 1;
        s_DepotIconRect_005dfe38[1] =
            (short)(*(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0x92 + s_Unknown_005dff3c[4] * 4) >> 6);

        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_684(s_DepotIconRect_005dfe38, player + 0x60);
        Winsys3d.Ordinal_667(0);
    }

    /// <summary>
    /// Le quatrieme temps de <see cref="DepotState8Write"/> : <b>l'ecriture proprement dite</b>.
    ///
    /// <para>Le fichier est d'abord relu pour verifier que sa somme de controle correspond ; si elle
    /// ne correspond pas, on repart avec le libelle <c>+0x40c</c> sans rien ecrire. Sinon la somme
    /// du bloc est recalculee sur <c>0x1ffc</c> octets <b>signes</b>, les trois morceaux sont ecrits,
    /// et la table d'icones du fichier recoit les vingt-deux entrees du magasin — chacune ajoutee
    /// seulement si aucune ligne identique n'existe deja.</para>
    ///
    /// <para><b>Le drapeau de succes est pose ici</b> (<c>DAT_005dff20 = 1</c>), et c'est lui que
    /// l'etat 9 consulte pour savoir s'il doit remettre les objets d'avant. Poser ce drapeau trop
    /// tot rendrait la sortie destructrice.</para>
    /// </summary>
    /// <returns>Le libelle a afficher.</returns>
    private static byte* WriteDepotSaveFile(byte* mainMem, byte* player, byte* labels,
                                            out bool written)
    {
        written = false;

        s_Unknown_005dff04 = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 3);
        s_Unknown_005dff34 = Saves.RetrySeekAndReadCardFile(
            s_Unknown_005dff04, mainMem + 0x1b8000, 0x3f00, 0x100);
        Saves.RetryCloseCardFile(s_Unknown_005dff04);

        if (s_Unknown_005dff34 < 0) return *(byte**)(labels + 0x408);
        if (*(int*)(mainMem + 0x1b80fc) != *(int*)(player + 0x1ffc))
        {
            return *(byte**)(labels + 0x40c);
        }

        // La somme, sur des octets SIGNES.
        int sum = 0;
        byte* p = player;
        for (int n = 0x1ffc; n != 0; n--, p++) sum += (sbyte)*p;
        *(uint*)(player + 0x1ffc) = (uint)sum;

        s_Unknown_005dff04 = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 3);
        int verdict = 0;
        verdict |= Saves.RetrySeekAndWriteCardFile(s_Unknown_005dff04, player, 0, 0x2000);
        verdict |= Saves.RetrySeekAndWriteCardFile(s_Unknown_005dff04, mainMem + 0x1b0000, 0x2000, 0x1f00);
        verdict |= Saves.RetrySeekAndWriteCardFile(s_Unknown_005dff04, player + 0x1f00, 0x3f00, 0x100);
        Saves.RetryCloseCardFile(s_Unknown_005dff04);

        if (verdict < 0) return *(byte**)(labels + 0x408);

        s_Unknown_005dff04 = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 3);
        s_Unknown_005dff34 = Saves.RetrySeekAndReadCardFile(
            s_Unknown_005dff04, mainMem + 0x1b8000, 0x4000, 0x2000);

        if (s_Unknown_005dff34 < 0)
        {
            Saves.RetryCloseCardFile(s_Unknown_005dff04);
            return *(byte**)(labels + 0x408);
        }

        MergeStoreIconsIntoFile(mainMem);

        s_Unknown_005dff34 = Saves.RetrySeekAndWriteCardFile(
            s_Unknown_005dff04, mainMem + 0x1b8000, 0x4000, 0x2000);
        Saves.RetryCloseCardFile(s_Unknown_005dff04);

        if (s_Unknown_005dff34 < 0) return *(byte**)(labels + 0x408);

        written = true;
        return *(byte**)(labels + 0x404);
    }

    /// <summary>
    /// La fusion de la table d'icones — voir <see cref="WriteDepotSaveFile"/>.
    ///
    /// <para>Les vingt-deux emplacements du magasin sont parcourus ; ceux qui portent
    /// <c>0x8000</c> sans <c>0x4000</c> a <c>+0x12</c> cherchent leur ligne dans la table du fichier
    /// en comparant <b>quatre dwords</b>, et ne s'y ajoutent que si aucune ne correspond. La table
    /// se termine sur une ligne dont le premier octet est nul.</para>
    /// </summary>
    private static void MergeStoreIconsIntoFile(byte* mainMem)
    {
        s_Unknown_005dfee8 = mainMem + 0x1b0000;

        for (s_Unknown_005dff34 = 0; s_Unknown_005dff34 < 0x16;
             s_Unknown_005dff34++, s_Unknown_005dfee8 += 0x280)
        {
            ushort flags = *(ushort*)(s_Unknown_005dfee8 + 0x12);
            if ((flags & 0x4000) != 0 || (flags & 0x8000) == 0) continue;

            s_Unknown_005dff2c = mainMem + 0x1b8000;
            s_Unknown_005dff08 = s_Unknown_005dfee8;

            if (*s_Unknown_005dff2c != 0)
            {
                bool found;
                do
                {
                    s_Unknown_005dfe40 = 0;
                    while (s_Unknown_005dfe40 < 4
                           && *(int*)(s_Unknown_005dfee8 + s_Unknown_005dfe40 * 4)
                              == *(int*)(s_Unknown_005dff2c + s_Unknown_005dfe40 * 4))
                    {
                        s_Unknown_005dfe40++;
                    }

                    found = s_Unknown_005dfe40 == 4;
                    if (found) break;

                    s_Unknown_005dff2c += 0x10;
                }
                while (*s_Unknown_005dff2c != 0);

                if (*s_Unknown_005dff2c != 0) continue;    // Ghidra: goto LAB_00540ae1
            }

            for (s_Unknown_005dfe40 = 0; s_Unknown_005dfe40 < 0x10; s_Unknown_005dfe40++)
            {
                *s_Unknown_005dff2c = s_Unknown_005dfee8[s_Unknown_005dfe40];
                s_Unknown_005dff2c++;
            }
        }
    }
    /// <summary>
    /// Ghidra: le bloc qui suit <c>switchD_0053eaef_caseD_a</c> — <b>la queue d'affichage du
    /// coffre</b>, commune a tous les etats. C'est elle qui dessine le cadre du cliche : le curseur
    /// de peripherique, le portrait, les icones, les quatre lignes de texte et les quatre panneaux.
    ///
    /// <para><b>Le quatrieme descripteur.</b> <c>DAT_00569f88</c> n'est pas une globale :
    /// <c>0x00569f88 - 0x00568530 = 0x1a58</c>, soit exactement <b>trois</b> descripteurs de
    /// <c>0x8c8</c>. C'est <c>MenuDescriptor(3)</c>, et la boucle finale les dessine tous les
    /// quatre, du dernier au premier. Le mode 1 n'en dessine que deux — c'est la difference de
    /// cadre la plus visible entre les deux ecrans.</para>
    ///
    /// <para><b>Le curseur de peripherique ne se dessine qu'a l'etat 4.</b> La condition de
    /// l'original est <c>(DAT_0056dc90 &lt; 0) || (DAT_005dff7c == 4)</c>, et son premier terme est
    /// mort (voir <see cref="RunDepotMode0"/>) : il ne reste que l'etat 4. Sa position se prend en
    /// deux temps — la liste de lignes du peripherique donne un indice, cet indice donne un couple
    /// (x, y) dans <see cref="s_DepotRowPos_0056dc68"/>, decale de <c>0x86</c> et <c>0x48</c>.</para>
    ///
    /// <para><b>Le troisieme libelle est choisi par un octet du personnage</b> (<c>+0x150</c>) dans
    /// une table a <c>+0x320</c> du repertoire, et le quatrieme est precede d'un triplet de mise en
    /// page dont le dernier terme <b>anime</b> : <c>(compteur d'images / 4) % 3</c> ajoute a
    /// <c>+0x151</c> fois trois. Trois images, pas un index fixe.</para>
    /// </summary>
    private static uint* DrawDepotFrame(uint* otBase, uint* prim)
    {
        UiWin.EmitViewportRect(otBase, &prim);              // Ghidra: FUN_0051be20

        // -------------------------------------------------- le curseur de peripherique
        if (s_Unknown_005dff7c == 4)
        {
            *(byte**)s_DepotSprite_005dfe20 =
                *(byte**)*(byte***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(short*)(s_DepotSprite_005dfe20 + 0x10) = 0x19;

            byte row = s_DepotRows_0056dc58[s_Unknown_005dff58][1 + s_Unknown_005dff64];
            *(short*)(s_DepotSprite_005dfe20 + 8) = (short)(s_DepotRowPos_0056dc68[row * 2] + 0x86);
            *(uint*)(s_DepotSprite_005dfe20 + 4) = 0x1000000;
            *(short*)(s_DepotSprite_005dfe20 + 0x0a) =
                (short)(s_DepotRowPos_0056dc68[row * 2 + 1] + 0x48);

            prim = Prims.DrawSpriteVariant(otBase, prim, (int*)s_DepotSprite_005dfe20, 0, 0);
            prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20);
        }

        // -------------------------------------------------- le portrait du personnage
        byte* portrait = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 4);
        *(byte**)s_DepotSprite_005dfe20 = portrait;
        portrait[5] = 0;

        // Meme piege : `DAT_005dff3c[1]` est l'octet de +4 (pointeur de dword cote Ghidra).
        *(short*)(s_DepotSprite_005dfe20 + 0x10) = s_Unknown_005dff3c[4];
        *(short*)(s_DepotSprite_005dfe20 + 8) = 0x20;
        *(short*)(s_DepotSprite_005dfe20 + 0x0a) = 0x18;
        *(uint*)(s_DepotSprite_005dfe20 + 4) = 0x1000000;

        prim = Prims.DrawSpriteVariant(otBase, prim, (int*)s_DepotSprite_005dfe20, 0,
                   s_Unknown_005dff3c[0x0b]);
        prim = Prims.DrawFlatPrimitive(otBase, prim, (int*)s_DepotSprite_005dfe20);
        UiWin.EmitClipRect(otBase, &prim);                  // Ghidra: FUN_0051bdb0

        prim = DrawIconList_005412F0(otBase, prim, s_DepotIcons_0056dca0);

        // -------------------------------------------------- les quatre lignes de texte
        byte* labels = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* panel3 = MenuDescriptor(3);

        BindMenuLabels(*(byte**)(labels + 0x360), (uint)(nint)s_Unknown_005dff3c, panel3, 0);
        prim = Ui.RunTextEngine(prim, panel3);

        // Le nom du personnage, pris tel quel dans son bloc.
        BindMenuLabels(s_Unknown_005dff14 + 0x100, (uint)(nint)s_Unknown_005dff3c, panel3, 0);
        prim = Ui.RunTextEngine(prim, panel3);

        BindMenuLabels(*(byte**)(labels + 800 + s_Unknown_005dff14[0x150] * 4),
                       (uint)(nint)s_Unknown_005dff3c, panel3, 0);
        prim = Ui.RunTextEngine(prim, panel3);

        // Le triplet de mise en page, dont le dernier terme anime sur trois images.
        s_Unknown_005e909c = 1;
        s_Unknown_005e909d = 8;
        s_Unknown_005e909e = (byte)((s_FadeCounter_005e9960 >> 2 & 0x1fffffff) % 3
                                    + s_Unknown_005dff14[0x151] * 3u);

        BindMenuLabels(*(byte**)(labels + 0x368), (uint)(nint)s_Unknown_005dff3c, panel3, 0);
        prim = Ui.RunTextEngine(prim, panel3);

        // -------------------------------------------------- les quatre panneaux
        // Ghidra: _DAT_00568536 et _DAT_0056853a, soit MenuDescriptor(0) + 6 et + 0x0a.
        byte* panel0 = MenuDescriptor(0);
        *(short*)(panel0 + 6) = unchecked((short)0xa0);
        *(short*)(panel0 + 0x0a) = 0x28;
        prim = Ui.RunTextEngine(prim, panel0);

        // Ghidra: _DAT_00568e00, _DAT_00568e02 et DAT_00568e1b — MenuDescriptor(1) + 8, + 0x0a, + 0x23.
        byte* panel1 = MenuDescriptor(1);
        *(short*)(panel1 + 8) = 0x48;
        *(short*)(panel1 + 0x0a) = 0x40;
        panel1[0x23] = 0;

        for (s_Unknown_005dff34 = 3; s_Unknown_005dff34 >= 0; s_Unknown_005dff34--)
        {
            prim = UiWin.DrawUiPanel(otBase, prim, MenuDescriptor(s_Unknown_005dff34));
        }

        return prim;
    }
}
