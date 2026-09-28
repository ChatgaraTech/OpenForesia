// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES VOLUMES DE DECLENCHEMENT — `TestTriggerVolumes_00534CD0` et sa sonde de posture.
//
// Ce qui se declenche quand une entite ENTRE quelque part, par opposition au contact qui se
// declenche quand elle bute (`MotionAndContact.cs`). C'est par ici que passent les changements de
// sous-zone, les scenes qui s'arment, les pieges de sol.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_00519940 @ 0x00519940 — <b>le coup du groupe contre les entites de scene</b>,
    /// le miroir exact de <see cref="SweepPartyForMeleeHit_00519C30"/> : celle-la cherche quel
    /// membre un monstre touche, celle-ci quels monstres un membre touche.
    ///
    /// <para>
    /// <b>Elle s'appelait <c>EmitFootstep_00519940</c> et ce nom etait faux</b>, comme l'etaient
    /// <c>FootstepParameters</c> et <c>s_FootstepTable_00562cf0</c>, ses deux voisines. Le nom venait du site d’appel — la
    /// derniere image d'une animation — et du fait que le corps n'avait jamais ete lu. Il n'y a
    /// aucun son de pas ici : la fonction croise un gabarit avec les entites voisines, resout les
    /// degats et pousse les touches en arriere. La table de 0x00562cf0 n'est pas faite de
    /// parametres de pas mais de <b>gabarits de portee</b>, six <c>short</c> par posture, de la
    /// meme forme que la sonde que <see cref="TickSceneEntityAi_0049A980"/> pose sur sa pile — et
    /// ses quatre lignes entierement nulles (4, 7, 12, 15) sont exactement les postures que le site
    /// d'appel exclut. Decouvert le 2026-08-29, en jouant : taper plantait.
    /// </para>
    /// <para>
    /// <b>Le balayage passe par la grille spatiale</b>, pas par un tableau : trois colonnes sur
    /// trois lignes autour de la case de l'attaquant, et pour chacune la liste simplement chainee
    /// que <see cref="LinkIntoWorldGrid"/> remplit (chainage a <c>+0x68</c>). L'origine
    /// <c>0x001c5000</c> et le decalage d'une ligne vers l'arriere sont tous deux caches dans la
    /// constante <c>0x389f</c> : <c>(0x389f + ligne) * 0x20 * 4 = (ligne - 1) * 0x80 + 0x1c5000</c>.
    /// </para>
    /// <para>
    /// TODO(REVERSE): <b>la borne de ligne laisse passer une ligne de trop.</b>
    /// <c>uVar3 &lt; 0x71800</c> autorise l'indice de ligne 0x20, alors que la grille n'en a que
    /// 0x20 (0..0x1f) — la lecture tombe alors dans les quatre premiers octets de la grille
    /// suivante. C'est dans le binaire, transcrit tel quel ; a verser dans <c>legacy-bug.md</c> le
    /// jour ou on l'observe en jeu.
    /// </para>
    /// <para>
    /// La parade est <b>plus maigre que celle du groupe</b> : une entite qui porte le bit
    /// <c>0x10</c> et fait face au coup a moins d'un demi-tour pres ne fait que jouer le son 8,
    /// sans refaire ses statistiques ni noter son assaillant. C'est la seule difference de fond
    /// entre les deux miroirs.
    /// </para>
    /// </summary>
    /// <returns>La <b>derniere</b> entite blessee, ou <c>null</c>. Le site d'appel l'ignore ;
    /// <c>DAT_005de0b8</c> porte l'information utile — 1 si un coup a porte.</returns>
    private static byte* SweepSceneEntitiesForMeleeHit_00519940(byte* attacker, int* position, short* probe)
    {
        byte* mainMem = GetMainMemPtr();
        byte* last = null;

        *Mem.Camera(0x005de0b8) = 0;

        uint column = (uint)((((position[0] >> 0xc) + 0x2000) >> 9) - 1);
        uint row = (uint)(((((position[2] >> 0xc) + 0x2000) >> 9) + 0x389f) * 0x20);

        for (int lines = 3; lines != 0; lines--, row += 0x20)
        {
            // Ghidra remet la colonne de depart par `uVar5 = uVar6 - 2` apres la boucle interne ;
            // ici le curseur est local au tour, donc `column` ne bouge pas et le geste disparait.
            uint cell = column;

            for (int columns = 3; columns != 0; columns--, cell++)
            {
                if (cell >= 0x20 || row >= 0x71800) continue;

                for (byte* victim = *(byte**)(mainMem + (cell + row) * 4);
                     victim != null;
                     victim = *(byte**)(victim + 0x68))
                {
                    uint flags = *(uint*)victim;

                    if ((flags & 0x400000) == 0) continue;
                    if (LegacyPlus.Coutures.VictimeExclue(LegacyPlus.Coutures.Balayage.Melee, attacker, victim, flags & 0x50102040) != 0) continue;   // Couture legacy ++ (LP-004)
                    if (LegacyPlus.Coutures.VictimeProtegee(LegacyPlus.Coutures.Balayage.Melee, attacker, victim, *(short*)(victim + 0x96) != 0)) continue;      // deja en train de clignoter — Couture legacy ++ (LP-004)
                    if (!TestMeleeReach_00535A90(position, victim, probe, attacker)) continue;

                    short heading = *(short*)((byte*)position + 0x16);

                    if ((flags & 0x10) != 0
                        && (uint)((*(short*)(victim + 0x32) - heading - *probe - 0x400) & 0xfff) <= 0x7ff)
                    {
                        Aud.PlayPositionalSound(8, 0x800, 0x1000, (int*)(victim + 0x1c));
                        continue;
                    }

                    if (ResolveMeleeHit_00539B80((uint*)attacker, (uint*)victim) != 0)
                    {
                        // Le recul part dans le cap de l'attaquant, seize fois le sinus.
                        *(int*)(victim + 0x50) += (int)Winsys3d.GetSinTableEntry(heading) * 0x10;
                        *(int*)(victim + 0x58) += Winsys3d.GetCosTableEntry(heading) * 0x10;
                        *(uint*)victim |= 0x40;

                        *Mem.Camera(0x005de0b8) = 1;
                        last = victim;
                    }

                    // Le clignotement de degat est pose meme quand la resolution n'a rien rendu :
                    // c'est dans le binaire, hors du `if`.
                    LegacyPlus.Coutures.CoupPorte(LegacyPlus.Coutures.Balayage.Melee, attacker, victim);   // Couture legacy ++ (LP-004)
                    victim[0x96] = 0x10;
                    victim[0x97] = 0;
                }
            }
        }

        return last;
    }


    /// <summary>
    /// Ghidra: FUN_00534cd0 @ 0x00534cd0 — <b>les volumes de declenchement</b>. Le nom
    /// « eclairage » que ce depot lui donnait d'apres son seul site d'appel etait faux : elle
    /// parcourt la liste de morceaux en attente (<c>DAT_005fa560</c>, la meme que
    /// <see cref="ResetPendingChunkRecords"/>) et teste l'entite contre une suite de <b>boites
    /// alignees sur les axes</b>, chacune tenant dans un enregistrement de 0x10 octets.
    ///
    /// <para>
    /// L'empaquetage des boites est irregulier et vaut d'etre ecrit une fois : le mot 1 porte
    /// <c>{centre X, centre Y}</c>, le mot 2 <c>{centre Z, demi-etendue X}</c> et le mot 3
    /// <c>{demi-etendue Y, demi-etendue Z}</c>. Le mot 0 porte l'etat sur son octet bas, le type
    /// et ses drapeaux sur le suivant, et l'index de script sur sa moitie haute.
    /// </para>
    /// <para>
    /// Cinq types se distinguent par les quatre bits bas du second octet : <b>1</b> compte les
    /// passages (l'etat gagne 8 a la sortie), <b>2</b> lance son script en sortant, <b>3</b> le
    /// lance a la premiere sortie seulement, <b>8</b> change la couleur de fond et pose le bit 3
    /// du monde, <b>9</b> fait naitre l'effet 0x59 quand les points de vie ou de magie ont bouge
    /// depuis la derniere image. Tout le reste lance son script en <b>entrant</b>.
    /// </para>
    /// <para>
    /// Le test <c>(*entite &amp; 0xfc0000) == 0x800000</c> revient six fois : c'est « est-ce un
    /// membre du groupe », et il conditionne la plupart des effets. Une entite quelconque traverse
    /// donc la plupart des volumes sans rien declencher.
    /// </para>
    /// <para>
    /// Le <c>uVar10 = (int)uVar2 &gt;&gt; 0xc &amp; 0xffff</c> que Ghidra place en fin de boucle exterieure
    /// est un <b>no-op</b> : la valeur n'est jamais lue autrement qu'a travers un <c>(short)</c>.
    /// Conserve implicitement, pas transcrit en tant que tel.
    /// </para>
    /// </summary>
    private static void TestTriggerVolumes_00534CD0(uint* entityPtr)
    {
        if ((Mem.s_WorldFlags_005de02c & 0x10) != 0) return;
        if (LegacyPlus.Coutures.VolumesTus()) return;   // Couture legacy ++ (LP-012)

        byte* entity = (byte*)entityPtr;
        byte* pending = Mem.Entity(0x005fa560);

        // Ghidra: `uVar5 = (ushort)DAT_005fa560` puis `if ((short)uVar5 != 0)`, ce qui se lit comme
        // un mot de seize bits et ne l'est pas. Le binaire fait `movzx ax,byte [5FA560h]` : le
        // compte tient sur UN OCTET, et l'octet suivant est deja la premiere entree de la liste.
        // Lire un `short` ici faisait entrer cette premiere entree dans le compte — un compte de 2
        // suivi d'une entree 5 donnait 0x0502 tours — et la boucle sortait de la liste pour
        // dereferencer des pointeurs quelconques. Plantage natif 0xC0000005 des qu'une zone
        // arrivait avec des morceaux en attente. Le bornage de la boucle relit le meme octet.
        int count = *pending;
        if (count == 0) return;

        int worldY = *(int*)(entity + 0x20);
        int worldX = *(int*)(entity + 0x1c);
        int worldZ = *(int*)(entity + 0x24);

        bool isPartyMember = (*entityPtr & 0xfc0000) == 0x800000;

        for (int slot = 1; slot <= count; slot++)
        {
            uint* record = *(uint**)(*(byte**)(s_Unknown_005e97cc + 0x88) + pending[slot] * 4);
            if (record == null) return;

            // L'original n'a aucune borne : il fait confiance a la sentinelle 0xffffffff. Un
            // enregistrement atteint par un mauvais indice n'en a pas, et la marche part alors
            // dans de la memoire quelconque — c'est-a-dire une violation d'acces native, sans
            // aucune indication de sa cause. La borne ci-dessous ne change rien a un parcours
            // normal (les listes de boites font quelques dizaines d'entrees) et transforme ce
            // plantage muet en un message qui nomme les valeurs. Meme raison d'etre que la borne
            // de balayage de RunTextEngine et que StagingBank.
            int walked = 0;
            const int WalkLimit = 0x400;

            for (uint header = *record; header != 0xffffffff; record += 4, header = *record)
            {
                if (++walked > WalkLimit)
                {
                    throw new InvalidOperationException(
                        $"FUN_00534cd0 : plus de 0x{WalkLimit:X} boites parcourues sans sentinelle " +
                        $"0xffffffff dans le morceau {pending[slot]} (creneau {slot} sur {count}). " +
                        "La liste de boites atteinte n'en est pas une : c'est l'indice de morceau " +
                        "ou la liste des morceaux en attente de 0x005fa560 qui est faux, pas la " +
                        "marche. Relever les octets de 0x005fa560 au moment du plantage.");
                }

                if (isPartyMember) *(byte*)record = (byte)(*(byte*)record + 1);

                byte kind = (byte)(header >> 8);
                bool inside = false;

                int centre = *(short*)((byte*)record + 4);
                int delta = Math.Abs((short)(worldX >> 0xc) - centre);

                if (delta < *(short*)((byte*)record + 0x0a))
                {
                    centre = *(short*)((byte*)record + 6);
                    delta = Math.Abs((short)(worldY >> 0xc) - centre);

                    if (delta < *(short*)((byte*)record + 0x0c))
                    {
                        centre = *(short*)((byte*)record + 8);
                        delta = Math.Abs((short)(worldZ >> 0xc) - centre);
                        inside = delta < *(short*)((byte*)record + 0x0e);
                    }
                }

                if (inside)
                {
                    byte type = (byte)(kind & 0xf);

                    if ((header & 0xf00) != 0
                        && (type != 1 || (header & 0x80) != 0 || !isPartyMember))
                    {
                        if (type == 3 && isPartyMember) *(byte*)record = (byte)(*(byte*)record + 8);

                        if (type == 8)
                        {
                            *(byte*)Mem.Camera(0x005de044) = (byte)(header >> 0x10);
                            Mem.s_WorldFlags_005de02c |= 8;
                            Mem.s_BackColourR_005e93a0 = 0xff;
                            Mem.s_BackColourG_005e93a2 = 0xff;
                            Mem.s_BackColourB_005e93a4 = 0xff;
                        }

                        if (kind == 9 && isPartyMember && (*entityPtr & 0x40000000) == 0)
                        {
                            byte* block = GetMainMemPtr() + 0xf0000 + (entity[8] * 0x2000);
                            if (*(short*)(block + 0x14c) != *(short*)(block + 0x148)
                                || *(short*)(block + 0x14e) != *(short*)(block + 0x14a))
                            {
                                *(short*)(block + 0x14c) = *(short*)(block + 0x148);
                                *(short*)(block + 0x14e) = *(short*)(block + 0x14a);
                                SpawnEffectEntity(entity, 0x59, 0, -1);
                            }
                        }

                        continue;
                    }

                    RunEntityScript(s_Unknown_005e97cc, entity, header >> 0x10);
                    *(byte*)record = (byte)(*(byte*)record | 0x80);
                }
                else
                {
                    byte type = (byte)(kind & 0xf);

                    if (type == 2 || (type == 3 && (header & 0x80) == 0 && isPartyMember))
                    {
                        RunEntityScript(s_Unknown_005e97cc, entity, header >> 0x10);
                        *(byte*)record = (byte)(*(byte*)record | 0x80);
                    }
                    else if (type == 1 && isPartyMember)
                    {
                        *(byte*)record = (byte)(*(byte*)record + 8);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00562cf0</c> — les <b>gabarits de portee au corps a corps</b>, un par posture,
    /// <b>0xc octets (six shorts) par entree</b>, releves dans le binaire
    /// (<c>at Legacy/Binaries/Blaze.exe 0x00562cf0 384</c>).
    ///
    /// <para>
    /// <b>La table compte dix-huit entrees</b>, pas seize ni vingt : le dword de 0x00562dc8 est
    /// <c>0x02030100</c>, une suite d'octets croissants qui n'a plus la forme des six shorts. La
    /// borne se lit donc dans la donnee, pas dans les sites d'appel. Quatre entrees (4, 7, 12, 15)
    /// sont entierement nulles — les postures qui ne frappent pas, et ce sont exactement celles que le
    /// site d’appel ecarte par <c>(posture &amp; 7) != 4 &amp;&amp; != 7</c>.
    /// </para>
    /// <para>
    /// L'indice est <b>l'octet de posture entier</b>, pas ses trois bits bas ; seule l'entree 6 de
    /// la posture 0 est detournee vers <c>(posture &gt;&gt; 3) + 0x10</c>, ce qui donne exactement les
    /// deux dernieres entrees. Un indice hors des dix-huit lit, dans l'original, la structure
    /// suivante — la lacune le dit plutot que de rendre des octets inventes.
    /// </para>
    /// </summary>
    private static readonly short[] s_MeleeProbes_00562cf0 =
    [
        0x0000, 0x0028, 0x003c, 0x0096, unchecked((short)0xffa0), 0x006e,   //  0
        0x0000, 0x001e, 0x001e, 0x0096, 0x0100, 0x005a,                     //  1
        0x0000, 0x0000, 0x0046, 0x0096, unchecked((short)0xff40), 0x0064,   //  2
        0x0000, 0x0028, 0x003c, 0x0096, unchecked((short)0xff80), 0x005a,   //  3
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     //  4
        0x0000, 0x0032, 0x0032, 0x0096, 0x0000, 0x0064,                     //  5
        0x0000, 0x0032, 0x0050, 0x0096, unchecked((short)0xff40), 0x0050,   //  6
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     //  7
        0x0000, 0x001e, 0x0028, 0x0096, 0x0000, 0x0064,                     //  8
        0x0000, 0x001e, 0x0028, 0x0096, 0x0100, 0x006e,                     //  9
        0x0000, 0x0000, 0x0046, 0x0096, unchecked((short)0xff40), 0x005a,   // 10
        0x0000, 0x001e, 0x0046, 0x0096, unchecked((short)0xff00), 0x005a,   // 11
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     // 12
        0x0000, 0x0032, 0x0032, 0x0096, 0x0000, 0x0064,                     // 13
        0x0000, 0x0028, 0x0050, 0x0096, unchecked((short)0xff00), 0x005a,   // 14
        0x0000, 0x0000, 0x0000, 0x0000, 0x0000, 0x0000,                     // 15
        0x0000, 0x0032, 0x0078, 0x0064, unchecked((short)0xfe80), 0x0082,   // 16
        0x0000, 0x0032, 0x006e, 0x0064, unchecked((short)0xfe80), 0x006e,   // 17
    ];

    private static short* s_MeleeProbeStorage;

    /// <summary>L'entree de <see cref="s_MeleeProbes_00562cf0"/> pour une posture, en memoire
    /// native puisque l'appele la recoit par pointeur.</summary>
    private static short* MeleeProbeForPosture(uint row)
    {
        if (row >= 18)
        {
            throw new NotImplementedException(
                $"Gabarit de portee d’indice {row}, hors des dix-huit entrees " +
                "relevees a 0x00562cf0. Dans le binaire cette lecture tombe dans la " +
                "structure suivante (0x00562dc8) ; relever ce qu'elle porte avant d'aller plus " +
                $"loin : `OpenForesia.Tools -- at Legacy/Binaries/Blaze.exe 0x{0x00562cf0 + row * 0xc:x8} 12`.");
        }

        if (s_MeleeProbeStorage == null)
        {
            s_MeleeProbeStorage = (short*)NativeMemory.Alloc((nuint)s_MeleeProbes_00562cf0.Length, sizeof(short));
            for (int i = 0; i < s_MeleeProbes_00562cf0.Length; i++)
            {
                s_MeleeProbeStorage[i] = s_MeleeProbes_00562cf0[i];
            }
        }

        return s_MeleeProbeStorage + row * 6;
    }
}
