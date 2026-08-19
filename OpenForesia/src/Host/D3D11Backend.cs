using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.Direct3D.Compilers;
using Silk.NET.Direct3D11;
using Silk.NET.DXGI;

namespace OpenForesia.Blaze;

/// <summary>
/// Sommet tel que le jeu l'émet — disposition de <c>D3DTLVERTEX</c>, 32 octets.
///
/// Les coordonnées sont **déjà projetées** : le jeu est un portage PlayStation, où la projection
/// est faite par le GTE côté processeur. <c>Rhw</c> vaut 1/w et sert à l'interpolation
/// perspective-correcte des coordonnées de texture, pas à une transformation.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct TlVertex
{
    public float X, Y, Z, Rhw;
    public uint Diffuse;   // D3DCOLOR, ordre BGRA
    public uint Specular;  // D3DCOLOR, ordre BGRA
    public float U, V;
}

/// <summary>
/// Backend Direct3D 11 destiné à remplacer <c>3dhelp.dll</c> derrière la façade de
/// <see cref="DeviceShim"/>.
///
/// Raison d'être : <c>3dhelp</c> affiche via DirectDraw en mode exclusif, change physiquement le
/// mode d'affichage (<c>3dhelp.c:1275</c>) et présente par flip matériel. Mesuré sur matériel
/// moderne, ce chemin négocie un mode 1440x900 alors que le jeu ne rend que 1024x768 au plus —
/// d'où l'image cantonnée au coin haut gauche, et vraisemblablement les écrans noirs
/// intermittents. Une swap chain DXGI en mode fenêtré supprime les trois mécanismes à la fois.
///
/// Architecture : le jeu rend dans une **cible hors écran** aux dimensions qu'il choisit lui-même
/// (320x240 à 1024x768) ; cette cible est ensuite présentée mise à l'échelle dans la fenêtre. Cette
/// indirection est ce qui rend le mode fenêtré à la fois gratuit et fidèle — redimensionner la
/// fenêtre n'a aucun effet observable côté jeu.
/// </summary>
internal static unsafe class D3D11Backend
{
    private static D3D11? s_Api;
    private static D3DCompiler? s_Compiler;

    private static ComPtr<ID3D11Device> s_Device;
    private static ComPtr<ID3D11DeviceContext> s_Context;
    private static ComPtr<IDXGISwapChain> s_SwapChain;

    // Chaîne d'échange : cible de la présentation finale, aux dimensions de la zone cliente.
    private static ComPtr<ID3D11RenderTargetView> s_BackBufferView;
    private static int s_BackBufferWidth, s_BackBufferHeight;

    // Cible hors écran : ce dans quoi le jeu rend réellement.
    private static ComPtr<ID3D11Texture2D> s_SceneTexture;
    private static ComPtr<ID3D11RenderTargetView> s_SceneView;
    private static ComPtr<ID3D11ShaderResourceView> s_SceneSrv;
    private static ComPtr<ID3D11Texture2D> s_DepthTexture;
    private static ComPtr<ID3D11DepthStencilView> s_DepthView;
    private static int s_SceneWidth, s_SceneHeight;

    private static ComPtr<ID3D11VertexShader> s_VertexShader;
    private static ComPtr<ID3D11PixelShader> s_PixelShader;
    private static ComPtr<ID3D11InputLayout> s_InputLayout;
    private static ComPtr<ID3D11Buffer> s_ConstantBuffer;
    private static ComPtr<ID3D11Buffer> s_VertexBuffer;
    private static ComPtr<ID3D11SamplerState> s_PointSampler;
    private static ComPtr<ID3D11BlendState> s_OpaqueBlend;
    private static ComPtr<ID3D11BlendState> s_AlphaBlend;
    private static ComPtr<ID3D11RasterizerState> s_Rasterizer;
    private static ComPtr<ID3D11DepthStencilState> s_DepthOff;

    // Texture blanche 1x1 : permet de n'avoir qu'un seul shader de pixels. Une primitive non
    // texturée se dessine en liant cette texture, dont l'échantillonnage est neutre.
    private static ComPtr<ID3D11ShaderResourceView> s_WhiteTexture;

    private const int MaxVerticesPerBatch = 4096;

    /// <summary>Vrai une fois <see cref="Initialize"/> mené à bien.</summary>
    internal static bool Ready { get; private set; }

    // =====================================================================
    // Shaders
    // =====================================================================

