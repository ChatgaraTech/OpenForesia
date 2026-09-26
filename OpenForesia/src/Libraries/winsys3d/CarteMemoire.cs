// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur pour les sept corps (cadres de pile, arites, valeur d'EAX en sortie).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LA FAMILLE DE FICHIERS DE LA CARTE MEMOIRE — composantes 2, 9, 10, 12 et 13
//
// C'est le lot que `byebye-dll.md` designait comme « le plus gros et le plus coherent » de ce qui
// se detache de la masse. Il retire **huit imports** d'un coup :
//
//     ordinal  corps           Win32 dessous            composante
//     18       FUN_00a044c0    FindClose/FindFirstFileA    2
//     19       FUN_00a04560    FindNextFileA               2
//     20       FUN_00a045e0    DeleteFileA                 2
//     21       FUN_00a04600    CreateFileA                 2
//     22       FUN_00a04650    CloseHandle                 13
//     23       FUN_00a04660    SetFilePointer              12
//     24       FUN_00a04680    ReadFile                     9
//     25       FUN_00a046b0    WriteFile                   10
//
// Plus les deux corps que la composante 2 soude aux quatre premiers sans que la reconstruction
// les importe — `FUN_00a03f80` (le pivot) et, par lui, les ordinaux 29 et 30. Une composante se
// porte entiere ou pas du tout : ils sont donc ici, sans appelant C#, exactement comme dans la
// DLL ou rien de `Blaze.exe` ne les nomme.
//
// ============================================================================================
// CE QUI SOUDE LA COMPOSANTE 2, ET POURQUOI LA COUPURE EST SURE
//
// Deux globales, et deux seulement (relevees sur les 27 000 lignes de `winsys3d.c`) :
//
//   - `DAT_00a2c250`, la poignee de recherche. Trois references : 18 l'ecrit, 18 et 19 la lisent.
//     Personne d'autre dans la DLL. C'est l'etat d'une enumeration, et les deux bouts partent
//     ensemble ;
//   - `DAT_00dedc58`, le tampon de chemin resolu. Douze references, toutes dans `FUN_00a03f80`,
//     qui l'ecrit de bout en bout a chaque appel et en rend l'adresse. **Ce n'est pas de l'etat
//     porte d'un appel a l'autre**, c'est un retour de fonction dans un casier statique — le
//     motif d'avant `sprintf_s`. Sa taille sort de l'espacement des symboles : le suivant est
//     `DAT_00dedd58`, soit 0x100 octets.
//
// Aucune fonction restee native ne touche ni l'une ni l'autre, et aucune ne peut appeler nos
// corps : la coupure ne separe donc aucune representation partagee, au sens de la regle 1 de
// `byebye-dll.md`.
//
// ============================================================================================
// LA BRANCHE `cdrom:` ETAIT MORTE — RETIREE LE 2026-09-26 (LEGACY ++, INSTALL-FROM-ISO)
//
// `FUN_00a03f80` compare les SIX premiers octets du descripteur a `"cdrom:"` et, s'ils
// concordent, refabrique le chemin par `_splitpath`/`_makepath` (wsyskern, ordinaux 4 et 5, voir
// `wsyskern/Chemins.cs`) en remplacant le nom de fichier par `descripteur + 6`.
//
// **Aucun site de la reconstruction ne peut y entrer.** Les quatre points d'entree ne recoivent
// qu'un descripteur, `Saves.s_CardPathDescriptor_00604940`, et `SetCardPathDeviceFields`
// (FUN_00512890) y ecrit `"save\"` avant tout autre usage : les six premiers octets valent donc
// `s`, `a`, `v`, `e`, `\` et la premiere lettre du nom de fichier. Elle a ete transcrite tout de
// meme, jusqu'au 2026-09-26 : en mode « legacy ++ », le jeu ne connait plus de CD, et la branche
// a ete retiree avec le reste. Une ligne `// Ghidra:` dans ResoudreChemin dit ce qu'elle faisait.
//
// ============================================================================================
// DEUX DEFAUTS DE L'ORIGINAL, RELEVES ET NON REPRODUITS A L'IDENTIQUE
//
// **Le tampon de repertoire de `FUN_00a03f80`.** Son cadre de pile fait 0x108 octets ; le
// desassemblage place le tampon de lecteur a +0x08 et celui de repertoire a +0x0c, ce qui ne
// laisse a ce dernier que 0xfc = 252 octets. Or `_splitpath` y ecrit jusqu'a 255 octets plus le
// NUL. Un chemin de plus de 251 caracteres deborde donc la pile de l'original, de quatre octets.
// Ici le tampon fait 256 octets : le debordement n'a pas lieu. **Ce n'est pas une correction de
// confort** — deborder un `stackalloc` corrompt le cadre du CLR, ce qui ne ressemble en rien a
// ce que fait l'original ; on ne peut pas reproduire ce defaut-la, seulement le noter. Il est
// hors d'atteinte de toute facon : le descripteur fait 0x20 octets.
//
// **`FindClose` sur une poignee qui n'en est pas une.** L'ordinal 18 ferme la recherche
// precedente sans test : au tout premier appel il passe donc zero, et apres une recherche qui a
// echoue il passe `INVALID_HANDLE_VALUE`. Win32 rend FALSE et ne dit rien de plus. C'est
// transcrit tel quel, l'appel compris.
// ============================================================================================

