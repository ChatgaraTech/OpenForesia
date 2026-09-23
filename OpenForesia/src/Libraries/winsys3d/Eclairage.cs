// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`Legacy/Raw/Winsys3d/FUN_00a03e70`, `FUN_00a24000`, `FUN_00a23cf0`,
// `FUN_00a23810`, `FUN_00a23dd0`, `FUN_00a1fba0`).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// L'ECLAIRAGE DE LA `libgte` — ordinal 793 et `NormalColorCol3` (407)
//
// Le GTE eclaire un sommet en trois temps, et les registres qu'il consulte sont ceux que
// `Matrices.cs` pose (tous aliases sur la memoire de la DLL) :
//
//     1. l = L . n >> 12          la matrice de lumiere (0x011d0850) par la normale ; < 0 -> 0
//     2. c = C . l >> 12          la matrice de couleur (0x011d0828) ; < 0 -> 0
//        c += fond << 4           la couleur de fond (0x011f0898), un octet par composante
//     3. rgb = couleur . c >> 12  par la couleur du sommet, sature a 0 .. 255 ; le code recopie
//
// `NormalColorCol3` fait ces trois temps pour trois normales. **Les deux premieres sont ecrites
// en ligne, la troisieme passe par trois fonctions** (`23cf0`, `23810`, `23dd0`) : le compilateur
// a deroule deux fois, puis appele. Les deux chemins font le meme calcul — ils sont transcrits
// chacun sous sa forme, parce que les fonctions de la troisieme portent un parametre de plus.
//
// **La puissance de `FUN_00a23cf0`.** Son troisieme argument `n` eleve chaque composante de `l` a
// la puissance 2^n (`ftol(pow(l / 4096, 2^n) * 4096)`, un reflet speculaire). `NormalColorCol3`
// passe toujours 0, donc la branche ne s'execute pas dans ce qui est atteint ; elle est transcrite
// quand meme. Ghidra en avait perdu le `* 4096` final (`fmul [0A2A0E0h]` apres le `pow`), et les
// trois arguments de l'appel a `ApplyMatrixSV` — les deux sont repris du desassemblage.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dEclairage
{
    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03e70 @ 0x00a03e70 (ordinal 793) — eclaire <paramref name="nombre"/>
    /// triangles : pour chacun, trois normales courtes consecutives (0x18 octets) et trois
    /// couleurs de sortie (12 octets), avec la couleur de base de l'export 817. Si
    /// `Elo_NoLighting` est pose, n'eclaire rien et ecrit `3 . nombre` mots nuls
    /// (`rep stosd`). `ret 0Ch`.
    /// </summary>
    internal static void EclairerTriangles(byte* normales, uint* couleurs, int nombre)
    {
        if (Winsys3dData.EloNoLighting_011be758 != 0)
        {
            for (uint n = (uint)(nombre * 3); n != 0; n--) *couleurs++ = 0;
            return;
        }

        for (; nombre != 0; nombre--)
        {
            NormalColorCol3((short*)normales, (short*)(normales + 8), (short*)(normales + 0x10),
                Winsys3dData.CouleurEclairage_00b6d198,
                (byte*)couleurs, (byte*)(couleurs + 1), (byte*)(couleurs + 2));
            normales += 0x18;
            couleurs += 3;
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a24000 @ 0x00a24000 (ordinal 407) — `NormalColorCol3` : la couleur
    /// eclairee de trois normales, chacune ecrite sur quatre octets `r, g, b, code`. `ret 1Ch`.
    /// </summary>
    internal static void NormalColorCol3(short* n0, short* n1, short* n2, byte* couleur,
                                         byte* sortie0, byte* sortie1, byte* sortie2)
    {
        CouleurEnLigne(n0, couleur, sortie0);
        CouleurEnLigne(n1, couleur, sortie1);

        int* lumiere = stackalloc int[4];
        int* teinte = stackalloc int[4];
        LumiereDeNormale(n2, lumiere, 0);
        TeinteDeLumiere(lumiere, teinte, 1);
        AppliquerCouleur(couleur, teinte, sortie2);
    }

    /// <summary>
    /// Les deux premieres normales de <see cref="NormalColorCol3"/>, deroulees dans son corps :
    /// le calcul de `23cf0(n, l, 0)`, `23810(l, c, 1)` et `23dd0` a la suite, sans la puissance.
    /// </summary>
    private static void CouleurEnLigne(short* normale, byte* couleur, byte* sortie)
    {
        int* l = stackalloc int[4];
        int* c = stackalloc int[4];
        Winsys3dMatrices.ApplyMatrixSV((short*)Winsys3dData.RegistreLumiere_011d0850, normale, l);
        if (l[0] < 0) l[0] = 0;
        if (l[1] < 0) l[1] = 0;
        if (l[2] < 0) l[2] = 0;
        Winsys3dMatrices.ApplyMatrixLV((short*)Winsys3dData.RegistreCouleur_011d0828, l, c);
        if (c[0] < 0) c[0] = 0;
        if (c[1] < 0) c[1] = 0;
        if (c[2] < 0) c[2] = 0;
        byte* fond = Winsys3dData.CouleurFond_011f0898;
        c[0] += fond[0] << 4;
        c[1] += fond[1] << 4;
        c[2] += fond[2] << 4;
        AppliquerCouleur(couleur, c, sortie);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a23cf0 @ 0x00a23cf0 — `l = L . n >> 12`, chaque composante
    /// ramenee a 0 si negative ; puis, si <paramref name="puissance"/> est positive (plafonnee a
    /// 31), `l = ftol(pow(l / 4096, 2^puissance) * 4096)`.
    ///
    /// Ecart assume sur cette branche, jamais prise par ce qui est atteint : `Math.Pow` au lieu du
    /// `pow` x87 de la CRT (`wsyskern` ordinal 3). `__ftol` est reproduit jusqu'a son cas limite
    /// (hors plage -> 0x80000000).
    /// </summary>
    internal static void LumiereDeNormale(short* normale, int* l, int puissance)
    {
        Winsys3dMatrices.ApplyMatrixSV((short*)Winsys3dData.RegistreLumiere_011d0850, normale, l);
        if (l[0] < 0) l[0] = 0;
        if (l[1] < 0) l[1] = 0;
        if (l[2] < 0) l[2] = 0;
        if (puissance <= 0) return;

        if (puissance > 0x1f) puissance = 0x1f;
        double exposant = 1 << puissance;
        l[0] = Ftol(System.Math.Pow(l[0] * (1.0 / 4096), exposant) * 4096.0);
        l[1] = Ftol(System.Math.Pow(l[1] * (1.0 / 4096), exposant) * 4096.0);
        l[2] = Ftol(System.Math.Pow(l[2] * (1.0 / 4096), exposant) * 4096.0);
    }

    /// <summary>
    /// `__ftol` de la CRT : troncature vers zero, et l'« entier indefini » 0x80000000 pour une
    /// valeur hors de la plage d'un `int` ou NaN — la ou la conversion de .NET 9 sature.
    /// </summary>
    private static int Ftol(double v)
        => v >= -2147483648.0 && v < 2147483648.0 ? (int)v : int.MinValue;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a23810 @ 0x00a23810 — `c = C . l >> 12` sur 64 bits, chaque
    /// composante ramenee a 0 si negative ; si <paramref name="avecFond"/>, plus la couleur de
    /// fond decalee de 4.
    /// </summary>
    internal static void TeinteDeLumiere(int* l, int* c, byte avecFond)
    {
        Winsys3dMatrices.ApplyMatrixLV((short*)Winsys3dData.RegistreCouleur_011d0828, l, c);
        if (c[0] < 0) c[0] = 0;
        if (c[1] < 0) c[1] = 0;
        if (c[2] < 0) c[2] = 0;
        if (avecFond != 0)
        {
            byte* fond = Winsys3dData.CouleurFond_011f0898;
            c[0] += fond[0] << 4;
            c[1] += fond[1] << 4;
            c[2] += fond[2] << 4;
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a23dd0 @ 0x00a23dd0 — `rgb = couleur . c >> 12`, chaque
    /// composante saturee a 0 .. 255 ; le quatrieme octet (le code de primitive) est recopie.
    /// </summary>
    internal static void AppliquerCouleur(byte* couleur, int* c, byte* sortie)
    {
        sortie[0] = Saturer(couleur[0] * c[0] >> 12);
        sortie[1] = Saturer(couleur[1] * c[1] >> 12);
        sortie[2] = Saturer(couleur[2] * c[2] >> 12);
        sortie[3] = couleur[3];
    }

    private static byte Saturer(int v) => v < 0 ? (byte)0 : v > 0xff ? (byte)0xff : (byte)v;
}
