// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // Écran de composition de groupe de mise au point (Ghidra: FUN_00536270)
    //
    // Chemin A de l'état de jeu 1, atteint uniquement quand
    // <see cref="s_DebugModeLatch_005633a8"/> vaut 0, c'est-à-dire après que le moteur de texte a
    // dessiné la séquence "Gurkha" (voir <see cref="CheatCodeProbe"/>). Ce n'est pas l'écran de
    // création de personnage du jeu vendu : c'est un outil de développeur qui compose un groupe de
    // quatre personnages en deux clics, avec 9000 PV et 200 PM chacun, et qui affiche
    // « BUFFER OVER » en boucle si le chargement des ressources déborde.
    //
    // Toutes les données ci-dessous sont relevées directement dans `.data` de Legacy/Binaries/Blaze.exe
    // (`OpenForesia.Tools -- at`), pas devinées.
    // =====================================================================

    /// <summary>Copie Latin-1 **sans** ajouter de terminateur, pour reproduire un bloc `.data` dont
    /// le remplissage et les octets nuls comptent (contrairement à <see cref="ToAnsiZ"/>).</summary>
    private static byte* ToAnsiBlock(string s)
    {
        byte* p = (byte*)NativeMemory.Alloc((nuint)s.Length);
        for (int i = 0; i < s.Length; i++) p[i] = (byte)s[i];
        return p;
    }

    // Ghidra: 0x0056c894..0x0056c8d3 — les huit noms de classe, 64 octets contigus dans .data
    // (décalage fichier 0x16b094). Reproduits comme un seul bloc plutôt que huit chaînes
    // indépendantes : le remplissage variable (« Elf » n'occupe que 4 octets, « Sorcerer » 12) fait
    // partie de la disposition d'origine, et PTR_s_Fighter_0056c7c0 est un vrai tableau de
    // pointeurs dedans.
    private static readonly byte* s_ClassNameBlock_0056c894 = ToAnsiBlock(
        "Fairy\0\0\0" + "Dwarf\0\0\0" + "Elf\0" + "Hunter\0\0" +
        "Sorcerer\0\0\0\0" + "Rogue\0\0\0" + "Priest\0\0" + "Fighter\0");

    // Ghidra: &PTR_s_Fighter_0056c7c0 — huit pointeurs vers le bloc ci-dessus, dans l'ordre des
    // identifiants de classe (les trois bits bas de l'octet d'identité +0x150).
    private static readonly int[] s_ClassNameOffsets_0056c7c0 =
        [0x38, 0x30, 0x28, 0x1c, 0x14, 0x10, 0x08, 0x00];

    /// <summary>Ghidra: <c>(&amp;PTR_s_Fighter_0056c7c0)[classe]</c>.</summary>
    private static byte* ClassName_0056c7c0(int classIndex)
        => s_ClassNameBlock_0056c894 + s_ClassNameOffsets_0056c7c0[classIndex];

    // Ghidra: 0x0056c8d4..0x0056c96b — 152 octets contigus : seize noms de personnage par défaut de
    // huit octets chacun (index décroissant : 0x56c8d4 = identité 15), puis « BUFFER OVER »,
    // « FEMALE » et « MALE ». Reproduits d'un bloc parce que la copie de nom lit **seize** octets
    // dans des enregistrements de **huit** (voir DebugPartyName_0056c780) : le débordement doit
    // tomber sur les mêmes octets que dans le binaire.
    //
    // L'identité 9 est « Süßi » (Latin-1 : 53 FC DF 69) — les noms sont allemands, ce qui
    // colle avec le studio T&E Soft / l'édition européenne.
    private static readonly byte* s_DebugNameBlock_0056c8d4 = ToAnsiBlock(
        "Dina\0\0\0\0" + "Dora\0\0\0\0" + "Erna\0\0\0\0" + "Tussi\0\0\0" +
        "Nicola\0\0" + "Mimi\0\0\0\0" + "Süßi\0\0\0\0" + "Bella\0\0\0" +
        "Tommi\0\0\0" + "Matze\0\0\0" + "Egon\0\0\0\0" + "Willi\0\0\0" +
        "Werner\0\0" + "Hugo\0\0\0\0" + "Hansi\0\0\0" + "Elo\0" +
        "BUFFER OVER\0" + "FEMALE\0" + "\0" + "MALE\0" + "\0\0\0");

    // Ghidra: &PTR_DAT_0056c780 — seize pointeurs vers les noms ci-dessus. Un index hors de 0..15
    // lèverait ici, là où l'original lirait silencieusement à côté ; tous les écrivains connus de
    // l'octet d'identité le plafonnent à 15 (`(genre << 3) | classe`).
    private static readonly int[] s_DebugPartyNameOffsets_0056c780 =
        [0x78, 0x70, 0x68, 0x60, 0x58, 0x50, 0x48, 0x40,
         0x38, 0x30, 0x28, 0x20, 0x18, 0x10, 0x08, 0x00];

    /// <summary>Ghidra: <c>(&amp;PTR_DAT_0056c780)[identité]</c>.</summary>
    private static byte* DebugPartyName_0056c780(int identity)
        => s_DebugNameBlock_0056c8d4 + s_DebugPartyNameOffsets_0056c780[identity];

    private static byte* s_FemaleLabel_0056c95c => s_DebugNameBlock_0056c8d4 + 0x88;
    private static byte* s_MaleLabel_0056c964 => s_DebugNameBlock_0056c8d4 + 0x90;
    private static byte* s_BufferOverLabel_0056c950 => s_DebugNameBlock_0056c8d4 + 0x7c;

    // Ghidra: 0x00564a2c — quatre octets `3E 00 00 00`, soit la chaîne « > » : le curseur de
    // sélection de cet écran. Un simple caractère, pas un glyphe dédié.
    private static readonly byte* s_CursorGlyph_00564a2c = ToAnsiZ(">");

    // Ghidra: &DAT_0056c7e0 — huit lignes de huit octets, une par classe. Les sept premiers octets
    // sont les statistiques de départ (recopiées à la fois dans +0x280 et +0x2a0, la paire
    // préparé/courant que <see cref="CommitStagedStatBlock"/> fige) ; le huitième est l'élément,
    // qui atterrit dans +0x151.
    private static readonly byte[] s_ClassBaseStats_0056c7e0 =
    [
        0x18, 0x08, 0x08, 0x10, 0x10, 0x08, 0x10, 0x00,   // 0 Fighter
        0x08, 0x10, 0x18, 0x08, 0x08, 0x10, 0x10, 0x06,   // 1 Priest
        0x08, 0x08, 0x08, 0x18, 0x10, 0x08, 0x18, 0x05,   // 2 Rogue
        0x08, 0x10, 0x10, 0x08, 0x08, 0x18, 0x10, 0x04,   // 3 Sorcerer
        0x10, 0x08, 0x08, 0x14, 0x14, 0x08, 0x10, 0x03,   // 4 Hunter
        0x08, 0x14, 0x10, 0x08, 0x08, 0x14, 0x10, 0x01,   // 5 Elf
        0x14, 0x08, 0x08, 0x10, 0x14, 0x08, 0x10, 0x02,   // 6 Dwarf
        0x08, 0x14, 0x14, 0x08, 0x08, 0x10, 0x10, 0x07,   // 7 Fairy
    ];

    // Ghidra: &DAT_0056c820 / &DAT_0056c828 / &DAT_0056c830. **Contenu identique**, octet pour
    // octet, à s_ClassStartIcon_00562ab8 / s_StatBitRemap_00562ac0 / s_StatBitTable_00562ac8, qui
    // servent au même calcul dans FinalizeCharacterStats — le binaire porte les deux jeux de tables
    // à deux adresses différentes. Déclarés séparément plutôt que réutilisés : la traçabilité par
    // adresse Ghidra est ce qui permettra plus tard de dire si c'est une duplication du compilateur
    // ou deux tables qu'un correctif ferait diverger.
    private static readonly byte[] s_ClassStartIcon_0056c820 = [0, 1, 0, 0, 3, 0, 0, 2];
    private static readonly byte[] s_StatBitRemap_0056c828 = [1, 2, 3, 6, 5, 4, 7, 8];
    private static readonly ushort[] s_StatBitTable_0056c830 =
        [1, 0x8000, 0x4000, 0x2000, 0x1000, 0x800, 0x400, 0x200, 0x100, 0, 0, 0];

    // Ghidra: &DAT_0056c848 / &DAT_0056c858 — seize octets chacun, indexés par l'identité complète
    // (genre compris, contrairement aux tables ci-dessus). Ils alimentent +0x153 et +0x152, les deux
    // champs que le sous-état 3 de la création de personnage compose autrement (apparence /
    // style) — voir s_ClassDefault_00562b70.
    private static readonly byte[] s_DebugAppearanceA_0056c848 =
        [0x08, 0x02, 0x00, 0x04, 0x0a, 0x02, 0x04, 0x06, 0x09, 0x03, 0x0f, 0x0d, 0x01, 0x03, 0x05, 0x07];
    private static readonly byte[] s_DebugAppearanceB_0056c858 =
        [0x0c, 0x0f, 0x09, 0x02, 0x0d, 0x0e, 0x04, 0x0f, 0x00, 0x0f, 0x0e, 0x10, 0x0b, 0x0d, 0x04, 0x0f];

    // Ghidra: DAT_00566c84 / DAT_00566c88 / DAT_00566c8c / DAT_00566c90 / DAT_00566c94 — cinq
    // champs espacés de quatre octets. Les valeurs que cette fonction leur pose (0xff, 0xff, 0xff,
    // 1, 1) sont **exactement** leurs valeurs au chargement, relevées dans le binaire : c'est une
    // remise à l'état initial, pas une configuration.
    //
    // Largeur : les trois premiers sont lus ailleurs dans Blaze.c comme des `char`
    // (`DAT_00566c8c != DAT_005de534`, lui-même `(char)param_4`), donc `byte`.
    // TODO(REVERSE) : 0x00566c90 et 0x00566c94 n'ont aucun autre site de lecture dans les
    // 98 000 lignes de Blaze.c ; leur largeur réelle reste à confirmer.
    // `internal` pour LP-009 (src/LegacyPlus/HubAuberge.cs), qui lit la zone et la sous-zone chargées.
    internal static byte s_Unknown_00566c84 = 0xff, s_Unknown_00566c88 = 0xff, s_Unknown_00566c8c = 0xff;
    internal static byte s_Unknown_00566c90 = 1, s_Unknown_00566c94 = 1;

    // Ghidra: DAT_005de53c — recopie d'une entrée de la table d'emplacements
    // (`DAT_005de53c = (&DAT_005e93c8)[i]`, Blaze.c 57361), relue en `uint` plus loin. Octet.
    internal static byte s_Unknown_005de53c;   // `internal` pour LP-009 : le membre qui passe la porte du hub

    /// <summary>
    /// Ghidra: FUN_00536270 @ 0x00536270 — le <b>chemin A</b> de l'état de jeu 1 : l'écran de
    /// composition de groupe de mise au point.
    ///
    /// <para>Deux moitiés bien distinctes. La première est une boucle d'image classique : une
    /// colonne « MALE / FEMALE » et une colonne des huit classes, un curseur « &gt; » sur chacune,
    /// et la liste du groupe déjà composé en bas. Carré ajoute le personnage courant, Croix retire
    /// le dernier, Start valide, Select+Start abandonne vers le menu principal. Le groupe est
    /// écrit directement dans les octets d'identité <c>MainMem+0xf0150 + n*0x2000</c>, les mêmes
    /// que la création de personnage normale remplit.</para>
    ///
    /// <para>La seconde moitié initialise les <b>quatre</b> enregistrements de personnage d'un
    /// coup (voir <see cref="InitialiseDebugPartyRecord"/>) — c'est le pendant de
    /// <see cref="FinalizeCharacterStats"/>, en plus expéditif — puis vérifie que le chargement des
    /// ressources n'a pas dépassé <c>MainMem+0x138000</c> avant de passer à l'état de jeu 2. En cas
    /// de dépassement, l'original affiche « BUFFER OVER » dans une boucle infinie dont il ne sort
    /// jamais ; transcrit tel quel.</para>
    ///
    /// <para>Valider avec un groupe vide n'annule pas : l'écran retombe sur un groupe par défaut
    /// câblé en dur — Guerrier masculin, Prêtre féminin, Voleur masculin, Sorcier féminin.</para>
    /// </summary>
    private static void RunDebugPartySelect()
    {
        // Ghidra: _DAT_005e9898 = 1; _DAT_005e9908 = 1 — le champ `isbg` des deux draw envs, comme
        // en tête de RunMainMenuFrameLoop.
        *(Mem.s_DrawEnv0_005e9880 + 0x18) = 1;
        *(Mem.s_DrawEnv1_005e98f0 + 0x18) = 1;

        uint classIndex = 0;      // Ghidra: uVar8    — 0..7, la classe sous le curseur
        uint gender = 0;          // Ghidra: local_18 — 0 = MALE, 1 = FEMALE

        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);   // Ghidra: FUN_0052a1e0

        int partyCount = 0;       // Ghidra: local_24

        // Le groupe par défaut, posé avant même la première image : Guerrier masculin (0),
        // Prêtre féminin (9 = 8|1), Voleur masculin (2), Sorcier féminin (0xb = 8|3).
        byte* mainMem = GetMainMemPtr();
        mainMem[0xf0150] = 0;
        byte* member = mainMem + 0xf0150;    // Ghidra: local_1c — le curseur d'écriture du groupe
        mainMem[0xf2150] = 9;
        mainMem[0xf4150] = 2;
        mainMem[0xf6150] = 0xb;

        // Ghidra: local_14/local_10/local_c/local_a/local_8/local_6/local_4 — sept locales
        // contiguës dont l'adresse est passée telle quelle à FUN_0051c120, FUN_00521f20 et
        // FUN_0051f9c0. Les trois premiers dwords forment le « curseur de texte » décrit sur
        // DrawTextRun ; les mêmes octets relus comme une primitive sprite donnent la position
        // empaquetée (+8), les deux demi-tailles (+0xc, +0xe) et l'index de texture (+0x10).
        // Alloué une seule fois hors boucle, comme le cadre de pile de l'original.
        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;

        bool partyFull = false;

        while (true)
        {
            // Ghidra: l'expression d'emplacement manette habituelle, mise à l'échelle 0x10 —
            // voir CurrentPadState.
            byte* pad = Res.CurrentPadState();
            ushort pressed = *(ushort*)(pad + 2);     // fronts « vient d'être pressé »

            // SELECT (0x0001) + START (0x0008) ensemble : abandon vers le menu principal. Le garde
            // sur DAT_0056c778 (posé à -1 juste avant par RunGameState1Entry) empêche qu'une
            // manette encore enfoncée en arrivant déclenche la sortie.
            if (((byte)pressed & 9) == 9 && s_Unknown_0056c778 < 0)
            {
                s_GameStateId_005de510 = 0;
                return;
            }

            // START seul : valider le groupe.
            if ((pressed & 8) != 0) break;

            // Ghidra: *(ushort *)(&DAT_00604a24 + iVar9 + 2), soit pad+6 — le masque de répétition
            // automatique que Inp.UpdateButtonEdges entretient, limité aux quatre directions.
            ushort repeat = *(ushort*)(pad + 6);
            uint before = gender * 8 | classIndex;

            // DROITE recule le genre et GAUCHE l'avance — l'inverse de l'écran de création normal
            // (voir CharacterSelectSubState7, qui lit les mêmes bits dans l'autre sens). Transcrit
            // tel quel : c'est un écran de mise au point, personne ne l'a corrigé.
            if ((repeat & 0x20) != 0) gender--;
            if ((repeat & 0x80) != 0) gender++;
            gender &= 1;
            if ((repeat & 0x10) != 0) classIndex--;
            if ((repeat & 0x40) != 0) classIndex++;
            classIndex &= 7;
            if (before != (gender * 8 | classIndex)) Aud.PlayUiSound3A();   // Ghidra: FUN_0053db60

            // CARRÉ (0x8000, donc le bit 0x80 de l'octet pad+3) : ajouter le personnage courant.
            if ((pad[3] & 0x80) != 0)
            {
                Aud.PlayUiSound38();                        // Ghidra: FUN_0053db80
                if (partyCount == 4) { partyFull = true; break; }
                *member = (byte)((gender << 3) | classIndex);
                member += 0x2000;
                partyCount++;
            }

            // CROIX (0x4000) : retirer le dernier.
            if ((pad[3] & 0x40) != 0 && partyCount != 0)
            {
                Aud.PlayUiSound39();                        // Ghidra: FUN_0053dba0
                member -= 0x2000;
                partyCount--;
            }

            *(uint*)(d + 4) = 0x1000000;
            *(short*)(d + 8) = 0x38;
            *(short*)(d + 0xa) = 0x20;
            *(byte**)d = (byte*)s_GlyphAtlas_00566c28;
            primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_MaleLabel_0056c964, 0, 0, -1);
            *(short*)(d + 8) += 0x40;
            primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_FemaleLabel_0056c95c, 0, 0, -1);

            // Le curseur de genre : 0x2c devant MALE (x=0x38), 0x6c devant FEMALE (x=0x78).
            *(short*)(d + 0xa) = 0x20;
            *(short*)(d + 8) = (short)((gender << 6) + 0x2c);
            primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_CursorGlyph_00564a2c, 0, 0, -1);

            // Les huit classes, une par ligne de onze pixels.
            *(short*)(d + 8) = 0x38;
            *(short*)(d + 0xa) = 0x2e;
            *(byte**)d = (byte*)s_GlyphAtlas_00566c28;    // réécrit alors qu'il n'a pas changé
            for (int i = 0; i < 8; i++)
            {
                primCursor = Prims.DrawTextRun(otBase, primCursor, draw, ClassName_0056c7c0(i), 0, 0, -1);
                *(short*)(d + 0xa) += 0xb;
            }
            *(short*)(d + 0xa) = (short)(classIndex * 0xb + 0x2e);
            *(short*)(d + 8) = 0x2c;
            primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_CursorGlyph_00564a2c, 0, 0, -1);

            // Le groupe déjà composé, relu depuis les octets d'identité eux-mêmes.
            *(short*)(d + 0xa) = 0x94;
            if (partyCount != 0)
            {
                byte* entry = GetMainMemPtr() + 0xf0150;
                for (int i = partyCount; i != 0; i--)
                {
                    *(short*)(d + 8) = 0x46;
                    primCursor = Prims.DrawTextRun(otBase, primCursor, draw,
                        ClassName_0056c7c0(*entry & 7), 0, 0, -1);
                    *(short*)(d + 8) += 0x40;
                    primCursor = Prims.DrawTextRun(otBase, primCursor, draw,
                        (*entry & 8) != 0 ? s_FemaleLabel_0056c95c : s_MaleLabel_0056c964, 0, 0, -1);
                    *(short*)(d + 0xa) += 0xb;
                    entry += 0x2000;
                }
            }

            // Le même bloc relu comme une primitive sprite : le fond de l'écran, pris à la première
            // entrée de la table +0x94 du répertoire de ressources (même idiome que le carrousel).
            *(short*)(d + 8) = 0xa0;
            *(short*)(d + 0xa) = 0x78;
            *(short*)(d + 0xc) = 0x1420;
            *(short*)(d + 0xe) = 0x1149;
            *(short*)(d + 0x10) = 0;
            *(uint**)d = **(uint***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            *(uint*)(d + 4) = 0x1000000;
            primCursor = Prims.DrawSpritePrimitive(otEntry, primCursor, draw, 0, 0);   // FUN_00521f20
            primCursor = Prims.DrawFlatPrimitive(otEntry, primCursor, draw);           // FUN_0051f9c0

            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        }

        int partySize;
        if (partyFull)
        {
            // Sortie « groupe déjà plein » : Ghidra pose iVar3 = 4 avant de sauter à LAB_00536694,
            // sans repasser par le groupe par défaut.
            partySize = 4;
        }
        else
        {
            partySize = partyCount;
            if (partyCount == 0)
            {
                // Valider sans avoir rien ajouté ne renvoie pas au menu : on repose le groupe par
                // défaut (identique à celui écrit à l'entrée) et on force la taille à 4.
                mainMem = GetMainMemPtr();
                mainMem[0xf0150] = 0;
                mainMem[0xf2150] = 9;
                mainMem[0xf4150] = 2;
                mainMem[0xf6150] = 0xb;
                s_PartySize_005e9020 = 4;
                partySize = s_PartySize_005e9020;
            }
        }

        s_PartySize_005e9020 = partySize;
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);

        byte* record = GetMainMemPtr() + 0xf0000;
        for (int i = 0; i < 4; i++, record += 0x2000) InitialiseDebugPartyRecord(record);

        RefreshPartyAbilityMask();       // Ghidra: FUN_00535710
        LoadPartyMemberResources();      // Ghidra: FUN_00536ba0

        // Le garde-fou : LoadPartyMemberResources laisse son curseur d'accumulation dans
        // DAT_005de738, et il ne doit pas avoir dépassé MainMem+0x138000.
        if ((int)((nint)GetMainMemPtr() - (nint)s_Unknown_005de738) + 0x138000 >= 0)
        {
            // Ghidra: `*puVar13 = (iVar9 <= iVar3) - 1 & 0x80010000;` — 0x80010000 pour les
            // emplacements couverts par la taille du groupe, 0 au-delà. L'emplacement 0 est un cas
            // à part et vaut toujours 0x80000000.
            byte* slotFlags = Inp.s_SlotFlags_005e93c0;
            for (int i = 0; i < 4; i++, slotFlags += 0x9c)
            {
                *(uint*)slotFlags = i == 0
                    ? 0x80000000u
                    : (partySize <= i ? 0u : 0x80010000u);
            }

            s_Unknown_00566c84 = 0xff;
            s_Unknown_00566c88 = 0xff;
            s_Unknown_00566c8c = 0xff;
            s_Unknown_00566c90 = 1;
            s_Unknown_00566c94 = 1;
            s_Unknown_005de53c = 0;
            s_GameStateId_005de510 = 2;
            return;
        }

        // Dépassement : l'original reste ici pour toujours, à réafficher « BUFFER OVER » image après
        // image. Aucune sortie, aucun test — transcrit tel quel, comme la boucle d'attente de
        // LoadResourceEntry quand le CD manque.
        while (true)
        {
            *(short*)(d + 8) = 0x38;
            *(short*)(d + 0xa) = 0x20;
            *(byte**)d = (byte*)s_GlyphAtlas_00566c28;
            *(uint*)(d + 4) = 0x1000000;
            primCursor = Prims.DrawTextRun(otBase, primCursor, draw, s_BufferOverLabel_0056c950, 0, 0, -1);
            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        }
    }

    /// <summary>
    /// Ghidra: le corps de la boucle `local_18 = 4` de FUN_00536270 — l'initialisation d'un des
    /// quatre enregistrements de personnage de 0x2000 octets, à partir du seul octet d'identité
    /// déjà posé en <c>+0x150</c>.
    ///
    /// <para>C'est le pendant « développeur » de <see cref="FinalizeCharacterStats"/> : mêmes
    /// champs, mêmes cinq fonctions de calcul en fin de parcours, mais les statistiques viennent
    /// d'une table par classe au lieu de la répartition de points, et les points de vie et de magie
    /// sont écrasés par 9000 / 200.</para>
    /// </summary>
    private static void InitialiseDebugPartyRecord(byte* record)
    {
        *(uint*)record = 0;

        for (int i = 0; i < 8; i++)
        {
            *(ushort*)(record + 0x1fc0 + i * 2) =
                s_StatBitTable_0056c830[s_StatBitRemap_0056c828[i]];
        }

        // Le nom par défaut. Seize octets copiés depuis un enregistrement qui n'en fait que huit :
        // le débordement est dans l'original, et il est inoffensif puisque tout affichage s'arrête
        // au premier octet nul. s_DebugNameBlock_0056c8d4 reproduit les octets qui suivent.
        byte* name = DebugPartyName_0056c780(record[0x150]);
        for (int i = 0; i < 0x10; i++) record[0x100 + i] = name[i];

        for (int i = 0; i < 7; i++)
        {
            ushort value = s_ClassBaseStats_0056c7e0[(record[0x150] & 7) * 8 + i];
            *(ushort*)(record + 0x280 + i * 2) = value;
            *(ushort*)(record + 0x2a0 + i * 2) = value;
        }

        byte identity = record[0x150];
        record[0x151] = s_ClassBaseStats_0056c7e0[(identity & 7) * 8 + 7];   // l'élément
        record[0x153] = s_DebugAppearanceA_0056c848[identity];
        record[0x152] = s_DebugAppearanceB_0056c858[identity];

        // Les PV/PM de départ de la classe, **immédiatement écrasés** par les six écritures qui
        // suivent. Ce n'est pas une erreur de lecture : l'original appelle bien les deux, et c'est
        // précisément cette paire (valeur par classe, puis 9000/200) qui a permis d'établir que
        // +0x148/+0x14a sont des PV et des PM.
        SetClassBaseHpMp(record);
        *(ushort*)(record + 0x148) = 9000;
        *(ushort*)(record + 0x14c) = 9000;
        *(ushort*)(record + 0x14a) = 200;
        *(ushort*)(record + 0x14e) = 200;
        *(ushort*)(record + 0x144) = 1;
        *(ushort*)(record + 0x146) = 1;

        byte classIndex = (byte)(record[0x150] & 7);
        *(uint*)(record + 0x26c) = 0;
        *(uint*)(record + 0x140) = 0;
        *(uint*)(record + 0x114) = (uint)(1 << (record[0x151] & 0x1f)) | 0x800;
        record[0x2b0] = 0;
        record[0x2b1] = 1;
        *(short*)(record + 0x112) = (short)(1 << classIndex);
        record[0x2b2] = (byte)(((record[0x150] & 7) == 0 || classIndex == 6) ? 2 : 0xff);
        record[0x2b3] = 0xff;
        record[0x2b4] = 0xff;
        record[0x2b5] = s_ClassStartIcon_0056c820[record[0x150] & 7];
        record[0x2b6] = 0;
        record[0x2b7] = 0;
        record[0x2b8] = 0;
        record[0x2b9] = 0;
        record[0x2ba] = 0;
        record[0x2bb] = 0;
        record[0x2be] = 0;
        record[0x2bf] = 0;
        record[0x2bd] = 0;   // 0x2bc n'est pas écrit — comme dans l'original

        // Dix octets isolés, un tous les 0x280 à partir de +0x300 : intégralement recouverts par la
        // copie de portrait juste en dessous. Mort dans l'original, conservé tel quel.
        for (int i = 0; i < 0x1900; i += 0x280) record[0x300 + i] = 0;

        // Le portrait. Contrairement à FinalizeCharacterStats, qui le prend dans une zone déjà
        // chargée de MainMem, ce chemin va le relire depuis BLAZE.ALL via l'entrée +0xb0 du
        // répertoire de ressources, indexée par la classe.
        byte* scratch = GetMainMemPtr() + 0x1b0000;
        int* entry = *(int**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0xb0) + classIndex * 4);
        Res.LoadResourceEntry(entry, scratch);                             // Ghidra: FUN_00529b50
        Mem.CopyDwords((uint*)scratch, (uint*)(record + 0x300), 0x640);    // Ghidra: FUN_005352e0

        // Les sorts et les herbes du mode de mise au point — pas de l'équipement (relu le
        // 2026-09-25, Documentation/Reference/Sorts.md) : +0x160, +0x168 et +0x170 sont les masques
        // de sorts connus des écoles 0, 1 et 2, huit octets chacun ; leurs quatre premiers octets à
        // 0xff font connaître les 32 premiers sorts de chaque école, là où le jeu normal ne pose que
        // le bit 0. +0x178..+0x17e sont les sept compteurs d'herbes du Chasseur (école 3, que
        // StoreItemInInventory_005312D0 incrémente), remplis de 11, 22, 33… jusqu'à 77.
        record[0x160] = 0xff; record[0x161] = 0xff; record[0x162] = 0xff; record[0x163] = 0xff;
        record[0x168] = 0xff; record[0x169] = 0xff; record[0x16a] = 0xff; record[0x16b] = 0xff;
        record[0x170] = 0xff; record[0x171] = 0xff; record[0x172] = 0xff; record[0x173] = 0xff;
        record[0x178] = 0x0b; record[0x179] = 0x16; record[0x17a] = 0x21; record[0x17b] = 0x2c;
        record[0x17c] = 0x37; record[0x17d] = 0x42; record[0x17e] = 0x4d;

        for (int i = 0; i < 0x1900; i += 0x280) record[0x313 + i] |= 0x80;

        CommitStagedStatBlock(record);              // Ghidra: FUN_00538ab0
        ScaleStatBlockByDifficulty(record + 0x100); // Ghidra: FUN_00538f50
        ComputeDerivedStats(record);                // Ghidra: FUN_00538c50
        ApplyEquipmentStatBonuses(record);          // Ghidra: FUN_00538e20
        FinalizeEquipmentEffects(record + 0x100);   // Ghidra: FUN_00538fa0

        NativeMemory.Clear(record + 0x2e0, 0x20);
        record[0x2fe] = 0xff;

        // Huit banques de 0x40 octets contiguës (0x1d00..0x1eff) — le compilateur a déroulé huit
        // effacements séparés, exactement comme dans FinalizeCharacterStats.
        for (int bank = 0; bank < 8; bank++) NativeMemory.Clear(record + 0x1d00 + bank * 0x40, 0x40);

        NativeMemory.Clear(record + 0x180, 0x80);
        *(uint*)(record + 0x158) = 0;
    }

    // Ghidra: DAT_005e92b0 â€” per-game-state value read from the loaded resource file's own table
    // at offset 0x48; read again by not-yet-reconstructed code (e.g. FUN_00542160).
    internal static int s_Unknown_005e92b0;

    // Ghidra: &DAT_005e9680 â€” 4 pointeurs de sortie, un par emplacement de groupe, remplis par
    // RelocateAnimationBundle. Jamais lu dans FUN_00536ba0 lui-mÃªme (seule son ADRESSE sert de borne
    // de boucle) â€” Ã©crit uniquement.
    private static byte* s_PartyAnimationResult_005e9680 => Mem.Arena(0x005e9680);

    // Ghidra: &DAT_005e9800 â€” 4 pointeurs de sortie, un par emplacement de groupe, remplis par
    // RelocateModelBundle.
    private static byte* s_PartyModelResult_005e9800 => Mem.Arena(0x005e9800);

    // Ghidra: DAT_005de738 â€” le curseur d'accumulation final (Ghidra: EBP) aprÃ¨s le chargement des
    // quatre emplacements ; probablement lu par FUN_00535500 ou par un code de rendu pas encore
    // atteint.
    private static byte* s_Unknown_005de738;

    /// <summary>
    /// Ghidra: FUN_00536ba0 @ 0x00536ba0 â€” charge et relocalise les ressources des quatre
    /// emplacements du groupe pour le rendu de l'Ã©tat de jeu 1 : deux textures, un paquet de modÃ¨le
    /// et un paquet d'animation par emplacement, indexÃ©s par l'identitÃ© de personnage que
    /// <c>Mem.s_SelectScratch_00573338[0x150 + i*0x2000]</c> stocke pour cet emplacement (le mÃªme champ
    /// que le code voisin de <c>StepSubState5Phase</c> lit dÃ©jÃ  comme Â« personnage courant Â»).
    /// <para>
    /// ReconstruitÂ depuis le dÃ©sassemblage brut (<c>Legacy/Raw/Blaze/FUN_00536ba0.asm</c>), pas le
    /// pseudo-code Ghidra : plusieurs locales <c>undefined4</c> y cachent des pointeurs, et au moins
    /// un nom de variable du pseudo-code ne correspond pas Ã  ce que montrent les vrais dÃ©calages de
    /// pile. Deux piÃ¨ges rÃ©currents, rÃ©solus en calculant le delta de pile instruction par
    /// instruction :
    /// </para>
    /// <list type="bullet">
    /// <item>Le nettoyage de pile aprÃ¨s un appel <b>cdecl</b> est parfois diffÃ©rÃ© de plusieurs
    /// instructions â€” ex. l'appel Ã  <see cref="DecodeResourceChunk"/> (asm 0x536c9c) : son
    /// <c>ADD ESP,0xc</c> n'arrive qu'Ã  0x536cba, cinq instructions plus loin. Toute lecture
    /// <c>[ESP+n]</c> dans cette fenÃªtre pointe donc plus loin que ne le suggÃ¨re le dÃ©calage
    /// post-prologue.</item>
    /// <item>Les deux thunks winsys3d appelÃ©s directement ici (<c>CALL 0x545c50</c> /
    /// <c>CALL 0x545c4a</c>, qui correspondent Ã  <c>Ordinal_677</c>/<c>Ordinal_662</c> dans la passe
    /// d'analyse complÃ¨te de <c>Blaze.c</c>, ligne ~82714) sont <b>stdcall</b> : l'appelÃ© dÃ©pile
    /// lui-mÃªme ses arguments, donc aucun <c>ADD ESP</c> explicite ne suit et le delta de pile revient
    /// silencieusement Ã  zÃ©ro.</item>
    /// </list>
    /// <para>
    /// <c>EBP</c> dÃ©marre Ã  <c>MainMem + 0xf8000</c> â€” la mÃªme base que la liste Â« blocs bruts Â» de
    /// <see cref="LoadCharacterSelectResources"/> â€” et s'accumule exactement comme le <c>cursor</c>
    /// de cette fonction sÅ“ur : le paquet de modÃ¨le puis le paquet d'animation de chaque emplacement
    /// s'empilent cÃ´te Ã  cÃ´te dans cette rÃ©gion ; un emplacement dupliquÃ© ne consomme aucun espace et
    /// laisse le curseur intact pour le suivant.
    /// </para>
    /// <para>
    /// TODO(REVERSE) : <see cref="s_PartySize_005e9020"/> n'est PAS utilisÃ© ici â€” la boucle est
    /// bornÃ©e en dur sur les 4 dwords de <c>DAT_005e9680</c>, indÃ©pendamment de la taille rÃ©elle du
    /// groupe. RÃ´le exact des bits du dword de <see cref="s_SlotFlags_005e93c0"/>
    /// (<c>0xefffdff9</c>/<c>0x81808a00</c>) non Ã©tabli. Absence de garde
    /// <c>ClutData != null</c>/<c>PixelData != null</c> pour la texture 2 (contrairement Ã
    /// <see cref="DecodeResourceChunk"/>) confirmÃ©e dans l'asm, pas une omission de ce portage.
    /// </para>
    /// </summary>
    private static void LoadPartyMemberResources()
    {
        byte* mainMem = GetMainMemPtr();
        byte* directoryBase = Mem.s_MainResourceDirectory_005e97c4;
        if (mainMem == null || directoryBase == null) return;

        byte* modelAnimCursor = mainMem + 0xf8000;   // Ghidra: EBP

        for (int i = 0; i < 4; i++)
        {
            byte* charBlock = mainMem + 0xf02bd + i * 0x2000;      // Ghidra: pcVar2
            byte* record = Inp.s_SlotFlags_005e93c0 + i * 0x9c;        // Ghidra: &DAT_005e93c0 + i*0x9c
            byte* recordTail = record + 4;                          // Ghidra: EDI ("&DAT_005e93c4 + i*0x9c")

            // Ghidra: uVar8 = pcVar2[-0x16d] == Mem.s_SelectScratch_00573338[0x150 + i*0x2000].
            byte characterId = charBlock[-0x16d];

            // Ghidra: le balayage des emplacements dÃ©jÃ  traitÃ©s (j < i) portant la mÃªme identitÃ©.
            int duplicateOf = -1;
            for (int j = 0; j < i; j++)
            {
                if ((mainMem + 0xf0000 + 0x150 + j * 0x2000)[0] == characterId) { duplicateOf = j; break; }
            }

            if (duplicateOf != -1)
            {
                recordTail[0] = (byte)duplicateOf;
                continue;
            }

            // Ghidra: *(EDI-4) = (*(EDI-4) & 0xefffdff9) | 0x81808a00 â€” drapeaux d'Ã©tat, rÃ´le des
            // bits non Ã©tabli.
            uint* recordFlags = (uint*)record;
            *recordFlags = (*recordFlags & 0xefffdff9u) | 0x81808a00u;
            recordTail[7] = charBlock[0];   // Ghidra: EDI[7] = *pcVar2  (record+4+7 = record+0xB)

            // ---- texture 1 : LoadResourceEntry + DecodeResourceChunk, table +0x84, pas 8 octets ----
            // **Un déréférencement, pas une adresse inline** : `0x00536c42 MOV ESI,[EAX+0x84]` puis
            // `MOV EAX,[ESI + EDI*1]` (Legacy/Raw/Blaze/FUN_00536ba0.asm), et la pseudo-C dit la
            // même chose (`*(int *)(DAT_005e97c4 + 0x84) + iVar9`). Ce portage lisait la table
            // **à** +0x84 au lieu de suivre le pointeur qui s'y trouve ; corrigé le 2026-08-17,
            // après que RunDebugPartySelect a rendu cette fonction atteignable pour la première
            // fois et qu'elle a fauté ici sur-le-champ. Comparer avec le paquet de modèle plus bas,
            // qui déréférence bien `*(int*)(directoryBase + 0x2c)`.
            int* texDir1Entry = (int*)(*(int*)(directoryBase + 0x84) + characterId * 8);
            uint texMeta1 = (uint)texDir1Entry[1];
            byte* meta1 = stackalloc byte[4];
            meta1[0] = (byte)texMeta1;
            meta1[1] = (byte)((byte)(texMeta1 >> 8) + i * 4);
            meta1[2] = (byte)i;

            Res.LoadResourceEntry((int*)(nint)texDir1Entry[0], mainMem + 0x1b0000);
            Res.DecodeResourceChunk(mainMem + 0x1b0000,
                (ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0x80 + i * 4), meta1);

            // ---- texture 2 : LoadResourceEntry + OpenTim/ReadTim manuel (pas DecodeResourceChunk â€”
            // le rectangle CLUT est une bande fixe 64x1 par emplacement le long de X, pas une taille
            // calculÃ©e). Table +0x84, dÃ©calÃ©e de 0x80 octets = 16 entrÃ©es aprÃ¨s la texture 1.
            int* texDir2Entry = (int*)((byte*)texDir1Entry + 0x80);
            uint texMeta2 = (uint)texDir2Entry[1];
            byte meta2_0 = (byte)texMeta2;
            byte meta2_1 = (byte)((byte)(texMeta2 >> 8) + 0x10);
            const byte meta2_2 = 3;

            Res.LoadResourceEntry((int*)(nint)texDir2Entry[0], mainMem + 0x1b0000);

            Resources.TimImage tim;
            Winsys3d.OpenTim(mainMem + 0x1b0000);
            Winsys3d.ReadTim(&tim);

            short* clutRect = tim.ClutRect;
            clutRect[0] = (short)(i * 0x40);
            clutRect[1] = (short)(meta2_1 + 0x1c0);
            clutRect[2] = 0x40;
            clutRect[3] = 1;
            Winsys3d.Ordinal_674(clutRect, tim.ClutData);

            short* pixelRect = tim.PixelRect;
            pixelRect[0] = (short)(((meta2_0 & 0xf) << 6) + (i << 4));
            pixelRect[1] = (short)(((meta2_0 & 0x10) + meta2_2 * 4) << 4);
            Winsys3d.Ordinal_674(pixelRect, tim.PixelData);

            // Ghidra 0x536d91-dc7 â€” mÃªme formule de tpage que DecodeResourceChunk.
            int x = pixelRect[0];
            int y = pixelRect[1];
            int tpage = ((x >> 2) & 0xf0) | (y & 0x100);
            tpage |= 0x2000;
            tpage = (short)tpage >> 4;
            tpage |= ((int)tim.Mode & 3) << 7;
            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0x90 + i * 4) = (ushort)tpage;
            *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0x92 + i * 4) = (ushort)((meta2_1 + 0x1c0) << 6);

            // ---- paquet de modÃ¨le : LoadResourceEntry (dans la rÃ©gion partagÃ©e et cumulative
            // modelAnimCursor) + RelocateModelBundle, table +0x2c, pas 8 octets ----
            int* modelEntry = (int*)(*(int*)(directoryBase + 0x2c) + characterId * 8);
            int modelResPtr = modelEntry[0];
            uint pageBits = (uint)modelEntry[1];
            byte pb1 = (byte)(i * 0x40);                              // Ã©crasÃ©, pas additionnÃ©
            byte pb2 = (byte)((byte)(pageBits >> 16) + i);            // += i
            uint adjustedPageBits = (pageBits & 0xff0000ffu) | ((uint)pb1 << 8) | ((uint)pb2 << 16);
            byte* modelMeta = stackalloc byte[8];
            *(uint*)(modelMeta + 4) = adjustedPageBits;

            byte* afterModel = Res.LoadResourceEntry((int*)(nint)modelResPtr, modelAnimCursor);
            RelocateModelBundle((int*)modelAnimCursor, modelMeta, (byte**)s_PartyModelResult_005e9800 + i);

            // ---- paquet d'animation : LoadResourceEntry (juste aprÃ¨s le modÃ¨le) +
            // RelocateAnimationBundle, table +0x7c, pas 4 octets (indexÃ©e par l'identitÃ©, pas par
            // l'emplacement) ----
            int animResPtr = (*(int**)(directoryBase + 0x7c))[characterId];
            byte* afterAnim = Res.LoadResourceEntry((int*)(nint)animResPtr, afterModel);
            RelocateAnimationBundle((int*)afterModel, (byte**)s_PartyAnimationResult_005e9680 + i);

            modelAnimCursor = afterAnim;
            recordTail[0] = (byte)i;
        }

        s_Unknown_005de738 = modelAnimCursor;
        UploadPartyIconTextures();   // Ghidra: FUN_00535500
    }

    /// <summary>
    /// Ghidra: FUN_00535500 @ 0x00535500 — téléverse en VRAM les dix vignettes de chacun des quatre
    /// emplacements du groupe : une palette de seize couleurs et une image de 24x24 en 4 bits par
    /// pixel chacune. Appelée en toute fin de <see cref="LoadPartyMemberResources"/>.
    ///
    /// <para><b>Reconstruite depuis le désassemblage brut</b>
    /// (<c>Legacy/Raw/Blaze/FUN_00535500.asm</c>), et il le fallait : la pseudo-C passe
    /// <c>&amp;local_c</c> au premier <c>Ordinal_674</c> alors que les quatre champs du rectangle
    /// sont écrits dans <c>local_8</c>/<c>local_6</c>/<c>local_4</c>/<c>local_2</c>, et elle donne
    /// au second appel un <c>&amp;stack0xffffffe8</c> que rien ne remplit. Deux artefacts, une seule
    /// cause : le <c>PUSH 0x0</c> de l'appel à <see cref="Ordinal_667"/> est émis <b>avant</b> les
    /// quatre écritures, si bien que les <c>[ESP+0x18..0x1e]</c> de l'assembleur et le
    /// <c>LEA EAX,[ESP+0x14]</c> qui suit l'appel désignent la même structure de quatre mots. C'est
    /// exactement le piège déjà rencontré dans <c>FUN_005126c0</c>. Les deux rectangles réutilisent
    /// d'ailleurs le <b>même</b> emplacement de pile, ce que la pseudo-C perd complètement.</para>
    ///
    /// <para>La disposition d'un sous-bloc se déduit alors entièrement, et elle boucle : les dix
    /// sous-blocs de 0x280 octets qui composent la région <c>+0x300</c> de l'enregistrement (0x640
    /// dwords, celle que <see cref="InitialiseDebugPartyRecord"/> et
    /// <see cref="FinalizeCharacterStats"/> remplissent depuis une ressource par classe) se lisent
    /// <c>0x140</c> d'en-tête + <c>0x20</c> de palette + <c>0x120</c> de pixels = <c>0x280</c>,
    /// sans reste.</para>
    ///
    /// <para>Le couple posé à la fin se recalcule et tombe juste, ce qui confirme tout le reste :
    /// <c>DAT_005e8fa0</c> est l'entrée 0x28 de <see cref="s_EntityDecodeTable_005e8f00"/>, et
    /// <c>0x7c40</c> est bien l'identifiant de palette de l'emplacement 0 (<c>(0x1f1 &lt;&lt; 6) | (0 &gt;&gt; 4)</c>),
    /// tandis que <c>0x216</c> porte la page de texture de la zone pixels
    /// (<c>(0x180 &gt;&gt; 6) | ((0x100 &amp; 0x100) &gt;&gt; 4)</c> = 0x16).</para>
    ///
    /// <para>TODO(REVERSE) : ce que représentent les dix vignettes par personnage (objets équipés ?
    /// sorts ?) n'est pas établi — seul leur format l'est.</para>
    /// </summary>
    private static void UploadPartyIconTextures()
    {
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return;

        // Ghidra: ESI démarre à MainMem + 0xf0440, soit l'enregistrement + 0x440 : la palette du
        // premier sous-bloc (0x300 + 0x140).
        byte* slotBase = mainMem + 0xf0440;

        // Le rectangle de quatre mots que les deux téléversements se partagent.
        short* rect = stackalloc short[4];

        for (int slot = 0; slot < 4; slot++, slotBase += 0x2000)
        {
            byte* row = slotBase;
            for (int icon = 0; icon < 10; icon++, row += 0x280)
            {
                // L'octet de tête du sous-bloc (row - 0x140 = enregistrement + 0x300 + icon*0x280).
                if (row[-0x140] == 0) continue;

                // La palette : seize couleurs sur une ligne, rangée par emplacement dans la bande
                // de CLUT en bas de VRAM.
                rect[0] = (short)(icon * 16);        // x
                rect[1] = (short)(0x1f1 + slot);     // y
                rect[2] = 0x10;                      // largeur
                rect[3] = 1;                         // hauteur
                Winsys3d.Ordinal_667(0);
                Winsys3d.Ordinal_674(rect, row);

                // Les pixels : 6 unités de 16 bits de large (donc 24 pixels en 4 bits) sur 24
                // lignes, les emplacements empilés verticalement et les vignettes côte à côte.
                rect[0] = (short)(0x180 + icon * 6);   // x
                rect[1] = (short)(0x100 + slot * 0x18);// y
                rect[2] = 6;                           // largeur, en unités de 16 bits
                rect[3] = 0x18;                        // hauteur
                Winsys3d.Ordinal_667(0);
                Winsys3d.Ordinal_674(rect, row + 0x20);
            }
        }

        // Ghidra: DAT_005e8fa0 / DAT_005e8fa2 — l'entrée 0x28 de la table de décodage : la page de
        // texture et la palette que le rendu de ces vignettes ira chercher.
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xa0) = 0x216;
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xa2) = 0x7c40;
    }
}
