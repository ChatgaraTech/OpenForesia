// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `Blaze` porte encore les aides que cette classe appelle : ToAnsiZ, BuildCdPath,
// GetMainMemPtr, Malloc/Free, RaiseQuitException, s_ErrorCaption.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// La sortie video : la chaine DirectDraw, le lecteur de film VfW, l'ecran-titre et la mise en
/// place des quatre surfaces de vue (Ghidra: 0x00512 et 0x00542).
///
/// <para>
/// <b>Classe mixte, pour deux raisons distinctes et toutes deux mesurees.</b>
/// </para>
/// <para>
/// 1. <c>MoviePlayerWindowProc</c> porte <c>[UnmanagedCallersOnly]</c> : l'attribut exige une
/// methode statique, puisque Windows appelle la procedure de fenetre par simple adresse de code.
/// Elle rejoint l'instance par le point d'entree statique <c>Blaze.Vid</c> - le seul chemin dont
/// dispose un rappel natif. C'est le meme motif que les huit gestionnaires de <see cref="SaveData"/>,
/// applique cette fois a un corps qui appelle d'autres membres.
/// </para>
/// <para>
/// 2. <c>s_WindowedStartup</c> reste <b>statique deliberement</b>. Ce n'est pas une globale
/// <c>.data</c> du jeu : c'est une decision d'hote, lue une fois dans l'environnement, et
/// <c>src/Host/DeviceShim.cs</c> comme <c>src/Host/SyntheticDevice.cs</c> la lisent pendant
/// l'initialisation de leurs propres types. En faire un champ d'instance ferait dependre cette
/// initialisation-la de la construction complete des sous-systemes - arene basse comprise - sur le
/// chemin de demarrage le plus fragile du projet. La modeliser comme un etat d'instance serait en
/// faire ce qu'elle n'est pas.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>.
/// </para>
/// </summary>
internal sealed unsafe class Video(GameMemory mem, Resources res)
{
    // =====================================================================
    // Movie / intro-video subsystem (Ghidra: FUN_00542xxx cluster, ~30 functions).
    //
    // This is Blaze & Blade's own intro-movie player: a hidden VfW-style window
    // (window class "VfwClass") wrapping what strongly appears to be a DirectDraw
    // output surface (FUN_00512930/FUN_00512a10/FUN_00512b40/FUN_00512c80 use vtable
    // offsets that match the public, documented IDirectDraw/IDirectDrawSurface (v1)
    // COM layout exactly: 0x50=SetCooperativeLevel â€” confirmed by the sibling error
    // string "SetCooperativeLevel() failed to g[et]..."; 0x54=SetDisplayMode called
    // with 640x480x16; 0x18=CreateSurface; 0x2c=Flip/0x6c=Restore in a device-lost
    // retry loop) plus a *second*, Blaze-owned C++ "stream" object (vtable
    // PTR_LAB_0054d0a0, constructed by FUN_005431e0) used to actually decode and
    // step video frames (open/pause/stop calls at vtable offsets 0x1c/0x20/0x24,
    // matching FUN_00543100/140/180; a 100ms SetTimer drives per-frame stepping).
    //
    // TODO(REVERSE): the Blaze-owned "stream" object's vtable slots point at
    // LAB_xxxxxxxx addresses with *no* decompiled body anywhere in Blaze.c â€” Ghidra
    // never saw their instructions, only their address taken (same situation as the
    // ordinal-790 device-lost callback documented above). The exact decoder backing
    // it (VfW vs. an internal codec) can't be determined without further disassembly
    // of Blaze.exe outside what Blaze.c provides. Rather than invent a specific COM
    // interface/IID (the two IID byte blobs `DAT_0054e8c8`/`DAT_0054e8d8` referenced
    // deeper in this cluster, e.g. FUN_00543320, aren't reproduced in Blaze.c's text
    // either), this reconstruction implements every part that *is* independently
    // verifiable (window lifetime, DirectDraw surface lifetime, joystick capture,
    // the message pump, the exe-relative movie path building) faithfully, and stops
    // exactly at the "step one frame" vtable call â€” TrySetupMovieStream below always
    // reports failure, which the original's own code already treats as a normal,
    // handled case (RunMainLoop keeps going without the intro movies playing).
    // =====================================================================

    // Ghidra: DAT_005dffa8, a 0x30-byte "movie player" struct; single global instance.
    private sealed class MoviePlayerState
    {
        public IntPtr HWnd;                 // offset 0x10 (param_1[4])
        public IntPtr HInstance;            // offset 0x14 (param_1[5])
        public void* DirectDrawWrapper;     // offset 0x1c (param_1[7]) â€” Ghidra: FUN_00512930 result
        public void* Stream;                // offset 0x18 (param_1[6]) â€” Ghidra: FUN_005431e0 result; TODO(REVERSE) vtable unrecoverable
        public bool Playing;                // offset 0x00 (*param_1)
        public int Result;                  // offset 0x2c (param_1[0xb])
        public byte* TitleBitmap;           // offset 0x08 (param_1[2]) â€” the attract screen's TITLE640.BMP
                                            // image. Allocated on *wsyskern's* heap inside LoadBitmapFile,
                                            // so it is released with WsysFree, never the CRT Free.
        public int TitleBitmapLoaded;       // offset 0x0c (param_1[3]) â€” nonzero once TitleBitmap is cached
        public int AttractActive;           // offset 0x04 (param_1[1]) â€” set while the title screen is up;
                                            // FUN_00542970 tests it alongside Playing
        public int TimerArmed;              // offset 0x20 (param_1[8]) â€” nonzero once Win32.SetTimer(hwnd,1,...) ran
        public int Field24;                 // offset 0x24 â€” OpenMovieStream writes 2
        public int Field28;                 // offset 0x28 â€” OpenMovieStream writes 1; gates the WM_TIMER step
    }

    private MoviePlayerState? s_MoviePlayer; // Ghidra: DAT_005dffa8

    private readonly byte* s_VfwClassName = ToAnsiZ("VfwClass"); // Ghidra: s_VfwClass_0056e6b8 â€” TODO(REVERSE): exact bytes not recovered, name inferred from symbol
    private readonly byte* s_MovieWindowTitle = ToAnsiZ("Blaze & Blade"); // Ghidra: s_Blaze___Blade_005631d8 â€” TODO(REVERSE): exact bytes not recovered

    // =====================================================================
    // DirectDraw surface setup (Ghidra: FUN_005129e0 + FUN_00512a10).
    //
    // The vtable offsets below are the documented IDirectDraw / IDirectDrawSurface v1 layouts, and
    // every one used here cross-checks against a decompiled call site:
    //   IDirectDraw:        0x18 CreateSurface, 0x4c RestoreDisplayMode,
    //                       0x50 SetCooperativeLevel, 0x54 SetDisplayMode
    //   IDirectDrawSurface: 0x30 GetAttachedSurface, 0x58 GetSurfaceDesc,
    //                       0x44 GetDC, 0x68 ReleaseDC  (the last two are what the attract blit uses)
    // =====================================================================

    private const int IDirectDraw_CreateSurface = 0x18 / 4;
    private const int IDirectDraw_RestoreDisplayMode = 0x4c / 4;
    private const int IDirectDraw_SetCooperativeLevel = 0x50 / 4;
    private const int IDirectDraw_SetDisplayMode = 0x54 / 4;
    private const int IDirectDrawSurface_GetAttachedSurface = 0x30 / 4;
    private const int IDirectDrawSurface_GetSurfaceDesc = 0x58 / 4;

