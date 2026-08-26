// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Blaze porte les aides que cette classe appelle encore : GetMainMemPtr.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// Le moteur de texte de l'interface (Ghidra: FUN_005443a0 et son sous-arbre).
///
/// <para>
/// <b>Premiere vraie classe d'instance de la transcription</b>, convertie le 2026-08-19. Sa
/// signature declare ce qu'elle touche : <c>GameMemory</c>, et rien d'autre. Avant, elle lisait
/// huit globales <c>.data</c> appartenant a cinq autres fichiers, sans que rien ne le dise.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>. Il n'y a qu'un moteur de texte dans le jeu :
/// une classe d'instance modelise sa dependance, pas une multiplicite qui n'existe pas.
/// </para>
/// </summary>
internal sealed unsafe class UiText
{
    private readonly GameMemory mem;

    /// <summary>
    /// Les trois tables qui se construisent par appel sont montees ici, et non en initialiseur de
    /// champ : un initialiseur ne peut pas lire un membre d'instance. En faire des proprietes
    /// <c>=&gt;</c> les reconstruirait a chaque acces - un changement de comportement, pas de forme.
    /// </summary>
    internal UiText(GameMemory memory)
    {
        mem = memory;
        s_GlyphWidths_0056e7b0 = (byte[])s_GlyphWidthsProportional.Clone();
        s_ScanHandlers   = BuildTextTable(false);
        s_RenderHandlers = BuildTextTable(true);
    }
   // =====================================================================
    // The UI text engine (Ghidra: FUN_005443a0 and its subtree)
    //
    // Shared by the whole game, not just the menu: FUN_005443a0 has 49 call sites. It runs one
    // byte stream **twice** over a descriptor, dispatching every byte below 0x20 through a
    // different 32-entry table per pass:
    //
    //   pass 1  scan    PTR_LAB_0056e870   measures and skips; collects '$'-escapes, draws nothing
    //   pass 2  render  PTR_FUN_0056e728   emits glyphs
    //
    // Both tables are recovered verbatim in Documentation/Reference/DataSymbols.md, along with the five
    // handler bodies Ghidra never emitted (they are reached only through these tables) which were
    // decoded by hand from Legacy/Binaries/Blaze.exe.
    //
    // What it produces is **not** ordinary primitives. Glyphs are rasterised into a staging buffer
    // (DAT_005e0300, 4 bits per pixel), and each finished line is enqueued as a VRAM upload node
    // onto the same list FlushDeferredImageUploads drains. The cursor threaded in and out of
    // FUN_005443a0 is that node cursor.
    //
    // Descriptor layout, from the offsets these functions touch:
    //   +0x00  ushort  tpage/clut slot, indexes DAT_005e8f00
    //   +0x0a  short   line height budget
    //   +0x0c  uint    packed line state; byte +0x0d is the line index
    //   +0x0e  sbyte   pen X accumulator
    //   +0x10  short   glyph count; byte +0x11 is the line index again
    //   +0x12  byte    line width, in units of 8 pixels
    //   +0x14  byte    staging bank
    //   +0x15  sbyte   nested-text stack depth
    //   +0x68  16-entry ordering table (ClearOTagR'd by control code 0x0c)
    //   +0xa4  ordering-table cursor base
    //   +0x828 sprite primitive base
    //   +0x8ac text cursor      +0x8a8 completion callback     +0x8b0 callback user data
    //   +0x8b4 nested-text return stack
    //   +0x8c6 byte    frame countdown       +0x8c7 byte  flags
    // =====================================================================

    // Ghidra: &DAT_005e0300 â€” the 4bpp glyph staging arena, addressed as (bank + line) * 0x800.
    // The next named global is DAT_005e8300, which puts its size at 0x8000 â€” sixteen banks.
    private readonly byte* s_GlyphStaging_005e0300 = (byte*)NativeMemory.AllocZeroed(0x8000);

    /// <summary>
    /// The 4bpp glyph staging bank for one line, bounds-checked. The buffer holds 16 banks of
    /// 0x800 bytes. Its extent in the original is inferred from where the next named global sits,
    /// so an index past it is a defect in this reconstruction rather than original behaviour â€” and
    /// has to say so rather than quietly scribble over another allocation.
    /// </summary>
    private byte* StagingBank(uint bank, uint line)
    {
        uint index = bank + line;
        if (index >= 0x10)
        {
            throw new InvalidOperationException(
                $"UI text staging bank {index} (bank {bank} + line {line}) is past the 16-bank " +
                "buffer at DAT_005e0300. The line counter is a byte, so a descriptor laying out " +
                "more than 16 lines would corrupt unrelated memory here.");
        }
        return s_GlyphStaging_005e0300 + index * 0x800;
    }

    /// <summary>
    /// One tpage/clut slot of the table at DAT_005e8f00, bounds-checked. A descriptor carries its
    /// slot as a full ushort, so a wrong or unpopulated descriptor can index far past the table.
    /// </summary>
    internal uint* DecodeSlot(uint index)
    {
        if (index >= 0x400)
        {
            throw new InvalidOperationException(
                $"tpage/clut slot {index} is past the 1024-entry table at DAT_005e8f00 â€” the " +
                "descriptor asking for it is not the one this table was filled for.");
        }
        return (uint*)(mem.s_EntityDecodeTable_005e8f00 + index * 4);
    }

    private byte* s_StagingWrite_005e8304;   // Ghidra: DAT_005e8304 â€” staging write pointer
    private int s_TextState_005e8308;        // Ghidra: DAT_005e8308 â€” 0 / 1 / 2 / -1, see below
    private byte* s_TrimAnchor_005e830c;     // Ghidra: DAT_005e830c â€” leading-space trim anchor
    private uint* s_OtCursor_005e8314;       // Ghidra: DAT_005e8314 â€” OT cursor in the descriptor
    private int s_LineIndex_005e8318;        // Ghidra: DAT_005e8318 â€” lines emitted this page
    private uint* s_UploadCursor_005e831c;   // Ghidra: DAT_005e831c â€” the node cursor, in and out
    private uint s_PenX_005e8320;            // Ghidra: DAT_005e8320 â€” pen X, in 4bpp columns
    private uint* s_SpriteCursor_005e8324;   // Ghidra: DAT_005e8324 â€” sprite cursor in the descriptor
    private uint s_BreakPen_005e8328;        // Ghidra: DAT_005e8328 â€” pen saved at the last break
    private uint s_BreakColumn_005e8300;     // Ghidra: _DAT_005e8300 â€” column saved at the last break
    private byte* s_BreakStaging_005e02fc;   // Ghidra: DAT_005e02fc â€” staging ptr at the last break
    private int s_GlyphMode_005dffd0;        // Ghidra: DAT_005dffd0 â€” 0 none / 1 latin / 2 DBCS
    private int s_Unknown_005dffe8;          // Ghidra: DAT_005dffe8

    /// <summary>
    /// Ghidra: &amp;DAT_0056e7b0 â€” proportional glyph widths, one byte per folded slot.
    /// <para>
    /// **Taken from `FUN_00507890`, not from the `.data` image.** The two differ, and the `.data`
    /// copy is the stale one: `FUN_00507890` builds the real table as 186 stack literals and copies
    /// it over (`FUN_00507870` is its fixed-width counterpart, every entry 8). The `.data` copy
    /// gives `I`=6/`J`=2 and `M`=5/`N`=8 â€” impossible for a proportional font, and the direct cause
    /// of the uneven spacing: narrow glyphs advanced too far, wide ones not far enough. This table
    /// gives `I`=2, `M`=8, `i`=2, `l`=2, `m`=8, `t`=4, `Q`=8.
    /// </para>
    /// The folding that indexes it is confirmed against the machine code at 0x00543EE3:
    /// `CMP SI,0x41 / LEA EAX,[ESI-0x22] / JAE / LEA EAX,[ESI-0x21]`, then
    /// `MOVSX AX,[ESI + 0x0056E7B0]` â€” so both the folding and the base are faithful as written.
    /// <para>
    /// TODO(REVERSE): neither setter has a call site in `Blaze.c` â€” both are reached only through
    /// an address-taken table, so which one runs when is not established. The proportional one is
    /// installed here because the original demonstrably renders proportional text.
    /// </para>
    /// <para>
    /// **Both setters now have a call site**, which settles the TODO above: the name-entry
    /// sub-state (<c>FUN_00507cb0</c>) calls <c>FUN_00507870</c> before laying out its
    /// eight-row character grid and <c>FUN_00507890</c> immediately after. The grid needs every
    /// cell the same width to line up, so the screen switches to the fixed-width table for the
    /// grid and back to the proportional one for everything else. The table is therefore mutable,
    /// not a constant.
    /// </para>
    /// </summary>
    internal readonly byte[] s_GlyphWidthsProportional =
    [
        0x02,0x04,0x08,0x06,0x0a,0x08,0x02,0x04,0x04,0x06,0x06,0x02,0x04,0x02,0x04,0x06,
        0x04,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x02,0x02,0x04,0x06,0x04,0x06,0x06,
        0x06,0x06,0x06,0x05,0x05,0x07,0x06,0x02,0x06,0x07,0x05,0x08,0x07,0x07,0x06,0x08,
        0x06,0x06,0x06,0x06,0x07,0x08,0x06,0x06,0x07,0x03,0x04,0x03,0x06,0x08,0x05,0x05,
        0x05,0x05,0x05,0x04,0x05,0x05,0x02,0x03,0x06,0x02,0x08,0x05,0x06,0x05,0x05,0x04,
        0x06,0x04,0x05,0x06,0x06,0x06,0x06,0x06,0x04,0x02,0x04,0x07,0x07,0x02,0x06,0x06,
        0x06,0x06,0x02,0x07,0x04,0x0a,0x06,0x06,0x0a,0x04,0x0a,0x08,0x04,0x06,0x05,0x05,
        0x03,0x06,0x07,0x02,0x04,0x03,0x06,0x06,0x08,0x0a,0x0a,0x06,0x06,0x06,0x06,0x06,
        0x06,0x0a,0x07,0x05,0x05,0x06,0x06,0x03,0x03,0x04,0x04,0x08,0x07,0x08,0x08,0x08,
        0x08,0x08,0x06,0x07,0x06,0x06,0x06,0x06,0x06,0x07,0x06,0x06,0x06,0x06,0x05,0x06,
        0x08,0x05,0x05,0x05,0x06,0x06,0x03,0x03,0x04,0x04,0x07,0x05,0x06,0x06,0x06,0x06,
        0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,0x06,
    ];

    // La table réellement consultée. Déclarée **après** la table proportionnelle : les
    // initialiseurs de champs statiques s'exécutent dans l'ordre du texte, et l'inverse donnerait
    // un Clone() sur null au constructeur de type.
    internal readonly byte[] s_GlyphWidths_0056e7b0;

    /// <summary>Ghidra: FUN_00507870 @ 0x00507870 — installe la table fixe, toutes les entrées à 8.</summary>
    internal void UseFixedGlyphWidths()
    {
        for (int i = 0; i < s_GlyphWidths_0056e7b0.Length; i++) s_GlyphWidths_0056e7b0[i] = 8;
    }

    /// <summary>Ghidra: FUN_00507890 @ 0x00507890 — restaure la table proportionnelle.</summary>
    internal void UseProportionalGlyphWidths()
        => s_GlyphWidthsProportional.CopyTo(s_GlyphWidths_0056e7b0, 0);

