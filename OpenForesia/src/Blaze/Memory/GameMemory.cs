// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// GameMemory : le modele du segment .data de Blaze.exe.
//
// UN SEUL objet, parce que le binaire a UN SEUL segment .data. Ce n'est pas un pis-aller : les
// huit arenes, les tables recouvrees et les globales isolees sont lues et ecrites par tous les
// sous-systemes. Leur donner un proprietaire par sous-systeme modeliserait un decoupage que le
// binaire n'a pas. Mesure au moment de l'extraction : ~870 references croisees entre fichiers,
// et aucun fichier ferme sur son etat.
//
// Ce que cette classe apporte par rapport aux 640 champs statiques d'avant : une SIGNATURE. Un
// sous-systeme qui prend GameMemory en parametre declare qu'il touche a .data ; avant, il le
// faisait en silence.
//
// Regle : une globale n'entre ici que si elle modelise .data. Le comportement va ailleurs -
// RunMainLoop et LoadUiFontFromCd ont ete sortis vers Platform/Startup.cs pour cette raison.
//
// Voir ARCHITECTURE.md (racine) pour la regle d'etat, et la carte des symboles reconstruits en
// fin de Documentation/DataSymbols.md pour le controle d'adjacence.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

internal sealed unsafe class GameMemory
{
    /// <summary>
    /// Les trois tables .data qui se construisent par appel sont montees ici, et non en
    /// initialiseur de champ : un initialiseur ne peut pas appeler un membre d'instance, et en
    /// faire des proprietes => reconstruirait la table a chaque acces - un changement de
    /// comportement, pas un changement de forme.
    /// </summary>
    internal GameMemory()
    {
        s_SelectSubStateBytes_00562a98 = BuildSelectSubStateBytes();
        s_Unknown_005606f8             = BuildLabelDescriptor005606f8();
        s_SceneToneTable_00562a58      = BuildSceneToneTable();
    }

    // =====================================================================
    // Fusionne depuis Memory/Arena.GameState.cs le 2026-08-19.
    // =====================================================================
    // =====================================================================
    // L'arène 0x005e9000 – 0x005e9a00
    //
    // Dans le binaire c'est **une seule région contiguë** de globales. Ce portage l'avait découpée
    // en une dizaine d'allocations indépendantes, ce qui a produit à répétition la même famille de
    // défauts : deux symboles voisins déclarés séparément ne partagent plus leur mémoire, et une
    // écriture faite par un bout n'est jamais lue par l'autre. Quatorze champs corrigés ainsi le
    // 2026-08-16 et le 2026-08-17, plus les trois recouvrements que le regroupement lui-même met au
    // jour (voir plus bas). Un seul bloc, et chaque symbole devient un décalage dedans.
    //
    // **La disposition n'est pas choisie, elle est déduite** : chaque symbole s'arrête là où
    // commence son voisin d'adresse. Le recoupement décisif vient des huit banques de 0x40 octets
    // que `FUN_00536ea0` restaure depuis une sauvegarde — 0x5e9060, 0x5e92c0, 0x5e9360, 0x5e96e0,
    // 0x5e9720, 0x5e9780, 0x5e9840, 0x5e9980. Sept d'entre elles butent exactement sur un symbole
    // déjà connu ; la huitième (0x5e96e0) ne tombait juste qu'en ramenant les tables d'actifs de 24
    // à 16 entrées, ce qui du même coup fait tenir `s_PartyAnimationResult_005e9680` dans
    // l'intervalle où il doit être. Trois bornes fausses tombent ensemble, ce qui vaut confirmation.
    //
    //   +0x000  0x5e9000  s_PartyAbilityMask      0x060   (0x100 déclarés, donc débordait sur le suivant)
    //   +0x060  0x5e9060  s_PanelSelection        0x120   (banque 0 sur ses 0x40 premiers octets)
    //   +0x180  0x5e9180  s_SlotArea              0x4c0   (banques à +0x2c0 et +0x360 ; drapeaux à +0x240)
    //   +0x640  0x5e9640  s_AnimationTable        0x040   (16 pointeurs, pas 24)
    //   +0x680  0x5e9680  s_PartyAnimationResult  0x020
    //   +0x6a0  0x5e96a0  s_ModelTable            0x040   (16 pointeurs, pas 24)
    //   +0x6e0  0x5e96e0  banque
    //   +0x720  0x5e9720  banque
    //   +0x780  0x5e9780  banque
    //   +0x7c0  0x5e97c0  scalaires (tampon et répertoire de ressources, pas d'entrelacs)
    //   +0x800  0x5e9800  s_PartyModelResult      0x010
    //   +0x840  0x5e9840  banque
    //   +0x880  0x5e9880  s_EnvBlock              0x0e0
    //   +0x960  0x5e9960  s_FadeCounter
    //   +0x980  0x5e9980  banque
    //
    // Les scalaires isolés de la région (0x5e97c0..0x5e9824, 0x5e9960) restent pour l'instant des
    // champs C# : aucune banque ni aucun bloc ne les recouvre, donc ils ne peuvent pas produire le
    // défaut ci-dessus. TODO(REVERSE) : les ramener dans l'arène quand un site les lira par adresse
    // plutôt que par nom.
    internal const int GameArenaBase = 0x005e9000;
    // Etendue de 0x0a00 a 0x1600 le 2026-08-18 : FUN_00516350 atteint 0x005e9b87 et y efface 0x40
    // dwords par pas de 0x28, soit jusqu'a 0x005ea587.
    internal const int GameArenaSize = 0x1600;
    internal readonly byte* s_GameArena_005e9000 = (byte*)NativeMemory.AllocZeroed(GameArenaSize);

    /// <summary>Le symbole d'adresse Ghidra <paramref name="va"/> dans l'arène.</summary>
    // Vue d'arene, rapatriee depuis Engine/UiWindow.cs : 0x005e9060 est dans l'etendue de
    // GameArena, et ses 0x40 premiers octets sont aussi la banque 2 (s_StateBank2_005e9060).
    internal byte* s_PanelSelection_005e9060 => Arena(0x005e9060);

