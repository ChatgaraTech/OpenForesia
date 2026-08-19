// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Le point d'acces unique au modele de .data.
//
// ---------------------------------------------------------------------------------------------
// POURQUOI UN CHAMP STATIQUE ICI, ALORS QUE TOUT LE PROPOS EST D'EN SORTIR
//
// L'etape A rend la memoire instanciable ; elle ne convertit pas encore les ~600 fonctions
// transcrites en methodes d'instance. Tant qu'elles sont statiques, elles ont besoin d'un point
// d'entree statique vers l'instance. C'est celui-ci, et il est UNIQUE - un seul champ, pas 640.
//
// Il disparait a l'etape C : chaque sous-systeme recevra `GameMemory` par son constructeur, et
// `Mem` n'aura plus de lecteur. Le jour ou la derniere reference disparait, ce fichier avec.
//
// Ce n'est donc pas un singleton de confort : c'est un echafaudage date, avec une condition de
// retrait ecrite.
// ---------------------------------------------------------------------------------------------
//
// Une globale ne se deplace JAMAIS pour rendre un fichier autonome. Elle se deplace uniquement
// quand la carte d'adresses le dit - voir ARCHITECTURE.md, la regle d'etat, et la carte des
// symboles reconstruits en fin de Documentation/DataSymbols.md.
//
// Le defaut d'aliasing a ete corrige SEIZE fois dans ce depot. Il nait toujours du meme geste :
// separer ce que le binaire garde ensemble. Mesure au decoupage : 107 paires de symboles voisins
// d'adresse vivent maintenant dans deux fichiers differents.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Le modele de .data.
    ///
    /// <para>
    /// <b>L'initialiseur doit rester ICI, et pas dans un composition root.</b> Tente le 2026-08-19,
    /// le deplacer vers <c>Program.cs</c> fait planter le jeu au demarrage : le module initializer
    /// de <c>Program.cs</c> reclame l'arene de liste d'affichage, et cette reclamation touche
    /// <b>deliberement</b> <c>Mem.s_UiDescriptors_00560ff0</c> et
    /// <c>Mem.s_StatusDescriptor_00560728</c>. Ces descripteurs portent leur ordering table a
    /// <c>+0xa4</c>, masquee sur 24 bits, donc ils doivent vivre sous <c>0x01000000</c> et etre
    /// pris au meme instant precoce que l'arene - mesure : pris paresseusement, « l'arene
    /// reussissait et le descripteur echouait ensuite ».
    /// </para>
    /// <para>
    /// <c>GameMemory</c> doit donc exister <b>pendant</b> l'initialisation statique de
    /// <c>Blaze</c>. Ce n'est pas une regression du refactor : avant lui, les 16 allocations
    /// etaient deja des initialiseurs de champ de <c>Blaze</c> et precedaient exactement de la meme
    /// facon la reclamation de l'arene.
    /// </para>
    /// <para>
    /// Ce champ disparait a l'etape C : chaque sous-systeme recevra <c>GameMemory</c> par son
    /// constructeur, et <c>Mem</c> n'aura plus de lecteur. La contrainte ci-dessus devra alors etre
    /// retraitee, pas oubliee.
    /// </para>
    /// </summary>
    internal static GameMemory Mem = new GameMemory();
}
