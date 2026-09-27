// =====================================================================
// LP-003 — le montant des sorts du groupe monte avec la MAG du lanceur.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-spell-scaling).
// Dans le jeu de 1998, un petit sort (diviseur 3 ou 4) ne gagne rien avec le niveau : sa part
// `MAG / diviseur − DEF` reste nulle contre un monstre de fin de jeu, et seul le montant fixe de la
// fiche reste. Le tweak multiplie ce montant par `1 + coef × MAG / 100`, en entiers.
//
// Groupe seulement ; ne tire aucun nombre aléatoire ; `MAG / diviseur − DEF` n'est pas touchée.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class ScalingMagique
{
    /// <summary>Le bit de nature « membre du groupe » du mot 0 d'une entité.</summary>
    private const uint MembreDuGroupe = 0x800000;

    /// <summary>La puissance magique d'un bloc de combat, celle que la formule d'origine divise.</summary>
    private const int DecalageMag = 0x38;

    /// <summary>
    /// Plafond du montant : le total des dégâts passe ensuite en <c>short</c>, et une attaque
    /// bornée à 999 plus l'échange élémentaire doivent y tenir encore (design D3).
    /// </summary>
    private const int MontantMax = 30000;

    internal static void Installer()
    {
        int coef = ReglagesLegacyPlus.ScalingMagique;       // centièmes
        if (coef == 0) return;

        Coutures.Montant precedent = Coutures.MontantDeSort;
        Coutures.MontantDeSort = (attaquant, blocAttaquant, montant) =>
        {
            montant = precedent(attaquant, blocAttaquant, montant);
            if ((*attaquant & MembreDuGroupe) == 0) return montant;

            int mag = Math.Max(0, (int)*(short*)(blocAttaquant + DecalageMag));
            int multiplie = montant + montant * coef * mag / 10000;
            return (ushort)Math.Min(multiplie, MontantMax);
        };
    }
}