    internal byte* Arena(int va) => s_GameArena_005e9000 + (va - GameArenaBase);

    // Les huit banques de 0x40 octets, dans l'ordre où le bloc de restauration de FUN_00536ea0 les
    // recopie depuis la sauvegarde (source : enregistrement +0x1d00..+0x1eff, huit banques
    // contiguës là-bas, dispersées ici).
    internal byte* s_StateBank0_005e96e0 => Arena(0x005e96e0);
    internal byte* s_StateBank1_005e9780 => Arena(0x005e9780);
    internal byte* s_StateBank2_005e9060 => Arena(0x005e9060);
    internal byte* s_StateBank3_005e9840 => Arena(0x005e9840);
    internal byte* s_StateBank4_005e9720 => Arena(0x005e9720);
    internal byte* s_StateBank5_005e92c0 => Arena(0x005e92c0);
    internal byte* s_StateBank6_005e9980 => Arena(0x005e9980);
    internal byte* s_StateBank7_005e9360 => Arena(0x005e9360);

    internal const int EnvBlockSize = 224;   // 0x005e9960 - 0x005e9880
    internal byte* s_EnvBlock_005e9880 => Arena(0x005e9880);

    internal byte* s_DrawEnv0_005e9880 => s_EnvBlock_005e9880 + 0x00;
    internal byte* s_DispEnv0_005e98dc => s_EnvBlock_005e9880 + 0x5C;
    internal byte* s_DrawEnv1_005e98f0 => s_EnvBlock_005e9880 + 0x70;
    internal byte* s_DispEnv1_005e994c => s_EnvBlock_005e9880 + 0xCC;

    // Ghidra: DAT_005633a8 â€” **le verrou du mode de mise au point**, résolu le 2026-08-17.
    //
    // Valeur au chargement relevée dans le binaire (`at Legacy/Blaze.exe 0x005633a8`, .data) : **1**.
    // Largeur confirmée par le code machine : `0x00536218  39 1D A8 33 56 00  CMP dword [0x5633a8],EBX`
    // â€” un dword, donc bien un `int`.
    //
    // Le seul écrivain de tout le binaire est <see cref="CheatCodeProbe"/> (FUN_0051c0e0), qui le
    // met à **0** une fois la séquence "Gurkha" entièrement filée. Les trois lecteurs testent
    // tous `== 0`, et chacun active alors un comportement de développeur :
    // <list type="bullet">
    // <item><see cref="InitViewportSurfaces"/> : damier de mise au point dans le tampon de rendu
    // au lieu d'un effacement en noir ;</item>
    // <item>le pilotage libre de la caméra (voir <c>StepSubState7Phase</c>) ;</item>
    // <item><see cref="RunGameState1Entry"/> : bascule vers <see cref="RunDebugPartySelect"/>
    // (FUN_00536270), l'écran de composition de groupe de mise au point, au lieu du chemin
    // normal FUN_00536ea0.</item>
    // </list>
    // Donc 0 = mise au point active, 1 = jeu normal. Initialisé à 1 comme dans .data : les trois
    // lecteurs prenaient jusqu'ici la branche « développeur » par le seul effet du zéro par défaut
    // de C#.
    //
    // **Fusionné avec l'ancien `s_Unknown_005633a8`**, qui déclarait la même adresse une seconde
    // fois (celui-là recevait l'écriture de CheatCodeProbe, celui-ci portait les lectures : la
    // même classe de défaut d'aliasing que les six champs corrigés le 2026-08-16).
    internal int s_DebugModeLatch_005633a8 = 1;

    // Ghidra: DAT_005631f8 â€” the game's **data root** on the CD. Recovered from Legacy/Blaze.exe at
    // VA 0x005631f8: the bytes are "@:\FRENCH\", NOT the bare drive root this was previously assumed
    // to be. See Documentation/DataSymbols.md.
    //
    // '@' (0x40, 'A'-1) is the drive-letter placeholder; RunApplication's scan writes the detected
    // letter into index 0 and resets it to '@' when nothing matches. Only index 0 is ever patched â€”
    // the language directory is static data, which is exactly why Blaze.c's text (code only, no
    // .data) showed a single-byte store and gave no hint that the rest of the string existed.
    //
    // This is a distinct buffer from the CD *presence marker* path 16 bytes earlier
    // (s____Blaze_Blade_005631e8 = "@:\Blaze&Blade"). Conflating the two is what made every
    // CD-relative path resolve one directory too high and find nothing.
    internal readonly byte* s_CdDataRootPath = ToAnsiZ("@:\\FRENCH\\");

    // Ghidra: DAT_0056321c â€” the CD-check file's size, plus a fixed 0xd8000000 added once right
    // before RunMainLoop. Read back by Handler_00544810 (UI text control code 0x01), where
    // `(DAT_0056321c + 1)` multiplies a returned text-cursor pointer â€” see the TODO(REVERSE) there.
    internal uint s_CdCheckFileSize_0056321c;

    internal uint s_RandSeed_0056e9c0; // Ghidra: DAT_0056e9c0 â€” the CRT rand seed
    internal uint s_Unknown_005e9630;
    internal uint s_Unknown_005e97f0;
    internal uint s_ScreenStride_005e97e0;
    // Ghidra: DAT_005e97c0 â€” renamed from an earlier "screen/back-buffer base" guess once
    // FUN_0052a030 (LoadFirstLevelResources) showed it's really the destination buffer for the
    // top-level "<cd-drive-root>SYSTEM.INI"-equivalent resource-file load, immediately after
    // RunMainLoop sets it to `mainMem + 0xe0000` here â€” a fixed scratch address inside winsys3d's
    // arena reserved for that blob (LoadFirstLevelResources then treats it as a struct: offsets
    // 0x10/0x48/0x6c/100 are read back as sub-tables/fields once loaded).
    internal byte* s_MainResourceBuffer_005e97c0;
    internal int s_Unknown_005e9820;

