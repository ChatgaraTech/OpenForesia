using Silk.NET.Core.Native;
using Silk.NET.Direct3D11;
using Silk.NET.DXGI;

namespace OpenForesia.Blaze;

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
/// Ce que le backend doit savoir faire est délibérément étroit : le jeu est un portage
/// PlayStation, où la projection est faite par le GTE côté processeur. Tous les sommets qui
/// arrivent sont donc **déjà transformés** (<c>D3DTLVERTEX</c> : x, y, z, rhw en coordonnées
/// écran). Aucun pipeline de transformation n'est nécessaire, pour les menus comme pour la 3D.
/// </summary>
internal static unsafe class D3D11Backend
{
    /// <summary>
    /// Sonde de faisabilité : crée un device Direct3D 11 et rapporte le niveau de fonctionnalité
    /// obtenu. Sert à valider que Silk.NET fonctionne dans un processus 32 bits avant d'engager la
    /// réécriture. Ne crée ni fenêtre ni swap chain.
    /// </summary>
    internal static bool Probe()
    {
        try
        {
            using var api = D3D11.GetApi(null);

            ComPtr<ID3D11Device> device = default;
            ComPtr<ID3D11DeviceContext> context = default;
            D3DFeatureLevel level = default;

            int hr = api.CreateDevice(
                default(ComPtr<IDXGIAdapter>),
                D3DDriverType.Hardware,
                nint.Zero,
                (uint)CreateDeviceFlag.BgraSupport,
                null,
                0,
                D3D11.SdkVersion,
                ref device,
                ref level,
                ref context);

            if (hr < 0)
            {
                Console.Error.WriteLine($"[D3D11] CreateDevice a échoué : 0x{hr:X8}");
                return false;
            }

            Console.Error.WriteLine($"[D3D11] device créé, feature level 0x{(uint)level:X4}");

            context.Dispose();
            device.Dispose();
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[D3D11] sonde impossible : {ex.GetType().Name}: {ex.Message}");
            return false;
        }
    }
}