    /// <summary>
    /// Les sommets arrivent en coordonnées écran. Le shader ne fait donc **aucune** transformation
    /// de modèle, de vue ou de projection : il convertit des pixels en coordonnées normalisées.
    ///
    /// Le jeu de passes sur <c>w</c> mérite une explication. Le matériel divise systématiquement
    /// par <c>w</c> après le shader. Pour que la position écran ressorte intacte tout en gardant
    /// une interpolation perspective-correcte des coordonnées de texture, on émet
    /// <c>w = 1/rhw</c> et on pré-multiplie <c>xyz</c> par ce même <c>w</c> : la division du
    /// matériel les ramène exactement à leur valeur d'origine, et le matériel dispose malgré tout
    /// du <c>w</c> dont il a besoin pour interpoler.
    /// </summary>
    private const string ShaderSource = """
        cbuffer Constants : register(b0)
        {
            float2 InvTargetSize;     // 1 / (largeur, hauteur) de la cible de rendu
            float  Modulate;          // 1 = texture x diffuse, 0 = texture seule
            float  AlphaFromTexture;  // 1 = MODULATEALPHA (alpha texture x diffuse), 0 = MODULATE
            float2 ViewportOrigin;    // origine du viewport, à retrancher des positions
            float2 Padding;
        };

        Texture2D    SourceTexture : register(t0);
        SamplerState SourceSampler : register(s0);

        struct VSInput
        {
            float4 Position : POSITION;     // x, y en pixels ; z profondeur ; w = rhw
            float4 Diffuse  : COLOR0;
            float4 Specular : COLOR1;
            float2 Texcoord : TEXCOORD0;
        };

        struct VSOutput
        {
            float4 Position : SV_POSITION;
            float4 Diffuse  : COLOR0;
            float2 Texcoord : TEXCOORD0;
        };

        VSOutput VSMain(VSInput input)
        {
            VSOutput output;

            // Le jeu émet ses sommets en coordonnées **absolues dans la surface d'affichage**, où
            // son viewport est posé à ViewportOrigin. On rend dans une cible aux dimensions du
            // viewport seul : il faut donc retrancher cette origine, sinon l'image se décale et
            // sort du cadre d'autant. Mesuré : sans ce retranchement l'image est décalée de
            // (32, 24) par rapport au rendu d'origine — exactement l'origine que le jeu pose.
            float2 ndc = (input.Position.xy - ViewportOrigin) * InvTargetSize * 2.0f - 1.0f;
            ndc.y = -ndc.y;

            float rhw = input.Position.w;
            float w = (rhw != 0.0f) ? (1.0f / rhw) : 1.0f;

            output.Position = float4(ndc * w, input.Position.z * w, w);
            output.Diffuse  = input.Diffuse;
            output.Texcoord = input.Texcoord;
            return output;
        }

        float4 PSMain(VSOutput input) : SV_Target
        {
            float4 t = SourceTexture.Sample(SourceSampler, input.Texcoord);
            // Direct3D 3 décide de la combinaison texture/diffuse par TEXTUREMAPBLEND. Le jeu ne
            // le pose qu'à deux valeurs, **2 (MODULATE) et 4 (MODULATEALPHA)** — jamais DECAL :
            // `winsys3d.c:2522/2526` et `6024/6027`, le choix ne dépendant que d'un bit de capacité
            // du device. Les deux multiplient la texture par la diffuse. La modulation est donc
            // toujours active sur ce chemin ; `Modulate` reste paramétrable pour le quad de
            // présentation, qui n'est pas une primitive du jeu.
            float4 c = lerp(t, t * input.Diffuse, Modulate);
            // Couleur-clé. Les textures du jeu arrivent en ARGB1555 (ou 4444) : le bit d'alpha à
            // zéro y signifie « texel transparent », ce que Direct3D 3 réalisait par DDCKEY_SRCBLT
            // — un test, pas un mélange. On reproduit le test, ce qui évite d'introduire une
            // dépendance à l'ordre de dessin qui n'existait pas dans l'original. La texture blanche
            // 1x1 des primitives non texturées a un alpha plein, donc ce test ne les touche pas.
            if (t.a <= 0.0f) discard;
            // Alpha : `TEXTUREMAPBLEND` 2 (MODULATE) prend celui de la diffuse seule, 4
            // (MODULATEALPHA) y multiplie celui de la texture. Le jeu pose l'un ou l'autre selon
            // une capacité du device (`winsys3d.c:2522/2526`). Cet alpha ne compte que lorsque
            // ALPHABLENDENABLE est actif — sinon l'état de mélange opaque l'ignore.
            c.a = lerp(input.Diffuse.a, t.a * input.Diffuse.a, AlphaFromTexture);
            return c;
        }
        """;

    private static ComPtr<ID3D10Blob> Compile(string entryPoint, string target)
    {
        byte[] source = System.Text.Encoding.ASCII.GetBytes(ShaderSource);
        nint entryPtr = Marshal.StringToHGlobalAnsi(entryPoint);
        nint targetPtr = Marshal.StringToHGlobalAnsi(target);
        nint namePtr = Marshal.StringToHGlobalAnsi("OpenForesia.hlsl");

        try
        {
            ID3D10Blob* code = null;
            ID3D10Blob* errors = null;

            int hr;
            fixed (byte* pSource = source)
            {
                hr = s_Compiler!.Compile(pSource, (nuint)source.Length, (byte*)namePtr,
                    null, (ID3DInclude*)null, (byte*)entryPtr, (byte*)targetPtr,
                    0, 0, &code, &errors);
            }

            if (hr < 0)
            {
                string message = errors != null
                    ? Marshal.PtrToStringAnsi((nint)errors->GetBufferPointer()) ?? "(sans détail)"
                    : "(sans détail)";
                throw new InvalidOperationException(
                    $"Compilation du shader {entryPoint} échouée (0x{hr:X8}) : {message}");
            }

            return new ComPtr<ID3D10Blob>(code);
        }
        finally
        {
            Marshal.FreeHGlobal(entryPtr);
            Marshal.FreeHGlobal(targetPtr);
            Marshal.FreeHGlobal(namePtr);
        }
    }

    // =====================================================================
    // Initialisation
    // =====================================================================

    /// <summary>
    /// Crée la fenêtre, le device, la swap chain fenêtrée et toutes les ressources fixes. La cible
    /// hors écran est dimensionnée par <paramref name="sceneWidth"/> / <paramref name="sceneHeight"/>
    /// et suivra ensuite les changements de taille de rendu du jeu.
    /// </summary>
    /// <summary>
    /// Crée la fenêtre du jeu si elle n'existe pas déjà, **sans** device ni swap chain, et rend son
    /// handle.
    ///
    /// Étape volontairement distincte de <see cref="Initialize"/>. Le régime de démarrage —
    /// écran-titre et attract — présente en GDI et n'a besoin d'aucun objet Direct3D, alors que
    /// créer le device pendant l'initialisation du jeu l'empêche d'atteindre sa boucle de rendu
    /// (mesuré, voir le commentaire de <c>DeviceShim.EnsureBackend</c>). Séparer les deux permet
    /// d'avoir la fenêtre tôt sans payer ce prix.
    ///
    /// Idempotente : sans effet si la fenêtre existe déjà, quelles que soient les dimensions
    /// demandées.
    /// </summary>
    internal static IntPtr EnsureWindow(int clientWidth, int clientHeight, string title = "OpenForesia")
        => D3D11Window.Create(clientWidth, clientHeight, title);

