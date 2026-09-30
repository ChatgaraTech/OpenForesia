// =====================================================================
// LP-001, LP-002 — la difficulté : dégâts et PV des monstres, multipliés (1 = jeu d'origine).
// Les multiplicateurs arrivent en centièmes (`ReglagesLegacyPlus`) : 100 = ×1, 250 = ×2,5.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-tweaks-difficulty, design D3-D4).
// Ne tire aucun nombre aléatoire. Les bornes tiennent au type : PV et dégâts sont des `short` signés
// partout où le jeu les compare, et le jeu ne fait jamais zéro dégât — dégâts dans [1, 32767], PV
// dans [1, 32000] (une marge pour les soins, voir PvMax).
//
// S'applique PAR-DESSUS la difficulté du jeu de 1998 (`s_DifficultyBonus_0056d2c0`), qui n'est pas
// touchée.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class Difficulte
{
    /// <summary>Le bit de nature « membre du groupe » du mot 0 d'une entité.</summary>
    private const uint MembreDuGroupe = 0x800000;

    internal static void Installer()
    {
        int degats = ReglagesLegacyPlus.DegatsMonstres;
        if (degats != 100)
        {
            // LP-001 : seuls les coups reçus par le groupe changent.
            Coutures.Degats precedent = Coutures.DegatsSubis;
            Coutures.DegatsSubis = (attaquant, victime, d) =>
            {
                d = precedent(attaquant, victime, d);
                return (*victime & MembreDuGroupe) != 0 ? (short)Borner(d * degats / 100) : d;
            };
        }

        int pv = ReglagesLegacyPlus.PvMonstres;
        if (pv != 100)
        {
            // LP-002 : toute entité de scène, à la naissance de ses statistiques.
            Coutures.Pv precedent = Coutures.PvALaNaissance;
            Coutures.PvALaNaissance = (stats, p) =>
            {
                p = precedent(stats, p);
                return (ushort)Math.Clamp(p * pv / 100, 1, PvMax);
            };
        }
    }

    /// <summary>
    /// Les PV d'un monstre au plus. Pas 32 767 : un soin ou une régénération ajoute AVANT de ramener
    /// au maximum (WorldSimulationStep, StatusEffects), et un PV qui déborde le <c>short</c> passe
    /// négatif — le bit de mort est posé sans la séquence de mort, le monstre reste figé et
    /// intouchable (le Dragon rouge de l'arène, 2026-09-30). Un nombre rond, lisible pour le joueur.
    /// </summary>
    private const int PvMax = 32000;

    private static int Borner(int valeur) => Math.Clamp(valeur, 1, short.MaxValue);
}
