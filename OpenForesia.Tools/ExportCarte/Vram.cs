namespace ExportCarte;

/// <summary>
/// La mémoire vidéo de la PlayStation, 1024 × 512 mots de 16 bits, remplie comme le jeu la remplit
/// à l'entrée d'une sous-zone, et les textures qu'on en découpe.
///
/// <para>
/// Miroir de : <c>DecodeResourceChunk</c> (<c>Engine/Resources.cs</c>, FUN_005322f0). Le jeu ne
/// respecte <b>pas</b> les positions que l'en-tête TIM déclare : il les remplace par celles que
/// dicte le descripteur de trois octets qui accompagne l'entrée d'archive —
/// image en <c>x = (meta[0] &amp; 0xf) × 64</c>, <c>y = ((meta[0] &amp; 0x10) + meta[2] × 4) × 16</c>,
/// palette en <c>x = 0</c>, <c>y = meta[1] + 0x1c0</c>, rangée sur 256 mots de large. Puis il range
/// dans la table de décodage (<c>0x005e8f00</c>) un couple page de texture / palette, que les faces
/// désignent par son rang.
/// </para>
/// </summary>
internal sealed class Vram
{
    public const int Width = 1024, Height = 512;
    public readonly ushort[] Words = new ushort[Width * Height];

    /// <summary>Ce qui a été écrit, pour reconnaître une face qui lit une région vide.</summary>
    public readonly bool[] Written = new bool[Width * Height];

    /// <summary>La table de décodage : rang → (page de texture, palette), dans l'ordre de chargement.</summary>
    public readonly List<(ushort TPage, ushort Clut)> Slots = new();

    /// <summary>
    /// Charge une TIM au rang suivant de la table. Rend faux si l'en-tête n'est pas une TIM.
    /// </summary>
    public bool LoadTim(byte[] tim, byte meta0, byte meta1, byte meta2)
    {
        if (tim.Length < 8 || BitConverter.ToUInt32(tim, 0) != 0x10) { Slots.Add((0, 0)); return false; }
        uint flags = BitConverter.ToUInt32(tim, 4);
        int off = 8;
        // ReadTIM (ordinal 662) : le mode vaut flags & 7, ou 2 quand la TIM n'a pas de palette.
        uint mode = flags & 7;
        bool hasClut = (flags & 8) != 0;
        if (!hasClut) mode = 2;

        if (hasClut)
        {
            int len = BitConverter.ToInt32(tim, off);
            int w = BitConverter.ToUInt16(tim, off + 8), h = BitConverter.ToUInt16(tim, off + 10);
            // Miroir : clutRect = {0, meta1 + 0x1c0, 256, round256(w*h)/256}.
            int packed = (w * h + 0xff) & ~0xff;
            int y0 = meta1 + 0x1c0;
            int data = off + 12;
            for (int i = 0; i < w * h && data + i * 2 + 1 < tim.Length; i++)
            {
                int x = i & 0xff, y = y0 + (i >> 8);
                if (y >= Height) break;
                Put(x, y, BitConverter.ToUInt16(tim, data + i * 2));
            }
            _ = packed;
            off += len;
        }

        int px = 0, py = 0;
        if (off + 12 <= tim.Length)
        {
            int w = BitConverter.ToUInt16(tim, off + 8), h = BitConverter.ToUInt16(tim, off + 10);
            if (meta0 != 0x1c)
            {
                px = (meta0 & 0x0f) << 6;
                py = ((meta0 & 0x10) + meta2 * 4) << 4;
                int data = off + 12;
                for (int y = 0; y < h; y++)
                    for (int x = 0; x < w; x++)
                    {
                        int at = data + (y * w + x) * 2;
                        if (at + 1 >= tim.Length) break;
                        Put(px + x, py + y, BitConverter.ToUInt16(tim, at));
                    }
            }
            else
            {
                px = BitConverter.ToUInt16(tim, off + 4);
                py = BitConverter.ToUInt16(tim, off + 6);
            }
        }

        // Miroir : le mot de page, à la façon de GetTPage, opération par opération.
        int tpage = ((px >> 2) & 0xf0) | (py & 0x100);
        tpage |= 0x2000;
        tpage = (short)tpage >> 4;
        tpage |= ((int)mode & 3) << 7;
        ushort clut = (ushort)((meta1 + 0x1c0) << 6);
        Slots.Add(((ushort)tpage, clut));
        return true;
    }

