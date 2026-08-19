// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La surface P/Invoke de winsys3d.dll.
//
// PAS D'INTERFACE, et c'est un choix mesure : `SyntheticDevice` substitue le device 3D de
// 3dhelp, jamais winsys3d, qui reste toujours la vraie DLL. Sans second implementeur, une
// interface ne serait que de l'indirection - voir ARCHITECTURE.md et design.md, D2.
//
// Les commentaires d'ordinal sont conserves : ils sont la trace vers winsys3d.c, au meme titre
// qu'un `// Ghidra:` l'est vers Blaze.c.
//
// `static` par NECESSITE et non par paresse : [DllImport] exige `extern static`, et il n'y a
// aucun etat a porter. Une classe statique est ici la forme correcte.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

internal static unsafe class Winsys3d
{
    // winsys3d ordinal 57 (winsys3d.c FUN_00a05060) â€” maps a Shift-JIS code to a 16x16 1bpp glyph
    // inside the KANJI resource the DLL locks at startup, or to a blank when it is out of range.
    [DllImport("winsys3d.dll", EntryPoint = "#57")] internal static extern ushort* Ordinal_57(uint sjis);

    // winsys3d.dll ordinal 823 (named export). Role now fully confirmed by reading its body and
    // its callee chain in winsys3d.c (FUN_00a010d0 -> dialog proc FUN_00a012f0): despite the name,
    // this is the game's **renderer-selection dialog / launcher**, not a device creation call.
    // It enumerates DirectDraw drivers (DirectDrawEnumerateA), probes for a 3Dfx card by
    // LoadLibrary("GLIDE2X.DLL") + GetProcAddress("_grSstQueryBoards@4"), then shows modal dialog
    // resource 0x66 â€” the same resource id whose RT_BITMAP LoadBitmapEx loads, i.e. hBitmap /
    // hPalette are that dialog's splash image â€” and writes the chosen driver back to the ini.
    // See Documentation/DLL/winsys3d.md for the full analysis and struct layout.
    //
    // `titleStrings` is a pointer to an array of 5 ANSI string pointers, consumed by the dialog
    // proc's WM_INITDIALOG handler (winsys3d.c:218-223):
    //   [0] dialog window title   [1] static 0x3e9   [2] static 0x3ea
    //   [3] OK button (id 1)      [4] Cancel button (id 2)
    // The DLL null-checks the array pointer itself but then dereferences all five entries
    // unconditionally, so a non-null array must have five valid strings.
    [DllImport("winsys3d.dll")]
    internal static extern int Setup3DDevice(IntPtr hBitmap, IntPtr hPalette, byte** titleStrings);

    // winsys3d.dll ordinal 756, confirmed against winsys3d.c (decompiled DLL source): builds
    // gamma-correction / RGB translation tables from a float multiplier (default 1.0f = 0x3f800000).
    [DllImport("winsys3d.dll")]
    internal static extern void CreateTranslationTables(float gamma);

    // The following winsys3d.dll entry points are exported by ordinal only (no name in the DLL's
    // own export table). Their RVAs were resolved against winsys3d.dll's export directory and
    // cross-checked directly in winsys3d.c (decompiled DLL source, see
    // Documentation/DLL/winsys3d.md for the full analysis):
    //   - ordinal 337 (winsys3d.c: FUN_00a1df00) builds the engine's trig lookup tables
    //     (fills DAT_011cb420[0x1000] via fsin()) â€” no arguments, run once.
    //   - ordinals 450/451 (winsys3d.c: FUN_00a25c20/FUN_00a25c40) are simple reads from that
    //     same 0x1000-entry ring buffer, 450 phase-shifted by 0x400 (a quarter period) relative
    //     to 451 â€” i.e. a classic packed cos()/sin() lookup table pair.
    //   - ordinal 790 (winsys3d.c: FUN_00a06410) just stores the pointer into a DLL-private
    //     global (DAT_011be798) that winsys3d.dll later calls indirectly as a callback
    //     (confirmed via cross-reference in winsys3d.c); likely a device-lost/reset notification
    //     hook. TODO(REVERSE): the callback passed at the RunApplication call site
    //     (Ghidra: &LAB_00512ef0) has no decompiled function body in Blaze.c (Ghidra only saw it
    //     addressed-of, never as a call target), so it cannot be reconstructed yet.
    [DllImport("winsys3d.dll", EntryPoint = "#337")] internal static extern void InitTrigTables();

