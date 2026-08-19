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
    // Game state 0 â€” the main menu (Ghidra: FUN_00542130 and its subtree).
    //
    // Rendering follows the PlayStation display-list model the rest of winsys3d already showed:
    // FrameBegin prepares a primitive buffer and an ordering table, each renderer links its
    // primitive into the OT and returns the advanced buffer cursor, and FrameEnd walks the OT and
    // swaps buffers. The OT pointer is threaded through return values â€” that threading *is* the
    // display list, so it is modelled as written rather than hidden behind a field.
    // =====================================================================

    // Ghidra: DAT_005e9300 â€” which of the two draw envs is current, i.e. the back/front selector.
    private static byte* s_CurrentDrawEnv;
    // Deplace dans GameMemory le 2026-08-19 : globale .data.
    // Ghidra: DAT_005e9818 — le **budget de primitives par frame**, identifié en reconstruisant
    // FUN_0051d1f0. FrameBegin le recharge à 0x9c4 (2500) et chaque primitive émise le décrémente ;
    // à zéro, les boucles d'émission s'arrêtent net. Il n'est écrit nulle part ailleurs, et son
    // adresse tombe dans la queue non initialisée de .data — donc sans la remise à 2500 de
    // FrameBegin, plus rien ne se dessinerait du tout.
    private static int s_Unknown_005e9818;
    private static uint s_FadeCounter_005e9960;      // Ghidra: DAT_005e9960
    private static uint s_Unknown_005e9814;
    private static int s_Unknown_005de154, s_Unknown_005de158, s_Unknown_005de15c;
    private static int s_Unknown_005de51c;
    /// <summary>
    /// The menu's selected entry â€” <c>menuContext[0x8c4]</c>, i.e. address 0x0056e61c.
    /// <para>
    /// **Ghidra names `DAT_005e909f` as the switch selector here, and that is wrong.** Two facts
    /// rule it out: that global is written in exactly two places in all of `Blaze.c`, both `= 0`
    /// and both in unrelated entity/sound code, so the switch could only ever pick case 0; and a
    /// per-frame diff of the descriptor (`OPENFORESIA_TRACE_MENU=1`) shows **offset 0x8c4** stepping
    /// 0 â†’ 1 â†’ 2 in exact time with the arrow presses that move the cursor on screen.
    /// </para>
    /// <para>
    /// 0x0056e61c is three bytes before `DAT_0056e61f`, the flags byte the same `if` reads and
    /// which was itself found to be `menuContext[0x8c7]` â€” one dword, two fields. Ghidra never
    /// named 0x0056e61c at all, because nothing reaches it as an absolute address; it is only ever
    /// `descriptor + 0x8c4`. Same shape of defect as the four before it, and the sixth overall.
    /// </para>
    /// </summary>
    private static int s_MenuSelection_0056e61c => s_MenuContext_0056dd58[0x8c4];
    // Ghidra names DAT_0056e61f as its own symbol, but it is **inside** the menu context:
    // 0x0056e61f - 0x0056dd58 = 0x8c7, the descriptor's flags byte. Modelling it separately made
    // the confirm gate always open, so the menu selected an entry on its very first frame. Bit 2
    // is the one Handler_00544b10 raises when the text stream ends.
    private static byte s_Unknown_0056e61f => s_MenuContext_0056dd58[0x8c7];
    /// <summary>
    /// Ghidra: DAT_005d9de0 â€” **le mÃªme global que <see cref="s_GammaIni"/>**, la clÃ© Â« Gamma Â» de
    /// l'ini, et non un second. Le commentaire de LoadOptionsIni le disait dÃ©jÃ  ; il avait quand
    /// mÃªme Ã©tÃ© redÃ©clarÃ© ici, et n'Ã©tant jamais Ã©crit il valait zÃ©ro. ConsÃ©quence visible : la
    /// page d'options passait ses palettes dans <c>Gamma(valeur, 0.0)</c>, ce qui les rendait
    /// noires â€” fond et cadre invisibles, libellÃ©s intacts parce qu'eux n'y passent pas.
    /// SeptiÃ¨me occurrence d'un global reconstruit sous deux noms.
    /// </summary>
    private static int s_Unknown_005d9de0 => (int)s_GammaIni;

    private static int s_Unknown_00571a3c;

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
    private static int s_Unknown_00564a4c = 1;

    /// <summary>
    /// Ghidra: FUN_0052a1e0 @ 0x0052a1e0 â€” begins a frame: picks the scratch bases out of MainMem
    /// according to which draw env is current, clears a 1024-entry ordering table
    /// (<c>Ordinal_688</c>, libgpu's ClearOTagR) and resets the primitive cursor.
    /// <para>
    /// Ghidra recovers only two parameters where the call sites pass three, and renders both
    /// writes as <c>*param_1</c>. **Confirmed against the disassembly**
    /// (`Legacy/Raw/FUN_0052a1e0.asm`): the function has no prologue, so its arguments sit at
    /// `[ESP+4]`, `[ESP+8]` and `[ESP+0xc]`, and `0052a257 MOV ECX,[ESP+0xc]` shows the second
    /// write targets **arg3** â€” the primitive buffer. Plain `RET` with the caller's `ADD ESP,0xc`
    /// makes it cdecl with three arguments.
    /// </para>
    /// </summary>
    private static void FrameBegin(uint** otBase, uint** otEntry, uint** primCursor)
    {
        // Every caller uses all three out-parameters immediately, so they must be written on every
        // path. Returning early without setting them left uninitialised stack in the caller and
        // took DrawOTag down with a null-page access â€” a defect in this reconstruction, not in the
        // original, which has no such early exit.
        *otBase = null;
        *otEntry = null;
        *primCursor = null;

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "FUN_0052a1e0: winsys3d.dll's MainMem export resolves to null, so the frame's " +
                "ordering table and primitive buffer have nowhere to live â€” see REVERSE_STATUS.md " +
                "TODO #33. Every frame is built inside that arena, so this cannot degrade quietly.");
        }

        bool firstEnv = s_CurrentDrawEnv == Mem.s_DrawEnv0_005e9880;

        uint* ot = (uint*)(mainMem + (firstEnv ? 0x1c7000 : 0x1c6000));
        *otBase = ot;
        *otEntry = (uint*)((byte*)ot + 0xffc);   // Ghidra: *param_2 = iVar1 + 0xffc
        Winsys3d.Ordinal_688(ot, 0x400);                   // ClearOTagR(ot, 1024)
        Mem.s_Unknown_005e8310 = 0;

        *primCursor = (uint*)(mainMem + (firstEnv ? 0x1e4000 : 0x1c8000));
        s_Unknown_005e9818 = 0x9c4;

        // Ghidra: FUN_00533000(DAT_005e97c0) â€” palette animation, once per frame.
        AnimatePalettes(Mem.s_MainResourceBuffer_005e97c0);
    }

    /// <summary>
    /// Ghidra: FUN_0052a280 @ 0x0052a280 â€” ends a frame: advances the frame counter, swaps the
    /// draw/display env pair, walks the ordering table and immediately begins the next frame.
    /// <para>
    /// Ghidra recovers **no** parameters for this function, yet every call site passes five. The
    /// mapping below is **confirmed against the disassembly** (`Legacy/Raw/FUN_0052a280.asm`):
    /// tracing `SUB ESP,0x14` plus two `PUSH`es puts the arguments at `[ESP+0x20]`..`[ESP+0x30]`,
    /// giving `Ordinal_701(*arg2)`, `Ordinal_132(arg4)`, the guard on `arg5`, and the tail call
    /// `FUN_0052a1e0(arg1, arg2, arg3)` â€” cdecl, three arguments, `ADD ESP,0xc`.
    /// </para>
    /// </summary>

    private static void FrameEnd(uint** otBase, uint** otEntry, uint** primCursor, int mode, int flag)
    {
        CrtRand();                             // Ghidra: FUN_00547140() â€” result discarded here

        s_FadeCounter_005e9960++;

        int cosine = Winsys3d.GetCosTableEntry((int)(s_FadeCounter_005e9960 * 0x200));
        sbyte level = (sbyte)((cosine >> 6) - 0x80);
        s_Unknown_005e9814 = (uint)(((byte)level << 16) | ((byte)level << 8) | (byte)level);

        Winsys3d.Ordinal_667(0);

        // Ghidra: puVar3 = &DAT_005e9880; if (DAT_005e9300 == &DAT_005e9880) puVar3 = &DAT_005e98f0;
        // â€” the buffer swap: pick the *other* draw env, then publish its matching disp env at +0x5c.
        byte* nextDrawEnv = s_CurrentDrawEnv == Mem.s_DrawEnv0_005e9880
            ? Mem.s_DrawEnv1_005e98f0
            : Mem.s_DrawEnv0_005e9880;
        s_CurrentDrawEnv = nextDrawEnv;

        Winsys3d.Ordinal_657(nextDrawEnv + 0x5c);       // the DISPENV sits 0x5c past the DRAWENV
        Winsys3d.Ordinal_660(nextDrawEnv);

        FlushDeferredImageUploads();           // Ghidra: FUN_00545140()

        Winsys3d.Ordinal_701(*otEntry);                 // DrawOTag â€” asm: Winsys3d.Ordinal_701(*arg2)

        // asm 0052a314: MOV ECX,[ESP+0x2c] â€” that slot is **arg4**, not arg5. The `if` below tests
        // [ESP+0x30], which is arg5. An earlier reading passed the same value to both.
        Winsys3d.Ordinal_132(mode);

        if (flag != 0)
        {
            PollInputAndAudio();               // Ghidra: FUN_00512520 / FUN_005048f0 /
                                               //         FUN_00504a30 / FUN_005357a0
        }

        FrameBegin(otBase, otEntry, primCursor);

        // Ghidra: if (DAT_00571a3c != 0) draw the "Veuillez connecter la manette !" banner.
        if (s_Unknown_00571a3c != 0)
        {
            int* bannerCursor = stackalloc int[3];
            bannerCursor[0] = (int)(nint)s_GlyphAtlas_00566c28;
            bannerCursor[1] = 0x01000000;
            bannerCursor[2] = 0x00640010;

            *primCursor = DrawTextRun(*otBase, *primCursor, bannerCursor,
                s_ControllerBannerText_00568510, 0, 0, -1);
        }
    }

    // Ghidra: &DAT_00566c28 â€” confirmed via the `at` PE-inspector verb against Legacy/Blaze.exe
    // (.data, not a code-only-decompile gap): dword0 packs an 8x8 glyph cell (origin Y=0x30, cell
    // 8x8), dword1=0x02300000 is the atlas attributes/texture-page word DrawTextRun forwards into
    // DrawSpriteVariant.
    //
    // Promu en global 2026-08-17 : c'est l'atlas de texte partagé du jeu, pas une donnée propre à
    // la bannière manette — RunDebugPartySelect s'en sert pour ses dix appels à DrawTextRun, et il
    // le réécrit dans son bloc de dessin exactement comme l'original recopie l'adresse.
    private static readonly uint* s_GlyphAtlas_00566c28 = AllocGlyphAtlas(0x40803000, 0x02300000);

    private static uint* AllocGlyphAtlas(uint packing, uint attributes)
    {
        uint* p = (uint*)NativeMemory.Alloc(8);
        p[0] = packing;
        p[1] = attributes;
        return p;
    }

    /// <summary>Ghidra: FUN_00529a80 @ 0x00529a80 â€” screen/viewport setup for the menu state.</summary>
    private static void SetupMenuScreen()
    {
        // Ghidra: a small struct at DAT_00566c48 filled with 0xcf / 0x3fff clip values, then two
        // ordinal calls. Modelled as a raw block because none of its fields are individually named.
        uint* block = s_MenuScreenBlock_00566c48;
        block[0] = 0xcf;
        *(ushort*)((byte*)block + 4) = 0x3fff;
        *(ushort*)((byte*)block + 6) = 0x3fff;
        *(ushort*)((byte*)block + 8) = 0;
        *(ushort*)((byte*)block + 0x0a) = 0;
        *(ushort*)((byte*)block + 0x10) = 0x3fff;
        *(ushort*)((byte*)block + 0x12) = 0x3fff;

        Winsys3d.Ordinal_762(block);
        Winsys3d.Ordinal_97(0xc, 0, 0);
    }

    // Deplace dans GameMemory le 2026-08-19 : globale .data.

    // Ghidra: DAT_005e97c8 â€” the tail pointer LoadResourceEntry returns for the state's own load.
    private static byte* s_Unknown_005e97c8;

    /// <summary>Ghidra: DAT_005e97d0 — le pendant de s_Unknown_005e97c8 pour le menu objets ;
    /// voisin immédiat, même rôle : la poignée que le chargeur de ressources rend.</summary>
    private static byte* s_Unknown_005e97d0;


    // Ghidra: _DAT_0054d074 â€” the float the gamma value is scaled by before CreateTranslationTables.
    // TODO(REVERSE): .rdata content; 1.0f keeps the product equal to the raw setting.
    private const float s_GammaScale_0054d074 = 1.0f;

    // Ghidra: PTR_s____Music_Mel00_01_WAV_00566cc8 â€” the music filename table. Entries recovered
    // from Legacy/Blaze.exe are CD-root absolute templates; see Documentation/DataSymbols.md.
    // TODO(REVERSE): only the sampled entries are transcribed. Call sites index up to at least
    // 0x26, so the remaining slots stay null and PlayMusicTrack returns without starting anything
    // rather than indexing past the table.
    private static readonly byte*[] s_MusicTrackPaths = BuildMusicTrackTable();

    private static byte*[] BuildMusicTrackTable()
    {
        var table = new byte*[0x30];
        table[0] = ToAnsiZ("@:/Music/Mel00_01.WAV");
        table[1] = ToAnsiZ("@:/Music/Mel00_02.WAV");
        table[7] = ToAnsiZ("@:/Music/End01.WAV");
        return table;
    }

    private static readonly uint* s_MenuScreenBlock_00566c48 = (uint*)NativeMemory.AllocZeroed(0x40);
    private static readonly uint* s_MenuRenderBlock_00566c70 = (uint*)NativeMemory.AllocZeroed(0x40);


    /// <summary>Ghidra: FUN_00542130 @ 0x00542130 â€” game state 0's entry point: the main menu.</summary>
    private static void RunMainMenuState()
    {
        LoadMenuResources();      // Ghidra: FUN_00542160()
        SetupMenuScreen();        // Ghidra: FUN_00529a80()
        s_FadeCounter_005e9960 = 0;

        // Ghidra: while (DAT_005de510 == 0) FUN_00542250();
        while (s_GameStateId == 0)
        {
            RunMainMenuFrameLoop();
        }
    }

    /// <summary>Ghidra: FUN_00529ad0 @ 0x00529ad0 â€” starts a music track by table index.</summary>
    private static void PlayMusicTrack(int trackIndex)
    {
        // Ghidra: patches the path's first character with the CD drive letter, then starts the
        // stream. The table entries are CD-**root** absolute templates ("@:/Music/Mel00_01.WAV"),
        // recovered at PTR_s____Music_Mel00_01_WAV_00566cc8 â€” see Documentation/DataSymbols.md.
        if (trackIndex < 0 || trackIndex >= s_MusicTrackPaths.Length) return;
        byte* path = s_MusicTrackPaths[trackIndex];
        if (path == null) return;
        path[0] = Mem.s_CdDataRootPath[0];
        StartMusicStream(path);
    }

    /// <summary>
    /// Ghidra: FUN_00542250 @ 0x00542250 â€” the main menu's frame loop. Runs until a menu entry is
    /// chosen, then sets the next game state (or exits) and returns.
    /// </summary>


    private static void RunMainMenuFrameLoop()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);

        PlayMusicTrack(6);

        // Ghidra: DAT_005e9898 = 1; DAT_005e9908 = 1 â€” the two draw envs' `isbg`.
        *(Mem.s_DrawEnv0_005e9880 + 0x18) = 1;
        *(Mem.s_DrawEnv1_005e98f0 + 0x18) = 1;

        JoystickResetState(false);

        // Ghidra: the entity-slot table init. The table it reads from is selected by two joystick
        // axis comparisons against -0x8000 (a disconnected/centred sentinel), producing an index
        // 0..3 into a four-entry pointer table at 0x0056e660.
        // TODO(REVERSE): that table is .data content and its four entries are not modelled, so the
        // per-slot byte writes into DAT_00604a2a are skipped. The slot table itself is initialised
        // exactly as the original does.
        InitialiseEntitySlots();

        s_Unknown_005de538 = 0;

        // Ghidra: FUN_00544350(&DAT_0056e670, 0, &DAT_0056dd58, 0)
        BindMenuLabels(s_MenuLabels_0056e670, 0, s_MenuContext_0056dd58, 0);

        while (true)
        {
            s_Unknown_005de15c = 1;

            // Ghidra: the fade. uVar3 is the grey level; it walks down while the low nibble of the
            // frame counter is not 8 and the level is still above 0x7f.
            uint level = (s_FadeCounter_005e9960 & 0xf) * 0x10;
            s_Unknown_005de154 = (0xb - s_SizeSetting_005de160) * 0x10;
            s_Unknown_005de158 = (0xb - s_SizeSetting_005de160) * 0x0c;

            // ECHAFAUDAGE TEMPORAIRE (OPENFORESIA_TRACE_MENU=1) : pourquoi la validation ouvre
            // l'entrÃ©e 0 mais pas l'entrÃ©e 3. On relÃ¨ve ce que la boucle voit rÃ©ellement.
            if (s_TraceMenu) TraceMenuDescriptorChanges();

            if ((s_FadeCounter_005e9960 & 0xf) != 8 && level > 0x7f)
            {
                s_FadeCounter_005e9960--;
                level = 0x80;

                primCursor = Ui.RunTextEngine(primCursor, s_MenuContext_0056dd58);  // Ghidra: FUN_005443a0
                primCursor = DrawUiPanel(otEntry, primCursor, s_MenuContext_0056dd58);  // Ghidra: FUN_00545340

                int gammaBefore = s_Unknown_005d9de0;

                if ((s_Unknown_0056e61f & 4) == 0)
                {
                    switch (s_MenuSelection_0056e61c)
                    {
                        case 0: s_GameStateId = 1; break;
                        case 1: s_GameStateId = 4; break;
                        case 2: s_GameStateId = 6; break;

                        case 3:
                            // The options page. Drains the input queue, rebuilds textures, runs the
                            // options UI, writes the settings back and rebuilds the gamma tables if
                            // the value changed.
                            while (InputDrainStub() != 0) { }
                            Winsys3d.ReInitializeAllTextures();
                            int shiftX, shiftY;
                            Winsys3d.GetAbsShift(&shiftX, &shiftY);
                            Winsys3d.SetAbsShift(0, 0);
                            if (s_SizeSetting_005de160 < 7) s_SmallTextureFlag = 1;
                            OptionsPage();                        // Ghidra: FUN_00512320

                            // DEVIATION, une ligne, et mesurée. La page d'options se quitte par
                            // Échap, qui est lié au bouton 0x4000 de la manette. Au retour, la
                            // première image du menu voit ce bouton **encore enfoncé**, son panneau
                            // le prend pour une validation et rouvre aussitôt la page : à l'écran,
                            // un aller-retour instantané au menu principal.
                            //
                            // Le jeu a son propre idiome pour ça — la boucle de vidange juste
                            // au-dessus, `while (FUN_005048f0() != 0)`, qui attend le relâchement
                            // avant d'entrer dans la page. On l'applique symétriquement à la
                            // sortie.
                            //
                            // TODO(REVERSE): l'original n'a pas cette seconde vidange, ce qui veut
                            // dire que son panneau distingue une touche *nouvellement* enfoncée
                            // d'une touche maintenue — un front — là où cette reconstruction lit
                            // l'état maintenu (`CurrentPadWord`, le mot d'état, pas le mot de
                            // fronts que `UpdateButtonEdges` calcule juste à côté). C'est là qu'est
                            // le correctif propre ; celui-ci en tient lieu en attendant, et se
                            // retire d'une ligne le jour où la sémantique du panneau est établie.
                            //
                            // La boucle est **cadencée** par Ordinal_132, contrairement à celle du
                            // dessus : sans elle, elle tourne à vide sans laisser la pompe de
                            // messages s'exécuter, et l'état DirectInput ne se rafraîchit jamais —
                            // mesuré, le jeu restait figé dans la page. C'est la même attente que
                            // l'éditeur de touches utilise entre deux captures.
                            UiPageTrace("options : retour au menu, vidange…");
                            while (InputDrainStub() != 0) { Winsys3d.Ordinal_132(0); }

                            // …et on referme la garde de validation. Sans cela le `switch`
                            // ci-dessus se redéclenche à l'image suivante sur la même entrée :
                            // mesuré, `Clear(TARGET)` reste figé pendant que `BeginScene` continue
                            // de compter, signe que le menu n'a jamais repris la main.
                            //
                            // Le bit 2 est celui que `Handler_00544b10` lève quand le flux de texte
                            // d'une page se termine, et qui referme la garde jusqu'à la validation
                            // suivante. Le rétablir ici, c'est remettre le menu dans l'état où il
                            // était avant d'ouvrir la page.
                            //
                            // **Les deux champs, pas un seul.** L'état interactif du menu est le
                            // couple `[0x3b] == 1` et bit 2 levé — c'est ce que le relevé par image
                            // montre en régime stable, et la validation les fait tomber ensemble
                            // (`+0x03B:1->0` et `+0x8C7:05->01`). Ne relever que le bit 2 referme
                            // la garde sans marquer la page terminée : le menu redevient visible
                            // mais son curseur ne répond plus.
                            //
                            // `Handler_00544b10` remet en plus le curseur à zéro (`[0x8c4] = 0`) ;
                            // on ne le fait pas, pour laisser la sélection là où le joueur l'avait.
                            s_MenuContext_0056dd58[0x3b] = 1;
                            s_MenuContext_0056dd58[0x8c7] |= 4;
                            UiPageTrace("options : vidange terminée, garde refermée");

                            Winsys3d.SetAbsShift(shiftX, shiftY);
                            SaveOptionsIni();
                            if (gammaBefore != s_Unknown_005d9de0)
                            {
                                Winsys3d.CreateTranslationTables(s_Unknown_005d9de0 * s_GammaScale_0054d074);
                            }
                            break;

                        case 4:
                            Exit(0);   // Ghidra: FUN_00547770(0)
                            break;
                    }

                    if (s_GameStateId != 0)
                    {
                        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
                        s_Unknown_005de15c = 0;
                        s_Unknown_005de51c = 0;
                        return;
                    }
                }
            }

            s_Unknown_005de15c = 0;

            // Ghidra: the per-frame draw of the menu **background sprite**.
            //
            // `local_14` .. `local_4` are ONE contiguous 20-byte structure on the original's
            // stack, and it is `&local_14` that both renderers receive:
            //
            //     +0x00  local_14  descriptor pointer, from the resource directory
            //     +0x04  local_10  colour â€” the fade's grey, level * 0x10101
            //     +0x08  local_c   x = 0xa0 (160)      +0x0a  local_a  y = 0x78 (120)
            //     +0x0c  local_8   tpage = 0x1420      +0x0e  local_6  clut  = 0x1149
            //     +0x10  local_4   0
            //
            // This had been reconstructed as **two separate heap blocks**, so the sprite reached
            // the renderers with a null position, size and texture page â€” which is why no
            // background was drawn on either rendering path. Same shape of bug as the four
            // DRAWENV/DISPENV structures, whose contiguity is likewise a contract; it is the
            // fifth occurrence in this project, and the second where the evidence was "a `DAT_`
            // or a local that is really a field of a larger structure".
            byte* sprite = s_MenuBackgroundSprite;
            *(uint*)(sprite + 0x04) = level * 0x10101;
            *(ushort*)(sprite + 0x08) = 0xa0;
            *(ushort*)(sprite + 0x0a) = 0x78;
            *(ushort*)(sprite + 0x0c) = 0x1420;
            *(ushort*)(sprite + 0x0e) = 0x1149;
            *(uint*)(sprite + 0x10) = 0;

            // Ghidra: local_14 = *(*(DAT_005e97c4 + 0x94) + 4)
            if (Mem.s_MainResourceDirectory_005e97c4 != null)
            {
                int* table = *(int**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
                if (table != null) *(int*)sprite = table[1];
            }

            primCursor = DrawSpritePrimitive(otEntry, primCursor, (int*)sprite, 0, 0);
            primCursor = DrawFlatPrimitive(otEntry, primCursor, (int*)sprite);
            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        }
    }

    /// <summary>
    /// Ghidra: `&local_14` in FUN_00542250 â€” the menu background sprite's descriptor, 20 bytes,
    /// **one contiguous block**. See the layout comment at its only use site; splitting it is what
    /// kept the background from being drawn.
    /// </summary>
    private static readonly byte* s_MenuBackgroundSprite = (byte*)NativeMemory.AllocZeroed(0x20);

    private static readonly bool s_TraceMenu =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_MENU") == "1";

    /// <summary>
    /// ECHAFAUDAGE TEMPORAIRE (<c>OPENFORESIA_TRACE_MENU=1</c>). Reports which bytes of the menu
    /// descriptor change from one frame to the next, alongside the pad word that caused it. The
    /// cursor visibly moves, so whatever holds its position must show up here â€” the same method
    /// that identified <c>menuContext[0x8c7]</c>.
    /// </summary>
    private static void TraceMenuDescriptorChanges()
    {
        const int extent = 0x918;
        byte* context = s_MenuContext_0056dd58;
        s_MenuTraceSnapshot ??= new byte[extent];

        ushort pad = CurrentPadWord();
        var changes = new System.Text.StringBuilder();
        int changed = 0;

        for (int i = 0; i < extent; i++)
        {
            if (context[i] == s_MenuTraceSnapshot[i]) continue;
            if (++changed <= 12)
                changes.Append($" +0x{i:X3}:{s_MenuTraceSnapshot[i]:X2}->{context[i]:X2}");
            s_MenuTraceSnapshot[i] = context[i];
        }

        if (changed != 0 || pad != 0)
            Console.Error.WriteLine(
                $"[MENU] pad=0x{pad:X4} fade={s_FadeCounter_005e9960} " +
                $"drapeaux=0x{s_Unknown_0056e61f:X2} changÃ©s={changed}{changes}");
    }

    private static byte[]? s_MenuTraceSnapshot;
}