    /// <summary>
    /// Ghidra: FUN_00547130 @ 0x00547130 â€” trivial setter, folded directly into RunMainLoop above
    /// (`s_Flag_0056e9c0 = 1`) rather than kept as a separate one-line method.
    /// </summary>


    // =====================================================================
    // Fusionne depuis Memory/Arenas.cs le 2026-08-19.
    // =====================================================================

    // Ghidra: &DAT_00551640.. et &DAT_00571500.. — les deux plages de parametres et d'etat par
    // classe d'entite que les onze initialiseurs remettent a neuf. Une allocation chacune, pas
    // cent champs separes : les initialiseurs les parcourent par pas de dword.
    internal const int EntityParamsBase = 0x00551640;
    internal readonly byte* s_EntityParams_00551640 = (byte*)NativeMemory.AllocZeroed(0x200);
    internal int* EntityParam(int va) => (int*)(s_EntityParams_00551640 + (va - EntityParamsBase));

    internal const int EntityStateBase = 0x00571500;
    internal readonly byte* s_EntityState_00571500 = (byte*)NativeMemory.AllocZeroed(0x500);
    internal int* EntityState(int va) => (int*)(s_EntityState_00571500 + (va - EntityStateBase));

    // ---------------------------------------------------------------------------------------
    // Les arenes de la carte du monde.
    // ---------------------------------------------------------------------------------------

    /// <summary>
    /// Ghidra: 0x005eca00..0x00600c00 — **une seule region continue**, celle des tableaux
    /// d'entites. Les six <c>FUN_005352c0</c> de <c>FUN_00516350</c> la couvrent bout a bout :
    /// 0x5ecaa0 (0x20 entrees), 0x5edee0 (0x80), 0x5f2d40 (0x80), 0x5f7de0 (0x40), 0x5fa5e0 (0x80),
    /// 0x5ff400 (0x20) — chacune de 0x9c octets, chaque bloc finissant exactement ou le suivant
    /// commence. Une allocation, pas cinquante champs : c'est le defaut d'aliasing habituel evite
    /// d'avance.
    /// </summary>
    internal const int EntityArenaBase = 0x005eca00;
    internal readonly byte* s_EntityArena_005eca00 = (byte*)NativeMemory.AllocZeroed(0x14200);
    internal byte* Entity(int va) => s_EntityArena_005eca00 + (va - EntityArenaBase);

    /// <summary>Ghidra: 0x005de030..0x005de0b0 — le bloc de camera et de position du groupe. Son
    /// adresse est stockee telle quelle dans <c>DAT_005edebc</c>, d'ou l'allocation native.</summary>
    internal const int CameraBlockBase = 0x005de030;
    internal readonly byte* s_CameraBlock_005de030 = (byte*)NativeMemory.AllocZeroed(0x80);
    internal int* Camera(int va) => (int*)(s_CameraBlock_005de030 + (va - CameraBlockBase));

    /// <summary>Ghidra: 0x006067c0..0x00606800 — le second bloc de vue, dont l'adresse part dans
    /// <c>DAT_005e9ae0</c> et <c>DAT_005eca94</c>.</summary>
    internal const int ViewBlockBase = 0x006067c0;
    internal readonly byte* s_ViewBlock_006067c0 = (byte*)NativeMemory.AllocZeroed(0x40);
    internal int* View(int va) => (int*)(s_ViewBlock_006067c0 + (va - ViewBlockBase));

    // Ghidra: DAT_005e85c0 (l'ordre de sélection, un octet par entrée : 0 = non retenu, 1..4 = la
    // place dans le groupe) et DAT_005e8640 (les libellés de ligne, 0x16 octets par entrée). Deux
    // symboles voisins d'une même région, déclarés ensemble plutôt que séparément — c'est le défaut
    // d'aliasing que ce projet a déjà rencontré deux douzaines de fois. Le bloc s'arrête bien avant
    // 0x5e8f00 (s_EntityDecodeTable) et commence après 0x5e85a8, fin de la table des sons.
    internal const int PartyListBase = 0x005e85c0;
    internal readonly byte* s_PartyListArena_005e85c0 = (byte*)NativeMemory.AllocZeroed(0x340);

    // Ghidra: &DAT_005de410 — le bloc d'état du menu objets. **Une seule allocation**, pas une
    // vingtaine de champs : FUN_00524210 remet à zéro DAT_005de4c4..DAT_005de50c d'affilée, ce qui
    // les désigne comme contigus, et DAT_005de410/DAT_005de464 vivent dans le même voisinage. Le
    // bloc s'arrête juste avant DAT_005de510, qui est s_GameStateId — d'où les 0x100 octets.
    //
    // C'est le défaut d'aliasing que ce projet a rencontré deux douzaines de fois : déclarer ces
    // adresses en champs séparés marche jusqu'au jour où un site les parcourt.
    //
    // **Etendu vers le bas le 2026-08-18** : le sous-ecran « Invertaire » (FUN_00524880) atteint
    // DAT_005de110 (le tampon de nom d'objet que FUN_0051bcc0 remplit), DAT_005de2d8 (les vingt-
    // deux drapeaux « objet retenu »), DAT_005de390 (le titre de la carte memoire en UTF-16) et
    // DAT_005de428/430/488/4a0. La base descend donc a 0x005de100 et le bloc fait 0x420 octets ;
    // ItemMenuState() etant adresse par VA absolue, aucun site existant ne bouge.
    internal const int ItemMenuArenaBase = 0x005de100;
    internal readonly byte* s_ItemMenuArena_005de100 = (byte*)NativeMemory.AllocZeroed(0x460);
    internal int* ItemMenuState(int va) => (int*)(s_ItemMenuArena_005de100 + (va - ItemMenuArenaBase));
    internal byte* ItemMenuBytes(int va) => s_ItemMenuArena_005de100 + (va - ItemMenuArenaBase);