    /// <summary>
    /// Une TIM posee a des rectangles imposes : palette rangee lineairement sur
    /// <paramref name="clutLargeur"/> mots de large a partir de (<paramref name="clutX"/>,
    /// <paramref name="clutY"/>), image en (<paramref name="px"/>, <paramref name="py"/>). Range le
    /// couple page / palette a l'emplacement <paramref name="slot"/> de la table. Miroir de :
    /// la seconde texture de LoadPartyMemberResources (Screens/DebugParty.cs), qui ne passe pas par
    /// DecodeResourceChunk.
    /// </summary>
    public void LoadTimAt(byte[] tim, int slot, int clutX, int clutY, int clutLargeur, int px, int py)
    {
        if (tim.Length < 8 || BitConverter.ToUInt32(tim, 0) != 0x10) return;
        uint flags = BitConverter.ToUInt32(tim, 4);
        int off = 8;
        uint mode = (flags & 8) != 0 ? flags & 7 : 2;
        if ((flags & 8) != 0)
        {
            int len = BitConverter.ToInt32(tim, off);
            int w = BitConverter.ToUInt16(tim, off + 8), h = BitConverter.ToUInt16(tim, off + 10);
            for (int i = 0; i < w * h && off + 12 + i * 2 + 1 < tim.Length; i++)
                Put(clutX + i % clutLargeur, clutY + i / clutLargeur, BitConverter.ToUInt16(tim, off + 12 + i * 2));
            off += len;
        }
        if (off + 12 <= tim.Length)
        {
            int w = BitConverter.ToUInt16(tim, off + 8), h = BitConverter.ToUInt16(tim, off + 10);
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    int at = off + 12 + (y * w + x) * 2;
                    if (at + 1 >= tim.Length) break;
                    Put(px + x, py + y, BitConverter.ToUInt16(tim, at));
                }
        }
        int tpage = ((px >> 2) & 0xf0) | (py & 0x100);
        tpage |= 0x2000;
        tpage = (short)tpage >> 4;
        tpage |= ((int)mode & 3) << 7;
        while (Slots.Count <= slot) Slots.Add((0, 0));
        Slots[slot] = ((ushort)tpage, (ushort)(clutY << 6 | (clutX >> 4)));
    }

    /// <summary>Comme <see cref="LoadTim"/>, mais range le couple a un emplacement impose.</summary>
    public void LoadTimSlot(byte[] tim, int slot, byte meta0, byte meta1, byte meta2)
    {
        int avant = Slots.Count;
        LoadTim(tim, meta0, meta1, meta2);
        var pair = Slots[^1];
        Slots.RemoveAt(Slots.Count - 1);
        while (Slots.Count <= slot) Slots.Add((0, 0));
        Slots[slot] = pair;
        _ = avant;
    }

    private void Put(int x, int y, ushort v)
    {
        x &= Width - 1;
        if ((uint)y >= Height) return;
        Words[y * Width + x] = v;
        Written[y * Width + x] = true;
    }

    public ushort At(int x, int y) => Words[(y & (Height - 1)) * Width + (x & (Width - 1))];

    /// <summary>
    /// La couleur d'un texel, comme le GPU la lit : page (<c>tpage</c>, bits 0-3 x/64, bit 4 y/256,
    /// bits 7-8 profondeur) et palette (<c>clut</c>, bits 0-5 x/16, bits 6-14 y). Rend aussi si le
    /// texel tombe dans une région que rien n'a écrite.
    /// </summary>
    public ushort Texel(ushort tpage, ushort clut, int u, int v, out bool known)
    {
        int tx = (tpage & 0xf) * 64, ty = ((tpage >> 4) & 1) * 256;
        int depth = (tpage >> 7) & 3;
        int cx = (clut & 0x3f) * 16, cy = (clut >> 6) & 0x1ff;
        u &= 0xff; v &= 0xff;
        int y = ty + v;
        switch (depth)
        {
            case 0:
            {
                int wx = tx + (u >> 2);
                ushort w = At(wx, y);
                int idx = (w >> ((u & 3) * 4)) & 0xf;
                known = Written[(y & 511) * Width + (wx & 1023)] && Written[(cy & 511) * Width + ((cx + idx) & 1023)];
                return At(cx + idx, cy);
            }
            case 1:
            {
                int wx = tx + (u >> 1);
                ushort w = At(wx, y);
                int idx = (w >> ((u & 1) * 8)) & 0xff;
                known = Written[(y & 511) * Width + (wx & 1023)] && Written[(cy & 511) * Width + ((cx + idx) & 1023)];
                return At(cx + idx, cy);
            }
            default:
                known = Written[(y & 511) * Width + ((tx + u) & 1023)];
                return At(tx + u, y);
        }
    }

    /// <summary>
    /// Une page de 256 × 256 texels en RGBA, telle qu'une face la voit. Le mot <c>0x0000</c> est
    /// transparent (la règle de la PlayStation) ; un texel lu dans une région vide est magenta.
    /// </summary>
    public byte[] Page(ushort tpage, ushort clut, out int unknownTexels)
    {
        var rgba = new byte[256 * 256 * 4];
        unknownTexels = 0;
        for (int v = 0; v < 256; v++)
            for (int u = 0; u < 256; u++)
            {
                ushort c = Texel(tpage, clut, u, v, out bool known);
                int o = (v * 256 + u) * 4;
                if (!known) { rgba[o] = 255; rgba[o + 1] = 0; rgba[o + 2] = 255; rgba[o + 3] = 255; unknownTexels++; continue; }
                if (c == 0) continue;   // transparent
                rgba[o] = (byte)((c & 31) << 3 | (c & 31) >> 2);
                rgba[o + 1] = (byte)(((c >> 5) & 31) << 3 | ((c >> 5) & 31) >> 2);
                rgba[o + 2] = (byte)(((c >> 10) & 31) << 3 | ((c >> 10) & 31) >> 2);
                rgba[o + 3] = 255;
            }
        return rgba;
    }
}
