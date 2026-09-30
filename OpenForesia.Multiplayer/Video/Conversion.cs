// Les conversions de l'image : BGRA (la capture) vers NV12 (l'encodeur), a l'echelle et en
// gardant les proportions ; NV12 (le decodeur) vers BGRA (l'affichage). BT.601 en plage limitee,
// la convention des codecs H.264 de Windows.
//
// NV12 : un plan Y de largeur x hauteur octets, puis un plan UV entrelace de largeur x hauteur/2.
// La mise a l'echelle est au plus proche voisin : l'image du jeu est faite de grands aplats, et
// c'est le moins couteux (quelques millisecondes par image en 960 x 720).

namespace OpenForesia.Multiplayer.Video;

internal static unsafe class Conversion
{
    /// <summary>
    /// Remplit <paramref name="nv12"/> (<paramref name="largeur"/> × <paramref name="hauteur"/>)
    /// avec l'image BGRA source, mise à l'échelle au plus grand qui tienne en gardant ses
    /// proportions ; le reste en noir.
    /// </summary>
    internal static void BgraVersNv12(byte* source, int pas, int sl, int sh, byte* nv12, int largeur, int hauteur)
    {
        // La place de l'image dans la sortie, en coordonnées paires (NV12 regroupe 2 × 2 pixels).
        double echelle = Math.Min((double)largeur / sl, (double)hauteur / sh);
        int w = Math.Max(2, (int)(sl * echelle) & ~1), h = Math.Max(2, (int)(sh * echelle) & ~1);
        int x0 = ((largeur - w) / 2) & ~1, y0 = ((hauteur - h) / 2) & ~1;

        new Span<byte>(nv12, largeur * hauteur).Fill(16);                       // Y noir
        new Span<byte>(nv12 + largeur * hauteur, largeur * hauteur / 2).Fill(128); // UV neutre

        Span<int> colonnes = w <= 4096 ? stackalloc int[w] : new int[w];
        for (int x = 0; x < w; x++) colonnes[x] = Math.Min(sl - 1, (int)((x + 0.5) * sl / w)) * 4;

        byte* plan = nv12 + largeur * hauteur;
        for (int y = 0; y < h; y++)
        {
            byte* ligne = source + Math.Min(sh - 1, (int)((y + 0.5) * sh / h)) * pas;
            byte* sortieY = nv12 + (y0 + y) * largeur + x0;
            for (int x = 0; x < w; x++)
            {
                byte* px = ligne + colonnes[x];
                int b = px[0], g = px[1], r = px[2];
                sortieY[x] = (byte)(((66 * r + 129 * g + 25 * b + 128) >> 8) + 16);
            }

            if ((y & 1) != 0) continue;
            byte* sortieUV = plan + ((y0 + y) / 2) * largeur + x0;
            for (int x = 0; x < w; x += 2)
            {
                byte* px = ligne + colonnes[x];
                int b = px[0], g = px[1], r = px[2];
                sortieUV[x] = (byte)(((-38 * r - 74 * g + 112 * b + 128) >> 8) + 128);
                sortieUV[x + 1] = (byte)(((112 * r - 94 * g - 18 * b + 128) >> 8) + 128);
            }
        }
    }

    /// <summary>Une image NV12 en BGRA (4 octets par pixel, lignes de <c>largeur × 4</c> octets).</summary>
    internal static void Nv12VersBgra(byte* nv12, int largeur, int hauteur, byte* bgra)
    {
        byte* plan = nv12 + largeur * hauteur;
        for (int y = 0; y < hauteur; y++)
        {
            byte* ligneY = nv12 + y * largeur;
            byte* ligneUV = plan + (y / 2) * largeur;
            uint* sortie = (uint*)(bgra + y * largeur * 4);
            // La chrominance vaut pour deux pixels voisins : calculée une fois par paire.
            for (int x = 0; x + 1 < largeur; x += 2)
            {
                int d = ligneUV[x] - 128, e = ligneUV[x + 1] - 128;
                int rouge = 409 * e + 128, vert = -100 * d - 208 * e + 128, bleu = 516 * d + 128;
                int c0 = 298 * (ligneY[x] - 16), c1 = 298 * (ligneY[x + 1] - 16);
                sortie[x] = 0xFF000000u | (uint)Borner((c0 + rouge) >> 8) << 16 | (uint)Borner((c0 + vert) >> 8) << 8 | Borner((c0 + bleu) >> 8);
                sortie[x + 1] = 0xFF000000u | (uint)Borner((c1 + rouge) >> 8) << 16 | (uint)Borner((c1 + vert) >> 8) << 8 | Borner((c1 + bleu) >> 8);
            }
        }
    }

    private static byte Borner(int v) => (byte)(v < 0 ? 0 : v > 255 ? 255 : v);
}
