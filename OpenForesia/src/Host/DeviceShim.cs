// =====================================================================
// Façade devant le device 3D de 3dhelp.dll, et point de bascule vers le backend Direct3D 11.
//
// Le chemin d'affichage du jeu passe par la vtable de ce device : winsys3d.dll l'appelle sans
// jamais toucher aux champs de l'objet. Il suffit donc de reprendre les slots du chemin chaud pour
// détourner tout le rendu, sans modifier ni winsys3d ni 3dhelp — qui restent des références de
// rétro-ingénierie non modifiées.
//
// Mécanisme : on n'alloue pas de faux objet. On copie la vtable du vrai device, on remplace les
// slots voulus, et on écrit notre copie dans `device[0]`. Conséquences :
//   - `this` reste le vrai objet, donc les slots non repris fonctionnent inchangés ;
//   - nos thunks reçoivent le vrai `this` et peuvent réémettre vers la fonction d'origine ;
//   - les appels internes que 3dhelp se fait à lui-même via sa vtable passent aussi par nous.
//
// Variables d'environnement :
//   OPENFORESIA_LEGACY_3DHELP=1   rétablit le pass-through intégral vers 3dhelp
//   OPENFORESIA_DUMP_GEOMETRY=1   relève sommets et décalages sur quelques primitives
//
// Cartographie des slots : Documentation/DLL/3dhelp.md
// =====================================================================

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `Blaze` porte les aides que cette classe appelle : ToAnsiZ, VirtualAlloc, GetModuleHandleA,
// RenderSizeFromSizeSetting, s_WindowedStartup. La dependance Host -> Blaze est assumee.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

internal static unsafe class DeviceShim
{
    internal const int DeviceVtableSlots = 64;     // la vtable réelle en expose 59, arrondi supérieur

    private static void** s_ShimRealVtable;       // la vtable d'origine, cible des réémissions
    private static void** s_ShimOurVtable;        // notre copie, installée dans device[0]

    // Budget **par type d'appel**, et non global : une première version à budget unique a été
    // entièrement consommée par une rafale de 398 BindTexture(NULL) au démarrage, sans qu'aucun
    // dessin n'ait été journalisé. Chaque slot a donc son propre compteur.
    // Fenêtre d'échantillonnage : les premiers appels viennent de l'attract, pas du menu. On saute
    // donc un préambule avant de journaliser, sinon le budget est consommé par la mauvaise phase.
    private const int ShimLogSkip = 400;
    private const int ShimLogBudget = 24;
    private static readonly int[] s_ShimSeen = new int[DeviceVtableSlots];

    // =====================================================================
    // Étape 3 — le chemin chaud passe par le backend Direct3D 11 au lieu d'être réémis vers 3dhelp.
    //
    // La bascule est conservée pour pouvoir comparer les deux chemins sans recompiler :
    // OPENFORESIA_LEGACY_3DHELP=1 rétablit le pass-through intégral.
    // =====================================================================

    private static readonly bool s_UseD3D11 =
        Environment.GetEnvironmentVariable("OPENFORESIA_LEGACY_3DHELP") != "1";

    /// <summary>Vrai une fois le backend initialisé, ce qui n'arrive qu'à la première pose de viewport.</summary>
    private static bool s_BackendLive;

    /// <summary>Ombrage courant : <c>D3DSHADE_FLAT</c> vaut 1, Gouraud vaut 2.</summary>
    private static bool s_FlatShading;

    /// <summary>Dimensions de rendu demandées par le jeu, relevées à la pose du viewport.</summary>
    private static int s_SceneWidth, s_SceneHeight;

    /// <summary>
    /// Exécute le chemin backend en rapportant toute exception au lieu de la laisser remonter.
    ///
    /// Ces thunks sont appelés depuis <c>winsys3d</c>, donc depuis du code natif. Une exception
    /// managée qui traverse une frame native ne « lève » pas : elle termine le processus sans
    /// message exploitable. La règle du dépôt veut qu'un manque soit bruyant — ici, bruyant veut
    /// dire journalisé et signalé, pas propagé.
    /// </summary>
    internal static bool Guarded(string quoi, Action action)
    {
        try
        {
            action();
            return true;
        }
        catch (Exception ex)
        {
            if (s_GuardReported.Add($"{quoi}:{ex.Message}"))
                Console.Error.WriteLine($"[D3D11] {quoi} : {ex.GetType().Name} — {ex.Message}");
            return false;
        }
    }

    private static readonly HashSet<string> s_GuardReported = [];

    /// <summary>
    /// Crée le backend au **premier début de scène**, et non à la pose du viewport.
    ///
    /// La pose du viewport paraissait le point naturel — c'est là que les dimensions de rendu sont
    /// connues. Mesuré : y créer la fenêtre et le device empêche le jeu d'atteindre sa boucle de
    /// rendu (48 lignes de rendu dans les relevés précédents, zéro après). La pose du viewport a
    /// lieu au milieu de l'initialisation du jeu, et y intercaler l'initialisation de D3D11/DXGI la
    /// perturbe. On ne fait donc qu'y **relever** les dimensions ; la création attend la boucle.
    /// </summary>
    private static void EnsureBackend()
    {
        if (!s_UseD3D11 || s_SceneWidth <= 0 || s_SceneHeight <= 0) return;

        if (!s_BackendLive)
        {
            D3D11Backend.Initialize(s_SceneWidth, s_SceneHeight, "OpenForesia");
            s_BackendLive = true;

            // Bascule de régime, irréversible : la swap chain existe, elle seule présente
            // désormais. La surface GDI du démarrage est libérée et ne sera pas réallouée.
            StartupSurface.Retire();

            // Tâche 4.3 : 3dhelp ne présente plus rien depuis que le chemin chaud passe par le
            // backend. On lui reprend l'affichage, sinon sa fenêtre en mode exclusif recouvre la
            // nôtre et le mode d'affichage reste celui qu'il a négocié.
            D3D11Window.ReleaseLegacyDisplay();
            D3D11Window.Focus();

            // Le mode d'affichage vient d'être rendu ; on remet la fenêtre là où elle était avant
            // que les changements d'affichage de `3dhelp` ne la déplacent. La continuité visuelle
            // entre l'écran-titre et le menu en dépend.
            D3D11Window.RestorePlacement();
        }
        else
        {
            D3D11Backend.EnsureSceneTarget(s_SceneWidth, s_SceneHeight);
        }
    }

