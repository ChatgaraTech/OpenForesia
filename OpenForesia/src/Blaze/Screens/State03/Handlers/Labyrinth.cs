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
// Aucun corps de ce fichier ne consulte le groupe : ils sont tous du cote decor. Ce qui prend le
// groupe ou la partie en charge vit dans `LabyrinthMecanismes.cs`, ouvert au lot 4 du 2026-09-18 —
// voir le test d appartenance d ARCHITECTURE.md.
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
    /// creneau <b>47</b> du bloc <c>0x00558128</c>. (Le lot 3 avait ecrit « 30 » : c'est faux, le
    /// creneau 30 de ce bloc est <see cref="EntityHandler_00461980"/>. Releve a nouveau dans le
    /// binaire le 2026-09-18, et les deux corps sont bien au <b>meme</b> creneau de leurs blocs
    /// respectifs, ce qui confirme qu'ils sont le meme sous-type.)</summary>
    private static void EntityHandler_00462860(byte* e) => TickColourBreathingDecor(e);

    /// <summary>Ghidra: <c>0x00463640</c> — le meme, sous-zones 4 et 9, creneau <b>47</b> du bloc
    /// <c>0x005587a8</c>. Le compilateur a emis deux fois le meme corps ; le desassemblage STRICT
    /// est identique.</summary>
    private static void EntityHandler_00463640(byte* e) => TickColourBreathingDecor(e);

    // =====================================================================
    // LOT 4 (2026-09-18) — LE DECOR DES DIX PLUS SIMPLES QUI RESTAIENT
    //
    // Sept corps de decor, un par creneau, repartis sur les trois blocs. Trois autres du meme lot
    // consultent le GROUPE ou la table des emplacements et ne sont donc pas ici — voir le test
    // d'appartenance d'ARCHITECTURE.md.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00460260 @ 0x00460260 — creneau <b>46</b> du bloc <c>0x00557aa8</c>
    /// (sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 51 instructions.
    ///
    /// <para>
    /// <b>Une lueur rouge qui s'eteint quand son drapeau se leve.</b> A la naissance la teinte part
    /// de <c>(200, 20, 20)</c> — un rouge franc. L'etat 1 fait battre le seul canal rouge sur le
    /// sinus de la phase <c>+0x12</c> : <c>(sinus * 20 &gt;&gt; 12) - 26</c>, ecrit sur <b>un
    /// octet</b>, donc avec ses debordements. La phase avance de 100 par image et retombe de
    /// <c>0x1000</c> au tour complet : <b>41 images</b> par battement.
    /// </para>
    /// <para>
    /// <b>Le drapeau la fait basculer.</b> <see cref="ScriptFlagSet"/> lit le numero range en
    /// <c>+0x14</c> par le placement ; des qu'il est pose, l'etat passe a 2 — et l'etat 2
    /// <b>eclaircit de -20 par image sur les trois canaux</b>, ce qui est un assombrissement.
    /// C'est le seul site connu ou <see cref="BrightenEntityTint_00404DE0"/> recoit des arguments
    /// negatifs, et donc le seul ou sa <b>borne basse</b> sert : la teinte se pose sur
    /// <c>(0, 0, 0)</c> en une dizaine d'images et y reste.
    /// </para>
    /// <para>
    /// L'etat 2 ne revient jamais a 1 : le battement du rouge est perdu pour de bon.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460260(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            e[0x38] = 0xc8;
            e[0x39] = 0x14;
            e[0x3a] = 0x14;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 100;
            if (*(short*)(e + 0x12) >= 0x1000) *(short*)(e + 0x12) -= 0x1000;

            int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: call 00545a1c

            // Ghidra: `lea ecx,[eax+eax*4] ; shl ecx,2 ; sar ecx,0Ch ; sub cl,1Ah` — la
            // soustraction se fait sur CL, donc sur huit bits.
            e[0x38] = (byte)((wave * 0x14 >> 0xc) - 0x1a);

            if (ScriptFlagSet(*(short*)(e + 0x14))) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2) BrightenEntityTint_00404DE0(e, -20, -20, -20);
    }

    /// <summary>
    /// Ghidra: FUN_00463300 @ 0x00463300 — creneau <b>34</b> du bloc <c>0x005587a8</c>
    /// (sous-zones 4 et 9). Decompilee dans <c>Blaze.c</c>, 55 instructions.
    ///
    /// <para>
    /// <b>Une porte a un battant, ouverte et refermee par ailleurs.</b> L'etat 1 fait tourner
    /// l'angle <c>+0x34</c> de <b>+100 par image pendant dix images</b> — <c>0x3e8</c> en tout —
    /// puis passe au repos (<c>0x63</c>) ; l'etat 2 fait le retour exact, <b>-100 dix fois</b>, et
    /// revient a l'etat <b>0</b>, donc a la naissance. Le son <c>0x10</c> de la sous-zone part sur
    /// la premiere image de chacun des deux mouvements, a hauteur <c>0x800</c> et gain plein.
    /// </para>
    /// <para>
    /// <b>Elle ne se declenche pas elle-meme</b> : l'etat 0 ne fait que remettre <c>+0x12</c> a
    /// zero, et rien dans ce corps ne pose l'etat 1 ou 2. C'est un script de zone ou une entite
    /// voisine qui les pose — la porte n'est que le battant.
    /// </para>
    /// <para>
    /// Les deux branches ne sont pas symetriques sur la sortie : l'ouverture finit au <b>repos</b>,
    /// la fermeture a l'etat <b>0</b>. Une porte refermee est donc reouvrable dans les memes
    /// termes, une porte ouverte ne se rouvre pas.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463300(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x12) = 0;

        if (*(short*)(e + 0x10) == 1)
        {
            // Ghidra: FUN_00468080(param_1, **(int **)(DAT_005e97cc + 0xa8) + 0x10, 0x800, 0x1000, 0, 0)
            if (*(short*)(e + 0x12) == 0)
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x10), 0x800, 0x1000, 0, 0);

            *(short*)(e + 0x34) += 100;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 10)
            {
                *(short*)(e + 0x10) = 99;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            if (*(short*)(e + 0x12) == 0)
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x10), 0x800, 0x1000, 0, 0);

            *(short*)(e + 0x34) -= 100;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 10)
            {
                *(short*)(e + 0x10) = 0;
                *(short*)(e + 0x12) = 0;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00461b00 @ 0x00461b00 — creneau <b>20</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 57 instructions.
    ///
    /// <para>
    /// <b>Ce qui descend une fois et se tait.</b> A la naissance elle retient sa hauteur de depart
    /// en <b>cases entieres</b> dans <c>+0x12</c> (<c>+0x20 &gt;&gt; 12</c>) et arme le compteur
    /// <c>+0x98</c> a 7. L'etat 1 la fait descendre de <c>+0x14</c> cases par image — la vitesse
    /// vient du <b>placement</b>, pas du code — jusqu'a <c>depart - +0x16</c> cases, ou elle se
    /// cale exactement et passe a l'etat 2. <b>Elle n'en repart jamais.</b>
    /// </para>
    /// <para>
    /// <b>Le grondement bat toutes les sept images</b> : le son <c>0x1f</c> de la sous-zone a
    /// hauteur <c>0x300</c> part quand <c>+0x98</c> vaut 7, et le compteur descend puis se recharge
    /// a 7 des qu'il touche zero. La butee joue le meme son a hauteur <c>0x200</c>, plus grave :
    /// c'est le choc final.
    /// </para>
    /// <para>
    /// <b>La comparaison de butee est faite en cases, pas en unites</b> : <c>hauteur &gt;&gt; 12</c>
    /// contre la case visee. Un pas de <c>+0x14</c> qui ne divise pas la distance depasse donc la
    /// butee d'une fraction de case, et c'est l'ecriture <c>case * 0x1000</c> qui rattrape.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461B00(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x98) = 7;
            *(short*)(e + 0x12) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        if (*(short*)(e + 0x10) != 1) return;

        if (*(short*)(e + 0x98) == 7)
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x300, 0x1000, 0, 0);

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) == 0) *(short*)(e + 0x98) = 7;

        int height = *(int*)(e + 0x20) + *(short*)(e + 0x14) * -0x1000;
        *(int*)(e + 0x20) = height;

        int floor = *(short*)(e + 0x12) - *(short*)(e + 0x16);
        if (height >> 0xc < floor)
        {
            *(int*)(e + 0x20) = floor * 0x1000;
            *(short*)(e + 0x10) = 2;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x200, 0x1000, 0, 0);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00461e80 @ 0x00461e80 — creneau <b>54</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 57 instructions.
    ///
    /// <para>
    /// <b>La chute de dix images de <see cref="EntityHandler_00461E00"/>, mais declenchee au
    /// contact et a deux voisines.</b> Le depart demande le bit <c>0x10</c> ou <c>0x40</c> du mot
    /// d'etat — attache ou touche — et l'etat au repos. Elle <b>marque alors deux entites
    /// voisines</b> du bit <c>0x40</c> : celle qui la suit de <c>+0x14</c> rangs et celle qui la
    /// suit de <c>+0x16</c> rangs dans sa propre table, au pas de <c>0x9c</c>. C'est le meme
    /// adressage relatif que les portes a deux battants de la mine, ici a <b>trois</b> battants.
    /// </para>
    /// <para>
    /// <b>L'etat 1 ne dure qu'une image</b> : il arme la vitesse a <c>10</c>, allume le bit
    /// <c>0x2000</c>, joue le son <c>0x20</c> de la sous-zone par
    /// <see cref="TryPlaySoundNearEntity"/> — donc muet au-dela de 2000 unites — et passe a l'etat
    /// 2 dans le meme pas. L'etat 2 accelere de <c>0x14</c> par image et pousse l'angle
    /// <c>+0x34</c>, dix fois, puis prend <c>0x40002000</c> d'un coup.
    /// </para>
    /// <para>
    /// <b>Le compteur n'est pas <c>+0x10</c> mais <c>+0x9a</c></b>, parce que <c>+0x10</c> porte
    /// l'etat : c'est la seule difference d'architecture avec sa sœur <c>0x00461e00</c>, qui n'a
    /// pas d'etat a garder et compte donc sur <c>+0x10</c>.
    /// </para>
    /// <para>
    /// Le binaire lit le mot d'etat <i>avant</i> d'ecrire <c>+0x12</c> et le range apres ; les deux
    /// champs ne se recouvrent pas, l'ordre est donc sans effet.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461E80(byte* e)
    {
        if ((*(uint*)e & 0x50) != 0 && *(short*)(e + 0x10) == 0)
        {
            // Ghidra: param_1[(short)param_1[5] * 0x27] |= 0x40 — 0x27 dwords, soit 0x9c octets.
            *(uint*)(e + *(short*)(e + 0x14) * 0x9c) |= 0x40;
            *(uint*)(e + *(short*)(e + 0x16) * 0x9c) |= 0x40;

            *(short*)(e + 0x10) = 1;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) = 10;
            *(uint*)e |= 0x2000;

            // Ghidra: FUN_00468080(param_1, **(int **)(DAT_005e97cc + 0xa8) + 0x20, 0x800, 0x1000, 0, 0)
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x800, 0x1000, 0, 0);

            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x9a) = 1;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 0x14;
            *(short*)(e + 0x34) += *(short*)(e + 0x12);

            (*(short*)(e + 0x9a))++;
            if (*(short*)(e + 0x9a) > 9)
            {
                *(short*)(e + 0x10) = 99;
                *(uint*)e |= 0x40002000;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00460fd0 @ 0x00460fd0 — creneau <b>31</b> du bloc <c>0x00557aa8</c>
    /// (sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 62 instructions.
    ///
    /// <para>
    /// <b>Ce qui respire en volume, puis clignote quarante images et s'eteint.</b> L'etat 1 fait
    /// battre les <b>trois</b> echelles ensemble autour de <c>0xc00</c> — trois quarts de taille —
    /// d'un huitieme du sinus, la phase avancant de <c>0x28</c> par image : <b>164 images</b> par
    /// cycle, presque trois secondes.
    /// </para>
    /// <para>
    /// <b>La hauteur, elle, ne bat pas : elle DERIVE.</b> Le binaire fait <c>+0x20 += sinus</c> et
    /// non <c>+0x20 = base + sinus</c> — le sinus est <i>accumule</i>. Sur un tour complet la somme
    /// des valeurs echantillonnees au pas de <c>0x28</c> n'est pas nulle, si bien que l'entite
    /// monte ou descend lentement sans jamais revenir. C'est ce que le binaire fait ; transcrit tel
    /// quel.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est l'extinction</b> : le compteur <c>+0x5c</c> — un mot de 32 bits, pas un
    /// demi-mot — avance d'un par image et le bit <c>0x4000</c> suit sa <b>parite</b>, donc
    /// l'entite clignote une image sur deux. Passe <c>0x28</c>, l'etat va au repos et le bit reste
    /// <b>pose</b> : elle finit visible, pas invisible.
    /// </para>
    /// <para>
    /// Le binaire appelle la table de sinus <b>quatre fois</b> avec la meme phase, et avance la
    /// phase entre le quatrieme appel et l'usage de son resultat ; c'est une lecture pure, faite
    /// une fois ici.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460FD0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(uint*)(e + 0x5c) = 0;
            *(short*)(e + 0x28) = 0xc00;
            *(short*)(e + 0x2a) = 0xc00;
            *(short*)(e + 0x2c) = 0xc00;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: Ordinal_451

            *(int*)(e + 0x20) += wave;

            short scale = (short)((wave >> 3) + 0xc00);
            *(short*)(e + 0x28) = scale;
            *(short*)(e + 0x2a) = scale;
            *(short*)(e + 0x2c) = scale;

            *(short*)(e + 0x12) += 0x28;
            if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            uint ticks = *(uint*)(e + 0x5c) + 1;
            *(uint*)(e + 0x5c) = ticks;

            uint word = (ticks & 1) == 0 ? *(uint*)e & 0xffffbfff : *(uint*)e | 0x4000;
            *(uint*)e = word;

            if ((int)ticks > 0x28)
            {
                *(short*)(e + 0x10) = 99;
                *(uint*)e = word | 0x4000;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00462250 @ 0x00462250 — creneau <b>14</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 65 instructions.
    ///
    /// <para>
    /// <b>Le bloc qu'on pousse, quatrieme de la famille des rochers qui roulent.</b> Meme
    /// charpente que les trois de la foret : <see cref="MoveSceneEntity_00455B30"/> avec un masque
    /// de refus — ici <c>0x840</c>, le plus etroit des quatre —, extinction du bit
    /// <c>0x80000000</c> quand le deplacement le rend, <see cref="Rolling"/> pour le grondement,
    /// <see cref="TestTriggerVolumes_00534CD0"/> et remise a zero des trois vitesses.
    /// </para>
    /// <para>
    /// <b>Ce qui lui est propre : la sous-zone 6 a son propre son.</b> La <c>6</c> joue le son
    /// <c>0x1e</c> a hauteur <c>0x400</c> et gain <c>0xb00</c> — plus grave et plus doux ; partout
    /// ailleurs c'est le <c>0x16</c> a <c>0x800</c> et gain plein. C'est le seul gestionnaire de
    /// decor du labyrinthe qui lit <see cref="s_Unknown_005de52c"/> pour choisir un son.
    /// </para>
    /// <para>
    /// Le repos entre deux grondements est de <b>dix</b> images (<c>+0x98</c> charge a 10, decompte
    /// borne a zero), contre quinze en foret.
    /// </para>
    /// </summary>
    private static void EntityHandler_00462250(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x98) = 0;

        if ((short)MoveSceneEntity_00455B30(e, 0x840) == 1) *(uint*)e &= 0x7fffffff;

        if (Rolling(e) && *(short*)(e + 0x98) == 0)
        {
            // Ghidra: `cmp byte [5de52ch],6` — la sous-zone courante choisit son, hauteur et gain.
            if (s_Unknown_005de52c == 6)
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1e), 0x400, 0xb00, 0, 0);
            else
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x16), 0x800, 0x1000, 0, 0);

            *(short*)(e + 0x98) = 10;
        }

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0;

        TestTriggerVolumes_00534CD0((uint*)e);

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00460310</c> — creneau <b>45</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>57 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460310.asm</c>.
    ///
    /// <para>
    /// <b>Une source d'effets qui seme autour d'elle.</b> Elle allume le bit <c>0x4000</c> a la
    /// naissance et compte les images en <c>+0x12</c>. La naissance d'un effet <c>0x3c</c> part
    /// <b>au plus tard a la 90e image</b>, et des la <b>50e</b> avec une chance sur trente par
    /// image : le tirage est fait a <i>chaque</i> image, meme trop tot, donc il consomme le flux du
    /// hasard sans arret.
    /// </para>
    /// <para>
    /// <b>L'effet ne naît pas sur elle mais a cote.</b> Le corps deplace <c>+0x1c</c> et
    /// <c>+0x24</c> de <c>(tirage sur 200 - 100)</c> cases — donc <b>+/- 100 cases en X et en
    /// Z</b> —, appelle <see cref="SpawnEffectNearCamera_00468110"/> (sous-type <c>0x3c</c>, muet
    /// au-dela de 1800 unites de la camera), puis <b>remet les deux champs a leur valeur</b>. Le
    /// deplacement n'existe que le temps de l'appel : l'entite elle-meme ne bouge pas.
    /// </para>
    /// <para>
    /// Le binaire empile <b>six</b> arguments alors que la naissance n'en lit que quatre — le meme
    /// ecart qu'aux autres sites d'appel, et <c>add esp,18h</c> le confirme.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460310(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(uint*)e |= 0x4000;
        }

        (*(short*)(e + 0x12))++;

        // Ghidra: le tirage precede les deux seuils et se fait donc a chaque image.
        bool early = CrtRand() % 30 == 1 && *(short*)(e + 0x12) >= 0x32;   // Ghidra: FUN_00547140
        if (!early && *(short*)(e + 0x12) < 0x5a) return;

        int keptX = *(int*)(e + 0x1c);
        int keptZ = *(int*)(e + 0x24);

        *(short*)(e + 0x12) = 0;

        *(int*)(e + 0x1c) += (CrtRand() % 200 - 100) << 0xc;
        *(int*)(e + 0x24) += (CrtRand() % 200 - 100) << 0xc;

        SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);   // Ghidra: FUN_00468110

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x24) = keptZ;
    }

    // =====================================================================
    // LOT 5 (2026-09-18) — LA FIN DE LA CATEGORIE B DU DECOR, ET SES TROIS PLUS SIMPLES « C »
    //
    // Huit corps de decor. Les deux autres du lot lancent un script de zone et vivent donc dans
    // `LabyrinthMecanismes.cs`.
    // =====================================================================

    /// <summary>
    /// Pose le drapeau de script <paramref name="index"/> dans la banque d'etat 0, <b>sans le
    /// garde a 32 octets</b> de <see cref="ScriptFlagRaise"/>. Ce n'est pas une simplification :
    /// <c>0x00460c00</c> et <c>0x004606c0</c> ecrivent <c>or [eax+5e96e0h],dl</c> sans aucune
    /// comparaison, la ou <c>0x00458e90</c> en foret rend la main au-dela de l'indice 255. Les
    /// appeler par le meme code importerait un garde que ces deux corps n'ont pas.
    /// </summary>
    private static void ScriptFlagRaiseUnbounded(short index)
    {
        int cell = DivideTowardZeroBy8(index);
        int bit = (index - cell * 8) & 0x1f;

        Mem.s_StateBank0_005e96e0[(short)cell] |= (byte)(1 << bit);
    }

    /// <summary>
    /// Ghidra: FUN_00460c00 @ 0x00460c00 — creneau <b>22</b> du bloc <c>0x00557aa8</c>
    /// (sous-zones 0 a 3). Decompilee dans <c>Blaze.c</c>, 81 instructions ; le desassemblage
    /// <c>Legacy/Raw/Blaze/FUN_00460C00.asm</c> a servi a recuperer un argument que Ghidra avait
    /// perdu.
    ///
    /// <para>
    /// <b>Ce qui tourne une fois, en trente-deux images, et ne recommence jamais.</b> L'etat 0
    /// rend la main : c'est un script de zone qui pose l'etat 1. L'etat 1 est un <b>verrou a
    /// drapeau</b> : il ne laisse passer que si le drapeau numero <c>+0x14</c> est <i>absent</i>,
    /// et il le pose au passage. Une fois le drapeau levé, l'entite ne peut plus jamais partir.
    /// </para>
    /// <para>
    /// <b>L'etat 2 fait tourner <c>+0x32</c> de <c>0x20</c> par image, trente-deux fois</b> — un
    /// demi-tour exactement (<c>0x400</c>) — en sonnant le <c>0x12</c> de la sous-zone aux images
    /// 0, 10, 20 et 30, a hauteur <c>0x320</c>. <b>L'etat 3 secoue la camera</b> quarante images
    /// par <see cref="TryPublishCameraEffectNearEntity"/>, d'un decalage vertical tire dans
    /// <c>[-3, 3[</c>. L'etat 4 est la fin : rien.
    /// </para>
    /// <para>
    /// <b>La secousse ecrase les deux autres axes au lieu de les laisser.</b> Le site d'appel
    /// empile <c>(0, tirage, 0)</c> et non <c>(999, tirage, 999)</c> : la valeur sentinelle
    /// <c>999</c> voudrait dire « ne touche pas a celui-la », et les autres sites de la montagne et
    /// de la vieille tour l'emploient. Ici les deux axes horizontaux sont donc remis a zero a
    /// chaque image, ce qui annule toute secousse qu'une autre entite aurait demandee.
    /// <b>Ghidra avait perdu ce troisieme argument</b> : sa pseudo-C n'en montre que deux, et c'est
    /// <c>add esp,18h</c> qui donne les six.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460C00(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 1 && !ScriptFlagSet(*(short*)(e + 0x14)))
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
            ScriptFlagRaiseUnbounded(*(short*)(e + 0x14));
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short beat = *(short*)(e + 0x12);
            *(short*)(e + 0x32) += 0x20;

            if (beat == 0 || beat == 10 || beat == 0x14 || beat == 0x1e)
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x12), 0x320, 0x1000, 0, 0);

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x20)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            // Ghidra: FUN_0041be60(param_1, 0, rand() % 6 - 3, 0, 0, 0) — six push, add esp,18h.
            TryPublishCameraEffectNearEntity((uint*)e, 0, (int)(CrtRand() % 6) - 3, 0, 0, 0);

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x28) *(short*)(e + 0x10) = 4;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00461390 @ 0x00461390 — creneau <b>41</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 93 instructions.
    ///
    /// <para>
    /// <b>Elle frappe a chaque image, et ce qu'elle touche decolle.</b>
    /// <see cref="StrikePartyMembers_0045DA40"/> est appelee <i>hors de toute garde d'etat</i>, avec
    /// 5 % de degats et le selecteur <c>1</c> — la moitie haute de l'octet de contact
    /// <c>+9</c>. Elle rend le masque des membres reellement touches, et le corps reparcourt alors
    /// les quatre emplacements : chaque bit pose allume le bit <c>1</c> du mot de tete du membre et
    /// lui ecrit <c>0x19000</c> en <c>+0x54</c>, soit une <b>detente verticale</b>. Elle projette
    /// en l'air ce qu'elle heurte. <b>Abimer n'est pas decider</b> — c'est du decor, au meme titre
    /// que le pilon de la mine.
    /// </para>
    /// <para>
    /// <b>Son propre depart, lui, se mesure a une VOISINE.</b> L'etat 1 calcule la distance entre
    /// elle et l'entite qui la suit de <c>+0x14</c> rangs dans sa table (au pas de <c>0x9c</c>), en
    /// <b>trois</b> dimensions, et part des que cette distance tombe sous <b>1000</b> — avec le son
    /// <c>0x2b</c> de la sous-zone a hauteur <c>0x200</c> et gain <b>double</b> (<c>0x2000</c>),
    /// le plus fort de la zone. L'etat 2 la fait <b>monter</b> de <c>0x5000</c> par image pendant
    /// 35 images, puis l'etat 3, qui ne fait rien.
    /// </para>
    /// <para>
    /// La montee est ecrite apres le test de fin, si bien que <b>l'image qui passe a l'etat 3 monte
    /// encore</b> : 35 avances, pas 34.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461390(byte* e)
    {
        // Ghidra: FUN_0045da40(param_1, 5, 1, 0, 0)
        ushort struck = StrikePartyMembers_0045DA40(e, 5, 1, 0, 0);

        byte* member = Inp.s_SlotFlags_005e93c0;
        for (short i = 0; i < 4; i++, member += 0x9c)
        {
            if ((struck >> (i & 0x1f) & 1) == 0) continue;

            *(uint*)member |= 1;
            *(int*)(member + 0x54) = 0x19000;
        }

        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;

        if (*(short*)(e + 0x10) == 1)
        {
            byte* neighbour = e + *(short*)(e + 0x14) * 0x9c;

            int dx = (*(int*)(e + 0x1c) >> 0xc) - (*(int*)(neighbour + 0x1c) >> 0xc);
            int dy = (*(int*)(e + 0x20) >> 0xc) - (*(int*)(neighbour + 0x20) >> 0xc);
            int dz = (*(int*)(e + 0x24) >> 0xc) - (*(int*)(neighbour + 0x24) >> 0xc);

            if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz)) < 1000)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2b), 0x200, 0x2000, 0, 0);
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x22) *(short*)(e + 0x10) = 3;

            *(int*)(e + 0x20) += 0x5000;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00461980 @ 0x00461980 — creneau <b>30</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 95 instructions.
    ///
    /// <para>
    /// <b>Le bloc qu'on pousse dont la position SURVIT au changement de carte.</b> C'est le seul
    /// gestionnaire de decor du labyrinthe qui garde un etat hors de son propre enregistrement :
    /// ses trois coordonnees vivent dans <b>trois tables de huit entrees</b> gravees dans
    /// <c>.data</c>, indexees par son numero de placement <c>+0x14</c> —
    /// <c>0x005517a8</c> (X), <c>0x005517c8</c> (hauteur), <c>0x005517e8</c> (Z). Voir
    /// <c>Documentation/Reference/DataSymbols.md</c>. A chaque image il se <i>relit</i> depuis les
    /// tables, se deplace, et s'y <i>reecrit</i> : quitter la carte et revenir le retrouve ou on
    /// l'avait laisse.
    /// </para>
    /// <para>
    /// <b>Le controle de sanité de la naissance a un defaut, et il est dans le binaire.</b> Si X
    /// sort de <c>[201, 399]</c> ou Z de <c>[-1399, -1201]</c>, les trois entrees sont remises a la
    /// pose de depart <c>(100, 605, -1300)</c> — la meme que porte <c>0x00551798</c> dans le
    /// binaire. Mais <b>100 est hors de l'intervalle teste</b> : une fois le bloc renvoye chez
    /// lui, le controle se redeclenche a chaque nouvelle naissance et le renvoie au meme endroit.
    /// C'est sans consequence — la remise est idempotente — mais le test ne dit pas ce qu'il a
    /// l'air de dire. Transcrit tel quel.
    /// </para>
    /// <para>
    /// La hauteur <c>0x005517c8</c> n'est <b>pas</b> testee : seuls X et Z le sont, et une hauteur
    /// aberrante passe.
    /// </para>
    /// <para>
    /// Le reste est la charpente des rochers qui roulent, comme
    /// <see cref="EntityHandler_00462250"/> : masque de refus <c>0x88c0</c>,
    /// <see cref="Rolling"/> pour le grondement — ici le son <c>0x1e</c> a <c>0x400</c> et gain
    /// <c>0xb00</c> <b>sans condition de sous-zone</b>, la ou sa sœur reserve ce reglage a la
    /// sous-zone 6 —, repos de dix images, <see cref="TestTriggerVolumes_00534CD0"/> et remise a
    /// zero des trois vitesses.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461980(byte* e)
    {
        // Ghidra: (&DAT_005517a8)[i], (&DAT_005517c8)[i], (&DAT_005517e8)[i] — trois tables de
        // huit int, dans la plage s_EntityParams_00551640.
        int* storedX = Mem.EntityParam(0x005517a8);
        int* storedY = Mem.EntityParam(0x005517c8);
        int* storedZ = Mem.EntityParam(0x005517e8);

        if (*(short*)(e + 0x10) == 0)
        {
            short i = *(short*)(e + 0x14);

            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x10) = 1;

            if (storedX[i] < 0xc9 || storedX[i] > 399 ||
                storedZ[i] > -0x4b1 || storedZ[i] < -0x577)
            {
                storedX[i] = 100;
                storedY[*(short*)(e + 0x14)] = 0x25d;
                storedZ[*(short*)(e + 0x14)] = -0x514;
            }
        }

        short slot = *(short*)(e + 0x14);
        *(int*)(e + 0x1c) = storedX[slot] << 0xc;
        *(int*)(e + 0x20) = storedY[slot] << 0xc;
        *(int*)(e + 0x24) = storedZ[slot] << 0xc;

        if ((short)MoveSceneEntity_00455B30(e, 0x88c0) == 1) *(uint*)e &= 0x7fffffff;

        if (Rolling(e) && *(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1e), 0x400, 0xb00, 0, 0);
            *(short*)(e + 0x98) = 10;
        }

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0;

        storedX[*(short*)(e + 0x14)] = *(int*)(e + 0x1c) >> 0xc;
        storedY[*(short*)(e + 0x14)] = *(int*)(e + 0x20) >> 0xc;
        storedZ[*(short*)(e + 0x14)] = *(int*)(e + 0x24) >> 0xc;

        TestTriggerVolumes_00534CD0((uint*)e);

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00461100 @ 0x00461100 — creneau <b>53</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 96 instructions.
    ///
    /// <para>
    /// <b>Le balancier qui change de bord tous les cinq a neuf coups.</b> Quatre tirages a la
    /// naissance, et aucun n'est le meme : une attente de <c>[10, 70[</c> images avant le premier
    /// mouvement (etat 2), un <b>sens de depart</b> a pile ou face — <c>(rand() % 2) * 2 - 1</c>,
    /// donc <c>-1</c> ou <c>+1</c> —, la premiere cible a <c>+0x34 + sens * 0x400</c>, et un
    /// <b>nombre de coups</b> dans <c>[5, 10[</c> avant le demi-tour.
    /// </para>
    /// <para>
    /// <b>L'angle poursuit sa cible au huitieme de l'ecart par image</b> : <c>+0x34 += (cible -
    /// +0x34) &gt;&gt; 3</c>, une approche exponentielle qui n'arrive jamais tout a fait. Ce n'est
    /// pas l'arrivee qui declenche le coup suivant mais un <b>compteur</b> : chaque palier dure
    /// <c>[40, 70[</c> images, tire a neuf.
    /// </para>
    /// <para>
    /// <b>A la fin de chaque palier</b> : le son <c>0x1d</c> de la sous-zone a hauteur
    /// <c>0x1200</c> — le plus aigu de la zone —, la cible avance de <c>sens * 0x400</c>, le
    /// compteur de coups descend, et le palier suivant est retire. Quand les coups sont epuises, le
    /// sens s'inverse et un nouveau lot de <c>[5, 10[</c> est tire.
    /// </para>
    /// <para>
    /// <b>La butee est un recentrage brutal, et elle precede l'avance de la cible.</b> Si
    /// <c>+0x34</c> a depasse <c>±4000</c>, angle <i>et</i> cible sont remis a <b>zero</b> — pas
    /// bornes, remis a zero — puis la cible repart de <c>sens * 0x400</c>. Le balancier saute donc
    /// au centre au lieu de rebondir.
    /// </para>
    /// <para>
    /// <b>Le decompte de l'attente n'a lieu que dans l'etat 2</b> : le binaire l'ecrit dans la
    /// condition meme (<c>if (etat == 2 &amp;&amp; --compteur == 0)</c>), pas avant. Et l'etat 1
    /// est teste dans la meme image que la bascule, donc le premier palier perd une image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461100(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x3c + 10);          // Ghidra: FUN_00547140

            // Ghidra: l'idiome du reste signe, developpe par le compilateur en six instructions.
            short way = (short)((CrtRand() % 2) * 2 - 1);
            *(short*)(e + 0x16) = way;
            *(short*)(e + 0x14) = (short)(*(short*)(e + 0x34) + way * 0x400);

            *(short*)(e + 0x9a) = (short)(CrtRand() % 5 + 5);
        }

        if (*(short*)(e + 0x10) == 2 && --(*(short*)(e + 0x12)) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1e + 0x28);
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))--;

        *(short*)(e + 0x34) += (short)((*(short*)(e + 0x14) - *(short*)(e + 0x34)) >> 3);

        if (*(short*)(e + 0x12) != 0) return;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1d), 0x1200, 0x1000, 0, 0);

        if (*(short*)(e + 0x34) > 4000 || *(short*)(e + 0x34) < -4000)
        {
            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x14) = 0;
        }

        *(short*)(e + 0x14) += (short)(*(short*)(e + 0x16) * 0x400);

        (*(short*)(e + 0x9a))--;
        *(short*)(e + 0x12) = (short)(CrtRand() % 0x1e + 0x28);

        if (*(short*)(e + 0x9a) == 0)
        {
            *(short*)(e + 0x9a) = (short)(CrtRand() % 5 + 5);
            *(short*)(e + 0x16) = (short)-*(short*)(e + 0x16);
        }
    }

    /// <summary>
    /// Ghidra: FUN_004620d0 @ 0x004620d0 — creneau <b>13</b> du bloc <c>0x00558128</c>
    /// (sous-zones 5 a 8). Decompilee dans <c>Blaze.c</c>, 109 instructions.
    ///
    /// <para>
    /// <b>La navette : elle va et vient sur une droite, indefiniment.</b> Son cap <c>+0x32</c> est
    /// fixe par le placement et n'est jamais touche ; sa vitesse est <c>+0x14</c> et la longueur
    /// du trajet <c>+0x16</c>. Les etats 1 et 2 sont l'aller et le retour, et chacun renvoie a
    /// l'autre : il n'y a pas de fin. Le son <c>0x1d</c> de la sous-zone part a la naissance et a
    /// <b>chaque</b> demi-tour.
    /// </para>
    /// <para>
    /// <b>La distance parcourue est comptee en unites de vitesse, pas en images</b> :
    /// <c>+0x9a += vitesse</c> a chaque pas, et le demi-tour a lieu quand ce cumul <i>depasse</i>
    /// <c>+0x16</c>. Un placement lent parcourt donc la meme distance en plus d'images, ce qui est
    /// bien le comportement voulu.
    /// </para>
    /// </summary>
    private static void EntityHandler_004620D0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x9a) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1d), 0x800, 0x1000, 0, 0);
        }

        if (*(short*)(e + 0x10) == 1) TickShuttleLeg(e, true, 2);
        if (*(short*)(e + 0x10) == 2) TickShuttleLeg(e, false, 1);
    }

    /// <summary>
    /// Un pas de la navette <see cref="EntityHandler_004620D0"/>. Les deux branches du binaire sont
    /// identiques au signe pres sur <c>+0x1c</c> et <c>+0x24</c>, et a l'etat vise.
    ///
    /// <para>
    /// <b>Les deux vitesses publiees ne s'inversent PAS avec le sens</b>, et c'est la seule chose
    /// que ce partage risquait d'effacer : <c>+0x50</c> recoit toujours le cosinus et <c>+0x58</c>
    /// toujours le sinus, non nies, dans les <i>deux</i> branches. Seul le deplacement change de
    /// signe. Ce qui lit <c>+0x50</c>/<c>+0x58</c> — le frottement, les volumes de declenchement —
    /// voit donc toujours la navette aller dans le sens de l'aller.
    /// </para>
    /// <para>
    /// Le sinus va sur <c>Z</c> et le cosinus sur <c>X</c> : c'est la convention inverse de celle
    /// de la mine, et elle est bien celle du binaire (<c>Ordinal_451</c> puis <c>+0x24</c>,
    /// <c>Ordinal_450</c> puis <c>+0x1c</c>).
    /// </para>
    /// </summary>
    private static void TickShuttleLeg(byte* e, bool outbound, short nextState)
    {
        short speed = *(short*)(e + 0x14);

        int alongZ = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x32)) * speed;   // Ordinal_451
        int alongX = (int)Winsys3d.GetCosTableEntry(*(short*)(e + 0x32)) * speed;   // Ordinal_450

        *(short*)(e + 0x9a) += speed;

        *(int*)(e + 0x50) = alongX;

        if (outbound)
        {
            *(int*)(e + 0x24) += alongZ;
            *(int*)(e + 0x1c) += alongX;
        }
        else
        {
            *(int*)(e + 0x24) -= alongZ;
            *(int*)(e + 0x1c) -= alongX;
        }

        *(int*)(e + 0x58) = alongZ;
        *(int*)(e + 0x54) = 0;

        if (*(short*)(e + 0x16) < *(short*)(e + 0x9a))
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1d), 0x800, 0x1000, 0, 0);
            *(short*)(e + 0x10) = nextState;
            *(short*)(e + 0x9a) = 0;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00460800</c> — creneau <b>39</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>124 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460800.asm</c>.
    ///
    /// <para>
    /// <b>Ce qui s'effondre quand on monte dessus.</b> La naissance tire deux <b>vitesses de
    /// culbute</b> dans <c>[-15, 15[</c>, une par axe (<c>+0x14</c> et <c>+0x16</c>), et attend. Le
    /// bit <c>0x10</c> du mot d'etat — un membre rattache — declenche la chute, avec le son
    /// <c>0x1f</c> de la sous-zone a hauteur <c>0x300</c> et gain <c>0x1800</c>, une fois et demie
    /// le maximum ordinaire.
    /// </para>
    /// <para>
    /// <b>L'etat 1 est la descente lente : vingt images a <c>0x5000</c>.</b> A chaque image quatre
    /// effets <c>0x33</c> naissent — et le corps <b>remonte l'entite de <c>0xc8000</c> le temps de
    /// les faire naitre</b>, puis la redescend : la poussiere part du haut du bloc, pas de son
    /// centre. C'est un detournement de <c>+0x20</c>, pas une position d'effet.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est la chute libre, et elle accelere par le compteur lui-meme</b> :
    /// <c>+0x20 -= compteur &lt;&lt; 12</c>, le compteur partant de <b>5</b>. En trente images la
    /// vitesse triple. La culbute tourne les deux axes de leurs vitesses tirees a la naissance. Le
    /// son <c>0x20</c> part a l'image <b>25</b> exactement, la poussiere <c>0x33</c> s'arrete la, et
    /// trois effets <c>0x36</c> naissent par image des images 25 a 30.
    /// </para>
    /// <para>
    /// <b>Sous <c>-0x320000</c> l'entite s'eteint</b> — le bit <c>0x80000000</c> tombe — mais le
    /// corps continue : il n'y a pas de garde de sortie, et la hauteur testee est <b>tronquee a la
    /// case</b> (<c>&amp; 0xfffff000</c>) avant la comparaison.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460800(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = (short)(CrtRand() % 30 - 15);   // Ghidra: FUN_00547140
            *(short*)(e + 0x16) = (short)(CrtRand() % 30 - 15);
        }

        if ((*e & 0x10) != 0 && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1f), 0x300, 0x1800, 0, 0);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int height = *(int*)(e + 0x20);

            // Ghidra: 0x0046088d — la hauteur est relevee de 0xc8000 pour la naissance des quatre
            // effets, puis rendue baissee de 0x5000.
            *(int*)(e + 0x20) = height + 0xc8000;
            for (int i = 4; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x33, 0, -1);

            (*(short*)(e + 0x12))++;
            *(int*)(e + 0x20) = height - 0x5000;

            if (*(short*)(e + 0x12) == 0x14)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 5;
            }
        }

        if (*(short*)(e + 0x10) != 2) return;

        if (*(short*)(e + 0x12) == 0x19)
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x300, 0x1800, 0, 0);

        if (*(short*)(e + 0x12) < 0x19) SpawnEffectNearCamera_00468110(e, 0x33, 0, -1);

        short beat = *(short*)(e + 0x12);

        *(short*)(e + 0x32) += *(short*)(e + 0x14);
        *(short*)(e + 0x34) += *(short*)(e + 0x16);

        *(int*)(e + 0x20) -= beat << 0xc;
        *(short*)(e + 0x12) = (short)(beat + 1);

        // Ghidra: `and edx,0FFFFF000h ; cmp edx,0FFCE0000h ; jge` — la hauteur tronquee a la case.
        if ((*(int*)(e + 0x20) & unchecked((int)0xfffff000)) < -0x320000) *(uint*)e &= 0x7fffffff;

        if (*(short*)(e + 0x12) >= 0x19 && *(short*)(e + 0x12) <= 0x1e)
            for (int i = 3; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x36, 0, -1);
    }

    /// <summary>
    /// Ghidra: <c>0x00461f50</c> — creneau <b>12</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>124 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00461F50.asm</c>.
    ///
    /// <para>
    /// <b>Elle range QUATRE compteurs dans DEUX demi-mots</b>, et c'est ce qu'il faut voir d'abord :
    /// le corps depaquette les octets haut et bas de <c>+0x10</c> et de <c>+0x16</c> dans des
    /// variables de pile, travaille dessus, et les remballe en sortant. L'etat n'est donc que
    /// l'octet <b>bas</b> de <c>+0x10</c>, et <c>+0x11</c> porte un second compteur. Lire
    /// <c>+0x10</c> comme un mot entier ailleurs dans la zone donnerait n'importe quoi.
    /// </para>
    /// <para>
    /// <b>Ce qui s'ecroule en vingt-cinq images, puis se releve.</b> La naissance retient son angle
    /// dans <c>+0x14</c> et fait naitre <b>vingt et un</b> effets <c>0x33</c> d'un coup. L'etat 1
    /// est la chute : <c>+0x20 -= palier &lt;&lt; 12</c>, le palier montant d'un <b>toutes les deux
    /// images</b>, et l'angle <c>+0x34</c> avance de <c>0x12</c> par image. A la 25e image le son
    /// <c>0x25</c> de la sous-zone part et vingt et un effets <c>0x34</c> naissent.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est le relevement, et il s'arrete quand les DEUX mouvements sont finis.</b>
    /// L'angle revient vers <c>+0x14</c> par pas de <c>0x1c</c> et la hauteur descend encore par
    /// pas de <c>0x3000</c> jusqu'a <c>-0x190000</c> ; un compteur local compte combien des deux
    /// ont bouge, et c'est <b>zero</b> qui declenche la pose finale — angle cale sur <c>+0x14</c>,
    /// hauteur calee sur <c>-0x190000</c>, mot d'etat prenant <c>0x50000000</c>.
    /// </para>
    /// <para>
    /// <b>Le bit <c>0x40000000</c> de cette pose est la garde d'entree du corps</b>, et la sortie
    /// par cette garde <b>saute le remballage</b> : une fois pose, plus rien n'est ecrit. Seule
    /// <see cref="ClearEntityVelocity_00460FC0"/>, appelee avant la garde, continue de tourner.
    /// </para>
    /// <para>
    /// Le binaire compare le sous-compteur a <c>BL</c>, c'est-a-dire a l'etat, qui vaut 1 a cet
    /// endroit ; c'est ecrit <c>== 1</c> ici.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461F50(byte* e)
    {
        // Ghidra: `mov ax,[esi+10h] ; sar eax,8` deux fois — les quatre octets sont depaquetes
        // dans des variables de pile avant l'appel, et remballes a la fin.
        byte state = e[0x10];
        byte tier = e[0x11];
        byte half = e[0x16];
        byte total = e[0x17];

        ClearEntityVelocity_00460FC0(e);          // Ghidra: FUN_00460fc0

        if ((*(uint*)e & 0x40000000) != 0) return;   // sort SANS remballer

        if (state == 0)
        {
            *(short*)(e + 0x14) = *(short*)(e + 0x34);

            tier = 0;
            half = 0;
            total = 0;
            state = 1;

            for (int i = 0x15; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x33, 0, -1);
        }

        if (state == 1)
        {
            *(int*)(e + 0x20) -= tier << 0xc;

            half++;
            total++;
            if (half == 1) { half = 0; tier++; }

            *(short*)(e + 0x34) += 0x12;

            if (total == 0x19)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x25), 0x800, 0x1000, 0, 0);
                state = 2;

                for (int i = 0x15; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x34, 0, -1);
            }
        }

        if (state == 2)
        {
            short goal = *(short*)(e + 0x14);
            int moving = 0;

            if (*(short*)(e + 0x34) > goal)
            {
                *(short*)(e + 0x34) -= 0x1c;
                moving = 1;
            }

            if (*(int*)(e + 0x20) > -0x190000)
            {
                *(int*)(e + 0x20) -= 0x3000;
                moving++;
            }

            if (moving == 0)
            {
                *(short*)(e + 0x34) = goal;
                *(uint*)e |= 0x50000000;
                *(int*)(e + 0x20) = -0x190000;
                state = 3;
            }
        }

        // Ghidra: `mov ch,[esp+10h] ; mov cl,bl ; mov [esi+10h],cx` et son jumeau sur +0x16.
        e[0x10] = state;
        e[0x11] = tier;
        e[0x16] = half;
        e[0x17] = total;
    }

    /// <summary>
    /// Ghidra: <c>0x00461260</c> — creneau <b>52</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>83 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00461260.asm</c>.
    ///
    /// <para>
    /// <b>Une entite qui sert de porte-voix a TROIS endroits fixes de la carte.</b> Elle ne se sert
    /// pas de sa propre position : a chaque image elle la sauvegarde, se <b>teleporte</b> tour a
    /// tour sur trois coordonnees ecrites en dur dans le code, fait naitre un effet <c>0x3b</c> a
    /// chacune, puis se remet ou elle etait. Les trois points partagent la meme hauteur
    /// (<c>-0xfa000</c>) :
    /// </para>
    /// <list type="bullet">
    ///   <item><c>(0x76c000, -0xfa000, -0x14e6000)</c>, cap <c>0</c></item>
    ///   <item><c>(0x8ca000, -0xfa000, -0x1324000)</c>, cap <c>0x400</c></item>
    ///   <item><c>(0x8ca000, -0xfa000, -0x1644000)</c>, cap <c>0x400</c></item>
    /// </list>
    /// <para>
    /// <b>Tout le corps est sous un garde de CAMERA</b> : si la hauteur visee
    /// <c>0x005de06c</c> descend sous <c>-0x1f4000</c>, la fonction rend la main sans rien faire.
    /// Les trois effets ne naissent donc que quand la vue est assez haute — c'est un garde de
    /// visibilite, pas de proximite.
    /// </para>
    /// <para>
    /// <b>Le cap <c>+0x32</c> est sauvegarde et rendu, mais <c>+0x14</c> ne l'est PAS</b> : il est
    /// mis a zero avant le premier effet et jamais restitue. Le champ de placement est donc perdu
    /// des la premiere image. C'est ce que le binaire fait.
    /// </para>
    /// <para>
    /// <b>Elle sonne aussi, toutes les vingt images</b> : le son <c>0x17</c> de la sous-zone par
    /// <see cref="Audio.PlaySoundEffect"/>, donc <b>sans atténuation par la distance</b>, a hauteur
    /// <c>0x500</c>. C'est le meme son que <see cref="EntityHandler_004603C0"/> — la source
    /// immobile des sous-zones 0 a 3 — mais plus grave (<c>0x500</c> contre <c>0x700</c>) et sur
    /// une periode de <b>vingt</b> images et non vingt et une : la remise a zero se fait sur
    /// l'egalite a <c>0x14</c>, pas sur un depassement.
    /// </para>
    /// </summary>
    private static void EntityHandler_00461260(byte* e)
    {
        // Ghidra: `mov eax,[5de06ch] ; cmp eax,0FFE0C000h ; jl` — la hauteur visee par la camera.
        if (*Mem.Camera(0x005de06c) < -0x1f4000) return;

        short keptSpin = *(short*)(e + 0x32);
        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);

        *(short*)(e + 0x14) = 0;          // jamais restitue
        *(short*)(e + 0x32) = 0;

        *(int*)(e + 0x1c) = 0x76c000;
        *(int*)(e + 0x20) = -0xfa000;
        *(int*)(e + 0x24) = -0x14e6000;
        SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);

        *(int*)(e + 0x1c) = 0x8ca000;
        *(int*)(e + 0x20) = -0xfa000;
        *(int*)(e + 0x24) = -0x1324000;
        *(short*)(e + 0x32) = 0x400;
        SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);

        *(int*)(e + 0x1c) = 0x8ca000;
        *(int*)(e + 0x20) = -0xfa000;
        *(int*)(e + 0x24) = -0x1644000;
        *(short*)(e + 0x32) = 0x400;
        SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x20) = keptY;
        *(int*)(e + 0x24) = keptZ;
        *(short*)(e + 0x32) = keptSpin;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x98) = 0;
        }

        // Ghidra: FUN_0053cea0(**(int **)(DAT_005e97cc + 0xa8) + 0x17, 0x500, 0x800, 0x800)
        if (*(short*)(e + 0x98) == 0) Aud.PlaySoundEffect(ZoneSound(0x17), 0x500, 0x800, 0x800);

        (*(short*)(e + 0x98))++;
        if (*(short*)(e + 0x98) == 0x14) *(short*)(e + 0x98) = 0;
    }

    // =====================================================================
    // LOT 7 (2026-09-18) — LA CATEGORIE C DU DECOR, VIDEE
    //
    // Les neuf corps « moyens » de la table 0x0056adc8. Six sont ici ; les trois qui prennent le
    // GROUPE en charge sont dans `LabyrinthMecanismes.cs`.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00460430</c> — creneau <b>44</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>202 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460430.asm</c>.
    ///
    /// <para>
    /// <b>La grande sœur de <see cref="EntityHandler_00461260"/> : quatorze points fixes au lieu de
    /// trois.</b> Meme principe exactement — elle met sa position et son cap de cote, se
    /// <b>teleporte</b> sur une suite de coordonnees ecrites en dur, fait naitre un effet
    /// <c>0x3b</c> a chacune, puis se remet ou elle etait. <b>Sept endroits, deux caps chacun</b>
    /// (<c>0x400</c> puis <c>0xc00</c>, sauf le sixieme qui prend <c>0x800</c> puis <c>0</c>) :
    /// </para>
    /// <list type="number">
    ///   <item><c>( 0x514000, -0x46000, -0x35c000)</c></item>
    ///   <item><c>( 0x514000, -0x64000, -0x24e000)</c></item>
    ///   <item><c>(-0x834000, -0x64000,  0x190000)</c></item>
    ///   <item><c>( 0x514000, -0x64000,  0x94c000)</c></item>
    ///   <item><c>( 0x514000, -0x46000,  0xa64000)</c></item>
    ///   <item><c>( 0x42e000, -0x64000,  0x44c000)</c> — caps <c>0x800</c> et <c>0</c></item>
    ///   <item><c>(-0x2bc000, -0x64000,  0x384000)</c></item>
    /// </list>
    /// <para>
    /// <b>Et elle porte le meme bruit de fond que les deux autres</b> : le son <c>0x17</c> de la
    /// sous-zone par <see cref="Audio.PlaySoundEffect"/>, donc sans attenuation, a hauteur
    /// <c>0x700</c> et sur une periode de <b>vingt et une</b> images — les memes valeurs que
    /// <see cref="EntityHandler_004603C0"/>, au mot pres. Les trois sont la meme source sonore
    /// posee dans trois blocs.
    /// </para>
    /// <para>
    /// <b><c>+0x14</c> est mis a zero et jamais restitue</b>, comme chez
    /// <see cref="EntityHandler_00461260"/> : seuls <c>+0x1c</c>, <c>+0x20</c>, <c>+0x24</c> et
    /// <c>+0x32</c> sont sauvegardes sur la pile et rendus.
    /// </para>
    /// <para>
    /// Le corps est <b>entierement deroule</b> dans le binaire — vingt-huit appels ecrits l'un
    /// apres l'autre. Il est ecrit ici comme une table et une boucle : c'est la reconstruction du
    /// flot que <c>Strategy.md</c> demande, et la table se lit.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460430(byte* e)
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

        int keptX = *(int*)(e + 0x1c);
        int keptY = *(int*)(e + 0x20);
        int keptZ = *(int*)(e + 0x24);
        short keptSpin = *(short*)(e + 0x32);

        *(short*)(e + 0x14) = 0;          // jamais restitue

        for (int i = 0; i < s_LabyrinthBeaconPoints.Length; i++)
        {
            (int x, int y, int z, short first, short second) = s_LabyrinthBeaconPoints[i];

            *(int*)(e + 0x1c) = x;
            *(int*)(e + 0x20) = y;
            *(int*)(e + 0x24) = z;

            *(short*)(e + 0x32) = first;
            SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);      // Ghidra: FUN_00468110

            *(short*)(e + 0x32) = second;
            SpawnEffectNearCamera_00468110(e, 0x3b, 0, -1);
        }

        *(int*)(e + 0x1c) = keptX;
        *(int*)(e + 0x20) = keptY;
        *(int*)(e + 0x24) = keptZ;
        *(short*)(e + 0x32) = keptSpin;
    }

    /// <summary>Les sept endroits ou <see cref="EntityHandler_00460430"/> va faire naitre ses
    /// effets, avec les deux caps qu'elle emploie a chacun. <b>Ce ne sont pas des donnees de
    /// <c>.data</c></b> : le binaire les porte en immediats, dans un corps entierement deroule.
    /// L'ordre est celui du binaire, et il compte — <c>SpawnEffectNearCamera_00468110</c> mesure
    /// une distance a la camera, donc chaque point peut echouer separement.</summary>
    private static readonly (int X, int Y, int Z, short First, short Second)[] s_LabyrinthBeaconPoints =
    [
        ( 0x514000, -0x46000, -0x35c000, 0x400, 0xc00),
        ( 0x514000, -0x64000, -0x24e000, 0x400, 0xc00),
        (-0x834000, -0x64000,  0x190000, 0x400, 0xc00),
        ( 0x514000, -0x64000,  0x94c000, 0x400, 0xc00),
        ( 0x514000, -0x46000,  0xa64000, 0x400, 0xc00),
        ( 0x42e000, -0x64000,  0x44c000, 0x800, 0x000),
        (-0x2bc000, -0x64000,  0x384000, 0x400, 0xc00),
    ];

    /// <summary>
    /// Ghidra: <c>0x00460d60</c> — creneau <b>15</b> du bloc <c>0x00557aa8</c> (sous-zones 0 a 3).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>135 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00460D60.asm</c>.
    ///
    /// <para>
    /// <b>Ce qui tourne, leve son drapeau, puis fait trembler la carte cinquante images.</b>
    /// L'etat 0 <b>rend la main</b> apres avoir range son angle de depart dans <c>+0x5c</c> : c'est
    /// un script de zone qui pose l'etat 1. L'etat 1 sonne le <c>0x10</c> de la sous-zone et passe
    /// a 2.
    /// </para>
    /// <para>
    /// <b>L'etat 2 tourne de <c>0x78</c> par image pendant douze images</b> et <b>repose le drapeau
    /// <c>+0x14</c> a CHAQUE image</b> — pas une fois, douze fois ; c'est sans effet, le bit etant
    /// deja pose, mais c'est ce que fait le binaire. La sortie depend de <c>+0x16</c> : l'etat
    /// <b>3</b> si <c>+0x16 == 1</c>, l'etat <b>4</b> — la fin — sinon. Le binaire l'ecrit
    /// <c>setne cl ; add ecx,3</c>.
    /// </para>
    /// <para>
    /// <b>L'etat 3 est le tremblement, et son bruit depend de la carte</b> : la sous-zone <b>0</b>
    /// joue le son <c>0x14</c> toutes les <b>trois</b> images, la sous-zone <b>2</b> le son
    /// <c>0x17</c> toutes les <b>dix</b>, et les autres ne sonnent pas. La camera est secouee
    /// verticalement d'un tirage dans <c>[-3, 4[</c> a chaque image, par
    /// <see cref="TryPublishCameraEffectNearEntity"/> avec la sentinelle <c>999</c> sur les deux
    /// autres axes — l'inverse de <see cref="EntityHandler_00460C00"/>, qui les ecrase.
    /// </para>
    /// <para>
    /// <b>A la cinquantieme image, le binaire publie DEUX secousses</b> : une a zero, puis celle du
    /// tirage, qui l'ecrase aussitot. La premiere ne sert donc qu'a sa valeur de retour, qui est
    /// jetee. Transcrit tel quel.
    /// </para>
    /// <para>
    /// <b>L'etat <c>0x63</c> est une entree separee</b>, posee par ailleurs : un quart de tour
    /// (<c>0x618</c>) d'un coup, le bit <c>0x2000</c>, et la fin.
    /// </para>
    /// </summary>
    private static void EntityHandler_00460D60(byte* e)
    {
        short state = *(short*)(e + 0x10);

        if (state == 0)
        {
            *(int*)(e + 0x5c) = *(short*)(e + 0x34);
            *(short*)(e + 0x98) = 0;
            return;
        }

        if (state == 1)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x10), 0x800, 0x1000, 0, 0);
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            ScriptFlagRaiseUnbounded(*(short*)(e + 0x14));

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x0c)
            {
                *(short*)(e + 0x12) = 0;

                // Ghidra: `setne cl ; add ecx,3` — 3 quand +0x16 vaut 1, 4 sinon.
                *(short*)(e + 0x10) = (short)(*(short*)(e + 0x16) == 1 ? 3 : 4);
            }

            *(short*)(e + 0x34) += 0x78;
        }

        if (*(short*)(e + 0x10) == 3)
        {
            *(uint*)e |= 0x2000;
            (*(short*)(e + 0x98))++;

            if (s_Unknown_005de52c == 0 && *(short*)(e + 0x98) == 3)
            {
                *(short*)(e + 0x98) = 0;
                Aud.PlaySoundEffect(ZoneSound(0x14), 0x400, 0x800, 0x800);
            }

            if (s_Unknown_005de52c == 2 && *(short*)(e + 0x98) == 0x0a)
            {
                *(short*)(e + 0x98) = 0;
                Aud.PlaySoundEffect(ZoneSound(0x17), 0x400, 0x800, 0x800);
            }

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) >= 0x32)
            {
                *(short*)(e + 0x10) = 4;
                TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);
            }

            TryPublishCameraEffectNearEntity((uint*)e, 999, (int)(CrtRand() % 7) - 3, 999, 0, 0);
        }

        if (*(short*)(e + 0x10) == 0x63)
        {
            *(short*)(e + 0x34) += 0x618;
            *(uint*)e |= 0x2000;
            *(short*)(e + 0x10) = 4;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00461c60</c> — creneau <b>9</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>130 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00461C60.asm</c>.
    ///
    /// <para>
    /// <b>Le pilon du labyrinthe : il descend, ecrase, attend, remonte, et recommence.</b> A la
    /// naissance il retient sa hauteur en <b>cases</b> dans <c>+0x14</c>, arme sa vitesse a dix et
    /// sonne le <c>0x21</c> de la sous-zone.
    /// </para>
    /// <list type="number">
    ///   <item><b>Etat 1, la descente</b> : la vitesse <c>+0x12</c> gagne 2 par image et la hauteur
    ///     la perd. <b>Cent soixante cases sous son depart</b>, le bit <c>0x2000</c> tombe — il
    ///     disparait. <b>260 cases sous son depart</b>, il se cale, frappe le groupe a <b>15 %</b>
    ///     et sonne.</item>
    ///   <item><b>Etat 2</b> : quinze images d'arret en bas.</item>
    ///   <item><b>Etat 3, la remontee</b> : <c>0xa000</c> par image jusqu'a retrouver sa hauteur de
    ///     depart.</item>
    ///   <item><b>Etat 4</b> : quinze images d'arret en haut, puis retour a l'etat 1, hauteur calee
    ///     et son <c>0x21</c> a nouveau — mais a hauteur <c>0x800</c> et non <c>0x400</c>.</item>
    /// </list>
    /// <para>
    /// <b>Le son du choc n'est PAS un son de sous-zone.</b> C'est l'identifiant <b><c>0x0b</c></b>
    /// ecrit en dur, a hauteur <c>0x653</c> — le seul son en clair de tout le decor du labyrinthe.
    /// </para>
    /// <para>
    /// <b>Il pousse aussi par <see cref="AddEntityVelocity_00454580"/></b>, en plus de deplacer sa
    /// propre hauteur : la descente pousse de <c>-(+0x12 &lt;&lt; 12)</c> et la remontee de
    /// <c>+0xa000</c>. Ce qu'il porte suit donc le mouvement.
    /// </para>
    /// <para><b>Abimer n'est pas decider</b> : c'est du decor, au meme titre que le pilon de la
    /// mine.</para>
    /// </summary>
    private static void EntityHandler_00461C60(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);          // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 10;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x21), 0x400, 0x1000, 0, 0);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 2;

            int fall = *(short*)(e + 0x12) << 0xc;
            *(int*)(e + 0x20) -= fall;

            AddEntityVelocity_00454580(e, 0, -fall, 0);          // Ghidra: FUN_00454580

            int home = *(short*)(e + 0x14);
            int height = *(int*)(e + 0x20);

            if (height <= (home - 0xa0) << 0xc) *(uint*)e &= 0xffffdfff;

            int floor = (home - 0x104) << 0xc;
            if (height <= floor)
            {
                // Ghidra: FUN_00468080(param_1, 0x0b, 0x653, 0x800, 0, 0) — un identifiant en clair.
                TryPlaySoundNearEntity((uint*)e, 0x0b, 0x653, 0x800, 0, 0);

                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(int*)(e + 0x20) = floor;

                StrikePartyMembers_0045DA40(e, 0x0f, 0, 0, 0);   // Ghidra: FUN_0045da40
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) == 0x0f)
            {
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x12))++;

            *(int*)(e + 0x20) += 0xa000;
            AddEntityVelocity_00454580(e, 0, 0xa000, 0);

            if (*(int*)(e + 0x20) >= *(short*)(e + 0x14) << 0xc)
            {
                *(short*)(e + 0x10) = 4;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 4) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 0x0f) return;

        *(short*)(e + 0x10) = 1;
        *(short*)(e + 0x12) = 0;
        *(int*)(e + 0x20) = *(short*)(e + 0x14) << 0xc;

        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x21), 0x800, 0x1000, 0, 0);
    }

    /// <summary>
    /// Ghidra: <c>0x00463150</c> — creneau <b>33</b> du bloc <c>0x005587a8</c> (sous-zones 4 et 9).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>133 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00463150.asm</c>.
    ///
    /// <para>
    /// <b>Un chemin en huit temps, qui entraine ses DEUX voisines a la fin.</b> Les etats forment
    /// deux moities symetriques, l'aller (<c>1</c> a <c>4</c>) et le retour (<c>0x63</c> a
    /// <c>0x66</c>), et chaque moitie fait la meme chose : monter a <c>0x73000</c>, glisser
    /// horizontalement par pas croissants plafonnes a 15 cases, marquer une pause de 46 images,
    /// puis redescendre par pas <b>pairs</b>.
    /// </para>
    /// <para>
    /// <b>Les pas de descente sont arrondis a l'entier pair inferieur</b> — <c>(n &amp; ~1)
    /// &lt;&lt; 11</c>, c'est-a-dire <c>(n / 2) &lt;&lt; 12</c> — donc la descente est deux fois
    /// plus lente que la glissade, et par paliers de deux images.
    /// </para>
    /// <para>
    /// <b>La seconde moitie ne part que si <c>+0x14</c> vaut 1</b>, et ce champ n'est mis qu'a zero
    /// par ce corps : c'est un script ou une voisine qui l'arme.
    /// </para>
    /// <para>
    /// <b>A la toute fin, elle ecrit l'etat 2 dans les DEUX entites qui la suivent</b> —
    /// <c>+0xac</c> et <c>+0x148</c>, c'est-a-dire <c>+0x10</c> a un et deux rangs de <c>0x9c</c>.
    /// Le meme adressage relatif que les portes a deux battants de la mine, ici a trois.
    /// </para>
    /// </summary>
    private static void EntityHandler_00463150(byte* e)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x14) = 0;

        if (*(short*)(e + 0x10) == 1)
        {
            *(int*)(e + 0x20) += 0x5000;
            if (*(int*)(e + 0x20) > 0x73000)
            {
                *(int*)(e + 0x20) = 0x73000;
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(int*)(e + 0x1c) -= *(short*)(e + 0x12) << 0xc;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x0f) *(short*)(e + 0x12) = 0x0f;

            if (*(int*)(e + 0x1c) < -0x28000)
            {
                *(int*)(e + 0x1c) = -0x28000;
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x2d)
            {
                *(short*)(e + 0x10) = 4;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 4)
        {
            // Ghidra: `and ebp,0FFFFFFFEh ; shl ebp,0Bh` — le pas arrondi a l'entier pair.
            *(int*)(e + 0x20) -= (*(short*)(e + 0x12) & ~1) << 0xb;

            (*(short*)(e + 0x12))++;

            if (*(int*)(e + 0x20) < -0xc8000)
            {
                *(int*)(e + 0x20) = -0xc8000;
                *(short*)(e + 0x10) = 0x63;
            }
        }

        if (*(short*)(e + 0x10) == 0x63 && *(short*)(e + 0x14) == 1)
        {
            *(int*)(e + 0x20) += 0x5000;
            if (*(int*)(e + 0x20) > 0x73000)
            {
                *(int*)(e + 0x20) = 0x73000;
                *(short*)(e + 0x10) = 0x64;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 0x64)
        {
            *(int*)(e + 0x1c) += *(short*)(e + 0x12) << 0xc;

            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x0f) *(short*)(e + 0x12) = 0x0f;

            if (*(int*)(e + 0x1c) > 0x6a4000)
            {
                *(int*)(e + 0x1c) = 0x6a4000;
                *(short*)(e + 0x10) = 0x66;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) == 0x65)
        {
            (*(short*)(e + 0x12))++;
            if (*(short*)(e + 0x12) > 0x2d)
            {
                *(short*)(e + 0x10) = 0x66;
                *(short*)(e + 0x12) = 0;
            }
        }

        if (*(short*)(e + 0x10) != 0x66) return;

        *(int*)(e + 0x20) -= (*(short*)(e + 0x12) & ~1) << 0xb;
        (*(short*)(e + 0x12))++;

        if (*(int*)(e + 0x20) >= -0xc3000) return;

        *(int*)(e + 0x20) = -0xc3000;
        *(short*)(e + 0x10) = 0;

        // Ghidra: `mov [eax+0ACh],cx ; mov [eax+148h],cx` — +0x10 des deux entites suivantes,
        // au pas de 0x9c (0xac = 0x9c + 0x10, 0x148 = 2*0x9c + 0x10).
        *(short*)(e + 0x9c + 0x10) = 2;
        *(short*)(e + 2 * 0x9c + 0x10) = 2;
    }

    /// <summary>
    /// La fin de course partagee par <see cref="EntityHandler_00462330"/> et
    /// <see cref="EntityHandler_004625B0"/> : passe <paramref name="threshold"/> images de rebond,
    /// les deux echelles perdent <c>0xc8</c> par image, et des que l'une passe sous zero les deux
    /// sont mises a zero et <c>+0x10</c> aussi — ce qui <b>relance la naissance</b>, le corps
    /// testant <c>+0x10 == 0</c> en tete. Les deux ne tournent donc jamais tout a fait.
    ///
    /// <para>Le coup au groupe, <b>10 %</b>, est porte a chaque image et hors de toute garde : il
    /// part meme pendant la disparition.</para>
    /// </summary>
    private static void TickStomperFade(byte* e, short threshold)
    {
        if (*(short*)(e + 0x12) > threshold)
        {
            *(short*)(e + 0x2a) -= 0xc8;
            *(short*)(e + 0x28) -= 0xc8;

            if (*(short*)(e + 0x2a) < 0 || *(short*)(e + 0x28) < 0)
            {
                *(short*)(e + 0x2a) = 0;
                *(short*)(e + 0x28) = 0;
                *(short*)(e + 0x10) = 0;
            }
        }

        StrikePartyMembers_0045DA40(e, 0x0a, 0, 0, 0);       // Ghidra: FUN_0045da40
    }

    /// <summary>
    /// Ghidra: <c>0x00462330</c> — creneau <b>16</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8).
    /// <b>Absente de <c>Blaze.c</c></b> ; relevee au desassembleur, <b>193 instructions</b>, dump
    /// conserve dans <c>Legacy/Raw/Blaze/FUN_00462330.asm</c>.
    ///
    /// <para>
    /// <b>Ce qui tombe du plafond, rebondit, et frappe a chaque bond.</b> Le corps entier est sous
    /// un <b>double garde de camera</b> : <c>0x005de068</c> tronque a la case doit etre
    /// <b>positif</b>, et <c>0x005de06c</c> ne doit pas depasser <c>-0x1f4000</c>. Hors de ce
    /// cadrage, rien ne tourne.
    /// </para>
    /// <para>
    /// <b>Attention : ce n'est PAS <c>+0x10</c> qui porte l'etat, c'est <c>+0x16</c></b>, et
    /// <c>+0x10</c> est la <b>vitesse de rotation</b> de <c>+0x34</c>. Il part de <c>0x10</c> et
    /// gagne <c>0x20</c> a chaque rebond, si bien que la chose tourne de plus en plus vite.
    /// </para>
    /// <list type="number">
    ///   <item><b>Naissance</b> : elle se pose en <c>(0x7d0000, -0x6a4000, -0x1004000)</c>,
    ///     echelles a zero, et <see cref="FinalizeEntityPlacement2"/> la raccroche au terrain.</item>
    ///   <item><b><c>+0x16 == 1</c></b> : les deux echelles enflent de <c>0xc8</c> par image
    ///     jusqu'a <c>0x1000</c>.</item>
    ///   <item><b><c>+0x16 == 2</c></b> : la chute, par pas croissants, jusqu'a
    ///     <c>-0xa5a000</c> ; la phase <c>+0x14</c> repart alors a <c>-0x40</c>.</item>
    ///   <item><b><c>+0x16 == 0</c></b> : le rebond. La hauteur suit <c>|sinus| * 256</c> au-dessus
    ///     de <c>+0x60</c> plus <c>0x1e000</c>, la phase avance de <c>0x40</c> par image, et
    ///     <b>chaque passage du sinus par zero</b> — le contact — secoue la camera, fait naitre
    ///     <b>quinze</b> effets <c>0x30</c> et sonne le <c>0x19</c> de la sous-zone.</item>
    /// </list>
    /// <para>
    /// <b>Le second passage par zero accelere la rotation</b> (<c>+0x10 += 0x20</c>) et secoue la
    /// camera de <c>0x1e</c> au lieu de zero, mais ne sonne pas : les deux contacts d'un cycle ne
    /// sont pas traites de la meme facon.
    /// </para>
    /// <para>
    /// La derive horizontale est ecrite <c>(+0x12 + 0x280) &lt;&lt; 7</c> ajoute a <c>+0x24</c>, et
    /// <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c> recoivent la position d'avant — ce que
    /// <see cref="FinalizeEntityPlacement2"/> lit comme point de depart de sa marche.
    /// </para>
    /// </summary>
    private static void EntityHandler_00462330(byte* e)
    {
        // Ghidra: `test eax,0FFFFF000h ; jl` puis `cmp dword [5de06ch],0FFE0C000h ; jg`.
        if ((*Mem.Camera(0x005de068) & unchecked((int)0xfffff000)) < 0) return;
        if (*Mem.Camera(0x005de06c) > -0x1f4000) return;

        ClearEntityVelocity_00460FC0(e);          // Ghidra: FUN_00460fc0

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 0x10;           // la VITESSE DE ROTATION, pas un etat
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 1;

            *(int*)(e + 0x20) = -0x6a4000;
            *(int*)(e + 0x1c) = 0x7d0000;
            *(int*)(e + 0x24) = -0x1004000;

            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x28) = 0;

            FinalizeEntityPlacement2((uint*)e);   // Ghidra: FUN_00533940
        }

        if (*(short*)(e + 0x16) == 1)
        {
            *(short*)(e + 0x28) += 0xc8;
            *(short*)(e + 0x2a) += 0xc8;

            if (*(short*)(e + 0x28) > 0x1000)
            {
                *(short*)(e + 0x28) = 0x1000;
                *(short*)(e + 0x2a) = 0x1000;
                *(short*)(e + 0x16) = 2;
            }
        }

        if (*(short*)(e + 0x16) == 2)
        {
            (*(short*)(e + 0x14))++;
            *(short*)(e + 0x34) += 0x10;
            *(int*)(e + 0x20) -= *(short*)(e + 0x14) << 0xc;

            if (*(int*)(e + 0x20) > -0xa5a000) return;

            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = -0x40;
            return;
        }

        if (*(short*)(e + 0x16) != 0) return;

        if (Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) == 0)
        {
            TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);

            for (int i = 0x0f; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x19), 0x300, 0x1800, 0, 0);
        }

        (*(short*)(e + 0x12))++;

        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);

        *(int*)(e + 0x24) += (*(short*)(e + 0x12) + 0x280) << 7;

        FinalizeEntityPlacement2((uint*)e);

        // Ghidra: le sinus est relu, decale de 8 et NIE quand il est negatif — une valeur absolue.
        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) << 8;
        if (lift < 0) lift = -lift;

        *(short*)(e + 0x14) += 0x40;
        *(int*)(e + 0x20) = *(int*)(e + 0x60) + lift + 0x1e000;

        if (*(short*)(e + 0x14) > 0x1000) *(short*)(e + 0x14) -= 0x1000;

        if (Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) == 0)
        {
            *(short*)(e + 0x10) += 0x20;

            TryPublishCameraEffectNearEntity((uint*)e, 999, 0x1e, 999, 0, 0);

            for (int i = 0x0f; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x30, 0, -1);
        }

        *(short*)(e + 0x34) += *(short*)(e + 0x10);
        if (*(short*)(e + 0x34) > 0x1000) *(short*)(e + 0x34) -= 0x1000;

        TickStomperFade(e, 0xf5);
    }

    /// <summary>
    /// Ghidra: <c>0x004625b0</c> — creneau <b>17</b> du bloc <c>0x00558128</c> (sous-zones 5 a 8),
    /// <b>le creneau voisin</b> du precedent. <b>Absente de <c>Blaze.c</c></b> ; relevee au
    /// desassembleur, <b>208 instructions</b>, dump conserve dans
    /// <c>Legacy/Raw/Blaze/FUN_004625B0.asm</c>.
    ///
    /// <para>
    /// <b>La sœur de <see cref="EntityHandler_00462330"/>, et elle tombe de l'autre cote.</b> Meme
    /// charpente exactement — meme decoupage <c>+0x16</c>, meme <c>+0x10</c> qui est une vitesse de
    /// rotation, meme rebond sur <c>|sinus|</c> — avec <b>six differences</b>, toutes assumees :
    /// </para>
    /// <list type="bullet">
    ///   <item><b>Le garde de camera est INVERSE</b> : <c>0x005de068</c> tronque doit etre
    ///     <b>negatif ou nul</b> (<c>jg</c> et non <c>jl</c>). Les deux ne tournent donc jamais
    ///     ensemble : elles se partagent la carte.</item>
    ///   <item>Sa position de depart est <b>tiree au hasard</b> : <c>+0x20</c> vaut
    ///     <c>(rand() % 0xbb8 - 0xe74) &lt;&lt; 12</c>, et <c>+0x24</c> est fixe a
    ///     <c>0x834000</c>.</item>
    ///   <item>Elle descend jusqu'a <c>-0x14b4000</c>, et <b>tire une phase de repart</b>
    ///     (<c>+0x12 = rand() % 0x14</c>) au lieu de repartir de zero.</item>
    ///   <item>Sa rotation <c>+0x34</c> va <b>a rebours</b> — <c>-0x10</c> a la chute, puis
    ///     <c>-= +0x10</c> au rebond — et repasse par <c>+0x1000</c> quand elle devient
    ///     negative.</item>
    ///   <item>Elle seme <b>neuf</b> effets <c>0x31</c> la ou sa sœur en seme quinze de
    ///     <c>0x30</c>.</item>
    ///   <item>Sa derive est <b>negative</b> sur <c>+0x24</c> — <c>-(+0x12 &lt;&lt; 7) -
    ///     0x14000</c> — donc elle remonte le couloir quand sa sœur le descend.</item>
    /// </list>
    /// <para>
    /// Le seuil de disparition est <c>0xaa</c> et non <c>0xf5</c> : elle tient moins longtemps.
    /// </para>
    /// </summary>
    private static void EntityHandler_004625B0(byte* e)
    {
        // Ghidra: `test eax,0FFFFF000h ; jg` — le garde INVERSE de celui de sa sœur.
        if ((*Mem.Camera(0x005de068) & unchecked((int)0xfffff000)) > 0) return;
        if (*Mem.Camera(0x005de06c) > -0x1f4000) return;

        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 0x10;
            *(short*)(e + 0x12) = 0;
            *(short*)(e + 0x14) = 0;
            *(short*)(e + 0x16) = 1;

            int drop = CrtRand() % 0xbb8;         // Ghidra: FUN_00547140
            *(int*)(e + 0x24) = 0x834000;
            *(short*)(e + 0x2a) = 0;
            *(short*)(e + 0x28) = 0;
            *(int*)(e + 0x20) = (drop - 0xe74) << 0xc;

            FinalizeEntityPlacement2((uint*)e);
        }

        if (*(short*)(e + 0x16) == 1)
        {
            *(short*)(e + 0x28) += 0xc8;
            *(short*)(e + 0x2a) += 0xc8;

            if (*(short*)(e + 0x28) > 0x1000)
            {
                *(short*)(e + 0x28) = 0x1000;
                *(short*)(e + 0x2a) = 0x1000;
                *(short*)(e + 0x16) = 2;
            }
        }

        if (*(short*)(e + 0x16) == 2)
        {
            (*(short*)(e + 0x14))++;
            *(short*)(e + 0x34) -= 0x10;
            *(int*)(e + 0x20) -= *(short*)(e + 0x14) << 0xc;

            if (*(short*)(e + 0x34) < 0) *(short*)(e + 0x34) += 0x1000;

            if (*(int*)(e + 0x20) > -0x14b4000) return;

            *(short*)(e + 0x16) = 0;
            *(short*)(e + 0x14) = -0x40;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x14);
            return;
        }

        if (*(short*)(e + 0x16) != 0) return;

        if (Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) == 0)
        {
            TryPublishCameraEffectNearEntity((uint*)e, 999, 0, 999, 0, 0);

            for (int i = 9; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x19), 0x300, 0x1800, 0, 0);
        }

        (*(short*)(e + 0x12))++;

        *(int*)(e + 0x44) = *(int*)(e + 0x1c);
        *(int*)(e + 0x4c) = *(int*)(e + 0x24);
        *(int*)(e + 0x48) = *(int*)(e + 0x20);

        *(int*)(e + 0x24) -= (*(short*)(e + 0x12) << 7) + 0x14000;

        FinalizeEntityPlacement2((uint*)e);

        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) << 8;
        if (lift < 0) lift = -lift;

        *(short*)(e + 0x14) += 0x40;
        *(int*)(e + 0x20) = *(int*)(e + 0x60) + lift + 0x1e000;

        if (*(short*)(e + 0x14) > 0x1000) *(short*)(e + 0x14) -= 0x1000;

        if (Winsys3d.GetSinTableEntry(*(short*)(e + 0x14)) == 0)
        {
            *(short*)(e + 0x10) += 0x20;

            TryPublishCameraEffectNearEntity((uint*)e, 999, 0x1e, 999, 0, 0);

            for (int i = 9; i != 0; i--) SpawnEffectNearCamera_00468110(e, 0x31, 0, -1);
        }

        *(short*)(e + 0x34) -= *(short*)(e + 0x10);
        if (*(short*)(e + 0x34) < 0) *(short*)(e + 0x34) += 0x1000;

        TickStomperFade(e, 0xaa);
    }
}
