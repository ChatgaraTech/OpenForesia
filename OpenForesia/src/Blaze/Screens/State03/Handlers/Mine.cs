// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LES GESTIONNAIRES D'ENTITE DE LA MINE ABANDONNEE — zone 1, table 0x0056adc8.
//
// Ouvert le 2026-09-16, sur le patron de `Forest.cs` : tout ce qui, dans la mine, n'est ni une
// bete ni un effet — les decors qui sonnent, qui balancent, les portes gardees par un drapeau de
// script. La mine installe trois blocs dans cette table, un par groupe de sous-zones :
//
//   0x005539a8  sous-zones 0, 1, 2
//   0x00554028  sous-zones 5 a 8
//   0x005546a8  sous-zones 3, 4, 9
//
// Les particules de la mine sont dans `Effects/MineParticles.cs`, ses scenes cinema dans
// `Cutscene/MineScenes.cs`.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LES DECORS QUI SONNENT
    // =====================================================================

    /// <summary>
    /// L'identifiant de son numero <paramref name="slot"/> de la zone : l'octet
    /// <paramref name="slot"/> du premier enregistrement de la table <c>+0xa8</c> du repertoire de
    /// zone, <b>etendu sans signe</b> (<c>movzx dx, byte [ecx+slot]</c>). Meme lecture que
    /// <see cref="EntityHandler_004566C0"/> en foret : chaque zone donne sa propre voix aux decors.
    /// </summary>
    private static short ZoneSound(int slot)
        => (*(byte**)(*(byte**)(s_Unknown_005e97cc + 0xa8)))[slot];

    /// <summary>
    /// Le corps commun de <b>quatre</b> gestionnaires de la mine, relevés au desassembleur et
    /// <b>identiques instruction pour instruction</b> a cinq constantes pres :
    ///
    /// <list type="table">
    ///   <item><term><c>0x004523A0</c>, <c>0x00454B40</c></term><description>toutes les 15 images,
    ///     son 0x13, hauteur 0x800, gain 0x1000, portee 1500 — creneau 25 des blocs 0-2 et
    ///     3-4-9.</description></item>
    ///   <item><term><c>0x00453000</c>, <c>0x00454AD0</c></term><description>toutes les 10 images,
    ///     son 0x16, hauteur 0x400, gain 0x2000, portee 2000 — creneau 26 des blocs 5-8 et
    ///     3-4-9.</description></item>
    /// </list>
    /// <para>
    /// <b>Premier tour</b> : l'etat passe a 1, le compteur <c>+0x12</c> a zero, et le mot d'etat
    /// prend le bit <c>0x4000</c>. <b>Ensuite</b>, a chaque image, le compteur avance ; quand il
    /// atteint la periode il revient a zero et le son part par
    /// <see cref="TryPlayDistanceSound"/> — attenue par la distance a la camera, muet au-dela de
    /// la portee. Le premier son part donc a la periode-ieme image, pas a la naissance.
    /// </para>
    /// </summary>
    private static void TickRepeatingDistanceSound(
        byte* e, short period, int soundSlot, short pitch, short gain, int range)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) != 1) return;

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != period) return;

        *(short*)(e + 0x12) = 0;
        TryPlayDistanceSound((uint*)e, ZoneSound(soundSlot), pitch, gain, range, 0);
    }

    /// <summary>Ghidra: <c>0x004523a0</c> — sous-type 25 du bloc <c>0x005539a8</c>, sous-zones 0 a 2.
    /// Absente de <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_004523A0.asm</c>. Voir
    /// <see cref="TickRepeatingDistanceSound"/>.</summary>
    private static void EntityHandler_004523A0(byte* e)
        => TickRepeatingDistanceSound(e, 0x0f, 0x13, 0x800, 0x1000, 0x5dc);

    /// <summary>Ghidra: <c>0x00454b40</c> — sous-type 25 du bloc <c>0x005546a8</c>, sous-zones 3, 4
    /// et 9 ; la copie conforme de <see cref="EntityHandler_004523A0"/>. Absente de
    /// <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_00454B40.asm</c>.</summary>
    private static void EntityHandler_00454B40(byte* e)
        => TickRepeatingDistanceSound(e, 0x0f, 0x13, 0x800, 0x1000, 0x5dc);

    /// <summary>Ghidra: <c>0x00453000</c> — sous-type 26 du bloc <c>0x00554028</c>, sous-zones 5 a 8.
    /// Absente de <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_00453000.asm</c>. Voir
    /// <see cref="TickRepeatingDistanceSound"/>.</summary>
    private static void EntityHandler_00453000(byte* e)
        => TickRepeatingDistanceSound(e, 0x0a, 0x16, 0x400, 0x2000, 0x7d0);

    /// <summary>Ghidra: <c>0x00454ad0</c> — sous-type 26 du bloc <c>0x005546a8</c>, sous-zones 3, 4
    /// et 9 ; la copie conforme de <see cref="EntityHandler_00453000"/>. Absente de
    /// <c>Blaze.c</c> ; <c>Legacy/Raw/Blaze/FUN_00454AD0.asm</c>.</summary>
    private static void EntityHandler_00454AD0(byte* e)
        => TickRepeatingDistanceSound(e, 0x0a, 0x16, 0x400, 0x2000, 0x7d0);

    /// <summary>
    /// Ghidra: <c>0x004511f0</c> — <b>le decor qui sonne a sa propre voix</b>, sous-type <b>54</b> du
    /// bloc <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 28 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_004511F0.asm</c>.
    ///
    /// <para>
    /// <b>La cousine de <see cref="TickRepeatingDistanceSound"/>, pas sa copie.</b> Trois ecarts :
    /// </para>
    /// <list type="bullet">
    /// <item>la <b>hauteur</b> et le <b>gain</b> ne sont pas des constantes : ils sont lus dans
    /// l'entite, <c>+0x16</c> et <c>+0x14</c> — c'est le placement qui les fixe ;</item>
    /// <item>le compteur avance <b>sans que l'etat soit reteste</b> : il n'y a pas de
    /// <c>cmp [eax+10h],1</c> entre l'amorcage et le decompte ;</item>
    /// <item>le bit <c>0x4000</c> est pose par un <c>or dh,40h</c> sur l'octet 1 du mot d'etat —
    /// meme bit, autre ecriture.</item>
    /// </list>
    /// <para>Periode 15, son 0x13, portee 1500.</para>
    /// </summary>
    private static void EntityHandler_004511F0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(uint*)e |= 0x4000;
            *(short*)(e + 0x12) = 0;
        }

        (*(short*)(e + 0x12))++;
        if (*(short*)(e + 0x12) != 0x0f) return;

        *(short*)(e + 0x12) = 0;
        TryPlayDistanceSound(
            (uint*)e, ZoneSound(0x13), *(short*)(e + 0x16), *(short*)(e + 0x14), 0x5dc, 0);
    }

    /// <summary>
    /// Ghidra: <c>0x00453de0</c> — <b>le balancier qui grince aux deux bouts</b>, sous-type <b>8</b>
    /// du bloc <c>0x00554028</c>, sous-zones 5 a 8. Absente de <c>Blaze.c</c> ; 32 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00453DE0.asm</c>.
    ///
    /// <para>
    /// Une <b>vitesse</b> <c>+0x12</c> qui part a <c>0x18</c> et une <b>acceleration</b>
    /// <c>+0x14</c> de <c>-1</c>. A chaque image la vitesse prend l'acceleration et s'ajoute a
    /// l'angle <c>+0x34</c> : le mouvement ralentit, s'arrete, repart. Quand la vitesse touche
    /// exactement <c>-0x18</c> ou <c>+0x18</c>, l'acceleration change de signe et un son part
    /// (<see cref="TryPlaySoundNearEntity"/>, son 0x22 de la zone, hauteur 0x400, gain 0x1000).
    /// </para>
    /// <para>
    /// <b>Le test est une egalite</b> (<c>cmp dx,0FFE8h / je</c>, <c>cmp dx,18h / jne</c>) : la
    /// vitesse avancant d'une unite a la fois, elle passe forcement par les deux bornes. La premiere
    /// image se deroule dans le meme appel que l'amorcage.
    /// </para>
    /// </summary>
    private static void EntityHandler_00453DE0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x18;
            *(short*)(e + 0x14) = -1;
        }

        if (*(short*)(e + 0x10) != 1) return;

        short push = *(short*)(e + 0x14);
        *(short*)(e + 0x12) += push;
        short speed = *(short*)(e + 0x12);
        *(short*)(e + 0x34) += speed;

        if (speed != -0x18 && speed != 0x18) return;

        *(short*)(e + 0x14) = (short)-push;
        TryPlaySoundNearEntity((uint*)e, ZoneSound(0x22), 0x400, 0x1000, 0, 0);
    }

    // =====================================================================
    // LES DECORS QUI BOUGENT
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00452b60 @ 0x00452b60 — <b>le decor qui flotte</b>, sous-type <b>52</b> du bloc
    /// <c>0x00554028</c>, sous-zones 5 a 8. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Au premier tour il fige sa hauteur de repos en <c>+0x54</c> et se tire une <b>phase</b> au
    /// hasard sur douze bits : deux decors voisins ne montent jamais ensemble. Ensuite la phase
    /// avance de <c>0x1e</c> par image — un tour complet en 137 images — et la hauteur vaut le
    /// repos plus <c>sin(phase) * 0x17c</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00452B60(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 0x1000);
            *(int*)(e + 0x54) = *(int*)(e + 0x20);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 0x1e;
            if (*(short*)(e + 0x12) > 0xfff) *(short*)(e + 0x12) -= 0x1000;

            int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: Ordinal_451
            *(int*)(e + 0x20) = *(int*)(e + 0x54) + wave * 0x17c;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00452b20 @ 0x00452b20 — <b>le souffle intermittent</b>, sous-type <b>53</b> du
    /// bloc <c>0x00554028</c>, sous-zones 5 a 8. Decompilee dans <c>Blaze.c</c>, onze lignes.
    ///
    /// <para>
    /// Un compteur <c>+0x10</c> qui avance a chaque image et reboucle a 200. <b>Une image sur
    /// deux</b> — quand le compteur, apres increment, est impair — il fait naitre l'effet
    /// <c>0x3d</c> pres de la camera par <see cref="SpawnEffectNearCamera_00468110"/>. Le
    /// rebouclage est donc sans effet visible : 200 est pair, le rythme ne se rompt pas.
    /// </para>
    /// </summary>
    private static void EntityHandler_00452B20(byte* e)
    {
        *(short*)(e + 0x10) += 1;

        if ((*(ushort*)(e + 0x10) & 1) != 0)
        {
            SpawnEffectNearCamera_00468110(e, 0x3d, 0, -1);   // Ghidra: FUN_00468110(e,0x3d,0,-1,0,0)
        }

        if (*(short*)(e + 0x10) == 200) *(short*)(e + 0x10) = 0;
    }

    // =====================================================================
    // LES DECORS GARDES PAR UN DRAPEAU DE SCRIPT
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00451170 @ 0x00451170 — <b>le decor qui s'efface quand le script le dit</b>,
    /// sous-type <b>55</b> du bloc <c>0x005539a8</c>, sous-zones 0 a 2. Decompilee dans
    /// <c>Blaze.c</c>.
    ///
    /// <para>
    /// Il ne fait rien tant que le bit de script numero <c>+0x14</c> de la banque 0 n'est pas pose
    /// (<see cref="ScriptFlagSet"/>). Des qu'il l'est : l'etat passe a 1, puis la teinte perd
    /// <c>250</c> par canal a chaque image (<see cref="BrightenEntityTint_00404DE0"/>, qui borne a
    /// zero — une image, ou deux si un canal partait au-dessus de <c>0xfa</c>) ; quand le canal
    /// rouge est a zero l'etat passe a 2, et l'etat 2 pose les bits <c>0x6000</c> du mot d'etat.
    /// Les trois marches peuvent tomber dans le meme appel.
    /// </para>
    /// </summary>
    private static void EntityHandler_00451170(byte* e)
    {
        if (!ScriptFlagSet(*(short*)(e + 0x14))) return;

        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;

        if (*(short*)(e + 0x10) == 1)
        {
            BrightenEntityTint_00404DE0(e, -250, -250, -250);   // Ghidra: 0xffffff06
            if (e[0x38] == 0) *(short*)(e + 0x10) = 2;
        }

        if (*(short*)(e + 0x10) == 2) *(uint*)e |= 0x6000;
    }

    /// <summary>
    /// Ghidra: FUN_004545b0 @ 0x004545b0 — <b>le meme decor, sans fondu</b>, sous-type <b>55</b> du
    /// bloc <c>0x005546a8</c>, sous-zones 3, 4 et 9. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Meme creneau que <see cref="EntityHandler_00451170"/> dans l'autre groupe de sous-zones, et
    /// meme garde par le bit de script <c>+0x14</c>. Mais l'etat 1 ne touche pas a la teinte : il
    /// passe directement a 2. Le drapeau pose, le decor prend les bits <c>0x6000</c> <b>dans le
    /// meme appel</b>.
    /// </para>
    /// </summary>
    private static void EntityHandler_004545B0(byte* e)
    {
        if (!ScriptFlagSet(*(short*)(e + 0x14))) return;

        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x10) = 1;
        if (*(short*)(e + 0x10) == 1) *(short*)(e + 0x10) = 2;
        if (*(short*)(e + 0x10) == 2) *(uint*)e |= 0x6000;
    }
}
