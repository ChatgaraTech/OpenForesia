// La capture d'une fenetre par Windows.Graphics.Capture (tache 2.1, design D3), sans CsWinRT.
//
//   D3D11CreateDevice ─► IDXGIDevice ─► CreateDirect3D11DeviceFromDXGIDevice (IDirect3DDevice WinRT)
//   IGraphicsCaptureItemInterop.CreateForWindow(hwnd) ─► GraphicsCaptureItem
//   Direct3D11CaptureFramePool.CreateFreeThreaded(...) ─► TryGetNextFrame depuis n'importe quel fil
//   frame.Surface ─► IDirect3DDxgiInterfaceAccess ─► ID3D11Texture2D ─► copie du rectangle client
//   dans une texture STAGING ─► Map : les pixels BGRA, lisibles par le processeur
//
// La capture d'une fenetre porte son cadre (titre, bordures) : on n'en garde que la zone client,
// situee par DwmGetWindowAttribute(DWMWA_EXTENDED_FRAME_BOUNDS), l'origine de ce que Windows capture.
//
// Rangs de vtable : d3d11.h, dxgi.h, windows.graphics.capture.h, windows.graphics.capture.interop.h,
// windows.graphics.directx.direct3d11.interop.h.

using System.Runtime.InteropServices;
using static OpenForesia.Multiplayer.Natif.Com;

namespace OpenForesia.Multiplayer.Natif;

internal sealed unsafe partial class CaptureFenetre : IDisposable
{
    [LibraryImport("d3d11.dll")]
    private static partial int D3D11CreateDevice(IntPtr adaptateur, int pilote, IntPtr logiciel, uint drapeaux, int* niveaux,
        uint nombreNiveaux, uint version, void** device, int* niveau, void** contexte);
    [LibraryImport("d3d11.dll")] private static partial int CreateDirect3D11DeviceFromDXGIDevice(void* dxgi, void** inspectable);
    [LibraryImport("dwmapi.dll")] private static partial int DwmGetWindowAttribute(IntPtr hwnd, uint attribut, void* valeur, uint taille);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool GetClientRect(IntPtr hwnd, Rect* r);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool ClientToScreen(IntPtr hwnd, Point* p);
    [LibraryImport("user32.dll")] [return: MarshalAs(UnmanagedType.Bool)] private static partial bool IsWindow(IntPtr hwnd);

    [StructLayout(LayoutKind.Sequential)] private struct Rect { public int Gauche, Haut, Droite, Bas; }
    [StructLayout(LayoutKind.Sequential)] private struct Point { public int X, Y; }
    [StructLayout(LayoutKind.Sequential)] private struct Taille { public int Largeur, Hauteur; }          // SizeInt32

    [StructLayout(LayoutKind.Sequential)]
    private struct Texture2DDesc
    {
        public uint Largeur, Hauteur, Mips, Tableau, Format, Echantillons, Qualite, Usage, Liaison, AccesCpu, Divers;
    }

    [StructLayout(LayoutKind.Sequential)] private struct Boite { public uint Gauche, Haut, Avant, Droite, Bas, Arriere; }
    [StructLayout(LayoutKind.Sequential)] private struct Plan { public byte* Donnees; public uint Pas, PasProfondeur; }

    private static readonly Guid IID_IDXGIDevice = new("54ec77fa-1377-44e6-8c32-88fd5f44c84c");
    private static readonly Guid IID_ID3D11Texture2D = new("6f15aaf2-d208-4e89-9ab4-489535d34f9c");
    private static readonly Guid IID_IGraphicsCaptureItemInterop = new("3628E81B-3CAC-4C60-B7F4-23CE0E0C3356");
    private static readonly Guid IID_IGraphicsCaptureItem = new("79C3F95B-31F7-4EC2-A464-632EF5D30760");
    private static readonly Guid IID_IDirect3D11CaptureFramePoolStatics2 = new("589B103F-6BBC-5DF5-A991-02E28B3B66D5");
    private static readonly Guid IID_IGraphicsCaptureSession2 = new("2C39AE40-7D2E-5044-804E-8B6799D4CF9E");
    private static readonly Guid IID_IGraphicsCaptureSession3 = new("F2CDD966-22AE-5EA1-9596-3A289344C3BE");
    private static readonly Guid IID_IDirect3DDxgiInterfaceAccess = new("A9B3D012-3DF2-4EE3-B8D1-8695F457D3C1");
    private static readonly Guid IID_IClosable = new("30D5A829-7FA4-4026-83BB-D75BAE4EA99E");

