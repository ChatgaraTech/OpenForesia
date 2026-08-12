// =====================================================================
// Surface hors écran du **régime GDI de démarrage** — écran-titre et attract.
//
// Ces deux phases ne dessinent pas en Direct3D : le jeu y fait un StretchDIBits sur le contexte de
// périphérique d'une surface. Le backend leur fournit donc une DIB section plutôt qu'une surface
// DirectDraw, ce qui supprime le mode exclusif et le changement de mode d'affichage dont ce
// contexte était le seul produit réellement consommé.
//
// C'est la transposition de la décision déjà prise pour le chemin Direct3D : rendre dans une cible
// hors écran, puis présenter. Le cadrage devient un problème de présentation.
//
// Une DIB section plutôt que le contexte de la fenêtre : celui-ci est découpé par la zone de clip
// courante, effacé sur WM_ERASEBKGND et invalidé au redimensionnement, alors que l'attract ne
// redessine que sur WM_COMMAND ou toutes les vingt secondes. Il faut de toute façon une mémoire
// d'image ; autant qu'elle serve de cible.
//
// Voir openspec/changes/render-title-screen-in-window/design.md
// =====================================================================

using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

internal static unsafe class StartupSurface
{
    [DllImport("gdi32.dll")] private static extern IntPtr CreateCompatibleDC(IntPtr hdc);
    [DllImport("gdi32.dll")] private static extern IntPtr CreateDIBSection(
        IntPtr hdc, void* bmi, uint usage, void** bits, IntPtr section, uint offset);
    [DllImport("gdi32.dll")] private static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
    [DllImport("gdi32.dll")] private static extern int DeleteObject(IntPtr obj);
    [DllImport("gdi32.dll")] private static extern int DeleteDC(IntPtr hdc);
    [DllImport("gdi32.dll")] private static extern int GdiFlush();

    private static IntPtr s_MemDc;
    private static IntPtr s_Bitmap;
    private static IntPtr s_PreviousBitmap;
    private static void* s_Bits;

    /// <summary>Dimensions de la surface, ou zéro tant qu'elle n'existe pas.</summary>
    internal static int Width { get; private set; }
    internal static int Height { get; private set; }

    /// <summary>Vrai dès qu'une surface est allouée et qu'elle a reçu au moins un dessin.</summary>
    internal static bool HasContent { get; private set; }

    /// <summary>
    /// Vrai une fois le régime Direct3D entré. **Irréversible** : à partir de là, plus rien ne
    /// dessine en GDI sur la fenêtre du jeu.
    ///
    /// <para>
    /// Les deux régimes écrivent dans la même fenêtre par des moyens incompatibles. Tant que la
    /// swap chain n'existe pas, le démarrage présente en GDI ; dès qu'elle existe, elle seule
    /// présente. Un état explicite plutôt qu'une condition déduite ailleurs, pour que la frontière
    /// soit lisible en un seul endroit.
    /// </para>
    /// </summary>
    internal static bool Retired { get; private set; }

    /// <summary>
    /// Entre en régime Direct3D et libère la surface de démarrage. Sans effet si déjà fait.
    /// </summary>
    internal static void Retire()
    {
        if (Retired) return;
        Release();
        Retired = true;   // après Release, qui remet HasContent à faux
    }

    /// <summary>Contexte de périphérique de la surface, ou zéro si l'allocation a échoué.</summary>
    internal static IntPtr Dc => s_MemDc;

    /// <summary>
    /// Rend le contexte de périphérique d'une surface aux dimensions demandées, en la créant ou en
    /// la recréant si nécessaire. Rend <c>IntPtr.Zero</c> en cas d'échec — l'appelant retombe alors
    /// sur son chemin d'origine plutôt que de dessiner dans le vide.
    /// </summary>
    internal static IntPtr EnsureDc(int width, int height)
    {
        if (Retired) return IntPtr.Zero;   // le régime GDI est clos, on ne réalloue pas
        if (width <= 0 || height <= 0) return IntPtr.Zero;
        if (s_MemDc != IntPtr.Zero && Width == width && Height == height) return s_MemDc;

        Release();

        s_MemDc = CreateCompatibleDC(IntPtr.Zero);
        if (s_MemDc == IntPtr.Zero) return IntPtr.Zero;

        // BITMAPINFOHEADER, 40 octets. Hauteur positive : disposition ascendante, celle d'un BMP
        // sur disque — le vidage de vérification (tâche 4.3) n'a alors rien à retourner.
        byte* bmi = stackalloc byte[40];
        for (int i = 0; i < 40; i++) bmi[i] = 0;
        *(uint*)(bmi + 0x00) = 40;             // biSize
        *(int*)(bmi + 0x04) = width;           // biWidth
        *(int*)(bmi + 0x08) = height;          // biHeight
        *(ushort*)(bmi + 0x0c) = 1;            // biPlanes
        *(ushort*)(bmi + 0x0e) = 32;           // biBitCount
        *(uint*)(bmi + 0x10) = 0;              // biCompression = BI_RGB

        void* bits;
        s_Bitmap = CreateDIBSection(s_MemDc, bmi, 0 /* DIB_RGB_COLORS */, &bits, IntPtr.Zero, 0);
        if (s_Bitmap == IntPtr.Zero) { Release(); return IntPtr.Zero; }

        s_Bits = bits;
        s_PreviousBitmap = SelectObject(s_MemDc, s_Bitmap);
        Width = width;
        Height = height;
        HasContent = false;
        return s_MemDc;
    }