    // =====================================================================
    // Fusionne depuis Memory/DataTables.cs le 2026-08-19.
    // =====================================================================
    // =====================================================================
    // Les huit sous-états de la sélection des personnages
    //
    // Ghidra: PTR_LAB_00560708 @ 0x00560708 — table de huit pointeurs relevée dans
    // Legacy/Blaze.exe au décalage fichier 0x15ED08. FUN_00506880 y répartit chaque frame par
    // DAT_00578de8. Aucun de ces huit corps n'existe dans Blaze.c : seule cette table les désigne,
    // donc Ghidra ne les a jamais désassemblés. Un re-run ciblé sur ces huit adresses les produit
    // d'un coup.
    //
    // Vérifié dans le binaire : les huit sont de vrais prologues de fonction, et **sept sur huit**
    // ouvrent sur la même instruction, `mov eax, [0x00562A48]` — le drapeau « première frame de ce
    // sous-état » que LoadCharacterSelectResources met à 1 et que FUN_00506880 relève à chaque
    // changement de sous-état. Le premier lit `[0x005633A8]` à la place.
    // =====================================================================

    // Ghidra: &DAT_00560708 — la table elle-même, relevée dans Legacy/Blaze.exe au décalage
    // fichier 0x15ED08. Les huit corps sont désormais décompilés et rangés dans
    // Legacy/Raw/Blaze/FUN_<adresse>.c ; il reste à les transcrire.
    internal readonly int[] s_CharacterSelectSubStateAddresses =
    {
        0x00507010, 0x00507cb0, 0x00508410, 0x00508860,
        0x00508e00, 0x00509470, 0x005098f0, 0x0050a910,
    };

    // Ghidra: &DAT_00560ff0 et suivants — la rangée de descripteurs d'interface que la boucle de
    // frame dessine, pas 0x8c8 octets. Trois instances statiques, relevées dans Legacy/Blaze.exe
    // aux décalages 0x15F5F0, 0x15FEB8 et 0x160780 ; FUN_00509470 confirme la troisième en
    // référençant 0x00562180 = 0x00560ff0 + 2 * 0x8c8. Seuls les 0x68 premiers octets de chaque
    // sont initialisés — la queue est nulle dans le fichier, vérifié.
    //
    // Le champ +4 est une paire (x, y) empaquetée : les trois boîtes sont à x = 0x28, 0x78, 0xd0
    // pour un même y = 0x18, et le champ +0x14 vaut leur index 0, 1, 2.
    internal const int UiDescriptorStride = 0x8c8;
    internal const int UiDescriptorCount = 3;
    // Alloué bas. Ces descripteurs portent leur **propre** ordering table à +0xa4, que
    // ClearOTagR remplit de liens masqués à 0xffffff (voir la marche de liens dans
    // DrawUiPanel) : au-dessus de 0x01000000 cette table se relit sur des adresses tronquées.
    // Le masque de RunTextEngine n'est que le premier symptôme du même problème. Dans
    // l'original la question ne se pose pas, ces structures sont des globales de .data.
    //
    // Paresseux plutôt qu'initialiseur de champ, pour la raison exposée sur
    // s_MenuContext_0056dd58 : un échec d'allocation basse doit rester rattrapable par relance,
    // pas se présenter en TypeInitializationException.
    internal byte* s_UiDescriptorsStorage;
    internal byte* s_UiDescriptors_00560ff0
        => s_UiDescriptorsStorage != null
            ? s_UiDescriptorsStorage
            : (s_UiDescriptorsStorage = BuildUiDescriptors());

    internal byte* BuildUiDescriptors()
    {
        byte* block = AllocLowZeroed((uint)(UiDescriptorCount * UiDescriptorStride));

        // Les 0x68 octets initialisés de chaque instance, relevés dans Legacy/Blaze.exe aux
        // décalages 0x15F5F0, 0x15FEB8 et 0x160780 — 26 dwords, pas 8. Les mots de fonction
        // sont testés par DrawUiPanel en `champ & 0x1000000` : sans f8 (texte) ni f16 (cadre),
        // le panneau se dessine vide, exactement comme le descripteur du menu principal quand il
        // était laissé à zéro.
        //   f5   l'index de la boîte (0, 1, 2)      f12  curseur      — bit 0x1000000 absent
        //   f8   texte        — bit présent         f14  flèches      — bit absent
        //   f16  cadre        — bit présent
        const int UiDescriptorHeaderDwords = 26;
        ReadOnlySpan<uint> headers = new uint[]
        {
            // 0x00560ff0
            0x00000031, 0x00180028, 0x00180048, 0x00000000,
            0x01080000, 0x00000000, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x005618b8
            0x00000031, 0x00180078, 0x00180050, 0x00000000,
            0x01080000, 0x00000001, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x00562180
            0x00000031, 0x001800d0, 0x00180038, 0x00000000,
            0x021a0000, 0x00000002, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
        };

        for (int i = 0; i < UiDescriptorCount; i++)
        {
            uint* dest = (uint*)(block + i * UiDescriptorStride);
            for (int f = 0; f < UiDescriptorHeaderDwords; f++)
            {
                dest[f] = headers[i * UiDescriptorHeaderDwords + f];
            }
        }

        return block;
    }

    // Ghidra: &DAT_00560728 — une neuvième instance du même type, celle que la boucle de frame
    // passe au moteur de texte (FUN_005443a0) puis dessine. Relevée au décalage fichier 0x15ED28.
    //
    // Alloué bas et paresseux pour la même raison que s_UiDescriptors_00560ff0 : il traverse
    // RunTextEngine puis DrawUiPanel, donc il porte une ordering table à +0xa4.
    internal byte* s_StatusDescriptorStorage;
    internal byte* s_StatusDescriptor_00560728
        => s_StatusDescriptorStorage != null
            ? s_StatusDescriptorStorage
            : (s_StatusDescriptorStorage = BuildStatusDescriptor());

