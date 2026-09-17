// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE FLASH BLANC QUI FERME UNE ZONE — le corps 0x004183d0 et ses QUATRE amorces.
//
// Sorti de `ForestParticlesA.cs` le 2026-09-16 : ce n'est pas un effet de la foret. La mine
// abandonnee emploie le meme corps, par DEUX amorces a elle — une par groupe de sous-zones —, et
// le nom du fichier mentait donc pour elles. Toute zone qui reutilisera ce flash viendra ici.
//
// LE CORPS EST DECOMPILE, LES AMORCES NE LE SONT PAS. C'est le troisieme cas de figure que la
// liste des gestionnaires manquants recouvre, apres les corps dupliques et les corps jamais
// decompiles : un corps DECOMPILE atteint par une amorce qui, elle, ne l'est pas. Verifier ou
// pointe le `call` d'une amorce coute deux minutes et evite de transcrire a la main ce que Ghidra
// a deja rendu — les quatre amorces de ce fichier l'ont prouve quatre fois.
//
// LES QUATRE AMORCES NE DIFFERENT QUE PAR DEUX CHOSES : le genre pose en `+0x5c`, et le NUMERO
// D'ENREGISTREMENT dont elles recopient la pose. Et par zone, elles vont par PAIRES : une par
// groupe de sous-zones, copie exacte l'une de l'autre.
//
//   0x00410370   foret   +0x5c = 0x3a   enregistrement 0
//   0x004137e0   foret   +0x5c = 0x3a   enregistrement 0   (copie exacte de la precedente)
//   0x004060c0   mine    +0x5c = 0x39   enregistrement 6   (sous-zones 0 a 2)
//   0x00407fe0   mine    +0x5c = 0x39   enregistrement 6   (sous-zones 5 a 8, copie exacte)
//
// `DAT_005fa9a4` et ses voisins ne sont pas des globales isolees — pas plus que `DAT_005fa5fc`
// pour la foret. Ce sont les champs `+0x1c`/`+0x20`/`+0x24` (position) et `+0x30`/`+0x32`/`+0x34`
// (orientation) d'un enregistrement de la troisieme table d'entites, `0x005fa5e0`, dont les
// enregistrements font 0x9c octets : 0x005fa9a4 - 0x1c - 0x005fa5e0 = 0x3a8 = 6 x 0x9c, tout
// juste. La table en porte 0x80, l'indice est valide.
//
// Le flash se joue donc **la ou se tient une entite designee de la zone**, pas a la position que
// l'entite de flash portait deja.
//
// Relevés bruts : Legacy/Raw/Blaze/FUN_00410370.asm, FUN_004060C0.asm.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LE FLASH BLANC DE SORTIE DE ZONE, ET SES DEUX AMORCES (2026-08-30)
    //
    // Une TROISIEME amorce est venue le 2026-09-16, celle de la mine abandonnee (0x004060c0) —
    // voir plus bas. Le bandeau ci-dessous est celui d'origine, garde tel quel.
    //
    // 0x00410370 et 0x004137e0 sont absents de Blaze.c, mais ce ne sont pas des lacunes : ce
    // sont deux AMORCES de dix-neuf instructions, identiques l'une a l'autre au dernier octet
    // pres, qui posent six champs puis sautent dans FUN_004183d0 — laquelle est, elle,
    // decompilee. Le releve est dans Legacy/Raw/Blaze/FUN_00410370.asm.
    //
    // C'est le troisieme cas de figure que la liste des gestionnaires manquants recouvre, apres
    // les corps dupliques et les corps jamais decompiles : un corps DECOMPILE atteint par une
    // amorce qui, elle, ne l'est pas. Verifier ou pointe le `call` d'une amorce coute deux
    // minutes et evite de transcrire a la main ce que Ghidra a deja rendu.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00410370</c> — <b>l'amorce du flash de sortie de zone</b>. Absente de
    /// <c>Blaze.c</c> ; dix-neuf instructions.
    ///
    /// <para>
    /// Elle pose <c>+0x5c = 0x3a</c> puis <b>recopie la pose de l'emplacement 0 de la table de
    /// zone</b> — <c>DAT_005fa5fc</c> et ses voisins ne sont pas des globales isolees, ce sont les
    /// champs <c>+0x1c</c>, <c>+0x20</c>, <c>+0x24</c> (position) et <c>+0x30</c>, <c>+0x32</c>,
    /// <c>+0x34</c> (orientation) de <c>DAT_005fa5e0 + 0</c>. Le flash se joue donc la ou se tient
    /// la premiere entite de la zone, pas a la position que l'entite portait deja.
    /// </para>
    /// </summary>
    private static void EntityHandler_00410370(byte* e)
    {
        byte* anchor = Mem.Entity(0x005fa5e0);

        *(int*)(e + 0x5c) = 0x3a;
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);
        *(short*)(e + 0x30) = *(short*)(anchor + 0x30);
        *(short*)(e + 0x32) = *(short*)(anchor + 0x32);
        *(short*)(e + 0x34) = *(short*)(anchor + 0x34);

        TickAreaExitFlash_004183D0((uint*)e);
    }

    /// <summary>Ghidra: <c>0x004137e0</c> — la seconde amorce. Comparee instruction par
    /// instruction a <see cref="EntityHandler_00410370"/> : <b>zero difference</b>, pas meme une
    /// planche. Le compilateur a emis la meme amorce deux fois.</summary>
    private static void EntityHandler_004137E0(byte* e) => EntityHandler_00410370(e);

    /// <summary>
    /// Ghidra: <c>0x004060c0</c> — <b>la troisieme amorce, celle de la mine abandonnee</b>
    /// (zone 1, sous-zones 0, 1 et 2 ; sous-type <b>33</b> du bloc <c>0x00553ca8</c>, que la table
    /// <c>0x0056b0c8</c> installe pour les trois sous-zones). Absente de <c>Blaze.c</c> ;
    /// dix-sept instructions, relevees au desassembleur —
    /// <c>Legacy/Raw/Blaze/FUN_004060C0.asm</c>.
    ///
    /// <para>
    /// <b>Meme corps que <see cref="EntityHandler_00410370"/>, a DEUX differences pres</b>, et le
    /// desassemblage les donne toutes les deux : le genre pose en <c>+0x5c</c> vaut <c>0x39</c> au
    /// lieu de <c>0x3a</c>, et l'ancre lue est l'enregistrement <b>6</b> de la table
    /// <c>0x005fa5e0</c> au lieu du <b>0</b>.
    /// </para>
    /// <para>
    /// <b>L'ancre etait la seule vraie question.</b> Le binaire lit six adresses qui ressemblent a
    /// des globales isolees — <c>0x005fa9a4</c>, <c>0x005fa9a8</c>, <c>0x005fa9ac</c>,
    /// <c>0x005fa9b8</c>, <c>0x005fa9ba</c>, <c>0x005fa9bc</c>. Elles n'en sont pas :
    /// <c>0x005fa9a4 - 0x1c - 0x005fa5e0 = 0x3a8 = 6 * 0x9c</c>, tout juste. Ce sont les champs de
    /// position et d'orientation du septieme enregistrement de la troisieme table d'entites, qui
    /// en porte <c>0x80</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_004060C0(byte* e)
        => EnterAreaExitFlash(e, 0x39, 6, 0);

    /// <summary>
    /// Ghidra: <c>0x00418370</c> et <c>0x00418640</c> — <b>les deux amorces de la vieille tour</b>,
    /// creneaux 46 et 47 de la table <c>0x0056b0c8</c>, sous-zones 5, 6, 8 et 10. Absentes de
    /// <c>Blaze.c</c> ; dix-neuf instructions chacune.
    ///
    /// <para>
    /// <b>La forme de <see cref="EntityHandler_004060C0"/>, avec un ajout</b> : le genre vaut
    /// <c>0x3b</c>, l'ancre est l'enregistrement <b>0</b> (<c>0x005fa5fc - 0x1c</c>) pour la
    /// premiere et <b>2</b> (<c>0x005fa734 - 0x1c = 0x005fa5e0 + 2 × 0x9c</c>) pour la seconde, et
    /// <b>Z est decale de <c>0x64000</c></b> (<c>add ecx,64000h</c>) — le flash se joue 100 unites
    /// plus loin que l'ancre. C'est la seule amorce de la famille a deplacer son point.
    /// </para>
    /// </summary>
    private static void EntityHandler_00418370(byte* e) => EnterAreaExitFlash(e, 0x3b, 0, 0x64000);

    /// <summary>Ghidra: <c>0x00418640</c> — la seconde amorce de la tour, ancre 2. Voir
    /// <see cref="EntityHandler_00418370"/>.</summary>
    private static void EntityHandler_00418640(byte* e) => EnterAreaExitFlash(e, 0x3b, 2, 0x64000);

    /// <summary>
    /// Ghidra: <c>0x00423240</c> — <b>l'amorce du Labyrinthe des morts</b>, creneau de la table
    /// <c>0x0056b0c8</c> des sous-zones 4 et 9. Absente de <c>Blaze.c</c> ; dix-neuf instructions.
    /// Le genre vaut <c>0x2f</c>, l'ancre est l'enregistrement <b>0</b>, et c'est <b>X</b> qui est
    /// decale, de <c>-0x64000</c> (<c>add ecx,0FFF9C000h</c> sur <c>0x005fa5fc</c>) — la ou la tour
    /// decalait Z vers l'avant.
    /// </summary>
    private static void EntityHandler_00423240(byte* e) => EnterAreaExitFlash(e, 0x2f, 0, 0, -0x64000);

    /// <summary>Le corps commun des amorces a ancre : le genre en <c>+0x5c</c>, la pose de
    /// l'enregistrement <paramref name="anchorIndex"/> de <c>0x005fa5e0</c> (position, Z decale de
    /// <paramref name="zShift"/> et X de <paramref name="xShift"/>, puis les trois angles), et le
    /// flash.</summary>
    private static void EnterAreaExitFlash(byte* e, int kind, int anchorIndex, int zShift, int xShift = 0)
    {
        byte* anchor = Mem.Entity(0x005fa5e0) + anchorIndex * 0x9c;

        *(int*)(e + 0x5c) = kind;
        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c) + xShift;
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24) + zShift;
        *(short*)(e + 0x30) = *(short*)(anchor + 0x30);
        *(short*)(e + 0x32) = *(short*)(anchor + 0x32);
        *(short*)(e + 0x34) = *(short*)(anchor + 0x34);

        TickAreaExitFlash_004183D0((uint*)e);
    }

    /// <summary>
    /// Ghidra: FUN_004183d0 @ 0x004183d0 — <b>le flash blanc qui ferme une zone</b>. Decompilee
    /// dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Quatre etats, et le voile fait tout le travail.</b> Le corps ne dessine rien lui-meme :
    /// il pousse le voile plein cadre <see cref="GameMemory.s_Unknown_005de024"/> — trois octets de
    /// couleur, l'octet de tete a 1 pour l'allumer. L'etat 1 monte de <c>8</c> par image pendant
    /// trente images, l'etat 2 redescend de <c>6</c> pendant vingt-neuf, puis remet les trois
    /// octets a zero. Les additions <b>debordent sans etre bornees</b> : c'est le binaire, et
    /// c'est ce qui donne au flash sa coupure nette plutot qu'un fondu.
    /// </para>
    /// <para>
    /// <b>Le gel precede tout.</b> L'entree pose <see cref="FreezeAllEntities"/> ; l'etat 3 rend la
    /// main avec <see cref="UnfreezeAllEntities"/>, <b>remet les trois pointeurs de camera sur la
    /// vue de marche</b> — <c>0x005de078</c>, <c>0x00564650</c>, <c>0x005de068</c>, la meme triade
    /// que la camera de carte relit a chaque image — puis rappelle le mode de camera et eteint
    /// l'entite.
    /// </para>
    /// <para>
    /// <b>Quatre sons, et un compteur qui sert de memoire.</b> <c>+0x98</c> ne compte pas des
    /// images : il retient <b>lequel des sons a deja ete joue</b>, de sorte que chacun ne parte
    /// qu'une fois. Le dernier (<c>0x11</c>, hauteur 800) accompagne le changement de zone.
    /// </para>
    /// <para>
    /// <b>Une bizarrerie du binaire, conservee.</b> A l'etat 2, <c>FUN_0047ac40</c> est appele
    /// <b>deux fois</b> avec exactement les memes arguments — une fois dans chaque branche, une
    /// fois apres elles. Ghidra le rend ainsi et le desassemblage le confirme : c'est une entite
    /// de plus par image, pas une lecture de travers.
    /// </para>
    /// </summary>
    private static void TickAreaExitFlash_004183D0(uint* e)
    {
        byte* r = (byte*)e;

        if (*(short*)(r + 0x10) == 0)
        {
            *(short*)(r + 0x10) = 1;
            *(short*)(r + 0x12) = 0;
            *(short*)(r + 0x16) = 0;
            FreezeAllEntities();                              // Ghidra: FUN_00535300(0)
            *(short*)(r + 0x98) = 0;
            *e &= 0xfbffffff;
        }

        if (*(short*)(r + 0x10) == 1)
        {
            if (*(short*)(r + 0x98) == 1)
            {
                Aud.PlaySoundEffect(0x30, 0x2aa, 0x1000, 0x1000);
                *(short*)(r + 0x98) = 2;
            }

            if (*(short*)(r + 0x98) == 0)
            {
                Aud.PlaySoundEffect(0x30, 0x195, 0x1000, 0x1000);
                *(short*)(r + 0x98) = 1;
            }

            if (*(short*)(r + 0x12) < 0x0f)
            {
                *(short*)(r + 0x14) = 0;
                SpawnStatusEffectEntity_0047AC40(
                    r, (int*)(r + 0x1c), null, (byte)(*(uint*)(r + 0x5c) + 0xc0), 0, -1);
            }

            Mem.s_Unknown_005de024 = ShiftScreenVeil(Mem.s_Unknown_005de024, 8);

            (*(short*)(r + 0x12))++;

            if (*(short*)(r + 0x12) == 0x1e)
            {
                *(short*)(r + 0x10) = 2;
                *(short*)(r + 0x12) = 0;
            }
        }

        if (*(short*)(r + 0x10) == 2)
        {
            Mem.s_Unknown_005de024 = ShiftScreenVeil(Mem.s_Unknown_005de024, -6);

            (*(short*)(r + 0x12))++;

            if (*(short*)(r + 0x12) < 0x14)
            {
                *(short*)(r + 0x14) = 1;
            }
            else
            {
                if (*(short*)(r + 0x98) == 2)
                {
                    Aud.PlaySoundEffect(0x0f, 0x195, 0x1800, 0x1800);
                    *(short*)(r + 0x98) = 3;
                }

                *(short*)(r + 0x14) = 2;
            }

            // Les deux appels du binaire, portant les memes arguments. Voir le resume.
            SpawnStatusEffectEntity_0047AC40(
                r, (int*)(r + 0x1c), null, (byte)(*(uint*)(r + 0x5c) + 0xc0), 0, -1);
            SpawnStatusEffectEntity_0047AC40(
                r, (int*)(r + 0x1c), null, (byte)(*(uint*)(r + 0x5c) + 0xc0), 0, -1);

            if (*(short*)(r + 0x12) == 0x29)
            {
                *(short*)(r + 0x10) = 3;
                *(short*)(r + 0x12) = 0;
                // Ghidra: les trois canaux a zero, l'octet de commande a 1 — le voile reste
                // allume mais ne teint plus rien.
                Mem.s_Unknown_005de024 = 0x01000000;
            }
        }

        if (*(short*)(r + 0x10) != 3) return;

        (*(short*)(r + 0x12))++;
        if (*(short*)(r + 0x12) <= 0x1d) return;

        Aud.PlaySoundEffect(0x11, 800, 0x1800, 0x1800);
        UnfreezeAllEntities();                                // Ghidra: FUN_00535400

        *(byte**)Mem.Entity(0x005eca94) = (byte*)Mem.Camera(0x005de078);
        *(byte**)Mem.Arena(0x005e9ae0) = s_WorldMapCamera_00564650;
        *(byte**)Mem.Entity(0x005edebc) = (byte*)Mem.Camera(0x005de068);

        CameraModeApply_005037E0(0);                          // Ghidra: FUN_005037b0 -> FUN_005037e0
        *e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00407fe0</c> — <b>la quatrieme amorce, et une copie EXACTE de la troisieme</b>.
    /// Sous-type <b>33</b> du bloc <c>0x00554328</c>, sous-zones <b>5 a 8</b> de la mine, la ou
    /// <see cref="EntityHandler_004060C0"/> sert les sous-zones 0 a 2. Absente de <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Comparee octet a octet : identique sur les 49 premiers octets</b>, soit tout le corps
    /// jusqu'au <c>call</c> — dont seul le deplacement relatif differe, consequence mecanique de
    /// l'adresse. Meme genre <c>0x39</c>, meme ancre (l'enregistrement 6 de la table
    /// <c>0x005fa5e0</c>), meme cible.
    /// </para>
    /// <para>
    /// Le compilateur a donc emis la meme amorce <b>une fois par groupe de sous-zones</b> de la
    /// mine, exactement comme il l'avait fait deux fois pour la foret. <b>Quatre amorces, un seul
    /// corps.</b>
    /// </para>
    /// </summary>
    private static void EntityHandler_00407FE0(byte* e) => EntityHandler_004060C0(e);
}
