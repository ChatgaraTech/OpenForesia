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

    // ---------------------------------------------------------------------------------------------
    // POURQUOI LES SOUS-SYSTEMES SONT PARESSEUX ALORS QUE `Mem` NE L'EST PAS
    //
    // Le module initializer de Program.cs appelle TryPrepareDisplayListArena(), et son premier
    // geste est de lire un champ statique de `Blaze` : cela declenche TOUTE l'initialisation
    // statique de `Blaze` AVANT que l'arene basse ne soit reclamee. Tant que ces champs etaient
    // `static readonly`, chaque sous-systeme converti ajoutait donc son travail d'allocation devant
    // la course pour la memoire basse - les tables de UiText, les huit chaines de Video.
    //
    // MESURE le 2026-08-19, apres la conversion de Video : 5 courses perdues sur 8, contre environ
    // 1 sur 4 documente. Rendus paresseux : voir le releve dans REVERSE_STATUS.md.
    //
    // `Mem` reste immediat parce que la reclamation le lit vraiment
    // (s_UiDescriptors_00560ff0, s_StatusDescriptor_00560728). Les quatre autres, non : rien dans
    // TryPrepareDisplayListArena ne les touche. Les construire avant la course etait du travail
    // gratuit place au pire endroit possible.
    //
    // La paresse ici ne contredit pas la lecon � pris paresseusement, le descripteur echouait � :
    // celle-ci porte sur les DESCRIPTEURS de memoire basse, qui restent pris immediatement.
    // ---------------------------------------------------------------------------------------------
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
    private static UiText? s_ui;
    internal static UiText Ui => s_ui ??= new UiText(Mem);
    /// <summary>
    /// L'emetteur de primitives 2D. Construit APRES <see cref="Ui"/>, dont il depend : l'ordre
    /// textuel de ce fichier est ce qui garantit l'ordre d'initialisation.
    /// </summary>
    private static Primitives2d? s_prims;
    internal static Primitives2d Prims => s_prims ??= new Primitives2d(Mem, Ui);
    /// <summary>
    /// La sauvegarde sur carte memoire. Ses huit rappels natifs ne passent PAS par ce champ : ils
    /// sont statiques et s'atteignent par le nom de type, <c>SaveData.CardWriteStatusHandler0</c>.
    /// </summary>
    private static SaveData? s_saves;
    internal static SaveData Saves => s_saves ??= new SaveData(Mem);
    /// <summary>
    /// La sortie video. Son unique rappel natif ne passe pas par ce champ au sens habituel : c'est
    /// <c>MoviePlayerWindowProc</c>, statique, qui le LIT pour rejoindre l'instance.
    /// </summary>
    private static Video? s_vid;
    internal static Video Vid => s_vid ??= new Video(Mem);
    /// <summary>
    /// Les entrees. Son rappel natif <c>CountJoystickCallback</c> ne lit PAS ce champ : il n'a
    /// besoin que de son parametre, donc il reste un statique autonome.
    /// </summary>
    private static Input? s_inp;
    internal static Input Inp => s_inp ??= new Input(Mem);
    /// <summary>
    /// Le son. Aucun rappel natif : c'est la premiere conversion sans un seul
    /// <c>[UnmanagedCallersOnly]</c>, et la seule dont un membre reste statique par simple portee
    /// plutot que par contrainte de plateforme - <c>Audio.InvokeComMethod</c>, que
    /// <c>Platform/Startup.cs</c> emploie pour liberer une manette.
    ///
    /// <para>
    /// La paresse compte ici plus qu'ailleurs : cette classe alloue quatre blocs natifs a la
    /// construction (le format de sortie, les huit positions de notification, les deux tampons de
    /// chemin). Immediat, ce travail se serait place devant la course pour la memoire basse.
    /// </para>
    /// </summary>
    private static Audio? s_aud;
    internal static Audio Aud => s_aud ??= new Audio(Mem);
}
