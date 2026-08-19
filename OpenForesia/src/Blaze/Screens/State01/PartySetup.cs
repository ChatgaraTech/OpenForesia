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
    // Écran de constitution du groupe — l'état de jeu 1, chemin B (Ghidra: FUN_00536ea0)
    //
    // La liste des personnages sauvegardés, dans laquelle le joueur en choisit jusqu'à quatre pour
    // former son groupe. C'est l'écran auquel mènent **les deux** entrées que Vincent a identifiées
    // sur le jeu de 1998 : « Début du jeu » au menu principal, et « Constituer un groupe » à la fin
    // de la création d'un personnage — toutes deux passent par l'état de jeu 1, dont le chemin A
    // est l'écran de mise au point (RunDebugPartySelect) et le chemin B celui-ci.
    //
    // C'est aussi **le seul appelant de LoadSaveDirectory** (FUN_005126c0) : le point qui restait
    // ouvert après la séance sur la sauvegarde. La table des répertoires n'était vide que parce que
    // rien ne remplissait son rôle d'appelant.
    // =====================================================================
    private static byte* s_PartySelectionOrder_005e85c0 => Mem.s_PartyListArena_005e85c0;
    private static byte* s_PartyRowText_005e8640 => Mem.s_PartyListArena_005e85c0 + (0x005e8640 - GameMemory.PartyListBase);
    private const int PartyRowStride = 0x16;

    // Ghidra: &DAT_0056c868 — les abréviations de classe, pas de 5, huit entrées (décalage fichier
    // 0x16b068). L'original les indexe depuis &DAT_0056c85d avec un compteur qui vaut 0xb..0xe, ce
    // qui retombe exactement sur cette base ; ici l'index est ramené à 0..3.
    private static readonly byte* s_ClassAbbrev_0056c868 =
        ToAnsiZ("Gue.\0Pre.\0Vol.\0Mag.\0Cha.\0Elf \0Nai.\0Lut.");

    // Ghidra: DAT_0056c890 — l'octet 'N' (Niveau) posé juste avant le nombre.
    private const byte LevelPrefix_0056c890 = (byte)'N';

    // Ghidra: s__________00566b8c — huit tirets, la ligne de remplacement quand le fichier de
    // sauvegarde d'une entrée ne s'ouvre pas.
    private static readonly byte* s_DashesLabel_00566b8c = ToAnsiZ("--------");

    // Ghidra: &DAT_00563148 — l'enregistrement de texture à deux dwords de la ligne de liste,
    // 0x10102000 / 0x003e0000 (décalage fichier 0x161748). **Mutable** : la boucle de rendu réécrit
    // ses octets 0 et 1 à chaque ligne pour déplacer la fenêtre de texture. Le 0x3e du second dword
    // désigne l'entrée 0x3e de la table de pages, soit s_EntityDecodeTable_005e8f00 + 0xf8, que la
    // même boucle écrit juste avant de dessiner.
    private static readonly byte* s_RowSpriteRecord_00563148 = BuildRowSpriteRecord();

    private static byte* BuildRowSpriteRecord()
    {
        uint* record = (uint*)NativeMemory.AllocZeroed(8);
        record[0] = 0x10102000;
        record[1] = 0x003e0000;
        return (byte*)record;
    }

    // Ghidra: les huit banques de 0x40 octets que le chargement d'un membre restaure depuis
    // l'enregistrement de sauvegarde (+0x1d00..+0x1efc) — les mêmes que FinalizeCharacterStats
    // remet à zéro à la création. Toutes dans l'arène 0x5e9000, donc de simples alias.
    private static byte* s_Bank_005e96e0 => Mem.Arena(0x005e96e0);
    private static byte* s_Bank_005e9720 => Mem.Arena(0x005e9720);
    private static byte* s_Bank_005e9780 => Mem.Arena(0x005e9780);
    private static byte* s_Bank_005e9840 => Mem.Arena(0x005e9840);
    private static byte* s_Bank_005e9980 => Mem.Arena(0x005e9980);
    private static byte* s_Bank_005e92c0 => Mem.Arena(0x005e92c0);
    private static byte* s_Bank_005e9360 => Mem.Arena(0x005e9360);
    private static byte* s_EquipPairs_005e9320 => Mem.Arena(0x005e9320);

    /// <summary>
    /// Ghidra: <c>FUN_00547960(dest, &amp;DAT_005630c4, valeur)</c> — le <c>sprintf</c> de la CRT,
    /// appelé avec le format <c>"%d"</c> et un seul argument entier. Écrit ici en formatage décimal
    /// direct plutôt qu'en transcrivant un moteur variadique : c'est la décision déjà consignée
    /// dans <c>Documentation/DataSymbols.md</c> pour ce site, qui est le seul.
    /// </summary>
    private static void FormatDecimal(byte* destination, int value)
    {
        byte* cursor = destination;
        if (value < 0) { *cursor++ = (byte)'-'; value = -value; }

        byte* digits = stackalloc byte[12];
        int count = 0;
        do { digits[count++] = (byte)('0' + value % 10); value /= 10; } while (value != 0);
        while (count != 0) *cursor++ = digits[--count];
        *cursor = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00536ea0 @ 0x00536ea0 — l'écran de constitution du groupe. Trois temps :
    /// l'énumération des sauvegardes et la composition d'une ligne de texte par fichier ; une boucle
    /// d'image avec une petite machine à sous-phases (navigation, deux boîtes de dialogue de
    /// confirmation) ; puis, à la sortie, le rechargement effectif des membres retenus.
    ///
    /// <para><b>Aucune sauvegarde du tout</b> : la fonction pose <see cref="s_GameStateId"/> à 4 et
    /// revient aussitôt — l'écran de sélection des personnages. C'est le parcours « premier
    /// lancement » du jeu, et il explique pourquoi « Début du jeu » mène directement à la création
    /// tant que le dossier <c>save\</c> est vide.</para>
    /// </summary>
    private static void RunPartyConstitution()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);              // Ghidra: FUN_0052a1e0
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);          // Ghidra: FUN_0052a280

        s_PartySize_005e9020 = 0;
        byte* scratch = GetMainMemPtr() + 0xf0000;

        PlayMusicTrack(0x25);          // Ghidra: FUN_00529ad0(0x25)
        JoystickResetState(false);     // Ghidra: FUN_00504b10(0)

        // Ghidra: `FUN_005126c0(1)`. L'argument est poussé par l'appelant mais **jamais lu** par la
        // fonction (vérifié dans Legacy/Raw/Blaze/FUN_005126c0.asm : elle attaque directement sur
        // `MOV EAX,[0x0056315c]`, sans toucher à la pile d'arguments) — d'où la signature sans
        // paramètre côté port.
        LoadSaveDirectory();           // Ghidra: FUN_005126c0

        // --- Une ligne de texte par sauvegarde : nom, classe, niveau -----------------------
        int entryCount = s_CardEntryCount_00604981;
        BuildSaveDirectoryRows(scratch, entryCount);

        // Ghidra: 0x19 dwords à zéro depuis &DAT_005e85c0 — l'ordre de sélection, remis à neuf.
        NativeMemory.Clear(s_PartySelectionOrder_005e85c0, 0x64);

        if (entryCount == 0)
        {
            // Aucune sauvegarde : on bascule vers l'écran de sélection des personnages.
            s_GameStateId = 4;
            return;
        }

        BindMenuLabels(Mem.s_Unknown_005606f8, 0, Mem.s_LoadScreenDescriptors_0056b558, 0);
        BindMenuLabels(LabelAt(0x34), 0, Mem.s_LoadScreenDescriptor2_0056be20, 0);

        int cursorRow = 0;         // Ghidra: iVar12 — la ligne sous le curseur
        int windowTop = 0;         // Ghidra: local_34 — la première ligne affichée
        int windowOffset = 0;      // Ghidra: local_24 — windowTop * 0x16
        int cursorOffset = 0;      // Ghidra: local_30 — cursorRow * 0x16
        int partyCount = 0;        // Ghidra: iVar11
        int pendingPartyCount = 0; // Ghidra: local_28 — repris dans partyCount en fin d'image
        int phase = 0;             // Ghidra: local_38

        // Le même bloc de sept locales contiguës que RunDebugPartySelect — voir sa note.
        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;

        while (true)
        {
            s_Unknown_005de15c = 1;

            // Ghidra: la marge d'affichage, comme en tête de RunMainMenuFrameLoop mais avec une
            // largeur figée à 0xa0 dès que l'option « Small » est active en grande taille.
            if (s_SmallTextureFlag == 0 && s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de158 = 0;
                s_Unknown_005de154 = 0;
            }
            else
            {
                s_Unknown_005de154 = 0xa0;
                s_Unknown_005de158 = (0xb - s_SizeSetting_005de160) * 0xc;
            }

            byte* pad = CurrentPadState();

            if (s_GameStateId != 1)
            {
                // --- La sortie : recharger les membres retenus -----------------------------
                s_Unknown_005de15c = 0;
                s_PartySize_005e9020 = 0;

                byte* member = GetMainMemPtr() + 0xf0000;
                while (s_PartySize_005e9020 < partyCount)
                {
                    // L'entrée dont l'ordre de sélection vaut la place courante + 1. Si aucune ne
                    // correspond, l'original retombe sur l'entrée 0 — transcrit tel quel.
                    int index = 0;
                    while (index < entryCount
                        && s_PartySelectionOrder_005e85c0[index] != s_PartySize_005e9020 + 1) index++;
                    if (index == entryCount) index = 0;

                    SetCardFileName(s_CardDirectoryEntries_006049bc + index * 0x28);
                    SetCardPathDeviceFields();

                    *(uint*)(member + 0x1ffc) = 0;
                    IntPtr handle = RetryOpenCardFile(s_CardPathDescriptor_00604940, 1);
                    RetrySeekAndReadCardFile(handle, member, 0, 0x2000);
                    RetryCloseCardFile(handle);

                    // La même somme de contrôle signée sur 0x1ffc octets que WriteCardSaveFile
                    // calcule à l'écriture.
                    int checksum = 0;
                    for (int i = 0; i < 0x1ffc; i++) checksum += (sbyte)member[i];

                    if (*(int*)(member + 0x1ffc) == checksum)
                    {
                        member[0x2bc] = 0;
                        member[0x2be] = 0;
                        member[0x2bf] = 0;

                        // Seul le premier membre restaure l'état partagé du groupe (inventaire,
                        // équipement, drapeaux) : les suivants n'apportent que leur enregistrement.
                        if (s_PartySize_005e9020 == 0) RestorePartyGlobals(member);
                    }

                    s_PartySize_005e9020++;
                    member += 0x2000;
                }

                RefreshPartyAbilityMask();       // Ghidra: FUN_00535710
                LoadPartyMemberResources();      // Ghidra: FUN_00536ba0

                if ((int)((nint)GetMainMemPtr() - (nint)s_Unknown_005de738) + 0x138000 >= 0)
                {
                    // Le même bloc de fin que RunDebugPartySelect — voir sa note sur 0x80010000.
                    byte* slotFlags = s_SlotFlags_005e93c0;
                    for (int i = 0; i < 4; i++, slotFlags += 0x9c)
                    {
                        *(uint*)slotFlags = i == 0
                            ? 0x80000000u
                            : (s_PartySize_005e9020 <= i ? 0u : 0x80010000u);
                    }

                    s_Unknown_00566c90 = 1;
                    s_Unknown_00566c94 = 1;
                    s_Unknown_00566c84 = 0xff;
                    s_Unknown_00566c88 = 0xff;
                    s_Unknown_00566c8c = 0xff;
                    s_Unknown_005de53c = 0;
                    return;
                }

                // Débordement : comme dans RunDebugPartySelect, l'original reste ici pour toujours.
                while (true)
                {
                    *(short*)(d + 8) = 0x38;
                    *(short*)(d + 0xa) = 0x20;
                    *(byte**)d = (byte*)s_GlyphAtlas_00566c28;
                    *(uint*)(d + 4) = 0x1000000;
                    primCursor = DrawTextRun(otBase, primCursor, draw, s_BufferOverLabel_0056c950, 0, 0, -1);
                    FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
                }
            }

            // --- La machine à sous-phases ------------------------------------------------
            byte* pendingLabel = null;   // Ghidra: puVar15, lié en fin de case (LAB_00537335)

            switch (phase)
            {
                case 0:
                {
                    // Ghidra: le mot de répétition automatique (pad+6), pas les fronts.
                    ushort repeat = *(ushort*)(pad + 6);
                    int nextRow = -1, nextOffset = 0;

                    if ((repeat & 0x40) != 0)
                    {
                        if (cursorRow < entryCount - 1)
                        {
                            nextRow = cursorRow + 1;
                            nextOffset = cursorOffset + PartyRowStride;
                            if (windowTop + 5 < nextRow)
                            {
                                windowTop = cursorRow - 4;
                                windowOffset = cursorOffset - 4 * PartyRowStride;
                            }
                        }
                    }
                    else if ((repeat & 0x10) != 0 && cursorOffset > 0)
                    {
                        nextRow = cursorRow - 1;
                        nextOffset = cursorOffset - PartyRowStride;
                        if (nextRow < windowTop) { windowTop = nextRow; windowOffset = nextOffset; }
                    }

                    if (nextRow >= 0)
                    {
                        cursorOffset = nextOffset;
                        PlayUiSound3A();          // Ghidra: FUN_0053db60
                        cursorRow = nextRow;
                    }

                    // START, avec au moins un membre retenu : passer à la confirmation.
                    if ((pad[2] & 8) != 0 && partyCount > 0)
                    {
                        PlayUiSound38();          // Ghidra: FUN_0053db80
                        phase = 4;
                    }

                    // CARRÉ : retenir ou retirer la ligne courante.
                    if ((pad[3] & 0x80) != 0)
                    {
                        if (s_PartySelectionOrder_005e85c0[cursorRow] == 0)
                        {
                            if (partyCount < 4)
                            {
                                partyCount++;
                                s_PartySelectionOrder_005e85c0[cursorRow] = (byte)partyCount;
                                pendingPartyCount = partyCount;
                                PlayUiSound38();
                                if (partyCount == 4) phase = 4;
                            }
                            else
                            {
                                PlayUiSound3B();  // Ghidra: FUN_0053dbc0 — le refus
                            }
                        }
                        else
                        {
                            // Retirer resserre les rangs : toute place supérieure recule d'un cran.
                            byte removed = s_PartySelectionOrder_005e85c0[cursorRow];
                            for (int i = 0; i < entryCount; i++)
                                if (removed < s_PartySelectionOrder_005e85c0[i])
                                    s_PartySelectionOrder_005e85c0[i]--;
                            pendingPartyCount = partyCount - 1;
                            s_PartySelectionOrder_005e85c0[cursorRow] = 0;
                            PlayUiSound38();
                        }
                    }

                    // CROIX : demander à quitter.
                    if ((pad[3] & 0x40) != 0) phase = 1;
                    break;
                }

                case 1:
                    if ((pad[3] & 0x80) != 0) phase = 2;
                    pendingLabel = LabelAt(0x38);
                    break;

                case 2:
                case 5:
                    if ((pad[3] & 0x40) != 0)
                    {
                        // CROIX pendant la boîte de dialogue : on annule et on revient à la liste.
                        Mem.s_Unknown_0056b593 = 0;
                        Mem.s_Unknown_0056be1f = (byte)(Mem.s_Unknown_0056be1f & 0xfb);
                        PlayUiSound39();          // Ghidra: FUN_0053dba0
                        phase = 0;
                        pendingLabel = Mem.s_Unknown_005606f8;
                        break;
                    }

                    if ((Mem.s_Unknown_0056be1f & 4) == 0)
                    {
                        if (Mem.s_Unknown_005e909f != 0)
                        {
                            phase = 0;
                            pendingLabel = Mem.s_Unknown_005606f8;
                            break;
                        }
                        // La phase 2 est la sortie vers le menu principal, la 5 le lancement de la
                        // partie. Le `return` de la première est celui de l'original : elle ne
                        // repasse pas par le bloc de rechargement.
                        if (phase == 2) { s_GameStateId = 0; return; }
                        s_GameStateId = 3;
                        break;
                    }
                    pendingLabel = LabelAt(0);
                    break;

                case 3:
                    if ((pad[3] & 0xc0) != 0) { s_GameStateId = 0; return; }
                    break;

                case 4:
                {
                    // Ici l'original lit les **fronts** (pad+2), pas la répétition.
                    ushort pressed = *(ushort*)(pad + 2);
                    if ((pressed & 0x4000) != 0)
                    {
                        Mem.s_Unknown_0056b58b = 0;
                        Mem.s_Unknown_0056be1f = (byte)(Mem.s_Unknown_0056be1f & 0xfd);
                        PlayUiSound39();
                        phase = 0;
                        pendingLabel = Mem.s_Unknown_005606f8;
                        break;
                    }
                    if ((pressed & 0x8000) != 0)
                    {
                        Mem.s_Unknown_0056b58b = 0;
                        Mem.s_Unknown_0056be1f = (byte)(Mem.s_Unknown_0056be1f & 0xfd);
                        phase = 5;
                        *(ushort*)(pad + 2) = (ushort)(pressed & 0x7fff);
                        pendingLabel = LabelAt(0);
                        break;
                    }
                    pendingLabel = LabelAt(0x30);
                    break;
                }
            }

            if (pendingLabel != null)
                BindMenuLabels(pendingLabel, 0, Mem.s_LoadScreenDescriptors_0056b558, 0);

            // --- Les six lignes visibles --------------------------------------------------
            int windowEnd = windowTop + 6;
            if (entryCount < windowEnd) windowEnd = entryCount;

            *(short*)(d + 0x10) = 0;
            *(byte**)d = s_RowSpriteRecord_00563148;

            for (int i = windowTop, offset = windowOffset; i < windowEnd; i++, offset += PartyRowStride)
            {
                byte* row = s_PartyRowText_005e8640 + offset;
                bool chosen = s_PartySelectionOrder_005e85c0[i] != 0;

                EmitViewportRect(otBase, &primCursor);          // Ghidra: FUN_0051be20

                // La fenêtre de texture de la ligne : l'index parcourt une grille de 16 colonnes,
                // et les deux octets de tête de l'enregistrement de texture la déplacent.
                int bank = i >> 4;
                *(short*)(d + 0x10) = 0;
                s_RowSpriteRecord_00563148[0] = (byte)(i << 4);
                s_RowSpriteRecord_00563148[1] = (byte)(bank * 0x40 + 0x20);
                *(byte**)d = s_RowSpriteRecord_00563148;
                *(uint*)(d + 4) = 0x1000000;
                *(short*)(d + 8) = 0x48;
                *(short*)(d + 0xa) = (short)((i - windowTop) * 0x14 + 0x32);

                // L'entrée 0x3e de la table de pages, celle que le second dword de
                // l'enregistrement désigne — écrite juste avant le tracé, comme EmitCardGlyphTile.
                *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xf8) = 0xf;
                *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfa) =
                    (ushort)(((i << 4) >> 4) & 0xf | (bank + 0x1fe) * 0x40);

                primCursor = DrawSpriteVariant(otBase, primCursor, draw, chosen ? 0u : 3u, 0);
                primCursor = DrawFlatPrimitive(otBase, primCursor, draw);
                EmitClipRect(otBase, &primCursor);              // Ghidra: FUN_0051bdb0

                // Le texte de la ligne, décalé de 0x1e à droite et de 4 vers le bas. La teinte
                // Ghidra `&LAB_00404040 + (-(sel != 0) & 0xbfbfc0)` vaut 0x404040 (gris éteint)
                // pour une ligne non retenue et 0x1000000 (pas de teinte) pour une retenue.
                *(short*)(d + 8) += 0x1e;
                *(short*)(d + 0xa) += 4;
                *(uint*)(d + 4) = chosen ? 0x1000000u : 0x404040u;
                *(byte**)d = (byte*)s_GlyphAtlas_00566c28;
                primCursor = DrawTextRun(otBase, primCursor, draw, row, 0, 0, PartyRowStride);

                if (cursorRow == i)
                {
                    EmitViewportRect(otBase, &primCursor);
                    *(uint**)d = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                    *(short*)(d + 0xa) -= 2;
                    *(short*)(d + 0x10) = 0x18;
                    *(uint*)(d + 4) = 0x1000000;
                    *(short*)(d + 8) = 0x34;
                    primCursor = DrawSpriteVariant(otBase, primCursor, draw, 0, 0);
                    primCursor = DrawFlatPrimitive(otBase, primCursor, draw);
                    EmitClipRect(otBase, &primCursor);
                }
            }

            // Les deux descripteurs d'interface de l'écran, l'un après l'autre.
            for (int i = 0; i < 2; i++)
            {
                byte* panel = Mem.s_LoadScreenDescriptors_0056b558 + i * GameMemory.UiDescriptorStride;
                primCursor = Ui.RunTextEngine(primCursor, panel);            // Ghidra: FUN_005443a0
                primCursor = DrawUiPanel(otBase, primCursor, panel);      // Ghidra: FUN_00545340
            }

            // Le fond, comme partout ailleurs : la première entrée de la table +0x94.
            s_Unknown_005de15c = 0;
            *(short*)(d + 0x10) = 0;
            *(short*)(d + 8) = 0xa0;
            *(short*)(d + 0xa) = 0x78;
            *(short*)(d + 0xc) = 0x1420;
            *(short*)(d + 0xe) = 0x1149;
            *(uint**)d = **(uint***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            *(uint*)(d + 4) = 0x1000000;
            primCursor = DrawSpritePrimitive(otEntry, primCursor, draw, 0, 0);   // Ghidra: FUN_00521f20
            primCursor = DrawFlatPrimitive(otEntry, primCursor, draw);

            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
            partyCount = pendingPartyCount;
        }
    }

    /// <summary>
    /// Ghidra: le bloc <c>DAT_005e9020 == 0</c> de FUN_00536ea0 — l'état **partagé** du groupe,
    /// restauré depuis le seul enregistrement du premier membre : huit banques de 0x40 octets
    /// prises à <c>+0x1d00..+0x1efc</c>, trois octets de compteurs à <c>+0x1f01..+0x1f03</c>, et
    /// huit paires de mots à <c>+0x1f04</c>. Exactement les zones que
    /// <see cref="FinalizeCharacterStats"/> remet à zéro à la création — les deux fonctions sont les
    /// deux moitiés du même format.
    ///
    /// <para>L'octet <c>+0x1f00</c> décide : à zéro, l'état est présent et recopié ; sinon tout est
    /// remis à l'état neuf (banques à zéro, paires à <c>0 / -1</c>).</para>
    /// </summary>
    private static void RestorePartyGlobals(byte* record)
    {
        byte*[] banks =
        [
            s_Bank_005e96e0, s_Bank_005e9780, Mem.s_PanelSelection_005e9060, s_Bank_005e9840,
            s_Bank_005e9720, s_Bank_005e92c0, s_Bank_005e9980, s_Bank_005e9360,
        ];

        if (record[0x1f00] == 0)
        {
            s_Unknown_005de528 = record[0x1f01];
            s_Unknown_005de52c = record[0x1f02];
            s_Unknown_005de530 = record[0x1f03];

            for (int bank = 0; bank < banks.Length; bank++)
                for (int i = 0; i < 0x40; i += 4)
                    *(uint*)(banks[bank] + i) = *(uint*)(record + 0x1d00 + bank * 0x40 + i);

            for (int i = 0; i < 8; i++)
            {
                byte* destination = s_EquipPairs_005e9320 + i * 8;
                byte* source = record + 0x1f04 + i * 8;
                *(ushort*)destination = *(ushort*)source;
                *(ushort*)(destination + 2) = *(ushort*)(source + 2);
                *(ushort*)(destination + 4) = *(ushort*)(source + 4);
            }
        }
        else
        {
            s_Unknown_005de528 = 0;
            s_Unknown_005de52c = 0;
            s_Unknown_005de530 = 0;

            foreach (byte* bank in banks) NativeMemory.Clear(bank, 0x40);

            for (int i = 0; i < 8; i++)
            {
                *(uint*)(s_EquipPairs_005e9320 + i * 8) = 0;
                *(uint*)(s_EquipPairs_005e9320 + i * 8 + 4) = 0xffffffff;
            }
        }

        s_Unknown_00566c84 = 0xff;
        s_Unknown_00566c88 = 0xff;
    }

    private static int s_Unknown_005de7e8, s_Unknown_005de7ec, s_Unknown_005de7f0, s_Unknown_005de7f4,
                        s_Unknown_005de7f8, s_Unknown_005de7fc, s_Unknown_0056c778, s_Unknown_005de800;
    // Les huit octets que ce bloc remet à zéro ne sont PAS des globales isolées : ce sont quatre
    // champs de chacun des deux descripteurs de l'écran de chargement — voir
    // s_LoadScreenDescriptors_0056b558 pour la démonstration par les adresses.
}
