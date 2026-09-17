// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DU LABYRINTHE DES MORTS — zone 5, table 0x0056adc8.
//
// Ouvert le 2026-09-17 sur le patron de `OldTower.cs`, `Mine.cs` et `FireMountain.cs`, avec le lot
// des sept plus simples. Le labyrinthe installe TROIS blocs dans cette table, un par groupe de
// sous-zones :
//
//   0x00557aa8  sous-zones 0, 1, 2, 3
//   0x00558128  sous-zones 5, 6, 7, 8
//   0x005587a8  sous-zones 4, 9
//
// Aucun des sept corps de ce lot ne consulte le groupe : ils sont tous du cote decor, et il n y a
// donc pas encore de `LabyrinthMecanismes.cs` — voir le test d appartenance d ARCHITECTURE.md.
//
// Le son d une zone se lit par `ZoneSound` (`Mine.cs`), la banque 0 de l etat de partie par
// `Mem.s_StateBank0_005e96e0` et la sous-zone courante par `s_Unknown_005de52c` : meme classe
// partielle. Le tri de la zone par complexite est dans `Documentation/Etat/Zone5ParComplexite.md`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // CRENEAU 55 DU BLOC 0x00558128 — LE DECOR QU'ON POSE ET QU'ON OUBLIE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x004610e0</c> — creneau <b>55</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>six instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_004610E0.asm</c>. C'est le plus court gestionnaire
    /// d'entite de la zone.
    ///
    /// <para>
    /// <b>Il ne fait rien d'autre que se poser.</b> A chaque image il reecrit son echelle
    /// horizontale a <c>0x10cc</c> — un peu plus de 1,0 en virgule fixe 1.12 — et rallume le bit
    /// <c>0x4000</c> de son mot d'etat. Pas d'etat interne, pas de compteur, pas de son : le
    /// binaire n'a meme pas de branchement.
    /// </para>
    /// <para>
    /// L'ordre du binaire lit le mot d'etat <i>avant</i> d'ecrire <c>+0x28</c> et le range apres ;
    /// les deux champs ne se recouvrent pas, l'ordre est donc sans effet.
    /// </para>
    /// </summary>
    private static void EntityHandler_004610E0(byte* e)
    {
        *(short*)(e + 0x28) = 0x10cc;
        *(uint*)e |= 0x4000;
    }

    // =====================================================================
    // CRENEAU 53 DU BLOC 0x00557AA8 — CE QUI N'APPARAIT QUE SI LE DRAPEAU 236 EST POSE
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00460240</c> — creneau <b>53</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>dix instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460240.asm</c>.
    ///
    /// <para>
    /// <b>Un decor dont l'affichage tient a un drapeau de script.</b> Le bit <c>0x4000</c> est
    /// pose sans condition a chaque image ; le bit <c>0x2000</c> ne l'est que si le <b>drapeau
    /// 236</b> — bit 4 de l'octet <c>0x005e96fd</c>, la banque 0 de l'etat de partie — est pose.
    /// L'entite existe donc toujours, mais elle ne devient visible qu'une fois le drapeau leve par
    /// un script de la zone.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460240(byte* e)
    {
        *(uint*)e |= 0x4000;

        // Ghidra: `mov dl,[5e96fdh] ; test dl,10h` — l'octet 0x1d de la banque 0.
        if ((Mem.s_StateBank0_005e96e0[0x1d] & 0x10) != 0) *(uint*)e |= 0x2000;
    }

    // =====================================================================
    // CRENEAU 36 DU BLOC 0x00557AA8 — CE QUE LA SOUS-ZONE 1 CACHE AU DEPART
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00460a90</c> — creneau <b>36</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>treize instructions</b>,
    /// dump conserve dans <c>Legacy/Raw/Blaze/FUN_00460A90.asm</c>.
    ///
    /// <para>
    /// <b>Une porte a deux etages, et le premier depend de la carte.</b> Dans la <b>sous-zone 1</b>
    /// et seulement la, l'entite reste muette tant que le <b>drapeau 234</b> (bit 2 de l'octet
    /// <c>0x005e96fd</c>) n'est pas pose. Partout ailleurs le gestionnaire passe directement au
    /// second etage.
    /// </para>
    /// <para>
    /// <b>Le second etage lit le mot d'etat de l'entite.</b> Le bit <c>0x2000</c> est pose si le
    /// bit <c>0x40000000</c> est <i>absent</i>, ou — quand il est present — si le bit <c>0x20</c>
    /// l'est aussi. Le binaire ecrit cette disjonction en deux tests convergents
    /// (<c>je LAB_00460ab3</c> depuis le premier, chute depuis le second).
    /// </para>
    /// </summary>
    private static void EntityHandler_00460A90(byte* e)
    {
        // Ghidra: `cmp byte [5de52ch],1` — la sous-zone courante.
        if (s_Unknown_005de52c == 1 && (Mem.s_StateBank0_005e96e0[0x1d] & 4) == 0) return;

        uint flags = *(uint*)e;

        if ((flags & 0x40000000) == 0 || (flags & 0x20) != 0) *(uint*)e = flags | 0x2000;
    }

    // =====================================================================
    // CRENEAU 54 DU BLOC 0x00557AA8 — LE BRUIT DE FOND, TOUTES LES VINGT ET UNE IMAGES
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_004603c0 @ 0x004603c0 — creneau <b>54</b> du bloc <c>0x00557aa8</c>
    /// (sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 25 instructions.
    ///
    /// <para>
    /// <b>Une source sonore immobile.</b> Elle joue le son <c>0x17</c> de la sous-zone —
    /// <see cref="ZoneSound"/> — a hauteur <c>0x700</c> et gain <c>0x800</c> sur les deux voies,
    /// puis se tait vingt images. Le compteur vit dans <c>+0x98</c> : il repart de zero des qu'il
    /// depasse <c>0x14</c>, ce qui donne <b>une periode de vingt et une images</b> et non de vingt.
    /// </para>
    /// <para>
    /// <b>Le premier son part a la naissance</b>, pas a la fin de la premiere periode : l'etat 0
    /// remet <c>+0x98</c> a zero, et le test du son suit immediatement. C'est l'inverse de
    /// <c>TickRepeatingDistanceSound</c> de la mine, qui attend sa periode.
    /// </para>
    /// <para>
    /// <b>Le son n'est pas attenue par la distance</b> : il passe par
    /// <c>Aud.PlaySoundEffect</c> et non par <see cref="TryPlaySoundNearEntity"/>. La position de
    /// l'entite n'est jamais lue.
    /// </para>
    /// </summary>
    private static void EntityHandler_004603C0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x98) = 0;
        }

        // Ghidra: FUN_0053cea0(**(int **)(DAT_005e97cc + 0xa8) + 0x17, 0x700, 0x800, 0x800)
        if (*(short*)(e + 0x98) == 0) Aud.PlaySoundEffect(ZoneSound(0x17), 0x700, 0x800, 0x800);

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) > 0x14) *(short*)(e + 0x98) = 0;
    }

    // =====================================================================
    // CRENEAU 21 DU BLOC 0x00557AA8 — CE QUI FLOTTE TANT QUE LE DRAPEAU 250 TIENT
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00460d10</c> — creneau <b>21</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>25 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460D10.asm</c>.
    ///
    /// <para>
    /// <b>Sa hauteur a deux regimes, et c'est un drapeau de script qui tranche.</b> Tant que le
    /// <b>drapeau 250</b> — bit 2 de l'octet <c>0x005e96ff</c>, banque 0 — est pose, <c>+0x20</c>
    /// suit le sinus de la phase <c>+0x12</c> : <c>(sinus - 0xa000) * 7</c>, soit une oscillation
    /// entre <c>-0x4d000</c> et <c>-0x3f000</c>. Drapeau retire, la hauteur est <b>figee</b> a
    /// <c>-0xaa000</c>, bien plus bas — et la phase cesse d'avancer.
    /// </para>
    /// <para>
    /// <b>La phase avance de <c>0x50</c> par image</b> et retombe de <c>0x1000</c> des qu'elle
    /// atteint le tour complet : <b>82 images</b> par cycle, moins d'une seconde et demie. Le sinus
    /// est lu <i>avant</i> l'avance — le binaire empile l'argument puis fait l'addition.
    /// </para>
    /// <para>
    /// La multiplication par sept est ecrite <c>x*8 - x</c> par le compilateur
    /// (<c>shl ecx,3 ; sub ecx,eax</c>), et la comparaison de fin de tour est <b>signee</b>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460D10(byte* e)
    {
        // Ghidra: `mov al,[5e96ffh] ; test al,4` — l'octet 0x1f de la banque 0.
        if ((Mem.s_StateBank0_005e96e0[0x1f] & 4) == 0)
        {
            *(int*)(e + 0x20) = unchecked((int)0xfff56000);
            return;
        }

        int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: call 00545a1c
        *(short*)(e + 0x12) += 0x50;

        *(int*)(e + 0x20) = (wave - 0xa000) * 7;

        if (*(short*)(e + 0x12) >= 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    // =====================================================================
    // CRENEAU 8 DU BLOC 0x00558128 — LE BALANCEMENT QUI COMMENCE OU IL VEUT
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00461be0 @ 0x00461be0 — creneau <b>8</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 33 instructions.
    ///
    /// <para>
    /// <b>Il oscille verticalement autour de la hauteur ou le placement l'a pose.</b> A la
    /// naissance il retient cette hauteur en <b>cases entieres</b> dans <c>+0x9a</c>
    /// (<c>+0x20 &gt;&gt; 12</c>) et tire une <b>phase de depart au hasard</b> dans
    /// <c>[0, 4000[</c> — <c>rand() % 4000</c>, reste <b>signe</b>. Deux exemplaires poses sur la
    /// meme carte ne battent donc pas ensemble.
    /// </para>
    /// <para>
    /// <b>Amplitude et vitesse viennent du placement</b>, pas du code : <c>+0x14</c> multiplie le
    /// sinus, <c>+0x16</c> est le pas de phase par image. La hauteur vaut
    /// <c>sinus * amplitude + case * 0x1000</c>, et la phase retombe de <c>0x1000</c> des qu'elle
    /// <b>depasse</b> le tour complet — strictement, ce qui laisse passer la valeur <c>0x1000</c>
    /// elle-meme une image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461BE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4000);          // Ghidra: FUN_00547140
            *(short*)(e + 0x9a) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: Ordinal_451

            *(int*)(e + 0x20) = wave * *(short*)(e + 0x14) + *(short*)(e + 0x9a) * 0x1000;

            *(short*)(e + 0x12) += *(short*)(e + 0x16);
            if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
        }
    }

    // =====================================================================
    // CRENEAU 10 DU BLOC 0x00558128 — CE QUI TOMBE DIX IMAGES ET S'ETEINT
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00461e00 @ 0x00461e00 — creneau <b>10</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 35 instructions.
    ///
    /// <para>
    /// <b>Une chute de dix images, sonnee au depart.</b> La premiere image allume le bit
    /// <c>0x2000</c>, arme la vitesse a <c>10</c> dans <c>+0x12</c> et joue le son <c>0x20</c> de
    /// la sous-zone par <see cref="TryPlaySoundNearEntity"/> — donc <b>muet au-dela de 2000 unites
    /// de la camera</b>. Les images suivantes accelerent de <c>0x14</c> et ajoutent la vitesse a
    /// <c>+0x34</c>.
    /// </para>
    /// <para>
    /// <b>La dixieme image l'eteint</b> : le compteur d'etat <c>+0x10</c> avance a chaque tour, et
    /// des qu'il depasse 9 le mot d'etat prend <c>0x40002000</c> d'un coup. Le bit
    /// <c>0x40000000</c> est aussi la garde d'entree du corps : une fois pose, le gestionnaire ne
    /// fait plus que remettre la vitesse a zero.
    /// </para>
    /// <para>
    /// <b>La vitesse est remise a zero avant tout</b>, y compris apres l'extinction — c'est le
    /// premier appel du corps, hors de la garde.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461E00(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);          // Ghidra: FUN_00460fc0

        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 10;
            *(uint*)e |= 0x2000;

            // Ghidra: FUN_00468080(param_1, **(int **)(DAT_005e97cc + 0xa8) + 0x20, 0x800, 0x1000, 0, 0)
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x800, 0x1000, 0, 0);
        }
        else
        {
            *(short*)(e + 0x12) += 0x14;
            *(short*)(e + 0x34) += *(short*)(e + 0x12);
        }

        (*(short*)(e + 0x10))++;
        if (*(short*)(e + 0x10) > 9) *(uint*)e |= 0x40002000;
    }

    // =====================================================================
    // LOT 3 (2026-09-17) — LE DECOR QUI CHANGE DE COULEUR
    // =====================================================================

    /// <summary>
    /// Le corps partage de <see cref="EntityHandler_00462860"/> (sous-zones 5 a 8) et de
    /// <see cref="EntityHandler_00463640"/> (sous-zones 4 et 9). Absent de <c>Blaze.c</c> ;
    /// 91 instructions, et le desassemblage des deux est <b>identique</b> une fois la relocalisation
    /// retiree : pas meme une planche pour les distinguer.
    ///
    /// <para>
    /// <b>Un decor qui respire en couleur.</b> Sa phase <c>+0x12</c> part d'un des quinze
    /// seiziemes de tour tires au hasard (<c>rand() % 15</c>, decale de 8 bits) et avance de
    /// <c>0x100</c> par image — un battement en seize images.
    /// </para>
    /// <para>
    /// <b>Les trois echelles suivent le sinus</b>, un seizieme autour de <c>0x1000</c>, et les trois
    /// canaux de teinte aussi — mais chacun avec <b>son propre grain de bruit</b> : le rouge part de
    /// <c>0x76</c> plus un tirage sur <c>0x28</c>, le vert et le bleu de <c>0x80</c> moins un tirage
    /// sur <c>0x1e</c>, et les trois retranchent <c>sinus / 128</c>. La teinte n'est donc jamais deux
    /// fois la meme, meme a phase egale.
    /// </para>
    /// <para>
    /// <b>L'arithmetique est celle d'un octet</b>, avec ses debordements : le binaire fait
    /// <c>sub dl,bl</c> et <c>add dl,76h</c> sur des registres de huit bits, et les trois canaux sont
    /// ensuite assembles en un seul dword ecrit d'un coup dans <c>+0x38</c> — l'octet
    /// <c>+0x3b</c> est donc remis a zero au passage.
    /// </para>
    /// <para>
    /// Le binaire appelle la table de sinus <b>six fois</b> avec la meme phase ; c'est une lecture
    /// pure, sans effet sur le tirage, et elle est faite une fois ici par valeur distincte.
    /// </para>
    /// </summary>
    private static void TickColourBreathingDecor(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);          // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)((CrtRand() % 0x0f) << 8);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) += 0x100;

        int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: call 00545a1c
        byte damp = (byte)(wave >> 7);                                    // Ghidra: `sar ebx,7`

        byte red = (byte)((byte)(CrtRand() % 0x28) - damp + 0x76);
        byte green = (byte)((byte)(0x80 - (byte)(CrtRand() % 0x1e)) - damp);
        byte blue = (byte)((byte)(0x80 - (byte)(CrtRand() % 0x1e)) - damp);

        // Ghidra: `mov dh,al ; mov dl,bl ; shl edx,8 ; or edx,eax ; mov [esi+38h],edx` — les trois
        // canaux sont ranges en un seul dword, ce qui remet +0x3b a zero.
        *(uint*)(e + 0x38) = ((uint)blue << 16) | ((uint)green << 8) | red;

        short scale = (short)((wave >> 4) + 0x1000);
        *(short*)(e + 0x28) = scale;
        *(short*)(e + 0x2a) = scale;
        *(short*)(e + 0x2c) = scale;

        if (*(short*)(e + 0x12) >= 0x1000) *(short*)(e + 0x12) -= 0x1000;
    }

    /// <summary>Ghidra: <c>0x00462860</c> — le decor qui respire en couleur, sous-zones 5 a 8,
    /// creneau <b>30</b> du bloc <c>0x00558128</c>.</summary>
    private static void EntityHandler_00462860(byte* e) => TickColourBreathingDecor(e);

    /// <summary>Ghidra: <c>0x00463640</c> — le meme, sous-zones 4 et 9. Le compilateur a emis deux
    /// fois le meme corps ; le desassemblage STRICT est identique.</summary>
    private static void EntityHandler_00463640(byte* e) => TickColourBreathingDecor(e);
}
