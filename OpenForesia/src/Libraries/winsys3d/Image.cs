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
//   2. ce que `.rdata` et la partie initialisee de `.data` portent, a leurs VA : les constantes,
//      les chaines, les tables et valeurs initiales (les fichiers generes ci-dessous) ;
//   3. les seize pointeurs que Windows RELOGEAIT, ecrits pour la base reelle — dont les cinq noms
//      de resolution. Deux visent du code (0x00a2a0ac/b0, la table de portee SEH de la detection
//      MMX) et ne servent plus : ils pointent dans une zone sans code ;
//   4. les trois entrees de la table d'imports que le C# appelle encore a travers elle, comme le
//      binaire : `GetProcAddress` (que `Host/SyntheticDevice.cs` detourne), `DirectDrawEnumerateA`
//      et `DirectDrawCreate` ;
//   5. le POINT D'ENTREE, a la premiere lecture de l'image — la ou Windows l'appelait au
//      chargement : `Attacher`, puis `Detacher` a la sortie du processus.
//
// **Aucun fichier `.dll` n'est lu, et presque tout est du C# lisible.** Les donnees de 1998 ont ete
// transcrites une fois pour toutes du `winsys3d.dll` par `OpenForesia.Tools extraire-donnees`, qui
// GENERE ces fichiers et les verifie octet par octet contre la DLL :
//
//     ConstantesRdata.cs    `.rdata` hors table d'exports — flottants, noms de commandes CD
//     ValeursInitiales.cs   les valeurs initiales de `.data` — chaines, tables, memos, CRT
//     Exports.cs            les 32 exports de donnees
//     ModeleDialogue.cs     le dialogue 0x66, controle par controle
//
// Les pointeurs que Windows relogeait y sont ecrits « adresse de … » (`EcrivainImage.Adresses`) :
// il n'y a plus de liste de relocations. Seules les deux polices (la TIM 0x65, les kanji 0x70)
// restent des blobs binaires, embarques depuis `src/Libraries/Donnees/`.
//
// `Data.cs` ne change pas : ses proprietes designent toujours `base + (VA - 0x00a00000)`, et
// seule la base change — c'etait le sens de son garde-fou 4.
// ============================================================================================

using System;
using System.Collections.Generic;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

/// <summary>
/// Ce qu'emploient les fichiers generes pour ecrire dans l'image : chaque methode range ses valeurs
/// a une VA de 1998 (`base + VA - 0x00a00000`), en petit-boutiste, comme dans le fichier.
/// </summary>
internal readonly unsafe struct EcrivainImage(byte* image)
{
    private byte* A(uint va) => image + (va - 0x00a00000u);

    internal void Mots(uint va, params uint[] valeurs)
    {
        for (int i = 0; i < valeurs.Length; i++) ((uint*)A(va))[i] = valeurs[i];
    }

    /// <summary>Des pointeurs vers d'autres VA de l'image, reloges pour sa base reelle.</summary>
    internal void Adresses(uint va, params uint[] cibles)
    {
        for (int i = 0; i < cibles.Length; i++) ((uint*)A(va))[i] = (uint)A(cibles[i]);
    }

    internal void DemiMots(uint va, params ushort[] valeurs)
    {
        for (int i = 0; i < valeurs.Length; i++) ((ushort*)A(va))[i] = valeurs[i];
    }

    internal void Octets(uint va, params byte[] valeurs)
    {
        for (int i = 0; i < valeurs.Length; i++) A(va)[i] = valeurs[i];
    }

    /// <summary>Un `float` donne par ses bits (le commentaire du fichier genere en dit la valeur).</summary>
    internal void Flottant(uint va, uint bits) => *(uint*)A(va) = bits;

    /// <summary>Un `double` donne par ses bits.</summary>
    internal void Double(uint va, ulong bits) => *(ulong*)A(va) = bits;

    internal void Qmot(uint va, ulong valeur) => *(ulong*)A(va) = valeur;

    /// <summary>Une chaine C ASCII, NUL compris.</summary>
    internal void Chaine(uint va, string texte)
    {
        byte* p = A(va);
        for (int i = 0; i < texte.Length; i++) p[i] = (byte)texte[i];
        p[texte.Length] = 0;
    }
}

internal static unsafe class Winsys3dImage
{
    private const nuint ImageBase = 0x00a00000;
    private const int TailleImage = 0x816000;

    private static byte* s_Base;
    private static bool s_Detache;
    private static Dictionary<string, uint> s_ExportsParNom = null!;
    private static Dictionary<int, uint> s_ExportsParOrdinal = null!;

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

    /// <summary>Une police de `src/Libraries/Donnees/`, embarquee sous son nom de fichier.</summary>
    private static byte[] Donnee(string nom)
    {
        using var flux = typeof(Winsys3dImage).Assembly.GetManifestResourceStream(nom)
            ?? throw new InvalidOperationException($"Donnee embarquee introuvable : {nom} (src/Libraries/Donnees/).");
        var octets = new byte[flux.Length];
        flux.ReadExactly(octets);
        return octets;
    }

