// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur** : `Legacy/Raw/Winsys3d/FUN_00a05810.disasm.txt` (SetShift, et
// GetShift a sa suite), `FUN_00a05950`, `FUN_00a05930`, `FUN_00a057b0`.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE DECALAGE D'ECRAN — quatre exports nommes, 2026-09-23
//
// La PlayStation dessine dans un repere de 320 x 240 ; la DLL le pose dans sa surface par un
// decalage et un zoom. Trois quantites, toutes aliasees sur la DLL (`Data.cs`) :
//
//     DecalX/Y      0x011be778/77c   le decalage de vue, en unites PlayStation (SetShift)
//     DecalAbsX/Y   0x011be780/784   le decalage absolu (SetAbsShift), mis a l'echelle du zoom
//     Zoom          0x00a2c498       0..n ; l'echelle vaut (zoom + 9) * 32 / 320
//
// et une quantite derivee, le decalage **en pixels entiers** que toutes les primitives ajoutent :
//
//     DecalEcranX = OrigineX[mode] + DecalAbsX * (zoom + 9) * 32 / 320 + DecalX
//
// ou `/ 320` est une division entiere tronquee vers zero — `imul 66666667h ; sar edx,7`, puis le
// bit de signe ajoute : exactement le `/` du C#. Le produit deborde silencieusement comme en
// 1998 (`imul ecx,esi ; shl ecx,5`, sur 32 bits) : ce fichier n'est pas `checked`.
//
// --------------------------------------------------------------------------------------------
// CE QUI EST TRANSCRIT TEL QUEL
//
// **`SetShift` fait pointer les DEUX tables de conversion sur la meme.** `mov [0A2C4A0h],eax ;
// mov [0A2C4A4h],eax` : X et Y lisent la table du zoom, `0x00a31138 + zoom * 16000`. Ce n'est
// pas le cas de l'ordinal 657, qui pose deux tables differentes pour une surface de plus de 320 ;
// ici l'echelle est la meme sur les deux axes.
//
// **`SetShift` n'appelle le viewport que si son quatrieme argument est nul** — le device `+0x50`,
// origine `OrigineX[mode] + x`, taille `(zoom + 9) * 32` par `(zoom + 9) * 24`. Le repartiteur
// (opcode 0xfb) l'appelle avec 1 : le viewport ne bouge pas en cours d'image.
//
// **`ShiftVertices` arrondit son facteur, pas son produit.** `(zoom + 9) * 32 * (1/320)` est
// range en 32 bits (`fstp dword [esp]`), puis chaque coordonnee vaut `decalage + facteur * x`
// calcule en precision etendue et arrondi une seule fois au `fstp`. Le C# fait ce calcul en
// `double` : le produit de deux flottants 32 bits y est exact, la somme arrondie comme sous le mot
// de controle 53 bits de Windows — le meme resultat.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dDecalageEcran
{
    private const int Device_Viewport = 0x50 / 4;

    /// <summary>Un `D3DTLVERTEX` : huit flottants.</summary>
    private const int FlottantsParSommet = 8;

    /// <summary>
    /// Ghidra: winsys3d.c SetShift @ 0x00a05810 (ordinal 796) — pose le decalage de vue et le
    /// zoom, et recalcule le decalage d'ecran. `stdcall`, `ret 10h`. Un zoom negatif vaut 0 ; le
    /// viewport du device n'est redefini que si <paramref name="garderViewport"/> est nul.
    /// </summary>
    internal static void SetShift(int x, int y, int zoom, int garderViewport)
    {
        if (zoom < 0) zoom = 0;

        int mode = Winsys3dData.ModeAffichage_00a2c49c;
        if (garderViewport == 0)
        {
            ((delegate* unmanaged[Thiscall]<void*, int, int, int, int, int>)
                Winsys3dData.DeviceVtable[Device_Viewport])(
                    Winsys3dData.Device_011be760,
                    Winsys3dData.OrigineX_00a2c4d8[mode] + x,
                    Winsys3dData.OrigineY_00a2c4f0[mode] + y,
                    (zoom + 9) * 0x20,
                    (zoom + 9) * 0x18);
        }

        Winsys3dData.Zoom_00a2c498 = zoom;
        Winsys3dData.DecalY_011be77c = y;
        Winsys3dData.DecalX_011be778 = x;

        float* table = (float*)(Winsys3dData.TablesEchelle_00a31138 + zoom * 16000);
        Winsys3dData.TableEcranX_00a2c4a0 = table;
        Winsys3dData.TableEcranY_00a2c4a4 = table;

        // Le mode est relu apres l'appel au device (`mov ebp,[0A2C49Ch]` a 0x00a0587f).
        mode = Winsys3dData.ModeAffichage_00a2c49c;
        Winsys3dData.DecalEcranX_011be770 = Winsys3dData.OrigineX_00a2c4d8[mode]
            + Winsys3dData.DecalAbsX_011be780 * (zoom + 9) * 0x20 / 0x140 + x;
        Winsys3dData.DecalEcranY_011be774 = Winsys3dData.OrigineY_00a2c4f0[mode]
            + Winsys3dData.DecalAbsY_011be784 * (zoom + 9) * 0x20 / 0x140 + y;
    }

    /// <summary>
    /// Ghidra: winsys3d.c SetAbsShift @ 0x00a05950 (ordinal 791) — pose le decalage absolu et
    /// recalcule le decalage d'ecran avec le zoom et le decalage de vue courants. `ret 8`.
    /// </summary>
    internal static void SetAbsShift(int x, int y)
    {
        Winsys3dData.DecalAbsX_011be780 = x;
        Winsys3dData.DecalAbsY_011be784 = y;

        int zoom = Winsys3dData.Zoom_00a2c498;
        int mode = Winsys3dData.ModeAffichage_00a2c49c;
        Winsys3dData.DecalEcranX_011be770 = x * (zoom + 9) * 0x20 / 0x140
            + Winsys3dData.OrigineX_00a2c4d8[mode] + Winsys3dData.DecalX_011be778;
        Winsys3dData.DecalEcranY_011be774 = y * (zoom + 9) * 0x20 / 0x140
            + Winsys3dData.OrigineY_00a2c4f0[mode] + Winsys3dData.DecalY_011be77c;
    }

    /// <summary>Ghidra: winsys3d.c GetAbsShift @ 0x00a05930 (ordinal 776) — rend le decalage absolu. `ret 8`.</summary>
    internal static void GetAbsShift(int* x, int* y)
    {
        *x = Winsys3dData.DecalAbsX_011be780;
        *y = Winsys3dData.DecalAbsY_011be784;
    }

    /// <summary>
    /// Ghidra: winsys3d.c ShiftVertices @ 0x00a057b0 (ordinal 824) — met a l'echelle du zoom et
    /// decale x et y de <paramref name="nombre"/> `D3DTLVERTEX`. `ret 8`. Voir l'en-tete pour
    /// l'arrondi.
    /// </summary>
    internal static void ShiftVertices(float* sommets, int nombre)
    {
        float facteur = (Winsys3dData.Zoom_00a2c498 + 9) * 0x20 * Winsys3dData.UnSur320_00a2a06c;
        for (; nombre > 0; nombre--)
        {
            sommets[0] = (float)((double)facteur * sommets[0] + Winsys3dData.DecalEcranX_011be770);
            sommets[1] = (float)((double)facteur * sommets[1] + Winsys3dData.DecalEcranY_011be774);
            sommets += FlottantsParSommet;
        }
    }
}