    private const int PiloteMateriel = 1;                        // D3D_DRIVER_TYPE_HARDWARE
    private const uint SupportBgra = 0x20;                       // D3D11_CREATE_DEVICE_BGRA_SUPPORT
    private const uint VersionSdk = 7;
    private const int FormatBgra = 87;                           // DXGI_FORMAT_B8G8R8A8_UNORM = DirectXPixelFormat
    private const uint UsageStaging = 3, LectureCpu = 0x20000, MapLecture = 1;
    private const uint CadreEtendu = 9;                          // DWMWA_EXTENDED_FRAME_BOUNDS

    private void* _device, _contexte, _deviceWinRT, _item, _pool, _session, _staging;
    private Taille _taillePool;
    private uint _largeurStaging, _hauteurStaging;

    internal IntPtr Fenetre { get; }

    /// <summary>Commence la capture de <paramref name="fenetre"/>. Le fil appelant doit être initialisé (<see cref="Com.InitialiserFil"/>).</summary>
    internal CaptureFenetre(IntPtr fenetre)
    {
        Fenetre = fenetre;
        void* device, contexte;
        int niveau;
        Verifier(D3D11CreateDevice(IntPtr.Zero, PiloteMateriel, IntPtr.Zero, SupportBgra, null, 0, VersionSdk, &device, &niveau, &contexte), "D3D11CreateDevice");
        _device = device;
        _contexte = contexte;

        void* dxgi = Interroger(_device, IID_IDXGIDevice);
        if (dxgi == null) throw new COMException("IDXGIDevice introuvable");
        void* winrt;
        try { Verifier(CreateDirect3D11DeviceFromDXGIDevice(dxgi, &winrt), "CreateDirect3D11DeviceFromDXGIDevice"); }
        finally { Liberer(dxgi); }
        _deviceWinRT = winrt;

        // IGraphicsCaptureItemInterop : IUnknown, puis CreateForWindow (3), CreateForMonitor (4).
        void* interop = Fabrique("Windows.Graphics.Capture.GraphicsCaptureItem", IID_IGraphicsCaptureItemInterop);
        try
        {
            void* item;
            Guid iid = IID_IGraphicsCaptureItem;
            Verifier(((delegate* unmanaged[Stdcall]<void*, IntPtr, Guid*, void**, int>)Vtable(interop)[3])(interop, fenetre, &iid, &item),
                     "GraphicsCaptureItem.CreateForWindow");
            _item = item;
        }
        finally { Liberer(interop); }

        // IGraphicsCaptureItem : DisplayName (6), Size (7).
        Taille taille;
        Verifier(((delegate* unmanaged[Stdcall]<void*, Taille*, int>)Vtable(_item)[PremiereMethodeWinRT + 1])(_item, &taille), "GraphicsCaptureItem.Size");
        _taillePool = taille;

        // IDirect3D11CaptureFramePoolStatics2 : CreateFreeThreaded (6).
        void* statiques = Fabrique("Windows.Graphics.Capture.Direct3D11CaptureFramePool", IID_IDirect3D11CaptureFramePoolStatics2);
        try
        {
            void* pool;
            Verifier(((delegate* unmanaged[Stdcall]<void*, void*, int, int, Taille, void**, int>)Vtable(statiques)[PremiereMethodeWinRT])(
                         statiques, _deviceWinRT, FormatBgra, 2, taille, &pool), "Direct3D11CaptureFramePool.CreateFreeThreaded");
            _pool = pool;
        }
        finally { Liberer(statiques); }

        // IDirect3D11CaptureFramePool : Recreate (6), TryGetNextFrame (7), FrameArrived (8, 9),
        // CreateCaptureSession (10).
        void* session;
        Verifier(((delegate* unmanaged[Stdcall]<void*, void*, void**, int>)Vtable(_pool)[PremiereMethodeWinRT + 4])(_pool, _item, &session),
                 "Direct3D11CaptureFramePool.CreateCaptureSession");
        _session = session;

        // Sans le curseur, sans le cadre jaune quand Windows le permet (IsCursorCaptureEnabled :
        // Session2, put au rang 7 ; IsBorderRequired : Session3, put au rang 7). Un refus est sans gravité.
        Poser(_session, IID_IGraphicsCaptureSession2, PremiereMethodeWinRT + 1, false);
        Poser(_session, IID_IGraphicsCaptureSession3, PremiereMethodeWinRT + 1, false);

        // IGraphicsCaptureSession : StartCapture (6).
        Verifier(((delegate* unmanaged[Stdcall]<void*, int>)Vtable(_session)[PremiereMethodeWinRT])(_session), "GraphicsCaptureSession.StartCapture");
    }

