// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c et
// verifiee au desassembleur (`Legacy/Raw/Wsyskern/ProjectionMatrix.disasm.txt`).
//
// SOURCE DE VERITE : `wsyskern.c`. Base de chargement 0x10000000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES MATRICES D3D DE `wsyskern` — ordinaux 30 (`ZeroMatrix`), 16 (`IdentityMatrix`), 25
// (`ScaleMatrix`) et 21 (`ProjectionMatrix`)
//
// `wsyskern` exporte une petite bibliotheque de matrices 4 x 4 flottantes a la maniere des
// utilitaires `d3dutil` du SDK DirectX 5 : `ZeroMatrix`, `IdentityMatrix`, `ProjectionMatrix`,
// `ViewMatrix`… Le jeu n'en importe aucune. `winsys3d` appelle `ProjectionMatrix`, depuis
// `FUN_00a1dbc0` (ordinal 564) — la mise a jour de la projection du device, que `SetGeomScreen`
// (379) declenche —, et `ScaleMatrix` (25), deux fois depuis son initialisation (789). Ce fichier
// ne porte que ce que ces appels atteignent.
//
// ============================================================================================
// `ZeroMatrix` ET `IdentityMatrix` NE SONT PAS DES FONCTIONS DE CALCUL
//
// Leurs corps entiers sont `mov eax, 100581B8h ; ret` et `mov eax, 10058178h ; ret` : ils rendent
// l'adresse d'un bloc statique de 64 octets. Les deux blocs sont dans la queue NON INITIALISEE du
// `.data`, et **une seule fonction les ecrit** : `FUN_10006a40`, appelee a l'initialisation de la
// DLL, qui met le premier a zero et le second a l'unite (quatre `1.0f` a +0, +0x14, +0x28, +0x3c).
// Ghidra ne la relie a aucun des deux noms — les six references sont des adresses brutes, trouvees
// en cherchant les octets `78 81 05 10` dans le binaire. Apres elle, personne n'ecrit plus : les
// appelants ne font que copier. Deux tableaux statiques, remplis par la meme fonction une fois
// pour toutes, les remplacent exactement.
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
    /// DAT_100581b8 — le bloc que rend `ZeroMatrix` : seize `float` nuls. Alloue **epingle** (tas
    /// des objets epingles) : son adresse sort de la fonction, comme celle d'une statique C, et ne
    /// doit donc jamais bouger.
    /// </summary>
    private static readonly float[] s_MatriceNulle_100581b8 = System.GC.AllocateArray<float>(16, pinned: true);

    /// <summary>DAT_10058178 — le bloc que rend `IdentityMatrix`, epingle lui aussi.</summary>
    private static readonly float[] s_MatriceUnite_10058178 = System.GC.AllocateArray<float>(16, pinned: true);

    static WsyskernMatrices() => InitialiserMatrices();

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10006a40 @ 0x10006a40 — appelee a l'initialisation de la DLL : met la
    /// matrice nulle a zero, la matrice unite a zero, puis pose les quatre `1.0f` de sa diagonale
    /// (`m[15]`, `m[10]`, `m[5]`, `m[0]`, dans cet ordre).
    /// </summary>
    private static void InitialiserMatrices()
    {
        for (int i = 0; i < 16; i++) s_MatriceNulle_100581b8[i] = 0;
        for (int i = 0; i < 16; i++) s_MatriceUnite_10058178[i] = 0;
        s_MatriceUnite_10058178[15] = 1.0f;
        s_MatriceUnite_10058178[10] = 1.0f;
        s_MatriceUnite_10058178[5] = 1.0f;
        s_MatriceUnite_10058178[0] = 1.0f;
    }

    /// <summary>
    /// Ghidra: wsyskern.c ZeroMatrix @ 0x10006a90 (ordinal 30) — rend l'adresse d'une matrice
    /// nulle statique (`mov eax, 100581B8h ; ret`).
    /// </summary>
    internal static float* ZeroMatrix()
        => (float*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref s_MatriceNulle_100581b8[0]);

    /// <summary>
    /// Ghidra: wsyskern.c IdentityMatrix @ 0x10006aa0 (ordinal 16) — rend l'adresse de la matrice
    /// unite statique (`mov eax, 10058178h ; ret`).
    /// </summary>
    internal static float* IdentityMatrix()
        => (float*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref s_MatriceUnite_10058178[0]);

    /// <summary>
    /// Ghidra: wsyskern.c ScaleMatrix @ 0x10007050 (ordinal 25) — la matrice d'echelle : l'unite, avec
    /// <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> sur `m[0]`, `m[5]`, `m[10]`.
    /// Copiee dans <paramref name="sortie"/>, qu'elle rend (`mov eax,[esp+4Ch]`, que Ghidra perd en
    /// declarant la fonction `void`). `ret 10h`. Verifie au desassembleur
    /// (`Legacy/Raw/Wsyskern/ScaleMatrix.disasm.txt`).
    /// </summary>
    internal static float* ScaleMatrix(float* sortie, float x, float y, float z)
    {
        float* m = stackalloc float[16];
        float* unite = IdentityMatrix();
        for (int i = 0; i < 16; i++) m[i] = unite[i];
        m[0] = x;
        m[5] = y;
        m[10] = z;
        for (int i = 0; i < 16; i++) sortie[i] = m[i];
        return sortie;
    }

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
