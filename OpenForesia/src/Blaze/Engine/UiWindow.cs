// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `Blaze` porte encore ce que cette classe lit : le drapeau de petite texture de la page
// d'options, et les trois dwords de MainMenu.cs decrits plus bas.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// Les cadres de l'interface : rectangles de decoupe et de vue, sprites de coin et de bord, et le
/// panneau complet que tous les ecrans dessinent (Ghidra: 0x00545).
///
/// <para>
/// <b>Dernier fichier de l'etape C</b>, et le seul choisi par elimination plutot que par cloture.
/// Comme <see cref="Model3d"/>, il ne possede AUCUNE globale : cinq methodes, zero champ. Sa
/// conversion ne deplace pas d'etat, elle rend visible ce qu'il lit ailleurs.
/// </para>
/// <para>
/// <b>TODO(REVERSE) — recouvrement d'arene, latent, releve le 2026-08-21, NON corrige.</b>
/// Les trois dwords <c>s_Unknown_005de154</c> / <c>_158</c> / <c>_15c</c> que cette classe lit
/// vivent dans <c>Screens/State00/MainMenu.cs</c> en champs separes, alors que leurs adresses
/// tombent A L'INTERIEUR de <c>GameMemory.s_ItemMenuArena_005de100</c> — 0x460 octets, donc
/// jusqu'a <c>0x005de560</c>. Idem pour <c>s_CheatIndex_005de164</c> (<c>UiText.cs</c>) et
/// <c>s_Unknown_005de168</c> (<c>Input.cs</c>).
/// </para>
/// <para>
/// Verifie : <b>aucun site n'adresse l'arene entre 0x005de154 et 0x005de170</b>, donc le
/// recouvrement ne se manifeste pas aujourd'hui — c'est un risque de structure, pas un defaut
/// observable.
/// </para>
/// <para>
/// <b>Cette phrase a cesse d'etre vraie une fois, le 2026-08-26.</b>
/// <c>DrawSpeechOverlay_005309B0</c> a ete ecrite en posant son origine par
/// <c>Mem.ItemMenuState(0x005de154…)</c>, c'est-a-dire par l'arene, alors que les deux emetteurs
/// ci-dessous lisent les champs. La bulle du menu rapide se dessinait donc dans le coin haut
/// gauche, origine restee a zero. Le site fautif a ete ramene sur les champs ; le recouvrement,
/// lui, reste. C'est la demonstration que ce TODO n'est pas theorique : il attend le prochain
/// site distrait.
/// </para>
/// <para>
/// Deux raisons de ne pas y toucher ici : le corriger changerait le comportement, et
/// l'etendue reelle de l'arene est elle-meme douteuse (son commentaire annonce 0x420 octets, son
/// allocation en fait 0x460, et ces 0x40 d'ecart sont exactement ce qui rend vrai ou faux le
/// signalement de <c>s_EntityList_005de558</c> par les tests). Trancher demande de retrouver
/// l'etendue dans <c>Blaze.c</c> : c'est de la reconstruction, pas du refactor.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>.
/// </para>
/// </summary>
internal sealed unsafe class UiWindow(GameMemory mem, UiText ui, Input inp, Audio aud)
{
    /// <summary>Trace du choix latche par un panneau, sous <c>OPENFORESIA_TRACE_SCRIPT=1</c> : le
    /// curseur, le creneau de <c>DAT_005e9060</c> vise, les drapeaux et le nombre de lignes. C'est
    /// ce qui relie « le joueur a repondu Oui » a « le script a lu telle valeur ».</summary>
    private static readonly bool s_TraceChoice =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_SCRIPT") == "1";

    // =====================================================================
    // The UI window renderer (Ghidra: FUN_00545340 and its four primitive emitters)
    //
    // Draws one UI panel: its scroll arrows, its frame corners and edges, the text display list
    // FUN_005443a0 already built inside the descriptor, and its cursor. The panel's own ordering
    // table is copied into the frame's, offset by the panel's screen position â€” which is why the
    // text engine builds a self-contained list rather than writing into the frame directly.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0051bdb0 @ 0x0051bdb0 â€” links a screen-space clip rectangle, chosen by the
    /// small-texture flag and the detail level.
    /// </summary>
    internal void EmitClipRect(uint* otEntry, uint** prim)
    {
        if (s_Unknown_005de15c == 0) return;

        uint* p = *prim;
        p[1] = (s_SmallTextureFlag == 0 && 7 < mem.s_SizeSetting_005de160)
            ? 0xfb00000b
            : 0xfb000001;
        p[2] = (uint)s_Unknown_005de154;
        p[3] = (uint)s_Unknown_005de158;
        p[0] = *otEntry | 0x3000000;
        *otEntry = (uint)(nuint)p;
        *prim = p + 4;                       // Ghidra: *param_2 + 0x10 bytes
    }