    // DDSURFACEDESC is 0x6c bytes â€” the literal FUN_00512a10 writes into dwSize, corroborated by the
    // 0x1b-dword (108-byte) zeroing loop that precedes each use.
    /// <summary>
    /// Vrai quand la sÃ©quence de dÃ©marrage â€” Ã©cran-titre et attract â€” s'affiche dans la fenÃªtre du
    /// jeu au lieu de l'affichage exclusif du binaire d'origine.
    ///
    /// <para>
    /// C'est le drapeau de la **seule dÃ©viation de fidÃ©litÃ© volontaire** de ce projet. Les trois
    /// autres Ã©carts consignÃ©s dans <c>REVERSE_STATUS.md</c> sont subis â€” le refus du 16 bits par
    /// Windows moderne, les surfaces perdues, l'arÃ¨ne relocalisÃ©e par l'ASLR â€” et la reconstruction
    /// les contourne pour rester fonctionnelle. Celui-ci est demandÃ© : le jeu ne doit plus prendre
    /// l'affichage en exclusif ni changer le mode, pour n'avoir qu'une seule fenÃªtre du lancement
    /// au menu.
    /// </para>
    /// <para>
    /// <c>OPENFORESIA_LEGACY_STARTUP=1</c> rÃ©tablit intÃ©gralement le comportement d'origine, sur le
    /// modÃ¨le d'<c>OPENFORESIA_LEGACY_3DHELP</c>. Ce qu'il gouverne : le niveau de coopÃ©ration
    /// DirectDraw et le changement de mode dans <see cref="SetupDirectDrawSurfaces"/>, et les
    /// quatre sites qui affichent ou maximisent la fenÃªtre <c>VfwClass</c>.
    /// </para>
    /// </summary>
    // `internal` et non `private` : les deux classes de `src/Host/` le lisent. Il reste ici malgre
    // sa nature de decision d'hote, parce que huit de ses dix sites de lecture sont dans la
    // transcription elle-meme (niveau de cooperation DirectDraw, les quatre sites `VfwClass`).
    // Le deplacer vers Host aurait fait lire Blaze depuis Host, soit l'inverse de la dependance a
    // sens unique voulue.
    internal static readonly bool s_WindowedStartup =
        Environment.GetEnvironmentVariable("OPENFORESIA_LEGACY_STARTUP") != "1";

    private const int DdSurfaceDescSize = 0x6c;
    private const uint DDSD_CAPS = 0x0001;
    private const uint DDSD_HEIGHT = 0x0002;
    private const uint DDSD_WIDTH = 0x0004;
    private const uint DDSD_BACKBUFFERCOUNT = 0x0020;
    private const uint DDSCAPS_COMPLEX = 0x0008;
    private const uint DDSCAPS_FLIP = 0x0010;
    private const uint DDSCAPS_PRIMARYSURFACE = 0x0200;
    private const uint DDSCAPS_BACKBUFFER = 0x0004;

    /// <summary>
    /// Ghidra: FUN_00512a10 @ 0x00512a10 (`__thiscall`) â€” brings DirectDraw up and creates the
    /// flippable primary surface. **This is what populates `wrapper[1]`**, the surface the attract
    /// screen's blit reaches through GetDC/ReleaseDC; FUN_00512930 explicitly leaves it null, which
    /// is why recovering the overlay vtable alone never produced a picture.
    /// <para>
    /// Two spots in the decompiled body are decompiler noise, reconstructed from intent: the guard
    /// renders as <c>((uint)(param_1 + 2) &amp; 4) &amp;&amp; ((uint)(param_1 + 2) &amp; 2)</c>, which
    /// tests an *address*. 4 and 2 are `DDSD_WIDTH` and `DDSD_HEIGHT`, so the real test is on the
    /// surface description's flags, and the `SetRect` that follows takes its width and height from
    /// that description.
    /// </para>
    /// </summary>
    private int SetupDirectDrawSurfaces(uint* wrapper, IntPtr hwnd)
    {
        // Ghidra: FUN_005129e0 â€” DirectDrawCreate(NULL, &wrapper[0], NULL), 0 on failure.
        void* directDraw;
        int hr = Win32.DirectDrawCreate(null, &directDraw, null);
        if (hr < 0 || directDraw == null) return unchecked((int)0x80004005); // E_FAIL â€” Ghidra's -0x7fffbffb
        wrapper[0] = (uint)(nuint)directDraw;

        void** ddVtable = *(void***)directDraw;

        // Ghidra: (**(*dd + 0x50))(dd, hwnd, 0x13)
        // 0x13 = DDSCL_FULLSCREEN | DDSCL_ALLOWREBOOT | DDSCL_EXCLUSIVE.
        var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)ddVtable[IDirectDraw_SetCooperativeLevel];

        // DEVIATION DELIBEREE ET CONDITIONNELLE â€” change `render-title-screen-in-window`.
        //
        // L'original prend l'affichage en `DDSCL_FULLSCREEN | DDSCL_EXCLUSIVE` puis le bascule en
        // 640x480, et c'est la moitiÃ© Â« affichage Â» de l'Ã©cran plein Ã©cran qu'on veut supprimer.
        // Tout cet appareillage n'a qu'un produit rÃ©ellement consommÃ© : le contexte de pÃ©riphÃ©rique
        // que `BlitTitleScreen` obtenait par `GetDC` sur la surface primaire. Le backend le fournit
        // dÃ©sormais (`StartupSurface`), donc plus rien ne le rÃ©clame ici :
        //
        //   * le back buffer obtenu par `GetAttachedSurface` n'Ã©tait jamais stockÃ©,
        //   * aucun `Flip` n'est jamais appelÃ© sur cette chaÃ®ne,
        //   * les champs du wrapper renseignÃ©s ensuite ne sont lus que par le chemin vidÃ©o
        //     DirectShow, laissÃ© nul (TODO #13).
        //
        // On garde donc l'objet DirectDraw et son cycle de vie â€” `ReleaseDirectDrawSurfaces` le
        // libÃ¨re toujours â€” et on ne retire que la prise d'affichage. `OPENFORESIA_LEGACY_STARTUP=1`
        // rÃ©tablit intÃ©gralement le chemin d'origine.
        if (!s_WindowedStartup)
        {
            hr = setCooperativeLevel(directDraw, hwnd, 0x13);
            if (hr < 0) return hr;
        }
        else
        {
            // 8 = DDSCL_NORMAL, la mÃªme valeur que la sÃ©quence de dÃ©montage d'origine emploie.
            hr = setCooperativeLevel(directDraw, hwnd, 8);
            if (hr < 0) return hr;

            // Ni changement de mode, ni chaÃ®ne primaire/flip : plus personne ne les consomme, et
            // c'est leur suppression qui fait disparaÃ®tre le plein Ã©cran. Le wrapper garde les
            // valeurs de son constructeur ; les renseigner reviendrait Ã  inventer une description
            // de surface qui n'existe plus.
            return 0;
        }

        // Ghidra: (**(*dd + 0x54))(dd, 0x280, 0x1e0, 0x10) â€” 640 x 480 x 16bpp.
        var setDisplayMode = (delegate* unmanaged[Stdcall]<void*, uint, uint, uint, int>)ddVtable[IDirectDraw_SetDisplayMode];
        hr = setDisplayMode(directDraw, 0x280, 0x1e0, 0x10);

        // ENVIRONMENTAL DEVIATION, measured rather than assumed. The original only ever asks for
        // 640x480x16, and that is what is attempted first. Modern Windows declines 16bpp mode
        // changes outright â€” measured here as E_NOTIMPL (0x80004001) â€” while the same 640x480 at
        // 32bpp succeeds and everything downstream (CreateSurface, GetDC, StretchDIBits) then works.
        // Falling back keeps the faithful call as the primary path and makes the deviation explicit
        // and conditional, rather than silently rewriting the original's request.
        if (hr == unchecked((int)0x80004001) /* E_NOTIMPL */)
        {
            hr = setDisplayMode(directDraw, 0x280, 0x1e0, 32);
        }

        if (hr < 0) return hr;

        // DEVIATION, deliberate and flagged. The original restores the display mode only through its
        // own teardown path. This reconstruction still throws on unreconstructed game states, and an
        // abrupt exit while holding an exclusive 640x480 mode leaves the user's desktop switched.
        // This restore-on-exit hook is a safety net, not fidelity â€” remove it once the shutdown path
        // is reliably reached.
        RegisterDisplayModeRestore(directDraw);

        // Ghidra: 0x1b-dword zeroing loop, then dwSize = 0x6c, dwFlags = 0x21, ddsCaps = 0x218.
        byte* desc = stackalloc byte[DdSurfaceDescSize];
        for (int i = 0; i < DdSurfaceDescSize; i++) desc[i] = 0;
        *(uint*)(desc + 0x00) = DdSurfaceDescSize;
        *(uint*)(desc + 0x04) = DDSD_CAPS | DDSD_BACKBUFFERCOUNT;                        // 0x21
        *(uint*)(desc + 0x14) = 1;                                                        // dwBackBufferCount
        *(uint*)(desc + 0x68) = DDSCAPS_PRIMARYSURFACE | DDSCAPS_FLIP | DDSCAPS_COMPLEX;  // 0x218

