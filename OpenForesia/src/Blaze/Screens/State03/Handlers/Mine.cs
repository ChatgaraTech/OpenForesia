// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DECOR DE LA MINE ABANDONNEE — zone 1, table 0x0056adc8.
//
// Ouvert le 2026-09-16 sur le patron de `Forest.cs`, puis coupe en deux le meme jour, a
// 3 734 lignes. La mine installe trois blocs dans cette table, un par groupe de sous-zones :
//
//   0x005539a8  sous-zones 0, 1, 2
//   0x00554028  sous-zones 5 a 8
//   0x005546a8  sous-zones 3, 4, 9
//
// CE QUI EST ICI, ET CE QUI EST DANS `MineMecanismes.cs`. La coupure est une coupure de ROLE,
// pas de taille, et elle se lit dans le corps :
//
//   ici                 le decor : ce qui sonne, bouge, brille, tombe — et ce qui frappe.
//                       Le pilon et les deux masses enlevent des points de vie au groupe et
//                       restent pourtant ici : abimer n'est pas decider.
//
//   MineMecanismes.cs   ce qui prend le GROUPE ou la PARTIE en charge — compte les membres, lit
//                       la table des emplacements, lance un script, coupe les manettes, change
//                       de carte. Ascenseurs, wagonnet, tremie, saisie, portes, catapulte,
//                       battant pivotant, nacelle.
//
// `ZoneSound`, `AddEntityVelocity_00454580`, `EmitJetPair` et `ScrapeSoundTick` vivent ici et
// servent des deux cotes : les deux fichiers sont la meme classe partielle.
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
    /// <c>DAT_00571570</c>, dans l'arene d'etat — le signal qu'attend
    /// <see cref="EntityHandler_00454E10"/>.
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

        ScrapeSoundTick(e);

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
    /// Le raclement, commun au creneau 41 (<see cref="TickScrapingBlock"/>) et au creneau 37 : des
    /// que la poussee <c>+0x44</c> ou <c>+0x4c</c> depasse <c>0x1000</c> en valeur absolue et que
    /// le delai <c>+0x12</c> est ecoule, le son <c>0x2d</c> de la zone part et le delai repart de
    /// vingt images. Le delai descend ensuite d'un par image, plancher a zero.
    /// </summary>
    private static void ScrapeSoundTick(byte* e)
    {
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
    }

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

    // =====================================================================
    // UN DECLENCHEUR ET SON MECANISME (sous-zones 3, 4 et 9)
    //
    // 0x00454D90 (ci-dessus) pose le bit 4 de DAT_00571570 quand le groupe s'y rattache ;
    // 0x00454E10, creneau 17 du meme bloc, l'attend, l'efface et se met en mouvement. Blaze.c
    // cite ce mot a un troisieme endroit, FUN_004551d0, qui n'est pas encore transcrit.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00454e10</c> — <b>le mecanisme qui bascule au signal</b>, sous-type <b>17</b>
    /// du bloc <c>0x005546a8</c>, sous-zones 3, 4 et 9. Absente de <c>Blaze.c</c> ; 50 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00454E10.asm</c>.
    ///
    /// <para>
    /// Au repos il ne fait rien tant que le bit <c>4</c> de <c>DAT_00571570</c> n'est pas pose —
    /// celui que <see cref="EntityHandler_00454D90"/> leve. Le bit vu, il l'<b>efface</b>, joue le
    /// son <c>0x1a</c> de la zone et part avec une vitesse de rotation <c>+0x12 = 0x100</c>.
    /// </para>
    /// <para>
    /// <b>Etat 1</b> : l'angle <c>+0x34</c> perd la vitesse, qui perd <c>0x20</c> — il freine,
    /// puis repart dans l'autre sens. Des que l'angle est <b>au moins</b> <c>0x140</c> (test signe,
    /// <c>jl</c>), etat 2 et vitesse <c>-0x46</c>. <b>Etat 2</b> : meme chose, la vitesse gagnant
    /// <c>0x10</c>, jusqu'a ce que l'angle retombe a <c>0x140</c> ou moins ; il y est cale et l'etat
    /// passe a 3, ou il reste.
    /// </para>
    /// </summary>
    private static void EntityHandler_00454E10(byte* e)
    {
        int* signal = Mem.EntityState(0x00571570);

        if ((*(byte*)signal & 4) == 0 && *(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *signal &= ~4;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0x100;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1a), 0x400, 0x1000, 0, 0);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short speed = *(short*)(e + 0x12);
            *(short*)(e + 0x34) -= speed;
            *(short*)(e + 0x12) = (short)(speed - 0x20);

            if (*(short*)(e + 0x34) >= 0x140)
            {
                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = -0x46;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            short speed = *(short*)(e + 0x12);
            *(short*)(e + 0x34) -= speed;
            *(short*)(e + 0x12) = (short)(speed + 0x10);

            if (*(short*)(e + 0x34) <= 0x140)
            {
                *(short*)(e + 0x34) = 0x140;
                *(short*)(e + 0x10) = 3;
            }
        }
    }

    // =====================================================================
    // D'AUTRES DECORS QUI SONNENT ET QUI BOUGENT
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_00454130 @ 0x00454130 — <b>le decor qui respire</b>, sous-type <b>38</b> du bloc
    /// <c>0x00554028</c>, sous-zones 5 a 8. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Vitesse remise a zero a chaque image. Au premier tour : une phase et un cap tires sur
    /// <c>4000</c>, et un decompte de son tire sur dix. Ensuite la phase avance de <c>0x30</c>, le
    /// cap de <c>2</c>, et l'<b>echelle verticale</b> <c>+0x2a</c> vaut
    /// <c>0x15dc + (sin(phase) &gt;&gt; 3)</c> — elle gonfle et degonfle d'environ un dixieme. Les deux
    /// rebouclages sont stricts (<c>0x1000 &lt; x</c>).
    /// </para>
    /// <para>
    /// Toutes les quinze images, a partir du decompte tire, il joue le son <c>0x1d</c> de la zone
    /// (hauteur <c>0x600</c>) : deux decors du meme genre ne sonnent donc pas ensemble.
    /// </para>
    /// </summary>
    private static void EntityHandler_00454130(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = (short)(CrtRand() % 4000);
            *(short*)(e + 0x32) = (short)(CrtRand() % 4000);
            *(short*)(e + 0x98) = (short)(CrtRand() % 10);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 0x30;
            if (*(short*)(e + 0x12) > 0x1000) *(short*)(e + 0x12) -= 0x1000;

            int wave = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12));   // Ghidra: Ordinal_451
            *(short*)(e + 0x32) += 2;
            *(short*)(e + 0x2a) = (short)((wave >> 3) + 0x15dc);

            if (*(short*)(e + 0x32) > 0x1000) *(short*)(e + 0x32) -= 0x1000;
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 0x0f)
        {
            *(short*)(e + 0x98) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1d), 0x600, 0x1000, 0, 0);
        }
    }

    /// <summary>
    /// Ghidra: FUN_004528d0 @ 0x004528d0 — <b>le decor qui file droit devant lui</b>, sous-type
    /// <b>39</b> du bloc <c>0x005539a8</c>, sous-zones 0 a 2. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// A chaque image sa vitesse est remise a zero puis recoit <c>(cos(cap) * 8, 0, -sin(cap) * 8)</c> :
    /// il avance le long de son cap <c>+0x32</c>, qu'il ne change jamais. Un cycle de cinq images
    /// sur <c>+0x10</c> ; a la premiere de chaque cycle il joue le son <c>0x10</c> de la zone
    /// (hauteur <c>0x500</c>, gain <c>0x800</c>).
    /// </para>
    /// </summary>
    private static void EntityHandler_004528D0(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        short heading = *(short*)(e + 0x32);
        int sine = (int)Winsys3d.GetSinTableEntry(heading);     // Ghidra: Ordinal_451
        int cosine = Winsys3d.GetCosTableEntry(heading);        // Ghidra: Ordinal_450
        AddEntityVelocity_00454580(e, cosine << 3, 0, sine * -8);

        if (*(short*)(e + 0x10) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x10), 0x500, 0x800, 0, 0);
        }

        *(short*)(e + 0x10) += 1;
        if (*(short*)(e + 0x10) == 5) *(short*)(e + 0x10) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00452670 @ 0x00452670 — <b>le debris qui se detache et tombe</b>, sous-type
    /// <b>10</b> du bloc <c>0x005539a8</c>, sous-zones 0 a 2. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Tant que le bit <c>0x40000000</c> n'est pas pose. Au premier tour : bit <c>0x2000</c>, etat 1.
    /// <b>Etat 1</b> : il attend — a chaque image une chance sur quinze (<c>rand % 15 == 1</c>), et de
    /// toute facon a la vingtieme. Alors : son <c>0x10</c> de la zone, deux effets <c>0x35</c> pres
    /// de la camera, une vitesse de chute <c>+0x12 = 5</c>, une rotation <c>+0x14</c> tiree sur
    /// <c>±60</c> et un pas de cap <c>+0x16</c> tire sur <c>±25</c>.
    /// </para>
    /// <para>
    /// <b>Etat 2</b> : le cap tourne, la hauteur descend de la vitesse (en <c>0x1000</c>), l'angle
    /// <c>+0x34</c> tourne, et la vitesse gagne 2 — une chute qui accelere. Il s'eteint sous
    /// <c>-0xfa0000</c>. La vitesse est incrementee <b>apres</b> avoir servi, comme dans le binaire.
    /// </para>
    /// </summary>
    private static void EntityHandler_00452670(byte* e)
    {
        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x2000;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int roll = CrtRand();
            *(short*)(e + 0x12) += 1;

            if (roll % 0x0f == 1 || *(short*)(e + 0x12) == 0x14)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x10), 0x600, 0x1000, 0, 0);
                SpawnEffectNearCamera_00468110(e, 0x35, 0, -1);
                SpawnEffectNearCamera_00468110(e, 0x35, 0, -1);

                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 5;
                *(short*)(e + 0x14) = (short)(CrtRand() % 0x78 - 0x3c);
                *(short*)(e + 0x16) = (short)(CrtRand() % 0x32 - 0x19);
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x32) += *(short*)(e + 0x16);

            short fall = *(short*)(e + 0x12);
            *(int*)(e + 0x20) += fall * -0x1000;
            *(short*)(e + 0x34) += *(short*)(e + 0x14);
            *(short*)(e + 0x12) = (short)(fall + 2);

            if (*(int*)(e + 0x20) < -0xfa0000) *(uint*)e &= 0x7fffffff;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00453e60 @ 0x00453e60 — <b>le decor qui s'effondre dans le sol</b>, sous-type
    /// <b>9</b> du bloc <c>0x00554028</c>, sous-zones 5 a 8. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// Tant que le bit <c>0x40000000</c> n'est pas pose, et vitesse remise a zero a chaque image.
    /// <b>Naissance</b> : il retient sa hauteur de pose en unites entieres (<c>+0x14</c>), joue le son
    /// <c>0x25</c> de la zone et fait naitre <b>onze</b> effets <c>0x37</c> pres de la camera.
    /// <b>Etat 1</b> : il s'enfonce de <c>+0x12 * 0x8000</c> par image — a la fois dans sa position et
    /// dans sa vitesse — jusqu'a <b>400 unites</b> sous sa pose ; alors etat 2 et bit <c>0x2000</c>.
    /// <b>Etat 2</b> : il tourne de <c>0x80</c> par image et ses echelles <c>+0x28</c> et <c>+0x2c</c>
    /// perdent <c>0x200</c> ; des que <c>+0x28</c> passe sous 1, il pose <c>0x40000000</c> et ne fait
    /// plus rien.
    /// </para>
    /// </summary>
    private static void EntityHandler_00453E60(byte* e)
    {
        ClearEntityVelocity_00460FC0(e);

        if ((*(uint*)e & 0x40000000) != 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 1;
            *(short*)(e + 0x14) = (short)(*(int*)(e + 0x20) >> 0xc);

            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x25), 0x400, 0x1000, 0, 0);
            for (int i = 0; i < 0x0b; i++) SpawnEffectNearCamera_00468110(e, 0x37, 0, -1);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int sink = *(short*)(e + 0x12) * -0x8000;
            *(int*)(e + 0x20) += sink;
            AddEntityVelocity_00454580(e, 0, *(short*)(e + 0x12) * -0x8000, 0);

            if ((*(int*)(e + 0x20) >> 0xc) < *(short*)(e + 0x14) - 400)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e |= 0x2000;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x32) += 0x80;
            *(short*)(e + 0x28) -= 0x200;
            *(short*)(e + 0x2c) -= 0x200;

            if (*(short*)(e + 0x28) < 1) *(uint*)e |= 0x40000000;
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00451a00</c> — <b>la chute de pierres au signal du script</b>, sous-type
    /// <b>30</b> du bloc <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 86
    /// instructions, <c>Legacy/Raw/Blaze/FUN_00451A00.asm</c>.
    ///
    /// <para>
    /// Au repos, il attend le bit <c>0x10</c> du <b>premier octet de la banque 3</b>
    /// (<c>0x005e9840</c>). Le bit vu : bit <c>0x2000</c> du mot d'etat, son <c>0x2b</c> de la zone
    /// (hauteur <c>0x300</c>), et il passe <b>directement a l'etat 2</b> avec un compteur a 3.
    /// </para>
    /// <para>
    /// <b>L'etat 2 est une attente</b> : le compteur monte jusqu'a 10, puis l'etat passe a 1 et le
    /// compteur repart de 3. <b>L'etat 1 est la chute</b> : la hauteur descend de
    /// <c>compteur * 0x1000</c> (le compteur d'avant l'increment), le compteur gagne 1 et plafonne a
    /// 10, l'angle <c>+0x34</c> tourne de <c>0x0f</c>. Dans la bande de hauteur
    /// <c>]-0x320000, -0x2bc000[</c>, il joue une fois le son <c>0x2c</c> et fait naitre
    /// <b>quatre</b> effets <c>0x32</c> par image. Sous <c>-0x4b0000</c>, il s'eteint.
    /// </para>
    /// </summary>
    private static void EntityHandler_00451A00(byte* e)
    {
        if ((Mem.s_StateBank3_005e9840[0] & 0x10) == 0 && *(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x2000;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2b), 0x300, 0x1000, 0, 0);
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 3;
            *(short*)(e + 0x98) = 0;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            short step = *(short*)(e + 0x12);
            *(int*)(e + 0x20) -= step << 0xc;
            step++;
            *(short*)(e + 0x12) = step;
            if (step > 0x0a) *(short*)(e + 0x12) = 0x0a;

            int height = *(int*)(e + 0x20);
            *(short*)(e + 0x34) += 0x0f;

            if (height < -0x2bc000 && height > -0x320000)
            {
                if (*(short*)(e + 0x98) == 0)
                {
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2c), 0x200, 0x1000, 0, 0);
                    *(short*)(e + 0x98) = 1;
                }

                for (int i = 0; i < 4; i++) SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
            }

            if (*(int*)(e + 0x20) < -0x4b0000) *(uint*)e &= 0x7fffffff;
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) == 0x0a)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 3;
            }
        }
    }

    /// <summary>
    /// Ghidra: <c>0x00453280</c> — <b>le decor a deux places</b>, sous-type <b>56</b> du bloc
    /// <c>0x00554028</c>, sous-zones 5 a 8. Absente de <c>Blaze.c</c> ; 109 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00453280.asm</c>.
    ///
    /// <para>
    /// <b>Il se tient toujours a celle de ses deux places qui est la plus proche du point que vise
    /// la camera</b> (<c>0x005de068</c>/<c>06c</c>/<c>070</c>). Au premier tour il construit la
    /// seconde place a partir de trois champs de placement : <c>+0x30</c>, <c>+0x34</c> et
    /// <c>+0x14</c> deviennent les coordonnees <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c> (en
    /// <c>0x1000</c>), et <c>+0x30</c>/<c>+0x34</c> sont remis a zero. <c>+0x16</c> porte le cap de
    /// la seconde place.
    /// </para>
    /// <para>
    /// Ensuite, a chaque image, deux distances en unites entieres par la racine carree de winsys3d
    /// (<c>Ordinal_442</c>, stdcall) : la place courante et l'autre. Si la courante est
    /// <b>strictement</b> plus loin, les deux places s'echangent — position et cap.
    /// </para>
    /// </summary>
    private static void EntityHandler_00453280(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = *(short*)(e + 0x30) << 0xc;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x48) = *(short*)(e + 0x34) << 0xc;
            *(int*)(e + 0x4c) = *(short*)(e + 0x14) << 0xc;
            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x34) = 0;
        }

        int cx = *Mem.Camera(0x005de068) >> 0xc;
        int cy = *Mem.Camera(0x005de06c) >> 0xc;
        int cz = *Mem.Camera(0x005de070) >> 0xc;

        int hereX = cx - (*(int*)(e + 0x1c) >> 0xc);
        int hereY = cy - (*(int*)(e + 0x20) >> 0xc);
        int hereZ = cz - (*(int*)(e + 0x24) >> 0xc);

        int thereX = cx - (*(int*)(e + 0x44) >> 0xc);
        int thereY = cy - (*(int*)(e + 0x48) >> 0xc);
        int thereZ = cz - (*(int*)(e + 0x4c) >> 0xc);

        int here = (int)Winsys3d.Ordinal_442((uint)(hereX * hereX + hereY * hereY + hereZ * hereZ));
        int there = (int)Winsys3d.Ordinal_442((uint)(thereX * thereX + thereY * thereY + thereZ * thereZ));

        if (here <= there) return;

        int x = *(int*)(e + 0x1c), y = *(int*)(e + 0x20), z = *(int*)(e + 0x24);
        short heading = *(short*)(e + 0x32);

        *(int*)(e + 0x1c) = *(int*)(e + 0x44);
        *(int*)(e + 0x20) = *(int*)(e + 0x48);
        *(int*)(e + 0x24) = *(int*)(e + 0x4c);
        *(short*)(e + 0x32) = *(short*)(e + 0x16);

        *(int*)(e + 0x44) = x;
        *(int*)(e + 0x48) = y;
        *(int*)(e + 0x4c) = z;
        *(short*)(e + 0x16) = heading;
    }

    /// <summary>
    /// Ghidra: <c>0x00454bb0</c> — <b>la navette qui fait son circuit</b>, sous-type <b>15</b> du
    /// bloc <c>0x005546a8</c>, sous-zones 3, 4 et 9. Absente de <c>Blaze.c</c> ; 155 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00454BB0.asm</c>. Le role a l'ecran n'est pas etabli.
    ///
    /// <para>
    /// <b>Un circuit ferme de huit etapes</b> dans <c>+0x14</c>, cadence par <c>+0x16</c>. Un pas
    /// horizontal vaut <c>0x14000</c> sur X, un pas vertical <c>0xd400</c> sur Y :
    /// </para>
    /// <list type="table">
    ///   <item><term>0</term><description>X+ huit images</description></item>
    ///   <item><term>1</term><description>un tour complet : le cap gagne <c>0x40</c> jusqu'a <c>0x1000</c>, remis a 0</description></item>
    ///   <item><term>2</term><description>X- huit images</description></item>
    ///   <item><term>3</term><description>X- et Y- pendant 286 images — la descente</description></item>
    ///   <item><term>4</term><description>X- huit images</description></item>
    ///   <item><term>5</term><description>le cap gagne <c>0x40</c> jusqu'a <c>0x800</c> — un demi-tour depuis 0</description></item>
    ///   <item><term>6</term><description>X+ huit images</description></item>
    ///   <item><term>7</term><description>X+ et Y+ pendant 286 images — la remontee — puis retour a 0</description></item>
    /// </list>
    /// <para>
    /// Chaque changement d'etape rend la main aussitot, sans executer la suivante dans la meme image.
    /// Un compteur <c>+0x5c</c> sur 32 bits, independant, joue le son <c>0x1b</c> de la zone toutes
    /// les dix images : il est teste a 10 <b>avant</b> l'increment, remis a zero, puis incremente
    /// dans la meme image.
    /// </para>
    /// </summary>
    private static void EntityHandler_00454BB0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
            *(int*)(e + 0x5c) = 0;
        }

        if (*(int*)(e + 0x5c) == 0x0a)
        {
            *(int*)(e + 0x5c) = 0;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1b), 0x400, 0x1000, 0, 0);
        }

        *(int*)(e + 0x5c) += 1;

        if (*(short*)(e + 0x10) != 1) return;

        const int stepX = 0x14000;
        const int stepY = 0xd400;

        switch (*(short*)(e + 0x14))
        {
            case 0:
                *(short*)(e + 0x16) += 1;
                *(int*)(e + 0x1c) += stepX;
                if (*(short*)(e + 0x16) >= 8) NextCircuitStep(e, 1);
                return;

            case 1:
                *(short*)(e + 0x16) += 1;
                *(short*)(e + 0x32) += 0x40;
                if (*(short*)(e + 0x32) >= 0x1000)
                {
                    NextCircuitStep(e, 2);
                    *(short*)(e + 0x32) = 0;
                }
                return;

            case 2:
                *(short*)(e + 0x16) += 1;
                *(int*)(e + 0x1c) -= stepX;
                if (*(short*)(e + 0x16) >= 8) NextCircuitStep(e, 3);
                return;

            case 3:
                *(int*)(e + 0x1c) -= stepX;
                *(short*)(e + 0x16) += 1;
                *(int*)(e + 0x20) -= stepY;
                if (*(short*)(e + 0x16) >= 0x11e) NextCircuitStep(e, 4);
                return;

            case 4:
                *(short*)(e + 0x16) += 1;
                *(int*)(e + 0x1c) -= stepX;
                if (*(short*)(e + 0x16) >= 8) NextCircuitStep(e, 5);
                return;

            case 5:
                *(short*)(e + 0x32) += 0x40;
                *(short*)(e + 0x16) += 1;
                if (*(short*)(e + 0x32) >= 0x800)
                {
                    NextCircuitStep(e, 6);
                    *(short*)(e + 0x32) = 0x800;
                }
                return;

            case 6:
                *(short*)(e + 0x16) += 1;
                *(int*)(e + 0x1c) += stepX;
                if (*(short*)(e + 0x16) >= 8) NextCircuitStep(e, 7);
                return;

            case 7:
                *(short*)(e + 0x16) += 1;
                *(int*)(e + 0x1c) += stepX;
                *(int*)(e + 0x20) += stepY;
                if (*(short*)(e + 0x16) >= 0x11e) NextCircuitStep(e, 0);
                return;
        }
    }

    /// <summary>Le passage a l'etape <paramref name="step"/> du circuit de
    /// <see cref="EntityHandler_00454BB0"/> : le compteur d'images repart de zero.</summary>
    private static void NextCircuitStep(byte* e, short step)
    {
        *(short*)(e + 0x14) = step;
        *(short*)(e + 0x16) = 0;
    }

    // =====================================================================
    // LES EMETTEURS A POSITIONS FIXES
    //
    // Deux corps qui n'agissent pas LA OU ILS SONT : ils se deplacent un instant sur des
    // coordonnees codees en dur, y font naitre l'effet 0x3c, puis reviennent a leur place dans la
    // meme image. La position d'origine est gardee sur la pile et restauree en sortant.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00451250</c> — <b>les trois jets de la salle</b>, sous-type <b>50</b> du bloc
    /// <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 155 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00451250.asm</c>.
    ///
    /// <para>
    /// Un cycle de quinze images sur <c>+0x12</c> (remis a zero quand il atteint 15). A chaque
    /// image, <c>+0x14 = 1</c>, puis trois positions fixes, a la hauteur commune <c>-0x33e000</c> :
    /// a chacune, <b>deux</b> effets <c>0x3c</c> sous deux caps differents. En debut de cycle
    /// (<c>+0x12 == 0</c>), le son de proximite <c>0x13</c> (hauteur <c>0x800</c>, gain
    /// <c>0x1800</c>, portee 3000) part <b>quatre</b> fois : a la place d'origine, puis apres
    /// chacun des trois points.
    /// </para>
    /// </summary>
    private static void EntityHandler_00451250(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 0;
        }

        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) == 0x0f) *(short*)(e + 0x12) = 0;

        if (*(short*)(e + 0x12) == 0) PlayJetSound(e);

        int homeX = *(int*)(e + 0x1c), homeY = *(int*)(e + 0x20), homeZ = *(int*)(e + 0x24);
        short homeHeading = *(short*)(e + 0x32);

        *(short*)(e + 0x14) = 1;
        const int height = -0x33e000;   // 0xffcc2000

        EmitJetPair(e, 0x514000, height, -0xfe7000, 0x8c8, 0x0c8);         // 0xff019000
        if (*(short*)(e + 0x12) == 0) PlayJetSound(e);

        EmitJetPair(e, -0x0e0000, height, -0x1356000, 0x4c8, 0xb38);       // 0xfff20000, 0xfecaa000
        if (*(short*)(e + 0x12) == 0) PlayJetSound(e);

        EmitJetPair(e, -0x424000, height, -0x092e000, 0xa00, 0x200);       // 0xffbdc000, 0xff6d2000
        if (*(short*)(e + 0x12) == 0) PlayJetSound(e);

        *(int*)(e + 0x1c) = homeX;
        *(int*)(e + 0x20) = homeY;
        *(int*)(e + 0x24) = homeZ;
        *(short*)(e + 0x32) = homeHeading;
    }

    /// <summary>Un point de <see cref="EntityHandler_00451250"/> : l'entite est posee sur
    /// <c>(x, y, z)</c>, puis l'effet <c>0x3c</c> nait deux fois, sous deux caps.</summary>
    private static void EmitJetPair(byte* e, int x, int y, int z, short heading1, short heading2)
    {
        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x20) = y;
        *(int*)(e + 0x24) = z;

        *(short*)(e + 0x32) = heading1;
        SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);
        *(short*)(e + 0x32) = heading2;
        SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);
    }

    /// <summary>Le son des jets : le <c>0x13</c> de la zone par <see cref="TryPlayDistanceSound"/>,
    /// a la position courante de l'entite.</summary>
    private static void PlayJetSound(byte* e)
        => TryPlayDistanceSound((uint*)e, ZoneSound(0x13), 0x800, 0x1800, 0xbb8, 0);

    /// <summary>
    /// Ghidra: <c>0x00452bd0</c> — <b>les douze jets</b>, sous-type <b>49</b> du bloc
    /// <c>0x00554028</c>, sous-zones 5 a 8. Absente de <c>Blaze.c</c> ; 138 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00452BD0.asm</c>.
    ///
    /// <para>
    /// Le cousin de <see cref="EntityHandler_00451250"/>, ecrit autrement : les douze points sont
    /// <b>quatre tableaux construits sur la pile</b> a chaque appel (X, Z, Y et cap, en unites
    /// entieres), parcourus par une boucle. Six points a la hauteur <c>-100</c>, six a
    /// <c>-200</c> ; les caps vont par paires. A chaque point, un effet <c>0x3c</c>.
    /// </para>
    /// <para>
    /// Le cycle est de quinze images sur <c>+0x98</c>, incremente <b>avant</b> la boucle. Quand il
    /// vaut 15, le son <c>0x1d</c> de la zone (hauteur <c>0x600</c>, gain <c>0x900</c>) part
    /// <b>a chacun des douze points</b>, puis le compteur revient a zero apres la boucle.
    /// <c>+0x14</c> est remis a zero a chaque image.
    /// </para>
    /// <para>
    /// <b>Le quatrieme cap est <c>0xffffff38</c></b>, soit <c>-200</c> sur 16 bits — la valeur
    /// qui remplit les tableaux voisins, reprise telle quelle. Rien ne dit que c'est voulu ;
    /// c'est ce que le binaire ecrit.
    /// </para>
    /// </summary>
    private static void EntityHandler_00452BD0(byte* e)
    {
        int* pointX = stackalloc int[12] { 0, 0, 0x30c, 0x30c, 0x3e8, 0x3e8, 0xce4, 0xce4, 0xd5c, 0xd5c, 0xdca, 0xdca };
        int* pointZ = stackalloc int[12] { -0x2bc, -0x2bc, -0x2bc, -0x2bc, -0x820, -0x820, -0xcd0, -0xcd0, -0xd48, -0xd48, -0xdba, -0xdba };
        int* pointY = stackalloc int[12] { -100, -100, -100, -100, -100, -100, -200, -200, -200, -200, -200, -200 };
        int* heading = stackalloc int[12] { 0x800, 0, 0x8c8, -200, 0x800, 0, 0x600, 0xe00, 0x600, 0xe00, 0x600, 0xe00 };

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x98) = 0;
        }

        *(short*)(e + 0x98) += 1;

        int homeX = *(int*)(e + 0x1c), homeY = *(int*)(e + 0x20), homeZ = *(int*)(e + 0x24);
        short homeHeading = *(short*)(e + 0x32);
        *(short*)(e + 0x14) = 0;

        for (int i = 0; i < 12; i++)
        {
            *(int*)(e + 0x1c) = pointX[i] << 0xc;
            *(int*)(e + 0x20) = pointY[i] << 0xc;
            *(int*)(e + 0x24) = pointZ[i] << 0xc;
            *(short*)(e + 0x32) = (short)heading[i];

            SpawnEffectNearCamera_00468110(e, 0x3c, 0, -1);

            if (*(short*)(e + 0x98) == 0x0f)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x1d), 0x600, 0x900, 0, 0);
            }
        }

        if (*(short*)(e + 0x98) == 0x0f) *(short*)(e + 0x98) = 0;

        *(int*)(e + 0x1c) = homeX;
        *(int*)(e + 0x20) = homeY;
        *(int*)(e + 0x24) = homeZ;
        *(short*)(e + 0x32) = homeHeading;
    }

    // =====================================================================
    // LE PILON — creneau 11 des blocs 0x005539a8 et 0x00554028
    //
    // Deux corps de meme dessin : une masse qui flotte sur un sinus, puis tombe d'un coup en
    // accelerant jusqu'a -0xc8000, ou elle s'arrete en secouant la camera. L'etat 5 la remonte a
    // 0x190000 et la rend a l'etat 0. Aucun des deux ne fait passer son entite de l'etat 1 a
    // l'etat 2 : c'est le script de la salle qui declenche la chute.
    //
    // Ce qui les separe est la maniere de frapper. Celui des sous-zones 0 a 2 appelle
    // StrikePartyMembers A CHAQUE IMAGE, quel que soit son etat ; celui des sous-zones 5 a 8 ne
    // frappe qu'a l'arrivee, et marque d'abord les membres touches (bit 0x20000000, mot de contact
    // 0x8000) avant de les frapper pour moitie moins.
    // =====================================================================

    /// <summary>
    /// Le flottement du pilon, commun aux deux corps : la hauteur et la vitesse verticale gagnent
    /// toutes deux <c>sin(+0x12) &lt;&lt; 3</c>, puis la phase avance de <c>0x80</c> et reboucle a
    /// <c>0x1000</c>.
    /// <para>
    /// <b>Le binaire appelle le sinus deux fois</b> avec la meme phase, une fois pour la position
    /// et une fois pour la vitesse ; la table ne changeant pas entre les deux, un seul appel dit la
    /// meme chose.
    /// </para>
    /// </summary>
    private static void PestleFloat(byte* e)
    {
        int lift = (int)Winsys3d.GetSinTableEntry(*(short*)(e + 0x12)) << 3;   // Ghidra: Ordinal_451

        *(int*)(e + 0x20) += lift;
        AddEntityVelocity_00454580(e, 0, lift, 0);

        *(short*)(e + 0x12) += 0x80;
        if (*(short*)(e + 0x12) >= 0x1000) *(short*)(e + 0x12) = 0;
    }

    /// <summary>
    /// La remontee du pilon, etat 5 des deux corps : <c>0xa000</c> par image jusqu'a
    /// <c>0x190000</c>, ou l'etat revient a 0.
    /// </summary>
    private static void PestleRise(byte* e)
    {
        *(int*)(e + 0x20) += 0xa000;
        if (*(int*)(e + 0x20) < 0x190000) return;

        *(int*)(e + 0x20) = 0x190000;
        *(short*)(e + 0x10) = 0;
    }

    /// <summary>
    /// Ghidra: FUN_00452790 @ 0x00452790 — <b>le pilon des sous-zones 0 a 2</b>, sous-type
    /// <b>11</b> du bloc <c>0x005539a8</c>. Decompilee dans <c>Blaze.c</c> ; la pseudo-C perd un
    /// argument de <c>FUN_00454580</c>, que le desassemblage rend (quatre <c>push</c>,
    /// <c>add esp,10h</c>).
    ///
    /// <para>
    /// <b>Il frappe a chaque image</b> : <see cref="StrikePartyMembers_0045DA40"/> pour 10 pour
    /// cent, en tete du corps et sans condition d'etat. Naissance : etat 1, vitesse de chute
    /// <c>+0x14 = 5</c> et phase tiree sur <c>(rand % 31) &lt;&lt; 7</c>.
    /// </para>
    /// <para>
    /// <b>Etat 2, la chute</b> : la hauteur perd <c>+0x14 &lt;&lt; 12</c> et la vitesse gagne 2 —
    /// la vitesse d'avant l'increment est celle qui sert. A <c>-0xc8000</c> ou plus bas : etat 3,
    /// hauteur callee, bit <c>0x40000000</c>, son <c>0x23</c> de la zone, et une secousse de
    /// camera de <c>0x28</c> par <see cref="TryPublishCameraEffectNearEntity"/>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00452790(byte* e)
    {
        StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 5;
            *(short*)(e + 0x12) = (short)((CrtRand() % 0x1f) << 7);
        }

        if (*(short*)(e + 0x10) == 1) PestleFloat(e);

        if (*(short*)(e + 0x10) == 2)
        {
            short fall = *(short*)(e + 0x14);
            *(int*)(e + 0x20) -= fall << 0xc;
            *(short*)(e + 0x14) = (short)(fall + 2);

            if (*(int*)(e + 0x20) <= -0xc8000)
            {
                *(short*)(e + 0x10) = 3;
                *(int*)(e + 0x20) = -0xc8000;
                *(uint*)e |= 0x40000000;

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x23), 0x800, 0x1000, 0, 0);
                TryPublishCameraEffectNearEntity((uint*)e, 999, 0x28, 999, 0, 0);
            }
        }

        if (*(short*)(e + 0x10) == 5) PestleRise(e);
    }

    /// <summary>
    /// Ghidra: <c>0x00453f60</c> — <b>le pilon des sous-zones 5 a 8</b>, sous-type <b>11</b> du
    /// bloc <c>0x00554028</c>. Absente de <c>Blaze.c</c> ; 133 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00453F60.asm</c>.
    ///
    /// <para>
    /// Le cousin de <see cref="EntityHandler_00452790"/> : meme flottement, meme remontee, meme
    /// palier de chute. Trois differences, toutes a la chute. La vitesse part de <b>10</b> et
    /// gagne <b>3</b> par image au lieu de 5 et 2 — il tombe plus vite. L'arrivee remet en plus le
    /// compteur <c>+0x9a</c> a zero.
    /// </para>
    /// <para>
    /// <b>Et il ne frappe qu'a l'arrivee.</b> Les quatre bits bas de <c>+0x09</c> — le meme demi-
    /// octet de contact que lit <see cref="StrikePartyMembers_0045DA40"/> — designent les membres
    /// sous la masse : chacun recoit le bit <c>0x20000000</c> de son mot d'etat et le bit
    /// <c>0x8000</c> de son mot de contact <c>+0x90</c>, <b>avant</b> l'appel qui leur enleve
    /// <b>5</b> pour cent. Le marquage precede la frappe et survit donc a la remise a zero de
    /// <c>+0x09</c> que la frappe fait en sortant.
    /// </para>
    /// <para>
    /// <b>L'etat 4 n'est atteint par aucune ligne de ce corps</b> : il fait monter <c>+0x9a</c> et
    /// efface le bit <c>0x2000</c> des qu'il depasse zero. C'est le script de la salle qui l'y met.
    /// </para>
    /// </summary>
    private static void EntityHandler_00453F60(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x14) = 0x0a;
            *(short*)(e + 0x12) = (short)((CrtRand() % 0x1f) << 7);
        }

        if (*(short*)(e + 0x10) == 1) PestleFloat(e);

        if (*(short*)(e + 0x10) == 2)
        {
            short fall = *(short*)(e + 0x14);
            *(int*)(e + 0x20) -= fall << 0xc;
            *(short*)(e + 0x14) = (short)(fall + 3);

            if (*(int*)(e + 0x20) <= -0xc8000)
            {
                *(int*)(e + 0x20) = -0xc8000;
                *(uint*)e |= 0x40000000;
                *(short*)(e + 0x10) = 3;
                *(short*)(e + 0x9a) = 0;

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x23), 0x800, 0x1000, 0, 0);
                TryPublishCameraEffectNearEntity((uint*)e, 999, 0x28, 999, 0, 0);

                byte contact = (byte)(e[9] & 0x0f);
                byte* member = Inp.s_SlotFlags_005e93c0;

                for (int i = 0; i < 4; i++, member += 0x9c)
                {
                    uint flags = *(uint*)member;
                    if ((flags & 0x80000000) == 0) continue;
                    if ((flags & 0x50000000) != 0) continue;
                    if (((contact >> i) & 1) == 0) continue;

                    *(uint*)member = flags | 0x20000000;
                    *(uint*)(member + 0x90) |= 0x8000;
                }

                StrikePartyMembers_0045DA40(e, 5, 0, 0, 0);
            }
        }

        if (*(short*)(e + 0x10) == 4)
        {
            *(short*)(e + 0x9a) += 1;
            if (*(short*)(e + 0x9a) > 0) *(uint*)e &= 0xffffdfff;
        }

        if (*(short*)(e + 0x10) == 5) PestleRise(e);
    }

    // =====================================================================
    // LES DEUX MASSES QUI S'ECRASENT — creneaux 18 et 19
    //
    // 0x00452410 (sous-zones 0 a 2) et 0x00453650 (sous-zones 5 a 8) partagent une naissance mot
    // pour mot : quatre demi-cotes a 0xa000, plancher fige, pose sur le terrain, et une entite
    // fille qui nait par SpawnStatusEffectEntity_0047AC40 — sous-type 0xf0 pour l'une, 0xef pour
    // l'autre. Leurs chutes, elles, n'ont rien a voir.
    // =====================================================================

    /// <summary>
    /// La naissance commune aux deux masses : les quatre demi-cotes de collision, le plancher
    /// <c>+0x60</c>/<c>+0x64</c> fige sur la hauteur de pose, la pose sur le terrain par
    /// <see cref="TryPlaceEntity"/> puis <see cref="FinalizeEntityPlacement2"/>, et un delai
    /// <c>+0x12</c> de dix images. La fille, elle, nait chez chaque appelant : les deux sous-types
    /// different.
    /// </summary>
    private static void CrusherBirth(byte* e)
    {
        *(ushort*)(e + 0x82) = 0xa000;
        *(ushort*)(e + 0x86) = 0xa000;
        *(ushort*)(e + 0x8a) = 0xa000;
        *(ushort*)(e + 0x8e) = 0xa000;

        *(int*)(e + 0x64) = *(int*)(e + 0x20);
        *(int*)(e + 0x60) = *(int*)(e + 0x20);

        TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);   // Ghidra: FUN_005330d0
        FinalizeEntityPlacement2((uint*)e);                                  // Ghidra: FUN_00533940

        *(short*)(e + 0x12) = 10;
    }

    /// <summary>
    /// Ghidra: FUN_00452410 @ 0x00452410 — <b>la masse que l'approche declenche</b>, sous-type
    /// <b>18</b> du bloc <c>0x005539a8</c>, sous-zones 0 a 2. Decompilee dans <c>Blaze.c</c>.
    ///
    /// <para>
    /// <b>Naissance</b> : <see cref="CrusherBirth"/> avec la fille <c>0xf0</c>, les trois echelles
    /// a <c>0x2000</c>, et la hauteur decalee de <c>(rand % 400 - 200) &lt;&lt; 12</c> — deux
    /// masses de la meme salle ne pendent pas a la meme hauteur.
    /// </para>
    /// <para>
    /// <b>Etat 1, l'attente</b> : elle se rend visible (bit <c>0x4000</c>) et mesure la distance
    /// horizontale a chacun des quatre emplacements actifs du groupe. <b>Sous 600 unites</b>, elle
    /// passe a l'etat 2, remet son compteur a zero et redevient invisible.
    /// </para>
    /// <para>
    /// <b>Etat 2, la chute</b> : la vitesse <c>+0x12</c> gagne 4 par image et la hauteur en perd
    /// autant, jusqu'au plancher <c>+0x64</c>. Alors : etat 3, hauteur calee a
    /// <c>+0x64 - 0xa000</c>, secousse <c>+0x14 = 10</c>, son <c>0x11</c> de la zone, une frappe a
    /// 10 pour cent, et <c>rand % 5 + 11</c> effets <c>0x38</c> pres de la camera.
    /// </para>
    /// <para>
    /// <b>Etat 3, la secousse</b> : la camera recoit <c>+0x14</c> par
    /// <see cref="TryPublishCameraEffectNearEntity"/>, puis <c>+0x14</c> <b>change de signe</b> et
    /// se rapproche de zero d'un cran ; la masse clignote au rythme de ce signe. A zero, elle
    /// repart a l'etat 1 en remontant de <c>(rand % 300 + 800) &lt;&lt; 12</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00452410(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;

            CrusherBirth(e);

            *(short*)(e + 0x28) = 0x2000;
            *(short*)(e + 0x2a) = 0x2000;
            *(short*)(e + 0x2c) = 0x2000;

            *(int*)(e + 0x20) += (CrtRand() % 400 - 200) * 0x1000;
            *(uint*)e &= 0xffffbfff;

            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf0, 0, -1);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(uint*)e |= 0x4000;

            byte* member = Inp.s_SlotFlags_005e93c0;
            for (int i = 0; i < 4; i++, member += 0x9c)
            {
                uint flags = *(uint*)member;
                if ((flags & 0x80000000) == 0) continue;
                if ((flags & 0x50000000) != 0) continue;

                int dx = (*(int*)(e + 0x1c) >> 0xc) - (*(int*)(member + 0x1c) >> 0xc);
                int dz = (*(int*)(e + 0x24) >> 0xc) - (*(int*)(member + 0x24) >> 0xc);

                if ((int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) >= 600) continue;

                *(short*)(e + 0x10) = 2;
                *(short*)(e + 0x12) = 0;
                *(uint*)e &= 0xffffbfff;
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 4;
            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;

            if (*(int*)(e + 0x20) < *(int*)(e + 0x64))
            {
                *(short*)(e + 0x10) = 3;
                *(int*)(e + 0x20) = *(int*)(e + 0x64) - 0xa000;
                *(short*)(e + 0x14) = 10;

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x11), 0x800, 0x1000, 0, 0);
                StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);

                short burst = (short)(CrtRand() % 5 + 10);
                for (int i = burst + 1; i > 0; i--)
                {
                    SpawnEffectNearCamera_00468110(e, 0x38, 0, -1);
                }
            }
        }

        if (*(short*)(e + 0x10) == 3)
        {
            TryPublishCameraEffectNearEntity((uint*)e, 999, *(short*)(e + 0x14), 999, 0, 0);

            short shake = (short)-*(short*)(e + 0x14);
            *(short*)(e + 0x14) = shake;

            if (shake < 1)
            {
                *(short*)(e + 0x14) = (short)(shake + 1);
                *(uint*)e &= 0xffffbfff;
            }
            else
            {
                *(short*)(e + 0x14) = (short)(shake - 1);
                *(uint*)e |= 0x4000;
            }

            if (*(short*)(e + 0x14) == 0)
            {
                *(short*)(e + 0x10) = 1;
                *(int*)(e + 0x20) += (CrtRand() % 300 + 800) * 0x1000;
                *(uint*)e &= 0xffffbfff;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_00453650 @ 0x00453650 — <b>la masse qui rebondit et revient se pendre</b>,
    /// sous-type <b>19</b> du bloc <c>0x00554028</c>, sous-zones 5 a 8. Decompilee dans
    /// <c>Blaze.c</c> ; le desassemblage tranche une constante que Ghidra rend comme un symbole
    /// de fonction (voir plus bas).
    ///
    /// <para>
    /// <b>Naissance</b> : elle retient sa pose entiere en <c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>,
    /// puis <see cref="CrusherBirth"/> avec la fille <c>0xef</c>.
    /// </para>
    /// <para>
    /// <b>Etat 1, la chute</b> : la vitesse <c>+0x12</c> gagne 2 par image, la hauteur en perd
    /// autant, et elle frappe le groupe a 10 pour cent <b>a chaque image de descente</b>. Au
    /// plancher <c>+0x60</c> : etat 2, son <c>0x20</c> de la zone, bit <c>0x2000</c>,
    /// <c>rand % 3 + 2</c> effets <c>0x34</c>, et <b>quatre tirages</b> qui composent le rebond —
    /// un pas en x (<c>+0x14</c>), un pas en z (<c>+0x16</c>), une rotation de cap (<c>+0x12</c>)
    /// et une rotation d'assiette (<c>+0x5c</c>), les deux premiers sur <c>±10</c> et les deux
    /// autres sur <c>±40</c>.
    /// </para>
    /// <para>
    /// <b>Etats 2 a 20, le vol</b> : chaque image la fait monter de <c>0x5000</c>, avancer de son
    /// pas, tourner de ses deux rotations, <b>et changer d'etat</b> — l'etat sert de compteur. Le
    /// bit <c>0x4000</c> suit la parite de l'etat, donc elle clignote. Un tirage est consomme et
    /// <b>jete</b> a chaque image. Passe l'etat 20 : etat 100 et la hauteur saute de
    /// <c>0x514000</c>.
    /// </para>
    /// <para>
    /// <b>Ghidra rend ce <c>0x514000</c> comme <c>FUN_00514000</c></b> — l'immediat tombe sur
    /// l'adresse d'une fonction et le decompilateur y a vu un symbole. Le desassemblage donne
    /// <c>add eax,514000h</c> : c'est un nombre.
    /// </para>
    /// <para>
    /// <b>Etat 100 puis 0x65</b> : elle revient exactement a sa pose, cap et assiette a zero, est
    /// reposee sur le terrain, et repart a l'etat 1 sans le bit <c>0x2000</c>.
    /// </para>
    /// </summary>
    private static void EntityHandler_00453650(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x44) = *(int*)(e + 0x1c);
            *(int*)(e + 0x4c) = *(int*)(e + 0x24);
            *(int*)(e + 0x48) = *(int*)(e + 0x20);

            *(short*)(e + 0x10) = 1;

            CrusherBirth(e);

            *(uint*)e &= 0xffffbfff;
            SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xef, 0, -1);
        }

        if (*(short*)(e + 0x10) == 0x65)
        {
            *(uint*)e &= 0xffffbfff;

            *(int*)(e + 0x1c) = *(int*)(e + 0x44);
            *(int*)(e + 0x20) = *(int*)(e + 0x48);
            *(int*)(e + 0x24) = *(int*)(e + 0x4c);

            *(short*)(e + 0x30) = 0;
            *(short*)(e + 0x32) = 0;
            *(short*)(e + 0x34) = 0;
            *(short*)(e + 0x10) = 1;

            FinalizeEntityPlacement2((uint*)e);

            *(short*)(e + 0x12) = 10;
            *(uint*)e &= 0xffffdfff;
        }

        if (*(short*)(e + 0x10) == 1)
        {
            *(short*)(e + 0x12) += 2;
            *(int*)(e + 0x20) += *(short*)(e + 0x12) * -0x1000;

            StrikePartyMembers_0045DA40(e, 10, 0, 0, 0);

            if (*(int*)(e + 0x20) < *(int*)(e + 0x60))
            {
                *(int*)(e + 0x20) = *(int*)(e + 0x60);
                *(short*)(e + 0x10) = 2;

                *(short*)(e + 0x14) = (short)(CrtRand() % 0x14 - 10);
                *(short*)(e + 0x16) = (short)(CrtRand() % 0x14 - 10);
                *(short*)(e + 0x12) = (short)(CrtRand() % 0x50 - 0x28);
                *(int*)(e + 0x5c) = CrtRand() % 0x50 - 0x28;

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x800, 0x1000, 0, 0);

                short burst = (short)(CrtRand() % 3 + 1);
                *(uint*)e |= 0x2000;

                for (int i = burst + 1; i > 0; i--)
                {
                    SpawnEffectNearCamera_00468110(e, 0x34, 0, -1);
                }
            }
        }

        short state = *(short*)(e + 0x10);
        if (state > 1 && state != 100)
        {
            *(int*)(e + 0x20) += 0x5000;
            *(short*)(e + 0x10) = (short)(state + 1);

            *(int*)(e + 0x1c) += *(short*)(e + 0x14) * 0x1000;
            *(short*)(e + 0x34) += (short)*(int*)(e + 0x5c);
            *(int*)(e + 0x24) += *(short*)(e + 0x16) * 0x1000;
            *(short*)(e + 0x32) += *(short*)(e + 0x12);

            if (((state + 1) & 1) == 0) *(uint*)e |= 0x4000;
            else *(uint*)e &= 0xffffbfff;

            CrtRand();   // Ghidra: FUN_00547140 — le tirage est jete

            if (*(short*)(e + 0x10) > 0x14)
            {
                *(short*)(e + 0x10) = 100;
                *(int*)(e + 0x20) += 0x514000;
            }
        }

        if (*(short*)(e + 0x10) == 100) *(short*)(e + 0x10) = 0x65;
    }

    /// <summary>
    /// Ghidra: <c>0x004518d0</c> — <b>la seconde chute de pierres</b>, sous-type <b>29</b> du bloc
    /// <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 88 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_004518D0.asm</c>.
    ///
    /// <para>
    /// La jumelle de <see cref="EntityHandler_00451A00"/>, au meme signal — le bit <c>0x10</c> du
    /// premier octet de la banque 3 — et avec la meme bande sonore a mi-chute. Trois differences :
    /// </para>
    /// <list type="bullet">
    ///   <item>elle <b>tombe tout de suite</b>, sans l'etat 2 d'attente de dix images ;</item>
    ///   <item>ses deux sons echangent leurs hauteurs : <c>0x2b</c> a <c>0x200</c> au depart,
    ///     <c>0x2c</c> a <c>0x300</c> a mi-chute ;</item>
    ///   <item>sa rotation n'est pas la constante <c>0x0f</c> mais un tirage, <c>-10 - rand % 30</c>,
    ///     fait une fois a la naissance et applique a <c>+0x34</c> a chaque image — elle tourne
    ///     donc <b>dans l'autre sens</b>, et pas deux fois pareil.</item>
    /// </list>
    /// </summary>
    private static void EntityHandler_004518D0(byte* e)
    {
        if ((Mem.s_StateBank3_005e9840[0] & 0x10) == 0 && *(short*)(e + 0x10) == 0) return;

        if (*(short*)(e + 0x10) == 0)
        {
            *(uint*)e |= 0x2000;
            TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2b), 0x200, 0x1000, 0, 0);

            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 3;
            *(short*)(e + 0x98) = 0;
            *(short*)(e + 0x14) = (short)(-10 - CrtRand() % 0x1e);
        }

        if (*(short*)(e + 0x10) != 1) return;

        short step = *(short*)(e + 0x12);
        *(int*)(e + 0x20) -= step << 0xc;
        step++;
        *(short*)(e + 0x12) = step;
        if (step > 0x0a) *(short*)(e + 0x12) = 0x0a;

        int height = *(int*)(e + 0x20);
        *(short*)(e + 0x34) += *(short*)(e + 0x14);

        if (height < -0x2bc000 && height > -0x320000)
        {
            if (*(short*)(e + 0x98) == 0)
            {
                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2c), 0x300, 0x1000, 0, 0);
                *(short*)(e + 0x98) = 1;
            }

            for (int i = 0; i < 4; i++) SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);
        }

        if (*(int*)(e + 0x20) < -0x4b0000) *(uint*)e &= 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: <c>0x00454610</c> — <b>les cinq jets des sous-zones 3, 4 et 9</b>, sous-type
    /// <b>51</b> du bloc <c>0x005546a8</c>. Absente de <c>Blaze.c</c> ; 153 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00454610.asm</c>.
    ///
    /// <para>
    /// La troisieme de la famille des emetteurs a positions fixes, avec
    /// <see cref="EntityHandler_00451250"/> et <see cref="EntityHandler_00452BD0"/> : elle se pose
    /// un instant sur chacun de <b>cinq</b> points codes en dur, tous a la hauteur
    /// <c>-0xc8000</c>, y fait naitre l'effet <c>0x3c</c> sous <b>deux</b> caps, et revient chez
    /// elle dans la meme image. Dix effets par image en tout.
    /// </para>
    /// <para>
    /// <b>Son cycle est de vingt images et son son n'est pas positionnel</b> : c'est
    /// <c>Aud.PlaySoundEffect</c> directement, sans attenuation ni voie — le son <c>0x1d</c> de la
    /// zone, hauteur <c>0x600</c>, meme gain <c>0x800</c> des deux cotes. La ou ses deux cousines
    /// sonnent a chaque point, celle-ci ne sonne <b>qu'une fois</b>, avant la serie.
    /// </para>
    /// <para>Le dernier cap, <c>0xff9c</c>, vaut <b>-100</b> sur seize bits.</para>
    /// </summary>
    private static void EntityHandler_00454610(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x98) = 0;
        }

        *(short*)(e + 0x98) += 1;
        if (*(short*)(e + 0x98) == 0x14)
        {
            Aud.PlaySoundEffect(ZoneSound(0x1d), 0x600, 0x800, 0x800);   // Ghidra: FUN_0053cea0
            *(short*)(e + 0x98) = 0;
        }

        int homeX = *(int*)(e + 0x1c), homeY = *(int*)(e + 0x20), homeZ = *(int*)(e + 0x24);
        short homeHeading = *(short*)(e + 0x32);

        *(short*)(e + 0x14) = 1;
        const int height = -0xc8000;   // 0xfff38000

        EmitJetPair(e, 0x1f7000, height, -0x02f000, 0x0a44, 0x0244);   // 0xfffd1000
        EmitJetPair(e, 0x443000, height, 0x0ae000, 0x04c8, 0x0cc8);
        EmitJetPair(e, 0x627000, height, -0x1f6000, 0x08c8, 0x00c8);   // 0xffe0a000
        EmitJetPair(e, 0x63a000, height, -0x456000, 0x0800, 0x0000);   // 0xffbaa000
        EmitJetPair(e, 0x198000, height, -0x516000, 0x079c, -100);     // 0xffaea000, 0xff9c

        *(int*)(e + 0x1c) = homeX;
        *(int*)(e + 0x20) = homeY;
        *(int*)(e + 0x24) = homeZ;
        *(short*)(e + 0x32) = homeHeading;
    }

    // =====================================================================
    // L'EFFONDREMENT — creneau 31 du bloc 0x005539a8, le dernier de la table
    //
    // 1354 octets, le plus gros corps de la table 0x0056adc8 de la mine, et son seul a huit etats
    // enchaines. La masse tombe, s'ecrase, puis PART EN COURBE : quatre de ses etats deplacent
    // l'entite en coordonnees polaires — cap +0x14, rayon +0x16 divise par deux — en faisant
    // varier le cap, le rayon, ou les deux. C'est ce qui lui donne sa trajectoire de debris lance.
    //
    // En plus de sa propre chute, elle surveille A CHAQUE IMAGE le membre du groupe que designe
    // l'octet 0x005e9062, et lance le script 3 sur lui des qu'il passe sous -0x384000 — le fond du
    // gouffre. C'est la seule chose de ce corps qui ne concerne pas la masse elle-meme.
    // =====================================================================

    /// <summary>
    /// Un pas en coordonnees polaires, partage par les etats 3, 4, 6 et 7 de
    /// <see cref="EntityHandler_00451B40"/> : la position avance de
    /// <c>(sin, cos)</c> du cap <c>+0x14</c>, multiplies par <b>la moitie</b> du rayon
    /// <c>+0x16</c>. Le binaire relit <c>+0x16</c> entre les deux multiplications ; rien ne
    /// l'ecrit entre-temps.
    /// </summary>
    private static void CollapseArcStep(byte* e)
    {
        short heading = *(short*)(e + 0x14);
        int half = *(short*)(e + 0x16) >> 1;

        *(int*)(e + 0x1c) += (int)Winsys3d.GetSinTableEntry(heading) * half;   // Ordinal_451
        *(int*)(e + 0x24) += Winsys3d.GetCosTableEntry(heading) * half;        // Ordinal_450
    }

    /// <summary>
    /// La surveillance du membre tombe. L'octet <c>0x005e9062</c> — dans la banque d'etat 2 —
    /// designe un emplacement du groupe ; des que sa hauteur passe sous <c>-0x384000</c>, le
    /// script <b>3</b> part sur lui.
    ///
    /// <para>
    /// <b>Mais pas forcement sur lui.</b> Trois candidats sont essayes dans l'ordre : l'emplacement
    /// designe s'il est actif, sinon le meneur <c>0x005de538</c> s'il l'est, sinon <b>le plus petit
    /// emplacement actif</b> — la boucle de repli descend de 3 a 0 et garde la derniere trouvaille,
    /// donc la plus basse. Aucun actif, et rien ne part.
    /// </para>
    /// <para>L'etat <c>0xff</c> desarme la surveillance.</para>
    /// </summary>
    private static void WatchFallenMember(byte* e)
    {
        byte* slots = Inp.s_SlotFlags_005e93c0;
        int watched = Mem.Arena(0x005e9062)[0];

        if (*(int*)(slots + watched * 0x9c + 0x20) >= -0x384000) return;   // 0xffc7c000
        if (*(short*)(e + 0x10) == 0xff) return;

        byte* member = slots + watched * 0x9c;
        if (PartySlotIsActive(member))
        {
            RunEntityScript(s_Unknown_005e97cc, member, 3);
            return;
        }

        byte* leader = slots + (byte)Mem.s_Unknown_005de538 * 0x9c;
        if (PartySlotIsActive(leader))
        {
            RunEntityScript(s_Unknown_005e97cc, leader, 3);
            return;
        }

        // Ghidra: la boucle descend de 3 a 0 et ecrase son candidat a chaque trouvaille ; elle
        // rend donc le PLUS PETIT emplacement actif, pas le premier rencontre. 0x63 est la
        // sentinelle « aucun ».
        int found = 0x63;
        for (int i = 3; i >= 0; i--)
        {
            if (PartySlotIsActive(slots + i * 0x9c)) found = i;
        }

        if (found == 0x63) return;
        RunEntityScript(s_Unknown_005e97cc, slots + found * 0x9c, 3);
    }

    /// <summary>Un emplacement de groupe est actif quand son mot de tete porte le bit de signe et
    /// aucun bit de <c>0x50000000</c> — la lecture que font <see cref="WatchFallenMember"/>,
    /// <see cref="PartyMembersInBox"/> et <see cref="ActivePartySlotCount_00466D70"/>.</summary>
    private static bool PartySlotIsActive(byte* member)
    {
        uint flags = *(uint*)member;
        return (flags & 0x80000000) != 0 && (flags & 0x50000000) == 0;
    }

    /// <summary>
    /// Ghidra: <c>0x00451b40</c> — <b>l'effondrement</b>, sous-type <b>31</b> du bloc
    /// <c>0x005539a8</c>, sous-zones 0 a 2. Absente de <c>Blaze.c</c> ; 1354 octets,
    /// <c>Legacy/Raw/Blaze/FUN_00451B40.asm</c>. <b>Le dernier creneau de la table
    /// <c>0x0056adc8</c> de la mine.</b>
    ///
    /// <para>
    /// Comme les deux chutes de pierres (<see cref="EntityHandler_00451A00"/>,
    /// <see cref="EntityHandler_004518D0"/>), elle dort tant que le bit <c>0x10</c> du premier
    /// octet de la banque 3 n'est pas pose. Elle naît alors a l'etat 2. <b>Pendant les etats 3 a
    /// 7, deux effets <c>0x33</c> naissent par image</b>, sans condition : c'est la trainee.
    /// </para>
    /// <list type="table">
    ///   <item><term>2</term><description>vingt images d'attente, puis l'etat 1 avec le cap de
    ///     depart <c>0x514</c>, rayon et compteurs a zero</description></item>
    ///   <item><term>1</term><description><b>la chute</b> : la hauteur perd <c>+0x12 &lt;&lt; 12</c>
    ///     par image (vitesse plafonnee a 10) et l'assiette <c>+0x34</c> perd 10. Au plancher
    ///     <c>-0x2ee000</c> : le son <c>0x2c</c> une seule fois, onze effets <c>0x32</c>, etat 3,
    ///     et — si le bit <c>0x20</c> de son mot d'etat est pose — le script <b>0</b> sur le membre
    ///     surveille</description></item>
    ///   <item><term>3</term><description>le cap <b>recule de 10 par image</b> et le rayon monte
    ///     d'un jusqu'a 20 : la courbe se resserre en accelerant. Le lacet <c>+0x32</c> tourne de
    ///     la moitie de <c>+0x44</c>, qui monte lui aussi jusqu'a 20, et l'assiette remonte de 5
    ///     par image, plafonnee a zero. Cap sous <c>-200</c> : etat 4, avec <c>+0x5c = -10</c>
    ///     </description></item>
    ///   <item><term>4</term><description>le cap n'avance plus d'un pas fixe mais de <c>+0x5c</c>,
    ///     qui gagne 9 par image jusqu'a 20 — <b>la courbe s'inverse puis repart</b>. Passe
    ///     l'image 170 le pas perd 6 par image ; a l'image 245, etat 5. Le lacet tourne toujours,
    ///     mais <c>+0x44</c> <b>redescend</b> jusqu'a -10</description></item>
    ///   <item><term>5</term><description>soixante images en ligne droite, <c>0x2000</c> par image
    ///     sur x et sur z a la fois, lacet de 8 ; puis l'etat 6, cap remis a <c>0x2bc</c>
    ///     </description></item>
    ///   <item><term>6</term><description>cap fige, <b>le rayon seul grandit</b> jusqu'a 40 :
    ///     l'arc s'ouvre. A l'image 285 (<c>0x11d</c>), etat 7</description></item>
    ///   <item><term>7</term><description><b>la retombee</b> : le rayon <b>perd deux par image</b>
    ///     avec un plancher a 5, l'assiette pique de 20, et la hauteur retombe en accelerant.
    ///     Sous <c>-0x578000</c>, etat 8</description></item>
    ///   <item><term>8</term><description>les bits <c>0x6000</c> poses, et au bout de soixante
    ///     images le bit de signe efface — elle disparait</description></item>
    /// </list>
    /// <para>
    /// <b>Le compteur <c>+0x9a</c> court a travers les etats 3, 4, 6 et 7 sans jamais etre remis a
    /// zero entre eux</b> : c'est lui, et non les etats, qui donne les deux echeances de 245 et de
    /// 285 images. L'etat 2 est le seul a le remettre a zero.
    /// </para>
    /// </summary>
    private static void EntityHandler_00451B40(byte* e)
    {
        if ((Mem.s_StateBank3_005e9840[0] & 0x10) == 0 && *(short*)(e + 0x10) == 0) return;

        short entry = *(short*)(e + 0x10);
        if (entry >= 3 && entry <= 7)
        {
            for (int i = 0; i < 2; i++) SpawnEffectNearCamera_00468110(e, 0x33, 0, -1);
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x10) = 2;
            *(short*)(e + 0x12) = 3;
            *(short*)(e + 0x98) = 0;
        }

        if ((Mem.s_StateBank3_005e9840[0] & 0x10) != 0) WatchFallenMember(e);

        // --- Etat 1 : la chute -------------------------------------------------------------
        if (*(short*)(e + 0x10) == 1)
        {
            short speed = *(short*)(e + 0x12);
            *(int*)(e + 0x20) -= speed << 0xc;
            speed++;
            *(short*)(e + 0x12) = speed;
            if (speed > 0x0a) *(short*)(e + 0x12) = 0x0a;

            *(short*)(e + 0x34) -= 10;

            const int floor = -0x2ee000;   // 0xffd12000

            if (*(int*)(e + 0x20) < floor)
            {
                if (*(short*)(e + 0x98) == 0)
                {
                    TryPlaySoundNearEntity((uint*)e, ZoneSound(0x2c), 0x300, 0x1000, 0, 0);
                    *(short*)(e + 0x98) = 1;
                }

                *(int*)(e + 0x20) = floor;
                for (int i = 0; i < 0x0b; i++) SpawnEffectNearCamera_00468110(e, 0x32, 0, -1);

                bool announced = (e[0] & 0x20) != 0;
                *(short*)(e + 0x10) = 3;

                if (announced && (Mem.s_StateBank3_005e9840[0] & 0x10) != 0)
                {
                    byte* watched = Inp.s_SlotFlags_005e93c0 + Mem.Arena(0x005e9062)[0] * 0x9c;
                    RunEntityScript(s_Unknown_005e97cc, watched, 0);
                }
            }
        }

        // --- Etat 2 : l'attente ------------------------------------------------------------
        if (*(short*)(e + 0x10) == 2)
        {
            *(short*)(e + 0x12) += 1;
            if (*(short*)(e + 0x12) == 0x14)
            {
                *(short*)(e + 0x10) = 1;
                *(short*)(e + 0x12) = 3;
                *(short*)(e + 0x14) = 0x514;
                *(short*)(e + 0x16) = 0;
                *(short*)(e + 0x9a) = 0;
                *(int*)(e + 0x44) = 0;
            }
        }

        // --- Etat 3 : la courbe qui se resserre ---------------------------------------------
        if (*(short*)(e + 0x10) == 3)
        {
            *(short*)(e + 0x9a) += 1;

            short radius = *(short*)(e + 0x16);
            CollapseArcStep(e);

            *(short*)(e + 0x14) -= 10;
            *(short*)(e + 0x16) = (short)(radius + 1);

            if (*(short*)(e + 0x14) < -200)
            {
                *(short*)(e + 0x10) = 4;
                *(int*)(e + 0x5c) = -10;
                return;
            }

            if (radius + 1 > 0x14) *(short*)(e + 0x16) = 0x14;

            int spin = *(int*)(e + 0x44);
            *(short*)(e + 0x32) += (short)(spin >> 1);
            spin++;
            *(int*)(e + 0x44) = spin;
            if (spin > 0x14) *(int*)(e + 0x44) = 0x14;

            *(short*)(e + 0x34) += 5;
            if (*(short*)(e + 0x34) > 0) *(short*)(e + 0x34) = 0;
        }

        // --- Etat 4 : la courbe qui s'inverse -----------------------------------------------
        if (*(short*)(e + 0x10) == 4)
        {
            *(short*)(e + 0x9a) += 1;

            short radius = *(short*)(e + 0x16);
            CollapseArcStep(e);

            *(short*)(e + 0x14) += (short)*(int*)(e + 0x5c);

            int pace = *(int*)(e + 0x5c) + 9;
            *(short*)(e + 0x16) = (short)(radius + 1);
            *(int*)(e + 0x5c) = pace;
            if (pace > 0x14) *(int*)(e + 0x5c) = 0x14;

            short age = *(short*)(e + 0x9a);
            if (age > 0xaa) *(int*)(e + 0x5c) -= 6;

            if (age >= 0xf5)
            {
                *(short*)(e + 0x10) = 5;
                *(short*)(e + 0x12) = 0;
                return;
            }

            if (radius + 1 > 0x14) *(short*)(e + 0x16) = 0x14;

            int spin = *(int*)(e + 0x44);
            *(short*)(e + 0x32) += (short)(spin >> 1);
            spin--;
            *(int*)(e + 0x44) = spin;
            if (spin < -10) *(int*)(e + 0x44) = -10;
        }

        // --- Etat 5 : la ligne droite --------------------------------------------------------
        short state = *(short*)(e + 0x10);

        if (state == 5)
        {
            *(short*)(e + 0x12) += 1;
            *(short*)(e + 0x32) += 8;
            *(int*)(e + 0x1c) += 0x2000;
            *(int*)(e + 0x24) += 0x2000;

            if (*(short*)(e + 0x12) > 0x3c)
            {
                *(short*)(e + 0x10) = 6;
                *(short*)(e + 0x14) = 0x2bc;
                *(short*)(e + 0x16) = 0;
                return;
            }
        }

        // --- Etat 6 : l'arc qui s'ouvre ------------------------------------------------------
        if (state == 6)
        {
            *(short*)(e + 0x9a) += 1;
            *(short*)(e + 0x32) += 8;

            short radius = *(short*)(e + 0x16);
            CollapseArcStep(e);

            *(short*)(e + 0x16) = (short)(radius + 1);
            if (radius + 1 > 0x28) *(short*)(e + 0x16) = 0x28;

            if (*(short*)(e + 0x9a) >= 0x11d)
            {
                *(short*)(e + 0x10) = 7;
                *(short*)(e + 0x12) = 0x0d;
                return;
            }
        }

        // --- Etat 7 : la retombee ------------------------------------------------------------
        if (*(short*)(e + 0x10) == 7)
        {
            *(short*)(e + 0x9a) += 1;
            *(short*)(e + 0x32) += 8;
            *(short*)(e + 0x34) -= 20;

            short radius = *(short*)(e + 0x16);
            CollapseArcStep(e);

            short speed = *(short*)(e + 0x12);
            *(int*)(e + 0x20) -= speed << 0xc;
            *(short*)(e + 0x12) = (short)(speed + 1);

            *(short*)(e + 0x16) = (short)(radius - 2);
            if (radius - 2 < 5) *(short*)(e + 0x16) = 5;

            if (*(int*)(e + 0x20) < -0x578000)   // 0xffa88000
            {
                *(short*)(e + 0x10) = 8;
                *(short*)(e + 0x12) = 0;
                return;
            }
        }

        // --- Etat 8 : l'extinction -----------------------------------------------------------
        if (*(short*)(e + 0x10) != 8) return;

        *(uint*)e |= 0x6000;
        *(short*)(e + 0x12) += 1;
        if (*(short*)(e + 0x12) >= 0x3c) *(uint*)e &= 0x7fffffff;
    }

    // =====================================================================
    // LE ROCHER QUI TOMBE ET ECLATE — creneau 63 du bloc 0x00554428 (table 0x0056b1c8)
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00408ef0</c> — <b>le rocher qui tombe, eclate et blesse</b>, creneau 63 du bloc
    /// <c>0x00554428</c>, sous-zones 5 a 8 de la mine. Absente de <c>Blaze.c</c> ; 191 instructions,
    /// <c>Legacy/Raw/Blaze/FUN_00408EF0.asm</c>. Il vit ici et non dans <c>Effects/</c> : c'est un
    /// decor qui tombe et qui frappe, comme le pilon et les masses.
    ///
    /// <para>
    /// <b>Son modele est choisi par <c>+0x14</c></b>, ajoute a <see cref="s_Unknown_005edec4"/> —
    /// l'indice ou commencent les modeles de zone. La variante 0 fait aussi naitre un effet
    /// <c>0xEF</c> a sa position.
    /// </para>
    /// <para>
    /// <b>Etat 1, la chute</b> : il descend d'une vitesse <c>+0x12</c> qui part de 10 cases et gagne
    /// une case par image. <b>Au sol</b> (<c>+0x64</c>) il se pose, tire une vitesse de rebond
    /// (<c>-8..7</c> cases en X et Z, <c>20..22</c> vers le haut), fait naitre <b>trois eclats
    /// <c>0xF4</c></b>, joue le son <c>0x20</c> de la zone a moins de 2000 unites de la camera, tire
    /// trois vitesses de rotation entre <c>-20</c> et <c>19</c>, et <b>blesse le groupe</b> par
    /// <see cref="DamagePartyNearPoint_00418F50"/> : <c>3 %</c> des points de vie a moins de
    /// <c>0x50</c>. Tombe sous <c>-0xbb8000</c> sans toucher le sol, il meurt sur-le-champ.
    /// </para>
    /// <para>
    /// <b>Etat 2, le rebond</b> — il commence dans l'image meme de l'impact : il vole sur sa
    /// vitesse, tourne sur ses trois axes, perd une case de vitesse verticale par image, <b>clignote
    /// une image sur deux</b> (bit <c>0x4000</c> suivant la parite de <c>+0x16</c>) et meurt quand la
    /// vitesse verticale passe sous <c>-15</c>, ou sous la meme profondeur que la chute.
    /// </para>
    /// </summary>
    private static void EntityHandler_00408EF0(byte* e)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            e[0x04] = (byte)(s_Unknown_005edec4 + e[0x14]);
            e[0x0a] = 2;
            e[0x0b] = 0;
            *(short*)(e + 0x10) = 1;
            *(short*)(e + 0x12) = 10;
            *(short*)(e + 0x16) = 0;

            if (*(short*)(e + 0x14) == 0)
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xef, 0, -1);
        }

        if (*(short*)(e + 0x10) == 1)
        {
            int height = *(int*)(e + 0x20) - (*(short*)(e + 0x12) << 0xc);
            *(int*)(e + 0x20) = height;

            if (height <= *(int*)(e + 0x64))
            {
                *(int*)(e + 0x20) = *(int*)(e + 0x64);
                *(short*)(e + 0x10) = 2;

                *(int*)(e + 0x50) = SignedMask(CrtRand(), 0xf) - 8;
                *(int*)(e + 0x58) = SignedMask(CrtRand(), 0xf) - 8;
                *(int*)(e + 0x54) = CrtRand() % 3 + 0x14;

                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf4, 0, -1);
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf4, 0, -1);
                SpawnStatusEffectEntity_0047AC40(e, (int*)(e + 0x1c), null, 0xf4, 0, -1);

                TryPlaySoundNearEntity((uint*)e, ZoneSound(0x20), 0x800, 0x1000, 0, 0);   // Ghidra: FUN_00468080

                *(int*)(e + 0x44) = CrtRand() % 0x28 - 0x14;
                *(int*)(e + 0x48) = CrtRand() % 0x28 - 0x14;
                *(int*)(e + 0x4c) = CrtRand() % 0x28 - 0x14;

                DamagePartyNearPoint_00418F50(
                    *(int*)(e + 0x1c), *(int*)(e + 0x20), *(int*)(e + 0x24), 0x50, 3, 0);
            }

            *(short*)(e + 0x12) += 1;
            if (*(int*)(e + 0x20) < -0xbb8000)
            {
                *(short*)(e + 0x10) = 2;
                *(uint*)e &= 0x7fffffff;
                return;   // le seul chemin qui saute l'avance de +0x16
            }
        }

        if (*(short*)(e + 0x10) == 2)
        {
            int height = *(int*)(e + 0x20);
            if (height < -0xbb8000) *(uint*)e &= 0x7fffffff;

            *(int*)(e + 0x1c) += *(int*)(e + 0x50) << 0xc;
            *(int*)(e + 0x24) += *(int*)(e + 0x58) << 0xc;

            int climb = *(int*)(e + 0x54);
            *(int*)(e + 0x20) = (climb << 0xc) + height;

            *(short*)(e + 0x30) += (short)*(int*)(e + 0x44);
            *(short*)(e + 0x34) += (short)*(int*)(e + 0x4c);
            *(short*)(e + 0x32) += (short)*(int*)(e + 0x48);

            climb -= 1;
            *(int*)(e + 0x54) = climb;

            uint flags = *(uint*)e;
            flags = (e[0x16] & 1) != 0 ? flags | 0x4000 : flags & 0xffffbfff;
            *(uint*)e = flags;

            if (climb < -15) *(uint*)e = flags & 0x7fffffff;
        }

        *(short*)(e + 0x16) += 1;
    }
}