    /// <summary>
    /// Ghidra: FUN_0051be20 @ 0x0051be20 â€” links the panel's viewport rectangle, derived from the
    /// detail level the options page and the F11/F12 hotkeys set.
    /// </summary>
    internal void EmitViewportRect(uint* otEntry, uint** prim)
    {
        if (s_Unknown_005de15c == 0) return;

        uint* p = *prim;
        p[1] = (uint)(mem.s_SizeSetting_005de160 - 0x5000000);
        p[2] = (uint)(mem.s_SizeSetting_005de160 * -0x10 + 0xb0);
        p[3] = (uint)(mem.s_SizeSetting_005de160 * -0x0c + 0x84);
        p[0] = *otEntry | 0x3000000;
        *otEntry = (uint)(nuint)p;
        *prim = p + 4;
    }

    /// <summary>
    /// Ghidra: FUN_00545160 @ 0x00545160 â€” a six-word textured sprite (tag 0x05000000).
    /// </summary>
    private uint* EmitSprite(uint* otEntry, uint* prim, uint tpage, uint position,
                                    uint offset, uint clut, uint size)
    {
        prim[1] = (offset >> 0x10) & 0x60 | tpage & 0xffff | 0xe1000000;
        prim[2] = clut | 0x64000000;
        prim[3] = ((position >> 0x10) + ((offset >> 8) & 0xff)) * 0x10000
                  | (position + (offset & 0xff)) & 0xffff;
        prim[4] = offset & 0xf0000 | tpage & 0xffff0000 | size & 0xffff;
        prim[5] = (size >> 8) & 0xff0000 | (size >> 0x10) & 0xff;
        prim[0] = *otEntry | 0x5000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 6;
    }

    /// <summary>
    /// Ghidra: FUN_00545220 @ 0x00545220 â€” an eight-word tinted sprite (tag 0x07000000), the
    /// frame edges. Its colour word is built by packing three 5-bit channels.
    /// </summary>
    private uint* EmitTintedSprite(uint* otEntry, uint* prim, uint tpage, uint position,
                                          uint offset, uint clut, uint tint, uint size)
    {
        prim[1] = (offset >> 0x10) & 0x60 | tpage & 0xffff | 0xe1000000;
        prim[2] = ((((((tint >> 8) & 0xff) >> 3 | 0xffffc400) << 5 | (tint & 0xff) >> 3) << 5
                    | (uint)((byte)(-(sbyte)(tint >> 0x18)) >> 3)) << 5)
                  | (uint)((byte)(-(sbyte)(tint >> 0x10)) >> 3);
        if (mem.s_DeviceCapFlag_00564a48) clut &= 0xfdffffff;
        prim[3] = clut | 0x64000000;
        prim[4] = ((position >> 0x10) + ((offset >> 8) & 0xff)) * 0x10000
                  | (position + (offset & 0xff)) & 0xffff;
        prim[6] = size;
        prim[7] = 0xe2000000;
        prim[5] = offset & 0xf0000 | tpage & 0xffff0000 | tint & 0xffff;
        prim[0] = *otEntry | 0x7000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 8;
    }

    // Ghidra: &DAT_005e9060 â€” the per-panel selection index the scroll handling writes back.
    // 0x120 utiles, borné par inp.s_SlotArea_005e9180. Ses 0x40 premiers octets sont aussi la banque 2
    // (voir s_StateBank2_005e9060) — c'est la même mémoire, comme dans l'original.
    // Declaration deplacee dans GameMemory le 2026-08-19 : 0x005e9060 tombe dans l'etendue de
    // GameArena, donc c'est une VUE d'arene et non un stockage propre. Les sites d'appel d'ici
    // l'atteignent par mem.s_PanelSelection_005e9060.

