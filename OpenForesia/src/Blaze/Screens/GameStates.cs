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

    /// <summary>
    /// Ghidra: FUN_0052a3d0 @ 0x0052a3d0 â€” the real, never-returning per-frame game loop. Ticks via
    /// an indirect call through a Blaze-owned function pointer (Ghidra: PTR_FUN_0056c96c) whose
    /// assignment site hasn't been found yet anywhere in the reconstructed call graph, then
    /// dispatches to FUN_0052a440 whenever the game-state id (Ghidra: DAT_005de510) changes away
    /// from 0. The only way out, in the original, is process termination via Exit()/ExitProcess â€”
    /// there is no normal `return` path, so RunApplication's post-RunMainLoop shutdown sequence is
    /// dead code in practice (still preserved faithfully in RunApplication's `finally` block).
    /// TODO(REVERSE): PTR_FUN_0056c96c's assignment site not found; FUN_0052a440 (the per-state
    /// dispatcher) not yet reconstructed â€” next frontier after LoadFirstLevelResources above.
    /// </summary>
    private static void RunGameLoop()
    {
        s_GameStateId_005de510 = 0;
        while (true)
        {
            do
            {
                if (Mem.s_MainResourceBuffer_005e97c0 != null)
                {
                    int* perStateTable1 = *(int**)(Mem.s_MainResourceBuffer_005e97c0 + 0x48);
                    s_Unknown_005e92b0 = perStateTable1[s_GameStateId_005de510];
                    int* perStateTable2 = *(int**)(Mem.s_MainResourceBuffer_005e97c0 + 0x6c);
                    int entryPtrValue = perStateTable2[s_GameStateId_005de510];
                    // Ghidra: FUN_00529b50(entry, DAT_006008c8) â€” this destination is always
                    // ApplyResourceDestinationOverride's first case *by construction* (the call site
                    // passes DAT_006008c8 as both the value being compared and the comparand), so
                    // it's always a no-op here regardless of DAT_006008c8's real (never assigned
                    // in this reconstruction) value.
                    Res.LoadResourceEntry((int*)(nint)entryPtrValue, (byte*)Res.s_OverrideDestA_006008c8);
                }

                s_MainLoopTickCallback();
            } while (s_GameStateId_005de510 == 0);

            if (s_GameStateId_005de510 != 7 && s_GameStateId_005de510 != 9 && s_GameStateId_005de510 != 2 && s_GameStateId_005de510 != 3)
            {
                Res.RunLoadingScreen();     // Ghidra: FUN_0052a440
            }
        }
    }

    // Ghidra: DAT_005de510 â€” current game-state id, shared between RunGameLoop and the tick below.
    internal static int s_GameStateId_005de510;

    // Ghidra: PTR_FUN_0056c96c â€” the per-frame tick RunGameLoop calls indirectly. **Resolved**: the
    // pointer's initialised value was read out of Legacy/Binaries/Blaze.exe as 0x00537CA0, and FUN_00537ca0
    // already has a decompiled body in Blaze.c (line 83364), so no Ghidra re-run was needed. See
    // Documentation/Reference/DataSymbols.md.
    //
    // Kept as a function pointer rather than a direct call: the original really does dispatch
    // through a statically initialised .data slot, and preserving that indirection keeps the shape
    // of the code honest. A managed function pointer is enough â€” there is no native caller.
    private static delegate*<void> s_MainLoopTickCallback = &RunGameStateTick;


    private static uint* s_Zoom8BitTextures;
    private static uint* GetZoom8BitTexturesPtr()
    {
        if (s_Zoom8BitTextures == null)
        {
            IntPtr module = (IntPtr)Libraries.Winsys3dImage.Base /* image memoire de winsys3d : Libraries/winsys3d/Image.cs */;
            if (module != IntPtr.Zero)
            {
                s_Zoom8BitTextures = (uint*)(IntPtr)Libraries.Winsys3dImage.Exporte(ToAnsiZ("Zoom8BitTextures"));
            }
        }
        return s_Zoom8BitTextures;
    }

    /// <summary>
    /// Ghidra: FUN_00537ca0 @ 0x00537ca0 â€” the per-frame tick, and the game's real **per-state
    /// dispatcher**. Note this displaces a long-standing assumption in REVERSE_STATUS.md that
    /// FUN_0052a440 played that role; the tick pointer reaches this function instead.
    /// <para>
    /// Every one of the eleven state handlers below **has a decompiled body in Blaze.c** â€” they are
    /// ordinary uncovered code, not blocked on anything, so each stub names its Ghidra function so
    /// the next state to reconstruct is obvious from the exception alone.
    /// </para>
    /// </summary>
    private static void RunGameStateTick()
    {
        // Ghidra: *(undefined4 *)Zoom8BitTextures_exref = 0;
        uint* zoom = GetZoom8BitTexturesPtr();
        if (zoom != null) *zoom = 0;

        Winsys3d.ReInitializeAllTextures();

        switch (s_GameStateId_005de510)
        {
            case 0:
                RunMainMenuState();
                return;
            case 1:
                RunGameState1Entry();
                return;
            case 2:
                // Ghidra: FUN_00542160(); DAT_00564a4c = 0; FUN_00537da0(); DAT_00564a4c = 1;
                //         DAT_005de510 = 3;   <- the one case that advances the state itself
                Res.LoadMenuResources();          // Ghidra: FUN_00542160
                Mem.s_Unknown_00564a4c = 0;
                RunDestinationSelect();       // Ghidra: FUN_00537da0
                Mem.s_Unknown_00564a4c = 1;
                s_GameStateId_005de510 = 3;
                break;
            case 3:
                // Ghidra: sets Zoom8BitTextures back to 1 before dispatching.
                RunWorldMapState();     // Ghidra: thunk_FUN_00516020 -> FUN_00516020
                break;
            case 4:
                // Ghidra: FUN_005066d0() â€” le chargement est reconstruit, la boucle de frame
                // s'arrête sur ses huit sous-états non décompilés. Voir REVERSE_STATUS.md.
                RunCharacterSelectState();
                break;
            case 5:
                throw new NotImplementedException("FUN_00504c90 (game state 5) not yet reconstructed");
            case 6:
                RunItemMenuState();     // Ghidra: FUN_00524180
                break;
            case 7:
                RunEndingState_00531B40();   // Ghidra: FUN_00531b40 — Screens/State07/Ending.cs
                break;
            case 8:
                throw new NotImplementedException("FUN_005315d0 (game state 8) not yet reconstructed");
            case 9:
                throw new NotImplementedException("FUN_0051bb00 (game state 9) not yet reconstructed");
            case 10:
                RunRebootScreen_00513BA0();   // Ghidra: FUN_00513ba0
                break;
            default:
                // Ghidra: `uRam00000000 = 1;` â€” a store to address 0. Not a translation artifact:
                // the original deliberately faults on an unknown state, i.e. this is its assert.
                throw new InvalidOperationException(
                    $"FUN_00537ca0: unknown game state {s_GameStateId_005de510}. The original stores to " +
                    "address 0 here, deliberately crashing â€” reproduced as an exception rather " +
                    "than by writing to a null pointer.");
        }
    }
}
