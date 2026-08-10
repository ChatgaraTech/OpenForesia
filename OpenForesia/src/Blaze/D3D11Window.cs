using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

/// <summary>
/// Fenêtre du jeu, créée et possédée par le backend Direct3D 11.
///
/// Elle remplace celle que <c>3dhelp</c> crée dans <c>FUN_0ffe1c20</c> (classe « Example », titre
/// « Spieltitel », <c>WS_POPUP</c> 640x480 — restes d'un code d'exemple). La différence qui compte
/// n'est pas le style : c'est qu'aucun appelant ne la passe à DirectDraw, donc rien ne peut la
/// faire basculer en coopératif exclusif ni déclencher de changement de mode d'affichage.
/// </summary>
internal static unsafe class D3D11Window
{
    private const uint WsOverlappedWindow = 0x00CF0000;
    private const uint WsPopup = 0x80000000;
    private const uint WsVisible = 0x10000000;
    private const int SwShow = 5;
    private const int SwShowNoActivate = 4;

    private const uint WmDestroy = 0x0002;
    private const uint WmClose = 0x0010;
    private const uint WmSize = 0x0005;
    private const uint WmKeyDown = 0x0100;
    private const uint WmSysKeyDown = 0x0104;
    private const int VkEscape = 0x1B;
    private const int VkReturn = 0x0D;

    private const uint PmRemove = 0x0001;

