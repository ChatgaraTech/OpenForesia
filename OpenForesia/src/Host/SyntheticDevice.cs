// =====================================================================
// Device 3D synthétique — remplace entièrement celui de `3dhelp.dll`.
//
// Pourquoi. `3dhelp` prend l'affichage en mode exclusif et bascule le mode du bureau dans
// `Create3DDevice`, ce qui fait clignoter l'écran entre l'écran-titre et le menu. Trois tentatives
// pour l'en empêcher tout en le laissant s'initialiser ont échoué (voir `Documentation/DLL/3dhelp.md`) :
// sa création de device **exige** une vraie bascule en mode exclusif. La seule voie restante est de
// ne plus le faire initialiser du tout.
//
// Comment. `winsys3d` charge `3dhelp` **dynamiquement** — `LoadLibraryA` puis
// `GetProcAddress("Create3DDevice")`, `winsys3d.c:5854-5855` — et non par table d'imports. On
// détourne donc `GetProcAddress` dans la table d'imports de `winsys3d` (fonction Win32 ordinaire,
// aucun objet COM) et on rend notre propre fabrique de device.
//
// Ce que le device doit encaisser, relevé dans `winsys3d.c:5856-5864` :
//
//     DAT_011be760 = Create3DDevice()
//     device->vtable[0x18]()                    crée la fenêtre chez l'original
//     device->vtable[0x0C]() ; [0x2C]()
//     iVar4 = device->vtable[0x30](0, largeur, ...)
//     si iVar4 == 0  ->  « 3D Graphics initialisation error » + ExitProcess(3)
//     puis [0x70] et [0x68] avec des matrices, et la suite de l'init
//
// **`+0x30` doit rendre non nul.** C'est le slot qui, chez l'original, prend l'affichage.
//
// L'arité, le point critique. `stdcall` nettoie côté appelé : chaque slot doit retirer de la pile
// exactement autant d'octets que l'original, sous peine de la corrompre — un bug qui ne se
// diagnostique pas. La table ci-dessous est extraite du binaire (`ret N` de chaque fonction de la
// vtable `0x0FFE6000`) et documentée dans `Documentation/DLL/3dhelp.md`. Elle concorde avec les
// huit slots que `DeviceShim` déclarait déjà. **Ne pas se fier à Ghidra pour ces comptes.**
//
// Actif par défaut depuis le 2026-08-12 : mesuré, il supprime le changement de mode (0 ms contre
// 500 ms) et produit un rendu de menu **identique au bit près** au chemin d'origine — 0 pixel
// différent sur 172 800. `OPENFORESIA_LEGACY_STARTUP=1` ou `OPENFORESIA_LEGACY_3DHELP=1` le
// désactivent.
//
// Ce qu'il ne fait pas : porter les textures. Les objets qu'il rend au jeu existent et répondent,
// mais leur handle vaut zéro — « aucune texture liée », l'état que le backend traite déjà en
// rendant les primitives en aplats. Le portage réel reste la tâche 3.5 de
// `replace-3d-backend-with-d3d11`.
// =====================================================================

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `DeviceShim` etait la meme classe partielle avant le decoupage : ses thunks Shim_*, son budget
// de journalisation et Guarded restent partages. `Blaze` porte ToAnsiZ, VirtualAlloc et le reste.
using static OpenForesia.Blaze.Blaze;
using static OpenForesia.Blaze.DeviceShim;

namespace OpenForesia.Blaze;

internal static unsafe class SyntheticDevice
{
    /// <summary>
    /// Actif par défaut : c'est lui qui supprime le clignotement d'écran au démarrage.
    ///
    /// <para>
    /// Deux façons de le désactiver, toutes deux cohérentes avec ce qu'elles signifient :
    /// <c>OPENFORESIA_LEGACY_STARTUP=1</c> rétablit toute la séquence de démarrage d'origine, dont
    /// la prise d'affichage par <c>3dhelp</c> ; <c>OPENFORESIA_LEGACY_3DHELP=1</c> demande que tout
    /// passe par <c>3dhelp</c>, ce qui suppose évidemment qu'il existe.
    /// </para>
    /// <para>
    /// Champ statique depuis la sortie de <c>partial class Blaze</c> (2026-08-19). Il devait être
    /// une propriété calculée tant que ce fichier faisait partie de la classe partielle : l'ordre
    /// d'initialisation des champs statiques <b>entre fichiers d'une même classe</b> n'est pas
    /// garanti, et celui-ci dépend de <c>Video.s_WindowedStartup</c>. Entre deux classes
    /// distinctes, la garantie existe — lire un membre de <c>Blaze</c> déclenche l'initialisation
    /// de <c>Blaze</c> avant la lecture. Le contournement n'a donc plus d'objet.
    /// </para>
    /// <para>
    /// Le comportement est inchangé : <c>Video.s_WindowedStartup</c> est lui-même un champ
    /// <c>static readonly</c>, donc les deux variables d'environnement étaient déjà lues une seule
    /// fois à l'initialisation du type. Rien dans le projet ne les modifie en cours d'exécution.
    /// </para>
    /// </summary>
    private static readonly bool s_UseSyntheticDevice =
        Video.s_WindowedStartup && Environment.GetEnvironmentVariable("OPENFORESIA_LEGACY_3DHELP") != "1";

    /// <summary>
    /// Octets retirés de la pile par chaque slot, indexés par <c>offset / 4</c>. Extrait du binaire ;
    /// voir la table dans <c>Documentation/DLL/3dhelp.md</c>. `0xEC` n'a pas de cible valide dans la
    /// vtable et vaut zéro par défaut.
    /// </summary>
    private static readonly byte[] s_SlotCleanup =
    [
     /* 0x00 */ 4, 4, 4, 4, 0, 0, 4, 0,
     /* 0x20 */ 0, 0, 0, 8, 12, 0, 4, 0,
     /* 0x40 */ 0, 0, 0, 0, 16, 4, 4, 8,
     /* 0x60 */ 8, 4, 4, 4, 4, 4, 4, 4,
     /* 0x80 */ 4, 4, 8, 4, 4, 4, 4, 4,
     /* 0xA0 */ 4, 4, 8, 8, 0, 0, 0, 20,
     /* 0xC0 */ 28, 4, 4, 4, 4, 4, 8, 8,
     /* 0xE0 */ 0, 0, 8, 0, 4, 4, 0, 0,
    ];

