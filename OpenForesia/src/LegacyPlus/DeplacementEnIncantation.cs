// =====================================================================
// LP-006 — se déplacer en lançant un sort, à une part de la vitesse de marche.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-move-while-casting).
// Dans le jeu de 1998, la croix calcule la vitesse de marche même pendant une incantation, puis
// `ApplyGroundMotion_0051A020` la remet à zéro : le lanceur est cloué. Ici, il en garde une part.
// Le lanceur garde sa pose d'incantation : il glisse. La combinaison (boutons d'action) n'est pas
// touchée. Ne tire aucun nombre aléatoire.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class DeplacementEnIncantation
{
    internal static void Installer()
    {
        int coef = ReglagesLegacyPlus.DeplacementEnIncantation;      // centièmes, 0..100
        if (coef == 0) return;

        Coutures.Vitesse precedent = Coutures.VitesseEnIncantation;
        Coutures.VitesseEnIncantation = (membre, vitesse) =>
        {
            int gardee = precedent(membre, vitesse);                    // 0 à l'origine
            return Math.Max(gardee, (int)((long)vitesse * coef / 100));
        };
    }
}
