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
/// <b>Le menu de mise au point de la carte du monde</b> — <c>FUN_0051aa30</c> et les globales qu'il
/// est seul a toucher.
///
/// <para>Fichier ouvert le 2026-09-15. Un seul appelant, <see cref="RunWorldMapTick"/>, donc
/// <c>Screens/State03/</c> ; un fichier a part plutot que <c>WorldMapTick.cs</c> parce que c'est un
/// ecran complet et coherent en soi, le meme raisonnement que <c>WorldMapDepot.cs</c>.</para>
///
/// <para><b>Comment on l'atteint.</b> Deux conditions, prises dans la branche courte d'affichage :
/// le verrou <c>DAT_005633a8</c> doit etre tombe (le texte « Gurkha » trace une fois, voir
/// <c>s_DebugModeLatch_005633a8</c>), et le bit 1 de <c>DAT_005de02c</c> leve — c'est SELECT qui
/// le bascule, au cas 2 du repartiteur de <see cref="RunWorldMapTick"/>.</para>
///
/// <para><b>Les treize lignes</b>, dans l'ordre de la table <c>0x00563370</c> relevee dans le
/// binaire. La colonne « commande » lit la manette comme le reste du moteur : <c>+0</c> tenu,
/// <c>+2</c> vient d'etre presse, <c>+6</c> repetition automatique ; HAUT <c>0x10</c>,
/// DROITE <c>0x20</c>, BAS <c>0x40</c>, GAUCHE <c>0x80</c>, CROIX <c>0x4000</c>,
/// CARRE <c>0x8000</c>.</para>
/// <code>
///  0 QUEST    zone d'arrivee (DAT_005dbc14)        CARRE : s'y teleporter, sous-zone 0
///  1 MAP      sous-zone d'arrivee (DAT_005ddfd0)   CARRE : s'y teleporter dans la zone courante
///  2 PLAYER   taille du groupe, 1..4               le groupe est reconstruit sur le meneur
///  3 FLAG     banque 0x005e96e0, 64 octets         CARRE tenu : edition bit a bit ; CROIX : tout a 0
///  4 WORK     banque 0x005e9780, 64 octets         idem
///  5 TEMP     banque 0x005e9060, 64 octets         CARRE tenu : HAUT/BAS change l'octet entier
///  6 ITEM     banque 0x005e9840, 64 octets         comme FLAG
///  7 SYSTEM   0x005e9000, 32 octets                comme FLAG, et recopie dans chaque fiche +0x2e0
///  8 TEX      page de texture 4..15 / 20..31       l'affiche en 256x224 ; CARRE tenu : defiler
///  9 REBOOT   CARRE : etat de jeu 10
/// 10 MENU     CARRE : etat de jeu 1
/// 11 MONITOR  GAUCHE/DROITE : DAT_005de020 a 0/1   l'affichage de mise au point de la carte
/// 12 FONT     GAUCHE/DROITE : DAT_00566be0 a 0/1   aucun lecteur dans Blaze.c
/// </code>
///
/// <para><b>Les noms des banques sont ceux du developpeur</b>, et ils valent au-dela de ce menu :
/// quatre des huit banques de 64 octets que la sauvegarde recopie (<c>s_StateBank0..3</c> dans
/// <c>GameMemory</c>) s'appelaient FLAG, WORK, TEMP et ITEM.</para>
/// </summary>
public static unsafe partial class Blaze
{
    /// <summary>Ghidra: DAT_005de01c — la ligne selectionnee du menu. Un <c>dword</c> signe
    /// (<c>mov eax,[5DE01Ch]</c> puis <c>jge</c>). Queue non initialisee de <c>.data</c> : zero au
    /// chargement.</summary>
    private static int s_DebugMenuSelection_005de01c;

    /// <summary>Ghidra: DAT_005de018 — quand il est non nul, le menu s'affiche <b>sans rien
    /// faire</b> : la liste est tracee, la selection bouge, puis la fonction rend la main avant le
    /// repartiteur et avant le curseur. <b>Aucun ecrivain dans Blaze.c</b>, et l'adresse est dans la
    /// queue non initialisee de <c>.data</c> (verifie avec <c>at</c>) : il vaut donc toujours zero.
    /// TODO(REVERSE) : chercher un ecrivain par adresse calculee, ou dans une DLL.</summary>
    private static int s_Unknown_005de018;

    /// <summary>Ghidra: DAT_005ddf88, DAT_005dbc20, DAT_005ddfd4, DAT_005dbc1c, DAT_005dbb48 —
    /// l'octet selectionne dans chacune des cinq banques, FLAG, WORK, TEMP, ITEM et SYSTEM.
    /// Tous des <b>octets</b> au niveau machine (<c>inc byte [.]</c>, <c>mov al,[.]</c>), la ou
    /// Ghidra montre des <c>CONCAT31</c>. Queue non initialisee de <c>.data</c>.</summary>
    private static byte s_DebugFlagIndex_005ddf88, s_DebugWorkIndex_005dbc20, s_DebugTempIndex_005ddfd4;
    private static byte s_DebugItemIndex_005dbc1c, s_DebugSystemIndex_005dbb48;

    /// <summary>Ghidra: DAT_005dbc24 — le bit sous le curseur, 0 a droite, 7 a gauche. Un octet
    /// (<c>inc byte [5DBC24h]</c>), ramene a <c>&amp; 7</c> apres chaque edition.</summary>
    private static byte s_DebugBitCursor_005dbc24;

    /// <summary>Ghidra: DAT_00563368 — la page de texture que la ligne TEX affiche. <b>Initialise a
    /// 4 dans <c>.data</c></b> (releve : <c>04 00 00 00</c>), la premiere page que le pas autorise.
    /// Un dword (<c>and eax,1Fh / mov [563368h],eax</c>).</summary>
    private static int s_DebugTexPage_00563368 = 4;

    /// <summary>Ghidra: _DAT_00566be0 — la ligne FONT. <b>Initialise a 1 dans <c>.data</c></b>
    /// (releve : <c>01 00 00 00</c>). Blaze.c ne le lit nulle part ailleurs.
    /// TODO(REVERSE) : son lecteur, s'il existe, passe par une adresse calculee ou par une DLL.</summary>
    private static int s_Unknown_00566be0 = 1;

    /// <summary>Ghidra: 0x00564a30 — <c>"v"</c>, le curseur de bit trace au-dessus de la rangee.</summary>
    private static readonly byte* s_BitCursorGlyph_00564a30 = ToAnsiZ("v");

    /// <summary>Ghidra: 0x00564968..0x005649c0 — les treize libelles, releves dans le binaire.</summary>
    private static readonly byte*[] s_DebugMenuLabels =
    [
        ToAnsiZ("QUEST"), ToAnsiZ("MAP"), ToAnsiZ("PLAYER"), ToAnsiZ("FLAG"), ToAnsiZ("WORK"),
        ToAnsiZ("TEMP"), ToAnsiZ("ITEM"), ToAnsiZ("SYSTEM"), ToAnsiZ("TEX"), ToAnsiZ("REBOOT"),
        ToAnsiZ("MENU"), ToAnsiZ("MONITOR"), ToAnsiZ("FONT"),
    ];