    internal static void Initialize(int sceneWidth, int sceneHeight, string title = "OpenForesia")
    {
        if (Ready) return;

        IntPtr hwnd = EnsureWindow(sceneWidth, sceneHeight, title);

        s_Api = D3D11.GetApi(null);
        s_Compiler = D3DCompiler.GetApi();

        var (clientW, clientH) = D3D11Window.ClientSize();
        if (clientW <= 0) { clientW = sceneWidth; clientH = sceneHeight; }

        var swapDesc = new SwapChainDesc
        {
            BufferDesc = new ModeDesc
            {
                Width = (uint)clientW,
                Height = (uint)clientH,
                Format = Format.FormatB8G8R8A8Unorm,
                RefreshRate = new Rational(0, 1),
            },
            SampleDesc = new SampleDesc(1, 0),
            BufferUsage = DXGI.UsageRenderTargetOutput,
            BufferCount = 2,
            OutputWindow = hwnd,
            // Le point de toute la change : fenêtré, donc ni mode exclusif ni changement de mode.
            Windowed = 1,
            SwapEffect = SwapEffect.Discard,
            Flags = 0,
        };

        IDXGISwapChain* swapChain = null;
        ID3D11Device* device = null;
        ID3D11DeviceContext* context = null;
        D3DFeatureLevel level = default;

        int hr = s_Api.CreateDeviceAndSwapChain(
            (IDXGIAdapter*)null,
            D3DDriverType.Hardware,
            nint.Zero,
            (uint)CreateDeviceFlag.BgraSupport,
            null,
            0,
            D3D11.SdkVersion,
            &swapDesc,
            &swapChain,
            &device,
            &level,
            &context);

        if (hr < 0)
            throw new InvalidOperationException($"CreateDeviceAndSwapChain a échoué : 0x{hr:X8}");

        s_SwapChain = new ComPtr<IDXGISwapChain>(swapChain);
        s_Device = new ComPtr<ID3D11Device>(device);
        s_Context = new ComPtr<ID3D11DeviceContext>(context);

        DisableDxgiFullscreenShortcut(hwnd);

        CreateShadersAndStates();
        CreateBackBufferView(clientW, clientH);
        EnsureSceneTarget(sceneWidth, sceneHeight);

        Ready = true;
        Console.Error.WriteLine(
            $"[D3D11] backend prêt — fenêtre {clientW}x{clientH}, scène {sceneWidth}x{sceneHeight}, " +
            $"feature level 0x{(uint)level:X4}");
    }

    /// <summary>
    /// Retire à DXGI la main sur Alt+Entrée.
    ///
    /// Par défaut, DXGI intercepte ce raccourci et bascule la swap chain en **plein écran
    /// exclusif** — c'est-à-dire exactement le mécanisme qu'on vient de retirer à `3dhelp`, avec
    /// changement de mode d'affichage à la clé. Le plein écran de cette change est sans bordure et
    /// relève de la fenêtre, pas de DXGI.
    /// </summary>
    private static void DisableDxgiFullscreenShortcut(IntPtr hwnd)
    {
        const uint DxgiMwaNoAltEnter = 2;

        ComPtr<IDXGIFactory> factory = default;
        Guid iid = IDXGIFactory.Guid;
        int hr = s_SwapChain.GetParent(ref iid, (void**)factory.GetAddressOf());
        if (hr < 0)
        {
            Console.Error.WriteLine($"[D3D11] fabrique DXGI introuvable (0x{hr:X8}) ; Alt+Entrée reste à DXGI");
            return;
        }

        factory.MakeWindowAssociation(hwnd, DxgiMwaNoAltEnter);
        factory.Dispose();
    }

