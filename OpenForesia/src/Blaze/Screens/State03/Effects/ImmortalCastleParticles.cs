// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES PARTICULES DU CHATEAU DE L'IMMORTEL — zone 6.
//
// Ouvert le 2026-09-18 avec le palier 1 de la zone, sur le patron de `MineParticles.cs` et de
// `LabyrinthParticles.cs` : ce qui nait, scintille et meurt au chateau, et ne decide de rien.
//
// LA ZONE SE LIT EN TROIS GROUPES DE SOUS-ZONES, ET LES TROIS SONT LE MEME BLOC RECOPIE :
//
//     sous-zones 0 a 4    bloc 0x00559328   planches a partir de 0x00550480
//     sous-zones 6 a 10   bloc 0x005599a8   planches a partir de 0x005505d0   (+0x150)
//     sous-zones 5 et 11  bloc 0x0055a028   planches a partir de 0x00550740   (+0x2c0)
//
// Les trois plages de 0xa0 octets sont IDENTIQUES OCTET POUR OCTET, et les trois se terminent sur
// le meme changement de famille (`48 00 10 10 01 02 10 00`). Le compilateur de 1998 a emis chaque
// corps trois fois en ne changeant que l'adresse de sa planche : c'est ce qui rend ce lot bon
// marche, et c'est aussi ce qui le rend dangereux — une plage relevee trop court se recopierait
// trois fois. Voir `sh OpenForesia.Tools/check-descriptors.sh`.
//
// Carte des comportements : Documentation/Ennemies/Comportement.md.
// Etat implemente / teste : Documentation/Etat/GestionnairesEntites.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES TROIS PLANCHES DE GROUPE
    //
    // Chacune couvre 0xa0 octets, soit VINGT enregistrements, et porte a elle seule les trois
    // particules du palier 1 :
    //
    //     +0x00   la spirale triple      (corps de 0x00406130, la mine)
    //     +0x78   la bulle qui monte     (corps de 0x004079E0)
    //     +0x80   le grain               (corps de 0x004070F0), quatre images
    //
    // LA BORNE BASSE EST UNE BORNE PROUVEE, pas une supposition : `s_Descriptors_00550460`
    // (LabyrinthParticles.cs) s'arrete exactement sur 0x00550480, et sa propre note dit deja que
    // « 0x00550480 change de forme (00 00 08 08) ». La borne haute l'est autant : a +0xa0 le
    // cinquieme octet, la page et la tuile changent ensemble.
    //
    // Les enregistrements 5 a 11 (tuiles 0x18 x 0x18) et 12 a 14 (couche 0x03) n'appartiennent a
    // aucun des trois corps ci-dessous — ce sont les planches de gestionnaires des paliers
    // suivants. Ils sont releves ici parce que la plage se releve d'un bloc, pas au descripteur.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550480</c> — <b>la planche des sous-zones 0 a 4 du chateau</b>.
    /// Vingt enregistrements, de <c>0x00550480</c> a <c>0x0055051f</c> inclus.
    /// </summary>
    private static readonly byte[] s_Descriptors_00550480 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x00550480  <- 0x00426410, la spirale
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x00550488
        0x60, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x00550490
        0x80, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x00550498
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x005504a0
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005504a8
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005504b0
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005504b8
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005504c0
        0x00, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x005504c8
        0x18, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x005504d0
        0x30, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x005504d8
        0x00, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x005504e0
        0x20, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x005504e8
        0x40, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x005504f0
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005504f8  <- 0x004270C0, la bulle
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550500  <- 0x00426FD0, le grain
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550508
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550510
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550518
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005505d0</c> — <b>la meme planche pour les sous-zones 6 a
    /// 10</b>, a <c>+0x150</c>. Memes octets, verifies un a un contre
    /// <see cref="s_Descriptors_00550480"/>, meme borne haute.</summary>
    private static readonly byte[] s_Descriptors_005505D0 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x005505d0  <- 0x004289F0, la spirale
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x005505d8
        0x60, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x005505e0
        0x80, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x005505e8
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x005505f0
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x005505f8
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550600
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550608
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550610
        0x00, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x00550618
        0x18, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x00550620
        0x30, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x00550628
        0x00, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550630
        0x20, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550638
        0x40, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x00550640
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550648  <- 0x00429930, la bulle
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550650  <- 0x00429840, le grain
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550658
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550660
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x00550668
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550740</c> — <b>la meme planche pour les sous-zones 5 et
    /// 11</b>, a <c>+0x2c0</c> de la premiere. Memes octets, meme borne haute. C'est elle aussi
    /// que la gerbe a cinq phases emploie, et dont elle <b>repeint l'octet 6 du second
    /// enregistrement</b> (<c>DAT_0055074e</c>) — voir <see cref="TickFivePhaseBurst"/>.</summary>
    private static readonly byte[] s_Descriptors_00550740 =
    [
        0x00, 0x00, 0x08, 0x08, 0x01, 0x02, 0x10, 0x00,   // 0x00550740  <- 0x0042BF70, la spirale
        0xc0, 0xc0, 0x3f, 0x3f, 0x01, 0x02, 0x09, 0x00,   // 0x00550748  <- repeint par la gerbe
        0x60, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x00550750
        0x80, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x00550758
        0xa0, 0x88, 0x20, 0x20, 0x01, 0x01, 0x10, 0x00,   // 0x00550760
        0x48, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550768
        0x60, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550770
        0x78, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550778
        0x90, 0x30, 0x18, 0x18, 0x00, 0x03, 0x10, 0x00,   // 0x00550780
        0x00, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x00550788
        0x18, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x00550790
        0x30, 0x30, 0x18, 0x18, 0x00, 0x01, 0x10, 0x00,   // 0x00550798
        0x00, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x005507a0
        0x20, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x005507a8
        0x40, 0x80, 0x20, 0x20, 0x01, 0x01, 0x03, 0x00,   // 0x005507b0
        0x00, 0x00, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005507b8  <- 0x0042E060, la bulle
        0x08, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005507c0  <- 0x0042DF70, le grain
        0x10, 0x08, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005507c8
        0x08, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005507d0
        0x10, 0x10, 0x08, 0x08, 0x01, 0x03, 0x10, 0x00,   // 0x005507d8
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00550890</c> — <b>la planche de role 2 des deux gerbes du chateau</b>,
    /// deux enregistrements, un par amorce. Meme forme que <see cref="s_Descriptors_0054F620"/>
    /// (mine) et que <c>0x00550438</c> (Labyrinthe) : des tuiles <c>0x0f x 0x0f</c> de la page
    /// <c>0x09</c>, prises a <c>y = 0xf0</c>, qui se suivent de <c>0x10</c> en X.
    ///
    /// <para>
    /// <b>Bornee des deux cotes par un changement de forme</b> : <c>0x00550888</c> porte une tuile
    /// <c>0x10 x 0x10</c> de la page <c>0x10</c> (c'est la planche de <c>0x0042DB10</c>) et
    /// <c>0x005508a0</c> une <c>0x0f x 0x0f</c> prise a <c>x = 0xc0</c>.
    /// </para>
    /// </summary>
    private static readonly byte[] s_Descriptors_00550890 =
    [
        0x70, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x00550890  <- 0x0042C9E0
        0x80, 0xf0, 0x0f, 0x0f, 0x01, 0x02, 0x09, 0x00,   // 0x00550898  <- 0x0042D130
    ];

    private static byte* s_Descriptors00550480Storage;
    private static byte* s_Descriptors005505D0Storage;
    private static byte* s_Descriptors00550740Storage;
    private static byte* s_Descriptors00550890Storage;

    private static byte* Descriptors00550480() => NativeCopy(s_Descriptors_00550480, ref s_Descriptors00550480Storage);
    private static byte* Descriptors005505D0() => NativeCopy(s_Descriptors_005505D0, ref s_Descriptors005505D0Storage);
    private static byte* Descriptors00550740() => NativeCopy(s_Descriptors_00550740, ref s_Descriptors00550740Storage);
    private static byte* Descriptors00550890() => NativeCopy(s_Descriptors_00550890, ref s_Descriptors00550890Storage);

    // =====================================================================
    // LA SPIRALE TRIPLE — creneau 61 des trois blocs
    //
    // Les trois amorces sont le patron le plus benin : `mov eax,[esp+4] ; push eax ; call <corps>`,
    // seize octets plus loin, exactement comme 0x00406120 dans la mine. Et les trois corps appeles
    // (0x00426420, 0x00428A00, 0x0042BF80) sont, LIGNE POUR LIGNE, celui de 0x00406130 : 316
    // instructions comparees une a une, ZERO ecart hors la planche, qui est posee trois fois.
    // C'est pourquoi le corps a ete sorti en TickTripleSpiral_00406130 (MineParticles.cs) plutot
    // que recopie ici.
    // =====================================================================

    /// <summary>Ghidra: <c>0x00426410</c> — l'amorce du creneau <b>61</b> du bloc
    /// <c>0x00559328</c> (table <c>0x0056b2c8</c>, sous-zones 0 a 4). Absente de <c>Blaze.c</c> ;
    /// elle appelle <c>FUN_00426420</c>, seize octets plus loin.</summary>
    private static void EntityHandler_00426410(byte* e)
        => TickTripleSpiral_00406130(e, Descriptors00550480());   // Ghidra: FUN_00426420, &DAT_00550480

    /// <summary>Ghidra: <c>0x004289f0</c> — la meme amorce pour les sous-zones 6 a 10, creneau
    /// <b>61</b> du bloc <c>0x005599a8</c>. Elle appelle <c>FUN_00428a00</c>.</summary>
    private static void EntityHandler_004289F0(byte* e)
        => TickTripleSpiral_00406130(e, Descriptors005505D0());   // Ghidra: FUN_00428a00, &DAT_005505d0

    /// <summary>Ghidra: <c>0x0042bf70</c> — la meme amorce pour les sous-zones 5 et 11, creneau
    /// <b>61</b> du bloc <c>0x0055a028</c>. Elle appelle <c>FUN_0042bf80</c>.</summary>
    private static void EntityHandler_0042BF70(byte* e)
        => TickTripleSpiral_00406130(e, Descriptors00550740());   // Ghidra: FUN_0042bf80, &DAT_00550740

    // =====================================================================
    // LA BULLE QUI MONTE — creneau 63, et LE GRAIN — creneau 62
    //
    // Six corps decompiles dans Blaze.c, et six fois la meme lecture : 69 instructions pour la
    // bulle, 75 pour le grain, UNE SEULE immediate differente a chaque fois — l'adresse de la
    // planche. Les parents sont ceux de la mine, 0x004079E0 et 0x004070F0.
    // =====================================================================

    /// <summary>Ghidra: FUN_004270c0 @ 0x004270c0 — la bulle des sous-zones 0 a 4, creneau
    /// <b>63</b> du bloc <c>0x00559328</c>. <see cref="EntityHandler_004079E0"/> a la planche
    /// pres : <c>0x005504f8</c>, le seizieme enregistrement de
    /// <see cref="s_Descriptors_00550480"/>.</summary>
    private static void EntityHandler_004270C0(byte* e)
        => TickRisingBubble(e, Descriptors00550480() + 0x78);   // Ghidra: &DAT_005504f8

    /// <summary>Ghidra: FUN_00429930 @ 0x00429930 — la meme bulle, sous-zones 6 a 10, planche
    /// <c>0x00550648</c>.</summary>
    private static void EntityHandler_00429930(byte* e)
        => TickRisingBubble(e, Descriptors005505D0() + 0x78);   // Ghidra: &DAT_00550648

    /// <summary>Ghidra: FUN_0042e060 @ 0x0042e060 — la meme bulle, sous-zones 5 et 11, planche
    /// <c>0x005507b8</c>.</summary>
    private static void EntityHandler_0042E060(byte* e)
        => TickRisingBubble(e, Descriptors00550740() + 0x78);   // Ghidra: &DAT_005507b8

    /// <summary>Ghidra: FUN_00426fd0 @ 0x00426fd0 — le grain des sous-zones 0 a 4, creneau
    /// <b>62</b> du bloc <c>0x00559328</c>. <see cref="EntityHandler_004070F0"/> a la planche
    /// pres : <c>0x00550500</c>, et les QUATRE images que <c>+0x40</c> parcourt tiennent dans la
    /// plage (enregistrements 16 a 19).</summary>
    private static void EntityHandler_00426FD0(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors00550480() + 0x80);   // Ghidra: &DAT_00550500

    /// <summary>Ghidra: FUN_00429840 @ 0x00429840 — le meme grain, sous-zones 6 a 10, planche
    /// <c>0x00550650</c>.</summary>
    private static void EntityHandler_00429840(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors005505D0() + 0x80);   // Ghidra: &DAT_00550650

    /// <summary>Ghidra: FUN_0042df70 @ 0x0042df70 — le meme grain, sous-zones 5 et 11, planche
    /// <c>0x005507c0</c>.</summary>
    private static void EntityHandler_0042DF70(byte* e)
        => TickMineSpeck_004070F0(e, Descriptors00550740() + 0x80);   // Ghidra: &DAT_005507c0

    // =====================================================================
    // LA GERBE A CINQ PHASES — creneaux 10 et 11 du bloc 0x0055a028
    //
    // Le meme couple qu'au Labyrinthe (0x00423d40 / 0x00424490) et a la mine (0x0040b1b0 /
    // 0x0040b900) : deux amorces qui posent leur numero de reproduction en +0x98, leur duree en
    // +0x96 et leur propre tuile de role 2, puis appellent un corps commun. Le corps, FUN_0042ca20,
    // est TickFivePhaseBurst a la planche pres — 514 instructions, zero autre ecart contre
    // FUN_0040b1f0.
    // =====================================================================

    /// <summary>Ghidra: <c>0x0042c9e0</c> — <b>la premiere amorce de la gerbe</b>, creneau
    /// <b>10</b> du bloc <c>0x0055a028</c> (sous-zones 5 et 11). Absente de <c>Blaze.c</c> :
    /// <c>+0x98 = 0x0a</c>, <c>+0x96 = 0x0b</c>, planche de role 2 <c>0x00550890</c>.</summary>
    private static void EntityHandler_0042C9E0(byte* e)
    {
        *(short*)(e + 0x98) = 0x0a;
        *(short*)(e + 0x96) = 0x0b;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550890();       // Ghidra: &DAT_00550890
        }

        TickFivePhaseBurst(e, Descriptors00550740());          // Ghidra: FUN_0042ca20, &DAT_00550740
    }

    /// <summary>Ghidra: <c>0x0042d130</c> — <b>la seconde amorce</b>, creneau <b>11</b> du meme
    /// bloc. Les deux nombres sont echanges — <c>+0x98 = 0x0b</c>, <c>+0x96 = 0x0a</c> — et la
    /// planche de role 2 est le second enregistrement, <c>0x00550898</c>.</summary>
    private static void EntityHandler_0042D130(byte* e)
    {
        *(short*)(e + 0x98) = 0x0b;
        *(short*)(e + 0x96) = 0x0a;

        if (*(short*)(e + 0x14) == 2 && *(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550890() + 8;   // Ghidra: &DAT_00550898
        }

        TickFivePhaseBurst(e, Descriptors00550740());          // Ghidra: FUN_0042ca20, &DAT_00550740
    }

    // =====================================================================
    // LES CINQ CORPS « PRESQUE ECRITS » DU PALIER 1
    //
    // Chacun est un corps deja transcrit ailleurs, a quelques lignes pres, et ces quelques lignes
    // sont le travail. Deux d'entre eux ferment en prime un gestionnaire de la GROTTE SCELLEE
    // (zone 10), qui est le meme corps a la planche pres : 0x00442C20 et 0x00439170. Ils sont ecrits
    // ici, avec leur parent, plutot que dans un fichier de zone 10 qui n'existe pas — la meme regle
    // qui a mis les copies de la grotte dans `LabyrinthParticles.cs`.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00550570</c> — <b>la planche de la braise du chateau</b>,
    /// sous-zones 0 a 4. <b>Cinq</b> enregistrements <c>0x18 x 0x18</c>, exactement ce que
    /// <c>+0x40</c> parcourt (0 a 4). Bornee par <c>0x00550598</c>, ou la tuile passe en
    /// <c>0x0f x 0x0f</c>.</summary>
    private static readonly byte[] s_Descriptors_00550570 =
    [
        0x60, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550570  <- 0x00426940
        0x78, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550578
        0x90, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550580
        0xa8, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550588
        0xc0, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x00550590
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005506c0</c> — la meme planche pour les sous-zones 6 a 10.
    /// Memes octets ; bornee par <c>0x005506e8</c>, ou la tuile passe en
    /// <c>0x17 x 0x17</c>.</summary>
    private static readonly byte[] s_Descriptors_005506C0 =
    [
        0x60, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005506c0  <- 0x00428E40
        0x78, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005506c8
        0x90, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005506d0
        0xa8, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005506d8
        0xc0, 0xd0, 0x18, 0x18, 0x01, 0x02, 0x10, 0x00,   // 0x005506e0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005508b8</c> — <b>la planche du semis de nuage du chateau</b>,
    /// sous-zones 5 et 11. Quatre enregistrements, comme <see cref="s_Descriptors_00550460"/> dont
    /// elle porte les memes octets ; bornee par <c>0x005508d8</c>, ou les trois memes tuiles
    /// recommencent pour une autre table. Les quatre servent : le mode 0 fait tourner <c>+0x40</c>
    /// de 0 a 2, le mode 1 le pose a 3.</summary>
    private static readonly byte[] s_Descriptors_005508B8 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005508b8  <- 0x0042B430
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005508c0
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005508c8
        0x60, 0x48, 0x1f, 0x1f, 0x01, 0x02, 0x10, 0x00,   // 0x005508d0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550888</c> — <b>la planche de la braise qui retombe</b>,
    /// sous-zones 5 et 11. <b>Un seul</b> enregistrement : le corps pose <c>+0x40 = 0</c> et ne
    /// l'avance jamais. Bornee des deux cotes par un changement de forme — <c>0x00550880</c> est
    /// une tuile <c>0x1f x 0x1f</c> de la couche <c>0x0a</c>, <c>0x00550890</c> une
    /// <c>0x0f x 0x0f</c> de la page <c>0x09</c>, celle de la gerbe.</summary>
    private static readonly byte[] s_Descriptors_00550888 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00550888  <- 0x0042DB10
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551408</c> — la meme planche pour la <b>grotte scellee</b>,
    /// memes octets, meme unique enregistrement. Bornee par <c>0x00551400</c>
    /// (<c>0xa0 0x88 0x20 0x20</c>) et <c>0x00551410</c> (<c>0x48 0x00 0x10 0x10</c>).</summary>
    private static readonly byte[] s_Descriptors_00551408 =
    [
        0x08, 0x08, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551408  <- 0x00442C20
    ];

    private static byte* s_Descriptors00550570Storage;
    private static byte* s_Descriptors005506C0Storage;
    private static byte* s_Descriptors005508B8Storage;
    private static byte* s_Descriptors00550888Storage;
    private static byte* s_Descriptors00551408Storage;

    private static byte* Descriptors00550570() => NativeCopy(s_Descriptors_00550570, ref s_Descriptors00550570Storage);
    private static byte* Descriptors005506C0() => NativeCopy(s_Descriptors_005506C0, ref s_Descriptors005506C0Storage);
    private static byte* Descriptors005508B8() => NativeCopy(s_Descriptors_005508B8, ref s_Descriptors005508B8Storage);
    private static byte* Descriptors00550888() => NativeCopy(s_Descriptors_00550888, ref s_Descriptors00550888Storage);
    private static byte* Descriptors00551408() => NativeCopy(s_Descriptors_00551408, ref s_Descriptors00551408Storage);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00426940"/> et de son jumeau. Decompile dans
    /// <c>Blaze.c</c>, 149 instructions.
    ///
    /// <para>
    /// <b>La braise du Labyrinthe, autrement reglee.</b> L'ossature est celle de
    /// <see cref="TickLabyrinthEmber"/> — meme suite d'etats, meme tirage perdu, meme extinction par
    /// la teinte, meme <c>+0x40</c> qui tourne de 0 a 4 sur cinq enregistrements — mais
    /// <b>sept constantes changent</b>, et deux d'entre elles ne sont pas des reglages :
    /// </para>
    /// <list type="table">
    ///   <item><term>le cap</term><description>devie de <c>rand % 0x200 + 0x700</c> et non de
    ///     <c>rand % 0x200 - 0x100</c> : la braise du chateau part <b>a l'oppose</b> de sa
    ///     pose, la ou celle du Labyrinthe part autour.</description></item>
    ///   <item><term>la descente de naissance</term><description><b>absente</b> : le Labyrinthe
    ///     descend de <c>0x32</c> cases (<c>+0x20 += -0x32000</c>), le chateau ne descend pas et
    ///     pose a la place <c>+0x2e = -0x1e</c>, une couche de dessin.</description></item>
    ///   <item><term>l'echelle</term><description><c>rand % 0x5dc + 0xfa0</c> — deux fois plus
    ///     grosse que les <c>rand % 0x400 + 0x7d0</c> du Labyrinthe.</description></item>
    ///   <item><term>la vitesse</term><description><c>rand % 10 + 10</c> au lieu de
    ///     <c>+ 0x14</c>, et elle ne perd qu'<b>une</b> unite par image au lieu de
    ///     deux.</description></item>
    ///   <item><term>le genre</term><description><c>+0x0a = 4</c> au lieu de
    ///     <c>6</c>.</description></item>
    /// </list>
    /// <para>
    /// <b>Le tirage perdu est la aussi</b> : deux <c>call 00547140h</c> se suivent et le premier
    /// resultat n'est jamais lu. Reproduit, parce qu'il decale le flux des tirages suivants.
    /// </para>
    /// </summary>
    private static void TickCastleEmber(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;

            short scale = (short)(CrtRand() % 0x5dc + 0xfa0);
            e[0x0a] = 4;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            CrtRand();                        // Ghidra: tirage PERDU, `call` sans lecture du retour
            *(short*)(e + 0x32) += (short)(CrtRand() % 0x200 + 0x700);

            *(int*)(e + 0x5c) = CrtRand() % 0xa + 0xa;
            *(int*)(e + 0x54) = (CrtRand() % 0xa + 0xa) << 0xc;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4);
            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = (short)(CrtRand() % 5 + 2);
            *(uint*)e |= 0x4000;

            *(int*)(e + 0x1c) += (CrtRand() % 0x50 - 0x28) << 0xc;
            *(short*)(e + 0x2e) = -0x1e;
            *(int*)(e + 0x24) += (CrtRand() % 0x50 - 0x28) << 0xc;

            e[0x38] = 0x64;
            e[0x39] = 0x64;
            e[0x3a] = 0x64;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))--;

            if (*(short*)(e + 0x12) <= 0)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0xffffbfff;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        int speed = *(int*)(e + 0x5c);

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * speed;
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * speed;

        *(short*)(e + 0x28) += 0x96;
        *(short*)(e + 0x2a) += 0x96;

        int slowed = speed - 1;
        if (slowed < 0) slowed = 0;
        *(int*)(e + 0x5c) = slowed;

        *(int*)(e + 0x20) += *(int*)(e + 0x54);

        BrightenEntityTint_00404DE0(e, -8, -8, -8);                // Ghidra: FUN_00404de0

        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;

        (*(short*)(e + 0x16))++;
        if (*(short*)(e + 0x16) > *(short*)(e + 0x14))
        {
            *(short*)(e + 0x16) = 0;

            byte frame = (byte)(e[0x40] + 1);
            e[0x40] = frame;

            if (frame == 5) e[0x40] = 0;
        }
    }

    /// <summary>Ghidra: FUN_00426940 @ 0x00426940 — creneau <b>59</b> du bloc <c>0x00559328</c>
    /// (sous-zones 0 a 4). Voir <see cref="TickCastleEmber"/>.</summary>
    private static void EntityHandler_00426940(byte* e)
        => TickCastleEmber(e, Descriptors00550570());   // Ghidra: &DAT_00550570

    /// <summary>Ghidra: FUN_00428e40 @ 0x00428e40 — creneau <b>59</b> du bloc <c>0x005599a8</c>
    /// (sous-zones 6 a 10). <b>La copie conforme</b> du precedent : 149 instructions comparees une
    /// a une, une seule immediate d'ecart, la planche.</summary>
    private static void EntityHandler_00428E40(byte* e)
        => TickCastleEmber(e, Descriptors005506C0());   // Ghidra: &DAT_005506c0

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0042DB10"/> et de sa copie de la grotte scellee.
    /// Decompile dans <c>Blaze.c</c>, 116 instructions.
    ///
    /// <para>
    /// <b>La braise qui retombe de <see cref="EntityHandler_0040C460"/></b> (mine), a treize lignes
    /// pres. Meme ossature : deux variantes choisies par <c>+0x14</c>, une vitesse verticale qui
    /// tombe d'un par image et passe sous zero, un noircissement de 10 par canal qui tue l'entite
    /// quand le rouge atteint zero.
    /// </para>
    /// <list type="bullet">
    ///   <item>la variante <b>0</b> monte de <c>rand % 10 + 8</c> cases (la mine :
    ///     <c>rand % 15 + 15</c>) ;</item>
    ///   <item>la variante <b>non nulle</b> <b>AFFECTE</b> sa hauteur au lieu de l'ajouter :
    ///     <c>+0x20 = (+0x14 &amp; ~0x1f) * 0x80</c>, quand la mine ecrit <c>+=</c>. Elle nait donc
    ///     a une altitude absolue, pas au-dessus de sa pose ;</item>
    ///   <item>le genre vaut <c>9</c> et non <c>0x0c</c>.</item>
    /// </list>
    /// <para>
    /// <b>Et le seuil de noircissement est le meme des deux cotes</b> — <c>-10</c> —, ce qui rend
    /// MORT le second etage de tests que le binaire emet quand meme : la mine y separait un seuil
    /// de <c>-5</c> pour la variante 0 d'un seuil de <c>-10</c> pour l'autre. Trois sauts qui ne
    /// peuvent plus rien changer, gardes tels quels dans le binaire et resumes ici.
    /// </para>
    /// </summary>
    private static void TickCastleFallingEmber(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;

            short scale = (short)(CrtRand() % 0x800 + 0x800);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            if (*(short*)(e + 0x14) == 0)
            {
                *(int*)(e + 0x50) = (CrtRand() % 10 - 5) * 0x1000;
                *(int*)(e + 0x58) = (CrtRand() % 10 - 5) * 0x1000;
                *(int*)(e + 0x54) = CrtRand() % 10 + 8;
                *(int*)(e + 0x20) += 0x96000;
            }
            else
            {
                *(int*)(e + 0x50) = (CrtRand() % 0x1e - 0x0f) * 0x1000;
                *(int*)(e + 0x58) = (CrtRand() % 0x1e - 0x0f) * 0x1000;
                *(int*)(e + 0x54) = CrtRand() % 5 + 8;
                // Ghidra: `mov [esi+20h],edx` — une AFFECTATION, la ou la mine fait `add`.
                *(int*)(e + 0x20) = (*(short*)(e + 0x14) & ~0x1f) * 0x80;
            }
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50);
        *(int*)(e + 0x24) += *(int*)(e + 0x58);
        *(int*)(e + 0x20) += *(int*)(e + 0x54) * 0x1000;

        int rise = *(int*)(e + 0x54) - 1;
        *(int*)(e + 0x54) = rise;

        // Ghidra: le binaire reteste ici +0x14, puis rise contre -10, puis +0x14 encore. Aucun de
        // ces trois sauts ne peut plus rien changer : on sait deja que rise < -10, et les deux
        // branches de +0x14 menent au meme fondu. Voir le resume.
        if (rise >= -10) return;

        short red = (short)(e[0x38] - 10);
        sbyte green = (sbyte)(e[0x39] - 10);
        sbyte blue = (sbyte)(e[0x3a] - 10);

        if (red < 0)
        {
            red = 0;
            green = 0;
            blue = 0;
            *(uint*)e &= 0x7fffffff;
        }

        e[0x38] = (byte)red;
        e[0x39] = (byte)green;
        e[0x3a] = (byte)blue;
    }

    /// <summary>Ghidra: FUN_0042db10 @ 0x0042db10 — creneau <b>6</b> du bloc <c>0x0055a028</c>
    /// (sous-zones 5 et 11). Voir <see cref="TickCastleFallingEmber"/>.</summary>
    private static void EntityHandler_0042DB10(byte* e)
        => TickCastleFallingEmber(e, Descriptors00550888());   // Ghidra: &DAT_00550888

    /// <summary>Ghidra: FUN_00442c20 @ 0x00442c20 — creneau <b>59</b> du bloc <c>0x0055e7a8</c>,
    /// sous-zones 13, 14 et 15 de la <b>grotte scellee</b>. Le meme corps a la planche pres : 116
    /// instructions comparees une a une, un seul <c>diff</c> de deux lignes.</summary>
    private static void EntityHandler_00442C20(byte* e)
        => TickCastleFallingEmber(e, Descriptors00551408());   // Ghidra: &DAT_00551408

    /// <summary>
    /// Ghidra: FUN_0042b430 @ 0x0042b430 — creneau <b>31</b> du bloc <c>0x0055a028</c>
    /// (sous-zones 5 et 11). Decompilee dans <c>Blaze.c</c>, 441 instructions.
    ///
    /// <para>
    /// <b><see cref="TickCloudSeed"/> a la planche pres — et a DEUX pointeurs pres.</b> Le corps
    /// est celui de <see cref="EntityHandler_00421DC0"/> (Labyrinthe, sous-zones 4 et 9) : meme
    /// texte, meme longueur, meme repli sur <c>Res.s_EntityList_005de558</c> pour la cible a
    /// l'entree et pour le porteur au moment du contact. Mais les deux zones ne protegent pas les
    /// memes lectures ensuite : le chateau <b>relit <c>+0x78</c> sans repli</b> apres le contact, et
    /// c'est lui, en revanche, qui <b>protege le porteur du mode 1</b>, que le Labyrinthe lit nu.
    /// </para>
    /// <para>
    /// C'est la lecon du lot 8 du Labyrinthe, une zone plus loin : <b>ce qu'un corps fait d'un
    /// pointeur nul n'est pas une regle de bloc</b>. Il se lit site par site.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042B430(byte* e)
        => TickCloudSeed(e, Descriptors005508B8(), AbsentHost.FallBackOnEntityList,
                         guardRereadAfterHit: false, guardReturnCarrier: true);

    /// <summary>
    /// Ghidra: FUN_00429be0 @ 0x00429be0 — creneau <b>32</b> du bloc <c>0x00559f28</c>
    /// (table <c>0x0056b1c8</c>, sous-zones 5 et 11). Decompilee dans <c>Blaze.c</c>, 389
    /// instructions.
    ///
    /// <para>
    /// <b><see cref="TickOwnerFrontSweep"/>, et le seul ecart qui compte tient en un octet</b> : le
    /// sous-type seme vaut <c>0xe1</c> et non <c>0xee</c>. Les vingt-cinq autres lignes du
    /// <c>diff</c> contre <see cref="EntityHandler_004224F0"/> sont de l'ordonnancement de
    /// registres — le compilateur a distribue les memes lectures autrement — et la table de sauts
    /// est <c>0x0042a0e0</c> au lieu de <c>0x004229f0</c> : <b>neuf entrees de part et d'autre, au
    /// meme pas de 11 octets et au meme decalage de <c>0x2b2</c> depuis la tete du corps</b>, donc
    /// la meme table.
    /// </para>
    /// </summary>
    private static void EntityHandler_00429BE0(byte* e) => TickOwnerFrontSweep(e, 0xe1);

    /// <summary>
    /// Ghidra: FUN_0042a540 @ 0x0042a540 — creneau <b>30</b> du bloc <c>0x00559f28</c>
    /// (sous-zones 5 et 11). Decompilee dans <c>Blaze.c</c>, 263 instructions.
    ///
    /// <para>
    /// <b><see cref="TickShockwaveOrSpray"/> dans sa forme gardee</b>, celle de
    /// <see cref="EntityHandler_00422E50"/> : le porteur <c>+0x6c</c> et le lanceur
    /// <c>+0x6c</c> du porteur retombent tous deux sur <c>Res.s_EntityList_005de558</c> quand ils
    /// sont nuls. Compare a cette jumelle-la et non au modele nu, le <c>diff</c> tombe a
    /// <b>une instruction</b>.
    /// </para>
    /// <para>
    /// Cette instruction-la : le Labyrinthe ecrit <c>+0x36</c> par
    /// <c>and ecx,0xfffffffe ; or ecx,2</c> — il <b>efface le bit 0</b> avant de poser le bit 1 —,
    /// le chateau par un <c>or byte [esi+36h],2</c> nu, qui laisse le bit 0 tel qu'il etait.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042A540(byte* e)
        => TickShockwaveOrSpray(e, AbsentHost.FallBackOnEntityList, clearFlagBit0: false);

    // =====================================================================
    // PALIER 2, LOT 1 (2026-09-18) — LES EFFETS DU CHATEAU
    //
    // Les huit formes des tables 0x0056b0c8 / 0x0056b1c8 / 0x0056b2c8 que le palier 2 laissait, et
    // qui ne decident de rien. Les quatre qui lancent un script de zone sont ailleurs
    // (Handlers/ImmortalCastleMecanismes.cs) : elles prennent la partie en charge.
    //
    // Aucune n'avait de parent deja ecrit — le releve LOOSE contre les 957 corps du depot est
    // revenu vide, sauf pour 0x0042A350. Ce sont donc de vraies lectures, mais courtes.
    // =====================================================================

    /// <summary>Ghidra: <c>&amp;DAT_00550728</c> — <b>la planche de l'etincelle qui tourne autour de
    /// son porteur</b>, sous-zones 6 a 10. <b>Trois</b> enregistrements, exactement ce que
    /// <c>+0x40</c> parcourt. Bornee par <c>0x00550740</c>, la planche de groupe du palier 1 :
    /// une borne deja prouvee.</summary>
    private static readonly byte[] s_Descriptors_00550728 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550728  <- 0x00427C10
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550730
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00550738
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005508d8</c> — la meme planche pour les sous-zones 5 et 11, et
    /// **deux** corps s'en servent : <see cref="EntityHandler_0042B0C0"/> et
    /// <see cref="EntityHandler_0042A350"/>. Trois enregistrements ; bornee par <c>0x005508f0</c>,
    /// ou la tuile passe en <c>0x20 x 0x20</c>. Elle commence exactement ou
    /// <see cref="s_Descriptors_005508B8"/> s'arrete — les deux plages se touchent.</summary>
    private static readonly byte[] s_Descriptors_005508D8 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005508d8  <- 0x0042B0C0, 0x0042A350
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005508e0
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x005508e8
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551118</c> — la meme planche pour la <b>grotte scellee</b>,
    /// sous-zones 6 et 7. Trois enregistrements ; bornee par <c>0x00551130</c>
    /// (<c>0x18 0x00 0x18 0x18</c>). Elle commence exactement ou
    /// <see cref="s_Descriptors_005510F8"/> s'arrete.</summary>
    private static readonly byte[] s_Descriptors_00551118 =
    [
        0x00, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551118  <- 0x00439B00
        0x10, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551120
        0x20, 0xd0, 0x0f, 0x0f, 0x01, 0x02, 0x10, 0x00,   // 0x00551128
    ];

    /// <summary>Ghidra: <c>&amp;DAT_005506e8</c> — <b>la planche du bloc qui tombe</b>, une seule
    /// tuile <c>0x17 x 0x17</c>. Le corps pose <c>+0x40 = 0</c> et ne l'avance jamais. Elle
    /// commence exactement ou <see cref="s_Descriptors_005506C0"/> s'arrete, et se ferme sur
    /// <c>0x005506f0</c> ou la tuile passe en <c>0x0f x 0x0f</c>.</summary>
    private static readonly byte[] s_Descriptors_005506E8 =
    [
        0x30, 0x00, 0x17, 0x17, 0x01, 0x02, 0x10, 0x00,   // 0x005506e8  <- 0x00429270
    ];

    private static byte* s_Descriptors00550728Storage;
    private static byte* s_Descriptors005508D8Storage;
    private static byte* s_Descriptors00551118Storage;
    private static byte* s_Descriptors005506E8Storage;

    private static byte* Descriptors00550728() => NativeCopy(s_Descriptors_00550728, ref s_Descriptors00550728Storage);
    private static byte* Descriptors005508D8() => NativeCopy(s_Descriptors_005508D8, ref s_Descriptors005508D8Storage);
    private static byte* Descriptors00551118() => NativeCopy(s_Descriptors_00551118, ref s_Descriptors00551118Storage);
    private static byte* Descriptors005506E8() => NativeCopy(s_Descriptors_005506E8, ref s_Descriptors005506E8Storage);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00426C40"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 60 instructions.
    ///
    /// <para>
    /// <b>Un grain qui traverse la cour, et sa position n'est PAS relative.</b> A la naissance il
    /// s'ecrit directement dans <c>+0x1c</c>, <c>+0x20</c> et <c>+0x24</c> — pas de <c>+=</c> — :
    /// <c>±1000</c> unites en X, <c>-1800</c> a <c>+2200</c> en Z, <c>0</c> a <c>599</c> de haut.
    /// C'est un point du monde, pas un decalage autour d'un porteur.
    /// </para>
    /// <para>
    /// Ensuite il avance en <b>Z seulement</b>, de <c>0x50</c> a <c>0x63</c> unites par image, et
    /// meurt de deux facons : son decompte <c>+0x12</c> (40 a 99 images) passe sous zero, <b>ou</b>
    /// il sort par le fond, au-dela de <c>0x898000</c>. Le test de sortie masque les douze bits bas
    /// avant de comparer — le binaire compare des cases entieres, pas des seiziemes.
    /// </para>
    /// </summary>
    private static void TickCastleCrossingMote_00426C40(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x1000;
            *(short*)(e + 0x2a) = 0x1000;

            *(int*)(e + 0x1c) = (CrtRand() % 2000 - 1000) * 0x1000;
            *(int*)(e + 0x24) = (CrtRand() % 4000 - 0x708) * 0x1000;
            *(int*)(e + 0x20) = (CrtRand() % 600) << 0xc;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x3c + 0x28);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x14 + 0x50);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;

        uint z = (uint)(*(int*)(e + 0x24) + *(short*)(e + 0x14) * 0x1000);
        *(int*)(e + 0x24) = (int)z;

        if (*(short*)(e + 0x12) < 0 || (int)(z & 0xfffff000) > 0x898000) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_00426c40 @ 0x00426c40 — creneau <b>7</b> du bloc <c>0x00559328</c>
    /// (sous-zones 0 a 4). Sa planche est <c>0x005504f8</c>, c'est-a-dire l'enregistrement de la
    /// bulle dans <see cref="s_Descriptors_00550480"/> : le grain et la bulle partagent leur
    /// tuile.</summary>
    private static void EntityHandler_00426C40(byte* e)
        => TickCastleCrossingMote_00426C40(e, Descriptors00550480() + 0x78);   // Ghidra: &DAT_005504f8

    /// <summary>Ghidra: FUN_004293f0 @ 0x004293f0 — creneau <b>7</b> du bloc <c>0x005599a8</c>
    /// (sous-zones 6 a 10). La copie conforme, planche <c>0x00550648</c>.</summary>
    private static void EntityHandler_004293F0(byte* e)
        => TickCastleCrossingMote_00426C40(e, Descriptors005505D0() + 0x78);   // Ghidra: &DAT_00550648

    /// <summary>
    /// Ghidra: FUN_00426d20 @ 0x00426d20 — creneau <b>3</b> du bloc <c>0x00559228</c>
    /// (table <c>0x0056b1c8</c>, sous-zones 0 a 4). Decompilee dans <c>Blaze.c</c>, 69 instructions.
    ///
    /// <para>
    /// <b>Le meme trajet que <see cref="TickCastleCrossingMote_00426C40"/>, mais en volume.</b> La
    /// naissance et la traversee sont identiques au mot pres ; ce qui change est ce qui est
    /// dessine : <b>aucune planche n'est posee</b>, <c>+0x04</c> (le modele) est mis a zero, et
    /// l'entite recoit trois echelles — <c>+0x28</c> et <c>+0x2c</c> a 300, <c>+0x2a</c> tiree
    /// entre 6000 et 8999 — plus deux angles a <c>0x400</c>. C'est un objet long et fin, etire sur
    /// un seul axe, qui traverse la cour.
    /// </para>
    /// <para>
    /// Son decompte de vitesse est <c>rand % 0x1e + 0x50</c> au lieu de <c>rand % 0x14 + 0x50</c> :
    /// il va un peu plus vite, ou un peu moins.
    /// </para>
    /// </summary>
    private static void EntityHandler_00426D20(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            e[0x04] = 0;
            e[0x0a] = 3;
            e[0x0b] = 1;
            *(short*)(e + 0x28) = 300;
            *(short*)(e + 0x2c) = 300;

            int stretch = CrtRand();
            *(short*)(e + 0x32) = 0x400;
            *(short*)(e + 0x34) = 0x400;
            *(short*)(e + 0x2a) = (short)(stretch % 3000 + 6000);

            *(int*)(e + 0x1c) = (CrtRand() % 2000 - 1000) * 0x1000;
            *(int*)(e + 0x24) = (CrtRand() % 4000 - 0x708) * 0x1000;
            *(int*)(e + 0x20) = (CrtRand() % 600) << 0xc;

            *(short*)(e + 0x12) = (short)(CrtRand() % 0x3c + 0x28);
            *(short*)(e + 0x14) = (short)(CrtRand() % 0x1e + 0x50);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;

        uint z = (uint)(*(int*)(e + 0x24) + *(short*)(e + 0x14) * 0x1000);
        *(int*)(e + 0x24) = (int)z;

        if (*(short*)(e + 0x12) < 0 || (int)(z & 0xfffff000) > 0x898000) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00426E20"/> et de son miroir. Absent de <c>Blaze.c</c> ;
    /// 27 et 28 instructions, relevees au desassembleur.
    ///
    /// <para>
    /// <b>Un decor qui n'existe que si le scenario le veut.</b> Il se pose une fois — angles a
    /// zero, <c>+0x34</c> a <c>0x400</c>, echelles <c>0x12c</c> / <c>0x157c</c> / <c>0x12c</c>,
    /// modele <c>+0x0b = 0xf3</c> — puis, <b>a chaque image</b>, il recopie le bit <c>0x40</c> de
    /// <c>DAT_005e9702</c> dans le bit <c>0x4000</c> de son mot d'etat. Il ne fait rien d'autre :
    /// il apparait et disparait avec un drapeau de la banque de script, et n'y touche jamais.
    /// </para>
    /// <para>
    /// C'est la seule chose qui le separe d'un decor de <c>0x0056adc8</c> : il vit dans une table
    /// d'effets. La regle de rangement suit le <b>role</b>, et ce corps ne decide de rien.
    /// </para>
    /// </summary>
    /// <param name="kind">Le genre pose en <c>+0x0a</c> : <c>0x0e</c> pour la copie des sous-zones
    /// 0 a 4, <c>0</c> pour celle des sous-zones 6 a 10.</param>
    /// <param name="clearContactBit">Vrai pour la seconde copie seulement, qui efface en plus le
    /// bit 0 de <c>+0x36</c> a la naissance — l'unique instruction qui les separe.</param>
    private static void TickCastleFlaggedProp_00426E20(byte* e, byte kind, bool clearContactBit)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            if (clearContactBit) e[0x36] &= 0xfe;

            *(short*)(e + 0x12) = 0;
            e[0x04] = 0;
            e[0x0a] = kind;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x10) = 1;
            e[0x0b] = 0xf3;
            *(short*)(e + 0x34) = 0x400;
            *(short*)(e + 0x28) = 0x12c;
            *(short*)(e + 0x2c) = 0x12c;
            *(short*)(e + 0x2a) = 0x157c;
        }

        if (*(short*)(e + 0x10) != 1) return;

        // Ghidra: `test byte [5E9702h],40h` — DAT_005e96e0 + 0x22, la banque de script 0.
        if ((Mem.s_StateBank0_005e96e0[0x22] & 0x40) != 0) *(uint*)e |= 0x4000;
        else *(uint*)e &= 0xffffbfff;
    }

    /// <summary>Ghidra: <c>0x00426e20</c> — creneau <b>2</b> du bloc <c>0x00559228</c>
    /// (sous-zones 0 a 4). Genre <c>0x0e</c>, et il ne touche pas a <c>+0x36</c>.</summary>
    private static void EntityHandler_00426E20(byte* e)
        => TickCastleFlaggedProp_00426E20(e, 0x0e, clearContactBit: false);

    /// <summary>Ghidra: <c>0x00429680</c> — creneau <b>2</b> du bloc <c>0x005598a8</c>
    /// (sous-zones 6 a 10). Genre <c>0</c>, et il efface le bit 0 de <c>+0x36</c>.</summary>
    private static void EntityHandler_00429680(byte* e)
        => TickCastleFlaggedProp_00426E20(e, 0, clearContactBit: true);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00427C10"/> et de ses deux copies. Decompile dans
    /// <c>Blaze.c</c>, 62 instructions.
    ///
    /// <para>
    /// <b>Une etincelle qui tourne autour de son porteur, et sa position est refaite a chaque
    /// image</b> — avant meme le test d'etat, donc y compris a l'image de sa mort. Elle se tient
    /// <c>0x73000</c> au-dessus du porteur <c>+0x74</c>, et a <b>quatorze unites</b> de lui dans le
    /// cap <c>+0x5c + +0x32 - 0xc00</c>, un cap dont une part vient d'elle et l'autre du porteur.
    /// </para>
    /// <para>
    /// <b>Elle vit onze images</b>, son image de planche est tiree au hasard (<c>rand % 3</c>) puis
    /// tourne d'un cran par image en boucle sur trois — d'ou les trois enregistrements. L'echelle
    /// est <c>4000</c> sur les deux axes.
    /// </para>
    /// <para>
    /// <b>Le porteur n'est jamais teste.</b> Un <c>+0x74</c> nul serait une faute de page dans le
    /// binaire et une violation d'acces ici, a la meme lecture — comme chez ses cousines des
    /// sous-zones 5 a 8 du Labyrinthe.
    /// </para>
    /// </summary>
    private static void TickCastleHostOrbitSpark_00427C10(byte* e, byte* plate)
    {
        byte* host = *(byte**)(e + 0x74);

        *(int*)(e + 0x20) = *(int*)(host + 0x20) + 0x73000;

        int heading = *(int*)(e + 0x5c) - 0xc00 + *(short*)(e + 0x32);
        *(int*)(e + 0x1c) = *(int*)(host + 0x1c) + (int)Winsys3d.GetSinTableEntry(heading) * 0x0e;
        *(int*)(e + 0x24) = *(int*)(host + 0x24) + Winsys3d.GetCosTableEntry(heading) * 0x0e;

        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = plate;
            int frame = CrtRand();
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 4000;
            *(short*)(e + 0x2a) = 4000;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x0b;
            e[0x40] = (byte)(sbyte)(frame % 3);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;

        byte next = (byte)(e[0x40] + 1);
        e[0x40] = next;
        if (next > 2) e[0x40] = 0;

        if (*(short*)(e + 0x12) < 1) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_00427c10 @ 0x00427c10 — creneau <b>30</b> du bloc <c>0x005599a8</c>
    /// (sous-zones 6 a 10), planche <c>0x00550728</c>.</summary>
    private static void EntityHandler_00427C10(byte* e)
        => TickCastleHostOrbitSpark_00427C10(e, Descriptors00550728());   // Ghidra: &DAT_00550728

    /// <summary>Ghidra: FUN_0042b0c0 @ 0x0042b0c0 — creneau <b>30</b> du bloc <c>0x0055a028</c>
    /// (sous-zones 5 et 11), planche <c>0x005508d8</c>.</summary>
    private static void EntityHandler_0042B0C0(byte* e)
        => TickCastleHostOrbitSpark_00427C10(e, Descriptors005508D8());   // Ghidra: &DAT_005508d8

    /// <summary>Ghidra: FUN_00439b00 @ 0x00439b00 — creneau <b>44</b> du bloc <c>0x0055d428</c>,
    /// sous-zones 6 et 7 de la <b>grotte scellee</b>. Le meme corps a la planche pres :
    /// <c>0x00551118</c>. Ecrit ici, avec son parent du chateau.</summary>
    private static void EntityHandler_00439B00(byte* e)
        => TickCastleHostOrbitSpark_00427C10(e, Descriptors00551118());   // Ghidra: &DAT_00551118

    /// <summary>
    /// Ghidra: <c>0x00429270</c> — creneau <b>58</b> du bloc <c>0x005599a8</c>
    /// (table <c>0x0056b2c8</c>, sous-zones 6 a 10). Absente de <c>Blaze.c</c> ; 67 instructions,
    /// relevees au desassembleur.
    ///
    /// <para>
    /// <b>Un bloc qui tombe et qui blesse la ou il passe.</b> Il part avec la vitesse deja posee
    /// dans <c>+0x50</c> / <c>+0x54</c> / <c>+0x58</c> par ce qui l'a fait naitre, et <c>+0x54</c>
    /// perd <c>0x2000</c> par image : c'est la pesanteur. Sous <c>0xfa000</c> de hauteur il passe a
    /// l'etat 2.
    /// </para>
    /// <para>
    /// <b>A CHAQUE IMAGE DE SA CHUTE il appelle <see cref="DamagePartyNearPoint_00418F50"/></b> sur
    /// sa propre position, avec un rayon qui est <b>son echelle divisee par 50</b> — plus il est
    /// gros, plus il fait mal loin — pour <c>2 %</c> des points de vie maximum. Le site empile huit
    /// arguments et le corps appele n'en lit que six, comme partout ailleurs.
    /// </para>
    /// <para>
    /// L'etat 2 est l'extinction : la teinte perd 10 par canal et par image, et le bloc disparait
    /// quand son canal rouge atteint zero. Il ne s'arrete pas de tomber pour autant — rien ne
    /// touche plus a sa position.
    /// </para>
    /// </summary>
    private static void EntityHandler_00429270(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors005506E8();   // Ghidra: &DAT_005506e8
            e[0x40] = 0;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int x = *(int*)(e + 0x1c) + *(int*)(e + 0x50);
            int y = *(int*)(e + 0x20) + *(int*)(e + 0x54);
            int z = *(int*)(e + 0x24) + *(int*)(e + 0x58);

            *(int*)(e + 0x1c) = x;
            *(int*)(e + 0x20) = y;
            *(int*)(e + 0x24) = z;
            *(int*)(e + 0x54) += -0x2000;                 // Ghidra: add eax,0FFFFE000h

            if (y < 0xfa000) *(short*)(e + 0x10) = 2;

            // Ghidra: `imul 51EB851Fh ; sar edx,4` puis report du signe — une division par 50.
            short radius = (short)(*(short*)(e + 0x28) / 50);
            DamagePartyNearPoint_00418F50(x, y, z, radius, 2, 2);   // Ghidra: FUN_00418f50
        }

        if (*(short*)(e + 0x10) != 2) return;

        BrightenEntityTint_00404DE0(e, -10, -10, -10);     // Ghidra: FUN_00404de0
        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x0042a350</c> — creneau <b>33</b> du bloc <c>0x0055a028</c>
    /// (sous-zones 5 et 11). Absente de <c>Blaze.c</c> ; 71 instructions.
    ///
    /// <para>
    /// <b><see cref="TickTwoHostOrbitMote"/> a deux choses pres</b>, et le corps du Labyrinthe est
    /// le seul de tout le depot dont elle soit proche (21 lignes). La planche est
    /// <c>0x005508d8</c>, celle que <see cref="EntityHandler_0042B0C0"/> emploie deja. Et
    /// <b>le second porteur n'est pas protege</b> : le Labyrinthe fait retomber <c>+0x6c</c> nul
    /// sur <c>Res.s_EntityList_005de558</c>, le chateau ne teste que <c>+0x74</c>.
    /// </para>
    /// <para>
    /// Le reste du <c>diff</c> est de l'ordonnancement : les deux porteurs y vivent dans les
    /// registres <b>echanges</b> (<c>ebp</c> et <c>edi</c>), ce qui donne dix lignes qui ne disent
    /// rien.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042A350(byte* e)
        => TickTwoHostOrbitMote(e, Descriptors005508D8(), guardOffset: false);   // Ghidra: &DAT_005508d8

    /// <summary>
    /// Ghidra: FUN_00429330 @ 0x00429330 — creneau <b>14</b> du bloc <c>0x005597a8</c>
    /// (table <c>0x0056b0c8</c>, sous-zones 6 a 10). Decompilee dans <c>Blaze.c</c>, 60
    /// instructions.
    ///
    /// <para>
    /// <b>Une source, et rien d'autre.</b> A chaque image elle fait naitre <b>six</b> entites sur
    /// sa propre position — trois du sous-type <c>199</c> par
    /// <see cref="SpawnStatusEffectEntity_0047AC40"/> et trois du sous-type <c>0xc3</c> par
    /// <see cref="SpawnCastEntity_0047AAE0"/> — puis s'eteint au bout de quarante images. Elle ne
    /// regarde rien : ni le groupe, ni un drapeau, ni sa cible.
    /// </para>
    /// <para>
    /// <b>C'est la moitie de <see cref="EntityHandler_00426B10"/></b>, son cousin des sous-zones
    /// 0 a 4, qui seme les memes six entites mais y ajoute quatre sons et un script de zone. Celui-
    /// la est un mecanisme et vit dans <c>Handlers/ImmortalCastleMecanismes.cs</c> ; celui-ci ne
    /// decide de rien et reste ici. Les deux corps ne sont pas des copies : trente-quatre
    /// instructions les separent.
    /// </para>
    /// </summary>
    private static void EntityHandler_00429330(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;

        int* position = (int*)(e + 0x1c);
        SpawnStatusEffectEntity_0047AC40(e, position, null, 199, 0, -1);
        SpawnStatusEffectEntity_0047AC40(e, position, null, 199, 0, -1);
        SpawnStatusEffectEntity_0047AC40(e, position, null, 199, 0, -1);
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);
        SpawnCastEntity_0047AAE0(e, 0xc3, 0, -1);

        if (*(short*)(e + 0x12) > 0x28) *(uint*)e &= 0x7fffffff;
    }
}
