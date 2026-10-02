// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES 48 EFFETS DE LA TABLE 0x0056d4e0 — 0x0053afb0 a 0x0053cdf0.
//
// LE SQUELETTE COMMUN, celui des entrees 0 a 33 :
//   1. `PrepareCombatPair_00538870` appareille (voir `CombatPairing.cs` : appel a deux sorties) ;
//   2. un masque d'immunite lu en +0x14 du bloc de la victime, quand l'effet en a un. Pose,
//      l'effet echoue en faisant naitre le sous-type 0xbf — la marque du coup sans effet ;
//   3. un jet de sauvegarde, 0x0053A6C0 ou 0x0053A790 selon l'effet, quand il en a un ;
//   4. une duree calculee sur une statistique de l'assaillant, DECALEE et decalee seulement
//      (jamais divisee), rangee en +0x80..+0xc2 du bloc de la victime SI elle depasse ce qui s'y
//      trouve — un effet deja pose n'est jamais raccourci, seulement rallonge ;
//   5. un bit d'etat en +0x40, et c'est SA POSE, pas la duree, qui declenche la marque visible :
//      le sous-type 0x29 ne nait qu'a la premiere fois.
//
// Les entrees 34 a 42 sont les LEVEES : meme appareillage, un bit efface, sa duree remise a zero.
// Les entrees 43 a 47 touchent aux points de vie.
//
// CES 48 CORPS RESTENT 48 FONCTIONS. Ils ne different pas seulement par des constantes — les uns
// ont un masque d'immunite et pas les autres, le jet change de famille, cinq d'entre eux font
// naitre une seconde entite. Les factoriser demanderait une table a huit colonnes dont trois
// seraient des drapeaux de presence : moins lisible que les corps, et ce ne serait plus ce que le
// binaire contient.
//
// Ce qu'ils font VOIR est dans `Effects/StatusMarkers.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    // ------------------------------------------------------------------ 0 a 2 : sans jet, sans marque

    /// <summary>Ghidra: FUN_0053afb0 @ 0x0053afb0 — <b>l'effet 0</b>. Duree
    /// <c>attaque/4 + 0x19</c> en <c>+0x80</c>, bit <c>0x8000000</c>. Ni masque d'immunite, ni jet
    /// de sauvegarde, ni marque visible : il se pose toujours.</summary>
    private static uint ApplyStatusEffect_0053AFB0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 2) + 0x19;
        if (*(ushort*)(victimBlock + 0x80) < duration)
        {
            *(short*)(victimBlock + 0x80) = (short)duration;
        }

        *(uint*)(victimBlock + 0x40) |= 0x8000000;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053b020 @ 0x0053b020 — <b>l'effet 1</b>, la jumelle de
    /// <see cref="ApplyStatusEffect_0053AFB0"/> : duree <c>attaque/4 + 0x3c</c> en <c>+0x82</c>,
    /// bit <c>0x2000000</c>.</summary>
    private static uint ApplyStatusEffect_0053B020(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 1, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 2) + 0x3c;
        if (*(ushort*)(victimBlock + 0x82) < duration)
        {
            *(short*)(victimBlock + 0x82) = (short)duration;
        }

        *(uint*)(victimBlock + 0x40) |= 0x2000000;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053b090 @ 0x0053b090 — <b>l'effet 2</b>, la troisieme de la meme
    /// forme : duree <c>attaque/4 + 0x3c</c> en <c>+0x84</c>, bit <c>0x4000000</c>.</summary>
    private static uint ApplyStatusEffect_0053B090(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 2, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 2) + 0x3c;
        if (*(ushort*)(victimBlock + 0x84) < duration)
        {
            *(short*)(victimBlock + 0x84) = (short)duration;
        }

        *(uint*)(victimBlock + 0x40) |= 0x4000000;
        return 0;
    }

    // ------------------------------------------------------------------ 3 a 6 : les quatre a intensite

    /// <summary>
    /// Ghidra: FUN_0053b100 @ 0x0053b100 — <b>l'effet 3</b>, premier des quatre qui rangent une
    /// <b>intensite</b> a cote de leur duree.
    ///
    /// <para>
    /// L'intensite est <c>bloc[+0x2a] / 24 + fiche[+0x18]</c>, et <b>elle n'est ecrite que quand la
    /// duree l'est</b> — les deux affectations sont dans le meme <c>if</c>. Un effet plus court que
    /// celui deja pose ne change donc rien du tout, pas meme l'intensite.
    /// </para>
    /// <para>
    /// <b>La division par 24 se lit dans le binaire, pas dans <c>Blaze.c</c>.</b> Ghidra en rend
    /// les trois morceaux separement (<c>x / 0x18</c>, <c>x &gt;&gt; 0xf</c>, et un terme en
    /// <c>&gt;&gt; 0x3f</c>) sans les recomposer, ce qui se lit comme une formule a trois termes
    /// alors que c'en est une seule. Le desassembleur est sans ambiguite :
    /// <c>imul 0x2AAAAAAB ; sar edx,2 ; shr eax,31 ; add</c> est la division signee par 24, celle
    /// qui tronque vers zero — ce que <c>/</c> fait deja en C#. Et la fiche est lue en
    /// <b>non signe</b> (<c>movzx</c>), pas en <c>short</c> comme Ghidra la declare.
    /// </para>
    /// </summary>
    private static uint ApplyStatusEffect_0053B100(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 3, keep) != 0)
        {
            return 1;
        }

        int magnitude = *(short*)(attackerBlock + 0x2a) / 24
                        + *(ushort*)(SpellSheetOf(attacker) + 0x18);

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x28;
        if (*(ushort*)(victimBlock + 0x86) < duration)
        {
            *(short*)(victimBlock + 0x86) = (short)duration;
            *(short*)(victimBlock + 0xe4) = (short)magnitude;
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053b1b0 @ 0x0053b1b0 — <b>l'effet 4</b>, la meme que
    /// <see cref="ApplyStatusEffect_0053B100"/> en <c>+0x88</c> / <c>+0xe6</c>.</summary>
    private static uint ApplyStatusEffect_0053B1B0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 4, keep) != 0)
        {
            return 1;
        }

        int magnitude = *(short*)(attackerBlock + 0x2a) / 24
                        + *(ushort*)(SpellSheetOf(attacker) + 0x18);

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x28;
        if (*(ushort*)(victimBlock + 0x88) < duration)
        {
            *(short*)(victimBlock + 0x88) = (short)duration;
            *(short*)(victimBlock + 0xe6) = (short)magnitude;
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053b260 @ 0x0053b260 — <b>l'effet 5</b>, en <c>+0x8a</c> /
    /// <c>+0xe8</c>.</summary>
    private static uint ApplyStatusEffect_0053B260(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 5, keep) != 0)
        {
            return 1;
        }

        int magnitude = *(short*)(attackerBlock + 0x2a) / 24
                        + *(ushort*)(SpellSheetOf(attacker) + 0x18);

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x28;
        if (*(ushort*)(victimBlock + 0x8a) < duration)
        {
            *(short*)(victimBlock + 0x8a) = (short)duration;
            *(short*)(victimBlock + 0xe8) = (short)magnitude;
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053b310 @ 0x0053b310 — <b>l'effet 6</b>, en <c>+0x8c</c> /
    /// <c>+0xea</c>. Le dernier des quatre.</summary>
    private static uint ApplyStatusEffect_0053B310(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 6, keep) != 0)
        {
            return 1;
        }

        int magnitude = *(short*)(attackerBlock + 0x2a) / 24
                        + *(ushort*)(SpellSheetOf(attacker) + 0x18);

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x28;
        if (*(ushort*)(victimBlock + 0x8c) < duration)
        {
            *(short*)(victimBlock + 0x8c) = (short)duration;
            *(short*)(victimBlock + 0xea) = (short)magnitude;
        }

        return 0;
    }

    // ------------------------------------------------------------------ 11 a 14 : avec masque et jet

    /// <summary>Ghidra: FUN_0053b730 @ 0x0053b730 — <b>l'effet 11</b>. Masque d'immunite
    /// <c>0xfbaf9500</c> — le plus large de la famille —, jet B, duree
    /// <c>defense/8 + 0x1a</c> en <c>+0x96</c>, bit <c>0x40</c>. Sa premiere fois fait naitre
    /// <b>deux</b> entites : la marque <c>0x29</c> et un effet <c>0x47</c>.</summary>
    private static uint ApplyStatusEffect_0053B730(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xb, keep) != 0)
        {
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce() && (*(uint*)(victimBlock + 0x14) & 0xfbaf9500) != 0)   // Couture legacy ++ (LP-015)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce()   // Couture legacy ++ (LP-015)
            && RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x3c) >> 3) + 0x1a;
        if (*(ushort*)(victimBlock + 0x96) < duration)
        {
            *(short*)(victimBlock + 0x96) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x40) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x40;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564818), 0x29, 0, 0);
            SpawnEffectEntity((byte*)victim, 0x47, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053b810 @ 0x0053b810 — <b>l'effet 12</b>. Masque <c>0xfb099500</c>,
    /// jet A, duree <c>attaque/32 + 5</c> en <c>+0x98</c> — la duree la plus courte de la table —,
    /// bit <c>0x80</c>, plus un effet <c>0x46</c> de saveur 2.</summary>
    private static uint ApplyStatusEffect_0053B810(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xc, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0xfb099500) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x34) >> 5) + 5;
        if (*(ushort*)(victimBlock + 0x98) < duration)
        {
            *(short*)(victimBlock + 0x98) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x80) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x80;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005648b0), 0x29, 0, 0);
            SpawnEffectEntity((byte*)victim, 0x46, 0, 2);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053b8f0 @ 0x0053b8f0 — <b>l'effet 13</b>. Masque <c>0xf3099500</c>,
    /// jet A, duree <c>attaque/8 + 6</c> en <c>+0x9a</c>, bit <c>0x100</c>.</summary>
    private static uint ApplyStatusEffect_0053B8F0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xd, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0xf3099500) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x34) >> 3) + 6;
        if (*(ushort*)(victimBlock + 0x9a) < duration)
        {
            *(short*)(victimBlock + 0x9a) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x100) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x100;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564820), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053b9c0 @ 0x0053b9c0 — <b>l'effet 14</b>. Masque <c>0xfb299500</c>,
    /// jet B, duree <c>defense/4 + 0x42</c> en <c>+0x9c</c>, bit <c>0x200</c>.</summary>
    private static uint ApplyStatusEffect_0053B9C0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xe, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0xfb299500) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x3c) >> 2) + 0x42;
        if (*(ushort*)(victimBlock + 0x9c) < duration)
        {
            *(short*)(victimBlock + 0x9c) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x200) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x200;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564810), 0x29, 0, 0);
        }

        return 0;
    }

    // ------------------------------------------------------------------ 15 a 28 : sans masque d'immunite

    /// <summary>Ghidra: FUN_0053ba90 @ 0x0053ba90 — <b>l'effet 15</b>. Ni masque ni jet : duree
    /// <c>attaque/8 + 10</c> en <c>+0x9e</c>, bit <c>0x400</c>.</summary>
    private static uint ApplyStatusEffect_0053BA90(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xf, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0x9e) < duration)
        {
            *(short*)(victimBlock + 0x9e) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x400) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x400;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564850), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053bb20 @ 0x0053bb20 — <b>l'effet 16</b>. Pas de masque, mais un
    /// jet A : duree <c>attaque/8 + 10</c> en <c>+0xa0</c>, bit <c>0x800</c>.</summary>
    private static uint ApplyStatusEffect_0053BB20(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x10, keep) != 0)
        {
            return 1;
        }

        if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xa0) < duration)
        {
            *(short*)(victimBlock + 0xa0) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x800) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x800;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564860), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053bbd0 @ 0x0053bbd0 — <b>l'effet 17</b>. Ni masque ni jet : duree
    /// <c>attaque/8 + 10</c> en <c>+0xa2</c>, bit <c>0x1000</c>.</summary>
    private static uint ApplyStatusEffect_0053BBD0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x11, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xa2) < duration)
        {
            *(short*)(victimBlock + 0xa2) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x1000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x1000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564848), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053bc60 @ 0x0053bc60 — <b>l'effet 18</b>. Jet A, duree
    /// <c>attaque/8 + 10</c> en <c>+0xa4</c>, bit <c>0x2000</c>.</summary>
    private static uint ApplyStatusEffect_0053BC60(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x12, keep) != 0)
        {
            return 1;
        }

        if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xa4) < duration)
        {
            *(short*)(victimBlock + 0xa4) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x2000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x2000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564858), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053bd10 @ 0x0053bd10 — <b>l'effet 19</b>. Ni masque ni jet : duree
    /// <c>attaque/8 + 0x28</c> en <c>+0xa6</c>, bit <c>0x4000</c>.</summary>
    private static uint ApplyStatusEffect_0053BD10(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x13, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x28;
        duration = LegacyPlus.Coutures.DureeDEtat(duration);   // Couture legacy ++ (LP-015)
        if (*(ushort*)(victimBlock + 0xa6) < duration)
        {
            *(short*)(victimBlock + 0xa6) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x4000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x4000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564840), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053bda0 @ 0x0053bda0 — <b>l'effet 20</b>. Son masque d'immunite est
    /// <c>0x80000000</c> — <b>un seul bit</b>, le plus etroit de la table. Jet A, duree
    /// <c>attaque/8 + 0x28</c> en <c>+0xa8</c>, bit <c>0x8000</c>.</summary>
    private static uint ApplyStatusEffect_0053BDA0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x14, keep) != 0)
        {
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce() && (*(uint*)(victimBlock + 0x14) & 0x80000000) != 0)   // Couture legacy ++ (LP-015)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce()   // Couture legacy ++ (LP-015)
            && RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x28;
        duration = LegacyPlus.Coutures.DureeDEtat(duration);   // Couture legacy ++ (LP-015)
        if (*(ushort*)(victimBlock + 0xa8) < duration)
        {
            *(short*)(victimBlock + 0xa8) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x8000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x8000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564838), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053be70 @ 0x0053be70 — <b>l'effet 21</b>. Duree
    /// <c>attaque/8 + 10</c> en <c>+0xaa</c>, bit <c>0x10000</c>.</summary>
    private static uint ApplyStatusEffect_0053BE70(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x15, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xaa) < duration)
        {
            *(short*)(victimBlock + 0xaa) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x10000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x10000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564868), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053bf00 @ 0x0053bf00 — <b>l'effet 22</b>. Jet B, duree
    /// <c>attaque/8 + 10</c> en <c>+0xac</c>, bit <c>0x20000</c>.</summary>
    private static uint ApplyStatusEffect_0053BF00(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x16, keep) != 0)
        {
            return 1;
        }

        if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xac) < duration)
        {
            *(short*)(victimBlock + 0xac) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x20000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x20000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564878), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053bfb0 @ 0x0053bfb0 — <b>l'effet 23</b>. Duree
    /// <c>attaque/8 + 10</c> en <c>+0xae</c>, bit <c>0x40000</c>.</summary>
    private static uint ApplyStatusEffect_0053BFB0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x17, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xae) < duration)
        {
            *(short*)(victimBlock + 0xae) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x40000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x40000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564880), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053c040 @ 0x0053c040 — <b>l'effet 24</b>. Jet B, duree
    /// <c>attaque/8 + 10</c> en <c>+0xb0</c>, bit <c>0x80000</c>.</summary>
    private static uint ApplyStatusEffect_0053C040(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x18, keep) != 0)
        {
            return 1;
        }

        if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xb0) < duration)
        {
            *(short*)(victimBlock + 0xb0) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x80000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x80000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564870), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053c0f0 @ 0x0053c0f0 — <b>l'effet 25</b>, duree <c>attaque/8 + 0x19</c> en
    /// <c>+0xb2</c>, bit <c>0x100000</c>.
    ///
    /// <para><b>Le seul de la famille a poser un bit sans faire naitre de marque</b>, et le bit
    /// <c>0x100000</c> est justement celui que les guerisons interrogent pour refuser leur soin.
    /// L'effet est donc invisible sur le moment et se voit ailleurs.</para>
    /// </summary>
    private static uint ApplyStatusEffect_0053C0F0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x19, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x19;
        if (*(ushort*)(victimBlock + 0xb2) < duration)
        {
            *(short*)(victimBlock + 0xb2) = (short)duration;
        }

        // Ghidra: le `if` du binaire est conserve tel quel, bien qu'un OU soit idempotent.
        if ((*(uint*)(victimBlock + 0x40) & 0x100000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x100000;
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053c160 @ 0x0053c160 — <b>l'effet 26</b>. Duree
    /// <c>attaque/8 + 10</c> en <c>+0xb4</c>, bit <c>0x200000</c>.</summary>
    private static uint ApplyStatusEffect_0053C160(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x1a, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xb4) < duration)
        {
            *(short*)(victimBlock + 0xb4) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x200000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x200000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564888), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053c1f0 @ 0x0053c1f0 — <b>l'effet 27</b>. Jet B, duree
    /// <c>attaque/8 + 10</c> en <c>+0xb6</c>, et son bit est <c>0x10000000</c> : il rompt la suite
    /// des puissances de deux consecutives, la place naturelle etant deja prise.</summary>
    private static uint ApplyStatusEffect_0053C1F0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x1b, keep) != 0)
        {
            return 1;
        }

        if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 10;
        if (*(ushort*)(victimBlock + 0xb6) < duration)
        {
            *(short*)(victimBlock + 0xb6) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x10000000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x10000000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564800), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053c2a0 @ 0x0053c2a0 — <b>l'effet 28</b>. Duree
    /// <c>attaque/8 + 0x19</c> en <c>+0xb8</c>, bit <c>0x400000</c>.</summary>
    private static uint ApplyStatusEffect_0053C2A0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x1c, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 3) + 0x19;
        if (*(ushort*)(victimBlock + 0xb8) < duration)
        {
            *(short*)(victimBlock + 0xb8) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x400000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x400000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564898), 0x29, 0, 0);
        }

        return 0;
    }

    // ------------------------------------------------------------------ 29 : la grande levee

    /// <summary>
    /// Ghidra: FUN_0053c330 @ 0x0053c330 — <b>l'effet 29</b> : la <b>levee generale</b>, celle qui
    /// efface d'un coup les dix etats de <c>+0x9e</c> a <c>+0xb0</c>.
    ///
    /// <para>
    /// <b>Elle n'appareille pas les deux combattants comme les autres</b> : elle appelle
    /// <see cref="ValidateCombatants_0053A860"/> puis fait le travail de
    /// <see cref="PrepareCombatPair_00538870"/> a la main, et seulement pour la victime — le bloc
    /// de l'assaillant ne lui sert a rien. Le marqueur qu'elle passe est <c>0xff</c>, celui des
    /// effets qui ne sont pas un coup.
    /// </para>
    /// <para>
    /// L'appariement lui-meme differe aussi : l'assaillant retient sa cible en <c>+0x70</c> et non
    /// en <c>+0x70</c> de la victime — c'est la seule de la table a ecrire ce champ-la.
    /// </para>
    /// <para>
    /// <b>Le bit <c>0x100000</c> la refuse</b>, comme il refuse les guerisons : elle fait alors
    /// naitre la marque <c>0x29</c> de l'atlas <c>0x005648a0</c> et rend 1.
    /// </para>
    /// </summary>
    private static uint ClearAllStatusEffects_0053C330(uint* attacker, uint* victim, int keep)
    {
        if (ValidateCombatants_0053A860(attacker, victim) != 0) return 1;

        *(uint**)((byte*)attacker + 0x70) = victim;
        victim[0x1c] = (uint)attacker;

        byte* victimBlock = (*victim & 0x800000) == 0
            ? ResolveSceneCombatBlock_00538830(victim, 0xff)
            : ResolvePartyCombatBlock_005387C0(victim, 0xff, keep);

        if ((*(uint*)(victimBlock + 0x40) & 0x100000) != 0)
        {
            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005648a0), 0x29, 0, 0);
            return 1;
        }

        if ((*victim & 0x800000) != 0) TriggerEquippedItems_005238E0(victim, 0x11);

        // 0xfff003ff : les dix bits 0x400 a 0x80000 tombent ensemble.
        *(uint*)(victimBlock + 0x40) &= 0xfff003ff;

        // Ghidra les ecrit dans un ordre disperse (0xa2, 0x9e, 0xaa, 0xae, 0xa4, 0xa0, 0xac, 0xb0,
        // 0xa6, 0xa8) : c'est l'ordonnancement du compilateur, pas un sens. Remis en ordre.
        for (int at = 0x9e; at <= 0xb0; at += 2)
        {
            *(short*)(victimBlock + at) = 0;
        }

        return 0;
    }

    // ------------------------------------------------------------------ 31 a 33

    /// <summary>Ghidra: FUN_0053c510 @ 0x0053c510 — <b>l'effet 31</b>. Masque <c>0xfb099500</c>,
    /// jet B, duree <c>defense/4 + 6</c> en <c>+0xe2</c>, bit <c>2</c>.</summary>
    private static uint ApplyStatusEffect_0053C510(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x1e, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0xfb099500) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x3c) >> 2) + 6;
        if (*(ushort*)(victimBlock + 0xe2) < duration)
        {
            *(short*)(victimBlock + 0xe2) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 2) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 2;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005647c8), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053c5e0 @ 0x0053c5e0 — <b>l'effet 32</b>, masque <c>0xf3099500</c>, jet A,
    /// duree <c>defense/4 + 0x24</c> en <c>+0xc0</c>.
    ///
    /// <para><b>Le seul qui compte au lieu de poser un bit</b> : sa marque visible depend d'un
    /// compteur en <c>+0xec</c>, incremente tant qu'il reste sous 3. L'effet s'empile donc jusqu'a
    /// trois fois, et trois marques coexistent — la ou tous les autres n'en font naitre
    /// qu'une.</para>
    /// </summary>
    private static uint ApplyStatusEffect_0053C5E0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x1f, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0xf3099500) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x3c) >> 2) + 0x24;
        if (*(ushort*)(victimBlock + 0xc0) < duration)
        {
            *(short*)(victimBlock + 0xc0) = (short)duration;
        }

        if (*(ushort*)(victimBlock + 0xec) < 3)
        {
            *(ushort*)(victimBlock + 0xec) += 1;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005647f8), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>Ghidra: FUN_0053c6b0 @ 0x0053c6b0 — <b>l'effet 33</b>, le dernier des poseurs.
    /// Duree <c>attaque/4 + 0x1e</c> en <c>+0xc2</c>, bit <c>0x1000000</c>.</summary>
    private static uint ApplyStatusEffect_0053C6B0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x20, keep) != 0)
        {
            return 1;
        }

        int duration = (*(short*)(attackerBlock + 0x38) >> 2) + 0x1e;
        if (*(ushort*)(victimBlock + 0xc2) < duration)
        {
            *(short*)(victimBlock + 0xc2) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x1000000) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x1000000;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x00564828), 0x29, 0, 0);
        }

        return 0;
    }

    // ------------------------------------------------------------------ 34 a 42 : les levees
    //
    // Toutes de la meme forme, et toutes avec le marqueur 0xff : appareiller, effacer un bit du
    // mot d'etat, remettre la duree correspondante a zero. Aucune ne fait naitre quoi que ce soit
    // et aucune ne peut echouer une fois l'appareillage passe.

    /// <summary>Ghidra: FUN_0053c740 @ 0x0053c740 — <b>leve l'effet du bit 1</b> (<c>+0xe0</c>),
    /// celui que <see cref="ApplyTrapEffect_0053C420"/> pose.</summary>
    private static uint ClearStatusEffect_0053C740(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xfffffffe;
        *(short*)(victimBlock + 0xe0) = 0;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053c790 @ 0x0053c790 — <b>leve l'effet du bit 4</b> (<c>+0x8e</c>),
    /// celui de <see cref="ApplyStatusEffect_0053B3C0"/>.</summary>
    private static uint ClearStatusEffect_0053C790(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xfffffffb;
        *(short*)(victimBlock + 0x8e) = 0;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053c7e0 @ 0x0053c7e0 — <b>leve l'effet du bit 2</b>
    /// (<c>+0xe2</c>).</summary>
    private static uint ClearStatusEffect_0053C7E0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xfffffffd;
        *(short*)(victimBlock + 0xe2) = 0;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053c830 @ 0x0053c830 — <b>leve deux effets a la fois</b>, les bits
    /// <c>8</c> et <c>0x200</c> (<c>+0x90</c> et <c>+0x9c</c>). Le masque <c>0xfffffdf7</c> les
    /// efface d'un seul ET.</summary>
    private static uint ClearStatusEffect_0053C830(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xfffffdf7;
        *(short*)(victimBlock + 0x90) = 0;
        *(short*)(victimBlock + 0x9c) = 0;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053c890 @ 0x0053c890 — <b>leve l'effet du bit 0x100</b>
    /// (<c>+0x9a</c>).</summary>
    private static uint ClearStatusEffect_0053C890(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xfffffeff;
        *(short*)(victimBlock + 0x9a) = 0;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053c8e0 @ 0x0053c8e0 — <b>leve l'effet du bit 0x10</b>
    /// (<c>+0x92</c>).</summary>
    private static uint ClearStatusEffect_0053C8E0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xffffffef;
        *(short*)(victimBlock + 0x92) = 0;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053c930 @ 0x0053c930 — <b>leve l'effet du bit 0x20</b>
    /// (<c>+0x94</c>).</summary>
    private static uint ClearStatusEffect_0053C930(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xffffffdf;
        *(short*)(victimBlock + 0x94) = 0;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053c980 @ 0x0053c980 — <b>la levee des neuf</b> : le masque
    /// <c>0xfffffc80</c> efface les bits <c>0x37f</c> d'un coup, et neuf durees tombent avec eux.
    ///
    /// <para><b>Le bit <c>0x80</c> survit</b>, alors qu'il est dans la plage — c'est l'effet 12,
    /// celui de <see cref="ApplyStatusEffect_0053B810"/>. La levee generale n'est donc pas
    /// generale, et l'exception est ecrite dans la constante.</para>
    /// </summary>
    private static uint ClearStatusEffect_0053C980(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(uint*)(victimBlock + 0x40) &= 0xfffffc80;

        *(short*)(victimBlock + 0xe0) = 0;
        *(short*)(victimBlock + 0x8e) = 0;
        *(short*)(victimBlock + 0xe2) = 0;
        *(short*)(victimBlock + 0x90) = 0;
        *(short*)(victimBlock + 0x9a) = 0;
        *(short*)(victimBlock + 0x92) = 0;
        *(short*)(victimBlock + 0x94) = 0;
        *(short*)(victimBlock + 0x9c) = 0;
        *(short*)(victimBlock + 0x96) = 0;
        return 0;
    }

    /// <summary>Ghidra: FUN_0053ca30 @ 0x0053ca30 — <b>la levee de l'effet 32</b> : elle ne touche
    /// aucun bit, seulement la duree <c>+0xc0</c> et le <b>compteur</b> <c>+0xec</c>, puisque
    /// <see cref="ApplyStatusEffect_0053C5E0"/> compte au lieu de poser un bit.</summary>
    private static uint ClearStatusEffect_0053CA30(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(short*)(victimBlock + 0xc0) = 0;
        *(short*)(victimBlock + 0xec) = 0;
        return 0;
    }

    // ------------------------------------------------------------------ 43 a 47 : les points de vie

    /// <summary>
    /// Ghidra: FUN_0053ca80 @ 0x0053ca80 — <b>la guerison</b>, entree 43 : celle de la potion.
    ///
    /// <para>
    /// Le montant est <c>fiche[+0x18] + bloc[+0x24]/8</c> — la valeur de base de la fiche de sort
    /// ou d'objet, plus le huitieme d'une statistique de celui qui s'en sert. Il s'ajoute aux
    /// points de vie courants <c>+0x4c</c>, <b>plafonnes au maximum</b> <c>+0x48</c>.
    /// </para>
    /// <para>
    /// Les deux ecritures qui suivent sont l'<b>affichage</b> : <c>+0x102</c> a 1 arme le nombre
    /// flottant au-dessus de la cible, <c>+0x100</c> lui donne sa valeur. C'est le meme couple que
    /// les degats emploient, avec un montant positif.
    /// </para>
    /// <para>
    /// <b>Le plafond est applique apres coup, pas avant</b> : le nombre affiche est le montant
    /// entier, meme quand la moitie a ete perdue au plafond. Fidele.
    /// </para>
    /// </summary>
    private static uint HealTarget_0053CA80(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        int amount = *(ushort*)(SpellSheetOf(attacker) + 0x18)
                     + (*(short*)(attackerBlock + 0x24) >> 3);

        victimBlock[0x102] = 1;
        *(short*)(victimBlock + 0x100) = (short)amount;
        *(short*)(victimBlock + 0x4c) += (short)amount;

        if (*(short*)(victimBlock + 0x48) < *(short*)(victimBlock + 0x4c))
        {
            *(short*)(victimBlock + 0x4c) = *(short*)(victimBlock + 0x48);
        }

        SpawnParticle_0050F070((uint*)((byte*)victim + 0x1c), (uint)amount, 1);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053cb30 @ 0x0053cb30 — <b>la guerison partagee</b>, entree 44 : le meme soin
    /// que <see cref="HealTarget_0053CA80"/>, mais <b>divise par le nombre de soignables</b>.
    ///
    /// <para>
    /// Elle refait a la main l'appareillage de
    /// <see cref="PrepareCombatPair_00538870"/> — c'est la seule facon d'obtenir au passage la
    /// <b>liste</b> sur laquelle compter : celle du groupe si l'assaillant en est
    /// (<see cref="s_PartySize_005e9020"/> entrees), sinon les <c>0x20</c> entites de scene.
    /// </para>
    /// <para>
    /// Le diviseur compte les vivants dont le bloc ne porte pas <c>0x100000</c> — le meme bit qui
    /// refuse le soin individuel. <b>Resoudre le bloc de chacun pour le compter n'est pas gratuit</b> :
    /// la boucle rebase les statistiques de toute la liste au passage, effet de bord du binaire
    /// qui est conserve.
    /// </para>
    /// <para>
    /// Le montant est <c>fiche[+0x18] + bloc[+0x24]/4</c> — un <b>quart</b> la ou le soin simple
    /// prend un huitieme — puis divise. La division est faite sans garde : si le compte est nul,
    /// l'original faute, et la transcription leve.
    /// </para>
    /// </summary>
    private static uint HealPartyShare_0053CB30(uint* attacker, uint* victim, int keep)
    {
        if (ValidateCombatants_0053A860(attacker, victim) != 0) return 1;

        attacker[0x1c] = (uint)victim;
        victim[0x1c] = (uint)attacker;

        byte* attackerBlock;
        byte* roster;
        int remaining;

        if ((*attacker & 0x800000) == 0)
        {
            attackerBlock = ResolveSceneCombatBlock_00538830(attacker, 0xff);
            remaining = 0x20;
            roster = Mem.Entity(0x005ecaa0);
        }
        else
        {
            attackerBlock = ResolvePartyCombatBlock_005387C0(attacker, 0xff, keep);
            roster = Inp.s_SlotFlags_005e93c0;
            remaining = s_PartySize_005e9020;
        }

        byte* victimBlock = (*victim & 0x800000) == 0
            ? ResolveSceneCombatBlock_00538830(victim, 0xff)
            : ResolvePartyCombatBlock_005387C0(victim, 0xff, keep);

        if ((*attacker & 0x800000) != 0)
        {
            if (keep == 0) *(uint*)Mem.Arena(0x005e99c0) = 0;
            TriggerEquippedItems_005238E0(attacker, 7);
        }

        if ((*victim & 0x800000) != 0)
        {
            if (keep == 0) *(uint*)Mem.Arena(0x005e99c0) = 0;
            TriggerEquippedItems_005238E0(victim, 0x11);
        }

        if ((*victim & 0x600000) == 0x600000) return 1;

        if ((*(uint*)(victimBlock + 0x40) & 0x100000) != 0)
        {
            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005648a0), 0x29, 0, 0);
            return 1;
        }

        int share = 0;
        for (byte* slot = roster; remaining != 0; remaining--, slot += 0x9c)
        {
            uint flags = *(uint*)slot;
            if ((flags & 0x80000000) == 0 || (flags & 0x50000000) != 0) continue;

            byte* block = (flags & 0x800000) == 0
                ? ResolveSceneCombatBlock_00538830((uint*)slot, 0xff)
                : ResolvePartyCombatBlock_005387C0((uint*)slot, 0xff, keep);

            if ((*(uint*)(block + 0x40) & 0x100000) == 0) share++;
        }

        int amount = (*(ushort*)(SpellSheetOf(attacker) + 0x18)
                      + (*(short*)(attackerBlock + 0x24) >> 2)) / share;

        victimBlock[0x102] = 1;
        *(short*)(victimBlock + 0x4c) += (short)amount;
        *(short*)(victimBlock + 0x100) = (short)amount;

        if (*(short*)(victimBlock + 0x48) < *(short*)(victimBlock + 0x4c))
        {
            *(short*)(victimBlock + 0x4c) = *(short*)(victimBlock + 0x48);
        }

        SpawnParticle_0050F070((uint*)((byte*)victim + 0x1c), (uint)amount, 1);
        return 0;
    }

    /// <summary>Ghidra: FUN_0053cd20 @ 0x0053cd20 — <b>la guerison complete</b>, entree 45 : les
    /// points de vie passent au maximum, et le nombre affiche est <b>ce qui manquait</b>. Elle ne
    /// fait naitre aucune particule, contrairement aux deux precedentes.</summary>
    private static uint HealTargetFully_0053CD20(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        victimBlock[0x102] = 1;
        *(short*)(victimBlock + 0x100) =
            (short)(*(short*)(victimBlock + 0x48) - *(short*)(victimBlock + 0x4c));
        *(short*)(victimBlock + 0x4c) = *(short*)(victimBlock + 0x48);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053cd80 @ 0x0053cd80 — <b>la resurrection</b>, entree 46.
    ///
    /// <para>
    /// Points de vie au maximum, le <b>double mot</b> <c>+0x18</c> de l'entite remis a zero
    /// (<c>mov dword [esi+18h],0</c>, verifie au desassembleur), le bit
    /// <c>0x40000000</c> — la mort — retire du bloc, et le mot d'etat de l'entite refait :
    /// <c>&amp; 0xbfdfdf79 | 0x1000000</c>. <b>Le ET et le OU sont dans cet ordre</b> et le bit
    /// pose n'est pas dans le masque efface, donc il survit ; l'ordre inverse donnerait autre
    /// chose.
    /// </para>
    /// </summary>
    private static uint ReviveTarget_0053CD80(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        *(short*)(victimBlock + 0x4c) = *(short*)(victimBlock + 0x48);
        victim[6] = 0;
        *(uint*)(victimBlock + 0x40) &= 0xbfffffff;
        *victim = *victim & 0xbfdfdf79 | 0x1000000;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053cdf0 @ 0x0053cdf0 — <b>la mort immediate</b>, entree 47 et derniere.
    ///
    /// <para>
    /// <b>Deux jets de sauvegarde, l'un apres l'autre</b> — c'est la seule de la table a en
    /// demander deux, et il faut passer les deux. Ils ne sont tentes que si la cible a encore des
    /// points de vie : sur un corps deja a zero, l'effet s'applique sans jet.
    /// </para>
    /// </summary>
    private static uint SlayTarget_0053CDF0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0xff, keep) != 0)
        {
            return 1;
        }

        if (*(short*)(victimBlock + 0x4c) != 0)
        {
            if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0)
            {
                return 1;
            }

            if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0)
            {
                return 1;
            }
        }

        *victim |= 0x40000000;
        *(uint*)(victimBlock + 0x40) |= 0x40000000;
        *(short*)(victimBlock + 0x4c) = 0;
        return 0;
    }
}