    [StructLayout(LayoutKind.Sequential)]
    private struct WndClassExA
    {
        public uint cbSize;
        public uint style;
        public IntPtr lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public byte* lpszMenuName;
        public byte* lpszClassName;
        public IntPtr hIconSm;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Msg
    {
        public IntPtr hwnd;
        public uint message;
        public nuint wParam;
        public nint lParam;
        public uint time;
        public int ptX;
        public int ptY;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect
    {
        public int Left, Top, Right, Bottom;
        public int Width => Right - Left;
        public int Height => Bottom - Top;
    }

    [DllImport("user32.dll", CharSet = CharSet.Ansi)] private static extern ushort RegisterClassExA(WndClassExA* c);
    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern IntPtr CreateWindowExA(uint exStyle, byte* className, byte* windowName, uint style,
        int x, int y, int w, int h, IntPtr parent, IntPtr menu, IntPtr instance, void* param);
    [DllImport("user32.dll")] private static extern IntPtr DefWindowProcA(IntPtr hWnd, uint msg, nuint wParam, nint lParam);
    [DllImport("user32.dll")] private static extern int ShowWindow(IntPtr hWnd, int cmd);
    [DllImport("user32.dll")] private static extern int UpdateWindow(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern int PeekMessageA(Msg* msg, IntPtr hWnd, uint min, uint max, uint remove);
    [DllImport("user32.dll")] private static extern int TranslateMessage(Msg* msg);
    [DllImport("user32.dll")] private static extern IntPtr DispatchMessageA(Msg* msg);
    [DllImport("user32.dll")] private static extern void PostQuitMessage(int exitCode);
    [DllImport("user32.dll")] private static extern int GetClientRect(IntPtr hWnd, Rect* rect);
    [DllImport("user32.dll")] private static extern int AdjustWindowRect(Rect* rect, uint style, int menu);
    [DllImport("user32.dll")] private static extern IntPtr LoadCursorA(IntPtr instance, nint cursorName);
    [DllImport("user32.dll")] private static extern int DestroyWindow(IntPtr hWnd);
    [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] private static extern int SetForegroundWindow(IntPtr hWnd);
    [DllImport("kernel32.dll")] private static extern IntPtr GetModuleHandleA(byte* moduleName);

    private static byte* s_ClassName;
    private static byte* s_WindowTitle;

    /// <summary>Handle de la fenêtre, ou zéro tant qu'elle n'est pas créée.</summary>
    internal static IntPtr Handle { get; private set; }

    /// <summary>Passe à vrai quand la fenêtre a été fermée.</summary>
    internal static bool CloseRequested { get; private set; }

    /// <summary>Passe à vrai quand la zone cliente a changé de taille depuis le dernier <see cref="TakeResize"/>.</summary>
    private static bool s_Resized;

    internal static bool TakeResize()
    {
        bool r = s_Resized;
        s_Resized = false;
        return r;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static IntPtr WindowProc(IntPtr hWnd, uint msg, nuint wParam, nint lParam)
    {
        switch (msg)
        {
            case WmClose:
                CloseRequested = true;
                return IntPtr.Zero;

            case WmDestroy:
                PostQuitMessage(0);
                return IntPtr.Zero;

            case WmSize:
                s_Resized = true;
                return IntPtr.Zero;

            case WmKeyDown:
            case WmSysKeyDown:
                // Alt+Entrée : la convention universelle pour la bascule plein écran. DXGI ne peut
                // plus l'intercepter (MakeWindowAssociation, voir D3D11Backend), donc elle nous
                // revient. Le bit 29 de lParam porte l'état de la touche Alt.
                if ((int)wParam == VkReturn && (lParam & (1 << 29)) != 0)
                {
                    ToggleFullscreen();
                    return IntPtr.Zero;
                }
                if ((int)wParam == VkEscape) CloseRequested = true;
                break;
        }

        return DefWindowProcA(hWnd, msg, wParam, lParam);
    }

    /// <summary>
    /// Crée la fenêtre avec une zone cliente des dimensions demandées. Le style est fenêtré : le
    /// plein écran sans bordure est une bascule ultérieure, pas un mode de création.
    /// </summary>
    internal static IntPtr Create(int clientWidth, int clientHeight, string title)
    {
        if (Handle != IntPtr.Zero) return Handle;

        s_ClassName = (byte*)Marshal.StringToHGlobalAnsi("OpenForesiaWindow");
        s_WindowTitle = (byte*)Marshal.StringToHGlobalAnsi(title);

        IntPtr instance = GetModuleHandleA(null);

        var wc = default(WndClassExA);
        wc.cbSize = (uint)sizeof(WndClassExA);
        wc.style = 0x0003; // CS_HREDRAW | CS_VREDRAW
        wc.lpfnWndProc = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, uint, nuint, nint, IntPtr>)&WindowProc;
        wc.hInstance = instance;
        wc.hCursor = LoadCursorA(IntPtr.Zero, 32512 /* IDC_ARROW */);
        wc.hbrBackground = IntPtr.Zero; // on peint entièrement, pas d'effacement GDI
        wc.lpszClassName = s_ClassName;

        if (RegisterClassExA(&wc) == 0)
        {
            int err = Marshal.GetLastWin32Error();
            // 1410 = ERROR_CLASS_ALREADY_EXISTS, sans gravité si la classe survit d'un essai précédent.
            if (err != 0 && err != 1410)
                throw new InvalidOperationException($"RegisterClassExA a échoué (code {err})");
        }

        var r = new Rect { Left = 0, Top = 0, Right = clientWidth, Bottom = clientHeight };
        AdjustWindowRect(&r, WsOverlappedWindow, 0);

        const int CwUseDefault = unchecked((int)0x80000000);
        Handle = CreateWindowExA(0, s_ClassName, s_WindowTitle, WsOverlappedWindow,
            CwUseDefault, CwUseDefault, r.Width, r.Height,
            IntPtr.Zero, IntPtr.Zero, instance, null);

        if (Handle == IntPtr.Zero)
            throw new InvalidOperationException(
                $"CreateWindowExA a échoué (code {Marshal.GetLastWin32Error()})");

        // Affichage **sans activation**, et restitution du premier plan à la fenêtre qui l'avait.
        // Tant que 3dhelp est encore initialisé, c'est lui qui détient la saisie du jeu : voler le
        // focus ici empêche le title screen de recevoir la moindre touche, et le jeu n'atteint
        // jamais sa boucle de rendu. Ce ménage disparaîtra avec la tâche 4.2, quand la saisie sera
        // portée sur cette fenêtre.
        IntPtr previous = GetForegroundWindow();
        ShowWindow(Handle, SwShowNoActivate);
        UpdateWindow(Handle);
        if (previous != IntPtr.Zero && previous != Handle) SetForegroundWindow(previous);
        return Handle;
    }

    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern IntPtr FindWindowA(byte* className, byte* windowName);
    [DllImport("user32.dll", CharSet = CharSet.Ansi)]
    private static extern int ChangeDisplaySettingsA(void* devMode, uint flags);

    private const int SwHide = 0;

    /// <summary>
    /// Rend l'affichage que <c>3dhelp</c> s'est approprié à l'initialisation.
    ///
    /// <c>3dhelp</c> prend le coopératif exclusif puis change le mode d'affichage
    /// (<c>3dhelp.c:1240</c> et <c>1275</c>) au moment où <c>Setup3DDevice</c> crée le device. On ne
    /// peut pas sauter cette création : <c>winsys3d</c> appelle le device par sa vtable et n'a rien
    /// d'autre à appeler. Mais depuis que le chemin chaud passe par le backend, <c>3dhelp</c> ne
    /// présente plus rien — son emprise sur l'écran est inerte, seulement gênante.
    ///
    /// On la défait donc en deux gestes : masquer sa fenêtre, ce qui désactive le mode exclusif, et
    /// restaurer le mode d'affichage enregistré. Aucun des deux ne touche à la DLL.
    ///
    /// Sa fenêtre se reconnaît à sa classe « Example » (<c>FUN_0ffe1c20</c> — reste d'un code
    /// d'exemple, avec le titre « Spieltitel »).
    /// </summary>
    internal static void ReleaseLegacyDisplay()
    {
        byte* className = (byte*)Marshal.StringToHGlobalAnsi("Example");
        try
        {
            IntPtr legacy = FindWindowA(className, null);
            if (legacy != IntPtr.Zero) ShowWindow(legacy, SwHide);

            // devMode nul + flags nuls = retour au mode enregistré pour cet écran.
            int r = ChangeDisplaySettingsA(null, 0);
            Console.Error.WriteLine(
                $"[MODE] affichage rendu : fenêtre 3dhelp={(legacy != IntPtr.Zero ? "masquée" : "introuvable")}, " +
                $"ChangeDisplaySettings={r}");
        }
        finally
        {
            Marshal.FreeHGlobal((nint)className);
        }
    }

    // =====================================================================
    // Bascule fenêtre / plein écran sans bordure
    // =====================================================================

    [StructLayout(LayoutKind.Sequential)]
    private struct MonitorInfo
    {
        public uint cbSize;
        public Rect rcMonitor;
        public Rect rcWork;
        public uint dwFlags;
    }

    [DllImport("user32.dll")] private static extern int GetWindowLongA(IntPtr hWnd, int index);
    [DllImport("user32.dll")] private static extern int SetWindowLongA(IntPtr hWnd, int index, int value);
    [DllImport("user32.dll")] private static extern int GetWindowRect(IntPtr hWnd, Rect* rect);
    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hWnd, IntPtr insertAfter, int x, int y, int cx, int cy, uint flags);
    [DllImport("user32.dll")] private static extern IntPtr MonitorFromWindow(IntPtr hWnd, uint flags);
    [DllImport("user32.dll")] private static extern int GetMonitorInfoA(IntPtr monitor, MonitorInfo* info);

    private const int GwlStyle = -16;
    private const uint SwpFrameChanged = 0x0020;
    private const uint SwpNoZOrder = 0x0004;
    private const uint SwpNoOwnerZOrder = 0x0200;
    private const uint MonitorDefaultToNearest = 2;

    private static bool s_Fullscreen;
    private static Rect s_WindowedRect;
    private static int s_WindowedStyle;

    /// <summary>Vrai quand la fenêtre occupe l'écran entier, sans bordure.</summary>
    internal static bool IsFullscreen => s_Fullscreen;

    /// <summary>
    /// Bascule entre fenêtre et plein écran **sans bordure**.
    ///
    /// Le plein écran est obtenu en retirant la bordure et en dimensionnant la fenêtre à l'écran :
    /// aucun changement de mode d'affichage, aucun mode exclusif. C'est ce que font les jeux
    /// modernes, et c'est ce qui garantit qu'on ne retombe pas dans le problème que cette change a
    /// supprimé.
    ///
    /// La position et les dimensions de la fenêtre sont mémorisées à l'aller et restituées au
    /// retour.
    /// </summary>
    internal static void ToggleFullscreen()
    {
        if (Handle == IntPtr.Zero) return;

        if (!s_Fullscreen)
        {
            Rect r;
            GetWindowRect(Handle, &r);
            s_WindowedRect = r;
            s_WindowedStyle = GetWindowLongA(Handle, GwlStyle);

            IntPtr monitor = MonitorFromWindow(Handle, MonitorDefaultToNearest);
            var info = default(MonitorInfo);
            info.cbSize = (uint)sizeof(MonitorInfo);
            if (GetMonitorInfoA(monitor, &info) == 0) return;

            SetWindowLongA(Handle, GwlStyle, unchecked((int)(WsPopup | WsVisible)));
            SetWindowPos(Handle, IntPtr.Zero,
                info.rcMonitor.Left, info.rcMonitor.Top,
                info.rcMonitor.Width, info.rcMonitor.Height,
                SwpFrameChanged | SwpNoZOrder | SwpNoOwnerZOrder);

            s_Fullscreen = true;
        }
        else
        {
            SetWindowLongA(Handle, GwlStyle, s_WindowedStyle);
            SetWindowPos(Handle, IntPtr.Zero,
                s_WindowedRect.Left, s_WindowedRect.Top,
                s_WindowedRect.Width, s_WindowedRect.Height,
                SwpFrameChanged | SwpNoZOrder | SwpNoOwnerZOrder);

            s_Fullscreen = false;
        }

        s_Resized = true;
        Console.Error.WriteLine($"[FEN] {(s_Fullscreen ? "plein écran sans bordure" : "fenêtre")}");
    }

    /// <summary>Amène notre fenêtre au premier plan.</summary>
    internal static void Focus()
    {
        if (Handle == IntPtr.Zero) return;
        ShowWindow(Handle, SwShow);
        SetForegroundWindow(Handle);
    }

    /// <summary>Dimensions actuelles de la zone cliente.</summary>
    internal static (int Width, int Height) ClientSize()
    {
        Rect r;
        if (Handle == IntPtr.Zero || GetClientRect(Handle, &r) == 0) return (0, 0);
        return (r.Width, r.Height);
    }

    /// <summary>
    /// Vide la file de messages sans bloquer. Retourne faux quand la fermeture a été demandée, ce
    /// qui permet d'écrire la boucle appelante en <c>while (PumpMessages())</c>.
    /// </summary>
    internal static bool PumpMessages()
    {
        Msg msg;
        while (PeekMessageA(&msg, IntPtr.Zero, 0, 0, PmRemove) != 0)
        {
            if (msg.message == 0x0012 /* WM_QUIT */) CloseRequested = true;
            TranslateMessage(&msg);
            DispatchMessageA(&msg);
        }
        return !CloseRequested;
    }

    internal static void Destroy()
    {
        if (Handle != IntPtr.Zero)
        {
            DestroyWindow(Handle);
            Handle = IntPtr.Zero;
        }
    }
}