    internal byte* BuildStatusDescriptor()
    {
        byte* block = AllocLowZeroed((uint)UiDescriptorStride);
        uint* f = (uint*)block;

        // Les 26 dwords du décalage fichier 0x15ED28, queue comprise : f25 manquait.
        f[0] = 0x00000031; f[1] = 0x00a80028; f[2] = 0x002800e0; f[3] = 0x00000000;
        f[4] = 0x021a0000; f[5] = 0x0000000c; f[6] = 0x010f0808; f[7] = 0x02cf1020;
        f[8] = 0x01070c10; f[9] = 0x01000000; f[10] = 0x00000000; f[11] = 0x01000000;
        f[12] = 0x000f28d4; f[13] = 0x01000000; f[14] = 0x000f0a00; f[15] = 0x01000000;
        f[16] = 0x010f0000; f[17] = 0x01000000; f[19] = 0x01000000; f[21] = 0x01000000;
        f[23] = 0x01000000; f[25] = 0x01000000;

        return block;
    }

    // Ghidra: &DAT_0056b558 et &DAT_0056be20 — **deux** instances du même type, contiguës
    // (0x56b558 + 0x8c8 = 0x56be20), celles de l'écran de chargement de sauvegarde
    // (<c>FUN_00536ea0</c>, l'état de jeu 1 normal). Sa boucle de rendu les parcourt exactement
    // ainsi : <c>do { RunTextEngine; DrawUiPanel; p += 0x8c8; } while (p &lt; 0x56c6e8)</c>, et
    // 0x56c6e8 est bien 0x56b558 + 2 * 0x8c8 — ce qui borne le tableau à deux entrées.
    //
    // Relevées dans Legacy/Blaze.exe : mêmes 26 dwords initialisés que les autres instances, queue
    // nulle. Allouées bas et paresseusement pour la même raison que s_UiDescriptors_00560ff0 : elles
    // traversent RunTextEngine puis DrawUiPanel, donc portent une ordering table à +0xa4.
    internal const int LoadScreenDescriptorCount = 2;
    internal byte* s_LoadScreenDescriptorsStorage;
    internal byte* s_LoadScreenDescriptors_0056b558
        => s_LoadScreenDescriptorsStorage != null
            ? s_LoadScreenDescriptorsStorage
            : (s_LoadScreenDescriptorsStorage = BuildLoadScreenDescriptors());

    internal byte* BuildLoadScreenDescriptors()
    {
        byte* block = AllocLowZeroed((uint)(LoadScreenDescriptorCount * UiDescriptorStride));

        ReadOnlySpan<uint> headers = new uint[]
        {
            // 0x0056b558
            0x00000031, 0x00a80028, 0x002800e0, 0x00000000,
            0x021a0000, 0x0000000c, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f28d4, 0x01000000, 0x000f0c00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x0056be20
            0x00000031, 0x00100028, 0x009000e0, 0x00000000,
            0x081a0000, 0x00000004, 0x010f0808, 0x02cf1020,
            0x01071018, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
        };

        for (int i = 0; i < LoadScreenDescriptorCount; i++)
        {
            uint* dest = (uint*)(block + i * UiDescriptorStride);
            for (int f = 0; f < 26; f++) dest[f] = headers[i * 26 + f];
        }

        return block;
    }

    internal byte* s_LoadScreenDescriptor2_0056be20 => s_LoadScreenDescriptors_0056b558 + UiDescriptorStride;

    // **Huit champs de ces deux descripteurs, jusqu'ici déclarés comme des octets isolés** —
    // la même classe de défaut d'aliasing que les six corrigés le 2026-08-16, trouvée le
    // 2026-08-17 en lisant FUN_00536ea0. Les adresses le disent d'elles-mêmes une fois la taille du
    // descripteur (0x8c8) posée, et la symétrie est parfaite : quatre champs par descripteur, aux
    // mêmes décalages que ceux déjà nommés sur les autres instances
    // (<see cref="s_Unknown_00560763"/> = +0x3b, <see cref="s_MenuSelection_0056e61c"/> = +0x8c4,
    // <see cref="s_Unknown_0056e61f"/> = +0x8c7).
    //
    //   0x56b593 = descripteur 1 + 0x3b     0x56be5b = descripteur 2 + 0x3b
    //   0x56be1c = descripteur 1 + 0x8c4    0x56c6e4 = descripteur 2 + 0x8c4
    //   0x56be1d = descripteur 1 + 0x8c5    0x56c6e5 = descripteur 2 + 0x8c5
    //   0x56be1f = descripteur 1 + 0x8c7    0x56c6e7 = descripteur 2 + 0x8c7
    //
    // Séparés, les huit ne recevaient jamais les écritures réelles de DrawUiPanel/RunTextEngine, et
    // les remises à zéro de RunGameState1Entry tombaient dans le vide (les huit avertissements
    // CS0414 « assigné mais jamais utilisé » qu'ils produisaient en étaient le symptôme visible).
    internal ref byte s_Unknown_0056b593 => ref s_LoadScreenDescriptors_0056b558[0x3b];
    internal ref byte s_Unknown_0056b58b => ref s_LoadScreenDescriptors_0056b558[0x33];
    internal ref byte s_Unknown_0056be1c => ref s_LoadScreenDescriptors_0056b558[0x8c4];
    internal ref byte s_Unknown_0056be1d => ref s_LoadScreenDescriptors_0056b558[0x8c5];
    internal ref byte s_Unknown_0056be1f => ref s_LoadScreenDescriptors_0056b558[0x8c7];
    internal ref byte s_Unknown_0056be5b => ref s_LoadScreenDescriptor2_0056be20[0x3b];
    internal ref byte s_Unknown_0056c6e4 => ref s_LoadScreenDescriptor2_0056be20[0x8c4];
    internal ref byte s_Unknown_0056c6e5 => ref s_LoadScreenDescriptor2_0056be20[0x8c5];
    internal ref byte s_Unknown_0056c6e7 => ref s_LoadScreenDescriptor2_0056be20[0x8c7];

    // Ghidra: &DAT_00562a98 — quatre octets par sous-état, relevés au décalage fichier 0x161098.
    // Seul le troisième varie (0x02 pour les sous-états 0 à 4, 0x03 pour 5 à 7).
    internal readonly byte* s_SelectSubStateBytes_00562a98;

