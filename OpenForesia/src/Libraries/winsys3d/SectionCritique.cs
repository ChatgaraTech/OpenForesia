// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c.
//
// ============================================================================================
// LA SECTION CRITIQUE — ordinaux 40 et 41
//
// Deux corps d'une ligne chacun, sur la meme section `&DAT_00b6d6d8` :
//
//     FUN_00a04fd0 (40)   EnterCriticalSection(&DAT_00b6d6d8)
//     FUN_00a04fe0 (41)   LeaveCriticalSection(&DAT_00b6d6d8)
//     FUN_00a04ff0        InitializeCriticalSection(&DAT_00b6d6d8)   — NON EXPORTE, appele par le
//                                                                      point d'entree de la DLL
//
// ============================================================================================
// CORRIGE LE 2026-09-23 : LA SECTION EST PARTAGEE AVEC LE MINUTEUR NATIF
//
// La premiere version de ce fichier (2026-09-21) affirmait que « cette section ne protege rien a
// l'interieur de la DLL » — `DAT_00b6d6d8` n'apparait que dans ces trois corps de `winsys3d.c` —
// et creait donc SA PROPRE section en C#. **C'etait faux.** Le rappel du minuteur multimedia de
// 17 ms, `LAB_00a06360`, que Ghidra n'a pas decompile parce qu'il n'est atteint que par adresse,
// commence par `call 00A04FD0h` et finit par `call 00A04FE0h` : il prend CETTE section, sur son
// propre fil, autour de l'avance de `VSyncCounter`, de `DeliverEvent` et du rappel
// `DAT_011be6a0` (`Legacy/Raw/Winsys3d/LAB_00a06360.disasm.txt`).
//
// Et c'est tout son objet : au demarrage, le jeu ouvre ses huit evenements de carte memoire SOUS
// la section (`Platform/Startup.cs`, `SetupJoystickHandlers`), parce que `OpenEvent` modifie la
// liste chainee que `DeliverEvent`, depuis le minuteur, parcourt. Deux sections distinctes ne
// s'excluent pas : pendant deux jours, ce parcours pouvait tomber au milieu d'un ajout.
//
// **La lecon, generale** : compter les references d'une globale dans la decompilation ne voit
// pas le code que Ghidra n'a pas decompile. Un rappel passe par adresse (`push 0A06360h`) est un
// participant de plus, a chercher au desassembleur.
//
// Le port designe donc la section de la DLL (`Data.cs`, aliasee), deja initialisee par son
// point d'entree : il n'en cree plus, et ne l'initialise pas une seconde fois.
//
// ============================================================================================
// POURQUOI KERNEL32 ET NON UN `lock` C#
//
// Le jeu **relache une fois de trop**. `Platform/Startup.cs` prend la section une fois
// (l'installation des gestionnaires de manette) et la relache DEUX fois : celle qui va avec, et
// une seconde sur le chemin d'extinction, sans prise correspondante.
//
//   - `LeaveCriticalSection` sur une section qu'on ne possede pas est un comportement **non
//     defini** au sens de la documentation, mais en pratique Win32 ne dit rien et ne leve rien ;
//   - `Monitor.Exit` sans possession leve `SynchronizationLockException`, immediatement.
//
// Et surtout, un `lock` C# ne peut pas exclure le minuteur natif. On appelle les vraies fonctions
// Win32, sur la vraie section — la transcription litterale des deux corps.
//
// **Ne pas « corriger » l'appel en trop.** Il est dans `Blaze.c`, il est donc dans le jeu de 1998,
// et le projet reproduit ses defauts au lieu de les reparer.
// ============================================================================================

using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dSectionCritique
{
    /// <summary>Ghidra: winsys3d.c FUN_00a04fd0 @ 0x00a04fd0 (ordinal 40) — prend la section.</summary>
    internal static void Entrer() => Win32.EnterCriticalSection(Winsys3dData.SectionCritique_00b6d6d8);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a04fe0 @ 0x00a04fe0 (ordinal 41) — relache la section.
    ///
    /// Appele une fois de plus qu'`Entrer` sur le chemin d'extinction du jeu ; voir le pave de
    /// tete. C'est pour cela que ce corps passe par `kernel32` et non par `Monitor`.
    /// </summary>
    internal static void Sortir() => Win32.LeaveCriticalSection(Winsys3dData.SectionCritique_00b6d6d8);
}
