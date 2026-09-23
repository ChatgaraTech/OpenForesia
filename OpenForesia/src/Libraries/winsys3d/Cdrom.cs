// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (arite, valeur d'EAX, taille du bloc rendu).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// CE QUI RESTE DE LA `libcd` — ordinal 92, `CdSearchFile`
//
// Le jeu de 1998 lisait ses donnees par le lecteur CD de la PlayStation, avec la `libcd` de
// PSY-Q. Le portage PC lit un systeme de fichiers ordinaire : la famille entiere a donc ete
// videe, et il n'en reste que des exports qui ne font plus rien. Le voisinage, releve par les
// commentaires d'ordinal de Ghidra :
//
//     ordinal  corps           ce qu'il en reste
//     92       FUN_00a05160    rend &DAT_00dedd58 en ignorant ses deux arguments — ce fichier
//     93       FUN_00a02e10    souche : rend 0, deux arguments (`Souches.cs`)
//     94       FUN_00a05170    `CdComstr` : rend "CdlNop", "CdlSetloc", "CdlPlay"… — pas importe
//     95       FUN_00a02e20    souche : rend 0, un argument (`Souches.cs`)
//     104      FUN_00a02e20    la meme souche
//
// **C'est l'ordinal 94 qui nomme la famille.** Son corps est un `switch` qui rend les chaines
// `CdlNop`, `CdlSetloc`, `CdlPlay`, `CdlForward`, `CdlBackward` : ce sont les commandes de la
// `libcd`, mot pour mot, et c'est `CdComstr`. Il n'est importe par personne, mais il etablit que
// 92 a 104 sont ce bloc-la.
//
// ============================================================================================
// POURQUOI `CdSearchFile` ET PAS UN NOM PRUDENT
//
// Trois preuves concordantes, et c'est ce qui autorise le nom plutot qu'un `Unknown_…` :
//
//   1. **la taille du bloc rendu.** `DAT_00dedd58` occupe 0x18 octets — le symbole suivant est
//      `DAT_00dedd70`. La `CdlFILE` de PSY-Q fait exactement cela : `CdlLOC pos` (4 octets :
//      minute, seconde, secteur, piste), `u_long size` (4), `char name[16]`. 4 + 4 + 16 = 24 ;
//   2. **la signature.** `CdSearchFile(CdlFILE *fp, char *name)` rend un `CdlFILE*`. Le corps
//      prend bien deux arguments (`ret 8`) et rend bien un pointeur ;
//   3. **le site d'appel.** `Blaze.c` ligne 74553 :
//      `Ordinal_92(&DAT_005e9040, PTR_s_BLAZE_ALL_1_00566bbc)` — un tampon de sortie et le nom
//      d'un fichier du CD, `BLAZE_ALL.1`.
//
// ============================================================================================
// CE QUE LE CORPS FAIT VRAIMENT, ET CE QUE CELA VEUT DIRE
//
//     mov eax,0DEDD58h
//     ret 8
//
// Deux instructions. Il **ignore le tampon de sortie et le nom du fichier**, et rend l'adresse
// d'une `CdlFILE` statique. Et cette `CdlFILE` n'est ecrite par personne : `DAT_00dedd58` a
// **une seule reference dans les 27 000 lignes de `winsys3d.c`**, celle-ci. Ce sont donc 24
// octets de zeros dont l'adresse circule — position nulle, taille nulle, nom vide.
//
// Le jeu ne s'en apercoit pas : son unique site jette le retour (`Platform/Startup.cs`), et la
// ligne suivante appelle l'ordinal 104, qui est une souche. La recherche de fichier sur le CD a
// ete remplacee ailleurs, pas ici.
//
// **La reconstruction passe `null, null` la ou l'original passe deux vrais pointeurs.** Aucune
// difference observable — le corps ne les lit pas, et le `ret 8` retire les huit octets dans les
// deux cas — mais c'est une infidelite du site d'appel, pas de ce corps. Notee ici parce que
// c'est le genre de chose qui ne se retrouve plus une fois le `[DllImport]` parti.
// ============================================================================================

using System.Runtime.InteropServices;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dCdrom
{
    /// <summary>
    /// &amp;DAT_00dedd58 — la `CdlFILE` statique que <see cref="CdSearchFile"/> rend. 0x18 octets,
    /// taille lue dans l'espacement des symboles (`DAT_00dedd70` suit). **Personne ne l'ecrit**,
    /// ni dans la DLL ni ici : elle reste a zero pour la duree du processus, exactement comme le
    /// segment de donnees d'origine.
    /// </summary>
    private static readonly byte* s_FicheCd_00dedd58 = (byte*)NativeMemory.AllocZeroed(0x18);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05160 @ 0x00a05160 (ordinal 92) — `CdSearchFile` de la libcd,
    /// vide dans ce portage : il rend l'adresse d'une `CdlFILE` statique **sans lire ni le
    /// tampon de sortie ni le nom du fichier**.
    ///
    /// Les deux arguments sont gardes dans la signature parce que le `ret 8` les retire de la
    /// pile ; les supprimer serait le defaut qu'`Ordinal_45` portait avant le 2026-08-21.
    /// </summary>
    internal static void* CdSearchFile(void* fiche, byte* nom) => s_FicheCd_00dedd58;
}
