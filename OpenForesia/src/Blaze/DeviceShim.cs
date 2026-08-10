// =====================================================================
// ECHAFAUDAGE TEMPORAIRE — NE PAS COMMITER TEL QUEL
//
// Façade en pass-through devant le device 3D de 3dhelp.dll. Elle ne change aucun comportement :
// chaque slot intercepté journalise ses arguments puis réémet l'appel vers la fonction d'origine.
//
// But : voir enfin ce que winsys3d envoie réellement au device — arguments de DrawPrimitive,
// contenu des sommets, liaison de texture — et comparer un lancement où le menu s'affiche à un
// lancement noir. C'est le seul point aveugle qui reste (voir ANALYSE_ECRAN_NOIR_MENU.md).
//
// Mécanisme : on n'alloue pas de faux objet. On copie la vtable du vrai device, on remplace les
// slots voulus, et on écrit notre copie dans `device[0]`. Conséquences :
//   - `this` reste le vrai objet, donc les slots non interceptés fonctionnent inchangés ;
//   - nos thunks reçoivent le vrai `this` et peuvent réémettre vers la fonction d'origine ;
//   - les appels internes que 3dhelp se fait à lui-même via sa vtable passent aussi par nous.
//
// Cartographie des slots : Documentation/DLL/3dhelp.md
// =====================================================================

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    private const int DeviceVtableSlots = 64;     // la vtable réelle en expose 59, arrondi supérieur

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

    private static void InstallDeviceShim()
    {
        if (s_ShimOurVtable != null) return;

        IntPtr mod = GetModuleHandleA(ToAnsiZ("winsys3d.dll"));
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
    private static int Shim_SetViewport(void* self, int x, int y, int w, int h)
    {
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
    private static void Shim_ClearTarget(void* self, void* rects, int count)
    {
        if (ShimTake(0x5C)) { Console.Error.WriteLine($"[SHIM] Clear(TARGET) rects=0x{(nint)rects:X8} n={count}"); }
        ((delegate* unmanaged[Thiscall]<void*, void*, int, void>)s_ShimRealVtable[0x5C / 4])(self, rects, count);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static void Shim_ClearZ(void* self, void* rects, int count)
    {
        if (ShimTake(0x60)) { Console.Error.WriteLine($"[SHIM] Clear(ZBUFFER) rects=0x{(nint)rects:X8} n={count}"); }
        ((delegate* unmanaged[Thiscall]<void*, void*, int, void>)s_ShimRealVtable[0x60 / 4])(self, rects, count);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static void Shim_BindTexture(void* self, void* texture)
    {
        if (ShimTake(0x64)) { Console.Error.WriteLine($"[SHIM] BindTexture 0x{(nint)texture:X8}"); }
        ((delegate* unmanaged[Thiscall]<void*, void*, void>)s_ShimRealVtable[0x64 / 4])(self, texture);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static void Shim_ShadeMode(void* self, uint value)
    {
        if (ShimTake(0x8C)) { Console.Error.WriteLine($"[SHIM] SetRenderState(SHADEMODE, {value})"); }
        ((delegate* unmanaged[Thiscall]<void*, uint, void>)s_ShimRealVtable[0x8C / 4])(self, value);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static void Shim_BeginScene(void* self)
    {
        if (ShimTake(0xB0)) { Console.Error.WriteLine("[SHIM] BeginScene"); }
        ((delegate* unmanaged[Thiscall]<void*, void>)s_ShimRealVtable[0xB0 / 4])(self);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Shim_EndScene(void* self)
    {
        int hr = ((delegate* unmanaged[Thiscall]<void*, int>)s_ShimRealVtable[0xB4 / 4])(self);
        if (ShimTake(0xB4)) { Console.Error.WriteLine($"[SHIM] EndScene -> 0x{hr:X8}"); }
        return hr;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static void Shim_DrawPrimitive(void* self, uint type, uint vertexType, void* vertices,
                                           uint count, uint flags)
    {
        if (ShimTake(0xBC))
        {

            // Octets bruts plutôt qu'une interprétation : la disposition supposée (D3DTLVERTEX)
            // donnait un 4e champ systématiquement nul, ce qui est invalide pour un TLVERTEX mais
            // normal pour un D3DLVERTEX (`dwReserved`). On ne tranche pas, on regarde.
            uint* c = (uint*)vertices;
            var sb = new System.Text.StringBuilder(
                $"[SHIM] DrawPrimitive type={type} fmt={vertexType} n={count} flags={flags}");
            for (int vi = 0; vi < (int)count && vi < 2; vi++)
            {
                sb.Append($"\n         v{vi} brut:");
                for (int w = 0; w < 8; w++) sb.Append($" {c[vi * 8 + w]:X8}");
            }
            Console.Error.WriteLine(sb.ToString());
        }
        ((delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)
            s_ShimRealVtable[0xBC / 4])(self, type, vertexType, vertices, count, flags);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Shim_Flip(void* self, void* target, uint flags)
    {
        int hr = ((delegate* unmanaged[Thiscall]<void*, void*, uint, int>)s_ShimRealVtable[0xDC / 4])(self, target, flags);
        if (ShimTake(0xDC)) { Console.Error.WriteLine($"[SHIM] Flip target=0x{(nint)target:X8} flags={flags} -> 0x{hr:X8}"); }

        // Flip marque la fin d'une frame : bilan périodique, seul repère fiable de progression.
        if (s_ShimSeen[0xDC / 4] % 60 == 0) { ShimReport(); ReportComFailures(); }
        return hr;
    }
}
