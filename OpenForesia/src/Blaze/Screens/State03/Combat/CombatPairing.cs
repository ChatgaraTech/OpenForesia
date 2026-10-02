// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// L'APPARIEMENT DE COMBAT — ce qui doit arriver AVANT qu'un effet touche quelqu'un.
//
// `PrepareCombatPair_00538870` appareille les deux combattants et resout leurs deux blocs de
// statistiques. C'EST UN APPEL A DEUX SORTIES ECRITES PAR ADRESSE : dans l'original, les
// parametres 2 et 3 sont passes par pointeur et RECOIVENT les blocs, si bien que `param_2` cesse
// d'etre la victime pour devenir le bloc de l'assaillant, et `param_3` cesse d'etre le drapeau
// pour devenir le bloc de la victime. Les corps qui ont encore besoin de l'entite victime la
// sauvent AVANT l'appel. C'est le piege de lecture numero un de tout ce repertoire.
//
// `ResolvePartyCombatBlock_005387C0` rend `fiche + 0x100` : le bloc de combat et la fiche de
// personnage sont LA MEME MEMOIRE vue 0x100 octets plus bas. Le bloc a ses PV en +0x4c et son mot
// d'etat en +0x40 ; la fiche les a en +0x14c et +0x140. L'herboristerie (`HerbEffects.cs`) ecrit
// par la fiche, les effets d'objet par le bloc.
//
// Les deux familles de jet de sauvegarde (0x0053A6C0 et 0x0053A790) sont ici aussi : l'effet
// choisit la sienne.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_005387c0 @ 0x005387c0 — <b>le bloc de combat d'un membre du groupe</b>. Il vit
    /// dans la memoire principale, a <c>(emplacement[8] + 0x78) * 0x2000</c>, et la fonction rend
    /// sa <b>seconde moitie</b> (<c>+0x100</c>) : c'est celle-la que la resolution du coup lit.
    ///
    /// <para>
    /// <c>keep != 0</c> saute entierement le recalcul et se contente de poser la marque. C'est ce
    /// qui permet a une chaine de coups successifs de ne recalculer qu'une fois.
    /// </para>
    /// </summary>
    private static byte* ResolvePartyCombatBlock_005387C0(uint* slot, byte marker, int keep)
    {
        byte* block = GetMainMemPtr() + (*((byte*)slot + 8) + 0x78) * 0x2000;

        if (keep == 0)
        {
            CommitStagedStatBlock(block);
            ScaleStatBlockByDifficulty(block + 0x100);
            ComputeDerivedStats(block);
            ApplyEquipmentStatBonuses(block);
            FinalizeEquipmentEffects(block + 0x100);
        }

        block[0x203] = marker;
        return block + 0x100;
    }

    /// <summary>
    /// Ghidra: FUN_00538830 @ 0x00538830 — <b>le bloc de combat d'une entite de scene</b>, dans
    /// <c>s_EntityStatBlocks_005ea560</c>, 0x120 octets par emplacement. Trois passes seulement la
    /// ou un membre du groupe en demande cinq : une bete n'a ni equipement ni statistiques
    /// preparees.
    ///
    /// <para>La marque part en <c>+0x103</c> — <c>&amp;DAT_005ea663</c> dans la pseudo-C, soit
    /// <c>0x005ea560 + 0x103</c>.</para>
    /// </summary>
    private static byte* ResolveSceneCombatBlock_00538830(uint* slot, byte marker)
    {
        byte* block = s_EntityStatBlocks_005ea560 + *((byte*)slot + 8) * 0x120;

        RebaseStatBlockOnModel_00538AF0((byte*)slot);
        ScaleStatBlockByDifficulty(block);
        FinalizeEquipmentEffects(block);

        block[0x103] = marker;
        return block;
    }

    /// <summary>
    /// Ghidra: FUN_00538870 @ 0x00538870 — <b>l'ouverture de tout echange de coups</b> : elle valide
    /// les deux combattants, les <b>appareille</b> (chacun retient l'autre en <c>+0x70</c>), resout
    /// leurs deux blocs de combat, declenche leurs objets equipes, et rend non nul quand le coup ne
    /// doit pas avoir lieu.
    ///
    /// <para>
    /// <b>Le bloc resolu depend de la nature</b> : bit <c>0x800000</c> — membre du groupe — passe
    /// par <see cref="ResolvePartyCombatBlock_005387C0"/>, tout le reste par
    /// <see cref="ResolveSceneCombatBlock_00538830"/>.
    /// </para>
    /// <para>
    /// <b>Deux refus</b> : une victime dont le genre vaut <c>0x600000</c> en entier, et une victime
    /// dont le bloc porte <c>0x100000</c> en <c>+0x40</c> — celle-la recoit en plus une entite
    /// <c>0x29</c>, la marque visible du coup qui ne passe pas.
    /// </para>
    /// </summary>
    private static uint PrepareCombatPair_00538870(
        uint* attacker, uint* victim, byte** attackerBlock, byte** victimBlock, byte marker, int keep)
    {
        if (ValidateCombatants_0053A860(attacker, victim) != 0) return 1;

        attacker[0x1c] = (uint)victim;
        victim[0x1c] = (uint)attacker;

        *attackerBlock = (*attacker & 0x800000) == 0
            ? ResolveSceneCombatBlock_00538830(attacker, marker)
            : ResolvePartyCombatBlock_005387C0(attacker, marker, keep);

        *victimBlock = (*victim & 0x800000) == 0
            ? ResolveSceneCombatBlock_00538830(victim, marker)
            : ResolvePartyCombatBlock_005387C0(victim, marker, keep);

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

        if ((*(uint*)(*victimBlock + 0x40) & 0x100000) != 0)
        {
            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005648a0), 0x29, 0, 0);
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053a6c0 @ 0x0053a6c0 — <b>le jet de sauvegarde</b>, celui du piege et du sort de
    /// statut. Quatre tirages, deux par camp : chacun ajoute a sa caracteristique un aleatoire borne
    /// par elle-meme et un second borne par la moitie de <c>+0x2c</c>.
    ///
    /// <para>
    /// <b>L'assaillant doit depasser STRICTEMENT</b> : a egalite la victime resiste, et le
    /// <c>0xbf</c> — l'effet « ca n'a pas pris » — part sur elle. Le biais est donc du cote du
    /// defenseur, d'un point.
    /// </para>
    /// <para>
    /// L'assaillant compte sur <c>+0x34</c>, la victime sur <c>+0x36</c> : ce ne sont pas les memes
    /// champs, et les deux sont divises par deux avant d'entrer dans le calcul.
    /// </para>
    /// </summary>
    private static uint RollSavingThrow_0053A6C0(
        uint* attacker, uint* victim, byte* attackerBlock, byte* victimBlock)
    {
        if (ValidateCombatants_0053A860(attacker, victim) != 0) return 1;

        if ((*attacker & 0x800000) != 0) TriggerEquippedItems_005238E0(attacker, 8);
        if ((*victim & 0x800000) != 0) TriggerEquippedItems_005238E0(victim, 0x12);

        // L'ordre des quatre tirages est celui du binaire, et il compte : ils partagent une seule
        // suite pseudo-aleatoire.
        int offence = *(short*)(attackerBlock + 0x34) >> 1;
        int r1 = CrtRand();
        short attackerSpread = *(short*)(attackerBlock + 0x2c);
        int r2 = CrtRand();
        int defence = *(short*)(victimBlock + 0x36) >> 1;
        int r3 = CrtRand();
        short victimSpread = *(short*)(victimBlock + 0x2c);
        int r4 = CrtRand();

        if (r1 % ((attackerSpread >> 1) + 1) + r2 % (offence + 1) + offence
            <= r4 % (defence + 1) + defence + r3 % ((victimSpread >> 1) + 1))
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053c420 @ 0x0053c420 — <b>l'application d'un effet de piege</b>, evenement
    /// <c>0x1d</c>. Une des cinq variantes de la meme forme dans le binaire (voir
    /// <c>FUN_0053b3c0</c>, <c>FUN_0053c510</c>…) : meme squelette, autre evenement, autre masque
    /// d'immunite, autre bit d'etat.
    ///
    /// <para>
    /// <b>Trois refus successifs</b> avant que quoi que ce soit ne s'applique : l'ouverture de
    /// <see cref="PrepareCombatPair_00538870"/>, le masque d'immunite <c>0xf3099500</c> lu en
    /// <c>+0x14</c> du bloc de la victime, et le jet de sauvegarde. Les deux derniers font naitre le
    /// <c>0xbf</c> sur la victime — c'est le meme retour visuel pour « immunise » et pour
    /// « resiste ».
    /// </para>
    /// <para>
    /// <b>Ce qui s'applique</b> : <c>+0xe0</c> du bloc de la victime recoit <c>attaque / 4 + 6</c>,
    /// mais <b>seulement si c'est plus que ce qui s'y trouve</b> — un second piege plus faible ne
    /// raccourcit pas l'effet du premier. Puis, la premiere fois seulement (bit 1 de <c>+0x40</c>),
    /// l'entite <c>0x29</c> et l'entite <c>0x3a</c>.
    /// </para>
    /// <para>
    /// <b>Le champ <c>+0x44</c> de la victime est mis a zero le temps du <c>0x3a</c>, puis
    /// restaure.</b> C'est ce que fait l'original ; ce que ce champ porte n'est pas etabli.
    /// </para>
    /// </summary>
    private static uint ApplyTrapEffect_0053C420(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 0x1d, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0xf3099500) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x34) >> 2) + 6;
        if (*(ushort*)(victimBlock + 0xe0) < duration)
        {
            *(short*)(victimBlock + 0xe0) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 1) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 1;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005647c0), 0x29, 0, 0);

            uint saved = victim[0x11];
            victim[0x11] = 0;
            SpawnEffectEntity((byte*)victim, 0x3a, 0, 0);
            victim[0x11] = saved;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053b3c0 @ 0x0053b3c0 — <b>l'application d'un effet d'etat</b>, evenement
    /// <c>7</c>. La sœur de <see cref="ApplyTrapEffect_0053C420"/> : meme squelette exactement,
    /// quatre valeurs changees.
    ///
    /// <table>
    /// <tr><td/><td><c>0x0053b3c0</c></td><td><c>0x0053c420</c></td></tr>
    /// <tr><td>evenement</td><td><c>7</c></td><td><c>0x1d</c></td></tr>
    /// <tr><td>masque d'immunite</td><td><c>0xfb299500</c></td><td><c>0xf3099500</c></td></tr>
    /// <tr><td>duree</td><td><c>+0x8e</c>, <c>attaque / 4 + 0x24</c></td><td><c>+0xe0</c>, <c>attaque / 4 + 6</c></td></tr>
    /// <tr><td>bit de <c>+0x40</c></td><td><c>4</c></td><td><c>1</c></td></tr>
    /// <tr><td>atlas du <c>0x29</c></td><td><c>0x005647d0</c></td><td><c>0x005647c0</c></td></tr>
    /// </table>
    ///
    /// <para>
    /// <b>Une cinquieme difference, et elle n'est pas cosmetique</b> : le champ <c>+0x44</c> de la
    /// victime est mis a <b>1</b> le temps du <c>0x3a</c>, la ou <c>0x0053c420</c> le met a
    /// <b>0</b>. Ce champ decide de quel corps <see cref="EntityHandler_004459E0"/> execute — il a
    /// deux moities complementes, une pour <c>0</c> et une pour <c>1</c>. La poussiere semee par un
    /// effet d'etat n'a donc ni la meme planche, ni la meme teinte, ni la meme taille que celle
    /// semee par un piege. <b>C'est par ce champ que le meme sous-type <c>0x99</c> rend deux
    /// aspects</b>, ce que le commentaire de <c>0x0053c420</c> ne pouvait pas savoir quand il
    /// notait « ce que ce champ porte n'est pas etabli ».
    /// </para>
    /// <para>
    /// Les quatre autres variantes de la meme forme (<c>FUN_0053b4b0</c>, <c>b580</c>,
    /// <c>b660</c>, <c>b730</c>) ecrivent <c>+0x90</c>, <c>+0x92</c>, <c>+0x94</c>… : les durees
    /// d'etat sont un bloc de mots consecutifs, une par effet.
    /// </para>
    /// </summary>
    private static uint ApplyStatusEffect_0053B3C0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 7, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0xfb299500) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrow_0053A6C0(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x34) >> 2) + 0x24;
        if (*(ushort*)(victimBlock + 0x8e) < duration)
        {
            *(short*)(victimBlock + 0x8e) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 4) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 4;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005647d0), 0x29, 0, 0);

            uint saved = victim[0x11];
            victim[0x11] = 1;
            SpawnEffectEntity((byte*)victim, 0x3a, 0, 0);
            victim[0x11] = saved;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053a790 @ 0x0053a790 — <b>le second jet de sauvegarde</b>, celui des trois
    /// effets <c>8</c>, <c>9</c> et <c>10</c>. Meme calcul que
    /// <see cref="RollSavingThrow_0053A6C0"/> au champ pres : l'assaillant compte sur
    /// <c>+0x3c</c> et la victime sur <c>+0x3e</c>, la ou l'autre lit <c>+0x34</c> et <c>+0x36</c>.
    /// Les evenements d'equipement changent aussi (<c>9</c>/<c>0x13</c> contre <c>8</c>/<c>0x12</c>).
    ///
    /// <para>
    /// Le meme biais d'un point vers le defenseur, pour la meme raison : l'assaillant doit depasser
    /// <b>strictement</b>. Et le meme retour visuel, le <c>0xbf</c> de
    /// <see cref="EntityHandler_00475D40"/>.
    /// </para>
    /// </summary>
    private static uint RollSavingThrowB_0053A790(
        uint* attacker, uint* victim, byte* attackerBlock, byte* victimBlock)
    {
        if (ValidateCombatants_0053A860(attacker, victim) != 0) return 1;

        if ((*attacker & 0x800000) != 0) TriggerEquippedItems_005238E0(attacker, 9);
        if ((*victim & 0x800000) != 0) TriggerEquippedItems_005238E0(victim, 0x13);

        int offence = *(short*)(attackerBlock + 0x3c) >> 1;
        int r1 = CrtRand();
        short attackerSpread = *(short*)(attackerBlock + 0x2c);
        int r2 = CrtRand();
        int defence = *(short*)(victimBlock + 0x3e) >> 1;
        int r3 = CrtRand();
        short victimSpread = *(short*)(victimBlock + 0x2c);
        int r4 = CrtRand();

        if (r1 % ((attackerSpread >> 1) + 1) + r2 % (offence + 1) + offence
            <= r4 % (defence + 1) + defence + r3 % ((victimSpread >> 1) + 1))
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053b4b0 @ 0x0053b4b0 — <b>l'effet d'etat 8</b>, duree en <c>+0x90</c>.
    /// Troisieme membre de la famille decrite sur <see cref="ApplyStatusEffect_0053B3C0"/>.
    ///
    /// <para>
    /// <b>Deux differences de forme avec <c>0x0053b3c0</c></b>, et non de simples valeurs : le jet
    /// de sauvegarde est <see cref="RollSavingThrowB_0053A790"/> et non
    /// <see cref="RollSavingThrow_0053A6C0"/>, et la premiere fois ne fait naitre <b>que</b> la
    /// marque <c>0x29</c> — <b>pas de <c>0x3a</c></b>, donc pas de poussiere. C'est le seul effet
    /// discret de la famille.
    /// </para>
    /// <para>Sa duree se calcule aussi autrement : <c>attaque / 8 + 0x15</c>, sur <c>+0x3c</c> du
    /// bloc de l'assaillant et non <c>+0x34</c>.</para>
    /// </summary>
    private static uint ApplyStatusEffect_0053B4B0(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 8, keep) != 0)
        {
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce() && (*(uint*)(victimBlock + 0x14) & 0xf3299500) != 0)   // Couture legacy ++ (LP-015)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce()   // Couture legacy ++ (LP-015)
            && RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x3c) >> 3) + 0x15;
        if (*(ushort*)(victimBlock + 0x90) < duration)
        {
            *(short*)(victimBlock + 0x90) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 8) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 8;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005647e0), 0x29, 0, 0);
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053b580 @ 0x0053b580 — <b>l'effet d'etat 9</b>, duree en <c>+0x92</c>.
    ///
    /// <para>
    /// <b>Le plus voyant de la famille</b> : son masque d'immunite est <c>0x80008000</c>, deux bits
    /// seulement la ou les autres en portent une douzaine — presque personne n'y est immunise —
    /// et sa premiere fois fait naitre <b>deux</b> entites, la marque <c>0x29</c> et un
    /// <c>0xbb</c> (<see cref="EntityHandler_004768A0"/>) avec l'atlas
    /// <see cref="s_Descriptors_00551A78"/>, le meme que <see cref="EntityHandler_00475A30"/>
    /// donne a son propre enfant.
    /// </para>
    /// </summary>
    private static uint ApplyStatusEffect_0053B580(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 9, keep) != 0)
        {
            return 1;
        }

        if ((*(uint*)(victimBlock + 0x14) & 0x80008000) != 0)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x3c) >> 2) + 0x42;
        if (*(ushort*)(victimBlock + 0x92) < duration)
        {
            *(short*)(victimBlock + 0x92) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x10) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x10;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005647e8), 0x29, 0, 0);
            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), Descriptors00551A78(), 0xbb, 0, 9);
        }

        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053b660 @ 0x0053b660 — <b>l'effet d'etat 10</b>, duree en <c>+0x94</c>. La
    /// plus banale des quatre : meme squelette, meme jet <see cref="RollSavingThrowB_0053A790"/>,
    /// une seule entite a la premiere fois.
    /// </summary>
    private static uint ApplyStatusEffect_0053B660(uint* attacker, uint* victim, int keep)
    {
        byte* attackerBlock;
        byte* victimBlock;

        if (PrepareCombatPair_00538870(
                attacker, victim, &attackerBlock, &victimBlock, 10, keep) != 0)
        {
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce() && (*(uint*)(victimBlock + 0x14) & 0xf3099500) != 0)   // Couture legacy ++ (LP-015)
        {
            SpawnEffectEntity((byte*)victim, 0xbf, 0, 0);
            return 1;
        }

        if (!LegacyPlus.Coutures.MalusForce()   // Couture legacy ++ (LP-015)
            && RollSavingThrowB_0053A790(attacker, victim, attackerBlock, victimBlock) != 0) return 1;

        int duration = (*(short*)(attackerBlock + 0x3c) >> 2) + 0x42;
        if (*(ushort*)(victimBlock + 0x94) < duration)
        {
            *(short*)(victimBlock + 0x94) = (short)duration;
        }

        if ((*(uint*)(victimBlock + 0x40) & 0x20) == 0)
        {
            *(uint*)(victimBlock + 0x40) |= 0x20;

            SpawnStatusEffectEntity_0047AC40(
                (byte*)victim, (int*)(victim + 7), SpriteAtlas(0x005647f0), 0x29, 0, 0);
        }

        return 0;
    }
}
