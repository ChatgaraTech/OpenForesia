// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur** : `Legacy/Raw/Winsys3d/FUN_00a05c60`, `05d50`, `05df0`, `05e90`,
// `05f50` (`.disasm.txt`).
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LA VRAM PLAYSTATION EMULEE — les quatre transferts de la `libgpu`, 2026-09-23
//
// La DLL garde une VRAM de 1 024 x 512 mots de 16 bits (`Data.Vram_00b6dbd0`), une ligne tous
// les 0x800 octets. Le jeu y ecrit ses images comme sur la console ; le cache de textures les en
// tire a la demande (`ChargementTextures.cs`). Quatre ordinaux la touchent directement :
//
//     ordinal  corps           libgpu        sens
//     665      FUN_00a05e90    ClearImage    une couleur -> VRAM
//     674      FUN_00a05d50    LoadImage     memoire -> VRAM
//     676      FUN_00a05df0    MoveImage     VRAM -> VRAM
//     684      FUN_00a05f50    StoreImage    VRAM -> memoire
//
// Les trois qui ECRIVENT la VRAM invalident d'abord le rectangle dans le cache de textures
// (`InvaliderRectangle`, `FUN_00a05c60`) : sinon une texture deja convertie survivrait a l'image
// qui l'a remplacee. `StoreImage` ne l'invalide pas, il ne fait que lire. Les quatre appellent le
// rappel de fin d'image `DAT_011be754` s'il est pose, et rendent 0.
//
// --------------------------------------------------------------------------------------------
// CE QUI EST TRANSCRIT TEL QUEL
//
// **Les lignes se copient VERS L'AVANT**, `rep movsd` puis `rep movsb` sur `largeur * 2` octets.
// Pour `MoveImage`, dont les rectangles peuvent se chevaucher, c'est ce que le binaire fait et
// pas un `memmove` : `CopierLigne` reproduit la copie mot a mot, dans l'ordre.
//
// **La couleur de `ClearImage` est fausse (LB-044).** `((b & 0xff) << (g + 10)) << (r + 5)`,
// decalages masques a cinq bits par le processeur, puis doublee dans les deux moities du mot :
// la composante bleue est DECALEE par les deux autres au lieu d'etre assemblee avec elles. Juste
// pour le noir, le seul cas que le jeu emploie.
//
// **`ClearImage` remplit par paires de pixels** : `largeur / 2` mots de 32 bits par ligne, le
// dernier pixel d'une largeur impaire reste tel quel.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dVram
{
    /// <summary>Une ligne de VRAM : 1 024 pixels de 16 bits.</summary>
    private const int PasLigne = 0x800;

    /// <summary>L'adresse du pixel (x, y) dans la VRAM emulee.</summary>
    private static byte* Pixel(int x, int y) => Winsys3dData.Vram_00b6dbd0 + (y * 0x400 + x) * 2;

    /// <summary>
    /// Copie une ligne de <paramref name="largeur"/> pixels VERS L'AVANT : `(largeur * 2) >> 2`
    /// mots, puis `(largeur * 2) &amp; 3` octets — le `rep movsd ; rep movsb` du binaire.
    /// </summary>
    private static void CopierLigne(byte* destination, byte* source, int largeur)
    {
        uint n = (uint)(largeur * 2);
        uint* d = (uint*)destination;
        uint* s = (uint*)source;
        for (uint i = n >> 2; i != 0; i--) *d++ = *s++;
        byte* db = (byte*)d;
        byte* sb = (byte*)s;
        for (uint i = n & 3; i != 0; i--) *db++ = *sb++;
    }

    /// <summary>Le rappel de fin d'image, appele sans argument s'il est pose.</summary>
    private static void Rappel()
    {
        nint rappel = Winsys3dData.RappelFinImage_011be754;
        if (rappel != 0) ((delegate* unmanaged[Stdcall]<void>)rappel)();
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05e90 @ 0x00a05e90 (ordinal 665) — `ClearImage(rect, r, g, b)`,
    /// `ret 10h`. Voir l'en-tete pour la couleur (LB-044) et le remplissage par paires.
    /// </summary>
    internal static int ClearImage(short* rect, byte r, byte g, uint b)
    {
        uint moitie = ((b & 0xff) << ((g + 10) & 0x1f)) << ((r + 5) & 0x1f);
        uint couleur = (moitie << 16) + moitie;
        byte* ligne = Pixel(rect[0], rect[1]);
        InvaliderRectangle(rect[0], rect[1], rect[2], rect[3]);

        for (int y = 0; y < rect[3]; y++)
        {
            if ((short)(rect[2] & ~1) > 0)
            {
                uint* p = (uint*)ligne;
                for (int i = 0; i < rect[2] >> 1; i++) p[i] = couleur;
            }
            ligne += PasLigne;
        }

        Rappel();
        return 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05d50 @ 0x00a05d50 (ordinal 674) — `LoadImage(rect, donnees)`,
    /// `ret 8` : memoire -> VRAM, lignes de `largeur` pixels contigues. Rien (rend 0, sans
    /// rappel) si `y` depasse 0x1ff, compare en `short` signe.
    /// </summary>
    internal static int LoadImage(short* rect, byte* donnees)
    {
        if (rect[1] > 0x1ff) return 0;

        InvaliderRectangle(rect[0], rect[1], rect[2], rect[3]);
        byte* ligne = Pixel(rect[0], rect[1]);
        for (int y = 0; y < rect[3]; y++)
        {
            CopierLigne(ligne, donnees + y * rect[2] * 2, rect[2]);
            ligne += PasLigne;
        }

        Rappel();
        return 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05df0 @ 0x00a05df0 (ordinal 676) — `MoveImage(rect, x, y)`,
    /// `ret 0Ch` : VRAM -> VRAM, vers l'avant. Invalide le rectangle d'ARRIVEE seulement.
    /// </summary>
    internal static int MoveImage(short* rect, int x, int y)
    {
        byte* source = Pixel(rect[0], rect[1]);
        byte* destination = Pixel(x, y);
        InvaliderRectangle(x, y, rect[2], rect[3]);

        for (int i = 0; i < rect[3]; i++)
        {
            CopierLigne(destination, source, rect[2]);
            source += PasLigne;
            destination += PasLigne;
        }

        Rappel();
        return 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05f50 @ 0x00a05f50 (ordinal 684) — `StoreImage(rect, donnees)`,
    /// `ret 8` : VRAM -> memoire. N'invalide rien.
    /// </summary>
    internal static int StoreImage(short* rect, byte* donnees)
    {
        byte* ligne = Pixel(rect[0], rect[1]);
        for (int y = 0; y < rect[3]; y++)
        {
            CopierLigne(donnees + y * rect[2] * 2, ligne, rect[2]);
            ligne += PasLigne;
        }

        Rappel();
        return 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05c60 @ 0x00a05c60 — invalide dans le cache de textures tout ce
    /// qu'un rectangle de VRAM recouvre, `ret 10h`. Deux passes, toutes deux sur l'objet cache
    /// <c>DAT_012141d4</c> (`mov ecx,[12141D4h]` avant chaque appel, que Ghidra ne montre pas) :
    /// <list type="number">
    /// <item>les CLUT : par tranche de 16 pixels et par ligne, les emplacements qui les emploient
    /// (<see cref="Winsys3dCacheTextures.LibererParClut"/>) ;</item>
    /// <item>les pages de texture (64 x 256) touchees, videes en entier
    /// (<see cref="Winsys3dCacheTextures.ViderPage"/>).</item>
    /// </list>
    /// </summary>
    internal static void InvaliderRectangle(int x, int y, int largeur, int hauteur)
    {
        int clutDebut = (x >> 4 & 0x3f) | y << 6;
        for (int ligne = 0; ligne < hauteur << 6; ligne += 0x40)
        {
            for (int k = 0; k < largeur >> 4; k++)
                Winsys3dCacheTextures.LibererParClut(Winsys3dData.Objet_012141d4, ligne + clutDebut + k, 0x100);
        }

        int lignesDePages = hauteur >> 8;
        if (lignesDePages * 0x100 < hauteur) lignesDePages++;
        int colonnesDePages = largeur >> 6;
        if (colonnesDePages * 0x40 < largeur) colonnesDePages++;

        int pageDebut = (y >> 4 & unchecked((int)0xfffffff0)) + (x >> 6);
        for (int rang = 0; rang < lignesDePages << 4; rang += 0x10)
        {
            for (int k = 0; k < colonnesDePages; k++)
                Winsys3dCacheTextures.ViderPage(Winsys3dData.Objet_012141d4, rang + pageDebut + k);
        }
    }

    // ============================================================================================
    // `LoadTPage` ET `LoadClut` — ordinaux 696 et 695 (2026-09-23), verifies au desassembleur
    // (`Legacy/Raw/Winsys3d/FUN_00a06070`, `FUN_00a06020`). Appeles par `FntLoad` (710).
    //
    // Tous deux rendent le code calcule par `mov ax, si` : seul le MOT BAS est pose, le mot haut
    // est celui que l'appel au cache vient de laisser dans `eax`. Le C# rend le mot bas ; l'unique
    // appelant masque par 0xffff.
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a06070 @ 0x00a06070 (ordinal 696) — `LoadTPage(pixels, tp, abr, x,
    /// y, w, h)` : charge l'image dans la VRAM au rectangle `(x, y, w >> (2 - tp), h)` — la largeur
    /// en mots de 16 bits selon la profondeur —, puis rend le mot de page de texture
    /// `((tp &amp; 3) &lt;&lt; 2 | abr &amp; 3) &lt;&lt; 5 | (y &amp; 0x100 | x >> 2 &amp; 0xf0) >> 4`, et
    /// vide cette page du cache. `ret 1Ch`.
    /// </summary>
    internal static ushort LoadTPage(byte* pixels, int tp, int abr, int x, int y, int w, short h)
    {
        short* rect = stackalloc short[4];
        rect[0] = (short)x;
        rect[1] = (short)y;
        rect[2] = (short)(w >> (2 - tp));
        rect[3] = h;
        LoadImage(rect, pixels);
        Winsys3dSouches.RendZeroUnArgument();   // FUN_00a02e20(0)
        int page = ((tp & 3) << 2 | abr & 3) << 5 | (y & 0x100 | x >> 2 & 0xf0) >> 4;
        Winsys3dCacheTextures.ViderPage(Winsys3dData.Objet_012141d4, page);
        return (ushort)page;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a06020 @ 0x00a06020 (ordinal 695) — `LoadClut(clut, x, y)` : copie
    /// les 16 couleurs (huit mots) directement dans la VRAM a `(x, y)` — sans passer par
    /// `LoadImage`, donc sans invalider de rectangle —, puis libere du cache ce qui dependait de
    /// cette CLUT et rend son identifiant `(x >> 4) &amp; 0x3f | y &lt;&lt; 6`. `ret 0Ch`.
    /// </summary>
    internal static ushort LoadClut(uint* clut, int x, int y)
    {
        uint* vram = (uint*)(Winsys3dData.Vram_00b6dbd0 + (y * 0x400 + x) * 2);
        for (int i = 0; i < 8; i++) vram[i] = clut[i];
        int id = x >> 4 & 0x3f | y << 6;
        Winsys3dCacheTextures.LibererParClut(Winsys3dData.Objet_012141d4, id, 0x10);
        return (ushort)id;
    }
}
