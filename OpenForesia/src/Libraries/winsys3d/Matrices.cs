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
// dans `DLLGlobales.md`, et ce qui le rend portable alors que ses huit voisins ne le sont pas.
//
// Ce fichier est donc une amorce : quand la masse se decoupera, les huit autres viendront ici.
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
}
