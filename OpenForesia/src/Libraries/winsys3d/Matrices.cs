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
// bas). Puis 360, 418 et 795, les trois corps a deux branches MMX / scalaire (voir la fin du
// fichier). Reste natif 379 (`SetGeomScreen`, qui entraine la matrice de projection de
// `wsyskern`).
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

    // ============================================================================================
    // LES TROIS CORPS A DEUX BRANCHES — 360, 418, 795 (2026-09-23), verifies au desassembleur
    // (`Legacy/Raw/Winsys3d/FUN_00a20630`, `FUN_00a22010`, `FUN_00a226c0`, `FUN_00a1f8f0`,
    // `FUN_00a203e0`).
    //
    // Chacun choisit entre une branche MMX et une branche scalaire sur `MMXInstalled`, que
    // l'initialisation (789) pose par un `__try { pand ; emms }` : **1 sur toute machine qui fait
    // tourner le jeu aujourd'hui**. Les deux branches sont transcrites, parce que le drapeau est
    // une globale de la DLL et que le choisir a sa place serait decider pour elle.
    //
    // **Elles ne calculent pas la meme chose**, et c'est la chose a retenir :
    //
    //   - 360 (`CompMatrix`) : les deux branches additionnent PUIS decalent. Meme resultat au bit
    //     pres (`pmaddwd` ne sature pas : il enroule comme `imul`/`add`). Seul l'ORDRE DES LECTURES
    //     differe — la branche MMX tient les lignes de la gauche en registres avant d'ecrire —, ce
    //     qui ne compte que si la destination recouvre un operande ;
    //   - 418 et 795 : la branche MMX fait `pmaddwd` sur `(vx, vy, vz, 0x1000)` contre
    //     `(m0, m1, m2, t)`, puis `psrad 12` sur CHAQUE moitie avant `paddd`. Donc
    //     `((vx.m0 + vy.m1) >> 12) + ((vz.m2 + 0x1000.t) >> 12)` — deux arrondis au lieu d'un, et
    //     la translation **tronquee a ses 16 bits bas** (`psllq mm3, 30h`). La branche scalaire,
    //     elle, fait `(somme >> 12) + t` sur 32 bits. C'est la MMX que le jeu execute.
    //
    // Ghidra a bien lu les deux branches ; il omet seulement que la scalaire de 418/795 ecrit
    // `0x011d0890` a CHAQUE sommet (trois `mov [11D0890h], eax` par tour), pas au troisieme.
    // ============================================================================================

    private static bool Mmx => Winsys3dData.MmxInstalle_00dedbe8 != 0;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a1f8f0 @ 0x00a1f8f0 (ordinal 345) — la rotation d'un vecteur court
    /// par une matrice, sans translation : `r[i] = (ligne i . v) >> 12`, en entiers. Par la forme,
    /// le `ApplyMatrixSV` de la libgte. `ret 0Ch`.
    ///
    /// Un ecart assume : l'original ecrit aussi `r[3]`, avec un mot de pile jamais initialise
    /// (`mov ecx,[esp+1Ch]`) ; le C# y met 0. Son seul appelant, 360, ne relit pas ce mot.
    /// </summary>
    internal static void ApplyMatrixSV(short* m, short* v, int* r)
    {
        int x = v[0], y = v[1], z = v[2];
        r[0] = m[0] * x + m[1] * y + m[2] * z >> 12;
        r[1] = m[4] * y + m[5] * z + m[3] * x >> 12;
        r[2] = m[7] * y + m[8] * z + m[6] * x >> 12;
        r[3] = 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a203e0 @ 0x00a203e0 (ordinal 356) — `TransMatrix` : range les trois
    /// `int` de <paramref name="v"/> dans la translation de la matrice (`+0x14`). `ret 8`.
    /// </summary>
    internal static void TransMatrix(byte* m, int* v)
    {
        int* t = (int*)(m + 0x14);
        t[0] = v[0];
        t[1] = v[1];
        t[2] = v[2];
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a20630 @ 0x00a20630 (ordinal 360) — `CompMatrix(a, b, c)` : la
    /// rotation `c = a . b >> 12`, puis la translation `c.t = (a . (short)b.t) >> 12 + (short)a.t`.
    /// `ret 0Ch`.
    ///
    /// **Les deux translations sont lues sur 16 bits, au tout debut** (`mov ax,[ebx+14h]`…),
    /// avant la branche : la composition ignore les bits hauts d'une translation. Et le produit
    /// de translation relit la rotation de <paramref name="a"/> APRES l'ecriture de
    /// <paramref name="c"/> — sans effet sauf si `c` est `a`.
    ///
    /// L'ordre des ecritures est celui du binaire dans les deux branches : colonne par colonne,
    /// les trois lignes a chaque fois (0, 3, 6, puis 1, 4, 7, puis 2, 5, 8).
    /// </summary>
    internal static void CompMatrix(short* a, short* b, short* c)
    {
        short taX = a[10], taY = a[12], taZ = a[14];
        short* tb = stackalloc short[4];
        tb[0] = b[10];
        tb[1] = b[12];
        tb[2] = b[14];
        tb[3] = 0;

        if (!Mmx)
        {
            // Chaque produit relit les deux operandes en memoire, au moment de l'ecrire.
            for (int colonne = 0; colonne < 3; colonne++)
                for (int ligne = 0; ligne < 3; ligne++)
                    c[ligne * 3 + colonne] = (short)(a[ligne * 3] * b[colonne]
                                                   + a[ligne * 3 + 1] * b[3 + colonne]
                                                   + a[ligne * 3 + 2] * b[6 + colonne] >> 12);
        }
        else
        {
            // Les trois lignes de `a` en registres (`movq` masques) avant toute ecriture ; une
            // colonne de `b` relue a chaque tour.
            short* lignes = stackalloc short[9];
            for (int i = 0; i < 9; i++) lignes[i] = a[i];
            for (int colonne = 0; colonne < 3; colonne++)
            {
                int b0 = b[colonne], b1 = b[3 + colonne], b2 = b[6 + colonne];
                for (int ligne = 0; ligne < 3; ligne++)
                    c[ligne * 3 + colonne] = (short)(lignes[ligne * 3] * b0
                                                   + lignes[ligne * 3 + 1] * b1
                                                   + lignes[ligne * 3 + 2] * b2 >> 12);
            }
        }

        int* r = stackalloc int[4];
        ApplyMatrixSV(a, tb, r);
        r[0] += taX;
        r[1] += taY;
        r[2] += taZ;
        TransMatrix((byte*)c, r);
    }

    /// <summary>La rotation-translation d'un sommet par le registre, branche scalaire : un arrondi, translation sur 32 bits.</summary>
    private static void TransformerScalaire(short* v, out int x, out int y, out int z)
    {
        short* m = (short*)Winsys3dData.RegistreRotation_011d0870;
        int* t = (int*)(Winsys3dData.RegistreRotation_011d0870 + 0x14);
        x = (m[0] * v[0] + m[1] * v[1] + m[2] * v[2] >> 12) + t[0];
        y = (m[3] * v[0] + m[4] * v[1] + m[5] * v[2] >> 12) + t[1];
        z = (m[6] * v[0] + m[7] * v[1] + m[8] * v[2] >> 12) + t[2];
    }

    /// <summary>
    /// La meme, branche MMX : `pmaddwd` de `(vx, vy, vz, 0x1000)` par `(m0, m1, m2, (short)t)`,
    /// `psrad 12` sur chaque moitie, puis la somme. Deux arrondis, translation sur 16 bits.
    /// </summary>
    private static void TransformerMmx(short* v, out int x, out int y, out int z)
    {
        short* m = (short*)Winsys3dData.RegistreRotation_011d0870;
        int* t = (int*)(Winsys3dData.RegistreRotation_011d0870 + 0x14);
        x = (m[0] * v[0] + m[1] * v[1] >> 12) + (m[2] * v[2] + 0x1000 * (short)t[0] >> 12);
        y = (m[3] * v[0] + m[4] * v[1] >> 12) + (m[5] * v[2] + 0x1000 * (short)t[1] >> 12);
        z = (m[6] * v[0] + m[7] * v[1] >> 12) + (m[8] * v[2] + 0x1000 * (short)t[2] >> 12);
    }

    /// <summary>
    /// La perspective, commune aux deux branches : `h = table[z & 0xffff]`, puis le mot d'ecran —
    /// Y en haut (`(y.h & ~0xfff) + ofy << 12`, decale de 4), X en bas sur 16 bits signes
    /// (`(x.h >> 12) + ofx`, `movsx`) — ADDITIONNES, pas assembles : un X negatif retranche 1 au Y.
    /// </summary>
    private static int ProjeterXY(int x, int y, int z)
    {
        int h = Winsys3dData.TableProjection_011d0898[z & 0xffff];
        int haut = (int)((uint)(y * h) & 0xfffff000u) + (Winsys3dData.DecalGeomY_011cb410 << 12);
        return (haut << 4) + (short)((x * h >> 12) + Winsys3dData.DecalGeomX_011cb414);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a22010 @ 0x00a22010 (ordinal 418) — la projection par lots : pour
    /// chaque unite de <paramref name="nombre"/>, trois sommets courts (`SVECTOR`, 8 octets)
    /// passes par le registre, leur XY d'ecran dans <paramref name="xy"/> et leur profondeur sur
    /// 16 bits dans <paramref name="profondeur"/>. `ret 14h`.
    ///
    /// Le quatrieme argument ne recoit qu'une chose, avant tout le reste : un mot nul. Son slot de
    /// pile sert ensuite d'indice a la boucle MMX.
    /// </summary>
    internal static void RotTransPersParTrois(short* sommets, int* xy, uint* profondeur, ushort* drapeaux, uint nombre)
    {
        *drapeaux = 0;
        if (!Mmx)
        {
            if (nombre == 0) return;
            do
            {
                for (int k = 0; k < 3; k++)
                {
                    // La branche est re-testee a chaque sommet, comme dans le binaire.
                    int x, y, z;
                    if (!Mmx) TransformerScalaire(sommets, out x, out y, out z);
                    else TransformerMmx(sommets, out x, out y, out z);
                    sommets += 4;
                    *xy = ProjeterXY(x, y, z);
                    Winsys3dData.DernierXY_011d0890 = *xy;
                    xy++;
                    *profondeur++ = (uint)z & 0xffff;
                }
            } while (--nombre != 0);
            return;
        }

        for (uint i = 0, n = nombre * 3; n != 0; n--, i++)
        {
            TransformerMmx(sommets + i * 4, out int x, out int y, out int z);
            xy[i] = ProjeterXY(x, y, z);
            profondeur[i] = (uint)z & 0xffff;
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a226c0 @ 0x00a226c0 (ordinal 795) — la jumelle de 418 sans
    /// profondeur ni drapeaux : trois sommets par unite, leur seul XY d'ecran. `ret 0Ch`.
    /// Les deux branches, et le `0x011d0890` de la scalaire, sont ceux de 418.
    /// </summary>
    internal static void RotTransPersParTroisSansProfondeur(short* sommets, int* xy, uint nombre)
    {
        if (!Mmx)
        {
            if (nombre == 0) return;
            do
            {
                for (int k = 0; k < 3; k++)
                {
                    int x, y, z;
                    if (!Mmx) TransformerScalaire(sommets, out x, out y, out z);
                    else TransformerMmx(sommets, out x, out y, out z);
                    sommets += 4;
                    *xy = ProjeterXY(x, y, z);
                    Winsys3dData.DernierXY_011d0890 = *xy;
                    xy++;
                }
            } while (--nombre != 0);
            return;
        }

        for (uint i = 0, n = nombre * 3; n != 0; n--, i++)
        {
            TransformerMmx(sommets + i * 4, out int x, out int y, out int z);
            xy[i] = ProjeterXY(x, y, z);
        }
    }
}
