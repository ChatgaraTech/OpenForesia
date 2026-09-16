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

    /// <summary>
    /// Ghidra: FUN_00454d90 @ 0x00454d90 — <b>le decor que le groupe declenche en s'y
    /// rattachant</b>, sous-type <b>14</b> du bloc <c>0x005546a8</c>, sous-zones 3, 4 et 9.
    /// Decompilee dans <c>Blaze.c</c>. Le role a l'ecran n'est pas etabli.
    ///
    /// <para>
    /// A chaque image il compte les membres du groupe <b>rattaches a lui</b>
    /// (<see cref="CountPartyMembersMatching"/> : <c>+0x78</c> pointe sur lui, bit <c>0x10</c>,
    /// pas <c>0x40000000</c>) et efface le bit <c>0x10</c> de son octet 7. Des qu'il y en a un et
    /// qu'il est au repos, il s'arme : etat 1, compteur a zero, et le bit <c>4</c> de
    /// <c>DAT_00571570</c>, dans l'arene d'etat. Qui relit ce bit n'est pas trace ici.
    /// </para>
    /// <para>
    /// Arme, il avance <c>+0x34</c> de <c>0x40</c> par image pendant cinq images, puis passe a
    /// l'etat 2 : bit <c>0x10</c> repose, <c>+0x58 = -0x3c000</c> (la composante Z de la vitesse,
    /// comme chez les particules) et bit <c>0x2000</c> du mot d'etat. Il ne revient jamais a
    /// l'etat 0.
    /// </para>
    /// </summary>
    private static void EntityHandler_00454D90(byte* e)
    {
        int standing = CountPartyMembersMatching(e, 0x10, 0x40000000);   // Ghidra: FUN_00535000
        e[7] &= 0xef;

        if (standing != 0 && standing > 0 && *(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *Mem.EntityState(0x00571570) |= 4;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x34) += 0x40;
            *(short*)(e + 0x12) += 1;

            if (*(short*)(e + 0x12) > 4)
            {
                *(short*)(e + 0x10) = 2;
                e[7] |= 0x10;
                *(uint*)(e + 0x58) = 0xfffc4000;
                *(uint*)e |= 0x2000;
            }
        }
    }

    // =====================================================================
    // TROIS DECORS, TROIS FOIS — UN PAR GROUPE DE SOUS-ZONES (2026-09-16)
    //
    // Les creneaux 40, 41 et 43 portent le meme decor dans les trois blocs de la mine. Les neuf
    // corps se ramenent a trois : identiques deux a deux instruction pour instruction (releves et
    // textes de Blaze.c compares adresses masquees), a une seule constante pres dans le cas du
    // creneau 41.
    //
    //   creneau 40  0x00452950  0x00454220  0x004544E0   TickBobbingSpinner
    //   creneau 41  0x004529F0  0x004542C0  0x00455A90   TickScrapingBlock (0x882, 0x882, 0x800)
    //   creneau 43  0x00452A90  0x00454450  0x00456010   TickFlickeringGlow
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00454580 @ 0x00454580 — <b>ajoute un vecteur a la vitesse d'une entite</b>,
    /// <c>+0x50</c>, <c>+0x54</c>, <c>+0x58</c>. Decompilee dans <c>Blaze.c</c>, trois lignes. La
    /// somme de <see cref="ClearEntityVelocity_00460FC0"/>.
    /// </summary>
    private static void AddEntityVelocity_00454580(byte* e, int dx, int dy, int dz)
    {
        *(int*)(e + 0x50) += dx;
        *(int*)(e + 0x54) += dy;
        *(int*)(e + 0x58) += dz;
    }

    /// <summary>
    /// Le corps du creneau 40 — <c>FUN_00452950</c>, <c>FUN_00454220</c>, <c>FUN_004544E0</c>, tous
    /// trois decompiles et identiques : <b>le decor qui danse sur place</b>.
    ///
    /// <para>
    /// Au premier tour il se tire une phase (<c>rand % 4000</c>), un sens de rotation
    /// (<c>-1</c>, <c>0</c> ou <c>+1</c>) et retient sa hauteur de pose en unites entieres
    /// (<c>+0x16 = Y &gt;&gt; 12</c>). Ensuite, a chaque image, sa hauteur devient
    /// <c>(sin(phase) + (pose - 100) * 0x20) * 0x80</c> — il ondule autour d'un point <b>cent
    /// unites sous sa pose</b> — et la difference avec l'image precedente est versee dans sa vitesse
    /// verticale, remise a zero juste avant. La phase avance de <c>0x32</c>, et il tourne de quatre
    /// pas dans son sens.
    /// </para>
    /// <para>
    /// <b>Le rebouclage de phase est strict</b> : <c>0x1000 &lt; phase</c>, donc la valeur
    /// <c>0x1000</c> elle-meme est gardee une image. Transcrit tel quel.
    /// </para>
    /// </summary>
    private static void TickBobbingSpinner(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4000);
            *(short*)(e + 0x14) = (short)(CrtRand() % 3 - 1);
            *(short*)(e + 0x16) = (short)(*(int*)(e + 0x20) >> 0xc);
        }

        int before = *(int*)(e + 0x20);
        int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: Ordinal_451
        int height = (wave + (*(short*)(e + 0x16) - 100) * 0x20) * 0x80;
        *(int*)(e + 0x20) = height;
        AddEntityVelocity_00454580(e, 0, height - before, 0);

        *(short*)(e + 0x12) += 0x32;
        if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;

        *(short*)(e + 0x32) += (short)(*(short*)(e + 0x14) * 4);
    }

    /// <summary>Ghidra: FUN_00452950 @ 0x00452950 — creneau 40, sous-zones 0 a 2.</summary>
    private static void EntityHandler_00452950(byte* e) => TickBobbingSpinner(e);

    /// <summary>Ghidra: FUN_00454220 @ 0x00454220 — creneau 40, sous-zones 5 a 8.</summary>
    private static void EntityHandler_00454220(byte* e) => TickBobbingSpinner(e);

    /// <summary>Ghidra: FUN_004544e0 @ 0x004544e0 — creneau 40, sous-zones 3, 4 et 9.</summary>
    private static void EntityHandler_004544E0(byte* e) => TickBobbingSpinner(e);

    /// <summary>
    /// Le corps du creneau 41 — <c>FUN_004529F0</c>, <c>FUN_004542C0</c>, <c>FUN_00455A90</c>,
    /// decompiles, identiques a <paramref name="moveMask"/> pres : <b>le bloc qui racle quand on le
    /// pousse</b>.
    ///
    /// <para>
    /// A chaque image il se deplace par <see cref="MoveSceneEntity_00455B30"/> et <b>s'eteint</b> si
    /// le deplacement rend 1. Si sa poussee horizontale (<c>+0x44</c> ou <c>+0x4c</c>) depasse
    /// <c>0x1000</c> en valeur absolue et que son delai <c>+0x12</c> est ecoule, il joue le son
    /// <c>0x2d</c> de la zone et rearme le delai a vingt images. Le delai decroit d'une image a
    /// chaque tour, borne a zero, et la vitesse est remise a zero en sortant.
    /// </para>
    /// <para>
    /// <b>Le delai est remis a zero a CHAQUE tour tant que l'etat vaut 0</b> — et rien, dans ce
    /// corps, ne fait quitter l'etat 0. Le son peut donc repartir a chaque image de poussee, sauf
    /// si le deplacement change <c>+0x10</c>. Transcrit tel quel : c'est ce que fait le binaire,
    /// et <see cref="MoveSceneEntity_00455B30"/> n'a pas ete relu sous cet angle.
    /// </para>
    /// </summary>
    private static void TickScrapingBlock(byte* e, uint moveMask)
    {
        if (*(short*)(e + 0x10) == 0) *(short*)(e + 0x12) = 0;

        if ((short)MoveSceneEntity_00455B30(e, moveMask) == 1) *(uint*)e &= 0x7fffffff;

        int pushX = *(int*)(e + 0x44);
        if (pushX < 0) pushX = -pushX;
        int pushZ = *(int*)(e + 0x4c);
        if (pushZ < 0) pushZ = -pushZ;

        if ((pushX > 0x1000 || pushZ > 0x1000) && *(short*)(e + 0x12) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2d), 0x800, 0x1000, 0, 0);   // FUN_00468080
            *(short*)(e + 0x12) = 0x14;
        }

        *(short*)(e + 0x12) -= 1;
        if (*(short*)(e + 0x12) < 0) *(short*)(e + 0x12) = 0;

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>Ghidra: FUN_004529f0 @ 0x004529f0 — creneau 41, sous-zones 0 a 2, masque
    /// <c>0x882</c>.</summary>
    private static void EntityHandler_004529F0(byte* e) => TickScrapingBlock(e, 0x882);

    /// <summary>Ghidra: FUN_004542c0 @ 0x004542c0 — creneau 41, sous-zones 5 a 8, masque
    /// <c>0x882</c>.</summary>
    private static void EntityHandler_004542C0(byte* e) => TickScrapingBlock(e, 0x882);

    /// <summary>Ghidra: FUN_00455a90 @ 0x00455a90 — creneau 41, sous-zones 3, 4 et 9, masque
    /// <c>0x800</c> : la seule des trois qui differe.</summary>
    private static void EntityHandler_00455A90(byte* e) => TickScrapingBlock(e, 0x800);

    /// <summary>
    /// Le corps du creneau 43 — <c>0x00452A90</c>, <c>0x00454450</c>, <c>0x00456010</c>, tous trois
    /// <b>absents de <c>Blaze.c</c></b>, releves sous <c>Legacy/Raw/Blaze/</c> et identiques
    /// instruction pour instruction : <b>la lueur qui palpite</b>.
    ///
    /// <para>
    /// Vitesse remise a zero a chaque image. Au premier tour, une phase de depart
    /// <c>(rand % 15) &lt;&lt; 8</c>. Ensuite la phase avance de <c>0x100</c> par image — seize
    /// images par cycle — et la <b>teinte grise</b> vaut
    /// <c>0x78 + (rand &amp; 0xf) - (sin(phase) &gt;&gt; 7)</c>, calculee sur un octet puis recopiee
    /// sur les trois canaux (<c>+0x38</c> ecrit en <c>dword</c> : l'octet <c>+0x3b</c> passe a
    /// zero). Un sinus lent qui respire, et un bruit de quinze niveaux par-dessus.
    /// </para>
    /// <para>
    /// <b>La soustraction est sur un octet</b> (<c>sub al,bl</c> puis <c>add al,78h</c>) : le
    /// resultat reboucle modulo 256 au lieu de saturer. Avec un sinus borne a <c>±32</c> apres le
    /// decalage, il reste entre <c>0x58</c> et <c>0xa7</c> — le rebouclage n'arrive jamais, mais
    /// c'est bien la largeur du binaire. La phase revient a zero des qu'elle atteint
    /// <c>0x1000</c>, test signe.
    /// </para>
    /// </summary>
    private static void TickFlickeringGlow(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)((CrtRand() % 15) << 8);
        }

        if (*(short*)(e + 0x10) != 1) return;

        *(short*)(e + 0x12) += 0x100;

        int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // 0x00545a1c : sinus
        byte noise = (byte)(CrtRand() & 0x0f);
        byte grey = (byte)(noise - (byte)(wave >> 7) + 0x78);

        *(uint*)(e + 0x38) = (uint)(grey << 16 | grey << 8 | grey);

        if (*(short*)(e + 0x12) >= 0x1000) *(short*)(e + 0x12) = 0;
    }

    /// <summary>Ghidra: <c>0x00452a90</c> — creneau 43, sous-zones 0 a 2.
    /// <c>Legacy/Raw/Blaze/FUN_00452A90.asm</c>.</summary>
    private static void EntityHandler_00452A90(byte* e) => TickFlickeringGlow(e);

    /// <summary>Ghidra: <c>0x00454450</c> — creneau 43, sous-zones 5 a 8.
    /// <c>Legacy/Raw/Blaze/FUN_00454450.asm</c>.</summary>
    private static void EntityHandler_00454450(byte* e) => TickFlickeringGlow(e);

    /// <summary>Ghidra: <c>0x00456010</c> — creneau 43, sous-zones 3, 4 et 9.
    /// <c>Legacy/Raw/Blaze/FUN_00456010.asm</c>.</summary>
    private static void EntityHandler_00456010(byte* e) => TickFlickeringGlow(e);
}
