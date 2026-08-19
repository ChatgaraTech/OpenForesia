// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Globales transversales : les symboles de .data dont un controle d'adjacence a montre qu'ils
// appartiennent a un voisinage partage, sans qu'aucune arene ne les couvre encore.
//
// ---------------------------------------------------------------------------------------------
// CE FICHIER EST VIDE, ET C'EST VOULU.
//
// La regle qui le gouverne, et qui vaut pour tout le decoupage :
//
//     Une globale ne se deplace JAMAIS pour rendre un fichier autonome.
//     Elle se deplace uniquement quand la carte d'adresses le dit.
//
// Une globale n'entre donc ici que sur PREUVE : un controle d'adjacence dans la section
// « carte des symboles reconstruits » de Documentation/DataSymbols.md montre qu'elle est voisine
// d'un symbole modelise ailleurs, et que les deux doivent partager leur memoire. Jamais parce
// qu'un fichier serait « plus propre » sans elle.
//
// Pourquoi cette regle existe : le defaut d'aliasing a ete corrige SEIZE fois dans ce depot. Il
// nait toujours du meme geste - separer ce que le binaire garde ensemble. Deux symboles voisins
// d'adresse declares separement ne partagent plus leur memoire, et une ecriture faite par un bout
// n'est jamais lue par l'autre. Aucune erreur, aucun plantage : une valeur qui reste a zero.
//
// L'etat mesure au moment du decoupage (2026-08-19) :
//   - 398 declarateurs a suffixe d'adresse, sur 375 adresses distinctes ;
//   - 187 d'entre eux ont un voisin a 4 octets ou moins ;
//   - 107 paires adjacentes (ecart <= 64 octets) atterrissent dans DEUX FICHIERS DIFFERENTS.
//
// Ce sont ces 107 paires qui rendent l'index d'adresses indispensable : apres le decoupage, leur
// adjacence n'est plus visible dans le code, seulement dans DataSymbols.md.
//
// Quand une globale y entre, ecrire pourquoi : l'adresse, le voisin, et le site qui a revele le
// partage.
// ---------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
}