    /// <summary>Vrai dans la fenêtre d'échantillonnage ; compte toujours les appels.</summary>
    private static bool ShimTake(int slot)
    {
        int n = ++s_ShimSeen[slot / 4];
        return n > ShimLogSkip && n <= ShimLogSkip + ShimLogBudget;
    }

    /// <summary>Bilan par slot, pour voir ce qui a été appelé et combien de fois.</summary>
    private static void ShimReport()
    {
        var sb = new System.Text.StringBuilder("[SHIM] bilan :");
        (int Slot, string Nom)[] noms =
        [
            (0x5C, "Clear(TARGET)"), (0x60, "Clear(Z)"), (0x64, "BindTexture"),
            (0x8C, "ShadeMode"), (0xB0, "BeginScene"), (0xB4, "EndScene"),
            (0xBC, "DrawPrimitive"), (0xDC, "Flip"),
        ];
        foreach ((int slot, string nom) in noms) sb.Append($" {nom}={s_ShimSeen[slot / 4]}");
        if (s_DumpGeometry && s_GeoMaxX > float.MinValue)
            sb.Append($" | enveloppe X=[{s_GeoMinX:F0}..{s_GeoMaxX:F0}] Y=[{s_GeoMinY:F0}..{s_GeoMaxY:F0}] cible={s_SceneWidth}x{s_SceneHeight}");
        Console.Error.WriteLine(sb.ToString());
    }

