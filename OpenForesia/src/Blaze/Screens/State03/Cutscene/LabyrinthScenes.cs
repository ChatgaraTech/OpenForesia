// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DU LABYRINTHE DES MORTS — zone 5, table 0x0056b0c8.
//
// Ouvert le 2026-09-17 sur le patron de `OldTowerScenes.cs`. L'enregistrement de camera de la scene
// parlante du Labyrinthe est 0x005714b0, le suivant de celui de la tour (0x00571480) au pas de
// 0x30 : il tombe dans `s_ForestSceneBlock_00571420` (Effects/ForestParticlesB.cs), qui le couvre
// deja d'un seul tenant.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x005714b0</c> — <b>l'enregistrement de camera de la scene parlante du
    /// Labyrinthe</b>, <c>0x30</c> octets. Son <c>+0x28</c> (<c>0x005714d8</c>) est la memoire
    /// d'emplacement que les deux amorces passent comme membre cadre.
    /// </summary>
    private static byte* LabyrinthSceneRecord => ForestSceneField(0x005714b0);

    /// <summary>
    /// Ghidra: <c>0x004236f0</c> — <b>la premiere amorce de la scene parlante du Labyrinthe</b>,
    /// sous-zones 4 et 9. Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Aucun script (<c>-1</c>), la memoire d'emplacement du bloc, <b>la piste de musique 0x17</b>, et
    /// <c>+0x98 = 3</c>. Le corps appele, <c>FUN_00423720</c>, est
    /// <see cref="TickForestCutsceneWithDialogue"/> : son texte ne differe de <c>FUN_00419f70</c>
    /// (tour) que par les adresses de l'enregistrement, toutes decalees de <c>0x30</c>
    /// (<c>0x00571480</c> -&gt; <c>0x005714b0</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_004236F0(byte* e)
    {
        byte* record = LabyrinthSceneRecord;
        *(short*)(e + 0x98) = 3;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x17);   // Ghidra: FUN_00423720
    }

    /// <summary>Ghidra: <c>0x00424460</c> — <b>la seconde amorce</b>, meme corps et memes arguments,
    /// avec <c>+0x98 = 4</c>. Absente de <c>Blaze.c</c>.</summary>
    private static void EntityHandler_00424460(byte* e)
    {
        byte* record = LabyrinthSceneRecord;
        *(short*)(e + 0x98) = 4;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x17);   // Ghidra: FUN_00423720
    }
}
