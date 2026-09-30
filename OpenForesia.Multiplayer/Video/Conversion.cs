// Les conversions de l'image : BGRA (la capture) vers NV12 (l'encodeur), recadree et mise a
// l'echelle ; NV12 (le decodeur) vers BGRA (l'affichage). BT.601 en plage limitee, la convention des
// codecs H.264 de Windows.
//
// NV12 : un plan Y de largeur x hauteur octets, puis un plan UV entrelace de largeur x hauteur/2.
//
// Le recadrage : en fenetre sans bordure, la fenetre du jeu couvre tout l'ecran (16:9) et le jeu y
// dessine son image 4:3 au milieu, entre deux bandes noires. On ne garde que le plus grand rectangle
// centre aux proportions du flux : sans lui, l'image utile n'occupait que les trois quarts du flux
// (720 x 540 dans du 960 x 720), et l'invite la voyait encadree de noir sur les quatre cotes.
//
// La mise a l'echelle est un filtre triangle separable, elargi au rapport de reduction (celui de
// Pillow, « bilinear » avec anticrenelage) : chaque pixel de la sortie moyenne les pixels de la source
// qu'il couvre. Au plus proche voisin, comme au premier jet, une reduction de 1440 a 960 sautait une
// colonne sur trois : les traits d'un pixel des textes disparaissaient ou doublaient au hasard. Les
// deux passes vont par quatre canaux a la fois (SSE4.1).
//
// La chrominance, commune a 2 x 2 pixels en NV12, est leur moyenne, et non plus celle du premier.

using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace OpenForesia.Multiplayer.Video;

internal static unsafe class Conversion
{
    private const int Bits = 14, Un = 1 << Bits;

    /// <summary>Les poids d'une passe : pour chaque pixel de la sortie, le premier pixel lu, leur nombre, et les poids (sur <see cref="Un"/>).</summary>
    private sealed class Filtre
    {
        internal required int Source, Sortie, Pas;
        internal required int[] Debut, Nombre, Poids;
    }

    [ThreadStatic] private static Filtre? t_FiltreX, t_FiltreY;
    [ThreadStatic] private static int[]? t_Colonne, t_Ligne, t_LignePrecedente;

    private static Filtre Calculer(int source, int sortie)
    {
        double echelle = (double)source / sortie, support = Math.Max(1.0, echelle);
        int pas = (int)Math.Ceiling(2 * support) + 2;
        var f = new Filtre
        {
            Source = source, Sortie = sortie, Pas = pas,
            Debut = new int[sortie], Nombre = new int[sortie], Poids = new int[sortie * pas],
        };
        Span<double> w = stackalloc double[pas];
        for (int i = 0; i < sortie; i++)
        {
            double centre = (i + 0.5) * echelle;
            int debut = Math.Max(0, (int)Math.Floor(centre - support));
            int fin = Math.Min(source, (int)Math.Ceiling(centre + support));
            double somme = 0;
            int premier = -1, dernier = -1;
            for (int j = 0; j < fin - debut; j++)
            {
                double d = Math.Abs((debut + j + 0.5 - centre) / support);
                w[j] = d < 1 ? 1 - d : 0;
                somme += w[j];
                if (w[j] > 0) { if (premier < 0) premier = j; dernier = j; }
            }
            if (premier < 0)
            {
                premier = dernier = Math.Clamp((int)centre - debut, 0, fin - debut - 1);
                w[premier] = somme = 1;
            }

            // En virgule fixe ; l'arrondi va au poids le plus fort, pour que la somme fasse exactement Un.
            int total = 0, fort = 0;
            for (int j = premier; j <= dernier; j++)
            {
                int p = (int)Math.Round(w[j] / somme * Un);
                f.Poids[i * pas + j - premier] = p;
                total += p;
                if (p > f.Poids[i * pas + fort]) fort = j - premier;
            }
            f.Poids[i * pas + fort] += Un - total;
            f.Debut[i] = debut + premier;
            f.Nombre[i] = dernier - premier + 1;
        }
        return f;
    }

    private static Filtre Obtenir(ref Filtre? cache, int source, int sortie) =>
        cache is { } f && f.Source == source && f.Sortie == sortie ? f : cache = Calculer(source, sortie);

    /// <summary>Quatre octets (B, G, R, A) en quatre entiers.</summary>
    private static Vector128<int> Charger(byte* p) =>
        Sse41.IsSupported ? Sse41.ConvertToVector128Int32(p) : Vector128.Create((int)p[0], p[1], p[2], p[3]);

