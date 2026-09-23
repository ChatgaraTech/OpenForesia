// GENERE par `OpenForesia.Tools extraire-donnees` depuis Legacy/Binaries — NE PAS EDITER A LA MAIN.
// Voir OpenForesia.Tools/ExtractionDonnees.cs. Chaque valeur a ete reencodee et comparee octet par
// octet a la DLL de 1998 lors de la generation.
//
// Les exports de DONNEES de `winsys3d.dll` — ceux que le jeu lit par `GetProcAddress` (voir
// `Winsys3dImage.Exporte`). Les exports de code n'ont plus d'objet : leurs corps sont en C#.

namespace OpenForesia.Libraries;

internal static class Winsys3dExports
{
    /// <summary>(ordinal, adresse, nom — nul si l'export n'en a pas).</summary>
    internal static readonly (int Ordinal, uint Adresse, string? Nom)[] Donnees =
    [
        (757, 0x00a2c47c, "Elo_Lighting"),
        (758, 0x011be758, "Elo_NoLighting"),   // EloNoLighting_011be758
        (781, 0x00dedbe8, "MMXInstalled"),   // MmxInstalle_00dedbe8
        (782, 0x00b6dbd4, "MainMem"),   // MainMem_00b6dbd4
        (797, 0x00a2c478, null),
        (799, 0x011f0eb8, null),
        (800, 0x011f08a0, null),
        (801, 0x011f0e98, null),
        (802, 0x011f0c80, null),
        (803, 0x011f0c88, null),
        (804, 0x011f0c78, null),
        (805, 0x011f0ea8, null),
        (806, 0x011f0eac, null),
        (807, 0x011f0eb0, null),
        (808, 0x011cb418, null),
        (809, 0x011d0890, null),   // DernierXY_011d0890
        (810, 0x011d0824, null),
        (811, 0x011f0e90, null),
        (812, 0x011f0c70, null),
        (813, 0x011d0848, null),
        (814, 0x011d0870, null),   // RegistreRotation_011d0870
        (815, 0x011d0850, null),   // RegistreLumiere_011d0850
        (816, 0x011d0828, null),   // RegistreCouleur_011d0828
        (817, 0x00b6d198, null),   // CouleurEclairage_00b6d198
        (818, 0x00b6dbd0, null),   // Vram_00b6dbd0
        (819, 0x011cb410, null),   // DecalGeomY_011cb410
        (820, 0x011cb414, null),   // DecalGeomX_011cb414
        (821, 0x011d0898, null),   // TableProjection_011d0898
        (825, 0x00a2c504, "TexFilterPossible"),   // TexFilterPossible_00a2c504
        (826, 0x00dedbf4, "VSyncCounter"),   // VSyncCounter_00dedbf4
        (827, 0x011be67c, "VSyncDelayed"),   // VSyncDelayed_011be67c
        (830, 0x011be750, "Zoom8BitTextures"),   // Zoom8BitTextures_011be750
    ];
}