    /// <summary>
    /// Ghidra: FUN_0051c0e0 @ 0x0051c0e0 â€” advances a cheat-code matcher over every character the
    /// text engine draws, against the string at <c>DAT_00564a40</c>.
    /// </summary>
    internal void CheatCodeProbe(byte ch)
    {
        // 2026-08-17 : les deux inconnues de cette fonction sont résolues â€” la séquence est
        // "Gurkha" (voir s_CheatSequence_00564a40) et le verrou est
        // <see cref="s_DebugModeLatch_005633a8"/>, dont la valeur .data au chargement est 1.
        //
        // **Pas de `return` dans cette première branche** : l'original enchaîne sur la comparaison
        // qui suit (Blaze.c 66791-66799 â€” le `if` n'a pas de `return`), si bien qu'après avoir
        // atteint le terminateur il recompare immédiatement le caractère courant à la première
        // lettre. Le `return` que cette reconstruction avait ajouté empêchait toute reprise du
        // filage sur le caractère qui déclenche la remise à zéro.
        if (s_CheatSequence_00564a40[s_CheatIndex_005de164] == 0)
        {
            mem.s_DebugModeLatch_005633a8 = 0;
            s_CheatIndex_005de164 = 0;
        }

        if (ch == s_CheatSequence_00564a40[s_CheatIndex_005de164])
        {
            s_CheatIndex_005de164++;
            return;
        }

        s_CheatIndex_005de164 = 0;
    }

    // Ghidra: &DAT_00564a40 â€” relevé dans le binaire (`at Legacy/Binaries/Blaze.exe 0x00564a40`, .data,
    // décalage fichier 0x163040) : les octets sont `47 75 72 6b 68 61 00 00`, soit **"Gurkha"**.
    // Il est logé juste après les trois étiquettes de mise au point "DPT"/"LOC"/"ANG" (0x564a20),
    // ce qui conforte la lecture. La séquence n'est PAS comparée à une saisie clavier/manette :
    // CheatCodeProbe est appelée sur chaque caractère que DrawTextRun **dessine**, donc le code
    // s'arme quand le jeu affiche ce texte quelque part â€” typiquement un personnage nommé
    // « Gurkha » sur l'écran de saisie du nom.
    private readonly byte[] s_CheatSequence_00564a40 = "Gurkha\0"u8.ToArray();
    private int s_CheatIndex_005de164;   // Ghidra: DAT_005de164