    /// <summary>
    /// Mode de capture : laisse <c>3dhelp</c> s'initialiser réellement, mais relève ce que les
    /// accesseurs de capacités rendent. Sert à obtenir les 56 octets que <c>winsys3d</c> copie
    /// depuis <c>+0x4C</c> — valeurs qu'il faut **mesurer et non inventer**, puisqu'elles décident
    /// du filtrage de texture, de l'alpha et du mélange.
    ///
    /// L'écran clignote pendant une capture : c'est justement le chemin d'origine qui tourne.
    /// </summary>
    private static readonly bool s_CaptureDeviceCaps =
        Environment.GetEnvironmentVariable("OPENFORESIA_CAPTURE_CAPS") == "1";

    // `internal` : DeviceShim le lit pour savoir si la facade est superflue.
    internal static void* s_SyntheticDevice;
    private static void* s_RealGetProcAddress;
    private static void* s_RealCreate3DDevice;
    private static void** s_CaptureRealVtable;
    private static void* s_DeviceDesc;

    /// <summary>
    /// <c>D3DDEVICEDESC</c> que <c>+0x4C</c> rend — **relevé sur une exécution réelle**, pas
    /// inventé (<c>OPENFORESIA_CAPTURE_CAPS=1</c>, 2026-08-12).
    ///
    /// <para>
    /// <c>winsys3d</c> copie les 56 octets à l'offset 100 — le <c>dpcTriCaps</c>, un
    /// <c>D3DPRIMCAPS</c> — dans ses globales <c>DAT_00a2f438</c>… puis en teste les bits pour
    /// décider du filtrage de texture, du mélange et de l'alpha (`winsys3d.c:2495-2528`). Des
    /// capacités inventées produiraient un rendu subtilement faux plutôt qu'un échec franc, d'où la
    /// capture.
    /// </para>
    /// <para>
    /// **Portée de la mesure** : ce sont les capacités que <c>3dhelp</c> rapportait sur cette
    /// machine. Un autre matériel en donnerait d'autres. Les figer rend donc le device synthétique
    /// non portable en toute rigueur ; c'est acceptable ici parce que le chemin chaud est repris par
    /// le backend Direct3D 11 et que ces bits ne gouvernent plus que le choix de branches dans
    /// <c>winsys3d</c>. À revoir si le rendu s'écarte sur une autre configuration.
    /// </para>
    /// </summary>
    private static readonly uint[] s_CapturedDeviceDesc =
    [
        0x000000CC, 0x000001E3, 0x00000002, 0x021BBEF1, 0x00000008, 0x00000001,
        0x00000001, 0x00000010, 0x00000007, 0x00000001, 0x00000008, 0x00000038,
        0x00000272, 0x003379B1, 0x000000FF, 0x00001FFF, 0x00001FFF, 0x000000FF,
        0x000C528F, 0x00000CCD, 0x0303073F, 0x000000CF, 0x0000001F, 0x00000000,
        0x00000000, 0x00000038, 0x00000272, 0x003379B1, 0x000000FF, 0x00001FFF,
        0x00001FFF, 0x000000FF, 0x000C528F, 0x00000CCD, 0x0303073F, 0x000000CF,
        0x0000001F, 0x00000000, 0x00000000, 0x00000D00, 0x00000600, 0x00000000,
        0x0000FFFE, 0x00000001, 0x00000001, 0x00004000, 0x00004000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000,
    ];

    /// <summary>Profondeur rapportée par les accesseurs <c>+0x20</c> / <c>+0x24</c> / <c>+0x28</c> — relevée à 16.</summary>
    private const int CapturedBitDepth = 16;
    private static readonly bool[] s_SynthSeen = new bool[DeviceVtableSlots];

    /// <summary>Décalage du dernier slot neutre appelé, écrit par son trampoline.</summary>
    private static int* s_LastSynthSlot;

    [DllImport("kernel32.dll")] private static extern int VirtualProtect(void* address, nuint size, uint newProtect, uint* oldProtect);
    // `VirtualAlloc` est déjà déclaré dans Blaze.cs — même classe partielle.

    private const uint MemCommitReserve = 0x1000 | 0x2000;
    private const uint PageExecuteReadWrite = 0x40;
    private const int StubSize = 16;

    /// <summary>
    /// Fabrique un trampoline qui note le décalage du slot puis saute au thunk neutre de son arité.
    ///
    /// <para>
    /// Sans cela, un thunk neutre partagé par plusieurs slots ne peut pas dire lequel l'a appelé, et
    /// l'inventaire — le vrai produit de ce chantier — reste illisible. Le trampoline écrit une
    /// constante en mémoire puis saute : il ne touche aucun registre ni la pile, donc les arguments
    /// et la convention d'appel arrivent intacts au thunk.
    /// </para>
    /// <code>
    ///   C7 05 &lt;adresse&gt; &lt;slot&gt;   mov dword [s_LastSynthSlot], slot
    ///   E9 &lt;rel32&gt;                jmp thunk
    /// </code>
    /// </summary>
    private static void* MakeSlotStub(byte* stub, int slotOffset, void* thunk)
    {
        stub[0] = 0xC7; stub[1] = 0x05;
        *(void**)(stub + 2) = s_LastSynthSlot;
        *(int*)(stub + 6) = slotOffset;
        stub[10] = 0xE9;
        *(int*)(stub + 11) = (int)((byte*)thunk - (stub + 15));
        return stub;
    }

    /// <summary>
    /// Détourne <c>GetProcAddress</c> dans la table d'imports de <c>winsys3d</c>. À appeler
    /// **avant** <c>Ordinal_789</c>.
    /// <para>
    /// Depuis le 2026-09-23, la DLL n'est plus chargée : sa mémoire est l'image que construit
    /// <c>Libraries/winsys3d/Image.cs</c>, et le 789 porté appelle <c>GetProcAddress</c> à travers
    /// l'entrée <c>0x0121532c</c> de cette image, comme le binaire. C'est donc cette entrée qu'on
    /// remplace — directement : il n'y a plus de table d'imports chargée à parcourir.
    /// </para>
    /// </summary>
    internal static void InstallSyntheticDevice()
    {
        if ((!s_UseSyntheticDevice && !s_CaptureDeviceCaps) || s_RealGetProcAddress != null) return;

        void** entree = OpenForesia.Libraries.Winsys3dData.ImportGetProcAddress_0121532c;
        s_RealGetProcAddress = *entree;
        *entree = (delegate* unmanaged[Stdcall]<IntPtr, byte*, void*>)&GetProcAddressThunk;

        Console.Error.WriteLine("[SYNTH] GetProcAddress détourné dans l'image de winsys3d");
    }