        void* primarySurface;
        var createSurface = (delegate* unmanaged[Stdcall]<void*, void*, void**, void*, int>)ddVtable[IDirectDraw_CreateSurface];
        hr = createSurface(directDraw, desc, &primarySurface, null);
        if (hr < 0) return hr;

        wrapper[1] = (uint)(nuint)primarySurface;   // the assignment the whole display path hinges on

        void** surfaceVtable = *(void***)primarySurface;

        // Ghidra: auStack_28[0] = 4 (DDSCAPS_BACKBUFFER); (**(*surface + 0x30))(surface, &caps, &out)
        uint* caps = stackalloc uint[4];
        caps[0] = DDSCAPS_BACKBUFFER; caps[1] = 0; caps[2] = 0; caps[3] = 0;
        void* backBuffer;
        var getAttachedSurface = (delegate* unmanaged[Stdcall]<void*, uint*, void**, int>)surfaceVtable[IDirectDrawSurface_GetAttachedSurface];
        hr = getAttachedSurface(primarySurface, caps, &backBuffer);
        if (hr < 0) return hr;

        // Ghidra: second 0x1b-dword zeroing loop, dwSize = 0x6c, then (**(*surface + 0x58))(...)
        byte* surfaceDesc = stackalloc byte[DdSurfaceDescSize];
        for (int i = 0; i < DdSurfaceDescSize; i++) surfaceDesc[i] = 0;
        *(uint*)(surfaceDesc + 0x00) = DdSurfaceDescSize;
        var getSurfaceDesc = (delegate* unmanaged[Stdcall]<void*, byte*, int>)surfaceVtable[IDirectDrawSurface_GetSurfaceDesc];
        hr = getSurfaceDesc(primarySurface, surfaceDesc);
        if (hr < 0) return hr;

        uint flags = *(uint*)(surfaceDesc + 0x04);
        uint height = *(uint*)(surfaceDesc + 0x08);
        uint width = *(uint*)(surfaceDesc + 0x0c);

        if ((flags & DDSD_WIDTH) != 0 && (flags & DDSD_HEIGHT) != 0)
        {
            // Win32 RECT here (four LONGs at wrapper[4..7]), not the four-short libgpu RECT used by
            // the winsys3d ordinals elsewhere in this file.
            Win32.SetRect((int*)(wrapper + 4), 0, 0, (int)width, (int)height);
            wrapper[0xe] = 0;   // Ghidra: param_1[0xe] = 0
            wrapper[0xf] = 1;   // Ghidra: param_1[0xf] = 1
            wrapper[3] = 1;     // Ghidra: param_1[3]  = 1
            hr = 0;
        }