    /// <summary>
    /// Remplit <paramref name="nv12"/> (<paramref name="largeur"/> × <paramref name="hauteur"/>)
    /// avec l'image BGRA source, recadrée aux proportions du flux puis mise à l'échelle ; le reste
    /// (des arrondis, au plus deux pixels) en noir.
    /// </summary>
    internal static void BgraVersNv12(byte* source, int pas, int sl, int sh, byte* nv12, int largeur, int hauteur)
    {
        // Le recadrage, centré, aux proportions de la sortie.
        int cl = sl, ch = sh;
        if ((long)sl * hauteur > (long)sh * largeur) cl = Math.Max(1, (int)((long)sh * largeur / hauteur));
        else ch = Math.Max(1, (int)((long)sl * hauteur / largeur));
        source += (sh - ch) / 2 * pas + (sl - cl) / 2 * 4;
        sl = cl;
        sh = ch;

        // La place de l'image dans la sortie, en coordonnées paires (NV12 regroupe 2 × 2 pixels).
        double echelle = Math.Min((double)largeur / sl, (double)hauteur / sh);
        int w = Math.Min(largeur, Math.Max(2, (int)Math.Round(sl * echelle)) & ~1);
        int h = Math.Min(hauteur, Math.Max(2, (int)Math.Round(sh * echelle)) & ~1);
        int x0 = ((largeur - w) / 2) & ~1, y0 = ((hauteur - h) / 2) & ~1;

        if (w != largeur || h != hauteur)
        {
            new Span<byte>(nv12, largeur * hauteur).Fill(16);                       // Y noir
            new Span<byte>(nv12 + largeur * hauteur, largeur * hauteur / 2).Fill(128); // UV neutre
        }

        Filtre fx = Obtenir(ref t_FiltreX, sl, w), fy = Obtenir(ref t_FiltreY, sh, h);
        if (t_Colonne == null || t_Colonne.Length < sl * 4) t_Colonne = new int[sl * 4];
        if (t_Ligne == null || t_LignePrecedente == null || t_Ligne.Length < w * 3)
        {
            t_Ligne = new int[w * 3];
            t_LignePrecedente = new int[w * 3];
        }
        int[] ligne = t_Ligne, precedente = t_LignePrecedente;
        var arrondi = Vector128.Create(Un / 2);

        byte* plan = nv12 + largeur * hauteur;
        fixed (int* colonne = t_Colonne, poidsX = fx.Poids, poidsY = fy.Poids)
        {
            for (int y = 0; y < h; y++)
            {
                // Passe verticale : la ligne y de la sortie, encore à la largeur de la source.
                byte* debutY = source + fy.Debut[y] * pas;
                int ny = fy.Nombre[y];
                int* py = poidsY + y * fy.Pas;
                for (int x = 0; x < sl; x++)
                {
                    byte* p = debutY + x * 4;
                    Vector128<int> acc = Charger(p) * py[0];
                    for (int k = 1; k < ny; k++) acc += Charger(p + k * pas) * py[k];
                    ((acc + arrondi) >> Bits).Store(colonne + x * 4);
                }

                // Passe horizontale, puis la luminance.
                byte* sortieY = nv12 + (y0 + y) * largeur + x0;
                for (int x = 0; x < w; x++)
                {
                    int* c = colonne + fx.Debut[x] * 4;
                    int nx = fx.Nombre[x];
                    int* px = poidsX + x * fx.Pas;
                    Vector128<int> acc = Vector128.Load(c) * px[0];
                    for (int k = 1; k < nx; k++) acc += Vector128.Load(c + k * 4) * px[k];
                    acc = (acc + arrondi) >> Bits;
                    int b = acc.GetElement(0), g = acc.GetElement(1), r = acc.GetElement(2);
                    sortieY[x] = (byte)(((66 * r + 129 * g + 25 * b + 128) >> 8) + 16);
                    ligne[3 * x] = b;
                    ligne[3 * x + 1] = g;
                    ligne[3 * x + 2] = r;
                }

                // La chrominance, une fois par paire de lignes : la moyenne des 2 × 2 pixels.
                if ((y & 1) == 0)
                {
                    (ligne, precedente) = (precedente, ligne);
                    continue;
                }
                byte* sortieUV = plan + (y0 + y) / 2 * largeur + x0;
                for (int x = 0; x < w; x += 2)
                {
                    int i = 3 * x;
                    int b = (precedente[i] + precedente[i + 3] + ligne[i] + ligne[i + 3] + 2) >> 2;
                    int g = (precedente[i + 1] + precedente[i + 4] + ligne[i + 1] + ligne[i + 4] + 2) >> 2;
                    int r = (precedente[i + 2] + precedente[i + 5] + ligne[i + 2] + ligne[i + 5] + 2) >> 2;
                    sortieUV[x] = (byte)(((-38 * r - 74 * g + 112 * b + 128) >> 8) + 128);
                    sortieUV[x + 1] = (byte)(((112 * r - 94 * g - 18 * b + 128) >> 8) + 128);
                }
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
