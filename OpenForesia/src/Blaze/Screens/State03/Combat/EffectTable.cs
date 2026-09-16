// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE REPARTITEUR DES EFFETS — les tables 0x0056d4e0 et 0x0056d4c0.
//
// C'est le carrefour que l'objet et le sort partagent : une potion, un piege de coffre et un sort
// de guerison entrent tous les trois par `ApplyItemEffect_0056D4E0`.
//
// LA TABLE NE COMMENCE PAS OU LE DEPOT LA FAISAIT COMMENCER. `ApplyItemEffect_0056D4E0` indexe
// 0x0056d4e0 ; l'herboristerie, elle, indexe 0x0056d4c0 — HUIT DWORDS PLUS TOT. Les sept entrees
// de l'herboristerie sont suivies d'un ZERO, et c'est ce zero qui separe les deux blocs :
// l'entree 7 est nulle, l'entree 8 est l'effet 0 des objets. Releve le 2026-09-01.
//
// Les 48 corps d'effet sont dans `StatusEffects.cs`, les sept plantes dans `HerbEffects.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LA TABLE DES 48 EFFETS (0x0056d4e0) — 0x0053afb0 .. 0x0053cdf0
    //
    // C'est le sous-systeme que l'opcode 18 du script d'objet et les sorts partagent : une potion,
    // un piege de coffre et un sort de guerison entrent tous les trois par ici. Les 48 corps sont
    // dans Blaze.c, courts (14 a 87 lignes) et tres reguliers ; cinq etaient deja transcrits par
    // le chemin des pieges de coffre (0x0053b3c0, 0x0053b4b0, 0x0053b580, 0x0053b660, 0x0053c420),
    // les 43 autres le sont ici.
    //
    // LE SQUELETTE COMMUN, celui des entrees 0 a 33 :
    //
    //   1. PrepareCombatPair_00538870 appareille les deux combattants et resout leurs deux blocs
    //      de statistiques. C'est un appel A DEUX SORTIES ECRITES PAR ADRESSE : dans l'original
    //      les parametres 2 et 3 sont passes par pointeur et RECOIVENT les blocs, si bien que
    //      `param_2` cesse d'etre la victime pour devenir le bloc de l'assaillant et `param_3`
    //      cesse d'etre le drapeau pour devenir le bloc de la victime. Les corps qui ont encore
    //      besoin de l'entite victime la sauvent AVANT l'appel (`iVar1 = param_2`), et cette
    //      sauvegarde est reproduite ici par le simple fait que `victim` reste `victim`.
    //   2. un masque d'immunite lu en +0x14 du bloc de la victime, quand l'effet en a un : pose,
    //      l'effet echoue en faisant naitre le sous-type 0xbf — la marque du coup sans effet ;
    //   3. un jet de sauvegarde, RollSavingThrow_0053A6C0 ou RollSavingThrowB_0053A790 selon
    //      l'effet, quand il en a un ;
    //   4. une duree calculee sur une statistique de l'assaillant, decalee et decalee seulement
    //      (jamais divisee), rangee en +0x80..+0xc2 du bloc de la victime SI elle depasse ce qui
    //      s'y trouve — un effet deja pose n'est jamais raccourci, seulement rallonge ;
    //   5. un bit d'etat en +0x40, et c'est SA POSE, pas la duree, qui declenche la marque
    //      visible : le 0x29 ne nait qu'a la premiere fois.
    //
    // Les entrees 34 a 42 sont les LEVEES : meme appareillage, puis un bit efface et sa duree
    // remise a zero. Les entrees 43 a 47 touchent aux points de vie.
    //
    // Ces 48 corps restent 48 fonctions. Ils ne different pas seulement par des constantes — les
    // uns ont un masque d'immunite et pas les autres, le jet de sauvegarde change de famille, et
    // cinq d'entre eux font naitre une seconde entite. Les factoriser demanderait une table a
    // huit colonnes dont trois seraient des drapeaux de presence : ce serait moins lisible que
    // les corps eux-memes, et ce ne serait plus ce que le binaire contient.
    // =====================================================================

    /// <summary>
    /// La <b>fiche de l'effet en cours</b> : le mot <c>+0x06</c> de l'entite porte la famille en
    /// octet haut et l'indice en octet bas, tels que <see cref="CastSpell_0053A980"/> les y a
    /// ecrits, et le repertoire principal les resout en une entree de <c>0x30</c> octets. C'est la
    /// meme chaine que dans le lanceur, lue ici a l'envers.
    ///
    /// <para>
    /// <b>Ce n'est pas reserve aux sorts</b>, et c'est ce qui fait tenir le script d'objet : le
    /// champ <c>+0x06</c> est le <b>passage de main entre l'opcode 14 et l'opcode 18</b>. Le 14
    /// (<see cref="ItemOpCast"/>) appelle <c>CastSpell_0053A980</c>, qui y range la paire et fait
    /// naitre l'effet visible ; le 18 la relit ici pour aller chercher dans la fiche le montant de
    /// base — le nombre de points qu'une potion rend. Un script qui emettrait le 18 sans le 14
    /// lirait la paire laissee par l'effet precedent, ce que le binaire fait aussi : aucune garde
    /// n'est ajoutee.
    /// </para>
    /// </summary>
    private static byte* SpellSheetOf(uint* caster)
    {
        byte* e = (byte*)caster;
        return *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c) + e[7] * 4)
               + e[6] * 0x30;
    }

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;DAT_0056d4e0)[genre])(…)</c> — <b>la table des 48 effets</b>, de
    /// <c>0x0053afb0</c> a <c>0x0053cdf0</c>. Relevee dans le binaire ; l'etendue est deduite, le
    /// dword qui suit la 48e entree vaut 1, debut d'une table de masques de bits.
    ///
    /// <para>Rendue en <c>switch</c> comme les deux autres repartiteurs du fichier, et pour la
    /// meme raison : c'est une table de sauts dans l'original, et <c>Strategy.md</c> demande de
    /// reconstruire le flot. <b>L'original ne borne pas son indice</b> — au-dela de 47 il saute sur
    /// ce qui suit la table ; ici on le dit.</para>
    /// </summary>
    private static uint ApplyItemEffect_0056D4E0(byte kind, uint* source, byte* target, uint flag)
    {
        uint* victim = (uint*)target;
        int keep = (int)flag;

        return kind switch
        {
            0 => ApplyStatusEffect_0053AFB0(source, victim, keep),
            1 => ApplyStatusEffect_0053B020(source, victim, keep),
            2 => ApplyStatusEffect_0053B090(source, victim, keep),
            3 => ApplyStatusEffect_0053B100(source, victim, keep),
            4 => ApplyStatusEffect_0053B1B0(source, victim, keep),
            5 => ApplyStatusEffect_0053B260(source, victim, keep),
            6 => ApplyStatusEffect_0053B310(source, victim, keep),
            7 => ApplyStatusEffect_0053B3C0(source, victim, keep),
            8 => ApplyStatusEffect_0053B4B0(source, victim, keep),
            9 => ApplyStatusEffect_0053B580(source, victim, keep),
            10 => ApplyStatusEffect_0053B660(source, victim, keep),
            11 => ApplyStatusEffect_0053B730(source, victim, keep),
            12 => ApplyStatusEffect_0053B810(source, victim, keep),
            13 => ApplyStatusEffect_0053B8F0(source, victim, keep),
            14 => ApplyStatusEffect_0053B9C0(source, victim, keep),
            15 => ApplyStatusEffect_0053BA90(source, victim, keep),
            16 => ApplyStatusEffect_0053BB20(source, victim, keep),
            17 => ApplyStatusEffect_0053BBD0(source, victim, keep),
            18 => ApplyStatusEffect_0053BC60(source, victim, keep),
            19 => ApplyStatusEffect_0053BD10(source, victim, keep),
            20 => ApplyStatusEffect_0053BDA0(source, victim, keep),
            21 => ApplyStatusEffect_0053BE70(source, victim, keep),
            22 => ApplyStatusEffect_0053BF00(source, victim, keep),
            23 => ApplyStatusEffect_0053BFB0(source, victim, keep),
            24 => ApplyStatusEffect_0053C040(source, victim, keep),
            25 => ApplyStatusEffect_0053C0F0(source, victim, keep),
            26 => ApplyStatusEffect_0053C160(source, victim, keep),
            27 => ApplyStatusEffect_0053C1F0(source, victim, keep),
            28 => ApplyStatusEffect_0053C2A0(source, victim, keep),
            29 => ClearAllStatusEffects_0053C330(source, victim, keep),
            30 => ApplyTrapEffect_0053C420(source, victim, keep),
            31 => ApplyStatusEffect_0053C510(source, victim, keep),
            32 => ApplyStatusEffect_0053C5E0(source, victim, keep),
            33 => ApplyStatusEffect_0053C6B0(source, victim, keep),
            34 => ClearStatusEffect_0053C740(source, victim, keep),
            35 => ClearStatusEffect_0053C790(source, victim, keep),
            36 => ClearStatusEffect_0053C7E0(source, victim, keep),
            37 => ClearStatusEffect_0053C830(source, victim, keep),
            38 => ClearStatusEffect_0053C890(source, victim, keep),
            39 => ClearStatusEffect_0053C8E0(source, victim, keep),
            40 => ClearStatusEffect_0053C930(source, victim, keep),
            41 => ClearStatusEffect_0053C980(source, victim, keep),
            42 => ClearStatusEffect_0053CA30(source, victim, keep),
            43 => HealTarget_0053CA80(source, victim, keep),
            44 => HealPartyShare_0053CB30(source, victim, keep),
            45 => HealTargetFully_0053CD20(source, victim, keep),
            46 => ReviveTarget_0053CD80(source, victim, keep),
            47 => SlayTarget_0053CDF0(source, victim, keep),
            _ => throw new NotImplementedException(
                     $"Table d'effets 0x0056d4e0, genre {kind} : au-dela des 48 entrees "
                     + "(0x0053afb0..0x0053cdf0). L'original saute dans la table sans borne ; "
                     + "ici on le dit."),
        };
    }

    // ================================================================================
    // LES SEPT EFFETS DE L'HERBORISTERIE — 0x0053ace0 a 0x0053af60
    //
    // LA TABLE DES EFFETS NE COMMENCE PAS OU LE DEPOT LA FAIT COMMENCER. ApplyItemEffect_0056D4E0
    // indexe 0x0056d4e0 ; l'herboristerie, elle, indexe 0x0056d4c0 — HUIT DWORDS PLUS TOT. Ses
    // sept entrees (0x0053ace0, 0x0053ad80, 0x0053ae20, 0x0053ae70, 0x0053aec0, 0x0053af10,
    // 0x0053af60) sont suivies d'un ZERO, et c'est ce zero qui separe les deux blocs : l'entree 7
    // est nulle, l'entree 8 est l'effet 0 des objets. Un bloc de sept fonctions qu'AUCUN autre
    // appelant n'atteint, releve le 2026-09-01.
    //
    // ELLES NE TRAVAILLENT PAS SUR LE MEME OBJET QUE LES 48 AUTRES. Les effets d'objet appareillent
    // par PrepareCombatPair_00538870 et ecrivent dans le BLOC DE COMBAT ; celles-ci se contentent
    // de ValidateCombatants_0053A860 et ecrivent DIRECTEMENT dans la fiche de personnage, a
    // MainMem + (indice + 0x78) * 0x2000. C'est la meme memoire vue 0x100 octets plus bas —
    // ResolvePartyCombatBlock_005387C0 rend `fiche + 0x100` —, ce qui se verifie champ par champ :
    // le bloc a ses PV en +0x4c et son mot d'etat en +0x40, la fiche les a en +0x14c et +0x140.
    // Elles sautent donc tout l'appareillage : ni recalcul des statistiques derivees, ni bonus
    // d'equipement, ni marqueur +0x203.
    //
    // DEUX SOIGNENT, CINQ LEVENT. Les deux premieres rendent des points — le meme montant tire de
    // la meme facon, `rand() % 27 + 4 + caracteristique/4` —, les cinq autres effacent un bit du
    // mot d'etat. Chacune des cinq a sa jumelle exacte parmi les levees d'objet (les entrees 34 a
    // 42), a UNE difference pres, signalee sur les deux concernees : Hysopius et Belladone
    // effacent le bit SANS remettre la duree a zero, la ou leurs jumelles font les deux.
    // ================================================================================

    /// <summary>
    /// Ghidra: <c>(*(code *)(&amp;PTR_FUN_0056d4c0)[plante])(…)</c> — <b>la table des sept effets de
    /// l'herboristerie</b>, relevee dans le binaire a <c>0x0056d4c0</c>. L'indice est l'octet
    /// <c>+0x06</c> de l'entite du sort, c'est-a-dire <b>l'indice de la plante dans l'ecole 3</b>.
    ///
    /// <para>
    /// Rendue en <c>switch</c> comme <see cref="ApplyItemEffect_0056D4E0"/> et pour la meme raison.
    /// <b>Le site d'appel passe trois arguments</b> (lanceur, cible, 0) mais <b>aucune des sept ne
    /// lit le troisieme</b> — le desassemblage le confirme sur les cinq que <c>Blaze.c</c> n'a pas
    /// decompilees, et Ghidra n'en a type que deux sur les deux qu'il a rendues. Il n'est donc pas
    /// transmis ici.
    /// </para>
    /// <para>
    /// <b>Aucun autre appelant n'atteint ce bloc</b> : l'herboristerie est la seule ecole a indexer
    /// <c>0x0056d4c0</c>, et elle-meme n'est pas atteignable en jeu normal (voir
    /// <c>Documentation/Reference/Sorts.md</c>).
    /// </para>
    /// </summary>
    private static uint ApplyHerbEffect_0056D4C0(byte plant, uint* caster, uint* target)
        => plant switch
        {
            0 => HealHerbHitPoints_0053ACE0(caster, target),        // Lavande
            1 => HealHerbManaPoints_0053AD80(caster, target),       // Sauge
            2 => ClearHerbStatus_0053AE20(caster, target),          // Verveine
            3 => ClearHerbStatus_0053AE70(caster, target),          // Mercuriale
            4 => ClearHerbStatus_0053AEC0(caster, target),          // Fenouil
            5 => ClearHerbStatus_0053AF10(caster, target),          // Hysopius
            6 => ClearHerbStatus_0053AF60(caster, target),          // Belladone
            _ => throw new NotImplementedException(
                     $"Table des herbes 0x0056d4c0, plante {plant} : au-dela des sept entrees "
                     + "(0x0053ace0..0x0053af60). L'entree 7 est un pointeur NUL dans le binaire, "
                     + "et au-dela commence la table des effets d'objet."),
        };
}