    /// <summary>
    /// Ghidra: FUN_00543980 @ 0x00543980 â€” enqueues the finished line's staging bank as a VRAM
    /// upload node, onto the same list <see cref="FlushDeferredImageUploads"/> drains.
    /// </summary>
    private void EnqueueGlyphUpload(byte* descriptor)
    {
        int clut = (int)*DecodeSlot(*(ushort*)descriptor);
        uint packed = *(uint*)(descriptor + 0x0c);
        ushort bank = *(ushort*)(descriptor + 0x14);

        uint* node = s_UploadCursor_005e831c;
        node[1] = (uint)(nuint)StagingBank((byte)bank, (packed >> 8) & 0xff);
        node[3] = 0x100040;
        node[2] = (uint)((byte)bank * 0x100000 +
                         ((((packed >> 4) & 0xf0) + (uint)((((clut + 1) & 0x10) + 2) * 0x10)) * 0x10000
                          | (uint)((clut + 1) & 0xf) << 6));

        node[0] = (uint)(nuint)mem.s_Unknown_005e8310;
        mem.s_Unknown_005e8310 = (int)(nuint)node;
        s_UploadCursor_005e831c = node + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00543a10 @ 0x00543a10 â€” links the finished line's textured sprite into the
    /// descriptor's own 16-entry ordering table.
    /// </summary>
    internal void EmitTextSprite(byte* descriptor)
    {
        ushort bank = *(ushort*)(descriptor + 0x14);
        uint packed = *(uint*)(descriptor + 0x0c);
        int clut = (int)*DecodeSlot(*(ushort*)descriptor);
        uint rect = *(uint*)(descriptor + 0x20);

        uint* prim = s_SpriteCursor_005e8324;
        prim[2] = *(uint*)(descriptor + 0x24) | 0x64000000;
        prim[1] = (rect >> 0x10) & 0x60 | (uint)(clut + 1) & 0xffff | 0xe1000000;
        prim[3] = (((rect >> 8) & 0xff) + (uint)(ushort)((sbyte)((packed >> 8) & 0xf) * 0x0c)) * 0x10000
                  | ((rect & 0xff) + (packed & 0xff));
        prim[4] = rect & 0xf0000 | (uint)(clut + 1) & 0xffff0000
                  | ((((uint)(byte)bank * 0x100 + packed) & 0xf00) * 0x10 + 0x2000 + (packed & 0xff)) & 0xffff;
        prim[5] = (packed >> 0x10) & 0xff | (packed >> 8) & 0xff0000;

        prim[0] = *s_OtCursor_005e8314 | 0x5000000;
        *s_OtCursor_005e8314 = (uint)(nuint)prim;
        s_SpriteCursor_005e8324 = prim + 6;
    }

    /// <summary>
    /// Ghidra: FUN_00544cb0 @ 0x00544cb0 â€” closes the current line: trims the leading run the
    /// engine had provisionally drawn, then grows the descriptor's height budget by one line.
    /// </summary>
    private void FlushLine(byte* descriptor)
    {
        if (s_TextState_005e8308 == 2)
        {
            uint trim = (((uint)descriptor[0x12] * 8 - s_PenX_005e8320) - 10) >> 1;
            if ((ushort)trim < 0x100)
            {
                uint span = (uint)((int)((trim & 0xffff) + 1) >> 1);
                for (int row = 0xf; row != 0; row--)
                {
                    Win32.CrtMemMove(s_TrimAnchor_005e830c + span, s_TrimAnchor_005e830c,
                               (nuint)((s_PenX_005e8320 + 1) >> 1));

                    byte* fill = s_TrimAnchor_005e830c;
                    for (uint w = span >> 2; w != 0; w--) { *(uint*)fill = 0xffffffff; fill += 4; }
                    for (uint b = span & 3; b != 0; b--) { *fill = 0xff; fill++; }

                    s_TrimAnchor_005e830c += 0x80;   // Ghidra: DAT_005e830c + 0x20 on a uint*
                }

                s_PenX_005e8320 += trim & 0xffff;
                descriptor[0x0e] = (byte)(descriptor[0x0e] + (byte)trim);
            }
        }

        s_LineIndex_005e8318++;
        int height = s_LineIndex_005e8318 * 0x0c + 6;
        if (*(short*)(descriptor + 0x0a) < height && (descriptor[0x8c7] & 2) != 0)
        {
            *(short*)(descriptor + 0x0a) = (short)height;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00544c50 @ 0x00544c50 â€” pass-2 handler for control code <c>0x0c</c>: begins a
    /// page. Clears the descriptor's own 16-entry ordering table and re-bases every cursor.
    /// </summary>
    private byte* Handler_00544c50(byte* text, byte* descriptor)
    {
        Winsys3d.Ordinal_688((uint*)(descriptor + 0x68), 0x10);   // ClearOTagR on the descriptor's own OT
        *(ushort*)(descriptor + 0x10) = 0;
        *(uint*)(descriptor + 0x0c) = 0xf000000;
        s_OtCursor_005e8314 = (uint*)(descriptor + 0xa4);
        s_SpriteCursor_005e8324 = (uint*)(descriptor + 0x828);
        s_PenX_005e8320 = 0;                            // Ghidra: DAT_005e8320 = 0
        s_GlyphMode_005dffd0 = 0;
        s_StagingWrite_005e8304 = StagingBank(descriptor[0x14], 0);
        return text;   // Ghidra returns unaff_ESI â€” the caller's cursor, unchanged
    }

    /// <summary>
    /// Ghidra: FUN_00544d90 @ 0x00544d90 — pass-2 handler for control code <c>0x0d</c>: ends the
    /// line, uploads it, emits its sprite, and re-points every cursor at the next line's slots.
    ///
    /// <para>
    /// <b>Son premier argument EST le drapeau de remise a zero.</b> <c>FUN_00544d90</c> lit
    /// <c>[esp+0Ch]</c> — l'argument 1 — et remet <see cref="s_TextState_005e8308"/> a zero des
    /// qu'il est non nul. Appele depuis la table des codes de controle, cet argument est le
    /// <b>curseur de texte</b>, qui n'est jamais nul : <b>un retour a la ligne explicite remet donc
    /// toujours l'etat a zero</b>.
    /// </para>
    /// <para>
    /// <b>Les deux autres appelants passent zero</b>, verifie au desassembleur : le repli de mot sur
    /// un espace (<c>0x00543c62</c>, <c>push esi ; push 0</c>) et le report de mot partiel
    /// (<c>0x00543d7f</c>). Eux gardent l'etat, ce qui est le comportement voulu — la ligne
    /// reportee est la suite de la precedente, pas une ligne neuve.
    /// </para>
    /// <para>
    /// <b>Ce site-ci passait <c>0</c> jusqu'au 2026-08-26</b>, et c'etait le defaut du texte de la
    /// foret : sans remise a zero, l'etat 2 (« un espace de tete a ete retire ») survivait d'une
    /// ligne a l'autre — <see cref="NewLine"/> le ramene a 1, et le premier glyphe de la ligne
    /// suivante le repousse a 2. <see cref="FlushLine"/> centrait alors <b>toutes</b> les lignes du
    /// bloc au lieu de la seule qui commence par un espace, en decalant les pixels vers la droite
    /// et en laissant des debris de la ligne precedente.
    /// </para>
    /// </summary>
    private byte* Handler_00544d90(byte* text, byte* descriptor)
    {
        // Ghidra: `mov eax,[esp+0Ch] ; cmp eax,ebx ; je` — l'argument 1 sert de drapeau, tel quel.
        NewLine(text != null ? 1 : 0, descriptor);
        return text;
    }

    private void NewLine(int reset, byte* descriptor)
    {
        FlushLine(descriptor);
        if (s_PenX_005e8320 != 0) EnqueueGlyphUpload(descriptor);
        if ((sbyte)descriptor[0x0e] != 0) EmitTextSprite(descriptor);

        uint packed = (*(uint*)(descriptor + 0x0c) + 0x100) & 0xff00ff00;
        *(uint*)(descriptor + 0x0c) = packed;
        *(short*)(descriptor + 0x10) = (short)packed;

        uint line = (packed >> 8) & 0xff;
        s_OtCursor_005e8314 = (uint*)(descriptor + (0x29 - line) * 4);
        s_SpriteCursor_005e8324 = (uint*)(descriptor + (0x828 - line * 0x80));
        s_StagingWrite_005e8304 = StagingBank(descriptor[0x14], line);
        s_PenX_005e8320 = 0;                            // Ghidra: DAT_005e8320 = 0
        s_BreakStaging_005e02fc = null;

        if (reset != 0) s_TextState_005e8308 = 0;
        if (s_TextState_005e8308 == 2) s_TextState_005e8308 = 1;
        s_GlyphMode_005dffd0 = 0;
    }

    /// <summary>
    /// Pass-2 handler for control code <c>0x09</c> â€” end of page / wait. **Decoded by hand from
    /// Legacy/Binaries/Blaze.exe**: Ghidra never emitted this body, since it is reached only through
    /// PTR_FUN_0056e728. Bytes and full listing in Documentation/Reference/DataSymbols.md.
    /// <code>
    /// 8B 4C 24 08  MOV ECX,[ESP+8]     8B 44 24 04  MOV EAX,[ESP+4]
    /// 8A 81 ..     MOV AL,[ECX+0x8C7]  8A 10        MOV DL,[EAX]
    /// C6 41 3B 01  MOV [ECX+0x3B],1    40           INC EAX
    /// 0C 04        OR  AL,4            88 91 ..     MOV [ECX+0x8C5],DL
    /// C6 81 ..     MOV [ECX+0x8C4],0   89 81 ..     MOV [ECX+0x8AC],EAX
    /// 88 81 ..     MOV [ECX+0x8C7],AL  C3           RET
    /// </code>
    /// Raising flag bit 2 is what makes every later <see cref="RunTextEngine"/> call return at
    /// once, so a finished page costs nothing on subsequent frames.
    /// </summary>
    internal byte* Handler_00544b10(byte* text, byte* descriptor)
    {
        byte flags = descriptor[0x8c7];
        descriptor[0x3b] = 1;
        flags |= 4;
        descriptor[0x8c4] = 0;
        descriptor[0x8c7] = flags;

        descriptor[0x8c5] = *text;          // the operand byte â€” '?' for the main menu
        text++;
        *(byte**)(descriptor + 0x8ac) = text;
        return text;
    }

    /// <summary>
    /// Ghidra: FUN_00544fb0 @ 0x00544fb0 â€” handler for control code <c>0x14</c>, render pass only
    /// (the scan pass uses the generic "skip one operand byte" stub, matching the single byte this
    /// consumes). Fully decompiled, unlike most of its neighbours.
    ///
    /// <para>If a sprite run is pending (<c>descriptor[0x0e] != 0</c>), flushes it
    /// (<see cref="EmitTextSprite"/>), folds its width into the pen column at <c>+0x0c</c>, and
    /// snapshots the current break state (<see cref="s_BreakStaging_005e02fc"/>,
    /// <see cref="s_BreakColumn_005e8300"/>, <see cref="s_BreakPen_005e8328"/>) â€” the same
    /// bookkeeping <see cref="FlushLine"/> reads back on a word-wrap. The last of the three reads
    /// <c>descriptor[0x0e]</c> **after** it was just cleared two lines above, so it is always 0;
    /// transcribed as the literal read anyway, matching this project's fidelity rule, rather than
    /// folded to a constant.</para>
    ///
    /// <para>Unconditionally writes one text byte into <c>descriptor[0x22]</c> â€” the upper word of
    /// the same <c>+0x20</c> "rect" dword <see cref="Handler_005449b0"/> (code <c>0x04</c>) writes
    /// the lower word of, and <see cref="EmitTextSprite"/> reads back whole. Bits 0-3 there fold
    /// into the sprite's page term and bits 5-6 into its blend-mode term, so this sets a
    /// page/blend byte for the sprite about to be flushed â€” consistent with this code appearing in
    /// the "Bonus spécial !" text the user's behavioural reference notes as rendered in gold.</para>
    /// </summary>
    private byte* Handler_00544fb0(byte* text, byte* descriptor)
    {
        if (descriptor[0x0e] != 0)
        {
            EmitTextSprite(descriptor);
            byte pending = descriptor[0x0e];
            descriptor[0x0e] = 0;
            descriptor[0x0c] = (byte)(descriptor[0x0c] + pending);
            s_BreakStaging_005e02fc = s_StagingWrite_005e8304;
            s_BreakColumn_005e8300 = s_PenX_005e8320;
            s_BreakPen_005e8328 = descriptor[0x0e];
        }

        descriptor[0x22] = *text;
        return text + 1;
    }

    /// <summary>
    /// Handler for control code <c>0x0f</c>, in **both** passes â€” the table has <c>0x00544e50</c>
    /// at slot 15 on the scan side as well as the render side. **Decoded by hand from
    /// Legacy/Binaries/Blaze.exe** (file offset <c>0x144250</c>, 33 bytes then <c>0x90</c> padding); Ghidra
    /// never emitted it, since it is reached only through the two tables.
    /// <code>
    /// 8B 4C 24 08        MOV ECX,[ESP+8]      ; descriptor
    /// 8A 81 C7 08 00 00  MOV AL,[ECX+0x8C7]   ; flags
    /// C6 41 33 01        MOV byte [ECX+0x33],1
    /// 0C 02              OR  AL,2
    /// 88 81 C7 08 00 00  MOV [ECX+0x8C7],AL
    /// 8B 44 24 04        MOV EAX,[ESP+4]      ; text cursor, returned unchanged
    /// 89 81 AC 08 00 00  MOV [ECX+0x8AC],EAX
    /// C3                 RET
    /// </code>
    /// <para>Sibling of <see cref="Handler_00544b10"/> (code <c>0x09</c>) and it reads best against
    /// it. Three differences: it raises flag bit **1** rather than bit 2, it writes <c>+0x33</c>
    /// rather than <c>+0x3b</c>, and it consumes **no operand byte** â€” the returned cursor is the
    /// one it was handed.</para>
    /// <para><b>What the two byte writes are.</b> Neither is a loose flag: they are the **top byte
    /// of a feature dword** in the descriptor header, i.e. its <c>0x1000000</c> enable bit. The
    /// header is a row of (parameter, enable) pairs â€” see Documentation/Reference/DataSymbols.md â€” with the
    /// cursor's parameter at <c>+0x30</c> and the scroll arrows' at <c>+0x38</c>. So
    /// <c>[ECX+0x33] = 1</c> turns <c>0x000F28D4</c> into <c>0x010F28D4</c>: it **switches the
    /// cursor on**, exactly as code <c>0x09</c> switches the scroll arrows on through
    /// <c>+0x3b</c>.</para>
    /// <para>Flag bit 1 is tested at the top of <see cref="RunTextEngine"/>, which returns at once
    /// when it is set. Code <c>0x0f</c> therefore means <b>stop here and show the cursor</b> â€” a
    /// prompt awaiting a choice, which is what the character-select screen reaches on validating a
    /// character.</para>
    /// </summary>
    private byte* Handler_00544e50(byte* text, byte* descriptor)
    {
        byte flags = descriptor[0x8c7];
        descriptor[0x33] = 1;               // le bit 0x1000000 du dword de curseur, à +0x30
        flags |= 2;
        descriptor[0x8c7] = flags;

        *(byte**)(descriptor + 0x8ac) = text;
        return text;                        // aucun octet d'opérande consommé
    }


    /// <summary>
    /// La racine de ressources designee par un octet d'operande de code de controle.
    ///
    /// <para>Quatre gestionnaires font le meme calcul, que le desassemblage rend litteralement :
    /// <c>mov eax,[ecx*4+5E97C0h]</c> sur un octet non borne (voir <c>0x00544a9c</c>). Ce n'est
    /// donc pas un tableau declare, mais les cinq emplacements qu'il couvre sont cinq globales
    /// voisines qui recoivent toutes le retour du meme chargeur, <c>FUN_00529b50</c> :</para>
    ///
    /// <list type="table">
    /// <item><term>0</term><description><c>DAT_005e97c0</c> — le tampon de ressources principal</description></item>
    /// <item><term>1</term><description><c>DAT_005e97c4</c> — son repertoire</description></item>
    /// <item><term>2</term><description><c>DAT_005e97c8</c> — le repertoire de l'etat de jeu courant</description></item>
    /// <item><term>3</term><description><c>DAT_005e97cc</c> — le repertoire de la zone</description></item>
    /// <item><term>4</term><description><c>DAT_005e97d0</c> — le repertoire du menu objets</description></item>
    /// </list>
    ///
    /// <para>L'emplacement 5 tomberait sur <c>DAT_005e97d4</c>, qui n'est pas une racine : son seul
    /// ecrivain (<c>Blaze.c</c> ligne 81679) y range un parametre, pas un retour de chargeur. La
    /// levee s'arrete donc a 5, et non a 256.</para>
    ///
    /// <para><b>Historique.</b> Seuls 0 et 1 etaient modelises jusqu'au 2026-08-25, chacun recopie
    /// dans les quatre gestionnaires ; une boite de dialogue passant par la racine 3 —
    /// le repertoire de zone, celui dont vit tout l'etat 3 — levait <c>NotImplementedException</c>
    /// dans <see cref="Handler_00544a80"/>.</para>
    ///
    /// <para><b>Verifie a l'execution</b> (sonde jetable dans l'auberge, 2026-08-25). Les cinq
    /// pointeurs et la premiere entree de leur table <c>+0x70</c> :</para>
    /// <code>
    /// racine 0  root=0x00ad0000  +0x70=0x00ad0194  [0] = 81 40 82 ED 82 F1 82 EE
    /// racine 1  root=0x00ad08b0  +0x70=0x00ad0a90  [0] = « Tire sur… »
    /// racine 2  root=0x00ad504c  +0x70=0x00ad5158  [0] = « Cle de l… »
    /// racine 3  root=0x00ad5d50  +0x70=0x00ad609c  [0] = 1F D0 1D 08 00 0C 20 2D
    /// racine 4  root=0x00ad975c  +0x70=0x00000000  (menu objets non charge)
    /// </code>
    /// <para>Les racines 1 et 2 rendent des libelles lisibles ; la racine 3 rend un flux qui
    /// commence par des <b>codes de controle</b> (<c>0x1f</c>, <c>0x1d</c>), c'est-a-dire une
    /// reelle chaine de dialogue — c'est bien la table de libelles de la zone. La racine 4 a une
    /// table nulle tant que le menu objets n'est pas charge, exactement comme l'original.</para>
    /// </summary>
    private byte* ResourceRoot(uint slot, string caller) => slot switch
    {
        0 => mem.s_MainResourceBuffer_005e97c0,   // le tampon principal
        1 => mem.s_MainResourceDirectory_005e97c4,// son repertoire
        2 => Blaze.s_Unknown_005e97c8,            // le repertoire de l'etat courant
        3 => Blaze.s_Unknown_005e97cc,            // celui de la zone
        4 => Blaze.s_Unknown_005e97d0,            // celui du menu objets
        _ => throw new NotImplementedException(
                 $"{caller} : racine de ressources {slot} — l'original indexe &DAT_005e97c0 de " +
                 "quatre en quatre ; seuls les emplacements 0 a 4 sont des racines connues, et " +
                 "l'emplacement 5 tomberait sur DAT_005e97d4, qui n'en est pas une."),
    };

    /// <summary>
    /// Handler for control code <c>0x1c</c>, in both passes — <c>0x00544e80</c>, decoded by hand
    /// from Legacy/Binaries/Blaze.exe (file offset <c>0x144280</c>). It is a **subroutine jump inside the
    /// text stream**: it pushes a return pointer onto the descriptor's stack at <c>+0x8b4</c> and
    /// returns a different string, chosen by a byte of the player's saved block.
    ///
    /// <para>The matching **pop** was already reconstructed — <see cref="RunTextEngine"/> unwinds
    /// <c>descriptor[0x15]</c> and resumes from <c>+0x8b4 + depth * 4</c> when it meets a NUL. Only
    /// the push side was missing, which is why the stack looked write-only until now.</para>
    ///
    /// <para><b>The substituted value.</b> <c>[0x006087b4]</c> is an IAT slot, so
    /// <c>*(*(0x006087b4))</c> is winsys3d's exported <c>MainMem</c> — the same pointer
    /// <see cref="GetMainMemPtr"/> returns. The offset <c>slot * 0x2000 + 0xF0153</c> is then
    /// exactly <c>DAT_00573338[0x153]</c> for that player, i.e. the byte sub-state 3 writes: the
    /// arena's per-player 0x2000 block at <c>MainMem + 0xf0000</c>.</para>
    ///
    /// <para>Operand: three bytes. <c>text[0]</c> picks the resource root, <c>text[1..2]</c> a
    /// little-endian index into its <c>+0x70</c> label table, to which the substituted value is
    /// added — so one control code selects one of a run of consecutive strings.</para>
    /// </summary>
    private byte* Handler_00544e80(byte* text, byte* descriptor)
    {
        byte value = 0;
        uint* owner = *(uint**)(descriptor + 0x8b0);
        if (owner != null && (*owner & 0xfc0000) == 0x800000)
        {
            byte* mainMem = GetMainMemPtr();
            if (mainMem == null)
            {
                throw new NotImplementedException(
                    "FUN_00544e80: winsys3d's MainMem resolves to null, so the per-player block " +
                    "this control code substitutes from cannot be located.");
            }
            value = mainMem[*((byte*)owner + 8) * 0x2000 + 0xF0153];
        }

        // Ghidra: descriptor[0x8b4 + descriptor[0x15] * 4] = text + 3, puis le compteur avance.
        byte depth = descriptor[0x15];
        *(byte**)(descriptor + 0x8b4 + depth * 4) = text + 3;
        descriptor[0x15] = (byte)(depth + 1);

        uint index = (uint)((text[2] << 8) | text[1]) + value;
        // Ghidra: *(int *)(&DAT_005e97c0 + text[0] * 4) — voir ResourceRoot.
        byte* root = ResourceRoot(text[0], "FUN_00544e80");

        return *(byte**)(*(byte**)(root + 0x70) + index * 4);
    }

    /// <summary>
    /// Ghidra: FUN_00544810 @ 0x00544810 — pass-2 handler for control code <c>0x01</c>: draws an
    /// inline icon glyph. The *other* icon path is <c>FUN_0051be90</c> — see
    /// <see cref="RenderSaveScreen"/>'s banner comment.
    ///
    /// <para><b>Correction du 2026-08-22</b> : ce commentaire affirmait que <c>FUN_0051be90</c>
    /// « n'a jamais eu de corps ». C'est faux — elle est décompilée dans <c>Blaze.c</c> à la ligne
    /// 66712, et elle est transcrite depuis, sous le nom
    /// <c>Primitives2d.DrawDecimalNumber</c>. Les deux chemins restent bien distincts : celui-ci
    /// passe par l'atlas imbriqué du moteur de texte, l'autre découpe un atlas de chiffres à la
    /// main et n'imprime que des nombres.</para>
    ///
    /// <para>Three operand bytes select (category, sub-category, index), each remapped through
    /// <see cref="s_PanelSelection_005e9060"/> exactly like <see cref="Handler_005449f0"/>'s single
    /// operand, then chase the same nested atlas <see cref="DrawUiPanel"/> and
    /// <see cref="Handler_00544e80"/> already use: root (le meme choix que <see cref="ResourceRoot"/>) → <c>+0x94</c> (atlas) →
    /// <c>[selector1]</c> (a sub-table pointer) → <c>[selector2 * 8]</c> (an 8-byte
    /// <c>(uv, flags)</c> record). <c>flags</c>' byte at bits 16-23 then indexes
    /// <see cref="s_EntityDecodeTable_005e8f00"/> — the same 1024-entry tpage/clut table
    /// <see cref="DecodeSlot"/> reads elsewhere, always in-bounds here since the index is an
    /// explicit <c>&amp; 0xff</c>. The five <c>DAT_005e8324[...]</c> stores that follow are the same
    /// six-word sprite primitive <see cref="EmitTextSprite"/> builds, just fed from this record
    /// instead of the descriptor's own line state.</para>
    ///
    /// <para><b>The return value — resolved, not just faithfully transcribed.</b> Ghidra's own
    /// read, confirmed by hand against the bytes at file offset <c>0x143c10</c> (there is no branch
    /// after it, so this is unconditional): <c>return (DAT_0056321c + 1) * (int)(param_1 + 3)</c>.
    /// Every sibling handler just returns the advanced cursor; this one is a genuine integer
    /// multiply of it by <c>DAT_0056321c + 1</c> (<see cref="s_CdCheckFileSize_0056321c"/>). Since
    /// <see cref="RunTextEngine"/> feeds this return value straight back in as the next parse
    /// cursor, correctness for *any* text pointer requires the multiplier to be exactly <c>1</c>,
    /// i.e. <c>DAT_0056321c == 0</c> exactly — which in turn requires the CD-check file
    /// (<see cref="RunApplication"/>'s <c>cdCheckPath</c>, TODO #2, now resolved — see there) to be
    /// **exactly 0x28000000 (640 MiB)**. It is: the real disc's <c>cd.idx</c> is exactly that size,
    /// confirming this was copy protection all along — an authentic disc makes the multiplier
    /// silently reduce to identity; anything else corrupts the text cursor several control codes
    /// later, far from the tamper site. With the real filename wired up and the CD mounted, this
    /// now returns a correct cursor and rendering proceeds past this control code.</para>
    /// </summary>
    internal byte* Handler_00544810(byte* text, byte* descriptor)
    {
        FlushLine(descriptor);
        if (s_PenX_005e8320 != 0) EnqueueGlyphUpload(descriptor);
        if ((sbyte)descriptor[0x0e] != 0) EmitTextSprite(descriptor);
        s_PenX_005e8320 = 0;
        descriptor[0x0c] = 0;

        byte selector0 = mem.s_PanelSelection_005e9060[text[0]];
        byte* root = ResourceRoot(selector0, "FUN_00544810");   // &DAT_005e97c0 + selector0 * 4
        byte* atlas = *(byte**)(root + 0x94);

        byte selector1 = mem.s_PanelSelection_005e9060[text[1]];
        byte* subTable = *(byte**)(atlas + selector1 * 4);

        byte selector2 = mem.s_PanelSelection_005e9060[text[2]];
        uint uv = *(uint*)(subTable + selector2 * 8);
        uint flags = *(uint*)(subTable + selector2 * 8 + 4);
        if (mem.s_DeviceCapFlag_00564a48) flags &= 0xfffffdff;

        uint rect = *(uint*)(descriptor + 0x20);
        uint clutEntry = *(uint*)(mem.s_EntityDecodeTable_005e8f00 + ((flags >> 0x10) & 0xff) * 4);

        uint* prim = s_SpriteCursor_005e8324;
        prim[2] = 0x65ffffff;
        prim[1] = (rect >> 0x10 & 0x60) | (clutEntry & 0xffff) | 0xe1000000;
        prim[3] = ((rect >> 8 & 0xff) << 0x10) | (rect & 0xff);
        prim[4] = (clutEntry & 0xffff0000) + (flags >> 6 & 0x3c00000) + (flags >> 8 & 0xf0000) | (uv & 0xffff);
        prim[5] = (uv >> 0x10 & 0xff) | (uv >> 8 & 0xff0000);

        prim[0] = *s_OtCursor_005e8314 | 0x5000000;
        *s_OtCursor_005e8314 = (uint)(nuint)prim;
        s_SpriteCursor_005e8324 = prim + 6;

        return (byte*)((mem.s_CdCheckFileSize_0056321c + 1) * (uint)(nuint)(text + 3));
    }

    /// <summary>
    /// Handler for control code <c>0x04</c> — <c>0x005449b0</c>, decoded by hand from
    /// Legacy/Binaries/Blaze.exe (file offset <c>0x143db0</c>).
    /// <code>
    /// 8B 44 24 04  MOV EAX,[ESP+4]      ; text cursor
    /// 8B 54 24 08  MOV EDX,[ESP+8]      ; descriptor
    /// 33 C9        XOR ECX,ECX
    /// 8A 68 01     MOV CH,[EAX+1]
    /// 8A 08        MOV CL,[EAX]
    /// 83 C0 02     ADD EAX,2
    /// 66 89 4A 20  MOV [EDX+0x20],CX
    /// C3           RET
    /// </code>
    /// <para>Two operand bytes, matching the pass-1 stub for this code (<c>0x00543960</c>, "skip
    /// two"). It writes both verbatim into the low word of the descriptor's own <c>rect</c> dword
    /// at <c>+0x20</c> â€” the same field <see cref="EmitTextSprite"/> reads back to place the
    /// finished line's sprite: <c>text[0]</c> lands in the byte <c>rect &amp; 0xff</c>, folded into
    /// the sprite's X-ish term there, and <c>text[1]</c> in the next byte, <c>(rect &gt;&gt; 8) &amp;
    /// 0xff</c>, folded into its Y-ish term. The dword's upper word â€” including the
    /// <c>0x1000000</c> enable bit <see cref="DrawUiPanel"/> tests before it copies this
    /// descriptor's finished text into the panel's own display list â€” is left untouched, so this
    /// code only ever nudges a position something else has already turned on.</para>
    /// </summary>
    private byte* Handler_005449b0(byte* text, byte* descriptor)
    {
        *(ushort*)(descriptor + 0x20) = (ushort)(text[0] | (text[1] << 8));
        return text + 2;
    }

    /// <summary>
    /// Handler for control code <c>0x02</c> — <c>0x00544970</c>, décodé à la main le 2026-08-23.
    /// Il pose d'un seul mot le <b>compteur de glyphes</b> de <c>+0x10</c> et l'<b>index de ligne</b>
    /// de <c>+0x11</c> : <c>texte[0]</c> dans l'octet bas, <c>texte[1]</c> dans le haut.
    ///
    /// <para>C'est bien une écriture de deux octets (<c>mov [edx+10h],cx</c>), pas une de un : les
    /// deux champs se posent ensemble et il n'y a pas moyen d'écrire l'un sans l'autre.</para>
    /// </summary>
    private byte* Handler_00544970(byte* text, byte* descriptor)
    {
        *(ushort*)(descriptor + 0x10) = (ushort)(text[0] | (text[1] << 8));
        return text + 2;
    }

    /// <summary>
    /// Handler for control code <c>0x03</c> — <c>0x00544990</c>, décodé à la main le 2026-08-23.
    /// Le jumeau exact du précédent, un champ plus loin : il écrit le mot de <c>+0x12</c>, dont
    /// l'octet bas est la <b>largeur de ligne</b> en unités de huit pixels.
    /// </summary>
    private byte* Handler_00544990(byte* text, byte* descriptor)
    {
        *(ushort*)(descriptor + 0x12) = (ushort)(text[0] | (text[1] << 8));
        return text + 2;
    }

    /// <summary>
    /// Handler for control code <c>0x05</c> — <c>0x005449d0</c>, décodé à la main le 2026-08-23.
    /// Il choisit la <b>banque de composition</b> des glyphes (<c>+0x14</c>) — et, ce faisant,
    /// <b>remet la profondeur de pile de sous-chaînes à zéro</b>.
    ///
    /// <para>Ce second effet n'est pas une déduction : l'original fait <c>movzx cx, byte [eax]</c>
    /// puis <c>mov [edx+14h],cx</c>, donc il écrit <b>deux</b> octets là où l'opérande n'en fournit
    /// qu'un, et le second — <c>+0x15</c>, la profondeur que les codes <c>0x0b</c> et <c>0x1c</c>
    /// empilent — reçoit le zéro de l'extension. Transcrire <c>descripteur[0x14] = texte[0]</c>
    /// aurait laissé la pile de retour à sa profondeur courante, et une sous-chaîne ouverte plus
    /// tôt serait revenue là où il ne fallait pas.</para>
    ///
    /// <para>Un seul octet d'opérande — la table de balayage le confirme, qui porte le talon
    /// <see cref="Advance1"/> à cet indice.</para>
    /// </summary>
    private byte* Handler_005449d0(byte* text, byte* descriptor)
    {
        *(ushort*)(descriptor + 0x14) = text[0];
        return text + 1;
    }

    /// <summary>
    /// Handler for control code <c>0x08</c> — <c>0x00544ab0</c>, decoded by hand from the binary on
    /// 2026-08-23. **C'est le code qui immobilise le joueur pendant un dialogue.** Il lit un
    /// sous-code d'un octet et l'aiguille sur une table de sauts de quatre entrées, relevée en
    /// <c>0x00544afc</c> (<c>c5 4a 54 00 / d4 4a 54 00 / de 4a 54 00 / ef 4a 54 00</c>) :
    ///
    /// <list type="table">
    /// <item><term>0</term><description><see cref="Blaze.FreezeAllEntities"/> — le monde se fige</description></item>
    /// <item><term>1</term><description><see cref="Blaze.UnfreezeAllEntities"/> — il repart</description></item>
    /// <item><term>2</term><description>bit 0 de <c>DAT_00604b78</c> effacé</description></item>
    /// <item><term>3</term><description>bit 0 de <c>DAT_00604b78</c> posé</description></item>
    /// </list>
    ///
    /// <para>Tout sous-code au-delà de 3 ne fait qu'avancer le curseur — la table n'a que quatre
    /// entrées et le <c>ja</c> de <c>0x00544abc</c> le renvoie sur la sortie commune.</para>
    ///
    /// <para>Le premier lecteur atteint est la boîte « - La réception - » de l'auberge : c'est ce
    /// code-ci qui explique qu'on ne puisse pas bouger tant qu'elle n'est pas passée.</para>
    /// </summary>
    private byte* Handler_00544ab0(byte* text, byte* descriptor)
    {
        _ = descriptor;                                     // le corps ne touche pas au descripteur

        switch (text[0])
        {
            case 0: Blaze.FreezeAllEntities(); break;
            case 1: Blaze.UnfreezeAllEntities(); break;
            case 2: Blaze.Saves.s_CutsceneActive_00604b78 &= ~1; break;
            case 3: Blaze.Saves.s_CutsceneActive_00604b78 |= 1; break;
        }

        return text + 1;
    }

    /// <summary>
    /// Handler for control code <c>0x0a</c> — <c>0x00544bc0</c> (74 octets), décodé à la main le
    /// 2026-08-23. **C'est le code par lequel un dialogue lance un script d'entité** : il lit un
    /// index de 16 bits dans le flux de texte et appelle <see cref="Blaze.RunEntityScript"/> sur
    /// l'entité qui porte le message.
    ///
    /// <para>Les deux valeurs viennent du descripteur : <c>+0x8b0</c> est cette entité — le même
    /// champ que <see cref="Handler_00544e80"/> interroge pour savoir si le message appartient à un
    /// membre du groupe — et <c>+0x8ac</c> est le curseur de texte lui-même.</para>
    ///
    /// <para><b>Le script peut détourner le flux de texte.</b> Le corps relit <c>+0x8ac</c> après
    /// l'appel et le compare à sa valeur d'avant : si le script l'a déplacé, c'est cette nouvelle
    /// adresse qui devient le curseur, et les deux octets d'opérande ne sont pas consommés. Sinon
    /// le curseur avance normalement de deux. C'est le même idiome « avant / après » que
    /// <c>TickCutsceneCamera</c> et le verbe 2 de la caméra de scénette emploient sur
    /// <c>DAT_005719b4</c> : la seule façon, pour l'appelé, de rendre la main ailleurs.</para>
    ///
    /// <para><b>Recoupement sur la longueur de l'opérande.</b> La table de balayage
    /// (<c>PTR_LAB_0056e870</c>) porte <c>0x00543960</c> à ce même indice, c'est-à-dire le talon
    /// <see cref="Advance2"/>. La passe 1 saute donc exactement deux octets là où la passe 2 en
    /// consomme deux : les deux tables se confirment l'une l'autre, et l'opérande fait bien deux
    /// octets.</para>
    /// </summary>
    private byte* Handler_00544bc0(byte* text, byte* descriptor)
    {
        byte* before = *(byte**)(descriptor + 0x8ac);

        Blaze.RunEntityScript(
            Blaze.s_Unknown_005e97cc,                       // Ghidra: DAT_005e97cc, le répertoire de zone
            *(byte**)(descriptor + 0x8b0),                  // l'entité qui porte le message
            (uint)(text[0] | (text[1] << 8)));

        byte* after = *(byte**)(descriptor + 0x8ac);
        return before == after ? text + 2 : after;
    }

    /// <summary>
    /// Handler for control code <c>0x0b</c> — <c>0x00544c10</c>, décodé à la main le 2026-08-23.
    /// C'est le <b>saut de sous-chaîne</b> sans substitution : le jumeau dépouillé de
    /// <see cref="Handler_00544e80"/>, qui fait la même chose mais en décalant l'index d'une valeur
    /// prise dans le bloc du joueur.
    ///
    /// <para>Il empile <c>texte + 3</c> sur la pile de retour du descripteur (<c>+0x8b4</c>,
    /// profondeur en <c>+0x15</c>), puis rend la chaîne désignée par ses trois octets d'opérande :
    /// <c>texte[0]</c> choisit la racine de ressources, <c>texte[1..2]</c> l'index dans sa table de
    /// libellés de <c>+0x70</c>. La dépile est déjà reconstruite dans <see cref="RunTextEngine"/>,
    /// qui reprend à <c>+0x8b4 + profondeur * 4</c> en rencontrant un NUL.</para>
    ///
    /// <para>Il est inscrit dans les <b>deux</b> tables, comme les codes <c>0x0f</c>, <c>0x1c</c> et
    /// <c>0x1f</c> : la passe de balayage doit suivre le même saut que la passe de rendu, sans quoi
    /// elle mesurerait la mauvaise chaîne.</para>
    ///
    /// <para>Premier lecteur atteint : la boîte « untel obtient tel objet » d'un coffre, dont
    /// <see cref="Blaze.GrantChestContents_00530F20"/> compose la phrase à partir de libellés qui
    /// s'appellent entre eux.</para>
    /// </summary>
    private byte* Handler_00544c10(byte* text, byte* descriptor)
    {
        byte depth = descriptor[0x15];
        *(byte**)(descriptor + 0x8b4 + depth * 4) = text + 3;
        descriptor[0x15] = (byte)(depth + 1);

        uint index = (uint)(text[1] | (text[2] << 8));
        // Ghidra: *(int *)(&DAT_005e97c0 + text[0] * 4) — voir ResourceRoot.
        byte* root = ResourceRoot(text[0], "FUN_00544c10");

        return *(byte**)(*(byte**)(root + 0x70) + index * 4);
    }

    // =====================================================================================
    // Les quinze setters de champ du descripteur (codes 0x10..0x13, 0x15..0x1b, 0x1e)
    //
    // Le binaire en fait quinze fonctions distinctes de seize a trente octets, qui ne different
    // que par le champ vise et la largeur ecrite. Elles sont repliees ici en trois corps
    // parametres, comme `ScriptOpAluArena` l'est pour les opcodes 4 a 7 de l'interpreteur : la
    // regularite est reelle et verifiee instruction par instruction, pas supposee.
    //
    // Les quinze figurent a l'identique dans les DEUX tables, donc la passe de balayage consomme
    // exactement les memes octets que la passe de rendu — ce qui donne au passage la longueur de
    // chaque operande sans avoir a la deduire.
    // =====================================================================================

    /// <summary>Un octet d'operande vers un octet du descripteur. Codes 0x10 a 0x13 et 0x15 a 0x17.</summary>
    private byte* SetDescriptorByte(byte* text, byte* descriptor, int field)
    {
        descriptor[field] = text[0];
        return text + 1;
    }

    /// <summary>Quatre octets d'operande, petit-boutiste, vers un dword du descripteur.
    /// Codes 0x18 a 0x1b.</summary>
    private byte* SetDescriptorDword(byte* text, byte* descriptor, int field)
    {
        *(uint*)(descriptor + field) = (uint)(text[0] | (text[1] << 8) | (text[2] << 16) | (text[3] << 24));
        return text + 4;
    }

    /// <summary>
    /// Handler for control code <c>0x1e</c> — <c>0x00545100</c>, décodé à la main le 2026-08-23.
    /// Le jumeau exact de <see cref="Handler_00545120"/> un champ plus bas : deux octets d'opérande
    /// dont le second part dans la <b>moitié haute</b> du dword de <c>+0x04</c>, pas dans l'octet
    /// suivant. Deux <c>short</c>, donc, et non un mot de seize bits.
    /// </summary>
    private byte* Handler_00545100(byte* text, byte* descriptor)
    {
        *(uint*)(descriptor + 4) = (uint)(text[0] | (text[1] << 0x10));
        return text + 2;
    }

    /// <summary>
    /// Handler for control code <c>0x07</c> — <c>0x00544a80</c>, décodé à la main le 2026-08-23.
    /// C'est un <b>saut de chaîne sans retour</b> : contrairement aux codes <c>0x0b</c> et
    /// <c>0x1c</c>, il n'empile rien sur <c>+0x8b4</c>. Le flux de texte part et ne revient pas.
    ///
    /// <para>L'index est <b>calculé</b> : <c>texte[1]</c> désigne une variable de la banque
    /// <c>DAT_005e9060</c>, dont la valeur est ajoutée à <c>texte[2]</c>. Une seule chaîne de script
    /// choisit donc parmi une suite de libellés consécutifs selon l'état du jeu — c'est le
    /// « selon que… » du scénariste. <c>texte[0]</c> choisit la racine de ressources comme
    /// ailleurs.</para>
    /// </summary>
    private byte* Handler_00544a80(byte* text, byte* descriptor)
    {
        _ = descriptor;                                     // le corps ne touche pas au descripteur

        uint index = (uint)(mem.s_PanelSelection_005e9060[text[1]] + text[2]);
        byte* root = ResourceRoot(text[0], "FUN_00544a80");   // &DAT_005e97c0 + text[0] * 4

        return *(byte**)(*(byte**)(root + 0x70) + index * 4);
    }

    /// <summary>
    /// Handler for control code <c>0x0e</c> — <c>0x00544b50</c>, décodé à la main le 2026-08-23.
    /// C'est l'<b>attente de fin de page</b> : le code qui arrête le défilement du texte et rend la
    /// main au joueur jusqu'à ce qu'il valide.
    ///
    /// <para>Il pose <c>+0x3b</c> à 1, le bit <c>4</c> des drapeaux de <c>+0x8c7</c>, et remet
    /// <c>+0x8c4</c> à zéro. Le seul octet d'opérande se coupe en deux : le <b>quartet bas</b> va
    /// dans <c>+0x8c5</c>, l'index de variable que <see cref="s_PanelSelection_005e9060"/> lira,
    /// et le <b>quartet haut</b> choisit un bit de drapeau supplémentaire — 1 pose <c>0x40</c>,
    /// 2 pose <c>0x80</c>, toute autre valeur n'en pose aucun.</para>
    ///
    /// <para>Il écrit lui-même le curseur dans <c>+0x8ac</c> <b>en plus</b> de le rendre. Les deux
    /// valeurs sont identiques ; l'écriture est conservée parce que rien ne dit que l'appelant est
    /// le seul lecteur de ce champ.</para>
    /// </summary>
    private byte* Handler_00544b50(byte* text, byte* descriptor)
    {
        byte flags = (byte)(descriptor[0x8c7] | 4);

        descriptor[0x3b] = 1;
        descriptor[0x8c4] = 0;
        descriptor[0x8c7] = flags;

        byte operand = text[0];
        descriptor[0x8c5] = (byte)(operand & 0xf);

        switch (operand >> 4)
        {
            case 1: flags |= 0x40; descriptor[0x8c7] = flags; break;
            case 2: flags |= 0x80; descriptor[0x8c7] = flags; break;
        }

        *(byte**)(descriptor + 0x8ac) = text + 1;
        return text + 1;
    }

    /// <summary>
    /// Handler for control code <c>0x1d</c> — <c>0x00544f10</c>, décodé à la main le 2026-08-23.
    /// Il <b>insère le nom du personnage qui parle</b> : le flux de texte saute dans le bloc de
    /// 0x2000 octets du membre du groupe désigné par <c>+0x8b0</c>, à <c>MainMem + 0xf0100 +
    /// indice * 0x2000</c> — la même adresse que <see cref="Blaze.GetEntityStatBlock"/> rend pour
    /// un membre du groupe.
    ///
    /// <para><b>Sans opérande.</b> L'adresse empilée sur la pile de retour est <c>texte</c> tel
    /// quel, et non <c>texte + n</c> comme aux codes <c>0x0b</c> et <c>0x1c</c> : c'est ce qui
    /// prouve que ce code n'en a pas, et la table de balayage le confirme en partageant le même
    /// corps.</para>
    ///
    /// <para>Si <c>+0x8b0</c> est nul, ou si l'entité n'est pas un membre du groupe
    /// (<c>0xfc0000</c> ne valant pas <c>0x800000</c>), <b>rien n'est inséré</b> : le curseur est
    /// rendu inchangé et le nom manque simplement de la phrase.</para>
    /// </summary>
    private byte* Handler_00544f10(byte* text, byte* descriptor)
    {
        uint* owner = *(uint**)(descriptor + 0x8b0);
        if (owner == null || (*owner & 0xfc0000) != 0x800000) return text;

        byte depth = descriptor[0x15];
        *(byte**)(descriptor + 0x8b4 + depth * 4) = text;
        descriptor[0x15] = (byte)(depth + 1);

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "FUN_00544f10 : MainMem de winsys3d vaut null, donc le bloc du personnage dont ce " +
                "code insere le nom est introuvable.");
        }

        return mainMem + *((byte*)owner + 8) * 0x2000 + 0xf0100;
    }

    /// <summary>
    /// Handler for control code <c>0x1f</c> — <c>0x00545120</c>, decoded by hand from the binary on
    /// 2026-08-23 (29 bytes; Ghidra never emitted it, it is reached only through the two tables).
    /// It sets the <b>panel extent</b>: two bytes of the text stream land in the descriptor's
    /// <c>+0x08</c> dword, the first in the low half and the second in the high half.
    ///
    /// <para>The two halves are the width and height <see cref="DrawUiPanel"/> reads back as
    /// <c>extent</c>, which is how a message sizes its own box. Note the packing is
    /// <c>text[1] &lt;&lt; 0x10</c>, not <c>&lt;&lt; 8</c> as in <see cref="Handler_005449b0"/> —
    /// this one writes two shorts, not one:</para>
    /// <code>
    /// 00545128  8A 48 01     MOV CL,[EAX+1]      0054512D  C1 E1 10  SHL ECX,0x10
    /// 0054512B  8A 10        MOV DL,[EAX]        00545130  0B CA     OR  ECX,EDX
    /// 00545136  83 C0 02     ADD EAX,2           00545139  89 4A 08  MOV [EDX+8],ECX
    /// </code>
    ///
    /// <para>Le premier lecteur atteint est la boîte « - La réception - » de l'auberge, qui ne
    /// s'ouvrait pas tant que <c>DAT_00563c5c</c> était modélisé en globale isolée plutôt qu'en
    /// champ du descripteur.</para>
    /// </summary>
    private byte* Handler_00545120(byte* text, byte* descriptor)
    {
        *(uint*)(descriptor + 8) = (uint)(text[0] | (text[1] << 0x10));
        return text + 2;
    }

    /// <summary>
    /// Handler for control code <c>0x06</c> — <c>0x005449f0</c>, decoded by hand from
    /// Legacy/Binaries/Blaze.exe (file offset <c>0x143df0</c>). It **prints a number in decimal**, which is
    /// what the dice screen needs for the stat total and the seven class figures.
    ///
    /// <para>Two operand bytes, matching the pass-1 stub for this code (<c>0x00543960</c>, "skip
    /// two"). <c>text[0]</c> is a byte offset into <c>DAT_005e9060</c> — the player-stat block —
    /// and <c>text[1]</c> the width to read there: <c>2</c> a word, <c>4</c> a dword, anything else
    /// a single byte.</para>
    ///
    /// <para>The value is then walked against the ten-entry divisor table at <c>DAT_00566be8</c>
    /// (1e9 down to 1) and each digit emitted through <see cref="RasterizeGlyph"/>. Leading zeros
    /// are suppressed by <c>EBP</c>, which only latches once a non-zero digit has been seen **or**
    /// the last divisor is reached — so a value of zero still prints one <c>'0'</c>.</para>
    /// </summary>
    internal byte* Handler_005449f0(byte* text, byte* descriptor)
    {
        byte* source = mem.s_PanelSelection_005e9060 + text[0];

        uint value = text[1] switch
        {
            2 => *(ushort*)source,
            4 => *(uint*)source,
            _ => *source,
        };

        bool started = false;
        for (int i = 0; i < s_DecimalDivisors_00566be8.Length; i++)
        {
            uint divisor = s_DecimalDivisors_00566be8[i];
            uint digit = value / divisor;
            value -= digit * divisor;

            // Ghidra: `TEST EAX,EAX / JNZ / CMP EBX,1 / JNZ / INC EBP` — le drapeau se lève sur le
            // premier chiffre non nul, ou d'office sur le dernier diviseur.
            if (digit != 0 || divisor == 1) started = true;
            if (!started) continue;

            RasterizeGlyph(descriptor, digit + 0x30, 0);
        }

        return text + 2;
    }

    // Ghidra: &DAT_00566be8 — dix diviseurs décimaux, relevés au décalage fichier 0x1651e8.
    internal readonly uint[] s_DecimalDivisors_00566be8 =
    [
        1000000000, 100000000, 10000000, 1000000, 100000,
        10000, 1000, 100, 10, 1,
    ];

    // The four pass-1 stubs, decoded from the binary. They are not semantic handlers: they declare
    // how many operand bytes a control code consumes so the scan can skip without drawing.
    private byte* Advance0(byte* p, byte* descriptor) => p;        // 0x00543940  8B 44 24 04 C3
    private byte* Advance1(byte* p, byte* descriptor) => p + 1;    // 0x00543950  ... 40 C3
    private byte* Advance2(byte* p, byte* descriptor) => p + 2;    // 0x00543960  ... 83 C0 02 C3
    private byte* Advance3(byte* p, byte* descriptor) => p + 3;    // 0x00543970  ... 83 C0 03 C3

    private delegate byte* TextControlHandler(byte* text, byte* descriptor);

    /// <summary>
    /// Builds one of the two dispatch tables. Both are modelled as tables rather than a switch
    /// because that is what the original is, and because the two differ only by substitution â€”
    /// a switch would hide that <c>0x00543950</c> alone covers six codes.
    /// </summary>
    private TextControlHandler[] BuildTextTable(bool renderPass)
    {
        var t = new TextControlHandler[0x20];

        // Slots with no reconstructed body. Each names its address and code rather than no-opping,
        // so an unreached corner of the text format announces itself the first time it is used.
        for (int i = 0; i < 0x20; i++)
        {
            int code = i;
            uint address = (renderPass ? s_RenderTable_0056e728 : s_ScanTable_0056e870)[i];
            t[i] = (text, descriptor) => throw new NotImplementedException(
                $"UI text control code 0x{code:x2} dispatches to FUN_{address:x8}, which Ghidra " +
                $"never emitted (reached only through {(renderPass ? "PTR_FUN_0056e728" : "PTR_LAB_0056e870")}). " +
                "Le corps se releve au desassembleur : `OpenForesia.Tools -- disasm " +
                $"Legacy/Binaries/Blaze.exe 0x{address:x8} <n>`. Le gestionnaire prend (texte, " +
                "descripteur) et rend le nouveau curseur de texte ; voir " +
                "Documentation/Reference/DataSymbols.md pour la table.");
        }

        if (!renderPass)
        {
            // Pass 1: every slot whose recovered address is one of the four stubs.
            for (int i = 0; i < 0x20; i++)
            {
                t[i] = s_ScanTable_0056e870[i] switch
                {
                    0x00543940 => Advance0,
                    0x00543950 => Advance1,
                    0x00543960 => Advance2,
                    0x00543970 => Advance3,
                    _ => t[i],
                };
            }
        }
        else
        {
            t[0x01] = Handler_00544810;   // 0x00544810, decompiled
            t[0x02] = Handler_00544970;   // 0x00544970, décodé ici — compteur de glyphes + index de ligne
            t[0x03] = Handler_00544990;   // 0x00544990, décodé ici — largeur de ligne
            t[0x04] = Handler_005449b0;   // 0x005449b0, decoded here
            t[0x05] = Handler_005449d0;   // 0x005449d0, décodé ici — banque de composition, remet la profondeur a zero
            t[0x08] = Handler_00544ab0;   // 0x00544ab0, decoded here — gel/dégel
            t[0x06] = Handler_005449f0;   // 0x005449f0, decoded here — nombre en décimal
            t[0x09] = Handler_00544b10;   // 0x00544b10, decoded here
            t[0x0a] = Handler_00544bc0;   // 0x00544bc0, decoded here — lance un script d'entité
            t[0x0c] = Handler_00544c50;   // 0x00544c50, decompiled
            t[0x0d] = Handler_00544d90;   // 0x00544d90, decompiled
            t[0x0e] = Handler_00544b50;   // 0x00544b50, décodé ici — attente de fin de page
            t[0x14] = Handler_00544fb0;   // 0x00544fb0, decompiled
        }

        // Les deux tables portent le même corps à ces emplacements : ils servent aux deux passes,
        // contrairement aux codes dont la passe 1 substitue un talon d'avance.
        t[0x07] = Handler_00544a80;   // 0x00544a80, décodé ici — saut de chaîne, index calculé
        t[0x0b] = Handler_00544c10;   // 0x00544c10, décodé ici — saut de sous-chaîne
        t[0x0f] = Handler_00544e50;   // 0x00544e50
        t[0x1c] = Handler_00544e80;   // 0x00544e80
        t[0x1d] = Handler_00544f10;   // 0x00544f10, décodé ici — insère le nom du locuteur
        t[0x1e] = Handler_00545100;   // 0x00545100, décodé ici
        t[0x1f] = Handler_00545120;   // 0x00545120, décodé ici

        // Les quinze setters de champ, repliés en trois corps paramétrés — voir leur bandeau.
        t[0x10] = (x, d) => SetDescriptorByte(x, d, 0x23);
        t[0x11] = (x, d) => SetDescriptorByte(x, d, 0x2b);
        t[0x12] = (x, d) => SetDescriptorByte(x, d, 0x43);
        t[0x13] = (x, d) => SetDescriptorByte(x, d, 0x1b);
        t[0x15] = (x, d) => SetDescriptorByte(x, d, 0x2a);
        t[0x16] = (x, d) => SetDescriptorByte(x, d, 0x42);
        t[0x17] = (x, d) => SetDescriptorByte(x, d, 0x1a);
        t[0x18] = (x, d) => SetDescriptorDword(x, d, 0x24);
        t[0x19] = (x, d) => SetDescriptorDword(x, d, 0x2c);
        t[0x1a] = (x, d) => SetDescriptorDword(x, d, 0x44);
        t[0x1b] = (x, d) => SetDescriptorDword(x, d, 0x1c);

        return t;
    }

    // Ghidra: PTR_LAB_0056e870 and PTR_FUN_0056e728, both recovered verbatim from Legacy/Binaries/Blaze.exe.
    private readonly uint[] s_ScanTable_0056e870 =
    [
        0x543950, 0x543970, 0x543960, 0x543960, 0x543960, 0x543950, 0x543960, 0x544A80,
        0x543950, 0x543950, 0x543960, 0x544C10, 0x543940, 0x543940, 0x543950, 0x544E50,
        0x544F70, 0x544F80, 0x544F90, 0x544FA0, 0x543950, 0x545010, 0x545020, 0x545030,
        0x545040, 0x545070, 0x5450A0, 0x5450D0, 0x544E80, 0x544F10, 0x545100, 0x545120,
    ];

    private readonly uint[] s_RenderTable_0056e728 =
    [
        0x544FB0, 0x544810, 0x544970, 0x544990, 0x5449B0, 0x5449D0, 0x5449F0, 0x544A80,
        0x544AB0, 0x544B10, 0x544BC0, 0x544C10, 0x544C50, 0x544D90, 0x544B50, 0x544E50,
        0x544F70, 0x544F80, 0x544F90, 0x544FA0, 0x544FB0, 0x545010, 0x545020, 0x545030,
        0x545040, 0x545070, 0x5450A0, 0x5450D0, 0x544E80, 0x544F10, 0x545100, 0x545120,
    ];

    private readonly TextControlHandler[] s_ScanHandlers;
    private readonly TextControlHandler[] s_RenderHandlers;


    // Ghidra: DAT_0056e9b4 / _b8 / _bc â€” recovered from Legacy/Binaries/Blaze.exe. Full-width Shift-JIS
    // punctuation that this renderer squeezes to half width.
    private readonly uint[] s_HalfWidthCodes_0056e9b4 = [0x8381, 0x7981, 0x6981];

    // Ghidra: &DAT_005dffd8 â€” the 15-byte row scratch the spreading pass fills.
    private readonly byte* s_DbcsRowScratch_005dffd8 = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>
    /// Ghidra: FUN_00544150 @ 0x00544150 â€” the DBCS/kanji glyph renderer. Fetches a 16x16 1bpp
    /// glyph, spreads each row's bits and unpacks it into the 4bpp staging buffer.
    /// <para>
    /// **Signature confirmed against `Legacy/Raw/FUN_00544150.asm`.** Ghidra renders the descriptor
    /// as <c>unaff_retaddr</c> because the function reuses arg2's stack slot as scratch
    /// (<c>[ESP+0x18]</c>). The listing settles it: at <c>00544318</c>, after four pushes,
    /// <c>MOV EAX,[ESP+0x14]</c> loads **arg1** and it is arg1 that receives <c>+0xe</c> and
    /// <c>+0x10</c> â€” so the call sites' <c>(descriptor, codepoint)</c> is right and the
    /// pseudocode is wrong. The fifth time this project has been saved by reading the listing.
    /// </para>
    /// </summary>
    private void RenderDbcsGlyph(byte* descriptor, uint codepoint)
    {
        if (s_GlyphMode_005dffd0 == 1) return;

        uint code = codepoint & 0xffff;
        uint swapped = (code >> 8) | (code << 8);
        s_GlyphMode_005dffd0 = 2;

        bool halfWidth = swapped == s_HalfWidthCodes_0056e9b4[2]
                      || swapped == s_HalfWidthCodes_0056e9b4[1]
                      || swapped == s_HalfWidthCodes_0056e9b4[0];

        ushort* rows = Winsys3d.Ordinal_57(code);

        uint spread = 0;
        byte* scratch = s_DbcsRowScratch_005dffd8;
        for (int row = 0xf; row != 0; row--)
        {
            // CONCAT11(lo, hi) â€” the row is byte-swapped out of the resource.
            ushort r = (ushort)(((*rows & 0xff) << 8) | (*rows >> 8));
            if (halfWidth) r >>= 1;

            byte carry = (byte)((byte)(r >> 1) | (byte)(r & 1));
            uint v = (uint)((r >> 1) & 0xfffc) >> 1;
            spread = (v & 0xe000) >> 6
                   | ((v & 0xf800) >> 5) & 0xff
                   | ((v & 0xfe00) >> 4) & 0x7f
                   | ((v & 0xff80) >> 3) & 0x3f
                   | ((v & 0xffe0) >> 2) & 0x1f
                   | ((v & 0xfff8) >> 1) & 0xf
                   | (v & 7)
                   | (uint)(carry & 3);

            byte packed = (byte)spread;
            if (row == 4 || row == 8 || row == 0x0c)
            {
                scratch--;
                packed |= *scratch;
            }
            *scratch = packed;

            scratch++;
            rows++;
        }

        byte* dest = s_StagingWrite_005e8304;
        scratch = s_DbcsRowScratch_005dffd8;
        for (uint shade = 0xf; shade != 0; shade--)
        {
            s_StagingWrite_005e8304 = dest;

            uint bits = *scratch;
            for (int i = 8; i != 0; i--)
            {
                spread <<= 4;
                if ((bits & 1) != 0) spread |= shade;
                bits >>= 1;
            }

            *(uint*)s_StagingWrite_005e8304 = ~spread;
            scratch++;
            dest = s_StagingWrite_005e8304 + 0x80;   // Ghidra: DAT_005e8304 + 0x20 on a uint*
        }

        // Ghidra: DAT_005e8304 + -0x1bf on a uint* = -0x6fc bytes from start+0x700, i.e. +4.
        // The listing agrees by a different route: start+0x780 then SUB EBP,0x77c.
        s_StagingWrite_005e8304 -= 0x6fc;

        descriptor[0x0e] = (byte)(descriptor[0x0e] + 8);
        s_PenX_005e8320 += 8;
        descriptor[0x10]++;
    }

    /// <summary>
    /// Ghidra: FUN_00543b20 @ 0x00543b20 â€” rasterises one character into the 4bpp staging buffer
    /// and advances the pen, implementing word wrap by backtracking to the last break.
    /// </summary>


    private void RasterizeGlyph(byte* descriptor, uint character, int oddAlign)
    {
        CheatCodeProbe((byte)character);

        ushort ch = (ushort)character;
        if (s_TextState_005e8308 == 0 && ch == 0x20)
        {
            s_TrimAnchor_005e830c = s_StagingWrite_005e8304;
            s_TextState_005e8308 = 1;
        }

        if (s_TextState_005e8308 == 1 && ch == 0x20) return;   // leading space, provisional

        short width;
        int advance;

        if (ch == 0x5e)   // '^' â€” a solid rule to the next 8-column boundary
        {
            advance = 8 - (int)(s_PenX_005e8320 & 7);
            width = (short)advance;
            FillStagingRun((short)advance);
            descriptor[0x0e] = (byte)(descriptor[0x0e] + (byte)advance);
            s_PenX_005e8320 += (uint)advance;
            descriptor[0x10]++;
            return;
        }

        s_GlyphMode_005dffd0 = 1;

        if ((ch < 0x21 && ch != 0x14) || 0xff < ch)
        {
            // A space or an unprintable: draw a blank and remember it as the wrap point.
            if ((uint)descriptor[0x12] * 8 - 10 < s_PenX_005e8320 && s_BreakStaging_005e02fc != null)
            {
                NewLine(0, descriptor);
                return;
            }

            width = 4;
            if (oddAlign != 0 && (descriptor[0x0e] & 1) != 0) width = 5;
            advance = width;

            uint penBefore = s_PenX_005e8320;
            FillStagingRun(width);

            s_BreakColumn_005e8300 = penBefore + (uint)advance;

            // Ghidra applies `+ ((pen & 1) + width) / 2 - 0x700` **once**, after the 15-row loop â€”
            // and FillStagingRun already does exactly that as its last statement. Repeating it here
            // advanced the staging pointer twice per space, which drifted every glyph after the
            // first space of a line into the wrong columns and eventually the wrong bank.
            s_BreakStaging_005e02fc = s_StagingWrite_005e8304;
            s_BreakPen_005e8328 = (uint)descriptor[0x0e] + (uint)advance;
        }
        else
        {
            if ((uint)descriptor[0x12] * 8 - 10 < s_PenX_005e8320 && s_BreakStaging_005e02fc != null)
            {
                ReflowPartialWord(descriptor);
            }

            // Ghidra: DAT_005e8308 = ((0 < DAT_005e8308) - 1 & 0xfffffffd) + 2 â€” a SETcc idiom.
            // Positive state becomes 2 (a leading space was trimmed); anything else becomes -1.
            s_TextState_005e8308 =
                (((s_TextState_005e8308 > 0 ? 1 : 0) - 1) & unchecked((int)0xfffffffd)) + 2;

            switch (character & 0xffff)
            {
                case 0x40: case 0x80: case 0x8a: case 0x9a: case 0xc0: case 0xe0:
                    character = 0xa5;
                    break;
            }

            ch = (ushort)character;
            short slot;
            if (ch < 0xe1)
            {
                if (ch < 0xc1)
                {
                    if (ch < 0xa1)
                    {
                        slot = ch < 0x61 ? (ch < 0x41 ? (short)(ch - 0x21) : (short)(ch - 0x22))
                                         : (short)(ch - 0x23);
                    }
                    else slot = (short)(ch - 0x44);
                }
                else slot = (short)(ch - 0x45);
            }
            else slot = (short)(ch - 0x46);

            int index = slot;
            width = index >= 0 && index < s_GlyphWidths_0056e7b0.Length
                ? s_GlyphWidths_0056e7b0[index]
                : (short)0;
            if (oddAlign != 0 && (((ushort)descriptor[0x0e] + width) & 1) != 0) width++;

            int bitOffset = (index % 0x1f) * 10;
            byte* rows = GetMainMemPtr() + (bitOffset >> 3) + (index / 0x1f) * 0x230 + 0x1ad8f0;
            advance = width;
            BlitGlyphRows(rows, bitOffset, advance);
        }

        descriptor[0x0e] = (byte)(descriptor[0x0e] + (byte)width);
        s_PenX_005e8320 += (uint)advance;
        descriptor[0x10]++;
    }

    /// <summary>The 15-row blank fill shared by the <c>'^'</c> rule and the space path.</summary>
    private void FillStagingRun(short width)
    {
        byte* row = s_StagingWrite_005e8304;
        uint pen = s_PenX_005e8320;

        for (int r = 0xf; r != 0; r--)
        {
            s_StagingWrite_005e8304 = row;
            uint column = pen & 1;
            uint bit = pen;
            for (int i = width + 1; i != 0; i--)
            {
                byte* cell = s_StagingWrite_005e8304 + ((int)column >> 1);
                if ((bit & 1) == 0) *cell = 0x0f;
                else *cell |= 0xf0;
                bit++;
                column++;
                pen = s_PenX_005e8320;
            }
            row = s_StagingWrite_005e8304 + 0x80;   // Ghidra: DAT_005e8304 + 0x20 on a uint*
        }

        s_StagingWrite_005e8304 += (int)(((pen & 1) + (uint)width) >> 1) - 0x700;
    }

    /// <summary>
    /// The 15-row glyph blit: one 12-bit slice per row out of the font bitmap, unpacked into
    /// 4bpp cells. Row 1 is forced solid, which is what gives the glyph its baseline.
    /// </summary>
    private void BlitGlyphRows(byte* rows, int bitOffset, int advance)
    {
        byte* row = s_StagingWrite_005e8304;
        uint pen = s_PenX_005e8320;
        int shift = (bitOffset + (((bitOffset + ((bitOffset >> 31) & 7)) >> 3) * -8)) & 0x1f;

        for (int r = 0xf; r != 0; r--)
        {
            s_StagingWrite_005e8304 = row;

            uint slice = ((((uint)rows[1] + (uint)rows[0] * 0x100) * 0x100 + rows[2]) << shift) >> 0xc;
            if (r == 1) slice = 0xffffffff;

            uint column = pen & 1;
            uint bit = pen;
            for (int i = advance; i != 0; i--)
            {
                int cell = (int)column >> 1;
                if ((bit & 1) == 0)
                {
                    if ((slice & 0x800) == 0) *(sbyte*)(s_StagingWrite_005e8304 + cell) = (sbyte)r;
                    else s_StagingWrite_005e8304[cell] = 0x0f;
                }
                else if ((slice & 0x800) == 0)
                {
                    s_StagingWrite_005e8304[cell] =
                        (byte)((r << 4) | (s_StagingWrite_005e8304[cell] & 0x0f));
                }
                else s_StagingWrite_005e8304[cell] |= 0xf0;

                bit++;
                column++;
                slice <<= 1;
                pen = s_PenX_005e8320;
            }

            rows += 0x28;
            row = s_StagingWrite_005e8304 + 0x80;
        }

        s_StagingWrite_005e8304 += (int)(((pen & 1) + (uint)advance) >> 1) - 0x700;
    }

    /// <summary>
    /// The word-wrap backtrack: rewinds the pen to the last break, starts a new line, and copies
    /// the partial word's staged pixels onto it â€” byte-aligned or nibble-shifted by parity.
    /// </summary>
    private void ReflowPartialWord(byte* descriptor)
    {
        uint carried = s_PenX_005e8320 - s_BreakColumn_005e8300;
        sbyte penBefore = (sbyte)descriptor[0x0e];
        sbyte penAtBreak = (sbyte)s_BreakPen_005e8328;

        s_PenX_005e8320 = s_BreakColumn_005e8300;
        descriptor[0x0e] = (byte)penAtBreak;
        byte* source = s_BreakStaging_005e02fc;

        NewLine(0, descriptor);

        int span = (int)(carried + 1) >> 1;

        if ((s_BreakColumn_005e8300 & 1) == 0)
        {
            for (int r = 0xf; r != 0; r--)
            {
                byte* src = source;
                byte* dst = s_StagingWrite_005e8304;
                for (uint w = (uint)span >> 2; w != 0; w--) { *(uint*)dst = *(uint*)src; src += 4; dst += 4; }
                source += 0x80;
                for (uint b = (uint)span & 3; b != 0; b--) { *dst = *src; src++; dst++; }
                s_StagingWrite_005e8304 += 0x80;
            }
        }
        else
        {
            for (int r = 0xf; r != 0; r--)
            {
                for (int i = 0; i < span; i++)
                {
                    s_StagingWrite_005e8304[i] =
                        (byte)((sbyte)source[i + 1] * 0x10 + (source[i] >> 4));
                }
                source += 0x80;
                s_StagingWrite_005e8304 += 0x80;
            }
        }

        s_StagingWrite_005e8304 += ((int)carried >> 1) - 0x780;
        s_PenX_005e8320 = carried;
        descriptor[0x0e] = (byte)(penBefore - penAtBreak);
        s_BreakStaging_005e02fc = null;
    }


    /// <summary>
    /// Ghidra: FUN_005443a0 @ 0x005443a0 â€” the UI text engine. Runs the descriptor's byte stream
    /// twice: pass 1 scans and collects <c>$</c>-escapes, pass 2 emits glyphs. Returns the advanced
    /// VRAM-upload node cursor.
    /// </summary>
    internal uint* RunTextEngine(uint* uploadCursor, byte* descriptorArg)
    {
        // Ghidra: puVar11 = (undefined4 *)(param_2 & 0xffffff). The identity in the original; a
        // reconstruction bug here rather than original behaviour if it ever bites.
        byte* descriptor = (byte*)((nuint)descriptorArg & 0xffffff);
        if (descriptor != descriptorArg)
        {
            throw new InvalidOperationException(
                $"FUN_005443a0 masks its descriptor to 24 bits, but this one is at " +
                $"0x{(nint)descriptorArg:X8}. Allocate it with AllocLowZeroed.");
        }

        // Ghidra: local_8d4[8]. The '&' path can index it at -1 (for the literal "&0"), so the
        // buffer is offset by one rather than silently clamping an index the original allows.
        byte* escapeBuffer = stackalloc byte[8];
        for (int i = 0; i < 8; i++) escapeBuffer[i] = 0;
        byte* escapes = escapeBuffer + 1;

        byte flags = descriptor[0x8c7];
        if ((flags & 4) != 0) return uploadCursor;
        if ((flags & 2) != 0) return uploadCursor;
        if ((flags & 0x40) != 0) return uploadCursor;
        if ((flags & 0x80) != 0) return uploadCursor;

        if ((sbyte)descriptor[0x8c6] != 0)
        {
            descriptor[0x8c6] = (byte)((sbyte)descriptor[0x8c6] - 1);
            return uploadCursor;
        }

        byte* text = *(byte**)(descriptor + 0x8ac);
        if (text == null) return uploadCursor;

        s_TextState_005e8308 = 0;
        s_BreakStaging_005e02fc = null;
        s_LineIndex_005e8318 = 1;
        if (s_Unknown_005dffe8 == 0) s_Unknown_005dffe8 = 1;

        s_UploadCursor_005e831c = uploadCursor;

        uint line = (*(uint*)(descriptor + 0x10) >> 8) & 0xff;
        *(uint*)(descriptor + 0x0c) = (*(uint*)(descriptor + 0x10) & 0xffff) | 0xf000000;
        s_OtCursor_005e8314 = (uint*)(descriptor + (0x29 - line) * 4);
        s_SpriteCursor_005e8324 = (uint*)(descriptor + (0x20a - line * 0x20) * 4);
        s_StagingWrite_005e8304 = StagingBank(descriptor[0x14], line);
        s_PenX_005e8320 = 0;

        // Ghidra: the 0x232-dword block copy into local_8c8 â€” pass 1 must leave no trace.
        const int SaveBytes = 0x232 * 4;
        byte* saved = stackalloc byte[SaveBytes];
        Buffer.MemoryCopy(descriptor, saved, SaveBytes, SaveBytes);

        // ---- pass 1: scan ----
        byte* escapeWrite = escapes;
        byte* cursor = text;
        bool terminated = false;

        // Neither pass has a length bound in the original: the stream is trusted to be
        // NUL-terminated .data. A cursor that walks off would read arbitrary memory here, so both
        // passes stop and say so instead. 0x4000 is far past any real page of text.
        int scanned = 0;
        const int ScanLimit = 0x4000;

        while (!terminated)
        {
            while (true)
            {
                if (++scanned > ScanLimit)
                {
                    throw new InvalidOperationException(
                        $"FUN_005443a0: scanned over 0x{ScanLimit:X} bytes without reaching a " +
                        "terminator. The descriptor text pointer at +0x8ac is not a NUL-terminated " +
                        "stream, so this walk is reading memory that is not text.");
                }

                byte b = *cursor;
                byte* next = cursor + 1;
                if (b == 0) break;

                if (b < 0x20)
                {
                    cursor = s_ScanHandlers[b](next, descriptor);
                    if (b == 9 || b == 0x0e || b == 0x0f) { terminated = true; break; }
                }
                else
                {
                    if (b > 0x7f && b < 0xa0) next = cursor + 2;   // DBCS lead byte
                    cursor = next;
                    // The original writes here unbounded into its 8-byte stack array, which is
                    // safe only because its own text never carries more escapes than that. Ours
                    // would corrupt the stack â€” the classic shape of a random CLR fatal error â€”
                    // so the write stops at the six slots the function initialises.
                    if (next[-1] == 0x24 && escapeWrite < escapes + 6)
                    {
                        *escapeWrite = *next;
                        escapeWrite++;
                    }
                }
            }
            if (terminated) break;

            if ((sbyte)descriptor[0x15] == 0)
            {
                *(uint*)(descriptor + 0x8ac) = 0;
                descriptor[0x8c7] &= 0xfe;
                break;
            }

            byte depth = (byte)((sbyte)descriptor[0x15] - 1);
            descriptor[0x15] = depth;
            cursor = *(byte**)(descriptor + 0x8b4 + depth * 4);
            *(uint*)(descriptor + 0x8b4 + depth * 4) = 0;
        }

        // ---- pass 2: render ----
        Buffer.MemoryCopy(saved, descriptor, SaveBytes, SaveBytes);
        cursor = text;
        bool skipping = false;
        scanned = 0;

        while (true)
        {
            bool reachedTail = false;

            while (true)
            {
                if (++scanned > ScanLimit)
                {
                    throw new InvalidOperationException(
                        $"FUN_005443a0: rendered over 0x{ScanLimit:X} bytes without reaching a " +
                        "terminator â€” same cause as the scan pass, see above.");
                }

                ushort u = *cursor;
                byte* next = cursor + 1;
                if (u == 0) break;

                if (u < 0x20)
                {
                    skipping = false;
                    cursor = s_RenderHandlers[u](next, descriptor);
                    if (u == 9 || u == 0x0e || u == 0x0f) { reachedTail = true; break; }
                }
                else if (u < 0x80 || 0x9f < u)
                {
                    if (*cursor == 0x24)          // '$' â€” an escape, already collected in pass 1
                    {
                        cursor += 2;
                    }
                    else
                    {
                        if (*cursor == 0x26)      // '&' â€” conditional on a collected escape
                        {
                            byte selector = *next;
                            int slot = 0;
                            if (selector > 0x2f && selector < 0x36)
                            {
                                slot = selector - 0x31;
                                next = cursor + 2;
                            }
                            byte want = escapes[slot];
                            if (*next != want) skipping = true;
                            if (*next == 0x78 && (want == 0x6d || want == 0x73)) skipping = false;
                            if (next[1] == 0x5b) next += 3;
                            else skipping = false;
                        }

                        cursor = next;
                        if (next[-1] == 0x5d) skipping = false;      // ']' closes the conditional
                        else if (!skipping)
                        {
                            RasterizeGlyph(descriptor, next[-1], *next < 0x20 ? 1 : 0);
                        }
                    }
                }
                else if (u < 0x83 && s_GlyphMode_005dffd0 != 2)
                {
                    // Half-width kana: look the two-byte code up in the table the resource
                    // directory points at, and fall back to the DBCS renderer on a miss.
                    uint code = u | ((uint)*next << 8);
                    short* table = *(short**)(*(byte**)(mem.s_MainResourceBuffer_005e97c0 + 0x70));
                    int index = 0;
                    while (*table != (short)code)
                    {
                        if (0xfe < (short)index) { index = -1; break; }
                        table++;
                        index++;
                    }

                    if (index >= 0 && (short)index < 0xff && ((short)index > 0 || s_GlyphMode_005dffd0 == 1))
                    {
                        RasterizeGlyph(descriptor, (uint)index, 1);
                    }
                    else
                    {
                        RenderDbcsGlyph(descriptor, *next + code * 0x100);
                    }
                    cursor += 2;
                }
                else
                {
                    RenderDbcsGlyph(descriptor, (uint)((*cursor << 8) | *next));
                    cursor += 2;
                }
            }

            if (!reachedTail)
            {
                skipping = false;
                if ((sbyte)descriptor[0x15] != 0)
                {
                    byte depth = (byte)((sbyte)descriptor[0x15] - 1);
                    descriptor[0x15] = depth;
                    cursor = *(byte**)(descriptor + 0x8b4 + depth * 4);
                    *(uint*)(descriptor + 0x8b4 + depth * 4) = 0;
                    continue;
                }

                descriptor[0x8c7] &= 0xfe;
                *(uint*)(descriptor + 0x8ac) = 0;

                uint completion = *(uint*)(descriptor + 0x8a8);
                if (completion != 0)
                {
                    // Ghidra: (*(code *)puVar11[0x22a])(puVar11[0x22c]) — le rappel recoit la
                    // donnee d'utilisateur de +0x8b0, et rien d'autre. La signature etait inconnue
                    // tant qu'aucun site d'affectation n'etait reconstruit ; GrantChestContents_00530F20
                    // en est un, et le desassemblage du site d'appel donne l'arite : un argument.
                    uint subject = *(uint*)(descriptor + 0x8b0);

                    switch (completion)
                    {
                        // 0x00474320 est un DOUBLE de 0x00450700, pas une seconde fonction : les
                        // 87 octets sont identiques dans Blaze.exe a l'exception du deplacement
                        // relatif du `call`, et les deux visent 0x005312D0. Le compilateur a emis
                        // le meme corps deux fois. Les aiguiller sur la meme transcription est
                        // donc fidele, et non une approximation.
                        case 0x00450700:
                        case 0x00474320:
                            Blaze.ChestPickupDone_00450700((uint*)subject);
                            break;

                        case 0x00530ed0:
                            // Le corps ne lit pas son argument.
                            Blaze.RestorePartyReactions_00530ED0();
                            break;

                        default:
                            throw new NotImplementedException(
                                $"Rappel de fin de texte 0x{completion:X8}, appele avec " +
                                $"0x{subject:X8} : pas encore reconstruit. Son corps se releve au " +
                                $"desassembleur (`OpenForesia.Tools -- disasm " +
                                $"Legacy/Binaries/Blaze.exe 0x{completion:x8} <n>`) ; il prend un " +
                                "seul argument, la donnee d'utilisateur de +0x8b0, et ne rend rien.");
                    }
                }
            }

            FlushLine(descriptor);
            if (s_PenX_005e8320 != 0) EnqueueGlyphUpload(descriptor);
            if ((sbyte)descriptor[0x0e] != 0) EmitTextSprite(descriptor);
            s_TextState_005e8308 = 0;
            return s_UploadCursor_005e831c;
        }
    }
}
