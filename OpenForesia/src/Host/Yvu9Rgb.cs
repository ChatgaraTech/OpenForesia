// =====================================================================
// YVU9 -> DIB BGR24 : la conversion de couleur des cinématiques — notre échafaudage.
//
// `Host/Indeo4/` rend du YVU9, la sortie native d'Indeo ; `StretchDIBits` veut du RGB. Le codec
// d'Intel faisait cette conversion lui-même, et on la refait comme lui, à une chose près :
//
//   - BT.601 plage limitée (Y 16..235), comme Intel ;
//   - la chrominance, au quart dans chaque sens, est **répétée** sur son bloc 4×4, comme Intel. Une
//     interpolation bilinéaire a été essayée : plus douce sur les bords colorés, mais elle s'écartait
//     d'Intel jusqu'à 153 niveaux sur ces bords. Ce que le jeu montrait, c'est la répétition ;
//   - **pas de tramage.** Intel tramait (motif ordonné 4×4) pour les écrans 8 et 16 bits de 1998 ;
//     en vraies couleurs, ce n'est que du bruit, et rien dans `Blaze.exe` ne dépend de ces octets.
//
// Mesure : `OpenForesia.Tools indeo <film.avi> --rgb` compare au RGB24 d'Intel ; l'écart ne dépasse
// pas l'amplitude du tramage. Voir `Documentation/DLL/ir41_32.md`.
// =====================================================================

namespace OpenForesia.Blaze;

internal static class Yvu9Rgb
{
    private static byte Clip(int v) => (byte)(v < 0 ? 0 : v > 255 ? 255 : v);

    /// <summary>
    /// Écrit <paramref name="image"/> dans <paramref name="dib"/>, une DIB 24 bits en BGR rangée du
    /// bas vers le haut, de pas <paramref name="stride"/> — ce que <c>StretchDIBits</c> attend avec
    /// une hauteur positive.
    /// </summary>
    internal static void ToBottomUpBgr(Yvu9Image image, byte[] dib, int stride)
    {
        int w = image.Width, h = image.Height, cw = image.ChromaWidth;

        for (int y = 0; y < h; y++)
        {
            int src = y * w;
            int chroma = (y >> 2) * cw;
            int dst = (h - 1 - y) * stride;

            for (int x = 0; x < w; x++, dst += 3)
            {
                int du = image.U[chroma + (x >> 2)] - 128;
                int dv = image.V[chroma + (x >> 2)] - 128;

                // BT.601, plage limitée, en 256es : 1,164 / 1,596 / 0,391 / 0,813 / 2,018.
                int luma = 298 * (image.Y[src + x] - 16);
                dib[dst + 0] = Clip((luma + 516 * du + 128) >> 8);             // B
                dib[dst + 1] = Clip((luma - 100 * du - 208 * dv + 128) >> 8);  // G
                dib[dst + 2] = Clip((luma + 409 * dv + 128) >> 8);             // R
            }
        }
    }
}
