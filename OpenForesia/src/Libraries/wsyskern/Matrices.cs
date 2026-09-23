// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c et
// verifiee au desassembleur (`Legacy/Raw/Wsyskern/ProjectionMatrix.disasm.txt`).
//
// SOURCE DE VERITE : `wsyskern.c`. Base de chargement 0x10000000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES MATRICES D3D DE `wsyskern` — ordinaux 30 (`ZeroMatrix`) et 21 (`ProjectionMatrix`)
//
// `wsyskern` exporte une petite bibliotheque de matrices 4 x 4 flottantes a la maniere des
// utilitaires `d3dutil` du SDK DirectX 5 : `ZeroMatrix`, `IdentityMatrix`, `ProjectionMatrix`,
// `ViewMatrix`… Le jeu n'en importe aucune. Seul `winsys3d` appelle `ProjectionMatrix`, depuis
// `FUN_00a1dbc0` (ordinal 564) — la mise a jour de la projection du device, que `SetGeomScreen`
// (379) declenche. Ce fichier ne porte que ce que cet appel atteint.
//
// ============================================================================================
// `ZeroMatrix` N'EST PAS UNE FONCTION DE CALCUL
//
// Son corps entier est `mov eax, 100581B8h ; ret` : il rend l'adresse d'un bloc statique de 64
// octets. Ce bloc est dans la queue NON INITIALISEE du `.data` (zero au chargement) et aucune
// instruction de la DLL ne l'ecrit — `ProjectionMatrix`, son seul appelant, ne fait que le copier.
// C'est donc une matrice nulle constante, et un tableau statique de seize zeros la remplace
// exactement.
//
// ============================================================================================
// CE QUE CALCULE `ProjectionMatrix(out, near, far, fov)`, lu sur la pile x87
//
//     a  = fov * 0.5                         (0.5 : le double a 0x10049b70)
//     q  = near / far
//     s  = sin a,  c = cos a
//     sf = (float) s                         ecrit dans le slot de `fov` (`fst [esp+58h]`)
//     Q  = sf / (1.0f - q)                   divise le sinus DEJA ARRONDI
//
//     m[0] = m[5] = (float) c     m[10] = (float) Q     m[11] = sf     m[14] = (float) -(Q * near)
//
// les onze autres a zero. **`m[14]` part du Q NON arrondi** : `fst` range `(float) Q` mais laisse
// la valeur etendue sur la pile, et c'est elle que `fmul [near]` multiplie. Le C# garde Q en
// `double` pour la meme raison.
//
// Un ecart assume, le meme que la table de sinus de `winsys3d/Trigonometrie.cs` : le 387 calcule
// en precision etendue, le C# en `double`. Les quatre resultats sont ensuite arrondis en `float`,
// ce qui absorbe l'ecart sauf pour une valeur tombant a moins d'un ulp de `double` d'une frontiere
// entre deux `float`.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class WsyskernMatrices
{
    /// <summary>
    /// DAT_100581b8 — le bloc que rend `ZeroMatrix` : seize `float` nuls, que rien n'ecrit.
    /// Alloue **epingle** (tas des objets epingles) : son adresse sort de la fonction, comme
    /// celle d'une statique C, et ne doit donc jamais bouger.
    /// </summary>
    private static readonly float[] s_MatriceNulle_100581b8 = System.GC.AllocateArray<float>(16, pinned: true);

    /// <summary>
    /// Ghidra: wsyskern.c ZeroMatrix @ 0x10006a90 (ordinal 30) — rend l'adresse d'une matrice
    /// nulle statique (`mov eax, 100581B8h ; ret`).
    /// </summary>
    internal static float* ZeroMatrix()
        => (float*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref s_MatriceNulle_100581b8[0]);

    /// <summary>
    /// Ghidra: wsyskern.c ProjectionMatrix @ 0x10006ab0 (ordinal 21) — la matrice de projection
    /// perspective D3D : voir le pave de tete. Remplit les seize `float` de
    /// <paramref name="sortie"/> et rend ce meme pointeur (`eax` n'est pas touche apres
    /// `mov eax,[esp+4Ch]`). `ret 10h`.
    /// </summary>
    internal static float* ProjectionMatrix(float* sortie, float proche, float lointain, float champ)
    {
        float* m = stackalloc float[16];
        float* nulle = ZeroMatrix();
        for (int i = 0; i < 16; i++) m[i] = nulle[i];

        double a = champ * 0.5;
        double q = (double)proche / lointain;
        double c = System.Math.Cos(a);
        float sf = (float)System.Math.Sin(a);
        double quotient = sf / (1.0f - q);

        m[0] = (float)c;
        m[5] = (float)c;
        m[10] = (float)quotient;
        m[11] = sf;
        m[14] = (float)-(quotient * proche);

        for (int i = 0; i < 16; i++) sortie[i] = m[i];
        return sortie;
    }
}
