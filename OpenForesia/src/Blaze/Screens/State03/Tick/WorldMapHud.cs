// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE DESSIN DE LA CARTE DU MONDE — le HUD, puis le monde.
//
// La rose des vents, les panneaux d'equipe et leurs jauges, le disque au sol, les bulles d'etat,
// la bulle de dialogue, puis `DrawWorldCells` qui pose le monde lui-meme et ses quatre listes de
// morceaux.
//
// TOUT CE FICHIER EST APPELE PAR IMAGE, depuis la branche d'affichage de `RunWorldMapTick`. C'est
// le test d'appartenance a `Tick/` : pas « ca dessine », mais « ca tourne a chaque image ».
//
// Le menu complet (`WorldMapMenu.cs`) et le coffre (`WorldMapDepot.cs`) prennent cette branche a
// leur compte quand ils sont ouverts, et le monde n'est alors plus dessine du tout.
//
// La camera est dans `WorldMapCamera.cs`, les descripteurs d'atlas dans `SpriteAtlas.cs`, les
// emetteurs de primitives dans `Engine/Primitives2d.cs`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_0053dcb0 @ 0x0053dcb0 — <b>absente de <c>Blaze.c</c></b> (Ghidra ne decompile pas
    /// une fonction dont seule l'adresse est prise), transcrite depuis
    /// <c>Legacy/Raw/Blaze/FUN_0053DCB0.asm</c>. Ouvre le pas de simulation : pour chaque membre du
    /// groupe, elle fait glisser la paire de mots de <c>+0x148</c> vers <c>+0x14c</c> — l'instantane
    /// « image precedente » que le bloc d'affichage relit — remet a zero le dword de <c>+0x140</c>
    /// et les 0x80 octets de <c>+0x180</c>.
    ///
    /// <para>Le bit 3 de <c>DAT_005de02c</c> qu'elle pose vient d'etre efface par son appelant, deux
    /// instructions plus haut. Transcrit tel quel.</para>
    /// </summary>
    private static void ResetPerCharacterFrameState()
    {
        byte* mainMem = GetMainMemPtr();

        *(byte*)Mem.Camera(0x005de044) = 0;
        Mem.s_WorldFlags_005de02c |= 8;

        for (int i = 0; i < s_PartySize_005e9020; i++)
        {
            byte* block = mainMem + 0xf0000 + 0x148 + i * 0x2000;
            *(ushort*)(block + 4) = *(ushort*)(block + 0);
            *(ushort*)(block + 6) = *(ushort*)(block + 2);
            *(int*)(block - 8) = 0;
            for (int k = 0; k < 0x20; k++) ((uint*)(block + 0x38))[k] = 0;
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e2b0 @ 0x0050e2b0 — le tri des membres du groupe en tete de chaque image :
    /// qui est jouable, qui est hors-jeu, et qui mene.
    ///
    /// <para>Trois compteurs en sortent, tous des octets (confirme au desassemblage — cinq
    /// <c>mov byte [.],0</c> a l'entree) : <c>DAT_005de034</c> le nombre de membres encore
    /// jouables, <c>DAT_005de544</c> le nombre de membres ecartes, et <c>DAT_005de518</c> ceux
    /// portant le bit <c>0x40000000</c>. Quand tous les membres sont ecartes ET tous portent ce
    /// bit, la partie bascule vers l'etat de jeu 9 avec un pas de boucle a <c>-2</c>.</para>
    ///
    /// <para>Deux index distincts sont suivis en parallele, et la pseudo-C les melange :
    /// <c>EBX</c> retient le dernier membre jouable rencontre (il devient le meneur quand il n'y
    /// en a qu'un), tandis que <c>[ESP+0x14]</c> retient le precedent pour l'empiler dans la liste
    /// <c>DAT_005edec0</c>. Tous deux partent de <c>DAT_005de538</c>.</para>
    ///
    /// <para>Placee ici et non dans <c>Engine/</c> : la carte du monde est aujourd'hui son unique
    /// appelant transcrit. Elle remontera quand <c>FUN_0050bf20</c> arrivera.</para>
    /// </summary>
    private static void PrepareLeaderAndPartyFlags()
    {
        byte* mainMem = GetMainMemPtr();

        s_Unknown_005de518 = 0;
        *(byte*)Mem.Camera(0x005de030) = 0;
        s_Unknown_005de544 = 0;
        *(byte*)Mem.Camera(0x005de034) = 0;
        *Mem.Entity(0x005f2ce4) = 0;

        // Les deux index suivis, tous deux amorces sur le meneur courant.
        uint lastPlayable = (uint)(byte)Mem.s_Unknown_005de538;
        uint previousPlayable = lastPlayable;

        // -1 tant qu'aucun membre « disponible » n'a ete vu ; sinon l'index du premier.
        int firstAvailable = -1;

        for (int index = 0; index < s_PartySize_005e9020; index++)
        {
            uint* flags = (uint*)(Inp.s_SlotFlags_005e93c0 + index * 0x9c);
            byte slotKind = Inp.s_PlayerSlots_005e9188[index * 0x4c];
            byte* pad = Inp.s_InputStateBase_00604a20 + slotKind * 0x10;
            byte* perCharacter = mainMem + 0xf02b8 + index * 0x2000;

            if ((pad[0xb] & 0x80) == 0)
            {
                // Manette absente : le membre est ecarte du jeu.
                *flags = (*flags & 0xf7ffffff) | 0x10000;
                *perCharacter = 1;
            }
            else if (firstAvailable == -1)
            {
                firstAvailable = index;
            }

            if (slotKind < 8)
            {
                uint value = *flags | 0x20000;
                *flags = value;
                if ((value & 0x10000) != 0 && (pad[0] & 0xf0) != 0)
                {
                    *flags = (value & 0xfffeffff) | 0x20000;
                    *perCharacter = 0;
                }

                value = *flags;
                if ((value & 0x10000) == 0)
                {
                    (*(byte*)Mem.Camera(0x005de034))++;
                    lastPlayable = (uint)index;
                }
                else if ((value & 0x40000000) != 0)
                {
                    *flags = value & 0xf7ffffff;
                }
            }
            else if ((byte)Mem.s_Unknown_005de538 != index)
            {
                *flags = (*flags & 0xf7ffffff) | 0x10000;
                *perCharacter = 1;
            }

            uint state = *flags;
            if ((state & 0x50200000) == 0 && *(short*)((byte*)flags + 0x12) == 0)
            {
                if ((state & 0x10000) != 0 && (*(uint*)(perCharacter - 0x178) & 0x1c0) == 0)
                {
                    Mem.Entity(0x005edec0)[*Mem.Entity(0x005f2ce4)] = (byte)previousPlayable;
                    (*Mem.Entity(0x005f2ce4))++;
                    previousPlayable = (uint)index;
                }
            }
            else
            {
                s_Unknown_005de544++;
                if ((state & 0x40000000) != 0) s_Unknown_005de518++;
                if ((state & 0x10200000) != 0) (*(byte*)Mem.Camera(0x005de030))++;
            }
        }

        if (*(byte*)Mem.Camera(0x005de034) == 0)
        {
            // Plus personne de jouable : on retombe sur le premier membre disponible, ou sur le
            // membre 0 si meme celui-la n'existe pas.
            byte fallback = firstAvailable < 0 ? (byte)0 : (byte)firstAvailable;
            s_Unknown_005de540 = fallback;
            Mem.s_Unknown_005de538 = fallback;

            *(uint*)(Inp.s_SlotFlags_005e93c0 + fallback * 0x9c) &= 0xfffeffff;
            *(mainMem + 0xf02b8 + fallback * 0x2000) = 0;
            *(byte*)Mem.Camera(0x005de034) = 1;
        }
        else if (*(byte*)Mem.Camera(0x005de034) == 1)
        {
            Mem.s_Unknown_005de538 = (byte)lastPlayable;
            s_Unknown_005de540 = (byte)lastPlayable;
        }

        if (*(byte*)Mem.Camera(0x005de030) >= s_PartySize_005e9020)
        {
            Mem.s_WorldFlags_005de02c |= 2;
            if (s_Unknown_005de518 >= s_PartySize_005e9020)
            {
                s_WorldMapStep_005645f8 = -2;
                s_GameStateId_005de510 = 9;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_0050e520 @ 0x0050e520 — passe le role de meneur au premier membre du groupe qui
    /// ne porte ni le masque demande ni les bits <c>0x400001c0</c> de son bloc par personnage.
    ///
    /// <para>Quand le membre retenu etait lui-meme ecarte (<c>0x10000</c>), la fonction le
    /// reintegre et <b>echange</b> deux octets — <c>+8</c> et <c>+9</c> de l'entree
    /// <c>DAT_005e9180</c> — entre l'ancien meneur et le nouveau. Ce sont l'emplacement de manette
    /// et l'index de personnage : c'est un echange de sieges, pas une copie.</para>
    ///
    /// <para>Trois autres appelants existent dans <c>Blaze.c</c> (<c>FUN_0050bf20</c> deux fois,
    /// <c>FUN_0052a850</c> une fois), aucun n'est encore transcrit. Elle rejoindra
    /// <c>Engine/</c> quand ce sera le cas.</para>
    /// </summary>
    private static void PassLeaderToNextEligible(uint* currentFlags, byte* currentSlot, uint mask)
    {
        // L'original teste ensuite `0 < DAT_005e9020` une seconde fois ; le test est mort, la garde
        // ci-dessus ayant deja rendu la main pour tout groupe de moins de deux membres.
        if (s_PartySize_005e9020 <= 1 || *(byte*)Mem.Camera(0x005de034) == 0) return;

        byte* mainMem = GetMainMemPtr();
        int index = 0;
        uint* flags = (uint*)Inp.s_SlotFlags_005e93c0;
        byte* slot = Inp.s_SlotArea_005e9180;
        uint* perCharacter = (uint*)(mainMem + 0xf0140);

        while ((*flags & mask) != 0 || (*perCharacter & 0x400001c0) != 0)
        {
            index++;
            flags = (uint*)((byte*)flags + 0x9c);
            slot += 0x4c;
            perCharacter = (uint*)((byte*)perCharacter + 0x2000);
            if (s_PartySize_005e9020 <= index) return;
        }

        Mem.s_Unknown_005de538 = (byte)index;
        *flags &= 0xf7ffffff;
        *currentFlags &= 0xf7ffffff;

        if ((*flags & 0x10000) != 0)
        {
            *flags &= 0xfffeffff;

            byte previousIndex = currentSlot[9];
            byte previousPad = currentSlot[8];
            currentSlot[8] = slot[8];
            currentSlot[9] = slot[9];
            slot[8] = previousPad;
            slot[9] = previousIndex;
        }
    }

    // =====================================================================
    // Les scalaires isoles que la boucle par image touche hors arene.
    // =====================================================================

    // =====================================================================
    // Les descripteurs d'atlas de la moitie dessin, releves dans le binaire (DataSymbols.md).
    //
    // Huit octets chacun : dword0 empaquette l'origine et la taille de cellule, dword1 part tel
    // quel comme moitie « attributs » du sprite. Ils sont poses ici plutot que dans GameMemory
    // parce qu'un seul ecran les lit — meme regle que s_WorldMapDescriptor_005633b0.
    // =====================================================================

    /// <summary>
    /// Ghidra: <c>0x00517e7c</c>..<c>0x00519703</c> — <b>la moitie dessin du bloc d'affichage</b>.
    /// Elle emet tout ce que la carte du monde montre : le voile de transition, les deux
    /// indicateurs d'ecran, le panneau d'interface, la rose des vents, les quatre panneaux de
    /// membre avec leurs modeles, le balayage 7x7 des cellules de decor, et les trois listes de
    /// morceaux.
    ///
    /// <para>
    /// <b>Deux objets suffisent a lire tous les appels.</b> Ghidra repand ici six noms —
    /// <c>unaff_EBX</c>, <c>unaff_ESI</c>, <c>unaff_EDI</c>, <c>unaff_EBP</c>, <c>local_60</c>,
    /// <c>piStack_74</c> — qui ne designent que la <b>base d'ordering table</b> (<c>[esp+0x14]</c>)
    /// et le <b>curseur de primitives</b> (<c>[esp+0x10]</c>), plus <c>uStack_54</c> pour l'entree
    /// d'ordering table (<c>[esp+0x20]</c>). Chaque appel de dessin prend <c>(base + N, curseur,
    /// …)</c> et <b>rend le nouveau curseur</b>, que l'appelant range aussitot. C'est le seul
    /// invariant qui compte : un curseur non rechaine ne plante pas ici mais dans winsys3d, sans
    /// pile utilisable.
    /// </para>
    /// <para>
    /// Le decalage <c>+ N</c> sur la base choisit la <b>couche</b> : <c>+8</c> l'interface,
    /// <c>+0xc</c> les etiquettes, <c>+0x14</c> les ombres, <c>-0x14</c> l'arriere-plan, et un
    /// decalage lu dans l'entite pour les particules.
    /// </para>
    /// </summary>
    private static void DrawWorldMapFrame(
        uint* otBase, uint* otEntry, uint** cursorSlot, int debugLatch, byte* mainMem)
    {
        uint* cursor = *cursorSlot;

        // Le bloc de **sept champs sur 0x12 octets** que toutes les fonctions de dessin recoivent
        // par adresse — voir la disposition complete sur Primitives2d.DrawSpritePrimitive. Cinq
        // entiers, pas trois : la premiere version en allouait trois, et DrawSpritePrimitive lisait
        // deja l'echelle et l'indice six octets au-dela.
        int* block = stackalloc int[5];
        byte* b = (byte*)block;

        // Le champ +0x04 — le « mot de base » — part avec ce que l'image PRECEDENTE a laisse dans
        // cet emplacement de pile, et cette valeur est connue : `0x0051833d` y ecrit
        // `mov dword [esp+38h], 1000000h`, et les autres sous-blocs de la fonction font de meme.
        // A la fin de chaque image, l'emplacement vaut donc 0x1000000 ; l'image suivante le relit
        // tel quel dans les deux sous-blocs qui ne l'ecrivent pas — le compteur de mise au point et
        // l'indicateur d'ecran.
        //
        // Ce n'est pas une valeur inventee « par coherence avec les voisins », et ce n'est pas
        // benin : le bit 24 du mot de base tombe dans l'octet de commande de la primitive
        // (`prim[1] >> 24`), et fait passer winsys3d de la commande 0x2c (`FUN_00a0f570`, qui teinte
        // les quatre sommets par les 24 bits bas du meme mot) a la commande 0x2d (`FUN_00a0f780`,
        // qui les met a 0xffffffff, donc la texture sans teinte). Un mot de base nul, c'est la
        // commande 0x2c avec une teinte noire : un carre noir opaque en haut a droite de l'ecran.
        // Voir legacy-bug.md LB-008, corrige le 2026-08-26 apres l'avoir vu en jeu a l'auberge.
        //
        // Seule la toute premiere image de la partie lit autre chose dans l'original — ce que le
        // demarrage avait laisse la. On ne modelise pas ce cas : il n'est pas determinable.
        block[1] = 0x1000000;

        // ---------------------------------------------------------- le voile de transition
        if ((byte)(Mem.s_Unknown_005de024 >> 0x18) != 0)
        {
            cursor = FillScreenQuad_0051F940(otBase, cursor, Mem.s_Unknown_005de024, 0, 0, 0x140, 0x100);
        }

        // ---------------------------------------------------------- le compteur de mise au point
        if (*(byte*)Mem.Camera(0x005de0b4) != 0)
        {
            (*(byte*)Mem.Camera(0x005de0b4))--;

            block[0] = (int)(nint)SpriteAtlas(0x00566c28);
            block[2] = (0x10 << 0x10) | 8;
            cursor = Prims.DrawTextRun(otBase, cursor, block, Mem.Entity(0x005fa5d0), 0, 0, 0x10);

            block[2] += 10 << 0x10;
            cursor = Prims.DrawTextRun(otBase, cursor, block, s_HpLabel_00564a14, 0, 0, -1);

            block[2] = ((block[2] >> 0x10) << 0x10) | (((block[2] & 0xffff) + 0x12) & 0xffff);
            block[0] = (int)(nint)SpriteAtlas(0x00566c30);
            cursor = Prims.DrawDecimalNumber(
                otBase, cursor, block, (uint)*(short*)Mem.Arena(0x005e9a22), 5, 0);
        }

        // ---------------------------------------------------------- les deux indicateurs d'ecran
        s_Unknown_005de15c = 1;
        s_Unknown_005de154 = 0;
        s_Unknown_005de158 = 0;

        UiWin.EmitViewportRect(otBase + 2, &cursor);
        UiWin.EmitClipRect(otBase + 2, &cursor);

        s_Unknown_005de15c = 0;

        byte* indicator = null;
        if (s_Unknown_005de54c == 2) indicator = SpriteAtlas(0x00564788);
        if (s_Unknown_005de54c == 1 && s_Unknown_005de550 == 2) indicator = SpriteAtlas(0x00564790);

        if (indicator != null)
        {
            // L'indicateur pulse : le sinus du compteur de fondu module son **echelle**, dans les
            // deux axes, autour de 0x1000 (= 1.0). La position (0x130, 0x10) ne bouge pas.
            //
            // **Ghidra se trompe ici aussi**, et dans l'autre sens que la rose des vents : il rend
            // le premier pouls par `uStack_40 = CONCAT22(uStack_40._2_2_, pouls)`, ce qui ecraserait
            // le X par une valeur de l'ordre de 0x1000 — hors ecran. Le desassemblage donne
            // `[esp+3Ch]=130h`, `[esp+3Eh]=10h`, `[esp+40h]=pouls`, `[esp+46h]=pouls` avec le bloc
            // a `esp+34h` : position conservee, pouls dans +0x0c **et** +0x0e. Releve le 2026-08-22.
            block[0] = (int)(nint)indicator;
            *(short*)(b + 8) = 0x130;
            *(short*)(b + 0xa) = 0x10;
            *(short*)(b + 0xc) = (short)(((int)Winsys3d.GetSinTableEntry(
                (int)(s_FadeCounter_005e9960 << 9)) >> 3) + 0x1000);
            *(short*)(b + 0xe) = (short)(((int)Winsys3d.GetSinTableEntry(
                (int)(s_FadeCounter_005e9960 << 9)) >> 3) + 0x1000);
            *(short*)(b + 0x10) = 0;

            // +0x04 n'est **pas** ecrit ici par l'original — il relit ce que l'image precedente a
            // laisse, soit 0x1000000, pose en haut de cette fonction. Voir LB-008.
            cursor = Prims.DrawSpritePrimitive(otBase, cursor, block, 0, 0);
        }

        // ---------------------------------------------------------- le panneau d'interface
        s_Unknown_005de15c = 1;
        s_Unknown_005de154 = s_MenuSlideDone_00564a56 == 0 && Mem.s_SizeSetting_005de160 > 7 ? 0 : 0xa0;
        s_Unknown_005de158 = (10 - Mem.s_SizeSetting_005de160) * 0xc;

        if (s_Unknown_00563c5c != 0)
        {
            cursor = Ui.RunTextEngine(cursor, s_WorldMapDescriptor_005633b0);
        }

        // Le panneau glisse vers -0x50 ou -0x40 selon qu'il est ouvert, d'un quart par image, et
        // ne se redessine que tant qu'il bouge.
        short before = *(short*)(s_WorldMapDescriptor_005633b0 + 6);
        *(short*)(s_WorldMapDescriptor_005633b0 + 6) +=
            (short)((((s_Unknown_00563c77 & 1) != 0 ? 0x70 : 0) - 0x50 - before) >> 2);

        if ((s_Unknown_00563c77 & 1) == 0 || before != *(short*)(s_WorldMapDescriptor_005633b0 + 6))
        {
            s_Unknown_00563c77 |= 0x20;
        }
        else
        {
            s_Unknown_00563c77 &= 0xdf;
        }

        if ((s_Unknown_00563c77 & 1) != 0 || before != *(short*)(s_WorldMapDescriptor_005633b0 + 6))
        {
            cursor = UiWin.DrawUiPanel(otBase, cursor, s_WorldMapDescriptor_005633b0);
        }

        s_Unknown_005de154 = 0;
        s_Unknown_005de158 = 0;
        s_Unknown_005de15c = 0;

        cursor = LegacyPlus.Coutures.DessinSurImpression(otBase, cursor);   // Couture legacy ++ (LP-008)

        if (debugLatch == 0 && s_DebugOverlayEnabled_005de020 != 0)
        {
            // Le journal MONITOR (0x0051815a..0x00518473) : les bits qui ont bouge depuis la
            // recopie de l'en-tete, dans une boite. Voir WorldMapDebugMenu.cs.
            cursor = DrawDebugMonitorChanges(otBase, cursor, block);
        }

        DrawCompassRose(otBase, otEntry, ref cursor, block);
        DrawPartyPanels(otBase, ref cursor, block, mainMem);
        DrawWorldCells(otBase, otEntry, ref cursor, mainMem);

        *cursorSlot = cursor;
    }

    /// <summary>
    /// L'indice, dans le cadre de pile modelise, de l'emplacement que Ghidra nomme par le decalage
    /// <paramref name="ghidraOffset"/> (<c>uStack_48</c> → 0x48). Le cadre va de <c>piStack_74</c>
    /// a <c>uStack_04</c>, et les fonctions de dessin en recoivent l'ADRESSE : c'est pour ça qu'il
    /// faut un bloc contigu et non des variables separees.
    /// </summary>
    private static int SlotOf(int ghidraOffset) => (0x74 - ghidraOffset) / 4;

    /// <summary>
    /// Ghidra: <c>LAB_00518473</c>..<c>0x005185f0</c> — <b>la rose des vents</b>. La camera est
    /// mise de cote, remplacee par une vue fixe a 800 unites, et le modele de la boussole est emis
    /// avec ses huit graduations placees par sinus/cosinus autour du cap courant. La pose est
    /// ensuite restituee a l'identique.
    /// </summary>
    private static void DrawCompassRose(uint* otBase, uint* otEntry, ref uint* cursor, int* unused)
    {
        int* frame = stackalloc int[SlotOf(0x04) + 1];
        short* pose = (short*)Mem.s_CameraVector_00604928;

        Winsys3d.Ordinal_378(0x80, 0xb8);   // SetGeomOffset(128, 184)

        // Mise de cote du decalage monde, de la pose et de la matrice.
        *(int*)Mem.Entity(0x005fa520) = Mem.s_Unknown_006008d0;
        *(int*)Mem.Entity(0x005fa524) = Mem.s_Unknown_006008d4;
        *(int*)Mem.Entity(0x005fa528) = Mem.s_Unknown_006008d8;
        *(short*)Mem.Entity(0x005fa534) = pose[0];
        *(short*)Mem.Entity(0x005fa536) = pose[1];
        *(short*)Mem.Entity(0x005fa538) = pose[2];

        for (int i = 0; i < 8; i++)
        {
            ((uint*)Mem.Arena(0x005e9b00))[i] = ((uint*)Mem.s_CameraMatrix_006008e0)[i];
        }

        Mem.s_Unknown_006008d0 = 0;
        Mem.s_Unknown_006008d4 = 0;
        Mem.s_Unknown_006008d8 = 0;

        pose[0] = 0x400;
        RotMatrix(pose, (short*)Mem.s_CameraMatrix_006008e0);
        Mem.CameraTranslation[0] = 0x41;
        Mem.CameraTranslation[1] = 0x21;
        Mem.CameraTranslation[2] = 800;

        s_Unknown_005de15c = 1;
        int slide;
        if (s_MenuSlideDone_00564a56 == 0 && Mem.s_SizeSetting_005de160 > 7)
        {
            slide = Mem.s_SizeSetting_005de160 - 0xb;
            s_Unknown_005de154 = 0;
        }
        else
        {
            slide = Mem.s_SizeSetting_005de160 + 9;
            s_Unknown_005de154 = 0xa0;
        }

        s_Unknown_005de158 = slide * 0xc;

        uint* pen = cursor;
        UiWin.EmitViewportRect(otBase + 2, &pen);
        cursor = pen;

        frame[SlotOf(0x30)] = 0x800;
        frame[SlotOf(0x2c)] = 0x800;
        frame[SlotOf(0x20)] = 0x1ffffff;
        frame[SlotOf(0x3c)] = 0;
        frame[SlotOf(0x38)] = 0;
        frame[SlotOf(0x34)] = 0;
        frame[SlotOf(0x28)] = 0;
        frame[SlotOf(0x24)] = 0x40000;

        // **Deux dereferencements, pas un.** `s_ModelResultB_005f7b40` est l'adresse du slot .data ;
        // RelocateModelBundle y range le **tableau d'offsets** du paquet, et le modele est son
        // element 0. Ghidra ecrit `*DAT_005f7b40` en typant DAT_005f7b40 comme un pointeur (il le
        // passe par adresse en 0x00532640, et le relit `DAT_005f7b40[1]` / `[3]` ailleurs), ce qui
        // masque un niveau. Un seul dereferencement donne le tableau lui-meme : son « en-tete »
        // se lit alors comme un modele dont +0x08 est un pointeur et +0x28 un petit entier, et la
        // boucle de faces part dans le decor.
        cursor = DrawWorldModel_0051C420(otBase + 2, cursor,
            **(byte***)s_ModelResultB_005f7b40, frame + SlotOf(0x3c), 0xe);

        frame[SlotOf(0x40)] &= unchecked((int)0xffff0000);
        frame[SlotOf(0x50)] = (int)(nint)SpriteAtlas(0x005646b8);
        frame[SlotOf(0x4c)] = 0x1ffffff;
        frame[SlotOf(0x48)] = 0xb00088;

        cursor = Prims.DrawSpriteVariant(otBase + 2, cursor, frame + SlotOf(0x50), 0, 0);
        cursor = Prims.DrawFlatPrimitive(otBase + 2, cursor, frame + SlotOf(0x50));

        frame[SlotOf(0x4c)] = 0x1ffffff;
        frame[SlotOf(0x50)] = (int)(nint)SpriteAtlas(0x005646c0);

        // Les **quatre** graduations — la boucle va de 0 a 0x1000 par pas de 0x400, soit les quatre
        // points cardinaux — placees autour du centre (0x9c, 0xc5) a vingt-deux unites de rayon.
        // L'indice 0..3 designe les descripteurs 0x005646c0, c8, d0, d8 : quatre voisins de la
        // plage relevee, d'ou l'exigence de contiguite sur SpriteAtlas.
        //
        // **Ghidra place le X dans le mauvais champ.** Il ecrit
        // `piStack_4c = CONCAT22(piStack_4c._2_2_, 0x9c - ...)`, soit +0x04 — le mot de base. Le
        // desassemblage dit `mov [esp+3Ch],cx` avec le bloc a `esp+34h`, donc **+0x08**, la moitie
        // basse de la position, dont le Y occupe deja la moitie haute. Releve le 2026-08-22 ;
        // la premiere transcription avait copie l'erreur, ce qui envoyait le X dans le mot de
        // couleur et laissait la graduation sur place.
        short tick = 0;
        for (int angle = 0; angle < 0x1000; angle += 0x400, tick++)
        {
            frame[SlotOf(0x40)] = (frame[SlotOf(0x40)] & unchecked((int)0xffff0000)) | (ushort)tick;

            int sin = (int)Winsys3d.GetSinTableEntry(angle - pose[1]);
            int cos = Winsys3d.GetCosTableEntry(angle - pose[1]);

            frame[SlotOf(0x48)] = ((0xc5 - (cos * 0x16 >> 0xc)) << 0x10)
                | (ushort)(0x9c - (short)(sin * 0x16 >> 0xc));

            cursor = Prims.DrawSpriteVariant(otBase + 2, cursor, frame + SlotOf(0x50), 0, 0);
        }

        cursor = Prims.DrawFlatPrimitive(otBase + 2, cursor, frame + SlotOf(0x50));
        pen = cursor;
        UiWin.EmitClipRect(otBase + 2, &pen);
        cursor = pen;

        // Restitution.
        for (int i = 0; i < 8; i++)
        {
            ((uint*)Mem.s_CameraMatrix_006008e0)[i] = ((uint*)Mem.Arena(0x005e9b00))[i];
        }

        Mem.s_Unknown_006008d0 = *(int*)Mem.Entity(0x005fa520);
        Mem.s_Unknown_006008d4 = *(int*)Mem.Entity(0x005fa524);
        Mem.s_Unknown_006008d8 = *(int*)Mem.Entity(0x005fa528);

        s_Unknown_005de15c = 0;
        s_Unknown_005de154 = 0;
        s_Unknown_005de158 = 0;

        pose[0] = *(short*)Mem.Entity(0x005fa534);
        pose[1] = *(short*)Mem.Entity(0x005fa536);
        pose[2] = *(short*)Mem.Entity(0x005fa538);

        Winsys3d.Ordinal_378(0xa0, 0x78);
    }

    /// <summary>
    /// Ghidra: <c>0x005185f0</c>..<c>0x00518e60</c> — <b>les panneaux des membres du groupe</b> :
    /// pour chacun, la barre de vie et de magie, les pictogrammes d'etat, puis son modele.
    /// </summary>
    private static void DrawPartyPanels(uint* otBase, ref uint* cursor, int* block, byte* mainMem)
    {
        byte* b = (byte*)block;

        // ============================================================ A. les nombres flottants
        //
        // Soixante-quatre emplacements de 0x28 octets a 0x005e9b60 — les memes que
        // TickEffectSlots_005e9b60 fait vivre. Chacun porte une position monde (+0x00), une valeur
        // (+0x0c), un mot d'attributs (+0x26) et son temoin d'activite (+0x27). Ghidra adresse
        // l'enregistrement par son octet +0x26, d'ou les decalages negatifs de la pseudo-C.
        block[0] = (int)(nint)SpriteAtlas(0x00566c38);
        *(short*)(b + 0x10) = 0;

        short* projected = stackalloc short[4];
        byte* slot = Mem.Arena(0x005e9b60);

        for (int i = 0; i < 0x40; i++, slot += 0x28)
        {
            if (slot[0x27] == 0) continue;
            if (Mdl.ProjectPoint((int*)slot, projected) == 0) continue;

            *(uint*)(b + 8) = *(uint*)projected;
            cursor = Prims.DrawDecimalNumber(
                otBase, cursor, block, (uint)*(short*)(slot + 0xc), 5, slot[0x26]);
        }

        cursor = Prims.DrawFlatPrimitive(otBase, cursor, block);

        // ============================================================ B. un panneau par membre
        *(short*)Mem.Entity(0x005fa544) = 0;
        *(short*)Mem.Entity(0x005fa53a) = 1;
        *(uint*)Mem.Entity(0x005fa52c) = 0x0e000e00;
        *(nint*)Mem.Entity(0x005fa540) = (nint)SpriteAtlas(0x00564690);

        *Mem.Camera(0x005de05c) = 0;

        byte* character = mainMem + 0xf0000;          // Ghidra: iVar12, +0x2000 par membre
        short* slotEntry = (short*)Inp.s_SlotArea_005e9180;   // Ghidra: local_50, pas de 0x4c
        byte* record = Inp.s_SlotFlags_005e93c0;      // les enregistrements de 0x9c octets
        byte* panel = s_PartyPanelDescriptors_005dbc68;

        for (int member = 0; member < s_PartySize_005e9020;
             member++, character += 0x2000, slotEntry += 0x26, record += 0x9c,
             panel += GameMemory.UiDescriptorStride)
        {
            uint recordFlags = *(uint*)record;                    // +0x00
            uint stateFlags = *(uint*)(record + 0x90);            // +0x90

            // La hauteur que la camera doit degager pour ce membre, cumulee dans DAT_005de05c.
            if ((stateFlags & 0x8000) == 0)
            {
                int reach = ((int)Winsys3d.GetSinTableEntry(
                                 *(short*)Mem.s_CameraVector_00604928) >> 3)
                          + *slotEntry;
                if (*Mem.Camera(0x005de05c) < reach) *Mem.Camera(0x005de05c) = reach;
            }

            s_Unknown_005de15c = 1;

            // Le decalage de dessin du panneau. Trois dispositions selon l'avancement de
            // l'animation d'ouverture du menu ; les quatre tables sont relevees dans
            // DataSymbols.md et le choix se lit ligne a ligne.
            if (s_MenuSlideDone_00564a56 == 0 && Mem.s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de154 = Mem.s_SizeSetting_005de160 < 10
                    ? (((member & 1) != 0 ? unchecked((int)0xffffffce) : 0) + 0x19)
                    : 0;
                s_Unknown_005de158 = (Mem.s_SizeSetting_005de160 - 0xb) * 0xc;
            }
            else
            {
                s_Unknown_005de158 = Mem.s_SizeSetting_005de160 < 5
                    ? (Mem.s_SizeSetting_005de160 + 9) * 0xc
                    : (member > 1 ? -0xe : 0xe) + (Mem.s_SizeSetting_005de160 + 9) * 0xc;

                if (Mem.s_SizeSetting_005de160 >= 9)
                    s_Unknown_005de154 = s_PanelX_00564928[member];
                else if (Mem.s_SizeSetting_005de160 >= 5)
                    s_Unknown_005de154 = s_PanelX_00564938[member];
                else
                    s_Unknown_005de154 = Mem.s_SizeSetting_005de160 < 1
                        ? s_PanelX_00564958[member]
                        : s_PanelX_00564948[member];
            }

            // Le coin du panneau, dans le descripteur de fenetre du membre. Un membre touche
            // tremble : deux tirages independants deplacent le panneau de -2 a +1 sur chaque axe.
            short* corner = (short*)(panel + 4);
            corner[0] = (short)(((member & 1) != 0 ? 0xd1 : 0) + 5);
            corner[1] = (short)(((member & 2) != 0 ? 0x1c : 0) + 0xa6
                                + (s_PartySize_005e9020 > 2 ? 0 : 0xe));

            if ((short)*(uint*)(record + 0x96) != 0)
            {
                corner[0] += (short)((CrtRand() & 3) - 2);
                corner[1] += (short)((CrtRand() & 3) - 2);
            }

            *(uint*)(panel + 0x1c) = (uint)(((recordFlags & 1) != 0
                ? unchecked((int)0xff5100af) : 0) + 0x02cf1020);

            // ---- les deux jauges : valeur courante en couleur, maximum en blanc
            *(short*)(b + 8) = (short)(corner[0] + 10);
            *(short*)(b + 0xa) = (short)(corner[1] + 0x12);

            uint gauge = GaugeColour(*(short*)(character + 0x14c), *(short*)(character + 0x148));
            *(uint*)(panel + 0x44) = gauge & 0xffffff;
            *(uint*)(b + 4) = gauge;

            block[0] = (int)(nint)SpriteAtlas(0x00566c20);
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x14c), 0x84, 0);

            *(short*)(b + 8) += 0x14;
            *(uint*)(b + 4) = 0x1000000;
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x148), 4, 0);

            *(uint*)(b + 4) = GaugeColour(*(short*)(character + 0x14e), *(short*)(character + 0x14a));
            *(short*)(b + 8) += 0x18;
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x14e), 0x84, 0);

            *(short*)(b + 8) += 0x14;
            *(uint*)(b + 4) = 0x1000000;
            cursor = Prims.DrawDecimalNumber(otBase + 2, cursor, block,
                (uint)*(short*)(character + 0x14a), 4, 0);

            // ---- le nom, puis deux barres obliques de separation
            *(short*)(b + 8) = (short)(corner[0] + 10);
            *(short*)(b + 0xa) = (short)(corner[1] + 10);
            *(uint*)(b + 4) = 0x1000000;
            block[0] = (int)(nint)SpriteAtlas(0x00566c28);
            cursor = Prims.DrawTextRunAlias(otBase + 2, cursor, block, character + 0x100, 0, 0, 8);

            *(short*)(b + 8) += 0x10;
            *(short*)(b + 0xa) += 8;
            block[0] = (int)(nint)SpriteAtlas(0x00566c18);
            cursor = Prims.DrawTextRun(otBase + 2, cursor, block, s_SlashLabel_00564a0c, 0, 0, -1);

            *(short*)(b + 8) += 0x2c;
            cursor = Prims.DrawTextRun(otBase + 2, cursor, block, s_SlashLabel_00564a0c, 0, 0, -1);

            // ---- les neuf pictogrammes d'etat, un par bit de s_StatusBits_00564900
            //
            // L'indice +0x10 avance a chaque rang, **qu'il soit dessine ou non** : les neuf
            // descripteurs sont contigus a partir de 0x005648b8, et c'est le rang qui choisit
            // lequel — pas l'ordre d'emission.
            *(short*)(b + 0x10) = 0;
            block[0] = (int)(nint)SpriteAtlas(0x005648b8);
            *(short*)(b + 8) = (short)(corner[0] + 10);
            *(short*)(b + 0xa) = (short)(corner[1] + 0x1a);

            // `cursor` est un parametre `ref`, dont on ne peut pas prendre l'adresse ; le curseur
            // passe par une locale le temps des deux encadrements, comme ailleurs dans ce fichier.
            uint* pen = cursor;
            UiWin.EmitViewportRect(otBase + 2, &pen);

            uint status = *(uint*)(character + 0x140);
            for (int bit = 0; bit < s_StatusBits_00564900.Length; bit++)
            {
                if ((status & s_StatusBits_00564900[bit]) != 0)
                    pen = Prims.DrawSpriteVariant(otBase + 2, pen, block, 0, 0);

                *(short*)(b + 8) += 8;
                (*(short*)(b + 0x10))++;
            }

            UiWin.EmitClipRect(otBase + 2, &pen);
            cursor = pen;

            *(short*)(b + 0x10) = 0;
            cursor = Prims.DrawFlatPrimitive(otBase + 2, cursor, block);
            cursor = UiWin.DrawUiPanel(otBase + 2, cursor, panel);

            s_Unknown_005de154 = 0;
            s_Unknown_005de158 = 0;
            s_Unknown_005de15c = 0;

            // ---- le membre lui-meme dans le monde
            if (VSyncDelayed() != 0) continue;

            if ((recordFlags & 0x4000) == 0)
            {
                int kind = record[4] * 4;
                int* transform = (int*)(record + 0x1c);

                cursor = Mdl.DrawAnimatedModel(otBase, cursor,
                    *(byte**)(*(int**)(Mem.Arena(0x005e9680) + kind)
                              + *(short*)(record + 0x18)),
                    *(int**)(Mem.Arena(0x005e9800) + kind),
                    transform,
                    *(short*)(record + 0x1a),
                    (uint)*(short*)(record + 0x0a));

                // L'ombre portee, si le decor la demande et si le membre est a l'ecran.
                if (character[0x2b9] != 0 && Mdl.ProjectPoint(transform, projected) != 0)
                {
                    block[0] = (recordFlags & 0x10000) == 0 && (byte)slotEntry[4] != 8
                        ? (int)(nint)SpriteAtlas(0x00564798 + *((byte*)slotEntry + 9) * 8)
                        : (int)(nint)SpriteAtlas(0x005647b8);

                    *(short*)(b + 8) = (short)(projected[0] - 0xc);
                    *(short*)(b + 0xa) = projected[1];

                    cursor = Prims.DrawSpriteVariant(otBase + 3, cursor, block, 0, 0);
                    cursor = Prims.DrawFlatPrimitive(otBase + 3, cursor, block);
                }

                if (character[0x2ba] != 0)
                {
                    cursor = DrawGroundDisc(otBase, cursor, transform, member);
                }

                if (*(int*)(record + 0x3c) != 0)
                {
                    cursor = DrawStatusBubble(otBase, cursor, transform, record);
                }
            }

            // La lueur du membre : sa position devient celle de la particule partagee.
            if ((stateFlags & 0x9000) == 0 && (recordFlags & 0x1000) == 0)
            {
                *(uint*)Mem.Entity(0x005fa524) = *(uint*)(record + 0x64);
                *(uint*)Mem.Entity(0x005fa520) = *(uint*)(record + 0x1c);
                *(uint*)Mem.Entity(0x005fa528) = *(uint*)(record + 0x24);

                // Ghidra: la paire `DAT_00564a4c = 0 / = 1` autour de l'appel, quatre fois dans le
                // binaire — elle force le chemin « sans capacite » de l'emetteur pour cette seule
                // primitive. Voir GameMemory, qui recense les paires.
                Mem.s_Unknown_00564a4c = 0;
                cursor = EmitParticle(otBase + 5, cursor, (int*)Mem.Entity(0x005fa520), 0, 0);
                Mem.s_Unknown_00564a4c = 1;
            }

            if ((recordFlags & 0x8000000) != 0)
            {
                cursor = DrawSpeechOverlay_005309B0(otBase, cursor, record, slotEntry, character);
            }
        }
    }

    /// <summary>
    /// Ghidra: les deux moities identiques du calcul de couleur de jauge, dans le bloc des
    /// panneaux. Le rapport <c>courant * 0xff / maximum</c> passe par deux regimes : sous la
    /// moitie il monte le vert en gardant le rouge sature, au-dessus il baisse le rouge. Le
    /// resultat sert deux fois — masque sur 24 bits dans le descripteur de fenetre, entier comme
    /// mot de base du sprite.
    /// </summary>
    private static uint GaugeColour(short current, short maximum)
    {
        int ratio = current * 0xff / maximum;
        return ratio < 0x80
            ? ((uint)(ratio << 9) >> 8 & 0xffffff) << 8 | 0xff
            : (uint)(ratio + 0x7f80) * 0x20000 | 0xffff;
    }

    /// <summary>Ghidra: le sous-bloc <c>character[0x2ba]</c> — le disque au sol sous le membre,
    /// pose par <c>FUN_0051c420</c> sur le modele <c>DAT_005f7b40[3]</c>.</summary>
    private static uint* DrawGroundDisc(uint* otBase, uint* cursor, int* transform, int member)
    {
        int* placement = stackalloc int[10];
        for (int i = 0; i < 10; i++) placement[i] = 0;

        Mem.CopyDwords((uint*)transform, (uint*)placement, 10);
        placement[1] += 0x32000;

        *(short*)((byte*)placement + 0x18) = (short)(s_FadeCounter_005e9960 << 6);
        placement[7] = (int)s_Unknown_005e9814;

        return DrawWorldModel_0051C420(otBase, cursor,
            ((byte**)*(byte***)s_ModelResultB_005f7b40)[3], placement,
            (uint)((s_FadeCounter_005e9960 << 6) & 0xffff0000) | s_PanelExtra_00563348[member]);
    }

    /// <summary>Ghidra: le sous-bloc <c>record[0x3c] != 0</c> de <c>FUN_00516fc0</c> — <b>la bulle
    /// d'etat au-dessus du membre</b> : poison, sommeil, silence et le reste.
    ///
    /// <para>Meme forme que <see cref="DrawGroundDisc"/> : la transformation du membre est recopiee,
    /// une composante est decalee, et le tout part en particule. Ici c'est la hauteur qui bouge —
    /// <c>placement[1] += (hauteur + 0x19) &lt;&lt; 12</c>, la hauteur venant de
    /// <see cref="StatusBubbleHeight_00538BC0"/> — et l'emplacement <c>0x10001000</c> de
    /// <c>placement[3]</c> qui choisit la case de la planche.</para>
    ///
    /// <para>La particule part sur <c>otBase</c> et non <c>otBase + 5</c> comme la lueur du membre :
    /// la bulle se dessine donc devant, ce qui est bien ce qu'on veut d'une icone d'etat.</para></summary>
    private static uint* DrawStatusBubble(uint* otBase, uint* cursor, int* transform, byte* record)
    {
        int* placement = stackalloc int[10];
        Mem.CopyDwords((uint*)transform, (uint*)placement, 10);

        int height = StatusBubbleHeight_00538BC0((uint*)record);

        placement[1] += (height + 0x19) * 0x1000;
        placement[3] = 0x10001000;
        placement[7] = 0x1000000;
        placement[9] = 0;

        return EmitParticle(otBase, cursor, placement, 0, 0);
    }

    /// <summary>Ghidra: &amp;DAT_0056d440 — <b>seize entrées de huit octets, une par classe de
    /// personnage</b> : la <b>boîte du corps</b>. <c>+0</c> et <c>+4</c> sont ses deux
    /// demi-étendues au sol, <c>+2</c> la hauteur à laquelle flotte la bulle d'état au-dessus de la
    /// tête. <c>+6</c> est nul dans les seize entrées. Relevées dans <c>.data</c>.
    ///
    /// <para><b>Le rôle de <c>+0</c> et <c>+4</c> a été établi le 2026-08-28</b>, en transcrivant
    /// <see cref="TestMeleeReach_00535A90"/> : <see cref="EntityFootprint_00538C10"/> rend cette
    /// entrée pour un membre du groupe et <c>stat + 0x58</c> pour une entité de scène, et le test
    /// de portée en tire les deux côtés du rectangle de collision. Les deux tables portent donc le
    /// <b>même enregistrement</b>, ce que confirme <see cref="StatusBubbleHeight_00538BC0"/> :
    /// elle lit <c>stat + 0x5a</c> là où celle-ci lit <c>+2</c>, soit le même champ des deux côtés.
    /// Les valeurs se recoupent aussi — la classe 6 est la plus large (0x40 sur 0x28) et la 7 la
    /// plus étroite (0x1e sur 0x14).</para>
    ///
    /// <para>L'étendue est relevée, pas supposée : à <c>0x0056d4c0</c> commence une table de
    /// pointeurs (<c>0x0053ace0</c>, <c>0x0053ad80</c>, …), donc seize entrées exactement — le même
    /// compte que <see cref="s_ReactionAnimations_0056aaa0"/>, et pour la même raison.</para></summary>
    private static readonly byte* s_ClassBodyMetrics_0056d440 = AllocBytes([
        0x2e, 0x00, 0x82, 0x00, 0x1e, 0x00, 0x00, 0x00,  0x28, 0x00, 0x7f, 0x00, 0x18, 0x00, 0x00, 0x00,
        0x26, 0x00, 0x7d, 0x00, 0x1a, 0x00, 0x00, 0x00,  0x21, 0x00, 0x7c, 0x00, 0x20, 0x00, 0x00, 0x00,
        0x26, 0x00, 0x82, 0x00, 0x1a, 0x00, 0x00, 0x00,  0x28, 0x00, 0x7d, 0x00, 0x1e, 0x00, 0x00, 0x00,
        0x40, 0x00, 0x76, 0x00, 0x28, 0x00, 0x00, 0x00,  0x1e, 0x00, 0x71, 0x00, 0x14, 0x00, 0x00, 0x00,
        0x26, 0x00, 0x7d, 0x00, 0x19, 0x00, 0x00, 0x00,  0x1e, 0x00, 0x7b, 0x00, 0x1c, 0x00, 0x00, 0x00,
        0x1c, 0x00, 0x75, 0x00, 0x18, 0x00, 0x00, 0x00,  0x1f, 0x00, 0x78, 0x00, 0x18, 0x00, 0x00, 0x00,
        0x24, 0x00, 0x78, 0x00, 0x18, 0x00, 0x00, 0x00,  0x24, 0x00, 0x78, 0x00, 0x1a, 0x00, 0x00, 0x00,
        0x32, 0x00, 0x72, 0x00, 0x28, 0x00, 0x00, 0x00,  0x1e, 0x00, 0x6e, 0x00, 0x14, 0x00, 0x00, 0x00,
    ]);

    /// <summary>
    /// Ghidra: FUN_00538bc0 @ 0x00538bc0 — <b>la hauteur de la tête d'une entité</b>, prise dans
    /// l'une de deux tables selon ce qu'elle est.
    ///
    /// <para>Un <b>membre du groupe</b> (bit <c>0x800000</c>) la tient de sa classe, lue dans le
    /// bloc de 0x2000 octets à <c>MainMem + 0xf0150</c> puis indexée dans
    /// <see cref="s_ClassBodyMetrics_0056d440"/>. Toute autre entité la tient de son propre bloc
    /// de statistiques, à <c>+0x5a</c>.</para>
    ///
    /// <para>La seconde branche passe par <see cref="s_EntityStatBlocks_005ea560"/> et non par une
    /// vue d'arène : c'est le défaut d'aliasing déjà résolu une fois pour cette table, et le
    /// re-commettre ici aurait rendu des hauteurs nulles pour toutes les entités de scène.</para>
    /// </summary>
    private static int StatusBubbleHeight_00538BC0(uint* entity)
    {
        if ((*entity & 0x800000) != 0)
        {
            byte characterClass = GetMainMemPtr()[(byte)entity[2] * 0x2000 + 0xf0150];
            return *(short*)(s_ClassBodyMetrics_0056d440 + characterClass * 8 + 2);
        }

        return *(short*)(s_EntityStatBlocks_005ea560 + (byte)entity[2] * 0x120 + 0x5a);
    }

    /// <summary>Ghidra: &amp;DAT_0056a850 — <b>la disposition du menu rapide</b>, neuf rangées de
    /// neuf octets indexées par <c>personnage[0x150] &amp; 7</c>, la classe. Chaque rangée s'ouvre
    /// sur son <b>nombre de catégories</b> puis porte huit <b>genres de catégorie</b>.
    ///
    /// <para>Les deux moitiés sont deux symboles dans <c>Blaze.c</c> et une seule table ici, parce
    /// que c'en est une : <c>(&amp;DAT_0056a850)[classe * 9]</c> lit l'octet de compte,
    /// <c>(&amp;DAT_0056a851)[classe * 9 + n]</c> lit le n-ième genre de la même rangée. Les allouer
    /// séparément aurait perdu l'adjacence dont l'indexation dépend.</para>
    ///
    /// <para><b>Le chevauchement supposé n'existe pas</b> — relevé le 2026-08-26 en transcrivant
    /// <see cref="TickQuickMenu_00530070"/>. On avait noté ici que le pas 9 et un second index
    /// allant jusqu'à quinze se contredisaient. L'octet de compte tranche : <c>0x2be &amp; 0xf</c>
    /// est ramené sous le compte de la rangée à chaque changement de catégorie (au plus 7), donc
    /// une rangée ne déborde jamais sur la suivante. La même lecture rend les deux <c>0xff</c> de la
    /// table <b>inatteignables</b> : celui de la rangée 2 est au huitième genre alors que le compte
    /// vaut 6, et toute la rangée 8 est hors de portée de <c>classe &amp; 7</c>.</para>
    ///
    /// <para>Genres : 0–5 les cinq emplacements d'équipement, 6 les sorts, 7 et 8 deux choix fixes,
    /// 9 le passage de la main à un autre membre. <see cref="DrawSpeechOverlay_005309B0"/> les
    /// interprète en icônes, <see cref="TickQuickMenu_00530070"/> en gestes.</para>
    ///
    /// <para>Les valeurs sont des <b>octets signés</b> — voir <see cref="s_SpeechIcons_0056aae0"/>
    /// pour ce que <c>-1</c> désignerait.</para></summary>
    private static readonly byte* s_MenuLayout_0056a850 = AllocBytes([
        0x07, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x09, 0x08,
        0x07, 0x00, 0x01, 0x03, 0x04, 0x05, 0x06, 0x09, 0x08,
        0x06, 0x00, 0x01, 0x03, 0x04, 0x05, 0x09, 0x08, 0xff,
        0x07, 0x00, 0x01, 0x03, 0x04, 0x05, 0x06, 0x09, 0x08,
        0x07, 0x00, 0x01, 0x03, 0x04, 0x05, 0x07, 0x09, 0x08,
        0x07, 0x00, 0x01, 0x03, 0x04, 0x05, 0x06, 0x09, 0x08,
        0x07, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x09, 0x08,
        0x07, 0x00, 0x01, 0x03, 0x04, 0x05, 0x06, 0x09, 0x08,
        0x04, 0x00, 0x01, 0x02, 0x05, 0xff, 0x05, 0x00, 0x01,
    ]);

    /// <summary>Ghidra: &amp;DAT_0056a851 — la seconde moitié de <see cref="s_MenuLayout_0056a850"/>,
    /// le genre de la catégorie <c>n</c>. Propriété calculée et non champ : le champ doit rester la
    /// table entière, sinon l'octet de compte n'a plus d'adresse.</summary>
    private static byte* s_SpeechIconIndex_0056a851 => s_MenuLayout_0056a850 + 1;

    /// <summary>
    /// Ghidra: FUN_005309b0 @ 0x005309b0 — <b>la surimpression de dialogue</b> : la bulle et son
    /// icône au-dessus d'un membre du groupe, gardée par le bit <c>0x8000000</c> de son
    /// enregistrement.
    ///
    /// <para>Elle projette la position du membre, remontée de <c>0x32000</c> comme le disque au sol,
    /// et <b>abandonne si elle sort de l'écran</b> — c'est le seul chemin qui rend le curseur
    /// inchangé.</para>
    ///
    /// <para><b>Le bit <c>0x80</c> de <c>personnage[0x2be]</c> échange deux teintes et décale le
    /// plan.</b> Avant l'appel médian, la première moitié se dessine en teinte globale ou en gris
    /// <c>0x404040</c> selon ce bit ; après, les deux se croisent, et dans le cas où le bit est posé
    /// la suite change en prime d'emplacement d'ordonnancement, de <c>otBase + 1</c> à
    /// <c>otBase</c>. Ce dernier point est facile à manquer : dans l'original c'est un
    /// <c>iVar1 = (int)param_1</c> discret au milieu d'une branche de couleur.</para>
    ///
    /// <para><b><c>&amp;LAB_00404040</c> n'est pas du code.</b> Ghidra l'a étiqueté comme un label
    /// parce que l'adresse tombe dans <c>.text</c>, mais c'est la couleur <c>0x404040</c> — le gris
    /// éteint. <c>PartySetup</c> avait déjà tranché le même cas ; le prendre pour un pointeur de
    /// fonction aurait donné un plantage inexplicable.</para>
    ///
    /// <para>La partie basse ne se dessine que si <c>personnage[0x2d0]</c> est non nul, et l'icône
    /// qu'elle pose dépend du même index que la bulle, par un aiguillage à quatre familles :
    /// 0–5 balaient les cinq octets de <c>+0x2b0</c> à la recherche de la cible courante, 6–7
    /// comparent <c>+0x2b6</c>, 8 prend une icône fixe, 9 va chercher un chiffre dans le bloc du
    /// personnage visé.</para>
    ///
    /// <para>Le quatrième paramètre est poussé par l'appelant et <b>jamais lu</b> par le corps.
    /// Conservé pour que le site d'appel reste lisible contre l'original.</para>
    /// </summary>
    private static uint* DrawSpeechOverlay_005309B0(
        uint* otBase, uint* cursor, byte* record, short* slotEntry, byte* character)
    {
        _ = slotEntry;                                       // Ghidra param_4 : pousse, jamais lu


        int* placement = stackalloc int[10];
        Mem.CopyDwords((uint*)(record + 0x1c), (uint*)placement, 10);
        placement[1] += 0x32000;

        // QUATRE mots, pas deux : ProjectPoint ecrit huit octets — le couple ecran en +0 et la
        // profondeur en +4. Deux suffisaient a l'usage qu'on en fait ici, et debordaient la pile de
        // quatre octets : le JIT pose un temoin de pile des qu'une methode contient un `stackalloc`,
        // et sa verification de sortie tuait le processus par 0xC0000409, sans message, dans
        // l'epilogue de cette fonction. Le cadre de l'original reserve bien huit octets a cet
        // endroit (`local_44` suivi de quatre octets muets avant `local_3c`).
        short* screen = stackalloc short[4];
        if (Mdl.ProjectPoint(placement, screen) == 0) return cursor;

        uint* otEntry = otBase + 1;

        int* sprite = stackalloc int[8];
        for (int i = 0; i < 8; i++) sprite[i] = 0;
        byte* s = (byte*)sprite;

        sprite[1] = (character[0x2be] & 0x80) == 0 ? (int)s_Unknown_005e9814 : 0x404040;

        // La bulle vit dans le repere de la fenetre d'objets : ces trois globales sont son origine.
        //
        // **Elles s'ecrivent par leur champ nomme, pas par l'arene.** 0x005de154, _158, _15c et
        // _160 tombent tous les quatre DANS ItemMenuArena, et portent aussi un champ simple ailleurs
        // (s_Unknown_005de154..15c dans State00/MainMenu.cs, s_SizeSetting_005de160 dans
        // GameMemory) — c'est le recouvrement que UiWindow signale en TODO(REVERSE) depuis le
        // 2026-08-21. Le lecteur, EmitViewportRect/EmitClipRect, lit les champs ; ecrire l'arene
        // ici ne lui parvenait donc pas, et la bulle se dessinait dans le coin haut gauche avec
        // une origine restee a zero. Releve le 2026-08-26, a la premiere image ou l'anneau a
        // survecu assez longtemps pour etre regarde.
        int zoom = Mem.s_SizeSetting_005de160;
        s_Unknown_005de15c = 1;
        s_Unknown_005de154 = screen[0] * (zoom + 9) * 0x20 / 0x140 + (0xb - zoom) * 0x10;
        s_Unknown_005de158 = screen[1] * (zoom + 9) * 0x20 / 0x140 + (0xb - zoom) * 0xc;

        UiWin.EmitViewportRect(otEntry, &cursor);            // Ghidra: FUN_0051be20

        int icon = (sbyte)s_SpeechIconIndex_0056a851[
            (character[0x150] & 7) * 9 + (character[0x2be] & 0xf)];

        // --- la bulle ---------------------------------------------------------------------------
        *(ushort*)(s + 0x10) = 0;
        sprite[0] = (int)(nint)(s_SpeechIcons_0056aae0 + icon * 8);
        *(short*)(s + 8) = -0xb;
        *(short*)(s + 0xa) = -0xb;

        cursor = Prims.DrawSpriteVariant(otEntry, cursor, sprite, 0, 0);
        cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sprite);

        *(short*)(s + 8) -= 4;
        *(short*)(s + 0xa) -= 4;
        sprite[0] = (int)(nint)s_SpeechBubble_0056ab30;
        *(ushort*)(s + 0x10) = 0;

        cursor = Prims.DrawSpriteVariant(otEntry, cursor, sprite, 0, 2);
        cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sprite);

        *(short*)(s + 8) += 6;
        *(short*)(s + 0xa) += 6;
        UiWin.EmitClipRect(otEntry, &cursor);                // Ghidra: FUN_0051bdb0

        // Les deux teintes se croisent, et le plan bouge avec elles.
        if ((character[0x2be] & 0x80) == 0)
        {
            sprite[1] = 0x404040;
        }
        else
        {
            sprite[1] = (int)s_Unknown_005e9814;
            otEntry = otBase;
        }

        UiWin.EmitViewportRect(otEntry, &cursor);

        // --- l'icone de contenu, seulement si personnage[0x2d0] le demande -----------------------
        if (character[0x2d0] != 0)
        {
            byte* directory = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            uint extra = 0;
            bool emit = true;

            switch (icon)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                {
                    byte wanted = character[0x2d1 + character[0x2bf]];

                    for (int i = 0; i < 5; i++)
                    {
                        if (character[0x2b0 + i] != wanted) continue;

                        *(short*)(s + 8) += 0xc;
                        *(short*)(s + 0xa) += 0xc;
                        sprite[0] = (int)(nint)s_SpeechMarker_0056ab70;
                        *(ushort*)(s + 0x10) = 0;

                        cursor = Prims.DrawSpriteVariant(otEntry, cursor, sprite, 0, 0);
                        cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sprite);

                        *(short*)(s + 8) -= 0xc;
                        *(short*)(s + 0xa) -= 0xc;
                    }

                    byte* glyph = *(byte**)(directory + 8);
                    sprite[0] = (int)(nint)glyph;
                    *(ushort*)(s + 0x10) = 0;
                    glyph[0] = (byte)(wanted * 0x18);
                    glyph[1] = (byte)(record[8] * 0x18);

                    // La branche 0-5 emet elle-meme, puis rejoint la sortie commune sans repasser
                    // par le decalage de quatre.
                    cursor = Prims.DrawSpriteVariant(otEntry, cursor, sprite, 0,
                                                     (uint)((record[8] << 8) | wanted));
                    emit = false;
                    break;
                }

                case 6:
                case 7:
                    if (character[0x2b6] == character[0x2bf])
                    {
                        *(short*)(s + 8) += 0xc;
                        *(short*)(s + 0xa) += 0xc;
                        sprite[0] = (int)(nint)s_SpeechMarker_0056ab70;
                        *(ushort*)(s + 0x10) = 0;

                        cursor = Prims.DrawSpriteVariant(otEntry, cursor, sprite, 0, 0);
                        cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sprite);

                        *(short*)(s + 8) -= 0xc;
                        *(short*)(s + 0xa) -= 0xc;
                    }

                    sprite[0] = (int)(nint)(*(byte**)(directory + 0x10 + character[0x2b5] * 4));
                    *(ushort*)(s + 0x10) = character[0x2bf];
                    break;

                case 8:
                    sprite[0] = (int)(nint)s_SpeechDigits_0056ab38;
                    *(ushort*)(s + 0x10) = character[0x2bf];
                    break;

                case 9:
                {
                    byte member = character[0x2bf];
                    sprite[0] = (int)(nint)(*(byte**)(directory + 4));
                    *(ushort*)(s + 0x10) = (ushort)(Inp.s_SlotFlags_005e93c0[member * 0x9c + 4] + 4);

                    // Ghidra reutilise le registre du personnage pour ce calcul, et sa moitie haute
                    // se retrouve dans l'argument. Transcrit tel quel : elle n'est nulle que tant
                    // que l'indice reste sous huit.
                    int block = member * 0x2000;
                    extra = (uint)(((block >> 0x10) << 0x10)
                                   | GetMainMemPtr()[block + 0xf02bd]);
                    break;
                }

                default:
                    emit = false;
                    break;
            }

            if (emit)
            {
                *(short*)(s + 8) += 4;
                *(short*)(s + 0xa) += 4;
                cursor = Prims.DrawSpriteVariant(otEntry, cursor, sprite, 0, extra);
                *(short*)(s + 8) -= 4;
                *(short*)(s + 0xa) -= 4;
            }

            cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sprite);
        }

        sprite[0] = (int)(nint)s_SpeechBubble_0056ab30;
        *(ushort*)(s + 0x10) = 0;
        cursor = Prims.DrawSpriteVariant(otEntry, cursor, sprite, 0, 6);
        cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sprite);
        UiWin.EmitClipRect(otEntry, &cursor);

        s_Unknown_005de15c = 0;
        s_Unknown_005de158 = 0;
        s_Unknown_005de154 = 0;

        return cursor;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005dbc68</c> — <b>quatre descripteurs de fenetre</b>, un par membre du
    /// groupe, au meme pas <c>0x8c8</c> que ceux de <see cref="GameMemory.UiDescriptorStride"/>.
    ///
    /// <para><b>Defaut d'aliasing resorbe le 2026-08-23.</b> Ce nom designait sa PROPRE allocation,
    /// alors que <see cref="s_WorldPanels_005dbc68"/> — la meme adresse — en a une autre, celle que
    /// la fin de <c>FUN_00516350</c> monte au chargement de zone. Le bloc des panneaux lisait donc
    /// quatre descripteurs vierges : le coin, la teinte et la couleur de jauge etaient bien ecrits
    /// a chaque image, mais tout ce qui fait le <b>cadre</b> — le mot de page <c>+0x00</c>, la
    /// hauteur <c>+0x0a</c>, le style <c>+0x18</c>/<c>+0x19</c>, les couleurs <c>+0x20</c>… —
    /// restait a zero. D'ou des jauges affichees en texte nu, la ou l'original les encadre comme
    /// une boite de dialogue.
    ///
    /// <para>Le commentaire precedent disait « le bloc est nul au chargement, donc tout ce qu'il
    /// porte vient du code ». La premiere moitie etait vraie — l'adresse tombe dans la queue non
    /// initialisee de <c>.data</c> — et c'est justement ce qui rendait la seconde trompeuse : le
    /// code qui le remplit existait, ailleurs, sous un autre nom.</para>
    /// </summary>
    private static byte* s_PartyPanelDescriptors_005dbc68 => s_WorldPanels_005dbc68;

    /// <summary>Ghidra: <c>&amp;DAT_00563348</c> — quatre demi-mots, un par membre, qui forment la
    /// moitie basse du mot d'attributs passe au disque au sol. Releves dans le binaire.</summary>
    private static readonly ushort[] s_PanelExtra_00563348 = [0x000c, 0x0002, 0x0003, 0x000e];


    /// <summary>
    /// Ghidra: <c>0x0051905b</c>..<c>0x00519703</c> — <b>le balayage des cellules de decor</b>, le
    /// gros du rendu du monde.
    ///
    /// <para>La borne indiquee par le TODO — 0x00518e60 — tombait encore dans la boucle des
    /// panneaux (la bulle d'etat est en 0x00518f02) ; le bloc commence au test du second voile,
    /// juste apres la sortie de cette boucle.</para>
    ///
    /// <para>Le cap de la camera (<c>DAT_0060492a</c>) choisit un des <b>huit octants</b>. L'octant
    /// donne le coin de depart (<see cref="GameMemory.s_CellSweepCorner_00564548"/>) et la liste des cellules
    /// a sauter (<see cref="GameMemory.s_CellSkipTables_00564558"/>). Le balayage descend ensuite 7 rangs de
    /// 7 colonnes, en <b>decroissant</b> sur les deux axes.</para>
    ///
    /// <para>Chaque cellule porte <b>deux listes chainees</b>, prises dans les deux grilles 32x32
    /// que le prologue efface : <c>MainMem + 0x1c5000</c> (indice 0x38a0) pour les entites, et
    /// <c>MainMem + 0x1c4000</c> (indice 0x3880) pour le decor pose. Les deux se parcourent par le
    /// chainage <c>+0x68</c> des enregistrements de 0x9c octets.</para>
    ///
    /// <para>Viennent enfin les <b>trois listes de morceaux</b> — trois suites d'octets terminees
    /// par <c>0xff</c>, prises a la file dans le meme tampon — puis le <b>ciel</b>, garde par le
    /// bit 0x40 du premier octet de <c>DAT_005eca80</c>.</para>
    /// </summary>
    private static void DrawWorldCells(uint* otBase, uint* otEntry, ref uint* cursor, byte* mainMem)
    {
        // ---------------------------------------------------------- le second voile plein ecran
        // Ghidra: DAT_005de02b, c'est-a-dire l'octet de poids fort de DAT_005de028 — meme montage
        // que DAT_005de024/DAT_005de027 en tete du bloc d'affichage.
        if ((byte)(Mem.s_Unknown_005de028 >> 0x18) != 0)
        {
            cursor = FillScreenQuad_0051F940(
                otBase + 2, cursor, Mem.s_Unknown_005de028, 0, 0, 0x140, 0x100);
        }

        // ---------------------------------------------------------- l'octant, le coin, les sauts
        short* pose = (short*)Mem.s_CameraVector_00604928;
        int octant = (((ushort)pose[1] + 0x100) >> 9) & 7;

        int skipCursor = octant * 0x10;
        int skipKey = GameMemory.s_CellSkipTables_00564558[skipCursor]
                    | GameMemory.s_CellSkipTables_00564558[skipCursor + 1] << 8;
        skipCursor += 2;

        ushort corner = GameMemory.s_CellSweepCorner_00564548[octant];
        int cellX = ((-Mem.s_Unknown_006008d0 >> 0xc) + 0x2000 >> 9) + (corner & 0xff);
        int cellZ = ((-Mem.s_Unknown_006008d8 >> 0xc) + 0x2000 >> 9) + (corner >> 8);

        // Quatre mots : ProjectPoint en ecrit huit octets (couple ecran puis profondeur). Voir
        // DrawSpeechOverlay_005309B0 pour ce que deux coutaient.
        short* projected = stackalloc short[4];

        for (int row = 7; row != 0; row--, cellZ--)
        {
            int x = cellX;

            for (int col = 7; col != 0; col--, x--)
            {
                if (skipKey == (row << 8 | col))
                {
                    // LB-009 : la cle suivante est lue sur **un seul octet** (`movzx ax, byte
                    // [edi]` en 0x00519143) alors que le curseur avance de deux et que la table
                    // porte des cles de 16 bits. La cle devient donc <= 0xff et ne peut plus
                    // egaler `rang << 8 | colonne`, qui vaut au moins 0x0101 : une seule cellule
                    // est sautee par image au lieu de huit. Transcrit tel quel.
                    skipKey = GameMemory.s_CellSkipTables_00564558[skipCursor];
                    skipCursor += 2;
                    continue;
                }

                // Les deux bornes sont comparees sur 16 bits, ce qui rejette aussi les negatifs.
                if ((ushort)x >= 0x20 || (ushort)cellZ >= 0x20) continue;

                // ==================================================== la grille des entites
                for (byte* e = *(byte**)(mainMem + ((cellZ + 0x38a0) * 0x20 + x) * 4);
                     e != null; e = *(byte**)(e + 0x68))
                {
                    uint flags = *(uint*)e;
                    if ((flags & 0x4000) != 0 || (e[0x36] & 0x80) != 0) continue;

                    // ECHAFAUDAGE : OPENFORESIA_TRACE_MODELES=1, voir EntityHandlerDispatch.cs.
                    if (s_TraceModels) TraceDrawnEntity(e, flags);

                    // La lueur ne suit que les chemins qui retombent sur LAB_005193ff ; les deux
                    // chemins de modele (0x80000 et 0x100000) sautent directement au maillon
                    // suivant.
                    bool glow = true;

                    if ((flags & 0x8000) == 0)
                    {
                        // -------- les particules : un decalage de couche pris dans l'entite,
                        // ou la couche d'arriere-plan.
                        if ((flags & 0x100000) != 0)
                        {
                            *(uint*)Mem.Entity(0x005fa52c) = 0x07000700;
                            cursor = EmitParticle(otBase + *(short*)(e + 0x2e), cursor,
                                (int*)(e + 0x1c), 0, *(ushort*)(e + 0x0a));
                        }
                        else if ((flags & 0x80000) != 0)
                        {
                            *(uint*)Mem.Entity(0x005fa52c) = 0x07000700;
                            cursor = EmitParticle(otBase - 5, cursor,
                                (int*)(e + 0x1c), 0, *(ushort*)(e + 0x0a));
                        }
                    }
                    else if (VSyncDelayed() == 0)
                    {
                        if ((flags & 0x80000) != 0)
                        {
                            cursor = DrawWorldModel_0051C420(otBase, cursor,
                                ((byte**)*(byte***)s_ModelResultB_005f7b40)[1],
                                (int*)(e + 0x1c), 0xc);
                            glow = false;
                        }
                        else if ((flags & 0x100000) != 0)
                        {
                            // Le paquet de modeles : la premiere serie tant que l'indice reste
                            // sous DAT_005edec4, la seconde au-dela — le meme debordement volontaire
                            // que les tables de sauts d'entites.
                            byte assetIndex = e[4];
                            byte* model = assetIndex < s_Unknown_005edec4
                                ? ((byte**)*(byte***)s_ModelResultA_005fa588)[assetIndex]
                                : ((byte**)*(byte***)Mem.Entity(0x00600780))
                                      [assetIndex - s_Unknown_005edec4];

                            cursor = DrawWorldModelVariant_0051E700(
                                otBase + *(short*)(e + 0x2e), cursor, model,
                                (int*)(e + 0x1c), *(ushort*)(e + 0x0a));
                            glow = false;
                        }
                        else if ((flags & 0x440000) != 0)
                        {
                            *(uint*)Mem.Entity(0x005fa52c) = 0x0e000e00;

                            int kind = e[4] * 4;
                            cursor = Mdl.DrawAnimatedModel(otBase, cursor,
                                *(byte**)(*(int**)(Mem.s_AnimationTable_005e9640 + kind)
                                          + *(short*)(e + 0x18)),
                                *(int**)(s_ModelTable_005e96a0 + kind),
                                (int*)(e + 0x1c),
                                *(short*)(e + 0x1a),
                                *(ushort*)(e + 0x0a));

                            // Les trois nombres de mise au point au-dessus de l'entite. Garde par
                            // le verrou du mode developpeur, donc hors du chemin ordinaire.
                            if (Mem.s_DebugModeLatch_005633a8 == 0 && s_DebugOverlayEnabled_005de020 != 0
                                && Mdl.ProjectPoint((int*)(e + 0x1c), projected) != 0)
                            {
                                int* dbg = stackalloc int[5];
                                dbg[0] = (int)(nint)SpriteAtlas(0x00566c30);
                                dbg[1] = 0x1000000;
                                byte* db = (byte*)dbg;
                                *(short*)(db + 8) = (short)(projected[0] - 0xc);
                                *(short*)(db + 0xa) = projected[1];

                                // La table des blocs de statistiques a sa propre allocation ; la
                                // lire par `Mem.Arena` serait le defaut d'aliasing que
                                // ResolveEntityBlocks a porte jusqu'au 2026-08-23.
                                byte* stats = s_EntityStatBlocks_005ea560 + e[8] * 0x120;

                                cursor = Prims.DrawDecimalNumber(
                                    otBase + 2, cursor, dbg, *(ushort*)(stats + 0x44), 5, 0);
                                *(short*)(db + 0xa) += 8;
                                cursor = Prims.DrawDecimalNumber(
                                    otBase + 2, cursor, dbg, (uint)*(short*)(stats + 0x4c), 5, 0);
                                *(short*)(db + 0xa) += 8;
                                cursor = Prims.DrawDecimalNumber(
                                    otBase + 2, cursor, dbg, (uint)*(short*)(stats + 0x4e), 5, 0);
                            }
                        }
                    }

                    // -------- la lueur de l'entite, meme montage que celle des membres du groupe
                    if (glow && (*(uint*)(e + 0x90) & 0x9000) == 0 && (flags & 0x1000) == 0)
                    {
                        *(uint*)Mem.Entity(0x005fa520) = *(uint*)(e + 0x1c);
                        *(uint*)Mem.Entity(0x005fa524) = *(uint*)(e + 0x64);
                        *(uint*)Mem.Entity(0x005fa528) = *(uint*)(e + 0x24);

                        Mem.s_Unknown_00564a4c = 0;
                        cursor = EmitParticle(otBase + 5, cursor, (int*)Mem.Entity(0x005fa520), 0, 0);
                        Mem.s_Unknown_00564a4c = 1;
                    }
                }

                // ==================================================== la grille du decor pose
                if (VSyncDelayed() != 0) continue;

                for (byte* e = *(byte**)(mainMem + ((cellZ + 0x3880) * 0x20 + x) * 4);
                     e != null; e = *(byte**)(e + 0x68))
                {
                    if ((*(uint*)e & 0x4000) != 0) continue;

                    // Les deux bits de poids fort de l'octet +0x07 — c'est-a-dire du mot d'etat —
                    // choisissent entre particule, modele statique et sa variante.
                    int kind = e[7] & 3;
                    byte** chunks = (byte**)Mem.Entity(0x005f7bc0)
                                  + (*Mem.Entity(0x00600838) & 0xff);

                    if (kind == 0)
                    {
                        Mem.s_Unknown_00564a4c = 0;
                        cursor = EmitParticle(otBase + *(short*)(e + 0x2e), cursor,
                            (int*)(e + 0x1c), 0, *(ushort*)(e + 0x0a));
                        Mem.s_Unknown_00564a4c = 1;
                    }
                    else if (kind == 1)
                    {
                        cursor = DrawWorldModel_0051C420(otBase, cursor, chunks[e[4]],
                            (int*)(e + 0x1c), *(ushort*)(e + 0x0a));
                    }
                    else if (kind == 2)
                    {
                        cursor = DrawWorldModelVariant_0051E700(otBase, cursor, chunks[e[4]],
                            (int*)(e + 0x1c), *(ushort*)(e + 0x0a));
                    }
                }
            }
        }

        // ---------------------------------------------------------- les trois listes de morceaux
        //
        // Le bloc de pose remis a neuf : position nulle, echelles 0x1000, orientation nulle,
        // teinte blanche. C'est le meme bloc de dix mots que DrawGroundDisc recopie.
        int* placement = stackalloc int[10];
        for (int i = 0; i < 10; i++) placement[i] = 0;
        placement[3] = 0x10001000;
        placement[4] = 0x1000;
        placement[7] = 0xffffff;

        byte* list = ((byte**)*(byte**)(s_Unknown_005e97cc + 0x80))[s_Unknown_005f2ce0[8]];

        if (VSyncDelayed() != 0) return;

        // Les trois bases sont trois octets consecutifs de DAT_00600830/34 ; les trois suites
        // d'octets se suivent dans le meme tampon, chacune close par 0xff.
        byte** table = (byte**)Mem.Entity(0x005f7bc0) + *Mem.Entity(0x00600832);
        for (; *list != 0xff; list++)
        {
            cursor = DrawChunkListA_0051FA40(otBase, cursor, table[*list], placement);
        }

        table = (byte**)Mem.Entity(0x005f7bc0) + *Mem.Entity(0x00600834);
        for (list++; *list != 0xff; list++)
        {
            cursor = DrawChunkListB_00521000(otEntry, cursor, table[*list], placement);
        }

        table = (byte**)Mem.Entity(0x005f7bc0) + *Mem.Entity(0x00600836);
        for (list++; *list != 0xff; list++)
        {
            cursor = DrawChunkListC_00522140(otEntry, cursor, table[*list], placement);
        }

        // ---------------------------------------------------------- le ciel
        if ((**(byte**)Mem.Entity(0x005eca80) & 0x40) != 0)
        {
            int* sky = *(int**)*(byte**)(s_Unknown_005e97cc + 0x3c);
            cursor = Prims.DrawSpriteVariant(otEntry, cursor, sky, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otEntry, cursor, sky);
        }
    }

    /// <summary>
    /// Ghidra: FUN_0051f940 @ 0x0051f940 (18 l.) — <b>le voile plein ecran</b> : un rectangle plat
    /// de cinq mots, chaine dans la table d'affichage avec la longueur 0x04000000. La couleur est
    /// prise sur 24 bits et le voile est saute si elle est nulle.
    /// </summary>
    private static uint* FillScreenQuad_0051F940(
        uint* otEntry, uint* prim, uint colour, uint x, short y, uint width, short height)
    {
        if ((colour & 0xffffff) == 0) return prim;

        prim[2] = colour & 0xffffff | 0x62000000;
        prim[1] = 0xe1000200;
        prim[3] = (uint)((int)y << 0x10) | x & 0xffff;
        prim[4] = (uint)((int)height << 0x10) | width & 0xffff;
        *prim = *otEntry | 0x4000000;
        *otEntry = (uint)(nuint)prim;
        return prim + 5;
    }

    /// <summary>Ghidra: FUN_0051e700 @ 0x0051e700 — la seconde voie d'emission d'un modele de
    /// decor, sœur de <c>FUN_0051c420</c> : meme signature a cinq arguments, meme place dans les
    /// deux grilles, mais un emplacement d'ordering table calcule **par face**. Transcrite dans
    /// <c>Engine/Model3d.cs</c>, a cote de ses deux sœurs.</summary>
    private static uint* DrawWorldModelVariant_0051E700(
        uint* otBase, uint* prim, byte* model, int* placement, uint flags)
        => Mdl.DrawDepthSortedModel(otBase, prim, model, placement, flags);

    /// <summary>Ghidra: FUN_0051fa40 @ 0x0051fa40 — la premiere des trois listes de morceaux,
    /// celle qui se dessine sur la base d'ordering table et trie chaque face a sa profondeur :
    /// <b>le terrain</b>. Transcrite dans <c>Engine/Model3d.cs</c>.</summary>
    private static uint* DrawChunkListA_0051FA40(
        uint* otBase, uint* prim, byte* chunk, int* placement)
        => Mdl.DrawChunkCellsSorted(otBase, prim, chunk, placement);

    /// <summary>Ghidra: FUN_00521000 @ 0x00521000 — la deuxieme liste de morceaux : meme balayage
    /// de cellules, sans tri par face. Transcrite dans <c>Engine/Model3d.cs</c>.</summary>
    private static uint* DrawChunkListB_00521000(
        uint* otEntry, uint* prim, byte* chunk, int* placement)
        => Mdl.DrawChunkCells(otEntry, prim, chunk, placement);

    /// <summary>Ghidra: FUN_00522140 @ 0x00522140 — la troisieme liste de morceaux : les faces se
    /// prennent a la file dans l''en-tete, sans grille. Transcrite dans <c>Engine/Model3d.cs</c>.</summary>
    private static uint* DrawChunkListC_00522140(
        uint* otEntry, uint* prim, byte* chunk, int* placement)
        => Mdl.DrawChunkFaces(otEntry, prim, chunk, placement);

    /// <summary>Ghidra: FUN_0051c420 @ 0x0051c420 — l'emission d'un modele de decor. Transcrite
    /// dans <c>Engine/Model3d.cs</c>, a cote de sa sœur animee FUN_0051d1f0 : meme banc GTE, memes
    /// primitives, et plusieurs ecrans l'appellent.</summary>
    private static uint* DrawWorldModel_0051C420(
        uint* otEntry, uint* prim, byte* model, int* placement, uint flags)
        => Mdl.DrawStaticModel(otEntry, prim, model, placement, flags);
}