    private static void Poser(void* objet, Guid iid, int rang, bool valeur)
    {
        void* i = Interroger(objet, iid);
        if (i == null) return;
        ((delegate* unmanaged[Stdcall]<void*, byte, int>)Vtable(i)[rang])(i, (byte)(valeur ? 1 : 0));
        Liberer(i);
    }

    /// <summary>La fenêtre capturée existe encore.</summary>
    internal bool Vivante => IsWindow(Fenetre);

    /// <summary>
    /// La dernière image arrivée, s'il y en a une : sa zone client, en BGRA, est remise à
    /// <paramref name="lire"/> (pixels, pas d'une ligne en octets, largeur, hauteur) le temps de
    /// l'appel. Faux si aucune image nouvelle n'est arrivée.
    /// </summary>
    internal bool Lire(Action<IntPtr, int, int, int> lire)
    {
        // IDirect3D11CaptureFramePool.TryGetNextFrame (7) : rend null s'il n'y a rien de neuf. On
        // vide la file pour ne garder que la plus récente.
        void* image = null;
        while (true)
        {
            void* suivante;
            Verifier(((delegate* unmanaged[Stdcall]<void*, void**, int>)Vtable(_pool)[PremiereMethodeWinRT + 1])(_pool, &suivante), "TryGetNextFrame");
            if (suivante == null) break;
            if (image != null) Fermer(image);
            image = suivante;
        }
        if (image == null) return false;

        try
        {
            // IDirect3D11CaptureFrame : Surface (6), SystemRelativeTime (7), ContentSize (8).
            Taille contenu;
            Verifier(((delegate* unmanaged[Stdcall]<void*, Taille*, int>)Vtable(image)[PremiereMethodeWinRT + 2])(image, &contenu), "ContentSize");
            if (contenu.Largeur != _taillePool.Largeur || contenu.Hauteur != _taillePool.Hauteur)
            {
                // La fenêtre a changé de taille : le bassin est refait, cette image est perdue.
                _taillePool = contenu;
                Verifier(((delegate* unmanaged[Stdcall]<void*, void*, int, int, Taille, int>)Vtable(_pool)[PremiereMethodeWinRT])(
                             _pool, _deviceWinRT, FormatBgra, 2, contenu), "Direct3D11CaptureFramePool.Recreate");
                return false;
            }

            void* surface;
            Verifier(((delegate* unmanaged[Stdcall]<void*, void**, int>)Vtable(image)[PremiereMethodeWinRT])(image, &surface), "Surface");
            void* texture;
            try
            {
                // IDirect3DDxgiInterfaceAccess : IUnknown, puis GetInterface (3).
                void* acces = Interroger(surface, IID_IDirect3DDxgiInterfaceAccess);
                if (acces == null) throw new COMException("IDirect3DDxgiInterfaceAccess introuvable");
                Guid iid = IID_ID3D11Texture2D;
                try { Verifier(((delegate* unmanaged[Stdcall]<void*, Guid*, void**, int>)Vtable(acces)[3])(acces, &iid, &texture), "GetInterface(ID3D11Texture2D)"); }
                finally { Liberer(acces); }
            }
            finally { Liberer(surface); }

            try { return Copier(texture, contenu, lire); }
            finally { Liberer(texture); }
        }
        finally { Fermer(image); }
    }

