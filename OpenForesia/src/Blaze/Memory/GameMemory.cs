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
// fin de Documentation/Reference/DataSymbols.md pour le controle d'adjacence.

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
    // Etendue de 0x0a00 a 0x1600 le 2026-08-18, puis **ramenee a 0x1560 le 2026-08-23**.
    //
    // Le site qui fixe la borne est le balayage de FUN_00516350 : `undefined1 *puVar9 =
    // &DAT_005e9b87`, 0x40 tours de pas 0x28, un OCTET remis a zero par tour. Le dernier ecrit est
    // donc 0x005e9b87 + 0x3f * 0x28 = **0x005ea55f**, soit l'index 0x155f — d'ou 0x1560. La borne
    // de 0x1600 venait d'un compte a un enregistrement pres (0x40 * 0x28 au lieu de 0x3f * 0x28),
    // et le meme calcul vaut pour la table de 0x40 enregistrements de 0x28 octets qui commence en
    // 0x005e9b60 : elle s'arrete elle aussi a 0x005ea55f.
    //
    // Reduire l'etendue n'est pas cosmetique. La queue 0x005ea560..0x005ea600 recouvrait
    // s_EntityStatBlocks_005ea560, une allocation DISTINCTE, et ce recouvrement a ete **vivant**
    // jusqu'au 2026-08-23 : ResolveEntityBlocks ecrivait les statistiques d'entite de scene dans
    // l'arene tandis que GetEntityStatBlock les relisait dans l'autre bloc, si bien que toute
    // entite de scene naissait a zero point de vie. Les deux sites sont corriges ; refermer
    // l'etendue empeche de rouvrir la porte par un simple `Mem.Arena(0x005ea5..)`.
    internal const int GameArenaSize = 0x1560;
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
    // Valeur au chargement relevée dans le binaire (`at Legacy/Binaries/Blaze.exe 0x005633a8`, .data) : **1**.
    // Largeur confirmée par le code machine : `0x00536218  39 1D A8 33 56 00  CMP dword [0x5633a8],EBX`
    // â€” un dword, donc bien un `int`.
    //
    // Le seul écrivain de tout le binaire est <see cref="CheatCodeProbe"/> (FUN_0051c0e0), qui le
    // met à **0** une fois la séquence "Gurkha" entièrement filée.
    //
    // **La séquence n'est pas une saisie, c'est un DESSIN.** CheatCodeProbe est appelée sur chaque
    // caractère que le moteur de texte trace ; le verrou tombe donc quand le jeu **affiche**
    // "Gurkha" quelque part. Nommer un personnage ainsi est le moyen sûr d'y arriver — l'écran de
    // saisie du nom le réaffiche à chaque lettre — mais n'importe quel texte tracé contenant cette
    // chaîne ferait la même chose. Et comme CheatCodeProbe est **le seul écrivain du binaire**, le
    // verrou ne se relève jamais : une fois à 0, il y reste jusqu'à la fin du processus.
    //
    // Les lecteurs testent tous `== 0`, et chacun active alors un comportement de développeur.
    // Il y en avait trois quand cette note a été écrite ; la reconstruction en compte une douzaine
    // aujourd'hui, dont :
    // <list type="bullet">
    // <item><see cref="InitViewportSurfaces"/> : damier de mise au point dans le tampon de rendu
    // au lieu d'un effacement en noir ;</item>
    // <item>le pilotage libre de la caméra (voir <c>StepSubState7Phase</c>) ;</item>
    // <item><see cref="RunGameState1Entry"/> : bascule vers <see cref="RunDebugPartySelect"/>
    // (FUN_00536270), l'écran de composition de groupe de mise au point, au lieu du chemin
    // normal FUN_00536ea0.</item>
    // <item><c>DrawStatsTab</c> : les **quatre rangs supplémentaires** de la fiche de personnage —
    // les emplacements 2, 3, 6 et 7 du bloc dérivé de <c>+0x130</c>, dessinés en colonne nue en
    // <c>x = 0xd4</c> et <b>sans libellé</b> ;</item>
    // <item><c>DrawSpellTab</c> : tous les sorts marqués comme connus ;</item>
    // <item>l'inventaire : tout objet marqué identifié (<c>record[0x13] |= 0x80</c>).</item>
    // </list>
    // Donc 0 = mise au point active, 1 = jeu normal. Initialisé à 1 comme dans .data : sans cette
    // initialisation, les lecteurs prenaient la branche « développeur » par le seul effet du zéro
    // par défaut de C#.
    //
    // **Fusionné avec l'ancien `s_Unknown_005633a8`**, qui déclarait la même adresse une seconde
    // fois (celui-là recevait l'écriture de CheatCodeProbe, celui-ci portait les lectures : la
    // même classe de défaut d'aliasing que les six champs corrigés le 2026-08-16).
    internal int s_DebugModeLatch_005633a8 = 1;

    // Ghidra: DAT_005631f8 â€” the game's **data root** on the CD. Recovered from Legacy/Binaries/Blaze.exe at
    // VA 0x005631f8: the bytes are "@:\FRENCH\", NOT the bare drive root this was previously assumed
    // to be. See Documentation/Reference/DataSymbols.md.
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
    //
    // CONTRAIREMENT a s_EntityState_00571500, cette plage-ci est SEMEE. Elle est sous la frontiere
    // 0x00571400, donc reellement presente dans le fichier, et ses 0x200 octets y sont initialises.
    // Les onze initialiseurs reecrivent les champs numeriques avec exactement les memes valeurs
    // (Init_0045a220 pose 0x9c4 en 0x00551660, et le binaire y porte deja c4 09 00 00), mais ils
    // ne touchent PAS aux trois flux d'etiquettes noyes dans la plage :
    //
    //   0x00551648  1f d0 20 · 08 00 · 0c · 開かない！？ · 0f · 08 01 · 00   — « ça ne s'ouvre pas ! »
    //   0x00551720  08 00 · 0c · 遠くで音がした。 · 0f · 08 01 · 00        — « un bruit, au loin »
    //   0x00551814  la note de developpeur, deja relevee dans Screens/State03/WorldMapTick.cs
    //
    // AllocZeroed rendait donc des flux vides a qui suit ces pointeurs. C'est
    // ShowEntityLabel_00445860 qui l'a mis au jour : elle pose &DAT_00551640 en +0x3c de son
    // entite, et ce champ est un pointeur de donnee, pas un drapeau.
    //
    // Les huit premiers octets (f0 80 0f 0f 01 00 11 00) precedent le flux de 0x00551648 et ne
    // sont ecrits par personne.
    // TODO(REVERSE) : leur role n'est pas etabli — un en-tete de boite, vraisemblablement, puisque
    // le flux du developpeur en 0x00551814 n'en a pas et se lit directement.
    internal const int EntityParamsBase = 0x00551640;
    internal readonly byte* s_EntityParams_00551640 = BuildEntityParams_00551640();
    internal int* EntityParam(int va) => (int*)(s_EntityParams_00551640 + (va - EntityParamsBase));

    /// <summary>Les 0x200 octets de <c>.data</c> a partir de <c>0x00551640</c>, releves verbatim
    /// dans <c>Legacy/Binaries/Blaze.exe</c> (decalage fichier <c>0x0014FC40</c>).</summary>
    private static byte* BuildEntityParams_00551640()
    {
        ReadOnlySpan<byte> bytes =
        [
            0xf0, 0x80, 0x0f, 0x0f, 0x01, 0x00, 0x11, 0x00, 0x1f, 0xd0, 0x20, 0x08, 0x00, 0x0c, 0x8a, 0x4a,
            0x82, 0xa9, 0x82, 0xc8, 0x82, 0xa2, 0x81, 0x49, 0x81, 0x48, 0x0f, 0x08, 0x01, 0x00, 0x00, 0x00,
            0xc4, 0x09, 0x00, 0x00, 0xc4, 0x09, 0x00, 0x00, 0x04, 0xf7, 0xff, 0xff, 0x04, 0xf7, 0xff, 0xff,
            0xe4, 0xf3, 0xff, 0xff, 0x24, 0xfa, 0xff, 0xff, 0xa4, 0x06, 0x00, 0x00, 0xe4, 0x0c, 0x00, 0x00,
            0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00,
            0x90, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00,
            0xd4, 0xfe, 0xff, 0xff, 0x64, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0xd4, 0xfe, 0xff, 0xff,
            0x54, 0x0b, 0x00, 0x00, 0x3c, 0xf6, 0xff, 0xff, 0x3c, 0xf6, 0xff, 0xff, 0x54, 0x0b, 0x00, 0x00,
            0xc4, 0x09, 0x00, 0x00, 0xc4, 0x09, 0x00, 0x00, 0x04, 0xf7, 0xff, 0xff, 0x04, 0xf7, 0xff, 0xff,
            0xe4, 0xf3, 0xff, 0xff, 0x24, 0xfa, 0xff, 0xff, 0xa4, 0x06, 0x00, 0x00, 0xe4, 0x0c, 0x00, 0x00,
            0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00,
            0x90, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00,
            0xd4, 0xfe, 0xff, 0xff, 0x64, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0xd4, 0xfe, 0xff, 0xff,
            0x54, 0x0b, 0x00, 0x00, 0x3c, 0xf6, 0xff, 0xff, 0x3c, 0xf6, 0xff, 0xff, 0x54, 0x0b, 0x00, 0x00,
            0x08, 0x00, 0x0c, 0x89, 0x93, 0x82, 0xad, 0x82, 0xc5, 0x89, 0xb9, 0x82, 0xaa, 0x82, 0xb5, 0x82,
            0xbd, 0x81, 0x42, 0x0f, 0x08, 0x01, 0x00, 0x00, 0xc4, 0x09, 0x00, 0x00, 0xc4, 0x09, 0x00, 0x00,
            0x04, 0xf7, 0xff, 0xff, 0x04, 0xf7, 0xff, 0xff, 0xe4, 0xf3, 0xff, 0xff, 0x24, 0xfa, 0xff, 0xff,
            0xa4, 0x06, 0x00, 0x00, 0xe4, 0x0c, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00,
            0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x90, 0x01, 0x00, 0x00, 0xd4, 0xfe, 0xff, 0xff, 0x64, 0x00, 0x00, 0x00,
            0x64, 0x00, 0x00, 0x00, 0xd4, 0xfe, 0xff, 0xff, 0x54, 0x0b, 0x00, 0x00, 0x3c, 0xf6, 0xff, 0xff,
            0x3c, 0xf6, 0xff, 0xff, 0x54, 0x0b, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x5d, 0x02, 0x00, 0x00,
            0xec, 0xfa, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x5d, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xec, 0xfa, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x5d, 0x02, 0x00, 0x00,
            0xec, 0xfa, 0xff, 0xff, 0x1f, 0xd0, 0x20, 0x08, 0x00, 0x0c, 0x95, 0xd0, 0x8a, 0x4a, 0x82, 0xab,
            0x82, 0xcc, 0x83, 0x68, 0x83, 0x41, 0x82, 0xcd, 0x8c, 0xbb, 0x8d, 0xdd, 0x8e, 0x67, 0x97, 0x70,
            0x8f, 0x6f, 0x97, 0x88, 0x82, 0xdc, 0x82, 0xb9, 0x82, 0xf1, 0x81, 0x42, 0x8d, 0xa1, 0x88, 0xe4,
        ];

        byte* block = (byte*)NativeMemory.Alloc(0x200);
        bytes.CopyTo(new Span<byte>(block, 0x200));
        return block;
    }

    internal const int EntityStateBase = 0x00571500;

    // Etendue de 0x500 a 0x538 le 2026-08-21 : FUN_005035c0 (la camera de scenette, opcode 29 de
    // l'interpreteur de script) sauvegarde trois blocs de vue jusqu'a 0x00571a34 inclus, soit
    // 0x00571a38 en borne. Les deux bornes restent tenues :
    //   au-dessus, s_Unknown_00571a3c (Screens/State00/MainMenu.cs) commence quatre octets plus
    //   loin — pas de recouvrement, et les tests de disposition le verifient ;
    //   la region est au-dessus de 0x00571400, donc dans la queue non initialisee de .data : zero
    //   au chargement, ecrit uniquement par le code. Une AllocZeroed est fidele.
    internal readonly byte* s_EntityState_00571500 = (byte*)NativeMemory.AllocZeroed(0x538);
    internal int* EntityState(int va) => (int*)(s_EntityState_00571500 + (va - EntityStateBase));
    internal byte* EntityStateBytes(int va) => s_EntityState_00571500 + (va - EntityStateBase);

    // =====================================================================
    // Ghidra: 0x00571270..0x00571400 — les parametres de vue, ajoutes le 2026-08-21 pour
    // FUN_00401000 / FUN_00401080, sur le chemin du prologue de la carte du monde.
    //
    // **69 symboles**, et ce sont des VUES dans un bloc, pas 69 champs. La region est atteinte
    // par decalage constant depuis plusieurs fonctions, et le projet a corrige ce defaut
    // d'aliasing une vingtaine de fois : declarer les adresses une a une marche jusqu'au jour ou
    // un site les parcourt.
    //
    // LES DEUX BORNES SONT DEDUITES, PAS CHOISIES — et la region n'est PAS celle qu'on croyait.
    //
    //   en dessous  0x005710f0..0x00571258  n'est pas du jeu : c'est du CRT. DAT_00571258 est lu
    //               par WideCharToMultiByte (FUN_0054b190), et DAT_005710f0 est la table de pages
    //               de code qui va avec, parcourue par paires jusqu'a &DAT_00571258. Y etendre une
    //               arene d'etat de jeu aurait melange deux proprietaires.
    //   0x0057125c..0x0057126c  n'est cite nulle part. La premiere adresse du jeu est 0x00571270,
    //               que FUN_004011c0 ajoute a s_ViewBlock_006067c0 — un vecteur de decalage.
    //   au-dessus   0x00571400 est exactement la fin du contenu de .data present dans le FICHIER :
    //               PointerToRawData 0x0014D600 + SizeOfRawData 0x22400 = 0x0016FA00, l'offset ou
    //               commence .idata. Au-dela, la section n'existe qu'en memoire et vaut zero au
    //               chargement. La borne est donc reelle, et c'est la seule du binaire ici.
    //
    // CORRECTION DU 2026-08-21, LE MEME JOUR : cette borne etait d'abord justifiee par « au-dessus
    // de 0x00571400 commence une TABLE INITIALISEE de valeurs 0x00208xxx ». **Cette table n'existe
    // pas.** C'etait un defaut de l'outil `at`, qui ne bornait pas la conversion VA -> offset a la
    // SizeOfRawData de la section : pour une adresse dans la queue non initialisee de .data, il
    // rendait un offset tombant dans les octets bruts de .idata (RVA 0x00208000 — d'ou les valeurs
    // 0x00208xxx) et les affichait sans broncher. Corrige dans OpenForesia.Tools/Program.cs
    // (RvaToFileOffset). Le chiffre 0x00571400 tenait par coincidence : c'est la meme frontiere.
    //
    // Et le bloc lui-meme est **integralement nul au chargement**, ce qui reste vrai apres la
    // correction : 0x00571270 est en-deca de la frontiere, donc reellement present dans le fichier,
    // et ses dwords y sont nuls. Une AllocZeroed est fidele, sans valeur a semer.
    //
    // Le nom dit ce qui est observe, pas plus : le bloc alimente s_ViewBlock_006067c0 par addition,
    // recoit des valeurs de camera (0x005de068/06c/070) en deux exemplaires paralleles
    // (0x00571300.. et 0x00571370..), et porte des bits de drapeau a 0x0057139c. Le detail de ses
    // 69 champs n'est pas trace.
    // =====================================================================
    internal const int ViewParamsBase = 0x00571270;
    internal readonly byte* s_ViewParams_00571270 = (byte*)NativeMemory.AllocZeroed(0x190);
    internal byte* ViewParams(int va) => s_ViewParams_00571270 + (va - ViewParamsBase);

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

    /// <summary>
    /// Ghidra: DAT_005de02c @ 0x005de02c — <b>les drapeaux de la boucle de monde</b>, quatre octets
    /// isoles juste avant le bloc de camera.
    ///
    /// <para>
    /// Le seul bit dont le role soit etabli est <c>0x10</c> : il gele la simulation. Il est pose par
    /// <c>FreezeAllEntities</c> (opcode 18 de l'interpreteur de script) et leve par
    /// <c>UnfreezeAllEntities</c> (opcode 19), et les deux boucles de mise a jour du monde le testent
    /// avant de faire avancer quoi que ce soit (Blaze.c l. 57957 et 63975). Les bits 1, 2, 4, 8 et
    /// 0x40 sont manipules ailleurs sans que leur role soit encore lisible.
    /// </para>
    /// <para>
    /// Non initialise dans <c>.data</c> : l'octet est au-dela de la queue presente dans le fichier,
    /// donc zero au chargement, ecrit uniquement par le code. Champ simple et non arene — c'est un
    /// mot isole, et le bloc de camera commence quatre octets plus loin.
    /// </para>
    /// </summary>
    internal uint s_WorldFlags_005de02c;

    /// <summary>Ghidra: DAT_005de024 et DAT_005de028 — deux mots que le prologue de
    /// <c>FUN_00516fc0</c> remet a zero juste avant <see cref="s_WorldFlags_005de02c"/>, dans la
    /// meme rafale de <c>mov [.],ebx</c>. Trois dwords contigus, trois champs : rien n'y accede par
    /// arithmetique de pointeur, ni dans le binaire ni ici. Role non etabli.</summary>
    internal uint s_Unknown_005de024;
    internal uint s_Unknown_005de028;

    /// <summary>
    /// Ghidra: 0x005de030..0x005de0b4 — le bloc de camera et de position du groupe. Son adresse est
    /// stockee telle quelle dans <c>DAT_005edebc</c>, d'ou l'allocation native.
    ///
    /// <para>
    /// Etendu de 0x80 a 0x84 le 2026-08-21 : le prologue de <c>FUN_00516fc0</c> ecrit
    /// <c>DAT_005de0b0</c>, qui tombait exactement sur l'ancienne borne de fin. Rien ne suit
    /// jusqu'a <c>ItemMenuArena</c> (0x005de100), la place etait libre.
    /// </para>
    /// <para>
    /// <b>Defaut d'aliasing resorbe le 2026-08-22</b> — <c>s_Unknown_005de034</c>
    /// (<c>Screens/State04/CharacterCreation.cs</c>) etait un <c>static int</c> ordinaire dont
    /// l'adresse tombe DANS ce bloc. Il n'avait pas encore fait de degat parce qu'aucun site
    /// n'ecrivait <c>Camera(0x005de034)</c> ; <c>PrepareLeaderAndPartyFlags</c>
    /// (<c>FUN_0050e2b0</c>) en ecrit un a chaque image, ce qui l'a rendu actif. C'est maintenant
    /// une VUE sur ce bloc. Les tests de disposition ne l'avaient pas vu : ils comparent des
    /// symboles a suffixe de VA entre eux, pas un champ de valeur a une etendue d'arene.
    /// <b>TODO(REVERSE)</b> : etendre le test a cette comparaison-la, c'est ce qui reste le plus
    /// rentable — <c>s_Unknown_005edec4</c> et <c>s_Unknown_005f2ce0</c> tombent tous deux dans
    /// <c>EntityArena</c> de la meme facon.
    /// </para>
    /// </summary>
    internal const int CameraBlockBase = 0x005de030;
    // Etendu de 0x84 a 0x88 le 2026-08-28 : FUN_00539b80 ecrit l OCTET DAT_005de0b4, qui tombait
    // exactement sur l ancienne borne de fin — meme cas que DAT_005de0b0 le 2026-08-21. Rien ne
    // suit jusqu a ItemMenuArena (0x005de100).
    internal readonly byte* s_CameraBlock_005de030 = (byte*)NativeMemory.AllocZeroed(0x88);
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
    // bloc s'arrête juste avant DAT_005de510, qui est s_GameStateId_005de510 — d'où les 0x100 octets.
    //
    // C'est le défaut d'aliasing que ce projet a rencontré deux douzaines de fois : déclarer ces
    // adresses en champs séparés marche jusqu'au jour où un site les parcourt.
    //
    // **Etendu vers le bas le 2026-08-18** : le sous-ecran « Invertaire » (FUN_00524880) atteint
    // DAT_005de110 (le tampon de nom d'objet que FUN_0051bcc0 remplit), DAT_005de2d8 (les vingt-
    // deux drapeaux « objet retenu »), DAT_005de390 (le titre de la carte memoire en UTF-16) et
    // DAT_005de428/430/488/4a0. La base descend donc a 0x005de100 ;
    // ItemMenuState() etant adresse par VA absolue, aucun site existant ne bouge.
    //
    // **TAILLE CORRIGEE le 2026-08-21 : 0x460 -> 0x410.** Le commentaire ci-dessus raisonnait une
    // etendue « juste avant DAT_005de510 », soit 0x410 exactement, mais l'allocation en demandait
    // 0x460 — code et commentaire se contredisaient depuis avant le decoupage. Les 0x50 octets de
    // trop avalaient DAT_005de558, c'est-a-dire s_EntityList_005de558, qui s'alloue son propre
    // bloc : une ecriture par l'arene n'aurait jamais ete lue par la liste, et reciproquement.
    // C'est l'un des defauts que OpenForesia.Tests signalait.
    //
    // 0x410 est verifie des deux cotes, pas choisi : l'adresse la plus haute qu'un site adresse
    // par ItemMenuState()/ItemMenuBytes() est 0x005de50c (39 sites releves), soit 0x005de510 en
    // fin d'acces ; et 0x005de510 est precisement DAT_005de510, un autre global (s_GameStateId_005de510).
    internal const int ItemMenuArenaBase = 0x005de100;
    internal readonly byte* s_ItemMenuArena_005de100 = (byte*)NativeMemory.AllocZeroed(0x410);
    internal int* ItemMenuState(int va) => (int*)(s_ItemMenuArena_005de100 + (va - ItemMenuArenaBase));
    internal byte* ItemMenuBytes(int va) => s_ItemMenuArena_005de100 + (va - ItemMenuArenaBase);

    // =====================================================================
    // Fusionne depuis Memory/DataTables.cs le 2026-08-19.
    // =====================================================================
    // =====================================================================
    // Les huit sous-états de la sélection des personnages
    //
    // Ghidra: PTR_LAB_00560708 @ 0x00560708 — table de huit pointeurs relevée dans
    // Legacy/Binaries/Blaze.exe au décalage fichier 0x15ED08. FUN_00506880 y répartit chaque frame par
    // DAT_00578de8. Aucun de ces huit corps n'existe dans Blaze.c : seule cette table les désigne,
    // donc Ghidra ne les a jamais désassemblés. Un re-run ciblé sur ces huit adresses les produit
    // d'un coup.
    //
    // Vérifié dans le binaire : les huit sont de vrais prologues de fonction, et **sept sur huit**
    // ouvrent sur la même instruction, `mov eax, [0x00562A48]` — le drapeau « première frame de ce
    // sous-état » que LoadCharacterSelectResources met à 1 et que FUN_00506880 relève à chaque
    // changement de sous-état. Le premier lit `[0x005633A8]` à la place.
    // =====================================================================

    // Ghidra: &DAT_00560708 — la table elle-même, relevée dans Legacy/Binaries/Blaze.exe au décalage
    // fichier 0x15ED08. Les huit corps sont désormais décompilés et rangés dans
    // Legacy/Raw/Blaze/FUN_<adresse>.c ; il reste à les transcrire.
    internal readonly int[] s_CharacterSelectSubStateAddresses =
    {
        0x00507010, 0x00507cb0, 0x00508410, 0x00508860,
        0x00508e00, 0x00509470, 0x005098f0, 0x0050a910,
    };

    // Ghidra: &DAT_00560ff0 et suivants — la rangée de descripteurs d'interface que la boucle de
    // frame dessine, pas 0x8c8 octets. Trois instances statiques, relevées dans Legacy/Binaries/Blaze.exe
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

        // Les 0x68 octets initialisés de chaque instance, relevés dans Legacy/Binaries/Blaze.exe aux
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
    // Relevées dans Legacy/Binaries/Blaze.exe : mêmes 26 dwords initialisés que les autres instances, queue
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
    // passe de texte. Seize octets relevés dans Legacy/Binaries/Blaze.exe au décalage fichier 0x15ECF8 :
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
    //
    // **Recouvrement d'arene corrige le 2026-08-22.** C'etaient trois `int` autonomes, alors que
    // leurs adresses sont espacees de DEUX octets — ce sont des `short`, et ils tombent tous les
    // trois dans GameArena. Le defaut n'etait pas theorique : `Screens/State03/WorldMap.cs` remet
    // deja l'orientation a zero par `*(uint*)Arena(0x005e9308) = 0`, donc la carte du monde ecrivait
    // dans une memoire et `Engine/Model3d.cs` lisait dans l'autre. Le cas 2 du repartiteur de
    // FUN_00516fc0 (le pilotage libre de la camera) y ajoute un troisieme ecrivain, ce qui a fait
    // remonter le probleme. Ce sont maintenant des VUES.
    internal short s_ScenePitch_005e9308
    {
        get => *(short*)Arena(0x005e9308);
        set => *(short*)Arena(0x005e9308) = value;
    }
    internal short s_SceneYaw_005e930a
    {
        get => *(short*)Arena(0x005e930a);
        set => *(short*)Arena(0x005e930a) = value;
    }
    internal short s_SceneRoll_005e930c
    {
        get => *(short*)Arena(0x005e930c);
        set => *(short*)Arena(0x005e930c) = value;
    }

    // Ghidra: DAT_005e93a0 / 93a2 / 93a4 — la couleur de fond passée à Ordinal_376 (SetBackColor).
    //
    // **Recouvrement d'arene corrige le 2026-08-22, exactement comme l'orientation ci-dessus.**
    // C'etaient trois `int` autonomes pour trois adresses espacees de DEUX octets. Le
    // desassemblage de FUN_00532bf0 (0x00532e38..) tranche : `movsx eax,word [5E93A0h]` a la
    // lecture, `mov [5E93A0h],ax` a l'ecriture — ce sont des `short`, et ils tombent dans
    // GameArena. La branche `DAT_005e9166 == 1` les recopie meme par DWORD
    // (`mov eax,[5E9160h] ; mov [5E93A0h],eax`), ce qui n'est representable qu'en vue.
    //
    // La correction est **neutre en comportement aujourd'hui** : avant elle, aucun site n'ecrivait
    // ces trois symboles, ni par champ ni par arene, et les deux memoires valaient donc zero de
    // part et d'autre. C'est le lissage de couleur de fond de FUN_00532bf0 qui allait devenir le
    // premier ecrivain — donc le premier a rendre l'aliasing vivant, comme il l'etait pour
    // l'orientation. Les six lecteurs existants (CharacterCreation, CharacterSelect, SaveSlots)
    // ne changent pas : ils castent deja en `(byte)`.
    internal short s_BackColourR_005e93a0
    {
        get => *(short*)Arena(0x005e93a0);
        set => *(short*)Arena(0x005e93a0) = value;
    }
    internal short s_BackColourG_005e93a2
    {
        get => *(short*)Arena(0x005e93a2);
        set => *(short*)Arena(0x005e93a2) = value;
    }
    internal short s_BackColourB_005e93a4
    {
        get => *(short*)Arena(0x005e93a4);
        set => *(short*)Arena(0x005e93a4) = value;
    }

    // Ghidra: DAT_00604928 — trois shorts contigus, la pose de caméra. Le troisième (+2) est
    // l'angle amorti vers le personnage courant ; FUN_0051c300 consomme le tout.
    internal readonly byte* s_CameraVector_00604928 = (byte*)NativeMemory.AllocZeroed(8);

    // Ghidra: DAT_006008d0 / d4 / d8 — le décalage monde appliqué à la position de l'entité avant
    // projection. Remis à zéro à l'entrée du sous-état 0.
    //
    // **DEFAUT D'ALIASING CORRIGE le 2026-08-21, et il etait VIVANT.** Ces trois adresses tombent
    // dans l'etendue d'EntityArena (0x005eca00..0x00600c00), et le code les atteignait des DEUX
    // cotes : `Screens/State03/WorldMap.cs` ecrivait par l'arene (`Mem.Entity(0x006008d0)`),
    // tandis que `Engine/Model3d.cs` LISAIT ces champs separes et que Destination/SaveSlots les
    // remettaient a zero. Les deux memoires ne se rencontraient jamais — le decalage monde pose
    // par la carte du monde etait donc invisible a la projection qui devait s'en servir.
    //
    // Les tests de disposition ne le voyaient pas : ils ne signalent qu'une ALLOCATION propre dans
    // une arene, et ceci etait un simple champ `int`. Angle mort a ajouter a celui deja ecrit dans
    // OpenForesia.Tests/README.md.
    //
    // Ecrits en proprietes plutot qu'en champs pour que les 12 sites d'appel ne bougent pas : ils
    // lisent et ecrivent exactement comme avant, mais dans la memoire de l'arene.
    internal int s_Unknown_006008d0 { get => *(int*)Entity(0x006008d0); set => *(int*)Entity(0x006008d0) = value; }
    internal int s_Unknown_006008d4 { get => *(int*)Entity(0x006008d4); set => *(int*)Entity(0x006008d4) = value; }
    internal int s_Unknown_006008d8 { get => *(int*)Entity(0x006008d8); set => *(int*)Entity(0x006008d8) = value; }

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
    //
    // **Recouvrement d'arene corrige le 2026-08-21** : 0x006008e0 tombe dans EntityArena, donc
    // ceci s'allouait un SECOND bloc pour une adresse que l'arene couvre deja. C'est une VUE,
    // pas un stockage propre — meme regle que s_EntitySlotTable_005e9180.
    internal byte* s_CameraMatrix_006008e0 => Entity(0x006008e0);

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
    internal byte* s_EntitySlotTable_005e9180 => Arena(0x005e9180);

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

    // =====================================================================
    // Rapatriees le 2026-08-19 pour fermer Engine/Input.cs : globales .data qu'il lisait
    // dans cinq autres fichiers. Les globales PROPRES a Input restent chez lui, en champs
    // d'instance - meme regle qu'aux quatre conversions precedentes.
    // =====================================================================

    internal byte* s_AnimationTable_005e9640 => Arena(0x005e9640);

    // Ghidra: DAT_00604b54 â€” the input-device object. Created by FUN_00523120, which
    // SetupJoystickHandlers now calls; ShutdownStage1 destroys it.
    //
    // **This global had been reconstructed under three different names** â€” `s_JoystickDevice`,
    // `s_JoystickDevice_00604b54` and, briefly, an `s_InputDevice_00604b54` added while
    // reconstructing the creation chain. One address, one field: the same shape of defect as
    // DAT_00566cc4 / s_ResolutionIndex, and the reason to check for an existing name before
    // declaring one.
    internal byte* s_JoystickDevice_00604b54;

    // Ghidra: DAT_005de160, ini key "Size", default 0xb (11). Clamped in LoadOptionsIni against a
    // per-resolution table (DAT_00562e00).
    //
    // Role established 2026-08-12 (the earlier TODO(REVERSE) here is answered): this is the
    // engine's render-size level. `SetShift` (winsys3d.c:4937) hands the 3D device a viewport of
    // `(Size + 9) * 0x20` by `(Size + 9) * 0x18` and selects the vertex-scaling lookup table with
    // the same value. So Size 11 is 640x480 â€” the engine's logical reference space â€” and the
    // shipped `Size=21` is 960x720, which matches the render target measured at runtime. Full
    // derivation in `Documentation/DLL/winsys3d.md`, section "Coordinate scaling".
    internal int s_SizeSetting_005de160 = 0xb;


    // Ghidra: DAT_00589c70 (ini "SoundVol") / DAT_0057925c (ini "CdVol") / DAT_005d9de0 (ini "Gamma").
    // All three default to 0x40 (64) in GetPrivateProfileIntA.
    internal uint s_SoundVolumeIni;
    internal uint s_CdVolumeIni;
    internal uint s_GammaIni;
    /// <summary>
    /// Ghidra: DAT_005d9de0 â€” **le mÃªme global que <see cref="s_GammaIni"/>**, la clÃ© Â« Gamma Â» de
    /// l'ini, et non un second. Le commentaire de LoadOptionsIni le disait dÃ©jÃ  ; il avait quand
    /// mÃªme Ã©tÃ© redÃ©clarÃ© ici, et n'Ã©tant jamais Ã©crit il valait zÃ©ro. ConsÃ©quence visible : la
    /// page d'options passait ses palettes dans <c>Gamma(valeur, 0.0)</c>, ce qui les rendait
    /// noires â€” fond et cadre invisibles, libellÃ©s intacts parce qu'eux n'y passent pas.
    /// SeptiÃ¨me occurrence d'un global reconstruit sous deux noms.
    /// </summary>
    internal int s_Unknown_005d9de0 => (int)s_GammaIni;

    internal int s_Unknown_005de538;

    // =====================================================================
    // Rapatriees le 2026-08-21, a la conversion d'Engine/Audio.cs puis d'Engine/Resources.cs :
    // trois globales .data que DEUX classes converties se partageaient, plus Startup et la carte
    // du monde. Elles etaient restees chez leur proprietaire en `internal` le temps que les deux
    // conversions soient verifiables separement.
    //
    // ELLES ETAIENT MAL CLASSEES, ET C'EST LA LECON. La regle de tri du 2026-08-19 dit :
    // « suffixe VA -> GameMemory ; pas de suffixe -> echafaudage, reste chez son proprietaire ».
    // Ces trois-la n'ont pas de suffixe VA et sont pourtant du .data pur - c'est le NOM qui ment,
    // pas la regle qui manque un cas. L'ancre `// Ghidra: DAT_...` du commentaire tranche, le nom
    // C# n'est qu'un indice. Corrige dans TODO.md.
    // =====================================================================

    /// <summary>
    /// One entry of the sound directory at Ghidra <c>DAT_005e8340</c>: a byte offset into the
    /// sample bank selected by the sound id, and the sample's length in bytes.
    ///
    /// <para>
    /// La structure suit son tableau : elle decrit la disposition d'une entree de <c>.data</c>,
    /// donc sa place est ici et non dans l'un des deux consommateurs. La laisser dans
    /// <c>Audio</c> aurait oblige <c>Memory/</c> a dependre d'<c>Engine/</c>.
    /// </para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SoundDirectoryEntry
    {
        public int Offset;
        public int Length;
    }

    // Ghidra: DAT_005e8340 (0x1e0 octets, lus depuis "BASE.SND") immediatement suivi de
    // DAT_005e8520 (0x88 octets, lus depuis le fichier de niveau par FUN_00512xxx @ 0x006335x).
    // C'est UN SEUL tableau contigu : 0x005e8520 - 0x005e8340 == 0x1e0 exactement, et
    // PlaySoundEffect indexe les DEUX moities depuis &DAT_005e8340 (`&DAT_005e8340 + soundId * 8`
    // jusqu'a l'id 0x4c), en ne changeant que la banque a laquelle le decalage de l'entree est
    // ajoute. 0x1e0 + 0x88 == 0x268 == 77 entrees de 8 octets, et 0x4c est exactement le dernier
    // index valide : les tailles se corroborent.
    internal const int SoundDirectoryBaseCount = 0x1e0 / 8;  // 60 entrees, ids 0x00..0x3b
    internal const int SoundDirectoryCount = 0x268 / 8;      // 77 entrees, ids 0x00..0x4c
    internal readonly SoundDirectoryEntry* s_SoundDirectory_005e8340 =
        (SoundDirectoryEntry*)NativeMemory.AllocZeroed(SoundDirectoryCount * (nuint)sizeof(SoundDirectoryEntry));

    // Ghidra: DAT_00604920 / DAT_00600900 — les deux banques d'echantillons PCM 8 bits brutes,
    // allouees dans RunApplication et remplies directement depuis le disque. Leur role est etabli
    // par PlaySoundEffect (FUN_0053cea0), qui ajoute le decalage d'une entree de repertoire a
    // l'une de ces deux bases pour obtenir l'adresse de l'echantillon : la banque « base » porte
    // les ids 0..0x3b (chargee depuis "\SND\BASE.SND", avec les 0x1e0 premiers octets du
    // repertoire), la banque « niveau » porte les ids 0x3c..0x4c (chargee par FUN_00512xxx avec
    // les 0x88 octets restants).
    //
    // Elles restent ALLOUEES PAR RunApplication, pas par ce constructeur : l'allocation a lieu au
    // meme instant qu'avant, seul le champ qui la recoit a demenage. Ce ne sont donc que des
    // emplacements ici, volontairement laisses nuls a la construction.
    internal void* s_BaseSoundBank_00604920;   // 790000 octets
    internal void* s_LevelSoundBank_00600900;  // 260000 octets

    // =====================================================================
    // Rapatriees le 2026-08-21, a la correction des defauts d'aliasing. Ce sont des globales
    // .data que la reconstruction avait allouees DEUX FOIS, une par lecteur - le defaut que
    // OpenForesia.Tests signalait. Les reunir ICI est ce qui garantit qu'il n'y en a plus qu'une.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00566c70</c> — le rectangle passe par adresse a <c>Ordinal_770</c> et
    /// <c>Ordinal_766</c>.
    ///
    /// <para>
    /// <b>Il etait alloue deux fois</b> : <c>s_MenuRenderBlock_00566c70</c> (0x40 octets, dans
    /// <c>MainMenu.cs</c>, ecrit par <c>LoadMenuResources</c>) et <c>s_ViewRect_00566c70</c>
    /// (0x10 octets, dans <c>WorldMap.cs</c>). Les deux chemins ecrivent les MEMES champs
    /// (<c>_DAT_00566c74</c>, <c>_DAT_00566c78</c>, <c>_DAT_00566c7a</c>) puis passent
    /// <c>&amp;DAT_00566c70</c> aux deux memes ordinaux — <c>Blaze.c</c> 63665/63669 et
    /// 89424/89428. C'est une seule memoire dans le binaire.
    /// </para>
    /// <para>
    /// <b>Taille 0x14, et elle est deduite, pas choisie.</b> La DLL ne la contraint pas : les
    /// ordinaux 766 et 770 sont des souches <c>return 0;</c> (winsys3d.md, table des souches),
    /// elles ne dereferencent jamais le pointeur. Restent deux bornes : les ecritures vont
    /// jusqu'a <c>+0xb</c>, et le symbole suivant est <c>DAT_00566c84</c>, soit 0x14 plus loin.
    /// 0x14 est donc le maximum que l'original pouvait occuper sans recouvrir son voisin.
    /// </para>
    /// </summary>
    internal readonly byte* s_ViewRect_00566c70 = (byte*)NativeMemory.AllocZeroed(0x14);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005de730</c> — les quatre dernieres faces retenues, une par emplacement
    /// de reference de face de l'entite. <c>FUN_00533940</c> les met a -1 en entree et s'en sert
    /// pour ne pas repartir en boucle sur une face deja visitee.
    ///
    /// <para>
    /// <b>Quatre shorts, et la taille est deduite de trois cotes qui concordent.</b> L'original
    /// initialise par DEUX ecritures de dword (<c>_DAT_005de730 = -1</c>, <c>_DAT_005de734 = -1</c>)
    /// mais relit par <c>(&amp;DAT_005de730)[local_50]</c> en shorts — donc 8 octets vus comme
    /// quatre shorts. L'indice <c>local_50</c> vaut <c>4 - local_4c</c> avec <c>local_4c</c> dans
    /// [1,4], ou le type de face qui indexe <c>slot + 0x80 + type * 4</c>, lui aussi borne a 4
    /// entrees : dans les deux cas 0..3. Et le symbole suivant, <c>s_Unknown_005de738</c>, est a
    /// exactement 8 octets.
    /// </para>
    /// <para>
    /// TODO(REVERSE): la region 0x005de700..0x005de72f est referencee par <c>Blaze.c</c> (une
    /// dizaine de <c>DAT_</c>) mais pas encore reconstruite. Si un site venait a l'indexer d'un
    /// bout a l'autre, ce bloc devrait la rejoindre plutot que rester isole — c'est la forme
    /// habituelle du defaut d'aliasing de ce depot.
    /// </para>
    /// </summary>
    internal readonly short* s_FaceWalkHistory_005de730 = (short*)NativeMemory.AllocZeroed(8);

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00564548</c> — huit demi-mots, un par octant de cap, relevés dans le
    /// binaire le 2026-08-23. L'octet bas est le décalage en X du coin de départ du balayage 7×7,
    /// l'octet haut son décalage en Z ; les deux se comptent en cellules à partir de la cellule de
    /// la caméra.
    ///
    /// <para>Table et non arène : ce sont seize octets d'initialisé, immédiatement suivis des huit
    /// tranches de <see cref="s_CellSkipTables_00564558"/>. Deux lecteurs — le balayage de
    /// <c>FUN_00516fc0</c> et celui, imbriqué, de <c>FUN_0051fa40</c> — d'où la place ici plutôt
    /// que chez l'un des deux.</para>
    /// </summary>
    internal static ReadOnlySpan<ushort> s_CellSweepCorner_00564548 =>
        [0x0403, 0x0402, 0x0302, 0x0202, 0x0203, 0x0204, 0x0304, 0x0404];

    /// <summary>
    /// Ghidra: <c>PTR_DAT_005645d8</c> et ce qu'elle pointe — huit pointeurs vers huit tables de
    /// 0x10 octets, relevés dans le binaire le 2026-08-23 : ils désignent <c>0x00564558</c>,
    /// <c>68</c>, <c>78</c>, <c>88</c>, <c>98</c>, <c>a8</c>, <c>b8</c>, <c>c8</c>, c'est-à-dire
    /// <b>huit tranches contiguës</b> qui s'arrêtent pile où commence la table de pointeurs
    /// elle-même. Elles sont donc modélisées d'un bloc : le code y avance au pointeur et pourrait
    /// déborder d'une tranche sur la suivante.
    ///
    /// <para>Chaque tranche est une suite de <b>huit clés de 16 bits</b> <c>rang &lt;&lt; 8 |
    /// colonne</c>, dans l'ordre exact où le balayage visite ses 49 cellules : ce sont les coins
    /// que l'octant de cap met hors du cône de vue.</para>
    /// </summary>
    internal static ReadOnlySpan<byte> s_CellSkipTables_00564558 =>
    [
        0x07, 0x03, 0x01, 0x03, 0x07, 0x02, 0x01, 0x02, 0x07, 0x01, 0x06, 0x01, 0x02, 0x01, 0x01, 0x01,
        0x07, 0x07, 0x01, 0x07, 0x07, 0x06, 0x07, 0x02, 0x07, 0x01, 0x06, 0x01, 0x02, 0x01, 0x01, 0x01,
        0x07, 0x07, 0x06, 0x07, 0x05, 0x07, 0x07, 0x06, 0x07, 0x02, 0x07, 0x01, 0x06, 0x01, 0x05, 0x01,
        0x07, 0x07, 0x06, 0x07, 0x02, 0x07, 0x01, 0x07, 0x07, 0x06, 0x07, 0x02, 0x07, 0x01, 0x01, 0x01,
        0x07, 0x07, 0x06, 0x07, 0x02, 0x07, 0x01, 0x07, 0x07, 0x06, 0x01, 0x06, 0x07, 0x05, 0x01, 0x05,
        0x07, 0x07, 0x06, 0x07, 0x02, 0x07, 0x01, 0x07, 0x01, 0x06, 0x01, 0x02, 0x07, 0x01, 0x01, 0x01,
        0x03, 0x07, 0x02, 0x07, 0x01, 0x07, 0x01, 0x06, 0x01, 0x02, 0x03, 0x01, 0x02, 0x01, 0x01, 0x01,
        0x07, 0x07, 0x01, 0x07, 0x01, 0x06, 0x01, 0x02, 0x07, 0x01, 0x06, 0x01, 0x02, 0x01, 0x01, 0x01,
    ];

    /// <summary>
    /// La cle de saut de 16 bits au curseur d'octet donne dans
    /// <see cref="s_CellSkipTables_00564558"/> — **y compris au-dela de la huitieme tranche**.
    ///
    /// <para><b>Pourquoi une aide plutot qu'une indexation directe.</b> L'original ne borne rien :
    /// il tient un <c>short*</c> qui part de <c>PTR_DAT_005645d8[octant] + 2</c> et avance d'un
    /// short a chaque cellule sautee (<c>Blaze.c</c> 68686-68687). Or la huitieme tranche — celle
    /// de l'octant 7 — finit sur la cle <c>(rang 1, colonne 1)</c>, qui est la **derniere cellule
    /// visitee** par le balayage. Le corps lit donc une neuvieme cle qui tombe deux octets apres la
    /// fin du bloc, en <c>0x005645d8</c>, c'est-a-dire dans <c>PTR_DAT_005645d8</c> elle-meme.</para>
    ///
    /// <para>Cette cle n'est jamais consultee : les deux boucles se terminent juste apres. Le
    /// debordement est donc sans consequence dans l'original — mais un <c>ReadOnlySpan</c> C# leve
    /// la ou une lecture x86 ne bronchait pas, et c'est ce qui faisait planter le rendu d'un
    /// modele des que la camera tombait dans l'octant 7. Voir <c>legacy-bug.md</c>, LB-013.</para>
    ///
    /// <para>Ce que l'aide rend au-dela de la fin, ce sont donc les <b>vrais octets</b> de
    /// <c>PTR_DAT_005645d8</c> : huit pointeurs de quatre octets vers les huit tranches ci-dessus,
    /// espaces de <c>0x10</c> (<c>0x00564558</c> .. <c>0x005645c8</c>), releves dans le binaire le
    /// 2026-08-25. La premiere cle debordee vaut ainsi <c>0x4558</c>, la moitie basse du premier
    /// pointeur — pas un garde, pas un zero de commodite.</para>
    /// </summary>
    internal static int NextCellSkipKey(int cursor)
    {
        ReadOnlySpan<byte> table = s_CellSkipTables_00564558;
        if (cursor + 1 < table.Length) return table[cursor] | table[cursor + 1] << 8;

        int over = cursor - table.Length;
        return TrailingPointerByte_005645d8(over) | TrailingPointerByte_005645d8(over + 1) << 8;
    }

    /// <summary>Un octet de <c>PTR_DAT_005645d8</c>, la table de pointeurs qui suit immediatement
    /// les huit tranches de saut. Voir <see cref="NextCellSkipKey"/>.</summary>
    private static int TrailingPointerByte_005645d8(int offset)
    {
        if ((uint)offset >= 32)
        {
            throw new NotImplementedException(
                $"PTR_DAT_005645d8 : le curseur de saut a depasse la table de pointeurs (octet {offset}) — " +
                "au-dela de 0x005645f8 la .data n'est pas relevee.");
        }

        uint pointer = 0x00564558u + (uint)(offset >> 2) * 0x10u;
        return (int)(pointer >> ((offset & 3) * 8)) & 0xff;
    }
}