    internal byte* BuildSelectSubStateBytes()
    {
        byte* table = (byte*)NativeMemory.AllocZeroed(8, 4);
        for (int row = 0; row < 8; row++)
        {
            table[row * 4 + 0] = 0x00;
            table[row * 4 + 1] = 0x01;
            table[row * 4 + 2] = (byte)(row < 5 ? 0x02 : 0x03);
            table[row * 4 + 3] = 0x00;
        }
        return table;
    }

    // Ghidra: DAT_00562b68 — le compteur que le test de sortie consulte au sous-état 7. Jamais
    // écrit par le code reconstruit pour l'instant, donc il lit 0.
    internal int s_Unknown_00562b68;

    // Ghidra: &DAT_005606f8 — le descripteur d'étiquette que le carrousel lie avant sa première
    // passe de texte. Seize octets relevés dans Legacy/Blaze.exe au décalage fichier 0x15ECF8 :
    // {0x0000000c, 0, 0x005066c0, 0}. Il précède immédiatement PTR_LAB_00560708.
    //
    // TODO(REVERSE): le troisième champ est un **pointeur de code**, 0x005066c0 — juste avant
    // FUN_005066d0, et absent de Blaze.c comme les huit sous-états l'étaient. Si BindMenuLabels
    // appelle à travers, ça se verra à l'exécution ; il faudra alors un re-run Ghidra ciblé de
    // plus sur cette adresse.
    internal readonly byte* s_Unknown_005606f8;

    internal byte* BuildLabelDescriptor005606f8()
    {
        byte* block = (byte*)NativeMemory.AllocZeroed(0x10);
        ((uint*)block)[0] = 0x0000000c;
        ((uint*)block)[1] = 0;
        ((uint*)block)[2] = 0x005066c0;
        ((uint*)block)[3] = 0;
        return block;
    }


    // Ghidra: DAT_005e9308 / 930a / 930c — l'orientation de la scène, en pas de 0x1000 par tour
    // (la convention des tables rsin/rcos de winsys3d, ordinaux 450/451).
    internal int s_ScenePitch_005e9308, s_SceneYaw_005e930a, s_SceneRoll_005e930c;

    // Ghidra: DAT_005e93a0 / 93a2 / 93a4 — la couleur de fond passée à Ordinal_376 (SetBackColor).
    internal int s_BackColourR_005e93a0, s_BackColourG_005e93a2, s_BackColourB_005e93a4;

    // Ghidra: DAT_00604928 — trois shorts contigus, la pose de caméra. Le troisième (+2) est
    // l'angle amorti vers le personnage courant ; FUN_0051c300 consomme le tout.
    internal readonly byte* s_CameraVector_00604928 = (byte*)NativeMemory.AllocZeroed(8);

    // Ghidra: DAT_006008d0 / d4 / d8 — le décalage monde appliqué à la position de l'entité avant
    // projection. Remis à zéro à l'entrée du sous-état 0.
    internal int s_Unknown_006008d0, s_Unknown_006008d4, s_Unknown_006008d8;

    // Ghidra: &DAT_006008e0 — la matrice de caméra, **un seul bloc de 0x20 octets**, exactement la
    // MATRIX du SDK PlayStation : neuf shorts de rotation (0x006008e0..0x006008f1) puis les trois
    // translations à +0x14, +0x18 et +0x1c, que Ghidra nomme DAT_006008f4, DAT_006008f8 et
    // **DAT_006008fc**.
    //
    // Ce dernier point mérite d'être explicite : DAT_006008fc n'est pas une variable isolée. C'est
    // la translation Z de cette matrice, et la boucle de frame l'amortit vers 800 à chaque image
    // (`DAT_006008fc = DAT_006008fc + (800 - DAT_006008fc >> 2)`) — autrement dit elle fait avancer
    // la caméra. Déclarer les trois translations à part de la rotation couperait la structure en
    // deux et FUN_0051d1f0 recopierait une matrice à moitié fausse dans le registre du GTE.
    internal readonly byte* s_CameraMatrix_006008e0 = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>Ghidra: DAT_006008f4 / f8 / fc — la rangée de translation de la matrice ci-dessus.</summary>
    internal int* CameraTranslation => (int*)(s_CameraMatrix_006008e0 + 0x14);

    // Ghidra: &DAT_00578420 — les seize emplacements de personnage du carrousel, pas 0x9c octets
    // (le 0x4e du pas `ushort *` de Ghidra, et le 0x27 de la copie par dwords en case 2). C'est le
    // même pas 0x9c que la table DAT_005e93c8 que CurrentPadState indexe.
    internal const int CharacterSlotCount = 16;
    internal const int CharacterSlotStride = 0x9c;
    internal readonly byte* s_CharacterSlots_00578420 =
        (byte*)NativeMemory.AllocZeroed(CharacterSlotCount, CharacterSlotStride);

    // Ghidra: &DAT_00572b48 — la destination de la copie faite à la validation : l'emplacement
    // choisi devient l'entité de scène que le sous-état suivant anime.
    //
    // Tout ce que FUN_00506880 nomme DAT_00572b4c / 52 / 60 / 62 / 64..6c / 70..74 tombe dedans :
    // ce sont ses champs +0x04, +0x0a, +0x18, +0x1a, +0x1c..+0x24 et +0x28..+0x2c. Il n'y a donc
    // aucun « bloc de globales 0x00572b40 » à modéliser à côté — c'est le même enregistrement de
    // 0x9c octets qu'un emplacement du carrousel, et les six décalages le confirment un à un.
    internal readonly byte* s_SceneEntity_00572b48 =
        (byte*)NativeMemory.AllocZeroed((nuint)CharacterSlotStride, 1);