    private static void CreateShadersAndStates()
    {
        using ComPtr<ID3D10Blob> vsCode = Compile("VSMain", "vs_4_0");
        using ComPtr<ID3D10Blob> psCode = Compile("PSMain", "ps_4_0");

        SilkMarshal.ThrowHResult(s_Device.CreateVertexShader(
            vsCode.GetBufferPointer(), vsCode.GetBufferSize(), default(ComPtr<ID3D11ClassLinkage>), ref s_VertexShader));
        SilkMarshal.ThrowHResult(s_Device.CreatePixelShader(
            psCode.GetBufferPointer(), psCode.GetBufferSize(), default(ComPtr<ID3D11ClassLinkage>), ref s_PixelShader));

        nint position = Marshal.StringToHGlobalAnsi("POSITION");
        nint color = Marshal.StringToHGlobalAnsi("COLOR");
        nint texcoord = Marshal.StringToHGlobalAnsi("TEXCOORD");
        try
        {
            // Disposition calquée sur D3DTLVERTEX. Les couleurs sont des D3DCOLOR, donc BGRA.
            var elements = stackalloc InputElementDesc[4];
            elements[0] = new InputElementDesc
            {
                SemanticName = (byte*)position, SemanticIndex = 0,
                Format = Format.FormatR32G32B32A32Float, InputSlot = 0, AlignedByteOffset = 0,
                InputSlotClass = InputClassification.PerVertexData,
            };
            elements[1] = new InputElementDesc
            {
                SemanticName = (byte*)color, SemanticIndex = 0,
                Format = Format.FormatB8G8R8A8Unorm, InputSlot = 0, AlignedByteOffset = 16,
                InputSlotClass = InputClassification.PerVertexData,
            };
            elements[2] = new InputElementDesc
            {
                SemanticName = (byte*)color, SemanticIndex = 1,
                Format = Format.FormatB8G8R8A8Unorm, InputSlot = 0, AlignedByteOffset = 20,
                InputSlotClass = InputClassification.PerVertexData,
            };
            elements[3] = new InputElementDesc
            {
                SemanticName = (byte*)texcoord, SemanticIndex = 0,
                Format = Format.FormatR32G32Float, InputSlot = 0, AlignedByteOffset = 24,
                InputSlotClass = InputClassification.PerVertexData,
            };

            SilkMarshal.ThrowHResult(s_Device.CreateInputLayout(
                elements, 4, vsCode.GetBufferPointer(), vsCode.GetBufferSize(), ref s_InputLayout));
        }
        finally
        {
            Marshal.FreeHGlobal(position);
            Marshal.FreeHGlobal(color);
            Marshal.FreeHGlobal(texcoord);
        }

        var cbDesc = new BufferDesc
        {
            ByteWidth = 32,
            Usage = Usage.Dynamic,
            BindFlags = (uint)BindFlag.ConstantBuffer,
            CPUAccessFlags = (uint)CpuAccessFlag.Write,
        };
        SilkMarshal.ThrowHResult(s_Device.CreateBuffer(in cbDesc, null, ref s_ConstantBuffer));

        var vbDesc = new BufferDesc
        {
            ByteWidth = (uint)(sizeof(TlVertex) * MaxVerticesPerBatch),
            Usage = Usage.Dynamic,
            BindFlags = (uint)BindFlag.VertexBuffer,
            CPUAccessFlags = (uint)CpuAccessFlag.Write,
        };
        SilkMarshal.ThrowHResult(s_Device.CreateBuffer(in vbDesc, null, ref s_VertexBuffer));

        // Échantillonnage au plus proche : le jeu vient d'un matériel sans filtrage, et la clé
        // "Filter" du .cfg est le réglage qui décidera plus tard d'activer le linéaire.
        var sampler = new SamplerDesc
        {
            Filter = Filter.MinMagMipPoint,
            AddressU = TextureAddressMode.Wrap,
            AddressV = TextureAddressMode.Wrap,
            AddressW = TextureAddressMode.Wrap,
            ComparisonFunc = ComparisonFunc.Never,
            MaxLOD = float.MaxValue,
        };
        SilkMarshal.ThrowHResult(s_Device.CreateSamplerState(in sampler, ref s_PointSampler));

        var opaque = default(BlendDesc);
        opaque.RenderTarget[0].BlendEnable = 0;
        opaque.RenderTarget[0].RenderTargetWriteMask = (byte)ColorWriteEnable.All;
        SilkMarshal.ThrowHResult(s_Device.CreateBlendState(in opaque, ref s_OpaqueBlend));

        var alpha = default(BlendDesc);
        alpha.RenderTarget[0].BlendEnable = 1;
        alpha.RenderTarget[0].SrcBlend = Blend.SrcAlpha;
        alpha.RenderTarget[0].DestBlend = Blend.InvSrcAlpha;
        alpha.RenderTarget[0].BlendOp = BlendOp.Add;
        alpha.RenderTarget[0].SrcBlendAlpha = Blend.One;
        alpha.RenderTarget[0].DestBlendAlpha = Blend.InvSrcAlpha;
        alpha.RenderTarget[0].BlendOpAlpha = BlendOp.Add;
        alpha.RenderTarget[0].RenderTargetWriteMask = (byte)ColorWriteEnable.All;
        SilkMarshal.ThrowHResult(s_Device.CreateBlendState(in alpha, ref s_AlphaBlend));

        var raster = new RasterizerDesc
        {
            FillMode = FillMode.Solid,
            // Le jeu émet ses triangles dans les deux sens ; le tri de faces se ferait à tort.
            CullMode = CullMode.None,
            DepthClipEnable = 1,
        };
        SilkMarshal.ThrowHResult(s_Device.CreateRasterizerState(in raster, ref s_Rasterizer));

        var depthOff = new DepthStencilDesc
        {
            DepthEnable = 0,
            DepthWriteMask = DepthWriteMask.Zero,
            DepthFunc = ComparisonFunc.Always,
        };
        SilkMarshal.ThrowHResult(s_Device.CreateDepthStencilState(in depthOff, ref s_DepthOff));

        CreateWhiteTexture();
    }

    private static void CreateWhiteTexture()
    {
        uint white = 0xFFFFFFFF;
        var desc = new Texture2DDesc
        {
            Width = 1, Height = 1, MipLevels = 1, ArraySize = 1,
            Format = Format.FormatB8G8R8A8Unorm,
            SampleDesc = new SampleDesc(1, 0),
            Usage = Usage.Immutable,
            BindFlags = (uint)BindFlag.ShaderResource,
        };
        var data = new SubresourceData { PSysMem = &white, SysMemPitch = 4 };

        ComPtr<ID3D11Texture2D> texture = default;
        SilkMarshal.ThrowHResult(s_Device.CreateTexture2D(in desc, in data, ref texture));
        SilkMarshal.ThrowHResult(s_Device.CreateShaderResourceView(
            texture, (ShaderResourceViewDesc*)null, ref s_WhiteTexture));
        texture.Dispose();
    }

    // =====================================================================
    // Cibles de rendu
    // =====================================================================

    private static void CreateBackBufferView(int width, int height)
    {
        s_BackBufferView.Dispose();
        s_BackBufferView = default;

        ComPtr<ID3D11Texture2D> backBuffer = default;
        SilkMarshal.ThrowHResult(s_SwapChain.GetBuffer(0, out backBuffer));
        SilkMarshal.ThrowHResult(s_Device.CreateRenderTargetView(
            backBuffer, (RenderTargetViewDesc*)null, ref s_BackBufferView));
        backBuffer.Dispose();

        s_BackBufferWidth = width;
        s_BackBufferHeight = height;
    }

    /// <summary>
    /// Aligne la cible hors écran sur les dimensions demandées par le jeu, en la recréant si elles
    /// ont changé. Le jeu peut modifier sa taille de rendu en cours d'exécution (F11 / F12), ce qui
    /// doit rester sans effet sur la fenêtre comme sur le mode d'affichage.
    /// </summary>
    internal static void EnsureSceneTarget(int width, int height)
    {
        if (width <= 0 || height <= 0) return;
        if (s_SceneWidth == width && s_SceneHeight == height && s_SceneView.Handle != null) return;

        s_SceneSrv.Dispose(); s_SceneSrv = default;
        s_SceneView.Dispose(); s_SceneView = default;
        s_SceneTexture.Dispose(); s_SceneTexture = default;
        s_DepthView.Dispose(); s_DepthView = default;
        s_DepthTexture.Dispose(); s_DepthTexture = default;

        var colorDesc = new Texture2DDesc
        {
            Width = (uint)width, Height = (uint)height, MipLevels = 1, ArraySize = 1,
            Format = Format.FormatB8G8R8A8Unorm,
            SampleDesc = new SampleDesc(1, 0),
            Usage = Usage.Default,
            BindFlags = (uint)(BindFlag.RenderTarget | BindFlag.ShaderResource),
        };
        SilkMarshal.ThrowHResult(s_Device.CreateTexture2D(in colorDesc, null, ref s_SceneTexture));
        SilkMarshal.ThrowHResult(s_Device.CreateRenderTargetView(
            s_SceneTexture, (RenderTargetViewDesc*)null, ref s_SceneView));
        SilkMarshal.ThrowHResult(s_Device.CreateShaderResourceView(
            s_SceneTexture, (ShaderResourceViewDesc*)null, ref s_SceneSrv));

        var depthDesc = new Texture2DDesc
        {
            Width = (uint)width, Height = (uint)height, MipLevels = 1, ArraySize = 1,
            Format = Format.FormatD24UnormS8Uint,
            SampleDesc = new SampleDesc(1, 0),
            Usage = Usage.Default,
            BindFlags = (uint)BindFlag.DepthStencil,
        };
        SilkMarshal.ThrowHResult(s_Device.CreateTexture2D(in depthDesc, null, ref s_DepthTexture));
        SilkMarshal.ThrowHResult(s_Device.CreateDepthStencilView(
            s_DepthTexture, (DepthStencilViewDesc*)null, ref s_DepthView));

        s_SceneWidth = width;
        s_SceneHeight = height;
    }

