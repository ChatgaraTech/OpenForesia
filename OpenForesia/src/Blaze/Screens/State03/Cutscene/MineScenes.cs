// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SCENES CINEMA DE LA MINE ABANDONNEE — zone 1.
//
// Ouvert le 2026-09-16, sur le patron de `ForestScenes.cs`, et pour la meme raison qu'il existe :
// une scene cinema n'est ni une particule ni un decor. Les corps sont ceux de la foret — le jeu de
// 1998 les a recompiles pour chaque zone en ne changeant que la base du bloc prive — et ce fichier
// ne porte que ce qui est PROPRE a la mine : les amorces, et l'adresse de leur bloc.
//
// Les VERBES que les scenes emploient sont dans `CutsceneVerbs.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LA SCENE PARLANTE DE LA MINE (2026-09-16)
    //
    // FUN_0040aaf0 est FUN_00411270 — la scene parlante de la foret, TickForestCutsceneWithDialogue —
    // a la base du bloc prive pres. Comparaison des textes de Blaze.c adresses masquees, puis `diff`
    // brut : les treize lignes qui different portent chacune une adresse de 0x005713c0.. contre
    // 0x00571420.., toutes decalees de la meme valeur, 0x60. Deux amorces l'appellent, relevees au desassembleur.
    //
    // LE BLOC PRIVE EST SOUS LA FRONTIERE DE .data, ET C'EST CE QUI DECIDE DE SON ADRESSAGE.
    // Les enregistrements de scene se suivent au pas de 0x30 : 0x005713c0, 0x005713f0, puis les
    // 0x00571420 et 0x00571450 de la foret, et Blaze.c en cite jusqu'a 0x005714fc. Mais
    // 0x005713c0..0x005713ff est DEJA couvert par une allocation, `s_ViewParams_00571270`
    // (GameMemory.cs), dont la borne 0x00571400 est la fin du contenu de .data dans le fichier et
    // non une frontiere entre deux structures. Allouer ici un second bloc pour la mine aurait donne
    // deux noms a la meme adresse — le defaut d'aliasing que MemoryLayoutTests.cs surveille.
    // L'enregistrement de la mine passe donc par Mem.ViewParams, adresse par adresse.
    //
    // Consequence a garder en tete : l'enregistrement 0x005713f0 CHEVAUCHE la borne (0x005713f0..
    // 0x0057141f) et tomberait a cheval sur deux allocations, avec un trou de 0x00571400 a
    // 0x0057141f qu'aucune allocation C# ne couvre. Aucun code transcrit ne l'emploie aujourd'hui.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x005713c0</c> — <b>l'enregistrement de camera de la scene parlante de la
    /// mine</b>, <c>0x30</c> octets, sur le meme plan que <see cref="ForestSceneRecord"/>. Son
    /// <c>+0x28</c> (<c>0x005713e8</c>) est la memoire d'emplacement que les deux amorces
    /// passent comme membre cadre.
    /// </summary>
    private static byte* MineSceneRecord => Mem.ViewParams(0x005713c0);

    /// <summary>
    /// Ghidra: <c>0x0040aac0</c> — <b>la premiere amorce de la scene parlante</b>, sous-type
    /// <b>7</b> du bloc <c>0x005549a8</c> (table <c>0x0056b0c8</c>, donc <c>0xc7</c> du pool
    /// <c>0x005ff400</c>), sous-zones 3, 4 et 9. Absente de <c>Blaze.c</c> ;
    /// <c>Legacy/Raw/Blaze/FUN_0040AAC0.asm</c>.
    ///
    /// <para>
    /// Elle pose <c>+0x98 = 0</c> et appelle <c>FUN_0040aaf0</c> avec <b>aucun script</b>
    /// (<c>-1</c>), la memoire d'emplacement du bloc, et <b>la piste 8</b>. C'est la premiere
    /// amorce connue qui demande un changement de musique : le bloc que
    /// <see cref="TickForestCutsceneWithDialogue"/> documente comme « jamais execute par les
    /// amorces » l'est par celle-ci — piste lancee si elle n'est pas deja en cours, effet
    /// <c>0x59</c> sur chaque membre vivant.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040AAC0(byte* e)
    {
        byte* record = MineSceneRecord;
        *(short*)(e + 0x98) = 0;
        TickForestCutsceneWithDialogue(e, record, -1, record[0x28], 8);   // Ghidra: FUN_0040aaf0
    }

    /// <summary>
    /// Ghidra: <c>0x0040b8d0</c> — <b>la seconde amorce</b>, sous-type <b>8</b> du meme bloc
    /// (<c>0xc8</c>). La forme exacte de <see cref="EntityHandler_00411240"/> : <c>+0x98 = 1</c>,
    /// le <b>script 1</b>, la memoire d'emplacement, et <c>-1</c> pour ne pas toucher a la musique.
    /// Absente de <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_0040B8D0.asm</c>.
    ///
    /// <para>
    /// <b>Elle differe de l'amorce de la foret par un octet</b> : celle-ci pose <c>+0x98 = 0x39</c>,
    /// celle de la mine <c>1</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_0040B8D0(byte* e)
    {
        byte* record = MineSceneRecord;
        *(short*)(e + 0x98) = 1;
        TickForestCutsceneWithDialogue(e, record, 1, record[0x28], 0xffff);   // Ghidra: FUN_0040aaf0
    }
}
