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

    // =====================================================================
    // PALIER 3 (2026-09-18) — LES « C » DES TABLES D'EFFETS
    // =====================================================================

    // ---------------------------------------------------------------------
    // LES QUATRE NUAGES A EFFET 32
    //
    // Le tri disait « leurs feuilles sont ecrites mais ce ne sont pas TickHostStatusCloud, 73
    // lignes d'ecart au plus proche ». Les 73 lignes ne sont pas dans le nuage : elles sont dans
    // une GERBE qui partage le corps, aiguillee par +0x9a comme le stroboscope de 0x0041FCA0 au
    // Labyrinthe. Retiree la gerbe, trois des quatre nuages sont `StepStatusCloud` tel quel, et les
    // trois « appelees inconnues » du tri (0x00425710, 0x00427480, 0x0042ABB0) sont
    // `ApplyStatusEffect32IfAllowed_00421D80` A L'OCTET PRES, deplacement du `jne` interne excepte.
    // ---------------------------------------------------------------------

    /// <summary>
    /// <b>La gerbe des nuages du chateau</b> — ce que <see cref="EntityHandler_0042A920"/>,
    /// <see cref="EntityHandler_0042B190"/> et <see cref="EntityHandler_004271B0"/> font quand
    /// <c>+0x9a</c> ne vaut pas 2. Trois temps :
    /// <list type="table">
    ///   <item><term>1 — vingt images</term><description>l'entite devient le <b>modele
    ///     <c>0x3f</c></b>, a plat (<c>+0x28 = 0x1fff</c>, <c>+0x2a = 0</c>, <c>+0x2c = 0x100</c>),
    ///     tournee au hasard, et fait naitre <b>une incantation <c>0x70</c> par image</b>. Le porteur
    ///     brut <c>+0x6c</c> — nul compris — est recopie en <c>+0x70</c> a chaque image, et en
    ///     <c>+0x74</c> a la naissance.</description></item>
    ///   <item><term>2 — une image</term><description>deux incantations <c>0xdf</c>, l'une a
    ///     <c>+0x5c = 200</c>, l'autre a <c>-200</c>.</description></item>
    ///   <item><term>3 — trente images</term><description>rien, puis la mort.</description></item>
    /// </list>
    /// </summary>
    /// <param name="relabelSeeds">Vrai pour <c>0x0042A920</c> et <c>0x0042B190</c> : les deux
    /// <c>0xdf</c> naissent avec <c>+0x9a = 2</c> — le corps le pose le temps des deux appels puis
    /// rend la valeur d'origine ; l'incantation copie son parent, donc ses deux enfants en
    /// heritent. <b>Ce ne sont pas des nuages</b> : une incantation de sous-type <c>0xc0 + n</c>
    /// va au creneau n de <c>0x0056b1c8</c> (<c>0x0056aec8 + sous-type * 4</c>), et le creneau 31
    /// de ce bloc est <c>0x0042ABF0</c> aux sous-zones 5 et 11 — un corps du palier 4, pas encore
    /// ecrit. Les nuages sont au creneau 31 de <c>0x0056b0c8</c>, qui sert la table du decor.
    /// <c>0x004271B0</c> ne marque pas ses enfants.</param>
    /// <param name="voiced">Vrai pour <c>0x004271B0</c> : le son <c>0x10</c> a la naissance
    /// (hauteur <c>0xdfa</c>, gain <c>0x1300</c>), et le son <c>0x15</c> (hauteur <c>0x800</c>) au
    /// temps 2, compte par <c>+0x98</c>. Le binaire teste aussi <c>+0x98 == 1</c> pour un son
    /// <c>0x1c</c>, <b>avant</b> le test a zero : comme <c>+0x98</c> vient d'etre remis a zero et
    /// que le temps 2 ne dure qu'une image, ce son-la ne part jamais. Transcrit tel quel.</param>
    private static void StepCastleCloudBurst(byte* e, bool relabelSeeds, bool voiced)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x16) = 0x14;
            *(byte**)(e + 0x74) = *(byte**)(e + 0x6c);
            if (voiced) TryPlaySoundNearEntity((uint*)e, 0x10, 0xdfa, 0x1300, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x16))--;
            if (*(short*)(e + 0x16) < 1)
            {
                *(short*)(e + 0x10) = 2;
                if (voiced) *(short*)(e + 0x98) = 0;
            }

            e[4] = 0x3f;
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(byte**)(e + 0x70) = *(byte**)(e + 0x6c);
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0x50000;
            *(int*)(e + 0x4c) = 0;
            *(ushort*)(e + 0x32) = (ushort)(CrtRand() % 0x400 + 0x400);   // Ghidra: FUN_00547140
            *(short*)(e + 0x14) = 0;
            *(ushort*)(e + 0x34) = (ushort)(CrtRand() % 0x1000);
            *(short*)(e + 0x12) = 200;
            *(short*)(e + 0x28) = 0x1fff;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x2c) = 0x100;
            SpawnCastEntity_0047AAE0(e, 0x70, 0, -1);   // Ghidra: FUN_0047aae0
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x10) = 3;
            *(short*)(e + 0x16) = 0x1e;

            short side = *(short*)(e + 0x9a);
            if (relabelSeeds) *(short*)(e + 0x9a) = 2;

            *(int*)(e + 0x5c) = 200;
            SpawnCastEntity_0047AAE0(e, 0xdf, 0, -1);
            *(int*)(e + 0x5c) = -200;
            SpawnCastEntity_0047AAE0(e, 0xdf, 0, -1);

            if (relabelSeeds) *(short*)(e + 0x9a) = side;

            if (voiced)
            {
                if (*(short*)(e + 0x98) == 1)
                {
                    TryPlaySoundNearEntity((uint*)e, 0x1c, 0x1000, 0x1300, 0, 0);
                    (*(short*)(e + 0x98))++;
                }

                if (*(short*)(e + 0x98) == 0)
                {
                    TryPlaySoundNearEntity((uint*)e, 0x15, 0x800, 0x1300, 0, 0);
                    (*(short*)(e + 0x98))++;
                }
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x16))--;
            if (*(short*)(e + 0x16) < 0) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x0042a920</c> — creneau <b>27</b> du bloc <c>0x00559e28</c> (<c>0x0056b0c8</c>,
    /// sous-zones 5 et 11). Absent de <c>Blaze.c</c> ; 189 instructions.
    ///
    /// <para>
    /// Le porteur nul devient <c>Res.s_EntityList_005de558</c>, puis
    /// <see cref="TrackCarrierOrDie"/> — voile noir compris — comme au Labyrinthe. <c>+0x9a == 0</c>
    /// : la gerbe (<see cref="StepCastleCloudBurst"/>), dont les deux enfants naissent marques
    /// <c>+0x9a = 2</c>.
    /// <c>+0x9a == 2</c> : <b>le nuage du creneau 31 du Labyrinthe</b>,
    /// <see cref="StepStatusCloud"/> en <c>0xdf</c>, l'effet garde sans repli de la cible. Avec
    /// <c>+0x9a == 1</c>, rien.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042A920(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) host = Res.s_EntityList_005de558;

        if (!TrackCarrierOrDie(e, host)) return;

        if (*(short*)(e + 0x9a) == 0) StepCastleCloudBurst(e, relabelSeeds: true, voiced: false);
        if (*(short*)(e + 0x9a) == 2) StepStatusCloud(e, host, 0xdf, CloudEffectGate.Guarded);   // Ghidra: FUN_0042abb0
    }

    /// <summary>
    /// Ghidra: <c>0x0042b190</c> — creneau <b>31</b> du bloc <c>0x00559e28</c>. Absent de
    /// <c>Blaze.c</c> ; 192 instructions. <see cref="EntityHandler_0042A920"/> a un test pres,
    /// relu ligne a ligne : la gerbe part pour <c>+0x9a</c> <b>0 ou 1</b>, pas seulement 0.
    /// </summary>
    private static void EntityHandler_0042B190(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) host = Res.s_EntityList_005de558;

        if (!TrackCarrierOrDie(e, host)) return;

        short side = *(short*)(e + 0x9a);
        if (side == 0 || side == 1) StepCastleCloudBurst(e, relabelSeeds: true, voiced: false);
        if (*(short*)(e + 0x9a) == 2) StepStatusCloud(e, host, 0xdf, CloudEffectGate.Guarded);   // Ghidra: FUN_0042abb0
    }

    /// <summary>
    /// Ghidra: FUN_004271b0 @ 0x004271b0 — creneau <b>31</b> du bloc <c>0x005597a8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 6 a 10). Decompile dans <c>Blaze.c</c>, 211 instructions.
    ///
    /// <para>
    /// Le meme assemblage que <see cref="EntityHandler_0042B190"/>, avec trois ecarts :
    /// </para>
    /// <list type="bullet">
    ///   <item>le porteur mort ne fait que <b>tuer le nuage</b> — pas de voile noir ;</item>
    ///   <item>la gerbe est <b>sonore</b> et ne marque pas ses enfants : ce sont deux <c>0xdf</c>
    ///     ordinaires ;</item>
    ///   <item>le nuage replie sa cible nulle sur la liste d'entites, et <b>tamponne
    ///     <c>+0x48</c></b> de ses semis (voir <see cref="StepStatusCloud"/>).</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_004271B0(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) host = Res.s_EntityList_005de558;

        *(int*)(e + 0x1c) = *(int*)(host + 0x1c);
        *(int*)(e + 0x20) = *(int*)(host + 0x20);
        *(int*)(e + 0x24) = *(int*)(host + 0x24);

        uint hostFlags = *(uint*)host;
        if ((hostFlags & 0x40000000) != 0 || (hostFlags & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;
            return;
        }

        short side = *(short*)(e + 0x9a);
        if (side == 0 || side == 1) StepCastleCloudBurst(e, relabelSeeds: false, voiced: true);

        if (*(short*)(e + 0x9a) == 2)
        {
            StepStatusCloud(e, host, 0xdf, CloudEffectGate.GuardedWithFallback, stampSeeds: true);   // Ghidra: FUN_00427480
        }
    }

    /// <summary>
    /// Ghidra: FUN_004255f0 @ 0x004255f0 — creneau <b>32</b> du bloc <c>0x00559128</c>
    /// (<c>0x0056b0c8</c>, sous-zones 0 a 4). Decompile dans <c>Blaze.c</c>, 91 instructions.
    ///
    /// <para>
    /// <b>Un nuage sans gerbe, et qui ne se prolonge pas.</b> Il ne lit pas <c>+0x9a</c> ; le
    /// porteur nul et la cible nulle se replient sur la liste d'entites ; le porteur mort tue le
    /// nuage sans voile. La naissance et l'attente de <c>+0x44</c> sont celles de
    /// <see cref="StepStatusCloud"/>.
    /// </para>
    /// <para>
    /// <b>La fin differe</b> : a l'etat 2, le semis toutes les trois images part <b>sans
    /// condition</b>, et le nuage meurt des que son compte <c>15 + rand() % 10</c> tombe a zero. Le
    /// Labyrinthe, lui, cesse de semer a zero mais ne meurt qu'a <c>-0x23</c> : il reste la
    /// trente-cinq fois trois images de plus, muet. C'est l'ecart que le tri mesurait.
    /// </para>
    /// </summary>
    private static void EntityHandler_004255F0(byte* e)
    {
        byte* host = *(byte**)(e + 0x6c);
        if (host == null) host = Res.s_EntityList_005de558;

        int* position = (int*)(e + 0x1c);
        position[0] = *(int*)(host + 0x1c);
        position[1] = *(int*)(host + 0x20);
        position[2] = *(int*)(host + 0x24);

        uint hostFlags = *(uint*)host;
        if ((hostFlags & 0x40000000) != 0 || (hostFlags & 0x80000000) == 0)
        {
            *(uint*)e &= 0x7fffffff;
            return;
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = 0;
            for (int i = 0; i < 0xb; i++)
                SpawnStatusEffectEntity_0047AC40(e, position, null, 0xdf, 0, -1);   // Ghidra: FUN_0047ac40

            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x32) *(uint*)e &= 0x7fffffff;

            if (*(int*)(e + 0x44) == 1)
            {
                *(short*)(e + 0x10) = 2;
                int roll = CrtRand();                                   // Ghidra: FUN_00547140
                *(short*)(e + 0x14) = 0;

                uint* victim = *(uint**)(e + 0x78);
                *(short*)(e + 0x12) = (short)(roll % 10 + 0xf);
                if (victim == null) victim = (uint*)Res.s_EntityList_005de558;

                if (ApplyStatusEffect32IfAllowed_00421D80((uint*)host, victim, 0))   // Ghidra: FUN_00425710
                {
                    *(uint*)e &= 0x7fffffff;
                    return;
                }
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        (*(short*)(e + 0x14))++;
        if (*(short*)(e + 0x14) != 3) return;

        *(short*)(e + 0x14) = 0;
        SpawnStatusEffectEntity_0047AC40(e, position, null, 0xdf, 0, -1);

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) <= 0) *(uint*)e &= 0x7fffffff;
    }

    // ---------------------------------------------------------------------
    // CE QUI JAILLIT
    // ---------------------------------------------------------------------

    /// <summary>
    /// Le corps de <see cref="EntityHandler_00426E80"/> et de son miroir. Decompile dans
    /// <c>Blaze.c</c>, 103 instructions ; les deux exemplaires ne different que par l'adresse de
    /// leur planche.
    ///
    /// <para>
    /// <b>Une etincelle qui tombe en s'eteignant.</b> A la naissance elle prend l'enregistrement
    /// de la bulle (<c>+0x78</c> de la planche de groupe) et ne l'avance jamais (<c>+0x40 = 0</c>),
    /// retient sa position de depart en <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, et tire :
    /// un cap <c>+0x12</c> sur un tour entier, un rayon <c>+0x14</c> de 0 a 199, une vitesse de
    /// chute <c>+0x16</c> de 3 a 6, une echelle carree de <c>0xc00</c> a <c>0x13ff</c>, et trois
    /// composantes de teinte de <c>0xa0</c> a <c>0xd1</c>.
    /// </para>
    /// <para>
    /// Chaque image elle se place <b>sur son cercle</b> — sinus et cosinus du cap, fois le rayon,
    /// depuis le depart —, descend de <c>+0x16</c> unites, et <c>+0x16</c> grandit d'un : elle
    /// accelere. <see cref="BrightenEntityTint_00404DE0"/> retire huit a chaque composante ; elle
    /// meurt quand le rouge <c>+0x38</c> atteint zero.
    /// </para>
    /// </summary>
    private static void TickCastleFallingSpark_00426E80(byte* e, byte* plate)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;
            e[0x0a] = 7;
            e[0x0b] = 0xf4;

            *(ushort*)(e + 0x12) = (ushort)(CrtRand() % 0x1000);   // Ghidra: FUN_00547140
            int radius = CrtRand();
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            *(short*)(e + 0x14) = (short)(radius % 200);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
            *(ushort*)(e + 0x16) = (ushort)(CrtRand() % 4 + 3);

            short scale = (short)(CrtRand() % 0x800 + 0xc00);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;

            e[0x38] = (byte)(CrtRand() % 0x32 - 0x60);
            e[0x39] = (byte)(CrtRand() % 0x32 - 0x60);
            e[0x3a] = (byte)(CrtRand() % 0x32 - 0x60);
        }

        if (*(short*)(e + 0x10) != 1) return;

        int sin = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: Ordinal_451
        *(int*)(e + 0x1c) = sin * *(short*)(e + 0x14) + *(int*)(e + 0x50);
        int cos = Winsys3d.GetCosTableEntry(*(short*)(e + 0x12));        // Ghidra: Ordinal_450
        *(int*)(e + 0x24) = cos * *(short*)(e + 0x14) + *(int*)(e + 0x58);

        *(int*)(e + 0x20) += *(short*)(e + 0x16) * 0x1000;
        (*(short*)(e + 0x16))++;

        BrightenEntityTint_00404DE0(e, -8, -8, -8);   // Ghidra: FUN_00404de0
        if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>Ghidra: FUN_00426e80 @ 0x00426e80 — creneau <b>60</b> du bloc <c>0x00559328</c>
    /// (<c>0x0056b2c8</c>, sous-zones 0 a 4), planche <c>0x005504f8</c>.</summary>
    private static void EntityHandler_00426E80(byte* e)
        => TickCastleFallingSpark_00426E80(e, Descriptors00550480() + 0x78);   // Ghidra: &DAT_005504f8

    /// <summary>Ghidra: FUN_004296f0 @ 0x004296f0 — creneau <b>60</b> du bloc <c>0x005599a8</c>
    /// (sous-zones 6 a 10), planche <c>0x00550648</c>.</summary>
    private static void EntityHandler_004296F0(byte* e)
        => TickCastleFallingSpark_00426E80(e, Descriptors005505D0() + 0x78);   // Ghidra: &DAT_00550648

    /// <summary>
    /// Ghidra: FUN_004294d0 @ 0x004294d0 — creneau <b>16</b> des blocs <c>0x00559128</c> et
    /// <c>0x005597a8</c> (<c>0x0056b0c8</c>, sous-zones 0 a 4 et 6 a 10 : la meme adresse dans les
    /// deux). Decompile dans <c>Blaze.c</c>, 131 instructions.
    ///
    /// <para>
    /// <b>Une grille de quinze incantations <c>0xc2</c>, et rien d'autre.</b> L'entite se pose
    /// successivement sur cinq X (<c>-4400</c> a <c>-3600</c> unites, au pas de
    /// 200) et, pour chacun, sur trois Z (<c>100</c>, <c>300</c>, <c>700</c>), a la hauteur 850, et fait naitre une incantation a chaque point. Puis elle meurt, dans
    /// la meme image. Le binaire deroule les quinze blocs ; c'est la boucle qui est ecrite ici.
    /// </para>
    /// </summary>
    private static void EntityHandler_004294D0(byte* e)
    {
        ReadOnlySpan<int> columns =
            [unchecked((int)0xfeed0000), unchecked((int)0xfef98000), unchecked((int)0xff060000),
             unchecked((int)0xff128000), unchecked((int)0xff1f0000)];

        foreach (int x in columns)
        {
            *(int*)(e + 0x1c) = x;
            *(int*)(e + 0x20) = 0x352000;
            *(int*)(e + 0x24) = 0x64000;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);   // Ghidra: FUN_0047aae0
            *(int*)(e + 0x24) = 0x12c000;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);
            *(int*)(e + 0x24) = 0x2bc000;
            SpawnCastEntity_0047AAE0(e, 0xc2, 0, -1);
        }

        *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: FUN_00429010 @ 0x00429010 — creneau <b>63</b> du bloc <c>0x005597a8</c>
    /// (<c>0x0056b0c8</c>, sous-zones 6 a 10). Decompile dans <c>Blaze.c</c>, 138 instructions,
    /// une table de sauts a huit entrees.
    ///
    /// <para>
    /// <b>Un jet qui part de l'un de huit points, toutes les vingt-cinq images environ.</b>
    /// </para>
    /// <list type="table">
    ///   <item><term>depart (etat 1)</term><description>teinte <c>(200, 0x32, 0xaa)</c>, echelle
    ///     8000, huit reprises ; <c>rand() % 8</c> choisit un point du mur ouest (X <c>-4100</c> ou
    ///     <c>-3900</c> unites, hauteur 300) et une vitesse <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>
    ///     — toujours vers le haut, et dans six cas sur huit vers <c>+Z</c> ou <c>-Z</c>. Le son
    ///     <c>0x29</c> de la zone part, hauteur 900.</description></item>
    ///   <item><term>jet (etat 2)</term><description>toutes les quatre images, un grain
    ///     <c>0xfa</c> nait a la position de l'entite, l'echelle perd 500 et la teinte
    ///     <c>(-16, -2, -8)</c>. Apres la huitieme reprise, etat 3.</description></item>
    ///   <item><term>repos (etat 3)</term><description>seize images, puis le son <c>0x1f</c>
    ///     (hauteur <c>0x1c2</c>, gain <c>0x1200</c>, <b>numero absolu</b>) et le retour a l'etat
    ///     1.</description></item>
    /// </list>
    /// <para>
    /// L'entite elle-meme ne se deplace pas : elle ne fait que poser <c>+0x50</c>..<c>+0x58</c>,
    /// que ses grains heritent en naissant. Le depart et la premiere image du jet tombent dans la
    /// meme image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00429010(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 8;
            *(short*)(e + 0x14) = 0;
            e[0x38] = 200;
            e[0x39] = 0x32;
            e[0x3a] = 0xaa;
            *(short*)(e + 0x28) = 8000;
            *(short*)(e + 0x2a) = 8000;

            int vx = 0, vy = 0x1a000, vz = 0xf000;
            int x = unchecked((int)0xfeffc000), z;

            switch (CrtRand() % 8)   // Ghidra: FUN_00547140
            {
                case 0: z = unchecked((int)0xff95c000); break;
                case 1: z = unchecked((int)0xffaec000); break;
                case 2: z = unchecked((int)0xffc7c000); break;
                case 3: vz = unchecked((int)0xffff1000); z = unchecked((int)0xffe0c000); break;
                case 4: vy = 0x27000; z = unchecked((int)0xffe0c000); break;
                case 5: vz = unchecked((int)0xffff1000); z = 0x1f4000; break;
                case 6: vx = 0x5000; vz = 0x5000; z = unchecked((int)0xffe0c000); break;
                default: vz = 0; x = unchecked((int)0xff0c4000); z = unchecked((int)0xffed4000); break;
            }

            *(int*)(e + 0x50) = vx;
            *(int*)(e + 0x54) = vy;
            *(int*)(e + 0x58) = vz;
            *(int*)(e + 0x1c) = x;
            *(int*)(e + 0x20) = 0x12c000;
            *(int*)(e + 0x24) = z;

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x29), 900, 0x1000, 0, 0);   // Ghidra: FUN_00468080
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x14))++;
            if (*(short*)(e + 0x14) > 3)
            {
                (*(short*)(e + 0x12))--;
                *(short*)(e + 0x14) = 0;

                if (*(short*)(e + 0x12) < 0)
                {
                    *(short*)(e + 0x10) = 3;
                    *(short*)(e + 0x12) = 0;
                }

                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xfa, 0, -1);   // Ghidra: FUN_0047ac40
                *(short*)(e + 0x28) -= 500;
                *(short*)(e + 0x2a) -= 500;
                BrightenEntityTint_00404DE0(e, -16, -2, -8);                               // Ghidra: FUN_00404de0
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0xf)
            {
                *(short*)(e + 0x10) = 1;
                TryPlaySoundNearEntity((uint*)e, 0x1f, 0x1c2, 0x1200, 0, 0);
            }
        }
    }

    // ---------------------------------------------------------------------
    // SOUS-ZONES 5 ET 11 — TROIS EFFETS, ET TROIS PLANCHES NEUVES
    // ---------------------------------------------------------------------

    /// <summary>Ghidra: <c>&amp;DAT_005507e0</c> — <b>la planche de l'eclat qui derive</b>, un seul
    /// enregistrement. C'est le « changement de famille » <c>48 00 10 10</c> sur lequel se ferme
    /// <see cref="s_Descriptors_00550740"/> : il avait ete lu comme borne, il est aussi une
    /// planche. Bornee en haut par <c>0x005507e8</c>, ou la tuile passe en <c>0x1f x 0x1f</c> de la
    /// couche <c>0x0a</c>. Le corps pose <c>+0x40 = 0</c> et ne l'avance jamais.</summary>
    private static readonly byte[] s_Descriptors_005507E0 =
    [
        0x48, 0x00, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x005507e0  <- 0x0042D9B0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00551410</c> — la meme planche pour la <b>grotte
    /// scellee</b>, memes octets, un seul enregistrement. Elle commence exactement ou
    /// <see cref="s_Descriptors_00551408"/> s'arrete et se ferme sur <c>0x00551418</c>
    /// (<c>00 00 2f 2f</c>).</summary>
    private static readonly byte[] s_Descriptors_00551410 =
    [
        0x48, 0x00, 0x10, 0x10, 0x01, 0x02, 0x10, 0x00,   // 0x00551410  <- 0x00442AC0
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00550828</c> — <b>la planche de l'etincelle qui jaillit</b>,
    /// <b>douze</b> enregistrements : deux rangees de six tuiles <c>0x1f x 0x1f</c> de la couche
    /// <c>0x0a</c>, a <c>y = 0xb0</c> puis <c>y = 0xd0</c>. Le corps avance <c>+0x40</c> et le
    /// <b>plafonne a 11</b> (<c>cmp al,0Ch ; jb</c> puis <c>mov byte [esi+40h],0Bh</c>) : la
    /// plage couvre exactement l'index le plus haut. Bornee par <c>0x00550888</c>
    /// (<see cref="s_Descriptors_00550888"/>, deja ecrite). Les huit enregistrements de
    /// <c>0x005507e8</c> a <c>0x00550827</c>, meme couche, rangee <c>y = 0x30</c>, n'appartiennent
    /// a aucun corps de ce palier.</summary>
    private static readonly byte[] s_Descriptors_00550828 =
    [
        0x00, 0xb0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550828  <- 0x0042D350
        0x20, 0xb0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550830
        0x40, 0xb0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550838
        0x60, 0xb0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550840
        0x80, 0xb0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550848
        0xa0, 0xb0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550850
        0x00, 0xd0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550858
        0x20, 0xd0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550860
        0x40, 0xd0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550868
        0x60, 0xd0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550870
        0x80, 0xd0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550878
        0xa0, 0xd0, 0x1f, 0x1f, 0x01, 0x02, 0x0a, 0x00,   // 0x00550880  <- +0x40 = 11, le plafond
    ];

    private static byte* s_Descriptors005507E0Storage;
    private static byte* s_Descriptors00551410Storage;
    private static byte* s_Descriptors00550828Storage;

    private static byte* Descriptors005507E0() => NativeCopy(s_Descriptors_005507E0, ref s_Descriptors005507E0Storage);
    private static byte* Descriptors00551410() => NativeCopy(s_Descriptors_00551410, ref s_Descriptors00551410Storage);
    private static byte* Descriptors00550828() => NativeCopy(s_Descriptors_00550828, ref s_Descriptors00550828Storage);

    /// <summary>
    /// Le corps de <see cref="EntityHandler_0042D9B0"/> (chateau) et de
    /// <see cref="EntityHandler_00442AC0"/> (grotte scellee). Absent de <c>Blaze.c</c> ; 114
    /// instructions, identiques aux deux adresses sauf la hauteur de depart et la planche.
    ///
    /// <para>
    /// <b>Un eclat qui part tout droit et s'eteint.</b> A la naissance sa hauteur <c>+0x20</c> gagne
    /// <paramref name="drop"/>, prend une echelle de <c>0x800</c> sur les trois axes, et tire trois
    /// vitesses de <b>2 a 6 unites par image, de signe tire a pile ou face</b> — six tirages en tout,
    /// un module et un signe par axe. Il vole ainsi <c>10 + rand() % 8</c> images a vitesse
    /// constante.
    /// </para>
    /// <para>
    /// Ensuite il continue de voler et <b>perd treize par image sur chaque composante de
    /// teinte</b>. Seul le rouge est teste : quand il passerait sous zero, les trois tombent a zero
    /// et l'eclat meurt. Le vert et le bleu, eux, peuvent boucler sous zero avant — l'octet est
    /// ecrit tel quel.
    /// </para>
    /// </summary>
    private static void TickCastleDriftSpark_0042D9B0(byte* e, byte* plate, int drop)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            int y = *(int*)(e + 0x20) + drop;
            *(short*)(e + 0x10) = 1;
            *(byte**)(e + 0x3c) = plate;
            e[0x40] = 0;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x28) = 0x800;
            *(short*)(e + 0x2a) = 0x800;
            *(short*)(e + 0x2c) = 0x800;
            *(int*)(e + 0x20) = y;

            for (int axis = 0; axis < 3; axis++)
            {
                int speed = CrtRand() % 5 + 2;                  // Ghidra: FUN_00547140
                int sign = (CrtRand() % 2) * 2 - 1;
                *(int*)(e + 0x50 + axis * 4) = speed * sign;
            }

            *(short*)(e + 0x12) = (short)(CrtRand() % 8 + 10);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(int*)(e + 0x1c) += *(int*)(e + 0x50) << 0xc;
        *(int*)(e + 0x20) += *(int*)(e + 0x54) << 0xc;
        *(int*)(e + 0x24) += *(int*)(e + 0x58) << 0xc;

        (*(short*)(e + 0x12))--;
        if (*(short*)(e + 0x12) >= 0) return;

        short r = (short)(e[0x38] - 0xd);
        short g = (short)(e[0x39] - 0xd);
        short b = (short)(e[0x3a] - 0xd);

        if (r < 0)
        {
            r = 0;
            g = 0;
            b = 0;
            *(uint*)e &= 0x7fffffff;
        }

        e[0x38] = (byte)r;
        e[0x39] = (byte)g;
        e[0x3a] = (byte)b;
    }

    /// <summary>Ghidra: <c>0x0042d9b0</c> — creneau <b>4</b> du bloc <c>0x0055a028</c>
    /// (<c>0x0056b2c8</c>, sous-zones 5 et 11) : un decalage vertical de cent unites, planche
    /// <c>0x005507e0</c>.</summary>
    private static void EntityHandler_0042D9B0(byte* e)
        => TickCastleDriftSpark_0042D9B0(e, Descriptors005507E0(), 0x64000);   // Ghidra: &DAT_005507e0

    /// <summary>Ghidra: <c>0x00442ac0</c> — la <b>grotte scellee</b>, table <c>0x0056b2c8</c>,
    /// sous-zones 13 a 15 : cent cinquante unites de decalage, planche <c>0x00551410</c>. Deux
    /// immediates d'ecart avec le chateau, et rien d'autre.</summary>
    private static void EntityHandler_00442AC0(byte* e)
        => TickCastleDriftSpark_0042D9B0(e, Descriptors00551410(), 0x96000);   // Ghidra: &DAT_00551410

    /// <summary>
    /// Ghidra: <c>0x0042d350</c> — creneau <b>5</b> du bloc <c>0x0055a028</c> (<c>0x0056b2c8</c>,
    /// sous-zones 5 et 11). Absent de <c>Blaze.c</c> ; 127 instructions.
    ///
    /// <para>
    /// <b>Une etincelle qui jaillit, retombe et s'eteint.</b> A la naissance sa hauteur
    /// <c>+0x20</c> gagne trente unites ; ce point devient son centre
    /// (<c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>), et elle
    /// tire : une echelle carree de <c>0x800</c> a <c>0xfff</c>, un cap <c>+0x32</c>, un pas de
    /// rayon <c>+0x9a</c> de 2 a 13, et une vitesse verticale <c>+0x14</c> de 10 a 29. Sa teinte est
    /// un gris <c>0x80</c>.
    /// </para>
    /// <para>
    /// Chaque image elle se pose sur son rayon <c>+0x12</c> autour du centre, puis le rayon grandit
    /// du pas : elle s'eloigne <b>en ligne droite</b> — le cap ne tourne pas —, sa hauteur change de
    /// <c>+0x14</c>, et <c>+0x14</c> perd un par image jusqu'a passer sous <c>-10</c>. De ce moment
    /// (<c>+0x44 = 1</c>) il <b>regagne</b> un par image, et la teinte perd deux par composante :
    /// elle meurt quand le rouge atteint zero.
    /// </para>
    /// <para>
    /// <b>L'animation ne commence qu'apres dix images impaires</b> : <c>+0x16</c> part de 10 et ne
    /// descend qu'une image sur deux (le bit 0 de l'horloge <c>+0x5c</c>) ; arrive a zero, il y
    /// reste, et chaque image impaire avance alors <c>+0x40</c> d'un cran jusqu'au douzieme.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042D350(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(byte**)(e + 0x3c) = Descriptors00550828();         // Ghidra: &DAT_00550828
            e[0x40] = 0;
            e[0x0a] = 6;
            e[0x0b] = 0;

            short scale = (short)(CrtRand() % 0x800 + 0x800);    // Ghidra: FUN_00547140
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            *(int*)(e + 0x20) += 0x1e000;

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(ushort*)(e + 0x32) = (ushort)(CrtRand() % 0x1000);
            *(int*)(e + 0x5c) = 0;
            *(short*)(e + 0x9a) = (short)(CrtRand() % 0xc + 2);
            *(int*)(e + 0x44) = 0;
            int climb = CrtRand() % 0x14;
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
            *(short*)(e + 0x16) = 10;
            *(int*)(e + 0x58) = *(int*)(e + 0x24);
            e[0x38] = 0x80;
            e[0x39] = 0x80;
            e[0x3a] = 0x80;
            *(short*)(e + 0x14) = (short)(climb + 10);
            *(int*)(e + 0x50) = *(int*)(e + 0x1c);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short radius = *(short*)(e + 0x12);

            *(int*)(e + 0x1c) = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * radius + *(int*)(e + 0x50);   // Ghidra: Ordinal_451
            *(int*)(e + 0x24) = Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * radius + *(int*)(e + 0x58);          // Ghidra: Ordinal_450
            *(int*)(e + 0x20) += *(short*)(e + 0x14) << 0xc;

            if (*(int*)(e + 0x44) == 0)
            {
                (*(short*)(e + 0x14))--;
                if (*(short*)(e + 0x14) < -10) *(int*)(e + 0x44) = 1;
            }
            else
            {
                (*(short*)(e + 0x14))++;
            }

            *(short*)(e + 0x12) = (short)(*(short*)(e + 0x9a) + radius);

            if (*(int*)(e + 0x44) == 1) BrightenEntityTint_00404DE0(e, -2, -2, -2);   // Ghidra: FUN_00404de0

            if ((e[0x5c] & 1) != 0)
            {
                (*(short*)(e + 0x16))--;
                if (*(short*)(e + 0x16) <= 0)
                {
                    e[0x40]++;
                    if (e[0x40] >= 0xc) e[0x40] = 0xb;
                }
            }

            if (e[0x38] == 0) *(uint*)e &= 0x7fffffff;
        }

        (*(int*)(e + 0x5c))++;
    }

    /// <summary>
    /// Ghidra: FUN_0042d770 @ 0x0042d770 — creneau <b>1</b> du bloc <c>0x00559f28</c>
    /// (<c>0x0056b1c8</c>, sous-zones 5 et 11). Decompile dans <c>Blaze.c</c>, 174 instructions.
    ///
    /// <para>
    /// <b>L'eclair, maillon par maillon.</b> Chaque maillon est le modele 8 a 11 — retire au sort a
    /// chaque image, c'est ce qui le fait crepiter —, etire a <c>+0x28 = 3000</c>, tourne au hasard
    /// autour de la verticale et incline de <c>0x400</c> a <c>0x5f3</c>. A la naissance il
    /// <b>decremente</b> son rang <c>+0x14</c>, le recopie en <c>+0x9a</c>, et, tant que le rang
    /// reste positif ou nul, <b>fait naitre le maillon suivant</b> — une incantation <c>0xc1</c>
    /// posee a 150 unites dans la direction de son inclinaison, sa propre position etant rendue
    /// aussitot apres. Le maillon suivant herite du rang, et la chaine s'arrete d'elle-meme.
    /// </para>
    /// <para>
    /// <c>+0x9a</c> sert ensuite de compte a rebours : le maillon est visible (<c>0x4000</c>)
    /// autant d'images que son rang. Quand il tombe a zero, il <b>blanchit le fond</b>
    /// (<c>0x005e9160</c> a <c>0xfa</c> sur trois composantes, mode 1) et s'efface. Le maillon de
    /// rang 3 seme en plus, pendant dix images, un <c>0xc4</c> et un <c>0xc6</c> — les creneaux 4
    /// et 6 de <c>0x0056b2c8</c> : l'eclat qui derive (<see cref="EntityHandler_0042D9B0"/>) et la
    /// braise qui retombe (<c>0x0042DB10</c>) — en se faisant
    /// passer pour un rang 0 le temps des deux naissances. A <c>-20</c>, il rend au fond les trois
    /// valeurs qu'il a <b>heritees</b> en <c>+0x50</c>..<c>+0x58</c> — ce corps ne les ecrit pas —
    /// en mode 2, puis s'eteint de quinze par composante et meurt quand le rouge passe sous zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_0042D770(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            e[4] = 8;
            e[0x0a] = 9;
            e[0x0b] = 0;
            *(short*)(e + 0x34) = (short)(CrtRand() % 500 + 0x400);   // Ghidra: FUN_00547140
            int heading = CrtRand();
            *(short*)(e + 0x28) = 3000;
            (*(short*)(e + 0x14))--;
            *(ushort*)(e + 0x32) = (ushort)(heading % 0x1000);
            *(short*)(e + 0x9a) = *(short*)(e + 0x14);

            if (*(short*)(e + 0x14) >= 0)
            {
                int z = *(int*)(e + 0x24);
                int x = *(int*)(e + 0x1c);
                int y = *(int*)(e + 0x20);

                int reach = Winsys3d.GetCosTableEntry(*(short*)(e + 0x34)) * 0x96 >> 0xc;    // Ghidra: Ordinal_450
                *(int*)(e + 0x1c) -= reach * Winsys3d.GetCosTableEntry(*(short*)(e + 0x32));
                reach = Winsys3d.GetCosTableEntry(*(short*)(e + 0x34)) * 0x96 >> 0xc;
                *(int*)(e + 0x24) += reach * (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32));   // Ghidra: Ordinal_451
                *(int*)(e + 0x20) += (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x34)) * 0x96;

                SpawnCastEntity_0047AAE0(e, 0xc1, 0, -1);   // Ghidra: FUN_0047aae0

                *(int*)(e + 0x1c) = x;
                *(int*)(e + 0x20) = y;
                *(int*)(e + 0x24) = z;
            }

            *(uint*)e |= 0x4000;
        }

        if (*(short*)(e + 0x10) == 1) e[4] = (byte)(CrtRand() % 4 + 8);

        (*(short*)(e + 0x9a))--;

        if (*(short*)(e + 0x9a) < 1)
        {
            if (*(short*)(e + 0x9a) == 0) SetSceneBackdrop(0xfa, 0xfa, 0xfa, 1);   // Ghidra: DAT_005e9160..66

            *(uint*)e &= 0xffffbfff;

            if (*(short*)(e + 0x14) == 3 && *(short*)(e + 0x9a) > -0xb)
            {
                *(short*)(e + 0x14) = 0;
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc4, 0, -1);   // Ghidra: FUN_0047ac40
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xc6, 0, -1);
                *(short*)(e + 0x14) = 3;
            }
        }

        if (*(short*)(e + 0x9a) >= -0x13) return;

        if (*(short*)(e + 0x9a) == -0x14)
        {
            SetSceneBackdrop((short)*(int*)(e + 0x50), (short)*(int*)(e + 0x54), (short)*(int*)(e + 0x58), 2);
        }

        short r = (short)(e[0x38] - 0xf);
        byte g = (byte)(e[0x39] - 0xf);
        byte b = (byte)(e[0x3a] - 0xf);

        if (r < 0)
        {
            r = 0;
            g = 0;
            b = 0;
            *(uint*)e &= 0x7fffffff;
        }

        e[0x38] = (byte)r;
        e[0x39] = g;
        e[0x3a] = b;
    }
}