    private bool Copier(void* texture, Taille contenu, Action<IntPtr, int, int, int> lire)
    {
        if (!ZoneClient(contenu, out Boite zone)) return false;
        uint largeur = zone.Droite - zone.Gauche, hauteur = zone.Bas - zone.Haut;

        if (_staging == null || _largeurStaging != largeur || _hauteurStaging != hauteur)
        {
            Liberer(_staging);
            var desc = new Texture2DDesc
            {
                Largeur = largeur, Hauteur = hauteur, Mips = 1, Tableau = 1, Format = FormatBgra, Echantillons = 1,
                Usage = UsageStaging, AccesCpu = LectureCpu,
            };
            void* staging;
            // ID3D11Device : CreateBuffer (3), CreateTexture1D (4), CreateTexture2D (5).
            Verifier(((delegate* unmanaged[Stdcall]<void*, Texture2DDesc*, void*, void**, int>)Vtable(_device)[5])(_device, &desc, null, &staging), "CreateTexture2D(staging)");
            _staging = staging;
            _largeurStaging = largeur;
            _hauteurStaging = hauteur;
        }

        // ID3D11DeviceContext : Map (14), Unmap (15), CopySubresourceRegion (46).
        void** vt = Vtable(_contexte);
        ((delegate* unmanaged[Stdcall]<void*, void*, uint, uint, uint, uint, void*, uint, Boite*, void>)vt[46])(
            _contexte, _staging, 0, 0, 0, 0, texture, 0, &zone);
        Plan plan;
        Verifier(((delegate* unmanaged[Stdcall]<void*, void*, uint, uint, uint, Plan*, int>)vt[14])(_contexte, _staging, 0, MapLecture, 0, &plan), "Map");
        try { lire((IntPtr)plan.Donnees, (int)plan.Pas, (int)largeur, (int)hauteur); }
        finally { ((delegate* unmanaged[Stdcall]<void*, void*, uint, void>)vt[15])(_contexte, _staging, 0); }
        return true;
    }

    /// <summary>Le rectangle client de la fenêtre dans l'image capturée, borné à l'image.</summary>
    private bool ZoneClient(Taille contenu, out Boite zone)
    {
        zone = default;
        Rect cadre, client;
        Point origine = default;
        if (DwmGetWindowAttribute(Fenetre, CadreEtendu, &cadre, (uint)sizeof(Rect)) < 0) return false;
        if (!GetClientRect(Fenetre, &client) || !ClientToScreen(Fenetre, &origine)) return false;

        int x = Math.Max(0, origine.X - cadre.Gauche), y = Math.Max(0, origine.Y - cadre.Haut);
        int droite = Math.Min(contenu.Largeur, x + client.Droite), bas = Math.Min(contenu.Hauteur, y + client.Bas);
        if (droite - x < 16 || bas - y < 16) return false;                 // réduite, ou pas encore dessinée
        zone = new Boite { Gauche = (uint)x, Haut = (uint)y, Avant = 0, Droite = (uint)droite, Bas = (uint)bas, Arriere = 1 };
        return true;
    }

    private static void Fermer(void* objet)
    {
        // IClosable : Close (6).
        void* c = Interroger(objet, IID_IClosable);
        if (c != null)
        {
            ((delegate* unmanaged[Stdcall]<void*, int>)Vtable(c)[PremiereMethodeWinRT])(c);
            Liberer(c);
        }
        Liberer(objet);
    }

    public void Dispose()
    {
        if (_session != null) Fermer(_session);
        if (_pool != null) Fermer(_pool);
        if (_item != null) Liberer(_item);
        Liberer(_staging);
        Liberer(_deviceWinRT);
        Liberer(_contexte);
        Liberer(_device);
        _session = _pool = _item = _staging = _deviceWinRT = _contexte = _device = null;
    }
}
