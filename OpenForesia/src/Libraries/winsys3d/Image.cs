// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`Legacy/Raw/Winsys3d/entry.disasm.txt`).
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// L'IMAGE MEMOIRE DE `winsys3d.dll`, SANS SON CODE — et son point d'entree (2026-09-23)
//
// Tout ce que le jeu execute de la DLL est en C#. Il ne lui restait qu'un role : PORTER SA
// MEMOIRE, celle que `Data.cs` designe (ses 150 globales, a leurs VA de 1998). Ce fichier la
// construit sans charger la DLL, ce qu'aurait fait le chargeur de Windows, moins le code :
//
//   1. une zone de 0x816000 octets (jusqu'a la fin de `.idata`), a zero — la queue non
//      initialisee de `.data` (8 Mo) l'est deja ;
//   2. `.rdata` et `.data` copiees depuis le fichier `winsys3d.dll` (a cote de l'executable), a
//      leur RVA : les constantes, les chaines, les tables initialisees ;
//   3. les RELOCATIONS qui tombent dans ces deux sections, pour la base reelle : seize, dont
//      les cinq noms de resolution. Deux visent du code (0x00a2a0ac/b0, la table de portee SEH
//      de la detection MMX) et ne servent plus : elles pointent dans une zone sans code ;
//   4. les trois entrees de la table d'imports que le C# appelle encore a travers elle, comme le
//      binaire : `GetProcAddress` (que `Host/SyntheticDevice.cs` detourne), `DirectDrawEnumerateA`
//      et `DirectDrawCreate` ;
//   5. le POINT D'ENTREE, a la premiere lecture de l'image — la ou Windows l'appelait au
//      chargement : `Attacher`, puis `Detacher` a la sortie du processus.
//
// `Data.cs` ne change pas : ses proprietes designent toujours `base + (VA - 0x00a00000)`, et
// seule la base change — c'etait le sens de son garde-fou 4.
//
// **Les ressources** (la police TIM 0x65, les kanji 0x70, le dialogue 0x66) sont lues dans le
// meme fichier, charge COMME DONNEES (`LOAD_LIBRARY_AS_DATAFILE`) : rien n'y est execute, ni son
// point d'entree, ni ses imports. Ce module de donnees tient lieu de `hInstance` de la DLL.
// ============================================================================================

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dImage
{
    private const nuint ImageBase = 0x00a00000;
    private const int TailleImage = 0x816000;
    private const uint LOAD_LIBRARY_AS_DATAFILE = 2;

    private static byte* s_Base;
    private static byte[] s_Fichier = null!;
    private static bool s_Detache;

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

    private static string CheminFichier => System.IO.Path.Combine(AppContext.BaseDirectory, "winsys3d.dll");

    private static void Construire()
    {
        s_Fichier = System.IO.File.ReadAllBytes(CheminFichier);
        byte* image = (byte*)Win32.VirtualAlloc(null, TailleImage, 0x3000 /* MEM_COMMIT|MEM_RESERVE */, 0x04 /* PAGE_READWRITE */);
        if (image == null)
            throw new InvalidOperationException("L'image memoire de winsys3d n'a pas pu etre allouee (0x816000 octets).");

        fixed (byte* f = s_Fichier)
        {
            int nt = *(int*)(f + 0x3c);
            int sections = *(ushort*)(f + nt + 6);
            int optionnel = *(ushort*)(f + nt + 0x14);
            byte* table = f + nt + 0x18 + optionnel;

            int debutRdata = 0, finRdata = 0, debutData = 0, finData = 0;
            for (int i = 0; i < sections; i++)
            {
                byte* s = table + i * 40;
                string nom = new string((sbyte*)s, 0, 8).TrimEnd('\0');
                int rva = *(int*)(s + 12), brut = *(int*)(s + 16), position = *(int*)(s + 20);
                if (nom != ".rdata" && nom != ".data") continue;
                Buffer.MemoryCopy(f + position, image + rva, brut, brut);
                if (nom == ".rdata") { debutRdata = rva; finRdata = rva + brut; }
                else { debutData = rva; finData = rva + brut; }
            }

            // Les relocations `HIGHLOW` qui tombent dans les octets copies.
            byte* repertoires = f + nt + 0x18 + 0x60;
            int relocRva = *(int*)(repertoires + 5 * 8), relocTaille = *(int*)(repertoires + 5 * 8 + 4);
            byte* reloc = f + RvaVersFichier(f, relocRva);
            uint delta = (uint)(image - (byte*)ImageBase);
            for (int p = 0; p < relocTaille;)
            {
                int page = *(int*)(reloc + p), taille = *(int*)(reloc + p + 4);
                if (taille == 0) break;
                for (int k = 8; k < taille; k += 2)
                {
                    ushort e = *(ushort*)(reloc + p + k);
                    int rva = page + (e & 0xfff);
                    if (e >> 12 != 3) continue;
                    bool copie = (rva >= debutRdata && rva < finRdata) || (rva >= debutData && rva < finData);
                    if (copie) *(uint*)(image + rva) += delta;
                }
                p += taille;
            }
        }

        s_Base = image;
        RemplirImports();
        Attacher();
        AppDomain.CurrentDomain.ProcessExit += (_, _) => Detacher();
    }

    private static int RvaVersFichier(byte* f, int rva)
    {
        int nt = *(int*)(f + 0x3c);
        int sections = *(ushort*)(f + nt + 6);
        byte* table = f + nt + 0x18 + *(ushort*)(f + nt + 0x14);
        for (int i = 0; i < sections; i++)
        {
            byte* s = table + i * 40;
            int debut = *(int*)(s + 12), taille = Math.Max(*(int*)(s + 8), *(int*)(s + 16));
            if (rva >= debut && rva < debut + taille) return rva - debut + *(int*)(s + 20);
        }
        throw new InvalidOperationException($"winsys3d.dll : la RVA 0x{rva:x} ne tombe dans aucune section.");
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
        fixed (byte* f = s_Fichier)
        {
            int nt = *(int*)(f + 0x3c);
            int dirRva = *(int*)(f + nt + 0x18 + 0x60);
            byte* dir = f + RvaVersFichier(f, dirRva);
            int baseOrdinal = *(int*)(dir + 0x10);
            int nbFonctions = *(int*)(dir + 0x14), nbNoms = *(int*)(dir + 0x18);
            int* fonctions = (int*)(f + RvaVersFichier(f, *(int*)(dir + 0x1c)));
            int* noms = (int*)(f + RvaVersFichier(f, *(int*)(dir + 0x20)));
            ushort* ordinaux = (ushort*)(f + RvaVersFichier(f, *(int*)(dir + 0x24)));

            int indice = -1;
            if ((nuint)nomOuOrdinal <= 0xffff)
                indice = (int)(nuint)nomOuOrdinal - baseOrdinal;
            else
                for (int i = 0; i < nbNoms; i++)
                {
                    byte* n = f + RvaVersFichier(f, noms[i]);
                    int k = 0;
                    while (n[k] == nomOuOrdinal[k] && n[k] != 0) k++;
                    if (n[k] == nomOuOrdinal[k]) { indice = ordinaux[i]; break; }
                }
            if (indice < 0 || indice >= nbFonctions || fonctions[indice] == 0) return null;
            return image + fonctions[indice];
        }
    }

    /// <summary>Le module tenant lieu de `hInstance` : le fichier charge comme donnees.</summary>
    private static IntPtr s_ModuleDonnees;

    /// <summary>
    /// Ghidra: winsys3d.c entry @ 0x00a01bb0 — `DLL_PROCESS_ATTACH` (et le point d'entree tout
    /// entier : c'est celui du PE, sans CRT devant). Les tables UV des sprites (la boucle x87 de
    /// 789, sur les constantes de `0x00a2a000`), la retouche de leurs dernieres entrees, la section
    /// critique, la VRAM (`Malloc(0x100000)`), `MainMem`, les ressources police (« TIM » 0x65) et
    /// kanji (« KANJI » 0x70), et le `hInstance`. `ret 0Ch`.
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

        s_ModuleDonnees = Win32.LoadLibraryExA(Blaze.Blaze.ToAnsiZ(CheminFichier), IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
        IntPtr police = Win32.FindResourceA(s_ModuleDonnees, (byte*)0x65, Winsys3dData.Chaine(0x00a2c170));   // « TIM »
        Winsys3dData.PoliceTim_011be6d4 = (byte*)Win32.LockResource(Win32.LoadResource(s_ModuleDonnees, police));
        IntPtr kanji = Win32.FindResourceA(s_ModuleDonnees, (byte*)0x70, Winsys3dData.Chaine(0x00a2c168));    // « KANJI »
        Winsys3dData.PoliceKanji_00b6d5b0 = (byte*)Win32.LockResource(Win32.LoadResource(s_ModuleDonnees, kanji));
        Winsys3dData.InstanceDll_00a2e8e8 = s_ModuleDonnees;
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
