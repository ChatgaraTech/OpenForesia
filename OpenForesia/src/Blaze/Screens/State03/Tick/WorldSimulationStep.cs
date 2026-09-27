// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE PAS DE SIMULATION DU MONDE — les appelees du cas 0 du repartiteur.
//
// SIX D'ENTRE ELLES SONT LA MEME PASSE SUR SIX TABLES DIFFERENTES. C'est la decouverte qui rend
// ce fichier court : les tables 0x005fa5e0, 0x005ecaa0, 0x005f7de0, 0x005ff400, 0x005edee0 et
// 0x005f2d40 portent toutes des enregistrements de 0x9c octets avec la meme disposition de tete
// — mot d'etat a +0x00, indice d'emplacement a +0x08, position a +0x1c/+0x20/+0x24, chainage de
// grille a +0x68 — et chaque passe fait les memes trois gestes : ignorer l'enregistrement
// inactif, appeler son gestionnaire par table de sauts, puis le raccrocher a la grille spatiale.
// Ce qui les distingue tient dans le CORPS DU GESTIONNAIRE, pas dans la passe.
//
// Le raccrochage est `LinkIntoWorldGrid`, et les deux grilles qu'il alimente sont exactement les
// deux pages de 0x400 dwords que le prologue remet a zero : 0x3880 * 0x20 * 4 = 0x1c4000 et
// 0x38a0 * 0x20 * 4 = 0x1c5000. Le rapprochement n'etait pas evident dans la pseudo-C, qui
// n'ecrit que les constantes 0x3880 et 0x38a0.
//
// Le fichier porte aussi le mouvement d'une entite de scene, son animation, sa disparition, le
// butin qu'elle laisse, et le lissage d'orientation et de couleur de fond.
//
// Les gestionnaires appeles par table de sauts sont resolus par `Dispatch/`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    // =====================================================================
    // LE PAS DE SIMULATION DU MONDE — les appelees du cas 0 (0x005176d5..0x0051776b).
    //
    // Elles restent ici plutot que de monter dans Engine/ : la regle de routage (4) d'
    // ARCHITECTURE.md les laisse chez leur appelant tant qu'un seul ecran les appelle, et c'est le
    // cas — la carte du monde est aujourd'hui le seul chemin transcrit qui y mene.
    //
    // **Six d'entre elles sont la meme passe sur six tables differentes.** C'est la decouverte qui
    // rend ce bloc court : les tables 0x005fa5e0, 0x005ecaa0, 0x005f7de0, 0x005ff400, 0x005edee0 et
    // 0x005f2d40 portent toutes des enregistrements de **0x9c octets** avec la meme disposition de
    // tete — mot d'etat a +0x00, indice d'emplacement a +0x08, position a +0x1c/+0x20/+0x24,
    // chainage de grille a +0x68 — et chaque passe fait les memes trois gestes : ignorer
    // l'enregistrement inactif, appeler son gestionnaire par table de sauts, puis le raccrocher a
    // la grille spatiale. Ce qui les distingue tient dans le corps du gestionnaire, pas dans la
    // passe.
    //
    // Le raccrochage est <see cref="LinkIntoWorldGrid"/>, et les deux grilles qu'il alimente sont
    // exactement les deux pages de 0x400 dwords que le prologue et le cas 0 remettent a zero :
    // 0x3880 * 0x20 * 4 = 0x1c4000 et 0x38a0 * 0x20 * 4 = 0x1c5000. Le rapprochement n'etait pas
    // evident dans la pseudo-C, qui n'ecrit que les constantes 0x3880 et 0x38a0.
    // =====================================================================

    /// <summary>
    /// Le raccrochage d'une entite a une des deux <b>grilles spatiales 32x32</b> de MainMem : la
    /// cellule est deduite de la position monde, et l'entite est poussee en tete de la liste
    /// simplement chainee de cette cellule.
    ///
    /// <para>
    /// <c>((v &gt;&gt; 12) + 0x2000) &gt;&gt; 9</c> : la position est en 20.12, donc <c>v &gt;&gt; 12</c> est en unites
    /// monde ; <c>+0x2000</c> recentre la plage <c>[-0x2000, 0x2000[</c> sur zero et <c>&gt;&gt; 9</c> la
    /// divise en 32 cases de 512. Le test d'appartenance est fait <b>en non signe</b> dans
    /// l'original, ce qui rejette d'un coup les cellules negatives ; transcrit tel quel.
    /// </para>
    /// <para>
    /// Le chainage stocke une <b>adresse</b> dans une case de quatre octets. C'est exact ici parce
    /// que la reconstruction est batie en x86 (<c>PlatformTarget x86</c> dans OpenForesia.csproj),
    /// donc un pointeur y fait bien quatre octets, comme en 1998.
    /// </para>
    /// </summary>
    /// <param name="gridOffset">0x1c4000 ou 0x1c5000 — l'origine de la grille dans MainMem.</param>
    private static void LinkIntoWorldGrid(byte* mainMem, int gridOffset, byte* record)
    {
        uint cellX = (uint)(((*(int*)(record + 0x1c) >> 0xc) + 0x2000) >> 9);
        uint cellZ = (uint)(((*(int*)(record + 0x24) >> 0xc) + 0x2000) >> 9);
        if (cellX >= 0x20 || cellZ >= 0x20) return;

        byte* cell = mainMem + gridOffset + (int)(cellZ * 0x20 + cellX) * 4;
        *(uint*)(record + 0x68) = *(uint*)cell;
        *(uint*)cell = (uint)(nuint)record;
    }

    /// <summary>Le pas d'amortissement de la pose de camera : meme forme que
    /// <see cref="SmoothWrappedAngleStep"/> a la profondeur pres — un quart de l'ecart au lieu d'un
    /// huitieme.</summary>
    private static short SmoothCameraStep(ushort delta)
        => delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-((0x1000 - (short)delta) >> 2);

    /// <summary>
    /// Ghidra: FUN_00532b40 @ 0x00532b40 — <b>le mouvement de scene scripte</b> : un flux
    /// d'enregistrements de 0x10 octets, lu a <c>DAT_005e9b3c</c>, dont chaque tour ajoute des
    /// deltas aux <b>consignes</b> que <see cref="SmoothSceneOrientationAndBackColour"/> poursuit.
    /// Le flux boucle sur lui-meme quand son premier octet vaut -1.
    ///
    /// <para>
    /// <b>Trois des quatre ecritures ont une forme inattendue, et les trois sont verifiees au
    /// desassembleur</b> (0x00532b57..0x00532bce) :
    /// </para>
    /// <list type="bullet">
    /// <item><c>0x005e9310</c> recoit <b>deux additions 16 bits independantes</b>, sans retenue de
    /// l'une vers l'autre — deux angles dans un dword.</item>
    /// <item><c>0x005e9314</c> est lu et ecrit en <b>dword</b> puis masque a 16 bits : la moitie
    /// haute (<c>0x005e9316</c>) est donc <b>remise a zero a chaque tour</b>. Idem pour
    /// <c>0x005e92bc</c>.</item>
    /// <item><c>0x005e92b8</c> voit ses deux moities <b>croisees</b> : la nouvelle moitie basse
    /// vaut l'ancienne moitie haute plus celle du flux, et la nouvelle moitie haute vaut l'ancienne
    /// moitie basse plus le mot suivant. Ghidra le rendait deja ainsi et on pouvait croire a un
    /// artefact ; <c>shr edi,10h ; add eax,edi ; shl edx,10h ; or eax,edx</c> dit que non.</item>
    /// </list>
    /// </summary>
    private static void AdvanceScriptedSceneMotion_00532B40()
    {
        // Ghidra: `if (DAT_005645fc != -1)`. C'est un OCTET (0x00563612 l'ecrit par CONCAT31), et
        // le depot le porte deja en `int` sous s_Unknown_005645fc — d'ou la comparaison a 0xff et
        // non a -1. **Ce symbole n'est PAS dans EntityArena** : le lire par Mem.Entity donnerait un
        // decalage negatif, ce qui a effectivement fait passer la garde a tort avant correction.
        if ((byte)s_Unknown_005645fc == 0xff) return;

        byte* stream = *(byte**)Mem.Arena(0x005e9b3c);

        uint deltaOrientation = *(uint*)(stream + 4);
        uint deltaRoll = *(uint*)(stream + 8);
        uint deltaColour = *(uint*)(stream + 0x0c);

        *(byte**)Mem.Arena(0x005e9b3c) = stream + 0x10;

        uint orientation = *(uint*)Mem.Arena(0x005e9310);
        *(uint*)Mem.Arena(0x005e9310) =
            ((deltaOrientation & 0xffff0000) + (orientation & 0xffff0000))
            | ((deltaOrientation + orientation) & 0xffff);

        *(uint*)Mem.Arena(0x005e9314) = (*(uint*)Mem.Arena(0x005e9314) + deltaRoll) & 0xffff;

        uint colour = *(uint*)Mem.Arena(0x005e92b8);
        *(uint*)Mem.Arena(0x005e92b8) =
            ((deltaRoll >> 0x10) + (colour >> 0x10)) | ((colour + deltaColour) << 0x10);

        *(uint*)Mem.Arena(0x005e92bc) = (deltaColour + *(uint*)Mem.Arena(0x005e92bc)) & 0xffff;

        if (**(sbyte**)Mem.Arena(0x005e9b3c) == -1)
        {
            *(byte**)Mem.Arena(0x005e9b3c) = **(byte***)Mem.Entity(0x005eca98);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005320f0 @ 0x005320f0 — <b>le defilement de texture</b> d'un morceau de decor :
    /// chaque face listee avance ses quatre coordonnees de texture d'une unite, et les quatre
    /// retombent ensemble de la periode des qu'elles l'ont toutes atteinte.
    ///
    /// <para>
    /// La periode est l'octet de <c>+0x3f</c> du morceau ; la liste des faces est terminee par
    /// <c>0xffff</c>. Les quatre coordonnees vivent dans deux dwords, un octet chacune sur les
    /// octets 0 et 2 — les octets 1 et 3 sont preserves par le masque <c>0xff00ff00</c>.
    /// </para>
    /// <para>
    /// <see cref="ScrollTextureTriangles_005321F0"/> est sa jumelle a deux differences pres : le
    /// pas d'enregistrement (0x10 au lieu de 0x20) et les deux decalages (+8/+0xc au lieu de
    /// +0x10/+0x14). Ecrites separement, comme les deux fonctions du binaire.
    /// </para>
    /// </summary>
    private static void ScrollTextureQuads_005320F0(byte* chunk)
        => ScrollTextureCoordinates(chunk, 0x20, 0x10);

    /// <summary>Ghidra: FUN_005321f0 @ 0x005321f0 — la jumelle de
    /// <see cref="ScrollTextureQuads_005320F0"/> pour les faces a trois sommets.</summary>
    private static void ScrollTextureTriangles_005321F0(byte* chunk)
        => ScrollTextureCoordinates(chunk, 0x10, 0x08);

    /// <summary>
    /// Le corps commun aux deux defilements. Factorise parce que les deux fonctions de l'original
    /// sont copie-collees a deux constantes pres — meme cas que
    /// <c>FreezeAllEntities</c>/<c>UnfreezeAllEntities</c>.
    /// </summary>
    private static void ScrollTextureCoordinates(byte* chunk, int stride, int offset)
    {
        if (chunk == null || *(short*)(chunk + 0x0e) == 0) return;

        ushort* faces = **(ushort***)(chunk + 0x38);
        if (faces == null) return;

        byte* records = *(byte**)(chunk + 0x28);
        ushort period = chunk[0x3f];

        for (ushort face = *faces; face != 0xffff; face = *(++faces))
        {
            byte* record = records + face * stride;
            uint first = *(uint*)(record + offset);
            uint second = *(uint*)(record + offset + 4);

            uint u0 = (first & 0xff) + 1;
            uint v0 = ((first >> 0x10) & 0xff) + 1;
            uint u1 = (second & 0xff) + 1;
            uint v1 = ((second >> 0x10) & 0xff) + 1;

            if (period <= (ushort)u0 && period <= (ushort)v0
                && period <= (ushort)u1 && period <= (ushort)v1)
            {
                u0 -= period;
                v0 -= period;
                u1 -= period;
                v1 -= period;
            }

            *(uint*)(record + offset) = ((v0 & 0xff) << 0x10) | (u0 & 0xff) | (first & 0xff00ff00);
            *(uint*)(record + offset + 4) = ((v1 & 0xff) << 0x10) | (u1 & 0xff) | (second & 0xff00ff00);
        }
    }

    /// <summary>Le pas d'amortissement des angles du cas 0 : le delta est deja ramene dans
    /// <c>[0, 0x1000[</c> par un <c>&amp; 0xfff</c>, et la moitie haute veut dire « dans l'autre
    /// sens ».</summary>
    private static short SmoothWrappedAngleStep(ushort delta)
        => delta < 0x800
            ? (short)((short)delta >> 3)
            : (short)-((0x1000 - (short)delta) >> 3);

    /// <summary>Le pas d'amortissement du cas 2, ou le delta n'est <b>pas</b> masque et a deja subi
    /// un decalage de trois sur 32 bits. La comparaison a <c>0x800</c> est signee — confirme au
    /// desassembleur (<c>cmp ax,800h ; jge</c>) et non deduit du texte de Ghidra.</summary>
    private static short SmoothSignedAngleStep(short delta)
        => delta < 0x800
            ? (short)(delta >> 3)
            : (short)-((0x1000 - delta) >> 3);

    /// <summary>
    /// Ghidra: FUN_00532bf0 @ 0x00532bf0 — <b>le lissage de la vue</b> : l'orientation de scene et la
    /// couleur de fond glissent chacune vers leur consigne, d'un huitieme de l'ecart par image.
    ///
    /// <para>
    /// Les deux moities sont batties sur le meme patron, avec un mode a trois etats
    /// (<c>DAT_005e902e</c> pour l'orientation, <c>DAT_005e9166</c> pour la couleur) :
    /// <b>0</b> glisse vers la consigne et met le resultat de cote, <b>1</b> restitue simplement ce
    /// qui a ete mis de cote, <b>2</b> glisse vers ce qui a ete mis de cote. Autrement dit : suivre,
    /// figer, revenir.
    /// </para>
    /// <para>
    /// Quand le pas calcule tombe a zero, la valeur <b>saute</b> directement a la consigne au lieu
    /// de rester bloquee a un ecart de moins de huit. C'est ce qui fait converger le lissage, et ce
    /// n'est pas un cas particulier ecrit a part : c'est la valeur d'initialisation de la variable
    /// avant le <c>if</c>.
    /// </para>
    /// <para>
    /// <b>Les deux moities ne comptent pas pareil.</b> L'orientation masque son ecart par
    /// <c>&amp; 0xfff</c> — ce sont des angles, ils font le tour — la ou la couleur ne masque rien et
    /// decale son ecart <b>deux fois</b> de trois. Verifie au desassembleur (0x00532bf0 et
    /// 0x00532e38) avant transcription : la difference est reelle, pas un artefact de Ghidra.
    /// </para>
    /// </summary>
    private static void SmoothSceneOrientationAndBackColour()
    {
        // Ghidra: DAT_005e9028 / 902a / 902c / 902e — l'orientation mise de cote, puis le mode.
        // Quatre shorts contigus, confirmes par `movsx eax,word [5E902Eh]` en tete de fonction.
        short* saved = (short*)Mem.Arena(0x005e9028);

        // Ghidra: DAT_005e9310 / 9312 pour la paire, DAT_005e9314 pour le roulis.
        short* orientTarget = (short*)Mem.Arena(0x005e9310);

        switch (saved[3])
        {
            case 0:
            {
                saved[2] = orientTarget[2];

                short stepPitch = SmoothWrappedAngleStep((ushort)((orientTarget[0] - Mem.s_ScenePitch_005e9308) & 0xfff));
                short stepYaw = SmoothWrappedAngleStep((ushort)((orientTarget[1] - Mem.s_SceneYaw_005e930a) & 0xfff));
                short stepRoll = SmoothWrappedAngleStep((ushort)((saved[2] - Mem.s_SceneRoll_005e930c) & 0xfff));

                short pitch = orientTarget[0];
                if (stepPitch != 0)
                {
                    Mem.s_ScenePitch_005e9308 += stepPitch;
                    pitch = Mem.s_ScenePitch_005e9308;
                }

                short yaw = orientTarget[1];
                if (stepYaw != 0)
                {
                    Mem.s_SceneYaw_005e930a += stepYaw;
                    yaw = Mem.s_SceneYaw_005e930a;
                }

                Mem.s_ScenePitch_005e9308 = pitch;
                Mem.s_SceneYaw_005e930a = yaw;

                if (stepRoll != 0)
                {
                    Mem.s_SceneRoll_005e930c += stepRoll;
                    saved[2] = Mem.s_SceneRoll_005e930c;
                }

                Mem.s_SceneRoll_005e930c = saved[2];

                // Ghidra: `DAT_005e9028 = _DAT_005e9308;` — une copie de DWORD, donc les deux
                // premiers shorts seulement. Le roulis mis de cote l'a deja ete ci-dessus.
                saved[0] = Mem.s_ScenePitch_005e9308;
                saved[1] = Mem.s_SceneYaw_005e930a;
                break;
            }

            case 1:
                Mem.s_ScenePitch_005e9308 = saved[0];
                Mem.s_SceneYaw_005e930a = saved[1];
                Mem.s_SceneRoll_005e930c = saved[2];
                break;

            case 2:
            {
                short stepPitch = SmoothSignedAngleStep((short)((saved[0] - Mem.s_ScenePitch_005e9308) >> 3));
                short stepYaw = SmoothSignedAngleStep((short)((saved[1] - Mem.s_SceneYaw_005e930a) >> 3));
                short stepRoll = SmoothSignedAngleStep((short)((saved[2] - Mem.s_SceneRoll_005e930c) >> 3));

                short pitch = saved[0];
                if (stepPitch != 0) pitch = (short)(stepPitch + Mem.s_ScenePitch_005e9308);

                short yaw = saved[1];
                if (stepYaw != 0) yaw = (short)(stepYaw + Mem.s_SceneYaw_005e930a);

                short roll = stepRoll == 0
                    ? saved[2]
                    : (short)(stepRoll + Mem.s_SceneRoll_005e930c);

                Mem.s_ScenePitch_005e9308 = pitch;
                Mem.s_SceneYaw_005e930a = yaw;
                Mem.s_SceneRoll_005e930c = roll;
                break;
            }
        }

        // Ghidra: DAT_005e9160 / 9162 / 9164 / 9166 — meme disposition pour la couleur de fond :
        // trois shorts mis de cote, puis le mode.
        short* savedColour = (short*)Mem.Arena(0x005e9160);
        short* colourTarget = (short*)Mem.Arena(0x005e92b8);

        switch (savedColour[3])
        {
            case 0:
            {
                short stepR = (short)((colourTarget[0] - Mem.s_BackColourR_005e93a0) >> 3);
                short stepG = (short)((colourTarget[1] - Mem.s_BackColourG_005e93a2) >> 3);
                short stepB = (short)((colourTarget[2] - Mem.s_BackColourB_005e93a4) >> 3);

                short red = colourTarget[0];
                if (stepR != 0) red = (short)(stepR + Mem.s_BackColourR_005e93a0);

                short green = colourTarget[1];
                if (stepG != 0) green = (short)(stepG + Mem.s_BackColourG_005e93a2);

                Mem.s_BackColourR_005e93a0 = red;
                Mem.s_BackColourG_005e93a2 = green;

                savedColour[2] = colourTarget[2];
                if (stepB != 0) savedColour[2] = (short)(stepB + Mem.s_BackColourB_005e93a4);

                savedColour[0] = Mem.s_BackColourR_005e93a0;
                savedColour[1] = Mem.s_BackColourG_005e93a2;
                Mem.s_BackColourB_005e93a4 = savedColour[2];
                break;
            }

            case 1:
                Mem.s_BackColourR_005e93a0 = savedColour[0];
                Mem.s_BackColourG_005e93a2 = savedColour[1];
                Mem.s_BackColourB_005e93a4 = savedColour[2];
                break;

            case 2:
            {
                short stepR = (short)((savedColour[0] - Mem.s_BackColourR_005e93a0) >> 3);
                short stepG = (short)((savedColour[1] - Mem.s_BackColourG_005e93a2) >> 3);
                short stepB = (short)((savedColour[2] - Mem.s_BackColourB_005e93a4) >> 3);

                short red = savedColour[0];
                if (stepR != 0) red = (short)(stepR + Mem.s_BackColourR_005e93a0);

                short green = savedColour[1];
                if (stepG != 0) green = (short)(stepG + Mem.s_BackColourG_005e93a2);

                Mem.s_BackColourR_005e93a0 = red;
                Mem.s_BackColourG_005e93a2 = green;

                Mem.s_BackColourB_005e93a4 = stepB == 0
                    ? savedColour[2]
                    : (short)(stepB + Mem.s_BackColourB_005e93a4);
                break;
            }
        }

        Winsys3d.Ordinal_376(
            (byte)Mem.s_BackColourR_005e93a0,
            (byte)Mem.s_BackColourG_005e93a2,
            (byte)Mem.s_BackColourB_005e93a4);
    }


    /// <summary>
    /// Ghidra: FUN_00455b30 @ 0x00455b30 — <b>le deplacement d'une entite de scene avec collision,
    /// et son annulation</b>. 890 octets, decompilee dans <c>Blaze.c</c> mais avec une signature
    /// ratee ; releve garde sous <c>Legacy/Raw/Blaze/FUN_00455B30.asm</c>. <b>Seize sites
    /// d'appel</b> : c'est la routine que partagent les gestionnaires d'entite de zone.
    ///
    /// <para>
    /// <b>Ce que Ghidra a manque, et ce que le desassembleur rend.</b> La pseudo-C ne declare
    /// qu'<b>un</b> parametre alors que les seize appels en empilent deux, et elle lit trois
    /// registres non initialises. Les quatre points se resolvent sur la pile :
    /// </para>
    /// <list type="bullet">
    /// <item><c>unaff_retaddr</c> est le <b>second argument</b>, le masque. Il n'est lu qu'une
    /// fois, a <c>0x00455d62</c>, dans la vue d'apres le <c>push esi</c> de l'appel qui precede —
    /// d'ou <c>[esp+0x40]</c> et non <c>[esp+0x3c]</c>, ce qui a fait croire a Ghidra qu'il
    /// s'agissait de l'adresse de retour ;</item>
    /// <item><c>unaff_EBP</c> est la copie de <c>+0x00</c> prise a l'entree
    /// (<c>[esp+0x10]</c>) ;</item>
    /// <item><c>unaff_EBX</c> est celle de <c>+0x60</c> (<c>[esp+0x14]</c>) ;</item>
    /// <item>et surtout, <b>l'appariement du bloc de restauration est decale d'un cran</b> dans la
    /// pseudo-C — elle rend <c>param_1[7] = uVar1</c> la ou <c>uVar1</c> vient de
    /// <c>param_1[0x18]</c>. Le desassembleur montre que <b>chaque champ retourne a lui-meme</b> :
    /// c'est une annulation, pas une permutation.</item>
    /// </list>
    ///
    /// <para>
    /// <b>Le corps, en trois temps.</b> La naissance (<c>+0x10 == 0</c>) pose les quatre demi-cotes
    /// a <c>0xa000</c>, met les vitesses a zero, fige le plancher <c>+0x60</c>/<c>+0x64</c> sur la
    /// hauteur courante et appelle <see cref="TryPlaceEntity"/> avec le masque <c>0x8000</c>.
    /// Ensuite l'entite est <b>sauvegardee</b> — onze champs plus le mot de contact <c>+0x90</c> —,
    /// deplacee, puis <see cref="FinalizeEntityPlacement2"/> decide de la face du terrain sous
    /// elle. Enfin, <b>deux conditions annulent tout</b> : le masque porte <c>0x2000</c> et la face
    /// atteinte porte <c>0x20</c> en <c>+0x0d</c> ; ou le mot de contact croise le masque. Dans les
    /// deux cas les onze champs reviennent et les vitesses horizontales <b>changent de signe</b> —
    /// l'entite rebondit.
    /// </para>
    /// <para>
    /// <b>La vitesse est divisee par le nombre de contacts.</b> Les quatre bits bas de <c>+0x09</c>
    /// comptent les cotes en appui ; <c>+0x50</c> et <c>+0x58</c> sont divises par ce compte. Puis
    /// les trois composantes poussees sont <b>halvees</b>, et si leur longueur depasse celle des
    /// composantes voulues (<c>+0x44</c>/<c>+0x48</c>/<c>+0x4c</c>), ce sont elles qui l'emportent :
    /// une entite coincee se laisse pousser plutot que d'insister.
    /// </para>
    /// <para>
    /// <b>Le mot de contact est sauvegarde sur seize bits et restitue sur trente-deux</b>
    /// (<c>mov bp,[esi+90h]</c> a la sauvegarde, <c>mov [esi+90h],ebp</c> a la restitution, apres
    /// masquage par <c>0xffff</c>). Une annulation efface donc la moitie haute de <c>+0x90</c>.
    /// C'est ce que le binaire fait ; transcrit tel quel.
    /// </para>
    /// </summary>
    /// <param name="mask">Le masque de refus, second argument des seize sites d'appel.</param>
    /// <returns>Le bit 29 du mot d'etat de l'entite, 0 ou 1.</returns>
    private static uint MoveSceneEntity_00455B30(byte* e, uint mask)
    {
        if (*(short*)(e + 0x10) == 0)
        {
            *(int*)(e + 0x50) = 0;
            *(ushort*)(e + 0x82) = 0xa000;
            *(ushort*)(e + 0x86) = 0xa000;
            *(ushort*)(e + 0x8a) = 0xa000;
            *(ushort*)(e + 0x8e) = 0xa000;
            *(int*)(e + 0x54) = 0;
            *(int*)(e + 0x58) = 0;
            *(int*)(e + 0x44) = 0;
            *(int*)(e + 0x48) = 0;
            *(int*)(e + 0x4c) = 0;
            *(short*)(e + 0x10) = 1;
            *(int*)(e + 0x64) = *(int*)(e + 0x20);
            *(int*)(e + 0x60) = *(int*)(e + 0x20);

            TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)e, 0x8000);
        }

        // ---------------------------------------------------------------- la sauvegarde
        uint savedState = *(uint*)e;                    // [esp+0x10], l'unaff_EBP de Ghidra
        int savedFloor = *(int*)(e + 0x60);             // [esp+0x14], l'unaff_EBX
        int savedCeiling = *(int*)(e + 0x64);           // [esp+0x18]
        int savedX = *(int*)(e + 0x1c);                 // [esp+0x1c]
        int savedZ = *(int*)(e + 0x24);                 // [esp+0x20]
        int saved80 = *(int*)(e + 0x80);                // [esp+0x24]
        int saved84 = *(int*)(e + 0x84);                // [esp+0x28]
        int saved88 = *(int*)(e + 0x88);                // [esp+0x2c]
        int saved8c = *(int*)(e + 0x8c);                // [esp+0x30]
        uint savedFace = *(uint*)(e + 0x7c);            // [esp+0x38]
        uint contact = *(ushort*)(e + 0x90);            // bp — SEIZE bits, voir l'en-tete

        // ---------------------------------------------------------------- la poussee des contacts
        byte contacts = e[9];
        if ((contacts & 0x0f) != 0)
        {
            int sides = 0;
            for (int bit = 0; bit <= 3; bit++)
            {
                if (((contacts >> bit) & 1) != 0) sides++;
            }

            *(int*)(e + 0x50) /= sides;
            *(int*)(e + 0x58) /= sides;
        }

        *(int*)(e + 0x54) >>= 1;
        *(int*)(e + 0x58) >>= 1;
        *(int*)(e + 0x50) >>= 1;

        int wanted = (int)Winsys3d.Ordinal_442((uint)(
            Squared(*(int*)(e + 0x44) >> 0xc)
          + Squared(*(int*)(e + 0x48) >> 0xc)
          + Squared(*(int*)(e + 0x4c) >> 0xc)));

        int pushed = (int)Winsys3d.Ordinal_442((uint)(
            Squared(*(int*)(e + 0x50) >> 0xc)
          + Squared(*(int*)(e + 0x54) >> 0xc)
          + Squared(*(int*)(e + 0x58) >> 0xc)));

        // La poussee l'emporte sur la volonte : une entite coincee se laisse deplacer.
        if (wanted < pushed)
        {
            *(int*)(e + 0x44) = *(int*)(e + 0x50);
            *(int*)(e + 0x4c) = *(int*)(e + 0x58);
        }

        ClampStep(e + 0x44);
        ClampStep(e + 0x4c);

        // ---------------------------------------------------------------- l'integration
        int vx = *(int*)(e + 0x44);
        int vy = *(int*)(e + 0x48);
        int vz = *(int*)(e + 0x4c);

        *(int*)(e + 0x1c) += vx;
        *(int*)(e + 0x24) += vz;
        *(int*)(e + 0x20) += vy;

        // Ghidra: `sar 2` puis, si le quotient est nul, la valeur entiere — un frottement d'un
        // quart qui ne s'endort jamais sur les petites vitesses.
        int brakeX = vx >> 2;
        if (brakeX == 0) brakeX = vx;

        int brakeZ = vz >> 2;
        if (brakeZ == 0) brakeZ = vz;

        int newX = *(int*)(e + 0x44) - brakeX;
        *(int*)(e + 0x44) = newX;

        int newZ = *(int*)(e + 0x4c) - brakeZ;

        newX -= newX >> 8;
        *(int*)(e + 0x44) = newX;

        int gravity = *(short*)(e + 0x9a);
        *(int*)(e + 0x48) = ((gravity >> 8) - (vy >> 8) - gravity) + vy;
        *(int*)(e + 0x4c) = newZ - (newZ >> 8);

        FinalizeEntityPlacement2((uint*)e);

        // ---------------------------------------------------------------- les deux annulations
        if ((mask & 0x2000) != 0)
        {
            contact &= 0xdfff;

            byte* face = *(byte**)(e + 0x7c);
            if ((face[0x0d] & 0x20) != 0)
            {
                RollBackMove(e, savedState, savedFloor, savedCeiling, savedX, savedZ,
                             saved80, saved84, saved88, saved8c, savedFace, contact & 0xffff);
            }
        }

        if ((*(uint*)(e + 0x90) & mask) != 0)
        {
            RollBackMove(e, savedState, savedFloor, savedCeiling, savedX, savedZ,
                         saved80, saved84, saved88, saved8c, savedFace, contact & 0xffff);
        }

        // ---------------------------------------------------------------- le plancher
        if (*(int*)(e + 0x20) <= *(int*)(e + 0x60) && (*(uint*)(e + 0x90) & 0xa000) == 0)
        {
            *(int*)(e + 0x20) = *(int*)(e + 0x60);
            *(int*)(e + 0x48) = 0;
        }
        else
        {
            *(short*)(e + 0x9a) = 0x3000;
        }

        // Ghidra: `shr eax,1Dh ; and ax,1` — le bit 29 du mot d'etat, sur seize bits.
        return (*(uint*)e >> 0x1d) & 1;
    }

    /// <summary>Le carre d'un entier, tel que les deux sommes de
    /// <see cref="MoveSceneEntity_00455B30"/> l'emploient.</summary>
    private static int Squared(int v) => v * v;

    /// <summary>Une composante horizontale bornee a <c>0x1e000</c> en valeur absolue, la
    /// comparaison portant sur la valeur <b>masquee</b> par <c>0xfffff000</c> — les douze bits de
    /// fraction sont ignores par le test mais gardes par l'affectation. Les deux bornes sont
    /// testees dans cet ordre, la seconde relisant le champ que la premiere a pu ecrire.</summary>
    private static void ClampStep(byte* field)
    {
        if ((*(int*)field & unchecked((int)0xfffff000)) > 0x1e000) *(int*)field = 0x1e000;
        if ((*(int*)field & unchecked((int)0xfffff000)) < -0x1e000) *(int*)field = -0x1e000;
    }

    /// <summary>
    /// Les onze champs sauvegardes par <see cref="MoveSceneEntity_00455B30"/>, remis en place, et
    /// les deux vitesses horizontales inversees. Le binaire ecrit ce bloc <b>deux fois</b>, aux
    /// deux conditions d'annulation, dans un ordre de champs different mais avec le meme effet ;
    /// il est factorise ici.
    /// </summary>
    private static void RollBackMove(
        byte* e, uint state, int floor, int ceiling, int x, int z,
        int f80, int f84, int f88, int f8c, uint face, uint contact)
    {
        *(uint*)(e + 0x7c) = face;
        *(uint*)e = state;
        *(uint*)(e + 0x90) = contact;
        *(int*)(e + 0x64) = ceiling;
        *(int*)(e + 0x60) = floor;
        *(int*)(e + 0x24) = z;
        *(int*)(e + 0x1c) = x;
        *(int*)(e + 0x84) = f84;
        *(int*)(e + 0x80) = f80;
        *(int*)(e + 0x8c) = f8c;
        *(int*)(e + 0x88) = f88;

        *(int*)(e + 0x44) = -*(int*)(e + 0x44);
        *(int*)(e + 0x4c) = -*(int*)(e + 0x4c);
    }

    /// <summary>
    /// Ghidra: <c>0x00456a30</c> — <b>le sous-type 29 de la carte suivante</b>, entree de la table
    /// <c>0x0056adc8</c> que la passe <see cref="TickEntityTable_005fa5e0"/> resout. 692 octets,
    /// <b>absente de <c>Blaze.c</c></b> ; releve garde sous
    /// <c>Legacy/Raw/Blaze/FUN_00456A30.asm</c>. Signalee en jeu par Vincent le 2026-08-30.
    ///
    /// <para>
    /// <b>C'est une entite qui roule et qui poursuit.</b> Elle tient sa position hors de son propre
    /// enregistrement, dans six octets de l'arene (<c>0x005e9708</c>..<c>0x005e970d</c>) : trois
    /// coordonnees sur seize bits, chacune en deux octets poids fort puis poids faible. Le drapeau
    /// <c>0x005e970e</c> commande le tout — son bit <c>2</c> dit que la position a ete semee, son
    /// bit <c>1</c> ce que la fin du corps decide.
    /// </para>
    /// <para>
    /// <b>Elle commence par designer le membre le plus proche.</b> Les quatre creneaux de
    /// <see cref="Inp.s_SlotFlags_005e93c0"/> vivants et non retires sont mesures a la distance
    /// euclidienne, et l'indice du plus proche part dans la <b>variable de script 2</b>
    /// (<c>0x005e9062</c>) — d'ou un script de la zone peut le relire. La distance de depart est
    /// <c>0x1869f</c>, soit 99 999.
    /// </para>
    /// <para>
    /// <b>La position se recharge a la naissance et se resauvegarde a chaque image.</b> Tant que
    /// <c>+0x10</c> vaut zero, les trois coordonnees viennent des six octets, decalees de douze
    /// bits avec extension de signe depuis le seizieme (<c>shl 0x10</c> puis <c>sar 4</c>). A la
    /// fin du corps, le trajet parcouru y est reecrit. L'entite <b>survit donc a sa propre
    /// disparition</b> : eteinte puis reengendree, elle reprend ou elle en etait.
    /// </para>
    /// <para>
    /// <b>Deux sons et un effet, au meme seuil.</b> Si l'une des deux vitesses horizontales depasse
    /// <c>0x2000</c> en valeur absolue, le compteur <c>+0x98</c> arme un son
    /// (<see cref="TryPlaySoundNearEntity"/>, identifiant 2) puis un effet
    /// (<see cref="SpawnEffectNearCamera_00468110"/>, sous-type <c>0x3d</c>). Le compteur decompte
    /// et se recharge a 15 : le son ne repart donc qu'une image sur seize tant que ca roule.
    /// </para>
    /// <para>
    /// <b>Elle s'eteint de deux facons</b> : en tombant sous <c>-0x1f4000</c>, ou quand
    /// <see cref="MoveSceneEntity_00455B30"/> rend 1 — c'est-a-dire quand le bit 29 du mot d'etat
    /// est pose. Le masque passe au deplacement est <c>0x8882</c>.
    /// </para>
    /// <para>
    /// <b>La fin mesure la distance a un point fixe</b> — <c>(-0x12c, +0xc8, +0x10cc)</c> en unites
    /// entieres — et pose <c>+0x0a</c> a <b>4</b> au-dela de 200, a <b>5</b> en deca, en posant ou
    /// en effacant le bit 1 de <c>0x005e970e</c>. Les trois vitesses sont remises a zero dans les
    /// deux cas, apres l'integration : le deplacement de l'image a bien eu lieu, mais rien n'est
    /// reporte a la suivante.
    /// </para>
    /// </summary>
    private static void EntityHandler_00456A30(byte* e)
    {
        // ---------------------------------------------------------------- le membre le plus proche
        int nearest = 0x1869f;
        byte* member = Inp.s_SlotFlags_005e93c0;

        for (int i = 0; i <= 3; i++, member += 0x9c)
        {
            uint flags = *(uint*)member;
            if ((flags & 0x80000000) == 0) continue;
            if ((flags & 0x50000000) != 0) continue;

            int dx = (*(int*)(e + 0x1c) - *(int*)(member + 0x1c)) >> 0xc;
            int dy = (*(int*)(e + 0x20) - *(int*)(member + 0x20)) >> 0xc;
            int dz = (*(int*)(e + 0x24) - *(int*)(member + 0x24)) >> 0xc;

            int range = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dy * dy + dz * dz));
            if (range >= nearest) continue;

            nearest = range;
            Mem.Arena(0x005e9062)[0] = (byte)i;   // la variable de script 2
        }

        // ---------------------------------------------------------------- la position semee
        byte* saved = Mem.Arena(0x005e9708);
        byte* control = Mem.Arena(0x005e970e);

        if ((control[0] & 2) == 0)
        {
            control[0] |= 2;
            saved[0] = 0xfe; saved[1] = 0xd4;      // X = 0xfed4
            saved[2] = 0x00; saved[3] = 0xc8;      // Y = 0x00c8
            saved[4] = 0x10; saved[5] = 0xcc;      // Z = 0x10cc
        }

        if (*(short*)(e + 0x10) == 0)
        {
            *(short*)(e + 0x98) = 0;

            // Ghidra: `shl eax,10h ; sar eax,4` — l'extension de signe se fait depuis le bit 15,
            // et le decalage net vaut douze bits.
            *(int*)(e + 0x1c) = ((saved[0] << 8 | saved[1]) << 0x10) >> 4;
            *(int*)(e + 0x20) = ((saved[2] << 8 | saved[3]) << 0x10) >> 4;
            *(int*)(e + 0x24) = ((saved[4] << 8 | saved[5]) << 0x10) >> 4;
        }

        // ---------------------------------------------------------------- le deplacement
        uint blocked = MoveSceneEntity_00455B30(e, 0x8882);
        TestTriggerVolumes_00534CD0((uint*)e);

        if (Rolling(e) && *(short*)(e + 0x98) == 0)
        {
            TryPlaySoundNearEntity((uint*)e, 2, 0x2bc, 0x1000, 0, 0);
            *(short*)(e + 0x98) = 0x0f;
        }

        if (Rolling(e))
        {
            // Le binaire empile six arguments ; les deux derniers sont nuls et non relus, comme au
            // site d'appel deja transcrit.
            SpawnEffectNearCamera_00468110(e, 0x3d, 0, -1);
        }

        (*(short*)(e + 0x98))--;
        if (*(short*)(e + 0x98) < 0) *(short*)(e + 0x98) = 0x0f;

        // ---------------------------------------------------------------- l'extinction
        if (*(int*)(e + 0x20) < unchecked((int)0xffe0c000) || (ushort)blocked == 1)
        {
            *(uint*)e &= 0x7fffffff;
        }

        // ---------------------------------------------------------------- la position resauvegardee
        saved[0] = (byte)(*(int*)(e + 0x1c) >> 0x14);
        saved[1] = (byte)(*(int*)(e + 0x1c) >> 0x0c);
        saved[2] = (byte)(*(int*)(e + 0x20) >> 0x14);
        saved[3] = (byte)(*(int*)(e + 0x20) >> 0x0c);
        saved[4] = (byte)(*(int*)(e + 0x24) >> 0x14);
        saved[5] = (byte)(*(int*)(e + 0x24) >> 0x0c);

        // ---------------------------------------------------------------- la distance au point fixe
        int ax = (*(int*)(e + 0x1c) >> 0xc) + 0x12c;
        int ay = (*(int*)(e + 0x20) >> 0xc) - 0xc8;
        int az = (*(int*)(e + 0x24) >> 0xc) - 0x10cc;

        if ((int)Winsys3d.Ordinal_442((uint)(ax * ax + ay * ay + az * az)) > 0xc8)
        {
            control[0] |= 1;
            e[0x0a] = 4;
        }
        else
        {
            control[0] &= 0xfe;
            e[0x0a] = 5;
        }

        *(int*)(e + 0x50) = 0;
        *(int*)(e + 0x54) = 0;
        *(int*)(e + 0x58) = 0;
    }

    /// <summary>Le test que <see cref="EntityHandler_00456A30"/> ecrit deux fois : l'une des deux
    /// vitesses horizontales depasse-t-elle <c>0x2000</c> en valeur absolue ? Le binaire developpe
    /// la valeur absolue par <c>cmp / jge / neg</c> aux quatre endroits.</summary>
    private static bool Rolling(byte* e)
    {
        int vx = *(int*)(e + 0x50);
        if (vx < 0) vx = -vx;
        if (vx > 0x2000) return true;

        int vz = *(int*)(e + 0x58);
        if (vz < 0) vz = -vz;
        return vz > 0x2000;
    }
    /// <summary>
    /// Ghidra: FUN_0050e5e0 @ 0x0050e5e0 — la passe sur la table <c>DAT_005fa5e0</c>, 0x80
    /// enregistrements. La plus simple des six : gestionnaire, puis raccrochage a la grille.
    ///
    /// <para>
    /// C'est la seule des six qui alimente la grille de <b>0x1c4000</b> ; les cinq autres vont dans
    /// celle de 0x1c5000. Les deux pages sont remises a zero cote a cote en tete de chaque pas,
    /// donc la separation est deliberee.
    /// </para>
    /// <para>
    /// Les deux ecritures qui suivent le gestionnaire — un <c>ushort</c> a <c>+0x96</c> remis a zero
    /// et le masque <c>&amp; 0xffffff8f</c> sur le mot d'etat — effacent la commande que le
    /// gestionnaire vient de consommer.
    /// </para>
    /// </summary>
    private static void TickEntityTable_005fa5e0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005fa5e0);

        for (int i = 0; i < 0x80; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0 && record[5] != 0)
            {
                DispatchEntityHandler(
                    Res.ResolveEntityHandler(0x0056adc8, record[5]), record,
                    $"FUN_0050e5e0, table 0x005fa5e0 entree {i}, sous-type {record[5]}");

                *(ushort*)(record + 0x96) = 0;
                *(uint*)record &= 0xffffff8f;
            }

            LinkIntoWorldGrid(mainMem, 0x1c4000, record);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e680 @ 0x0050e680 — la passe sur la table <c>DAT_005ecaa0</c>, 0x20
    /// enregistrements. La plus fournie des six : c'est elle qui porte le <b>clignotement</b>, la
    /// <b>poursuite de cible</b> et l'<b>evanouissement</b> des entites de scene.
    ///
    /// <para>
    /// Le compte a rebours de <c>+0x96</c> est un clignotement : sa parite choisit entre deux
    /// couples (mode 1 / blanc et mode 5 / rouge) ecrits a <c>+0x36</c> et <c>+0x38</c>. Quand il
    /// atteint zero et que l'entite portait <c>0x40000000</c>, elle est rendue au pool et la passe
    /// saute a l'enregistrement suivant — le <c>goto LAB_0050eaf7</c> de Ghidra, rendu ici par un
    /// <c>continue</c>.
    /// </para>
    /// <para>
    /// <b>La boucle de poursuite est ecrite telle quelle, y compris son inefficacite.</b> Elle
    /// balaie tout le groupe et reaffecte la cible a <b>chaque</b> membre eligible rencontre, sans
    /// s'arreter au premier : c'est donc le dernier qui gagne. Rien dans le binaire n'interrompt la
    /// boucle, et ce n'est pas transcrit autrement.
    /// </para>
    /// <para>
    /// Le bloc central publie onze champs de l'entite dans des globales, appelle le gestionnaire de
    /// la table 0x0056ad48, puis les relit — mais seulement si <c>+0x90</c> porte l'un des bits de
    /// <c>0x2000a88a</c>. C'est une convention d'appel par globales, comme celle deja rencontree
    /// autour de <c>DAT_005fa4e0</c>.
    /// </para>
    /// </summary>
    private static void TickEntityTable_005ecaa0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005ecaa0);

        for (int i = 0; i < 0x20; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if ((*(uint*)record & 0x4000008) == 0 && s_Unknown_00571a3c == 0)
            {
                if (s_Unknown_00564540 == 0)
                {
                    s_Unknown_00564540 = LegacyPlus.Coutures.DelaiRencontre((CrtRand() % 6 + 10) * 0x14);   // Couture legacy ++ (LP-007)
                }

                if (*(ushort*)(record + 0x96) != 0)
                {
                    ushort blink = (ushort)(*(ushort*)(record + 0x96) - 1);
                    *(ushort*)(record + 0x96) = blink;

                    if ((blink & 1) == 0)
                    {
                        *(ushort*)(record + 0x36) = 1;
                        *(uint*)(record + 0x38) = 0xffffff;
                    }
                    else
                    {
                        *(ushort*)(record + 0x36) = 5;
                        *(uint*)(record + 0x38) = 0xff;
                    }

                    if ((*(uint*)record & 0x40000000) != 0)
                    {
                        *(ushort*)(record + 0x28) -= (ushort)(*(short*)(record + 0x28) >> 3);
                        *(ushort*)(record + 0x2c) -= (ushort)(*(short*)(record + 0x2c) >> 3);

                        if ((record[0x0d] & 0xf) == 0xb)
                        {
                            RunEntityScript(s_Unknown_005e97cc, record, *(ushort*)(record + 0x0e));
                            record[0x0d] = 0xff;
                        }
                    }

                    if (*(ushort*)(record + 0x96) == 0)
                    {
                        uint state = *(uint*)record;
                        *(uint*)record = state & 0xffffffbf;

                        if ((state & 0x40000000) != 0)
                        {
                            if ((state & 0x200000) == 0)
                            {
                                Aud.PlayPositionalSound(4, 0x400, 0x1800, (int*)(record + 0x1c));
                                DespawnSceneEntity_0051a740(record);
                            }

                            *(uint*)record &= 0x7fffffff;
                            FreeEntitySlot(Mem.Entity(0x005f2d00), record[8]);
                            continue;
                        }
                    }
                }

                // La cible tenue a +0x74 est oubliee des qu'elle cesse d'etre active.
                byte* watched = *(byte**)(record + 0x74);
                if (watched != null && (*(uint*)watched & 0x80000000) == 0)
                {
                    *(byte**)(record + 0x74) = null;
                }

                byte* chased = *(byte**)(record + 0x6c);
                if (chased != null && (*(uint*)chased & 0x50000000) != 0)
                {
                    uint candidate = chased[8];
                    for (int n = s_PartySize_005e9020; n != 0; n--)
                    {
                        if ((*(Inp.s_SlotFlags_005e93c0 + candidate * 0x9c + 3) & 0x50) == 0)
                        {
                            *(byte**)(record + 0x6c) = Inp.s_SlotFlags_005e93c0 + candidate * 0x9c;
                        }

                        candidate = (uint)((int)(candidate + 1) % s_PartySize_005e9020);
                    }
                }

                uint flags = *(uint*)record;
                if ((flags & 0x10000000) == 0)
                {
                    if ((flags & 0x40000000) == 0)
                    {
                        PublishSceneEntityToGlobals(record);

                        DispatchEntityHandler(
                            Res.ResolveEntityHandler(0x0056ad48, record[5]), record,
                            $"FUN_0050e680, table 0x005ecaa0 entree {i}, sous-type {record[5]}");

                        *(uint*)record |= 0x600;

                        byte* stats = GetEntityStatBlock((uint*)record);
                        if ((stats[0x40] & 2) == 0 && *(uint*)Mem.Entity(0x005f2cf0) % 100 == 0)
                        {
                            // La regeneration lente : un seizieme du maximum tous les cent tours,
                            // borne au maximum.
                            short maximum = *(short*)(stats + 0x4a);
                            *(short*)(stats + 0x4e) += (short)(maximum + 0xf >> 4);
                            if (maximum < *(short*)(stats + 0x4e)) *(short*)(stats + 0x4e) = maximum;
                        }

                        AdvanceSceneEntityAnimation_0053aa10(record);

                        if ((*(uint*)(record + 0x90) & 0x2000a88a) != 0)
                        {
                            ReadSceneEntityBackFromGlobals(record);
                        }
                    }
                }
                else
                {
                    // L'evanouissement, dans un sens ou dans l'autre selon le bit 0x8000000 :
                    // l'echelle rejoint 0x1000 en se divisant par deux, ou tombe vers zero.
                    short scale = *(short*)(record + 0x28);
                    if ((flags & 0x8000000) == 0)
                    {
                        scale = (short)(scale + (short)(0x1000 - scale >> 1));
                        *(short*)(record + 0x28) = scale;
                        *(short*)(record + 0x2c) = scale;

                        scale = (short)(*(short*)(record + 0x2a) + (short)(0x1000 - *(short*)(record + 0x2a) >> 1));
                        *(short*)(record + 0x2a) = scale;

                        if (scale < 0x1003)
                        {
                            *(uint*)(record + 0x38) = 0xffffff;
                            *(uint*)record &= 0xefffffff;
                        }
                        else
                        {
                            *(int*)(record + 0x38) += 0x80808;
                        }
                    }
                    else
                    {
                        scale = (short)(scale - (scale >> 1));
                        *(short*)(record + 0x28) = scale;
                        *(short*)(record + 0x2c) = scale;

                        scale = (short)(*(short*)(record + 0x2a) + (short)(0x4000 - *(short*)(record + 0x2a) >> 1));
                        *(short*)(record + 0x2a) = scale;

                        if (scale > 0x3ffd)
                        {
                            *(uint*)record &= 0x7fffffff;
                            FreeEntitySlot(Mem.Entity(0x005f2d00), record[8]);
                            continue;
                        }

                        *(int*)(record + 0x38) += -0x80808;
                    }
                }

                // La distance a la camera decide du bit de proximite, et un compte a rebours
                // d'eloignement finit par declencher un son et deux bits d'etat.
                int dx = (*Mem.Camera(0x005de068) - *(int*)(record + 0x1c)) >> 0xc;
                int dz = (*Mem.Camera(0x005de070) - *(int*)(record + 0x24)) >> 0xc;
                int distance = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

                if (distance < 600)
                {
                    *(uint*)record |= 0x100;
                    if (*(ushort*)(record + 0x98) != 0) *(ushort*)(record + 0x98) = 200;
                }
                else
                {
                    *(uint*)record &= 0xfffffeff;
                    if (*(ushort*)(record + 0x98) != 0)
                    {
                        (*(ushort*)(record + 0x98))--;
                        if (*(ushort*)(record + 0x98) == 0)
                        {
                            *(uint*)record |= 0x18000000;
                            Aud.PlayPositionalSound(0x1f, 0x800, 0x2000, Mem.Camera(0x005de068));
                        }
                    }
                }
            }

            if ((*(uint*)record & 0x80000000) != 0)
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }

    /// <summary>
    /// Ghidra: les onze globales que <see cref="TickEntityTable_005ecaa0"/> remplit avant d'appeler
    /// le gestionnaire de la table 0x0056ad48, et relit apres. Extraites en deux methodes parce que
    /// la pseudo-C entrelace les onze ecritures de la relecture avec des temporaires renommes, ce
    /// qui la rend illisible sans rien changer au sens.
    ///
    /// <para><c>DAT_0060083c</c> n'est ecrit qu'en moitie basse (un <c>ushort</c>) et relu en
    /// entier : c'est bien ce que fait l'original.</para>
    /// </summary>
    private static void PublishSceneEntityToGlobals(byte* record)
    {
        *(uint*)Mem.Entity(0x005fa4e0) = *(uint*)(record + 0x1c);
        *(uint*)Mem.Entity(0x005fa4e4) = *(uint*)(record + 0x20);
        *(uint*)Mem.Entity(0x005fa4e8) = *(uint*)(record + 0x24);
        *(uint*)Mem.Arena(0x005e99c4) = *(uint*)(record + 0x7c);
        *(ushort*)Mem.Entity(0x0060083c) = *(ushort*)(record + 0x90);
        *(uint*)Mem.Arena(0x005e9b4c) = *(uint*)(record + 0x80);
        *(uint*)Mem.Arena(0x005e9b48) = *(uint*)(record + 0x8c);
        *(uint*)Mem.Arena(0x005e9b50) = *(uint*)(record + 0x84);
        *(uint*)Mem.Arena(0x005e9b44) = *(uint*)(record + 0x88);
        *(uint*)Mem.Entity(0x005ff3e0) = *(uint*)(record + 0x60);
        *(uint*)Mem.Entity(0x005ff3ec) = *(uint*)(record + 0x64);
    }

    /// <summary>Le retour des onze globales dans l'entite, plus la negation des deux vitesses de
    /// <c>+0x50</c> et <c>+0x58</c> — le rebond.</summary>
    private static void ReadSceneEntityBackFromGlobals(byte* record)
    {
        *(uint*)(record + 0x7c) = *(uint*)Mem.Arena(0x005e99c4);
        *(uint*)(record + 0x1c) = *(uint*)Mem.Entity(0x005fa4e0);
        *(uint*)(record + 0x20) = *(uint*)Mem.Entity(0x005fa4e4);
        *(uint*)(record + 0x90) = *(ushort*)Mem.Entity(0x0060083c);
        *(uint*)(record + 0x80) = *(uint*)Mem.Arena(0x005e9b4c);
        *(uint*)(record + 0x84) = *(uint*)Mem.Arena(0x005e9b50);
        *(uint*)(record + 0x24) = *(uint*)Mem.Entity(0x005fa4e8);
        *(uint*)(record + 0x8c) = *(uint*)Mem.Arena(0x005e9b48);
        *(uint*)(record + 0x60) = *(uint*)Mem.Entity(0x005ff3e0);
        *(uint*)(record + 0x88) = *(uint*)Mem.Arena(0x005e9b44);
        *(uint*)(record + 0x64) = *(uint*)Mem.Entity(0x005ff3ec);

        *(int*)(record + 0x50) = -*(int*)(record + 0x50);
        *(int*)(record + 0x58) = -*(int*)(record + 0x58);
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005646e0</c> et <c>&amp;DAT_005646e8</c> — les deux descripteurs du butin,
    /// huit octets chacun, releves dans le binaire. Meme forme d'enregistrement que
    /// <see cref="s_LevelUpEffect_005648A8"/>, et la donnee qui suit (0x005646f0) n'a plus cette
    /// forme : ce sont bien deux enregistrements isoles, l'un pour ce qui tombe, l'autre pour les
    /// eclats qui l'accompagnent.
    /// </summary>
    private static readonly byte[] s_LootDescriptors_005646E0 =
    [
        0x18, 0x90, 0x17, 0x17, 0x01, 0x00, 0x11, 0x06,   // 0x005646e0
        0x30, 0x90, 0x17, 0x17, 0x01, 0x00, 0x11, 0x07,   // 0x005646e8
    ];

    private static byte* s_LootDescriptorStorage;

    /// <summary>Le descripteur en memoire native : l'entite nee le garde par pointeur
    /// (<c>+0x3c</c>). <paramref name="which"/> vaut 0 pour 0x005646e0, 1 pour 0x005646e8.</summary>
    private static byte* LootDescriptor(int which)
    {
        if (s_LootDescriptorStorage == null)
        {
            s_LootDescriptorStorage = (byte*)NativeMemory.Alloc((nuint)s_LootDescriptors_005646E0.Length);
            for (int i = 0; i < s_LootDescriptors_005646E0.Length; i++)
            {
                s_LootDescriptorStorage[i] = s_LootDescriptors_005646E0[i];
            }
        }

        return s_LootDescriptorStorage + which * 8;
    }

    /// <summary>
    /// Ghidra: FUN_0051a740 @ 0x0051a740 — <b>la mise a mort d'une entite de scene, et ce qu'elle
    /// laisse tomber</b>. Appelee par <see cref="TickEntityTable_005ecaa0"/> quand le clignotement
    /// d'une entite sans le bit <c>0x200000</c> expire, juste apres le son de disparition.
    ///
    /// <para>
    /// Elle prend un emplacement dans le pool des objets tombes (<c>0x005f7b60</c> / table
    /// <c>0x005f7de0</c>, 0x40 entrees), y <b>recopie l'entite morte</b> — d'ou la position et
    /// l'orientation du butin, qui naissent la ou la bete est tombee — puis lui donne une vitesse
    /// verticale pour le faire sauter.
    /// </para>
    /// <para>
    /// <b>Le tirage du butin.</b> <c>modele + 0x22</c> est la chance de l'espece, et la
    /// <b>septieme caracteristique</b> du tueur (<c>+0x2ac</c>) y ajoute sa moitie — la meme valeur
    /// qui mene toute la progression dans <see cref="LevelUpCharacter_005391F0"/>. Si
    /// <c>rand() % 1000</c> depasse ce total, il ne tombe que de l'argent (descripteur
    /// 0x005646e0, drapeaux <c>0x80081000</c>) ; sinon c'est un objet (drapeaux <c>0x80089000</c>),
    /// dont la <b>classe</b> se tire en <c>0xb4..0xb9</c> par une cascade de cinq seuils — a moins
    /// qu'un second tirage, mene lui aussi par la chance du tueur, ne la ramene a zero.
    /// </para>
    /// <para>
    /// <b>Puis zero a trois eclats</b> (<c>rand() &amp; 3</c>, descripteur 0x005646e8, drapeaux
    /// <c>0x90081000</c>) partent en etoile autour du butin. La boucle s'arrete aussi des que le
    /// pool est plein — un butin peut donc tomber sans ses eclats, jamais l'inverse.
    /// </para>
    /// <para>
    /// TODO(REVERSE): <b>le tueur est lu en <c>+9</c></b> et non en <c>+8</c> comme partout
    /// ailleurs (<see cref="ReadSceneEntitySituation_00535F20"/> prend <c>chased[8]</c>). Les deux
    /// octets sont distincts ; celui-ci n'a pas d'ecrivain identifie dans ce qui est transcrit.
    /// Transcrit tel quel — si le butin se met a dependre du mauvais personnage, c'est ici.
    /// </para>
    /// </summary>
    /// <returns>1 si un emplacement etait libre, 0 sinon. L'appelant ignore la valeur.</returns>
    private static uint DespawnSceneEntity_0051a740(byte* record)
    {
        int index = AllocEntitySlot((ushort*)Mem.Entity(0x005f7b60));
        if (index < 0) return 0;

        byte* drop = Mem.Entity(0x005f7de0) + index * 0x9c;
        Mem.CopyDwords((uint*)record, (uint*)drop, 0x27);

        drop[0x08] = (byte)index;
        *(short*)(drop + 0x9a) = 0x5000;
        *(uint*)(drop + 0x38) = 0x1000000;
        *(uint*)(drop + 0x28) = 0x0c000c00;
        *(short*)(drop + 0x2c) = 0x0c00;
        *(short*)(drop + 0x0a) = 0;
        *(int*)(drop + 0x50) = 0;

        int hop = CrtRand();
        *(int*)(drop + 0x58) = 0;
        *(int*)(drop + 0x54) = (hop % 10 + 0x19) * 0x1000;

        byte* killer = GetMainMemPtr() + (record[9] + 0x78) * 0x2000;

        ResolveEntityBlocks(record, out byte* stats, out byte* species, out byte* model);

        *(uint*)(drop + 0x5c) = *(ushort*)(stats + 0x44);

        // L'ecart de niveau entre le tueur et la bete, borne a 0x2c, decale le tirage de colonne.
        int gap = *(ushort*)(killer + 0x146) - *(ushort*)(stats + 0x44);
        if (gap < 0) gap = 0;
        if (gap > 0x2c) gap = 0x2c;

        drop[0x07] = (byte)PickWeightedTableEntry(species[5], gap + 0x38);   // Ghidra: FUN_0053a920

        short luck = *(short*)(killer + 0x2ac);
        ushort odds = *(ushort*)(model + 0x22);

        if (odds + (luck >> 1) < CrtRand() % 1000)
        {
            // ---- rien que de l'argent ----
            int life = CrtRand();
            *(uint*)(drop + 0x14) = 0;
            *(byte**)(drop + 0x3c) = LootDescriptor(0);
            drop[0x40] = 0;
            *(uint*)drop = 0x80081000;
            *(int*)(drop + 0x10) = life % 0x1e + 200;
        }
        else
        {
            // ---- un objet ----
            int life = CrtRand();
            *(uint*)(drop + 0x14) = 0;
            *(uint*)drop = 0x80089000;
            *(int*)(drop + 0x10) = life % 0x1e + 300;

            int quality = (*(short*)(killer + 0x12c) >> 3) + 0x3c + (*(short*)(killer + 0x2ac) >> 1);
            if (quality > 0x5f) quality = 0x5f;

            if (CrtRand() % 100 < quality)
            {
                drop[0x06] = 0;
            }
            else
            {
                int roll = CrtRand() % 100;
                int kind = roll > 0x22 ? 0xb5 : 0xb4;
                if (roll > 0x3b) kind++;
                if (roll > 0x4a) kind++;
                if (roll > 0x56) kind++;
                if (roll > 0x5e) kind++;
                drop[0x06] = (byte)kind;
            }
        }

        // ---- les eclats ----
        uint sparkles = (uint)CrtRand() & 3;

        while (sparkles != 0)
        {
            int slot = AllocEntitySlot((ushort*)Mem.Entity(0x005f7b60));
            if (slot < 0) break;

            byte* sparkle = Mem.Entity(0x005f7de0) + slot * 0x9c;
            Mem.CopyDwords((uint*)record, (uint*)sparkle, 0x27);

            sparkle[0x08] = (byte)slot;
            *(short*)(sparkle + 0x9a) = 0x5000;
            *(uint*)(sparkle + 0x38) = 0x1000000;
            *(uint*)(sparkle + 0x5c) = *(ushort*)(stats + 0x44);
            *(uint*)(sparkle + 0x28) = 0x0c000c00;
            *(short*)(sparkle + 0x2c) = 0x1000;
            *(short*)(sparkle + 0x0a) = 0;

            int angle = CrtRand();
            int speed = (CrtRand() & 7) + 0x10;

            *(int*)(sparkle + 0x50) = (int)Winsys3d.GetSinTableEntry(angle) * speed;
            *(int*)(sparkle + 0x54) = (CrtRand() % 10 + 0x19) * 0x1000;
            *(int*)(sparkle + 0x58) = Winsys3d.GetCosTableEntry(angle) * speed;

            int life = CrtRand();
            *(uint*)(sparkle + 0x14) = 0;
            *(byte**)(sparkle + 0x3c) = LootDescriptor(1);
            sparkle[0x40] = 0;
            *(uint*)sparkle = 0x90081000;

            sparkles--;
            *(int*)(sparkle + 0x10) = life % 0x1e + 200;
        }

        return 1;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056d340</c> — <b>quarante-huit masques d'etat, un par minuteur</b>,
    /// releves dans le binaire le 2026-08-23. Le bloc de statistiques porte quarante-huit
    /// <c>short</c> a partir de <c>+0x80</c> ; quand le minuteur d'indice <i>i</i> retombe a zero,
    /// le bit que donne <c>masques[i]</c> est efface du mot d'etat <c>+0x40</c>.
    ///
    /// <para>La borne vient du code : la boucle avance de quatre en quatre <b>tant que l'adresse
    /// reste sous <c>0x0056d400</c></b>, soit 0x30 entrees. Onze d'entre elles valent zero — leur
    /// minuteur decompte sans rien lever a l'echeance, il n'agit que tant qu'il est non nul (c'est
    /// le cas des trois regenerations traitees a part dans la boucle).</para>
    /// </summary>
    private static readonly uint[] s_StatusTimerMasks_0056d340 =
    [
        0x08000000, 0x02000000, 0x04000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000004,
        0x00000008, 0x00000010, 0x00000020, 0x00000040,
        0x00000080, 0x00000100, 0x00000200, 0x00000400,
        0x00000800, 0x00001000, 0x00002000, 0x00004000,
        0x00008000, 0x00010000, 0x00020000, 0x00040000,
        0x00080000, 0x00100000, 0x00200000, 0x10000000,
        0x00400000, 0x00000000, 0x00000000, 0x00000000,
        0x00000000, 0x01000000, 0x00000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000000,
        0x00000000, 0x00000000, 0x00000000, 0x00000000,
    ];

    /// <summary>
    /// Ghidra: FUN_0047ac40 @ 0x0047ac40 — <b>fait naitre une entite dans la table
    /// <c>DAT_005f2d40</c></b> (0x80 emplacements), en recopiant les 0x27 dwords de la source puis
    /// en remplacant une dizaine de champs. Troisieme jumelle de <see cref="SpawnEffectEntity"/>
    /// (<c>FUN_0047a9d0</c>) et de <see cref="SpawnCastEntity_0047AAE0"/> : meme forme, autre table.
    ///
    /// <para>Elle prend six arguments la ou les deux autres en prennent quatre : la <b>position</b>
    /// est passee separement (elle ecrase <c>+0x1c</c>/<c>+0x20</c>/<c>+0x24</c> de la copie) et un
    /// <b>descripteur d'atlas</b> part dans <c>+0x3c</c>.</para>
    /// </summary>
    /// <param name="param6">Negatif veut dire « ne pas ecrire <c>+0x0a</c> » — la seule branche
    /// conditionnelle de la fonction.</param>
    /// <returns>1 si un emplacement etait libre, 0 sinon.</returns>
    private static uint SpawnStatusEffectEntity_0047AC40(
        byte* source, int* position, byte* descriptor, byte subtype, byte param5, int param6)
    {
        int index = AllocEntitySlot((ushort*)Mem.Entity(0x005f7ce0));
        if (index < 0) return 0;

        byte* born = Mem.Entity(0x005f2d40) + index * 0x9c;
        Mem.CopyDwords((uint*)source, (uint*)born, 0x27);

        *(int*)(born + 0x1c) = position[0];
        *(int*)(born + 0x20) = position[1];
        *(int*)(born + 0x24) = position[2];

        born[0x08] = (byte)index;
        born[0x36] |= 0x80;
        born[0x05] = subtype;
        *(byte**)(born + 0x3c) = descriptor;
        born[0x0b] = param5;
        *(byte**)(born + 0x6c) = source;
        born[0x40] = 0;
        if (param6 >= 0) born[0x0a] = (byte)param6;
        *(ushort*)(born + 0x10) = 0;

        *(uint*)born = (*(uint*)born & 0x10e82f02) | 0x80101000;
        return 1;
    }

    /// <summary>
    /// Ghidra: FUN_0053aa10 @ 0x0053aa10 — <b>le pas d'etats d'une entite de scene</b>, appele sur
    /// chacune, une fois par image, juste apres son gestionnaire et la regeneration lente.
    ///
    /// <para>Malgre le nom que ce port lui avait donne d'avance, elle n'anime rien : elle fait
    /// vivre le <b>bloc de statistiques</b> que <see cref="GetEntityStatBlock"/> designe — poison,
    /// minuteurs d'etat, regenerations, et les deux seuils de zero.</para>
    ///
    /// <para><b>Trois cadences.</b> A chaque image : la teinte <c>+0x0b</c> de l'entite, prise dans
    /// la fiche de sous-zone pour une entite ordinaire et dans son propre enregistrement pour un
    /// membre du groupe, et forcee a <c>0xff</c> par le bit <c>0x100</c> (petrification). Toutes les
    /// <b>trente</b> images : les deux poisons et le drain, chacun retirant trois pour cent du
    /// maximum — <b>au moins un point</b> — et decomptant sa duree. Toutes les <b>vingt</b> images :
    /// les quarante-huit minuteurs d'etat, dont trois portent une regeneration au lieu d'un
    /// simple decompte.</para>
    ///
    /// <para>Un membre du groupe empoisonne repasse en plus par un declenchement d'objet equipe
    /// (<c>FUN_005238e0</c>) toutes les vingt images, precede de la remise a zero de
    /// <c>DAT_005e99c0</c>.</para>
    ///
    /// <para><b>Les deux seuils.</b> Points de vie a zero : le bit <c>0x40000000</c> part dans le
    /// bloc de statistiques <b>et</b> dans le mot d'etat de l'entite. Points de magie a zero, et
    /// seulement au passage : le bit <c>0x80</c>, un plancher de douze sur <c>+0x98</c>, une entite
    /// de sous-type <c>0x29</c> dans la table <c>DAT_005f2d40</c> et une copie dans la table des
    /// effets.</para>
    /// </summary>
    private static void AdvanceSceneEntityAnimation_0053aa10(byte* record)
    {
        uint* entity = (uint*)record;

        // La garde d'entree : ni mort (0x40000000) ni retire (0x10000000).
        if ((entity[0] & 0x50000000) != 0) return;

        byte* stats = GetEntityStatBlock(entity);

        if ((entity[0] & 0x800000) == 0)
        {
            // Entite ordinaire : la teinte vient de la fiche de sous-zone, huit octets par entree.
            record[0x0b] = (*(byte**)(s_Unknown_005e97cc + 0x4c))[record[0x0c] * 8 + 2];
        }
        else
        {
            // Membre du groupe : la teinte est dans son enregistrement de 0x2000 octets, et le
            // reveil d'un membre a terre y regagne un seizieme de son maximum.
            byte* character = GetMainMemPtr() + 0xf0000 + record[8] * 0x2000;
            record[0x0b] = character[0x2bd];

            if (character[0x206] == 0)
            {
                character[0x206] = 0x28;
                *(short*)(character + 0x14c) +=
                    (short)(*(short*)(character + 0x128) + 0x3f >> 6);
            }
        }

        // La petrification efface la teinte.
        if ((*(uint*)(stats + 0x40) & 0x100) != 0) record[0x0b] = 0xff;

        uint frame = *(uint*)Mem.Entity(0x005f2cf0);

        // ------------------------------------------------------------ toutes les trente images
        if (frame % 0x1e == 0)
        {
            if ((stats[0x40] & 1) != 0)
            {
                *(short*)(stats + 0x4c) -= ThreePercentAtLeastOne(*(short*)(stats + 0x48));
                *(short*)(stats + 0xe0) -= 1;
                if (*(short*)(stats + 0xe0) == 0) *(uint*)(stats + 0x40) &= 0xfffffffe;
            }

            if ((stats[0x40] & 2) != 0)
            {
                *(short*)(stats + 0x4e) -= ThreePercentAtLeastOne(*(short*)(stats + 0x4a));
                *(short*)(stats + 0xe2) -= 1;
                if (*(short*)(stats + 0xe2) == 0) *(uint*)(stats + 0x40) &= 0xfffffffd;
            }

            // La petrification draine cinq pour cent, sans duree : elle ne s'arrete pas d'elle-meme.
            if ((*(uint*)(stats + 0x40) & 0x100) != 0)
            {
                int drain = *(short*)(stats + 0x48) * 5 / 100;
                if (drain == 0) drain = 1;
                *(short*)(stats + 0x4c) -= (short)drain;
            }

            if ((*(uint*)(stats + 0x40) & 0x103) != 0)
            {
                Aud.PlayPositionalSound(5, 0xa00, 0x1800, (int*)(record + 0x1c));
                *(ushort*)(record + 0x96) = 8;
            }
        }

        // ------------------------------------------------------------ toutes les vingt images
        if (frame % 0x14 == 0)
        {
            short* timer = (short*)(stats + 0x80);

            for (int i = 0; i < s_StatusTimerMasks_0056d340.Length; i++, timer++)
            {
                if (*timer == 0) continue;

                *timer -= 1;

                if (*timer == 0)
                {
                    *(uint*)(stats + 0x40) &= ~s_StatusTimerMasks_0056d340[i];
                    if ((byte*)timer == stats + 0xc0) *(ushort*)(stats + 0xec) = 0;
                    continue;
                }

                // Trois minuteurs agissent **tant qu'ils tournent** : deux regenerations dont le
                // pas vit a +0xfc/+0xfe, et une troisieme a taux fixe.
                if ((byte*)timer == stats + 0xbc)
                {
                    *(short*)(stats + 0x4c) += *(short*)(stats + 0xfc);
                }
                if ((byte*)timer == stats + 0xbe)
                {
                    *(short*)(stats + 0x4e) += *(short*)(stats + 0xfe);
                }
                if ((byte*)timer == stats + 0xc2)
                {
                    *(short*)(stats + 0x4c) += 0x14;
                }
            }

            if ((entity[0] & 0x800000) != 0)
            {
                *(uint*)Mem.Arena(0x005e99c0) = 0;
                TriggerEquippedItems_005238E0(entity, 0);
            }
        }

        // ------------------------------------------------------------ les bornes et les deux seuils
        if (*(short*)(stats + 0x4c) > *(short*)(stats + 0x48))
        {
            *(ushort*)(stats + 0x4c) = *(ushort*)(stats + 0x48);
        }
        if (*(short*)(stats + 0x4e) > *(short*)(stats + 0x4a))
        {
            *(short*)(stats + 0x4e) = *(short*)(stats + 0x4a);
        }

        if (*(short*)(stats + 0x4c) < 1)
        {
            *(short*)(stats + 0x4c) = 0;
            *(uint*)(stats + 0x40) |= 0x40000000;
            entity[0] |= 0x40000000;
        }

        if (*(short*)(stats + 0x4e) < 1)
        {
            *(short*)(stats + 0x4e) = 0;

            // Le bit 0x80 fait office de verrou : tout ce bloc ne s'execute qu'au passage a zero.
            if ((stats[0x40] & 0x80) == 0)
            {
                *(uint*)(stats + 0x40) |= 0x80;
                if (*(ushort*)(stats + 0x98) < 0xc) *(ushort*)(stats + 0x98) = 0xc;

                SpawnStatusEffectEntity_0047AC40(
                    record, (int*)(record + 0x1c), SpriteAtlas(0x005648b0), 0x29, 0, 0);
                SpawnEffectEntity(record, 0x46, 0, 2);
            }
        }
    }

    /// <summary>Ghidra: le calcul de degat des deux poisons — <c>maximum * 3 / 100</c>, ramene a
    /// <b>un</b> quand la division rend zero. Ecrit deux fois a l'identique dans
    /// <c>FUN_0053aa10</c>.</summary>
    private static short ThreePercentAtLeastOne(short maximum)
    {
        int amount = maximum * 3 / 100;
        return amount == 0 ? (short)1 : (short)amount;
    }

    /// <summary>
    /// Ghidra: FUN_0050eb20 @ 0x0050eb20 — la passe sur la table <c>DAT_005f7de0</c>, 0x40
    /// enregistrements : les <b>projectiles</b>, ou du moins ce qui vole et retombe.
    ///
    /// <para>
    /// C'est la seule des six qui integre un mouvement : la position de <c>+0x1c</c>/<c>+0x20</c>/
    /// <c>+0x24</c> avance de la vitesse de <c>+0x50</c>/<c>+0x54</c>/<c>+0x58</c>, les composantes
    /// horizontales perdent un huitieme par image (une trainee) et la verticale recoit la gravite
    /// de <c>+0x9a</c>. <see cref="FinalizeEntityPlacement2"/> replace ensuite l'objet sur le
    /// terrain, et le rebond est le bloc <c>0x802</c> : les deux vitesses horizontales sont
    /// niees et huit champs sauvegardes a l'entree sont restitues.
    /// </para>
    /// <para>
    /// Deux fins possibles : la duree de vie de <c>+0x10</c> qui tombe a zero, ou une chute
    /// au-dessous de <c>-0x4e1ffff</c> — soit -20000 en unites monde. Les deux effacent le bit
    /// d'activite, et c'est le test <b>relu</b> en bas de tour qui rend alors l'emplacement.
    /// </para>
    /// <para>
    /// La teinte suit la duree de vie restante : au-dessous de douze images, les trois composantes
    /// valent <c>vie * 0x100</c>, donc l'objet s'eteint en gris ; au-dessus, elles sont figees a
    /// 0x0c00.
    /// </para>
    /// </summary>
    private static void TickEntityTable_005f7de0()
    {
        byte* mainMem = GetMainMemPtr();
        byte* record = Mem.Entity(0x005f7de0);

        for (int i = 0; i < 0x40; i++, record += 0x9c)
        {
            if ((*(uint*)record & 0x80000000) == 0) continue;

            if (((byte)Mem.s_WorldFlags_005de02c & 0x10) == 0)
            {
                // Les huit champs que le rebond restituera.
                int saved7c = *(int*)(record + 0x7c);
                int saved60 = *(int*)(record + 0x60);
                int saved64 = *(int*)(record + 0x64);
                ushort saved90 = *(ushort*)(record + 0x90);
                int saved80 = *(int*)(record + 0x80);
                int saved84 = *(int*)(record + 0x84);
                int saved88 = *(int*)(record + 0x88);
                int saved8c = *(int*)(record + 0x8c);

                if ((*(uint*)record & 0x10008000) == 0)
                {
                    // La teinte pulse avec la duree de vie : un sinus ramene dans 0..0xff, puis
                    // repete sur les trois composantes.
                    int wave = (int)Winsys3d.GetSinTableEntry((int)*(short*)(record + 0x10) << 8);
                    uint grey = (uint)((wave >> 6) - 0x80) & 0xff;
                    *(int*)(record + 0x38) = (int)(((grey << 8 | grey) << 8) | grey);
                }

                int vx = *(int*)(record + 0x50);
                *(int*)(record + 0x1c) += vx;

                int vz = *(int*)(record + 0x58);
                int vy = *(int*)(record + 0x54);
                *(int*)(record + 0x24) += vz;
                *(int*)(record + 0x20) += vy;

                *(int*)(record + 0x50) = vx - (vx >> 3);
                *(int*)(record + 0x54) =
                    (((int)*(short*)(record + 0x9a) >> 8) - (vy >> 8)) - (int)*(short*)(record + 0x9a) + vy;
                *(int*)(record + 0x58) = vz - (vz >> 3);

                FinalizeEntityPlacement2((uint*)record);

                if ((*(uint*)(record + 0x90) & 0x802) != 0)
                {
                    *(int*)(record + 0x50) = -*(int*)(record + 0x50);
                    *(int*)(record + 0x58) = -*(int*)(record + 0x58);
                    *(int*)(record + 0x7c) = saved7c;
                    *(int*)(record + 0x60) = saved60;
                    *(int*)(record + 0x64) = saved64;
                    *(int*)(record + 0x80) = saved80;
                    *(int*)(record + 0x84) = saved84;
                    *(uint*)(record + 0x90) = saved90;
                    *(int*)(record + 0x88) = saved88;
                    *(int*)(record + 0x8c) = saved8c;
                }

                if (*(int*)(record + 0x20) < -0x4e1ffff)
                {
                    *(uint*)record &= 0x7fffffff;
                }
                else
                {
                    if (*(int*)(record + 0x20) <= *(int*)(record + 0x60)
                        && (*(uint*)(record + 0x90) & 0xa000) == 0)
                    {
                        // Le sol : la hauteur est ramenee au plancher et la vitesse verticale
                        // repart vers le haut, amortie d'un quart, puis annulee si elle est
                        // devenue plus petite que deux fois la gravite.
                        *(int*)(record + 0x20) = *(int*)(record + 0x60);

                        int bounce = *(int*)(record + 0x54);
                        if (bounce < 0) bounce = -bounce;
                        bounce = (bounce - (bounce >> 2)) - (int)*(short*)(record + 0x9a);
                        *(int*)(record + 0x54) = bounce;

                        if (bounce < *(short*)(record + 0x9a) * 2) *(int*)(record + 0x54) = 0;
                    }

                    short life = *(short*)(record + 0x10);
                    if (life < 0xc)
                    {
                        uint tint = (uint)((int)life << 8);
                        *(short*)(record + 0x2c) = (short)tint;
                        *(int*)(record + 0x28) = (int)((uint)((int)life << 0x18) | tint);
                    }
                    else
                    {
                        *(int*)(record + 0x28) = 0xc000c00;
                        *(ushort*)(record + 0x2c) = 0xc00;
                    }
                }

                (*(short*)(record + 0x10))--;
                if (*(short*)(record + 0x10) == 0) *(uint*)record &= 0x7fffffff;
            }

            if ((*(uint*)record & 0x80000000) == 0)
            {
                FreeEntitySlot(Mem.Entity(0x005f7b60), record[8]);
            }
            else
            {
                LinkIntoWorldGrid(mainMem, 0x1c5000, record);
            }
        }
    }
}
