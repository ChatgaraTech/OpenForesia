// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE MENU RAPIDE — l'anneau qui s'ouvre au-dessus du personnage.
//
// Ouvert par le bouton lu en +0x1fca, il laisse employer un objet PORTE sans passer par
// l'inventaire. `TickQuickMenu_00530070` le fait vivre, `UseCarriedItem_00523820` declenche
// la fiche - laquelle repart dans `Items/ItemScript.cs`.
//
// Il n'a pas d'etat de jeu a lui : la boucle par image lui cede simplement la main, comme elle le
// fait pour le menu complet (`WorldMapMenu.cs`) et pour le coffre (`WorldMapDepot.cs`).

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // Le menu rapide — l'anneau au-dessus du personnage, ouvert par le bouton `+0x1fca`.
    //
    // `DrawSpeechOverlay_005309B0` le dessine ; `TickQuickMenu_00530070` le pilote. Les deux lisent
    // la meme table de disposition `s_MenuLayout_0056a850` : la categorie courante est
    // `personnage[0x2be] & 0xf`, l'entree choisie dans la categorie est `personnage[0x2bf]`, et
    // `personnage[0x2d0]` porte le nombre d'entrees que la categorie courante propose (la liste
    // elle-meme etant a `+0x2d1`).
    //
    // Le bit 0x80 de `personnage[0x2be]` distingue les deux etages : eteint on change de categorie,
    // allume on parcourt les entrees de la categorie. Le bit 0x8000000 du mot d'etat est ce qui
    // maintient l'anneau a l'ecran ; l'eteindre le ferme.
    //
    // Les quatre sons sont ceux de tout le jeu : 0x38 valider, 0x39 annuler, 0x3a deplacer,
    // 0x3b refuser (Ghidra: FUN_0053db80 / FUN_0053dba0 / FUN_0053db60 / FUN_0053dbc0, quatre
    // enveloppes d'une ligne autour de FUN_0053cea0).
    // =====================================================================

    private static void QuickMenuConfirmSound() => Aud.PlaySoundEffect(0x38, 0x400, 0x2000, 0x2000);
    private static void QuickMenuCancelSound() => Aud.PlaySoundEffect(0x39, 0x400, 0x2000, 0x2000);
    private static void QuickMenuMoveSound() => Aud.PlaySoundEffect(0x3a, 0x400, 0x2000, 0x2000);
    private static void QuickMenuRefuseSound() => Aud.PlaySoundEffect(0x3b, 0x400, 0x2000, 0x2000);

    /// <summary>
    /// Ghidra: FUN_00530070 @ 0x00530070 — <b>le menu rapide</b>, la boucle d'entree de l'anneau
    /// qu'un membre du groupe ouvre au-dessus de lui. Prise quand le mot d'etat porte
    /// <c>0x8000000</c>.
    ///
    /// <para>Elle fait trois choses dans cet ordre : ouvrir, fermer ou valider l'etage courant ;
    /// deplacer le curseur d'entree et agir sur la validation, par un aiguillage sur le genre de la
    /// categorie ; puis — c'est la queue commune, atteinte par tous les chemins — deplacer le
    /// curseur de categorie et <b>recalculer la liste d'entrees</b> que la nouvelle categorie
    /// propose.</para>
    ///
    /// <para><b>Les deux axes ne font pas la meme chose selon que le bouton d'ouverture est
    /// tenu.</b> Bouton tenu : haut/bas changent de categorie, gauche/droite changent d'entree.
    /// Bouton relache : les deux axes changent d'entree, et la categorie ne bouge que si l'anneau
    /// n'est pas encore ouvert. Le repetiteur est <c>manette[6]</c>, le mot que
    /// <see cref="Input.UpdateButtonEdges"/> remplit avec la croix et sa repetition automatique —
    /// <c>0x10</c> haut, <c>0x20</c> droite, <c>0x40</c> bas, <c>0x80</c> gauche.</para>
    ///
    /// <para><b>Le test « le curseur a-t-il bouge » du genre 6 lit un registre ecrase.</b> Ghidra
    /// rend <c>puVar11</c> comme la valeur d'avant le deplacement, puis les deux boucles de
    /// recherche l'ecrasent avec leur compteur d'essais restants ; le binaire confirme
    /// (<c>cmp ebx,edx</c> en <c>0x00530376</c>, ou <c>ebx</c> sort de <c>dec ebx</c>). Transcrit
    /// tel quel : le son de deplacement se declenche donc des qu'une recherche a tourne, meme si
    /// elle est revenue sur le meme sort. C'est le comportement de 1998.</para>
    ///
    /// <para><b>Le genre <c>-1</c> de la table est inatteignable</b> — voir
    /// <see cref="s_MenuLayout_0056a850"/>. Le binaire n'y initialise pas le masque de la queue : le
    /// <c>ja</c> non signe saute par-dessus les cinq <c>mov ebx</c>, puis le <c>jg</c> signe laisse
    /// entrer la boucle avec <c>ebx</c> = le pointeur de manette. On pose zero a la place, ce qui
    /// rend la liste vide plutot qu'aleatoire, et on le dit ici.</para>
    ///
    /// <para><b>Un seul trou</b> : le genre 5 valide fait tourner le script d'objet
    /// (<see cref="RunItemScript_00523AA0"/>), une seconde machine a opcodes de 23 gestionnaires
    /// que le depot n'a pas encore abordee. Tout le reste du menu — navigation, equipement,
    /// sorts, passage de la main — est transcrit.</para>
    ///
    /// <para>Le desassemblage complet est conserve dans
    /// <c>Legacy/Raw/Blaze/FUN_00530070.disasm.txt</c> : Ghidra s'arrete au <c>ret</c> de
    /// <c>0x00530714</c> alors que les genres 8 et 9 vivent apres lui, et trois tables de sauts
    /// (<c>0x00530948</c>, <c>0x00530970</c>, <c>0x00530998</c>) ne se lisent que la.</para>
    /// </summary>
    private static void TickQuickMenu_00530070(uint* slot, byte* entry, byte* perChar, byte* pad)
    {

        byte state = perChar[0x2be];
        int kind = (sbyte)s_SpeechIconIndex_0056a851[(state & 0xf) + (perChar[0x150] & 7) * 9];

        ushort openMask = Input.CombatMask(*(ushort*)(perChar + 0x1fca));
        byte* target = pad;                    // Ghidra: puVar7 / eax, l'emplacement qu'on equipera
        bool close = false;                    // Ghidra: LAB_005304f7, atteint des deux etages

        if ((state & 0x80) == 0)
        {
            // ---------------------------------------------- l'anneau n'est pas encore ouvert
            ushort edge = *(ushort*)(pad + 2);
            ushort dismiss = (ushort)(openMask | 0x4000);

            if ((dismiss & edge) != 0)
            {
                *(ushort*)(pad + 2) = (ushort)(~dismiss & edge);
                close = true;
            }
            else if (((*(ushort*)pad & openMask) != 0 && (pad[6] & 0xa0) != 0)
                     || (edge & 0x8000) != 0)
            {
                // Le genre 5 se refuse tant que le delai d'objet du personnage court.
                if ((kind == 5 && perChar[0x207] != 0) || perChar[0x2d0] == 0)
                {
                    QuickMenuRefuseSound();
                }
                else
                {
                    perChar[0x2be] = (byte)(state | 0x80);
                    QuickMenuConfirmSound();
                }
            }
        }
        else
        {
            // ---------------------------------------------- l'anneau est ouvert sur une categorie
            ushort edge = *(ushort*)(pad + 2);

            if ((edge & openMask) != 0)
            {
                *(ushort*)(pad + 2) = (ushort)(edge & ~openMask);
                close = true;
            }
            else if (((*(ushort*)pad & openMask) != 0 && (pad[6] & 0x50) != 0)
                     || (edge & 0x4000) != 0)
            {
                perChar[0x2be] = (byte)(state & 0xf);
                QuickMenuCancelSound();
            }
            else if (perChar[0x2d0] != 0)
            {
                byte entries = perChar[0x2d0];            // Ghidra: bVar1, releve une seule fois

                // --- le curseur d'entree ; le genre 6 a son propre balayage plus bas ------------
                if (kind != 6)
                {
                    sbyte before = (sbyte)perChar[0x2bf];
                    byte repeat = pad[6];
                    byte forward;

                    if ((*(ushort*)pad & openMask) == 0)
                    {
                        if ((repeat & 0x90) != 0) perChar[0x2bf] = (byte)(before - 1);
                        forward = (byte)(pad[6] & 0x60);
                    }
                    else
                    {
                        if ((repeat & 0x80) != 0) perChar[0x2bf] = (byte)(before - 1);
                        forward = (byte)(pad[6] & 0x20);
                    }

                    if (forward != 0) perChar[0x2bf]++;
                    if (perChar[0x2bf] == 0xff) perChar[0x2bf] = (byte)(entries - 1);
                    if (perChar[0x2bf] >= entries) perChar[0x2bf] = 0;
                    if (before != (sbyte)perChar[0x2bf]) QuickMenuMoveSound();
                }

                switch (kind)
                {
                    // --- 0 a 4 : les cinq emplacements d'equipement ----------------------------
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        target = perChar + 0x2b0 + kind;
                        break;

                    // --- 5 : se servir de l'objet designe --------------------------------------
                    case 5:
                    {
                        byte item = perChar[perChar[0x2bf] + 0x2d1];

                        if ((pad[3] & 0x80) != 0)
                        {
                            if ((perChar[item * 0x280 + 0x313] & 0x10) == 0
                                || s_Unknown_00566c84 == 0
                                || (*(uint*)(perChar + 0x140) & 0x400003c0) != 0)
                            {
                                QuickMenuRefuseSound();
                            }
                            else
                            {
                                UseCarriedItem_00523820(slot, item);
                                CommitStagedStatBlock(perChar);              // Ghidra: FUN_00538ab0
                                ScaleStatBlockByDifficulty(perChar + 0x100); // Ghidra: FUN_00538f50
                                ComputeDerivedStats(perChar);                // Ghidra: FUN_00538c50
                                ApplyEquipmentStatBonuses(perChar);          // Ghidra: FUN_00538e20
                                FinalizeEquipmentEffects(perChar + 0x100);   // Ghidra: FUN_00538fa0
                                QuickMenuConfirmSound();

                                perChar[0x207] = 0x28;
                                perChar[0x2be] &= 0xf;
                                *slot &= 0xf7ffffff;
                            }
                        }
                        break;
                    }

                    // --- 6 : les sorts, parcourus en sautant ceux qui ne sont pas appris --------
                    case 6:
                    {
                        byte school = perChar[0x2b5];
                        int spells = s_SpellCounts_00564600[school];
                        uint* learned = (uint*)(perChar + 0x160 + school * 8);

                        // Ghidra: puVar11 / ebx. Charge ici avec l'index d'avant, puis ecrase par
                        // le compteur d'essais de chaque recherche — voir l'en-tete.
                        int witness = perChar[0x2bf];

                        ushort held = (ushort)(*(ushort*)pad & openMask);
                        if ((held != 0 && (pad[6] & 0x80) != 0)
                            || (held == 0 && (pad[6] & 0x90) != 0))
                        {
                            int index = witness;
                            int tries = spells;
                            while (tries != 0)
                            {
                                index--;
                                if (index < 0) index = spells - 1;
                                if ((learned[index >> 5] & (1u << (index & 0x1f))) != 0) break;
                                tries--;
                            }
                            witness = tries;
                            perChar[0x2bf] = (byte)index;
                        }

                        held = (ushort)(*(ushort*)pad & openMask);
                        if ((held != 0 && (pad[6] & 0x20) != 0)
                            || (held == 0 && (pad[6] & 0x60) != 0))
                        {
                            int index = perChar[0x2bf];
                            int tries = spells;
                            while (tries != 0)
                            {
                                index = (index + 1) % spells;
                                if ((learned[index >> 5] & (1u << (index & 0x1f))) != 0) break;
                                tries--;
                            }
                            witness = tries;
                            perChar[0x2bf] = (byte)index;
                        }

                        if (witness != perChar[0x2bf]) QuickMenuMoveSound();

                        if ((pad[3] & 0x80) != 0)
                        {
                            perChar[0x2b6] = perChar[0x2bf];
                            perChar[0x2be] &= 0xf;
                            *slot &= 0xf7ffffff;
                            QuickMenuConfirmSound();
                        }
                        break;
                    }

                    // --- 7 : un choix simple, garde a +0x2b6 -----------------------------------
                    case 7:
                        if ((*(ushort*)(pad + 2) & 0x8000) != 0)
                        {
                            perChar[0x2b6] = perChar[0x2bf];
                            perChar[0x2be] &= 0xf;
                            *slot &= 0xf7ffffff;
                            QuickMenuConfirmSound();
                        }
                        break;

                    // --- 8 : trois reglages selon la tranche ou tombe l'entree ------------------
                    case 8:
                        if ((*(ushort*)(pad + 2) & 0x8000) != 0)
                        {
                            byte pick = perChar[0x2bf];
                            if (pick <= 2) perChar[0x2b7] = pick;
                            if (pick >= 3 && pick <= 4) perChar[0x2b9] = (byte)((pick - 3) ^ 1);
                            if (pick >= 5 && pick <= 6) perChar[0x2ba] = (byte)((pick - 5) ^ 1);
                            QuickMenuConfirmSound();
                        }
                        break;

                    // --- 9 : passer la main a un autre membre ----------------------------------
                    case 9:
                    {
                        ushort confirmed = *(ushort*)(pad + 2);
                        if ((confirmed & 0x8000) != 0)
                        {
                            *(ushort*)(pad + 2) = (ushort)(confirmed & 0xfff);
                            pad[0x0e] = 0;

                            byte pick = perChar[0x2bf];
                            uint* pickFlags = (uint*)(Inp.s_SlotFlags_005e93c0 + pick * 0x9c);

                            if ((byte)Mem.s_Unknown_005de538 == pick
                                || (*pickFlags & 0x50000000) != 0)
                            {
                                QuickMenuRefuseSound();
                            }
                            else
                            {
                                // Ecriture d'octet dans l'original ; les huit ecrivains du global
                                // n'y posent jamais plus de 255, donc l'affectation entiere donne
                                // la meme valeur — voir DataSymbols.md, « 0x005de538 ».
                                Mem.s_Unknown_005de538 = pick;

                                if ((*pickFlags & 0x10000) == 0)
                                {
                                    QuickMenuConfirmSound();
                                }
                                else
                                {
                                    // Le membre vise etait mene par l'IA : on lui donne la manette.
                                    byte* mainMem = GetMainMemPtr();
                                    byte leader = (byte)Mem.s_Unknown_005de538;
                                    byte* leaderBlock = mainMem + 0xf0000 + leader * 0x2000;

                                    leaderBlock[0x2d0] = (byte)s_PartySize_005e9020;
                                    leaderBlock[0x2be] = (byte)(
                                        s_MenuLayout_0056a850[(leaderBlock[0x150] & 7) * 9] - 1);
                                    leaderBlock[0x2bf] = leader;
                                    leaderBlock[0x2b8] = 0;

                                    perChar[0x2b8] = 1;
                                    perChar[0x2be] &= 0xf;

                                    // Les deux octets d'affectation de manette s'echangent entre
                                    // l'entree de ce membre et celle du nouveau meneur.
                                    byte* leaderEntry = Inp.s_PlayerSlots_005e9188 + leader * 0x4c;
                                    byte keptKind = entry[9];
                                    byte keptPad = entry[8];
                                    entry[8] = leaderEntry[0];
                                    entry[9] = leaderEntry[1];
                                    leaderEntry[0] = keptPad;
                                    leaderEntry[1] = keptKind;

                                    *(uint*)(Inp.s_SlotFlags_005e93c0 + leader * 0x9c) &= 0xfffeffff;
                                    *slot |= 0x10000;
                                    QuickMenuConfirmSound();
                                }
                            }

                            perChar[0x2be] &= 0xf;
                            *slot &= 0xf7ffffff;
                            *(uint*)(Inp.s_SlotFlags_005e93c0
                                     + (byte)Mem.s_Unknown_005de538 * 0x9c) &= 0xf7ffffff;
                        }
                        break;
                    }
                }

                // --- la validation des cinq emplacements d'equipement --------------------------
                if (kind < 5)
                {
                    byte item = perChar[perChar[0x2bf] + 0x2d1];

                    if ((pad[3] & 0x80) != 0)
                    {
                        if ((*(ushort*)(perChar + item * 0x280 + 0x312)
                             & *(ushort*)(perChar + 0x112)) == 0)
                        {
                            QuickMenuRefuseSound();
                        }
                        else
                        {
                            // Les deux mains, +0x2b3 et +0x2b4, ne peuvent pas tenir le meme objet :
                            // on refuse quand la validation viserait une main alors que l'autre le
                            // porte deja. Tout autre emplacement passe.
                            byte* left = perChar + 0x2b3;
                            byte* right = perChar + 0x2b4;

                            if ((target == left && perChar[0x2b4] != item)
                                || (target == right && perChar[0x2b3] != item)
                                || (target != left && target != right))
                            {
                                // Revalider l'objet deja en place le retire.
                                *target = *target == item ? (byte)0xff : item;

                                CommitStagedStatBlock(perChar);              // Ghidra: FUN_00538ab0
                                ScaleStatBlockByDifficulty(perChar + 0x100); // Ghidra: FUN_00538f50
                                ComputeDerivedStats(perChar);                // Ghidra: FUN_00538c50
                                ApplyEquipmentStatBonuses(perChar);          // Ghidra: FUN_00538e20
                                FinalizeEquipmentEffects(perChar + 0x100);   // Ghidra: FUN_00538fa0
                                QuickMenuConfirmSound();
                            }
                            else
                            {
                                QuickMenuRefuseSound();
                            }
                        }
                    }
                }
            }
        }

        if (close)
        {
            perChar[0x2be] &= 0xf;
            *slot &= 0xf7ffffff;
            QuickMenuCancelSound();
        }

        // ------------------------------------------------------ la queue : le curseur de categorie
        ushort menuHeld = (ushort)(*(ushort*)pad & Input.CombatMask(*(ushort*)(perChar + 0x1fca)));

        // On ne change de categorie que si le bouton est tenu, ou si l'anneau n'est pas ouvert.
        if (menuHeld != 0 || (perChar[0x2be] & 0x80) == 0)
        {
            byte before = perChar[0x2be];
            int beforeCategory = before & 0xf;
            byte repeat = pad[6];
            byte forward;

            if (menuHeld != 0)
            {
                if ((repeat & 0x10) != 0) perChar[0x2be] = (byte)(before - 1);
                forward = (byte)(pad[6] & 0x40);
            }
            else
            {
                if ((repeat & 0x90) != 0) perChar[0x2be] = (byte)(before - 1);
                forward = (byte)(pad[6] & 0x60);
            }

            if (forward != 0) perChar[0x2be]++;

            sbyte categories = (sbyte)s_MenuLayout_0056a850[(perChar[0x150] & 7) * 9];
            if ((perChar[0x2be] & 0xf) == 0xf) perChar[0x2be] = (byte)(categories - 1);
            if ((perChar[0x2be] & 0xf) >= categories) perChar[0x2be] = 0;

            if (beforeCategory != (perChar[0x2be] & 0xf))
            {
                QuickMenuMoveSound();

                // La nouvelle categorie repart d'un etat par defaut, propre a son genre.
                switch ((sbyte)s_SpeechIconIndex_0056a851[
                            (perChar[0x2be] & 0xf) + (perChar[0x150] & 7) * 9])
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        perChar[0x2bf] = 0;
                        break;

                    case 6:
                        perChar[0x2d0] = 1;
                        perChar[0x2bf] = perChar[0x2b6];
                        break;

                    case 7:
                        perChar[0x2d0] = 7;
                        perChar[0x2bf] = perChar[0x2b6];
                        break;

                    case 8:
                        perChar[0x2d0] = 7;
                        perChar[0x2bf] = 0;
                        break;

                    case 9:
                        perChar[0x2d0] = (byte)s_PartySize_005e9020;
                        perChar[0x2bf] = 0;
                        break;
                }
            }
        }

        // ------------------------------------------------ la liste d'entrees de la categorie
        // Les genres 0 a 5 listent les objets portes dont le mot de type porte le bit de la
        // categorie ; les autres genres gardent la liste que le bloc ci-dessus leur a posee.
        int shown = (sbyte)s_SpeechIconIndex_0056a851[
                        (perChar[0x2be] & 0xf) + (perChar[0x150] & 7) * 9];

        uint slotBit = shown switch
        {
            0 => 0x100u,
            1 => 0x200u,
            2 => 0x400u,
            3 or 4 => 0x800u,
            5 => 0x1000u,
            _ => 0u,                            // inatteignable — voir l'en-tete
        };

        if (shown < 6)
        {
            perChar[0x2d0] = 0;

            for (int i = 0; i < 10; i++)
            {
                byte* item = perChar + 0x300 + i * 0x280;
                if (item[0] == 0 || (*(ushort*)(item + 0x12) & slotBit) == 0) continue;

                perChar[0x2d1 + perChar[0x2d0]] = (byte)i;
                perChar[0x2d0]++;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00523820 @ 0x00523820 — <b>se servir d'un objet porte</b>. Elle retrouve le bloc
    /// du personnage a partir de l'index range en <c>emplacement[8]</c>, puis lance le script de
    /// l'objet <paramref name="item"/> par <c>FUN_00523860</c>.
    ///
    /// <para>Le garde <c>item &lt; 10</c> est celui de l'original : dix objets portes au plus, et
    /// un index hors borne ne fait rien du tout.</para>
    /// </summary>
    private static void UseCarriedItem_00523820(uint* slot, uint item)
    {
        if (item >= 10) return;

        byte* record = GetMainMemPtr() + (((byte*)slot)[8] + 0x78) * 0x2000;
        RunItemScriptSaved_00523860(slot, record, record + item * 0x280 + 0x300);
    }

    /// <summary>
    /// Ghidra: FUN_00523860 @ 0x00523860 — le meme sas que <see cref="RunEntityScript"/> : bascule
    /// l'allocateur de <c>winsys3d</c> sur le tampon de travail le temps du script d'objet, si tant
    /// est qu'il y ait un etat a preserver.
    ///
    /// <para><b>Ghidra perd un parametre dans la seconde branche</b> — elle y appelle
    /// <c>FUN_00523aa0(unaff_retaddr, param_1, param_2, …)</c>, decale d'un cran, et prend le mot
    /// de retour pour un argument. Les deux branches passent les memes trois arguments ; c'est la
    /// forme retenue ici, et <c>FUN_00539190</c> porte le meme defaut sur le meme squelette.</para>
    /// </summary>
    private static void RunItemScriptSaved_00523860(uint* slot, byte* record, byte* item)
    {
        byte* script = item + *(ushort*)(item + 0x80);

        if (Winsys3d.Ordinal_46() < 0)
        {
            RunItemScript_00523AA0(slot, record, item, script);
            return;
        }

        *(int*)Mem.Arena(0x005e9318) = Winsys3d.Ordinal_46();
        Winsys3d.Ordinal_45(GetMainMemPtr() + 0x1c0000);
        RunItemScript_00523AA0(slot, record, item, script);
        Winsys3d.Ordinal_45((void*)(nint)(*(int*)Mem.Arena(0x005e9318)));
    }
}
