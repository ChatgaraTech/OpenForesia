// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`Legacy/Raw/Winsys3d/FUN_00a063d0`, `FUN_00a069c0`, `FUN_00a06410`).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES REGLAGES ET LA VIE DU DEVICE — ordinaux 798, 749, 790 (2026-09-23)
//
// Ce que la DLL demande au device de `3dhelp` hors du dessin : le filtrage des textures, le mode
// d'affichage, et le rappel a appeler quand le device a ete retabli. Le device est, depuis
// `Host/SyntheticDevice.cs`, un objet C# ; ces corps l'atteignent par sa vtable, en `__thiscall`,
// comme le binaire (`mov ecx,[11BE760h] ; mov edx,[ecx] ; call [edx+N]`).
//
// Ce fichier est aussi la place prevue pour l'initialisation (789), `Setup3DDevice` (823) et
// l'ordinal 750, qui rend le pointeur de device : ils partiront ensemble — voir
// `Documentation/Etat/byebye-dll.md`, « Et c'est pourquoi il ne faut PAS porter l'ordinal 750 ».
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dDevice
{
    /// <summary>La case `+0x90` de la vtable du device : le mode de filtrage des textures (1 ou 2).</summary>
    private const int Device_Filtrage = 0x90 / 4;

    /// <summary>La case `+0xe8` : demande un mode d'affichage `(largeur, hauteur)`, rend non nul s'il est accepte.</summary>
    private const int Device_ChangerMode = 0xe8 / 4;

    /// <summary>La case `+0x4c` : rend un bloc de capacites, dont l'octet `+0x8c` est relu.</summary>
    private const int Device_Capacites = 0x4c / 4;

    /// <summary>La case `+0x94` : reçoit 4 ou 2 selon le bit 3 de cet octet de capacites.</summary>
    private const int Device_Reglage94 = 0x94 / 4;

    /// <summary>
    /// Ghidra: winsys3d.c SetTextureFilterMode @ 0x00a063d0 (ordinal 798) — si le mode change, le
    /// retient et passe au device 2 (filtre) quand il est demande ET que `TexFilterPossible` le
    /// permet, 1 sinon. Un mode inchange ne fait rien. `ret 4`.
    /// </summary>
    internal static void SetTextureFilterMode(int mode)
    {
        if (mode == Winsys3dData.FiltreTexture_00a2c508) return;
        Winsys3dData.FiltreTexture_00a2c508 = mode;
        int valeur = mode != 0 && Winsys3dData.TexFilterPossible_00a2c504 != 0 ? 2 : 1;
        ((delegate* unmanaged[Thiscall]<void*, int, void>)Winsys3dData.DeviceVtable[Device_Filtrage])(
            Winsys3dData.Device_011be760, valeur);
    }

    /// <summary>
    /// Ghidra: winsys3d.c ChangeResolution @ 0x00a069c0 (ordinal 749) — demande au device le mode
    /// d'indice <paramref name="indice"/> (les deux tables de `.data`, 320 x 240 a 1024 x 768). S'il
    /// l'accepte : retient l'indice, puis selon le bit 3 de l'octet `+0x8c` des capacites, regle
    /// `+0x94` a 4, ou a 2 en posant `DAT_00b2d188`. Rend la reponse du device. `ret 4`.
    ///
    /// L'indice est retenu APRES l'appel des capacites et AVANT le test, comme dans le binaire ;
    /// il n'est pas borne.
    /// </summary>
    internal static int ChangeResolution(int indice)
    {
        int accepte = ((delegate* unmanaged[Thiscall]<void*, int, int, int>)Winsys3dData.DeviceVtable[Device_ChangerMode])(
            Winsys3dData.Device_011be760,
            Winsys3dData.LargeursModes_00a2c4a8[indice], Winsys3dData.HauteursModes_00a2c4c0[indice]);
        if (accepte == 0) return accepte;

        byte* capacites = ((delegate* unmanaged[Thiscall]<void*, byte*>)Winsys3dData.DeviceVtable[Device_Capacites])(
            Winsys3dData.Device_011be760);
        Winsys3dData.ModeAffichage_00a2c49c = indice;
        if ((capacites[0x8c] & 8) != 0)
        {
            ((delegate* unmanaged[Thiscall]<void*, int, void>)Winsys3dData.DeviceVtable[Device_Reglage94])(
                Winsys3dData.Device_011be760, 4);
            return accepte;
        }
        ((delegate* unmanaged[Thiscall]<void*, int, void>)Winsys3dData.DeviceVtable[Device_Reglage94])(
            Winsys3dData.Device_011be760, 2);
        Winsys3dData.Genre1Permis_00b2d188 = 1;
        return accepte;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a06410 @ 0x00a06410 (ordinal 790) — range le rappel que
    /// l'initialisation (789) appelle apres avoir retabli le device. `ret 4`.
    /// </summary>
    internal static void SetDeviceLostCallback(void* rappel) => Winsys3dData.RappelDevice_011be798 = rappel;
}
