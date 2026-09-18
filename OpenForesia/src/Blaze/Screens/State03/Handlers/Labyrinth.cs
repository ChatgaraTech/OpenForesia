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
    /// creneau <b>30</b> du bloc <c>0x00558128</c>.</summary>
    private static void EntityHandler_00462860(byte* e) => TickColourBreathingDecor(e);

    /// <summary>Ghidra: <c>0x00463640</c> — le meme, sous-zones 4 et 9. Le compilateur a emis deux
    /// fois le meme corps ; le desassemblage STRICT est identique.</summary>
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
}
