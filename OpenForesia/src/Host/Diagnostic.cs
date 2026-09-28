// =====================================================================
// Les outils de mise au point — présents dans la version Debug, absents de la version publiée.
//
// Demande de Vincent (2026-09-28) : aucun journal dans le jeu du joueur. `publish-release.ps1`
// publie en Release, où DEBUG n'est pas défini : tout ce qui se règle sur cet interrupteur y est
// éteint, quelle que soit la variable d'environnement ou la clé qu'on pose. Ce qu'il couvre :
//   - les sondes de couverture (handlers/objets/sorts-exerces.tsv, CoverageProbes.cs) ;
//   - la tournée du banc d'essai et son tournee-faite.txt (AiBench.cs) ;
//   - les captures de textures en PNG (D3D11Backend.DumpTexture) ;
//   - VS.Log (LB-054, winsys3d/Image.cs) ;
//   - la sortie console (Program.cs) : les lignes [RESERVE], [MARCHANDS], [BOUTIQUE]…
// Les trois journaux LegacyPlus (combat, position, VRAM) l'étaient déjà : `#if DEBUG` dans
// Coutures.cs.
// =====================================================================

namespace OpenForesia.Blaze;

internal static class Diagnostic
{
#if DEBUG
    internal static readonly bool Actif = true;
#else
    internal static readonly bool Actif = false;
#endif
}