    // libgte rcos/rsin. Bodies: winsys3d.c FUN_00a25c20 / FUN_00a25c40 â€” both plain lookups into the
    // same 0x1000-entry table at DAT_011cb420, rcos being rsin shifted a quarter turn
    // (`table[(a + 0x400) & 0xfff]`). 0x1000 steps per full turn, the PlayStation convention, which
    // is what makes `i << 8` sixteen evenly spaced positions in the character carousel.
    // Ordinals 452/453 are aliases of these same two entries.
    [DllImport("winsys3d.dll", EntryPoint = "#450")] internal static extern int GetCosTableEntry(int index);

    [DllImport("winsys3d.dll", EntryPoint = "#451")] internal static extern uint GetSinTableEntry(int index);

    [DllImport("winsys3d.dll", EntryPoint = "#790")] internal static extern void SetDeviceLostCallback(void* callback);

    // The following winsys3d.dll ordinals are used directly by RunMainLoop (Ghidra: FUN_00529c10)
    // and its immediate dependency graph. Signatures/behavior were confirmed against winsys3d.c â€”
    // see Documentation/DLL/winsys3d.md for the full per-ordinal writeup. Several are literal
    // no-op stubs in the shipped DLL (`return 0;` / `return;` with no body) â€” they are still
    // declared and called for real (rather than special-cased away) so the call graph stays
    // traceable to Blaze.c 1:1, and because "does nothing" is itself the DLL's real, confirmed
    // behavior here, not an assumption.
    // La sous-couche carte mémoire (winsys3d.c FUN_00a041d0/04600/04650/04660/04680/046b0/050d0/
    // 050f0/02e20) : de vrais CreateFileA/CloseHandle/SetFilePointer/ReadFile/WriteFile sur un
    // dossier "save\" — pas d'émulation de carte mémoire PS1, malgré le nom. Chaque arité relue
    // directement dans winsys3d.dll (RET n) plutôt que déduite de Ghidra : les ordinaux 67/68/69/72
    // partagent tous un corps qui ignore son argument mais le retire quand même de la pile
    // (RET 4), alors que Ghidra les avait décompilés en (void) — la même classe de piège que
    // documente déjà test-protocol.md, attrapée ici avant d'écrire le P/Invoke plutôt qu'après un
    // plantage natif.
    [DllImport("winsys3d.dll", EntryPoint = "#9")] internal static extern int Ordinal_9(int* transferState); // real: FUN_00a041d0, ResetEvent + un contrôle d'état sur *param_1

    [DllImport("winsys3d.dll", EntryPoint = "#18")] internal static extern byte* Ordinal_18(void* pathDescriptor, byte* dest); // real: FUN_00a044c0, FindFirstFileA + copie du nom tronqué et de la taille dans *dest ; null si aucune entrée

    // real: FUN_00a04560, FindNextFileA + même copie. Ghidra le décompile `void`, mais FUN_005126c0
    // (Legacy/Raw/Blaze/FUN_005126c0.asm:36, `TEST EAX,EAX` juste après le CALL) teste bien EAX au
    // retour : le premier `return;` de la fonction laisse BVar2(=le FindNextFileA FALSE) dans EAX par
    // convention de code-gen, donc EAX vaut 0 exactement quand il n'y a plus d'entrée — déclaré `int`
    // ici pour pouvoir lire cette valeur, même si le vrai prototype ne la nomme pas.
    [DllImport("winsys3d.dll", EntryPoint = "#19")] internal static extern int Ordinal_19(byte* dest);