    private static bool AsciiEqualsIgnoreCase(byte* p, string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            byte c = p[i];
            if (c == 0) return false;
            if (char.ToLowerInvariant((char)c) != char.ToLowerInvariant(s[i])) return false;
        }
        return p[s.Length] == 0;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void* GetProcAddressThunk(IntPtr module, byte* procName)
    {
        // Un import par ordinal a ses 16 bits de poids fort nuls : ce n'est pas une chaîne.
        var realGet = (delegate* unmanaged[Stdcall]<IntPtr, byte*, void*>)s_RealGetProcAddress;

        if ((nuint)procName > 0xFFFF && AsciiEqualsIgnoreCase(procName, "Create3DDevice"))
        {
            s_RealCreate3DDevice = realGet(module, procName);

            if (s_CaptureDeviceCaps)
            {
                Console.Error.WriteLine("[CAPS] Create3DDevice intercepté en mode capture — 3dhelp s'initialise normalement");
                return (delegate* unmanaged[Stdcall]<void*>)&Create3DDeviceCaptureThunk;
            }

            Console.Error.WriteLine("[SYNTH] Create3DDevice intercepté — 3dhelp ne sera pas initialisé");
            return (delegate* unmanaged[Stdcall]<void*>)&Create3DDeviceThunk;
        }

        return realGet(module, procName);
    }

