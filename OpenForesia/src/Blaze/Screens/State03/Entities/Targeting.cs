// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE CIBLAGE — trouver quelqu'un, sans rien decider de ce qu'on lui fera.
//
// `FindNearestOpponent_00474EB0`, `FindNearestSceneEntity_00474DB0`,
// `FindNearestPartyMember_00474CD0`, et `PickCastTarget_0051A5D0` pour une incantation.
//
// Fichier recolle de DEUX plages (decision D7) : `PickCastTarget` vivait sous le bandeau des sorts
// de monstre, les trois autres sous celui des postures. C'est le meme geste.
//
// Le choix de cible en cone, propre au tir, est dans `Combat/Projectiles.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_0051a5d0 @ 0x0051a5d0 — <b>la reconnaissance de la combinaison de boutons</b>
    /// saisie pendant l'incantation. Elle rend l'indice du sort dont la combinaison correspond, ou
    /// <c>-1</c>.
    ///
    /// <para>
    /// <b>C'est la table de sorts elle-meme qui porte les combinaisons.</b> Chaque fiche fait
    /// <c>0x30</c> octets ; <c>+0x1f</c> est la <b>longueur</b> de sa combinaison et <c>+0x20</c>
    /// ouvre la suite des coups attendus. La borne du balayage vient de
    /// <see cref="s_SpellCounts_00564600"/>, indexee par l'ecole <c>personnage[0x2b5]</c>.
    /// </para>
    /// <para>
    /// Cote joueur, <see cref="ApplyGroundMotion_0051A020"/> empile chaque bouton de face presse dans
    /// <c>entree[0x0c + n]</c> sous la forme <c>(manette[2..3] &gt;&gt; 8) &amp; 0xf0</c> et compte
    /// les coups dans <c>entree[0x0b]</c>, plafonne a <c>0x10</c>. <b>Les deux bornes se repondent
    /// exactement</b> : seize octets tiennent entre <c>+0x20</c> et la fin de la fiche, ce qui
    /// confirme les deux lectures l'une par l'autre.
    /// </para>
    /// <para>
    /// La comparaison est un <c>memcmp</c> que Ghidra a deroule ; il est reecrit en boucle. Le test
    /// <c>longueur != 0</c> est invariant de boucle dans le binaire — il est remonte avant, ce qui
    /// ne change rien : une combinaison vide ne correspond a rien et le balayage rendait
    /// <c>-1</c> apres l'avoir parcouru pour rien.
    /// </para>
    /// </summary>
    private static int PickCastTarget_0051A5D0(byte* entry, byte* perChar)
    {
        byte school = perChar[0x2b5];
        int count = s_SpellCounts_00564600[school];
        if (count == 0) return -1;

        byte length = entry[0x0b];
        if (length == 0) return -1;

        // La fiche de sort commence 0x20 octets plus tot ; on balaye directement les combinaisons.
        byte* combo = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c)
                                + school * 4) + 0x20;

        for (int index = 0; index < count; index++, combo += 0x30)
        {
            if (length != combo[-1]) continue;          // la longueur, en +0x1f de la fiche

            int matched = 0;
            while (matched < length && entry[0x0c + matched] == combo[matched]) matched++;

            if (matched == length) return index;
        }

        return -1;
    }

    /// <summary>
    /// Ghidra: FUN_00474eb0 @ 0x00474eb0 — <b>cherche la cible la plus proche du camp d'en
    /// face</b>. Dix-neuf lignes d'aiguillage : selon le mode et le bit <c>0x800000</c> qui
    /// distingue un membre du groupe, elle balaie soit la table des entites de scene, soit celle
    /// du groupe.
    ///
    /// <para>
    /// Avec le mode <c>1</c> — le seul que la carte du monde emploie — un membre du groupe cherche
    /// parmi les entites de scene, et une entite de scene parmi les membres du groupe. C'est donc
    /// bien « l'adversaire le plus proche » et non un obstacle quelconque : le nom
    /// <c>FindNearbyObstacle</c> que ce depot lui donnait d'apres son seul site d'appel etait une
    /// supposition, corrigee ici.
    /// </para>
    /// <para>
    /// <b>Ghidra la declare <c>void</c></b> alors que son appelant lit son resultat. Ce n'est pas
    /// une signature manquee : les deux branches sont des appels <b>terminaux</b>, et le resultat
    /// de l'appelee traverse tel quel. Rendu explicitement ici.
    /// </para>
    /// </summary>
    private static byte* FindNearestOpponent_00474EB0(uint* entity, uint mask, int mode)
    {
        if (mode == 0)
        {
            if ((*entity & 0x800000) == 0) return FindNearestSceneEntity_00474DB0(entity, mask);
        }
        else if ((*entity & 0x800000) != 0)
        {
            return FindNearestSceneEntity_00474DB0(entity, mask);
        }

        return FindNearestPartyMember_00474CD0(entity);
    }

    /// <summary>
    /// Ghidra: FUN_00474db0 @ 0x00474db0 — la plus proche des 0x20 entites de scene
    /// (<c>DAT_005ecaa0</c>) dans un rayon de <b>600 unites</b>, en excluant la demanderesse
    /// elle-meme.
    ///
    /// <para>
    /// Quatre conditions filtrent les candidates : active (<c>0x80000000</c>), sans aucun des bits
    /// <c>0x50002040</c>, sans cible deja tenue a <c>+0x74</c>, et sans compte a rebours
    /// d'invulnerabilite a <c>+0x96</c>.
    /// </para>
    /// <para>
    /// Le <paramref name="mask"/> est teste par <c>(*bloc &amp; ~mask) == 0</c> contre le mot de
    /// <c>+0x14</c> du bloc de statistiques. <b>Le seul appelant passe -1</b>, ce qui rend
    /// <c>~mask</c> nul et la condition toujours vraie : le filtre existe mais dort.
    /// </para>
    /// </summary>
    private static byte* FindNearestSceneEntity_00474DB0(uint* entity, uint mask)
    {
        byte* self = (byte*)entity;
        int x = *(int*)(self + 0x1c);
        int y = *(int*)(self + 0x20);
        int z = *(int*)(self + 0x24);

        int best = 600;
        byte* found = null;

        byte* candidate = Mem.Entity(0x005ecaa0);
        byte* stats = s_EntityStatBlocks_005ea560 + 0x14;

        for (int i = 0x20; i != 0; i--, candidate += 0x9c, stats += 0x120)
        {
            if ((*(uint*)candidate & 0x80000000) != 0
                && (*(uint*)candidate & 0x50002040) == 0
                && (*(uint*)stats & ~mask) == 0
                && *(int*)(candidate + 0x74) == 0
                && *(short*)(candidate + 0x96) == 0)
            {
                int dx = (x - *(int*)(candidate + 0x1c)) >> 0xc;
                int dy = (y - *(int*)(candidate + 0x20)) >> 0xc;
                int dz = (z - *(int*)(candidate + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                if (distance < best && self != candidate)
                {
                    best = distance;
                    found = candidate;
                }
            }
        }

        return found;
    }

    /// <summary>
    /// Ghidra: FUN_00474cd0 @ 0x00474cd0 — la jumelle de
    /// <see cref="FindNearestSceneEntity_00474DB0"/> sur la table du groupe
    /// (<c>DAT_005e93c0</c>), bornee a la taille reelle du groupe et sans le filtre de masque.
    /// Meme rayon de 600, memes quatre conditions, meme exclusion de soi.
    /// </summary>
    private static byte* FindNearestPartyMember_00474CD0(uint* entity)
    {
        byte* self = (byte*)entity;
        int best = 600;
        byte* found = null;

        int x = *(int*)(self + 0x1c);
        int y = *(int*)(self + 0x20);
        int z = *(int*)(self + 0x24);

        if (s_PartySize_005e9020 == 0) return null;

        byte* candidate = Inp.s_SlotFlags_005e93c0;

        for (int i = s_PartySize_005e9020; i != 0; i--, candidate += 0x9c)
        {
            if ((*(uint*)candidate & 0x80000000) != 0
                && (*(uint*)candidate & 0x50002040) == 0
                && *(int*)(candidate + 0x74) == 0
                && *(short*)(candidate + 0x96) == 0)
            {
                int dx = (x - *(int*)(candidate + 0x1c)) >> 0xc;
                int dy = (y - *(int*)(candidate + 0x20)) >> 0xc;
                int dz = (z - *(int*)(candidate + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));

                if (distance < best && self != candidate)
                {
                    best = distance;
                    found = candidate;
                }
            }
        }

        return found;
    }
}