    [DllImport("winsys3d.dll", EntryPoint = "#20")] internal static extern int Ordinal_20(void* pathDescriptor); // real: FUN_00a045e0, DeleteFileA — **polarité inversée** : rend 1 si la suppression a ÉCHOUÉ, 0 si elle a réussi (`NEG EAX; SBB EAX,EAX; INC EAX` en fin de corps)

    [DllImport("winsys3d.dll", EntryPoint = "#21")] internal static extern IntPtr Ordinal_21(void* pathDescriptor, uint flags); // real: FUN_00a04600, CreateFileA

    [DllImport("winsys3d.dll", EntryPoint = "#22")] internal static extern int Ordinal_22(IntPtr handle); // real: FUN_00a04650, CloseHandle

    [DllImport("winsys3d.dll", EntryPoint = "#23")] internal static extern int Ordinal_23(IntPtr handle, int offset, uint method); // real: FUN_00a04660, SetFilePointer

    [DllImport("winsys3d.dll", EntryPoint = "#24")] internal static extern uint Ordinal_24(IntPtr handle, void* buffer, uint size); // real: FUN_00a04680, ReadFile (0xffffffff sur échec)

    [DllImport("winsys3d.dll", EntryPoint = "#25")] internal static extern uint Ordinal_25(IntPtr handle, void* buffer, uint size); // real: FUN_00a046b0, WriteFile (0xffffffff sur échec)

    [DllImport("winsys3d.dll", EntryPoint = "#67")] internal static extern int Ordinal_67(int unused); // real: FUN_00a050f0, RET 4 confirmé — un paramètre ignoré ; rend toujours 1

    [DllImport("winsys3d.dll", EntryPoint = "#68")] internal static extern int Ordinal_68(int unused); // real: FUN_00a050d0, même famille qu'Ordinal_67/69

    [DllImport("winsys3d.dll", EntryPoint = "#69")] internal static extern int Ordinal_69(int unused); // real: FUN_00a050f0 — même corps qu'Ordinal_67, deux ordinaux pour la même adresse

    [DllImport("winsys3d.dll", EntryPoint = "#72")] internal static extern uint Ordinal_72(int unused); // real: FUN_00a02e20, le talon partagé par ~50 ordinaux ; rend toujours 0

    [DllImport("winsys3d.dll", EntryPoint = "#684")] internal static extern uint Ordinal_684(short* rect, void* destination); // real: FUN_00a05f50, copie rectangulaire VRAM→mémoire (icône de sauvegarde) ; rect = {x,y,w,h}

    [DllImport("winsys3d.dll", EntryPoint = "#41")] internal static extern void Ordinal_41(); // real: LeaveCriticalSection on the DLL's internal device lock

    [DllImport("winsys3d.dll", EntryPoint = "#45")] internal static extern int Ordinal_45(void* a, byte* b); // confirmed no-op (winsys3d.c: FUN_00a02e20)

    [DllImport("winsys3d.dll", EntryPoint = "#92")] internal static extern void* Ordinal_92(void* a, void* b); // confirmed no-op (FUN_00a05160 ignores its args)

    [DllImport("winsys3d.dll", EntryPoint = "#104")] internal static extern uint Ordinal_104(void* a); // confirmed no-op, returns 0 (FUN_00a02e20)

    [DllImport("winsys3d.dll", EntryPoint = "#110")] internal static extern void Ordinal_110(int a); // confirmed no-op (FUN_00a02e20)

    [DllImport("winsys3d.dll", EntryPoint = "#116")] internal static extern uint Ordinal_116(); // confirmed no-op, returns 0 (FUN_00a02490)

    [DllImport("winsys3d.dll", EntryPoint = "#129")] internal static extern void Ordinal_129(); // confirmed no-op (FUN_00a02490)

    [DllImport("winsys3d.dll", EntryPoint = "#132")] internal static extern int Ordinal_132(int a); // real: winsys3d.c FUN_00a053e0, VSync/frame-pacing wait â€” DLL-internal timing state

