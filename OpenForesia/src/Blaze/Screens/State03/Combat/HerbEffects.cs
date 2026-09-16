// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES SEPT EFFETS DE L'HERBORISTERIE — 0x0053ace0 a 0x0053af60, table 0x0056d4c0.
//
// ELLES NE TRAVAILLENT PAS SUR LE MEME OBJET QUE LES 48 AUTRES. Les effets d'objet appareillent
// par `PrepareCombatPair_00538870` et ecrivent dans le BLOC DE COMBAT ; celles-ci se contentent de
// `ValidateCombatants_0053A860` et ecrivent DIRECTEMENT dans la fiche de personnage, a
// `MainMem + (indice + 0x78) * 0x2000`. C'est la meme memoire vue 0x100 octets plus bas — voir
// `CombatPairing.cs`. Elles sautent donc tout l'appareillage : ni recalcul des statistiques
// derivees, ni bonus d'equipement, ni marqueur +0x203.
//
// DEUX SOIGNENT, CINQ LEVENT. Les deux premieres rendent des points — le meme montant tire de la
// meme facon, rand() modulo 27 plus 4 plus le quart d'une caracteristique —, les cinq autres
// effacent un bit du mot d'etat. Chacune des cinq a sa jumelle exacte parmi les levees d'objet
// (entrees 34 a 42), a UNE difference pres : Hysopius et Belladone effacent le bit SANS remettre
// la duree a zero, la ou leurs jumelles font les deux.
//
// Les fiches de sort correspondantes sont dans `Spells/Herbalism.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_0053ace0 @ 0x0053ace0 — <b>Lavande</b>, la premiere plante : elle <b>rend des
    /// points de vie</b>.
    ///
    /// <para>
    /// Le montant est <c>rand() % 27 + 4 + caracteristique/4</c>, la caracteristique etant celle du
    /// <b>lanceur</b> en <c>+0x126</c> de sa fiche. Il s'ajoute aux PV courants
    /// (<c>+0x14c</c>), <b>plafonnes au maximum</b> (<c>+0x148</c>), et part en nombre flottant
    /// au-dessus de la cible.
    /// </para>
    /// <para>
    /// <b>Le plafond est applique apres coup</b>, comme chez <see cref="HealTarget_0053CA80"/> : le
    /// nombre affiche est le montant entier, meme quand une partie a ete perdue au plafond.
    /// </para>
    /// </summary>
    private static uint HealHerbHitPoints_0053ACE0(uint* caster, uint* target)
    {
        if (ValidateCombatants_0053A860(caster, target) != 0) return 1;

        byte* mainMem = GetMainMemPtr();
        byte* casterSheet = mainMem + (((byte*)caster)[8] + 0x78) * 0x2000;
        byte* targetSheet = mainMem + (((byte*)target)[8] + 0x78) * 0x2000;

        int amount = CrtRand() % 0x1b + 4 + (*(short*)(casterSheet + 0x126) >> 2);

        *(short*)(targetSheet + 0x14c) += (short)amount;

        if (*(short*)(targetSheet + 0x148) < *(short*)(targetSheet + 0x14c))
        {
            *(short*)(targetSheet + 0x14c) = *(short*)(targetSheet + 0x148);
        }

        SpawnParticle_0050F070((uint*)((byte*)target + 0x1c), (uint)amount, 1);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053ad80 @ 0x0053ad80 — <b>Sauge</b> : la jumelle exacte de
    /// <see cref="HealHerbHitPoints_0053ACE0"/>, <b>sur les points de magie</b>. Meme tirage, meme
    /// caracteristique, meme nombre flottant ; seuls changent les deux champs — <c>+0x14e</c>
    /// courant, <c>+0x14a</c> maximum.
    /// </summary>
    private static uint HealHerbManaPoints_0053AD80(uint* caster, uint* target)
    {
        if (ValidateCombatants_0053A860(caster, target) != 0) return 1;

        byte* mainMem = GetMainMemPtr();
        byte* casterSheet = mainMem + (((byte*)caster)[8] + 0x78) * 0x2000;
        byte* targetSheet = mainMem + (((byte*)target)[8] + 0x78) * 0x2000;

        int amount = CrtRand() % 0x1b + 4 + (*(short*)(casterSheet + 0x126) >> 2);

        *(short*)(targetSheet + 0x14e) += (short)amount;

        if (*(short*)(targetSheet + 0x14a) < *(short*)(targetSheet + 0x14e))
        {
            *(short*)(targetSheet + 0x14e) = *(short*)(targetSheet + 0x14a);
        }

        SpawnParticle_0050F070((uint*)((byte*)target + 0x1c), (uint)amount, 1);
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053ae20 @ 0x0053ae20 — <b>Verveine</b> : leve <b>le bit 1</b> du mot d'etat
    /// (<c>+0x140</c> de la fiche) et remet sa duree (<c>+0x1e0</c>) a zero. 79 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; le releve est garde sous
    /// <c>Legacy/Raw/Blaze/FUN_0053AE20.asm</c>, <b>qui couvre les cinq levees d'un bloc</b> —
    /// elles se suivent sans trou jusqu'a l'effet 0 des objets.
    ///
    /// <para>
    /// C'est <b>l'exacte jumelle de <see cref="ClearStatusEffect_0053C740"/></b>, l'entree 34 des
    /// effets d'objet, aux deux champs pres qui sont les memes vus 0x100 octets plus haut. L'etat
    /// leve est celui qui <b>draine trois pour cent des PV maximum toutes les trente images</b>
    /// (voir la retenue de <see cref="AdvanceSceneEntityAnimation_0053aa10"/>).
    /// </para>
    /// </summary>
    private static uint ClearHerbStatus_0053AE20(uint* caster, uint* target)
    {
        if (ValidateCombatants_0053A860(caster, target) != 0) return 1;

        byte* sheet = GetMainMemPtr() + (((byte*)target)[8] + 0x78) * 0x2000;

        *(ushort*)(sheet + 0x1e0) = 0;
        *(uint*)(sheet + 0x140) &= 0xfffffffe;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053ae70 @ 0x0053ae70 — <b>Mercuriale</b> : leve <b>le bit 4</b> du mot d'etat
    /// et remet sa duree (<c>+0x18e</c>) a zero. 79 octets, <b>absente de <c>Blaze.c</c></b>.
    /// L'exacte jumelle de <see cref="ClearStatusEffect_0053C790"/>, l'entree 35.
    /// </summary>
    private static uint ClearHerbStatus_0053AE70(uint* caster, uint* target)
    {
        if (ValidateCombatants_0053A860(caster, target) != 0) return 1;

        byte* sheet = GetMainMemPtr() + (((byte*)target)[8] + 0x78) * 0x2000;

        *(ushort*)(sheet + 0x18e) = 0;
        *(uint*)(sheet + 0x140) &= 0xfffffffb;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053aec0 @ 0x0053aec0 — <b>Fenouil</b> : leve <b>le bit 0x100</b> du mot d'etat
    /// et remet sa duree (<c>+0x19a</c>) a zero. 79 octets, <b>absente de <c>Blaze.c</c></b>.
    /// L'exacte jumelle de <see cref="ClearStatusEffect_0053C890"/>, l'entree 38.
    ///
    /// <para>
    /// C'est <b>la petrification</b> — le seul des etats retenus qui n'ait pas de duree qui le
    /// termine : il draine cinq pour cent des PV maximum toutes les trente images et ne s'arrete
    /// jamais de lui-meme. <b>La duree +0x19a est donc remise a zero alors qu'elle n'est jamais
    /// employee</b> ; c'est ce que fait aussi l'entree 38, et c'est transcrit tel quel.
    /// </para>
    /// <para>
    /// Le binaire efface ce bit-la par <c>and ch, 0xfe</c> — un ET sur le seul octet <c>ch</c>,
    /// donc sur les bits 8 a 15 du mot. Le resultat est celui d'un <c>&amp; 0xfffffeff</c>, ecrit
    /// ainsi ici pour rester lisible ; les trois autres octets ne sont pas touches dans un cas
    /// comme dans l'autre.
    /// </para>
    /// </summary>
    private static uint ClearHerbStatus_0053AEC0(uint* caster, uint* target)
    {
        if (ValidateCombatants_0053A860(caster, target) != 0) return 1;

        byte* sheet = GetMainMemPtr() + (((byte*)target)[8] + 0x78) * 0x2000;

        *(ushort*)(sheet + 0x19a) = 0;
        *(uint*)(sheet + 0x140) &= 0xfffffeff;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053af10 @ 0x0053af10 — <b>Hysopius</b> : leve <b>le bit 2</b> du mot d'etat.
    /// 70 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>ET ELLE NE REMET PAS LA DUREE A ZERO</b>, contrairement a sa jumelle d'objet
    /// <see cref="ClearStatusEffect_0053C7E0"/> (entree 36), qui efface le bit <b>et</b> le
    /// <c>+0xe2</c> du bloc. Ici le <c>+0x1e2</c> de la fiche reste debout. Le corps fait 70 octets
    /// contre 79 pour les trois precedentes : les neuf octets manquants sont exactement le
    /// <c>mov word [eax+…],0</c> qu'elles ont et qu'elle n'a pas. Ce n'est pas une lecture
    /// approximative, c'est une instruction absente.
    /// </para>
    /// <para>
    /// <b>Ce que ca change</b> : la retenue ne decompte <c>+0x1e2</c> que tant que le bit est pose,
    /// donc une valeur restee la ne fait rien tant que l'etat ne revient pas. Mais une nouvelle
    /// application prend le <b>maximum</b> de la duree en place et de la nouvelle
    /// (<see cref="ApplyStatusEffect_0053B4B0"/> et ses voisines) : apres Hysopius, un etat
    /// reapplique peut durer ce que durait le precedent. L'etat en question est celui qui draine
    /// trois pour cent des PM maximum toutes les trente images.
    /// </para>
    /// <para>
    /// Aucune consequence en jeu : l'herboristerie est injouable. Note ici parce que la
    /// dissymetrie est reelle et qu'elle se lit dans la taille des corps.
    /// </para>
    /// </summary>
    private static uint ClearHerbStatus_0053AF10(uint* caster, uint* target)
    {
        if (ValidateCombatants_0053A860(caster, target) != 0) return 1;

        byte* sheet = GetMainMemPtr() + (((byte*)target)[8] + 0x78) * 0x2000;

        // PAS de remise a zero de +0x1e2 : voir la note. L'instruction n'est pas dans le binaire.
        *(uint*)(sheet + 0x140) &= 0xfffffffd;
        return 0;
    }

    /// <summary>
    /// Ghidra: FUN_0053af60 @ 0x0053af60 — <b>Belladone</b>, la septieme et derniere plante : leve
    /// <b>le bit 0x10</b> du mot d'etat. 70 octets, <b>absente de <c>Blaze.c</c></b>.
    ///
    /// <para>
    /// <b>Meme dissymetrie qu'<see cref="ClearHerbStatus_0053AF10"/></b> : sa jumelle d'objet
    /// <see cref="ClearStatusEffect_0053C8E0"/> (entree 39) efface le bit <b>et</b> la duree
    /// <c>+0x92</c> du bloc ; celle-ci laisse le <c>+0x192</c> de la fiche debout. Meme ecart de
    /// neuf octets, meme conclusion. L'etat leve est <b>l'effet d'etat 9</b>
    /// (<see cref="ApplyStatusEffect_0053B580"/>).
    /// </para>
    /// </summary>
    private static uint ClearHerbStatus_0053AF60(uint* caster, uint* target)
    {
        if (ValidateCombatants_0053A860(caster, target) != 0) return 1;

        byte* sheet = GetMainMemPtr() + (((byte*)target)[8] + 0x78) * 0x2000;

        // PAS de remise a zero de +0x192 : voir la note portee sur ClearHerbStatus_0053AF10.
        *(uint*)(sheet + 0x140) &= 0xffffffef;
        return 0;
    }
}
