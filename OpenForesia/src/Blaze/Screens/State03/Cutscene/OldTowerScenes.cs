// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES CINEMA DE LA VIEILLE TOUR — zone 4.
//
// Ouvert le 2026-09-17, sur le patron de `MineScenes.cs` : le corps des scenes est celui de la
// foret, recompile par zone a la base du bloc prive pres, et ce fichier ne porte que ce qui est
// PROPRE a la tour — les amorces, et l'adresse de leur enregistrement.
//
// LE BLOC PRIVE DE LA TOUR EST 0x00571480, et il n'a demande aucune decision de plan memoire :
// c'est le troisieme enregistrement (0x00571420 + 2 * 0x30) du domaine que
// `s_ForestSceneBlock_00571420` (Effects/ForestParticlesB.cs) couvre deja d'un seul tenant. Il passe
// donc par ForestSceneField, adresse par adresse, comme tout ce qui vit dans ce domaine.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x00571480</c> — <b>l'enregistrement de camera de la scene parlante de la
    /// tour</b>, <c>0x30</c> octets, sur le meme plan que <see cref="ForestSceneRecord"/>. Son
    /// <c>+0x28</c> (<c>0x005714a8</c>) est la memoire d'emplacement que les deux amorces passent
    /// comme membre cadre.
    /// </summary>
    private static byte* OldTowerSceneRecord => ForestSceneField(0x00571480);

    /// <summary>
    /// Ghidra: <c>0x00419f40</c> — <b>la premiere amorce de la scene parlante de la tour</b>,
    /// sous-zones 3, 7 et 9, table <c>0x0056b0c8</c>. Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// La forme exacte de <see cref="EntityHandler_0040AAC0"/> (mine) : aucun script (<c>-1</c>), la
    /// memoire d'emplacement du bloc, et une piste de musique — ici <b>la piste 0x10</b>, la ou la
    /// mine demande la 8 — avec <c>+0x98 = 0x3d</c> au lieu de <c>0</c>. Le corps appele,
    /// <c>FUN_00419f70</c>, est <see cref="TickForestCutsceneWithDialogue"/> : le diff STRICT avec
    /// <c>FUN_0040aaf0</c> ne rend que les adresses de l'enregistrement, toutes decalees de
    /// <c>0xc0</c> (<c>0x005713c0</c> -&gt; <c>0x00571480</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_00419F40(byte* e)
    {
        byte* record = OldTowerSceneRecord;
        *(short*)(e + 0x98) = 0x3d;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x10);   // Ghidra: FUN_00419f70
    }

    /// <summary>
    /// Ghidra: <c>0x0041b3f0</c> — <b>la seconde amorce</b>, meme bloc. La forme de
    /// <see cref="EntityHandler_0040B8D0"/> : <b>le script 1</b>, la memoire d'emplacement, et
    /// <c>-1</c> pour ne pas toucher a la musique, avec <c>+0x98 = 3</c>. Absente de
    /// <c>Blaze.c</c>.
    /// </summary>
    private static void EntityHandler_0041B3F0(byte* e)
    {
        byte* record = OldTowerSceneRecord;
        *(short*)(e + 0x98) = 3;
        TickForestCutsceneWithDialogue(e, record, 1, record[0x28], 0xffff);   // Ghidra: FUN_00419f70
    }
}