    /// <summary>Des octets copies dans un tableau EPINGLE : leur adresse est rangee dans l'image et ne doit pas bouger.</summary>
    private static byte* Epingler(byte[] source)
    {
        byte[] epingle = GC.AllocateArray<byte>(source.Length, pinned: true);
        source.CopyTo(epingle, 0);
        return (byte*)System.Runtime.CompilerServices.Unsafe.AsPointer(ref epingle[0]);
    }

    private static void Construire()
    {
        byte* image = (byte*)Win32.VirtualAlloc(null, TailleImage, 0x3000 /* MEM_COMMIT|MEM_RESERVE */, 0x04 /* PAGE_READWRITE */);
        if (image == null)
            throw new InvalidOperationException("L'image memoire de winsys3d n'a pas pu etre allouee (0x816000 octets).");

        var ecrivain = new EcrivainImage(image);
        Winsys3dConstantesRdata.Ecrire(ecrivain);
        Winsys3dValeursInitiales.Ecrire(ecrivain);

        s_ExportsParNom = new Dictionary<string, uint>();
        s_ExportsParOrdinal = new Dictionary<int, uint>();
        foreach (var (ordinal, adresse, nom) in Winsys3dExports.Donnees)
        {
            s_ExportsParOrdinal[ordinal] = adresse;
            if (nom != null) s_ExportsParNom[nom] = adresse;
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
        uint va;
        bool trouve = (nuint)nomOuOrdinal <= 0xffff
            ? s_ExportsParOrdinal.TryGetValue((int)(nuint)nomOuOrdinal, out va)
            : s_ExportsParNom.TryGetValue(new string((sbyte*)nomOuOrdinal), out va);
        return trouve ? image + (va - ImageBase) : null;
    }

    /// <summary>Le modele du dialogue 0x66 (`RT_DIALOG`), epingle, pour `DialogBoxIndirectParamA`.</summary>
    internal static byte* ModeleDialogue66 { get; private set; }

    // Les classes predefinies d'un `DLGITEMTEMPLATE`, par atome.
    internal const ushort ClasseBouton = 0x80, ClasseSaisie = 0x81, ClasseLibelle = 0x82,
                          ClasseListe = 0x83, ClasseDefilement = 0x84, ClasseCombo = 0x85;

    internal readonly record struct ControleDialogue(ushort Classe, ushort Id, string Texte, short X, short Y,
                                                     short Largeur, short Hauteur, uint Style, uint StyleEtendu);

    internal static ControleDialogue Controle(ushort classe, ushort id, string texte, short x, short y,
                                              short largeur, short hauteur, uint style, uint styleEtendu)
        => new(classe, id, texte, x, y, largeur, hauteur, style, styleEtendu);

    /// <summary>
    /// Assemble un `DLGTEMPLATE` (pas EX) avec police : l'en-tete, le titre et la police en UTF-16
    /// termines par un nul, puis chaque `DLGITEMTEMPLATE` aligne sur 4 octets, sa classe par atome
    /// (`0xffff`, atome), son texte, et zero octet de donnees de creation. C'est le format de la
    /// ressource 0x66 de `winsys3d.dll` — l'extraction verifie que cet assemblage la redonne.
    /// </summary>
    internal static byte[] ModeleDialogue(uint style, uint styleEtendu, short x, short y, short largeur, short hauteur,
                                          string titre, ushort taillePolice, string police, params ControleDialogue[] controles)
    {
        var o = new List<byte>();
        void W32(uint v) => o.AddRange(BitConverter.GetBytes(v));
        void W16(ushort v) => o.AddRange(BitConverter.GetBytes(v));
        void Texte(string s) { foreach (char c in s) W16(c); W16(0); }

        W32(style); W32(styleEtendu); W16((ushort)controles.Length);
        W16((ushort)x); W16((ushort)y); W16((ushort)largeur); W16((ushort)hauteur);
        W16(0); W16(0);                                  // pas de menu, classe par defaut
        Texte(titre); W16(taillePolice); Texte(police);
        foreach (var c in controles)
        {
            while (o.Count % 4 != 0) o.Add(0);
            W32(c.Style); W32(c.StyleEtendu);
            W16((ushort)c.X); W16((ushort)c.Y); W16((ushort)c.Largeur); W16((ushort)c.Hauteur); W16(c.Id);
            W16(0xffff); W16(c.Classe); Texte(c.Texte); W16(0);
        }
        return o.ToArray();
    }

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

        Winsys3dData.PoliceTim_011be6d4 = Epingler(Donnee("winsys3d.TIM-65.bin"));
        Winsys3dData.PoliceKanji_00b6d5b0 = Epingler(Donnee("winsys3d.KANJI-70.bin"));
        ModeleDialogue66 = Epingler(Winsys3dModeleDialogue.Construire());
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

        // Écart legacy ++ (LB-054) : l'original écrit toujours VS.Log, le journal de mise au point
        // de la synchronisation verticale resté dans la DLL livrée — un fichier inutile au joueur,
        // posé dans le dossier du jeu à chaque fermeture. Il ne s'écrit plus que sur demande.
        // Et jamais dans la version publiée (Release).
        if (OpenForesia.Blaze.Diagnostic.Actif && Environment.GetEnvironmentVariable("OPENFORESIA_VSLOG") == "1") EcrireJournal();
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