    [DllImport("winsys3d.dll", EntryPoint = "#136")] internal static extern int Ordinal_136(int a); // real: winsys3d.c FUN_00a05770, get/set DAT_011be6a4 ("frame time offset"?), returns previous value

    [DllImport("winsys3d.dll", EntryPoint = "#138")] internal static extern void Ordinal_138(); // confirmed no-op (FUN_00a26470)

    [DllImport("winsys3d.dll", EntryPoint = "#368")] internal static extern void Ordinal_368(void* eightDwordStruct); // real: winsys3d.c FUN_00a20ed0, copies 8 dwords into DLL-internal "shading defaults" state (EXT_011d0828); exact field semantics TODO(REVERSE)

    [DllImport("winsys3d.dll", EntryPoint = "#378")] internal static extern void Ordinal_378(int a, int b); // real: winsys3d.c FUN_00a21020, stores (a,b) into DLL-internal state (EXT_011cb414/EXT_011cb410); exact semantics TODO(REVERSE)

    // Ordinals 376/379/457 are winsys3d.dll's port of the PlayStation SDK's **libgte** (the
    // geometry engine), the sibling of the libgpu ordinals below. Identified from their bodies:
    [DllImport("winsys3d.dll", EntryPoint = "#376")] internal static extern void Ordinal_376(byte r, byte g, byte b); // real: winsys3d.c FUN_00a20fe0 â€” libgte SetBackColor: three bytes into DAT_011f0898

    [DllImport("winsys3d.dll", EntryPoint = "#379")] internal static extern void Ordinal_379(int h); // real: winsys3d.c FUN_00a21040 â€” libgte SetGeomScreen: stores the projection distance, then rebuilds the 0x10000-entry reciprocal table at EXT_011d0898

    [DllImport("winsys3d.dll", EntryPoint = "#457")] internal static extern int Ordinal_457(int y, int x); // real: winsys3d.c FUN_00a25d10 â€” libgte ratan2

    // The five entry points FUN_0051d1f0 (the animated-model draw) needs. Identified from their
    // bodies; see Documentation/DLL/winsys3d.md. Declared ahead of that reconstruction so the
    // identification is not lost.
    [DllImport("winsys3d.dll", EntryPoint = "#366")] internal static extern void Ordinal_366(void* matrix); // real: winsys3d.c FUN_00a20e70 â€” libgte SetRotMatrix: copies the nine rotation shorts into the MATRIX register (ordinal 814, offset 0)

    [DllImport("winsys3d.dll", EntryPoint = "#369")] internal static extern void Ordinal_369(void* matrix); // real: winsys3d.c FUN_00a20ef0 â€” libgte SetTransMatrix: copies the three translations from the matrix's +0x14/+0x18/+0x1c into the same register's translation row

    [DllImport("winsys3d.dll", EntryPoint = "#360")] internal static extern void Ordinal_360(void* destMatrix, void* a, void* b); // real: winsys3d.c FUN_00a20630 â€” composes two matrices (libgte CompMatrix family), with its own MMX/scalar split

    [DllImport("winsys3d.dll", EntryPoint = "#418")] internal static extern void Ordinal_418(void* vertices, void* outScreenXy, void* outDepth, void* outFlags, uint count); // real: winsys3d.c FUN_00a22010 â€” batch RotTransPers: transforms `count` vertices through the MATRIX register (ordinal 814) into screen XY and depth arrays. MMX/scalar split inside.

    [DllImport("winsys3d.dll", EntryPoint = "#793")] internal static extern void Ordinal_793(void* normals, void* outColours, uint triangleCount); // real: winsys3d.c FUN_00a03e70 â€” batch lighting. Gated on the DLL's `Elo_NoLighting`, which when set writes zeroes instead. Consumes EXT_00b6d198, i.e. data export ordinal 817.

    [DllImport("winsys3d.dll", EntryPoint = "#794")] internal static extern void Ordinal_794(void* matrix, void* scale); // real: winsys3d.c FUN_00a20540 â€” libgte ScaleMatrix: scales each of the matrix's three columns by one component of `scale`.