    /// <summary>Redimensionne la chaîne d'échange après un redimensionnement de la fenêtre.</summary>
    internal static void HandleResize()
    {
        var (w, h) = D3D11Window.ClientSize();
        if (w <= 0 || h <= 0 || (w == s_BackBufferWidth && h == s_BackBufferHeight)) return;

        s_Context.OMSetRenderTargets(0, ref Unsafe.NullRef<ComPtr<ID3D11RenderTargetView>>(),
            default(ComPtr<ID3D11DepthStencilView>));
        s_BackBufferView.Dispose();
        s_BackBufferView = default;

        SilkMarshal.ThrowHResult(s_SwapChain.ResizeBuffers(
            0, (uint)w, (uint)h, Format.FormatUnknown, 0));
        CreateBackBufferView(w, h);
    }

    // =====================================================================
    // Rendu
    // =====================================================================

    // Dimensions de la cible actuellement liée, pour que DrawTriangles puisse réécrire le tampon
    // de constantes par primitive (le facteur de modulation change d'un appel à l'autre).
    private static int s_ConstTargetW, s_ConstTargetH;

    private static void SetSceneTargetAndState()
    {
        var view = s_SceneView;
        var depth = s_DepthView;
        s_Context.OMSetRenderTargets(1, ref view, depth);

        var viewport = new Silk.NET.Direct3D11.Viewport(0, 0, s_SceneWidth, s_SceneHeight, 0, 1);
        s_Context.RSSetViewports(1, in viewport);

        s_ConstTargetW = s_SceneWidth;
        s_ConstTargetH = s_SceneHeight;
        UpdateConstants(s_ConstTargetW, s_ConstTargetH);
    }

    /// <summary>
    /// Origine du viewport que le jeu pose (slot <c>+0x50</c>). Les sommets qu'il émet sont en
    /// coordonnées absolues dans sa surface d'affichage ; la cible hors écran ne couvrant que le
    /// viewport, cette origine est retranchée au moment du rendu.
    /// </summary>
    internal static void SetViewportOrigin(int x, int y)
    {
        s_ViewportOriginX = x;
        s_ViewportOriginY = y;
    }

    private static int s_ViewportOriginX, s_ViewportOriginY;

    private static void UpdateConstants(int targetWidth, int targetHeight, float modulate = 1f,
                                        float alphaFromTexture = 0f, bool applyViewportOrigin = false)
    {
        MappedSubresource mapped = default;
        SilkMarshal.ThrowHResult(s_Context.Map(
            s_ConstantBuffer, 0, Map.WriteDiscard, 0, ref mapped));
        float* c = (float*)mapped.PData;
        c[0] = 1f / targetWidth;
        c[1] = 1f / targetHeight;
        c[2] = modulate;
        c[3] = alphaFromTexture;
        c[4] = applyViewportOrigin ? s_ViewportOriginX : 0f;
        c[5] = applyViewportOrigin ? s_ViewportOriginY : 0f;
        c[6] = 0f;
        c[7] = 0f;
        s_Context.Unmap(s_ConstantBuffer, 0);

        var cb = s_ConstantBuffer;
        s_Context.VSSetConstantBuffers(0, 1, ref cb);
        s_Context.PSSetConstantBuffers(0, 1, ref cb);
    }

    /// <summary>Efface la cible de couleur sans toucher au tampon de profondeur.</summary>
    internal static void ClearColor(float r, float g, float b, float a = 1f)
    {
        float* color = stackalloc float[4] { r, g, b, a };
        s_Context.ClearRenderTargetView(s_SceneView, color);
    }

    /// <summary>Efface le tampon de profondeur sans toucher à la cible de couleur.</summary>
    internal static void ClearDepth(float depth = 1f)
    {
        s_Context.ClearDepthStencilView(
            s_DepthView, (uint)ClearFlag.Depth, depth, 0);
    }

