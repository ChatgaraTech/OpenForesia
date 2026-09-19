// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DES RUINES SOUTERRAINES — zone 7, table 0x0056adc8, bloc 0x0055a1a8.
//
// Ouvert le 2026-09-19 avec la zone. Le bloc de decor des ruines est presque entierement fait de
// corps communs (0x004734B0 et ses voisins, `Handlers/Common.cs`) ; UN SEUL lui est propre, le son
// d'ambiance du creneau 9. Ce que la zone DECLENCHE est dans `UndergroundRuinsMecanismes.cs`.
//
// Le son d'une zone se lit par `ZoneSound` (`Mine.cs`) : meme classe partielle.
// Les particules de la zone sont dans `Effects/UndergroundRuinsParticles.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: FUN_00466be0 @ 0x00466be0 — creneau <b>9</b> du bloc <c>0x0055a1a8</c>. Decompilee
    /// dans <c>Blaze.c</c>, 23 instructions.
    ///
    /// <para>
    /// <b>Le son d'ambiance dont le placement regle le volume.</b> Toutes les vingt images — a la
    /// premiere comprise, puisque le son part quand l'horloge <c>+0x10</c> vaut <b>zero</b> et
    /// non quand elle atteint vingt —, voix <c>0x10</c> de la zone, hauteur <c>0x400</c>, et un
    /// gain egal sur les deux canaux qui vaut <c>+0x14 &lt;&lt; 8</c> : c'est la fiche de placement
    /// qui dit fort ou bas.
    /// </para>
    /// <para>
    /// Aucune naissance, aucune distance a la camera : le groupe l'entend pareil partout dans la
    /// sous-zone, comme <see cref="EntityHandler_004560A0"/> de la montagne. <b>L'horloge est
    /// <c>+0x10</c></b>, le champ qui porte l'etat chez presque tous les autres corps.
    /// </para>
    /// </summary>
    private static void EntityHandler_00466BE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int gain = *(short*)(e + 0x14) << 8;
            Aud.PlaySoundEffect(ZoneSound(0x10), 0x400, gain, gain);   // Ghidra: FUN_0053cea0
        }

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) == 0x14) *(short*)(e + 0x10) = 0;
    }
}