    // real: winsys3d.c FUN_00a226c0 â€” the depth-less sibling of ordinal 418, taken when the model's
    // 0x400 flag is set. **Three** parameters, not two: Ghidra names them (param_1, param_2,
    // param_3) and then swaps two of them inside the scalar path (`param_3 = param_2; param_2 =
    // iVar4`), which reads as a two-argument function if the swap is skipped. Both paths agree that
    // the third is the count: the scalar loop ends on `while (param_2 != 0)` after the swap, and the
    // MMX path runs `for (iVar4 = (int)param_3 * 3; iVar4 != 0; iVar4--)` without swapping. Same
    // count as ordinal 418, and three vertices consumed per unit in both.
    [DllImport("winsys3d.dll", EntryPoint = "#795")] internal static extern void Ordinal_795(void* vertices, void* outScreenXy, uint count);

    // winsys3d.c FUN_00a05ac0 â€” libgpu `SetDefDispEnv(DISPENV*, x, y, w, h)`. Writes disp={x,y,w,h},
    // screen={0,0,0x100,0xf0}, isinter=0, isrgb24=0 (18 of the struct's 20 bytes).
    [DllImport("winsys3d.dll", EntryPoint = "#658")] internal static extern void SetDefDispEnv(void* dispEnv, short x, short y, short w, short h);

    // winsys3d.c FUN_00a05b50 â€” libgpu `SetDefDrawEnv(DRAWENV*, x, y, w, h)`. Writes clip={x,y,w,h},
    // ofs={x,y}, tw={0,0,0,0}, tpage=10, dtd=1, dfe=1, isbg=0, r0=g0=b0=0 (first 28 bytes of 92).
    [DllImport("winsys3d.dll", EntryPoint = "#661")] internal static extern void SetDefDrawEnv(void* drawEnv, short x, short y, short w, short h);

    // Game-state-0 ordinals. winsys3d.c declares all of these with no parameters, but that is the
    // same decompiler artifact seen throughout this project â€” the **call sites are authoritative**,
    // and the argument counts below are theirs (Blaze.c: FUN_00529a80 and FUN_00542160).
    [DllImport("winsys3d.dll", EntryPoint = "#97")] internal static extern int Ordinal_97(int a, int b, int c);

    [DllImport("winsys3d.dll", EntryPoint = "#762")] internal static extern void Ordinal_762(void* p);

    [DllImport("winsys3d.dll", EntryPoint = "#765")] internal static extern int Ordinal_765(int a, uint rgb);

    [DllImport("winsys3d.dll", EntryPoint = "#766")] internal static extern int Ordinal_766(void* p);

    [DllImport("winsys3d.dll", EntryPoint = "#767")] internal static extern void Ordinal_767(int a, uint rgb);

    [DllImport("winsys3d.dll", EntryPoint = "#768")] internal static extern int Ordinal_768(int a);

    [DllImport("winsys3d.dll", EntryPoint = "#769")] internal static extern int Ordinal_769(int a);

    [DllImport("winsys3d.dll", EntryPoint = "#770")] internal static extern int Ordinal_770(void* p);

    // Frame ordinals. 688 is libgpu `ClearOTagR(u_long *ot, int n)` â€” winsys3d.c FUN_00a06160 keeps
    // both parameters, unlike its neighbours. 657/660 take a DISPENV/DRAWENV (PutDispEnv/PutDrawEnv
    // shaped), 701 takes the ordering table (DrawOTag shaped).
    [DllImport("winsys3d.dll", EntryPoint = "#688")] internal static extern uint* Ordinal_688(uint* ot, int count);

    [DllImport("winsys3d.dll", EntryPoint = "#657")] internal static extern int Ordinal_657(void* env);

    [DllImport("winsys3d.dll", EntryPoint = "#660")] internal static extern int Ordinal_660(void* env);

    [DllImport("winsys3d.dll", EntryPoint = "#701")] internal static extern void Ordinal_701(uint* ot);

