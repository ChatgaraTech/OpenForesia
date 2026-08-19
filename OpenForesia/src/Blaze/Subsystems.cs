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
// C'est ce que fait ce fichier. L'ordre y est textuel DANS UN SEUL FICHIER, donc garanti : `Mem`
// est declare en tete, et les sous-systemes qui le lisent viennent apres lui.
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
    /// Le modele de .data, et le point d'acces statique unique vers lui.
    ///
    /// <para>
    /// <b>Il est declare ICI, en tete du fichier, et cet ordre est le sujet.</b> Les trois champs
    /// qui suivent le lisent. Or une classe partielle ne garantit l'ordre d'initialisation de ses
    /// champs statiques qu'A L'INTERIEUR D'UN MEME FICHIER : entre fichiers, l'ordre n'est pas
    /// specifie. Il a fallu un plantage pour l'apprendre, le 2026-08-19 - huit champs
    /// <c>Arena(0x...)</c> devenus <c>null + offset</c> parce que leur fichier s'initialisait avant
    /// celui qui portait <c>Arena</c>. Composer dans un seul fichier est ce qui rend l'ordre a la
    /// fois lisible et garanti ; c'est pourquoi <c>Memory/Globals.cs</c> a ete replie ici.
    /// </para>
    /// <para>
    /// <b>Et l'initialiseur doit rester dans l'initialisation statique de Blaze</b>, pas dans un
    /// composition root appele depuis <c>Program.cs</c> : tente le meme jour, le jeu plante au
    /// demarrage. Le module initializer reclame l'arene de liste d'affichage, et cette reclamation
    /// touche <b>deliberement</b> <c>Mem.s_UiDescriptors_00560ff0</c> et
    /// <c>Mem.s_StatusDescriptor_00560728</c> : ces descripteurs portent leur ordering table a
    /// <c>+0xa4</c>, masquee sur 24 bits, donc ils doivent vivre sous <c>0x01000000</c> et etre pris
    /// au meme instant precoce que l'arene.
    /// </para>
    /// <para>
    /// Une globale ne se deplace JAMAIS pour rendre un fichier autonome : uniquement quand la carte
    /// d'adresses le dit. Le defaut d'aliasing a ete corrige seize fois dans ce depot, et il nait
    /// toujours du meme geste - separer ce que le binaire garde ensemble.
    /// </para>
    /// <para>
    /// Ce champ disparait quand plus aucune fonction statique ne le lira. Ce n'est pas un singleton
    /// de confort : c'est un echafaudage date, avec une condition de retrait ecrite.
    /// </para>
    /// </summary>
    internal static GameMemory Mem = new GameMemory();

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
    /// <summary>
    /// L'emetteur de primitives 2D. Construit APRES <see cref="Ui"/>, dont il depend : l'ordre
    /// textuel de ce fichier est ce qui garantit l'ordre d'initialisation.
    /// </summary>
    internal static readonly Primitives2d Prims = new Primitives2d(Mem, Ui);
    /// <summary>
    /// La sauvegarde sur carte memoire. Ses huit rappels natifs ne passent PAS par ce champ : ils
    /// sont statiques et s'atteignent par le nom de type, <c>SaveData.CardWriteStatusHandler0</c>.
    /// </summary>
    internal static readonly SaveData Saves = new SaveData(Mem);
}
