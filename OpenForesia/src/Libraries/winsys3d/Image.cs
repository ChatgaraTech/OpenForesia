// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`Legacy/Raw/Winsys3d/entry.disasm.txt`).
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// L'IMAGE MEMOIRE DE `winsys3d.dll`, SANS SON CODE NI SON FICHIER — et son point d'entree
// (2026-09-23)
//
// Tout ce que le jeu execute de la DLL est en C#. Il ne lui restait qu'un role : PORTER SA
// MEMOIRE, celle que `Data.cs` designe (ses 150 globales, a leurs VA de 1998). Ce fichier la
// construit sans la DLL, comme l'aurait fait le chargeur de Windows, moins le code :
//
//   1. une zone de 0x816000 octets (jusqu'a la fin de `.idata`), a zero — la queue non
//      initialisee de `.data` (8 Mo) l'est deja ;
//   2. `.rdata` et la partie initialisee de `.data`, a leur RVA : les constantes, les chaines, les
//      tables initialisees ;
//   3. les RELOCATIONS qui tombent dans ces deux sections, pour la base reelle : seize, dont
//      les cinq noms de resolution. Deux visent du code (0x00a2a0ac/b0, la table de portee SEH
//      de la detection MMX) et ne servent plus : elles pointent dans une zone sans code ;
//   4. les trois entrees de la table d'imports que le C# appelle encore a travers elle, comme le
//      binaire : `GetProcAddress` (que `Host/SyntheticDevice.cs` detourne), `DirectDrawEnumerateA`
//      et `DirectDrawCreate` ;
//   5. le POINT D'ENTREE, a la premiere lecture de l'image — la ou Windows l'appelait au
//      chargement : `Attacher`, puis `Detacher` a la sortie du processus.
//
// **Aucun fichier `.dll` n'est lu.** Les sections, les relocations, la table d'exports et les
// trois ressources (la police TIM 0x65, les kanji 0x70, le dialogue 0x66) ont ete extraits une fois
// pour toutes du `winsys3d.dll` de 1998 par `OpenForesia.Tools extraire-donnees`, et sont embarques
// dans l'assemblage depuis `src/Libraries/Donnees/`. Le programme ne livre plus la DLL.
//
// `Data.cs` ne change pas : ses proprietes designent toujours `base + (VA - 0x00a00000)`, et
// seule la base change — c'etait le sens de son garde-fou 4.
// ============================================================================================

