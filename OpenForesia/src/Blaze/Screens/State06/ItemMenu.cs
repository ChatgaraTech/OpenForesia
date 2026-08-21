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
    // =====================================================================
    // Le menu objets — l'état de jeu 6 (Ghidra: FUN_00524180)
    //
    // Atteint depuis « Menu objets », la troisième entrée du menu principal. Deux temps, comme les
    // autres états : un chargement de ressources (FUN_00524210) puis une boucle d'image
    // (FUN_00524320) qui déroule une machine à cinq sous-écrans sur DAT_00566ac8 :
    //
    //     -1  l'écran racine, trois lignes           FUN_00524680    82 lignes   <- transcrit
    //      0  « Invertaire »                         FUN_00524880  1377 lignes
    //      1  « Enchère »                            FUN_005277b0   267 lignes
    //      3  (atteint depuis les précédents)        FUN_00527e30   683 lignes
    //      4  une fermeture, qui repasse à -1        FUN_005298c0    84 lignes
    //
    // Les quatre sous-écrans restants sont nommés et gardés ; la racine, elle, marche et permet de
    // ressortir vers le menu principal.
    // =====================================================================

    /// <summary>Ghidra: DAT_005de410 — l'enregistrement courant, MainMem + 0xf0000.</summary>
    private static ref int s_ItemMenuRecord_005de410 => ref *Mem.ItemMenuState(0x005de410);

    /// <summary>Ghidra: DAT_005de4e0 — la ligne sous le curseur de l'écran racine, 0..2.</summary>
    private static ref int s_ItemMenuCursor_005de4e0 => ref *Mem.ItemMenuState(0x005de4e0);

    private static ref int s_Unknown_005de4d4 => ref *Mem.ItemMenuState(0x005de4d4);
    private static ref int s_Unknown_005de464 => ref *Mem.ItemMenuState(0x005de464);
    private static ref int s_Unknown_005de50c => ref *Mem.ItemMenuState(0x005de50c);

    /// <summary>
    /// Ghidra: DAT_00566ac8 et DAT_00566acc — le sous-écran courant et son compagnon, adjacents et
    /// tous deux remis à -1 au chargement. Déclarés ensemble pour la même raison que le bloc
    /// ci-dessus.
    /// </summary>
    private static readonly int* s_ItemMenuSubStatePair_00566ac8 = (int*)NativeMemory.AllocZeroed(2, sizeof(int));
    private static ref int s_ItemMenuSubState_00566ac8 => ref s_ItemMenuSubStatePair_00566ac8[0];
    private static ref int s_Unknown_00566acc => ref s_ItemMenuSubStatePair_00566ac8[1];

    /// <summary>
    /// Ghidra: &amp;DAT_00564fe0 et &amp;DAT_005658a8 — **deux** descripteurs d'interface contigus
    /// (0x564fe0 + 0x8c8 = 0x5658a8), ceux du menu objets. Sa boucle les parcourt exactement ainsi :
    /// <c>p = &amp;DAT_00564fe0; deux tours { RunTextEngine; DrawUiPanel; p += 0x8c8; }</c>, et le
    /// bandeau de titre lie dans le second.
    ///
    /// <para>Les 26 dwords d'en-tête de chacun sont relevés dans <c>Legacy/Blaze.exe</c>. Alloués
    /// bas et paresseusement pour la même raison que les autres : ils traversent
    /// <see cref="RunTextEngine"/> puis <see cref="DrawUiPanel"/>, donc portent une table
    /// d'ordonnancement à +0xa4.</para>
    /// </summary>
    /// <remarks>
    /// La boucle de rendu du menu objets n'en parcourt que **deux**, mais il y en a **trois**
    /// contigus en memoire : 0x564fe0 + 0x8c8 = 0x5658a8 + 0x8c8 = 0x566170. Le troisieme n'est
    /// utilise que par le sous-ecran « Invertaire » (FUN_00524880), qui y lie sa ligne de saisie
    /// et son bandeau de nom, et qui le dessine lui-meme via DrawUiPanel. D'ou deux compteurs :
    /// celui du rendu de la page, et celui de l'allocation.
    /// </remarks>
    private const int ItemMenuDescriptorCount = 2;
    private const int ItemMenuDescriptorAllocCount = 3;
    private static byte* s_ItemMenuDescriptorsStorage;
    private static byte* s_ItemMenuDescriptors_00564fe0
        => s_ItemMenuDescriptorsStorage != null
            ? s_ItemMenuDescriptorsStorage
            : (s_ItemMenuDescriptorsStorage = BuildItemMenuDescriptors());

    private static byte* s_ItemMenuTitleDescriptor_005658a8
        => s_ItemMenuDescriptors_00564fe0 + GameMemory.UiDescriptorStride;

    private static byte* BuildItemMenuDescriptors()
    {
        byte* block = AllocLowZeroed((uint)(ItemMenuDescriptorAllocCount * GameMemory.UiDescriptorStride));

        uint* a = (uint*)block;
        a[0] = 0x00000031; a[1] = 0x00a00008; a[2] = 0x002800e0; a[3] = 0x00000000;
        a[4] = 0x021a0000; a[5] = 0x0000000c; a[6] = 0x010f0808; a[7] = 0x02cf1020;
        a[8] = 0x01070c10; a[9] = 0x01000000; a[10] = 0x00000000; a[11] = 0x01000000;
        a[12] = 0x000f28d4; a[13] = 0x01000000; a[14] = 0x000f0c00; a[15] = 0x01000000;
        a[16] = 0x010f0000; a[17] = 0x01000000; a[19] = 0x01000000; a[21] = 0x01000000;
        a[23] = 0x01000000; a[25] = 0x01000000;

        uint* b = (uint*)(block + GameMemory.UiDescriptorStride);
        b[0] = 0x00000031; b[1] = 0x00080008; b[2] = 0x009000e0; b[3] = 0x00000000;
        b[4] = 0x081a0000; b[5] = 0x00000003; b[6] = 0x010f0808; b[7] = 0x02cf1020;
        b[8] = 0x01071018; b[9] = 0x01000000; b[10] = 0x00000000; b[11] = 0x01000000;
        b[12] = 0x00000000; b[13] = 0x01000000; b[14] = 0x00000000; b[15] = 0x01000000;
        b[16] = 0x010f0000; b[17] = 0x01000000; b[19] = 0x01000000; b[21] = 0x01000000;
        b[23] = 0x01000000; b[25] = 0x01000000;

        // Ghidra: &DAT_00566170 — releve dans Legacy/Blaze.exe, meme forme que les deux autres.
        // Son +4 vaut 0x00640014, exactement la valeur que la phase 9 de FUN_00524880 y reecrit.
        uint* c = (uint*)(block + 2 * GameMemory.UiDescriptorStride);
        c[0] = 0x00000031; c[1] = 0x00640014; c[2] = 0x008800e0; c[3] = 0x00000000;
        c[4] = 0x081a0000; c[5] = 0x0000000b; c[6] = 0x000f0808; c[7] = 0x02cf1020;
        c[8] = 0x01070000; c[9] = 0x01000000; c[10] = 0x00000000; c[11] = 0x01000000;
        c[12] = 0x00000000; c[13] = 0x01000000; c[14] = 0x00000000; c[15] = 0x01000000;
        c[16] = 0x000f0000; c[17] = 0x01000000; c[19] = 0x01000000; c[21] = 0x01000000;
        c[23] = 0x01000000; c[25] = 0x01000000;

        return block;
    }

    /// <summary>Ghidra: &amp;DAT_00566170 — le troisieme descripteur, propre au sous-ecran
    /// « Invertaire ».</summary>
    private static byte* s_ItemMenuEditDescriptor_00566170
        => s_ItemMenuDescriptors_00564fe0 + 2 * GameMemory.UiDescriptorStride;

    /// <summary>
    /// Ghidra: &amp;PTR_s_Invertaire_00566b58 — les trois lignes de l'écran racine, dans l'ordre où
    /// la boucle les dessine. Les chaînes sont relevées dans <c>Legacy/Blaze.exe</c> à 0x00566b64.
    ///
    /// <para><b>« Invertaire » est bien la faute de frappe de 1998</b>, pas une erreur de relevé :
    /// les octets sont <c>49 6e 76 65 72 74 61 69 72 65 00</c>. Voir <c>legacy-bug.md</c> — ne pas
    /// la corriger.</para>
    /// </summary>
    private static readonly byte*[] s_InventoryLabels_00566b58 =
    [
        ToAnsiZ("Invertaire"),
        ToAnsiZ("Enchère"),
        ToAnsiZ("Quitter"),
    ];

    /// <summary>Ghidra: FUN_00524180 @ 0x00524180 — l'état de jeu 6 : le chargement puis la boucle.</summary>
    private static void RunItemMenuState()
    {
        LoadItemMenuResources();     // Ghidra: FUN_00524210
        RunItemMenuFrameLoop();      // Ghidra: FUN_00524320
    }

    /// <summary>
    /// Ghidra: FUN_00524210 @ 0x00524210 — remet le bloc d'état à zéro, puis recharge les
    /// ressources de l'état : l'entrée principale, la remise en place de son en-tête, et la liste
    /// +0x10 décodée entrée par entrée dans la table de pages.
    /// </summary>
    private static void LoadItemMenuResources()
    {
        // Ghidra: DAT_005de4c4..DAT_005de50c remis à zéro d'affilée — dix-neuf entiers.
        NativeMemory.Clear(Mem.ItemMenuState(0x005de4c4), 19 * sizeof(int));
        s_ItemMenuSubState_00566ac8 = -1;
        s_Unknown_00566acc = -1;

        // Ghidra: FUN_0040a600() — corps vide, rien à reproduire.
        s_Unknown_005e97d0 = Res.LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, Mem.s_MainResourceDirectory_005e97c4);
        // Ghidra: DAT_005e97d0 — le pendant de DAT_005e97c8 pour cet état ; voisin immédiat.

        Res.FixupResourceHeader(Mem.s_MainResourceDirectory_005e97c4);

        byte* mainMem = GetMainMemPtr();
        if (Mem.s_MainResourceDirectory_005e97c4 == null || mainMem == null) return;

        int* textures = *(int**)(Mem.s_MainResourceDirectory_005e97c4 + 0x10);
        if (textures == null) return;

        byte* scratch = mainMem + 0x1b0000;
        byte* slot = Mem.s_EntityDecodeTable_005e8f00;
        for (int* entry = textures; *entry != 0; entry += 2, slot += 4)
        {
            Res.LoadResourceEntry((int*)(nint)(*entry), scratch);
            Res.DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
        }
    }

    /// <summary>
    /// Ghidra: FUN_00524320 @ 0x00524320 — la boucle d'image du menu objets.
    ///
    /// <para>Deux sorties : SELECT+START ensemble (le masque 9 sur le mot du bas) tant qu'aucun
    /// sous-écran n'est ouvert, et le passage de <see cref="s_GameStateId"/> à autre chose que 6,
    /// que la ligne « Quitter » de l'écran racine provoque.</para>
    ///
    /// <para>Le rendu de fin d'image rétrécit les deux DISPENV à 0x100 x 0xe0 le temps de l'envoi,
    /// puis les remet à 0x140 x 0xf0 — le même va-et-vient que la mappemonde, et pour la même
    /// raison : la page est cadrée en 256x224 dans un écran de 320x240.</para>
    /// </summary>
    private static void RunItemMenuFrameLoop()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);

        s_PartySize_005e9020 = 0;
        byte* mainMem = GetMainMemPtr();
        s_ItemMenuRecord_005de410 = (int)(nint)(mainMem + 0xf0000);

        // Ghidra: DAT_005e93c0 = DAT_005e93c0 & 0xe7ff8f79 | 0x80800e00 — le mot d'état de
        // l'emplacement 0, repris bit à bit.
        uint* slot0 = (uint*)Inp.s_SlotFlags_005e93c0;
        *slot0 = *slot0 & 0xe7ff8f79 | 0x80800e00;

        // Ghidra: les quatre emplacements repris. **Deux pas différents dans la même boucle**, ce
        // que la pseudo-C cache derrière ses types de pointeur : `pcVar2` avance de 0x9c sur
        // DAT_005e93c8 (= +0x248), tandis que `puVar3` est un `undefined2*` avançant de 0x26, soit
        // **0x4c octets**, sur DAT_005e9182 (= +0x02). La borne 0x5e9638 sur le premier donne les
        // quatre tours.
        //
        // Les avoir tous deux écrits à 0x9c au premier jet écrasait des champs voisins de l'arène
        // — dont la table d'emplacements que CurrentPadState indexe — et figeait le menu à sa
        // première image, écran noir. Mesuré le 2026-08-18.
        for (int i = 0; i < 4; i++)
        {
            Inp.s_SlotArea_005e9180[0x248 + i * 0x9c] = (byte)i;
            Inp.s_SlotArea_005e9180[0x0b + i * 0x4c] = (byte)i;
            *(ushort*)(Inp.s_SlotArea_005e9180 + 0x02 + i * 0x4c) = 0;
        }


        PlayMusicTrack(0x25);
        Inp.JoystickResetState(false);

        while (true)
        {
            Inp.s_Unknown_005de168 = 0;
            byte* pad = Res.CurrentPadState();

            // SELECT + START ensemble, et seulement hors sous-écran.
            if ((pad[2] & 9) == 9 && s_Unknown_00566acc < 0) break;

            if (s_GameStateId != 6)
            {
                s_Unknown_005de15c = 0;
                Inp.s_Unknown_005de168 = 0;
                return;
            }

            Inp.s_Unknown_005de168 = 1;
            s_Unknown_005de15c = 1;

            if (s_SmallTextureFlag == 0 && Mem.s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de158 = 0;
                s_Unknown_005de154 = 0;
            }
            else
            {
                s_Unknown_005de154 = 0xa0;
                s_Unknown_005de158 = (0xb - Mem.s_SizeSetting_005de160) * 0xc;
            }

            // Ghidra: les deux axes de manette comparés au sentinelle « absente », comme au
            // sous-état 7 de la sélection des personnages.
            s_Unknown_005de50c = *(short*)Inp.s_JoyAxis1_00604b0a == -0x8000 ? 1 : 0;
            if (*(short*)Inp.s_JoyAxis2_00604b2e == -0x8000) s_Unknown_005de50c += 2;

            switch (s_ItemMenuSubState_00566ac8)
            {
                case -1:
                    primCursor = ItemMenuRootScreen(otBase, otEntry, primCursor, pad);
                    break;
                case 0:
                    primCursor = ItemMenuInventoryScreen(otBase, otEntry, primCursor, pad);
                    break;
                case 1:
                    primCursor = ItemMenuAuctionScreen(otBase, otEntry, primCursor, pad);
                    break;
                case 3:
                    primCursor = ItemMenuUseScreen(otBase, otEntry, primCursor, pad);
                    break;
                case 4:
                    // Ghidra: FUN_005298c0(); DAT_00566ac8 = 0xffffffff; puis **pas de break** :
                    // la fermeture retombe volontairement dans le default de l'original.
                    CloseItemMenu();
                    s_ItemMenuSubState_00566ac8 = -1;
                    break;
                default:
                    break;
            }

            for (int i = 0; i < ItemMenuDescriptorCount; i++)
            {
                byte* panel = s_ItemMenuDescriptors_00564fe0 + i * GameMemory.UiDescriptorStride;
                primCursor = Ui.RunTextEngine(primCursor, panel);
                primCursor = UiWin.DrawUiPanel(otBase, primCursor, panel);
            }

            s_Unknown_005de15c = 0;

            // Le fond, centré en (0x80, 0x70) et non (0xa0, 0x78) : la page est cadrée en 256x224.
            byte* sprite = stackalloc byte[20];
            NativeMemory.Clear(sprite, 20);
            *(uint**)sprite = **(uint***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            *(uint*)(sprite + 4) = 0x1000000;
            *(short*)(sprite + 8) = 0x80;
            *(short*)(sprite + 0xa) = 0x70;
            *(ushort*)(sprite + 0xc) = 0x1420;
            *(ushort*)(sprite + 0xe) = 0x1149;
            *(short*)(sprite + 0x10) = 0;
            primCursor = Prims.DrawSpritePrimitive(otEntry, primCursor, (int*)sprite, 0, 0);
            primCursor = Prims.DrawFlatPrimitive(otEntry, primCursor, (int*)sprite);

            // Ghidra: les deux DISPENV rétrécis le temps de l'envoi, puis remis.
            SetDisplayEnvExtent(0x100, 0xe0);
            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
            SetDisplayEnvExtent(0x140, 0xf0);
        }

        s_GameStateId = 0;
        s_Unknown_005de15c = 0;
    }

    /// <summary>Ghidra: _DAT_005e9950/_DAT_005e98e0 et _DAT_005e9952/_DAT_005e98e2 — la largeur et
    /// la hauteur des deux DISPENV, écrites ensemble.</summary>
    private static void SetDisplayEnvExtent(ushort width, ushort height)
    {
        *(ushort*)(Mem.s_DrawEnv1_005e98f0 + 0x60) = width;
        *(ushort*)(Mem.s_DrawEnv1_005e98f0 + 0x62) = height;
        *(ushort*)(Mem.s_DrawEnv0_005e9880 + 0x60) = width;
        *(ushort*)(Mem.s_DrawEnv0_005e9880 + 0x62) = height;
    }

    /// <summary>
    /// Ghidra: FUN_00524680 @ 0x00524680 — l'écran racine du menu objets : trois lignes, un curseur
    /// qui boucle, et la validation qui ouvre le sous-écran correspondant. La troisième ligne,
    /// « Quitter », repose <see cref="s_GameStateId"/> à 0 et rend la main au menu principal.
    /// </summary>
    private static uint* ItemMenuRootScreen(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        BindMenuLabels(LabelAt(200), 0, s_ItemMenuDescriptors_00564fe0, 0);

        byte* mainMem = GetMainMemPtr();
        s_ItemMenuRecord_005de410 = (int)(nint)(mainMem + 0xf0000);

        int before = s_ItemMenuCursor_005de4e0;
        if ((pad[6] & 0x10) != 0) s_ItemMenuCursor_005de4e0--;
        if ((pad[6] & 0x40) != 0) s_ItemMenuCursor_005de4e0++;
        if (s_ItemMenuCursor_005de4e0 < 0) s_ItemMenuCursor_005de4e0 = 2;
        if (s_ItemMenuCursor_005de4e0 > 2) s_ItemMenuCursor_005de4e0 = 0;
        if (before != s_ItemMenuCursor_005de4e0) Aud.PlayUiSound3A();

        // CARRE : ouvrir la ligne choisie, apres avoir attendu le relachement.
        if ((pad[3] & 0x80) != 0)
        {
            Aud.PlayUiSound38();

            // DÉVIATION d'un jeton, et c'est la même que celle de la page d'options — voir sa note
            // dans RunMainMenuFrameLoop. L'original écrit cette vidange sans cadencement
            // (`iVar2 = FUN_005048f0(); while (iVar2 != 0) iVar2 = FUN_005048f0();`). Chez nous
            // `Inp.PollJoystickState` ne fait pas tourner la pompe de messages, donc l'état DirectInput
            // ne se rafraîchit jamais et la boucle ne se termine pas : **mesuré le 2026-08-18, le
            // menu objets se figeait à sa première image**, écran noir, sans plantage ni progrès.
            // `Ordinal_132(0)` rend la main à la pompe (winsys3d, FUN_00a053e0), ce qui laisse le
            // relâchement arriver.
            while (InputDrainStub() != 0) { Winsys3d.Ordinal_132(0); }

            s_ItemMenuSubState_00566ac8 = s_ItemMenuCursor_005de4e0;
            if (s_ItemMenuCursor_005de4e0 == 0)
            {
                s_Unknown_00566acc = -1;
                s_Unknown_005de4d4 = 0;
            }
            else if (s_ItemMenuCursor_005de4e0 == 1)
            {
                s_Unknown_00566acc = -1;
                s_Unknown_005de4d4 = 0;
                s_ItemMenuRecord_005de410 = (int)(nint)(mainMem + 0xf0000);
                s_PartySize_005e9020 = 0;
                s_Unknown_005de464 = 0;
                BindMenuLabels(Mem.s_Unknown_005606f8, 0, s_ItemMenuDescriptors_00564fe0, 0);
            }
            else if (s_ItemMenuCursor_005de4e0 == 2)
            {
                s_GameStateId = 0;
            }
        }

        // Les trois libelles, dessines a (100, 0x2c) puis empiles de 0xc en 0xc.
        int* cursor = stackalloc int[8];
        byte* c = (byte*)cursor;
        NativeMemory.Clear(c, 32);
        *(uint*)(c + 4) = 0x1000000;
        *(uint**)c = s_GlyphAtlas_00566c28;
        *(short*)(c + 8) = 100;
        *(short*)(c + 0xa) = 0x2c;
        for (int i = 0; i < s_InventoryLabels_00566b58.Length; i++)
        {
            primCursor = Prims.DrawTextRun(otBase, primCursor, cursor, s_InventoryLabels_00566b58[i], 0, 0, -1);
            *(short*)(c + 0xa) += 0xc;
        }

        // Le curseur, seize pixels a gauche de la colonne de texte.
        UiWin.EmitViewportRect(otBase, &primCursor);
        *(uint**)c = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
        *(short*)(c + 8) = 100 - 0x10;
        *(short*)(c + 0x10) = 0x18;
        *(short*)(c + 0xa) = (short)(s_ItemMenuCursor_005de4e0 * 0xc + 0x28);
        primCursor = Prims.DrawSpriteVariant(otBase, primCursor, cursor, 0, 0);
        primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, cursor);
        UiWin.EmitClipRect(otBase, &primCursor);

        // Ghidra type FUN_00524680 et FUN_00524190 en `void`, mais le site d'appel fait
        // `local_20 = FUN_00524680(...)` : EAX porte à la sortie le résultat du dernier appel, qui
        // est celui de FUN_005443a0 — le curseur de primitives que le moteur de texte a avancé.
        // Rendre l'ancien curseur laisse les dessins suivants réécrire par-dessus les primitives du
        // bandeau, ce qui casse la table d'ordonnancement : mesuré le 2026-08-18, DrawOTag ne
        // revenait jamais et le menu se figeait dans FrameEnd. Même défaut de type de retour que
        // Saves.RetryOpenCardFile.
        return DrawItemMenuTitle(primCursor);
    }

    /// <summary>
    /// Ghidra: FUN_00524190 @ 0x00524190 — le bandeau de titre, monté sur la pile puis lié dans le
    /// second descripteur.
    ///
    /// <para><b>La pseudo-C laissait un doute sur la disposition ; le désassemblage le lève</b>
    /// (<c>0x00524198</c>, <c>0x005241D3</c>, <c>0x005241D9</c>, <c>0x005241DE</c>) : les quatre
    /// octets de tête sont <c>0C 04 18 10</c>, et le quatrième **écrase le premier octet de la
    /// chaîne copiée**. Ce n'est pas une bavure : le libellé commence lui-même par <c>0x0C</c>
    /// (« début de page »), que ce préfixe remplace par sa propre séquence.</para>
    /// </summary>
    private static uint* DrawItemMenuTitle(uint* primCursor)
    {
        byte* label = LabelAt(0x34);
        if (label == null) return primCursor;

        byte* buffer = stackalloc byte[100];
        NativeMemory.Clear(buffer, 100);
        buffer[0] = 0x0c;
        buffer[1] = 0x04;
        buffer[2] = 0x18;
        buffer[3] = 0x10;

        int length = 0;
        while (label[length] != 0 && length < 94) length++;
        for (int i = 1; i <= length; i++) buffer[3 + i] = label[i];
        buffer[3 + length + 1] = 0;

        BindMenuLabels(buffer, 0, s_ItemMenuTitleDescriptor_005658a8, 0);
        return Ui.RunTextEngine(primCursor, s_ItemMenuTitleDescriptor_005658a8);
    }

    /// <summary>
    /// Ghidra: la construction des lignes du répertoire de sauvegardes — **le même bloc écrit deux
    /// fois dans l'original**, une fois en tête de <c>FUN_00536ea0</c> (l'écran d'organisation du
    /// groupe) et une fois en tête de <c>FUN_005277b0</c> (le sous-écran « Enchère » du menu
    /// objets), à la virgule près. Extrait ici plutôt que recopié, comme les <c>Retry…CardFile</c>.
    ///
    /// <para>Une ligne de 0x16 octets par entrée : le nom sur 11 caractères complété d'espaces,
    /// l'abréviation de classe sur 4, <c>'N'</c>, puis le niveau en décimal. Un fichier qui ne
    /// s'ouvre pas donne la ligne de huit tirets.</para>
    /// </summary>
    private static void BuildSaveDirectoryRows(byte* scratch, int entryCount)
    {
        for (int entry = 0, rowOffset = 0; entry < entryCount; entry++, rowOffset += PartyRowStride)
        {
            byte* row = s_PartyRowText_005e8640 + rowOffset;

            Saves.SetCardFileName(s_CardDirectoryEntries_006049bc + entry * 0x28);
            Saves.SetCardPathDeviceFields();

            IntPtr handle = Winsys3d.Ordinal_21(Saves.s_CardPathDescriptor_00604940, 1);
            if ((long)handle < 0)
            {
                // Ghidra: neuf octets copiés depuis s__________00566b8c (deux dwords puis l'octet
                // 8) — la ligne « fichier illisible ».
                for (int i = 0; i < 9; i++) row[i] = s_DashesLabel_00566b8c[i];
            }
            else
            {
                Winsys3d.Ordinal_24(handle, scratch, 0x2000);
                Winsys3d.Ordinal_22(handle);
                // Les seize octets du nom, à +0x100 de l'enregistrement — le même champ que
                // Saves.WriteCardSaveFile encode dans l'en-tête du fichier.
                for (int i = 0; i < 16; i++) row[i] = scratch[0x100 + i];
            }

            // Ghidra: la longueur utile s'arrête au premier octet nul, plafonnée à 0xb.
            int length = 0;
            while (length < 0xb && row[length] != 0) length++;
            for (int i = length; i < 0xb; i++) row[i] = (byte)' ';

            // Quatre octets d'abréviation de classe en 0xb..0xe, puis 'N' en 0xf et le niveau en
            // décimal à partir de 0x10.
            int classIndex = scratch[0x150] & 7;
            for (int i = 0; i < 4; i++) row[0xb + i] = s_ClassAbbrev_0056c868[classIndex * 5 + i];
            row[0xf] = LevelPrefix_0056c890;
            FormatDecimal(row + 0x10, *(ushort*)(scratch + 0x144));
        }
    }

    // ---------------------------------------------------------------------------------------
    // Le sous-écran « Enchère » (Ghidra: FUN_005277b0) et la fermeture (FUN_005298c0).
    // ---------------------------------------------------------------------------------------
}
