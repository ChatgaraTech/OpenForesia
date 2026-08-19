// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =======================================================================================
    // Le sous-ecran « Invertaire » — Ghidra: FUN_00524880 @ 0x00524880
    // =======================================================================================

    /// <summary>Ghidra: DAT_005de42c — l'entree de repertoire retenue, DAT_006049bc + ligne * 0x28.</summary>
    private static ref int s_ItemMenuChosenEntry_005de42c => ref *Mem.ItemMenuState(0x005de42c);

    /// <summary>Ghidra: DAT_005de430 — le nombre d'emplacements d'objet occupes, sur les 0x16.</summary>
    private static ref int s_ItemCount_005de430 => ref *Mem.ItemMenuState(0x005de430);

    /// <summary>Ghidra: DAT_005de4d8 — la phase de l'image precedente, comparee a DAT_005de4d4
    /// pour lever DAT_005de4dc a chaque changement.</summary>
    private static ref int s_ItemMenuPrevPhase_005de4d8 => ref *Mem.ItemMenuState(0x005de4d8);

    /// <summary>Ghidra: DAT_005de4dc — « on vient d'entrer dans cette phase ».</summary>
    private static ref int s_ItemMenuEntered_005de4dc => ref *Mem.ItemMenuState(0x005de4dc);

    /// <summary>
    /// Ghidra: DAT_005de4e8 — <b>la meme globale porte deux roles</b>. Dans le sous-ecran
    /// « Enchere » (FUN_005277b0) c'est un drapeau « liste deja chargee »
    /// (<see cref="s_ItemMenuLoaded_005de4e8"/>) ; ici c'est le curseur d'emplacement de carte
    /// memoire, borne a chaque image contre le compte de <see cref="s_CardSlotLists_00566a90"/>.
    /// Les deux sous-ecrans ne coexistent jamais et FUN_00524210 remet la plage a zero a l'entree
    /// du menu, d'ou le partage. Deux noms pour un seul entier, plutot qu'un nom faux sur l'un
    /// des deux sites.
    /// </summary>
    private static ref int s_ItemMenuCardSlot_005de4e8 => ref *Mem.ItemMenuState(0x005de4e8);

    /// <summary>Ghidra: DAT_005de4ec — le decalage de la grille d'objets (phase 9).</summary>
    private static ref int s_ItemGridScroll_005de4ec => ref *Mem.ItemMenuState(0x005de4ec);

    /// <summary>Ghidra: DAT_005de4f0 — le nombre d'objets listes (phase 8), puis la ligne
    /// choisie (phases 9 et suivantes). Encore une globale a deux emplois.</summary>
    private static ref int s_ItemMenuVisible_005de4f0 => ref *Mem.ItemMenuState(0x005de4f0);

    /// <summary>Ghidra: DAT_005de504 — la longueur du nom en cours de saisie (phase 10).</summary>
    private static ref int s_ItemNameLength_005de504 => ref *Mem.ItemMenuState(0x005de504);

    /// <summary>Ghidra: DAT_005de508 — leve des qu'un objet a ete renomme, ce qui fait passer la
    /// sortie par la sauvegarde au lieu du retour direct.</summary>
    private static ref int s_ItemRenamed_005de508 => ref *Mem.ItemMenuState(0x005de508);

    /// <summary>Ghidra: &amp;DAT_005de488 — la liste des index d'emplacement occupes, 0x16 octets.</summary>
    private static byte* s_ItemIndex_005de488 => Mem.ItemMenuBytes(0x005de488);

    /// <summary>Ghidra: &amp;DAT_005de2d8 — un octet par emplacement : « cet objet appartient au
    /// personnage courant ». Six dwords remis a zero d'un bloc.</summary>
    private static byte* s_ItemHeld_005de2d8 => Mem.ItemMenuBytes(0x005de2d8);

    /// <summary>Ghidra: &amp;DAT_005de110 — le tampon ou <see cref="BuildItemDisplayName"/> compose
    /// le libelle affichable d'un objet.</summary>
    private static byte* s_ItemNameScratch_005de110 => Mem.ItemMenuBytes(0x005de110);

    /// <summary>
    /// Ghidra: &amp;PTR_DAT_00566a90 — quatre listes d'emplacements de carte memoire, une par
    /// configuration de manettes branchees (l'index est <see cref="s_Unknown_005de50c"/>, calcule
    /// dans la boucle d'image a partir des deux axes « absents »). Le <b>premier octet</b> de
    /// chaque liste en donne le nombre : c'est tout ce que le prologue et la phase 0xd lisent.
    /// Releve dans <c>Legacy/Blaze.exe</c> a 0x00566a68.
    /// </summary>
    private static readonly byte[][] s_CardSlotLists_00566a90 =
    [
        [2, 0, 1, 0, 0, 0, 0, 0],
        [5, 2, 3, 4, 5, 1, 0, 0],
        [5, 0, 6, 7, 8, 9, 0, 0],
        [8, 2, 3, 4, 5, 6, 7, 8, 9, 0, 0, 0],
    ];

    /// <summary>Ghidra: DAT_005de468 / DAT_005de484 — l'etape du dialogue de carte memoire et le
    /// code de retour de la derniere operation ; <c>-3</c> vaut « carte absente ».</summary>
    private static ref int s_CardDialogStep_005de468 => ref *Mem.ItemMenuState(0x005de468);
    private static ref int s_CardDialogResult_005de484 => ref *Mem.ItemMenuState(0x005de484);

    /// <summary>Ghidra: &amp;DAT_005de390 — le message de carte memoire compose en UTF-16, trois
    /// morceaux mis bout a bout : un prefixe de ressource, le nom du personnage, un suffixe.</summary>
    private static ushort* s_CardMessage_005de390 => (ushort*)Mem.ItemMenuBytes(0x005de390);

    /// <summary>
    /// Ghidra: &amp;PTR_DAT_00566a58 — les identifiants d'emplacement de carte, quatre listes
    /// paralleles a <see cref="s_CardSlotLists_00566a90"/> (relevees a 0x00566a38). C'est cette
    /// table que la phase 0xd indexe pour poser <c>DAT_00604980</c>.
    /// </summary>
    private static readonly byte[][] s_CardSlotIds_00566a58 =
    [
        [0, 4, 0, 0, 0, 0, 0, 0],
        [0, 1, 2, 3, 4, 0, 0, 0],
        [0, 4, 5, 6, 7, 0, 0, 0],
        [0, 1, 2, 3, 4, 5, 6, 7],
    ];

    /// <summary>Ghidra: DAT_005de4fc / DAT_005de500 / DAT_005de4f8 — la case du clavier de
    /// renommage : colonne 0..9, ligne visible 0..7, et page de lignes 0..7. La case
    /// (ligne 7, page 7) est le « fin de saisie ».</summary>
    private static ref int s_KeyColumn_005de4fc => ref *Mem.ItemMenuState(0x005de4fc);
    private static ref int s_KeyRow_005de500 => ref *Mem.ItemMenuState(0x005de500);
    private static ref int s_KeyPage_005de4f8 => ref *Mem.ItemMenuState(0x005de4f8);

    /// <summary>Ghidra: &amp;DAT_005de4a0 — le nom en cours de saisie, 0x10 caracteres au plus.</summary>
    private static byte* s_ItemNameEdit_005de4a0 => Mem.ItemMenuBytes(0x005de4a0);

    /// <summary>Ghidra: DAT_005de428 — l'adresse de la case de clavier visee, gardee entre le
    /// calcul et la lecture du glyphe.</summary>
    private static ref nint s_KeyCellAddress_005de428 => ref *(nint*)Mem.ItemMenuState(0x005de428);

    /// <summary>
    /// Ghidra: DAT_00562c9c — le dword <c>0x00004081</c> releve dans <c>.data</c>, compare au
    /// glyphe de la case visee pour refuser un premier caractere interdit.
    ///
    /// <para><b>La comparaison ne peut pas aboutir telle qu'elle est compilee</b> : a
    /// <c>0x005266A7</c> le glyphe est charge en <c>XOR EBX,EBX / MOV BL,[EAX]</c>, donc borne a
    /// 0..255, et <c>0x005266BF</c> le compare a <c>[0x562c9c] &amp; 0xffff</c> = 0x4081. Le bip de
    /// refus (<see cref="PlayUiSound3B"/>) de cette branche est donc mort. Transcrit tel quel —
    /// c'est un defaut du binaire de 1998, pas de la transcription.</para>
    /// </summary>
    private static readonly uint s_Unknown_00562c9c = 0x00004081;

    /// <summary>Ghidra: DAT_005de4f4 — negatif tant que la ligne courante ne doit pas etre
    /// mise en valeur ; positif, elle est teintee par <see cref="s_Unknown_005e9814"/>.</summary>
    private static ref int s_Unknown_005de4f4 => ref *Mem.ItemMenuState(0x005de4f4);

    /// <summary>Ghidra: &amp;DAT_00566ad0 — l'ordre dans lequel les huit caracteristiques d'un
    /// objet sont affichees : la troisieme d'abord, puis la deuxieme, la premiere, et les cinq
    /// suivantes dans l'ordre. Releve a 0x00566ad0.</summary>
    private static readonly byte* s_StatSlotOrder_00566ad0 = AllocBytes([2, 1, 0, 3, 4, 5, 6, 7]);

    /// <summary>Ghidra: &amp;DAT_00566b08 — huit enregistrements de sprite de huit octets, un par
    /// element ; le champ <c>+0x10</c> de l'enregistrement de dessin sert d'index dans la table
    /// plutot que d'y faire pointer huit adresses distinctes.</summary>
    private static readonly byte* s_ElementIcons_00566b08 = AllocBytes(
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

    /// <summary>Ghidra: &amp;DAT_00566b48 — l'enregistrement de sprite d'une icone d'objet. Ses
    /// deux premiers octets sont **reecrits a chaque trace** avec la position de l'icone dans
    /// l'atlas, comme <see cref="s_RowSpriteRecord_00563148"/> pour les lignes de sauvegarde.</summary>
    private static readonly byte* s_ItemIconSprite_00566b48 =
        AllocBytes([0x00, 0x00, 0x18, 0x18, 0x00, 0x00, 0x3e, 0x00]);

    /// <summary>Ghidra: &amp;DAT_00566b50 — la pastille posee sur un emplacement que le membre
    /// porte reellement. Huit octets releves a 0x00566b50.</summary>
    private static readonly byte* s_EquippedMarker_00566b50 =
        AllocBytes([0xf8, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x03, 0x07]);

    /// <summary>Ghidra: &amp;DAT_00566b80 et &amp;DAT_00566b84 — <c>"???"</c> et <c>"????"</c>, ce
    /// qu'affichent les caracteristiques et le nom d'un objet non identifie (attribut 0x80 du
    /// champ +0x13 eteint).</summary>
    private static readonly byte* s_UnknownStatLabel_00566b80 = ToAnsiZ("???");
    private static readonly byte* s_UnknownItemLabel_00566b84 = ToAnsiZ("????");

    /// <summary>Ghidra: &amp;DAT_00564a0c — la chaine <c>"/"</c>, le separateur du compteur
    /// d'objets.</summary>
    private static readonly byte* s_SlashLabel_00564a0c = ToAnsiZ("/");

    /// <summary>Ghidra: &amp;DAT_00564a10 — deux codes de controle du moteur de texte, <c>0x0c</c>
    /// (debut de page) puis <c>0x0f</c> (attente), lies tels quels a la fin du chargement.</summary>
    private static readonly byte* s_PageWaitCommand_00564a10 = AllocBytes([0x0c, 0x0f, 0x00, 0x00]);

    /// <summary>
    /// Ghidra: &amp;DAT_00566ad8 — la rangee d'icones fixes du cadre de la fiche d'objet, dix
    /// enregistrements de quatre octets termines par un dword nul, releves a 0x00566ad8 :
    /// <c>{ index de descripteur, variante de sprite, dx, dy }</c>. Le premier octet vaut 1
    /// partout, donc tout est positionne par rapport au descripteur 0x5658a8.
    /// </summary>
    private static readonly byte* s_ItemPanelIcons_00566ad8 = AllocBytes(
    [
        1, 0x13, 0x58, 0x68,  1, 0x14, 0x90, 0x68,
        1, 0x1c, 0x58, 0x76,  1, 0x1d, 0x7c, 0x76,
        1, 0x1e, 0xa0, 0x76,  1, 0x1f, 0xc4, 0x76,
        1, 0x20, 0x58, 0x80,  1, 0x21, 0x7c, 0x80,
        1, 0x22, 0xa0, 0x80,  1, 0x23, 0xc4, 0x80,
        0, 0, 0, 0,
    ]);

    /// <summary>
    /// Ghidra: FUN_0051bcc0 @ 0x0051bcc0 — compose le libelle affichable d'un objet dans
    /// <paramref name="dest"/> et rend le pointeur de fin.
    ///
    /// <para>Le nom long vit a <c>+0x41</c> de la fiche et se termine par <c>'/'</c> (0x2f), pas
    /// par un NUL. Si l'octet 0 depasse 0x5f alors que l'octet 1 reste en dessous, le libelle est
    /// prefixe de <c>0x24</c> suivi de cet octet — une sequence de controle du moteur de texte,
    /// et c'est ce qui decale ensuite l'ecriture d'un cran.</para>
    ///
    /// <para><b>Le repli</b> : si les 0x40 octets defilent sans rencontrer le <c>'/'</c>, le nom
    /// long est considere absent et la fonction reprend le nom court de <c>+0x0</c> (0x10 octets
    /// au plus, arret au NUL), en reculant <paramref name="dest"/> du prefixe. Transcrit tel quel,
    /// y compris ce recul qui fait rendre un pointeur en amont du tampon quand le prefixe existe.</para>
    /// </summary>
    private static byte* BuildItemDisplayName(byte* dest, byte* item)
    {
        int written = 0;
        uint prefix = 0;

        if (item[0] > 0x5f && item[1] < 0x60)
        {
            dest[0] = 0x24;
            dest[1] = item[0];
            written = 2;
            prefix = 1;
        }

        uint i = 0;
        do
        {
            if (item[i + 0x41] == 0x2f) break;
            dest[written + i] = item[i + 0x41];
            i++;
        }
        while (i < 0x40);

        if (i >= 0x40)
        {
            i = prefix;
            if (prefix < 0x10)
            {
                do
                {
                    if (item[i] == 0) break;
                    dest[prefix + i] = item[i];
                    i++;
                }
                while (i < 0x10);
            }
            dest -= prefix;
        }

        return dest + i + written;
    }

    /// <summary>Ghidra: FUN_0051bd30 @ 0x0051bd30 — <see cref="BuildItemDisplayName"/> dans le
    /// tampon partage, termine par un NUL.</summary>
    private static byte* ItemDisplayName(byte* item)
    {
        byte* end = BuildItemDisplayName(s_ItemNameScratch_005de110, item);
        *end = 0;
        return s_ItemNameScratch_005de110;
    }

    /// <summary>
    /// Ghidra: les huit caracteristiques d'un objet sur deux rangees, puis ses deux valeurs
    /// longues — **le meme bloc ecrit deux fois** dans l'original, une fois dans la phase 9 de
    /// <c>FUN_00524880</c> et une fois en tete de <c>FUN_00529290</c>, a la virgule pres. Extrait
    /// ici plutot que recopie, comme <see cref="BuildSaveDirectoryRows"/>.
    ///
    /// <para>Un objet non identifie (attribut 0x80 du champ <c>+0x13</c> eteint) affiche
    /// <c>"???"</c> partout au lieu de ses chiffres. L'ordre des huit n'est pas 0..7 mais celui de
    /// <see cref="s_StatSlotOrder_00566ad0"/>, et l'index sert aussi de variante de glyphe
    /// (<c>slot + 2</c>).</para>
    /// </summary>
    private static uint* DrawItemStatRow(uint* otBase, uint* primCursor, int* draw, byte* item)
    {
        byte* d = (byte*)draw;

        for (int n = 0; n < 8; n++)
        {
            *(short*)(d + 8) = (short)((n & 3) * 0x24 + 0x60 + 8);
            *(short*)(d + 0xa) = (short)((n >> 2) * 10 + 0x7e);
            if ((item[0x13] & 0x80) == 0)
            {
                *(uint**)d = s_GlyphAtlas_00566c28;
                primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_UnknownStatLabel_00566b80, 0, 0, -1);
            }
            else
            {
                sbyte slot = (sbyte)s_StatSlotOrder_00566ad0[n];
                *(uint**)d = s_NumeralTileRecord_00566c30;
                primCursor = EmitCardGlyphTile(otBase, primCursor, draw,
                                               (uint)(int)(sbyte)item[0x18 + slot],
                                               3, (uint)(ushort)(slot + 2));
            }
        }

        *(short*)(d + 8) = 0x60;
        *(short*)(d + 0xa) = 0x70;
        short* longStat = (short*)(item + 0x30);
        for (int n = 0; n < 2; n++, longStat++)
        {
            *(short*)(d + 8) += 0x14;
            if ((item[0x13] & 0x80) == 0)
            {
                *(uint**)d = s_GlyphAtlas_00566c28;
                primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_UnknownStatLabel_00566b80, 0, 0, -1);
            }
            else
            {
                *(uint**)d = s_NumeralTileRecord_00566c30;
                int value = *longStat;
                if (value < 0) value = 0;
                primCursor = EmitCardGlyphTile(otBase, primCursor, draw, (uint)value, 3, 0);
            }
            *(short*)(d + 8) += 0x28;
        }

        return primCursor;
    }

    /// <summary>
    /// Ghidra: les huit temoins d'element d'un objet, second bloc partage entre la phase 9 de
    /// <c>FUN_00524880</c> et la fin de <c>FUN_00529290</c>. Le champ <c>+0x10</c> de
    /// l'enregistrement sert d'index dans <see cref="s_ElementIcons_00566b08"/>.
    /// </summary>
    private static uint* DrawItemElementRow(uint* otBase, uint* primCursor, int* draw, byte* item)
    {
        byte* d = (byte*)draw;

        *(short*)(d + 0x10) = 0;
        *(short*)(d + 8) = 0x60;
        *(short*)(d + 0xa) = 0x94;
        *(byte**)d = s_ElementIcons_00566b08;

        EmitViewportRect(otBase, &primCursor);
        for (int n = 0; n < 8; n++)
        {
            // Ghidra: `(-(bit != 0) & 0xfffffff9) + 9` — 2 quand l'element est present, 9 sinon ;
            // deux variantes de la meme icone.
            uint variant = (1 << n & *(ushort*)(item + 0x12)) != 0 ? 2u : 9u;
            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, variant);
            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
            *(short*)(d + 8) += 0x10;
            *(short*)(d + 0x10) += 1;
        }
        EmitClipRect(otBase, &primCursor);

        return primCursor;
    }

    /// <summary>
    /// Ghidra: FUN_00529290 @ 0x00529290 — la fiche detaillee de l'objet sous le curseur, dans le
    /// sous-ecran « Utilisation ». C'est <see cref="DrawItemStatRow"/> puis, entre les deux blocs
    /// partages, le **nom** de l'objet : soit <c>"????"</c> s'il n'est pas identifie, soit le
    /// libelle que <see cref="ItemDisplayName"/> compose, lie dans le bandeau apres un
    /// <see cref="DrawItemMenuTitle"/>.
    /// </summary>
    private static uint* DrawItemDetailCard(uint* otBase, uint* primCursor, byte* record, int cursor)
    {
        byte* item = record + 0x300 + record[0x2c1 + cursor] * 0x280;

        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;
        NativeMemory.Clear(d, 32);
        *(uint*)(d + 4) = 0x1000000;

        primCursor = DrawItemStatRow(otBase, primCursor, draw, item);

        *(short*)(d + 0xa) -= 0xc;
        *(short*)(d + 8) = 0x60;
        *(uint**)d = s_GlyphAtlas_00566c28;

        if ((item[0x13] & 0x80) == 0)
        {
            primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_UnknownItemLabel_00566b84, 0, 0, 0x10);
        }
        else
        {
            primCursor = DrawItemMenuTitle(primCursor);   // Ghidra: FUN_00524190

            byte* title = s_ItemMenuTitleDescriptor_005658a8;
            byte* cmd = stackalloc byte[100];
            NativeMemory.Clear(cmd, 100);
            cmd[0] = 0x0d;
            cmd[1] = 4;
            cmd[2] = (byte)(*(sbyte*)(d + 8) - *(sbyte*)(title + 4));
            cmd[3] = (byte)(*(sbyte*)(d + 0xa) - *(sbyte*)(title + 6) - 0x10);

            byte* name = ItemDisplayName(item);
            int length = 0;
            while (name[length] != 0) length++;
            for (int i = 0; i <= length; i++) cmd[4 + i] = name[i];

            BindMenuLabels(cmd, 0, title, 0);
            primCursor = Ui.RunTextEngine(primCursor, title);
        }

        return DrawItemElementRow(otBase, primCursor, draw, item);
    }

    /// <summary>
    /// Ghidra: FUN_00529070 @ 0x00529070 — la grille d'objets d'un membre du groupe : cinq
    /// colonnes de 0x1c pixels, une icone par emplacement occupe, sa quantite si l'objet est
    /// empilable, et une pastille sur les emplacements que le membre porte reellement (ceux que
    /// <c>record[0x2b0..0x2b4]</c> designe).
    ///
    /// <para>Ghidra rend l'index de boucle par une soustraction de pointeurs
    /// (<c>iVar7 = -0x2c1 - param_4</c> puis <c>pbVar9 + iVar7</c>) : ce n'est rien d'autre que le
    /// rang <c>i</c> dans la liste d'emplacements occupes, compare au curseur.</para>
    /// </summary>
    private static uint* DrawMemberItemGrid(uint* otBase, uint* primCursor, byte* slotFlags, byte* record)
    {
        if (record[0x2c0] == 0) return primCursor;

        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;
        NativeMemory.Clear(d, 32);

        for (int i = 0; i < record[0x2c0]; i++)
        {
            byte slot = record[0x2c1 + i];
            short sx = (short)((i % 5) * 0x1c);
            short sy = (short)((i / 5) * 0x1c);
            byte* item = record + slot * 0x280;   // les champs utiles sont en +0x310 et +0x313

            *(uint*)(d + 4) = i == s_ItemMenuCursor_005de4e0 ? s_Unknown_005e9814 : 0x1000000u;

            if ((item[0x313] & 0x30) != 0)
            {
                *(short*)(d + 8) = (short)(sx + 0x68);
                *(short*)(d + 0xa) = (short)(sy + 0x3c);
                *(uint**)d = s_NumeralTileRecord_00566c30;
                primCursor = EmitCardGlyphTile(otBase, primCursor, draw,
                                               *(ushort*)(item + 0x310), 0x82, 0);
            }

            *(short*)(d + 0x10) = 0;
            EmitViewportRect(otBase, &primCursor);

            for (int e = 0; e < 5; e++)
            {
                if (record[0x2b0 + e] != slot) continue;
                *(short*)(d + 8) = (short)(sx + 0x6c);
                *(short*)(d + 0xa) = (short)(sy + 0x38);
                *(byte**)d = s_EquippedMarker_00566b50;
                primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
            }

            // L'enregistrement d'icone partage, dont les deux premiers octets designent la case
            // de l'atlas : la colonne par l'emplacement, la ligne par le rang du membre.
            byte* icon = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x10);
            icon[0] = (byte)(slot * 0x18);
            icon[1] = (byte)(slotFlags[8] * 0x18);
            *(byte**)d = icon;
            *(short*)(d + 8) = (short)(sx + 0x60);
            *(short*)(d + 0xa) = (short)(sy + 0x2c);
            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0,
                                           (uint)((slotFlags[8] << 8) | slot));
            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
            EmitClipRect(otBase, &primCursor);
        }

        return primCursor;
    }

    /// <summary>
    /// Ghidra: FUN_005276c0 @ 0x005276c0 — dessine une rangee d'icones decrites par une table de
    /// quadruplets, chacune posee relativement au coin d'un descripteur d'interface
    /// (<c>descripteur + 4</c> et <c>+6</c>) plutot qu'en coordonnees absolues, ce qui les fait
    /// suivre le cadre quand une phase le deplace.
    ///
    /// <para>Les dix appels partagent un seul enregistrement de dessin : seule la variante
    /// (<c>+0x10</c>) et la position changent, et la primitive plate finale est emise une fois
    /// avec la variante remise a zero.</para>
    /// </summary>
    private static uint* DrawPanelIconRow(uint* otBase, uint* primCursor, byte* list)
    {
        EmitViewportRect(otBase, &primCursor);

        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;
        NativeMemory.Clear(d, 32);
        *(uint*)(d + 4) = 0x1000000;
        *(uint**)d = *(uint**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x14);

        for (byte* rec = list; *(int*)rec != 0; rec += 4)
        {
            byte* panel = s_ItemMenuDescriptors_00564fe0 + rec[0] * GameMemory.UiDescriptorStride;
            *(ushort*)(d + 0x10) = rec[1];
            *(short*)(d + 0xa) = (short)(*(short*)(panel + 6) + rec[3]);
            *(short*)(d + 8) = (short)(*(short*)(panel + 4) + rec[2]);
            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
        }

        *(ushort*)(d + 0x10) = 0;
        primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
        EmitClipRect(otBase, &primCursor);
        return primCursor;
    }

    /// <summary>
    /// Ghidra: FUN_00524880 @ 0x00524880 — le sous-ecran « Invertaire » : on choisit une
    /// sauvegarde, on charge son inventaire, on le parcourt et on peut renommer un objet avant de
    /// reecrire le fichier.
    ///
    /// <para><b>La disposition d'un inventaire.</b> Vingt-deux emplacements de 0x280 octets :
    /// <c>+0x00</c> la fiche (nom court en 0x00, nom long en 0x41 termine par <c>'/'</c>, quantite
    /// en 0x10, attributs en 0x12/0x13), <c>+0x140</c> une palette de seize couleurs et
    /// <c>+0x160</c> une icone 24x24 en 4 bits. Les dix premiers vivent dans le premier bloc du
    /// fichier de sauvegarde (a <c>+0x300</c>), les douze suivants dans son second bloc — d'ou les
    /// trois lectures de la phase 5 et les deux boucles de compte.</para>
    ///
    /// <para><b>La somme de controle est stockee deux fois</b>, a 0x1ffc et a 0x3ffc : la phase
    /// 0x11 reecrit <c>record[0x1f00..0x1fff]</c> a l'offset 0x3f00 du fichier, ce qui y recopie
    /// le meme dword. La phase 5 compare donc d'abord les deux exemplaires, puis recalcule la
    /// somme sur les 0x1ffc premiers octets.</para>
    ///
    /// <para><b>Le tampon de pile de 188 octets.</b> Le prologue y depose les 186 largeurs de
    /// glyphes proportionnelles ; la phase 10 passe la table globale en chasse fixe pour aligner
    /// sa grille de caracteres, puis la restaure depuis cette copie. Les phases 8 et la queue
    /// reutilisent les memes octets comme tampon de commande texte : Ghidra a fusionne deux
    /// locales qui partagent les emplacements de pile sans jamais etre vivantes ensemble, le
    /// prologue les reecrivant a chaque image.</para>
    ///
    /// <para>TODO(REVERSE) : les phases 9 (la grille d'objets), 10 (le renommage) et la boucle de
    /// reecriture du fichier (0xb..0x12, 0x5e..0x63 et les negatives) ne sont pas encore
    /// transcrites — chacune leve une exception nommee plutot que de ne rien faire.</para>
    /// </summary>
    private static uint* ItemMenuInventoryScreen(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* mainMem = GetMainMemPtr();

        byte* cmd = stackalloc byte[188];
        for (int i = 0; i < Ui.s_GlyphWidthsProportional.Length; i++) cmd[i] = Ui.s_GlyphWidthsProportional[i];

        int slotBefore = s_ItemMenuCardSlot_005de4e8;
        s_ItemMenuRecord_005de410 = (int)(nint)(mainMem + 0xf0000);
        byte* record = (byte*)(nint)s_ItemMenuRecord_005de410;

        sbyte slotCount = (sbyte)s_CardSlotLists_00566a90[s_Unknown_005de50c][0];
        if (s_ItemMenuCardSlot_005de4e8 < 0) s_ItemMenuCardSlot_005de4e8 = slotCount - 1;
        if (slotCount <= s_ItemMenuCardSlot_005de4e8) s_ItemMenuCardSlot_005de4e8 = 0;
        if (slotBefore != s_ItemMenuCardSlot_005de4e8) PlayUiSound3A();

        if (s_Unknown_00566acc < 0 && s_Unknown_005de4d4 == 0) s_Unknown_00566acc = 0;

        if (s_ItemMenuPrevPhase_005de4d8 != s_Unknown_005de4d4)
        {
            s_ItemMenuPrevPhase_005de4d8 = s_Unknown_005de4d4;
            s_ItemMenuEntered_005de4dc = 1;
        }

        // Ghidra: `if (DAT_00566acc < 0) goto switchD_00524e4e_caseD_2;` — la machine a phases est
        // sautee d'un bloc, mais la queue commune s'execute quand meme.
        if (s_Unknown_00566acc >= 0)
        {
            switch (s_Unknown_005de4d4)
            {
                case 0:
                {
                    Inp.JoystickResetState(false);   // Ghidra: FUN_00504b10(0)
                    LoadSaveDirectory();         // Ghidra: FUN_005126c0(1)

                    int entryCount = s_CardEntryCount_00604981;
                    BuildSaveDirectoryRows(record, entryCount);

                    // Ghidra: 0x19 dwords a zero depuis &DAT_005e85c0.
                    NativeMemory.Clear(s_PartySelectionOrder_005e85c0, 0x64);

                    if (entryCount == 0)
                    {
                        s_ItemMenuSubState_00566ac8 = -1;
                        return primCursor;
                    }

                    BindMenuLabels(Mem.s_Unknown_005606f8, 0, s_ItemMenuDescriptors_00564fe0, 0);
                    BindMenuLabels(LabelAt(0x34), 0, s_ItemMenuTitleDescriptor_005658a8, 0);
                    s_Unknown_005de4d4++;
                    s_ItemMenuPhase_005de4d0 = 0;
                    goto case 1;
                }

                case 1:
                {
                    int entryCount = s_CardEntryCount_00604981;

                    if (s_ItemMenuPhase_005de4d0 == 0)
                    {
                        // La meme navigation que l'Enchere, mais en choix unique : pas de CARRE
                        // qui retient plusieurs lignes, il valide directement.
                        ushort repeat = *(ushort*)(pad + 6);

                        if ((repeat & 0x40) != 0)
                        {
                            if (s_ItemMenuSelection_005de4c8 < entryCount - 1)
                            {
                                int next = s_ItemMenuSelection_005de4c8 + 1;
                                if (s_ItemMenuScroll_005de4c4 + 5 < next)
                                    s_ItemMenuScroll_005de4c4 = s_ItemMenuSelection_005de4c8 - 4;
                                s_ItemMenuSelection_005de4c8 = next;
                                PlayUiSound3A();
                            }
                        }
                        else if ((repeat & 0x10) != 0 && s_ItemMenuSelection_005de4c8 > 0)
                        {
                            s_ItemMenuSelection_005de4c8--;
                            if (s_ItemMenuSelection_005de4c8 < s_ItemMenuScroll_005de4c4)
                                s_ItemMenuScroll_005de4c4 = s_ItemMenuSelection_005de4c8;
                            PlayUiSound3A();
                        }

                        if ((pad[3] & 0x80) != 0)
                        {
                            PlayUiSound38();
                            s_Unknown_005de4d4 = 5;
                            s_ItemMenuChosenEntry_005de42c = (int)(nint)(s_CardDirectoryEntries_006049bc
                                + s_ItemMenuSelection_005de4c8 * 0x28);
                        }
                        else if ((pad[3] & 0x40) != 0)
                        {
                            s_ItemMenuSubState_00566ac8 = -1;
                        }
                    }
                    else if (s_ItemMenuPhase_005de4d0 == 3 && (pad[3] & 0xc0) != 0)
                    {
                        s_GameStateId = 0;   // Ghidra: DAT_005de510
                        return primCursor;
                    }

                    // Les six lignes visibles — le meme trace que l'Enchere, cadre en 256x224.
                    int windowEnd = s_ItemMenuScroll_005de4c4 + 6;
                    if (entryCount < windowEnd) windowEnd = entryCount;

                    int* draw = stackalloc int[8];
                    byte* d = (byte*)draw;
                    NativeMemory.Clear(d, 32);

                    for (int i = s_ItemMenuScroll_005de4c4; i < windowEnd; i++)
                    {
                        byte* row = s_PartyRowText_005e8640 + i * PartyRowStride;
                        bool chosen = s_PartySelectionOrder_005e85c0[i] != 0;

                        EmitViewportRect(otBase, &primCursor);

                        int bank = i >> 4;
                        *(short*)(d + 0x10) = 0;
                        s_RowSpriteRecord_00563148[0] = (byte)(i << 4);
                        s_RowSpriteRecord_00563148[1] = (byte)(bank * 0x40 + 0x20);
                        *(byte**)d = s_RowSpriteRecord_00563148;
                        *(uint*)(d + 4) = 0x1000000;
                        *(short*)(d + 8) = 0x28;
                        *(short*)(d + 0xa) = (short)((i - s_ItemMenuScroll_005de4c4) * 0x14 + 0x26);

                        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xf8) = 0xf;
                        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfa) =
                            (ushort)(((i << 4) >> 4) & 0xf | (bank + 0x1fe) * 0x40);

                        primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, chosen ? 0u : 3u, 0);
                        primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                        EmitClipRect(otBase, &primCursor);

                        *(short*)(d + 8) += 0x1e;
                        *(short*)(d + 0xa) += 4;
                        *(uint*)(d + 4) = chosen ? 0x1000000u : 0x404040u;
                        *(byte**)d = (byte*)s_GlyphAtlas_00566c28;
                        primCursor = Prims.DrawTextRun(otBase, primCursor, draw, row, 0, 0, PartyRowStride);

                        if (s_ItemMenuSelection_005de4c8 == i)
                        {
                            EmitViewportRect(otBase, &primCursor);
                            *(uint**)d = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                            *(short*)(d + 0xa) -= 2;
                            *(short*)(d + 0x10) = 0x18;
                            *(uint*)(d + 4) = 0x1000000;
                            *(short*)(d + 8) = 0x14;
                            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                            EmitClipRect(otBase, &primCursor);
                        }
                    }
                    break;
                }

                case 5:
                {
                    // Le chargement est **decale d'une image** : celle qui entre dans la phase se
                    // contente d'abaisser le drapeau, la suivante lit le fichier.
                    if (s_ItemMenuEntered_005de4dc != 0)
                    {
                        s_ItemMenuEntered_005de4dc = 0;
                        break;
                    }

                    byte* entry = (byte*)(nint)s_ItemMenuChosenEntry_005de42c;
                    Saves.SetCardFileName(entry);            // Ghidra: FUN_00512900
                    Saves.SetCardPathDeviceFields();         // Ghidra: FUN_005128e0(&DAT_00604980)

                    *(uint*)(record + 0x1ffc) = 0;
                    IntPtr handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                    int r1 = Saves.RetrySeekAndReadCardFile(handle, record, 0, 0x2000);
                    int r2 = Saves.RetrySeekAndReadCardFile(handle, mainMem + 0x1b1900, 0x2000, 0x2000);
                    int r3 = Saves.RetrySeekAndReadCardFile(handle, record + 0x2000, 0x4000,
                                                      (*(int*)(entry + 0x18) - 2) * 0x2000);
                    Saves.RetryCloseCardFile(handle);

                    bool loaded = false;

                    if ((r1 | r2 | r3) >= 0)
                    {
                        s_ItemRenamed_005de508 = 0;

                        int stored = *(int*)(record + 0x1ffc);
                        int checksum = 0;
                        for (int i = 0; i < 0x1ffc; i++) checksum += (sbyte)record[i];

                        if (stored == *(int*)(mainMem + 0x1b38fc) && stored == checksum)
                        {
                            UploadPartyIconTextures();   // Ghidra: FUN_00535500

                            // La fusion des objets empilables : les douze emplacements du second
                            // bloc sont confrontes aux dix du personnage. Nom court identique sur
                            // 0x10 octets, meme mot en +0x86 et l'attribut 0x10 leve : les
                            // quantites s'additionnent, plafonnees a 99, et l'entrant est efface.
                            byte* incoming = mainMem + 0x1b1900;
                            for (int n = 0; n < 12; n++, incoming += 0x280)
                            {
                                for (int slot = 0; slot < 10; slot++)
                                {
                                    byte* target = record + 0x300 + slot * 0x280;

                                    int k = 0;
                                    while (k < 0x10 && target[k] == incoming[k]) k++;
                                    if (k != 0x10) continue;
                                    if (*(short*)(target + 0x86) != *(short*)(incoming + 0x86)) continue;
                                    if ((incoming[0x13] & 0x10) == 0) continue;

                                    *(short*)(target + 0x10) += *(short*)(incoming + 0x10);
                                    incoming[0] = 0;
                                    if (*(ushort*)(target + 0x10) > 99)
                                    {
                                        target[0x10] = 99;
                                        target[0x11] = 0;
                                    }
                                    break;
                                }
                            }

                            // Les dix emplacements du personnage sont recopies au debut du bloc de
                            // travail ; les douze du fichier occupent deja la suite, a +0x1900.
                            Mem.CopyDwords((uint*)(record + 0x300), (uint*)(mainMem + 0x1b0000), 0x640);

                            // L'envoi en VRAM des vingt-deux icones, palette puis pixels. Le
                            // rectangle vit sur la pile ; l'original en garde l'adresse dans
                            // DAT_005de2fc/DAT_005de304 et les deux sources dans
                            // DAT_005de300/DAT_005de308, mais aucun de ces quatre pointeurs n'est
                            // relu ailleurs que dans les lignes qui suivent, et deux d'entre eux
                            // designent la pile — les garder au-dela de l'appel n'aurait aucun sens.
                            short* rect = stackalloc short[4];
                            byte* slotPtr = mainMem + 0x1b0000;
                            s_ItemCount_005de430 = 0;

                            for (int n = 0; n < 0x16; n++, slotPtr += 0x280)
                            {
                                if (slotPtr[0] != 0)
                                {
                                    s_ItemIndex_005de488[s_ItemCount_005de430] = (byte)n;
                                    s_ItemCount_005de430++;
                                }

                                // La palette : seize couleurs sur une ligne, rangee 0x1fc + n/16.
                                rect[0] = (short)((n & 0xf) << 4);
                                rect[1] = (short)((n >> 4) + 0x1fc);
                                rect[2] = 0x10;
                                rect[3] = 1;
                                Winsys3d.Ordinal_674(rect, slotPtr + 0x140);

                                // L'icone : 24x24 en 4 bits, soit six mots de large.
                                rect[0] = (short)((n % 10) * 0x18 / 4 + 0x3c0);
                                rect[1] = (short)((n / 10) * 0x18 + 0x88);
                                rect[2] = 6;
                                rect[3] = 0x18;
                                Winsys3d.Ordinal_674(rect, slotPtr + 0x160);
                            }

                            // La page et la palette courantes, deduites du **dernier** rectangle
                            // envoye — l'original laisse volontairement le pointeur sur lui.
                            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfa) = 0x7f00;
                            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xf8) = (ushort)(
                                (short)(((((ushort)rect[1] >> 8) << 8) | ((rect[0] >> 2) & 0xff))
                                        & 0x1f0 | 0x2000) >> 4);

                            // La liste des emplacements occupes du personnage, en tete du record.
                            record[0x2c0] = 0;
                            for (int n = 0, off = 0; off < 0x1900; n++, off += 0x280)
                            {
                                if (record[0x300 + off] == 0) continue;
                                record[0x2c1 + record[0x2c0]] = (byte)n;
                                record[0x2c0]++;
                            }

                            // Puis, une seconde fois, la liste des vingt-deux — l'original
                            // reconstruit DAT_005de488/DAT_005de430 a l'identique juste apres les
                            // avoir remplis dans la boucle d'envoi. Recopie tel quel.
                            s_ItemCount_005de430 = 0;
                            for (int n = 0; n < 0x16; n++)
                            {
                                if (mainMem[0x1b0000 + n * 0x280] == 0) continue;
                                s_ItemIndex_005de488[s_ItemCount_005de430] = (byte)n;
                                s_ItemCount_005de430++;
                            }

                            NativeMemory.Clear(s_ItemHeld_005de2d8, 24);   // six dwords
                            for (int i = 0; i < record[0x2c0]; i++)
                                s_ItemHeld_005de2d8[s_ItemIndex_005de488[i]] = 1;

                            s_Unknown_005de4d4 = 8;
                            BindMenuLabels(s_PageWaitCommand_00564a10, 0, s_ItemMenuDescriptors_00564fe0, 0);
                            loaded = true;
                        }
                    }

                    if (!loaded)
                    {
                        BindMenuLabels(LabelAt(0x24), 0, s_ItemMenuDescriptors_00564fe0, 0);
                        s_Unknown_005de4d4 = -1;
                    }
                    break;
                }

                case 8:
                {
                    if (s_ItemMenuEntered_005de4dc != 0)
                    {
                        s_ItemMenuEntered_005de4dc = 0;
                        s_Unknown_005de4e4 = 0;
                        s_ItemMenuVisible_005de4f0 = 0;
                        for (int off = 0; off < 0x1900; off += 0x280)
                            if (record[0x300 + off] != 0) s_ItemMenuVisible_005de4f0++;
                        BindMenuLabels(Mem.s_Unknown_005606f8, 0, s_ItemMenuDescriptors_00564fe0, 0);
                    }

                    // CROIX et CARRE sortent tous deux vers l'ecran racine, avec deux sons
                    // differents — le seul detail qui les distingue ici.
                    if ((pad[3] & 0x40) != 0 || (pad[3] & 0x80) != 0)
                    {
                        if ((pad[3] & 0x40) != 0) PlayUiSound39(); else PlayUiSound38();
                        s_Unknown_005de4d4 = 0;
                        s_ItemMenuSubState_00566ac8 = -1;
                        s_Unknown_00566acc = -1;
                        break;
                    }

                    int scrollBefore = s_Unknown_005de4e4;
                    if ((pad[6] & 0x10) != 0 && s_Unknown_005de4e4 != 0) s_Unknown_005de4e4--;
                    if ((pad[6] & 0x40) != 0 && s_Unknown_005de4e4 + 7 < s_ItemMenuVisible_005de4f0)
                        s_Unknown_005de4e4++;
                    if (scrollBefore != s_Unknown_005de4e4) PlayUiSound3A();

                    int* draw = stackalloc int[8];
                    byte* d = (byte*)draw;
                    NativeMemory.Clear(d, 32);
                    *(uint**)d = s_GlyphAtlas_00566c28;
                    *(uint*)(d + 4) = 0x1000000;
                    *(short*)(d + 8) = 0x68;
                    *(short*)(d + 0xa) = 0x2c;
                    primCursor = DrawItemMenuTitle(primCursor);   // Ghidra: FUN_00524190

                    // Sept lignes au plus. La position n'est calculee qu'une fois : c'est le code
                    // de controle 0x0d en tete de commande — un retour a la ligne — qui fait
                    // descendre le moteur de texte d'une ligne a chaque libelle lie.
                    cmd[0] = 0x0d;
                    byte* title = s_ItemMenuTitleDescriptor_005658a8;
                    int shown = 0;
                    record[0x2c0] = 0;

                    for (int n = 0; n < 10 && shown <= 6; n++)
                    {
                        byte* slot = record + 0x300 + n * 0x280;
                        if (slot[0] == 0 || n < s_Unknown_005de4e4) continue;

                        cmd[2] = (byte)(*(sbyte*)(d + 8) - *(sbyte*)(title + 4));
                        cmd[3] = (byte)(*(sbyte*)(d + 0xa) - *(sbyte*)(title + 6) - 0xc);
                        cmd[1] = 4;
                        shown++;
                        record[0x2c1 + record[0x2c0]] = (byte)n;
                        record[0x2c0]++;

                        byte* name = ItemDisplayName(slot);   // Ghidra: FUN_0051bd30
                        int length = 0;
                        while (name[length] != 0) length++;
                        for (int i = 0; i <= length; i++) cmd[4 + i] = name[i];

                        BindMenuLabels(cmd, 0, title, 0);
                        primCursor = Ui.RunTextEngine(primCursor, title);
                    }

                    // Le compteur « n / 403 », separe par la barre oblique de 0x00564a0c.
                    *(short*)(d + 8) = 0x38;
                    *(short*)(d + 0xa) = 0x90;
                    primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_SlashLabel_00564a0c, 0, 0, 0x10);

                    *(uint**)d = s_NumeralTileRecord_00566c30;
                    *(short*)(d + 8) = 0x20;
                    *(short*)(d + 0xa) = 0x90;
                    *(uint*)(d + 4) = 0x1000000;
                    primCursor = EmitCardGlyphTile(otBase, primCursor, draw,
                                                   (uint)s_ItemMenuVisible_005de4f0, 0x83, 0);
                    *(short*)(d + 8) += 0x20;
                    primCursor = EmitCardGlyphTile(otBase, primCursor, draw, 0x193, 3, 0);
                    break;
                }

                case 9:
                {
                    // ATTENTION : phase **sans predecesseur**. Aucun site du binaire n'ecrit 9 dans
                    // DAT_005de4d4 (verifie a l'octet sur Legacy/Blaze.exe : les seuls immediats
                    // sont 0, 5, 8, 0xb, 0xd, 0xe, 0x5e, 0x60, 0x62, -1 et -5), et dans cette
                    // fonction le seul chemin est 10 -> 9 par decrement, la phase 10 n'etant
                    // elle-meme atteinte que depuis 9. La paire est donc fermee sur elle-meme :
                    // la fiche detaillee et le clavier de renommage sont complets dans le binaire
                    // mais injoignables. Transcrits quand meme — le projet reconstruit le binaire,
                    // pas seulement ses chemins vivants. Voir legacy-bug.md.
                    byte* desc1 = s_ItemMenuTitleDescriptor_005658a8;
                    byte* desc2 = s_ItemMenuEditDescriptor_00566170;

                    if (s_ItemMenuEntered_005de4dc != 0)
                    {
                        s_ItemMenuEntered_005de4dc = 0;

                        record[0x2c0] = 0;
                        for (int n = 0, off = 0; off < 0x1900; n++, off += 0x280)
                        {
                            if (record[0x300 + off] == 0) continue;
                            record[0x2c1 + record[0x2c0]] = (byte)n;
                            record[0x2c0]++;
                        }

                        // Le cadre est retaille pour la fiche. L'ordre compte : le dword de +8
                        // est pose avant le short de +0xa, qui l'ecrase a moitie.
                        *(int*)(desc1 + 8) = 0xe0;
                        *(int*)(desc1 + 4) = 0x00080008;
                        *(short*)(desc1 + 0xa) = 0x90;
                        *(int*)(desc1 + 0x24) = 0x01000000;
                        *(int*)(desc2 + 0x24) = 0x01000000;
                        *(int*)(desc1 + 0x20) = 0x00071044;
                        *(int*)(desc2 + 4) = 0x00640014;
                        *(int*)(desc2 + 0x20) = 0x01070000;
                        desc2[0x1b] = 0;
                        desc2[0x43] = 0;

                        BindMenuLabels(LabelAt(0xcc), 0, s_ItemMenuDescriptors_00564fe0, 0);
                        primCursor = DrawItemMenuTitle(primCursor);
                        s_Unknown_005de4e4 = 0;
                        s_ItemGridScroll_005de4ec = 0;
                    }

                    if ((pad[3] & 0x40) != 0)
                    {
                        // Ghidra: `_DAT_005658c8 = CONCAT13(1,_DAT_005658c8)` — le seul octet de
                        // poids fort du dword de +0x20, pas le dword entier.
                        desc1[0x23] = 1;
                        PlayUiSound39();
                        s_Unknown_005de4d4 = 0;
                        s_ItemMenuSubState_00566ac8 = -1;
                        s_Unknown_00566acc = -1;
                        break;
                    }

                    int gridBefore = s_ItemGridScroll_005de4ec << 8 | s_Unknown_005de4e4;

                    if ((pad[6] & 0x10) != 0)
                    {
                        if (s_Unknown_005de4e4 == 0)
                        {
                            if (s_ItemGridScroll_005de4ec != 0) s_ItemGridScroll_005de4ec--;
                        }
                        else s_Unknown_005de4e4--;
                    }
                    if ((pad[6] & 0x40) != 0)
                    {
                        int lastRow = s_ItemCount_005de430 - 1;
                        if (s_ItemCount_005de430 > 4) lastRow = 4;
                        if (s_Unknown_005de4e4 < lastRow) s_Unknown_005de4e4++;
                        else if (s_ItemGridScroll_005de4ec < s_ItemCount_005de430 - 5
                              && s_ItemCount_005de430 > 4) s_ItemGridScroll_005de4ec++;
                    }
                    if (gridBefore != (s_ItemGridScroll_005de4ec << 8 | s_Unknown_005de4e4))
                        PlayUiSound3A();

                    byte* grid = mainMem + 0x1b0000;

                    if ((pad[3] & 0x80) != 0)
                    {
                        // Seul un objet dont l'attribut 0x8000 est leve peut etre renomme.
                        byte chosen = s_ItemIndex_005de488[s_Unknown_005de4e4 + s_ItemGridScroll_005de4ec];
                        if ((*(ushort*)(grid + chosen * 0x280 + 0x12) & 0x8000) == 0)
                        {
                            PlayUiSound3B();
                        }
                        else
                        {
                            s_ItemMenuVisible_005de4f0 = s_ItemGridScroll_005de4ec + s_Unknown_005de4e4;
                            PlayUiSound38();
                            s_Unknown_005de4d4++;
                        }
                    }

                    primCursor = DrawPanelIconRow(otBase, primCursor, s_ItemPanelIcons_00566ad8);

                    int* draw = stackalloc int[8];
                    byte* d = (byte*)draw;
                    NativeMemory.Clear(d, 32);

                    // Le curseur de ligne.
                    EmitViewportRect(otBase, &primCursor);
                    *(uint*)(d + 4) = 0x1000000;
                    *(uint**)d = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                    *(short*)(d + 0xa) = (short)((s_Unknown_005de4e4 << 4) + 0x18);
                    *(short*)(d + 0x10) = 0x18;
                    *(short*)(d + 8) = 0x58;
                    primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                    primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                    EmitClipRect(otBase, &primCursor);

                    // Cinq lignes au plus : icone, quantite et nom.
                    *(short*)(d + 0x10) = 0;
                    short y = 0x14;
                    for (int i = s_ItemGridScroll_005de4ec; ; i++, y += 0x10)
                    {
                        int limit = s_ItemCount_005de430 > 4 ? s_ItemGridScroll_005de4ec + 5
                                                            : s_ItemCount_005de430;
                        if (limit <= i) break;

                        bool current = i == s_ItemGridScroll_005de4ec + s_Unknown_005de4e4
                                    && s_Unknown_005de4f4 >= 0;
                        *(uint*)(d + 4) = current ? s_Unknown_005e9814 : 0x1000000u;
                        *(int*)(d + 0xc) = current ? 0x10001000 : 0x08000800;

                        byte v = s_ItemIndex_005de488[i];
                        byte* item = grid + v * 0x280;

                        // La quantite, seulement pour les objets empilables.
                        if ((item[0x13] & 0x30) != 0)
                        {
                            *(short*)(d + 0xa) = (short)(y + 0x10);
                            *(short*)(d + 8) = 0x68;
                            *(uint**)d = s_NumeralTileRecord_00566c30;
                            primCursor = EmitCardGlyphTile(otBase, primCursor, draw,
                                                           *(ushort*)(item + 0x10), 0x82, 0);
                        }

                        *(byte**)d = s_ItemIconSprite_00566b48;
                        *(short*)(d + 8) = 0x6c;
                        s_ItemIconSprite_00566b48[0] = (byte)((v % 10) * 0x18);
                        s_ItemIconSprite_00566b48[1] = (byte)((v / 10) * 0x18 - 0x78);
                        *(short*)(d + 0xa) = (short)(y + 0xc);

                        // Ghidra rend le dernier argument par deux CONCAT31 dont les trois octets
                        // hauts viennent de registres qu'il n'a pas su suivre (extraout_ECX,
                        // extraout_EDX) ; les masques 0xffff000f et 0xffff0010 ne laissent passer
                        // que le quartet bas et le bit 4 de l'index, ce dernier promu en bit 8.
                        // C'est la forme habituelle d'un identifiant de palette 4 bits.
                        uint clut = (uint)((v & 0xf) | ((v & 0x10) << 4));

                        EmitViewportRect(otBase, &primCursor);
                        if (*(short*)(d + 0xc) == 0x800)
                        {
                            // L'ombre portee : trois traces decales d'un pixel avant le vrai.
                            s_ItemIconSprite_00566b48[0]++;
                            primCursor = Prims.DrawSpritePrimitive(otBase, primCursor, draw, 2, clut);
                            s_ItemIconSprite_00566b48[1]++;
                            primCursor = Prims.DrawSpritePrimitive(otBase, primCursor, draw, 2, clut);
                            s_ItemIconSprite_00566b48[0]--;
                            primCursor = Prims.DrawSpritePrimitive(otBase, primCursor, draw, 2, clut);
                            s_ItemIconSprite_00566b48[1]--;
                        }
                        primCursor = Prims.DrawSpritePrimitive(otBase, primCursor, draw, 0, clut);
                        EmitClipRect(otBase, &primCursor);

                        *(short*)(d + 0xa) = (short)(y + 8);
                        *(uint*)(d + 4) = 0x1000000;
                        *(uint**)d = s_GlyphAtlas_00566c28;
                        *(short*)(d + 8) = 0x78;
                        byte* label = (item[0x13] & 0x80) != 0 ? item : s_UnknownItemLabel_00566b84;
                        primCursor = Prims.DrawTextRun(otBase, primCursor, draw, label, 0, 0, 0x10);
                    }

                    // La fiche de l'objet sous le curseur : huit caracteristiques sur deux
                    // rangees, deux valeurs longues, puis les huit temoins d'element. Les deux
                    // blocs sont ceux que FUN_00529290 ecrit a l'identique — voir
                    // DrawItemStatRow et DrawItemElementRow.
                    byte* detail = grid + s_ItemIndex_005de488[s_Unknown_005de4e4
                                                             + s_ItemGridScroll_005de4ec] * 0x280;
                    *(uint*)(d + 4) = 0x1000000;
                    primCursor = DrawItemStatRow(otBase, primCursor, draw, detail);
                    primCursor = DrawItemElementRow(otBase, primCursor, draw, detail);
                    break;
                }

                case 10:
                {
                    // Injoignable comme la phase 9 — voir sa note. La grille de caracteres exige
                    // une chasse fixe : la table globale des largeurs passe a 8 partout, et la
                    // copie de pile du prologue la remet en place a la sortie de la phase.
                    for (int i = 0; i < Ui.s_GlyphWidths_0056e7b0.Length; i++) Ui.s_GlyphWidths_0056e7b0[i] = 8;

                    byte* desc1 = s_ItemMenuTitleDescriptor_005658a8;
                    byte* desc2 = s_ItemMenuEditDescriptor_00566170;
                    byte* item = mainMem + 0x1b0000 + s_ItemIndex_005de488[s_ItemMenuVisible_005de4f0] * 0x280;

                    // La table qui traduit un octet de fiche en glyphe affichable.
                    byte* charset = *(byte**)(*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x70) + 8);

                    if (s_ItemMenuEntered_005de4dc != 0)
                    {
                        s_ItemMenuEntered_005de4dc = 0;
                        s_KeyColumn_005de4fc = 0;
                        s_KeyRow_005de500 = 0;
                        s_KeyPage_005de4f8 = 0;
                        s_ItemNameLength_005de504 = 0;

                        int n = 0;
                        while (n < 0x10 && item[n] != 0)
                        {
                            s_ItemNameEdit_005de4a0[n] = charset[item[n]];
                            n++;
                            s_ItemNameLength_005de504 = n;
                        }
                        s_ItemNameEdit_005de4a0[n] = 0;

                        *(int*)(desc1 + 4) = 0x00300008;
                        *(int*)(desc1 + 8) = 0xe0;
                        *(short*)(desc1 + 0xa) = 0x68;
                        desc1[0x12] = 0x20;
                        *(int*)(desc1 + 0x20) = 0x01070c28;
                        *(int*)(desc1 + 0x24) = 0x01000000;
                        *(int*)(desc2 + 4) = 0x0008002c;
                        *(int*)(desc2 + 8) = 0x98;
                        *(short*)(desc2 + 0xa) = 0x20;
                        *(int*)(desc2 + 0x20) = 0x01071028;
                        *(int*)(desc2 + 0x24) = 0x01000000;
                        desc2[0x1b] = 1;
                        desc2[0x43] = 1;

                        BindMenuLabels(LabelAt(0xd0), 0, s_ItemMenuDescriptors_00564fe0, 0);
                        BindMenuLabels(Mem.s_Unknown_005606f8, 0, desc1, 0);
                        primCursor = Ui.RunTextEngine(primCursor, desc1);
                    }

                    int cellBefore = (s_KeyPage_005de4f8 << 8 | s_KeyRow_005de500) << 8 | s_KeyColumn_005de4fc;

                    // START saute directement sur la case « fin de saisie ».
                    if ((pad[2] & 8) != 0)
                    {
                        s_KeyColumn_005de4fc = 0;
                        s_KeyRow_005de500 = 7;
                        s_KeyPage_005de4f8 = 7;
                    }

                    // Les debordements de colonne se propagent en **fabriquant** un appui de
                    // direction dans le tampon de manette, que le test de ligne juste dessous
                    // relira : c'est ainsi que le curseur passe d'un bout de ligne a l'autre.
                    if ((pad[6] & 0x80) != 0)
                    {
                        if (s_KeyColumn_005de4fc == 0)
                        {
                            if (s_KeyRow_005de500 != 0 || s_KeyPage_005de4f8 != 0)
                            {
                                s_KeyColumn_005de4fc = 9;
                                pad[6] |= 0x10;
                            }
                        }
                        else s_KeyColumn_005de4fc--;
                    }
                    if ((pad[6] & 0x20) != 0)
                    {
                        if (s_KeyColumn_005de4fc < 9) s_KeyColumn_005de4fc++;
                        else if (s_KeyRow_005de500 < 7 || s_KeyPage_005de4f8 < 7)
                        {
                            s_KeyColumn_005de4fc = 0;
                            *(ushort*)(pad + 6) |= 0x40;
                        }
                    }
                    if ((pad[6] & 0x10) != 0)
                    {
                        if (s_KeyRow_005de500 == 0)
                        {
                            if (s_KeyPage_005de4f8 != 0) s_KeyPage_005de4f8--;
                        }
                        else s_KeyRow_005de500--;
                    }
                    if ((pad[6] & 0x40) != 0)
                    {
                        if (s_KeyRow_005de500 < 7) s_KeyRow_005de500++;
                        else if (s_KeyPage_005de4f8 < 7) s_KeyPage_005de4f8++;
                    }
                    if (s_KeyRow_005de500 == 7 && s_KeyPage_005de4f8 == 7) s_KeyColumn_005de4fc = 0;

                    if (cellBefore != ((s_KeyPage_005de4f8 << 8 | s_KeyRow_005de500) << 8 | s_KeyColumn_005de4fc))
                        PlayUiSound3A();

                    bool onEndCell = s_KeyRow_005de500 == 7 && s_KeyPage_005de4f8 == 7;

                    if ((pad[3] & 0x80) == 0)
                    {
                        // CROIX : effacer le dernier caractere, ou quitter depuis la case de fin.
                        if ((pad[3] & 0x40) != 0)
                        {
                            if (s_ItemNameLength_005de504 != 0)
                            {
                                s_ItemNameLength_005de504--;
                                s_ItemNameEdit_005de4a0[s_ItemNameLength_005de504] = 0x20;
                                PlayUiSound39();
                            }
                            else if (onEndCell)
                            {
                                PlayUiSound39();
                                s_Unknown_005de4d4--;
                            }
                        }
                    }
                    else if (onEndCell)
                    {
                        s_Unknown_005de4d4--;
                        if (s_ItemNameLength_005de504 == 0)
                        {
                            PlayUiSound39();
                        }
                        else
                        {
                            PlayUiSound38();

                            // Le chemin inverse de l'entree : chaque glyphe saisi est retrouve
                            // par sa position dans la table de caracteres, et cet index est
                            // recrit dans le nom court de la fiche. L'ecriture s'arrete des
                            // qu'un glyphe est absent de la table (index 0).
                            for (int k = 0; k < 0x10; k++)
                            {
                                int index = 0;
                                byte* scan = charset;
                                if (*scan != 0)
                                {
                                    while (*scan != s_ItemNameEdit_005de4a0[k])
                                    {
                                        index++;
                                        scan++;
                                        if (*scan == 0) break;
                                    }
                                }
                                item[k] = (byte)index;
                                item[0x13] |= 0x40;
                                s_ItemRenamed_005de508 = 1;
                                if (index == 0) break;
                            }
                        }
                    }
                    else if (s_ItemNameLength_005de504 < 0xe)
                    {
                        // CARRE sur une lettre. L'adresse de la case : la table de la ligne, plus
                        // trois octets par colonne, plus deux au-dela de la cinquieme — la grille
                        // a une gouttiere au milieu.
                        byte* rowTable = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70)
                                                   + 0x50 + (s_KeyRow_005de500 + s_KeyPage_005de4f8) * 4);
                        s_KeyCellAddress_005de428 = (nint)(rowTable
                            + ((s_KeyColumn_005de4fc < 5 ? 1 : 0) - 1 & 2)
                            + s_KeyColumn_005de4fc * 3);

                        uint glyph = *(byte*)s_KeyCellAddress_005de428;

                        if (s_ItemNameLength_005de504 == 0 && glyph == (s_Unknown_00562c9c & 0xffff))
                        {
                            PlayUiSound3B();
                        }
                        else
                        {
                            PlayUiSound38();
                            s_ItemNameEdit_005de4a0[s_ItemNameLength_005de504] = (byte)glyph;
                            s_ItemNameLength_005de504++;
                            s_ItemNameEdit_005de4a0[s_ItemNameLength_005de504] = 0x20;
                            if (s_ItemNameLength_005de504 == 0xe)
                            {
                                s_KeyColumn_005de4fc = 0;
                                s_KeyRow_005de500 = 7;
                                s_KeyPage_005de4f8 = 7;
                            }
                        }
                    }

                    // --- Le trace : l'icone de l'objet, le curseur de case, le caret de saisie --
                    int* draw = stackalloc int[8];
                    byte* d = (byte*)draw;
                    NativeMemory.Clear(d, 32);

                    EmitViewportRect(otBase, &primCursor);
                    byte v = s_ItemIndex_005de488[s_ItemMenuVisible_005de4f0];
                    *(uint*)(d + 4) = 0x1000000;
                    *(byte**)d = s_ItemIconSprite_00566b48;
                    *(short*)(d + 0x10) = 0;
                    s_ItemIconSprite_00566b48[0] = (byte)((v % 10) * 0x18);
                    s_ItemIconSprite_00566b48[1] = (byte)((v / 10) * 0x18 - 0x78);
                    *(short*)(d + 0xa) = (short)(*(short*)(desc2 + 6) + 0xc);
                    *(short*)(d + 8) = (short)(*(short*)(desc2 + 4) + 0xc);
                    primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0,
                                                   (uint)((v & 0xf) | ((v & 0x10) << 4)));
                    primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);

                    *(uint**)d = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                    *(short*)(d + 0x10) = 0x18;
                    *(uint*)(d + 4) = 0x1000000;
                    *(short*)(d + 8) = (short)(s_KeyColumn_005de4fc * 0x10 + 0x18
                        + *(short*)(desc1 + 4) + ((s_KeyColumn_005de4fc < 5 ? 1 : 0) - 1 & 8));
                    *(short*)(d + 0xa) = (short)((s_KeyRow_005de500 + 1) * 0xc + *(short*)(desc1 + 6));
                    primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                    primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);

                    if (s_ItemNameLength_005de504 < 0xe)
                    {
                        *(short*)(d + 0x10) = 0x1a;
                        *(short*)(d + 8) = (short)(*(short*)(desc2 + 4)
                            + s_ItemNameLength_005de504 * 8 + desc2[0x20]);
                        *(short*)(d + 0xa) = (short)(*(short*)(desc2 + 6) + desc2[0x21]);
                        primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                        primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                    }
                    EmitClipRect(otBase, &primCursor);

                    // Les huit lignes de la grille, liees une par une dans le bandeau.
                    BindMenuLabels(Mem.s_Unknown_005606f8, 0, desc1, 0);
                    primCursor = Ui.RunTextEngine(primCursor, desc1);
                    for (int n = 0; n < 8; n++)
                    {
                        BindMenuLabels(LabelAt(0x50 + (n + s_KeyPage_005de4f8) * 4), 0, desc1, 0);
                        primCursor = Ui.RunTextEngine(primCursor, desc1);
                    }

                    BindMenuLabels(Mem.s_Unknown_005606f8, 0, desc2, 0);
                    primCursor = Ui.RunTextEngine(primCursor, desc2);
                    BindMenuLabels(s_ItemNameEdit_005de4a0, 0, desc2, 0);
                    primCursor = Ui.RunTextEngine(primCursor, desc2);
                    primCursor = DrawUiPanel(otBase, primCursor, desc2);

                    // La table des largeurs remise comme le prologue l'avait relevee.
                    for (int i = 0; i < Ui.s_GlyphWidths_0056e7b0.Length; i++) Ui.s_GlyphWidths_0056e7b0[i] = cmd[i];
                    break;
                }

                // --- La reecriture du fichier apres un renommage ---------------------------
                // Enchainement : 7 (attente) -> 0xb -> 0xc -> 0xd (choix de l'emplacement de
                // carte) -> 0xe (ouverture) -> 0xf -> 0x10 -> 0x11 (l'ecriture) -> 0x12 (fin).
                // Les 0x5e..0x62 sont les sorties d'erreur, les negatives les abandons.
                // Comme les phases 9 et 10, ce chemin n'est joignable qu'a travers elles :
                // DAT_005de508 n'est leve que par le renommage.

                case 7:
                    if ((pad[3] & 0x40) != 0)
                    {
                        s_Unknown_0056501b = 0;
                        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xfb);
                        PlayUiSound39();
                        if (s_ItemRenamed_005de508 != 0) { s_Unknown_005de4d4 = 0xb; break; }
                    }
                    else if ((s_ItemMenuDialogGate_005658a7 & 2) != 0) break;
                    goto case 0x12;

                case 0xb:
                    if (s_ItemMenuEntered_005de4dc != 0)
                    {
                        s_ItemMenuEntered_005de4dc = 0;
                        BindMenuLabels(LabelAt(0x28), 0, s_ItemMenuDescriptors_00564fe0, 0);
                        break;
                    }
                    if ((s_ItemMenuDialogGate_005658a7 & 2) != 0)
                    {
                        if ((pad[3] & 0x40) == 0) break;
                        s_Unknown_00565013 = 0;
                        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xfd);
                        PlayUiSound39();
                        s_Unknown_005de4d4 = 0;
                        s_ItemMenuSubState_00566ac8 = -1;
                        s_Unknown_00566acc = -1;
                        break;
                    }
                    BindMenuLabels(LabelAt(0), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    s_Unknown_005de4d4++;
                    break;

                case 0xc:
                    if ((s_ItemMenuDialogGate_005658a7 & 4) != 0)
                    {
                        if ((pad[3] & 0x40) == 0) break;
                        s_Unknown_0056501b = 0;
                        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xfb);
                        PlayUiSound39();
                        goto case 0x12;
                    }
                    if (Mem.s_Unknown_005e909f != 0) goto case 0x12;
                    s_Unknown_005de4d4++;
                    break;

                case 0xd:
                {
                    // Le choix de l'emplacement de carte : haut et bas font tourner le curseur
                    // dans la liste du branchement courant.
                    int slotBefore2 = s_ItemMenuCardSlot_005de4e8;
                    if ((pad[6] & 0x80) != 0) s_ItemMenuCardSlot_005de4e8--;
                    if ((pad[6] & 0x20) != 0) s_ItemMenuCardSlot_005de4e8++;

                    BindMenuLabels(LabelAt(4), 0, s_ItemMenuDescriptors_00564fe0, 0);

                    sbyte count = (sbyte)s_CardSlotLists_00566a90[s_Unknown_005de50c][0];
                    if (s_ItemMenuCardSlot_005de4e8 < 0) s_ItemMenuCardSlot_005de4e8 = count - 1;
                    if (count <= s_ItemMenuCardSlot_005de4e8) s_ItemMenuCardSlot_005de4e8 = 0;
                    if (slotBefore2 != s_ItemMenuCardSlot_005de4e8) PlayUiSound3A();

                    if ((pad[3] & 0x80) != 0)
                    {
                        s_CardDialogStep_005de468 = 0;
                        s_Unknown_005de4d4++;
                        s_CardDialogResult_005de484 = 0;
                        s_Unknown_00604960 = 0;
                        s_Unknown_0060498a = 0;
                        PlayUiSound38();
                        s_CardSlotIndex_00604980 =
                            s_CardSlotIds_00566a58[s_Unknown_005de50c][s_ItemMenuCardSlot_005de4e8];
                        BindMenuLabels(LabelAt(8), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    }
                    else if ((pad[3] & 0x40) != 0)
                    {
                        PlayUiSound39();
                        s_Unknown_005de4d4 = 0;
                        s_ItemMenuSubState_00566ac8 = -1;
                        s_Unknown_00566acc = -1;
                    }
                    break;
                }

                case 0xe:
                {
                    if (s_ItemMenuEntered_005de4dc != 0) { s_ItemMenuEntered_005de4dc = 0; break; }

                    s_CardCursorSlot_00604984 = 0;

                    // Ghidra type FUN_005126c0 en `undefined4` et distingue ici quatre retours.
                    // La transcription rend toujours 0 : tous les chemins observes du corps le
                    // font (voir LoadSaveDirectory). Les trois autres branches sont conservees
                    // parce qu'elles decrivent le dialogue d'erreur du dispositif.
                    // TODO(REVERSE) : etablir ce que rend reellement FUN_005126c0 sur echec —
                    // cela demande une re-passe Ghidra, la pseudo-C ne montre que des `return 0`.
                    int status = LoadSaveDirectory();
                    ushort* message = null;

                    switch (status)
                    {
                        case 0:
                        {
                            Saves.SetCardFileName(record + 0x1fd0);
                            Saves.SetCardPathDeviceFields();
                            IntPtr probe = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                            Saves.RetryCloseCardFile(probe);

                            if ((long)probe >= 0)
                            {
                                BindMenuLabels(LabelAt(0x18), 0, s_ItemMenuDescriptors_00564fe0, 0);
                                Saves.ResetCardTransferStatus((s_CardSlotIndex_00604980 & 3)
                                                        + (s_CardSlotIndex_00604980 & 4) * 4);
                                s_Unknown_005de4d4++;
                                s_CardDialogStep_005de468 = 3;
                                goto tail;
                            }

                            // Le message « fichier introuvable » : prefixe, nom du personnage,
                            // suffixe, chacun elargi d'un octet a deux.
                            ushort* dest = s_CardMessage_005de390;
                            for (byte* src = LabelAt(0x3c); *src != 0; src += 2)
                                *dest++ = (ushort)(src[0] | (src[1] << 8));
                            for (byte* src = record + 0x100; *src != 0; src += 2)
                                *dest++ = (ushort)(src[0] | (src[1] << 8));
                            for (byte* src = LabelAt(0x40); *src != 0; src += 2)
                                *dest++ = (ushort)(src[0] | (src[1] << 8));
                            *dest = 0;
                            message = s_CardMessage_005de390;
                            break;
                        }

                        case -3:
                            message = *(ushort**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0xc);
                            break;

                        case -2:
                            BindMenuLabels(LabelAt(0x10), 0, s_ItemMenuDescriptors_00564fe0, 0);
                            Saves.ResetCardTransferStatus((s_CardSlotIndex_00604980 & 3)
                                                    + (s_CardSlotIndex_00604980 & 4) * 4);
                            s_CardDialogStep_005de468 = 2;
                            s_Unknown_005de4d4 = 0x60;
                            goto tail;

                        case -1:
                            message = *(ushort**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x2c);
                            break;

                        default:
                            goto tail;
                    }

                    BindMenuLabels((byte*)message, 0, s_ItemMenuDescriptors_00564fe0, 0);
                    s_Unknown_005de4d4 = 0x62;
                    break;
                }

                case 0xf:
                    if ((pad[3] & 0x40) != 0) { s_Unknown_005de4d4 = 0x5e; break; }
                    if ((s_ItemMenuDialogGate_005658a7 & 2) != 0) break;
                    BindMenuLabels(LabelAt(0), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    s_Unknown_005de4d4++;
                    break;

                case 0x10:
                    if ((pad[3] & 0x40) != 0) { s_Unknown_005de4d4 = 0x5e; break; }
                    if ((s_ItemMenuDialogGate_005658a7 & 4) != 0) break;
                    if (Mem.s_Unknown_005e909f != 0) { s_Unknown_005de4d4 = 0x5e; break; }
                    BindMenuLabels(LabelAt(0x30), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    s_Unknown_005de4d4++;
                    break;

                case 0x11:
                {
                    // L'ecriture. Comme la phase 5, elle est decalee d'une image ; celle qui entre
                    // met le bloc de travail de cote (0x1b0000 -> 0x1b8000) pour pouvoir le
                    // restaurer si l'ecriture echoue.
                    if (s_ItemMenuEntered_005de4dc != 0)
                    {
                        Mem.CopyDwords((uint*)(mainMem + 0x1b0000), (uint*)(mainMem + 0x1b8000), 0xdc0);
                        s_ItemMenuEntered_005de4dc = 0;
                        break;
                    }

                    s_ItemMenuRecord_005de410 = (int)(nint)(mainMem + 0xf0000);
                    record = (byte*)(nint)s_ItemMenuRecord_005de410;

                    Saves.SetCardFileName(mainMem + 0xf1fd0);
                    Saves.SetCardPathDeviceFields();
                    Saves.PollCardReadStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                    Saves.ResetCardWriteEvents();

                    // Les objets retenus par le personnage repassent dans sa fiche, dans l'ordre,
                    // et les references d'equipement (+0x2b0, cinq octets) suivent leur nouveau
                    // rang. Seule la partie utile d'un emplacement est recopiee : 0xa0 dwords.
                    int kept = 0, keptOffset = 0;
                    for (int i = 0; i < s_ItemCount_005de430; i++)
                    {
                        byte slot = s_ItemIndex_005de488[i];
                        if (s_ItemHeld_005de2d8[slot] == 0) continue;

                        Mem.CopyDwords((uint*)(mainMem + 0x1b0000 + slot * 0x280),
                                   (uint*)(record + 0x300 + keptOffset), 0xa0);
                        for (int e = 0; e < 5; e++)
                            if (record[0x2b0 + e] == slot) record[0x2b0 + e] = (byte)kept;
                        kept++;
                        keptOffset += 0x280;
                    }
                    for (int off = kept * 0x280; off < 0x1900; off += 0x280) record[0x300 + off] = 0;

                    record[0x2c0] = 0;
                    for (int n = 0, off = 0; off < 0x1900; n++, off += 0x280)
                    {
                        if (record[0x300 + off] == 0) continue;
                        record[0x2c1 + record[0x2c0]] = (byte)n;
                        record[0x2c0]++;
                    }

                    UploadPartyIconTextures();

                    // Les objets **non** retenus sont tasses en tete du bloc de travail, qui part
                    // dans le second bloc du fichier ; le reste des douze est mis a zero.
                    int loose = 0, looseOffset = 0;
                    for (int i = 0; i < s_ItemCount_005de430; i++)
                    {
                        byte slot = s_ItemIndex_005de488[i];
                        if (s_ItemHeld_005de2d8[slot] != 0) continue;

                        Mem.CopyDwords((uint*)(mainMem + 0x1b0000 + slot * 0x280),
                                   (uint*)(mainMem + 0x1b0000 + looseOffset), 0xa0);
                        loose++;
                        looseOffset += 0x280;
                    }
                    for (int i = loose; i < 0xc; i++) mainMem[0x1b0000 + i * 0x280] = 0;

                    // La somme de controle de reference est relue a 0x3f00 avant de recalculer.
                    IntPtr readBack = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                    int verify = Saves.RetrySeekAndReadCardFile(readBack, mainMem + 0x1b4000, 0x3f00, 0x100);
                    Saves.RetryCloseCardFile(readBack);

                    bool written = false;
                    byte* failLabel = LabelAt(0x48);

                    if (verify >= 0)
                    {
                        if (*(int*)(mainMem + 0x1b40fc) == *(int*)(record + 0x1ffc))
                        {
                            int sum = 0;
                            for (int i = 0; i < 0x1ffc; i++) sum += (sbyte)record[i];
                            *(int*)(record + 0x1ffc) = sum;

                            IntPtr write = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 2);
                            int w1 = Saves.RetrySeekAndWriteCardFile(write, record, 0, 0x2000);
                            int w2 = Saves.RetrySeekAndWriteCardFile(write, mainMem + 0x1b0000, 0x2000, 0x1f00);
                            int w3 = Saves.RetrySeekAndWriteCardFile(write, record + 0x1f00, 0x3f00, 0x100);
                            Saves.RetryCloseCardFile(write);

                            if ((w1 | w2 | w3) >= 0)
                            {
                                BindMenuLabels(LabelAt(0x44), 0, s_ItemMenuDescriptors_00564fe0, 0);
                                s_Unknown_005de4d4++;
                                s_CardDialogStep_005de468 = 0;
                                written = true;
                            }
                        }
                        else failLabel = LabelAt(0x4c);
                    }

                    if (!written)
                    {
                        BindMenuLabels(failLabel, 0, s_ItemMenuDescriptors_00564fe0, 0);
                        Mem.CopyDwords((uint*)(mainMem + 0x1b8000), (uint*)(mainMem + 0x1b0000), 0xdc0);
                        s_Unknown_005de4d4 = 0x62;
                        s_CardDialogStep_005de468 = 0;
                    }
                    break;
                }

                case 0x12:
                    if ((pad[3] & 0x40) == 0)
                    {
                        if ((s_ItemMenuDialogGate_005658a7 & 2) != 0) break;
                    }
                    else
                    {
                        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xf9);
                        s_Unknown_0056501b = 0;
                        s_Unknown_00565013 = 0;
                    }
                    s_Unknown_005de4d4 = 0;
                    s_ItemMenuSubState_00566ac8 = -1;
                    s_Unknown_00566acc = -1;
                    break;

                case 0x5e:
                    Saves.PollCardReadStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                    Saves.ResetCardWriteEvents();
                    goto case 0x5f;

                case 0x5f:
                    s_Unknown_0056501b = 0;
                    s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xf9);
                    s_Unknown_00565013 = 0;
                    s_Unknown_005de4d4 = 0xd;
                    s_CardDialogStep_005de468 = 0;
                    PlayUiSound39();
                    break;

                case 0x60:
                case 0x61:
                    if ((pad[3] & 0x40) != 0)
                    {
                        s_CardDialogStep_005de468 = 0;
                        s_Unknown_005de4d4 = 0x5e;
                    }
                    else if (s_CardDialogResult_005de484 == -3)
                    {
                        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xfd);
                        s_Unknown_00565013 = 0;
                        s_CardDialogStep_005de468 = 0;
                        s_Unknown_005de4d4 = 0xe;
                        BindMenuLabels(LabelAt(8), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    }
                    else if ((s_ItemMenuDialogGate_005658a7 & 2) == 0)
                    {
                        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xfd);
                        s_Unknown_00565013 = 0;
                        Saves.PollCardReadStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                        Saves.ResetCardWriteEvents();
                        s_CardDialogStep_005de468 = 0;
                        s_Unknown_005de4d4 = 0xd;
                    }
                    break;

                case 0x62:
                    if ((s_ItemMenuDialogGate_005658a7 & 2) == 0)
                    {
                        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xfb);
                        s_Unknown_0056501b = 0;
                        s_Unknown_005de4d4 = 0xd;
                    }
                    break;

                case 99:
                    // Ghidra: `((3 < DAT_005e9020) - 1 & 0xfffffffc) + 4` — 4 si le groupe compte
                    // plus de trois membres, 0 sinon.
                    if ((s_ItemMenuDialogGate_005658a7 & 2) != 0) break;
                    s_Unknown_005de4d4 = s_PartySize_005e9020 > 3 ? 4 : 0;
                    s_Unknown_00566acc = -1;
                    break;

                case -5:
                    Saves.PollCardReadStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                    Saves.ResetCardWriteEvents();
                    goto case -4;

                case -4:
                    s_Unknown_0056501b = 0;
                    s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xf9);
                    s_Unknown_00565013 = 0;
                    s_Unknown_005de4d4 = 0;
                    s_Unknown_00566acc = -1;
                    PlayUiSound39();
                    break;

                case -3:
                case -2:
                    if ((pad[3] & 0x40) != 0) { s_Unknown_005de4d4 = -5; break; }
                    if (s_CardDialogResult_005de484 == -3)
                    {
                        s_CardDialogStep_005de468 = 0;
                        s_Unknown_005de4d4 = 0;
                        break;
                    }
                    if ((s_ItemMenuDialogGate_005658a7 & 2) != 0) break;
                    Saves.PollCardReadStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                    Saves.ResetCardWriteEvents();
                    s_Unknown_005de4d4 = 0;
                    s_Unknown_00566acc = -1;
                    break;

                case -1:
                    if ((s_ItemMenuDialogGate_005658a7 & 2) != 0) break;
                    s_Unknown_005de4d4 = 0;
                    s_Unknown_00566acc = -1;
                    break;

                default:
                    break;
            }
        }

    tail:
        // --- La queue commune : le portrait et le nom du personnage ---------------------------
        // Ghidra: switchD_00524e4e_caseD_2. Elle ne s'execute que du chargement fait jusqu'a la
        // sauvegarde, et jamais pendant le renommage (phase 10) qui occupe le meme cadre.
        if (s_Unknown_00566acc >= 0 && s_Unknown_005de4d4 > 5 && s_Unknown_005de4d4 < 99
            && s_ItemMenuPrevPhase_005de4d8 != 10)
        {
            EmitViewportRect(otBase, &primCursor);

            int* draw = stackalloc int[8];
            byte* d = (byte*)draw;
            NativeMemory.Clear(d, 32);
            *(uint*)(d + 4) = 0x1000000;
            *(uint**)d = *(uint**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 8);
            *(ushort*)(d + 0x10) = record[0x150];
            *(short*)(d + 8) = 0x20;
            *(short*)(d + 0xa) = 0x30;
            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, (uint)record[0x2bd] << 8);
            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
            EmitClipRect(otBase, &primCursor);

            // Le bandeau de nom, remonte de quatre pixels le temps du trace puis remis.
            byte* edit = s_ItemMenuEditDescriptor_00566170;
            *(short*)(edit + 6) -= 4;
            edit[0x12] = 9;

            BindMenuLabels(Mem.s_Unknown_005606f8, 0, edit, 0);
            primCursor = Ui.RunTextEngine(primCursor, edit);

            byte* name = record + 0x100;
            int length = 0;
            while (name[length] != 0) length++;
            for (int i = 0; i <= length; i++) cmd[1 + i] = name[i];
            cmd[0] = 0x20;

            BindMenuLabels(cmd, 0, edit, 0);
            primCursor = Ui.RunTextEngine(primCursor, edit);
            primCursor = DrawUiPanel(otBase, primCursor, edit);

            *(short*)(edit + 6) += 4;
            edit[0x12] = 0x1a;
        }

        if (s_Unknown_005de4d4 < 8 || s_Unknown_005de4d4 > 10)
            primCursor = DrawItemMenuTitle(primCursor);

        return primCursor;
    }

    private static ref int s_ItemMenuScroll_005de4c4 => ref *Mem.ItemMenuState(0x005de4c4);
    private static ref int s_ItemMenuSelection_005de4c8 => ref *Mem.ItemMenuState(0x005de4c8);
    private static ref int s_ItemMenuChosen_005de4cc => ref *Mem.ItemMenuState(0x005de4cc);
    private static ref int s_ItemMenuPhase_005de4d0 => ref *Mem.ItemMenuState(0x005de4d0);
    private static ref int s_Unknown_005de4e4 => ref *Mem.ItemMenuState(0x005de4e4);
    private static ref int s_ItemMenuLoaded_005de4e8 => ref *Mem.ItemMenuState(0x005de4e8);

    /// <summary>
    /// Ghidra: FUN_005277b0 @ 0x005277b0 — le sous-écran « Enchère » : la même liste de
    /// sauvegardes que l'écran d'organisation du groupe, dans laquelle on choisit jusqu'à quatre
    /// personnages, mais rendue plus à gauche et plus haut (x 0x28 au lieu de 0x48, y +0x26 au lieu
    /// de +0x32) parce que la page du menu objets est cadrée en 256x224.
    ///
    /// <para>Il partage sa construction de lignes avec <see cref="RunPartyConstitution"/> — voir
    /// <see cref="BuildSaveDirectoryRows"/>. Ce qui lui est propre : il ne charge la liste qu'une
    /// fois (<see cref="s_ItemMenuLoaded_005de4e8"/>), il sort vers le sous-écran 3 plutôt que vers
    /// un état de jeu, et sa sortie par CROIX ramène à l'écran racine.</para>
    ///
    /// <para><b>Aucune sauvegarde du tout</b> : il repasse aussitôt à l'écran racine, sans rien
    /// afficher — le pendant du <c>s_GameStateId = 4</c> de l'écran d'organisation.</para>
    /// </summary>
    private static uint* ItemMenuAuctionScreen(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        s_PartySize_005e9020 = 0;
        byte* mainMem = GetMainMemPtr();

        if (s_ItemMenuLoaded_005de4e8 == 0)
        {
            Inp.JoystickResetState(false);
            LoadSaveDirectory();

            int count = s_CardEntryCount_00604981;
            BuildSaveDirectoryRows(mainMem + 0xf0000, count);

            // Ghidra: 0x19 dwords à zéro depuis &DAT_005e85c0.
            NativeMemory.Clear(s_PartySelectionOrder_005e85c0, 0x64);

            if (count == 0)
            {
                s_ItemMenuSubState_00566ac8 = -1;
                return primCursor;
            }

            BindMenuLabels(Mem.s_Unknown_005606f8, 0, s_ItemMenuDescriptors_00564fe0, 0);
            BindMenuLabels(LabelAt(0x34), 0, s_ItemMenuTitleDescriptor_005658a8, 0);
            s_ItemMenuLoaded_005de4e8++;
            s_ItemMenuPhase_005de4d0 = 0;
        }
        else if (s_ItemMenuLoaded_005de4e8 != 1)
        {
            return primCursor;
        }

        int entryCount = s_CardEntryCount_00604981;

        if (s_ItemMenuPhase_005de4d0 != 0)
        {
            if (s_ItemMenuPhase_005de4d0 == 3)
            {
                if ((pad[3] & 0xc0) != 0) { s_GameStateId = 0; return primCursor; }
            }
            else if (s_ItemMenuPhase_005de4d0 == 4)
            {
                // Le rechargement des membres retenus, dans le même ordre que l'écran
                // d'organisation du groupe : l'entrée dont l'ordre vaut la place courante + 1, et
                // l'entrée 0 en repli si aucune ne correspond.
                byte* member = mainMem + 0xf0000;
                s_PartySize_005e9020 = 0;
                while (s_PartySize_005e9020 < s_ItemMenuChosen_005de4cc)
                {
                    int index = 0;
                    while (index < entryCount
                        && s_PartySelectionOrder_005e85c0[index] != s_PartySize_005e9020 + 1) index++;
                    if (index == entryCount) index = 0;

                    Saves.SetCardFileName(s_CardDirectoryEntries_006049bc + index * 0x28);
                    Saves.SetCardPathDeviceFields();

                    *(uint*)(member + 0x1ffc) = 0;
                    IntPtr handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                    Saves.RetrySeekAndReadCardFile(handle, member, 0, 0x2000);
                    Saves.RetryCloseCardFile(handle);

                    s_PartySize_005e9020++;
                    member += 0x2000;
                }

                // Ghidra: tout le bloc de sélection remis à zéro, puis passage au sous-écran 3.
                s_ItemMenuScroll_005de4c4 = 0;
                s_ItemMenuSelection_005de4c8 = 0;
                s_ItemMenuChosen_005de4cc = 0;
                s_ItemMenuPhase_005de4d0 = 0;
                s_ItemMenuSubState_00566ac8 = 3;
                s_ItemMenuCursor_005de4e0 = 0;
                s_Unknown_005de4e4 = 0;
                s_ItemMenuLoaded_005de4e8 = 0;
                UploadPartyIconTextures();   // Ghidra: FUN_00535500
            }
        }
        else
        {
            // --- La navigation, seulement hors phase ----------------------------------------
            ushort repeat = *(ushort*)(pad + 6);

            if ((repeat & 0x40) != 0)
            {
                if (s_ItemMenuSelection_005de4c8 < entryCount - 1)
                {
                    int next = s_ItemMenuSelection_005de4c8 + 1;
                    if (s_ItemMenuScroll_005de4c4 + 5 < next)
                        s_ItemMenuScroll_005de4c4 = s_ItemMenuSelection_005de4c8 - 4;
                    s_ItemMenuSelection_005de4c8 = next;
                    PlayUiSound3A();
                }
            }
            else if ((repeat & 0x10) != 0 && s_ItemMenuSelection_005de4c8 > 0)
            {
                s_ItemMenuSelection_005de4c8--;
                if (s_ItemMenuSelection_005de4c8 < s_ItemMenuScroll_005de4c4)
                    s_ItemMenuScroll_005de4c4 = s_ItemMenuSelection_005de4c8;
                PlayUiSound3A();
            }

            // START, avec au moins un personnage retenu : passer au rechargement.
            if ((pad[2] & 8) != 0 && s_ItemMenuChosen_005de4cc > 0)
            {
                PlayUiSound38();
                s_ItemMenuPhase_005de4d0 = 4;
            }

            // CARRÉ : retenir ou retirer la ligne courante — même resserrement des rangs que
            // l'écran d'organisation du groupe.
            if ((pad[3] & 0x80) != 0)
            {
                int row = s_ItemMenuSelection_005de4c8;
                if (s_PartySelectionOrder_005e85c0[row] == 0)
                {
                    if (s_ItemMenuChosen_005de4cc < 4)
                    {
                        s_ItemMenuChosen_005de4cc++;
                        s_PartySelectionOrder_005e85c0[row] = (byte)s_ItemMenuChosen_005de4cc;
                        PlayUiSound38();
                        if (s_ItemMenuChosen_005de4cc == 4) s_ItemMenuPhase_005de4d0 = 4;
                    }
                    else
                    {
                        PlayUiSound3B();
                    }
                }
                else
                {
                    byte removed = s_PartySelectionOrder_005e85c0[row];
                    for (int i = 0; i < entryCount; i++)
                        if (removed < s_PartySelectionOrder_005e85c0[i])
                            s_PartySelectionOrder_005e85c0[i]--;
                    s_PartySelectionOrder_005e85c0[row] = 0;
                    s_ItemMenuChosen_005de4cc--;
                    PlayUiSound38();
                }
            }

            // CROIX : revenir à l'écran racine.
            if ((pad[3] & 0x40) != 0) s_ItemMenuSubState_00566ac8 = -1;
        }

        // --- Les six lignes visibles ------------------------------------------------------
        int windowEnd = s_ItemMenuScroll_005de4c4 + 6;
        if (entryCount < windowEnd) windowEnd = entryCount;

        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;
        NativeMemory.Clear(d, 32);

        for (int i = s_ItemMenuScroll_005de4c4; i < windowEnd; i++)
        {
            byte* row = s_PartyRowText_005e8640 + i * PartyRowStride;
            bool chosen = s_PartySelectionOrder_005e85c0[i] != 0;

            EmitViewportRect(otBase, &primCursor);

            int bank = i >> 4;
            *(short*)(d + 0x10) = 0;
            s_RowSpriteRecord_00563148[0] = (byte)(i << 4);
            s_RowSpriteRecord_00563148[1] = (byte)(bank * 0x40 + 0x20);
            *(byte**)d = s_RowSpriteRecord_00563148;
            *(uint*)(d + 4) = 0x1000000;
            *(short*)(d + 8) = 0x28;
            *(short*)(d + 0xa) = (short)((i - s_ItemMenuScroll_005de4c4) * 0x14 + 0x26);

            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xf8) = 0xf;
            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfa) =
                (ushort)(((i << 4) >> 4) & 0xf | (bank + 0x1fe) * 0x40);

            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, chosen ? 0u : 3u, 0);
            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
            EmitClipRect(otBase, &primCursor);

            *(short*)(d + 8) += 0x1e;
            *(short*)(d + 0xa) += 4;
            *(uint*)(d + 4) = chosen ? 0x1000000u : 0x404040u;
            *(byte**)d = (byte*)s_GlyphAtlas_00566c28;
            primCursor = Prims.DrawTextRun(otBase, primCursor, draw, row, 0, 0, PartyRowStride);

            if (s_ItemMenuSelection_005de4c8 == i)
            {
                EmitViewportRect(otBase, &primCursor);
                *(uint**)d = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                *(short*)(d + 0xa) -= 2;
                *(short*)(d + 0x10) = 0x18;
                *(uint*)(d + 4) = 0x1000000;
                *(short*)(d + 8) = 0x14;
                primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                EmitClipRect(otBase, &primCursor);
            }
        }

        return primCursor;
    }

    /// <summary>
    /// Ghidra: FUN_005298c0 @ 0x005298c0 — la fermeture du menu objets : les quatre membres du
    /// groupe sont **réécrits dans leurs fichiers de sauvegarde**, chacun avec sa liste d'objets
    /// fusionnée.
    ///
    /// <para>Par membre : on rouvre son fichier (le nom vient de son propre enregistrement, à
    /// <c>+0x1fd0</c> — le même champ que la création y dépose), on relit deux extents dans le
    /// tampon de travail, on y fusionne les objets, on recalcule la somme de contrôle, et on
    /// réécrit trois extents.</para>
    ///
    /// <para><b>Deux bizarreries de l'original, transcrites telles quelles.</b> D'abord les deux
    /// lectures visent le **même tampon** : celle de <c>0x3f00</c> sur 0x100 octets est aussitôt
    /// écrasée par celle de <c>0x4000</c> sur 0x2000. Ensuite <c>DAT_005de4e0</c>, qui est le
    /// curseur de l'écran racine, sert ici de compteur de membre et ressort à 4 ; l'écran racine le
    /// ramène à 0 par son propre plafonnement à la première image suivante.</para>
    /// </summary>
    private static void CloseItemMenu()
    {
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return;

        byte* merged = mainMem + 0x1b4000;

        for (s_ItemMenuCursor_005de4e0 = 0; s_ItemMenuCursor_005de4e0 < 4; s_ItemMenuCursor_005de4e0++)
        {
            byte* record = mainMem + (s_ItemMenuCursor_005de4e0 + 0x78) * 0x2000;

            Saves.SetCardFileName(record + 0x1fd0);
            Saves.SetCardPathDeviceFields();

            IntPtr handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
            Saves.RetrySeekAndReadCardFile(handle, merged, 0x3f00, 0x100);
            Saves.RetryCloseCardFile(handle);

            handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
            Saves.RetrySeekAndReadCardFile(handle, merged, 0x4000, 0x2000);
            Saves.RetryCloseCardFile(handle);

            // Dix emplacements d'objet à +0x300, pas de 0x280 octets. Un emplacement n'est fusionné
            // que si son mot de drapeaux à +0x12 porte 0x8000 sans 0x4000.
            byte* entry = record + 0x300;
            for (int slot = 0; slot < 10; slot++, entry += 0xa0 * sizeof(int))
            {
                ushort flags = *(ushort*)(entry + 0x12);
                if ((flags & 0x4000) != 0 || (flags & 0x8000) == 0) continue;

                // La liste fusionnée est une suite d'enregistrements de 0x10 octets, terminée par
                // un premier octet nul. On y cherche les quatre premiers dwords de l'emplacement.
                byte* candidate = merged;
                if (*candidate != 0)
                {
                    bool found = false;
                    while (true)
                    {
                        int same = 0;
                        while (same < 4 && ((int*)entry)[same] == ((int*)candidate)[same]) same++;
                        if (same == 4) { found = true; break; }
                        candidate += 0x10;
                        if (*candidate == 0) break;
                    }
                    if (found || *candidate != 0) continue;
                }

                // Absent : on l'ajoute sur place, à l'octet terminateur.
                for (int i = 0; i < 0x10; i++) candidate[i] = entry[i];
            }

            // La même somme de contrôle signée sur 0x1ffc octets que partout ailleurs.
            int checksum = 0;
            for (int i = 0; i < 0x1ffc; i++) checksum += (sbyte)record[i];
            *(int*)(record + 0x1ffc) = checksum;

            handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 2);
            Saves.RetrySeekAndWriteCardFile(handle, record, 0, 0x2000);
            Saves.RetrySeekAndWriteCardFile(handle, record + 0x1f00, 0x3f00, 0x100);
            Saves.RetrySeekAndWriteCardFile(handle, merged, 0x4000, 0x2000);
            Saves.RetryCloseCardFile(handle);
        }
    }

    // ---------------------------------------------------------------------------------------
    // Le sous-écran d'utilisation d'objet (Ghidra: FUN_00527e30) et son résumé de groupe
    // (FUN_00529590).
    // ---------------------------------------------------------------------------------------

    /// <summary>Ghidra: DAT_005de4f0 — le membre visé par un transfert d'objet.</summary>
    private static ref int s_ItemMenuTarget_005de4f0 => ref *Mem.ItemMenuState(0x005de4f0);

    /// <summary>
    /// Ghidra: DAT_005de464 — **quatre octets, un par membre**, et non un entier isolé : le cas 3
    /// écrit <c>((byte*)&amp;DAT_005de464)[membre] = 1</c> tandis que le cas 0 relit le dword entier
    /// pour savoir si un transfert a eu lieu. Adressé ici des deux façons, comme l'original.
    /// </summary>
    private static byte* ItemMenuTransferFlags_005de464 => (byte*)Mem.ItemMenuState(0x005de464);

    /// <summary>
    /// Ghidra: DAT_005de2d0 — la valeur d'objet que les cas 0, 1 et 2 recalculent chacun de leur
    /// côté ; une globale de travail, pas un état.
    /// </summary>
    private static uint s_ItemValue_005de2d0;

    /// <summary>Ghidra: DAT_005de448 — la meilleure mise de l'enchere, -1 tant que personne n'a
    /// mise. C'est le dword juste apres les quatre de <c>DAT_005de438</c>.</summary>
    private static ref int s_Unknown_005de448 => ref *Mem.ItemMenuState(0x005de448);

    /// <summary>
    /// Ghidra: l'echange de manette entre deux emplacements, ecrit **trois fois** a l'identique
    /// dans le cas 2 de <c>FUN_00527e30</c> : le membre cede sa manette a
    /// <paramref name="target"/> et recupere la sienne (toujours 8, « aucune »), les seconds
    /// octets etant permutes avec.
    /// </summary>
    private static void HandOverController(byte* controllerSlot, int target)
    {
        byte* destination = Inp.s_PlayerSlots_005e9188 + target * 0x4c;
        destination[0] = controllerSlot[0];
        controllerSlot[0] = 8;
        byte swap = destination[1];
        destination[1] = controllerSlot[1];
        controllerSlot[1] = swap;
    }

    /// <summary>Ghidra: la recherche d'un emplacement libre **vers l'arriere** (GAUCHE), avec
    /// bouclage. Rend 1 si la manette a bouge, 0 si aucun emplacement n'etait libre.</summary>
    private static int MoveControllerBackward(byte* controllerSlot, int member)
    {
        if (s_PartySize_005e9020 == 0) return s_PartySize_005e9020;

        int probe = member;
        for (int tries = s_PartySize_005e9020; tries != 0; tries--)
        {
            probe--;
            if (probe < 0) probe = s_PartySize_005e9020 - 1;
            if (Inp.s_PlayerSlots_005e9188[probe * 0x4c] == 8 && probe != s_Unknown_005de4e4)
            {
                HandOverController(controllerSlot, probe);
                return 1;
            }
        }
        return 0;
    }

    /// <summary>Ghidra: la meme recherche **vers l'avant** (DROITE).</summary>
    private static int MoveControllerForward(byte* controllerSlot, int member)
    {
        if (s_PartySize_005e9020 == 0) return s_PartySize_005e9020;

        int probe = member;
        for (int tries = s_PartySize_005e9020; tries != 0; tries--)
        {
            probe++;
            if (probe >= s_PartySize_005e9020) probe = 0;
            if (Inp.s_PlayerSlots_005e9188[probe * 0x4c] == 8 && probe != s_Unknown_005de4e4)
            {
                HandOverController(controllerSlot, probe);
                return 1;
            }
        }
        return 0;
    }

    /// <summary>
    /// Ghidra: la remise a neuf des quatre emplacements a la fin de l'enchere — **le meme bloc
    /// ecrit deux fois** dans le cas 2, sur la sortie sans mise et sur l'adjudication.
    ///
    /// <para>Le pas de <c>puVar5</c> vaut 0x4e et non 0x9c parce que Ghidra le type en
    /// <c>undefined2 *</c> : 0x4e mots de 16 bits font bien 0x9c octets. Le
    /// <c>*(undefined4 *)(puVar5 + -9)</c> qui l'accompagne est le dword de
    /// <c>Inp.s_SlotFlags_005e93c0 + i * 0x9c</c>, dix-huit octets en amont.</para>
    /// </summary>
    private static void ResetAuctionControllers()
    {
        byte[] slotIds = s_CardSlotIds_00566a58[s_Unknown_005de50c];

        // Ghidra: `while ((int)puVar6 < 0x5e92b8)` depuis 0x5e9188 par pas de 0x4c — quatre tours.
        for (int i = 0; i < 4; i++)
        {
            Inp.s_PlayerSlots_005e9188[i * 0x4c + 1] = (byte)i;
            *(uint*)(Inp.s_SlotFlags_005e93c0 + i * 0x9c) = 0;
            Inp.s_PlayerSlots_005e9188[i * 0x4c] = 8;
            *(ushort*)(Inp.s_SlotFlags_005e93c0 + i * 0x9c + 0x12) = 0;
            Inp.s_InputStateBase_00604a20[slotIds[i] * 0x10 + 0xb] = 0;
        }
    }

    /// <summary>
    /// Ghidra: DAT_005e9080 — la valeur de l'objet sous le curseur, recopiee la ou le bandeau va
    /// la lire (« Au prix de: »).
    ///
    /// <para>C'est un champ de l'arene <c>0x005e9000</c>, pas une globale isolee :
    /// <c>0x5e9080 = 0x5e9060 + 0x20</c>, en plein dans les 0x120 octets utiles de
    /// <see cref="s_PanelSelection_005e9060"/>. Declare en champ C# separe, il etait ecrit dans
    /// une memoire que le moteur de texte ne lit pas — le bandeau affichait « Au prix de: 0 »
    /// quelle que soit la valeur calculee. Encore le defaut d'aliasing habituel du projet.</para>
    /// </summary>
    private static ref uint s_Unknown_005e9080 => ref *(uint*)Mem.Arena(0x005e9080);

    /// <summary>Ghidra: DAT_005de2f0 — <c>membre * 0x35 + 0x24</c>, pose a l'entree du choix de
    /// cible. TODO(REVERSE) : role exact non etabli.</summary>
    private static ref int s_Unknown_005de2f0 => ref *Mem.ItemMenuState(0x005de2f0);

    /// <summary>
    /// Ghidra: DAT_005658a7, DAT_0056501b et DAT_00565013 — encore trois « globales » qui sont en
    /// fait les champs <c>+0x8c7</c>, <c>+0x3b</c> et <c>+0x33</c> du **premier** descripteur du
    /// menu objets (0x564fe0 + 0x8c7 = 0x5658a7, + 0x3b = 0x56501b, + 0x33 = 0x565013). Exactement
    /// le même trio que sur l'écran de sauvegarde et sur l'écran d'organisation du groupe — la
    /// troisième fois que ces trois décalages se présentent ensemble.
    /// </summary>
    private static ref byte s_ItemMenuDialogGate_005658a7 => ref s_ItemMenuDescriptors_00564fe0[0x8c7];
    private static ref byte s_Unknown_0056501b => ref s_ItemMenuDescriptors_00564fe0[0x3b];
    private static ref byte s_Unknown_00565013 => ref s_ItemMenuDescriptors_00564fe0[0x33];

    /// <summary>
    /// Ghidra: le repliement « somme des valeurs absolues doublées » que trois sites recalculent à
    /// l'identique (cas 0 et 1 de <c>FUN_00527e30</c>, et <c>FUN_00529590</c>) sur un emplacement
    /// d'objet de 0x280 octets. Extrait plutôt que recopié trois fois.
    ///
    /// <para>Départ à la valeur brute de <c>+0x88</c>, puis huit shorts à <c>+0x20</c>, huit à
    /// <c>+0x30</c>, et huit octets signés à <c>+0x18</c>, chacun compté deux fois en valeur
    /// absolue pour les shorts et tel quel pour les octets.</para>
    /// </summary>
    private static uint ComputeItemValue(byte* slot)
    {
        uint value = *(ushort*)(slot + 0x88);

        for (int i = 0; i < 8; i++)
        {
            int v = *(short*)(slot + 0x20 + i * 2);
            if (v < 0) v = -v;
            value += (uint)(v * 2);
        }
        for (int i = 0; i < 8; i++)
        {
            int v = *(short*)(slot + 0x30 + i * 2);
            if (v < 0) v = -v;
            value += (uint)(v * 2);
        }
        for (int i = 0; i < 8; i++)
        {
            value += (uint)((sbyte)slot[0x18 + i] * 2);
        }

        return value;
    }

    /// <summary>
    /// Ghidra: la reconstruction de l'index d'emplacements occupés, en tête de
    /// <c>FUN_00527e30</c> (pour les quatre membres) et de nouveau dans son cas 1 (pour le membre
    /// courant seul). L'enregistrement porte à <c>+0x2c0</c> le nombre d'objets et à
    /// <c>+0x2c1</c> la liste de leurs numéros d'emplacement.
    /// </summary>
    private static void RebuildItemIndex(byte* record)
    {
        record[0x2c0] = 0;
        for (int slot = 0; slot < 10; slot++)
        {
            if (record[0x300 + slot * 0x280] == 0) continue;
            record[0x2c1 + record[0x2c0]] = (byte)slot;
            record[0x2c0]++;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00529590 @ 0x00529590 — le bandeau du groupe que les cas 0, -1 et -2 dessinent :
    /// par membre, son portrait, un cadre, son or, puis l'emplacement d'objet de plus grande valeur
    /// qu'il porte.
    ///
    /// <para>Les décalages viennent d'un pointeur que l'original place à
    /// <c>MainMem + 0xf02bd</c> et avance de 0x2000 : <c>[-0x16d]</c> est l'octet d'identité en
    /// <c>+0x150</c>, <c>[-0x51]</c> l'or en <c>+0x26c</c>, et <c>+99</c> mène à <c>+0x320</c>,
    /// c'est-à-dire l'emplacement d'objet 0 décalé de 0x20 — d'où le <c>[-0x20]</c> qui teste sa
    /// case « occupée ».</para>
    /// </summary>
    private static uint* DrawPartyItemSummary(uint* otBase, uint* primCursor)
    {
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return primCursor;

        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;
        NativeMemory.Clear(d, 32);

        for (int member = 0, y = 0x1c; member < s_PartySize_005e9020; member++, y += 0x35)
        {
            byte* record = mainMem + 0xf0000 + member * 0x2000;

            EmitViewportRect(otBase, &primCursor);

            *(byte**)d = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 8);
            (*(byte**)d)[5] = 0;
            *(ushort*)(d + 0x10) = record[0x150];
            *(short*)(d + 0xa) = 0x30;
            *(uint*)(d + 4) = member == s_Unknown_005de4e4 ? s_Unknown_005e9814 : 0x1000000u;
            *(short*)(d + 8) = (short)y;
            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, (uint)record[0x2bd] << 8);
            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);

            *(byte**)d = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x14);
            *(short*)(d + 8) = (short)(y - 4);
            *(ushort*)(d + 0x10) = 0x30;
            *(short*)(d + 0xa) = 100;
            primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
            primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
            EmitClipRect(otBase, &primCursor);

            // L'or, en +0x26c.
            *(short*)(d + 8) = (short)(y + 4);
            *(byte**)d = (byte*)s_NumeralTileRecord_00566c30;
            *(short*)(d + 0xa) = 100;
            primCursor = EmitCardGlyphTile(otBase, primCursor, draw, *(uint*)(record + 0x26c), 0x85, 0);

            // L'emplacement de plus grande valeur, s'il y en a un.
            int best = -1;
            int bestSlot = 0;
            for (int slot = 0; slot < 10; slot++)
            {
                byte* item = record + 0x300 + slot * 0x280;
                s_ItemValue_005de2d0 = ComputeItemValue(item);
                if (item[0] != 0 && best < (int)s_ItemValue_005de2d0)
                {
                    bestSlot = slot;
                    best = (int)s_ItemValue_005de2d0;
                }
            }

            if (best >= 0)
            {
                EmitViewportRect(otBase, &primCursor);
                byte* tile = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x10);
                *(byte**)d = tile;
                tile[0] = (byte)(bestSlot * 0x18);
                tile[1] = (byte)(member * 0x18);
                *(short*)(d + 8) = (short)(y + 8);
                *(ushort*)(d + 0x10) = 0;
                *(short*)(d + 0xa) = 0x74;
                primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0,
                                               (uint)((member & 0xff) << 8 | (bestSlot & 0xffff)));
                primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                EmitClipRect(otBase, &primCursor);

                *(short*)(d + 8) = (short)(y - 4);
                *(byte**)d = (byte*)s_NumeralTileRecord_00566c30;
                *(short*)(d + 0xa) = 0x90;
                primCursor = EmitCardGlyphTile(otBase, primCursor, draw, (uint)best, 0x86, 0);
            }
        }

        return primCursor;
    }

    /// <summary>
    /// Ghidra: FUN_00527e30 @ 0x00527e30 — le sous-écran d'utilisation d'objet, une machine à
    /// phases sur <c>DAT_005de4e8</c>. **Partiellement transcrit** : voir la note sur les cas 1 et 2.
    ///
    /// <para>Le prologue, commun à toutes les phases, reconstruit pour les quatre membres l'index
    /// des emplacements d'objet occupés — c'est ce que les phases suivantes parcourent.</para>
    ///
    /// <para>Phases transcrites : <b>0</b> (le choix du membre, l'entrée dans sa grille d'objets et
    /// les deux sorties), <b>3</b> (le transfert effectif d'un objet vers un autre membre),
    /// <b>-1</b> et <b>-2</b> (les deux boîtes de confirmation).</para>
    /// </summary>
    private static uint* ItemMenuUseScreen(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return primCursor;

        // Ghidra: la boucle 0xf0000..0xf8000 par pas de 0x2000 — les quatre membres.
        for (int member = 0; member < 4; member++)
            RebuildItemIndex(mainMem + 0xf0000 + member * 0x2000);

        switch (s_ItemMenuLoaded_005de4e8)
        {
            case 0:
            {
                // Le curseur de membre, sur les deux directions horizontales du mot de répétition.
                int before = s_Unknown_005de4e4;
                if ((pad[2] & 0x80) != 0) s_Unknown_005de4e4--;
                if ((pad[2] & 0x20) != 0) s_Unknown_005de4e4++;
                if (s_Unknown_005de4e4 < 0) s_Unknown_005de4e4 = s_PartySize_005e9020 - 1;
                if (s_Unknown_005de4e4 >= s_PartySize_005e9020) s_Unknown_005de4e4 = 0;
                if (before != s_Unknown_005de4e4) PlayUiSound3A();

                primCursor = DrawPartyItemSummary(otBase, primCursor);
                BindMenuLabels(LabelAt(0xe4), 0, s_ItemMenuDescriptors_00564fe0, 0);
                primCursor = DrawItemMenuTitle(primCursor);

                ushort pressed = *(ushort*)(pad + 2);

                // CROIX : sortir. Si aucun transfert n'a eu lieu on revient à l'écran racine ;
                // sinon on passe par la fermeture, qui réécrit les sauvegardes.
                if ((pressed & 0x4000) != 0)
                {
                    if (*(int*)ItemMenuTransferFlags_005de464 == 0)
                    {
                        s_Unknown_005de4d4 = 0;
                        s_ItemMenuSubState_00566ac8 = -1;
                        s_Unknown_00566acc = -1;
                        s_ItemMenuLoaded_005de4e8 = 0;
                        PlayUiSound39();
                        return primCursor;
                    }

                    s_ItemMenuSubState_00566ac8 = 4;
                    s_ItemMenuTarget_005de4f0 = -1;
                    s_Unknown_00566acc = -1;
                    s_ItemMenuCursor_005de4e0 = 0;
                    PlayUiSound39();
                    return primCursor;
                }

                // CARRÉ : entrer dans la grille d'objets du membre, s'il en porte au moins un.
                if ((pressed & 0x8000) != 0)
                {
                    byte* record = mainMem + 0xf0000 + s_Unknown_005de4e4 * 0x2000;
                    if (record[0x2c0] == 0) { PlayUiSound3B(); return primCursor; }

                    s_ItemMenuCursor_005de4e0 = 0;
                    s_ItemMenuLoaded_005de4e8++;
                    PlayUiSound38();

                    // Ghidra: le mot de tête de chaque emplacement de scène remis à zéro, pas de
                    // 0x4c octets, jusqu'à 0x5e92b0 — soit les quatre emplacements.
                    for (int i = 0; i < 4; i++)
                        *(ushort*)(Inp.s_SlotArea_005e9180 + i * 0x4c) = 0;

                    return primCursor;
                }
                break;
            }

            case 1:
            {
                // La grille d'objets du membre courant : on y choisit l'objet a transferer.
                byte* slotFlags = Inp.s_SlotFlags_005e93c0 + s_Unknown_005de4e4 * 0x9c;
                byte* record = mainMem + (s_Unknown_005de4e4 + 0x78) * 0x2000;
                slotFlags[8] = (byte)s_Unknown_005de4e4;

                // La liste des emplacements occupes, refaite a chaque image.
                record[0x2c0] = 0;
                for (int n = 0, off = 0; n < 10; n++, off += 0x280)
                {
                    if (record[0x300 + off] == 0) continue;
                    record[0x2c1 + record[0x2c0]] = (byte)n;
                    record[0x2c0]++;
                }

                // **La manette du membre**, pas celle du joueur courant : chaque membre du groupe
                // repond a la sienne. Ghidra rend l'adresse `&DAT_00604a20 + slot * 4` ; le
                // desassemblage donne `SHL EAX,0x4` puis `ADD EAX,0x604a20` (0x005281D6 et
                // 0x00528944) — le pas est de 0x10, pas de 4. Meme perte de decalage que sur les
                // trois sites deja corriges dans CurrentPadState.
                byte controller = Inp.s_PlayerSlots_005e9188[s_Unknown_005de4e4 * 0x4c];
                if (controller != 8) pad = Inp.s_InputStateBase_00604a20 + controller * 0x10;

                int before = s_ItemMenuCursor_005de4e0;
                if ((pad[6] & 0x80) != 0) s_ItemMenuCursor_005de4e0--;
                if ((pad[6] & 0x20) != 0) s_ItemMenuCursor_005de4e0++;

                // Haut et bas sautent d'une rangee de cinq, mais seulement si la grille en a deux.
                if ((pad[6] & 0x50) != 0)
                {
                    if (s_ItemMenuCursor_005de4e0 < 5)
                    {
                        if (record[0x2c0] > 5)
                        {
                            s_ItemMenuCursor_005de4e0 += 5;
                            if (record[0x2c0] <= s_ItemMenuCursor_005de4e0)
                                s_ItemMenuCursor_005de4e0 = record[0x2c0] - 1;
                        }
                    }
                    else s_ItemMenuCursor_005de4e0 -= 5;
                }

                if (s_ItemMenuCursor_005de4e0 < 0) s_ItemMenuCursor_005de4e0 = record[0x2c0] - 1;
                if (record[0x2c0] <= s_ItemMenuCursor_005de4e0) s_ItemMenuCursor_005de4e0 = 0;
                if (before != s_ItemMenuCursor_005de4e0) PlayUiSound3A();

                byte* item = record + 0x300 + record[0x2c1 + s_ItemMenuCursor_005de4e0] * 0x280;
                s_ItemValue_005de2d0 = ComputeItemValue(item);
                s_Unknown_005e9080 = s_ItemValue_005de2d0;

                // Le curseur de case : cinq colonnes de 0x1c (rendu par `(n*8 - n) * 4`), deux
                // rangees de 0x1c.
                int* draw = stackalloc int[8];
                byte* d = (byte*)draw;
                NativeMemory.Clear(d, 32);

                EmitViewportRect(otBase, &primCursor);
                *(uint**)d = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                *(short*)(d + 0x10) = 0x18;
                *(uint*)(d + 4) = 0x1000000;
                *(short*)(d + 8) = (short)(((s_ItemMenuCursor_005de4e0 % 5 << 3)
                                          - s_ItemMenuCursor_005de4e0 % 5) * 4 + 0x58);
                *(short*)(d + 0xa) = (short)(s_ItemMenuCursor_005de4e0 / 5 * 0x1c + 0x30);
                primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                EmitClipRect(otBase, &primCursor);

                primCursor = DrawPanelIconRow(otBase, primCursor, s_ItemPanelIcons_00566ad8);
                primCursor = DrawMemberItemGrid(otBase, primCursor, slotFlags, record);
                primCursor = DrawItemDetailCard(otBase, primCursor, record, s_ItemMenuCursor_005de4e0);

                BindMenuLabels(LabelAt(0xe8), 0, s_ItemMenuDescriptors_00564fe0, 0);

                // Le portrait et le nom du membre — le meme bandeau que la queue de
                // FUN_00524880, aux memes coordonnees.
                EmitViewportRect(otBase, &primCursor);
                NativeMemory.Clear(d, 32);
                *(uint*)(d + 4) = 0x1000000;
                *(uint**)d = *(uint**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 8);
                *(ushort*)(d + 0x10) = record[0x150];
                *(short*)(d + 8) = 0x20;
                *(short*)(d + 0xa) = 0x30;
                primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, (uint)record[0x2bd] << 8);
                primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                EmitClipRect(otBase, &primCursor);

                byte* edit = s_ItemMenuEditDescriptor_00566170;
                *(short*)(edit + 6) -= 4;
                edit[0x12] = 9;
                BindMenuLabels(Mem.s_Unknown_005606f8, 0, edit, 0);
                primCursor = Ui.RunTextEngine(primCursor, edit);

                byte* cmd = stackalloc byte[100];
                NativeMemory.Clear(cmd, 100);
                byte* memberName = record + 0x100;
                int nameLength = 0;
                while (memberName[nameLength] != 0) nameLength++;
                for (int i = 0; i <= nameLength; i++) cmd[1 + i] = memberName[i];
                cmd[0] = 0x20;
                BindMenuLabels(cmd, 0, edit, 0);
                primCursor = Ui.RunTextEngine(primCursor, edit);
                primCursor = DrawUiPanel(otBase, primCursor, edit);

                *(short*)(edit + 6) += 4;
                edit[0x12] = 0x1a;

                ushort pressedGrid = *(ushort*)(pad + 2);

                // CROIX : revenir au choix du membre.
                if ((pressedGrid & 0x4000) != 0)
                {
                    s_ItemMenuLoaded_005de4e8--;
                    PlayUiSound39();
                    return primCursor;
                }

                // CARRE : l'objet doit etre transferable — non porte par le membre, marque 0x8000
                // et pourvu d'au moins un des quatre bits 0xf00.
                if ((pressedGrid & 0x8000) != 0)
                {
                    int remaining = 5;
                    for (byte* worn = record + 0x2b0; remaining != 0; remaining--, worn++)
                        if (*worn != 0xff && item == record + 0x300 + *worn * 0x280) break;

                    if (remaining == 0
                        && (*(ushort*)(item + 0x12) & 0x8000) != 0
                        && (*(ushort*)(item + 0x12) & 0xf00) != 0)
                    {
                        s_ItemMenuLoaded_005de4e8++;
                        PlayUiSound38();

                        for (int i = 0; i < s_PartySize_005e9020; i++)
                            Mem.ItemMenuState(0x005de438)[i] = 0;

                        s_Unknown_005de2f0 = s_Unknown_005de4e4 * 0x35 + 0x24;
                        BindMenuLabels(LabelAt(0xec), 0, s_ItemMenuDescriptors_00564fe0, 0);
                        return primCursor;
                    }

                    // Ghidra: LAB_00528dbb — le bip de refus, puis retour immediat.
                    PlayUiSound3B();
                    return primCursor;
                }
                break;
            }

            case 2:
            {
                // L'enchère proprement dite : chaque autre membre du groupe mise sur l'objet avec
                // sa propre manette, par pas de 100, et confirme au CARRÉ. Quand tous ceux qui
                // tiennent une manette ont confirmé, la meilleure mise l'emporte.
                //
                // **L'adressage « détruit » de la pseudo-C n'en était pas.** Ghidra rend
                // `pbVar14[-0xffffffff00000008]` : c'est une constante 64 bits dont les 32 bits
                // bas valent 0xFFFFFFF8, soit l'indice -8. Le même champ est relu quelques lignes
                // plus haut en `*(short *)(pbVar14 + -8)`, ce qui le confirme sans ambiguïté :
                // c'est le drapeau « mise confirmée » en tête de l'enregistrement d'emplacement,
                // à `Inp.s_SlotArea_005e9180 + membre * 0x4c`. Aucune passe de désassemblage n'a été
                // nécessaire, seulement lire les deux formes côte à côte.
                primCursor = DrawItemMenuTitle(primCursor);   // Ghidra: FUN_00524190

                byte* seller = mainMem + (s_Unknown_005de4e4 + 0x78) * 0x2000;
                byte* lot = seller + 0x300 + seller[0x2c1 + s_ItemMenuCursor_005de4e0] * 0x280;
                s_ItemValue_005de2d0 = ComputeItemValue(lot);

                int* bids = Mem.ItemMenuState(0x005de438);

                // La meilleure mise. Une **égalité** ne départage pas : le gagnant redevient le
                // vendeur, ce que la résolution plus bas traite comme un refus.
                int best = -1;
                int winner = s_Unknown_005de4e4;
                for (int i = 0; i < s_PartySize_005e9020; i++)
                {
                    int bid = bids[i];
                    if (bid == 0 || i == s_Unknown_005de4e4) continue;
                    if (bid > best) { best = bid; winner = i; }
                    else if (bid == best) winner = s_Unknown_005de4e4;
                }
                s_Unknown_005de448 = best;
                s_ItemMenuTarget_005de4f0 = winner;

                int* draw = stackalloc int[8];
                byte* d = (byte*)draw;
                NativeMemory.Clear(d, 32);

                // L'objet mis aux enchères glisse vers le gagnant courant : la position converge
                // d'un quart de l'écart à chaque image.
                int changed = 0;
                s_Unknown_005de2f0 += (winner * 0x35 - s_Unknown_005de2f0 + 0x24) >> 2;

                EmitViewportRect(otBase, &primCursor);
                *(uint*)(d + 4) = 0x1000000;
                byte* lotIcon = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x10);
                lotIcon[0] = (byte)(seller[0x2c1 + s_ItemMenuCursor_005de4e0] * 0x18);
                lotIcon[1] = (byte)(s_Unknown_005de4e4 * 0x18);
                *(byte**)d = lotIcon;
                *(short*)(d + 0x10) = 0;
                *(short*)(d + 8) = (short)s_Unknown_005de2f0;
                *(short*)(d + 0xa) = 0x30;
                primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0,
                    (uint)((s_Unknown_005de4e4 << 8) | seller[0x2c1 + s_ItemMenuCursor_005de4e0]));
                primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                EmitClipRect(otBase, &primCursor);

                // Ghidra réutilise DAT_00566acc comme compteur le temps de la boucle.
                s_Unknown_00566acc = 0;
                int confirmed = 0;

                for (int member = 0, x = 0x1c; member < s_PartySize_005e9020; member++, x += 0x35)
                {
                    byte* slot = Inp.s_SlotArea_005e9180 + member * 0x4c;
                    byte* controllerSlot = slot + 8;          // Ghidra: &DAT_005e9188 + membre*0x4c
                    int* bid = bids + member;
                    uint* gold = (uint*)(mainMem + 0xf026c + member * 0x2000);

                    if (member == s_Unknown_005de4e4)
                    {
                        // Le vendeur ne mise pas sur son propre objet : sa manette est passée
                        // d'office au premier emplacement libre.
                        *(uint*)(d + 4) = 0x404040;
                        if (*controllerSlot != 8 && s_PartySize_005e9020 != 0)
                        {
                            int probe = member;
                            int tries = s_PartySize_005e9020;
                            changed = s_PartySize_005e9020;
                            while (tries != 0)
                            {
                                probe++;
                                if (probe >= s_PartySize_005e9020) probe = 0;
                                if (Inp.s_PlayerSlots_005e9188[probe * 0x4c] == 8
                                    && probe != s_Unknown_005de4e4)
                                {
                                    changed = 1;
                                    HandOverController(controllerSlot, probe);
                                    break;
                                }
                                tries--;
                            }
                            if (tries == 0) changed = 0;
                        }
                    }
                    else if (*controllerSlot == 8)
                    {
                        *(uint*)(d + 4) = 0x1000000;
                    }
                    else
                    {
                        // Ghidra rend l'adresse `&DAT_00604a20 + slot * 4` ; le désassemblage
                        // donne `SHL EAX,0x4` (0x00528944) — le pas est de 0x10. Même perte de
                        // décalage qu'au cas 1.
                        byte* memberPad = Inp.s_InputStateBase_00604a20 + *controllerSlot * 0x10;
                        s_Unknown_00566acc++;

                        if (*(short*)slot == 0)
                        {
                            *(uint*)(d + 4) = s_Unknown_005e9814;

                            // Monter la mise : jamais au-dessus de l'or disponible, jamais
                            // au-dessus de 99999, et le premier cran saute au prix de départ.
                            if ((memberPad[6] & 0x10) != 0 && s_ItemValue_005de2d0 <= *gold
                                && (uint)*bid < *gold)
                            {
                                changed = 1;
                                if ((uint)*bid < s_ItemValue_005de2d0)
                                {
                                    *bid = (int)s_ItemValue_005de2d0;
                                }
                                else
                                {
                                    uint raised = (uint)*bid + 100;
                                    if (raised < *gold)
                                    {
                                        *bid = (int)raised;
                                        if (raised > 99999) *bid = 99999;
                                    }
                                    else *bid = (int)*gold;
                                }
                            }

                            // Baisser : on retombe sur la centaine, et sous le prix de départ la
                            // mise est abandonnée.
                            if ((memberPad[6] & 0x40) != 0 && *bid != 0)
                            {
                                changed = 1;
                                int lowered = *bid % 100 == 0 ? *bid - 100 : *bid - *bid % 100;
                                *bid = lowered;
                                if (lowered < (int)s_ItemValue_005de2d0) *bid = 0;
                            }

                            if ((memberPad[6] & 0x50) != 0)
                                BindMenuLabels(LabelAt(0xec), 0, s_ItemMenuDescriptors_00564fe0, 0);

                            // GAUCHE et DROITE passent la manette au membre voisin qui n'en a pas.
                            // Le bit est effacé dans le tampon pour que le voisin ne le rejoue pas.
                            if ((*(ushort*)(memberPad + 6) & 0x80) != 0)
                            {
                                *(ushort*)(memberPad + 6) &= 0xff7f;
                                changed = MoveControllerBackward(controllerSlot, member);
                            }
                            if ((*(ushort*)(memberPad + 6) & 0x20) != 0)
                            {
                                *(ushort*)(memberPad + 6) &= 0xffdf;
                                changed = MoveControllerForward(controllerSlot, member);
                            }

                            // CARRÉ : confirmer sa mise.
                            if ((memberPad[3] & 0x80) != 0)
                            {
                                slot[0] = 1;
                                slot[1] = 0;
                                PlayUiSound38();
                            }
                        }
                        else
                        {
                            // Déjà confirmée : CROIX revient sur sa décision.
                            *(uint*)(d + 4) = 0x1000000;
                            if ((memberPad[3] & 0x40) != 0)
                            {
                                slot[0] = 0;
                                slot[1] = 0;
                                PlayUiSound39();
                            }
                            confirmed++;
                        }

                        // Le marteau, puis la mise et l'or qui resterait au membre.
                        EmitViewportRect(otBase, &primCursor);
                        *(uint**)d = *(uint**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x14);
                        *(short*)(d + 8) = (short)(x - 4);
                        *(short*)(d + 0x10) = 0x30;
                        *(short*)(d + 0xa) = 0x84;
                        primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                        primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                        EmitClipRect(otBase, &primCursor);

                        *(short*)(d + 8) = (short)(x + 4);
                        *(uint**)d = s_NumeralTileRecord_00566c30;
                        *(short*)(d + 0xa) = 0x7c;
                        primCursor = EmitCardGlyphTile(otBase, primCursor, draw, (uint)*bid, 0x85, 0);
                        *(short*)(d + 0xa) += 8;
                        primCursor = EmitCardGlyphTile(otBase, primCursor, draw,
                                                       *gold - (uint)*bid, 0x85, 0);
                    }

                    if (changed != 0) PlayUiSound3A();

                    // Le portrait du membre, dessiné pour tous, vendeur compris.
                    EmitViewportRect(otBase, &primCursor);
                    byte* portrait = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 8);
                    portrait[5] = 0;
                    *(byte**)d = portrait;
                    *(ushort*)(d + 0x10) = ((byte*)gold)[-0x11c];      // record[0x150]
                    *(short*)(d + 8) = (short)x;
                    *(short*)(d + 0xa) = 0x48;
                    primCursor = Prims.DrawSpriteVariant(otBase, primCursor, draw, 0,
                                                   (uint)((byte*)gold)[0x51] << 8);
                    primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, draw);
                    EmitClipRect(otBase, &primCursor);
                }

                // --- La résolution, une fois que tous les enchérisseurs ont confirmé ----------
                if (confirmed != s_Unknown_00566acc) break;

                for (int i = 0; i < s_PartySize_005e9020; i++)
                    *(ushort*)(Inp.s_SlotArea_005e9180 + i * 0x4c) = 0;

                if (s_Unknown_005de448 < 0)
                {
                    // Personne n'a misé : retour à la grille d'objets.
                    s_ItemMenuLoaded_005de4e8--;
                    PlayUiSound39();
                    ResetAuctionControllers();
                    return primCursor;
                }

                if (s_ItemMenuTarget_005de4f0 == s_Unknown_005de4e4)
                {
                    // Le vendeur « gagne » — c'est le cas d'égalité. Refus.
                    BindMenuLabels(LabelAt(0xf0), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    PlayUiSound3B();
                    return primCursor;
                }

                if (mainMem[s_ItemMenuTarget_005de4f0 * 0x2000 + 0xf02c0] >= 10)
                {
                    // Le gagnant a déjà dix objets : refus.
                    BindMenuLabels(LabelAt(0xf4), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    PlayUiSound3B();
                    return primCursor;
                }

                s_ItemMenuLoaded_005de4e8++;
                PlayUiSound38();

                // L'or change de mains, plafonné à 99999 chez le vendeur.
                int* winnerGold = (int*)(mainMem + s_ItemMenuTarget_005de4f0 * 0x2000 + 0xf026c);
                int* sellerGold = (int*)(mainMem + s_Unknown_005de4e4 * 0x2000 + 0xf026c);
                *winnerGold -= bids[s_ItemMenuTarget_005de4f0];
                *sellerGold += bids[s_ItemMenuTarget_005de4f0];
                if (*(uint*)sellerGold > 99999) *(uint*)sellerGold = 99999;

                bids[s_ItemMenuTarget_005de4f0] = 0;
                ResetAuctionControllers();
                return primCursor;
            }

            case 3:
            {
                // Le transfert : l'emplacement choisi chez le membre courant est cherché chez la
                // cible ; s'il y est déjà, le drapeau 0x8000 est propagé, sinon l'emplacement de la
                // cible est recopié par-dessus.
                byte* source = mainMem + 0xf0000 + s_Unknown_005de4e4 * 0x2000;
                byte* item = source + 0x300 + source[0x2c1 + s_ItemMenuCursor_005de4e0] * 0x280;
                byte* targetRecord = mainMem + 0xf0000 + s_ItemMenuTarget_005de4f0 * 0x2000;

                int matched = -1;
                for (int slot = 0; slot < 10; slot++)
                {
                    byte* candidate = targetRecord + 0x300 + slot * 0x280;
                    if (candidate[0] == 0) continue;

                    int same = 0;
                    while (same < 0x10 && candidate[same] == item[same]) same++;
                    if (same == 0x10 && (*(ushort*)(candidate + 0x12) & 0x8000) != 0)
                        *(ushort*)(item + 0x12) |= 0x8000;
                    matched = slot;
                }

                if (matched >= 0)
                {
                    Mem.CopyDwords((uint*)item, (uint*)(targetRecord + 0x300 + matched * 0x280), 0xa0);
                    UploadPartyIconTextures();               // Ghidra: FUN_00535500
                    PlaySoundEffect(0x29, 0x400, 0x1300, 0x1300);
                }

                // L'emplacement source est libéré, et les deux membres marqués « modifié ».
                byte* after = mainMem + 0xf0000 + s_Unknown_005de4e4 * 0x2000;
                after[0x300 + after[0x2c1 + s_ItemMenuCursor_005de4e0] * 0x280] = 0;

                int target = s_ItemMenuTarget_005de4f0;
                int current = s_Unknown_005de4e4;
                s_Unknown_005de4e4 = 0;
                ItemMenuTransferFlags_005de464[current] = 1;
                s_ItemMenuLoaded_005de4e8 = 0;
                ItemMenuTransferFlags_005de464[target] = 1;
                break;
            }

            case -2:
                primCursor = DrawPartyItemSummary(otBase, primCursor);
                if ((pad[3] & 0x40) != 0) return CancelItemMenuDialog(primCursor);

                if ((s_ItemMenuDialogGate_005658a7 & 4) == 0)
                {
                    if (Mem.s_Unknown_005e909f == 0)
                    {
                        s_Unknown_005de4d4 = 0;
                        s_ItemMenuSubState_00566ac8 = -1;
                        s_Unknown_00566acc = -1;
                    }
                    s_ItemMenuLoaded_005de4e8 = 0;
                    return primCursor;
                }
                break;

            case -1:
                primCursor = DrawPartyItemSummary(otBase, primCursor);
                if ((pad[3] & 0x40) != 0) return CancelItemMenuDialog(primCursor);

                if ((s_ItemMenuDialogGate_005658a7 & 2) == 0)
                {
                    BindMenuLabels(LabelAt(0), 0, s_ItemMenuDescriptors_00564fe0, 0);
                    s_ItemMenuLoaded_005de4e8--;
                    return primCursor;
                }
                break;
        }

        return primCursor;
    }

    /// <summary>Ghidra: LAB_00527f69 — la sortie de dialogue partagée par les cas -1 et -2 : les
    /// trois champs du descripteur remis à plat, le bip d'annulation, et retour à la phase 0.</summary>
    private static uint* CancelItemMenuDialog(uint* primCursor)
    {
        s_ItemMenuDialogGate_005658a7 = (byte)(s_ItemMenuDialogGate_005658a7 & 0xf9);
        s_Unknown_0056501b = 0;
        s_Unknown_00565013 = 0;
        PlayUiSound39();
        s_ItemMenuLoaded_005de4e8 = 0;
        return primCursor;
    }
}
