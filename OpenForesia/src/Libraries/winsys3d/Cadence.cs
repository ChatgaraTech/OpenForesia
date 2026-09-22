// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`ret` et `ret 4`, EAX = l'ancienne valeur).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// UN CASIER, UN LECTEUR, UN ECRIVAIN — ordinaux 135 et 136
//
// Deux corps de trois instructions autour d'une seule globale :
//
//     ordinal 135   FUN_00a05760   `mov eax,[11BE6A4h] ; ret`          la lit — PAS IMPORTE
//     ordinal 136   FUN_00a05770   l'echange et rend l'ancienne        importe, un seul site
//
// `DAT_011be6a4` a **trois references dans les 27 000 lignes de `winsys3d.c`**, et ce sont ces
// trois-la. Aucune autre fonction de la DLL ne la consulte : ce n'est pas de l'etat dont la DLL
// se sert, c'est un casier qu'elle prete au jeu. Exactement la situation de `DAT_011be6d8` avec
// les ordinaux 659/660 (voir `ListeAffichage.cs`), et c'est ce qui rend la paire portable.
//
// L'ordinal 135 n'est importe par personne. Il part quand meme : porter l'ecrivain sans le
// lecteur laisserait 135, natif, relire une globale que plus rien n'ecrit. Une composante se
// porte entiere.
//
// ============================================================================================
// LE ROLE N'EST PAS ETABLI, ET LES DEUX CORPS GARDENT DONC UN NOM D'ADRESSE
//
// Ce qu'on sait :
//
//   - le jeu appelle l'ordinal 136 **une seule fois, avec 0**, au demarrage — `Blaze.c` ligne
//     74470, entre l'ordinal 45 et `srand(1)`. Il jette la valeur rendue ;
//   - l'ordinal 136 est un echange qui rend l'ancienne valeur, forme typique d'un reglage
//     global qu'on veut pouvoir restaurer ;
//   - il vit dans le bloc d'ordinaux 129-138, ou l'ordinal 132 est la temporisation d'image et
//     l'ordinal 138 une souche vide.
//
// Cela suggere un niveau de trace ou un reglage de cadence — `Documentation/DLL/winsys3d.md`
// note « frame time offset ? » depuis le premier relevé, avec le point d'interrogation. Un
// echange rendant l'ancienne valeur, appele une fois avec 0, ressemble beaucoup au
// `SetGraphDebug` de la `libgpu`. **Rien ne le prouve**, et le depot ne renomme que ce qui est
// etabli : les deux corps s'appellent donc `Unknown_00a057…`, la globale `s_Unknown_011be6a4`,
// et le nom de ce FICHIER dit le voisinage d'ordinaux, pas le role.
//
// TODO(REVERSE): trancher entre « niveau de trace » et « reglage de cadence ». La DLL ne peut
// pas repondre — rien d'autre n'y lit la globale. Ce qui repondrait : le nom PSY-Q de l'ordinal
// 136, que `playst.dll` porte peut-etre (elle a les 758 noms de Sony) ; ou un appel natif a
// l'ordinal 135 apres avoir pose une valeur, pour voir si quoi que ce soit change a l'ecran.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dCadence
{
    /// <summary>
    /// DAT_011be6a4 — le casier. Zero au chargement, comme le `.data` de la DLL, et le jeu y
    /// remet zero au demarrage. Role non etabli : voir le pave de tete.
    /// </summary>
    private static uint s_Unknown_011be6a4;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05760 @ 0x00a05760 (ordinal 135) — rend la valeur du casier,
    /// sans argument.
    ///
    /// **Aucun appelant en C#** : la reconstruction n'importe pas cet ordinal. Il est ici parce
    /// que le laisser natif lui ferait relire une globale que plus rien n'ecrirait.
    /// </summary>
    internal static uint Unknown_00a05760() => s_Unknown_011be6a4;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a05770 @ 0x00a05770 (ordinal 136) — pose une valeur dans le
    /// casier et rend **l'ancienne**.
    ///
    /// Un seul site dans tout le jeu, au demarrage, avec 0, et la valeur rendue est jetee.
    /// </summary>
    internal static uint Unknown_00a05770(uint valeur)
    {
        uint precedente = s_Unknown_011be6a4;
        s_Unknown_011be6a4 = valeur;
        return precedente;
    }
}
