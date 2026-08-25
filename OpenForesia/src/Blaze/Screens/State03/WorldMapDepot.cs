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

    /// <summary>
    /// Ghidra: FUN_0053e0a0 @ 0x0053e0a0 — <b>le panneau du coffre et celui de la carte
    /// memoire</b>, selon le mode que <c>DAT_005de548</c> porte. 1359 lignes de pseudo-C
    /// (<c>Blaze.c</c> 87975..89333), et ce sont <b>deux ecrans</b>, pas un.
    ///
    /// <para><b>Mode 1 — le coffre</b> (verbe 58) : reprendre un objet depose. C'est la branche
    /// transcrite ici, 195 lignes, et c'est celle du coffre noir de la chambre.</para>
    ///
    /// <para><b>Mode 0 — la carte memoire</b> (verbe 57) : sauvegarder et charger. Les 1150 lignes
    /// restantes, avec leur propre automate de sous-etats, l'ouverture de fichier, le formatage et
    /// les messages d'erreur. <b>Non transcrite</b> — elle leve en se nommant.</para>
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

        if (s_Unknown_005de548 == 0)
        {
            throw new NotImplementedException(
                "FUN_0053e0a0, mode 0 : le panneau de CARTE MEMOIRE (sauvegarde et chargement) " +
                "n'est pas transcrit. C'est le verbe 57 qui pose ce mode ; le verbe 58 pose le " +
                "mode 1, le coffre, qui l'est. Blaze.c 88221..89333, 1150 lignes, automate de " +
                "sous-etats propre. Voir Documentation/Etat/TODO.md.");
        }

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
}
