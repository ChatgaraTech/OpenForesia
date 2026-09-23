// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// verifiee au desassembleur (`ret 4`, EAX = 1 ou 0).
//
// SOURCE DE VERITE : `winsys3d.c`. Base de chargement 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES EVENEMENTS DE LA `libapi` — ordinaux 6, 9, 10, 12, et les livraisons de carte 67/68/69
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
//     6        FUN_00a040d0    `OpenEvent`      **ici** (2026-09-23)
//     7        FUN_00a04130    `CloseEvent`     masse — delie et `Free`
//     8        FUN_00a04180    `WaitEvent`      masse — l'evenement Win32 partage
//     9        FUN_00a041d0    `TestEvent`      **ici** (2026-09-23)
//     10       FUN_00a04220    `EnableEvent`    **ici**
//     11       FUN_00a04250    `DisableEvent`   masse
//     12       FUN_00a04270    `DeliverEvent`   **ici** (2026-09-23), appele par 67/68/69
//     13       FUN_00a042d0    `UndeliverEvent` masse
//
// ============================================================================================
// POURQUOI CELUI-LA SEUL SE DETACHAIT — et pourquoi ce n'est plus vrai
//
// **Depuis le 2026-09-23, la liste est aliasee** (`Data.PremierEvenement_00dedc24`, `Dernier…`) :
// C# et natif parcourent LA MEME, celle de la DLL. Le raisonnement ci-dessous valait pour une
// copie ; il ne s'applique plus, et 6, 9 et 12 ont rejoint 10. 7, 8, 11 et 13 restent natifs
// parce que le jeu ne les importe pas.
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

using OpenForesia.Blaze;

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

    // ============================================================================================
    // 2026-09-23 — `OpenEvent`, `TestEvent`, `DeliverEvent` et les trois livraisons de carte,
    // verifies au desassembleur (`Legacy/Raw/Winsys3d/FUN_00a040d0`, `041d0`, `04270`, `050d0`,
    // `050f0`).
    //
    // **LB-045 : `OpenEvent` ne chaine pas vers l'avant.** Le nouveau nœud recoit l'ancien dernier
    // comme precedent (`[5]`), mais l'ancien dernier ne recoit jamais le nouveau comme suivant
    // (`[6]`), et aucun corps de la famille ne le fait. `DeliverEvent`, qui part de la tete et suit
    // `[6]`, ne voit donc que le PREMIER evenement ouvert. Sans consequence pour la carte : 
    // `TestEvent` declare arrivee toute fin d'operation carte, quel que soit l'etat du nœud.
    // ============================================================================================

    private const int EvStAlready = 0x4000;
    private const int EvMdNoIntr = 0x2000;

    /// <summary>`HwCARD` et `SwCARD` de la libapi, et `EvSpIOE` (fin d'entree-sortie).</summary>
    private const uint ClasseCarteMaterielle = 0xf0000011;
    private const uint ClasseCarteLogicielle = 0xf4000001;
    private const int SpecFinES = 4;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a040d0 @ 0x00a040d0 (ordinal 6) — `OpenEvent(classe, spec, mode,
    /// rappel)`, `ret 10h` : alloue un nœud de 0x1c octets dans le tas de `wsyskern` (`Malloc`,
    /// l'import), en attente (0x1000), et l'ajoute en fin de liste — sans chainage avant, LB-045.
    /// Rend le nœud, ou -1 si l'allocation echoue.
    /// </summary>
    internal static int* OpenEvent(uint classe, int spec, int mode, void* rappel)
    {
        int* n = (int*)Blaze.Wsyskern.Malloc(0x1c);
        if (n == null) return (int*)-1;

        n[0] = (int)classe;
        n[2] = spec;
        n[1] = EvStWait;
        n[3] = mode;
        n[4] = (int)rappel;
        n[5] = (int)Winsys3dData.DernierEvenement_00dedc20;
        n[6] = 0;
        if (Winsys3dData.PremierEvenement_00dedc24 == null) Winsys3dData.PremierEvenement_00dedc24 = n;
        Winsys3dData.DernierEvenement_00dedc20 = n;
        return n;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a041d0 @ 0x00a041d0 (ordinal 9) — `TestEvent`, `ret 4` : remet a
    /// zero l'evenement Win32, puis rend 1 — et rearme le nœud (0x2000) — s'il est declenche, OU
    /// si c'est une fin d'entree-sortie de carte (classe `HwCARD`/`SwCARD`, spec 4), quel que soit
    /// son etat. Rend 0 sinon.
    /// </summary>
    internal static int TestEvent(int* n)
    {
        Win32.ResetEvent(Winsys3dData.EvenementWin32_011be794);
        if (n[1] != EvStAlready)
        {
            uint classe = (uint)n[0];
            if ((classe != ClasseCarteLogicielle && classe != ClasseCarteMaterielle) || n[2] != SpecFinES)
                return 0;
        }
        n[1] = EvStActive;
        return 1;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04270 @ 0x00a04270 (ordinal 12) — `DeliverEvent(classe, spec)`,
    /// `ret 8` : cherche depuis la tete, par `[6]`, le premier nœud arme de cette classe et de cette
    /// spec. En mode `EvMdNOINTR` (0x2000) il le marque declenche et signale l'evenement Win32 ;
    /// sinon il appelle son rappel, sans argument. Rien s'il n'en trouve pas.
    /// </summary>
    internal static void DeliverEvent(uint classe, int spec)
    {
        for (int* n = Winsys3dData.PremierEvenement_00dedc24; n != null; n = (int*)n[6])
        {
            if ((uint)n[0] != classe || n[2] != spec || n[1] != EvStActive) continue;

            if (n[3] == EvMdNoIntr)
            {
                n[1] = EvStAlready;
                Win32.SetEvent(Winsys3dData.EvenementWin32_011be794);
                return;
            }
            ((delegate* unmanaged[Cdecl]<void>)n[4])();
            return;
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a050f0 @ 0x00a050f0 (ordinaux 67 et 69, une meme adresse) —
    /// livre une fin d'entree-sortie `SwCARD`, et rend 1. `ret 4`, argument ignore.
    /// </summary>
    internal static int LivrerFinCarteLogicielle()
    {
        DeliverEvent(ClasseCarteLogicielle, SpecFinES);
        return 1;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a050d0 @ 0x00a050d0 (ordinal 68) — livre une fin d'entree-sortie
    /// `HwCARD`, et rend 1. `ret 4`, argument ignore.
    /// </summary>
    internal static int LivrerFinCarteMaterielle()
    {
        DeliverEvent(ClasseCarteMaterielle, SpecFinES);
        return 1;
    }
}