    /// <summary>À appeler après chaque dessin réussi dans la surface.</summary>
    internal static void MarkDrawn() => HasContent = true;

    [DllImport("user32.dll")] private static extern IntPtr GetDC(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);
    [DllImport("gdi32.dll")] private static extern int StretchBlt(
        IntPtr dst, int xDst, int yDst, int wDst, int hDst,
        IntPtr src, int xSrc, int ySrc, int wSrc, int hSrc, uint rop);
    [DllImport("gdi32.dll")] private static extern int SetStretchBltMode(IntPtr hdc, int mode);
    [DllImport("gdi32.dll")] private static extern int SetBrushOrgEx(IntPtr hdc, int x, int y, void* previous);
    [DllImport("gdi32.dll")] private static extern int PatBlt(IntPtr hdc, int x, int y, int w, int h, uint rop);

    private const uint SRCCOPY = 0x00CC0020;
    private const uint BLACKNESS = 0x00000042;
    private const int HALFTONE = 4;

    /// <summary>
    /// Recopie la surface dans le contexte donné, avec les **mêmes règles de cadrage que la
    /// présentation Direct3D** (<c>D3D11Backend.Present</c>) : le plus grand rectangle au rapport
    /// d'aspect de la surface qui tienne dans la zone cliente, centré, bandes unies noires.
    /// </summary>
    internal static bool PresentToDc(IntPtr dc, int clientWidth, int clientHeight)
    {
        if (Retired) return false;   // 7.2 : plus aucun dessin GDI sur le HWND après la bascule
        if (dc == IntPtr.Zero || s_MemDc == IntPtr.Zero || !HasContent) return false;
        if (clientWidth <= 0 || clientHeight <= 0 || Width <= 0 || Height <= 0) return false;

        float scale = MathF.Min((float)clientWidth / Width, (float)clientHeight / Height);
        int drawW = (int)(Width * scale);
        int drawH = (int)(Height * scale);
        int originX = (clientWidth - drawW) / 2;
        int originY = (clientHeight - drawH) / 2;

        // Bandes unies : on n'efface que ce qui ne sera pas recouvert, pour éviter le scintillement
        // d'un effacement plein suivi d'un blit.
        if (originY > 0)
        {
            PatBlt(dc, 0, 0, clientWidth, originY, BLACKNESS);
            PatBlt(dc, 0, originY + drawH, clientWidth, clientHeight - originY - drawH, BLACKNESS);
        }
        if (originX > 0)
        {
            PatBlt(dc, 0, originY, originX, drawH, BLACKNESS);
            PatBlt(dc, originX + drawW, originY, clientWidth - originX - drawW, drawH, BLACKNESS);
        }

        // HALFTONE donne un rééchantillonnage correct à l'agrandissement ; il exige que l'origine
        // de brosse soit repositionnée après coup, faute de quoi les motifs se décalent.
        SetStretchBltMode(dc, HALFTONE);
        SetBrushOrgEx(dc, 0, 0, null);

        return StretchBlt(dc, originX, originY, drawW, drawH,
                          s_MemDc, 0, 0, Width, Height, SRCCOPY) != 0;
    }

    /// <summary>Présente dans la fenêtre donnée. Rend faux si rien n'a pu être affiché.</summary>
    internal static bool PresentTo(IntPtr hwnd, int clientWidth, int clientHeight)
    {
        if (hwnd == IntPtr.Zero) return false;

        IntPtr dc = GetDC(hwnd);
        if (dc == IntPtr.Zero) return false;
        try { return PresentToDc(dc, clientWidth, clientHeight); }
        finally { ReleaseDC(hwnd, dc); }
    }

    internal static void Release()
    {
        if (s_MemDc != IntPtr.Zero)
        {
            if (s_PreviousBitmap != IntPtr.Zero) SelectObject(s_MemDc, s_PreviousBitmap);
            DeleteDC(s_MemDc);
        }
        if (s_Bitmap != IntPtr.Zero) DeleteObject(s_Bitmap);

        s_MemDc = IntPtr.Zero;
        s_Bitmap = IntPtr.Zero;
        s_PreviousBitmap = IntPtr.Zero;
        s_Bits = null;
        Width = 0;
        Height = 0;
        HasContent = false;
    }
}
