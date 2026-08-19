// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
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
    private const int GameArenaBase = 0x005e9000;
    // Etendue de 0x0a00 a 0x1600 le 2026-08-18 : FUN_00516350 atteint 0x005e9b87 et y efface 0x40
    // dwords par pas de 0x28, soit jusqu'a 0x005ea587.
    private const int GameArenaSize = 0x1600;
    private static readonly byte* s_GameArena_005e9000 = (byte*)NativeMemory.AllocZeroed(GameArenaSize);

    /// <summary>Le symbole d'adresse Ghidra <paramref name="va"/> dans l'arène.</summary>
    private static byte* Arena(int va) => s_GameArena_005e9000 + (va - GameArenaBase);

    // Les huit banques de 0x40 octets, dans l'ordre où le bloc de restauration de FUN_00536ea0 les
    // recopie depuis la sauvegarde (source : enregistrement +0x1d00..+0x1eff, huit banques
    // contiguës là-bas, dispersées ici).
    private static byte* s_StateBank0_005e96e0 => Arena(0x005e96e0);
    private static byte* s_StateBank1_005e9780 => Arena(0x005e9780);
    private static byte* s_StateBank2_005e9060 => Arena(0x005e9060);
    private static byte* s_StateBank3_005e9840 => Arena(0x005e9840);
    private static byte* s_StateBank4_005e9720 => Arena(0x005e9720);
    private static byte* s_StateBank5_005e92c0 => Arena(0x005e92c0);
    private static byte* s_StateBank6_005e9980 => Arena(0x005e9980);
    private static byte* s_StateBank7_005e9360 => Arena(0x005e9360);

    private const int EnvBlockSize = 224;   // 0x005e9960 - 0x005e9880
    private static byte* s_EnvBlock_005e9880 => Arena(0x005e9880);

    private static readonly byte* s_DrawEnv0_005e9880 = s_EnvBlock_005e9880 + 0x00;
    private static readonly byte* s_DispEnv0_005e98dc = s_EnvBlock_005e9880 + 0x5C;
    private static readonly byte* s_DrawEnv1_005e98f0 = s_EnvBlock_005e9880 + 0x70;
    private static readonly byte* s_DispEnv1_005e994c = s_EnvBlock_005e9880 + 0xCC;

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
    private static int s_DebugModeLatch_005633a8 = 1;

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
    private static readonly byte* s_CdDataRootPath = ToAnsiZ("@:\\FRENCH\\");

    // Ghidra: DAT_0056321c â€” the CD-check file's size, plus a fixed 0xd8000000 added once right
    // before RunMainLoop. Read back by Handler_00544810 (UI text control code 0x01), where
    // `(DAT_0056321c + 1)` multiplies a returned text-cursor pointer â€” see the TODO(REVERSE) there.
    private static uint s_CdCheckFileSize_0056321c;

    /// <summary>
    /// Ghidra: FUN_00529c10 @ 0x00529c10 â€” Blaze &amp; Blade's own "RunMainLoop": plays the four
    /// intro movies (gracefully skipped if the movie subsystem can't be opened, exactly like the
    /// original's own handling of a failed open), re-initializes engine/device state, loads the
    /// UI font from the CD, sets up the viewport, and finally hands off to the real per-frame game
    /// loop (FUN_0052a3d0).
    /// </summary>
    private static void RunMainLoop()
    {
        // La fenÃªtre du jeu naÃ®t ici, avant l'Ã©cran-titre â€” pas de device, il attend le premier
        // dÃ©but de scÃ¨ne. Hors reconstruction : le binaire d'origine n'a pas de fenÃªtre Ã  lui Ã  ce
        // stade, il dessine dans l'affichage exclusif. Voir la change `render-title-screen-in-window`.
        DeviceShim.CreateGameWindow();

        InitMoviePlayer();
        bool streamOpened = TryOpenMovieStream();
        if (streamOpened)
        {
            if (PlayMovie(s_MovieNameTeps, 1) == 0)
            {
                PlayMovie(s_MovieNameOpening, 3);
            }

            // Ghidra: Blaze.c:74441-74451. The attract loop has FOUR exit conditions, one per step â€”
            // title screen, ANI01, title screen, OPENING â€” and every one of them must stay, even
            // while a step is degraded. Dropping the two ShowTitleScreenAndWait checks (Ghidra types
            // that thunk `void`, but the call site reads its result) is what previously made this
            // loop unbreakable, since PlayMovie always reports 0 while TrySetupMovieStream is a stub.
            while (true)
            {
                if (ShowTitleScreenAndWait() != 0) break;
                if (PlayMovie(s_MovieNameAni01, 3) != 0) break;
                if (ShowTitleScreenAndWait() != 0) break;
                if (PlayMovie(s_MovieNameOpening, 3) != 0) break;
            }
        }
        CloseMovieStreamIfOpen();

        Ordinal_129();
        Ordinal_41();
        Ordinal_45(null, s_CdDataRootPath); // Ghidra: apcStack_54[1]="j" â€” TODO(REVERSE): odd literal, but Ordinal_45 is a confirmed no-op regardless
        Ordinal_136(0);
        s_RandSeed_0056e9c0 = 1; // Ghidra: FUN_00547130(1) â€” srand(1); see CrtRand
        s_Unknown_005e9630 = Ordinal_116();
        Ordinal_771();
        InitTrigTables(); // Ordinal_337, re-run (idempotent â€” see winsys3d.md)
        // Ghidra: Ordinal_789(0, DAT_00566cc4) â€” Blaze.c line 79100. The second argument is **not**
        // inert, as an earlier note here claimed: winsys3d.c FUN_00a06420 opens with
        // `DAT_00a2c49c = param_2`, and that global indexes the base-offset tables SetShift uses to
        // position the viewport. Passing 0 selected the 320x240 entry (-160,-120), which is why the
        // scene rendered unscaled in a corner of a 640x480 screen.

        // `Ordinal_789` charge `3dhelp.dll` et lui fait crÃ©er son device, ce qui bascule le mode
        // d'affichage du bureau. Le device synthÃ©tique intercepte cette crÃ©ation. Voir
        // `SyntheticDevice.cs` ; opt-in tant qu'il n'est pas Ã©prouvÃ©.
        SyntheticDevice.InstallSyntheticDevice();

        Ordinal_789(0, s_ResolutionIndexForClamp);
        DeviceShim.InstallDeviceShim();   // ECHAFAUDAGE TEMPORAIRE â€” voir DeviceShim.cs
        DeviceShim.LogDisplayMode("aprÃ¨s init 3D"); // ECHAFAUDAGE TEMPORAIRE
        ShutdownMoviePlayer(); // Ghidra: FUN_00542f90()
        CheckDeviceCapabilityFlag(); // Ghidra: FUN_0050f120()
        Ordinal_110(0);
        Ordinal_681(0);
        SetShadingDefaults(); // Ghidra: FUN_00529e40()
        InitViewportSurfaces(); // Ghidra: FUN_00529eb0()
        // Ghidra: FUN_0040a600() â€” confirmed no-op (`return;` with an empty body).
        SetupJoystickHandlers(); // Ghidra: FUN_00504460()

        int detail = s_SizeSetting_005de160;
        SetShift((0xb - detail) * 0x10, (0xb - detail) * 0xc, detail, 0);

        LoadUiFontFromCd();

        Ordinal_710(0x3c0, 0);
        int consoleChannel = Ordinal_668(8, 0x20, 0xe0, 200, 0, 0x200);
        Ordinal_720(consoleChannel);
        Ordinal_92(null, null);
        s_Unknown_005e97f0 = Ordinal_104(null);
        s_ScreenStride_005e97e0 = 0x1010;
        byte* mainMem = GetMainMemPtr();
        s_MainResourceBuffer_005e97c0 = mainMem != null ? mainMem + 0xe0000 : null;
        s_Unknown_005e9820 = 0;

        Ordinal_132(0);
        Ordinal_712(1);

        LoadFirstLevelResources(); // Ghidra: FUN_0052a030()
        RunGameLoop();            // Ghidra: FUN_0052a3d0()

        Ordinal_138();
    }

    // Ghidra: DAT_0056e9c0 (set by FUN_00547130) / _DAT_005e9630 (set by the confirmed-no-op
    // Ordinal_116, always 0) / DAT_005e97f0 (set by the confirmed-no-op Ordinal_104, always 0) /
    // DAT_005e97e0 (screen stride/pitch?) / _DAT_005e9820 â€” all read again by not-yet-reconstructed
    // rendering code.
    private static uint s_RandSeed_0056e9c0; // Ghidra: DAT_0056e9c0 â€” the CRT rand seed
    private static uint s_Unknown_005e9630;
    private static uint s_Unknown_005e97f0;
    private static uint s_ScreenStride_005e97e0;
    // Ghidra: DAT_005e97c0 â€” renamed from an earlier "screen/back-buffer base" guess once
    // FUN_0052a030 (LoadFirstLevelResources) showed it's really the destination buffer for the
    // top-level "<cd-drive-root>SYSTEM.INI"-equivalent resource-file load, immediately after
    // RunMainLoop sets it to `mainMem + 0xe0000` here â€” a fixed scratch address inside winsys3d's
    // arena reserved for that blob (LoadFirstLevelResources then treats it as a struct: offsets
    // 0x10/0x48/0x6c/100 are read back as sub-tables/fields once loaded).
    private static byte* s_MainResourceBuffer_005e97c0;
    private static int s_Unknown_005e9820;

    /// <summary>
    /// Ghidra: FUN_00547130 @ 0x00547130 â€” trivial setter, folded directly into RunMainLoop above
    /// (`s_Flag_0056e9c0 = 1`) rather than kept as a separate one-line method.
    /// </summary>

    /// <summary>
    /// Ghidra: the font-loading sequence inlined at the end of FUN_00529c10 (no separate function â€”
    /// builds "&lt;cd-drive-root&gt;DATA\FONT.BMP"-style path via string concatenation, opens it with
    /// Ordinal_331/333/334/336, which are the same thin CreateFileA/SetFilePointer/ReadFile/
    /// CloseHandle wrappers already confirmed for OpenExistingFileReadOnly elsewhere â€” called via
    /// real Win32 here for consistency with that precedent â€” reads a chunk into the engine's main
    /// memory arena, then builds a font/glyph table via ordinals 92/104/132/712/720 and the two
    /// FUN_0052a030/FUN_0052a3d0 hand-off below).
    /// </summary>
    private static void LoadUiFontFromCd()
    {
        // Ghidra: s_DATA_Font_bmp_005684cc â€” only the derived symbol name survives; best guess below.
        byte* fontFileName = ToAnsiZ("DATA/Font.bmp"); // recovered verbatim from Legacy/Blaze.exe @ 0x005684cc â€” forward slash, mixed case
        byte* path = stackalloc byte[64];
        int i = 0;
        for (byte* p = s_CdDataRootPath; *p != 0 && i < 60; p++, i++) path[i] = *p;
        for (byte* p = fontFileName; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;

        IntPtr hFile = OpenExistingFileReadOnly(path);
        if ((int)hFile < 0) return; // TODO(REVERSE): original doesn't appear to guard this open failing either

        SetFilePointer(hFile, 0x3e, null, 0 /* FILE_BEGIN */);
        byte* mainMem = GetMainMemPtr();
        if (mainMem != null)
        {
            uint bytesRead;
            ReadFile(hFile, mainMem + 0x1ad8f0, 0xd48, &bytesRead, null);
        }
        CloseHandle(hFile);
    }
}