    /// <summary>
    /// Ghidra: <c>PTR_s_QUEST_00563370</c> — la table des libelles, lue entree par entree jusqu'a
    /// une chaine <b>vide</b> (pas un pointeur nul).
    ///
    /// <para>Relevee dans <c>.data</c> : treize pointeurs vers les libelles, puis un quatorzieme,
    /// <c>0x00572b40</c>, qui sert de terminateur parce que son premier octet est nul. C'est
    /// <see cref="s_EmptyLabel_00572b40"/>, deja modelise ; il est lu ici <b>au moment de
    /// l'appel</b> et non recopie, puisque <c>FUN_00512900(&amp;DAT_00572b40)</c> y ecrit.</para>
    ///
    /// <para>Une methode et non un tableau statique : le terminateur vit dans un autre fichier, et
    /// l'ordre d'initialisation des champs statiques entre fichiers n'est pas specifie.</para>
    /// </summary>
    private static byte* DebugMenuLabel_00563370(int index)
    {
        if (index < s_DebugMenuLabels.Length) return s_DebugMenuLabels[index];
        if (index == s_DebugMenuLabels.Length) return s_EmptyLabel_00572b40;

        // Le binaire lirait ici 0x005633a8, le verrou de mise au point, comme un pointeur.
        throw new NotImplementedException(
            "PTR_s_QUEST_00563370 : la table des libelles du menu de mise au point a ete lue au-dela " +
            "de son terminateur, parce que DAT_00572b40 ne commence plus par un octet nul. " +
            "FUN_00512900 y ecrit ; ce cas n'est pas modelise.");
    }

