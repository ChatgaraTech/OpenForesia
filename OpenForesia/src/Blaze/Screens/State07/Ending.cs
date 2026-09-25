// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'ETAT DE JEU 7 — LA FIN. On y arrive a la mort de Belzebuth : le film de fin, puis le retour
// au menu principal. Ouvert le 2026-09-25, quand la premiere partie menee jusqu'au bout a leve
// « FUN_00531b40 (game state 7) not yet reconstructed ».

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: <c>s_ENDING_AVI_0056ac50</c> — releve dans <c>.data</c> : « ENDING.AVI ».</summary>
    private static readonly byte* s_MovieNameEnding_0056ac50 = ToAnsiZ("ENDING.AVI");

    /// <summary>
    /// Ghidra: FUN_00531b40 @ 0x00531b40 — <b>la fin du jeu</b>.
    ///
    /// <para>
    /// La meme charpente que le debut de <see cref="RunMainLoop"/>, dans l'autre sens : le lecteur
    /// de films est cree, le device 3D est <b>suspendu</b> (<c>Ordinal_680(-1)</c>, c'est-a-dire
    /// l'ordinal 789 avec l'action -1 et le mode 2 : minuteur arrete, fenetre rendue,
    /// <c>device[0x34]</c>), le film de fin passe si le flux s'ouvre, puis le device est
    /// <b>rouvert</b> par <c>Ordinal_789(0, DAT_00566cc4)</c> — le meme appel qu'au demarrage — et le
    /// lecteur detruit. L'etat repasse a 0 : le menu principal.
    /// </para>
    /// <para>
    /// Comme au demarrage, un flux qui ne s'ouvre pas saute le film sans rien dire.
    /// </para>
    /// </summary>
    private static void RunEndingState_00531B40()
    {
        Vid.InitMoviePlayer();                                  // Ghidra: FUN_00542f60
        Winsys3d.Ordinal_680(-1);

        if (Vid.TryOpenMovieStream())                           // Ghidra: FUN_00542f40
        {
            Vid.PlayMovie(s_MovieNameEnding_0056ac50, 0);       // Ghidra: FUN_00542eb0
            Vid.CloseMovieStreamIfOpen();                       // Ghidra: FUN_00542f50
        }

        Winsys3d.Ordinal_789(0, Inp.s_ResolutionIndexForClamp); // Ghidra: DAT_00566cc4, voir Startup.cs
        Vid.ShutdownMoviePlayer();                              // Ghidra: FUN_00542f90
        s_GameStateId_005de510 = 0;
    }
}