    /// <summary>
    /// Dessine une liste de triangles de sommets déjà transformés. <paramref name="texture"/> à
    /// <c>null</c> sélectionne la texture blanche, ce qui rend la primitive non texturée sans
    /// nécessiter un second shader.
    /// </summary>
    internal static void DrawTriangles(
        ReadOnlySpan<TlVertex> vertices, ComPtr<ID3D11ShaderResourceView> texture,
        bool alphaBlend = false, float modulate = 1f, float alphaFromTexture = 0f,
        bool applyViewportOrigin = false)
    {
        if (vertices.Length < 3) return;

        // Découpage en lots sur une frontière multiple de 3, pour ne jamais scinder un triangle.
        if (vertices.Length > MaxVerticesPerBatch)
        {
            const int chunk = MaxVerticesPerBatch - MaxVerticesPerBatch % 3;
            for (int start = 0; start < vertices.Length; start += chunk)
            {
                int len = Math.Min(chunk, vertices.Length - start);
                DrawTriangles(vertices.Slice(start, len), texture, alphaBlend, modulate,
                              alphaFromTexture, applyViewportOrigin);
            }
            return;
        }

        UpdateConstants(s_ConstTargetW, s_ConstTargetH, modulate, alphaFromTexture, applyViewportOrigin);

        MappedSubresource mapped = default;
        SilkMarshal.ThrowHResult(s_Context.Map(s_VertexBuffer, 0, Map.WriteDiscard, 0, ref mapped));
        fixed (TlVertex* src = vertices)
        {
            System.Buffer.MemoryCopy(src, mapped.PData, (long)sizeof(TlVertex) * MaxVerticesPerBatch,
                (long)sizeof(TlVertex) * vertices.Length);
        }
        s_Context.Unmap(s_VertexBuffer, 0);

        uint stride = (uint)sizeof(TlVertex);
        uint offset = 0;
        var vb = s_VertexBuffer;
        s_Context.IASetVertexBuffers(0, 1, ref vb, in stride, in offset);
        s_Context.IASetInputLayout(s_InputLayout);
        s_Context.IASetPrimitiveTopology(D3DPrimitiveTopology.D3DPrimitiveTopologyTrianglelist);

        s_Context.VSSetShader(s_VertexShader, (ID3D11ClassInstance**)null, 0);
        s_Context.PSSetShader(s_PixelShader, (ID3D11ClassInstance**)null, 0);

        var srv = texture.Handle != null ? texture : s_WhiteTexture;
        s_Context.PSSetShaderResources(0, 1, ref srv);
        var sampler = s_PointSampler;
        s_Context.PSSetSamplers(0, 1, ref sampler);

        var blend = alphaBlend ? s_AlphaBlend : s_OpaqueBlend;
        float* factor = stackalloc float[4] { 1, 1, 1, 1 };
        s_Context.OMSetBlendState(blend, factor, 0xFFFFFFFF);
        s_Context.RSSetState(s_Rasterizer);
        s_Context.OMSetDepthStencilState(s_DepthOff, 0);

        s_Context.Draw((uint)vertices.Length, 0);
    }

    /// <summary>Prépare la cible hors écran pour une nouvelle image.</summary>
    internal static void BeginScene() => SetSceneTargetAndState();

    // Tampon de dépliage réutilisé d'un appel à l'autre : DrawPrimitive est le slot le plus
    // sollicité du chemin chaud (45 sites d'appel), il ne doit pas allouer par primitive.
    private static TlVertex[] s_Expanded = new TlVertex[MaxVerticesPerBatch];

    /// <summary>
    /// Exécute un <c>DrawPrimitive</c> tel que le jeu l'émet.
    ///
    /// Deux traductions sont nécessaires, toutes deux faites ici plutôt que dans le shader :
    ///
    /// - **Topologie.** Direct3D 11 ne connaît pas l'éventail de triangles ; les bandes et les
    ///   éventails sont donc dépliés en liste. Le coût est négligeable devant le nombre de sommets
    ///   en jeu, et cela évite un chemin de rendu distinct par topologie.
    /// - **Ombrage plat.** Direct3D 3 prend la couleur du **premier** sommet du triangle et
    ///   l'applique aux trois. On la recopie au dépliage, ce qui reproduit la règle exactement sans
    ///   dépendre d'une sémantique d'interpolation du matériel.
    /// </summary>
    internal static void DrawPrimitive(
        uint primitiveType, TlVertex* vertices, int count,
        ComPtr<ID3D11ShaderResourceView> texture, bool flatShading,
        bool alphaBlend = false, bool alphaFromTexture = false)
    {
        if (count < 3) return;

        int needed = primitiveType switch
        {
            4 => count - count % 3,          // D3DPT_TRIANGLELIST
            5 or 6 => (count - 2) * 3,       // D3DPT_TRIANGLESTRIP / D3DPT_TRIANGLEFAN
            _ => throw new NotImplementedException(
                $"DrawPrimitive : topologie {primitiveType} non portée " +
                "(1=points, 2=lignes, 3=bande de lignes). Aucune n'a encore été observée."),
        };

        if (needed <= 0) return;
        if (s_Expanded.Length < needed) s_Expanded = new TlVertex[needed];

        int n = 0;
        switch (primitiveType)
        {
            case 4:
                for (int i = 0; i + 2 < count; i += 3) Emit(vertices[i], vertices[i + 1], vertices[i + 2]);
                break;
            case 5:
                for (int i = 0; i + 2 < count; i++) Emit(vertices[i], vertices[i + 1], vertices[i + 2]);
                break;
            case 6:
                for (int i = 1; i + 1 < count; i++) Emit(vertices[0], vertices[i], vertices[i + 1]);
                break;
        }

        DrawTriangles(s_Expanded.AsSpan(0, n), texture, alphaBlend, 1f, alphaFromTexture ? 1f : 0f,
                      applyViewportOrigin: true);

        void Emit(TlVertex a, TlVertex b, TlVertex c)
        {
            if (flatShading) { b.Diffuse = a.Diffuse; c.Diffuse = a.Diffuse; }
            s_Expanded[n++] = a;
            s_Expanded[n++] = b;
            s_Expanded[n++] = c;
        }
    }

