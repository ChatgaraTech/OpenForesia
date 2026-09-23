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
        OpenForesia.Blaze.Winsys3d.InitTrigTables();     // FUN_00a1df00, reste natif (ordinal 337)
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
}