    /// <summary>
    /// Installe la façade. À appeler juste après <c>Ordinal_789</c>, quand le device existe.
    /// Sans effet si le device est absent ou si la façade est déjà posée.
    /// </summary>
    [DllImport("user32.dll")] private static extern IntPtr GetDC(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
    [DllImport("gdi32.dll")] private static extern int GetDeviceCaps(IntPtr hdc, int index);

    /// <summary>
    /// ECHAFAUDAGE TEMPORAIRE. Journalise le mode d'affichage actuellement actif. Sert à comparer
    /// le mode du bureau au mode que 3dhelp impose via SetDisplayMode (3dhelp.c:1275) : si les deux
    /// coïncident, aucun changement de mode physique n'a lieu au lancement.
    /// </summary>
    internal static void LogDisplayMode(string label)
    {
        IntPtr hdc = GetDC(IntPtr.Zero);
        if (hdc == IntPtr.Zero) { Console.Error.WriteLine($"[MODE] {label}: GetDC a échoué"); return; }
        int w = GetDeviceCaps(hdc, 8 /* HORZRES */);
        int h = GetDeviceCaps(hdc, 10 /* VERTRES */);
        int bpp = GetDeviceCaps(hdc, 12 /* BITSPIXEL */);
        int hz = GetDeviceCaps(hdc, 116 /* VREFRESH */);
        ReleaseDC(IntPtr.Zero, hdc);
        Console.Error.WriteLine($"[MODE] {label}: {w}x{h} {bpp}bpp {hz}Hz");
    }

    internal static void InstallDeviceShim()
    {
        if (s_ShimOurVtable != null) return;

        // Avec le device synthétique il n'y a rien à envelopper : sa vtable **est** déjà la nôtre,
        // chemin chaud compris. La poser une seconde fois ferait de nos propres thunks la « vtable
        // d'origine » vers laquelle ils se replient — donc une récursion infinie, observée comme un
        // débordement de pile dans `Shim_BindTexture`.
        if (SyntheticDevice.s_SyntheticDevice != null)
        {
            Console.Error.WriteLine("[SHIM] device synthétique en place — façade inutile");
            return;
        }

        IntPtr mod = Win32.GetModuleHandleA(ToAnsiZ("winsys3d.dll"));
        if (mod == IntPtr.Zero) return;

        void** device = *(void***)((byte*)mod + 0x7BE760);   // Ghidra: DAT_011be760
        if (device == null)
        {
            Console.Error.WriteLine("[SHIM] pas de device 3D, façade non posée");
            return;
        }

        s_ShimRealVtable = (void**)device[0];

        void** copy = (void**)NativeMemory.Alloc((nuint)(DeviceVtableSlots * sizeof(void*)));
        for (int i = 0; i < DeviceVtableSlots; i++) copy[i] = s_ShimRealVtable[i];

        // Seuls les slots du chemin d'affichage sont interceptés (Documentation/DLL/3dhelp.md).
        copy[0x5C / 4] = (delegate* unmanaged[Thiscall]<void*, void*, int, void>)&Shim_ClearTarget;
        copy[0x60 / 4] = (delegate* unmanaged[Thiscall]<void*, void*, int, void>)&Shim_ClearZ;
        copy[0x64 / 4] = (delegate* unmanaged[Thiscall]<void*, void*, void>)&Shim_BindTexture;
        copy[0x8C / 4] = (delegate* unmanaged[Thiscall]<void*, uint, void>)&Shim_ShadeMode;
        copy[0xB0 / 4] = (delegate* unmanaged[Thiscall]<void*, void>)&Shim_BeginScene;
        copy[0xB4 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Shim_EndScene;
        copy[0xBC / 4] = (delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)&Shim_DrawPrimitive;
        copy[0xDC / 4] = (delegate* unmanaged[Thiscall]<void*, void*, uint, int>)&Shim_Flip;
        copy[0x50 / 4] = (delegate* unmanaged[Thiscall]<void*, int, int, int, int, int>)&Shim_SetViewport;

        s_ShimOurVtable = copy;
        device[0] = copy;

        Console.Error.WriteLine(
            $"[SHIM] posée — device=0x{(nint)device:X8} vtable d'origine=0x{(nint)s_ShimRealVtable:X8} " +
            $"copie=0x{(nint)copy:X8} ; 8 slots interceptés");

        InstallInnerShims(device);
    }

    /// <summary>
    /// ECHAFAUDAGE TEMPORAIRE — tâche 1.1 de <c>render-title-screen-in-window</c>.
    ///
    /// Crée la fenêtre du jeu en tête de <c>RunMainLoop</c>, donc <b>avant l'écran-titre</b>, sans
    /// device ni swap chain.
    ///
    /// Deux mesures autorisent ce placement. La tâche 1.1 a séparé les deux moitiés de la mesure
    /// consignée dans <see cref="EnsureBackend"/> — créer la fenêtre <b>et</b> le device pendant
    /// l'initialisation empêche le jeu d'atteindre sa boucle de rendu — et montré que la fenêtre
    /// seule est sans effet : c'est le device qui est en cause, et il reste créé au premier
    /// <c>BeginScene</c>. La tâche 2.2 a vérifié que les touches terminent toujours l'attract avec
    /// cette fenêtre au premier plan.
    ///
    /// Dimensions : celles que le jeu déduira lui-même de son réglage <c>Size</c>, plutôt qu'une
    /// taille arbitraire suivie d'un redimensionnement quand la pose du viewport révèle la vraie.
    ///
    /// Dimensions : 640x480, l'espace logique de référence du moteur — établi dans
    /// <c>Documentation/DLL/winsys3d.md</c>. La taille de la fenêtre n'est de toute façon jamais
    /// observée par le jeu ; la swap chain se dimensionnera sur la zone cliente réelle.
    /// </summary>
    internal static void CreateGameWindow()
    {
        (int width, int height) = RenderSizeFromSizeSetting();

        // Un échec ici n'est pas fatal : sans fenêtre à nous, le jeu retombe sur le chemin
        // d'affichage d'origine. Bruyant plutôt que silencieux, conformément à la règle du dépôt,
        // mais pas propagé — cet appel est sur le chemin de démarrage du jeu.
        Guarded("CreateGameWindow", () =>
        {
            D3D11Backend.EnsureWindow(width, height, "OpenForesia");

            // Géométrie de référence, relevée avant tout changement d'affichage. `3dhelp` en
            // négocie encore un à son initialisation, et Windows repositionne les fenêtres à cette
            // occasion : sans ce relevé, la fenêtre se déplace entre l'écran-titre et le menu.
            D3D11Window.SavePlacement();

            // En démarrage fenêtré, notre fenêtre est la seule du jeu : c'est elle qui doit
            // recevoir la saisie.
            //
            // L'original s'arroge le premier plan par `OnMoviePlayerActivate` — `SetFocus` puis
            // `SW_MAXIMIZE` sur la fenêtre `VfwClass` — à chaque activation de l'application. Cette
            // fenêtre n'étant plus affichée, personne ne le fait plus. Or la boucle d'attract lit
            // les touches par `PeekMessageA(NULL, …)`, qui ne rapporte que les messages **du thread
            // appelant** : sans le focus, les frappes partent vers une autre application et le
            // démarrage n'atteint jamais le menu. Mesuré exactement ainsi.
            if (s_WindowedStartup) D3D11Window.Focus();
        });
    }

    // =====================================================================
    // Niveau inférieur : les objets COM que 3dhelp détient.
    //
    // Les wrappers de 3dhelp **avalent les codes de retour** (Clear et la liaison de texture
    // ignorent leur HRESULT). Pour voir si Direct3D refuse quelque chose, il faut intercepter les
    // interfaces elles-mêmes. Même technique : copie de vtable, remplacement de slots, écriture
    // dans l'objet — ce qui n'affecte que cette instance, pas la classe.
    //
    // Champs (indices en dwords, cf. Documentation/DLL/3dhelp.md) :
    //   [0x0D] surface retournée par Flip     [0x17] IDirect3DDevice2     [0x18] IDirect3DViewport
    // =====================================================================

    private const int ComVtableSlots = 40;        // couvre IDirect3DDevice2 (33 méthodes)

    private static void** s_RealD3dVtable;
    private static void** s_RealViewportVtable;
    private static void** s_RealSurfaceVtable;

    private static void** CopyComVtable(void** obj)
    {
        void** real = (void**)obj[0];
        void** copy = (void**)NativeMemory.Alloc((nuint)(ComVtableSlots * sizeof(void*)));
        for (int i = 0; i < ComVtableSlots; i++) copy[i] = real[i];
        return copy;
    }

    private static void InstallInnerShims(void** device)
    {
        void** d3d = (void**)device[0x17];
        void** viewport = (void**)device[0x18];
        void** surface = (void**)device[0x0D];

        Console.Error.WriteLine(
            $"[SHIM] objets COM : d3d=0x{(nint)d3d:X8} viewport=0x{(nint)viewport:X8} " +
            $"surface=0x{(nint)surface:X8}");

        if (d3d != null)
        {
            s_RealD3dVtable = (void**)d3d[0];
            void** c = CopyComVtable(d3d);
            c[0x28 / 4] = (delegate* unmanaged[Stdcall]<void*, int>)&Com_BeginScene;
            c[0x2C / 4] = (delegate* unmanaged[Stdcall]<void*, int>)&Com_EndScene;
            c[0x34 / 4] = (delegate* unmanaged[Stdcall]<void*, void*, int>)&Com_SetCurrentViewport;
            c[0x74 / 4] = (delegate* unmanaged[Stdcall]<void*, uint, uint, void*, uint, uint, int>)&Com_DrawPrimitive;
            d3d[0] = c;
        }

        if (viewport != null)
        {
            s_RealViewportVtable = (void**)viewport[0];
            void** c = CopyComVtable(viewport);
            c[0x30 / 4] = (delegate* unmanaged[Stdcall]<void*, uint, void*, uint, int>)&Com_Clear;
            c[0x44 / 4] = (delegate* unmanaged[Stdcall]<void*, void*, int>)&Com_SetViewport2;
            viewport[0] = c;
        }

        if (surface != null)
        {
            s_RealSurfaceVtable = (void**)surface[0];
            void** c = CopyComVtable(surface);
            c[0x2C / 4] = (delegate* unmanaged[Stdcall]<void*, void*, uint, int>)&Com_SurfaceFlip;
            surface[0] = c;
        }
    }

    // Compteurs séparés des slots du wrapper : ce sont d'autres objets.
    private static readonly int[] s_ComSeen = new int[8];

    private static bool ComTake(int i)
    {
        int n = ++s_ComSeen[i];
        return n > ShimLogSkip && n <= ShimLogSkip + ShimLogBudget;
    }

    /// <summary>Journalise systématiquement un HRESULT non nul, même hors fenêtre : c'est le signal recherché.</summary>
    private static void ComResult(int index, string nom, int hr)
    {
        if (hr != 0)
        {
            s_ComFailures[index]++;
            if (s_ComFailures[index] <= 8)
            {
                Console.Error.WriteLine($"[COM] {nom} ECHEC 0x{hr:X8} {DescribeD3dError(hr)}");
            }
        }
        else if (ComTake(index))
        {
            Console.Error.WriteLine($"[COM] {nom} -> S_OK");
        }
    }

    private static readonly int[] s_ComFailures = new int[8];

    internal static void ReportComFailures()
    {
        Console.Error.WriteLine(
            $"[COM] echecs cumules : Clear={s_ComFailures[0]} BeginScene={s_ComFailures[1]} " +
            $"EndScene={s_ComFailures[2]} DrawPrimitive={s_ComFailures[3]} Flip={s_ComFailures[4]}");
    }

    private static string DescribeD3dError(int hr) => (uint)hr switch
    {
        0x887601C2 => "DDERR_SURFACELOST",
        0x88760205 => "DDERR_WRONGMODE",
        0x887600C0 => "DDERR_NOEXCLUSIVEMODE",
        0x88760087 => "DDERR_INVALIDPARAMS",
        0x8876021C => "DDERR_NOTFLIPPABLE",
        0x88760213 => "DDERR_SURFACEBUSY",
        0x88760082 => "DDERR_INVALIDOBJECT",
        0x88760001 => "D3DERR_BADMAJORVERSION",
        0x8876000A => "D3DERR_SCENE_IN_SCENE",
        0x8876000B => "D3DERR_SCENE_NOT_IN_SCENE",
        0x8876000C => "D3DERR_SCENE_BEGIN_FAILED",
        0x8876000D => "D3DERR_SCENE_END_FAILED",
        _ => "(non repertorie)"
    };

    /// <summary>
    /// Slot +0x50 — la pose du viewport, appelée par <c>SetShift</c>. Elle renvoie un HRESULT que
    /// <c>SetShift</c> **ignore** : si la pose échoue, le viewport garde sa position précédente et
    /// rien ne le signale. C'est le suspect direct pour « menu en haut à gauche » plutôt que centré.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static int Shim_SetViewport(void* self, int x, int y, int w, int h)
    {
        // CORRECTION du 2026-08-14. Ce commentaire disait que le décalage (x, y) « n'a plus
        // d'objet », le backend rendant dans une cible à ses dimensions exactes. C'était faux, et
        // ça se voyait : le jeu émet ses sommets en coordonnées **absolues dans sa surface
        // d'affichage**, pas relatives au viewport. Les verser tels quels dans une cible qui ne
        // couvre que le viewport décale toute l'image de (x, y) — bande noire à gauche et en haut,
        // autant de rogné à droite et en bas. Mesuré : la recherche d'alignement entre notre rendu
        // et celui de `3dhelp` tombe exactement sur (32, 24), l'origine que le jeu pose ici.
        // L'origine est donc transmise au backend, qui la retranche.
        s_SceneWidth = w;
        s_SceneHeight = h;
        D3D11Backend.SetViewportOrigin(x, y);

        if (s_TraceViewport)
            Console.Error.WriteLine(
                $"[TRACE] +0x50 SetViewport({x}, {y}, {w}x{h}) — Size={CurrentSize()}, " +
                $"image {s_ShimSeen[0xB0 / 4]}, dessin #{s_ShimSeen[0xBC / 4]}");

        if (s_UseD3D11) return 0;

        if (s_ShimRealVtable == null) return 0;
        int hr = ((delegate* unmanaged[Thiscall]<void*, int, int, int, int, int>)
            s_ShimRealVtable[0x50 / 4])(self, x, y, w, h);
        Console.Error.WriteLine(
            $"[VP] SetViewport({x}, {y}, {w}x{h}) -> 0x{hr:X8} {(hr == 0 ? "OK" : DescribeD3dError(hr))}");
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int Com_SetViewport2(void* self, void* vp)
    {
        int hr = ((delegate* unmanaged[Stdcall]<void*, void*, int>)s_RealViewportVtable[0x44 / 4])(self, vp);
        uint* d = (uint*)vp;
        Console.Error.WriteLine(
            $"[VP] IDirect3DViewport2::SetViewport2 taille={d[0]} x={d[1]} y={d[2]} " +
            $"w={d[3]} h={d[4]} -> 0x{hr:X8} {(hr == 0 ? "OK" : DescribeD3dError(hr))}");
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int Com_SetCurrentViewport(void* self, void* viewport)
    {
        int hr = ((delegate* unmanaged[Stdcall]<void*, void*, int>)s_RealD3dVtable[0x34 / 4])(self, viewport);
        Console.Error.WriteLine(
            $"[VP] IDirect3DDevice2::SetCurrentViewport -> 0x{hr:X8} {(hr == 0 ? "OK" : DescribeD3dError(hr))}");
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int Com_Clear(void* self, uint count, void* rects, uint flags)
    {
        int hr = ((delegate* unmanaged[Stdcall]<void*, uint, void*, uint, int>)s_RealViewportVtable[0x30 / 4])(self, count, rects, flags);
        ComResult(0, $"Clear(n={count}, flags=0x{flags:X})", hr);
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int Com_BeginScene(void* self)
    {
        int hr = ((delegate* unmanaged[Stdcall]<void*, int>)s_RealD3dVtable[0x28 / 4])(self);
        ComResult(1, "BeginScene", hr);
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int Com_EndScene(void* self)
    {
        int hr = ((delegate* unmanaged[Stdcall]<void*, int>)s_RealD3dVtable[0x2C / 4])(self);
        ComResult(2, "EndScene", hr);
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int Com_DrawPrimitive(void* self, uint type, uint vertexType, void* vertices,
                                         uint count, uint flags)
    {
        int hr = ((delegate* unmanaged[Stdcall]<void*, uint, uint, void*, uint, uint, int>)
            s_RealD3dVtable[0x74 / 4])(self, type, vertexType, vertices, count, flags);
        ComResult(3, $"DrawPrimitive(type={type}, n={count})", hr);
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static int Com_SurfaceFlip(void* self, void* target, uint flags)
    {
        int hr = ((delegate* unmanaged[Stdcall]<void*, void*, uint, int>)s_RealSurfaceVtable[0x2C / 4])(self, target, flags);
        ComResult(4, $"Flip(flags=0x{flags:X})", hr);
        return hr;
    }


    // --- thunks ---------------------------------------------------------------------------

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_ClearTarget(void* self, void* rects, int count)
    {
        ShimTake(0x5C);
        if (s_UseD3D11 && s_BackendLive) { D3D11Backend.ClearColor(0f, 0f, 0f); return; }
        // Avec le device synthétique il n'y a pas de vtable d'origine : le repli devient neutre.
        if (s_ShimRealVtable == null) return;
        ((delegate* unmanaged[Thiscall]<void*, void*, int, void>)s_ShimRealVtable[0x5C / 4])(self, rects, count);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_ClearZ(void* self, void* rects, int count)
    {
        ShimTake(0x60);
        if (s_UseD3D11 && s_BackendLive) { D3D11Backend.ClearDepth(); return; }
        // Avec le device synthétique il n'y a pas de vtable d'origine : le repli devient neutre.
        if (s_ShimRealVtable == null) return;
        ((delegate* unmanaged[Thiscall]<void*, void*, int, void>)s_ShimRealVtable[0x60 / 4])(self, rects, count);
    }

    /// <summary>
    /// Slot +0x64 — liaison de texture. L'argument est un objet texture de <c>3dhelp</c> dont le
    /// backend ne sait rien : ses pixels vivent dans une surface DirectDraw créée par un chemin qui
    /// n'est pas encore identifié (tâche 3.5).
    ///
    /// En attendant, la liaison est **enregistrée mais neutre** : le backend dessine avec sa texture
    /// blanche. Les primitives texturées sortent donc en aplat de leur couleur diffuse. C'est
    /// volontairement visible — pas un no-op silencieux — pour que l'état d'avancement se lise à
    /// l'écran.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_BindTexture(void* self, void* texture)
    {
        ShimTake(0x64);
        if (s_UseD3D11 && s_BackendLive) { s_BoundTexture = texture; return; }
        // Avec le device synthétique il n'y a pas de vtable d'origine : le repli devient neutre.
        if (s_ShimRealVtable == null) return;
        ((delegate* unmanaged[Thiscall]<void*, void*, void>)s_ShimRealVtable[0x64 / 4])(self, texture);
    }

    private static void* s_BoundTexture;

    // =====================================================================
    // États de mélange — portés avec la liaison de texture (tâche 3.5), parce que sans eux
    // l'image sort exactement deux fois trop claire.
    //
    // Ce qui l'a établi : le panneau du menu ressortait à (14,7,57) là où `3dhelp` donne (7,3,28),
    // un facteur 2 net. La cause est l'alpha des sommets — la diffuse des quads du panneau porte
    // 0x7F — combiné au mélange que `FUN_00a03db0` (`winsys3d.c:3157-3160`) active dès que les
    // textures font 16 bits : ALPHABLENDENABLE(1), SRCBLEND(5 = SRCALPHA),
    // DESTBLEND(6 = INVSRCALPHA). Sans mélange, ces quads se posaient opaques.
    // =====================================================================

    /// <summary>2 = <c>D3DTBLEND_MODULATE</c>, 4 = <c>D3DTBLEND_MODULATEALPHA</c>.</summary>
    private static uint s_TextureMapBlend = 2;
    private static bool s_AlphaBlendEnable;
    private static uint s_SrcBlend = 2, s_DestBlend = 1;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_TextureMapBlend(void* self, uint mode)
    {
        if (s_TextureMapBlend != mode)
            Console.Error.WriteLine($"[SHIM] TEXTUREMAPBLEND = {mode} " +
                                    $"({(mode == 4 ? "MODULATEALPHA" : mode == 2 ? "MODULATE" : "inattendu")})");
        s_TextureMapBlend = mode;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_AlphaBlendEnable(void* self, uint on) => s_AlphaBlendEnable = on != 0;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_SrcBlend(void* self, uint blend) => s_SrcBlend = blend;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_DestBlend(void* self, uint blend) => s_DestBlend = blend;

    /// <summary>
    /// Vrai si le mélange actif est bien celui que le backend sait poser. Le jeu n'emploie que deux
    /// couples : <c>(2, 1)</c> = ONE/ZERO, c'est-à-dire opaque, et <c>(5, 6)</c> =
    /// SRCALPHA/INVSRCALPHA. Tout autre couple est signalé une fois plutôt que rendu de travers.
    /// </summary>
    private static bool AlphaBlendActive()
    {
        if (!s_AlphaBlendEnable) return false;
        if (s_SrcBlend == 5 && s_DestBlend == 6) return true;

        if (s_GuardReported.Add($"blend:{s_SrcBlend}/{s_DestBlend}"))
            Console.Error.WriteLine(
                $"[SHIM] couple de mélange non porté SRCBLEND={s_SrcBlend} DESTBLEND={s_DestBlend} " +
                "— rendu opaque en attendant");
        return false;
    }

    /// <summary>Relevé de géométrie et de décalages : <c>OPENFORESIA_DUMP_GEOMETRY=1</c>.</summary>
    private static readonly bool s_DumpGeometry =
        Environment.GetEnvironmentVariable("OPENFORESIA_DUMP_GEOMETRY") == "1";

    private static float s_GeoMinX = float.MaxValue, s_GeoMaxX = float.MinValue;
    private static float s_GeoMinY = float.MaxValue, s_GeoMaxY = float.MinValue;

    // =====================================================================
    // ECHAFAUDAGE TEMPORAIRE — tâche 4.5 de `replace-3d-backend-with-d3d11`.
    //
    // Deux échantillons ont montré que `Size` vaut 21 à `BeginScene` et 11 au dix-millième dessin de
    // la même image : il change donc *dans* l'image. Ce relevé donne la **séquence complète** sur
    // quelques images — chaque appel au viewport avec ses quatre arguments, et chaque transition de
    // `Size` avec l'indice du dessin où elle survient.
    //
    // OPENFORESIA_TRACE_VP=1
    // =====================================================================

    private static readonly bool s_TraceViewport =
        Environment.GetEnvironmentVariable("OPENFORESIA_TRACE_VP") == "1";

    private static byte* s_Winsys3dBase;
    private static int s_TracedSize = int.MinValue;

    /// <summary>Valeur courante de <c>DAT_00a2c498</c>, le niveau <c>Size</c> de winsys3d.</summary>
    private static int CurrentSize()
    {
        if (s_Winsys3dBase == null)
        {
            IntPtr mod = Win32.GetModuleHandleA(ToAnsiZ("winsys3d.dll"));
            if (mod == IntPtr.Zero) return int.MinValue;
            s_Winsys3dBase = (byte*)mod;
        }
        return *(int*)(s_Winsys3dBase + 0x2C498);
    }

    /// <summary>Vrai pendant la fenêtre d'observation : quelques images après la mise en régime.</summary>
    private static bool TracingFrame() =>
        s_TraceViewport && s_ShimSeen[0xB0 / 4] is >= 60 and <= 62;

    /// <summary>Note une transition de <c>Size</c>, où qu'elle survienne.</summary>
    private static void TraceSizeChange(string quand)
    {
        if (!s_TraceViewport) return;
        int size = CurrentSize();
        if (size == s_TracedSize) return;
        s_TracedSize = size;
        Console.Error.WriteLine(
            $"[TRACE] Size -> {size} ({(size + 9) * 0x20}x{(size + 9) * 0x18}) " +
            $"à {quand}, image {s_ShimSeen[0xB0 / 4]}, dessin #{s_ShimSeen[0xBC / 4]}");
    }

    /// <summary>
    /// ECHAFAUDAGE TEMPORAIRE. Relève les globales de décalage de <c>winsys3d</c> qui composent la
    /// translation appliquée à tous les sommets (<c>winsys3d.c:4934</c>, <c>SetShift</c>) :
    ///
    /// <code>
    /// DAT_011be770 = base_x[DAT_00a2c49c] + (DAT_011be780 * (Size+9) * 0x20) / 0x140 + shift_x
    /// </code>
    ///
    /// Sert à identifier laquelle de ces valeurs part en vrille lors des lancements où la géométrie
    /// sort de l'écran. Décalages relevés depuis la base de <c>winsys3d.dll</c> (Ghidra la place à
    /// 0x00a00000).
    /// </summary>
    private static void LogShiftGlobals()
    {
        IntPtr mod = Win32.GetModuleHandleA(ToAnsiZ("winsys3d.dll"));
        if (mod == IntPtr.Zero) return;
        byte* b = (byte*)mod;

        int composeX = *(int*)(b + 0x7BE770);   // DAT_011be770
        int composeY = *(int*)(b + 0x7BE774);   // DAT_011be774
        int shiftX = *(int*)(b + 0x7BE778);     // DAT_011be778
        int shiftY = *(int*)(b + 0x7BE77C);     // DAT_011be77c
        int absX = *(int*)(b + 0x7BE780);       // DAT_011be780
        int absY = *(int*)(b + 0x7BE784);       // DAT_011be784
        int resIndex = *(int*)(b + 0x2C49C);    // DAT_00a2c49c
        int size = *(int*)(b + 0x2C498);        // DAT_00a2c498

        int baseX = 0, baseY = 0;
        if ((uint)resIndex < 8)
        {
            baseX = *(int*)(b + 0x2C4D8 + resIndex * 4);
            baseY = *(int*)(b + 0x2C4F0 + resIndex * 4);
        }

        Console.Error.WriteLine(
            $"[SHIFT] composé=({composeX},{composeY}) base=({baseX},{baseY}) " +
            $"shift=({shiftX},{shiftY}) abs=({absX},{absY}) resIndex={resIndex} size={size}");

        // Test discriminant de la tâche 4.5. Les sommets ne sont pas mis à l'échelle en ligne : ils
        // traversent une table de flottants indexée par la coordonnée brute
        // (`winsys3d.c:10328`, `x = table[coord] + composé`). `SetShift` la choisit par
        // `base + Size * 16000` ; une branche « haute résolution » de `SetDispEnv`
        // (`winsys3d.c:5060`) la remplace par deux adresses figées et force `Size` à zéro.
        //
        // Lire la table tranche : `table[100] / 100` **est** le facteur réellement appliqué.
        // Attendu si tout va bien avec Size=21 : 3.0. Si l'on trouve 1.0, la branche haute
        // résolution est active et le correctif se situe côté `Size` / DISPENV, pas côté viewport.
        uint tableX = *(uint*)(b + 0x2C4A0);   // PTR_DAT_00a2c4a0
        uint tableY = *(uint*)(b + 0x2C4A4);   // PTR_DAT_00a2c4a4
        uint attendu = (uint)((nint)mod + 0x31138 + size * 16000);

        Console.Error.WriteLine(
            $"[SHIFT] tableX=0x{tableX:X8} tableY=0x{tableY:X8} " +
            $"attendu pour Size={size} : 0x{attendu:X8} {(tableX == attendu ? "(concorde)" : "(NE CONCORDE PAS — branche haute résolution ?)")}");

        if (tableX != 0)
        {
            float* t = (float*)(nint)tableX;
            Console.Error.WriteLine(
                $"[SHIFT] table[0]={t[0]:F3} table[1]={t[1]:F3} table[100]={t[100]:F3} " +
                $"table[320]={t[320]:F3} → facteur ≈ {(t[100] != 0 ? t[100] / 100f : 0f):F3}");
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_ShadeMode(void* self, uint value)
    {
        ShimTake(0x8C);
        if (s_UseD3D11 && s_BackendLive) { s_FlatShading = value == 1; return; }
        // Avec le device synthétique il n'y a pas de vtable d'origine : le repli devient neutre.
        if (s_ShimRealVtable == null) return;
        ((delegate* unmanaged[Thiscall]<void*, uint, void>)s_ShimRealVtable[0x8C / 4])(self, value);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_BeginScene(void* self)
    {
        ShimTake(0xB0);
        if (s_ShimSeen[0xB0 / 4] % 60 == 1) ShimReport();

        if (TracingFrame())
            Console.Error.WriteLine($"[TRACE] ===== image {s_ShimSeen[0xB0 / 4]} : BeginScene, Size={CurrentSize()}, cible={s_SceneWidth}x{s_SceneHeight}");
        TraceSizeChange("BeginScene");

        if (s_UseD3D11)
        {
            // **Appelée à chaque image, pas seulement à la création (2026-08-18).** Le
            // `if (!s_BackendLive)` d'avant rendait la seconde branche d'`EnsureBackend` — le
            // redimensionnement de la cible sur le viewport courant — définitivement morte : la
            // scène gardait pour toujours la taille du tout premier viewport. Ça ne se voyait pas
            // tant que rien ne changeait de résolution en cours de partie ; la page « Définition de
            // l'écran », une fois branchée, l'a montré aussitôt — le jeu dessinait en 320x240 dans
            // le coin d'une cible restée en 960x720, par-dessus l'image précédente.
            //
            // Le coût par image est de deux comparaisons d'entiers : `EnsureBackend` sort tout de
            // suite si la taille n'a pas bougé.
            Guarded("EnsureBackend", EnsureBackend);
            if (s_BackendLive) { Guarded("BeginScene", D3D11Backend.BeginScene); return; }
        }
        // Avec le device synthétique il n'y a pas de vtable d'origine : le repli devient neutre.
        if (s_ShimRealVtable == null) return;
        ((delegate* unmanaged[Thiscall]<void*, void>)s_ShimRealVtable[0xB0 / 4])(self);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static int Shim_EndScene(void* self)
    {
        ShimTake(0xB4);

        if (s_UseD3D11 && s_BackendLive)
        {
            // La présentation se fait ici, et non au slot +0xDC (« Flip »). Mesuré : sur 121
            // scènes et 12 655 primitives, +0xDC n'a été appelé **aucune** fois — le jeu présente
            // par un autre chemin que ce slot dans cette phase (vraisemblablement les Blt de
            // surface de +0x30 / +0xE8). EndScene est la frontière d'image fiable : son compte
            // suit exactement celui de BeginScene.
            Guarded("Present", () =>
            {
                D3D11Backend.Present();
                // La fenêtre est la nôtre : c'est ici, une fois par image, que sa file de messages
                // est vidée. Sans cela Windows la déclarerait « ne répond pas ».
                if (!D3D11Window.PumpMessages()) Environment.Exit(0);
            });
            return 0;
        }

        if (s_ShimRealVtable == null) return 0;
        return ((delegate* unmanaged[Thiscall]<void*, int>)s_ShimRealVtable[0xB4 / 4])(self);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static void Shim_DrawPrimitive(void* self, uint type, uint vertexType, void* vertices,
                                           uint count, uint flags)
    {
        ShimTake(0xBC);

        if (s_UseD3D11 && s_BackendLive)
        {
            // `vertexType` 3 = D3DVT_TLVERTEX, dont la disposition est exactement celle de
            // TlVertex : 32 octets, sommets déjà projetés. Confirmé au vidage brut — le 4e champ
            // vaut 0x38D1B717, soit rhw ≈ 0,0001, et non le `dwReserved` nul d'un D3DLVERTEX.
            void* v = vertices;
            uint t = type, vt = vertexType, n = count;

            // ECHAFAUDAGE TEMPORAIRE : les premières primitives réellement soumises au backend,
            // décodées en TLVERTEX. Objectif : vérifier que les coordonnées tombent bien dans
            // 0..s_SceneWidth / 0..s_SceneHeight, plutôt que de le supposer.
            // Relevé de géométrie sur demande : c'est lui qui a isolé la cause de l'écran noir
            // intermittent (coordonnées translatées hors écran). Conservé derrière une variable
            // d'environnement plutôt que supprimé — le même relevé servira pour l'échelle des
            // coordonnées et pour les textures.
            if (s_DumpGeometry && vt == 3 && s_ShimSeen[0xBC / 4] is 1 or 500 or 2000 or 10000)
            {
                // Relevé au premier dessin **et** en régime établi : `SetShift` peut être rappelée
                // entre les deux et changer la table de conversion sous nos pieds.
                if (s_ShimSeen[0xBC / 4] is 1 or 10000) LogShiftGlobals();
                var t2 = (TlVertex*)v;
                var sb = new System.Text.StringBuilder(
                    $"[GEO] dessin #{s_ShimSeen[0xBC / 4]} topologie={t} n={n} " +
                    $"texture={(s_BoundTexture != null ? "oui" : "non")} cible={s_SceneWidth}x{s_SceneHeight}");
                for (int i = 0; i < (int)n && i < 4; i++)
                    sb.Append($"\n      v{i} x={t2[i].X:F1} y={t2[i].Y:F1} z={t2[i].Z:F4} " +
                              $"rhw={t2[i].Rhw:G4} diffuse=0x{t2[i].Diffuse:X8} uv=({t2[i].U:F3},{t2[i].V:F3})");
                Console.Error.WriteLine(sb.ToString());
            }
            if (TracingFrame())
            {
                TraceSizeChange("un dessin");
                var tvx = (TlVertex*)v;
                if (n > 0)
                    Console.Error.WriteLine(
                        $"[TRACE]   dessin #{s_ShimSeen[0xBC / 4]} topo={t} n={n} " +
                        $"v0=({tvx[0].X:F0},{tvx[0].Y:F0}) v{n - 1}=({tvx[n - 1].X:F0},{tvx[n - 1].Y:F0})");
            }

            // ECHAFAUDAGE TEMPORAIRE — tâche 4.5. Enveloppe des coordonnées réellement soumises.
            // Le relevé du dessin #500 montre des sommets non mis à l'échelle alors que la table de
            // conversion contient bien le facteur 3 : certains chemins de `winsys3d` l'empruntent,
            // d'autres non. L'enveloppe globale dit lesquels dominent.
            if (s_DumpGeometry && vt == 3)
            {
                var tv = (TlVertex*)v;
                for (int i = 0; i < (int)n; i++)
                {
                    if (tv[i].X < s_GeoMinX) s_GeoMinX = tv[i].X;
                    if (tv[i].X > s_GeoMaxX) s_GeoMaxX = tv[i].X;
                    if (tv[i].Y < s_GeoMinY) s_GeoMinY = tv[i].Y;
                    if (tv[i].Y > s_GeoMaxY) s_GeoMaxY = tv[i].Y;
                }
            }

            Guarded($"DrawPrimitive(topologie={t}, format={vt})", () =>
            {
                if (vt != 3)
                    throw new NotImplementedException(
                        $"format de sommet {vt} non porté " +
                        "(1 = D3DVT_VERTEX, 2 = D3DVT_LVERTEX) ; seul TLVERTEX avait été observé");

                // La texture liée est identifiée par le pointeur de l'objet que `+0x64` a reçu :
                // c'est la seule identité que le jeu manipule, et la clé du registre du backend.
                // Vue nulle = objet inconnu ou pas encore chargé, cas où le backend lie sa texture
                // blanche — le comportement d'avant la tâche 3.5.
                var bound = s_BoundTexture != null
                    ? D3D11Backend.TextureView((nint)s_BoundTexture)
                    : default;

                D3D11Backend.DrawPrimitive(t, (TlVertex*)v, (int)n, bound, s_FlatShading,
                    alphaBlend: AlphaBlendActive(), alphaFromTexture: s_TextureMapBlend == 4);
            });
            return;
        }

        // Avec le device synthétique il n'y a pas de vtable d'origine : le repli devient neutre.
        if (s_ShimRealVtable == null) return;
        ((delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)
            s_ShimRealVtable[0xBC / 4])(self, type, vertexType, vertices, count, flags);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    internal static int Shim_Flip(void* self, void* target, uint flags)
    {
        ShimTake(0xDC);

        // Jamais atteint dans la phase menu (mesuré : 0 appel sur 121 images) ; la présentation a
        // lieu à EndScene. Conservé au cas où une autre phase emprunterait ce slot.
        if (s_UseD3D11 && s_BackendLive) return 0;

        if (s_ShimRealVtable == null) return 0;
        int hr = ((delegate* unmanaged[Thiscall]<void*, void*, uint, int>)s_ShimRealVtable[0xDC / 4])(self, target, flags);
        if (ShimTake(0xDC)) { Console.Error.WriteLine($"[SHIM] Flip target=0x{(nint)target:X8} flags={flags} -> 0x{hr:X8}"); }

        // Flip marque la fin d'une frame : bilan périodique, seul repère fiable de progression.
        if (s_ShimSeen[0xDC / 4] % 60 == 0) { ShimReport(); ReportComFailures(); }
        return hr;
    }
}