    /// <summary>
    /// Présente la cible hors écran dans la fenêtre : mise à l'échelle, rapport d'aspect préservé,
    /// image centrée, bandes restantes en noir uni.
    /// </summary>
    internal static void Present()
    {
        if (D3D11Window.TakeResize()) HandleResize();

        var backBuffer = s_BackBufferView;
        s_Context.OMSetRenderTargets(1, ref backBuffer, default(ComPtr<ID3D11DepthStencilView>));

        float* black = stackalloc float[4] { 0f, 0f, 0f, 1f };
        s_Context.ClearRenderTargetView(s_BackBufferView, black);

        // Rectangle de destination : le plus grand rectangle au rapport d'aspect de la scène qui
        // tienne dans la zone cliente, centré. Les bandes restantes gardent le noir de l'effacement.
        float scale = MathF.Min(
            (float)s_BackBufferWidth / s_SceneWidth,
            (float)s_BackBufferHeight / s_SceneHeight);
        float drawW = s_SceneWidth * scale;
        float drawH = s_SceneHeight * scale;
        float originX = (s_BackBufferWidth - drawW) * 0.5f;
        float originY = (s_BackBufferHeight - drawH) * 0.5f;

        var viewport = new Silk.NET.Direct3D11.Viewport(0, 0, s_BackBufferWidth, s_BackBufferHeight, 0, 1);
        s_Context.RSSetViewports(1, in viewport);
        s_ConstTargetW = s_BackBufferWidth;
        s_ConstTargetH = s_BackBufferHeight;
        UpdateConstants(s_ConstTargetW, s_ConstTargetH);

        // Le quad de présentation réutilise le même shader : couleur diffuse blanche, donc la
        // modulation est neutre et la scène ressort telle quelle.
        Span<TlVertex> quad =
        [
            Vertex(originX,         originY,         0, 0),
            Vertex(originX + drawW, originY,         1, 0),
            Vertex(originX,         originY + drawH, 0, 1),
            Vertex(originX + drawW, originY,         1, 0),
            Vertex(originX + drawW, originY + drawH, 1, 1),
            Vertex(originX,         originY + drawH, 0, 1),
        ];

        DrawTriangles(quad, s_SceneSrv);

        // Intervalle de synchronisation **1**, pas 0 — c'est la cadence du jeu, pas un réglage de
        // confort.
        //
        // Le jeu de 1998 ne se cadence pas lui-même : `Ordinal_132` (winsys3d, FUN_00a053e0) est la
        // seule attente d'image du moteur, et son corps commence par `switch (DAT_00a2c3b0)` dont le
        // `case 0` rend la main **immédiatement**. Or ce mode de cadencement n'est posé que par
        // `SetTimingMode`, appelé au seul endroit du binaire qu'est la page « Timing » du menu de
        // mise en route (FUN_00510d40) — jamais sur le chemin de démarrage normal. Le jeu tournait
        // donc en mode 0, et toute sa cadence venait du `Flip()` DirectDraw, bloquant sur le
        // retour de balayage.
        //
        // Présenter à 0 rendait tout le jeu libre : mesuré le 2026-08-18, l'écran de transition
        // entre deux états — 150 images par construction, soit 2,5 s à 60 Hz — passait en **moins
        // d'une seconde**, ce qui le rendait invisible. Toutes les animations du jeu étaient
        // affectées de la même façon, l'écran de chargement étant simplement celui où ça se voyait.
        //
        // Réserve assumée : sur un écran à plus de 60 Hz, le jeu tournera d'autant plus vite, là où
        // l'original était calé sur le 60 Hz de son mode d'affichage. Un vrai limiteur à 60 images
        // par seconde serait plus fidèle encore ; ce n'est pas fait ici, où l'on se contente de
        // restituer le comportement du `Flip` d'origine.
        SilkMarshal.ThrowHResult(s_SwapChain.Present(1, 0));

        static TlVertex Vertex(float x, float y, float u, float v) => new()
        {
            X = x, Y = y, Z = 0f, Rhw = 1f, Diffuse = 0xFFFFFFFF, Specular = 0, U = u, V = v,
        };
    }

    // =====================================================================
    // Vérification de bout en bout (tâche 2.6)
    // =====================================================================

    /// <summary>
    /// Rend une image de test pendant quelques secondes pour valider la chaîne complète : fenêtre,
    /// device, swap chain fenêtrée, cible hors écran, shaders, présentation à l'échelle.
    ///
    /// L'image contient un damier plein cadre et deux triangles à sommets colorés. Le damier valide
    /// l'échantillonnage et le cadrage — s'il ne remplit pas exactement la zone d'image, la mise à
    /// l'échelle est fausse ; les triangles valident l'interpolation de la couleur diffuse.
    /// </summary>
    internal static void SelfTest(int sceneWidth = 640, int sceneHeight = 480, int seconds = 8)
    {
        Initialize(sceneWidth, sceneHeight, "OpenForesia — test du backend Direct3D 11");

        using ComPtr<ID3D11ShaderResourceView> checker = CreateCheckerTexture(64);

        DateTime deadline = DateTime.UtcNow.AddSeconds(seconds);
        int frames = 0;

        while (D3D11Window.PumpMessages() && DateTime.UtcNow < deadline)
        {
            BeginScene();
            ClearColor(0.05f, 0.05f, 0.10f);
            ClearDepth();

            // Damier sur toute la scène : la moindre erreur de cadrage se voit immédiatement.
            float w = s_SceneWidth, h = s_SceneHeight;
            Span<TlVertex> background =
            [
                Tl(0, 0, 0, 0, 0xFFFFFFFF), Tl(w, 0, 8, 0, 0xFFFFFFFF), Tl(0, h, 0, 6, 0xFFFFFFFF),
                Tl(w, 0, 8, 0, 0xFFFFFFFF), Tl(w, h, 8, 6, 0xFFFFFFFF), Tl(0, h, 0, 6, 0xFFFFFFFF),
            ];
            DrawTriangles(background, checker);

            // Triangles à sommets colorés : valident l'interpolation de la diffuse.
            float cx = w * 0.5f, cy = h * 0.5f, r = MathF.Min(w, h) * 0.3f;
            Span<TlVertex> triangle =
            [
                Tl(cx,     cy - r, 0, 0, 0xFFFF0000),
                Tl(cx + r, cy + r, 0, 0, 0xFF00FF00),
                Tl(cx - r, cy + r, 0, 0, 0xFF0000FF),
            ];
            DrawTriangles(triangle, default, alphaBlend: true);

            Present();
            frames++;
        }

        Console.Error.WriteLine($"[D3D11] test terminé — {frames} images présentées");
        Shutdown();

        static TlVertex Tl(float x, float y, float u, float v, uint color) => new()
        {
            X = x, Y = y, Z = 0f, Rhw = 1f, Diffuse = color, Specular = 0, U = u, V = v,
        };
    }

    // =====================================================================
    // Registre des textures du jeu — tâche 3.5
    //
    // Le jeu réclame ses objets texture par le slot device `+0x10`, les charge par le slot `[3]`
    // de leur propre vtable, puis les lie par le slot device `+0x64`. Ces trois moments sont chez
    // nous depuis le device synthétique ; ce registre est ce qui les relie : la clé est le
    // **pointeur de l'objet texture**, la seule identité que le jeu manipule.
    //
    // La création de la vue est **différée**. Les premières textures sont chargées pendant
    // l'attract, avant que le device Direct3D 11 n'existe (mesuré : plus de 10 000 liaisons avant
    // la première image). On garde donc les texels convertis jusqu'à ce que le backend soit prêt,
    // et on les libère dès que la vue est créée.
    // =====================================================================