    /// <summary>
    /// Ghidra: FUN_00545340 @ 0x00545340 â€” draws one UI panel and returns the advanced primitive
    /// cursor. 17 call sites across the game; this is the shared window layer, not menu-specific.
    /// </summary>
    internal uint* DrawUiPanel(uint* otEntry, uint* prim, byte* descriptor)
    {
        EmitViewportRect(otEntry, &prim);

        // Which player slot drives this panel: taken from the owner object when it has one, and
        // from the active slot otherwise.
        uint slot;
        uint* owner = *(uint**)(descriptor + 0x8b0);
        bool resolved = false;
        if (owner != null)
        {
            uint kind = *owner & 0xfc0000;
            if (kind == 0x200000)
            {
                if (owner[0x1b] != 0) { slot = *(byte*)((nuint)owner[0x1b] + 8); resolved = true; }
                else slot = 0;
            }
            else if (kind == 0x400000) { slot = *((byte*)owner + 9); resolved = true; }
            else if (kind == 0x800000) { slot = (byte)owner[2]; resolved = true; }
            else slot = 0;
        }
        else slot = 0;

        if (!resolved) slot = inp.s_SlotArea_005e9180[0x248 + (uint)mem.s_Unknown_005de538 * 0x9c];
        // The original indexes four player slots with a stride of 0x9c/0x4c. `slot` comes from a
        // byte in .data, so a table this reconstruction has not populated yields 0..255 and walks
        // far past both arrays â€” and the value read then becomes a *write* offset below. This was
        // corrupting the managed heap (Fatal error 0x80131506), so it fails loudly instead.
        if (slot >= 4)
        {
            throw new InvalidOperationException(
                $"FUN_00545340: player slot {slot} is outside the four the slot tables hold. It " +
                "comes from DAT_005e93c8 via DAT_005de538; one of those is not initialised the way " +
                "the original has it.");
        }

        if ((*(uint*)(inp.s_SlotFlags_005e93c0 + slot * 0x9c) & 0x10000) != 0)
        {
            slot = inp.s_SlotArea_005e9180[0x248 + (uint)mem.s_Unknown_005de538 * 0x9c];
        }

        byte slotIndex1 = inp.s_PlayerSlots_005e9188[slot * 0x4c + 1];
        // The original reaches this only once the resource directory is loaded. Guarding is not
        // padding: a null here faults natively inside the emitters below, several frames later,
        // with nothing naming the cause.
        byte** atlasSlot = mem.s_MainResourceBuffer_005e97c0 != null
            ? *(byte***)(mem.s_MainResourceBuffer_005e97c0 + 0x94)
            : null;
        if (atlasSlot == null || *atlasSlot == null)
        {
            throw new InvalidOperationException(
                "FUN_00545340: the UI atlas at DAT_005e97c0+0x94 is not loaded, so the panel has " +
                "no texture metadata to draw from. LoadFirstLevelResources must run first.");
        }
        byte* atlas = *atlasSlot;

        uint tpage = *ui.DecodeSlot(*(ushort*)descriptor);
        uint extent = *(uint*)(descriptor + 8);
        // Second bound, on the value rather than the index: `buttons` addresses the per-controller
        // input table (0x140 bytes, 0x10 per entry) and is used for writes, so it must land inside
        // it. 8 is the original's "no controller" sentinel and is a legitimate value here.
        int controller = inp.s_PlayerSlots_005e9188[slot * 0x4c];
        if (controller * 0x10 + 0x10 > 0x140)
        {
            throw new InvalidOperationException(
                $"FUN_00545340: controller index {controller} for slot {slot} is past the input " +
                "state table at DAT_00604a20. Writing there would corrupt unrelated memory.");
        }
        int buttons = controller * 0x10;
        uint scrollFlags = *(uint*)(descriptor + 0x38);
        int position = *(int*)(descriptor + 4);

        // ---- the scroll arrows, four directions, each with its own repeat rule ----
        if ((scrollFlags & 0x1000000) != 0 && (descriptor[0x8c7] & 0x20) == 0)
        {
            byte* buttonState = inp.s_InputStateBase_00604a20 + buttons;

            if ((sbyte)descriptor[0x8c4] != 0 && (buttonState[6] & 0x10) != 0
                && (descriptor[0x8c7] & 0x40) != 0)
            {
                descriptor[0x8c4] = (byte)((sbyte)descriptor[0x8c4] - 2);
                aud.PlayUiSound3A();
            }
            if ((sbyte)descriptor[0x8c4] != 0 && (buttonState[6] & 0x10) != 0
                && (descriptor[0x8c7] & 0x80) != 0)
            {
                descriptor[0x8c4] = (byte)((sbyte)descriptor[0x8c4] - 3);
                aud.PlayUiSound3A();
            }
            if ((sbyte)descriptor[0x8c4] != 0 && (buttonState[6] & 0x10) != 0
                && (descriptor[0x8c7] & 0xc0) == 0)
            {
                descriptor[0x8c4] = (byte)((sbyte)descriptor[0x8c4] - 1);
                aud.PlayUiSound3A();
            }
            if ((int)(uint)descriptor[0x8c4] < (int)(descriptor[0x11] - 2)
                && (buttonState[6] & 0x40) != 0 && (descriptor[0x8c7] & 0x40) != 0)
            {
                descriptor[0x8c4] = (byte)(descriptor[0x8c4] + 2);
                aud.PlayUiSound3A();
            }
            if ((int)(uint)descriptor[0x8c4] < (int)(descriptor[0x11] - 3)
                && (buttonState[6] & 0x40) != 0 && (descriptor[0x8c7] & 0x80) != 0)
            {
                descriptor[0x8c4] = (byte)(descriptor[0x8c4] + 3);
                aud.PlayUiSound3A();
            }
            if ((int)(uint)descriptor[0x8c4] < (int)(descriptor[0x11] - 1)
                && (buttonState[6] & 0x40) != 0 && (descriptor[0x8c7] & 0xc0) == 0)
            {
                descriptor[0x8c4] = (byte)(descriptor[0x8c4] + 1);
                aud.PlayUiSound3A();
            }

            // Confirm: latch the selection back into the per-panel table and clear the edge.
            ushort edge = *(ushort*)(inp.s_InputStateBase_00604a20 + buttons + 2);
            if ((edge & 0x8000) != 0)
            {
                *(ushort*)(inp.s_InputStateBase_00604a20 + buttons + 2) = (ushort)(edge & 0x7fff);
                byte flags = descriptor[0x8c7];
                descriptor[0x3b] = 0;
                descriptor[0x8c7] = (byte)(flags & 0xfb);

                if (s_TraceChoice)
                {
                    Console.Error.WriteLine(
                        $"[CHOIX] descripteur 0x{(nuint)descriptor:x} curseur=+0x8c4:{descriptor[0x8c4]}" +
                        $" creneau=+0x8c5:{descriptor[0x8c5]} drapeaux=0x{flags:x2}" +
                        $" lignes=+0x11:{descriptor[0x11]}");
                }

                if ((flags & 0x40) != 0)
                    mem.s_PanelSelection_005e9060[descriptor[0x8c5]] = (byte)(descriptor[0x8c4] >> 1);
                if ((descriptor[0x8c7] & 0x80) != 0)
                    mem.s_PanelSelection_005e9060[descriptor[0x8c5]] = (byte)(descriptor[0x8c4] / 3);
                if ((descriptor[0x8c7] & 0xc0) == 0)
                    mem.s_PanelSelection_005e9060[descriptor[0x8c5]] = descriptor[0x8c4];

                aud.PlayUiSound38();
                descriptor[0x8c7] &= 0x3f;
            }

            uint clut = *(uint*)(descriptor + 0x3c);
            uint arrow = scrollFlags + (uint)((descriptor[0x8c4] + 0x155555) * 0xc00);
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, arrow, clut,
                              *(uint*)(atlas + 0xd8 + (uint)slotIndex1 * 8));
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, arrow + 0x400, clut,
                              *(uint*)(atlas + 0xc0));
        }

        // ---- the confirm cursor ----
        uint cursorFlags = *(uint*)(descriptor + 0x30);
        if ((cursorFlags & 0x1000000) != 0 && (descriptor[0x8c7] & 0x20) == 0)
        {
            ushort edge = *(ushort*)(inp.s_InputStateBase_00604a20 + buttons + 2);
            if ((edge & 0x8000) != 0)
            {
                *(ushort*)(inp.s_InputStateBase_00604a20 + buttons + 2) = (ushort)(edge & 0x7fff);
                descriptor[0x33] = 0;
                descriptor[0x8c7] &= 0xfd;
                aud.PlayUiSound38();
            }

            uint clut = *(uint*)(descriptor + 0x34);
            uint packed = (cursorFlags & 0xffff0000) | (byte)cursorFlags | (((extent >> 0x10) & 0xff) << 8);
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, packed + 0x40c, clut,
                              *(uint*)(atlas + 0xd8 + (uint)slotIndex1 * 8));
            prim = EmitSprite(otEntry, prim, tpage, (uint)position, packed, clut,
                              *(uint*)(atlas + (s_FadeCounter_005e9960 >> 1 & 3) * 8));
        }

        // ---- up to four decorations ----
        uint* cursor = prim;
        byte* slotDesc = descriptor + 0x48;
        byte* slotAtlas = atlas + 0x20;
        for (int i = 4; i != 0; i--)
        {
            uint flags = *(uint*)slotDesc;
            if ((flags & 0x1000000) != 0)
            {
                cursor = EmitSprite(otEntry, cursor, tpage, (uint)position, flags,
                                    *(uint*)(slotDesc + 4), *(uint*)slotAtlas);
                prim = cursor;
            }
            slotAtlas += 8;
            slotDesc += 8;
        }

        // ---- the frame: four corners then four edges ----
        uint frame = *(uint*)(descriptor + 0x40);
        if ((frame & 0x1000000) != 0)
        {
            uint clut = *(uint*)(descriptor + 0x44);
            prim = EmitSprite(otEntry, cursor, tpage, (uint)position, frame, clut, *(uint*)(atlas + 0x60));
            uint width = extent & 0xffff;
            prim = EmitSprite(otEntry, prim, tpage, width + (uint)position, frame, clut, *(uint*)(atlas + 0x68));
            uint height = extent & 0xffff0000;
            uint farCorner = width + (uint)position + height;
            prim = EmitSprite(otEntry, prim, tpage, farCorner, frame, clut, *(uint*)(atlas + 0x78));
            prim = EmitSprite(otEntry, prim, tpage, farCorner - width, frame, clut, *(uint*)(atlas + 0x70));

            uint horizontal = (extent - 0x10) & 0xffff | 0x100000;
            prim = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x10, frame, clut,
                                    *(uint*)(atlas + 0x40), horizontal);
            prim = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x10 + height, frame, clut,
                                    *(uint*)(atlas + 0x48), horizontal);
            uint vertical = height - 0x100000 | 0x10;
            prim = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x100000, frame, clut,
                                    *(uint*)(atlas + 0x50), vertical);
            cursor = EmitTintedSprite(otEntry, prim, tpage, (uint)position + 0x100000 + width, frame, clut,
                                      *(uint*)(atlas + 0x58), vertical);
            prim = cursor;
        }

        // ---- the text: copy the display list FUN_005443a0 built inside the descriptor,
        //      offsetting each sprite by the panel's position. The 24-bit link mask again.
        if ((*(uint*)(descriptor + 0x20) & 0x1000000) != 0)
        {
            uint link = *(uint*)(descriptor + 0xa4);
            int guard = 0;
            for (uint* node = (uint*)(link & 0xffffff); node != (uint*)0xffffff; node = (uint*)(link & 0xffffff))
            {
                // The original cannot reach a null link: ClearOTagR terminates the descriptor's
                // own ordering table with the 0xffffff sentinel, so the walk always ends. Reaching
                // null here means this reconstruction let the panel be drawn before the text engine
                // ever cleared that table â€” a defect on our side, and one that would otherwise show
                // up as an unexplained native fault.
                if (node == null || ++guard > 0x400)
                {
                    throw new InvalidOperationException(
                        $"FUN_00545340: the panel's ordering table at descriptor+0xa4 is not a " +
                        $"terminated list ({(node == null ? "null link" : "over 1024 nodes")}). It is " +
                        "cleared by control code 0x0c in FUN_005443a0 â€” the text engine must have " +
                        "laid the page out at least once before the panel is drawn.");
                }

                if ((*node & 0xff000000) != 0)
                {
                    cursor[1] = node[1];
                    prim[2] = node[2];
                    prim[3] = node[3] + (uint)position;
                    prim[4] = node[4];
                    prim[5] = node[5];
                    prim[0] = *otEntry | 0x5000000;
                    *otEntry = (uint)(nuint)prim;
                    cursor = prim + 6;
                    prim = cursor;
                }
                link = *node;
            }
        }

        // ---- the panel background ----
        if ((*(uint*)(descriptor + 0x18) & 0x1000000) != 0)
        {
            prim = EmitTintedSprite(otEntry, cursor, tpage, (uint)position,
                                    *(uint*)(descriptor + 0x18), *(uint*)(descriptor + 0x1c),
                                    *(uint*)(atlas + 0x80), extent);
        }

        EmitClipRect(otEntry, &prim);
        return prim;
    }
}