using System;
using System.Collections.Generic;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dImage
{
    private const nuint ImageBase = 0x00a00000;
    private const int TailleImage = 0x816000;
    private const int RvaRdata = 0x2a000, RvaData = 0x2c000;

    private static byte* s_Base;
    private static bool s_Detache;
    private static Dictionary<string, int> s_ExportsParNom = null!;
    private static Dictionary<int, int> s_ExportsParOrdinal = null!;

    /// <summary>
    /// La base de l'image, construite a la premiere lecture — paresseusement, jamais dans un
    /// initialiseur statique (le depot en a paye le prix une fois). Le point d'entree tourne a la
    /// fin de la construction, une fois la base posee : il ecrit lui-meme dans l'image.
    /// </summary>
    internal static byte* Base
    {
        get
        {
            if (s_Base != null) return s_Base;
            Construire();
            return s_Base;
        }
    }

    /// <summary>Un blob de `src/Libraries/Donnees/`, embarque sous son nom de fichier.</summary>
    internal static byte[] Donnee(string nom)
    {
        using var flux = typeof(Winsys3dImage).Assembly.GetManifestResourceStream(nom)
            ?? throw new InvalidOperationException($"Donnee embarquee introuvable : {nom} (src/Libraries/Donnees/).");
        var octets = new byte[flux.Length];
        flux.ReadExactly(octets);
        return octets;
    }

    /// <summary>Un blob embarque, copie dans un tableau EPINGLE : son adresse est rangee dans l'image et ne doit pas bouger.</summary>
    private static byte* DonneeEpinglee(string nom, out int taille)
    {
        byte[] source = Donnee(nom);
        byte[] epingle = GC.AllocateArray<byte>(source.Length, pinned: true);
        source.CopyTo(epingle, 0);
        taille = source.Length;
        return (byte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref epingle[0]);
    }

    private static IEnumerable<string[]> Lignes(string nom)
    {
        foreach (string ligne in System.Text.Encoding.ASCII.GetString(Donnee(nom)).Split('\n'))
            if (ligne.Length > 0) yield return ligne.Split(' ');
    }

    private static void Construire()
    {
        byte* image = (byte*)Win32.VirtualAlloc(null, TailleImage, 0x3000 /* MEM_COMMIT|MEM_RESERVE */, 0x04 /* PAGE_READWRITE */);
        if (image == null)
            throw new InvalidOperationException("L'image memoire de winsys3d n'a pas pu etre allouee (0x816000 octets).");

        byte[] rdata = Donnee("winsys3d.rdata.bin"), data = Donnee("winsys3d.data.bin");
        fixed (byte* r = rdata) Buffer.MemoryCopy(r, image + RvaRdata, rdata.Length, rdata.Length);
        fixed (byte* d = data) Buffer.MemoryCopy(d, image + RvaData, data.Length, data.Length);

        uint delta = (uint)(image - (byte*)ImageBase);
        foreach (string[] l in Lignes("winsys3d.relocations.txt"))
            *(uint*)(image + Convert.ToInt32(l[0], 16)) += delta;

        s_ExportsParNom = new Dictionary<string, int>();
        s_ExportsParOrdinal = new Dictionary<int, int>();
        foreach (string[] l in Lignes("winsys3d.exports.txt"))
        {
            int rva = Convert.ToInt32(l[1], 16);
            s_ExportsParOrdinal[int.Parse(l[0])] = rva;
            if (l[2] != "-") s_ExportsParNom[l[2]] = rva;
        }

        s_Base = image;
        RemplirImports();
        Attacher();
        AppDomain.CurrentDomain.ProcessExit += (_, _) => Detacher();
    }

    /// <summary>
    /// Les trois entrees de la table d'imports que du C# appelle a travers elle — comme le chargeur
    /// de Windows les aurait remplies. Les autres restent nulles : plus rien ne passe par elles.
    /// </summary>
    private static void RemplirImports()
    {
        IntPtr kernel32 = Win32.LoadLibraryA(Blaze.Blaze.ToAnsiZ("kernel32.dll"));
        IntPtr ddraw = Win32.LoadLibraryA(Blaze.Blaze.ToAnsiZ("ddraw.dll"));
        *Winsys3dData.ImportGetProcAddress_0121532c = (void*)Win32.GetProcAddress(kernel32, Blaze.Blaze.ToAnsiZ("GetProcAddress"));
        *Winsys3dData.ImportDirectDrawEnumerateA_01215244 = (void*)Win32.GetProcAddress(ddraw, Blaze.Blaze.ToAnsiZ("DirectDrawEnumerateA"));
        *Winsys3dData.ImportDirectDrawCreate_01215248 = (void*)Win32.GetProcAddress(ddraw, Blaze.Blaze.ToAnsiZ("DirectDrawCreate"));
    }

    /// <summary>
    /// Un export de `winsys3d.dll` — un nom, ou un ordinal quand le pointeur tient dans son mot bas
    /// (la convention `MAKEINTRESOURCE` de `GetProcAddress`). Rend son adresse DANS L'IMAGE : ce
    /// sont les exports de DONNEES (`MainMem`, `VSyncDelayed`, les registres GTE 799 a 821…) que
    /// le jeu lit. Nul si l'export n'existe pas, comme `GetProcAddress`.
    /// </summary>
    internal static void* Exporte(byte* nomOuOrdinal)
    {
        byte* image = Base;
        int rva;
        bool trouve = (nuint)nomOuOrdinal <= 0xffff
            ? s_ExportsParOrdinal.TryGetValue((int)(nuint)nomOuOrdinal, out rva)
            : s_ExportsParNom.TryGetValue(new string((sbyte*)nomOuOrdinal), out rva);
        return trouve ? image + rva : null;
    }

    /// <summary>Le modele du dialogue 0x66 (`RT_DIALOG`), epingle, pour `DialogBoxIndirectParamA`.</summary>
    internal static byte* ModeleDialogue66 { get; private set; }

    /// <summary>
    /// Ghidra: winsys3d.c entry @ 0x00a01bb0 — `DLL_PROCESS_ATTACH` (et le point d'entree tout
    /// entier : c'est celui du PE, sans CRT devant). Les tables UV des sprites (la boucle x87 de
    /// 789, sur les constantes de `0x00a2a000`), la retouche de leurs dernieres entrees, la section
    /// critique, la VRAM (`Malloc(0x100000)`), `MainMem`, les ressources police (« TIM » 0x65) et
    /// kanji (« KANJI » 0x70), et le `hInstance`. `ret 0Ch`.
    ///
    /// Ecart assume : les ressources ne sont plus trouvees par `FindResourceA`/`LockResource` dans le
    /// module de la DLL, mais prises aux blobs embarques ; le `hInstance` de la DLL devient celui de
    /// l'executable, et le dialogue s'ouvre sur son modele (`DialogBoxIndirectParamA`).
    /// </summary>
    private static void Attacher()
    {
        float* c = (float*)(s_Base + (0x00a2a000 - ImageBase));
        Winsys3dDevice.RemplirTablesUv((float*)Winsys3dData.TablesUvSpriteDefaut_011c4cd0,
            c[0], c[1], c[2], c[3], c[4], c[5], c[6], c[7]);
        Winsys3dDevice.RetoucherTablesUvSprite((uint*)Winsys3dData.TablesUvSpriteDefaut_011c4cd0);
        Win32.InitializeCriticalSection(Winsys3dData.SectionCritique_00b6d6d8);   // FUN_00a04ff0
        Winsys3dData.Vram_00b6dbd0 = (byte*)Wsyskern.Malloc(0x100000);
        Winsys3dData.MainMem_00b6dbd4 = s_Base + (0x00b6dbd8 - ImageBase);

        Winsys3dData.PoliceTim_011be6d4 = DonneeEpinglee("winsys3d.TIM-65.bin", out _);
        Winsys3dData.PoliceKanji_00b6d5b0 = DonneeEpinglee("winsys3d.KANJI-70.bin", out _);
        ModeleDialogue66 = DonneeEpinglee("winsys3d.DIALOG-66.bin", out _);
        Winsys3dData.InstanceDll_00a2e8e8 = Win32.GetModuleHandleA(null);
    }

    /// <summary>
    /// Ghidra: winsys3d.c entry @ 0x00a01bb0 — `DLL_PROCESS_DETACH`, que Windows declenchait dans
    /// `ExitProcess` : arrete le minuteur, rend la VRAM, ecrit `VS.Log`. Appele juste avant
    /// l'`ExitProcess` de la CRT du jeu (`Platform/Crt.cs`) et a la sortie du processus managé ;
    /// une seule fois. Une sortie par `TerminateProcess` ne l'appelait pas non plus.
    /// </summary>
    internal static void Detacher()
    {
        if (s_Base == null || s_Detache) return;
        s_Detache = true;
        Win32.timeKillEvent(Winsys3dData.Minuteur_011be6d0);
        Wsyskern.Free(Winsys3dData.Vram_00b6dbd0);
        EcrireJournal();
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a052e0 @ 0x00a052e0 — ecrit `VS.Log` (repertoire courant, ecrase) :
    /// pour chaque enregistrement du journal des attentes d'image, une ligne de tirets et deux
    /// `sprintf` de `wsyskern` (ordinal 1) — `VSyncCounter`, `VSyncShouldBe`, `PerfCounter`,
    /// `PerfShouldBe` (`%I64d`), puis `LastDelayed`, `Wait`, `Delayed`, `Mode`.
    ///
    /// **Les enregistrements 0 a n - 1**, alors que le journal remplit 1 a n (l'indice est
    /// incremente avant l'ecriture) : le premier bloc du fichier est vide et le dernier
    /// enregistrement n'y est pas. Transcrit tel quel — les `VS.Log` de la DLL native le montrent.
    /// </summary>
    private static void EcrireJournal()
    {
        IntPtr fichier = Win32.CreateFileA(Winsys3dData.Chaine(0x00a2c44c), 0xc0000000, 0, null, 2, 0x80, IntPtr.Zero);
        int nombre = Winsys3dData.IndiceJournal_011be69c;
        int* rec = (int*)Winsys3dData.JournalImages_00dedd78;
        byte* tampon = stackalloc byte[256];
        uint ecrits;
        for (int i = 0; i < nombre; i++, rec += 10)
        {
            Win32.WriteFile(fichier, Winsys3dData.Chaine(0x00a2c430), 0x1a, &ecrits, null);
            long perf = ((long)rec[3] << 32) | (uint)rec[2];
            long attendu = ((long)rec[5] << 32) | (uint)rec[4];
            int n = Ecrire(tampon, $"VSyncCounter={rec[0]}\r\nVSyncShouldBe={rec[1]}\r\nPerfCounter={perf}\r\nPerfShouldBe={attendu}\r\n");
            Win32.WriteFile(fichier, tampon, (uint)n, &ecrits, null);
            n = Ecrire(tampon, $"LastDelayed={rec[6]}\r\nWait={rec[7]}\r\nDelayed={rec[8]}\r\nMode={rec[9]}\r\n");
            Win32.WriteFile(fichier, tampon, (uint)n, &ecrits, null);
        }
        Win32.CloseHandle(fichier);
    }

    /// <summary>
    /// Le `sprintf` des deux formats de `0x00a2c3e4` et `0x00a2c3b4` : des `%d` et des `%I64d`, que
    /// le formatage decimal invariant de .NET rend a l'identique.
    /// </summary>
    private static int Ecrire(byte* tampon, FormattableString texte)
    {
        string s = FormattableString.Invariant(texte);
        for (int i = 0; i < s.Length; i++) tampon[i] = (byte)s[i];
        tampon[s.Length] = 0;
        return s.Length;
    }
}
