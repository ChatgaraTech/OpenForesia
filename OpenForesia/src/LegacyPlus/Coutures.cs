// =====================================================================
// Les coutures — les seuls points par lesquels le code transcrit s'ouvre aux tweaks.
//
// Hors reconstruction (legacy ++, openspec/changes/add-legacy-plus-tweaks-difficulty, design D1-D2).
// Le code de `src/Blaze/` appelle une couture en une ligne, marquée `// Couture legacy ++ (LP-NNN)`,
// et ne nomme jamais un tweak. Chaque couture vaut par défaut l'identité : aucun tweak actif, c'est
// le jeu de 1998.
//
// L'installation n'a pas de ligne au démarrage : le constructeur statique lit `LegacyPlus.ini` et
// installe les tweaks au premier appel d'une couture. C'est un constructeur de type, donc son ordre
// est garanti — contrairement aux initialiseurs d'une classe partielle répartie sur plusieurs
// fichiers.
// =====================================================================

namespace OpenForesia.LegacyPlus;

internal static unsafe class Coutures
{
    /// <summary>Les dégâts d'un coup, de mêlée ou de sort, avant qu'ils ne soient retirés.</summary>
    internal delegate short Degats(uint* attaquant, uint* victime, short degats);

    /// <summary>Les PV d'une entité de scène à la naissance de ses statistiques.</summary>
    internal delegate ushort Pv(byte* stats, ushort pv);

    /// <summary>
    /// Couture LP-001 — à l'entrée d'<c>ApplyDamage_00539A20</c> (Ghidra: FUN_00539a20), par où
    /// passent tous les coups. Rend le montant à retirer.
    /// </summary>
    internal static Degats DegatsSubis = (attaquant, victime, degats) => degats;

    /// <summary>
    /// Couture LP-002 — dans <c>ResetEntityStats</c> (Ghidra: FUN_005389e0), sur les PV maximaux
    /// (<c>+0x48</c>) et courants (<c>+0x4c</c>). Pure : les deux appels rendent la même valeur.
    /// </summary>
    internal static Pv PvALaNaissance = (stats, pv) => pv;

    /// <summary>Le montant fixe d'une fiche de sort, avant qu'il ne s'ajoute aux dégâts.</summary>
    internal delegate ushort Montant(uint* attaquant, byte* blocAttaquant, ushort montant);

    /// <summary>
    /// Couture LP-003 — dans <c>ResolveSpellHit_0053A2B0</c> (Ghidra: FUN_0053a2b0), sur le
    /// montant de la fiche (<c>fiche + 0x18 + variante * 2</c>), à l'intérieur de la condition
    /// d'élément d'origine. Rend le montant à ajouter.
    /// </summary>
    internal static Montant MontantDeSort = (attaquant, blocAttaquant, montant) => montant;

    static Coutures()
    {
        try
        {
            ReglagesLegacyPlus.Lire();
            Difficulte.Installer();
            ScalingMagique.Installer();
        }
        catch (Exception ex)
        {
            // Un réglage cassé ne doit jamais empêcher le jeu de tourner : retour au jeu de 1998.
            Console.Error.WriteLine($"[LEGACY++] installation des tweaks impossible ({ex.Message}) — aucun tweak actif");
            DegatsSubis = (attaquant, victime, degats) => degats;
            PvALaNaissance = (stats, pv) => pv;
            MontantDeSort = (attaquant, blocAttaquant, montant) => montant;
        }
    }
}