        return hr;
    }

    /// <summary>
    /// Ghidra: FUN_00512b40 @ 0x00512b40 (`__thiscall`) â€” releases the surfaces and, crucially,
    /// calls <c>SetCooperativeLevel(hwnd, DDSCL_NORMAL)</c> to **drop the exclusive display mode**
    /// before releasing the DirectDraw object.
    /// <para>
    /// This is not optional bookkeeping. `winsys3d.dll`'s own 3D initialiser (`Ordinal_789(0, 0)`)
    /// runs later in RunMainLoop, loads `3dhelp.dll` and calls its `Create3DDevice`; if this
    /// reconstruction is still holding DirectDraw exclusively, that creation fails and the DLL
    /// shows "3D-Graphics initialisation error" and calls <c>ExitProcess(3)</c>
    /// (`winsys3d.c:5966`). Observed exactly that way before this was reconstructed.
    /// </para>
    /// </summary>
    private int ReleaseDirectDrawSurfaces(uint* wrapper, IntPtr hwnd)
    {
        // Ghidra: param_1[2] then param_1[1] â€” Release each if non-null, in that order.
        for (int slot = 2; slot >= 1; slot--)
        {
            void* obj = (void*)(nuint)wrapper[slot];
            if (obj != null)
            {
                void** vt = *(void***)obj;
                var release = (delegate* unmanaged[Stdcall]<void*, uint>)vt[IUnknown_Release];
                release(obj);
                wrapper[slot] = 0;
            }
        }

        void* directDraw = (void*)(nuint)wrapper[0];
        if (directDraw != null)
        {
            void** ddVtable = *(void***)directDraw;

            // Ghidra: (**(*dd + 0x50))(dd, hwnd, 8) â€” 8 = DDSCL_NORMAL. On failure the original
            // shows its own message box and returns the HRESULT without releasing.
            var setCooperativeLevel = (delegate* unmanaged[Stdcall]<void*, IntPtr, uint, int>)ddVtable[IDirectDraw_SetCooperativeLevel];
            int hr = setCooperativeLevel(directDraw, hwnd, 8);
            if (hr < 0)
            {
                Win32.MessageBoxA(hwnd, s_SetCooperativeLevelFailed, s_ErrorCaption, 0x10);
                return hr;
            }

            var release = (delegate* unmanaged[Stdcall]<void*, uint>)ddVtable[IUnknown_Release];
            release(directDraw);
            wrapper[0] = 0;

            // The exclusive mode is gone, so the safety-net hook has nothing left to restore.
            s_DisplayModeOwner = null;
        }

        wrapper[0xd] = 0xff00ff;                       // Ghidra: param_1[0xd] = 0xff00ff
        Win32.SetRect((int*)(wrapper + 4), 0, 0, 0, 0);
        Win32.SetRect((int*)(wrapper + 8), 0, 0, 0, 0);
        wrapper[3] = 0;
        return 0;
    }

    internal const int IUnknown_Release = 0x08 / 4;
    private const int IDirectDrawSurface_GetDC = 0x44 / 4;
    private const int IDirectDrawSurface_Restore = 0x6c / 4;
    private const int IDirectDrawSurface_ReleaseDC = 0x68 / 4;

    /// <summary>
    /// Ghidra: the blit inside FUN_00542c40 (`Blaze.c:89916-89929`) â€” draws the loaded
    /// TITLE640.BMP onto the DirectDraw primary surface. Used both for the initial paint and for
    /// the WM_COMMAND(lParam == 1) repaint.
    /// <para>
    /// The bitmap is the raw .BMP file image `LoadBitmapFile` returned, so the two offsets the
    /// original indexes are BITMAPFILEHEADER fields: <c>+10</c> is `bfOffBits` (pixel data) and
    /// <c>+0xe</c> is the BITMAPINFOHEADER that immediately follows the 14-byte file header.
    /// </para>
    /// </summary>
    private void BlitTitleScreen(MoviePlayerState player)
    {
        if (player.TitleBitmap == null) return;

        // RÃ©gime GDI de dÃ©marrage : la mÃªme image est Ã©crite dans la surface hors Ã©cran du backend,
        // qui sera prÃ©sentÃ©e dans la fenÃªtre du jeu.
        //
        // **Ajout, pas substitution.** Le chemin DirectDraw ci-dessous reste intact ; l'Ã©cran-titre
        // continue donc de s'afficher exactement comme avant, en plus d'alimenter notre surface.
        // C'est ce qui rend ce palier rÃ©versible : le retrait du chemin d'origine est un pas
        // sÃ©parÃ© et assumÃ© (change `render-title-screen-in-window`, groupe 6).
        BlitTitleScreenToStartupSurface(player);

        if (player.DirectDrawWrapper == null) return;

        // Ghidra: piVar1 = *(int **)(*(int *)(param_1 + 0x1c) + 4) â€” the wrapper's primary surface.
        // Null until FUN_00512a10 has run; the original null-checks it, and so does this.
        void* surface = (void*)(nuint)((uint*)player.DirectDrawWrapper)[1];
        if (surface == null) return;

        void** surfaceVtable = *(void***)surface;

        IntPtr hdc = IntPtr.Zero;
        var getDc = (delegate* unmanaged[Stdcall]<void*, IntPtr*, int>)surfaceVtable[IDirectDrawSurface_GetDC];
        int hrDc = getDc(surface, &hdc);

        // A surface goes "lost" whenever the display mode changes or another application takes the
        // display; every later call on it then fails until it is restored. Measured happening here
        // between two attract-screen repaints. The original carries the same Restore-and-retry
        // pairing elsewhere in this cluster (vtable 0x6c), so this is its own handling, not a patch.
        if (hrDc == unchecked((int)0x887601c2) /* DDERR_SURFACELOST */)
        {
            var restore = (delegate* unmanaged[Stdcall]<void*, int>)surfaceVtable[IDirectDrawSurface_Restore];
            if (restore(surface) < 0) return;
            hrDc = getDc(surface, &hdc);
        }

        if (hrDc < 0 || hdc == IntPtr.Zero) return;

        byte* bmp = player.TitleBitmap;
        void* bits = bmp + *(int*)(bmp + 10);   // bfOffBits
        void* info = bmp + 0xe;                 // BITMAPINFOHEADER

        Win32.StretchDIBits(hdc, 0, 0, TitleScreenWidth, TitleScreenHeight,
                      0, 0, TitleScreenWidth, TitleScreenHeight,
                      bits, info, 0 /* DIB_RGB_COLORS */, 0xcc0020 /* SRCCOPY */);

        var releaseDc = (delegate* unmanaged[Stdcall]<void*, IntPtr, int>)surfaceVtable[IDirectDrawSurface_ReleaseDC];
        releaseDc(surface, hdc);
    }

    // Les dimensions que l'original emploie sur ce chemin, en source comme en destination : le
    // `StretchDIBits` de Blaze.c:89916-89929 et le `SetDisplayMode` de FUN_00512a10 portent les
    // mÃªmes littÃ©raux. Ce ne sont donc pas des valeurs choisies par la reconstruction.
    private const int TitleScreenWidth = 0x280;    // 640
    private const int TitleScreenHeight = 0x1e0;   // 480

    /// <summary>
    /// Ã‰crit l'Ã©cran-titre dans la surface hors Ã©cran du backend, en rÃ©utilisant le mÃªme
    /// <c>StretchDIBits</c> et les mÃªmes dimensions que le chemin d'origine.
    /// </summary>
    private void BlitTitleScreenToStartupSurface(MoviePlayerState player)
    {
        IntPtr dc = StartupSurface.EnsureDc(TitleScreenWidth, TitleScreenHeight);
        if (dc == IntPtr.Zero) return;

        byte* bmp = player.TitleBitmap;
        void* bits = bmp + *(int*)(bmp + 10);   // bfOffBits
        void* info = bmp + 0xe;                 // BITMAPINFOHEADER

        int scanlines = Win32.StretchDIBits(dc, 0, 0, TitleScreenWidth, TitleScreenHeight,
                                      0, 0, TitleScreenWidth, TitleScreenHeight,
                                      bits, info, 0 /* DIB_RGB_COLORS */, 0xcc0020 /* SRCCOPY */);
        if (scanlines > 0) StartupSurface.MarkDrawn();

        // PrÃ©sentation : cet appel est la frontiÃ¨re d'image du rÃ©gime GDI, ce qu'EndScene est au
        // rÃ©gime Direct3D. Un Ã©chec ne dÃ©grade que ce redessin â€” journalisÃ© une fois, jamais
        // propagÃ©, jamais interruptif pour la boucle d'attract.
        //
        // **Aucune pompe de messages ici**, contrairement Ã  ce que fait EndScene. La boucle
        // d'attract (`ShowTitleScreenAndWait`) est elle-mÃªme une pompe `PeekMessageA(NULL, â€¦, PM_REMOVE)`
        // qui tourne en permanence et dispatche : les messages de notre fenÃªtre sont donc dÃ©jÃ 
        // servis. Vider la file ici consommerait les `WM_KEYDOWN` qui terminent l'attract, et le
        // dÃ©marrage n'atteindrait plus jamais le menu.
        if (StartupSurface.HasContent && !PresentStartupSurface() && !s_StartupPresentReported)
        {
            s_StartupPresentReported = true;
            Console.Error.WriteLine("[GDI] prÃ©sentation de l'Ã©cran-titre indisponible â€” redessin ignorÃ©");
        }

    }

    private bool s_StartupPresentReported;

    /// <summary>
    /// PrÃ©sente la surface de dÃ©marrage dans la fenÃªtre du jeu. Rend faux si la fenÃªtre n'existe
    /// pas encore ou si le contexte de pÃ©riphÃ©rique est indisponible â€” l'appelant se contente alors
    /// de sauter ce redessin.
    /// </summary>
    private bool PresentStartupSurface()
    {
        IntPtr hwnd = D3D11Window.Handle;
        if (hwnd == IntPtr.Zero) return false;

        (int w, int h) = D3D11Window.ClientSize();
        return StartupSurface.PresentTo(hwnd, w, h);
    }

    /// <summary>
    /// Ghidra: FUN_00512930 @ 0x00512930 (`__thiscall`) â€” constructs the DirectDraw wrapper object.
    /// Every field the teardown and the blit later read is initialised **here**, which is why
    /// skipping it is not harmless: `Malloc` maps to <c>NativeMemory.Alloc</c> and does not zero,
    /// so `ReleaseDirectDrawSurfaces` read an uninitialised <c>wrapper[2]</c> as a COM pointer and
    /// dereferenced garbage. The original zeroes exactly these slots.
    /// </summary>
    private void ConstructDirectDrawWrapper(uint* wrapper)
    {
        // Ghidra: SetRect(ESI + 0x10, 0,0,0,0) and SetRect(ESI + 0x20, 0,0,0,0) â€” two Win32 RECTs.
        Win32.SetRect((int*)(wrapper + 4), 0, 0, 0, 0);
        Win32.SetRect((int*)(wrapper + 8), 0, 0, 0, 0);

        wrapper[0] = 0;          // Ghidra: MOV [ESI],EDI       â€” IDirectDraw
        wrapper[1] = 0;          // Ghidra: MOV [ESI+0x4],EDI   â€” primary surface
        wrapper[2] = 0;          // Ghidra: MOV [ESI+0x8],EDI   â€” the slot that caused the crash
        wrapper[3] = 0;          // Ghidra: MOV [ESI+0xc],EDI
        wrapper[0xc] = 0;        // Ghidra: MOV [ESI+0x30],EDI
        wrapper[0xd] = 0xff00ff; // Ghidra: MOV [ESI+0x34],0xff00ff â€” the colour key
        wrapper[0xe] = 0;        // Ghidra: MOV [ESI+0x38],EDI
        wrapper[0xf] = 1;        // Ghidra: MOV [ESI+0x3c],1
        wrapper[0x10] = 0;       // Ghidra: MOV [ESI+0x40],EDI

        // Ghidra: sub = FUN_00547220(0x18); if (sub) FUN_00512d10(sub, this, hwnd, &local);
        //         this[0x44] = (result != 0) ? result + 0xc : 0;
        // The 0x18-byte object is the "Overlay Callback Object" â€” a COM sink whose vtable slots are
        // known (0x0054D028) but which nothing can ever call back into here, because no real
        // DirectDraw overlay is registered against it. Leaving this null is the original's own
        // allocation-failure path, not an invented one.
        wrapper[0x11] = 0;       // TODO(REVERSE): FUN_00512d10's overlay callback sink.
    }

    // Ghidra: s_SetCooperativeLevel___failed_to_g_00563178 â€” only the truncated symbol name
    // survives in Blaze.c; the reconstructed text is a best guess, same caveat as the other
    // message strings.
    private readonly byte* s_SetCooperativeLevelFailed = ToAnsiZ("SetCooperativeLevel() failed to go back to normal mode.");

    // Safety net for the deviation noted above â€” not part of the original.
    private void* s_DisplayModeOwner;
    private void RegisterDisplayModeRestore(void* directDraw)
    {
        if (s_DisplayModeOwner != null) return;
        s_DisplayModeOwner = directDraw;
        AppDomain.CurrentDomain.ProcessExit += static (_, _) => Vid.RestoreDisplayModeIfHeld();
        AppDomain.CurrentDomain.UnhandledException += static (_, _) => Vid.RestoreDisplayModeIfHeld();
    }

    private void RestoreDisplayModeIfHeld()
    {
        void* dd = s_DisplayModeOwner;
        if (dd == null) return;
        s_DisplayModeOwner = null;
        void** vtable = *(void***)dd;
        var restore = (delegate* unmanaged[Stdcall]<void*, int>)vtable[IDirectDraw_RestoreDisplayMode];
        restore(dd);
    }

    /// <summary>
    /// Ghidra: FUN_00542a00 @ 0x00542a00 (`__thiscall`) â€” **stops playback**. This is the function
    /// that was missing when the attract loop had no reachable exit: it clears the playing flag and
    /// publishes a result, which is what lets PlayMoviePumpLoop return and PlayMovie report non-zero.
    /// </summary>
    private void StopMoviePlayback(MoviePlayerState player, IntPtr hwnd, int result)
    {
        // Ghidra: if (param_1[8] != 0) { KillTimer(param_2, 1); param_1[8] = 0; }
        if (player.TimerArmed != 0)
        {
            Win32.KillTimer(hwnd, 1);
            player.TimerArmed = 0;
        }

        // Ghidra: guarded by param_1[6] (the stream) and *(param_1[6] + 8). The body tears the
        // stream down through its own vtable (FUN_00512c80 / FUN_00543180 / slots 0x18 and 0x08).
        // Stream is null in this reconstruction â€” TODO(REVERSE) #13 â€” so the guard skips it exactly
        // as the original would with no stream open. Nothing is invented here.
        if (player.Stream != null)
        {
            // TODO(REVERSE): stream teardown via the unrecoverable vtable; unreachable while
            // TrySetupMovieStream is a stub.
        }

        player.Playing = false;   // Ghidra: *param_1 = 0
        player.Result = result;   // Ghidra: param_1[0xb] = param_3
    }

    /// <summary>Ghidra: FUN_00542970 @ 0x00542970 (`__thiscall`) â€” WM_ACTIVATEAPP handling.</summary>
    private void OnMoviePlayerActivate(MoviePlayerState player, int activated)
    {
        if (player.Playing || player.AttractActive != 0)
        {
            if (activated != 0)
            {
                // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup. Ce couple SetFocus +
                // SW_MAXIMIZE rend la fenÃªtre VfwClass plein Ã©cran **Ã  chaque activation de
                // l'application** pendant l'attract : la neutraliser Ã  la crÃ©ation ne suffit pas,
                // l'original la rÃ©tablit ici. C'est le site le plus important des quatre.
                // Le PostMessage qui suit dÃ©clenche le redessin et reste actif dans les deux cas.
                if (!s_WindowedStartup)
                {
                    Win32.SetFocus(player.HWnd);
                    Win32.ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE */);
                }
                Win32.PostMessageA(player.HWnd, 0x111 /* WM_COMMAND */, 1, 1);
                return;
            }
            StopMoviePlayback(player, player.HWnd, 1);
            Win32.ShowWindow(player.HWnd, 2 /* SW_SHOWMINIMIZED */);
        }
    }

    /// <summary>Ghidra: FUN_005429e0 @ 0x005429e0 (`__thiscall`) â€” WM_TIMER: step one movie frame.</summary>
    private void OnMoviePlayerTimer(MoviePlayerState player, nuint timerId)
    {
        if (timerId == 1 && player.Field28 != 0)
        {
            // TODO(REVERSE): FUN_00512c80 â€” the DirectDraw wrapper's per-frame flip. Not reachable
            // while the wrapper's vtable is unreconstructed (#13); Field28 is only set once a
            // stream is really open.
        }
    }

    /// <summary>
    /// Ghidra: FUN_00542600 @ 0x00542600 â€” the `"VfwClass"` window procedure. Ghidra never
    /// disassembled it because the address is only ever *stored* into `WNDCLASSA.lpfnWndProc`,
    /// never called, so it produced no body until the bytes were forced to disassemble; the
    /// listing lives in `Legacy/Raw/FUN_00542600.txt`.
    /// <para>
    /// Its message set is exactly the one the attract-mode title screen waits on, which is what
    /// makes the attract loop terminable at all.
    /// </para>
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static nint MoviePlayerWindowProc(IntPtr hwnd, uint msg, nuint wParam, nint lParam)
    {
        // Ghidra: if (DAT_005dffa8 == 0) return DefWindowProcA(...);
        MoviePlayerState? player = Vid.s_MoviePlayer;
        if (player == null) return Win32.DefWindowProcA(hwnd, msg, wParam, lParam);

        switch (msg)
        {
            case 0x0002: // WM_DESTROY â€” falls through to `return 0` in the original, doing nothing
                return 0;

            case 0x0010: // WM_CLOSE
                Win32.PostQuitMessage(0);
                return 0;

            case 0x001c: // WM_ACTIVATEAPP
                Vid.OnMoviePlayerActivate(player, (int)wParam);
                return 0;

            case 0x0100: // WM_KEYDOWN â€” only Escape stops playback; other keys fall through to 0
                if (wParam == 0x1b /* VK_ESCAPE */)
                {
                    Vid.StopMoviePlayback(player, hwnd, 1);
                }
                return 0;

            case 0x0113: // WM_TIMER
                Vid.OnMoviePlayerTimer(player, wParam);
                return 0;

            case 0x0201: // WM_LBUTTONDOWN
            case 0x0204: // WM_RBUTTONDOWN
            case 0x0207: // WM_MBUTTONDOWN
            case 0x03b5: // MM_JOY1BUTTONDOWN
            case 0x03b6: // MM_JOY2BUTTONDOWN
                Vid.StopMoviePlayback(player, hwnd, 1);
                return 0;

            case 0x0464: // MM_MCINOTIFY
                // Ghidra: iVar2 = FUN_00542560(hwnd, wParam, lParam); if (iVar2 == 0) return 0;
                //         FUN_00542a00(hwnd, 0);
                // TODO(REVERSE): FUN_00542560 is in Blaze.c but only matters once MCI playback is
                // actually started, which this reconstruction never does. Treated as "not handled".
                return 0;

            default:
                return Win32.DefWindowProcA(hwnd, msg, wParam, lParam);
        }
    }

    /// <summary>Ghidra: FUN_00542740 @ 0x00542740 â€” creates the hidden window the movie player renders into.</summary>
    private void CreateMoviePlayerWindow(MoviePlayerState player)
    {
        IntPtr hInstance = Win32.GetModuleHandleA(null);
        player.HInstance = hInstance;

        // Ghidra: SUB ESP,0x28 reserves exactly one WNDCLASSA, and the ten stores that follow are
        // its fields in order (Legacy/Raw/FUN_00542740.txt). Note the cursor comes from
        // wsyskern.dll's resource 10, not from a system cursor.
        var wc = new WNDCLASSA
        {
            style = 0,
            lpfnWndProc = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, uint, nuint, nint, nint>)&MoviePlayerWindowProc,
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = hInstance,
            hIcon = IntPtr.Zero,
            hCursor = Win32.LoadCursorA(Win32.GetModuleHandleA(ToAnsiZ("WSYSKERN.DLL")), (IntPtr)10),
            hbrBackground = Win32.GetStockObject(4 /* BLACK_BRUSH */),
            lpszMenuName = null,
            lpszClassName = s_VfwClassName,
        };

        Win32.RegisterClassA(&wc);

        // Ghidra: CreateWindowExA(8, "VfwClass", "Blaze & Blade", 0x80000000, 0, 0, 10, 10, 0, 0,
        //                          hInstance, 0) â€” WS_EX_TOPMOST + WS_POPUP, created tiny and
        // resized below, which is why this window is invisible in practice.
        player.HWnd = Win32.CreateWindowExA(
            8 /* WS_EX_TOPMOST */, s_VfwClassName, s_MovieWindowTitle, 0x80000000 /* WS_POPUP */,
            0, 0, 10, 10, IntPtr.Zero, IntPtr.Zero, hInstance, null);

        // Ghidra: *ESI = 0; *(ESI+0xc) = 0 â€” done before CreateWindowExA in the original; ordering
        // is immaterial since the window procedure early-outs while DAT_005dffa8 is null.
        player.Playing = false;
        player.TitleBitmapLoaded = 0;

        // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup.
        // Ces deux appels sont la moitiÃ© Â« fenÃªtre Â» du plein Ã©cran de dÃ©marrage : un popup
        // WS_EX_TOPMOST maximisÃ© puis Ã©tendu Ã  2000x2000. La fenÃªtre reste crÃ©Ã©e â€” `joySetCapture`
        // et la procÃ©dure de fenÃªtre en dÃ©pendent â€” mais n'est plus montrÃ©e.
        // MesurÃ© (tÃ¢che 2.2) : les touches terminent toujours l'attract, la boucle qui les lit
        // faisant un `PeekMessageA(NULL, â€¦)` Ã  l'Ã©chelle du thread.
        if (s_WindowedStartup) return;

        Win32.ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE */);
        Win32.MoveWindow(player.HWnd, 0, 0, 0x7d0, 0x7d0, 1);
    }

    /// <summary>Ghidra: FUN_00542940 @ 0x00542940 â€” destroys the movie player's hidden window.</summary>
    private void DestroyMoviePlayerWindow(MoviePlayerState player)
    {
        if (player.HWnd != IntPtr.Zero)
        {
            Win32.DestroyWindow(player.HWnd);
        }
        player.HWnd = IntPtr.Zero;
        Win32.UnregisterClassA(s_VfwClassName, player.HInstance);
    }

    /// <summary>
    /// Ghidra: FUN_00542800 @ 0x00542800. Allocates the DirectDraw-wrapper and custom stream
    /// sub-objects. In the original, this always returns success (1) once memory allocation
    /// succeeds â€” the stream object's own construction can silently leave its fields zeroed
    /// without failing the overall open. TODO(REVERSE): see the subsystem banner above â€” the
    /// stream object's vtable can't be faithfully constructed, so `player.Stream` stays null.
    /// </summary>
    private bool OpenMovieStream(MoviePlayerState player)
    {
        // Ghidra: iVar1 = FUN_00547220(0x48); then, only if that succeeded,
        //         iVar1 = FUN_00512930(*(param_1 + 0x10));   <- the HWnd
        //         *(param_1 + 0x1c) = iVar1;
        //         if (iVar1 == 0) return 0;
        //
        // FUN_00512930's disassembly (Legacy/Raw/FUN_00512930.txt) ends `MOV EAX,ESI; RET 0x4` â€” it
        // returns `this` unconditionally, even when its own internal Malloc fails. So the original's
        // `if (iVar1 == 0) return 0` only fires when the 0x48-byte allocation below fails, and the
        // open otherwise **succeeds**. A previous revision returned false here and justified it as
        // the original's no-DirectDraw path; that justification was wrong, and the deviation is now
        // unnecessary because the window procedure is reconstructed and the attract loop can end.
        void* ddrawWrapper = Malloc(0x48);
        if (ddrawWrapper == null) return false;

        // Ghidra: iVar1 = FUN_00512930(iVar1, param_1[0x10]) â€” the wrapper's constructor. It is not
        // optional: Malloc does not zero, and the teardown reads slots this initialises.
        ConstructDirectDrawWrapper((uint*)ddrawWrapper);
        player.DirectDrawWrapper = ddrawWrapper;

        player.Field24 = 2;   // Ghidra: *(param_1 + 0x24) = 2
        player.Field28 = 1;   // Ghidra: *(param_1 + 0x28) = 1

        void* stream = Malloc(0x120);
        player.Stream = null; // TODO(REVERSE): FUN_005431e0 builds a DirectShow graph object here
                              // (CLSID_FilterGraph / IID_IGraphBuilder, see #13). Left null so every
                              // stream-guarded branch takes the original's own "no stream" path.
        if (stream != null) Free(stream);

        // Ghidra: FUN_00512a10(*(param_1 + 0x10)) â€” the tail call of FUN_00542800, with the wrapper
        // in ECX and the window handle as its stack argument. This is what fills wrapper[1] with the
        // primary surface, so the attract screen's blit has something to draw into.
        SetupDirectDrawSurfaces((uint*)player.DirectDrawWrapper, player.HWnd);

        return true; // faithful: the original returns 1 once the wrapper allocation succeeded
    }

    /// <summary>Ghidra: FUN_005428d0 @ 0x005428d0 â€” releases the sub-objects allocated by OpenMovieStream.</summary>
    private void CloseMovieStream(MoviePlayerState player)
    {
        if (player.Stream != null)
        {
            // Ghidra: FUN_00543000() [reset stream vtable] + FUN_00547210(iVar1) [[Free]].
            Free(player.Stream);
        }
        player.Stream = null;

        // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup.
        if (player.HWnd != IntPtr.Zero && !s_WindowedStartup)
        {
            Win32.MoveWindow(player.HWnd, 0, 0, 2000, 2000, 1);
        }

        if (player.DirectDrawWrapper != null)
        {
            // Ghidra: FUN_00512b40(param_1[4]) â€” the real teardown. Skipping it is not harmless:
            // it is what drops the exclusive display mode, and without it winsys3d.dll's own 3D
            // init (Ordinal_789) cannot acquire DirectDraw and aborts the process.
            ReleaseDirectDrawSurfaces((uint*)player.DirectDrawWrapper, player.HWnd);
            Free(player.DirectDrawWrapper);
        }
        player.DirectDrawWrapper = null;

        // DEVIATION CONDITIONNELLE â€” voir s_WindowedStartup.
        if (player.HWnd != IntPtr.Zero && !s_WindowedStartup)
        {
            Win32.ShowWindow(player.HWnd, 3 /* SW_MAXIMIZE, per Ghidra literal */);
        }
    }

    /// <summary>Ghidra: FUN_00542f60 @ 0x00542f60 â€” allocates the movie-player struct and creates its window.</summary>
    internal void InitMoviePlayer()
    {
        // Ghidra: the allocation is followed by an explicit *(p+0xc) = 0; *(p+8) = 0 â€” the title
        // bitmap cache starts empty. A fresh MoviePlayerState is already zeroed, so this mirrors
        // the original rather than adding behavior.
        s_MoviePlayer = new MoviePlayerState { TitleBitmap = null, TitleBitmapLoaded = 0 };
        CreateMoviePlayerWindow(s_MoviePlayer);
    }

    /// <summary>Ghidra: FUN_00542f90 @ 0x00542f90 â€” destroys the window and frees the movie-player struct.</summary>
    internal void ShutdownMoviePlayer()
    {
        if (s_MoviePlayer == null) return;
        DestroyMoviePlayerWindow(s_MoviePlayer);
        if (s_MoviePlayer.Stream != null || s_MoviePlayer.DirectDrawWrapper != null)
        {
            CloseMovieStream(s_MoviePlayer);
        }

        // Ghidra: Free(*(DAT_005dffa8 + 8)) then FUN_00547210(iVar1) â€” note the two *different*
        // heaps, used correctly by the original: the title bitmap goes back to wsyskern (which
        // allocated it in LoadBitmapFile), the player struct to the CRT. The original calls this
        // unconditionally; wsyskern's Free tolerates null. Our struct is GC-managed, so only the
        // bitmap needs releasing here.
        Wsyskern.WsysFree(s_MoviePlayer.TitleBitmap);
        s_MoviePlayer.TitleBitmap = null;
        s_MoviePlayer.TitleBitmapLoaded = 0;

        s_MoviePlayer = null;
    }

    /// <summary>Ghidra: FUN_00542f40 @ 0x00542f40 -> FUN_00542800 â€” tries to open the movie stream.</summary>
    internal bool TryOpenMovieStream()
        => s_MoviePlayer != null && OpenMovieStream(s_MoviePlayer);

    // Ghidra: s__sDATA_TITLE640_BMP_0056e6d4 â€” the wsprintfA format string is "%sDATA\TITLE640.BMP",
    // its "%s" filled with the CD drive root (DAT_005631f8). Only Ghidra's derived symbol name
    // survives in Blaze.c, not the literal bytes, so the text below is a best guess reconstructed
    // from that name â€” same caveat as the movie filenames above and REVERSE_STATUS.md TODOs #17/#22.
    private readonly byte* s_TitleBitmapFileName = ToAnsiZ("DATA/TITLE640.BMP");

    /// <summary>
    /// Ghidra: FUN_00542f30 @ 0x00542f30 -> FUN_00542c40 â€” the attract-mode title screen: shows
    /// TITLE640.BMP and waits, returning non-zero to tell the attract loop to stop attracting.
    /// <para>
    /// Returns 1 when the caller should leave the attract loop (title bitmap unavailable, or â€” in
    /// the original â€” the user pressed something), 0 when the attract loop should continue to the
    /// next movie.
    /// </para>
    /// <para>
    /// TODO(REVERSE): Ghidra declares the FUN_00542f30 thunk as <c>void</c> (Blaze.c:90063) even
    /// though it tail-calls FUN_00542c40, which it types <c>undefined4 __fastcall</c>, and the call
    /// site in FUN_00529c10 reads the result (Blaze.c:74442, 74450). The call site is authoritative:
    /// this is the same unreliable-signature-recovery problem catalogued in REVERSE_STATUS.md, and
    /// following the <c>void</c> declaration here previously deleted two of the attract loop's four
    /// exits, hanging startup.
    /// </para>
    /// </summary>
    internal int ShowTitleScreenAndWait()
    {
        if (s_MoviePlayer == null) return 1;
        MoviePlayerState player = s_MoviePlayer;

        // Ghidra: *(param_1+0x2c) = 0
        player.Result = 0;

        // Ghidra: if (*(param_1+0xc) == 0) { ... } â€” load the title bitmap once, then cache it.
        if (player.TitleBitmapLoaded == 0)
        {
            byte* path = res.BuildCdPath(s_TitleBitmapFileName);
            player.TitleBitmap = Wsyskern.LoadBitmapFile(path, 0 /* silent on failure â€” see wsyskern.md */);
            Free(path);

            // Ghidra: if (iVar3 == 0) return 1; â€” the original's own graceful degradation when the
            // CD (or the file) is absent. Reporting 1 is what breaks FUN_00529c10's attract loop,
            // letting startup continue to engine initialization instead of attracting forever.
            if (player.TitleBitmap == null) return 1;

            player.TitleBitmapLoaded = 1;
        }

        // ---------------------------------------------------------------------------------------
        // Deliberately not reconstructed â€” the rest of FUN_00542c40 (Blaze.c:89916-89975):
        //
        //   * `*(param_1+4) = 1` then a StretchDIBits blit of the 640x480 bitmap through the
        //     DirectDraw wrapper's own vtable â€” GetDC at offset 0x44, ReleaseDC at 0x68 on the
        //     inner object at *(param_1+0x1c)+4. Both calls are null-checked in the original, so
        //     this part would in fact degrade safely.
        //   * joySetCapture on both joysticks, then a message-pump loop that exits with 1 on
        //     WM_KEYDOWN (0x100), WM_LBUTTONDOWN (0x201), WM_RBUTTONDOWN (0x204),
        //     WM_MBUTTONDOWN (0x207), MM_JOY1BUTTONDOWN (0x3b5) or MM_JOY2BUTTONDOWN (0x3b6) â€”
        //     freeing the bitmap on the way out â€” re-blits on WM_TIMER (0x111, lParam 1), raises
        //     the fatal exception on WM_QUIT (0x12), and exits with 0 once timeGetTime shows more
        //     than 19999 ms elapsed since the last blit.
        //
        // Ghidra: the GetDC / StretchDIBits / ReleaseDC sequence at Blaze.c:89916-89929.
        BlitTitleScreen(player);
        // ---------------------------------------------------------------------------------------

        // Ghidra: DVar4 = timeGetTime(); *(param_1+4) = 1; joySetCapture x2; then a message pump
        // that exits with 1 on input and with 0 once 19999 ms have elapsed since the last redraw.
        uint lastDraw = Win32.timeGetTime();
        player.AttractActive = 1;
        Win32.joySetCapture(player.HWnd, 0, 0x32, 1);
        Win32.joySetCapture(player.HWnd, 1, 0x32, 1);

        MSG msg;
        while (true)
        {
            while (Win32.PeekMessageA(&msg, IntPtr.Zero, 0, 0, 1 /* PM_REMOVE */) != 0)
            {
                // Ghidra: the key/mouse/joystick set that ends the attract screen. Handled here
                // *and* in the window procedure â€” the original tests them in this loop directly,
                // which is what makes the wait end even when the message is not dispatched.
                if (msg.message is 0x100 or 0x201 or 0x204 or 0x207 or 0x3b5 or 0x3b6)
                {
                    Wsyskern.WsysFree(player.TitleBitmap);
                    player.TitleBitmap = null;
                    player.TitleBitmapLoaded = 0;
                    player.AttractActive = 0;
                    Win32.joyReleaseCapture(0);
                    Win32.joyReleaseCapture(1);
                    return 1;
                }

                // Ghidra: WM_COMMAND with lParam == 1 re-blits and restarts the 20-second clock.
                if (msg.message == 0x111 && msg.lParam == 1)
                {
                    BlitTitleScreen(player);
                    lastDraw = Win32.timeGetTime();
                }

                if (msg.message == 0x12 /* WM_QUIT */)
                {
                    RaiseQuitException();
                }

                Win32.DispatchMessageA(&msg);
            }

            // Ghidra: if (19999 < timeGetTime() - DVar4) { *(param_1+4) = 0; joyRelease; return 0; }
            if (Win32.timeGetTime() - lastDraw > 19999)
            {
                player.AttractActive = 0;
                Win32.joyReleaseCapture(0);
                Win32.joyReleaseCapture(1);
                return 0;
            }
        }
    }

    /// <summary>Ghidra: FUN_00542f50 @ 0x00542f50 -> FUN_005428d0 â€” closes the stream if one is open.</summary>
    internal void CloseMovieStreamIfOpen()
    {
        if (s_MoviePlayer != null) CloseMovieStream(s_MoviePlayer);
    }

    /// <summary>
    /// Ghidra: FUN_00542a70 @ 0x00542a70 â€” opens the underlying video stream on the just-created
    /// window and arms the 100ms per-frame timer. TODO(REVERSE): entirely built out of calls
    /// through the unrecoverable custom "stream"/DirectDraw-wrapper vtables (see banner above);
    /// always reports failure here, matching how the original already handles a failed open.
    /// </summary>
    private bool TrySetupMovieStream(MoviePlayerState player)
        => false;

    /// <summary>
    /// Ghidra: FUN_00542b70 @ 0x00542b70 â€” plays the stream opened by TrySetupMovieStream, pumping
    /// window messages (and raising the original's WM_QUIT-triggered fatal exception faithfully)
    /// until playback finishes, with joystick input captured for skip/cancel for the duration.
    /// </summary>
    private int PlayMoviePumpLoop(MoviePlayerState player, byte* path)
    {
        // Ghidra: lstrcpyA(param_1[6] + 0xc, param_2) â€” copies into the (unrecoverable) stream
        // object's own filename buffer; nothing to copy into without that object, so skipped.
        player.Result = 0;

        Win32.joySetCapture(player.HWnd, 0, 0x32, 1);
        Win32.joySetCapture(player.HWnd, 1, 0x32, 1);

        if (TrySetupMovieStream(player))
        {
            // HAZARD â€” do not enable TrySetupMovieStream without also signalling completion.
            // This loop terminates only when `player.Playing` is cleared, and the sole thing that
            // clears it in the original is a callback through the movie stream object's vtable,
            // whose slots have no decompiled body anywhere in Blaze.c (REVERSE_STATUS.md TODO #13).
            // The loop is currently unreachable *only* because TrySetupMovieStream is a stub
            // returning false. Making that stub return true before the completion signal exists
            // re-hangs startup â€” this time in a tight PeekMessage spin rather than a bare one.
            // This is the same defect class that TODO #31 records for the attract loop.
            player.Playing = true;
            MSG msg;
            do
            {
                while (Win32.PeekMessageA(&msg, IntPtr.Zero, 0, 0, 1 /* PM_REMOVE */) != 0)
                {
                    if (msg.message == 0x12 /* WM_QUIT */)
                    {
                        RaiseQuitException();
                    }
                    Win32.DispatchMessageA(&msg);
                }
            } while (player.Playing);
        }

        Win32.joyReleaseCapture(0);
        Win32.joyReleaseCapture(1);
        return player.Result;
    }

    /// <summary>
    /// Ghidra: FUN_00542eb0 @ 0x00542eb0. Builds "&lt;cd-drive-root&gt;&lt;movieFileName&gt;" and plays it.
    /// `flag` is passed through from RunMainLoop but only reaches the (unrecoverable) stream vtable.
    /// </summary>
    internal int PlayMovie(byte* movieFileName, int flag)
    {
        if (s_MoviePlayer == null) return 0;

        byte* path = stackalloc byte[64];
        int i = 0;
        for (byte* p = mem.s_CdDataRootPath; *p != 0 && i < 60; p++, i++) path[i] = *p;
        for (byte* p = movieFileName; *p != 0 && i < 63; p++, i++) path[i] = *p;
        path[i] = 0;

        return PlayMoviePumpLoop(s_MoviePlayer, path);
    }

    // Ghidra: s_TEPS_AVI_005684f4 / s_OPENING_AVI_005684e8 / s_ANI01_AVI_005684dc â€” only Ghidra's
    // auto-derived symbol names survive in Blaze.c, not the literal bytes; best-guess filenames.
    internal readonly byte* s_MovieNameTeps = ToAnsiZ("TEPS.AVI");
    internal readonly byte* s_MovieNameOpening = ToAnsiZ("OPENING.AVI");
    internal readonly byte* s_MovieNameAni01 = ToAnsiZ("ANI01.AVI");


    /// <summary>
    /// Ghidra: FUN_00529e40 @ 0x00529e40 â€” pushes a fixed "shading defaults" struct into
    /// winsys3d.dll via ordinals 378/368. Field semantics beyond their literal values are unknown.
    /// </summary>
    internal void SetShadingDefaults()
    {
        // Ghidra: uStack_28=0xa0, uStack_24=0x78, then 5 (short,short) pairs of (64000, 0x600),
        // laid out as 8 little-endian dwords consumed by Ordinal_368 below.
        uint* buf = stackalloc uint[8];
        buf[0] = 0xa0;
        buf[1] = 0x78;
        buf[2] = 0x0600fa00; // (local_1e=0x600, local_20=64000=0xfa00)
        buf[3] = 0x0600fa00; // (local_1a, local_1c)
        buf[4] = 0x0600fa00; // (local_16, local_18)
        buf[5] = 0x0600fa00; // (local_12, local_14)
        buf[6] = 0x0000fa00; // (padding, local_10)
        buf[7] = 0;          // local_c

        Winsys3d.Ordinal_378(0, 0); // TODO(REVERSE): Ordinal_378 takes no visible args at this call site in
                            // Blaze.c beyond an implicit/optimized-out pair; passed (0,0) as a
                            // faithful placeholder â€” see winsys3d.md.
        Winsys3d.Ordinal_368(buf);
    }

    /// <summary>
    /// Ghidra: FUN_00529eb0 @ 0x00529eb0 â€” initializes several on-screen viewport/surface
    /// descriptors (via ordinals 658/661) and either fills the main render buffer with a
    /// checkerboard pattern + tiled blit (ordinals 674/667), or clears it to black (ordinals
    /// 665/667), depending on <see cref="s_DebugModeLatch_005633a8"/> â€” le damier est la branche de
    /// mise au point (verrou à 0), l'effacement en noir la branche normale (verrou à 1, sa valeur
    /// au chargement).
    /// </summary>
    internal void InitViewportSurfaces()
    {
        // Ghidra renders these four calls with their arguments spilled to unattributed stack slots
        // (uStack_18..puStack_50) rather than as call arguments, and gives the fourth only three of
        // its five. Reading the slots back in ascending address order â€” the cdecl push order â€” and
        // cross-checking against the DLL bodies recovers all four completely:
        //
        //   Ordinal_658(&DAT_005e98dc, 0, 0xf0, 0x140, 0xf0)   disp env 0 : (0,240,320,240)
        //   Ordinal_658(&DAT_005e994c, 0, 0,    0x140, 0xf0)   disp env 1 : (0,  0,320,240)
        //   Ordinal_661(&DAT_005e9880, 0, 0,    0x140, 0xf0)   draw env 0 : (0,  0,320,240)
        //   Ordinal_661(&DAT_005e98f0, 0, 0xf0, 0x140, 0xf0)   draw env 1 : (0,240,320,240)
        //
        // i.e. the standard PSX double-buffer setup: while one buffer is displayed at y=0 the other
        // is drawn at y=240, and vice versa. 0x140/0xf0 are 320x240.
        //
        // The previous reconstruction had the argument order rotated, passed stack scratch buffers
        // instead of the four globals, and passed **null** for the fourth â€” which is what made
        // winsys3d.dll write through a null pointer and take down the process.
        Winsys3d.SetDefDispEnv(mem.s_DispEnv0_005e98dc, 0, 0xf0, 0x140, 0xf0);
        Winsys3d.SetDefDispEnv(mem.s_DispEnv1_005e994c, 0, 0, 0x140, 0xf0);
        Winsys3d.SetDefDrawEnv(mem.s_DrawEnv0_005e9880, 0, 0, 0x140, 0xf0);
        Winsys3d.SetDefDrawEnv(mem.s_DrawEnv1_005e98f0, 0, 0xf0, 0x140, 0xf0);

        // Ghidra: the 12 global stores that follow the calls. Each one lands inside one of the four
        // structs above at an offset that matches the DISPENV/DRAWENV layout exactly â€” which is the
        // independent confirmation that the identification is right. They override some of what the
        // two DLL helpers just defaulted.
        //
        // _DAT_005e98e4/_e6/_e8/_ea = disp env 0's `screen` rect  (0,0,320,240) â€” replaces the
        // 0x100-wide default SetDefDispEnv writes.
        *(short*)(mem.s_DispEnv0_005e98dc + 0x08) = 0;
        *(short*)(mem.s_DispEnv0_005e98dc + 0x0a) = 0;
        *(short*)(mem.s_DispEnv0_005e98dc + 0x0c) = 0x140;
        *(short*)(mem.s_DispEnv0_005e98dc + 0x0e) = 0xf0;

        // _DAT_005e9954/_56/_58/_5a = disp env 1's `screen` rect, same values.
        *(short*)(mem.s_DispEnv1_005e994c + 0x08) = 0;
        *(short*)(mem.s_DispEnv1_005e994c + 0x0a) = 0;
        *(short*)(mem.s_DispEnv1_005e994c + 0x0c) = 0x140;
        *(short*)(mem.s_DispEnv1_005e994c + 0x0e) = 0xf0;

        // DAT_005e9896 / _DAT_005e9898 and DAT_005e9906 / _DAT_005e9908 = each draw env's `dtd`
        // (dither, offset 0x16 â€” already 1 from SetDefDrawEnv, so this store is a no-op) and `isbg`
        // (offset 0x18 â€” flipped from 0 to 1, i.e. clear the framebuffer to the background colour
        // before drawing). Ghidra shows the isbg stores as 2-byte writes overlapping the adjacent
        // `r0` byte; r0 was just zeroed by SetDefDrawEnv, so a byte store is equivalent here.
        *(mem.s_DrawEnv0_005e9880 + 0x16) = 1;
        *(mem.s_DrawEnv0_005e9880 + 0x18) = 1;
        *(mem.s_DrawEnv1_005e98f0 + 0x16) = 1;
        *(mem.s_DrawEnv1_005e98f0 + 0x18) = 1;

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return; // TODO(REVERSE): winsys3d.dll's MainMem not yet resolvable in this environment

        if (mem.s_DebugModeLatch_005633a8 == 0)
        {
            // Ghidra: two runs of `*(undefined4*)(mainMem + 4 + off) = 0x80008000;` â€” a checkerboard fill.
            for (int off = 0x1c3ffc - 4; off >= 0x1c1ffc; off -= 4)
                *(uint*)(mainMem + 4 + off) = 0x80008000;
            for (int off = 0x1b7ffc - 4; off >= 0x1afffc; off -= 4)
                *(uint*)(mainMem + 4 + off) = 0x80008000;

            uint frameOffset = 0;
            short* tileRect = stackalloc short[14];
            for (int i = 0; i < 0x20; i++)
            {
                Winsys3d.Ordinal_674(tileRect, mainMem + 0x1b0000);
                Winsys3d.Ordinal_667(0);
                frameOffset += 0x40;
                if (frameOffset == 0x400) frameOffset = 0;
            }
        }
        else
        {
            short* rect = stackalloc short[14];
            Winsys3d.Ordinal_665(rect, 0, 0, 0);
            Winsys3d.Ordinal_667(0);
        }
    }
}
