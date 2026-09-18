// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES DU CHATEAU DE L'IMMORTEL — zone 6, table 0x0056b0c8.
//
// Ouvert le 2026-09-18 avec le palier 1, sur le patron de `LabyrinthScenes.cs`. L'enregistrement de
// camera de la scene parlante du chateau est 0x005714e0, le suivant de celui du Labyrinthe
// (0x005714b0) au pas de 0x30. La suite est maintenant complete et se lit d'un coup :
//
//     0x00571420  foret 0        0x00571450  foret 1        0x00571480  vieille tour
//     0x005714b0  Labyrinthe     0x005714e0  chateau
//
// plus 0x005713c0 (mine) et 0x005713f0 (montagne du dragon de feu), qui sont SOUS la frontiere de
// .data et passent par Mem.ViewParams — voir `MineScenes.cs`.
//
// CELUI-CI EST LE PREMIER A FRANCHIR 0x00571500. Son +0x20 tombe exactement sur 0x00571500 et son
// +0x28 sur 0x00571508, c'est-a-dire dans la plage que s_EntityState_00571500 couvre AUSSI. Le
// recouvrement latent que GameMemory.cs signale depuis le 2026-09-17 (0x00571500..0x00571667 vu par
// deux noms) reste latent : aucune des trois adresses qu'EntityState sert reellement (0x00571528,
// 0x00571540, 0x00571570) n'est touchee ici. Mais la marge a fondu, et la fusion des deux plages en
// une seule arene 0x00571270..0x00571a38 est le correctif a mener — comme changement delibere, pas
// au detour d'un lot.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: <c>0x005714e0</c> — <b>l'enregistrement de camera de la scene parlante du
    /// chateau de l'immortel</b>, <c>0x30</c> octets. Son <c>+0x28</c> (<c>0x00571508</c>) est la
    /// memoire d'emplacement que les deux amorces passent comme membre cadre.
    /// </summary>
    private static byte* ImmortalCastleSceneRecord => ForestSceneField(0x005714e0);

    /// <summary>
    /// Ghidra: <c>0x0042c3c0</c> — <b>la premiere amorce de la scene parlante du chateau</b>,
    /// creneau <b>17</b> du bloc <c>0x00559e28</c> (table <c>0x0056b0c8</c>, sous-zones 5 et 11).
    /// Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// Aucun script (<c>-1</c>), la memoire d'emplacement du bloc, <b>la piste de musique
    /// 0x0e</b>, et <c>+0x98 = 0x0a</c>. Le corps appele, <c>FUN_0042c3f0</c>, est
    /// <see cref="TickForestCutsceneWithDialogue"/> : ses 372 instructions ne different de
    /// <c>FUN_0040aaf0</c> (mine) que par les treize adresses de l'enregistrement, toutes decalees
    /// de <c>0x120</c> (<c>0x005713c0</c> -&gt; <c>0x005714e0</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_0042C3C0(byte* e)
    {
        byte* record = ImmortalCastleSceneRecord;
        *(short*)(e + 0x98) = 0x0a;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x0e);   // Ghidra: FUN_0042c3f0
    }

    /// <summary>Ghidra: <c>0x0042d100</c> — <b>la seconde amorce</b>, creneau <b>18</b> du meme
    /// bloc : meme corps et memes arguments, avec <c>+0x98 = 0x0b</c>. Absente de
    /// <c>Blaze.c</c>.</summary>
    private static void EntityHandler_0042D100(byte* e)
    {
        byte* record = ImmortalCastleSceneRecord;
        *(short*)(e + 0x98) = 0x0b;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 0x0e);   // Ghidra: FUN_0042c3f0
    }
}