    // Ghidra: &DAT_00562a58 — huit teintes de fond, trois shorts et un mot de bourrage chacune,
    // relevées au décalage fichier 0x161058. La table s'arrête net sur DAT_00562a98, huit entrées
    // plus loin, ce qui en donne la taille sans avoir à la deviner.
    internal const int SceneToneStride = 8;
    internal readonly byte* s_SceneToneTable_00562a58;

    internal byte* BuildSceneToneTable()
    {
        ReadOnlySpan<short> tones =
        [
            192,  96,   0, 0,      0,  96, 192, 0,
            128,  96,  64, 0,     64, 128,  96, 0,
            192, 192, 192, 0,      0,   0,   0, 0,
            128, 128,  96, 0,     64,  32, 128, 0,
        ];

        byte* block = (byte*)NativeMemory.AllocZeroed(8, SceneToneStride);
        for (int i = 0; i < tones.Length; i++) ((short*)block)[i] = tones[i];
        return block;
    }

    // Ghidra: &DAT_00572be4 — les particules de la scène : quatre enregistrements de 0x9c octets,
    // du même type que l'entité ci-dessus, et **contigus avec elle** (0x00572b48 + 0x9c = 0x00572be4).
    //
    // La base a demandé deux lectures croisées, parce que les deux boucles qui parcourent ce
    // tableau ne le désignent pas au même endroit :
    //   FUN_00506880  part de &DAT_00572c34, pas 0x27 ints, borne 0x572ea4, et travaille sur
    //                 `piVar10 - 0xd` — soit 0x572c00, 0x572c9c, 0x572d38, 0x572dd4 ;
    //   FUN_00508e00  part de &DAT_00572c20, même pas, borne 0x572e90, et recopie 0x27 dwords vers
    //                 `puVar6 - 0xf` — soit 0x572be4, 0x572c80, 0x572d1c, 0x572db8.
    // Les secondes valent les premières moins 0x1c : la boucle de frame manipule le **champ de
    // position** (+0x1c), pas la base. Quatre records dans les deux cas, et la contiguïté avec
    // l'entité confirme le tout.
    internal const int SceneParticleCount = 4;
    internal const int SceneParticleStride = 0x9c;
    internal readonly byte* s_SceneParticles_00572be4 =
        (byte*)NativeMemory.AllocZeroed(SceneParticleCount, SceneParticleStride);

    // **Corrigé 2026-08-16** : DAT_00560fef, DAT_0056075b, DAT_00560763 ne sont PAS des globales
    // isolées — ce sont des décalages dans le MÊME descripteur que s_StatusDescriptor_00560728
    // (0x560fef-0x560728=0x8c7, 0x56075b-0x560728=0x33, 0x560763-0x560728=0x3b), exactement les
    // champs que BindMenuLabels/RunTextEngine/DrawUiPanel manipulent déjà via ce même pointeur
    // (`descriptor[0x8c7]`, `descriptor[0x33]`, `descriptor[0x3b]`). Déclarées comme trois `byte`
    // séparés, ces trois globales ne partageaient jamais la mémoire du vrai descripteur : chaque
    // appel à BindMenuLabels/DrawUiPanel armait ou effaçait des bits sur une copie fantôme que
    // StepSubState7Phase (et les sous-états 1/3/4/6 sœurs) ne voyait jamais — d'où des boîtes de
    // dialogue dont l'état affiché ne correspondait jamais à ce que la logique de phase lisait.
    // Trouvé par point d'arrêt matériel (x32dbg, Analyse/x32dbg/POINT_ARRET_0x00560758) : voir
    // REVERSE_STATUS.md, section « the generic OK dialog subsystem ».
    internal ref byte s_Unknown_00560fef => ref s_StatusDescriptor_00560728[0x8c7];
    internal ref byte s_Unknown_0056075b => ref s_StatusDescriptor_00560728[0x33];
    internal ref byte s_Unknown_00560763 => ref s_StatusDescriptor_00560728[0x3b];

    // **Corrigé 2026-08-16**, même défaut : DAT_005e909f (confirmGate) est le champ +0x3f d'un
    // tableau, pas une globale isolée — DAT_005e9060 et DAT_005e909f sont la MÊME mémoire
    // contiguë (0x5e909f - 0x5e9060 = 0x3f), confirmé par point d'arrêt matériel
    // (Analyse/x32dbg/POINT_ARRET_0x005E909F) : `DrawUiPanel` (le widget « flèches de défilement »,
    // Blaze.cs plus bas) écrit déjà `s_PanelSelection_005e9060[descriptor[0x8c5]]` au moment de la
    // validation — ce qui, pour tout écran armant `descriptor[0x8c5]=0x3f`, doit retomber
    // exactement sur cet octet. Séparé comme un `byte` isolé, ce champ ne recevait jamais
    // l'écriture réelle du widget.
    internal ref byte s_Unknown_005e909f => ref s_PanelSelection_005e9060[0x3f];

    /// <summary>
    /// Ghidra: FUN_005352e0 @ 0x005352e0 — recopie de <c>count</c> **dwords** de
    /// <c>source</c> vers <c>destination</c>.
    ///
    /// <para>**Confirmé** le 2026-08-17 sur le désassemblage complet
    /// (<c>Legacy/Raw/Blaze/FUN_005352e0.asm</c>, 32 octets en tout) : sortie immédiate si le
    /// compteur est nul, puis une boucle <c>MOV ESI,[EAX] / ADD EAX,4 / MOV [ECX],ESI /
    /// ADD ECX,4 / DEC EDX / JNZ</c>. C'était jusqu'ici une hypothèse déduite des deux sites
    /// d'appel du sous-état 0 (0x27 = 0x9c octets, et 10) ; l'ordre des arguments
    /// (source, destination, nombre) est lui aussi confirmé par les <c>[ESP+4]</c>/<c>[ESP+8]</c>/
    /// <c>[ESP+0xc]</c>.</para>
    /// </summary>
    internal void CopyDwords(uint* source, uint* destination, int count)
    {
        for (int i = 0; i < count; i++) destination[i] = source[i];
    }