using System;
using System.Runtime.InteropServices;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dCarteMemoire
{
    // ----------------------------------------------------------------------------------------
    // Les deux globales de la DLL. Elles restent ici plutot que dans un `winsys3d/Data.cs` : le
    // repertoire n'en a pas, et `SectionCritique.cs` a deja pose le precedent en gardant la
    // sienne avec les deux corps qui la prennent. Rien d'autre dans `winsys3d.c` ne les nomme —
    // c'est le releve qui fait de cette composante une composante fermee.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// DAT_00a2c250 — la poignee rendue par <c>FindFirstFileA</c>, relue par l'ordinal 19 et
    /// fermee au debut de l'ordinal 18. Zero au chargement, comme le `.data` de la DLL.
    /// </summary>
    private static IntPtr s_PoigneeRecherche_00a2c250;

    /// <summary>
    /// &amp;DAT_00dedc58 — le chemin resolu, rendu par <see cref="ResoudreChemin"/>. 0x100 octets :
    /// le symbole suivant est `DAT_00dedd58`. Jamais rendu, comme le segment de donnees d'origine.
    /// </summary>
    private static readonly byte* s_TamponChemin_00dedc58 = (byte*)NativeMemory.AllocZeroed(0x100);

    // ----------------------------------------------------------------------------------------
    // Le cadre `WIN32_FIND_DATAA`, en offsets plutot qu'en structure : c'est ainsi que les deux
    // corps le lisent, et les trois offsets sont confirmes au desassembleur (`[esp+30h]`,
    // `lea edi,[esp+34h]`, `mov ecx,[esp+28h]` avec le cadre a -8 des deux `push`).
    // ----------------------------------------------------------------------------------------
    private const int TailleFindData = 0x140;
    private const int OffsetTailleFichier = 0x20;   // nFileSizeLow
    private const int OffsetNomFichier = 0x2c;      // cFileName[MAX_PATH]

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03f80 @ 0x00a03f80 — resout un descripteur de carte memoire en
    /// chemin Win32, dans le casier statique <c>DAT_00dedc58</c> dont il rend l'adresse.
    ///
    /// L'original a deux chemins ; le premier, pour un descripteur commencant par
    /// <c>"cdrom:"</c>, est retire (legacy ++, plus de CD). Reste celui que le jeu prenait toujours :
    ///
    ///   - le descripteur est copie tel quel, puis **le premier <c>':'</c> a partir de
    ///     l'octet 2 devient un <c>'~'</c>**. C'est la convention de nommage de la carte memoire
    ///     emulee : un `:` est interdit dans un nom de fichier Windows, le portage le remplace.
    ///     A partir de l'octet 2 et non 0, pour ne pas manger le `C:` d'un chemin absolu.
    ///
    /// C'est le pendant exact du test `cFileName[4] == '~'` de <see cref="TrouverPremier"/> : ce
    /// que ce corps encode en ecriture, l'enumeration le decode en lecture.
    /// </summary>
    internal static byte* ResoudreChemin(byte* descripteur)
    {
        // Les six premiers octets du descripteur, NUL-termines, pour la comparaison.
        *(uint*)s_TamponChemin_00dedc58 = *(uint*)descripteur;
        *(ushort*)(s_TamponChemin_00dedc58 + 4) = *(ushort*)(descripteur + 4);
        s_TamponChemin_00dedc58[6] = 0;

        // Ghidra: `if (lstrcmpiA(DAT_00dedc58, "cdrom:") == 0)` — la branche qui refabriquait le
        // chemin par _splitpath / _makepath (wsyskern, ordinaux 4 et 5) avec le nom pris a
        // `descripteur + 6` — retiré (legacy ++, plus de CD ; install-from-iso). Elle etait morte :
        // le seul descripteur du jeu commence par « save\ ».

        // `strcpy(DAT_00dedc58, descripteur)` — un `repne scasb` suivi d'un `rep movsd`, donc le
        // NUL est copie lui aussi.
        byte* source = descripteur;
        byte* destination = s_TamponChemin_00dedc58;
        byte octet;
        do
        {
            octet = *source;
            *destination = octet;
            source++;
            destination++;
        }
        while (octet != 0);

        // Ghidra: `_strchr @ 0x00a27a20` — la CRT liee statiquement dans la DLL, rebranchee ici
        // (regle 4 de `byebye-dll.md` : ces corps-la ne se transcrivent pas).
        for (byte* p = s_TamponChemin_00dedc58 + 2; *p != 0; p++)
        {
            if (*p == (byte)':')
            {
                *p = (byte)'~';
                break;
            }
        }

        return s_TamponChemin_00dedc58;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a044c0 @ 0x00a044c0 (ordinal 18) — ouvre une enumeration sur le
    /// descripteur et ecrit la premiere entree dans <paramref name="entree"/>. Rend
    /// <paramref name="entree"/>, ou <c>null</c> si le motif ne designe rien.
    ///
    /// La fiche d'entree est un enregistrement de 0x1c octets au moins : le nom NUL-termine a
    /// l'octet 0, **borne a 19 caracteres**, et la taille du fichier a l'octet 0x18.
    /// </summary>
    internal static byte* TrouverPremier(byte* descripteur, byte* entree)
    {
        byte* donnees = stackalloc byte[TailleFindData];

        // Sans test : voir le pave de tete.
        Win32.FindClose(s_PoigneeRecherche_00a2c250);

        byte* chemin = ResoudreChemin(descripteur);
        s_PoigneeRecherche_00a2c250 = Win32.FindFirstFileA(chemin, donnees);
        if (s_PoigneeRecherche_00a2c250 == (IntPtr)(-1)) return null;

        CopierEntree(donnees, entree);
        return entree;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04560 @ 0x00a04560 (ordinal 19) — l'entree suivante de
    /// l'enumeration ouverte par <see cref="TrouverPremier"/>, dans le meme format.
    ///
    /// **Ghidra le decompile `void`, et il ne l'est pas.** Les deux sorties laissent une valeur
    /// exploitable dans EAX par convention de generation de code : zero quand `FindNextFileA` a
    /// rendu FALSE (`test eax,eax ; jne` juste avant le `ret 4`), et l'adresse de la fiche sur le
    /// chemin nominal (`mov eax,[esp+14Ch]`, jamais reecrit ensuite). `FUN_005126c0` teste bien
    /// EAX au retour — d'ou le `int` de la facade, et d'ou ce que ce corps rend.
    /// </summary>
    internal static int TrouverSuivant(byte* entree)
    {
        byte* donnees = stackalloc byte[TailleFindData];

        if (Win32.FindNextFileA(s_PoigneeRecherche_00a2c250, donnees) == 0) return 0;

        CopierEntree(donnees, entree);
        return (int)entree;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a045e0 @ 0x00a045e0 (ordinal 20) — `DeleteFileA`, **a polarite
    /// inversee** : rend 1 si la suppression a ECHOUE, 0 si elle a reussi. C'est le
    /// <c>NEG EAX ; SBB EAX,EAX ; INC EAX</c> de fin de corps, soit exactement `BVar1 == 0`.
    /// </summary>
    internal static int Supprimer(byte* descripteur)
    {
        byte* chemin = ResoudreChemin(descripteur);
        return Win32.DeleteFileA(chemin) == 0 ? 1 : 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04600 @ 0x00a04600 (ordinal 21) — `CreateFileA`, avec des
    /// drapeaux qui ne sont pas ceux de Win32 et qu'il faut lire comme le corps les calcule :
    ///
    ///   - l'acces est <c>((drapeaux * 4) | (drapeaux &amp; 2)) &lt;&lt; 29</c>. Les deux bits de
    ///     poids faible remontent ainsi en <c>GENERIC_READ</c> (0x80000000) et
    ///     <c>GENERIC_WRITE</c> (0x40000000) : le bit 0 donne la lecture, le bit 1 l'ecriture. Le
    ///     <c>| (drapeaux &amp; 2)</c> avant le decalage est ce qui fait remonter le bit 1 a la
    ///     bonne place malgre le facteur 4 ;
    ///   - la disposition est <c>3</c> (<c>OPEN_EXISTING</c>), ou <c>4</c>
    ///     (<c>OPEN_ALWAYS</c>) si le bit 0x200 est pose ;
    ///   - partage, securite, attributs et modele sont tous nuls.
    ///
    /// Rend la poignee brute, <c>INVALID_HANDLE_VALUE</c> comprise : c'est l'appelant qui teste.
    /// </summary>
    internal static IntPtr Ouvrir(byte* descripteur, uint drapeaux)
    {
        byte* chemin = ResoudreChemin(descripteur);
        uint acces = ((drapeaux * 4) | (drapeaux & 2)) << 29;
        uint disposition = (drapeaux & 0x200) != 0 ? 4u : 3u;
        return Win32.CreateFileA(chemin, acces, 0, null, disposition, 0, IntPtr.Zero);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a04650 @ 0x00a04650 (ordinal 22) — `CloseHandle`. Le
    /// corps est decompile `void` et ne touche pas EAX apres l'appel : la facade rend donc le
    /// booleen de Win32, tel quel.</summary>
    internal static int Fermer(IntPtr poignee) => Win32.CloseHandle(poignee);

    /// <summary>Ghidra: winsys3d.c FUN_00a04660 @ 0x00a04660 (ordinaux 23 et 333, meme corps) —
    /// `SetFilePointer`, avec le mot de poids fort toujours nul : le portage ne connait pas les
    /// fichiers de plus de 4 Gio. Rend la nouvelle position.</summary>
    internal static int Deplacer(IntPtr poignee, int deplacement, uint origine)
        => (int)Win32.SetFilePointer(poignee, deplacement, null, origine);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04680 @ 0x00a04680 (ordinal 24) — `ReadFile`, et
    /// **0xffffffff sur echec**, pas zero : un fichier au bout duquel on lit rend 0 octet lu, ce
    /// qui n'est pas la meme chose qu'une erreur.
    ///
    /// Le nombre d'octets lus est ecrit **par-dessus le parametre `taille`** dans l'original
    /// (`&amp;param_3`) ; ici c'est une variable locale, ce qui revient au meme, le parametre
    /// n'etant plus relu.
    /// </summary>
    internal static uint Lire(IntPtr poignee, void* tampon, uint taille)
    {
        uint lus = taille;
        if (Win32.ReadFile(poignee, tampon, taille, &lus, null) != 0) return lus;
        return 0xffffffff;
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a046b0 @ 0x00a046b0 (ordinal 25) — la sœur ecriture de
    /// <see cref="Lire"/>, au meme 0xffffffff pres.</summary>
    internal static uint Ecrire(IntPtr poignee, void* tampon, uint taille)
    {
        uint ecrits = taille;
        if (Win32.WriteFile(poignee, tampon, taille, &ecrits, null) != 0) return ecrits;
        return 0xffffffff;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04700 @ 0x00a04700 (ordinal 29) — `MoveFileA` sur deux
    /// descripteurs resolus.
    ///
    /// **Aucun appelant en C#** : la reconstruction n'importe pas cet ordinal. Il est ici parce
    /// que la composante 2 le soude a <see cref="ResoudreChemin"/>, et qu'une composante se porte
    /// entiere. Noter le defaut qu'il porte : les deux resolutions ecrivent dans le MEME casier
    /// `DAT_00dedc58`, donc la seconde ecrase la premiere et `MoveFileA` recoit deux fois le
    /// meme pointeur — c'est-a-dire deux fois le nom d'arrivee. **Cet ordinal ne peut pas
    /// fonctionner**, et c'est sans doute pourquoi rien ne l'appelle.
    /// **LB-035 de `Documentation/Reference/legacy-bug.md`** : reproduit tel quel, ne pas reparer.
    /// </summary>
    internal static int Renommer(byte* descripteurSource, byte* descripteurDestination)
    {
        byte* source = ResoudreChemin(descripteurSource);
        byte* destination = ResoudreChemin(descripteurDestination);
        return Win32.MoveFileA(source, destination);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04730 @ 0x00a04730 (ordinal 30) — `SetCurrentDirectoryA` sur un
    /// descripteur resolu. Sans appelant en C#, pour la meme raison que
    /// <see cref="Renommer"/>.
    /// </summary>
    internal static int ChangerRepertoire(byte* descripteur)
    {
        byte* chemin = ResoudreChemin(descripteur);
        return Win32.SetCurrentDirectoryA(chemin);
    }

    /// <summary>
    /// La queue commune aux ordinaux 18 et 19 — les deux corps la portent en double, octet pour
    /// octet, et le desassemblage le confirme (`00a04500` et `00a04585` sont la meme sequence).
    ///
    /// Deux choses s'y passent :
    ///
    ///   - **le nom est decale** si un <c>'~'</c> se trouve a l'indice 4 ou 5 : le nom utile
    ///     commence alors juste apres. C'est le decodage de la convention posee par
    ///     <see cref="ResoudreChemin"/>, qui remplace un <c>':'</c> par un <c>'~'</c> ;
    ///   - **le nom est tronque a 19 caracteres**, par un <c>'\0'</c> ecrit d'office a l'indice
    ///     0x13 du nom decale, avant meme d'en mesurer la longueur.
    /// </summary>
    private static void CopierEntree(byte* donnees, byte* entree)
    {
        byte* nom = donnees + OffsetNomFichier;
        if (nom[4] == (byte)'~') nom += 5;
        else if (nom[5] == (byte)'~') nom += 6;

        nom[0x13] = 0;

        // `repne scasb` puis `rep movsd` : la longueur mesuree comprend le NUL, qui est donc
        // recopie.
        byte* source = nom;
        byte* destination = entree;
        byte octet;
        do
        {
            octet = *source;
            *destination = octet;
            source++;
            destination++;
        }
        while (octet != 0);

        *(uint*)(entree + 0x18) = *(uint*)(donnees + OffsetTailleFichier);
    }

    private static byte* AllouerChaine(string texte)
    {
        byte[] octets = System.Text.Encoding.Latin1.GetBytes(texte + "\0");
        byte* p = (byte*)NativeMemory.Alloc((nuint)octets.Length);
        for (int i = 0; i < octets.Length; i++) p[i] = octets[i];
        return p;
    }
}
