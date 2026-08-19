// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Les sous-systemes du jeu, instancies une fois.
//
// ---------------------------------------------------------------------------------------------
// C'EST LE COMPOSITION ROOT, ET IL EST ICI PLUTOT QUE DANS Program.cs
//
// L'etape E du refactor prevoyait de composer dans `Program.cs`. Essaye le 2026-08-19 : le jeu
// plante au demarrage. Le module initializer de `Program.cs` reclame l'arene de liste d'affichage,
// et cette reclamation touche deliberement `Mem.s_UiDescriptors_00560ff0` - un descripteur qui doit
// vivre sous 0x01000000 et etre pris au meme instant precoce que l'arene.
//
// Les sous-systemes doivent donc naitre PENDANT l'initialisation statique de `Blaze`, pas apres.
// C'est ce que fait ce fichier. L'ordre y est textuel, donc garanti : `Mem` est declare dans
// Memory/Globals.cs, et tout initialiseur qui le lit doit s'executer apres lui.
//
// Ce n'est pas le composition root ideal - il ne peut pas prendre de decisions d'ordre au-dela de
// l'ordre textuel. C'est celui que la contrainte memoire basse autorise.
// ---------------------------------------------------------------------------------------------
//
// POURQUOI DES CHAMPS STATIQUES POUR ATTEINDRE DES INSTANCES
//
// Les ~600 fonctions transcrites sont encore statiques, et 405 membres DOIVENT le rester
// (P/Invoke, rappels [UnmanagedCallersOnly], pointeurs de fonction). Une fonction statique ne peut
// atteindre une instance que par un point d'entree statique. Ce sont ces points d'entree.
//
// Ils disparaissent au fur et a mesure : quand un sous-systeme appelant devient lui-meme une
// classe d'instance, il recoit ses dependances par constructeur et cesse de passer par ici. Le
// jour ou le dernier lecteur disparait, ce fichier avec.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Le moteur de texte de l'interface, premiere vraie classe d'instance de la transcription.
    ///
    /// <para>
    /// Sa signature - <c>UiText(GameMemory)</c> - declare ce qu'il touche. C'est tout l'objet de la
    /// conversion : avant, il lisait huit globales <c>.data</c> appartenant a cinq autres fichiers,
    /// en silence.
    /// </para>
    /// <para>
    /// Une seule instance, parce qu'il n'y a qu'un moteur de texte dans le jeu. Une classe
    /// d'instance modelise ici une <b>dependance</b>, pas une multiplicite.
    /// </para>
    /// </summary>
    internal static readonly UiText Ui = new UiText(Mem);
}