    /// <summary>
    /// Fabrique le device synthétique : un objet nul de 256 octets dont le premier champ pointe sur
    /// notre vtable. <c>winsys3d</c> ne lit jamais les champs de cet objet — vérifié
    /// exhaustivement, voir <c>Documentation/DLL/3dhelp.md</c> — donc sa disposition n'a pas à être
    /// reproduite ; seule la taille compte, pour que les lectures de diagnostic ne débordent pas.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void* Create3DDeviceThunk()
    {
        if (s_SyntheticDevice != null) return s_SyntheticDevice;

        void** vtable = (void**)NativeMemory.AllocZeroed((nuint)(DeviceVtableSlots * sizeof(void*)));

        s_LastSynthSlot = (int*)NativeMemory.AllocZeroed(sizeof(int));
        byte* stubs = (byte*)Win32.VirtualAlloc(null, (nuint)(DeviceVtableSlots * StubSize),
                                          MemCommitReserve, PageExecuteReadWrite);
        if (stubs == null)
        {
            Console.Error.WriteLine("[SYNTH] allocation exécutable refusée — device synthétique impossible");
            return null;
        }

        // Chaque slot reçoit d'abord le trampoline vers le thunk neutre de son arité.
        for (int i = 0; i < DeviceVtableSlots; i++)
        {
            byte cleanup = i < s_SlotCleanup.Length ? s_SlotCleanup[i] : (byte)0;
            vtable[i] = MakeSlotStub(stubs + i * StubSize, i * 4, NeutralThunk(cleanup));
        }

        // `+0x30` doit rendre non nul, faute de quoi winsys3d appelle ExitProcess(3).
        vtable[0x30 / 4] = (delegate* unmanaged[Thiscall]<void*, uint, uint, uint, int>)&Synth_InitDisplay;

        // Capacités relevées. `+0x4C` doit rendre un pointeur : winsys3d copie 56 octets depuis
        // `résultat + 100` et en teste les bits.
        s_DeviceDesc = NativeMemory.AllocZeroed((nuint)(s_CapturedDeviceDesc.Length * sizeof(uint)));
        for (int i = 0; i < s_CapturedDeviceDesc.Length; i++)
            ((uint*)s_DeviceDesc)[i] = s_CapturedDeviceDesc[i];

        vtable[0x4C / 4] = (delegate* unmanaged[Thiscall]<void*, void*>)&Synth_GetDeviceDesc;
        vtable[0x20 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_BitDepth;
        vtable[0x24 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_BitDepth;
        vtable[0x28 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_BitDepth;

        // `+0x10` — fabrique de textures. Rendre zéro fait obtenir un objet nul au jeu, qui le
        // déréférence plus loin (faute d'accès dans `Ordinal_701`/`DrawOTag` depuis `FrameEnd`).
        vtable[0x10 / 4] = (delegate* unmanaged[Thiscall]<void*, void*>)&Synth_CreateTexture;

        // `+0x3C` — drapeau « application active », lu par la pompe de messages de winsys3d
        // (`FUN_00a01d70`, `winsys3d.c:612`) : quand aucun message n'attend et que ce drapeau vaut
        // zéro, elle appelle `WaitMessage()` et **reboucle indéfiniment**. Rendre zéro condamne donc
        // le jeu à une attente oisive perpétuelle, sans plantage ni progrès. On rend actif.
        vtable[0x3C / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_Active;

        // `+0x40` — **« suis-je le peripherique materiel ? »**, et c'est le slot qui decidait
        // jusqu'ici que le jeu n'a pas de transparence.
        //
        // Le corps d'origine est un getter d'une ligne, `mov eax,[ecx+0A4h] ; ret`
        // (`0x0FFE17D0`). Le champ `+0xA4` est pose a l'initialisation du peripherique
        // (`0x0FFE224A`) par la comparaison de seize octets entre le GUID du pilote Direct3D
        // choisi et celui grave a `0x0FFE65F0` : `84E63DE0-46AA-11CF-816F-0000C020156E`, soit
        // **`IID_IDirect3DHALDevice`**. Autrement dit `+0xA4` vaut 1 quand le jeu tourne sur le
        // materiel, 0 sur le rasteriseur logiciel de 1998. Le meme champ sert aussi a poser
        // `D3DRENDERSTATE_DITHERENABLE` juste apres.
        //
        // Cote jeu, `FUN_0050f120` en tire `DAT_00564a48 = (getter() == 0)` — « pas de
        // materiel ». Ce drapeau est relu dans une trentaine d'endroits, et il ne fait qu'une
        // chose : **retirer la semi-transparence**. Il efface le bit `0x200` des attributs de
        // modele et le bit `0x2000000` des mots de couleur, ce qui fait retomber la primitive du
        // code `0x26` (triangle texture semi-transparent, `FUN_00a116e0`) sur le code `0x24`
        // (opaque, `FUN_00a0f090`). C'est un menagement pour le rasteriseur logiciel, pas une
        // capacite qui nous manque.
        //
        // Notre peripherique synthetique rendait zero par la souche neutre, et le jeu en
        // concluait « logiciel » : toute la transparence tombait. Signale par Vincent le
        // 2026-08-30 sur l'onde de choc du Guerrier, dont la planche est un degrade — le bout
        // sombre, qui doit s'effacer par mélange, etait peint en **noir opaque**.
        //
        // Nous dessinons en Direct3D 11, avec melange alpha : la reponse honnete est oui.
        vtable[0x40 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_IsHardwareDevice;

        // Les trois capacités de la page « Résolution » — voir le bloc de commentaire sur
        // Synth_DisplayModeMask pour ce qu'elles valent chez nous et pourquoi.
        vtable[0x1C / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_DisplayModeMask;
        vtable[0xE0 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_CanChangeDisplayMode;
        vtable[0xE8 / 4] = (delegate* unmanaged[Thiscall]<void*, uint, uint, int>)&Synth_ChangeDisplayMode;

        // Chemin chaud : les thunks déjà écrits et éprouvés de la façade.
        vtable[0x50 / 4] = (delegate* unmanaged[Thiscall]<void*, int, int, int, int, int>)&Shim_SetViewport;
        vtable[0x5C / 4] = (delegate* unmanaged[Thiscall]<void*, void*, int, void>)&Shim_ClearTarget;
        vtable[0x60 / 4] = (delegate* unmanaged[Thiscall]<void*, void*, int, void>)&Shim_ClearZ;
        vtable[0x64 / 4] = (delegate* unmanaged[Thiscall]<void*, void*, void>)&Shim_BindTexture;

        // États de mélange, portés avec la liaison de texture : sans eux les quads à alpha partiel
        // sortent opaques, donc deux fois trop clairs. Voir le bloc « États de mélange » de
        // DeviceShim.cs pour la mesure qui l'a établi.
        vtable[0x94 / 4] = (delegate* unmanaged[Thiscall]<void*, uint, void>)&Shim_TextureMapBlend;
        vtable[0x98 / 4] = (delegate* unmanaged[Thiscall]<void*, uint, void>)&Shim_AlphaBlendEnable;
        vtable[0xA0 / 4] = (delegate* unmanaged[Thiscall]<void*, uint, void>)&Shim_SrcBlend;
        vtable[0xA4 / 4] = (delegate* unmanaged[Thiscall]<void*, uint, void>)&Shim_DestBlend;
        vtable[0x8C / 4] = (delegate* unmanaged[Thiscall]<void*, uint, void>)&Shim_ShadeMode;
        vtable[0xB0 / 4] = (delegate* unmanaged[Thiscall]<void*, void>)&Shim_BeginScene;
        vtable[0xB4 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Shim_EndScene;
        vtable[0xBC / 4] = (delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)&Shim_DrawPrimitive;
        vtable[0xDC / 4] = (delegate* unmanaged[Thiscall]<void*, void*, uint, int>)&Shim_Flip;

        void** device = (void**)NativeMemory.AllocZeroed(256);
        device[0] = vtable;
        s_SyntheticDevice = device;

        Console.Error.WriteLine($"[SYNTH] device synthétique créé — objet=0x{(nint)device:X8} vtable=0x{(nint)vtable:X8}");
        return device;
    }

    // =====================================================================
    // Mode capture — relève les capacités que le vrai device rapporte.
    //
    // On laisse `3dhelp` créer son device et prendre l'affichage, puis on échange sa vtable pour
    // journaliser ce que rendent les quatre accesseurs que `winsys3d` consulte avant de configurer
    // le rendu (`winsys3d.c:2489-2499`). Échanger la vtable d'un objet `3dhelp` est éprouvé — c'est
    // ce que fait `DeviceShim` — à la différence des objets `ddraw`, qui ne le supportent pas.
    // =====================================================================

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void* Create3DDeviceCaptureThunk()
    {
        void** device = (void**)((delegate* unmanaged[Stdcall]<void*>)s_RealCreate3DDevice)();
        if (device == null) return device;

        s_CaptureRealVtable = (void**)device[0];

        void** copy = (void**)NativeMemory.Alloc((nuint)(DeviceVtableSlots * sizeof(void*)));
        for (int i = 0; i < DeviceVtableSlots; i++) copy[i] = s_CaptureRealVtable[i];

        copy[0x20 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Capture_20;
        copy[0x24 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Capture_24;
        copy[0x28 / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Capture_28;
        copy[0x4C / 4] = (delegate* unmanaged[Thiscall]<void*, int>)&Capture_4C;

        device[0] = copy;
        Console.Error.WriteLine($"[CAPS] device réel=0x{(nint)device:X8}, vtable d'origine=0x{(nint)s_CaptureRealVtable:X8}");
        return device;
    }

    private static int CaptureGetter(void* self, int slot, string nom)
    {
        int v = ((delegate* unmanaged[Thiscall]<void*, int>)s_CaptureRealVtable[slot / 4])(self);
        Console.Error.WriteLine($"[CAPS] +0x{slot:X2} {nom} -> 0x{v:X8} ({v})");
        return v;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Capture_20(void* self) => CaptureGetter(self, 0x20, "getter champ +0xB4");

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Capture_24(void* self) => CaptureGetter(self, 0x24, "getter champ +0xB8");

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Capture_28(void* self) => CaptureGetter(self, 0x28, "getter champ +0xB8 (bis)");

    /// <summary>
    /// Le slot décisif : <c>winsys3d</c> copie 56 octets depuis <c>résultat + 100</c>. On relève ce
    /// bloc tel quel, plus un peu de contexte autour, pour pouvoir le rejouer à l'identique dans le
    /// device synthétique.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Capture_4C(void* self)
    {
        int v = ((delegate* unmanaged[Thiscall]<void*, int>)s_CaptureRealVtable[0x4C / 4])(self);
        Console.Error.WriteLine($"[CAPS] +0x4C -> 0x{v:X8}");

        if (v == 0) { Console.Error.WriteLine("[CAPS] +0x4C a rendu zéro — rien à relever"); return v; }

        uint* p = (uint*)(nint)v;
        var sb = new System.Text.StringBuilder("[CAPS] bloc copié par winsys3d (14 dwords depuis +100) :");
        for (int i = 0; i < 14; i++) sb.Append($"\n[CAPS]   +{100 + i * 4,3} = 0x{p[25 + i]:X8}   -> DAT_00a2f{0x438 + i * 4:X3}");
        Console.Error.WriteLine(sb.ToString());

        // Structure entière. Le premier dword est `dwSize` : 0xCC = 204 = sizeof(D3DDEVICEDESC),
        // et le bloc copié à +100 est son `dpcTriCaps` (D3DPRIMCAPS, 56 octets). On relève tout,
        // pour pouvoir rejouer la structure à l'identique dans le device synthétique.
        int size = (int)p[0];
        if (size is < 4 or > 4096) size = 204;
        var sb2 = new System.Text.StringBuilder($"[CAPS] D3DDEVICEDESC complet, dwSize={size} :");
        for (int i = 0; i * 4 < size; i++)
        {
            if (i % 6 == 0) sb2.Append($"\n[CAPS]   [{i * 4,3}]");
            sb2.Append($" 0x{p[i]:X8},");
        }
        Console.Error.WriteLine(sb2.ToString());
        return v;
    }

    /// <summary>Thunk neutre correspondant au nombre d'octets à retirer de la pile.</summary>
    private static void* NeutralThunk(byte cleanupBytes) => cleanupBytes switch
    {
        0 => (delegate* unmanaged[Thiscall]<void*, int>)&Synth_0,
        4 => (delegate* unmanaged[Thiscall]<void*, uint, int>)&Synth_1,
        8 => (delegate* unmanaged[Thiscall]<void*, uint, uint, int>)&Synth_2,
        12 => (delegate* unmanaged[Thiscall]<void*, uint, uint, uint, int>)&Synth_3,
        16 => (delegate* unmanaged[Thiscall]<void*, uint, uint, uint, uint, int>)&Synth_4,
        20 => (delegate* unmanaged[Thiscall]<void*, uint, uint, uint, uint, uint, int>)&Synth_5,
        28 => (delegate* unmanaged[Thiscall]<void*, uint, uint, uint, uint, uint, uint, uint, int>)&Synth_7,
        _ => (delegate* unmanaged[Thiscall]<void*, int>)&Synth_0,
    };

    /// <summary>
    /// Journalise le premier appel à chaque slot. C'est l'inventaire que la tâche 4.1 de
    /// <c>replace-3d-backend-with-d3d11</c> demande, obtenu gratuitement : tout slot qui apparaît
    /// ici est réellement emprunté par <c>winsys3d</c> et devra être porté ou justifié.
    /// </summary>
    private static void NoteSynthSlot(int cleanupBytes, string origine)
    {
        int slot = s_LastSynthSlot != null ? *s_LastSynthSlot : -1;
        if ((uint)(slot / 4) < (uint)s_SynthSeen.Length)
        {
            if (s_SynthSeen[slot / 4]) return;      // un seul relevé par slot
            s_SynthSeen[slot / 4] = true;
        }
        Console.Error.WriteLine($"[SYNTH] slot +0x{slot:X2} neutre ({origine}, nettoie {cleanupBytes} octets)");
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_InitDisplay(void* self, uint a, uint b, uint c)
    {
        Console.Error.WriteLine($"[SYNTH] +0x30 prise d'affichage neutralisée ({a}, {b}, {c}) — rend succès");
        return 1;   // non nul = succès, sinon winsys3d appelle Win32.ExitProcess(3)
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static void* Synth_GetDeviceDesc(void* self) => s_DeviceDesc;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_BitDepth(void* self) => CapturedBitDepth;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_Active(void* self) => 1;

    /// <summary>
    /// Slot <c>+0x40</c> — le getter du champ <c>+0xA4</c> de <c>3dhelp</c>, c'est-a-dire
    /// « le pilote Direct3D choisi est-il <c>IID_IDirect3DHALDevice</c> ? ». Voir le bloc de
    /// commentaire au site d'installation : rendre zero fait croire au jeu qu'il tourne sur le
    /// rasteriseur logiciel de 1998, et lui fait retirer la semi-transparence partout.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_IsHardwareDevice(void* self) => 1;

    // =====================================================================
    // Les trois slots de la page « Résolution » (2026-08-18).
    //
    // Ils étaient neutres, donc à zéro, et la page en héritait deux dégradations qui se
    // masquaient : sans `+0xE0` l'entrée n'apparaissait même pas dans la page « Affichage », et
    // sans `+0x1c` la liste des modes était vide. La page était pourtant transcrite en entier.
    //
    // Ce que le vrai device rapportait, c'est ce que DirectDraw lui énumérait des modes de
    // l'écran. Chez nous la question ne se pose pas dans les mêmes termes : le rendu va dans une
    // cible hors écran que `D3D11Backend.Present` met à l'échelle dans la fenêtre, en conservant
    // le rapport d'aspect. **Les cinq modes sont donc tous rendables**, quel que soit l'écran, et
    // aucun changement de mode physique n'a lieu — ce qui est un progrès sur l'original, pas un
    // écart : c'est la même image, sans reprise de l'affichage.
    // =====================================================================

    /// <summary>
    /// `+0x1c` — le masque des modes d'affichage acceptés, un bit par index de la table de
    /// `winsys3d` (`Documentation/DLL/winsys3d.md`) : 0 = 320x240, 1 = 512x384, 2 = 640x480,
    /// 3 = 800x600, 4 = 1024x768. On les annonce tous les cinq.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_DisplayModeMask(void* self) => 0x1f;

    /// <summary>
    /// `+0xE0` — « le mode d'affichage est-il changeable ? ». C'est ce que la page « Affichage »
    /// consulte pour décider si sa sixième ligne existe.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_CanChangeDisplayMode(void* self) => 1;

    /// <summary>
    /// `+0xE8(largeur, hauteur)` — le changement de mode proprement dit, appelé par
    /// `ChangeResolution` (winsys3d ordinal 749). **Doit rendre non nul** : sur zéro, la fonction
    /// abandonne sans poser `DAT_00a2c49c`, donc sans que le nouvel index de résolution prenne
    /// effet.
    ///
    /// <para>Il n'y a rien à redimensionner ici. La cible de rendu ne suit pas le mode d'affichage
    /// mais le **viewport**, que <c>SetShift</c> repose juste après depuis la page, et que le slot
    /// `+0x50` relève pour <c>EnsureSceneTarget</c>. Cette fonction n'a donc qu'à consentir.</para>
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_ChangeDisplayMode(void* self, uint width, uint height)
    {
        Console.Error.WriteLine($"[SYNTH] +0xE8 mode {width}x{height} — accepté (mise à l'échelle en fenêtre)");
        return 1;
    }

    // =====================================================================
    // Objets texture synthétiques.
    //
    // Le jeu en réclame par le slot device `+0x10` et les manipule ensuite par leur propre vtable.
    // Ils n'ont **pas besoin de vrais pixels** : le backend Direct3D 11 ne porte pas encore la
    // liaison de texture (tâche 3.5 de `replace-3d-backend-with-d3d11`) et rend déjà les primitives
    // en aplats de leur couleur diffuse. Ce qu'il leur faut, c'est exister et répondre sans faute.
    //
    // Arités relevées dans le binaire, vtable de la classe texture à `0x0FFE61F8` :
    //   [0] destructeur           ret 4     [4] ?                    ret 0
    //   [1] rend le handle (+0x10) ret 0    [5] remontée +0x04→+0x08 ret 0
    //   [2] chargeur (BITMAPINFO)  ret 8    [6] couleur-clé          ret 4
    //   [3] chargeur (texels)      ret 16   [7] ?                    ret 0
    //
    // Disposition de l'objet d'origine : 32 octets, documentée dans `Documentation/DLL/3dhelp.md`.
    // On alloue la même taille, à zéro : le handle lu en `+0x10` vaut donc zéro. Rien ne le lit sur
    // notre chemin — c'était `3dhelp` qui s'en servait, et il n'est plus là ; le backend identifie
    // une texture par le **pointeur de l'objet**, que le slot device `+0x64` lui donne directement.
    //
    // **Quel slot charge réellement, mesuré le 2026-08-14.** Le slot `[2]`, celui que
    // `Documentation/DLL/3dhelp.md` décrivait comme « le chargeur », n'est **jamais appelé** : le
    // relevé des slots empruntés ne montre que `[3]`, une fois par objet créé. La lecture de
    // `winsys3d.c` le confirme — `FUN_00a03460` (chemin DIB palettisé, qui appelle `[2]`) n'est
    // atteint par aucun chemin observé, alors que `FUN_00a03980` (`winsys3d.c:3064`) appelle
    //
    //     texture->vtable[3](largeur, hauteur, texels, DDPIXELFORMAT*)
    //
    // avec des texels **déjà en 16 bits**, convertis depuis la VRAM émulée par la table de
    // traduction que `CreateTranslationTables` (ordinal 756) a construite. C'est le chemin porté
    // ici. Deux formats possibles, choisis par le jeu selon la table employée et lisibles dans le
    // `DDPIXELFORMAT` qu'il fournit : ARGB1555 (`7C00/03E0/001F/8000`) et ARGB4444
    // (`0F00/00F0/000F/F000`). On lit les masques plutôt que de les supposer.
    //
    // Le slot `[6]` (couleur-clé) reste neutre : `winsys3d` ne l'appelle que si `DAT_00b2d07c`
    // vaut moins de 16, ce qui n'arrive pas ici — la transparence passe par le bit d'alpha du
    // format 16 bits, que le shader teste.
    // =====================================================================

    /// <summary>
    /// Douze, et non huit. La vtable réelle de la classe texture, relue à <c>0x0FFE61F8</c> dans
    /// <c>3dhelp.dll</c>, porte au moins onze entrées utiles — la table de ce fichier n'en listait
    /// que huit, ce qui suffisait tant que seul le menu principal était en jeu. La page d'options
    /// appelle le slot <b>8</b> (<c>+0x20</c>) une fois par texture et par image : c'est
    /// <c>IsLost</c>, et lire au-delà de la vtable aurait sauté dans du vide.
    /// </summary>
    private const int TextureVtableSlots = 12;
    private static void** s_TextureVtable;
    private static byte* s_TextureStubs;
    private static int s_TextureCount;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static void* Synth_CreateTexture(void* self)
    {
        if (s_TextureVtable == null)
        {
            s_TextureStubs = (byte*)Win32.VirtualAlloc(null, (nuint)(TextureVtableSlots * StubSize),
                                                 MemCommitReserve, PageExecuteReadWrite);
            if (s_TextureStubs == null) return null;

            // Arités relevées dans le binaire ; les slots 8 et au-delà viennent de la relecture du
            // 2026-08-14 : [7] et [8] sont des accesseurs sans argument (`ret` sec), [9] est nul
            // dans la vtable d'origine, [10] prend un octet.
            byte[] cleanup = [4, 0, 8, 16, 0, 0, 4, 0, 0, 0, 4, 0];
            s_TextureVtable = (void**)NativeMemory.AllocZeroed((nuint)(TextureVtableSlots * sizeof(void*)));
            for (int i = 0; i < TextureVtableSlots; i++)
            {
                // Décalage marqué 0x100 + i pour distinguer ces slots de ceux du device au journal.
                s_TextureVtable[i] = MakeSlotStub(s_TextureStubs + i * StubSize, 0x100 + i * 4,
                                                  NeutralThunk(cleanup[i]));
            }

            // Les deux seuls slots que le jeu emprunte réellement (mesuré) : le chargeur et le
            // destructeur. Les autres restent neutres, et signalent leur premier appel s'ils
            // venaient à servir.
            s_TextureVtable[3] = (delegate* unmanaged[Thiscall]<void*, uint, uint, void*, void*, int>)&Synth_TextureLoad;
            s_TextureVtable[0] = (delegate* unmanaged[Thiscall]<void*, uint, int>)&Synth_TextureDestroy;
            s_TextureVtable[2] = (delegate* unmanaged[Thiscall]<void*, void*, void*, int>)&Synth_TextureLoadDib;
            s_TextureVtable[6] = (delegate* unmanaged[Thiscall]<void*, void*, int>)&Synth_TextureColourKey;
            s_TextureVtable[8] = (delegate* unmanaged[Thiscall]<void*, int>)&Synth_TextureIsLost;
        }

        void** texture = (void**)NativeMemory.AllocZeroed(32);
        texture[0] = s_TextureVtable;
        s_TextureCount++;
        if (s_TextureCount <= 3)
            Console.Error.WriteLine($"[SYNTH] texture synthétique #{s_TextureCount} = 0x{(nint)texture:X8}");
        return texture;
    }

    /// <summary>
    /// Slot texture <c>[3]</c> — le chargeur réellement emprunté par le jeu.
    /// <c>winsys3d.c:3064</c> : <c>vtable[3](largeur, hauteur, texels, DDPIXELFORMAT*)</c>.
    ///
    /// <para>
    /// Les texels sont fournis en 16 bits, jointifs, une ligne après l'autre de haut en bas — le
    /// pas vaut donc <c>largeur * 2</c>. La conversion vers BGRA se fait ici parce que c'est ici
    /// que le format est connu : ses masques arrivent dans le <c>DDPIXELFORMAT</c> du dernier
    /// argument, et le jeu en emploie deux selon la table de traduction choisie.
    /// </para>
    /// <para>
    /// Rend un HRESULT : <c>winsys3d</c> ne teste que le signe (<c>if (iVar10 &lt; 0)</c>, après
    /// quoi il détruit l'objet et abandonne la texture). Un échec de conversion est donc rapporté
    /// comme un échec plutôt que dissimulé.
    /// </para>
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_TextureLoad(void* self, uint width, uint height, void* texels, void* pixelFormat)
    {
        int hr = -1;
        Guarded("chargement de texture", () =>
        {
            if (self == null || texels == null || pixelFormat == null)
                throw new InvalidOperationException("argument nul");
            if (width == 0 || height == 0 || width > 4096 || height > 4096)
                throw new InvalidOperationException($"dimensions inattendues {width}x{height}");

            // DDPIXELFORMAT : dwSize, dwFlags, dwFourCC, dwRGBBitCount, puis les quatre masques.
            uint* format = (uint*)pixelFormat;
            uint bitCount = format[3];
            if (bitCount != 16)
                throw new NotImplementedException(
                    $"texture en {bitCount} bits par texel — seul le format 16 bits de " +
                    "FUN_00a03980 a été observé ; le chemin palettisé de FUN_00a03460 passerait " +
                    "par le slot [2], jamais appelé jusqu'ici");

            uint[] bgra = ConvertTexels16((ushort*)texels, (int)width, (int)height,
                                          format[4], format[5], format[6], format[7]);
            D3D11Backend.RegisterTexture((nint)self, (int)width, (int)height, bgra);
            hr = 0;

            if (s_TextureLoads++ < 4)
                Console.Error.WriteLine(
                    $"[SYNTH] texture chargée 0x{(nint)self:X8} {width}x{height} " +
                    $"masques R=0x{format[4]:X4} G=0x{format[5]:X4} B=0x{format[6]:X4} A=0x{format[7]:X4} " +
                    $"— {s_LastTransparentTexels} texels transparents sur {width * height}");
        });
        return hr;
    }

    private static int s_TextureLoads;
    private static int s_LastTransparentTexels;

    /// <summary>
    /// Slot texture <c>[2]</c> — le chargeur **depuis un DIB**, `vtable[2](BITMAPINFO*, pixels*)`.
    ///
    /// <para>
    /// Ce document et ce fichier ont d'abord affirmé que ce slot n'était jamais appelé. C'était
    /// vrai du menu principal, et faux en général : la page d'options l'emprunte pour ses quatre
    /// atlas de libellés (`FUN_0050fab0`, `Blaze.c`), qu'elle rastérise elle-même en 256x256 8 bits
    /// à partir de `FONT.PIC` et des fichiers texte du CD, puis charge par ici.
    /// </para>
    /// <para>
    /// Le DIB est un `BITMAPINFO` ordinaire : en-tête de 40 octets, `biHeight` positif donc lignes
    /// **de bas en haut**, pas de ligne aligné sur 4 octets, palette `RGBQUAD` en ligne pour 4 et
    /// 8 bits. La transparence n'arrive qu'ensuite, par le slot <c>[6]</c> — d'où la conservation
    /// des index et de la palette jusque-là.
    /// </para>
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_TextureLoadDib(void* self, void* bitmapInfo, void* pixels)
    {
        int hr = -1;
        Guarded("chargement de texture DIB", () =>
        {
            if (self == null || bitmapInfo == null || pixels == null)
                throw new InvalidOperationException("argument nul");

            var header = (uint*)bitmapInfo;
            int width = (int)header[1];
            int height = (int)header[2];
            uint bitCount = header[3] >> 16;          // biPlanes en bas, biBitCount en haut
            bool bottomUp = height > 0;
            if (!bottomUp) height = -height;

            if (width <= 0 || height <= 0 || width > 4096 || height > 4096)
                throw new InvalidOperationException($"dimensions inattendues {width}x{height}");
            if (bitCount != 4 && bitCount != 8)
                throw new NotImplementedException(
                    $"DIB en {bitCount} bits par pixel — seuls 4 et 8 bits sont produits par " +
                    "FUN_0050fab0, le seul appelant connu de ce slot");

            var palette = (uint*)((byte*)bitmapInfo + 0x28);
            var source = new DibSource
            {
                Width = width,
                Height = height,
                BitCount = (int)bitCount,
                BottomUp = bottomUp,
                Indices = CopyDibIndices((byte*)pixels, width, height, (int)bitCount),
                Palette = CopyPalette(palette),
            };

            s_DibSources[(nint)self] = source;
            D3D11Backend.RegisterTexture((nint)self, width, height, ExpandDib(source, -1, -1));
            hr = 0;

            if (s_DibLoads++ < 4)
                Console.Error.WriteLine(
                    $"[SYNTH] texture DIB chargée 0x{(nint)self:X8} {width}x{height} " +
                    $"{bitCount} bits, {(bottomUp ? "bas-haut" : "haut-bas")}");
        });
        return hr;
    }

    /// <summary>
    /// Slot texture <c>[6]</c> — la couleur-clé, un `DDCOLORKEY { low, high }` d'index de palette.
    /// Elle arrive **après** le chargement, donc la texture est reconstruite avec ces index rendus
    /// transparents plutôt que retouchée.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_TextureColourKey(void* self, void* colourKey)
    {
        Guarded("couleur-clé de texture", () =>
        {
            if (self == null || colourKey == null) return;
            if (!s_DibSources.TryGetValue((nint)self, out DibSource? source)) return;

            int low = (int)((uint*)colourKey)[0];
            int high = (int)((uint*)colourKey)[1];
            D3D11Backend.RegisterTexture((nint)self, source.Width, source.Height,
                                         ExpandDib(source, low, high));
        });
        return 0;
    }

    /// <summary>
    /// Slot texture <c>[8]</c> — dans <c>3dhelp</c>, `mov eax,[ecx+8]` puis un appel au slot
    /// <c>+0x60</c> de la surface DirectDraw : <c>IDirectDrawSurface::IsLost</c>. Son appelant
    /// (`FUN_0050fe90`) recrée la texture dès que le retour est non nul.
    /// <para>
    /// Une texture Direct3D 11 ne se perd pas comme une surface DirectDraw : on rend toujours
    /// « pas perdue ». C'est la même nature de déviation que le reste du device synthétique — la
    /// perte de surface est un mécanisme DirectDraw, pas un comportement du jeu.
    /// </para>
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_TextureIsLost(void* self) => 0;

    private sealed class DibSource
    {
        internal int Width, Height, BitCount;
        internal bool BottomUp;
        internal byte[] Indices = [];
        internal uint[] Palette = [];
    }

    private static readonly Dictionary<nint, DibSource> s_DibSources = [];
    private static int s_DibLoads;

    private static byte[] CopyDibIndices(byte* pixels, int width, int height, int bitCount)
    {
        int stride = ((width * bitCount + 31) / 32) * 4;
        byte[] copy = new byte[stride * height];
        fixed (byte* destination = copy)
            System.Buffer.MemoryCopy(pixels, destination, copy.Length, copy.Length);
        return copy;
    }

    private static uint[] CopyPalette(uint* palette)
    {
        uint[] copy = new uint[256];
        for (int i = 0; i < 256; i++) copy[i] = palette[i];
        return copy;
    }

    /// <summary>
    /// Développe les index en BGRA, en retournant les lignes si le DIB est de bas en haut et en
    /// rendant transparents les index compris dans la couleur-clé (bornes incluses, -1 = aucune).
    /// </summary>
    private static uint[] ExpandDib(DibSource source, int keyLow, int keyHigh)
    {
        int stride = ((source.Width * source.BitCount + 31) / 32) * 4;
        uint[] bgra = new uint[source.Width * source.Height];

        for (int y = 0; y < source.Height; y++)
        {
            int sourceRow = source.BottomUp ? source.Height - 1 - y : y;
            int rowStart = sourceRow * stride;

            for (int x = 0; x < source.Width; x++)
            {
                int index = source.BitCount == 8
                    ? source.Indices[rowStart + x]
                    : (x & 1) == 0
                        ? source.Indices[rowStart + (x >> 1)] >> 4
                        : source.Indices[rowStart + (x >> 1)] & 0x0f;

                uint colour = source.Palette[index] & 0x00ffffff;
                bool transparent = keyLow >= 0 && index >= keyLow && index <= keyHigh;
                bgra[y * source.Width + x] = transparent ? 0u : colour | 0xff000000u;
            }
        }

        return bgra;
    }

    /// <summary>
    /// Développe des texels 16 bits en BGRA 32 bits d'après les masques du format.
    ///
    /// <para>
    /// Chaque canal est ramené sur 8 bits par mise à l'échelle sur sa propre étendue, ce qui traite
    /// du même coup les 5 bits des couleurs en 1555, les 4 bits du 4444, et le bit unique d'alpha :
    /// pour lui, l'étendue vaut 1, donc le calcul rend 0 ou 255 — exactement la couleur-clé que
    /// Direct3D 3 réalisait par <c>DDCKEY_SRCBLT</c>.
    /// </para>
    /// </summary>
    private static uint[] ConvertTexels16(ushort* texels, int width, int height,
                                          uint maskR, uint maskG, uint maskB, uint maskA)
    {
        (int shiftR, uint maxR) = Field(maskR);
        (int shiftG, uint maxG) = Field(maskG);
        (int shiftB, uint maxB) = Field(maskB);
        (int shiftA, uint maxA) = Field(maskA);

        uint[] bgra = new uint[width * height];
        int transparent = 0;

        for (int i = 0; i < bgra.Length; i++)
        {
            uint texel = texels[i];
            uint r = maxR != 0 ? Scale((texel & maskR) >> shiftR, maxR) : 0;
            uint g = maxG != 0 ? Scale((texel & maskG) >> shiftG, maxG) : 0;
            uint b = maxB != 0 ? Scale((texel & maskB) >> shiftB, maxB) : 0;
            uint a = maxA != 0 ? Scale((texel & maskA) >> shiftA, maxA) : 255;
            if (a < 128) transparent++;
            bgra[i] = a << 24 | r << 16 | g << 8 | b;         // B8G8R8A8 : b, g, r, a en mémoire
        }

        s_LastTransparentTexels = transparent;
        return bgra;

        static (int shift, uint max) Field(uint mask)
        {
            if (mask == 0) return (0, 0);
            int shift = System.Numerics.BitOperations.TrailingZeroCount(mask);
            return (shift, mask >> shift);
        }

        static uint Scale(uint value, uint max) => max == 0 ? 0 : value * 255 / max;
    }

    /// <summary>
    /// Slot texture <c>[0]</c> — destructeur. <c>winsys3d</c> l'appelle sur échec de chargement et
    /// à l'éviction du cache LRU (<c>winsys3d.c:25471</c> et voisinage), avec le drapeau
    /// « libérer la mémoire » des destructeurs C++ de l'époque. On libère la vue Direct3D et
    /// l'objet lui-même, faute de quoi chaque éviction fuirait une texture.
    /// </summary>
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_TextureDestroy(void* self, uint flags)
    {
        Guarded("destruction de texture", () =>
        {
            if (self == null) return;
            D3D11Backend.ReleaseTexture((nint)self);
            if ((flags & 1) != 0) NativeMemory.Free(self);
        });
        return 0;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_0(void* self) { NoteSynthSlot(0, "0 arg"); return 0; }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_1(void* self, uint a) { NoteSynthSlot(4, "1 arg"); return 0; }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_2(void* self, uint a, uint b) { NoteSynthSlot(8, "2 args"); return 0; }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_3(void* self, uint a, uint b, uint c) { NoteSynthSlot(12, "3 args"); return 0; }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_4(void* self, uint a, uint b, uint c, uint d) { NoteSynthSlot(16, "4 args"); return 0; }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_5(void* self, uint a, uint b, uint c, uint d, uint e) { NoteSynthSlot(20, "5 args"); return 0; }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvThiscall)])]
    private static int Synth_7(void* self, uint a, uint b, uint c, uint d, uint e, uint f, uint g) { NoteSynthSlot(28, "7 args"); return 0; }
}
