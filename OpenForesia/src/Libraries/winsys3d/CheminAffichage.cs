// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur** (0x00a061b0, 288 octets) — voir « Ce que Ghidra ne donne pas ».
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE CHEMIN D'AFFICHAGE — premiere pierre : `DrawOTag`, l'ordinal 701
//
// C'est le seul `[DllImport]` du perimetre de `Documentation/Etat/CheminAffichage.md`, et c'est
// donc la porte : 157 corps et 12 408 lignes sont derriere lui. Il est porte ici **en premier et
// seul**, avec ses sept appelees encore natives (`Natif.cs`) et ses globales aliasees sur le
// `.data` reel de la DLL (`Data.cs`). Le jeu doit afficher exactement la meme chose qu'avant —
// c'est la seule verification qui compte, et elle est disponible des maintenant.
//
// ============================================================================================
// CE QUE `DrawOTag` FAIT, EN UNE IMAGE
//
//     si la garde est posee               -> ne rien faire du tout
//     remettre les trois memos a -1       l'etat du tampon de commandes redemarre a chaque image
//     basculer l'arene de travail         0 <-> 0x40000, un double tampon
//     si l'image est en retard            -> ne rien dessiner, la rattraper
//     etat de rendu = 8, compteur++
//     PASSE 1 : parcourir l'OT            FUN_00a26b20 sur chaque nœud vivant  (les textures)
//     effacer                             device[+0x5c] ou [+0x60], selon un octet
//     device[+0xb0]()                     BeginScene
//     PASSE 2 : parcourir l'OT            FUN_00a13400 sur chaque nœud vivant  (le rejeu)
//     device[+0xb4]()                     EndScene
//     le rappel de fin d'image, s'il existe
//
// **L'OT est une liste chainee sur 24 bits**, la convention PlayStation que
// `Documentation/DLL/winsys3d.md` documente en tete : le mot d'un nœud porte le lien dans ses
// 24 bits bas et une longueur dans l'octet haut. Un nœud dont l'octet haut est nul est **saute
// sans etre suivi** — mais la marche continue par son lien. La sentinelle est 0xffffff.
//
// **Les deux parcours sont distincts et complets.** Le premier prepare les textures de toute la
// scene avant que le moindre triangle ne parte ; le second rejoue. Les fusionner changerait
// l'ordre des liaisons de texture, donc l'image.
//
// ============================================================================================
// CE QUE GHIDRA NE DONNE PAS, ET QUE LE DESASSEMBLAGE A CORRIGE
//
// **`VSyncDelayed` est `DAT_011be67c`.** Ghidra affiche le nom d'export, ce qui cache que c'est
// une globale a une adresse connue ; le binaire fait `mov eax,[11BE67Ch]`. Les deux sont la meme
// chose — `Platform/Winsys3d.cs` la resout par `GetProcAddress` sous ce nom —, mais ici on la
// prend par son adresse, comme ses voisines.
//
// **`MainMem` est lu, pas ecrit.** `mov ecx,[0B6DBD4h]` puis `lea eax,[eax+ecx+200000h]` : la
// nouvelle arene vaut `MainMem + 0x200000 + bascule`.
//
// **La bascule est un double tampon, pas une addition.** Ghidra ecrit
// `iVar1 = -DAT_00dedbe4 ; DAT_00dedbe4 = iVar1 + 0x40000`, ce qui se lit comme une
// accumulation ; le binaire fait `eax = 0x40000 - [0DEDBE4h]`, et la valeur **alterne entre 0 et
// 0x40000**. Rien d'autre dans la DLL ne l'ecrit. Transcrire l'addition litteralement aurait
// donne la meme chose, mais personne n'aurait vu que c'etait un double tampon.
//
// **L'ordre des deux ecritures d'arene est inverse.** Ghidra pose `DAT_00dedbd8` puis
// `_DAT_00dedbdc` ; le binaire ecrit `[0DEDBDCh]` d'abord. Meme valeur dans les deux, donc sans
// consequence — note pour que la prochaine lecture ne s'y arrete pas.
//
// **`ret 4`** : un seul argument, et la valeur rendue n'est pas definie. La facade le declare
// deja `void`.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dCheminAffichage
{
    /// <summary>Le lien d'un nœud d'OT tient sur 24 bits ; l'octet haut porte autre chose.</summary>
    private const uint LienMasque = 0x00ffffff;

    /// <summary>La fin de l'OT. Ghidra l'ecrit `&amp;DAT_00ffffff`, ce qui est la meme valeur.</summary>
    private const uint LienFin = 0x00ffffff;

    private const int Device_Effacer5c = 0x5c / 4;
    private const int Device_Effacer60 = 0x60 / 4;
    private const int Device_BeginScene = 0xb0 / 4;
    private const int Device_EndScene = 0xb4 / 4;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a061b0 @ 0x00a061b0 (ordinal 701) — `DrawOTag` : dessine une
    /// image entiere a partir de la table d'ordonnancement que le jeu a remplie.
    ///
    /// Les sept appelees sont encore natives (`Natif.cs`) et les globales sont celles de la DLL
    /// (`Data.cs`) : ce corps-ci est donc interchangeable avec l'original, et c'est voulu — il
    /// doit produire la meme image avant qu'on porte la piece suivante,
    /// <c>FUN_00a13400</c>.
    /// </summary>
    internal static void DrawOTag(uint* ot)
    {
        // La garde : non nulle, l'image entiere est sautee. Personne dans le perimetre ne
        // l'ecrit — voir le TODO(REVERSE) de `Data.cs`.
        if (Winsys3dData.Garde_00dedc44 != 0) return;

        // L'etat du tampon de commandes redemarre a chaque image. C'est CE reset qui interdisait
        // de porter `DrawOTag` sans les producteurs tant que les globales etaient dupliquees :
        // ils auraient relu des memos que plus personne ne remettait a -1.
        Winsys3dData.Memo_00a2c484 = -1;
        Winsys3dData.TypePrimitif_00a2c488 = -1;
        Winsys3dData.Memo_00a2c48c = -1;

        // Le double tampon de l'arene de travail : 0 <-> 0x40000.
        int bascule = 0x40000 - Winsys3dData.Bascule_00dedbe4;
        Winsys3dData.Bascule_00dedbe4 = bascule;

        byte* arene = Winsys3dData.MainMem_00b6dbd4 + bascule + 0x200000;
        Winsys3dData.AreneDebut_00dedbdc = arene;
        Winsys3dData.Arene_00dedbd8 = arene;

        // L'image est en retard : tout le dessin est saute pour la rattraper. Le reset des memos
        // et la bascule ci-dessus ont quand meme eu lieu — c'est l'original qui en decide ainsi.
        if (Winsys3dData.VSyncDelayed_011be67c != 0) return;

        Winsys3dData.EtatRendu_00a2c480 = 8;
        Winsys3dData.CompteurImages_011be75c++;

        // ---- PASSE 1 : les textures de toute la scene, avant le moindre triangle ----
        Winsys3dNatif.Fun_00a26490();
        for (uint* noeud = ot; ; )
        {
            if ((byte)(*noeud >> 24) != 0) Winsys3dNatif.Fun_00a26b20(noeud);
            uint lien = *noeud & LienMasque;
            if (lien == LienFin) break;
            noeud = (uint*)lien;
        }
        Winsys3dNatif.Fun_00a027a0();
        Winsys3dNatif.Fun_00a26f20();

        // ---- l'effacement, puis BeginScene ----
        void** vtable = Winsys3dData.DeviceVtable;
        void* device = Winsys3dData.Device_011be760;

        if (Winsys3dData.ModeEffacement_011be6f0 != 0)
            ((delegate* unmanaged[Thiscall]<void*, int, int, void>)vtable[Device_Effacer5c])(device, 0, 0);
        else
            ((delegate* unmanaged[Thiscall]<void*, int, int, void>)vtable[Device_Effacer60])(device, 0, 0);

        // La vtable est relue apres l'effacement : le binaire recharge `[11BE760h]` avant chaque
        // appel, et ce n'est pas une optimisation manquee — l'effacement peut recreer le device.
        vtable = Winsys3dData.DeviceVtable;
        device = Winsys3dData.Device_011be760;
        ((delegate* unmanaged[Thiscall]<void*, void>)vtable[Device_BeginScene])(device);

        // ---- PASSE 2 : le rejeu ----
        for (uint* noeud = ot; ; )
        {
            if ((byte)(*noeud >> 24) != 0) Winsys3dNatif.Fun_00a13400(noeud);
            uint lien = *noeud & LienMasque;
            if (lien == LienFin) break;
            noeud = (uint*)lien;
        }
        Winsys3dNatif.Fun_00a027e0();

        vtable = Winsys3dData.DeviceVtable;
        device = Winsys3dData.Device_011be760;
        ((delegate* unmanaged[Thiscall]<void*, void>)vtable[Device_EndScene])(device);

        Winsys3dNatif.Fun_00a02820();

        // Le rappel de fin d'image, sans argument, s'il a ete pose.
        nint rappel = Winsys3dData.RappelFinImage_011be754;
        if (rappel != 0) ((delegate* unmanaged[Stdcall]<void>)rappel)();
    }
}