    // =====================================================================
    // Globales .data rapatriees le 2026-08-19, en preparation de la conversion de
    // Engine/UiText.cs en vraie classe : il les lisait sans les declarer.
    // =====================================================================

    // Ghidra: DAT_00564a48 â€” boolean capability flag set by CheckDeviceCapabilityFlag, read
    // throughout the renderer (DrawAnimatedModel, EmitParticle, DrawUiPanel's neighbours,
    // Handler_00544810) to gate a colour/attribute masking step. Was briefly duplicated under a
    // second, dead field (s_Unknown_00564a48, always 0 because nothing ever wrote it) that every
    // one of those call sites read instead of this one â€” merged back onto this field 2026-08-16.
    internal bool s_DeviceCapFlag_00564a48;

    // Ghidra: DAT_005e97c4 â€” the loaded resource file's own "next free address" bookkeeping value;
    // not yet used further by any reconstructed code past LoadFirstLevelResources itself.
    internal byte* s_MainResourceDirectory_005e97c4;

    // Ghidra: &DAT_005e8f00 â€” the menu state's own tpage/clut slot table, distinct from the
    // 0x005e8fc0 one LoadFirstLevelResources fills. Same 4-byte-per-entry layout.
    internal readonly byte* s_EntityDecodeTable_005e8f00 = (byte*)NativeMemory.AllocZeroed(0x1000);

    internal int s_Unknown_005e8310;

    // =====================================================================
    // Rapatriees le 2026-08-19 pour fermer Engine/Primitives2d.cs et Engine/SaveData.cs.
    // s_CardWriteStatus / s_CardReadStatus sont ecrites par des rappels [UnmanagedCallersOnly]
    // qui doivent rester statiques : ils les atteignent par le champ statique Blaze.Mem.
    // =====================================================================

    // Ghidra: DAT_00578e0c — la phase, indexée directement (contrairement au sous-état 6, la table
    // de saut à deux niveaux qui la lit s'est révélée n'être qu'une implémentation de compilateur
    // pour un switch clairsemé ; chaque valeur de phase correspond un pour un à un cas du switch
    // ci-dessous, confirmé en lisant &DAT_0050bea8 dans le binaire).
    internal int s_CardPhase_00578e0c;

    // Ghidra: DAT_00573338 — le bloc que la boucle place à MainMem + 0xf0000. C'est la même adresse
    // que la destination de la liste +0xb0 de LoadCharacterSelectResources : le chargement y dépose
    // des blocs bruts, la boucle de frame l'efface puis le relit
    // (`*(byte *)(DAT_00573338 + 0x151)` sert d'index dans la table de couleurs DAT_00562a58).
    //
    // **Dérivé de MainMem plutôt qu'affecté une fois (2026-08-18).** C'était un champ posé par le
    // seul LoadCharacterSelectResources, donc encore nul pour qui atteignait l'état de jeu 1 sans
    // être passé par l'écran de sélection des personnages — exactement ce que fait « Début du jeu »
    // au menu principal, et ce qui faisait échouer le `NativeMemory.Clear` de RunGameState1Entry sur
    // un pointeur nul. L'original n'a pas ce problème : il recalcule `*MainMem + 0xf0000` sur place
    // à chaque site (voir FUN_00536ea0, `iVar17 = *(int *)MainMem_exref + 0xf0000;`). On fait pareil.
    internal byte* s_SelectScratch_00573338
    {
        get
        {
            byte* mainMem = GetMainMemPtr();
            return mainMem != null ? mainMem + 0xf0000 : null;
        }
    }

    // Ghidra: &DAT_005e9180 â€” small per-slot table reset at the end of FUN_0052a030 (4 entries of
    // 0x4c/76 bytes: 0x13 dwords zeroed, byte[-1]=8, byte[0]=incrementing slot id). Also touched
    // (not yet reconstructed) by FUN_00542160's near-identical loop elsewhere.
    // **La même mémoire que <see cref="s_SlotArea_005e9180"/>**, qui déclarait déjà cette adresse de
    // son côté. Séparés, <see cref="InitialiseEntitySlots"/> écrivait dans l'un pendant que
    // <see cref="CurrentPadState"/> lisait l'autre : l'initialisation des emplacements manette
    // n'atteignait jamais son lecteur. Fusionnés le 2026-08-17 en un alias sur l'arène.
    internal byte* s_EntitySlotTable_005e9180 => Mem.Arena(0x005e9180);

    // Ghidra: DAT_00564a4c — une garde de réentrance, pas un drapeau statique. Dans Blaze.c, chaque
    // site qui l'écrit suit exactement le même motif : `DAT_00564a4c = 0; <appel réentrant>;
    // DAT_00564a4c = 1;` (au moins quatre paires : FUN_00520ae0/EmitParticle à trois endroits,
    // FUN_00537da0 dans le case 2 de FUN_00542ab0). Sa valeur stable dans le jeu original est donc
    // **1**, jamais 0 — le passage à 0 n'existe que pendant la fenêtre de l'appel imbriqué. Aucun de
    // ces sites n'est encore câblé dans ce port, donc la valeur par défaut de C# (0) était fausse :
    // elle inversait le culling que DrawAnimatedModel/EmitParticle/DrawSpriteVariant/DrawFlatPrimitive
    // testent (`if (s_Unknown_00564a4c == 0) return prim;`), faisant sauter des modèles qui, dans
    // l'original, seraient dessinés (juste avec le bit d'attribut nettoyé). Régression confirmée en
    // jeu le 2026-08-16 : le carrousel de personnages n'affichait plus que l'entité sélectionnée.
    // Voir DrawSpriteVariant pour la divergence avec s_SmallTextureFlag.
    internal int s_Unknown_00564a4c = 1;

    // Ghidra: DAT_00604ab0 / DAT_00604a00 — les drapeaux d'état de transfert écriture/lecture que
    // FUN_005046c0/FUN_00504770 sondent. 4 = prêt/inactif.
    internal int s_CardWriteStatus_00604ab0 = 4, s_CardReadStatus_00604a00 = 4;
}
