// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`Legacy/Raw/Winsys3d/FUN_00a063d0`, `FUN_00a069c0`, `FUN_00a06410`).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES REGLAGES ET LA VIE DU DEVICE — ordinaux 798, 749, 790, puis 789 et 750 (2026-09-23)
//
// Ce que la DLL demande au device de `3dhelp` hors du dessin : le filtrage des textures, le mode
// d'affichage, et le rappel a appeler quand le device a ete retabli. Le device est, depuis
// `Host/SyntheticDevice.cs`, un objet C# ; ces corps l'atteignent par sa vtable, en `__thiscall`,
// comme le binaire (`mov ecx,[11BE760h] ; mov edx,[ecx] ; call [edx+N]`).
//
// L'initialisation (789) et l'ordinal 750, qui rend le pointeur de device, sont partis ensemble :
// voir la seconde moitie du fichier. 750 attendait 789, son ecrivain — `Documentation/Etat/
// byebye-dll.md`, « Et c'est pourquoi il ne faut PAS porter l'ordinal 750 ». Reste `Setup3DDevice` (823).
// ============================================================================================

using System;
using OpenForesia.Blaze;

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

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a069b0 @ 0x00a069b0 (ordinal 680) — `FUN_00a06420(p, 2)` : le
    /// repartiteur d'initialisation (789) avec le second argument a 2. Appele par la pompe de
    /// messages sur `WM_QUIT`, avec -2. Role du 2 non etabli, d'ou le nom d'adresse.
    /// </summary>
    internal static void Unknown_00a069b0(int p) => InitialiserDevice(p, 2);

    // ============================================================================================
    // L'INITIALISATION ET L'EXTINCTION — ordinaux 789 et 750 (2026-09-23), transcrits sur le
    // desassemblage (`Legacy/Raw/Winsys3d/FUN_00a06420`, `FUN_00a03110`, `FUN_00a033c0`).
    //
    // **Ghidra a perdu les arguments de presque tous les appels au device** : `[0x18](&fenetre)`,
    // `[0x0c](fenetre)`, `[0x2c](x, x)`, `[0x30](0, largeur, hauteur)`, `[0x04](0 ou 1)`,
    // `[0x00](1)`, `[0x08](0)`, `[0xdc](&caps, 0)`, et le `hwnd` que recoivent le rappel et
    // `FUN_00a03110` (ou il le montrait en `unaff_retaddr`). Tous relus sur les `push`.
    //
    // **`GetProcAddress` passe par la table d'imports de la DLL** (`call [121532Ch]`), et le port
    // aussi : c'est cette entree que `Host/SyntheticDevice.cs` detourne pour substituer son device.
    // Appeler `GetProcAddress` directement contournerait le detournement.
    //
    // Deux codes restent natifs et sont passes par adresse, comme dans l'original : le rappel du
    // minuteur (`LAB_00a06360`) et la procedure de fenetre (`LAB_00a02ee0`).
    // ============================================================================================

    private const int Device_Detruire = 0x00 / 4;
    private const int Device_Reprendre = 0x04 / 4;
    private const int Device_Reglage08 = 0x08 / 4;
    private const int Device_Fenetre = 0x0c / 4;
    private const int Device_Creer = 0x18 / 4;
    private const int Device_Reglage2c = 0x2c / 4;
    private const int Device_OuvrirMode = 0x30 / 4;
    private const int Device_Suspendre = 0x34 / 4;
    private const int Device_Transformation68 = 0x68 / 4;
    private const int Device_Transformation70 = 0x70 / 4;
    private const int Device_ProfondeurBits = 0x28 / 4;
    private const int Device_Valeur24 = 0x24 / 4;
    private const int Device_Valeur20 = 0x20 / 4;
    private const int Device_LireCaps = 0xdc / 4;

    private static void Appeler(int caseVtable)
        => ((delegate* unmanaged[Thiscall]<void*, void>)Winsys3dData.DeviceVtable[caseVtable])(Winsys3dData.Device_011be760);

    private static void Appeler(int caseVtable, int a)
        => ((delegate* unmanaged[Thiscall]<void*, int, void>)Winsys3dData.DeviceVtable[caseVtable])(Winsys3dData.Device_011be760, a);

    private static int Lire(int caseVtable)
        => ((delegate* unmanaged[Thiscall]<void*, int>)Winsys3dData.DeviceVtable[caseVtable])(Winsys3dData.Device_011be760);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a01e30 @ 0x00a01e30 (ordinal 750) — rend le pointeur de device
    /// (`DAT_011be760`). Porte avec 789, qui l'ecrit : avant, son `[DllImport]` etait la fenetre sur
    /// la globale native.
    /// </summary>
    internal static void* LireDevice() => Winsys3dData.Device_011be760;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a06930 @ 0x00a06930 — `__try { pand mm0, mm1 ; emms }` : rend 1, ou 0
    /// si l'instruction MMX leve une exception. Le C# rend 1 : le runtime .NET x86 exige lui-meme
    /// SSE2, qui suppose MMX — aucune machine qui le fait tourner ne prendrait l'autre branche.
    /// </summary>
    internal static int DetecterMmx() => 1;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a06420 @ 0x00a06420 (ordinal 789) — le repartiteur d'initialisation et
    /// d'extinction du device. Range toujours <paramref name="mode"/> comme mode d'affichage. `ret 8`.
    ///
    /// <list type="bullet">
    /// <item><paramref name="action"/> -1 ou -2 : arrete le minuteur, rend sa procedure a la fenetre
    /// et detruit le cache ; en Glide hors -2, `device[4](1)` et c'est tout. Sinon `device[0x34]()`,
    /// et pour -2 seulement : detruit la fenetre, le device (`[0](1)`), decharge la DLL du device.</item>
    /// <item>autre valeur non nulle : rien.</item>
    /// <item>0, en Glide avec un device et une fenetre deja la : relance le minuteur, rappelle le jeu,
    /// reprepare le device (`FUN_00a03110`) et `device[4](0)`.</item>
    /// <item>0 sinon : l'initialisation complete — MMX, arenes, minuteur, chargement du device si
    /// besoin, ouverture du mode, matrices, projection, tables UV, preparation, puis la fin commune.</item>
    /// </list>
    /// Rend 1. Un mode refuse par le device affiche « 3D-Graphics initialisation error » et quitte (3).
    /// </summary>
    internal static int InitialiserDevice(int action, int mode)
    {
        Winsys3dData.ModeAffichage_00a2c49c = mode;
        if (action != 0)
        {
            if (action == -1 || action == -2)
            {
                Win32.timeKillEvent(Winsys3dData.Minuteur_011be6d0);
                RestaurerFenetre();
                if (Winsys3dData.Glide_011be738 != 0 && action != -2)
                {
                    Appeler(Device_Reprendre, 1);
                    return 1;
                }
                Appeler(Device_Suspendre);
                if (action == -2)
                {
                    Win32.DestroyWindow(Winsys3dData.Fenetre_011be79c);
                    Winsys3dData.Fenetre_011be79c = IntPtr.Zero;
                    if (Winsys3dData.Device_011be760 != null) Appeler(Device_Detruire, 1);
                    Winsys3dData.Device_011be760 = null;
                    Win32.FreeLibrary(Winsys3dData.ModuleDevice_011be6cc);
                    Winsys3dData.ModuleDevice_011be6cc = IntPtr.Zero;
                }
            }
            return 1;
        }

        long frequence = 0;
        if (Winsys3dData.Glide_011be738 != 0 && Winsys3dData.Device_011be760 != null
            && Winsys3dData.Fenetre_011be79c != IntPtr.Zero)
        {
            Winsys3dData.Minuteur_011be6d0 = Win32.timeSetEvent(0x11, 0, Winsys3dData.RappelMinuteur_00a06360, 0, 0);
            AppelerRappel();
            PreparerDevice(Winsys3dData.Fenetre_011be79c);
            Appeler(Device_Reprendre, 0);
            return 1;
        }

        Winsys3dData.MmxInstalle_00dedbe8 = DetecterMmx();
        byte* arene = Winsys3dData.MainMem_00b6dbd4 + 0x200000;
        Winsys3dData.AreneFin_00dedbe0 = arene;
        Winsys3dData.AreneDebut_00dedbdc = arene;
        Winsys3dData.Arene_00dedbd8 = arene;
        Winsys3dData.Minuteur_011be6d0 = Win32.timeSetEvent(0x11, 0, Winsys3dData.RappelMinuteur_00a06360, 0, 0);

        if (Winsys3dData.Device_011be760 == null)
        {
            byte* nom = Winsys3dData.Glide_011be738 == 0 ? Winsys3dData.Chaine3dhelp_00a2c54c : Winsys3dData.ChaineGldhelp_00a2c540;
            Winsys3dData.ModuleDevice_011be6cc = Win32.LoadLibraryA(nom);
            var getProcAddress = (delegate* unmanaged[Stdcall]<IntPtr, byte*, void*>)*Winsys3dData.ImportGetProcAddress_0121532c;
            Winsys3dData.FabriqueDevice_011be7a0 = getProcAddress(Winsys3dData.ModuleDevice_011be6cc, Winsys3dData.ChaineCreate3DDevice_00a2c530);
            void* device = ((delegate* unmanaged[Stdcall]<void*>)Winsys3dData.FabriqueDevice_011be7a0)();
            Winsys3dData.Device_011be760 = device;
            ((delegate* unmanaged[Thiscall]<void*, IntPtr*, void>)(*(void***)device)[Device_Creer])(
                device, Winsys3dData.AdresseFenetre_011be79c);
            Winsys3dData.EvenementWin32_011be794 = Win32.CreateEventA(null, 0, 0, null);
        }

        ((delegate* unmanaged[Thiscall]<void*, IntPtr, void>)Winsys3dData.DeviceVtable[Device_Fenetre])(
            Winsys3dData.Device_011be760, Winsys3dData.Fenetre_011be79c);
        int reglage = Winsys3dData.Unknown_011be73c;
        ((delegate* unmanaged[Thiscall]<void*, int, int, void>)Winsys3dData.DeviceVtable[Device_Reglage2c])(
            Winsys3dData.Device_011be760, reglage, reglage);
        int ouvert = ((delegate* unmanaged[Thiscall]<void*, int, int, int, int>)Winsys3dData.DeviceVtable[Device_OuvrirMode])(
            Winsys3dData.Device_011be760, 0,
            Winsys3dData.LargeursModes_00a2c4a8[mode], Winsys3dData.HauteursModes_00a2c4c0[mode]);

        if (ouvert != 0)
        {
            // La vtable est lue AVANT chaque `ScaleMatrix` (`mov ebp,[eax]`), le device relu apres.
            float* matrice = stackalloc float[16];
            void** vtable = Winsys3dData.DeviceVtable;
            float* echelle = WsyskernMatrices.ScaleMatrix(matrice, 1.0f, -1.0f, 1.0f);
            ((delegate* unmanaged[Thiscall]<void*, float*, void>)vtable[Device_Transformation70])(Winsys3dData.Device_011be760, echelle);
            vtable = Winsys3dData.DeviceVtable;
            echelle = WsyskernMatrices.ScaleMatrix(matrice, 0.5f, 0.5f, 0.5f);
            ((delegate* unmanaged[Thiscall]<void*, float*, void>)vtable[Device_Transformation68])(Winsys3dData.Device_011be760, echelle);

            Winsys3dMatrices.MettreAJourProjection(1000);
            AppelerRappel();

            float* c = Winsys3dData.ConstantesUv_00a2a070;
            RemplirTablesUv((float*)Winsys3dData.TablesUvSpriteDefaut_011c4cd0, c[1], c[3], c[5], c[7]);
            RetoucherTablesUvSprite((uint*)Winsys3dData.TablesUvSpriteDefaut_011c4cd0);
            RemplirTablesUv((float*)Winsys3dData.TablesUvPolyDefaut_011be868, c[9], c[10], c[11], c[12]);

            PreparerDevice(Winsys3dData.Fenetre_011be79c);
            if (Winsys3dData.Unknown_011be73c != 0)
            {
                Winsys3dData.TexFilterPossible_00a2c504 = 0;
                SetTextureFilterMode(0);
            }
        }
        else
        {
            Win32.MessageBoxA(Winsys3dData.Fenetre_011be79c, Winsys3dData.ChaineErreurInit_00a2c50c, null, 0);
            Win32.ExitProcess(3);
        }

        // La fin commune (0x00a067c2) — l'echec y retombe aussi, apres un `ExitProcess` qui ne rend pas.
        Winsys3dTrigonometrie.InitTrigTables();           // FUN_00a1df00 (ordinal 337)
        Winsys3dData.DispLargeur_011be6bc = 0x140;
        Winsys3dData.DispHauteur_011be6be = 0xf0;
        Winsys3dData.DispEntrelace_011be6c8 = 0;
        Winsys3dTexteEcran.FntOpen(0, 0, 0x140, 0xf0, 1, 0x50);

        int glide = Winsys3dData.Glide_011be738;
        byte* immediats = (byte*)Winsys3dData.SommetsImmediats_011cb138;
        byte* textures = (byte*)Winsys3dData.SommetsTextures_011be7e8;
        for (int o = 0; o < 0x80; o += 0x20)
        {
            uint z = glide == 0 ? 0x38d1b717u : 0x3f800000u;   // 1e-4 ou 1.0
            *(uint*)(immediats + 0xc + o) = z;
            *(uint*)(textures + 0xc + o) = z;
            *(uint*)(immediats + 0x8 + o) = z;
            *(uint*)(textures + 0x8 + o) = z;
            *(uint*)(textures + 0x10 + o) = 0x7f7f7f;
        }

        Win32.QueryPerformanceFrequency(&frequence);
        Winsys3dData.TopsParImage_011be684 = (int)((frequence + 0x1e) / 0x3c);
        return 1;
    }

    /// <summary>`if (DAT_011be798) DAT_011be798(fenetre)` — le rappel du jeu, `stdcall` a un argument (pas de `add esp` apres).</summary>
    private static void AppelerRappel()
    {
        void* rappel = Winsys3dData.RappelDevice_011be798;
        if (rappel != null)
            ((delegate* unmanaged[Stdcall]<IntPtr, void>)rappel)(Winsys3dData.Fenetre_011be79c);
    }

    /// <summary>
    /// Les deux boucles x87 de 789 (0x00a06605 et 0x00a066f1) : 25 tables de 0x101 `float` a la
    /// suite de <paramref name="b"/>. La table 0 vaut `o0 - i . k0` ; pour `k` de 1 a 8, avec
    /// `v = i - 0x20 (k - 1)`, les tables `k`, `8 + k` et `16 + k` valent `o1 - v . k1`,
    /// `o2 - v . k2` et `o3 - v . k3`. Ce sont les tables 32, 64 et 128 que
    /// `ChargementTextures.TableUv` choisit (`+ 0x11`, `+ 9`, `+ 1`).
    ///
    /// Produit et difference se font sur la pile x87 puis `fstp dword` : le C# calcule en `double`
    /// et arrondit une fois en `float`, ce que fait le 387 en precision double (le reglage des
    /// processus Windows 32 bits). Voir l'ecart assume de `wsyskern/Matrices.cs`.
    /// </summary>
    private static void RemplirTablesUv(float* b, float o0, float o1, float o2, float o3)
    {
        float* c = Winsys3dData.ConstantesUv_00a2a070;
        for (int i = 0; i <= 0x100; i++)
        {
            b[i] = (float)(o0 - (double)i * c[0]);
            int v = i;
            for (int k = 1; k <= 8; k++)
            {
                b[k * 0x101 + i] = (float)(o1 - (double)v * c[2]);
                b[(8 + k) * 0x101 + i] = (float)(o2 - (double)v * c[4]);
                b[(16 + k) * 0x101 + i] = (float)(o3 - (double)v * c[6]);
                v -= 0x20;
            }
        }
    }

    /// <summary>
    /// La retouche d'entre les deux boucles (0x00a06699) : la derniere entree de la table 0 des
    /// sprites, puis, de page en page (`+ 0x121` mots : une table et 32 entrees), la derniere entree
    /// utile des tables 128 (tant que `pas` &lt; 0x1694), 64 (tant que &lt; 0x1f9c) et 32 — un peu
    /// sous 1.0 (0.9996, 0.9992, 0.9984, 0.9969), pour ne pas deborder de la page.
    /// </summary>
    private static void RetoucherTablesUvSprite(uint* b)
    {
        b[0x100] = 0x3f7fe666;
        uint* p = b + (0x011c71f4 - 0x011c4cd0) / 4;
        for (int pas = 0; pas < 0x2420; pas += 0x484, p += 0x121)
        {
            if (pas < 0x1694) p[-0x7c8] = 0x3f7fcccd;
            if (pas < 0x1f9c) p[0] = 0x3f7f999a;
            p[0x7e8] = 0x3f7f3333;
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03110 @ 0x00a03110 — prepare le device pour <paramref name="fenetre"/>
    /// (argument que Ghidra montre en `unaff_retaddr`). `ret 4`.
    ///
    /// La table des 24 x 4 000 profondeurs ; `device[8](0)` ; le cache de textures (`Malloc(0x21020)`
    /// puis sa construction) ; la procedure de fenetre de la DLL a la place de celle du jeu ; les
    /// tables de couleurs si le gamma n'a jamais ete pose. Puis, hors Glide, les capacites du device
    /// — un `DDCAPS` de 0x17c octets, la profondeur de couleur, deux valeurs `+0x24`/`+0x20` et la
    /// memoire video, les 14 mots de capacites de rendu — d'ou les quatre modes de melange
    /// disponibles et trois drapeaux ; en Glide, des valeurs fixes.
    ///
    /// Ecart assume : seul `dwSize` du `DDCAPS` est ecrit par l'original, le reste est ce que la pile
    /// contenait ; le C# le met a zero. Le device le remplit de toute facon.
    /// </summary>
    internal static void PreparerDevice(IntPtr fenetre)
    {
        Win32.GetCommandLineA();

        float* t = Winsys3dData.TableProfondeurs_00a2f478;
        float facteur = Winsys3dData.FacteurProfondeurs_00a2a024;
        int pas = 0x120;
        int depart = -0x81600;
        do
        {
            int v = depart;
            for (int n = 0; n < 4000; n++)
            {
                *t++ = (float)((double)v * facteur);
                v += pas;
            }
            depart -= 0xe600;
            pas += 0x20;
        } while (depart > -0x1da600);

        Appeler(Device_Reglage08, 0);
        void* bloc = Blaze.Wsyskern.Malloc(0x21020);
        Winsys3dData.Objet_012141d4 = bloc == null ? null : Winsys3dCacheTextures.ConstruireCache(bloc);

        Winsys3dData.FenetreSousClassee_00a2f470 = fenetre;
        Winsys3dData.ProcedureOrigine_00b6d194 = Win32.SetWindowLongA(fenetre, -4, Winsys3dData.ProcedureFenetre_00a02ee0);

        // `fcomp` puis le bit C3 : un NaN compte pour « egal ».
        float gamma = Winsys3dData.GammaCourant_00a2c228, initial = Winsys3dData.GammaInitial_00a2a028;
        if (gamma == initial || float.IsNaN(gamma) || float.IsNaN(initial))
            Winsys3dChargementTextures.CreateTranslationTables(1.0f);

        uint* copie = Winsys3dData.CopieCapacites_00a2f438;
        byte* melanges = Winsys3dData.MelangesDisponibles_00a2f430;
        if (Winsys3dData.Glide_011be738 != 0)
        {
            for (int i = 0; i < 14; i++) copie[i] = 0;
            Winsys3dData.ProfondeurCouleur_00b2d07c = 0x10;
            melanges[3] = 1;
            melanges[2] = 1;
            melanges[1] = 1;
            melanges[0] = 1;
            Winsys3dData.CapacitesMelange_00a2f454 = 0x5000;
            Winsys3dData.TexturesCarrees_00b2d180 = 0;
            return;
        }

        byte* caps = stackalloc byte[0x17c];
        *(int*)caps = 0x17c;
        ((delegate* unmanaged[Thiscall]<void*, byte*, int, void>)Winsys3dData.DeviceVtable[Device_LireCaps])(
            Winsys3dData.Device_011be760, caps, 0);
        Winsys3dData.ProfondeurCouleur_00b2d07c = Lire(Device_ProfondeurBits);
        int a = Lire(Device_Valeur24);
        int b = Lire(Device_Valeur20);
        if (b <= 0x10 && a > 0x10 && *(uint*)(caps + 0x3c) < 0x700000) a = 0;   // dwVidMemTotal

        uint* source = (uint*)(((delegate* unmanaged[Thiscall]<void*, byte*>)Winsys3dData.DeviceVtable[Device_Capacites])(
            Winsys3dData.Device_011be760) + 0x64);
        for (int i = 0; i < 14; i++) copie[i] = source[i];

        Winsys3dData.TexturesCarrees_00b2d180 = (int)(copie[8] & 0x20);        // 0x00a2f458
        uint c448 = copie[4];                                                   // 0x00a2f448
        byte c44c = *(byte*)(copie + 5);                                        // 0x00a2f44c, octet bas
        uint bit10 = c448 & 0x10;
        melanges[0] = (byte)(bit10 != 0 && (c44c & 0x20) != 0 ? 1 : 0);
        melanges[2] = (byte)(bit10 != 0 && (c44c & 0x20) != 0 && a != 0 ? 1 : 0);
        melanges[1] = (byte)((c448 & 2) != 0 && (c44c & 2) != 0 ? 1 : 0);
        melanges[3] = (byte)(bit10 != 0 && (c44c & 2) != 0 ? 1 : 0);

        Winsys3dPrimitives.ChoisirMelange(0);
        if ((Winsys3dData.CapacitesMelange_00a2f454 & 0xc000) == 0) Winsys3dData.Unknown_00b2d184 = 1;

        if ((*(byte*)(copie + 10) & 8) != 0)                                    // 0x00a2f460
            Appeler(Device_Reglage94, 4);
        else
        {
            Appeler(Device_Reglage94, 2);
            Winsys3dData.Genre1Permis_00b2d188 = 1;
        }

        int f = Winsys3dData.CapacitesMelange_00a2f454;
        if ((f & 0x1000) == 0 && (f & 0x2000) != 0)
        {
            Winsys3dData.AlphaParMode_00a2c5a8[1] = 0x5f;
            return;
        }
        if (melanges[0] != 0 && melanges[1] == 0)
            Winsys3dData.AlphaParMode_00a2c5a8[1] = 0x5f;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a033c0 @ 0x00a033c0 — rend a la fenetre sa procedure d'origine (si
    /// `FUN_00a03110` l'a remplacee), puis detruit et libere le cache de textures.
    /// </summary>
    internal static void RestaurerFenetre()
    {
        if (Winsys3dData.ProcedureOrigine_00b6d194 != IntPtr.Zero)
            Win32.SetWindowLongA(Winsys3dData.FenetreSousClassee_00a2f470, -4, Winsys3dData.ProcedureOrigine_00b6d194);
        void* cache = Winsys3dData.Objet_012141d4;
        if (cache != null)
        {
            Winsys3dCacheTextures.DetruireCache(cache);
            Blaze.Wsyskern.WsysFree((byte*)cache);
        }
        Winsys3dData.Objet_012141d4 = null;
    }

    // ============================================================================================
    // LE DIALOGUE DE CHOIX DU PILOTE — ordinal 823, `Setup3DDevice` (2026-09-23), transcrit sur le
    // desassemblage (`Legacy/Raw/Winsys3d/FUN_00a01910`, `FUN_00a010d0`, `FUN_00a017d0`,
    // `FUN_00a279d0`).
    //
    // **Les deux corps passent l'adresse d'un bloc de LEUR PILE a du code natif**, qui ecrit dedans :
    // la procedure du dialogue (`FUN_00a012f0`) et le rappel d'enumeration des pilotes
    // (`LAB_00a01810`). La disposition est donc reproduite a l'octet pres, relevee sur les
    // `[esp+N]` : chaque corps prend un seul `stackalloc` de la taille de son cadre, et `pile[k]`
    // y designe `[S + 0x10 + k]`, `S` etant `esp` apres les quatre `push` du prologue.
    //
    // Restent natifs, passes par adresse : la procedure du dialogue et les deux rappels
    // d'enumeration (`LAB_00a01810`, `LAB_00a01000`). Les deux fonctions DirectDraw sont des thunks
    // `jmp [IAT]` : elles appellent a travers la table d'imports de la DLL, comme le binaire.
    // ============================================================================================

    /// <summary>Ghidra: winsys3d.c DirectDrawEnumerateA @ 0x00a279d0 — `jmp [1215244h]` : le thunk d'import.</summary>
    internal static int DirectDrawEnumerateA(IntPtr rappel, void* contexte)
        => ((delegate* unmanaged[Stdcall]<IntPtr, void*, int>)*Winsys3dData.ImportDirectDrawEnumerateA_01215244)(rappel, contexte);

    /// <summary>Ghidra: winsys3d.c DirectDrawCreate @ 0x00a279d6 — `jmp [1215248h]` : le thunk d'import.</summary>
    internal static int DirectDrawCreate(void* guid, void** objet, void* exterieur)
        => ((delegate* unmanaged[Stdcall]<void*, void**, void*, int>)*Winsys3dData.ImportDirectDrawCreate_01215248)(guid, objet, exterieur);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a017d0 @ 0x00a017d0 — cree l'objet DirectDraw du pilote par defaut,
    /// met `*compte` a 0, enumere ses modes d'affichage par le rappel natif `LAB_00a01000`
    /// (`EnumDisplayModes(0, NULL, compte, rappel)`, case `+0x20`), puis le libere (`Release`, `+8`).
    /// Le premier argument n'est pas lu. Le retour de `DirectDrawCreate` n'est pas teste. `ret 8`.
    /// </summary>
    internal static void EnumererModes(int inutilise, int* compte)
    {
        void* dd;
        DirectDrawCreate(null, &dd, null);
        void** vtable = *(void***)dd;
        *compte = 0;
        ((delegate* unmanaged[Stdcall]<void*, uint, void*, int*, IntPtr, int>)vtable[0x20 / 4])(
            dd, 0, null, compte, Winsys3dData.RappelModes_00a01000);
        ((delegate* unmanaged[Stdcall]<void*, uint>)(*(void***)dd)[8 / 4])(dd);
    }

    /// <summary>Le `strlen` + `rep movsd` + `rep movsb` du compilateur : copie jusqu'au NUL compris, sans borne.</summary>
    private static void CopierChaine(byte* destination, byte* source)
    {
        int i = 0;
        do destination[i] = source[i]; while (source[i++] != 0);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a010d0 @ 0x00a010d0 — dresse la liste des pilotes et montre le
    /// dialogue 0x66. `ret 4`.
    ///
    /// Le bloc des pilotes, sur la pile a `S+0x14`, est ce que recoivent le rappel d'enumeration et
    /// le dialogue : un compte, 20 noms, 20 descriptions, 20 mots (`-1` pour Glide), 20 drapeaux,
    /// le pointeur <paramref name="parametres"/>, puis 37 mots pour `grSstQueryBoards`. Entree 0 :
    /// « Software » / « Direct3D Software Emulation », avec les modes du pilote par defaut ; puis
    /// les pilotes DirectDraw ; puis Glide si `GLIDE2X.DLL` compte au moins une carte.
    ///
    /// Au retour : un dialogue en echec (-1) detruit la bitmap et la palette et quitte (0). Un compte
    /// remis a 0 donne « display » / « unknown » et rend 0. Sinon le pilote choisi est copie dans
    /// <paramref name="parametres"/> (`+4` le nom, `+0x18` la description) — **sans borne**, voir
    /// LB-048 —, les chaines sont liberees, et le corps rend 1.
    /// </summary>
    internal static int ListerPilotes(byte* parametres)
    {
        byte* pile = stackalloc byte[0x1e0];            // [S + 0x10] .. [S + 0x1f0]
        int* compte = (int*)(pile + 4);                  // S+0x14
        void** noms = (void**)(pile + 8);                // S+0x18
        void** descriptions = (void**)(pile + 0x58);     // S+0x68
        int* guides = (int*)(pile + 0xa8);               // S+0xb8
        int* drapeaux = (int*)(pile + 0xf8);             // S+0x108
        byte** appel = (byte**)(pile + 0x148);           // S+0x158
        int* cartes = (int*)(pile + 0x14c);              // S+0x15c

        noms[0] = Tableaux.Strdup(Winsys3dData.Chaine(0x00a2c0b8));            // « Software »
        descriptions[0] = Tableaux.Strdup(Winsys3dData.Chaine(0x00a2c09c));    // « Direct3D Software Emulation »
        guides[0] = 0;
        drapeaux[0] = 0;
        EnumererModes(0, drapeaux);
        *compte = 1;
        DirectDrawEnumerateA(Winsys3dData.RappelPilotes_00a01810, compte);

        IntPtr glide = Win32.LoadLibraryA(Winsys3dData.Chaine(0x00a2c090));    // « GLIDE2X.DLL »
        if (glide != IntPtr.Zero)
        {
            var getProcAddress = (delegate* unmanaged[Stdcall]<IntPtr, byte*, void*>)*Winsys3dData.ImportGetProcAddress_0121532c;
            void* interroger = getProcAddress(glide, Winsys3dData.Chaine(0x00a2c07c));   // « _grSstQueryBoards@4 »
            ((delegate* unmanaged[Stdcall]<int*, int>)interroger)(cartes);
            Win32.FreeLibrary(glide);
            if (cartes[0] != 0)
            {
                noms[*compte] = Tableaux.Strdup(Winsys3dData.Chaine(0x00a2c074));          // « Glide »
                descriptions[*compte] = Tableaux.Strdup(Winsys3dData.Chaine(0x00a2c068));  // « 3Dfx Glide »
                guides[*compte] = -1;
                drapeaux[*compte] = 4;
                *compte = *compte + 1;
            }
        }

        Winsys3dData.ParametresDialogue_00a2e8e0 = parametres;
        *appel = parametres;
        nint choix = Win32.DialogBoxParamA(Winsys3dData.InstanceDll_00a2e8e8, (byte*)0x66,
            *(IntPtr*)parametres, Winsys3dData.ProcedureDialogue_00a012f0, (nint)compte);
        if (choix == -1)
        {
            Win32.DeleteObject(*(IntPtr*)(parametres + 0x11c));
            Win32.DeleteObject(*(IntPtr*)(parametres + 0x120));
            Win32.ExitProcess(0);
        }

        if (*compte == 0)
        {
            CopierChaine(parametres + 4, Winsys3dData.Chaine(0x00a2c060));       // « display »
            CopierChaine(parametres + 0x18, Winsys3dData.Chaine(0x00a2c058));    // « unknown »
            return 0;
        }

        CopierChaine(parametres + 4, (byte*)noms[choix]);
        CopierChaine(parametres + 0x18, (byte*)descriptions[choix]);
        for (int i = 0; i < *compte; i++)
        {
            Blaze.Wsyskern.WsysFree((byte*)noms[i]);
            Blaze.Wsyskern.WsysFree((byte*)descriptions[i]);
        }
        return 1;
    }

    /// <summary>
    /// Ghidra: winsys3d.c Setup3DDevice @ 0x00a01910 (ordinal 823) — le lanceur : lit le pilote et la
    /// resolution dans le fichier de configuration a cote de l'executable (`Blaze.cfg`), montre le
    /// dialogue de choix s'il y a une bitmap, ecrit ce qui a change, et pose les deux drapeaux
    /// `Glide` (pilote « GLIDE ») et `DAT_011be73c` (pilote « SOFTWARE ») que l'initialisation
    /// (789) relit. `ret 0Ch`.
    ///
    /// Le bloc passe au dialogue commence a `S+0x10` : fenetre (0), nom du pilote (`+4`, **vingt
    /// octets seulement**), description (`+0x18`), resolution (`+0x118`), bitmap, palette et chaines
    /// (`+0x11c`, `+0x120`, `+0x124`). Suivent le chemin du fichier (`S+0x138`) et un tampon (`S+0x238`).
    ///
    /// `wsprintfA(tampon, "%d", n)` est rendu par le formatage decimal de .NET, identique pour un
    /// entier. `_strrchr` et `_strcmpi` sont ceux de la CRT liee dans la DLL, sur des chaines ASCII.
    /// </summary>
    internal static void Setup3DDevice(IntPtr bitmap, IntPtr palette, byte** chaines)
    {
        byte* pile = stackalloc byte[0x328];             // [S + 0x10] .. [S + 0x338]
        byte* bloc = pile;                                // S+0x10, le bloc du dialogue
        byte* nom = pile + 4;                             // S+0x14
        byte* description = pile + 0x18;                  // S+0x28
        int* resolution = (int*)(pile + 0x118);           // S+0x128
        byte* fichier = pile + 0x128;                     // S+0x138
        byte* tampon = pile + 0x228;                      // S+0x238

        Win32.GetModuleFileNameA(IntPtr.Zero, fichier, 0x100);
        byte* point = null;                               // _strrchr(fichier, '.')
        for (byte* p = fichier; ; p++)
        {
            if (*p == (byte)'.') point = p;
            if (*p == 0) break;
        }
        if (point != null) *point = 0;
        Win32.lstrcatA(fichier, Winsys3dData.Chaine(0x00a2c13c));                 // « .cfg »

        *(IntPtr*)(bloc + 0x120) = palette;
        *(int*)bloc = 0;
        *(IntPtr*)(bloc + 0x11c) = bitmap;
        *(byte***)(bloc + 0x124) = chaines;

        byte* sectionPilote = Winsys3dData.Chaine(0x00a2c124);                   // « D3DDriver »
        byte* sectionOptions = Winsys3dData.Chaine(0x00a2c104);                  // « Options »
        byte* cleResolution = Winsys3dData.Chaine(0x00a2c10c);                   // « Resolution »
        Win32.GetPrivateProfileStringA(sectionPilote, Winsys3dData.Chaine(0x00a2c130), Winsys3dData.Chaine(0x00a2c138),
            tampon, 0x100, fichier);                                             // « Driver », défaut « !! »
        CopierChaine(nom, tampon);
        Win32.GetPrivateProfileStringA(sectionPilote, Winsys3dData.Chaine(0x00a2c118), Winsys3dData.Chaine(0x00a2c138),
            tampon, 0x100, fichier);                                             // « Description »
        CopierChaine(description, tampon);
        int lue = (int)Win32.GetPrivateProfileIntA(sectionOptions, cleResolution, 2, fichier);
        *resolution = lue;

        if (bitmap != IntPtr.Zero)
        {
            if (ListerPilotes(bloc) == -1)
            {
                Win32.DeleteObject(bitmap);
                Win32.DeleteObject(palette);
                Win32.ExitProcess(1);
            }
            if (*resolution != lue)
            {
                EcrireEntier(tampon, *resolution);
                Win32.WritePrivateProfileStringA(sectionOptions, cleResolution, tampon, fichier);
                EcrireEntier(tampon, Winsys3dData.TaillesParResolution_00a2c018[*resolution]);
                Win32.WritePrivateProfileStringA(sectionOptions, Winsys3dData.Chaine(0x00a2c0f8), tampon, fichier);   // « Size »
                byte* petit = *resolution < 2 ? Winsys3dData.Chaine(0x00a2c0f4) : Winsys3dData.Chaine(0x00a2c0f0);   // « 1 » / « 0 »
                Win32.WritePrivateProfileStringA(sectionOptions, Winsys3dData.Chaine(0x00a2c0e8), petit, fichier);   // « Small »
            }
        }

        Winsys3dData.Glide_011be738 = ComparerSansCasse(nom, Winsys3dData.Chaine(0x00a2c0e0)) == 0 ? 1 : 0;      // « GLIDE »
        Winsys3dData.Unknown_011be73c = ComparerSansCasse(nom, Winsys3dData.Chaine(0x00a2c0d4)) == 0 ? 1 : 0;    // « SOFTWARE »
        Win32.WritePrivateProfileStringA(sectionPilote, Winsys3dData.Chaine(0x00a2c130), nom, fichier);
        Win32.WritePrivateProfileStringA(sectionPilote, Winsys3dData.Chaine(0x00a2c118), description, fichier);
    }

    /// <summary>`wsprintfA(tampon, "%d", n)`.</summary>
    private static void EcrireEntier(byte* tampon, int n)
    {
        string texte = n.ToString(System.Globalization.CultureInfo.InvariantCulture);
        for (int i = 0; i < texte.Length; i++) tampon[i] = (byte)texte[i];
        tampon[texte.Length] = 0;
    }

    /// <summary>`_strcmpi` de la CRT (locale « C ») : compare en ramenant les lettres ASCII en minuscules.</summary>
    private static int ComparerSansCasse(byte* a, byte* b)
    {
        for (int i = 0; ; i++)
        {
            int ca = a[i], cb = b[i];
            if (ca >= 'A' && ca <= 'Z') ca += 0x20;
            if (cb >= 'A' && cb <= 'Z') cb += 0x20;
            if (ca != cb || ca == 0) return ca - cb;
        }
    }
}