    // libgpu MoveImage(RECT* src, int dstX, int dstY) â€” copies a rectangle inside the emulated
    // VRAM. Body confirmed at winsys3d.c:5348 (`/* 0x5df0  676 */`): it walks `src->h` rows of
    // `src->w` texels from the arena and writes them at (dstX, dstY).
    [DllImport("winsys3d.dll", EntryPoint = "#676")] internal static extern void Ordinal_676(ushort* sourceRect, int destinationX, int destinationY);

    // real: winsys3d.c FUN_00a05e90 â€” libgpu `ClearImage(RECT*, r, g, b)`-shaped (takes a 4-short
    // RECT and floods VRAM), but its colour math decompiles as
    // `((p4 & 0xff) << (p3 + 10)) << (p2 + 5)`, which does not match ClearImage's plain (r,g,b).
    // TODO(REVERSE): left un-renamed until that is explained rather than assumed.
    [DllImport("winsys3d.dll", EntryPoint = "#665")] internal static extern int Ordinal_665(void* rect, byte r, byte g, uint b);

    [DllImport("winsys3d.dll", EntryPoint = "#667")] internal static extern void Ordinal_667(int a); // confirmed no-op (FUN_00a02e20)

    [DllImport("winsys3d.dll", EntryPoint = "#668")] internal static extern int Ordinal_668(int a, int b, int c, int d, int e, int f); // real: winsys3d.c FUN_00a020a0, registers a debug/console text channel slot (unused feature in the shipped build)

    // real: winsys3d.c FUN_00a05d50 â€” libgpu `LoadImage(RECT*, u_long* data)`: copies `data` row by
    // row into the VRAM rectangle. `rect` is a 4-short RECT {x,y,w,h}, and the body early-outs on
    // rect.y > 0x1ff, confirming a 1024x512 VRAM addressing scheme.
    [DllImport("winsys3d.dll", EntryPoint = "#674")] internal static extern int Ordinal_674(void* rect, void* srcBuffer);

    // winsys3d.c FUN_00a05bb0 â€” libgpu `OpenTIM(u_long *addr)`. Parks a cursor at `addr + 4` in the
    // DLL global DAT_011be790; ReadTIM below consumes and clears it, so the pair is one-shot.
    [DllImport("winsys3d.dll", EntryPoint = "#677")] internal static extern int OpenTim(void* timData);

    // winsys3d.c FUN_00a05bd0 â€” libgpu `ReadTIM(TIM_IMAGE*)`. Parses the TIM header the cursor
    // points at and fills the 5-dword TIM_IMAGE below. When the header's bit 3 is clear the image
    // has no CLUT and it writes mode=2, crect=null, caddr=null.
    [DllImport("winsys3d.dll", EntryPoint = "#662")] internal static extern void ReadTim(Blaze.TimImage* image);

    [DllImport("winsys3d.dll", EntryPoint = "#681")] internal static extern void Ordinal_681(int a); // confirmed no-op (FUN_00a02e20)

    [DllImport("winsys3d.dll", EntryPoint = "#710")] internal static extern void Ordinal_710(int width, int height); // real: winsys3d.c FUN_00a026e0, (re)creates the DLL's text/console overlay bitmap at the given size

    [DllImport("winsys3d.dll", EntryPoint = "#712")] internal static extern void Ordinal_712(int a); // confirmed no-op (FUN_00a02870)

    [DllImport("winsys3d.dll", EntryPoint = "#720")] internal static extern void Ordinal_720(nint a); // confirmed no-op (FUN_00a02870)

    [DllImport("winsys3d.dll", EntryPoint = "#771")] internal static extern void Ordinal_771(); // confirmed no-op (FUN_00a26470)

    [DllImport("winsys3d.dll", EntryPoint = "#789")] internal static extern int Ordinal_789(int a, int b); // real: winsys3d.c FUN_00a06420, device (re)init/teardown dispatcher; inert when a==0 besides storing b

