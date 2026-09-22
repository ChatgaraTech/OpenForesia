// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`ret 4`, EAX = 1 ou 0).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES EVENEMENTS DE LA `libapi` — ordinal 10, `EnableEvent`, et lui seul pour l'instant
//
// Les ordinaux 6 a 13 sont la famille d'evenements de la BIOS PlayStation, dans l'ordre de
// declaration de `libapi` — c'est le releve du 2026-08-18, dans `Documentation/DLL/winsys3d.md`.
// Un evenement est un nœud de sept mots, alloue par l'ordinal 6 dans le tas de `wsyskern` :
//
//     [0] classe   [1] ETAT   [2] mode   [3] arg3   [4] rappel   [5] precedent   [6] suivant
//
// et l'etat prend trois valeurs, celles de `libapi` : `EvStWAIT` 0x1000 (enregistre, pas arme),
// `EvStACTIVE` 0x2000 (arme), `EvStALREADY` 0x4000 (declenche, pas encore consomme).
//
//     ordinal  corps           libapi           verdict
//     6        FUN_00a040d0    `OpenEvent`      masse — `Malloc` et la liste chainee
//     7        FUN_00a04130    `CloseEvent`     masse — delie et `Free`
//     8        FUN_00a04180    `WaitEvent`      masse — l'evenement Win32 partage
//     9        FUN_00a041d0    `TestEvent`      masse — la liste et les deux canaux de carte
//     10       FUN_00a04220    `EnableEvent`    **ici**
//     11       FUN_00a04250    `DisableEvent`   masse
//     12       FUN_00a04270    `DeliverEvent`   masse — le repartiteur, il parcourt la liste
//     13       FUN_00a042d0    `UndeliverEvent` masse
//
// ============================================================================================
// POURQUOI CELUI-LA SEUL SE DETACHE
//
// Les sept autres **parcourent la liste chainee** que la DLL garde dans ses globales, ou
// allouent dedans. Les porter mettrait a jour notre copie de la liste pendant que le reste du
// code natif suivrait la sienne.
//
// L'ordinal 10 ne parcourt rien. Il recoit le nœud, teste son mot 1 et l'ecrit. **Et ce nœud
// n'est pas a nous** : il appartient a l'appelant, qui le tient de l'ordinal 6, c'est-a-dire du
// tas de `wsyskern` — une seule memoire, vue des deux cotes. Ecrire `[1]` depuis C# ou depuis la
// DLL revient exactement au meme, et l'ordinal 9 reste natif sur ces memes octets sans rien
// perdre. C'est la difference entre partager un ETAT (interdit, regle 1 de `byebye-dll.md`) et
// partager une MEMOIRE que l'appelant possede (sans consequence).
//
// ============================================================================================
// CE QU'IL FAUT NE PAS SE RAPPELER DE TRAVERS
//
// **Ce n'est pas un desenregistrement.** Le nom `EnableEvent` le dit, mais le releve du depot a
// mis du temps a le trancher : le vrai desenregistrement est l'ordinal 7, qui delie et `Free`.
// L'ordinal 10 arme, et rien de plus.
//
// **Il n'arme QUE depuis 0x1000.** Un nœud deja arme (0x2000) ou deja declenche (0x4000) ressort
// inchange, et le corps rend 0. C'est cette valeur de retour qui distingue « j'ai arme » de
// « il l'etait deja », et Ghidra la donne bien — mais elle est ignoree par tous les sites du jeu.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dEvenements
{
    private const int EvStWait = 0x1000;
    private const int EvStActive = 0x2000;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04220 @ 0x00a04220 (ordinal 10) — `EnableEvent` de la libapi :
    /// arme un nœud d'evenement, c'est-a-dire fait passer son mot 1 de <c>EvStWAIT</c> a
    /// <c>EvStACTIVE</c>.
    ///
    /// Rend 1 si le nœud etait bien en attente, 0 sinon — et dans ce second cas il ne touche a
    /// rien. Le nœud vient de l'ordinal 6, il est alloue dans le tas de `wsyskern` et reste
    /// partage avec les sept corps de la famille restes natifs ; voir le pave de tete.
    /// </summary>
    internal static int EnableEvent(void* noeud)
    {
        int* mots = (int*)noeud;
        if (mots[1] != EvStWait) return 0;

        mots[1] = EvStActive;
        return 1;
    }
}
