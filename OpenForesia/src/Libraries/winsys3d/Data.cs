// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c.
//
// ============================================================================================
// LES GLOBALES DE `winsys3d`, ALIASEES SUR LE `.data` REEL DE LA DLL CHARGEE
//
// **Ce fichier ne declare aucune variable.** Chaque « globale » ci-dessous est une propriete
// `ref` qui designe l'octet exact du segment de donnees de `winsys3d.dll`, telle qu'elle est en
// memoire. Un corps porte et son voisin reste natif voient donc LA MEME memoire, la meme qu'en
// 1998.
//
// ============================================================================================
// POURQUOI, ET POURQUOI C'EST UN TROISIEME CAS QUE `LISEZMOI.md` NE PREVOYAIT PAS
//
// La regle 2 du repertoire dit : « les globales d'une DLL ne rejoignent jamais `GameMemory` ».
// Elle vise a ne pas melanger deux espaces d'adressage, et elle reste vraie. Elle suppose en
// revanche qu'une DLL se porte ENTIEREMENT, d'un bloc, avec ses globales recreees en C#.
//
// Pour le chemin d'affichage, ce bloc pese **157 corps et 12 408 lignes** — voir
// `Documentation/Etat/CheminAffichage.md` — et il est indivisible en transcription classique :
// a etat inchange un producteur **allonge l'enregistrement precedent** du paquet
// (`paquet[curseur - 1] += n`), donc deux moities ne peuvent pas coexister avec deux copies des
// memos. Trois decoupages plus courts ont ete essayes et mesures, tous faux :
//
//     `DAT_011be760` seul   detache 1 corps sur 425
//     le tampon seul        sa base est aussi celle du rejeu
//     le rejeu seul         sa cloture contient 20 des 38 producteurs
//
// L'aliasing leve exactement cette contrainte. **Il n'y a plus deux copies : il n'y en a
// qu'une**, celle de la DLL. Le lot redevient divisible, corps par corps, et chaque etape se
// verifie a l'ecran au lieu d'attendre la douze-millieme ligne.
//
// Ce n'est pas un precedent nouveau, c'est la generalisation de deux choses qui tournent deja :
//
//   - l'ordinal 750 rend `DAT_011be760`, et `Screens/State00/Options.cs` s'en sert pour appeler
//     la vtable du device directement depuis `src/Blaze/`, depuis le 2026-08-12 ;
//   - `Platform/Winsys3d.cs` resout `MainMem`, `VSyncDelayed` et `TexFilterPossible` par
//     `GetProcAddress` et les lit **dans la DLL**, parce que P/Invoke ne sait pas importer une
//     donnee. Ce fichier fait la meme chose pour les globales qui n'ont pas de nom d'export.
//
// ============================================================================================
// LA REGLE QUI VA AVEC, ET QUI N'EST PAS NEGOCIABLE
//
// **Une globale aliasee ne se transcrit pas, elle se designe.** Tant qu'un seul corps de
// `winsys3d.dll` reste natif, aucune de ces adresses ne doit exister en double cote C# : une
// copie serait morte a la seconde ou le natif ecrirait la vraie.
//
// **Et le jour ou la DLL partira entierement**, ce fichier se remplace par de vraies variables,
// d'un seul geste et sans toucher aux appelants — c'est pour cela que tout passe par des
// proprietes et que personne ne manipule `Base` directement.
//
// ============================================================================================
// LES ADRESSES
//
// `winsys3d.dll` a pour `ImageBase` 0x00a00000, mais elle porte une section `.reloc` : Windows
// peut la charger ailleurs. **On ne se sert donc jamais de la VA telle quelle** — on prend la
// base reelle rendue par `GetModuleHandleA` et on y ajoute la RVA, soit `VA - 0x00a00000`.
//
// Les quatre regions en jeu, toutes dans `.data` (VA 0x00a2c000 .. 0x01214214, 8,2 Mo dont
// l'essentiel est non initialise dans le fichier) :
//
//     0x00a2c4xx   les memos d'etat du tampon de commandes
//     0x00b6dbd4   `MainMem`, le pointeur d'arene de la DLL
//     0x00dedbxx   l'arene de travail par image, en double tampon
//     0x011be7xx   le paquet courant, le curseur, le device, les rappels
// ============================================================================================

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dData
{
    /// <summary>L'`ImageBase` du PE, celle a laquelle les VA de `winsys3d.c` sont exprimees.</summary>
    private const nuint ImageBase = 0x00a00000;

    private static byte* s_Base;

    /// <summary>
    /// La base de chargement REELLE de `winsys3d.dll`. Resolue paresseusement et non dans un
    /// initialiseur statique : la DLL n'est pas forcement chargee quand cette classe l'est, et le
    /// depot a deja paye une fois le prix des initialiseurs statiques dont l'ordre n'est pas
    /// specifie (le decoupage de `WorldMapTick.cs`, huit champs rendus `null + offset`).
    /// </summary>
    private static byte* Base
    {
        get
        {
            if (s_Base != null) return s_Base;

            IntPtr module = Win32.GetModuleHandleA(Blaze.Blaze.ToAnsiZ("winsys3d.dll"));
            if (module == IntPtr.Zero) module = Win32.LoadLibraryA(Blaze.Blaze.ToAnsiZ("winsys3d.dll"));
            if (module == IntPtr.Zero)
                throw new InvalidOperationException(
                    "winsys3d.dll n'est pas chargee : les globales aliasees de Libraries/winsys3d " +
                    "n'ont pas d'adresse. Voir src/Libraries/winsys3d/Data.cs.");

            s_Base = (byte*)module;
            return s_Base;
        }
    }

    /// <summary>Une VA de `winsys3d.c` traduite en adresse reelle. `VA - 0x00a00000` est la RVA.</summary>
    private static byte* At(nuint va) => Base + (va - ImageBase);

    // ----------------------------------------------------------------------------------------
    // Les memos d'etat du tampon de commandes. Le coeur du perimetre : ce sont EUX qui rendent
    // le chemin d'affichage indivisible, parce qu'ils ne disent pas seulement « cet etat est deja
    // pose », ils disent « et l'enregistrement qui le porte est celui-ci, allonge-le ».
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_00a2c480 — l'etat de rendu courant. Remis a 8 par `DrawOTag` a chaque image.</summary>
    internal static ref int EtatRendu_00a2c480 => ref *(int*)At(0x00a2c480);

    /// <summary>DAT_00a2c484 — memo d'etat. Remis a -1 par `DrawOTag`.</summary>
    internal static ref int Memo_00a2c484 => ref *(int*)At(0x00a2c484);

    /// <summary>DAT_00a2c488 — le type de primitive courant. Remis a -1 par `DrawOTag`.</summary>
    internal static ref int TypePrimitif_00a2c488 => ref *(int*)At(0x00a2c488);

    /// <summary>DAT_00a2c48c — memo d'etat. Remis a -1 par `DrawOTag`.</summary>
    internal static ref int Memo_00a2c48c => ref *(int*)At(0x00a2c48c);

    // ----------------------------------------------------------------------------------------
    // L'arene de travail par image, et le pointeur d'arene de la DLL.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// `MainMem` @ 0x00b6dbd4 — le pointeur d'arene de la DLL, pose par son `entry` a
    /// `&amp;DAT_00b6dbd8`. C'est le meme que `Platform/Winsys3d.cs` resout par `GetProcAddress`
    /// sous son nom d'export ; ici on le prend par son adresse, pour ne pas dependre du nom.
    /// </summary>
    internal static ref byte* MainMem_00b6dbd4 => ref *(byte**)At(0x00b6dbd4);

    /// <summary>
    /// DAT_00dedbe4 — le **bascule de double tampon** de l'arene par image. `DrawOTag` fait
    /// `x = 0x40000 - x` a chaque appel : la valeur alterne entre 0 et 0x40000, et rien d'autre
    /// ne l'ecrit. Ghidra le rend en deux temps (`iVar1 = -x ; x = iVar1 + 0x40000`), ce qui est
    /// la meme chose.
    /// </summary>
    internal static ref int Bascule_00dedbe4 => ref *(int*)At(0x00dedbe4);

    /// <summary>DAT_00dedbd8 — le curseur d'allocation dans l'arene par image.</summary>
    internal static ref byte* Arene_00dedbd8 => ref *(byte**)At(0x00dedbd8);

    /// <summary>DAT_00dedbdc — sa copie, ecrite en meme temps et a la meme valeur.</summary>
    internal static ref byte* AreneDebut_00dedbdc => ref *(byte**)At(0x00dedbdc);

    /// <summary>
    /// DAT_00dedc44 — la garde d'entree de `DrawOTag` : non nul, l'image entiere est sautee.
    /// TODO(REVERSE): qui la pose. Aucun corps du perimetre ne l'ecrit.
    /// </summary>
    internal static ref int Garde_00dedc44 => ref *(int*)At(0x00dedc44);

    // ----------------------------------------------------------------------------------------
    // Le device, les compteurs et les rappels.
    // ----------------------------------------------------------------------------------------

    /// <summary>
    /// DAT_011be67c — l'export de donnees `VSyncDelayed`. Non nul, l'image est en retard et tout
    /// le dessin est saute pour la rattraper. Ghidra le nomme deja `VSyncDelayed` dans le corps de
    /// `DrawOTag` ; `Platform/Winsys3d.cs` le resout par `GetProcAddress` sous le meme nom.
    /// </summary>
    internal static ref int VSyncDelayed_011be67c => ref *(int*)At(0x011be67c);

    /// <summary>DAT_011be6f0 — un octet : choisit le slot `+0x5c` (non nul) ou `+0x60` (nul).</summary>
    internal static ref byte ModeEffacement_011be6f0 => ref *At(0x011be6f0);

    /// <summary>
    /// DAT_011be754 — un rappel de fin d'image, appele sans argument s'il est non nul. Pose par
    /// un corps hors du perimetre. TODO(REVERSE): par qui, et pour quoi.
    /// </summary>
    internal static ref IntPtr RappelFinImage_011be754 => ref *(IntPtr*)At(0x011be754);

    /// <summary>DAT_011be75c — le compteur d'images, incremente par `DrawOTag`.</summary>
    internal static ref int CompteurImages_011be75c => ref *(int*)At(0x011be75c);

    /// <summary>
    /// DAT_011be760 — **le device**. Une poignee, pas un etat : depuis le 2026-08-12 elle pointe
    /// sur un objet C#, `Host/SyntheticDevice.cs` detournant `GetProcAddress` dans la table
    /// d'imports de `winsys3d` pour rendre sa propre fabrique. L'ecrivain unique est
    /// `FUN_00a06420` (ordinal 789), reste natif.
    /// </summary>
    internal static ref void* Device_011be760 => ref *(void**)At(0x011be760);

    /// <summary>La vtable du device, c'est-a-dire `*DAT_011be760`.</summary>
    internal static void** DeviceVtable => *(void***)At(0x011be760);

    // ----------------------------------------------------------------------------------------
    // Le tampon de commandes lui-meme. Declare ici bien qu'aucun corps porte ne s'en serve
    // encore : c'est la piece autour de laquelle tout le perimetre est defini, et la laisser
    // implicite inviterait quelqu'un a en refaire une copie.
    // ----------------------------------------------------------------------------------------

    /// <summary>DAT_011be7ac — le paquet courant : celui qu'on remplit, qu'on rejoue, qu'on libere.</summary>
    internal static ref uint* PaquetCourant_011be7ac => ref *(uint**)At(0x011be7ac);

    /// <summary>DAT_011be7dc — le curseur d'ecriture dans le paquet, en mots de 32 bits.</summary>
    internal static ref int Curseur_011be7dc => ref *(int*)At(0x011be7dc);

    /// <summary>DAT_011be7a8 — la zone de sommets, dans le meme bloc que le paquet.</summary>
    internal static ref float* Sommets_011be7a8 => ref *(float**)At(0x011be7a8);

    /// <summary>DAT_00a2c578 — la derniere commande d'etat ecrite dans le paquet.</summary>
    internal static ref int DerniereCommande_00a2c578 => ref *(int*)At(0x00a2c578);

    /// <summary>DAT_00a2c57c — son argument.</summary>
    internal static ref int DerniereCommandeArg_00a2c57c => ref *(int*)At(0x00a2c57c);
}