    [DllImport("winsys3d.dll", EntryPoint = "SetShift")] internal static extern void SetShift(int x, int y, int zoomLevel, int flag); // real, named export (ordinal 796): camera/view shift+zoom, winsys3d.c confirmed

    // winsys3d.dll ordinals used by FUN_00504460 (joystick/interrupt-style event-handler
    // (de)registration bookkeeping â€” see winsys3d.c FUN_00a040d0/FUN_00a04220 and neighbors).
    [DllImport("winsys3d.dll", EntryPoint = "#6")] internal static extern void* Ordinal_6(uint messageId, uint p2, uint p3, void* callback); // real: winsys3d.c FUN_00a040d0, registers a handler, returns an opaque handle

    [DllImport("winsys3d.dll", EntryPoint = "#10")] internal static extern int Ordinal_10(void* handle); // real: winsys3d.c FUN_00a04220 — **arme** le gestionnaire (état 0x1000 → 0x2000), ce n'est pas un désenregistrement (celui-là est l'ordinal 7, FUN_00a04130)

    [DllImport("winsys3d.dll", EntryPoint = "#40")] internal static extern void Ordinal_40(); // real: winsys3d.c FUN_00a04fd0 — EnterCriticalSection(&DAT_00b6d6d8), la section que l'ordinal 41 relâche. Corps lu le 2026-08-18, RET sans argument

    [DllImport("winsys3d.dll", EntryPoint = "#63")] internal static extern void Ordinal_63(int a); // confirmed no-op (FUN_00a02870)

    [DllImport("winsys3d.dll", EntryPoint = "#64")] internal static extern int Ordinal_64(); // confirmed no-op, returns 0 (FUN_00a02490)

    [DllImport("winsys3d.dll", EntryPoint = "#66")] internal static extern void Ordinal_66(); // confirmed no-op (FUN_00a26470)

    [DllImport("winsys3d.dll", EntryPoint = "#750")] internal static extern void* Ordinal_750(); // real: winsys3d.c FUN_00a01e30, returns the DLL's main device-interface pointer (DAT_011be760)

    // winsys3d.dll: ReInitializeAllTextures is a named export (called by winsys3d.c itself at
    // winsys3d.c:4119). Zoom8BitTextures is an imported *data* symbol, same category as
    // TexFilterPossible/MainMem â€” resolved lazily via GetProcAddress, since P/Invoke has no
    // mechanism for imported data.
    [DllImport("winsys3d.dll")] internal static extern void ReInitializeAllTextures();

    // winsys3d.dll named exports (ordinal 776 and its setter) â€” the viewport shift the options page
    // saves and restores around its own screen.
    [DllImport("winsys3d.dll")] internal static extern void GetAbsShift(int* x, int* y);

    [DllImport("winsys3d.dll")] internal static extern void SetAbsShift(int x, int y);

    // winsys3d, exports nommÃ©s. `ShiftVertices` (ordinal 824, winsys3d.c:4913) applique
    // `x = dÃ©calage + (Size + 9) * 32 / 320 * x` â€” le facteur documentÃ© dans la section
    // Â« Coordinate scaling Â». Le fond de page passe par lui ; les widgets, eux, par la mise Ã 
    // l'Ã©chelle que le jeu fait lui-mÃªme (FUN_00510110). Les deux coexistent, c'est voulu.
    [DllImport("winsys3d.dll", ExactSpelling = true)]
    internal static extern void ShiftVertices(TlVertex* vertices, int count);

    [DllImport("winsys3d.dll", ExactSpelling = true)]
    internal static extern void SetTextureFilterMode(int mode);

    [DllImport("winsys3d.dll", ExactSpelling = true)]
    internal static extern int SetTimingMode(int mode);

    // winsys3d ordinal 749 (winsys3d.c:6010) : demande au device le mode d'affichage d'index
    // `resolution` par son slot `+0xE8`, et ne retient le changement que si le device l'accepte.
    [DllImport("winsys3d.dll", ExactSpelling = true)]
    internal static extern int ChangeResolution(int resolution);

}
