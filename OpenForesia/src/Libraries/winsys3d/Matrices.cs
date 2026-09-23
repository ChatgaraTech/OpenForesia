// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`ret 8`, les neuf `imul`/`sar 0Ch`).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LA MATRICE DE LA `libgte` — ordinal 794, `ScaleMatrix`, et lui seul pour l'instant
//
// `winsys3d.dll` emule le coprocesseur geometrique de la PlayStation. Neuf ordinaux importes en
// relevent, et **huit sont dans la masse** : ils ecrivent ou relisent les registres emules du
// GTE, que le chemin d'affichage natif consulte a chaque image.
//
//     ordinal  corps           registre GTE touche          verdict
//     360      FUN_00a20630    —                            masse (composition)
//     366      FUN_00a20e70    MATRIX (export de donnees 814)  masse
//     367      FUN_00a20eb0    matrice d'eclairage (815)    masse
//     369      FUN_00a20ef0    la translation du MATRIX     masse
//     418      FUN_00a22010    lit le MATRIX                masse
//     795      FUN_00a226c0    lit le MATRIX                masse
//     439      FUN_00a25910    —                            masse
//     442      FUN_00a25b50    —                            masse
//     794      FUN_00a20540    AUCUN                        **ici**
//
// `ScaleMatrix` est le seul du lot qui ne touche aucune globale : il lit et ecrit la matrice que
// l'APPELANT lui donne, et rien d'autre. C'est ce qui en fait une composante a lui tout seul
// dans `DLLGlobales.md`, et ce qui l'a rendu portable le premier.
//
// **Depuis le 2026-09-23, l'aliasing (`Data.cs`) a leve la « masse »** : les registres du GTE
// sont la memoire de la DLL, un corps porte et un corps natif les partagent. Neuf corps de plus
// sont ici — les reglages 366, 367, 368, 369, 376, 378 et les calculs 792, 439, 442 (voir plus
// bas). Restent natifs 360, 379 (`SetGeomScreen`, qui entraine la matrice de projection de
// `wsyskern`), 418 et 795.
//
// ============================================================================================
// CE QUE LE CORPS CALCULE
//
// Une matrice de rotation de la `libgte` est **3 x 3 shorts, rangee par lignes**, suivie de la
// translation — c'est la meme representation que lisent les ordinaux 366 et 369. `ScaleMatrix`
// multiplie chaque COLONNE par une composante de l'echelle :
//
//     m[0] m[3] m[6]   par  s[0]        (la colonne 0, soit les lignes 0, 1, 2 de la colonne 0)
//     m[1] m[4] m[7]   par  s[1]
//     m[2] m[5] m[8]   par  s[2]
//
// L'echelle est en **virgule fixe 4.12** : le corps decale de 12 bits apres le produit, donc
// 4096 est l'unite. C'est le `ONE` de la `libgte`, la meme convention que la table de sinus de
// `Trigonometrie.cs`.
//
// Le decalage est ARITHMETIQUE (`sar`, pas `shr`), et les operandes sont sign-etendus avant le
// produit (`movsx`) : une rotation porte des valeurs negatives, et les traiter en non signe
// retournerait la matrice. En C# la conversion `short` -> `int` est sign-etendue et `>>` sur un
// `int` est arithmetique, donc la transcription directe est exacte.
//
// **La boucle n'est pas une liberte.** Ghidra sort neuf lignes, et les trois dernieres inversent
// l'ordre des facteurs (`m[7] * s[1]` au lieu de `s[1] * m[7]`) — un artefact d'allocation de
// registres, la multiplication commute. L'ORDRE DES ECRITURES, lui, est reproduit exactement
// (0, 3, 6, puis 1, 4, 7, puis 2, 5, 8) : il ne compterait que si l'echelle pointait dans la
// matrice, ce qu'aucun site ne fait, mais le reproduire ne coute rien.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dMatrices
{
    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a20540 @ 0x00a20540 (ordinal 794) — `ScaleMatrix` de la libgte :
    /// multiplie chaque colonne de la matrice 3 x 3 de <paramref name="matrice"/> par la
    /// composante correspondante de <paramref name="echelle"/>, en virgule fixe 4.12.
    ///
    /// **Modifie la matrice sur place** et ne rend rien. Aucune globale : c'est une fonction des
    /// deux pointeurs qu'on lui donne, ce qui est la raison pour laquelle elle se detache alors
    /// que ses huit sœurs de la libgte restent natives.
    /// </summary>
    internal static void ScaleMatrix(short* matrice, short* echelle)
    {
        for (int colonne = 0; colonne < 3; colonne++)
        {
            int facteur = echelle[colonne];
            for (int ligne = 0; ligne < 3; ligne++)
            {
                short* cellule = matrice + ligne * 3 + colonne;
                *cellule = (short)(facteur * *cellule >> 12);
            }
        }
    }

    // ============================================================================================
    // LES REGLAGES DU GTE ET TROIS CALCULS — 2026-09-23, verifies au desassembleur
    // (`Legacy/Raw/Winsys3d/FUN_00a20e70` .. `FUN_00a25b50`).
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a20e70 @ 0x00a20e70 (ordinal 366) — `SetRotMatrix` : copie les neuf
    /// `short` de rotation (0x12 octets : quatre mots puis un demi-mot) dans le registre, sans
    /// toucher a sa translation ni au bourrage. `ret 4`.
    /// </summary>
    internal static void SetRotMatrix(byte* matrice)
    {
        uint* d = (uint*)Winsys3dData.RegistreRotation_011d0870;
        uint* s = (uint*)matrice;
        d[0] = s[0];
        d[1] = s[1];
        d[2] = s[2];
        d[3] = s[3];
        *(ushort*)(d + 4) = *(ushort*)(s + 4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a20eb0 @ 0x00a20eb0 (ordinal 367) — `SetLightMatrix` : copie les huit mots (0x20 octets). `ret 4`.</summary>
    internal static void SetLightMatrix(byte* matrice) => CopierHuitMots(Winsys3dData.RegistreLumiere_011d0850, matrice);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a20ed0 @ 0x00a20ed0 (ordinal 368) — copie huit mots dans la
    /// matrice a `0x011d0828`, voisine de la matrice d'eclairage : vraisemblablement le
    /// `SetColorMatrix` de la libgte, par la forme et par la place. `ret 4`.
    /// </summary>
    internal static void SetColorMatrix(byte* matrice) => CopierHuitMots(Winsys3dData.RegistreCouleur_011d0828, matrice);

    private static void CopierHuitMots(byte* destination, byte* source)
    {
        uint* d = (uint*)destination;
        uint* s = (uint*)source;
        for (int i = 0; i < 8; i++) d[i] = s[i];
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a20ef0 @ 0x00a20ef0 (ordinal 369) — `SetTransMatrix` : copie les
    /// trois translations (`+0x14`, `+0x18`, `+0x1c`) dans celles du registre. `ret 4`.
    /// </summary>
    internal static void SetTransMatrix(byte* matrice)
    {
        int* d = (int*)(Winsys3dData.RegistreRotation_011d0870 + 0x14);
        int* s = (int*)(matrice + 0x14);
        d[0] = s[0];
        d[1] = s[1];
        d[2] = s[2];
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a20fe0 @ 0x00a20fe0 (ordinal 376) — `SetBackColor(r, g, b)` : trois octets. `ret 0Ch`.</summary>
    internal static void SetBackColor(byte r, byte g, byte b)
    {
        byte* c = Winsys3dData.CouleurFond_011f0898;
        c[0] = r;
        c[1] = g;
        c[2] = b;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a21020 @ 0x00a21020 (ordinal 378) — range deux entiers, le premier
    /// a `0x011cb414`, le second a `0x011cb410` : par la forme, le `SetGeomOffset(ofx, ofy)` de la
    /// libgte. `ret 8`.
    /// </summary>
    internal static void SetGeomOffset(int x, int y)
    {
        Winsys3dData.DecalGeomX_011cb414 = x;
        Winsys3dData.DecalGeomY_011cb410 = y;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a22dd0 @ 0x00a22dd0 (ordinal 792) — applique la rotation et la
    /// translation du registre a <paramref name="nombre"/> vecteurs courts (`SVECTOR`, 8 octets) :
    /// `sortie = (M . v) >> 12 + T`, chaque composante tronquee a 16 bits. Le nombre est pris sur
    /// ses 16 bits bas. `ret 0Ch`.
    /// </summary>
    internal static void RotTransVecteursCourts(short* source, short* destination, uint nombre)
    {
        short* m = (short*)Winsys3dData.RegistreRotation_011d0870;
        int* t = (int*)(Winsys3dData.RegistreRotation_011d0870 + 0x14);
        for (nombre &= 0xffff; nombre != 0; nombre--)
        {
            destination[0] = (short)((m[1] * source[1] + m[2] * source[2] + m[0] * source[0] >> 12) + t[0]);
            destination[1] = (short)((m[4] * source[1] + m[5] * source[2] + m[3] * source[0] >> 12) + t[1]);
            destination[2] = (short)((m[7] * source[1] + m[8] * source[2] + m[6] * source[0] >> 12) + t[2]);
            destination += 4;
            source += 4;
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a25b50 @ 0x00a25b50 (ordinal 442) — `SquareRoot0` : la racine d'un
    /// entier non signe, par une table selon l'ordre de grandeur puis UN pas de Newton
    /// `(n / r + r) >> 1`, le tout sur 16 bits. Sous 0x100, la table donne directement le
    /// resultat. `ret 4`.
    /// </summary>
    internal static uint SquareRoot0(uint n)
    {
        uint r;
        if (n < 0x10000)
        {
            if (n < 0x100) return Winsys3dData.RacinesPetites_011d0620[n];
            r = Winsys3dData.Racines8_011d0420[n >> 8];
        }
        else if (n < 0x1000000) r = Winsys3dData.Racines16_011f0c90[n >> 16];
        else r = Winsys3dData.Racines24_011cb210[n >> 24];
        return (n / r + r) >> 1 & 0xffff;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a25910 @ 0x00a25910 (ordinal 439) — `VectorNormal` : norme d'un
    /// vecteur de trois `int` (la meme racine que <see cref="SquareRoot0"/>, en ligne), puis chaque
    /// composante `(c << 12) / norme` en division SIGNEE (`idiv`). Rend la norme ; 0 sans rien
    /// ecrire si elle est nulle. `ret 8`.
    /// </summary>
    internal static uint VectorNormal(int* vecteur, int* normalise)
    {
        int x = vecteur[0];
        uint n = (uint)(vecteur[1] * vecteur[1] + vecteur[2] * vecteur[2] + x * x);
        uint norme = SquareRoot0(n);
        if (norme == 0) return 0;

        normalise[0] = (x << 12) / (int)norme;
        normalise[1] = (vecteur[1] << 12) / (int)norme;
        normalise[2] = (vecteur[2] << 12) / (int)norme;
        return norme;
    }
}
