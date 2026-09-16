// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LA NAISSANCE D'UNE ENTITE — `SpawnParticle_0050F070`.
//
// Vingt lignes, et huit appelants repartis dans quatre repertoires : les effets d'herboristerie,
// les effets d'etat, la resolution du coup, la progression, et deux opcodes de script d'objet.
// C'est pourquoi elle n'est chez aucun d'eux.
//
// Fichier volontairement minuscule. Deux autres semeurs existent — `SpawnCastEntity_0047AAE0`
// (`Spells/SpellCasting.cs`) et `SpawnStatusEffectEntity_0047AC40`
// (`Tick/WorldSimulationStep.cs`) — mais chacun a un seul appelant naturel, et les deplacer ici
// serait un regroupement de confort, pas une unite reelle.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>Ghidra: FUN_0050f070 @ 0x0050f070 — <b>pose une particule</b> dans le pool de 64
    /// emplacements de <c>0x005e9b60</c>, pas <c>0x28</c>.
    ///
    /// <para>Le mot d'etat de <c>+0x24</c> empile trois choses : <c>0x14</c> en octet haut, qui est
    /// le compte a rebours que <c>FUN_0050f0d0</c> decremente par image, le genre en octet 2, et
    /// l'indice de l'emplacement en octet 1. Le pool plein ne fait rien du tout.</para></summary>
    private static void SpawnParticle_0050F070(uint* position, uint kind, byte flavour)
    {
        int index = AllocEntitySlot((ushort*)Mem.Arena(0x005e99e0));   // Ghidra: FUN_00532740
        if (index < 0) return;

        byte* entry = Mem.Arena(0x005e9b60) + index * 0x28;

        *(ushort*)(entry + 0x0c) = (ushort)kind;
        *(uint*)(entry + 0x24) = ((0x1400u | flavour) << 8 | (uint)(index & 0xff)) << 8;

        *(uint*)(entry + 0) = position[0];
        *(uint*)(entry + 4) = position[1];
        *(uint*)(entry + 8) = position[2];
    }
}
