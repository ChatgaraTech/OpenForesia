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
    // inside the KANJI resource the DLL locks at startup, or to a diagonal-line glyph (0x00a2c388) when out of range.
    internal static ushort* Ordinal_57(uint sjis) => Libraries.Winsys3dTexteEcran.Krom2RawAdd(sjis); // PORTE 2026-09-23 — Libraries/winsys3d/TexteEcran.cs, libgpu Krom2RawAdd

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
    /// <summary>Porte : <c>Libraries/winsys3d/Trigonometrie.cs</c>, winsys3d.c FUN_00a25c20 @ 0x00a25c20.</summary>
    internal static int GetCosTableEntry(int index) => Libraries.Winsys3dTrigonometrie.Rcos(index);

    /// <summary>Porte : <c>Libraries/winsys3d/Trigonometrie.cs</c>, winsys3d.c FUN_00a25c40 @ 0x00a25c40.</summary>
    internal static uint GetSinTableEntry(int index) => Libraries.Winsys3dTrigonometrie.Rsin((uint)index);

    internal static void SetDeviceLostCallback(void* callback) => Libraries.Winsys3dDevice.SetDeviceLostCallback(callback); // PORTE 2026-09-23 — Libraries/winsys3d/Device.cs, ordinal 790

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
    internal static int Ordinal_9(int* transferState) => Libraries.Winsys3dEvenements.TestEvent(transferState); // PORTE 2026-09-23 — libapi TestEvent, Libraries/winsys3d/Evenements.cs

    // ------------------------------------------------------------------------------------------
    // LA FAMILLE DE FICHIERS DE LA CARTE MEMOIRE — portée le 2026-09-22.
    //
    // Les huit ordinaux 18 à 25 sont maintenant dans `Libraries/winsys3d/CarteMemoire.cs`, avec
    // le pivot `FUN_00a03f80` qui résout un descripteur en chemin Win32. Ce sont les composantes
    // 2, 9, 10, 12 et 13 de `DLLGlobales.md` : elles ne touchent que deux globales, et aucune
    // fonction restée native ne les partage. Les commentaires de rôle sont conservés sur place —
    // la polarité inversée de l'ordinal 20 et le 0xffffffff des ordinaux 24/25 sont des pièges,
    // pas des détails.
    // ------------------------------------------------------------------------------------------

    /// <summary>real: FUN_00a044c0, FindFirstFileA + copie du nom tronqué et de la taille dans
    /// <c>*dest</c> ; null si aucune entrée.</summary>
    internal static byte* Ordinal_18(void* pathDescriptor, byte* dest)
        => Libraries.Winsys3dCarteMemoire.TrouverPremier((byte*)pathDescriptor, dest);

    // real: FUN_00a04560, FindNextFileA + même copie. Ghidra le décompile `void`, mais FUN_005126c0
    // (Legacy/Raw/Blaze/FUN_005126c0.asm:36, `TEST EAX,EAX` juste après le CALL) teste bien EAX au
    // retour : le premier `return;` de la fonction laisse BVar2(=le FindNextFileA FALSE) dans EAX par
    // convention de code-gen, donc EAX vaut 0 exactement quand il n'y a plus d'entrée — déclaré `int`
    // ici pour pouvoir lire cette valeur, même si le vrai prototype ne la nomme pas.
    internal static int Ordinal_19(byte* dest) => Libraries.Winsys3dCarteMemoire.TrouverSuivant(dest);

    /// <summary>real: FUN_00a045e0, DeleteFileA — **polarité inversée** : rend 1 si la suppression
    /// a ÉCHOUÉ, 0 si elle a réussi (<c>NEG EAX; SBB EAX,EAX; INC EAX</c> en fin de corps).</summary>
    internal static int Ordinal_20(void* pathDescriptor)
        => Libraries.Winsys3dCarteMemoire.Supprimer((byte*)pathDescriptor);

    /// <summary>real: FUN_00a04600, CreateFileA.</summary>
    internal static IntPtr Ordinal_21(void* pathDescriptor, uint flags)
        => Libraries.Winsys3dCarteMemoire.Ouvrir((byte*)pathDescriptor, flags);

    /// <summary>real: FUN_00a04650, CloseHandle.</summary>
    internal static int Ordinal_22(IntPtr handle) => Libraries.Winsys3dCarteMemoire.Fermer(handle);

    /// <summary>real: FUN_00a04660, SetFilePointer.</summary>
    internal static int Ordinal_23(IntPtr handle, int offset, uint method)
        => Libraries.Winsys3dCarteMemoire.Deplacer(handle, offset, method);

    /// <summary>real: FUN_00a04680, ReadFile (0xffffffff sur échec).</summary>
    internal static uint Ordinal_24(IntPtr handle, void* buffer, uint size)
        => Libraries.Winsys3dCarteMemoire.Lire(handle, buffer, size);

    /// <summary>real: FUN_00a046b0, WriteFile (0xffffffff sur échec).</summary>
    internal static uint Ordinal_25(IntPtr handle, void* buffer, uint size)
        => Libraries.Winsys3dCarteMemoire.Ecrire(handle, buffer, size);

    internal static int Ordinal_67(int unused) => Libraries.Winsys3dEvenements.LivrerFinCarteLogicielle(); // PORTE 2026-09-23 — livre une fin d'E/S SwCARD, rend 1

    internal static int Ordinal_68(int unused) => Libraries.Winsys3dEvenements.LivrerFinCarteMaterielle(); // PORTE 2026-09-23 — livre une fin d'E/S HwCARD, rend 1

    internal static int Ordinal_69(int unused) => Libraries.Winsys3dEvenements.LivrerFinCarteLogicielle(); // PORTE 2026-09-23 — meme corps qu'Ordinal_67

    internal static uint Ordinal_72(int unused) => Libraries.Winsys3dSouches.RendZeroUnArgument(); // real: FUN_00a02e20, le talon partagé par ~50 ordinaux ; rend toujours 0

    // winsys3d 684, libgpu `StoreImage` : VRAM -> memoire (icone de sauvegarde). PORTE le 2026-09-23 — `Libraries/winsys3d/Vram.cs`.
    internal static uint Ordinal_684(short* rect, void* destination) => (uint)Libraries.Winsys3dVram.StoreImage(rect, (byte*)destination);

    /// <summary>Porte : <c>Libraries/winsys3d/SectionCritique.cs</c>, winsys3d.c FUN_00a04fe0 @ 0x00a04fe0.</summary>
    internal static void Ordinal_41() => Libraries.Winsys3dSectionCritique.Sortir();

    // Ordinal_45 : souche `xor eax,eax; ret 4` (0x00a02e20). **UN seul argument** — le `ret 4` le dit
    // sans ambiguite. Il en portait DEUX ici jusqu'au 2026-08-21, ce qui laissait 4 octets sur la pile
    // a chaque appel en stdcall. Corrige sur la foi du code machine, pas de la pseudo-C.
    internal static int Ordinal_45(void* a) => (int)Libraries.Winsys3dSouches.RendZeroUnArgument();
    // Ordinal_46 : souche `xor eax,eax; ret` (0x00a02490), cdecl sans argument. Rend toujours 0,
    // donc le test `Ordinal_46() < 0` de FUN_00513dd0 est toujours faux dans cette version de la DLL.
    internal static int Ordinal_46() => (int)Libraries.Winsys3dSouches.RendZero();

    // real: winsys3d.c FUN_00a05160 — libcd `CdSearchFile`, vidé par le portage PC : il ignore ses
    // deux arguments et rend l'adresse d'une `CdlFILE` statique de 24 octets que personne n'écrit.
    // L'original passe ici `(&DAT_005e9040, "BLAZE_ALL.1")` ; le site C# passe `null, null`, ce qui
    // ne change rien puisque le corps ne les lit pas. Voir `Libraries/winsys3d/Cdrom.cs`.
    internal static void* Ordinal_92(void* a, void* b) => Libraries.Winsys3dCdrom.CdSearchFile(a, (byte*)b);

    internal static uint Ordinal_104(void* a) => Libraries.Winsys3dSouches.RendZeroUnArgument(); // confirmed no-op, returns 0 (FUN_00a02e20)

    internal static void Ordinal_110(int a) => _ = Libraries.Winsys3dSouches.RendZeroUnArgument(); // confirmed no-op (FUN_00a02e20)

    internal static uint Ordinal_116() => Libraries.Winsys3dSouches.RendZero(); // confirmed no-op, returns 0 (FUN_00a02490)

    internal static void Ordinal_129() => _ = Libraries.Winsys3dSouches.RendZero(); // confirmed no-op (FUN_00a02490)

    // real: winsys3d.c FUN_00a053e0, libgpu VSync(n) — the frame-pacing wait.
    internal static int Ordinal_132(int a) => Libraries.Winsys3dCadence.VSync(a); // PORTE 2026-09-23 — Libraries/winsys3d/Cadence.cs

    // real: winsys3d.c FUN_00a05770, get/set DAT_011be6a4 ("frame time offset"?), returns previous
    // value. Porté avec l'ordinal 135, son lecteur, que personne n'importe — une composante se porte
    // entière. Le rôle reste non établi : voir `Libraries/winsys3d/Cadence.cs`.
    internal static int Ordinal_136(int a) => (int)Libraries.Winsys3dCadence.Unknown_00a05770((uint)a);

    internal static void Ordinal_138() => Libraries.Winsys3dSouches.Rien(); // confirmed no-op (FUN_00a26470)

    internal static void Ordinal_368(void* eightDwordStruct) => Libraries.Winsys3dMatrices.SetColorMatrix((byte*)eightDwordStruct); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, vraisemblablement SetColorMatrix (0x011d0828)

    internal static void Ordinal_378(int a, int b) => Libraries.Winsys3dMatrices.SetGeomOffset(a, b); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, par la forme SetGeomOffset (0x011cb414/410)

    // Ordinals 376/379/457 are winsys3d.dll's port of the PlayStation SDK's **libgte** (the
    // geometry engine), the sibling of the libgpu ordinals below. Identified from their bodies:
    internal static void Ordinal_376(byte r, byte g, byte b) => Libraries.Winsys3dMatrices.SetBackColor(r, g, b); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, libgte SetBackColor

    // real: winsys3d.c FUN_00a21040 — libgte SetGeomScreen: stores the projection distance, rebuilds the
    // 0x10000-entry reciprocal table at EXT_011d0898, then updates the device projection (FUN_00a1dbc0,
    // which calls wsyskern ProjectionMatrix — also ported, Libraries/wsyskern/Matrices.cs).
    internal static void Ordinal_379(int h) => Libraries.Winsys3dMatrices.SetGeomScreen(h); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs

    /// <summary>
    /// Porte : <c>Libraries/winsys3d/Trigonometrie.cs</c>, winsys3d.c FUN_00a25d10 @ 0x00a25d10 —
    /// libgte ratan2. L'ordre des arguments est confirme par le desassemblage (`fild [esp+4]` puis
    /// `fild [esp+8]`, et `fpatan` divise st(1) par st(0)) : c'est bien `ratan2(y, x)`.
    /// </summary>
    internal static int Ordinal_457(int y, int x) => Libraries.Winsys3dTrigonometrie.Ratan2(y, x);

    // Ghidra nomme ce thunk `ReleaseTexture` parce qu'il l'a resolu par la table d'imports ; le nom
    // vient donc de la DLL elle-meme. Le corps est winsys3d.c 25890 : un renvoi d'une ligne vers
    // FUN_00a27700, avec l'ordinal en commentaire. Le site d'appel de FUN_0050bf20 (0x0050ca48)
    // empile un seul argument (`push 18h`), et le thunk du jeu est en 0x00545ad6 — un
    // `jmp dword [608804h]`.
    // PORTE le 2026-09-23 — `Libraries/winsys3d/CacheTextures.cs` : vide la page de texture `handle & 0x1f`.
    internal static void ReleaseTexture(int handle) => Libraries.Winsys3dCacheTextures.ReleaseTexture(handle);

    // libgte VectorNormal : divise un vecteur par sa longueur en 1.12 et rend cette longueur.
    // **La variante a trois `int` en sortie, pas la variante a trois `short`** — celle-la est
    // l'ordinal 440 (winsys3d.c FUN_00a259d0), a l'octet pres le meme corps. La distinction n'est
    // pas cosmetique : la sortie ecrase ici trois emplacements de pile de quatre octets, et
    // l'ordinal 440 n'en aurait rempli que six.
    internal static uint Ordinal_439(int* vector, int* normalized) => Libraries.Winsys3dMatrices.VectorNormal(vector, normalized); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, libgte VectorNormal

    // libgte RotTrans par lot : `count` sommets passes par le registre MATRIX (ordinal 814), avec
    // la rangee de translation, mais SANS division perspective — a la difference de l'ordinal 418.
    // Les deux tableaux sont a pas de QUATRE shorts (huit octets), dont trois seulement sont
    // ecrits : c'est ce qui donne la nappe {x, y, z, _} que FUN_00534330 relit ensuite.
    internal static void Ordinal_792(short* source, short* destination, uint count) => Libraries.Winsys3dMatrices.RotTransVecteursCourts(source, destination, count); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs

    internal static uint Ordinal_442(uint squared) => Libraries.Winsys3dMatrices.SquareRoot0(squared); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, libgte SquareRoot0 : table selon la grandeur, un pas de Newton, 16 bits

    // The five entry points FUN_0051d1f0 (the animated-model draw) needs. Identified from their
    // bodies; see Documentation/DLL/winsys3d.md. Declared ahead of that reconstruction so the
    // identification is not lost.
    internal static void Ordinal_366(void* matrix) => Libraries.Winsys3dMatrices.SetRotMatrix((byte*)matrix); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, libgte SetRotMatrix

    internal static void Ordinal_367(void* matrix) => Libraries.Winsys3dMatrices.SetLightMatrix((byte*)matrix); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, libgte SetLightMatrix (huit mots)

    internal static void Ordinal_369(void* matrix) => Libraries.Winsys3dMatrices.SetTransMatrix((byte*)matrix); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, libgte SetTransMatrix

    // real: winsys3d.c FUN_00a20630 — libgte CompMatrix. Le PREMIER argument est l'operande gauche et
    // le TROISIEME la destination : l'ancienne declaration les nommait a l'envers, les sites passaient
    // deja (parent, os, destination).
    internal static void Ordinal_360(void* a, void* b, void* destMatrix) => Libraries.Winsys3dMatrices.CompMatrix((short*)a, (short*)b, (short*)destMatrix); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs, branches MMX et scalaire

    // real: winsys3d.c FUN_00a22010 — batch RotTransPers: transforms `count` x 3 vertices through the
    // MATRIX register (ordinal 814) into screen XY and depth arrays. MMX/scalar split inside, and the
    // two branches do NOT compute the same thing — see Libraries/winsys3d/Matrices.cs.
    internal static void Ordinal_418(void* vertices, void* outScreenXy, void* outDepth, void* outFlags, uint count)
        => Libraries.Winsys3dMatrices.RotTransPersParTrois((short*)vertices, (int*)outScreenXy, (uint*)outDepth, (ushort*)outFlags, count); // PORTE 2026-09-23

    // real: winsys3d.c FUN_00a03e70 — batch lighting. Gated on the DLL's `Elo_NoLighting`, which when set
    // writes zeroes instead. Consumes EXT_00b6d198, i.e. data export ordinal 817.
    internal static void Ordinal_793(void* normals, void* outColours, uint triangleCount)
        => Libraries.Winsys3dEclairage.EclairerTriangles((byte*)normals, (uint*)outColours, (int)triangleCount); // PORTE 2026-09-23 — Libraries/winsys3d/Eclairage.cs

    // real: winsys3d.c FUN_00a20540 â€” libgte ScaleMatrix: scales each of the matrix's three columns
    // by one component of `scale`, in 4.12 fixed point. Le seul des neuf ordinaux libgte importés
    // qui ne touche aucun registre émulé du GTE : voir `Libraries/winsys3d/Matrices.cs`.
    internal static void Ordinal_794(void* matrix, void* scale)
        => Libraries.Winsys3dMatrices.ScaleMatrix((short*)matrix, (short*)scale);

    // real: winsys3d.c FUN_00a226c0 â€” the depth-less sibling of ordinal 418, taken when the model's
    // 0x400 flag is set. **Three** parameters, not two: Ghidra names them (param_1, param_2,
    // param_3) and then swaps two of them inside the scalar path (`param_3 = param_2; param_2 =
    // iVar4`), which reads as a two-argument function if the swap is skipped. Both paths agree that
    // the third is the count: the scalar loop ends on `while (param_2 != 0)` after the swap, and the
    // MMX path runs `for (iVar4 = (int)param_3 * 3; iVar4 != 0; iVar4--)` without swapping. Same
    // count as ordinal 418, and three vertices consumed per unit in both.
    internal static void Ordinal_795(void* vertices, void* outScreenXy, uint count)
        => Libraries.Winsys3dMatrices.RotTransPersParTroisSansProfondeur((short*)vertices, (int*)outScreenXy, count); // PORTE 2026-09-23 — Libraries/winsys3d/Matrices.cs

    // winsys3d.c FUN_00a05ac0 â€” libgpu `SetDefDispEnv(DISPENV*, x, y, w, h)`. Writes disp={x,y,w,h},
    // screen={0,0,0x100,0xf0}, isinter=0, isrgb24=0 (18 of the struct's 20 bytes).
    /// <summary>Porte : <c>Libraries/winsys3d/ListeAffichage.cs</c>, winsys3d.c FUN_00a05ac0 @ 0x00a05ac0.</summary>
    internal static void SetDefDispEnv(void* dispEnv, short x, short y, short w, short h)
        => Libraries.Winsys3dListeAffichage.SetDefDispEnv(dispEnv, x, y, w, h);

    // winsys3d.c FUN_00a05b50 â€” libgpu `SetDefDrawEnv(DRAWENV*, x, y, w, h)`. Writes clip={x,y,w,h},
    // ofs={x,y}, tw={0,0,0,0}, tpage=10, dtd=1, dfe=1, isbg=0, r0=g0=b0=0 (first 28 bytes of 92).
    /// <summary>Porte : <c>Libraries/winsys3d/ListeAffichage.cs</c>, winsys3d.c FUN_00a05b50 @ 0x00a05b50.</summary>
    internal static void SetDefDrawEnv(void* drawEnv, short x, short y, short w, short h)
        => Libraries.Winsys3dListeAffichage.SetDefDrawEnv(drawEnv, x, y, w, h);

    // Game-state-0 ordinals. winsys3d.c declares all of these with no parameters, but that is the
    // same decompiler artifact seen throughout this project â€” the **call sites are authoritative**,
    // and the argument counts below are theirs (Blaze.c: FUN_00529a80 and FUN_00542160).
    //
    // 2026-09-22 : l'ordinal 97 est une SOUCHE, `xor eax,eax ; ret 0Ch` (FUN_00a046e0). Le `ret 0Ch`
    // confirme les trois arguments ci-dessus, et le corps confirme qu'il ne fait rien.
    internal static int Ordinal_97(int a, int b, int c) => (int)Libraries.Winsys3dSouches.RendZeroTroisArguments();

    internal static void Ordinal_762(void* p) => Libraries.Winsys3dSouches.RienUnArgument();

    // 2026-09-22 : SOUCHE, `xor eax,eax ; ret 8` (FUN_00a02e10, 22 ordinaux). Le `ret 8` confirme
    // les deux arguments ci-dessus.
    internal static int Ordinal_765(int a, uint rgb) => (int)Libraries.Winsys3dSouches.RendZeroDeuxArguments();

    internal static int Ordinal_766(void* p) => (int)Libraries.Winsys3dSouches.RendZeroUnArgument();

    // 2026-09-22 : SOUCHE, `ret 8` (FUN_00a02e40, 41 ordinaux). Meme controle d arite.
    internal static void Ordinal_767(int a, uint rgb) => Libraries.Winsys3dSouches.RienDeuxArguments();

    internal static int Ordinal_768(int a) => (int)Libraries.Winsys3dSouches.RendZeroUnArgument();

    internal static int Ordinal_769(int a) => (int)Libraries.Winsys3dSouches.RendZeroUnArgument();

    internal static int Ordinal_770(void* p) => (int)Libraries.Winsys3dSouches.RendZeroUnArgument();

    // Frame ordinals. 688 is libgpu `ClearOTagR(u_long *ot, int n)` â€” winsys3d.c FUN_00a06160 keeps
    // both parameters, unlike its neighbours. 657/660 take a DISPENV/DRAWENV (PutDispEnv/PutDrawEnv
    // shaped), 701 takes the ordering table (DrawOTag shaped).
    /// <summary>Porte : <c>Libraries/winsys3d/ListeAffichage.cs</c>, winsys3d.c FUN_00a06160 @ 0x00a06160 — ClearOTagR.</summary>
    internal static uint* Ordinal_688(uint* ot, int count) => Libraries.Winsys3dListeAffichage.ClearOTagR(ot, count);

    /// <summary>Porte : <c>Libraries/winsys3d/ListeAffichage.cs</c> — PutDispEnv, avec la lecture des manettes (<c>Manettes.cs</c>).</summary>
    internal static int Ordinal_657(void* env) => Libraries.Winsys3dListeAffichage.PutDispEnv(env);

    /// <summary>Porte : <c>Libraries/winsys3d/ListeAffichage.cs</c>, winsys3d.c FUN_00a05b30 @ 0x00a05b30.</summary>
    internal static int Ordinal_660(void* env) => Libraries.Winsys3dListeAffichage.PutDrawEnv(env);

    // ------------------------------------------------------------------------------------------
    // ORDINAL 701, `DrawOTag` — la PORTE du chemin d'affichage, portée le 2026-09-22.
    //
    // C'est le seul `[DllImport]` du périmètre de `Documentation/Etat/CheminAffichage.md` : 157
    // corps et 12 408 lignes sont derrière lui. Seul le corps de `DrawOTag` est transcrit pour
    // l'instant ; ses sept appelées restent natives, appelées **par adresse** (`Natif.cs`), et ses
    // globales sont celles de la DLL, aliasées (`Data.cs`). L'image doit donc être identique.
    //
    // **`OPENFORESIA_LEGACY_DRAWOTAG=1` rebranche l'original**, pour pouvoir comparer les deux à
    // l'écran — c'est la seule vérification qui vaille sur ce lot, et le même genre d'échappatoire
    // que `OPENFORESIA_LEGACY_3DHELP`. À retirer quand le périmètre sera porté.
    // ------------------------------------------------------------------------------------------
    [DllImport("winsys3d.dll", EntryPoint = "#701")] private static extern void Ordinal_701_Natif(uint* ot);

    private static readonly bool s_DrawOTagLegacy =
        Environment.GetEnvironmentVariable("OPENFORESIA_LEGACY_DRAWOTAG") == "1";

    internal static void Ordinal_701(uint* ot)
    {
        if (s_DrawOTagLegacy) { Ordinal_701_Natif(ot); return; }
        Libraries.Winsys3dCheminAffichage.DrawOTag(ot);
    }

    // libgpu MoveImage(RECT* src, int dstX, int dstY) â€” copies a rectangle inside the emulated
    // VRAM. Body confirmed at winsys3d.c:5348 (`/* 0x5df0  676 */`): it walks `src->h` rows of
    // `src->w` texels from the arena and writes them at (dstX, dstY).
    // PORTE le 2026-09-23 — `Libraries/winsys3d/Vram.cs`.
    internal static void Ordinal_676(ushort* sourceRect, int destinationX, int destinationY) => _ = Libraries.Winsys3dVram.MoveImage((short*)sourceRect, destinationX, destinationY);

    // winsys3d 665 : libgpu `ClearImage(RECT*, r, g, b)`. Sa couleur `((b & 0xff) << (g + 10)) << (r + 5)`
    // est un defaut du binaire (LB-044), juste pour le noir, le seul cas employe. PORTE le 2026-09-23 —
    // `Libraries/winsys3d/Vram.cs`.
    internal static int Ordinal_665(void* rect, byte r, byte g, uint b) => Libraries.Winsys3dVram.ClearImage((short*)rect, r, g, b);

    internal static void Ordinal_667(int a) => _ = Libraries.Winsys3dSouches.RendZeroUnArgument(); // confirmed no-op (FUN_00a02e20)

    internal static int Ordinal_668(int a, int b, int c, int d, int e, int f) => Libraries.Winsys3dTexteEcran.FntOpen(a, b, c, d, e, f); // PORTE 2026-09-23 — Libraries/winsys3d/TexteEcran.cs, libgpu FntOpen(x, y, w, h, isbg, n) : ouvre une fenetre de texte

    // real: winsys3d.c FUN_00a05d50 â€” libgpu `LoadImage(RECT*, u_long* data)`: copies `data` row by
    // row into the VRAM rectangle. `rect` is a 4-short RECT {x,y,w,h}, and the body early-outs on
    // rect.y > 0x1ff, confirming a 1024x512 VRAM addressing scheme.
    // PORTE le 2026-09-23 — `Libraries/winsys3d/Vram.cs`.
    internal static int Ordinal_674(void* rect, void* srcBuffer) => Libraries.Winsys3dVram.LoadImage((short*)rect, (byte*)srcBuffer);

    // ------------------------------------------------------------------------------------------
    // LE LECTEUR DE CONTENEUR TIM — porté le 2026-09-22, `Libraries/winsys3d/Tim.cs`. C'était la
    // composante 3 de `DLLGlobales.md` : deux corps, une seule globale (`DAT_011be790`), dont les
    // dix références tiennent dans cette paire.
    // ------------------------------------------------------------------------------------------

    // winsys3d.c FUN_00a05bb0 â€” libgpu `OpenTIM(u_long *addr)`. Parks a cursor at `addr + 4` in the
    // DLL global DAT_011be790; ReadTIM below consumes and clears it, so the pair is one-shot.
    internal static int OpenTim(void* timData) => Libraries.Winsys3dTim.OpenTim(timData);

    // winsys3d.c FUN_00a05bd0 â€” libgpu `ReadTIM(TIM_IMAGE*)`. Parses the TIM header the cursor
    // points at and fills the 5-dword TIM_IMAGE below. When the header's bit 3 is clear the image
    // has no CLUT and it writes mode=2, crect=null, caddr=null.
    //
    // Le renvoi prend un `uint*` et non un `TimImage*` : `Libraries/` ne connait pas les types de
    // `src/Blaze/`, et ne doit pas — ce sont deux programmes distincts (LISEZMOI.md). La structure
    // fait exactement cinq mots, c'est ce que la DLL ecrit, et c'est le cast qui le dit.
    internal static void ReadTim(Resources.TimImage* image) => Libraries.Winsys3dTim.ReadTim((uint*)image);

    internal static void Ordinal_681(int a) => _ = Libraries.Winsys3dSouches.RendZeroUnArgument(); // confirmed no-op (FUN_00a02e20)

    internal static void Ordinal_710(int width, int height) => Libraries.Winsys3dTexteEcran.FntLoad(width, height); // PORTE 2026-09-23 — Libraries/winsys3d/TexteEcran.cs, libgpu FntLoad(tx, ty) : les deux arguments sont la position VRAM de la police, pas une taille

    internal static void Ordinal_712(int a) => Libraries.Winsys3dSouches.RienUnArgument(); // confirmed no-op (FUN_00a02870)

    internal static void Ordinal_720(nint a) => Libraries.Winsys3dSouches.RienUnArgument(); // confirmed no-op (FUN_00a02870)

    internal static void Ordinal_771() => Libraries.Winsys3dSouches.Rien(); // confirmed no-op (FUN_00a26470)

    [DllImport("winsys3d.dll", EntryPoint = "#789")] internal static extern int Ordinal_789(int a, int b); // real: winsys3d.c FUN_00a06420, device (re)init/teardown dispatcher; inert when a==0 besides storing b

    // winsys3d SetShift (ordinal 796) : decalage de vue et zoom. PORTE le 2026-09-23 — `Libraries/winsys3d/DecalageEcran.cs`.
    internal static void SetShift(int x, int y, int zoomLevel, int flag) => Libraries.Winsys3dDecalageEcran.SetShift(x, y, zoomLevel, flag);

    // winsys3d.dll ordinals used by FUN_00504460 (joystick/interrupt-style event-handler
    // (de)registration bookkeeping â€” see winsys3d.c FUN_00a040d0/FUN_00a04220 and neighbors).
    internal static void* Ordinal_6(uint messageId, uint p2, uint p3, void* callback) => Libraries.Winsys3dEvenements.OpenEvent(messageId, (int)p2, (int)p3, callback); // PORTE 2026-09-23 — libapi OpenEvent, Libraries/winsys3d/Evenements.cs

    // real: winsys3d.c FUN_00a04220 — libapi `EnableEvent` : **arme** le gestionnaire (état
    // 0x1000 → 0x2000), ce n'est pas un désenregistrement (celui-là est l'ordinal 7, FUN_00a04130).
    // Le nœud appartient à l'appelant — il vient de l'ordinal 6, resté natif, donc du tas de
    // `wsyskern` : une seule mémoire, vue des deux côtés. Voir `Libraries/winsys3d/Evenements.cs`.
    internal static int Ordinal_10(void* handle) => Libraries.Winsys3dEvenements.EnableEvent(handle);

    /// <summary>Porte : <c>Libraries/winsys3d/SectionCritique.cs</c>, winsys3d.c FUN_00a04fd0 @ 0x00a04fd0. winsys3d.c FUN_00a04fd0 — EnterCriticalSection(&DAT_00b6d6d8), la section que l'ordinal 41 relâche. Corps lu le 2026-08-18, RET sans argument</summary>
    internal static void Ordinal_40() => Libraries.Winsys3dSectionCritique.Entrer();

    internal static void Ordinal_63(int a) => Libraries.Winsys3dSouches.RienUnArgument(); // confirmed no-op (FUN_00a02870)

    internal static int Ordinal_64() => (int)Libraries.Winsys3dSouches.RendZero(); // confirmed no-op, returns 0 (FUN_00a02490)

    internal static void Ordinal_66() => Libraries.Winsys3dSouches.Rien(); // confirmed no-op (FUN_00a26470)

    [DllImport("winsys3d.dll", EntryPoint = "#750")] internal static extern void* Ordinal_750(); // real: winsys3d.c FUN_00a01e30, returns the DLL's main device-interface pointer (DAT_011be760)

    // winsys3d.dll: ReInitializeAllTextures is a named export (called by winsys3d.c itself at
    // winsys3d.c:4119). Zoom8BitTextures is an imported *data* symbol, same category as
    // TexFilterPossible/MainMem â€” resolved lazily via GetProcAddress, since P/Invoke has no
    // mechanism for imported data.
    // PORTE le 2026-09-23 — `Libraries/winsys3d/CacheTextures.cs`.
    internal static void ReInitializeAllTextures() => Libraries.Winsys3dCacheTextures.ReInitializeAllTextures();

    // winsys3d.dll named exports (ordinal 776 and its setter) â€” the viewport shift the options page
    // saves and restores around its own screen.
    // PORTE le 2026-09-23 — `Libraries/winsys3d/DecalageEcran.cs`.
    internal static void GetAbsShift(int* x, int* y) => Libraries.Winsys3dDecalageEcran.GetAbsShift(x, y);

    // PORTE le 2026-09-23 — `Libraries/winsys3d/DecalageEcran.cs`.
    internal static void SetAbsShift(int x, int y) => Libraries.Winsys3dDecalageEcran.SetAbsShift(x, y);

    // winsys3d, exports nommÃ©s. `ShiftVertices` (ordinal 824, winsys3d.c:4913) applique
    // `x = dÃ©calage + (Size + 9) * 32 / 320 * x` â€” le facteur documentÃ© dans la section
    // Â« Coordinate scaling Â». Le fond de page passe par lui ; les widgets, eux, par la mise Ã 
    // l'Ã©chelle que le jeu fait lui-mÃªme (FUN_00510110). Les deux coexistent, c'est voulu.
    // PORTE le 2026-09-23 — `Libraries/winsys3d/DecalageEcran.cs`.
    internal static void ShiftVertices(TlVertex* vertices, int count) => Libraries.Winsys3dDecalageEcran.ShiftVertices((float*)vertices, count);

    // PORTE le 2026-09-23 — `Libraries/winsys3d/Device.cs`, ordinal 798.
    internal static void SetTextureFilterMode(int mode) => Libraries.Winsys3dDevice.SetTextureFilterMode(mode);

    // PORTE le 2026-09-23 — `Libraries/winsys3d/Cadence.cs`, ordinal 822.
    internal static int SetTimingMode(int mode) => Libraries.Winsys3dCadence.SetTimingMode(mode);

    // winsys3d ordinal 749 (winsys3d.c:6010) : demande au device le mode d'affichage d'index
    // `resolution` par son slot `+0xE8`, et ne retient le changement que si le device l'accepte.
    // PORTE le 2026-09-23 — `Libraries/winsys3d/Device.cs`.
    internal static int ChangeResolution(int resolution) => Libraries.Winsys3dDevice.ChangeResolution(resolution);

}