    private sealed class GameTexture
    {
        internal int Width, Height;
        internal uint[]? Pixels;                             // BGRA ; libéré une fois la vue créée
        internal ComPtr<ID3D11ShaderResourceView> View;
        internal bool ViewCreated;
    }

    private static readonly Dictionary<nint, GameTexture> s_GameTextures = [];
    private static readonly HashSet<nint> s_MissingTextures = [];

    /// <summary>Nombre d'objets texture actuellement connus. Sert au bilan de fin d'image.</summary>
    internal static int GameTextureCount => s_GameTextures.Count;

    /// <summary>Enregistre les texels convertis d'un objet texture, en remplaçant les précédents.</summary>
    internal static void RegisterTexture(nint key, int width, int height, uint[] bgra)
    {
        if (s_GameTextures.TryGetValue(key, out GameTexture? existing))
        {
            existing.View.Dispose();
            existing.View = default;
            existing.ViewCreated = false;
        }

        s_GameTextures[key] = new GameTexture { Width = width, Height = height, Pixels = bgra };
    }

    /// <summary>Libère un objet texture détruit par le jeu.</summary>
    internal static void ReleaseTexture(nint key)
    {
        if (!s_GameTextures.Remove(key, out GameTexture? texture)) return;
        texture.View.Dispose();
    }

    /// <summary>
    /// Vue de l'objet texture donné, créée à la demande. Rend une vue nulle si l'objet est inconnu
    /// ou si le device n'existe pas encore — <see cref="DrawTriangles"/> traite ce cas en liant la
    /// texture blanche, c'est-à-dire le comportement d'avant cette tâche.
    /// </summary>
    internal static ComPtr<ID3D11ShaderResourceView> TextureView(nint key)
    {
        if (!Ready) return default;

        if (!s_GameTextures.TryGetValue(key, out GameTexture? texture))
        {
            // ECHAFAUDAGE : une liaison qui ne trouve pas sa texture retombe sur le blanc, ce qui
            // se voit mal à l'écran et se diagnostique mal. On le signale une fois par clé.
            if (s_MissingTextures.Add(key))
                Console.Error.WriteLine($"[D3D11] liaison sans texture enregistrée : 0x{key:X8}");
            return default;
        }

        if (!texture.ViewCreated)
        {
            uint[]? pixels = texture.Pixels;
            if (pixels == null) return default;
            fixed (uint* p = pixels) texture.View = CreateTexture(texture.Width, texture.Height, p);
            texture.ViewCreated = true;
            texture.Pixels = null;                            // la copie GPU suffit désormais
        }

        return texture.View;
    }

    /// <summary>
    /// Crée une texture immuable à partir d'un buffer BGRA 8 bits par canal, et rend sa vue.
    /// Utilisée par le portage de la liaison de texture (tâche 3.5) : le jeu fournit ses texels en
    /// 16 bits, la conversion vers BGRA a lieu chez l'appelant, qui seul connaît les masques.
    /// </summary>
    internal static ComPtr<ID3D11ShaderResourceView> CreateTexture(int width, int height, uint* bgra)
    {
        ComPtr<ID3D11Texture2D> texture = default;
        ComPtr<ID3D11ShaderResourceView> srv = default;

        var desc = new Texture2DDesc
        {
            Width = (uint)width, Height = (uint)height, MipLevels = 1, ArraySize = 1,
            Format = Format.FormatB8G8R8A8Unorm,
            SampleDesc = new SampleDesc(1, 0),
            Usage = Usage.Immutable,
            BindFlags = (uint)BindFlag.ShaderResource,
        };
        var data = new SubresourceData { PSysMem = bgra, SysMemPitch = (uint)(width * 4) };

        SilkMarshal.ThrowHResult(s_Device.CreateTexture2D(in desc, in data, ref texture));
        SilkMarshal.ThrowHResult(s_Device.CreateShaderResourceView(
            texture, (ShaderResourceViewDesc*)null, ref srv));
        texture.Dispose();
        return srv;
    }

    private static ComPtr<ID3D11ShaderResourceView> CreateCheckerTexture(int size)
    {
        uint[] pixels = new uint[size * size];
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
            pixels[y * size + x] = ((x / 8 + y / 8) & 1) == 0 ? 0xFF303860u : 0xFF9098C0u;

        ComPtr<ID3D11Texture2D> texture = default;
        ComPtr<ID3D11ShaderResourceView> srv = default;

        fixed (uint* p = pixels)
        {
            var desc = new Texture2DDesc
            {
                Width = (uint)size, Height = (uint)size, MipLevels = 1, ArraySize = 1,
                Format = Format.FormatB8G8R8A8Unorm,
                SampleDesc = new SampleDesc(1, 0),
                Usage = Usage.Immutable,
                BindFlags = (uint)BindFlag.ShaderResource,
            };
            var data = new SubresourceData { PSysMem = p, SysMemPitch = (uint)(size * 4) };
            SilkMarshal.ThrowHResult(s_Device.CreateTexture2D(in desc, in data, ref texture));
        }

        SilkMarshal.ThrowHResult(s_Device.CreateShaderResourceView(
            texture, (ShaderResourceViewDesc*)null, ref srv));
        texture.Dispose();
        return srv;
    }

    internal static void Shutdown()
    {
        if (!Ready) return;

        s_WhiteTexture.Dispose();
        s_DepthOff.Dispose();
        s_Rasterizer.Dispose();
        s_AlphaBlend.Dispose();
        s_OpaqueBlend.Dispose();
        s_PointSampler.Dispose();
        s_VertexBuffer.Dispose();
        s_ConstantBuffer.Dispose();
        s_InputLayout.Dispose();
        s_PixelShader.Dispose();
        s_VertexShader.Dispose();
        s_DepthView.Dispose();
        s_DepthTexture.Dispose();
        s_SceneSrv.Dispose();
        s_SceneView.Dispose();
        s_SceneTexture.Dispose();
        s_BackBufferView.Dispose();
        s_SwapChain.Dispose();
        s_Context.Dispose();
        s_Device.Dispose();

        D3D11Window.Destroy();
        Ready = false;
    }
}
