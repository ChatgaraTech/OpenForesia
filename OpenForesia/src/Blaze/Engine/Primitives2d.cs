// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `Blaze` porte encore les aides que cette classe appelle : EmitClipRect, EmitViewportRect.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// L'emetteur de primitives 2D : les quatre routines qui poussent un polygone plat, un sprite,
/// sa variante et une suite de caracteres dans la liste d'affichage (Ghidra: FUN_00542250 et le
/// sous-arbre de dessin qui le suit).
///
/// <para>
/// Sa signature dit ce dont le dessin 2D depend : <c>GameMemory</c> pour les tables d'entites et
/// les capacites du peripherique, et <c>UiText</c> - parce que <see cref="DrawTextRun"/> fait
/// passer chaque octet de texte par <c>CheatCodeProbe</c>. Ce lien-la existait deja dans le
/// binaire ; il etait simplement invisible tant que tout vivait dans une seule classe.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>.
/// </para>
/// </summary>
internal sealed unsafe class Primitives2d(GameMemory mem, UiText ui)
{

    /// <summary>
    /// Ghidra: the entity-slot initialisation loop at the top of FUN_00542250 â€” four 0x4c-byte
    /// slots, each stamped with its index and a leading 8.
    /// </summary>
    internal void InitialiseEntitySlots()
    {
        byte* slot = mem.s_EntitySlotTable_005e9180;
        for (int i = 0; i < 4; i++, slot += 0x4c)
        {
            slot[0] = 8;
            slot[1] = (byte)i;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051f9c0 @ 0x0051f9c0 â€” links a two-word flat primitive into the ordering table.
    /// The tag word carries the primitive length in its top byte (0x02000000), which is the
    /// PlayStation display-list convention.
    /// </summary>
    internal uint* DrawFlatPrimitive(uint* otEntry, uint* prim, int* sprite)
    {
        uint attributes = *(uint*)(*(int*)sprite + 4 + (short)sprite[4] * 8);

        if (mem.s_DeviceCapFlag_00564a48 && (attributes & 0x200) != 0)
        {
            // Ghidra: DAT_00564a4c, not the ini "Small" setting â€” see DrawSpritePrimitive's note.
            if (mem.s_Unknown_00564a4c == 0) return prim;
            attributes &= 0xfffffdff;
        }

        prim[1] = (uint)*(ushort*)(mem.s_EntityDecodeTable_005e8f00 + ((attributes >> 0x10) & 0xff) * 4)
                  | ((attributes & 0xff) << 5) | 0xe1000000;
        prim[2] = 0;
        *prim = *otEntry | 0x2000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 8;
    }

    /// <summary>
    /// Ghidra: FUN_00521f20 @ 0x00521f20 â€” links a ten-word textured sprite primitive into the
    /// ordering table (tag length 0x09000000). The bit packing is transcribed operation for
    /// operation; the shifts mix a colour, a texture page and four corner coordinates.
    /// </summary>
    internal uint* DrawSpritePrimitive(uint* otEntry, uint* prim, int* sprite, uint flags, uint extra)
    {
        uint* source = (uint*)(*(int*)sprite + (short)sprite[4] * 8);
        uint posX = (uint)sprite[2];
        short halfW = (short)sprite[3];
        short halfH = *(short*)((byte*)sprite + 0xe);
        if (halfW < 0) halfW = (short)-halfW;
        if (halfH < 0) halfH = (short)-halfH;

        uint baseWord = (uint)sprite[1];
        uint colour = source[0];
        uint size = (uint)(halfH * 0x10000 + halfW);
        uint attributes = source[1];

        if (mem.s_DeviceCapFlag_00564a48)
        {
            if ((attributes & 0x200) != 0)
            {
                // Ghidra: DAT_00564a4c both times here â€” confirmed against FUN_00521f20. This
                // function previously read s_SmallTextureFlag (the ini "Small" setting) instead,
                // which stayed 0 by default and silently dropped every sprite that took this path
                // â€” including the character-select carousel's portrait cards. Found 2026-08-16
                // while chasing exactly that: DrawSpriteVariant already had the correct symbol,
                // this sibling and DrawFlatPrimitive did not.
                if (mem.s_Unknown_00564a4c == 0) return prim;
                attributes &= 0xfffffdff;
            }
            if ((flags & 2) != 0)
            {
                if (mem.s_Unknown_00564a4c == 0) return prim;
                flags = (byte)flags & 0xfdu;
            }
        }

        uint alpha = (colour >> 0x10) & 0xff;
        if (alpha == 0) alpha = 0xff;

        uint scaledW = alpha * (uint)(short)size;
        uint scaledH = (size >> 0x10) * (colour >> 0x18);
        uint clut = *(uint*)(mem.s_EntityDecodeTable_005e8f00 + ((attributes >> 0x10) & 0xff) * 4);

        *prim = *otEntry | 0x9000000;
        *otEntry = (uint)(nuint)prim;

        prim[1] = ((attributes & 0xffffff00) | (((flags & 0xff) | 0x2c) << 8)) << 0x10 | (baseWord & 0x3ffffff);

        uint alphaSum = alpha + (colour & 0xff);
        uint packed = (colour >> 0x18) * 0x100 + (colour & 0xff00);
        uint left = (posX - (scaledW >> 0xd)) & 0xffff;
        uint top = (posX & 0xffff0000) + (scaledH >> 0xd) * 0xffff0000;
        uint right = (left + (scaledW >> 0xc)) & 0xffff;
        uint bottom = (scaledH >> 0xc) * 0x10000 + top;

        prim[2] = top | left;
        prim[3] = (uint)(((((ushort)((short)extra >> 2)) & 0xffc0) + (extra & 0xff)) * 0x10000)
                  + (clut & 0xffff0000) + ((attributes >> 8) & 0xff0000) | (colour & 0xffff);
        prim[4] = top | right;
        prim[5] = (((attributes & 0xff) << 5) | clut) << 0x10 | alphaSum | (colour & 0xff00);
        prim[6] = left | bottom;
        prim[8] = bottom | right;
        prim[7] = packed | (colour & 0xff);
        prim[9] = packed | alphaSum;

        return prim + 10;
    }

    /// <summary>
    /// Ghidra: FUN_00521df0 @ 0x00521df0 — la sœur courte de <see cref="DrawSpritePrimitive"/> :
    /// une primitive de **cinq** mots (longueur de tag 0x04000000) au lieu de dix. Même en-tête —
    /// même lecture du couple couleur/attributs à <c>sprite[0] + (short)sprite[4] * 8</c>, même
    /// garde sur les deux drapeaux — mais elle ne pose qu'un seul coin au lieu de quatre.
    ///
    /// <para>Le champ d'échelle se lit à <c>(colour >> 0x10) &amp; 0xff</c> et **0 y signifie
    /// 0x100**, pas 0xff : la sœur longue teste la même chose mais retombe sur 0xff. L'écart est
    /// dans l'original, transcrit tel quel.</para>
    ///
    /// <para>TODO(REVERSE) : le second drapeau est <c>DAT_00564a4c</c>, qui n'a pas de symbole
    /// reconstruit. <see cref="DrawSpritePrimitive"/> lui substitue <c>s_SmallTextureFlag</c>, mais
    /// celui-ci est <c>DAT_00564a38</c> (soit <c>AU_00564a05._51_4_</c>) — deux adresses
    /// différentes. Plutôt que de propager un rapprochement douteux, on déclare ici le symbole
    /// propre ; la divergence est à trancher, et elle touche aussi la sœur longue.</para>
    /// </summary>
    internal uint* DrawSpriteVariant(uint* otEntry, uint* prim, int* sprite, uint flags, uint extra)
    {
        uint* source = (uint*)(*(int*)sprite + (short)sprite[4] * 8);
        uint colour = source[0];
        uint posX = (uint)sprite[2];
        uint attributes = source[1];

        if (mem.s_DeviceCapFlag_00564a48)
        {
            if ((attributes & 0x200) != 0)
            {
                if (mem.s_Unknown_00564a4c == 0) return prim;
                attributes &= 0xfffffdff;
            }
            if ((flags & 2) != 0)
            {
                if (mem.s_Unknown_00564a4c == 0) return prim;
                flags = (byte)flags & 0xfdu;
            }
        }

        uint scale = (colour >> 0x10) & 0xff;
        if ((short)scale == 0) scale = 0x100;

        // Ghidra: &DAT_005e8f02 — la moitié haute de l'entrée de la table de pages, d'où le +2 sur
        // la même table que DrawSpritePrimitive indexe à &DAT_005e8f00.
        ushort page = *(ushort*)(mem.s_EntityDecodeTable_005e8f00 + 2 + ((attributes >> 0x10) & 0xff) * 4);

        prim[1] = ((attributes >> 8) | (flags & 0xff) | 100) << 0x18 | ((uint)sprite[1] & 0x3ffffff);
        prim[3] = (uint)(((((ushort)((short)extra >> 2)) & 0xffc0) | (extra & 0xff)) + page + (attributes >> 0x18))
                  * 0x10000 | (colour & 0xffff);
        prim[2] = posX;
        prim[4] = (colour >> 0x18) << 0x10 | scale;

        *prim = *otEntry | 0x4000000;
        *otEntry = (uint)(nuint)prim;

        return prim + 5;
    }

    /// <summary>
    /// Ghidra: FUN_0051c120 @ 0x0051c120 — draws a run of bytes from a nibble-indexed glyph atlas:
    /// the low nibble of each character selects the atlas column, the high nibble the row. Called
    /// from roughly 50 sites across the game (menus, HUD, the party screen) — it is the flat,
    /// unwrapped text primitive, distinct from the wrapping/paginating dialog-box engine built on
    /// <see cref="EmitTextSprite"/>. Each character is also probed for the cheat-code sequence via
    /// <see cref="CheatCodeProbe"/>.
    /// <para>
    /// <c>cursor</c> is a caller-built 3-int block, resolved from the raw disassembly (Ghidra's
    /// pseudo-C names it <c>int *param_3</c> without revealing the layout):
    /// cursor[0] = pointer to a 2-dword atlas descriptor — dword0 packs origin X (low byte),
    /// origin Y (byte 1), cell width (byte 2, i.e. <c>&gt;&gt;0x14</c>) and cell height (top 5 bits,
    /// <c>&gt;&gt;0x1b</c>); dword1 is forwarded verbatim as the "attributes" half of the sprite
    /// passed to <see cref="DrawSpriteVariant"/>. cursor[1] is forwarded as that sprite's
    /// texture/flags dword. cursor[2] is the starting screen position, X packed in the low word
    /// and Y in the high word — only the low word (X) advances as characters are drawn, matching
    /// the packed-position convention documented on <see cref="DrawSpritePrimitive"/>; neither the
    /// advanced X nor the loop's own local copy is written back to the caller's cursor[2].
    /// </para>
    /// <para>
    /// The on-stack "sprite" Ghidra shows as <c>local_14 = &amp;local_1c</c> is a pointer-to-a-
    /// pointer in the real stack layout (confirmed in <c>Legacy/Raw/Blaze/FUN_0051c120.asm</c>):
    /// the struct that <see cref="DrawSpriteVariant"/>/<see cref="DrawFlatPrimitive"/> actually read
    /// sits 8 bytes below the pointer that gets passed, and the intervening slot holds a
    /// self-reference back to that struct's own address — reproduced here with <c>frame[2]</c>.
    /// </para>
    /// <para>
    /// Previously mis-stubbed as a single-purpose "controller banner" helper
    /// (<c>ControllerBannerStub</c>) after only its first call site (<see cref="FrameEnd"/>'s
    /// "Veuillez connecter la manette !" message) had been read; this is the real, generic
    /// primitive underneath it.
    /// </para>
    /// </summary>
    internal uint* DrawTextRun(uint* otEntry, uint* prim, int* cursor, byte* text, uint flags, uint extra, int maxChars)
    {
        UiWin.EmitViewportRect(otEntry, &prim);

        uint* atlas = (uint*)*cursor;
        uint packing = *atlas;
        uint attributes = atlas[1];

        byte originX = (byte)packing;
        byte originY = (byte)(packing >> 8);
        byte cellW = (byte)(packing >> 0x14);
        byte cellH = (byte)(packing >> 0x1b);

        uint* frame = stackalloc uint[7];
        frame[1] = attributes;
        frame[2] = (uint)(nuint)frame;
        frame[3] = (uint)cursor[1];
        frame[4] = (uint)cursor[2];
        frame[6] = 0;

        byte ch = *text;
        byte* p = text;
        int remaining = maxChars;

        if (ch != 0)
        {
            while (remaining != 0)
            {
                byte x = (byte)((ch & 0xf) * cellW + originX);
                byte y = (byte)((ch >> 4) * cellH + originY);
                frame[0] = (uint)x | ((uint)y << 8) | 0x07070000u;

                prim = DrawSpriteVariant(otEntry, prim, (int*)(frame + 2), flags, extra);

                frame[4] = (frame[4] & 0xffff0000u) | (uint)(ushort)((short)(ushort)frame[4] + cellW);

                ui.CheatCodeProbe(*p);

                ch = p[1];
                if (ch == 0) break;
                p++;
                remaining--;
            }
        }

        prim = DrawFlatPrimitive(otEntry, prim, (int*)(frame + 2));
        UiWin.EmitClipRect(otEntry, &prim);

        return prim;
    }
}