    /// <summary>
    /// Ghidra: FUN_0051aa30 @ 0x0051aa30 — <b>une image du menu de mise au point</b> : la liste, le
    /// traitement de la ligne selectionnee, puis la colonne des valeurs. Rend le curseur de
    /// primitives. Transcrit contre le desassemblage complet
    /// (<c>Legacy/Raw/Blaze/FUN_0051AA30.asm</c>) : la pseudo-C tient, mais elle rend en
    /// <c>CONCAT31</c> des globales qui sont toutes des octets.
    ///
    /// <para><paramref name="block"/> est le bloc de sprite de sept champs decrit sur
    /// <see cref="Primitives2d.DrawSpritePrimitive"/>. Dans l'original, c'est un bloc de la pile de
    /// <c>FUN_00516fc0</c> (<c>[esp+0x34]</c> au site d'appel), le meme que le bloc d'affichage
    /// principal emploie. Cette fonction ecrit chaque champ avant de le faire lire, donc rien ne
    /// passe d'une image a l'autre par lui.</para>
    ///
    /// <para><b>Deux ecarts de la pseudo-C, verifies et sans effet</b> : Ghidra ecrit
    /// <c>DAT_005de52c = bVar11</c> sur l'etiquette commune aux cas 0 et 1, alors que le binaire ne
    /// l'ecrit qu'au cas 1 (le cas 0 l'a deja mis a zero) ; et il relit des instantanes
    /// (<c>uVar7</c>, <c>uVar14</c>…) d'index qui n'ont pas change entre-temps.</para>
    /// </summary>
    private static uint* RunDebugMenuFrame_0051AA30(uint* otBase, uint* prim, int* block, ushort* pad)
    {
        byte* b = (byte*)block;

        // pad[0] : tenu. pad[1] : vient d'etre presse. pad[3] : repetition automatique (+6).
        *(short*)(b + 8) = 0x18;
        *(short*)(b + 0xa) = 0x18;
        *(uint**)b = s_GlyphAtlas_00566c28;
        *(uint*)(b + 4) = 0x1000000;

        // La liste : un libelle tous les onze pixels, et le compte sert de borne a la selection.
        int itemCount = 0;
        for (byte* label = DebugMenuLabel_00563370(0); *label != 0; label = DebugMenuLabel_00563370(itemCount))
        {
            prim = Prims.DrawTextRun(otBase, prim, block, label, 0, 0, -1);
            *(short*)(b + 0xa) += 0xb;
            itemCount++;
        }

        // CARRE tenu fige la selection : c'est lui qui fait passer les lignes en mode edition.
        if ((pad[0] & 0x8000) == 0)
        {
            if ((pad[3] & 0x10) != 0) s_DebugMenuSelection_005de01c--;
            if ((pad[3] & 0x40) != 0) s_DebugMenuSelection_005de01c++;
            if (s_DebugMenuSelection_005de01c < 0) s_DebugMenuSelection_005de01c = itemCount - 1;
            if (itemCount <= s_DebugMenuSelection_005de01c) s_DebugMenuSelection_005de01c = 0;
        }

        if (s_Unknown_005de018 != 0) return prim;

        switch (s_DebugMenuSelection_005de01c)
        {
            case 0:     // QUEST
                if ((pad[3] & 0x80) != 0) s_DebugQuest_005dbc14--;
                if ((pad[3] & 0x20) != 0) s_DebugQuest_005dbc14++;
                if (s_DebugQuest_005dbc14 == 0xff) s_DebugQuest_005dbc14 = (byte)(s_Unknown_005e9760 - 1);
                if (s_Unknown_005e9760 <= s_DebugQuest_005dbc14) s_DebugQuest_005dbc14 = 0;

                // Changer de zone remet la sous-zone proposee a zero.
                if (s_DebugQuest_005dbc14 != (byte)Res.s_Unknown_005de528) s_DebugMap_005ddfd0 = 0;

                if ((pad[1] & 0x8000) != 0)
                {
                    // `mov [5DE528h],al` : seul l'octet bas de la zone est ecrit.
                    Res.s_Unknown_005de528 = (int)((uint)Res.s_Unknown_005de528 & 0xffffff00) | s_DebugQuest_005dbc14;
                    s_Unknown_00566c84 = 0xff;
                    s_Unknown_005de52c = 0;
                    CommitDebugWarp();
                }
                break;

            case 1:     // MAP
                if ((pad[3] & 0x80) != 0) s_DebugMap_005ddfd0--;
                if ((pad[3] & 0x20) != 0) s_DebugMap_005ddfd0++;

                // Bouger la sous-zone ramene la zone proposee sur la zone courante : MAP se
                // teleporte toujours dans la zone ou l'on est.
                if (((byte)pad[3] & 0xa0) != 0) s_DebugQuest_005dbc14 = (byte)Res.s_Unknown_005de528;

                if (s_DebugMap_005ddfd0 == 0xff) s_DebugMap_005ddfd0 = (byte)(s_Unknown_005f7b44 - 1);
                if (s_Unknown_005f7b44 <= s_DebugMap_005ddfd0) s_DebugMap_005ddfd0 = 0;

                if ((pad[1] & 0x8000) != 0)
                {
                    s_Unknown_005de52c = s_DebugMap_005ddfd0;
                    CommitDebugWarp();
                }
                break;

            case 2:     // PLAYER
                if ((pad[3] & 0x80) != 0) s_DebugPlayer_005dbc18--;
                if ((pad[3] & 0x20) != 0) s_DebugPlayer_005dbc18++;
                if (s_DebugPlayer_005dbc18 == 0) s_DebugPlayer_005dbc18 = 4;
                if (s_DebugPlayer_005dbc18 > 4) s_DebugPlayer_005dbc18 = 1;

                if (s_PartySize_005e9020 != s_DebugPlayer_005dbc18)
                {
                    RebuildDebugParty(s_DebugPlayer_005dbc18);
                }
                break;

            case 3:     // FLAG
                prim = EditDebugBitBank(otBase, prim, block, pad, ref s_DebugFlagIndex_005ddf88, Mem.s_StateBank0_005e96e0);
                break;

            case 4:     // WORK
                prim = EditDebugBitBank(otBase, prim, block, pad, ref s_DebugWorkIndex_005dbc20, Mem.s_StateBank1_005e9780);
                break;

            case 5:     // TEMP — l'octet entier, pas ses bits
                if ((pad[0] & 0x8000) == 0)
                {
                    StepDebugBankIndex(pad, ref s_DebugTempIndex_005ddfd4, 0x3f);
                }
                else
                {
                    if ((pad[3] & 0x10) != 0) Mem.s_StateBank2_005e9060[s_DebugTempIndex_005ddfd4]++;
                    if ((pad[3] & 0x40) != 0) Mem.s_StateBank2_005e9060[s_DebugTempIndex_005ddfd4]--;
                }

                if ((pad[1] & 0x4000) != 0) ClearDwords(Mem.s_StateBank2_005e9060, 0x10);
                break;

            case 6:     // ITEM
                prim = EditDebugBitBank(otBase, prim, block, pad, ref s_DebugItemIndex_005dbc1c, Mem.s_StateBank3_005e9840);
                break;

            case 7:     // SYSTEM
                prim = EditDebugSystemBank(otBase, prim, block, pad);
                break;

            case 8:     // TEX
            {
                byte* texel = Mem.Entity(0x005eca88);   // les octets U et V du descripteur monte en fin de fonction

                if ((pad[0] & 0x8000) != 0)
                {
                    // CARRE tenu : les directions TENUES defilent la page, par pas de quatre texels.
                    if ((pad[0] & 0x80) != 0) texel[0] += 4;
                    if ((pad[0] & 0x20) != 0) texel[0] -= 4;
                    if ((pad[0] & 0x10) != 0) texel[1] += 4;
                    if ((pad[0] & 0x40) != 0) texel[1] -= 4;
                }
                else
                {
                    if ((pad[3] & 0x80) != 0)
                    {
                        s_DebugTexPage_00563368--;
                        texel[0] = 0;
                        texel[1] = 0;
                    }

                    if ((pad[3] & 0x20) != 0)
                    {
                        s_DebugTexPage_00563368++;
                        texel[0] = 0;
                        texel[1] = 0;
                    }

                    // Quatre pages sont sautees de chaque cote de 0x10 : 0..3 et 16..19 ne sont
                    // jamais proposees. Le pas boucle donc sur 4..15 puis 20..31.
                    s_DebugTexPage_00563368 &= 0x1f;
                    if (s_DebugTexPage_00563368 == 0) s_DebugTexPage_00563368 = 4;
                    if (s_DebugTexPage_00563368 == 3) s_DebugTexPage_00563368 = 0x1f;
                    if (s_DebugTexPage_00563368 == 0x10) s_DebugTexPage_00563368 = 0x14;
                    if (s_DebugTexPage_00563368 == 0x13) s_DebugTexPage_00563368 = 0xf;
                }
                break;
            }

            case 9:     // REBOOT
                if ((pad[1] & 0x8000) != 0)
                {
                    s_WorldMapStep_005645f8 = -2;
                    Mem.s_WorldFlags_005de02c |= 2;
                    s_GameStateId_005de510 = 10;
                }
                break;

            case 10:    // MENU
                if ((pad[1] & 0x8000) != 0)
                {
                    s_WorldMapStep_005645f8 = -2;
                    Mem.s_WorldFlags_005de02c |= 2;
                    s_GameStateId_005de510 = 1;
                }
                break;

            case 11:    // MONITOR
                if ((pad[1] & 0x80) != 0) s_DebugOverlayEnabled_005de020 = 0;
                if ((pad[1] & 0x20) != 0) s_DebugOverlayEnabled_005de020 = 1;
                break;

            case 12:    // FONT
                if ((pad[1] & 0x80) != 0) s_Unknown_00566be0 = 0;
                if ((pad[1] & 0x20) != 0) s_Unknown_00566be0 = 1;
                break;
        }

        // Le curseur de ligne, puis la colonne des valeurs en x = 0x50, avec l'atlas de chiffres.
        *(short*)(b + 8) = 0xc;
        *(short*)(b + 0xa) = (short)(s_DebugMenuSelection_005de01c * 0xb + 0x18);
        prim = Prims.DrawTextRun(otBase, prim, block, s_CursorGlyph_00564a2c, 0, 0, -1);

        *(short*)(b + 8) = 0x50;
        *(short*)(b + 0xa) = 0x18;
        *(uint**)b = s_NumeralTileRecord_00566c30;

        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugQuest_005dbc14, 2, 0);
        *(short*)(b + 0xa) += 0xb;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugMap_005ddfd0, 2, 0);
        *(short*)(b + 0xa) += 0xb;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugPlayer_005dbc18, 2, 0);
        *(short*)(b + 0xa) += 0xb;

        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugFlagIndex_005ddf88, 2, 0);
        prim = DrawDebugBitRow(otBase, prim, block, Mem.s_StateBank0_005e96e0[s_DebugFlagIndex_005ddf88]);

        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugWorkIndex_005dbc20, 2, 0);
        prim = DrawDebugBitRow(otBase, prim, block, Mem.s_StateBank1_005e9780[s_DebugWorkIndex_005dbc20]);

        // TEMP : l'octet en decimal sur trois chiffres, a la place des huit bits.
        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugTempIndex_005ddfd4, 2, 0);
        *(short*)(b + 8) += 0x20;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, Mem.s_StateBank2_005e9060[s_DebugTempIndex_005ddfd4], 3, 0);
        *(short*)(b + 8) -= 0x20;
        *(short*)(b + 0xa) += 0xb;

        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugItemIndex_005dbc1c, 2, 0);
        prim = DrawDebugBitRow(otBase, prim, block, Mem.s_StateBank3_005e9840[s_DebugItemIndex_005dbc1c]);

        prim = Prims.DrawDecimalNumber(otBase, prim, block, s_DebugSystemIndex_005dbb48, 2, 0);
        prim = DrawDebugBitRow(otBase, prim, block, s_PartyAbilityMask_005e9000[s_DebugSystemIndex_005dbb48]);

        prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)s_DebugTexPage_00563368, 2, 0);

        if (s_DebugMenuSelection_005de01c == 8)
        {
            // La page elle-meme, en 256x224 dans le coin de l'ecran. Le descripteur est monte a la
            // main dans l'arene d'entites — le meme emplacement que le curseur du coffre — et il
            // designe l'entree 0x32 de la table de pages, qu'on reecrit pour viser la page choisie.
            *(short*)(b + 8) = 0;
            *(short*)(b + 0xa) = 0;
            *(short*)(b + 0x10) = 0;
            *(byte**)b = Mem.Entity(0x005eca88);

            // `shl ecx,6 / sar ecx,6 / and ecx,0Fh` et `shl eax,4 / sar eax,4 / and eax,10h` : les
            // extensions de signe tombent sous les masques, il reste les cinq bits bas de la page.
            // `or ch,2` y ajoute 0x200.
            int page = s_DebugTexPage_00563368;
            ushort pageEntry = (ushort)((((page << 6) >> 6) & 0xf) | (((page << 4) >> 4) & 0x10) | 0x200);

            byte* descriptor = Mem.Entity(0x005eca88);
            descriptor[2] = 0;          // largeur 0 : DrawSpriteVariant la lit comme 0x100
            descriptor[3] = 0xe0;
            descriptor[4] = 0;
            descriptor[5] = 3;
            descriptor[6] = 0x32;       // l'entree de la table de pages
            descriptor[7] = 0;

            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xc8) = pageEntry;
            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xca) = 0x7000;

            prim = Prims.DrawSpriteVariant(otBase, prim, block, 0, 0);     // Ghidra: FUN_00521df0
            prim = Prims.DrawFlatPrimitive(otBase, prim, block);           // Ghidra: FUN_0051f9c0
        }

        return prim;
    }

    /// <summary>
    /// Ghidra: l'etiquette <c>LAB_0051abd7</c> de <c>FUN_0051aa30</c>, commune aux lignes QUEST et
    /// MAP — <b>demander le rechargement</b> de la zone et de la sous-zone deja posees.
    ///
    /// <para>Le bit 2 de <c>DAT_005de02c</c> fait sortir <see cref="RunWorldMapTick"/> de sa boucle
    /// d'images, et le pas de boucle a 2 fait repasser la prochaine entree par la branche complete.
    /// Le meneur courant est retenu dans <c>DAT_005de53c</c>. Les deux <c>0xff</c> invalident les
    /// memoires de zone et de sous-zone, pour que le chargement ne soit pas saute.</para>
    /// </summary>
    private static void CommitDebugWarp()
    {
        s_Unknown_00566c88 = 0xff;
        s_Unknown_005de53c = (byte)Mem.s_Unknown_005de538;   // `mov al,[5DE538h]` : l'octet bas
        Mem.s_WorldFlags_005de02c |= 2;
        s_Unknown_005de530 = 0;
        s_WorldMapStep_005645f8 = 2;
    }

    /// <summary>
    /// Ghidra: le corps du cas 2 (PLAYER) de <c>FUN_0051aa30</c>, <b>la reconstruction du groupe</b>
    /// a une nouvelle taille.
    ///
    /// <para>Les emplacements 1 a 3 — toujours trois, quelle que soit la taille — recoivent une copie
    /// de l'emplacement 0 : la fiche de 0x9c octets en <c>0x005e93c0</c> et la ligne de manette de
    /// 0x4c octets en <c>0x005e9180</c>. Chaque copie garde son octet <c>+4</c>, recoit son propre
    /// numero en <c>+8</c> et <c>+0xb</c>, et sa ligne de manette est remise a « aucune »
    /// (<c>8</c>). Toutes portent ensuite le bit <c>0x10000</c> ; celles au-dela de la taille
    /// perdent en plus le bit <c>0x80000000</c>.</para>
    ///
    /// <para>Le meneur repasse a l'emplacement 0, et l'emplacement 0 prend la manette que le meneur
    /// avait — releve <b>avant</b> les copies, qui l'ecraseraient.</para>
    /// </summary>
    private static void RebuildDebugParty(byte partySize)
    {
        s_PartySize_005e9020 = partySize;

        byte* leaderPadRow = Inp.s_SlotArea_005e9180 + (byte)Mem.s_Unknown_005de538 * 0x4c;
        byte leaderPad = leaderPadRow[8];
        byte leaderPadIndex = leaderPadRow[9];

        for (int i = 1; i < 4; i++)
        {
            byte* member = Inp.s_SlotFlags_005e93c0 + i * 0x9c;
            byte* padRow = Inp.s_SlotArea_005e9180 + i * 0x4c;
            byte kept = member[4];

            Mem.CopyDwords((uint*)Inp.s_SlotFlags_005e93c0, (uint*)member, 0x27);   // Ghidra: FUN_005352e0
            Mem.CopyDwords((uint*)Inp.s_SlotArea_005e9180, (uint*)padRow, 0x13);    // Ghidra: FUN_005352e0

            member[9] = 0;
            member[4] = kept;
            member[0xb] = (byte)i;
            member[8] = (byte)i;
            padRow[8] = 8;
            padRow[9] = (byte)i;

            uint flags = *(uint*)member | 0x10000;
            *(uint*)member = flags;
            if (i >= s_PartySize_005e9020) *(uint*)member = flags & 0x7fffffff;
        }

        Inp.s_PlayerSlots_005e9188[0] = leaderPad;
        Inp.s_PlayerSlots_005e9188[1] = leaderPadIndex;
        Mem.s_Unknown_005de538 = (int)((uint)Mem.s_Unknown_005de538 & 0xffffff00);   // `mov byte [5DE538h],0`
        s_Unknown_005de540 = 0;
    }

    /// <summary>Ghidra: le pas d'index commun aux lignes FLAG a SYSTEM, quand CARRE n'est pas tenu :
    /// GAUCHE recule, DROITE avance, et l'index boucle sur <c>0..last</c>. Le binaire compare a
    /// <c>0xff</c> puis a <c>last + 1</c> en non signe.</summary>
    private static void StepDebugBankIndex(ushort* pad, ref byte index, byte last)
    {
        if ((pad[3] & 0x80) != 0) index--;
        if ((pad[3] & 0x20) != 0) index++;
        if (index == 0xff) index = last;
        if (index > last) index = 0;
    }

    /// <summary>Ghidra: le <c>rep stosd</c> de remise a zero d'une banque.</summary>
    private static void ClearDwords(byte* bank, int dwords)
    {
        for (int i = 0; i < dwords; i++) ((uint*)bank)[i] = 0;
    }

    /// <summary>
    /// Ghidra: le bit cible par <see cref="s_DebugBitCursor_005dbc24"/>, <c>'\x01' &lt;&lt; (c &amp;
    /// 0x1f)</c>. Le curseur n'est ramene a <c>&amp; 7</c> qu'<b>apres</b> l'edition : s'il vaut 8 ou
    /// 0xff a ce moment, <c>shl r8,cl</c> rend zero et l'appui ne touche aucun bit. Le transtypage en
    /// octet le reproduit.
    /// </summary>
    private static byte DebugBitMask() => (byte)(1 << (s_DebugBitCursor_005dbc24 & 0x1f));

    /// <summary>
    /// Ghidra: le corps des cas 3, 4 et 6 (FLAG, WORK, ITEM) de <c>FUN_0051aa30</c> — trois blocs
    /// identiques au registre de banque pres dans le binaire, reunis ici.
    ///
    /// <para>CARRE non tenu : GAUCHE/DROITE choisit l'octet. CARRE tenu : GAUCHE/DROITE deplace le
    /// curseur de bit, HAUT leve le bit et BAS le baisse, et le <c>"v"</c> est trace au-dessus de la
    /// rangee. Dans les deux cas, CROIX pressee remet les 64 octets a zero.</para>
    /// </summary>
    private static uint* EditDebugBitBank(uint* otBase, uint* prim, int* block, ushort* pad, ref byte index, byte* bank)
    {
        if ((pad[0] & 0x8000) == 0)
        {
            StepDebugBankIndex(pad, ref index, 0x3f);
        }
        else
        {
            if ((pad[3] & 0x80) != 0) s_DebugBitCursor_005dbc24++;
            if ((pad[3] & 0x20) != 0) s_DebugBitCursor_005dbc24--;

            if ((pad[1] & 0x10) != 0) bank[index] |= DebugBitMask();
            if ((pad[1] & 0x40) != 0) bank[index] &= (byte)~DebugBitMask();

            prim = DrawDebugBitCursor(otBase, prim, block);
        }

        if ((pad[1] & 0x4000) != 0) ClearDwords(bank, 0x10);

        return prim;
    }

    /// <summary>
    /// Ghidra: le corps du cas 7 (SYSTEM) de <c>FUN_0051aa30</c>. Meme forme que
    /// <see cref="EditDebugBitBank"/>, trois differences : l'index boucle sur 32 octets et non 64, la
    /// remise a zero couvre 8 dwords, et <b>chaque ecriture est recopiee dans la fiche de chaque
    /// membre du groupe</b>, a <c>MainMem + 0xf0000 + i * 0x2000 + 0x2e0 + index</c>. La banque
    /// <c>0x005e9000</c> est donc le miroir de travail d'un champ de fiche de personnage.
    ///
    /// <para>Le binaire relit <c>MainMem_exref</c> et <c>DAT_005dbb48</c> a chaque tour de la boucle
    /// des membres ; ni l'un ni l'autre ne change pendant la boucle.</para>
    /// </summary>
    private static uint* EditDebugSystemBank(uint* otBase, uint* prim, int* block, ushort* pad)
    {
        byte* mask = s_PartyAbilityMask_005e9000;

        if ((pad[0] & 0x8000) == 0)
        {
            StepDebugBankIndex(pad, ref s_DebugSystemIndex_005dbb48, 0x1f);
        }
        else
        {
            if ((pad[3] & 0x80) != 0) s_DebugBitCursor_005dbc24++;
            if ((pad[3] & 0x20) != 0) s_DebugBitCursor_005dbc24--;

            if ((pad[1] & 0x10) != 0)
            {
                mask[s_DebugSystemIndex_005dbb48] |= DebugBitMask();
                for (int i = 0; i < s_PartySize_005e9020; i++)
                {
                    GetMainMemPtr()[s_DebugSystemIndex_005dbb48 + i * 0x2000 + 0xf02e0] |= DebugBitMask();
                }
            }

            if ((pad[1] & 0x40) != 0)
            {
                mask[s_DebugSystemIndex_005dbb48] &= (byte)~DebugBitMask();
                for (int i = 0; i < s_PartySize_005e9020; i++)
                {
                    GetMainMemPtr()[s_DebugSystemIndex_005dbb48 + i * 0x2000 + 0xf02e0] &= (byte)~DebugBitMask();
                }
            }

            prim = DrawDebugBitCursor(otBase, prim, block);
        }

        if ((pad[1] & 0x4000) != 0)
        {
            ClearDwords(mask, 8);
            for (int i = 0; i < s_PartySize_005e9020; i++)
            {
                for (int j = 0; j < 0x20; j++)
                {
                    mask[j] = 0;
                    GetMainMemPtr()[(i << 0xd) + j + 0xf02e0] = 0;
                }
            }
        }

        return prim;
    }

    /// <summary>Ghidra: le <c>"v"</c> trace au-dessus du bit edite, apres avoir ramene le curseur a
    /// <c>&amp; 7</c> : x = <c>(0x15 - bit) * 8</c>, soit 0xa8 pour le bit 0 — la derniere colonne
    /// de <see cref="DrawDebugBitRow"/> —, et y huit pixels au-dessus du libelle de la ligne selectionnee. Le
    /// bloc porte encore l'atlas de glyphes : la colonne des valeurs ne l'a pas encore remplace.</summary>
    private static uint* DrawDebugBitCursor(uint* otBase, uint* prim, int* block)
    {
        byte* b = (byte*)block;

        s_DebugBitCursor_005dbc24 &= 7;
        *(short*)(b + 8) = (short)((0x15 - s_DebugBitCursor_005dbc24) * 8);
        *(short*)(b + 0xa) = (short)(s_DebugMenuSelection_005de01c * 0xb + 0x10);

        return Prims.DrawTextRun(otBase, prim, block, s_BitCursorGlyph_00564a30, 0, 0, -1);
    }

    /// <summary>Ghidra: les quatre boucles <c>iVar17 = 8; do { … } while (iVar17 != 0)</c> de la
    /// colonne des valeurs — huit chiffres 0/1, bit 7 a gauche, huit pixels chacun, a partir de
    /// x + 0x20. La fonction laisse le bloc sur la ligne suivante, a l'abscisse de depart.</summary>
    private static uint* DrawDebugBitRow(uint* otBase, uint* prim, int* block, byte value)
    {
        byte* b = (byte*)block;

        *(short*)(b + 8) += 0x20;
        for (int bit = 7; bit >= 0; bit--)
        {
            prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)((value >> bit) & 1), 1, 0);
            *(short*)(b + 8) += 8;
        }

        *(short*)(b + 0xa) += 0xb;
        *(short*)(b + 8) -= 0x60;
        return prim;
    }

    // =============================================================================================
    // L'AFFICHAGE MONITOR — ce que la ligne MONITOR du menu allume (DAT_005de020 = 1).
    //
    // Deux blocs de FUN_00516fc0, l'un en tete de chaque image, l'autre dans le bloc d'affichage
    // principal, qui travaillent a deux : le premier recopie trois banques dans leurs MIROIRS, le
    // second compare les banques aux miroirs et liste dans une boite les bits qui ont bouge entre
    // les deux. Entre les deux il y a le repartiteur et les scripts : c'est donc un journal des
    // drapeaux que le jeu leve ou baisse, image par image.
    // =============================================================================================

    /// <summary>Ghidra: 0x00564a28 / 0x00564a24 / 0x00564a20 / 0x00564a1c / 0x00564a18 — les
    /// quatre libelles de l'en-tete et le signe moins, releves dans le binaire.</summary>
    private static readonly byte* s_AngLabel_00564a28 = ToAnsiZ("ANG");
    private static readonly byte* s_LocLabel_00564a24 = ToAnsiZ("LOC");
    private static readonly byte* s_DptLabel_00564a20 = ToAnsiZ("DPT");
    private static readonly byte* s_ChgLabel_00564a1c = ToAnsiZ("CHG");
    private static readonly byte* s_MinusGlyph_00564a18 = ToAnsiZ("-");

    /// <summary>Ghidra: <c>PTR_s_EvFlag___00563350</c> — le libelle de chaque genre de changement,
    /// par <c>DAT_005dbc28</c>. Releves : <c>0x005649e8</c>, <c>0x005649dc</c>,
    /// <c>0x005649d0</c>.</summary>
    private static readonly byte*[] s_ChangeKindLabels_00563350 =
        [ToAnsiZ("EvFlag :"), ToAnsiZ("EvItem :"), ToAnsiZ("SysFlag:")];

    /// <summary>Ghidra: <c>PTR_DAT_00563360</c> — l'etat nouveau du bit, par
    /// <c>DAT_005dbb90</c> : 0 = <c>"Off"</c> (<c>0x005649cc</c>), 1 = <c>"On"</c>
    /// (<c>0x005649c8</c>).</summary>
    private static readonly byte*[] s_ChangeStateLabels_00563360 = [ToAnsiZ("Off"), ToAnsiZ("On")];

    /// <summary>Ghidra: 0x00564a10 — le flux de texte que la boite recoit : <c>0c 0f 00 00</c>,
    /// deux codes de controle et rien a ecrire. La liste est dessinee a cote par des primitives ; la
    /// boite ne sert que de cadre, et de verrou tant que le joueur ne l'a pas fermee.</summary>
    private static readonly byte* s_MonitorStream_00564a10 = AllocBytes([0x0c, 0x0f, 0x00, 0x00]);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005dbb08</c>, <c>&amp;DAT_005ddfd8</c>, <c>&amp;DAT_005ddf90</c> — les miroirs de
    /// FLAG, ITEM et SYSTEM. <b>Leurs etendues sont deduites, pas choisies</b> : la recopie en
    /// <c>rep movsd</c> prend 0x10, 0x10 et 8 dwords, et chacune finit pile sur un symbole deja
    /// connu — <c>0x005dbb48</c> (<see cref="s_DebugSystemIndex_005dbb48"/>) et <c>0x005de018</c>
    /// (<see cref="s_Unknown_005de018"/>) pour les deux premieres. Queue non initialisee de
    /// <c>.data</c> : zero au chargement.
    /// </summary>
    private static readonly byte* s_FlagMirror_005dbb08 = (byte*)NativeMemory.AllocZeroed(0x40);
    private static readonly byte* s_ItemMirror_005ddfd8 = (byte*)NativeMemory.AllocZeroed(0x40);
    private static readonly byte* s_SystemMirror_005ddf90 = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005dbb50</c> (le bit), <c>&amp;DAT_005dbb90</c> (son etat nouveau),
    /// <c>&amp;DAT_005dbbd0</c> (l'octet de banque), <c>&amp;DAT_005dbc28</c> (la banque : 0 FLAG,
    /// 1 ITEM, 2 SYSTEM) — la liste des changements, quatre colonnes paralleles de 0x40 octets, et
    /// <c>DAT_005dbc10</c> leur compte. Les quatre se suivent sans trou, et la derniere finit
    /// exactement sur <c>s_WorldPanels_005dbc68</c> : l'etendue de 0x40 est celle que la memoire
    /// laisse.
    ///
    /// <para><b>Le compte n'est borne par rien</b> : un octet, incremente a chaque bit different,
    /// sur 64 + 64 + 32 octets de banque. Voir <see cref="RecordBankChange"/> pour ce qui se passe
    /// au 65e changement.</para>
    /// </summary>
    private static readonly byte* s_ChangeBit_005dbb50 = (byte*)NativeMemory.AllocZeroed(0x40);
    private static readonly byte* s_ChangeState_005dbb90 = (byte*)NativeMemory.AllocZeroed(0x40);
    private static readonly byte* s_ChangeIndex_005dbbd0 = (byte*)NativeMemory.AllocZeroed(0x40);
    private static readonly byte* s_ChangeKind_005dbc28 = (byte*)NativeMemory.AllocZeroed(0x40);
    private static byte s_ChangeCount_005dbc10;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00563c78</c> — le descripteur d'interface de la boite MONITOR, 0x8c8
    /// octets comme tous les autres : il finit sur <c>0x00564540</c>, qui est
    /// <c>s_Unknown_00564540</c>. Ses 26 dwords d'en-tete sont releves dans le binaire, et le reste
    /// y est nul (verifie octet par octet jusqu'a la borne).
    ///
    /// <para><b><c>DAT_00564524</c> n'est pas une globale a part</b> : c'est son champ
    /// <c>+0x8ac</c>, le flux lie par <c>BindMenuLabels</c>. Blaze.c le teste deux fois et ne
    /// l'ecrit jamais sous ce nom, ce qui s'explique ainsi. Non nul = la boite est ouverte.</para>
    ///
    /// <para>Alloue bas et paresseusement, et pris au demarrage avec les autres descripteurs
    /// (<c>Platform/Winsys3d.cs</c>) : il traverse <c>RunTextEngine</c>, qui masque sur 24 bits.</para>
    /// </summary>
    private static byte* s_MonitorBoxStorage;
    private static byte* s_MonitorBox_00563c78
        => s_MonitorBoxStorage != null
            ? s_MonitorBoxStorage
            : (s_MonitorBoxStorage = BuildMonitorBox());

    private static byte* BuildMonitorBox()
    {
        byte* block = AllocLowZeroed(GameMemory.UiDescriptorStride);
        uint* a = (uint*)block;
        a[0] = 0x00000031; a[1] = 0x00580000; a[2] = 0x007800f0; a[3] = 0x00000000;
        a[4] = 0x0a200000; a[5] = 0x00000000; a[6] = 0x010f0808; a[7] = 0x02000000;
        a[8] = 0x01071010; a[9] = 0x01000000; a[10] = 0x00000000; a[11] = 0x01000000;
        a[12] = 0x010f40e4; a[13] = 0x01000000; a[14] = 0x000f1000; a[15] = 0x01000000;
        a[16] = 0x010f0000; a[17] = 0x01000000; a[19] = 0x01000000; a[21] = 0x01000000;
        a[23] = 0x01000000; a[25] = 0x01000000;
        return block;
    }

    /// <summary>
    /// Ghidra: <c>FUN_00516fc0</c> de <c>0x00517208</c> a <c>0x0051762b</c> — <b>l'en-tete MONITOR</b>,
    /// en tete de chaque image, puis la recopie des trois banques dans leurs miroirs. Transcrit
    /// contre <c>Legacy/Raw/Blaze/FUN_00516FC0-full.asm</c>.
    ///
    /// <code>
    /// y 0x10  ANG   l'orientation de camera, deux angles sur 12 bits (DAT_00604928, +2)
    /// y 0x18  LOC   la position du PERSONNAGE 0 — pas du meneur —, en unites entieres
    /// y 0x20  DPT   la translation de la matrice de camera (DAT_006008f4..fc)
    /// y 0x28  CHG   l'octet DAT_005de058
    /// </code>
    ///
    /// <para><b>Trois registres portent des constantes</b> dans tout ce bloc : <c>edi</c> =
    /// <c>0x00566c28</c> (l'atlas de glyphes), <c>ebp</c> = <c>0x00566c30</c> (celui des chiffres) et
    /// <c>ebx</c> = <c>0x38</c> (la colonne des nombres). Le prologue les pose et le retour de boucle
    /// (<c>0x0051974e</c>) les repose, apres que les trois <c>rep movsd</c> de la fin ont ecrase
    /// <c>edi</c> et <c>ebp</c>.</para>
    ///
    /// <para><b>Le champ <c>+4</c> du bloc n'est pas ecrit avant les quatre libelles</b> ; il ne
    /// l'est qu'avant le premier nombre. Ce qu'ils lisent est ce que l'image precedente a laisse
    /// dans l'emplacement de pile, soit 0x1000000 — le raisonnement de LB-008, applique au bloc
    /// partage par l'appelant, qui l'initialise ainsi.</para>
    ///
    /// <para>Ghidra ecrit <c>uStack_38 = (uint)uVar5 &lt;&lt; 0x10</c> apres la ligne ANG, ce qui
    /// remettrait X a zero. Le binaire n'ecrit que Y (<c>add [esp+56h],si</c>) ; sans effet, chaque
    /// appel qui suit repose X.</para>
    /// </summary>
    private static uint* DrawDebugMonitorHeader(uint* otBase, uint* prim, int* block)
    {
        byte* b = (byte*)block;

        *(short*)(b + 8) = 0x10;
        *(short*)(b + 0xa) = 0x10;
        *(uint**)b = s_GlyphAtlas_00566c28;
        prim = Prims.DrawTextRun(otBase, prim, block, s_AngLabel_00564a28, 0, 0, -1);
        *(short*)(b + 0xa) += 8;
        prim = Prims.DrawTextRun(otBase, prim, block, s_LocLabel_00564a24, 0, 0, -1);
        *(short*)(b + 0xa) += 8;
        prim = Prims.DrawTextRun(otBase, prim, block, s_DptLabel_00564a20, 0, 0, -1);
        *(short*)(b + 0xa) += 8;
        prim = Prims.DrawTextRun(otBase, prim, block, s_ChgLabel_00564a1c, 0, 0, -1);

        // ANG, sur la premiere ligne : on remonte de trois.
        *(short*)(b + 0xa) -= 0x18;
        short* pose = (short*)Mem.s_CameraVector_00604928;
        *(short*)(b + 8) = 0x38;
        *(uint**)b = s_NumeralTileRecord_00566c30;
        *(uint*)(b + 4) = 0x1000000;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)(pose[0] & 0xfff), 4, 0);
        *(short*)(b + 8) += 0x28;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)(pose[1] & 0xfff), 4, 0);

        // LOC : l'emplacement 0 de 0x005e93c0 lu en absolu (0x005e93dc/e0/e4).
        *(short*)(b + 0xa) += 8;
        byte* member0 = Inp.s_SlotFlags_005e93c0;
        prim = DrawSignedTriple(otBase, prim, block,
            *(int*)(member0 + 0x1c) >> 0xc, *(int*)(member0 + 0x20) >> 0xc, *(int*)(member0 + 0x24) >> 0xc);

        // DPT
        *(short*)(b + 0xa) += 8;
        prim = DrawSignedTriple(otBase, prim, block,
            Mem.CameraTranslation[0], Mem.CameraTranslation[1], Mem.CameraTranslation[2]);

        // CHG
        *(short*)(b + 0xa) += 8;
        *(short*)(b + 8) = 0x38;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)(*Mem.Camera(0x005de058) & 0xff), 5, 0);

        Mem.CopyDwords((uint*)Mem.s_StateBank0_005e96e0, (uint*)s_FlagMirror_005dbb08, 0x10);   // rep movsd
        Mem.CopyDwords((uint*)Mem.s_StateBank3_005e9840, (uint*)s_ItemMirror_005ddfd8, 0x10);
        Mem.CopyDwords((uint*)s_PartyAbilityMask_005e9000, (uint*)s_SystemMirror_005ddf90, 8);

        return prim;
    }

    /// <summary>Ghidra: le motif repete deux fois dans l'en-tete MONITOR — trois entiers signes en
    /// colonnes de 0x38 pixels. Les signes moins sont tous traces <b>d'abord</b>, en x = 0x30, 0x68
    /// et 0xa0 avec l'atlas de glyphes, puis les trois valeurs absolues sur cinq chiffres en x = 0x38,
    /// 0x70 et 0xa8 avec l'atlas de chiffres. <c>neg</c> sur <c>int.MinValue</c> le laisse tel quel,
    /// comme le <c>neg</c> du binaire.</summary>
    private static uint* DrawSignedTriple(uint* otBase, uint* prim, int* block, int v0, int v1, int v2)
    {
        byte* b = (byte*)block;

        *(uint**)b = s_GlyphAtlas_00566c28;
        if (v0 < 0)
        {
            v0 = -v0;
            *(short*)(b + 8) = 0x30;
            prim = Prims.DrawTextRun(otBase, prim, block, s_MinusGlyph_00564a18, 0, 0, -1);
        }

        if (v1 < 0)
        {
            v1 = -v1;
            *(short*)(b + 8) = 0x68;
            prim = Prims.DrawTextRun(otBase, prim, block, s_MinusGlyph_00564a18, 0, 0, -1);
        }

        if (v2 < 0)
        {
            v2 = -v2;
            *(short*)(b + 8) = 0xa0;
            prim = Prims.DrawTextRun(otBase, prim, block, s_MinusGlyph_00564a18, 0, 0, -1);
        }

        *(short*)(b + 8) = 0x38;
        *(uint**)b = s_NumeralTileRecord_00566c30;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)v0, 5, 0);
        *(short*)(b + 8) += 0x38;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)v1, 5, 0);
        *(short*)(b + 8) += 0x38;
        prim = Prims.DrawDecimalNumber(otBase, prim, block, (uint)v2, 5, 0);
        return prim;
    }

    /// <summary>
    /// Ghidra: <c>FUN_00516fc0</c> de <c>0x0051815a</c> a <c>0x00518473</c> — <b>le journal des
    /// drapeaux</b>, dans le bloc d'affichage principal, juste apres le panneau d'interface.
    ///
    /// <para>Tant que la boite est fermee, chaque bit de FLAG, ITEM et SYSTEM qui differe de son miroir
    /// est ajoute a la liste. S'il y en a au moins un, la boite s'ouvre sur le meneur, et le bit CARRE
    /// « vient d'etre presse » de sa manette est efface — pour que l'appui qui a peut-etre cause le
    /// changement ne ferme pas aussitot la boite. Tant qu'elle est ouverte, la liste n'est plus
    /// recalculee : elle reste affichee, une ligne de huit pixels par changement a partir de
    /// y = 0x68 — <c>EvFlag :</c>, l'octet, le bit, <c>On</c>/<c>Off</c> —, puis la boite passe
    /// par le moteur de texte et le moteur de panneau.</para>
    /// </summary>
    private static uint* DrawDebugMonitorChanges(uint* otBase, uint* prim, int* block)
    {
        byte* b = (byte*)block;
        byte* box = s_MonitorBox_00563c78;

        if (*(byte**)(box + 0x8ac) == null)     // Ghidra: DAT_00564524 == 0
        {
            s_ChangeCount_005dbc10 = 0;
            CollectBankChanges(Mem.s_StateBank0_005e96e0, s_FlagMirror_005dbb08, 0x40, 0);
            CollectBankChanges(Mem.s_StateBank3_005e9840, s_ItemMirror_005ddfd8, 0x40, 1);
            CollectBankChanges(s_PartyAbilityMask_005e9000, s_SystemMirror_005ddf90, 0x20, 2);

            if (s_ChangeCount_005dbc10 != 0)
            {
                byte* leader = Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c;
                byte* pad = Inp.s_InputStateBase_00604a20 + Inp.s_PlayerSlots_005e9188[leader[8] * 0x4c] * 0x10;
                *(ushort*)(pad + 2) &= 0x7fff;
                BindMenuLabels(s_MonitorStream_00564a10, leader, box, 0);   // Ghidra: FUN_00544350
            }

            // Relu : c'est la liaison qui vient d'etre faite qui ouvre la boite des cette image.
            if (*(byte**)(box + 0x8ac) == null) return prim;
        }

        *(uint*)(b + 4) = 0x1000000;
        *(short*)(b + 0xa) = 0x68;

        for (int i = 0; i < s_ChangeCount_005dbc10; i++)
        {
            *(short*)(b + 8) = 0x10;
            *(uint**)b = s_GlyphAtlas_00566c28;
            prim = Prims.DrawTextRun(otBase, prim, block, s_ChangeKindLabels_00563350[s_ChangeKind_005dbc28[i]], 0, 0, -1);

            *(short*)(b + 8) += 0x48;
            *(uint**)b = s_NumeralTileRecord_00566c30;
            prim = Prims.DrawDecimalNumber(otBase, prim, block, s_ChangeIndex_005dbbd0[i], 5, 0);

            *(short*)(b + 8) += 0x18;
            *(uint**)b = s_NumeralTileRecord_00566c30;
            prim = Prims.DrawDecimalNumber(otBase, prim, block, s_ChangeBit_005dbb50[i], 5, 0);

            *(short*)(b + 8) += 0x10;
            *(uint**)b = s_GlyphAtlas_00566c28;
            prim = Prims.DrawTextRun(otBase, prim, block, s_ChangeStateLabels_00563360[s_ChangeState_005dbb90[i]], 0, 0, -1);

            *(short*)(b + 0xa) += 8;
        }

        prim = Ui.RunTextEngine(prim, box);             // Ghidra: FUN_005443a0
        prim = UiWin.DrawUiPanel(otBase, prim, box);    // Ghidra: FUN_00545340
        return prim;
    }

    /// <summary>Ghidra: les trois doubles boucles de <c>0x00518163</c>, <c>0x005181dd</c> et
    /// <c>0x00518257</c>, identiques a la banque, au miroir, a la longueur et au genre pres.</summary>
    private static void CollectBankChanges(byte* bank, byte* mirror, int length, byte kind)
    {
        for (int index = 0; index < length; index++)
        {
            byte now = bank[index];
            byte before = mirror[index];

            for (int bit = 0; bit < 8; bit++)
            {
                int mask = 1 << bit;
                if (((before ^ now) & mask) != 0)
                {
                    RecordBankChange((byte)bit, (byte)index, kind, (now & mask) != 0 ? (byte)1 : (byte)0);
                }
            }
        }
    }

    /// <summary>
    /// Ghidra: une entree de la liste des changements, ecrite colonne par colonne a l'indice
    /// <c>DAT_005dbc10</c>, qui est ensuite incremente.
    ///
    /// <para><b>Au 65e changement, l'original deborde</b>, et sans rien pour l'arreter : l'indice 0x40
    /// ecrit l'octet de banque sur <c>0x005dbc10</c> — le compte lui-meme — et le genre sur
    /// <c>0x005dbc68</c>, le premier octet des panneaux du monde. Les colonnes sont ici des
    /// allocations separees ; le debordement n'est pas modelise et <b>leve</b> plutot que d'ecrire
    /// hors de ses tampons. Il faut 65 bits changes entre le haut de l'image et son affichage, ce
    /// que ni un script ni le menu (dont la remise a zero tombe sur une image ou ce bloc ne passe
    /// pas) ne produisent.</para>
    /// </summary>
    private static void RecordBankChange(byte bit, byte index, byte kind, byte state)
    {
        int slot = s_ChangeCount_005dbc10;
        if (slot >= 0x40)
        {
            throw new NotImplementedException(
                "FUN_00516fc0 (journal MONITOR, 0x0051818a) : plus de 64 bits ont change en une image. " +
                "L'original deborde de ses quatre colonnes de 0x40 octets : l'indice 0x40 ecrase le " +
                "compte DAT_005dbc10 et le debut de s_WorldPanels_005dbc68. Ce debordement n'est pas " +
                "modelise.");
        }

        s_ChangeBit_005dbb50[slot] = bit;
        s_ChangeIndex_005dbbd0[slot] = index;
        s_ChangeKind_005dbc28[slot] = kind;
        s_ChangeState_005dbb90[slot] = state;
        s_ChangeCount_005dbc10++;
    }
}
