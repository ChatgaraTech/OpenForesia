// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE LANCEMENT D'UN SORT — la plomberie que les quatre ecoles partagent.
//
// `CastSpell_0053A980` est la porte unique : une incantation au clavier, l'opcode 14 d'un script
// d'objet et le mot +0x06 d'une entite de scene entrent tous les trois par ici. Elle recoit une
// FAMILLE et un INDICE, adresse le repertoire d'etat en `+0x9c` a `famille * 4` puis
// `indice * 0x30`, et fait naitre le sous-type que la fiche porte en `+0x10`.
//
// C'est pourquoi un sort fait exactement la meme chose partout : la fiche decide, pas l'appelant.
// Le lanceur n'est d'ailleurs pas releve par la sonde - c'est la fiche qu'on verifie.
//
// Autour de `CastSpell` : le ciblage (`PickTargetBySearch_00474AD0`, `FindTargetAround_00474BA0`),
// la reservation des creneaux d'entite (`ReserveEntitySlots_00475090`), la tenue d'une incantation
// longue (`CanSustainChannel_00475110`), la poussee au sol (`ApplyGroundMotion_0051A020`), la
// naissance de l'entite de lancement (`SpawnCastEntity_0047AAE0`) et son aure (`TickCastAura_004765D0`).
//
// Les corps des sorts eux-memes sont dans `School0Wizard.cs`, `School1Priest.cs`,
// `School2Elementalist.cs`, `Herbalism.cs` et `MonsterSpells.cs`.
// Vue d'ensemble : Documentation/Reference/Sorts.md ; feuille de route : Documentation/Etat/SortsAtteignables.md.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>Ghidra: FUN_0053a980 @ 0x0053a980 — <b>declenche le sort ou l'effet d'objet</b>
    /// designe par sa famille et son indice.
    ///
    /// <para>L'entree de description vit dans le repertoire de ressources : la table de
    /// <c>+0x9c</c> donne un tableau par famille, indexe au pas de <c>0x30</c>. Ses octets
    /// <c>+0x10</c>, <c>+0x11</c> et <c>+0x12</c> fournissent le genre d'effet et deux parametres —
    /// et ce sont eux qui servent quand l'appelant passe <c>-1</c>, ce que le script d'objet ne
    /// fait jamais mais que ses autres appelants font.</para>
    ///
    /// <para>Comme <see cref="ItemOpSpawnSelf"/>, elle neutralise trois champs de l'entite le temps
    /// de l'appel — <c>+0x74</c>, <c>+0x78</c> et le mot de <c>+0x0a</c> — puis les restaure.
    /// L'entree trouvee est rendue : deux appelants la relisent.</para></summary>
    private static byte* CastSpell_0053A980(uint* caster, int family, int index, int a, int b)
    {
        NoteSpellCast(family, index);   // ECHAFAUDAGE : hors du binaire, voir la sonde plus bas.

        byte* e = (byte*)caster;

        // Le mot de +6 porte les DEUX : la famille en octet haut, l'indice en octet bas. Ghidra
        // l'ecrit `CONCAT31((int3)param_2, (char)param_3)` tronque en `short`, ce qui se lit mal ;
        // le binaire est sans ambiguite (`mov dh,cl ; mov dl,al ; mov [esi+6],dx` en 0x0053a991).
        *(short*)(e + 6) = (short)(((family & 0xff) << 8) | (index & 0xff));

        byte* keptWatched = *(byte**)(e + 0x74);
        byte* keptFound = *(byte**)(e + 0x78);
        ushort keptCounter = *(ushort*)(e + 0x0a);

        byte* entry = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c) + family * 4)
                      + index * 0x30;

        *(byte**)(e + 0x74) = e;
        *(byte**)(e + 0x78) = null;
        *(ushort*)(e + 0x0a) = 0;

        if (a < 0) a = entry[0x11];
        if (b < 0) b = entry[0x12];

        SpawnEffectEntity(e, entry[0x10], (byte)a, b);   // Ghidra: FUN_0047a9d0

        *(byte**)(e + 0x78) = keptFound;
        *(byte**)(e + 0x74) = keptWatched;
        *(ushort*)(e + 0x0a) = keptCounter;

        return entry;
    }

    /// <summary>
    /// Ghidra: FUN_00474ad0 @ 0x00474ad0 — <b>la recherche de cible</b> de l'opcode 17 du script
    /// d'objet. Elle balaie autour de l'acteur avec la sonde ci-dessous et range le premier
    /// trouve en <c>+0x78</c> de l'enregistrement.
    ///
    /// <para>
    /// <b>L'argument choisit le camp, le bit <c>0x800000</c> dit d'ou l'on part.</b> Le bit
    /// marque un <b>membre du groupe</b>, et les deux balayages sont les memes que ceux de la
    /// melee : <see cref="SweepSceneEntitiesForMeleeHit_00519AF0"/> pour la scene,
    /// <see cref="SweepPartyForMeleeHit_00519C30"/> pour le groupe. Le croisement des deux donne :
    /// </para>
    /// <list type="bullet">
    /// <item><b>mode 0</b> — le balayage <b>du meme camp</b> : un membre du groupe cherche un
    /// membre du groupe, une entite de scene cherche une entite de scene. C'est le mode de la
    /// potion qu'on donne a quelqu'un.</item>
    /// <item><b>mode non nul</b> — le camp <b>d'en face</b>, exactement l'aiguillage de
    /// <see cref="ProbeMeleeTarget_00474CA0"/>.</item>
    /// </list>
    /// <para>
    /// <b>Le mode 0 pose le bit <c>0x2000</c> sur l'acteur le temps du balayage</b>, puis restaure
    /// le mot d'etat entier. C'est ce qui empeche le balayage du groupe de se rendre lui-meme
    /// quand la source est elle-meme dans le groupe.
    /// </para>
    /// <para>
    /// <b>Le piege des arguments, deja rencontre sur la sœur <c>FUN_00474ca0</c>.</b> Ghidra rend
    /// deux des quatre appels sans leur quatrieme argument. Le desassembleur tranche : le
    /// <c>push 800h</c> est fait <b>une seule fois, avant le branchement</b> (0x00474b11 et
    /// 0x00474b47), donc <b>les quatre appels recoivent <c>0x800</c></b>. Rien a deviner.
    /// </para>
    /// </summary>
    /// <returns><b>0</b> quand une cible est trouvee, <b>1</b> sinon — et dans ce cas
    /// <c>+0x78</c> recoit l'acteur lui-meme, jamais un pointeur nul.</returns>
    private static uint PickTargetBySearch_00474AD0(uint* slot, byte mode)
    {
        byte* record = (byte*)slot;
        byte* actor = *(byte**)(record + 0x74);

        // Les six mots de la sonde, empiles sur place a chaque appel. Le sens de chacun est
        // etabli par TestMeleeReach_00535A90 : decalage de cap, avance, demi-largeur, tolerance
        // en hauteur, ouverture, profondeur. Ici un cone large et court, sans decalage de cap.
        short* probe = stackalloc short[6] { 0, 10, 0x28, 100, 0x100, 0x96 };

        byte* found;

        if (mode == 0)
        {
            // Ghidra: `mov eax,edi ; or ah,20h` — le bit 0x2000, pose puis retire.
            uint flags = *(uint*)actor;
            *(uint*)actor = flags | 0x2000;

            found = (flags & 0x800000) != 0
                ? SweepPartyForMeleeHit_00519C30(actor, (int*)(actor + 0x1c), probe, 0x800)
                : SweepSceneEntitiesForMeleeHit_00519AF0(actor, (int*)(actor + 0x1c), probe, 0x800);

            *(uint*)actor = flags;
        }
        else
        {
            found = (*(uint*)actor & 0x800000) != 0
                ? SweepSceneEntitiesForMeleeHit_00519AF0(actor, (int*)(actor + 0x1c), probe, 0x800)
                : SweepPartyForMeleeHit_00519C30(actor, (int*)(actor + 0x1c), probe, 0x800);
        }

        if (found != null)
        {
            *(byte**)(record + 0x78) = found;
            return 0;
        }

        *(byte**)(record + 0x78) = actor;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00474ba0 @ 0x00474ba0 — <b>la meme recherche de cible que
    /// <see cref="PickTargetBySearch_00474AD0"/>, mais qui rend le resultat</b> au lieu de le ranger.
    /// Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Les deux fonctions se suivent dans le binaire et ont le meme corps a la queue pres</b> :
    /// meme sonde de six mots, meme bit <c>0x2000</c> pose puis retire en mode 0, meme aiguillage
    /// des deux balayages. Elles ne different que par ce qu'elles font du resultat — celle-la
    /// l'ecrit en <c>+0x78</c> et rend un etat, celle-ci rend le pointeur. Le compilateur a emis
    /// les deux ; elles restent deux ici, comme dans le binaire.
    /// </para>
    /// <para>
    /// <b>Elle ne rend jamais <c>null</c> quand l'acteur existe</b> : faute de cible, elle rend
    /// l'acteur lui-meme. C'est ce qui permet a ses appelants de ranger le resultat sans garde.
    /// </para>
    /// <para>
    /// Ghidra rend deux des quatre appels de balayage sans leur quatrieme argument, le meme
    /// artefact que sur ses deux sœurs : le <c>push 800h</c> est fait avant le branchement, donc
    /// les quatre le recoivent.
    /// </para>
    /// </summary>
    private static byte* FindTargetAround_00474BA0(byte* record, int mode)
    {
        byte* actor = *(byte**)(record + 0x74);
        if (actor == null) return null;

        short* probe = stackalloc short[6] { 0, 10, 0x28, 100, 0x100, 0x96 };

        byte* found;

        if (mode == 0)
        {
            uint flags = *(uint*)actor;
            *(uint*)actor = flags | 0x2000;

            found = (flags & 0x800000) != 0
                ? SweepPartyForMeleeHit_00519C30(actor, (int*)(actor + 0x1c), probe, 0x800)
                : SweepSceneEntitiesForMeleeHit_00519AF0(actor, (int*)(actor + 0x1c), probe, 0x800);

            *(uint*)actor = flags;
        }
        else
        {
            found = (*(uint*)actor & 0x800000) != 0
                ? SweepSceneEntitiesForMeleeHit_00519AF0(actor, (int*)(actor + 0x1c), probe, 0x800)
                : SweepPartyForMeleeHit_00519C30(actor, (int*)(actor + 0x1c), probe, 0x800);
        }

        return found != null ? found : actor;
    }

    /// <summary>
    /// Ghidra: FUN_00475090 @ 0x00475090 — <b>reserver des emplacements d'entite a l'avance</b>,
    /// sans les prendre. Decompilee dans <c>Blaze.c</c>, jamais transcrite jusqu'ici faute
    /// d'appelant.
    ///
    /// <para>
    /// La liste libre d'un pool est un octet de curseur, un octet de capacite, puis les indices
    /// disponibles. Cette fonction <b>trie les <paramref name="count"/> plus petits indices
    /// restants vers la tete</b> par une selection partielle, <b>sans avancer le curseur</b> :
    /// l'appelant peut donc lire d'avance quels indices les prochaines allocations lui donneront,
    /// et s'en servir comme d'un rendez-vous.
    /// </para>
    /// <para>
    /// <b>C'est exactement ce que fait Demon polaire</b> : il reserve deux emplacements, range
    /// leurs indices dans <c>+0x16</c> et <c>+0x17</c>, puis engendre ses deux moities — qui se
    /// retrouvent ensuite par <c>0x005edee0 + indice * 0x9c</c>, sans jamais se passer de pointeur.
    /// </para>
    /// </summary>
    /// <returns>1 si la place existait, 0 sinon — et dans ce cas rien n'est touche.</returns>
    private static uint ReserveEntitySlots_00475090(byte* pool, int count)
    {
        if (pool[1] < count + pool[0]) return 0;

        byte* list = pool + pool[0] + 2;

        for (; count != 0; count--, list++)
        {
            int smallest = 0;
            byte value = *list;

            for (int i = 1; i < count; i++)
            {
                if (list[i] < value)
                {
                    smallest = i;
                    value = list[i];
                }
            }

            byte swap = list[smallest];
            list[smallest] = *list;
            *list = swap;
        }

        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_00475110 @ 0x00475110 — <b>le lanceur tient-il encore ?</b> Decompilee dans
    /// <c>Blaze.c</c>. C'est la garde des sorts <b>entretenus</b> : elle rend <b>0</b> tant que le
    /// lanceur peut continuer, <b>1</b> des qu'il faut couper.
    ///
    /// <para>
    /// Trois conditions doivent tenir ensemble pour continuer : le lanceur est <b>vivant</b>
    /// (<c>0x80000000</c>), il n'est <b>ni mort ni en train de mourir</b> (<c>0x50000000</c>
    /// absent), et son bloc de combat ne porte <b>aucune</b> alteration du masque
    /// <c>0x400003e8</c> en <c>+0x40</c> — le meme masque que
    /// <see cref="ApplyGroundMotion_0051A020"/> teste avant d'ouvrir la posture d'incantation.
    /// Etourdi, endormi ou petrifie, on ne tient pas un sort.
    /// </para>
    /// <para>
    /// Le bloc vient de la meme paire que <see cref="StatusBubbleHeight_00538BC0"/> : un membre du
    /// groupe le tient de <c>MainMem</c>, une entite de scene de
    /// <see cref="s_EntityStatBlocks_005ea560"/>.
    /// </para>
    /// </summary>
    private static uint CanSustainChannel_00475110(byte* record)
    {
        uint* actor = *(uint**)(record + 0x74);
        uint flags = *actor;

        byte* block = (flags & 0x800000) == 0
            ? s_EntityStatBlocks_005ea560 + (byte)actor[2] * 0x120
            : GetMainMemPtr() + (byte)actor[2] * 0x2000 + 0xf0100;

        if ((flags & 0x80000000) != 0
            && (flags & 0x50000000) == 0
            && (*(uint*)(block + 0x40) & 0x400003e8) == 0)
        {
            return 0;
        }

        return 1;
    }

    /// <summary>Ghidra: FUN_0051a020 @ 0x0051a020 (221 l.) — l'integration du deplacement au sol,
    /// appelee quand le personnage n'est pas en l'air.</summary>
    private static void ApplyGroundMotion_0051A020(uint* slot, byte* entry, byte* perChar, byte* pad, byte* anim)
    {
        byte* s = (byte*)slot;

        switch (perChar[0x150] & 7)
        {
            // ------------------------------------------------ la marche et sa variante
            case 0:
            case 6:
                if (pad[0x0e] != 0
                    && (sbyte)perChar[0x2b2] != -1
                    && s_Unknown_00566c84 != 0
                    && (*slot & 0x44) == 0
                    && (*(uint*)(perChar + 0x140) & 0x400003c0) == 0)
                {
                    *(uint*)(s + 0x18) = 5;
                    *slot = (*slot & 0xfeffffff) | 4;
                }

                if ((*(ushort*)pad & Input.CombatMask(*(ushort*)(perChar + 0x1fc4))) == 0 && (*slot & 0x2000000) != 0)
                {
                    *slot &= 0xfffffffb;
                }

                if (*(short*)(s + 0x18) == 5)
                {
                    *(int*)(s + 0x5c) = 0;
                    return;
                }

                break;

            // ------------------------------------------------ les quatre postures d'incantation
            case 1:
            case 3:
            case 5:
            case 7:
            {
                byte* spellSet = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                           + perChar[0x2b5] * 4);
                uint held = *slot;

                if ((held & 4) == 0)
                {
                    if ((held & 0x40) != 0) return;
                    if ((*(uint*)(perChar + 0x140) & 0x400003e8) != 0) return;

                    if ((*(ushort*)(pad + 2) & 1) != 0 && s_Unknown_00566c84 != 0)
                    {
                        // Le MODE COMBINAISON, par le bouton 0x0001 (SELECT, F1 au clavier) : compte
                        // a rebours a zero et saisie remise a vide. Tant que le bouton est tenu, les
                        // boutons de face s'empilent ; relache, PickCastTarget_0051A5D0 cherche la
                        // fiche dont la combinaison correspond (plus bas). Le bouton de classe, lui,
                        // charge le sort memorise. (Ce commentaire disait « annulation » jusqu'au
                        // 2026-10-02 : c'etait faux.)
                        *slot = held | 0x1000004;
                        *(uint*)(s + 0x18) = 5;
                        entry[0x0b] = 0;
                        *(ushort*)(pad + 0x0c) = 0;
                        *(ushort*)(pad + 0x0e) = 0;
                        *(short*)(s + 0x14) = 0;
                        SpawnCastEntity_0047AAE0(s, 0xbd, 0, 0);
                        return;
                    }

                    if ((Input.CombatMask(*(ushort*)(perChar + 0x1fc4)) & *(ushort*)(pad + 2)) == 0) return;
                    if (s_Unknown_00566c84 == 0) return;

                    *(uint*)(s + 0x18) = 5;
                    *slot = held | 0x1000004;

                    byte* spell = spellSet + perChar[0x2b6] * 0x30;
                    *(ushort*)(s + 0x14) = spell[0x13];
                    *(ushort*)(perChar + 0x204) = *(ushort*)(spell + 0x14);
                    SpawnCastEntity_0047AAE0(s, 0xbd, 0, 0);
                    *(uint*)Mem.Arena(0x005e99c0) = 0;
                    TriggerEquippedItems_005238E0(slot, 5);
                    return;
                }

                *(int*)(s + 0x5c) = LegacyPlus.Coutures.VitesseEnIncantation(slot, *(int*)(s + 0x5c));   // Couture legacy ++ (LP-006)

                if (*(short*)(s + 0x18) == 5)
                {
                    if (*(short*)(s + 0x14) == 0)
                    {
                        if ((*(ushort*)pad & 1) == 0)
                        {
                            // Le bouton est relache : on choisit la cible et on passe a l'etat 6.
                            *slot = held & 0xfeffffff;
                            *(uint*)(s + 0x18) = 6;
                            *(short*)(s + 0x14) = 5;

                            int chosen = PickCastTarget_0051A5D0(entry, perChar);
                            bool armed = false;

                            if (chosen >= 0)
                            {
                                uint* mask = (uint*)(perChar + ((chosen >> 5) + 0x58 + perChar[0x2b5] * 2) * 4);
                                if ((*mask & (1u << (chosen & 0x1f))) != 0)
                                {
                                    *(ushort*)(perChar + 0x204) = *(ushort*)(spellSet + chosen * 0x30 + 0x14);
                                    *(short*)(s + 0x16) = (short)chosen;
                                    armed = true;
                                }
                            }

                            if (!armed)
                            {
                                *slot = (*slot & 0xfffffffb) | 0x1000000;
                                *(uint*)(s + 0x18) = 0;
                            }
                        }
                        else if ((*(ushort*)(pad + 2) & 0xf000) != 0 && entry[0x0b] < 0x10)
                        {
                            // Le bouton est tenu : chaque bouton de face s'empile dans la combinaison.
                            // Écart legacy ++ (LB-058) : l'original enregistrait le mot tel quel ;
                            // il est rendu au codage de 1998 avant, pour que le bouton pressé soit
                            // celui que le menu dessine.
                            entry[0x0c + entry[0x0b]] = (byte)((Input.ComboButtons(*(ushort*)(pad + 2)) >> 8) & 0xf0);
                            entry[0x0b]++;
                        }
                    }
                    else
                    {
                        if ((*(ushort*)pad & Input.CombatMask(*(ushort*)(perChar + 0x1fc4))) == 0)
                        {
                            *slot = (held & 0xfffffffb) | 0x1000000;
                            *(uint*)(s + 0x18) = 0;
                        }
                        else
                        {
                            short remaining = (short)(*(short*)(s + 0x14) - 1);
                            *(short*)(s + 0x14) = remaining;
                            if (remaining == 0)
                            {
                                *(uint*)(s + 0x18) = 6;
                                *slot = held & 0xfeffffff;
                                *(short*)(s + 0x14) = 5;

                                uint index = perChar[0x2b6];
                                uint* mask = (uint*)(perChar + (((int)index >> 5) + 0x58 + perChar[0x2b5] * 2) * 4);
                                if ((*mask & (1u << (perChar[0x2b6] & 0x1f))) == 0) index = 0xffffffff;
                                *(short*)(s + 0x16) = (short)index;
                            }
                        }
                    }
                }

                if (*(short*)(s + 0x18) == 6)
                {
                    held = *slot;
                    if ((held & 0x2000000) != 0)
                    {
                        *(short*)(s + 0x14) = (short)(*(short*)(s + 0x14) - 1);
                        if (*(short*)(s + 0x14) == 0)
                        {
                            *(uint*)(s + 0x18) = 0;
                            *slot = (held & 0xfffffffb) | 0x1000000;
                            return;
                        }

                        *slot = held & 0xfeffffff;

                        // L'etat et l'image sont poses d'un seul dword : l'animation demarre a sa
                        // DERNIERE image.
                        *(uint*)(s + 0x18) = ((uint)(*(ushort*)(anim + 0x16) - 1) << 0x10) | 6;

                        if (*(short*)(s + 0x16) >= 0)
                        {
                            *(uint*)Mem.Arena(0x005e99c0) = 0;
                            TriggerEquippedItems_005238E0(slot, 6);

                            // Le cout n'est preleve que si la reserve le couvre — sinon
                            // l'incantation part sans rien consommer ni produire.
                            if (*(ushort*)(perChar + 0x204) <= (int)*(short*)(perChar + 0x14e))
                            {
                                *(ushort*)(perChar + 0x14e) =
                                    (ushort)(*(short*)(perChar + 0x14e) - *(ushort*)(perChar + 0x204));
                                CastSpell_0053A980(slot, perChar[0x2b5], *(short*)(s + 0x16), -1, -1);
                            }

                            *(ushort*)(s + 0x16) = 0xffff;
                            return;
                        }
                    }
                }

                break;
            }

            // ------------------------------------------------ la charge
            case 2:
                if (*(short*)(s + 0x18) != 5 || (*slot & 0x2000000) != 0)
                {
                    *(short*)(s + 0x14) = 0;
                    *slot &= 0xfffffffb;
                }

                if (*(short*)(s + 0x14) == 0)
                {
                    if (pad[0x0e] != 0
                        && (*slot & 0x44) == 0
                        && (*(uint*)(perChar + 0x140) & 0x400003c0) == 0)
                    {
                        *(ushort*)(pad + 2) &= 0x7fff;
                        *(uint*)(s + 0x18) = 5;
                        *(short*)(s + 0x14) = 0x10;
                        *slot = (*slot & 0xfeffffff) | 4;
                        *(short*)(s + 0x98) = *(short*)(s + 0x32);
                    }
                }
                else
                {
                    short remaining = (short)(*(short*)(s + 0x14) - 1);
                    *(short*)(s + 0x14) = remaining;
                    if (remaining == 0) *slot &= 0xfffffffb;
                }

                if (*(short*)(s + 0x18) == 5)
                {
                    uint posture = *(uint*)(perChar + 0x140);
                    int scale = 0x1000;
                    if ((posture & 0x4000) != 0) scale = 0x1333;
                    if ((posture & 0x8000) != 0) scale = 0xccc;
                    if ((posture & 4) != 0) scale = 0x800;

                    // La charge decelere toute seule : la vitesse suit le compte a rebours.
                    *(int*)(s + 0x5c) = *(short*)(s + 0x14) * 0x2000 * scale >> 0xc;
                    return;
                }

                break;

            // ------------------------------------------------ l'objet consommable
            case 4:
                if ((*slot & 0x44) == 0
                    && (*(ushort*)pad & Input.CombatMask(*(ushort*)(perChar + 0x1fc4))) != 0
                    && (*(ushort*)(pad + 2) & Input.CombatMask(*(ushort*)(perChar + 0x1fc4))) != 0
                    && s_Unknown_00566c84 != 0
                    && (*(uint*)(perChar + 0x140) & 0x400003c0) == 0)
                {
                    *(uint*)(s + 0x18) = 5;
                    *slot = (*slot & 0xfcffffff) | 4;
                    *(short*)(s + 0x14) = 5;
                }

                if ((*slot & 4) != 0)
                {
                    if ((*slot & 0x2000000) != 0)
                    {
                        if (*(short*)(s + 0x14) == 5)
                        {
                            // Ghidra lit ici les huit octets de +0x160 comme un COMPTE par
                            // emplacement, la ou la posture d'incantation lit la meme region
                            // comme un MASQUE de bits. Les deux vues coexistent dans l'original ;
                            // transcrites telles quelles. TODO(REVERSE) : etablir laquelle est la
                            // bonne, ou si les deux `kind` ne se recouvrent jamais.
                            byte* charges = perChar + 0x160 + perChar[0x2b5] * 8;
                            sbyte left = (sbyte)charges[perChar[0x2b6]];
                            if (left != 0)
                            {
                                charges[perChar[0x2b6]] = (byte)(left - 1);
                                s[6] = perChar[0x2b6];
                                *slot |= 0x800;
                                CastSpell_0053A980(slot, perChar[0x2b5], perChar[0x2b6], -1, -1);
                            }
                        }

                        *(short*)(s + 0x14) = (short)(*(short*)(s + 0x14) - 1);
                        if (*(short*)(s + 0x14) != 0)
                        {
                            *slot &= 0xfeffffff;
                            ushort frames = *(ushort*)(anim + 0x16);
                            *(int*)(s + 0x5c) = 0;
                            *(uint*)(s + 0x18) = ((uint)(frames - 1) << 0x10) | 5;
                            return;
                        }

                        *(uint*)(s + 0x18) = 0;
                        *slot = (*slot & 0xfffffffb) | 0x1000000;
                    }

                    *(int*)(s + 0x5c) = 0;
                    return;
                }

                break;

            default:
                *slot &= 0xfffffffb;
                break;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0047aae0 @ 0x0047aae0 — <b>fait naitre une entite d'incantation</b> dans la table
    /// <c>DAT_005edee0</c>, en recopiant les 0x27 dwords du lanceur puis en remplacant six champs.
    /// Jumelle de <c>SpawnEffectEntity</c> (<c>FUN_0047a9d0</c>) a la table pres.
    ///
    /// <para>Le masque final <c>&amp; 0x10e82f02 | 0x80109000</c> ne garde du mot d'etat copie que ce
    /// qui a un sens pour une entite d'effet.</para>
    /// </summary>
    /// <returns>1 si un emplacement etait libre, 0 sinon.</returns>
    private static uint SpawnCastEntity_0047AAE0(byte* source, byte subtype, byte param3, int param4)
    {
        int index = AllocEntitySlot((ushort*)Mem.Entity(0x006007a0));
        if (index < 0) return 0;

        byte* born = Mem.Entity(0x005edee0) + index * 0x9c;
        Mem.CopyDwords((uint*)source, (uint*)born, 0x27);

        born[0x36] |= 0x80;
        born[0x08] = (byte)index;
        born[0x05] = subtype;
        *(byte**)(born + 0x6c) = source;
        born[0x0b] = param3;
        if (param4 >= 0) born[0x0a] = (byte)param4;

        *(uint*)born = (*(uint*)born & 0x10e82f02) | 0x80109000;
        *(ushort*)(born + 0x10) = 0;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_004765d0 @ 0x004765d0 — <b>l'aureole d'incantation</b> : le gestionnaire du
    /// sous-type <c>0xbd</c>, creneau 189 de la table statique <see cref="s_StaticEntityHandlers_00551f30"/>.
    /// 350 octets, <b>absente de <c>Blaze.c</c></b>, transcrite au desassembleur — le releve est
    /// garde sous <c>Legacy/Raw/Blaze/FUN_004765D0.asm</c>.
    ///
    /// <para>
    /// C'est le pendant vivant de <see cref="SpawnCastEntity_0047AAE0"/> : ce que
    /// <see cref="ApplyGroundMotion_0051A020"/> fait naitre a chaque fois qu'un personnage prend la
    /// posture d'incantation (<c>+0x18 == 5</c>), et ce qui plantait a l'appui sur Select en foret.
    /// </para>
    ///
    /// <para>
    /// <b>La premiere entite n'est qu'une graine.</b> Le sous-type se comporte de deux façons selon
    /// que sa source porte ou non le bit <c>0x100000</c> — celui qui, dans la boucle de dessin de la
    /// grille, designe une entite <b>rendue comme modele du paquet</b>, indexee par <c>+0x04</c>.
    /// Un personnage ne le porte pas (il passe par la branche <c>0x440000</c>, le modele anime),
    /// mais <see cref="SpawnCastEntity_0047AAE0"/> le <b>force</b> sur tout ce qu'elle engendre.
    /// Le test lit donc « ma source est-elle une de mes semblables ? » :
    /// </para>
    /// <list type="bullet">
    /// <item><b>source = le personnage</b> — la graine se recopie <b>deux fois</b> par
    /// <see cref="SpawnCastEntity_0047AAE0"/>, en avançant sa rotation <c>+0x32</c> de <c>0x800</c>
    /// avant chaque copie, puis se marque <c>0x2000000</c> et s'eteint a l'image suivante. Trois
    /// tiers de tour, donc, dont il ne reste que deux exemplaires.</item>
    /// <item><b>source = la graine</b> — la copie se reaccroche au <b>personnage</b>
    /// (<c>source + 0x6c</c>), prend le modele <c>0x3d</c>, la teinte pleine <c>0xffffff</c>, et
    /// mesure une fois pour toutes la hauteur de tete du lanceur par
    /// <see cref="StatusBubbleHeight_00538BC0"/>.</item>
    /// </list>
    ///
    /// <para>
    /// Ensuite, a chaque image : la position est recopiee du lanceur avec la hauteur en decalage
    /// vertical (<c>(hauteur &amp; ~1) &lt;&lt; 11</c>), l'echelle des trois axes <b>approche</b> sa
    /// cible d'un quart de la distance restante — la cible etant <c>(hauteur &lt;&lt; 12) / 150</c>,
    /// que le binaire ecrit en multiplication magique <c>0x1B4E81B5</c> suivie de <c>sar 4</c> —
    /// et la rotation avance de <c>0x40</c> sur <c>+0x32</c>, <c>0x80</c> sur <c>+0x34</c>.
    /// </para>
    /// <para>
    /// <b>Trois conditions rendent la main</b>, et posent <c>0x2000000</c> sur le mot d'etat, ce qui
    /// bascule l'entite dans la branche d'extinction : le lanceur porte <c>0x50000000</c> ; c'est un
    /// membre du groupe (<c>0x800000</c>) qui a <b>quitte la posture 5</b> ; ou son mot d'etat
    /// vaut <c>0x400000</c> sur le masque <c>0x400004</c>. L'extinction <b>divise l'echelle par
    /// deux</b> a chaque image et rend le creneau des qu'elle passe sous <c>0x100</c>, en mettant le
    /// mot d'etat a zero — la passe <see cref="TickEntityTable_005edee0"/> relit ce mot juste apres
    /// et libere l'emplacement.
    /// </para>
    /// </summary>
    private static void TickCastAura_004765D0(byte* record)
    {
        byte* e = record;

        // ------------------------------------------------------------------ l'extinction
        if ((*(uint*)e & 0x2000000) != 0)
        {
            int scale = *(short*)(e + 0x28);
            scale -= scale >> 1;                       // la moitie, arrondie vers le haut

            *(short*)(e + 0x2c) = (short)scale;
            *(uint*)(e + 0x28) = ((uint)scale << 0x10) | (uint)scale;

            *(ushort*)(e + 0x32) += 0x40;
            *(ushort*)(e + 0x34) += 0x80;

            // Le mot d'etat a zero : la passe rend le creneau au pool 0x006007a0.
            if (scale < 0x100) *(uint*)e = 0;
            return;
        }

        // ------------------------------------------------------------------ l'amorcage
        if (*(ushort*)(e + 0x10) == 0)
        {
            byte* source = *(byte**)(e + 0x6c);

            e[0x36] |= 6;
            *(ushort*)(e + 0x10) = 1;
            *(uint*)(e + 0x38) = 0xffffff;
            *(uint*)(e + 0x28) = 0;
            *(ushort*)(e + 0x2c) = 0;
            *(ushort*)(e + 0x34) = 0;

            if ((*(uint*)source & 0x100000) == 0)
            {
                // Ghidra: LAB_004766f4 — la graine se dedouble et se marque pour l'extinction.
                *(ushort*)(e + 0x32) += 0x800;
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);
                *(ushort*)(e + 0x32) += 0x800;
                SpawnCastEntity_0047AAE0(e, 0xbd, 0, 0);

                *(uint*)e |= 0x2000000;
                return;
            }

            // La copie saute par-dessus la graine et se raccroche au lanceur lui-meme.
            byte* caster = *(byte**)(source + 0x6c);
            e[0x04] = 0x3d;
            *(byte**)(e + 0x6c) = caster;
            e[0x0b] = 0;
            e[0x0a] = 0x0f;
            *(short*)(e + 0x12) = (short)StatusBubbleHeight_00538BC0((uint*)caster);
        }

        // Ghidra: LAB_0047666c — le suivi, hors de toute branche d'etat.
        int height = *(short*)(e + 0x12);
        byte* anchor = *(byte**)(e + 0x6c);

        // `imul 0x1B4E81B5` puis `sar edx,4` et la correction de signe : une division signee par
        // 150, l'idiome du compilateur de 1998.
        int target = (height << 0xc) / 150;
        int current = *(short*)(e + 0x28);
        current += (target - current) >> 2;            // un quart de ce qui reste, par image

        *(short*)(e + 0x2c) = (short)current;
        *(uint*)(e + 0x28) = ((uint)current << 0x10) | (uint)current;

        *(int*)(e + 0x1c) = *(int*)(anchor + 0x1c);
        *(int*)(e + 0x20) = *(int*)(anchor + 0x20) + ((height & ~1) << 0xb);
        *(int*)(e + 0x24) = *(int*)(anchor + 0x24);

        uint state = *(uint*)anchor;
        bool spent;

        if ((state & 0x50000000) != 0) spent = true;
        else if ((state & 0x800000) != 0 && *(ushort*)(anchor + 0x18) != 5) spent = true;
        else spent = (state & 0x400004) == 0x400000;   // Ghidra: LAB_004766d3

        if (spent) *(uint*)e |= 0x2000000;

        // Ghidra: LAB_004766e5 — la rotation, commune aux deux branches.
        *(ushort*)(e + 0x32) += 0x40;
        *(ushort*)(e + 0x34) += 0x80;
    }
}
