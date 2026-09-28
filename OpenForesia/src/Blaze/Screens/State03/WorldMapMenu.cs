// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE MENU DE JEU — FUN_0052a850 et son enveloppe FUN_0052a7d0.
//
// C'est ce que la touche de menu ouvre depuis la carte du monde. Il n'a pas d'etat de jeu a lui :
// FUN_00516fc0 lui cede simplement sa branche d'affichage quand DAT_005de02c porte le bit 4 (voir
// « LA BRANCHE COURTE D'AFFICHAGE » dans WorldMapTick.cs), et le monde n'est plus dessine du tout.
// C'est pourquoi le fichier vit dans Screens/State03/ : un seul ecran l'appelle, et la regle de
// routage (2) d'ARCHITECTURE.md le laisse chez lui.
//
// SIX PAGES, un switch sur DAT_005de6f0, et 2533 lignes de pseudo-C dans Blaze.c.
//
// L'ecran s'ouvre, se dessine, change de page et d'onglet, choisit un objet, l'equipe ou le retire,
// regle la manette, pose un repere sur la carte, sauvegarde, et se referme.
//
// LE MENU EST ENTIEREMENT TRANSCRIT — les six pages (0 la fiche, 1 les objets de quete, 2 la dalle
// aux cristaux, 3 l'organisation du groupe, 4 la carte du monde et ses reperes, 5 « Options ») et
// les six onglets de la fiche (0 les statistiques, 1 l'inventaire, 2 l'equipement, 3 les sorts,
// 4 les herbes, 5 la manette). Plus aucun corps n'y manque : les trois levees qui restent sont des
// gardes sur des cas que les bornes rendent impossibles, pas des trous. Voir la carte au-dessus de
// RunPartyMenu_0052A850.
//
// Le prologue et l'epilogue sont verifies ligne a ligne au desassembleur (0x0052a850..0x0052aa1d et
// 0x0052f82c..0x0052f9c2), et il le fallait — trois fois :
//   * Ghidra reutilise `iVar29` pour deux choses differentes de part et d'autre du switch ;
//   * l'un des deux tests que sa pseudo-C ecrit sur un octet est un `cmp dword` dans le binaire ;
//   * son `if ((a == 1) || (X = 0, a == 2)) X = 1;` n'est PAS un if/else : la remise a zero n'a
//     lieu que sur le chemin `a != 1`.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>Ghidra: DAT_005de6f0 — <b>la page du menu</b>, 0 a 5. Nulle au chargement (relevee
    /// dans le binaire : l'adresse est dans la queue non initialisee de <c>.data</c>), donc le menu
    /// s'ouvre toujours sur la page 0. Elle survit a la fermeture : rouvrir le menu revient sur la
    /// page qu'on avait quittee.</summary>
    private static int s_MenuPage_005de6f0;

    /// <summary>
    /// Ghidra: FUN_0052a7d0 @ 0x0052a7d0 — <b>l'enveloppe du menu</b> : elle arme la fenetre de
    /// decoupe de l'interface, appelle <see cref="RunPartyMenu_0052A850"/>, puis la desarme.
    ///
    /// <para>
    /// Les trois dwords <c>DAT_005de154</c> / <c>_158</c> / <c>_15c</c> sont ceux que
    /// <c>UiWindow.EmitClipRect</c> relit : <c>_15c</c> est l'interrupteur, les deux autres les
    /// coins. Le meme calcul apparait mot pour mot dans <c>Screens/State01/PartySetup.cs</c> —
    /// largeur figee a <c>0xa0</c> et hauteur <c>(0xb - taille) * 0xc</c> des que l'option « Small »
    /// est active ou que la taille descend sous 8.
    /// </para>
    /// <para>
    /// <b>Ghidra la declare <c>void</c> et c'est faux.</b> Le site d'appel range son <c>eax</c>
    /// dans l'emplacement du curseur de primitives, et la fonction ne touche pas <c>eax</c> apres
    /// l'appel a <c>FUN_0052a850</c> (<c>mov dword [5de15ch],0</c> n'ecrit pas de registre) : elle
    /// rend donc le curseur que le menu a fait avancer. Verifie a 0x0052a841.
    /// </para>
    /// </summary>
    private static uint* RunPartyMenuFrame_0052A7D0(
        uint* otBase, uint* otEntry, uint* cursor, byte* slot, ushort* pad)
    {
        s_Unknown_005de15c = 1;

        if (s_SmallTextureFlag == 0 && Mem.s_SizeSetting_005de160 > 7)
        {
            s_Unknown_005de158 = 0;
            s_Unknown_005de154 = 0;
        }
        else
        {
            s_Unknown_005de154 = 0xa0;
            s_Unknown_005de158 = (0xb - Mem.s_SizeSetting_005de160) * 0xc;
        }

        uint* result = RunPartyMenu_0052A850(otBase, otEntry, cursor, slot, pad);

        s_Unknown_005de15c = 0;
        return result;
    }

    /// <summary>
    /// Ghidra: FUN_0052a850 @ 0x0052a850 — <b>le menu de jeu</b>, 2533 lignes de pseudo-C.
    ///
    /// <para>
    /// <b>Etat : le prologue, l'epilogue et la page 0 sont transcrits.</b> Le prologue
    /// (0x0052a850..0x0052aa1d) fait le changement de page, les deux pages que l'etat de la partie
    /// fait sauter, la remise a neuf de l'etat de page, le recadrage de la carte sur le meneur, et
    /// la fermeture. L'epilogue (0x0052f82c..0x0052f9c2) dessine les panneaux et le curseur, et il
    /// est <b>partage par les six pages</b>. Chaque page restante leve en se nommant, avec sa plage
    /// dans <c>Blaze.c</c>.
    /// </para>
    ///
    /// <list type="table">
    /// <listheader><term>Page</term><description>Blaze.c · taille · role observe</description></listheader>
    /// <item><term>0</term><description>75336..76574 — <b>1238 l.</b> la fiche de personnage : choix du
    /// membre (L1/R1), choix de l'onglet (gauche/droite), et le pave de statistiques recalcule a
    /// chaque image par les cinq passes <c>FUN_00538ab0</c>..<c>FUN_00538fa0</c></description></item>
    /// <item><term>1</term><description>76574..76672 — <b>98 l.</b> la page « Objets », celle des
    /// objets de quete ; sautee quand <c>DAT_005e9840</c> est nul, c'est-a-dire tant que le groupe
    /// n'en porte aucun</description></item>
    /// <item><term>2</term><description>76672..76697 — <b>25 l.</b> la page « Table », la dalle aux
    /// cristaux ; sautee quand le bit <c>0x10</c> de <c>DAT_005e9001</c> est absent</description></item>
    /// <item><term>3</term><description>76697..76833 — <b>136 l.</b> « Organisation du groupe » :
    /// le meneur, la mise de cote, et la couleur de chaque personnage</description></item>
    /// <item><term>4</term><description>76833..77060 — <b>227 l.</b> la carte du monde : elle
    /// defile, porte huit reperes poses par le joueur, et dessine le groupe en modele 3d</description></item>
    /// <item><term>5</term><description>77060..77698 — <b>638 l.</b> la plus grosse apres la 0 :
    /// « Options », sauvegarder sur carte memoire ou revenir au menu d'accueil</description></item>
    /// </list>
    ///
    /// <para>
    /// <b>Les quarante-trois appelees du menu sont maintenant toutes transcrites.</b> Les quatre qui
    /// manquaient — <c>FUN_0052fab0</c>, <c>FUN_0052fba0</c>, <c>FUN_0052fdc0</c>,
    /// <c>FUN_0051bd50</c> — le sont depuis le 2026-08-25 : ce qui reste du menu est du corps a
    /// transcrire, plus aucune dependance.
    /// </para>
    ///
    /// <para>
    /// <b>Deux pieges du prologue, tous deux invisibles dans <c>Blaze.c</c> :</b>
    /// </para>
    /// <list type="bullet">
    /// <item><c>DAT_005e9840</c> est compare en <b>dword</b> (<c>cmp [5E9840h],edi</c> a 0x0052a8bb),
    /// pas en octet. <c>DAT_005e9001</c>, lui, est bien un octet (<c>test byte</c>).</item>
    /// <item>Ghidra reutilise <c>iVar29</c> pour la page precedente ici et pour tout autre chose
    /// dans la page 0 : le binaire <b>recharge</b> <c>esi</c> depuis la pile en 0x0052aa0b, ou il
    /// vaut <c>param_1</c>. Ne pas faire circuler la variable de Ghidra a travers le switch.</item>
    /// </list>
    /// </summary>
    /// <param name="slot">Ghidra: <c>param_4</c> — la fiche du membre suivi,
    /// <c>&amp;DAT_005e93c0 + DAT_005de540 * 0x27</c>.</param>
    /// <param name="pad">Ghidra: <c>param_5</c> — l'etat de manette, lu a plusieurs largeurs.</param>
    private static uint* RunPartyMenu_0052A850(
        uint* otBase, uint* otEntry, uint* cursor, byte* slot, ushort* pad)
    {
        int previousPage = s_MenuPage_005de6f0;

        // Les deux pages qui se figent : la 5 tant que DAT_005de710 n'est pas negatif, la 0 tant
        // que DAT_005de70c ne l'est pas. Ce sont les deux verrous « une operation est en cours »,
        // et ils court-circuitent TOUTE la navigation, fermeture comprise.
        bool navigable = !(s_MenuPage_005de6f0 == 5 && s_Unknown_005de710 >= 0)
                      && !(s_MenuPage_005de6f0 == 0 && s_Unknown_005de70c >= 0);

        if (navigable)
        {
            if ((((byte*)pad)[3] & 4) != 0)
            {
                s_MenuPage_005de6f0--;
                RefreshPartyAbilityMask();                       // Ghidra: FUN_00535710

                // En reculant : la page 2 se saute vers la 1 si le bit 0x10 manque, puis la 1 se
                // saute vers la 0 si la banque est vide. Les deux tests s'enchainent, donc un
                // recul depuis la 3 peut atterrir directement sur la 0.
                if (s_MenuPage_005de6f0 == 2 && (*Mem.Arena(0x005e9001) & 0x10) == 0)
                {
                    s_MenuPage_005de6f0 = 1;
                }

                if (s_MenuPage_005de6f0 == 1 && *(int*)Mem.Arena(0x005e9840) == 0)
                {
                    s_MenuPage_005de6f0 = 0;
                }
            }

            if ((((byte*)pad)[3] & 8) != 0)
            {
                s_MenuPage_005de6f0++;
                RefreshPartyAbilityMask();                       // Ghidra: FUN_00535710

                // En avancant, les deux memes sauts dans l'autre sens.
                if (s_MenuPage_005de6f0 == 1 && *(int*)Mem.Arena(0x005e9840) == 0)
                {
                    s_MenuPage_005de6f0 = 2;
                }

                if (s_MenuPage_005de6f0 == 2 && (*Mem.Arena(0x005e9001) & 0x10) == 0)
                {
                    s_MenuPage_005de6f0 = 3;
                }
            }

            if (s_MenuPage_005de6f0 < 0) s_MenuPage_005de6f0 = 5;
            if (s_MenuPage_005de6f0 > 5) s_MenuPage_005de6f0 = 0;

            if (previousPage != s_MenuPage_005de6f0)
            {
                s_Unknown_005de6f8 = 0;
                s_Unknown_005de6fc = 0;
                s_Unknown_005de704 = 0;
                s_Unknown_005de70c = -1;
                s_Unknown_005de710 = -1;
                s_Unknown_005de718 = -1;

                RecentreMapOnLeader();

                Aud.PlayUiSound3A();                             // Ghidra: FUN_0053db60
            }

            // La fermeture. Elle rend le curseur INCHANGE — rien n'a ete dessine ce tour-ci — et
            // c'est le seul `return` du corps hors du switch.
            if ((((byte*)pad)[2] & 8) != 0)
            {
                (*Mem.Arena(0x005e9ae4))++;
                Mem.s_WorldFlags_005de02c &= 0xffffffbb;
                *Mem.Entity(0x005fa580) = 1;
                return cursor;
            }
        }

        switch (s_MenuPage_005de6f0)
        {
            case 0:
                cursor = RunCharacterSheetPage(otBase, cursor, slot, pad);
                break;

            case 1:
                cursor = RunKeyItemsPage(otBase, cursor, slot, pad);
                break;

            case 2:
                cursor = RunCrystalTablePage(otBase, cursor, slot);
                break;

            case 3:
                cursor = RunPartyOrderPage(otBase, cursor, slot, pad);
                break;

            case 4:
                cursor = RunWorldMapPage(otBase, cursor, slot, pad);
                break;

            case 5:
                cursor = RunSavePage(otBase, cursor, slot, pad);
                break;

            default:
                // Le binaire a bien un cas par defaut (0x0052f82c, atteint par `ja`), mais les
                // deux bornes ci-dessus rendent la page toujours comprise entre 0 et 5. Il ne peut
                // etre atteint que par un des deux court-circuits, qui n'ecrivent pas la page.
                throw new NotImplementedException(
                    $"FUN_0052a850 : page {s_MenuPage_005de6f0} hors de 0..5. Le binaire aiguille " +
                    "ce cas sur 0x0052f82c, non transcrit. Si cela se produit, c'est que la page " +
                    "a ete ecrite ailleurs qu'ici.");
        }

        // =================================================================================
        // L'EPILOGUE PARTAGE — 0x0052f82c..0x0052f9c2.
        //
        // Les six pages y retombent toutes : c'est ici que les panneaux sont reellement
        // dessines. L'etiquette 0x0052f82c sert a la fois de cas par defaut du saut indexe et
        // de point de chute de toutes les pages.
        // =================================================================================

        // Ghidra ecrit ce test avec une virgule : `if ((tab == 1) || (X = 0, tab == 2)) X = 1;`.
        // Le binaire est plus simple, et pas equivalent a un `if/else` : la remise a zero
        // n'a lieu QUE sur le chemin `tab != 1`.
        //   0052f831  cmp eax,1 ; je 0052F842
        //   0052f836  cmp eax,2 ; mov byte [5696E3h],0 ; jne 0052F849
        //   0052f842  mov byte [5696E3h],1
        if (s_Unknown_005de704 != 1) MenuDescriptor(2)[0x23] = 0;
        if (s_Unknown_005de704 == 1 || s_Unknown_005de704 == 2) MenuDescriptor(2)[0x23] = 1;

        byte* pageDescriptor = MenuDescriptor(1);
        if (s_MenuPage_005de6f0 == 0)
        {
            *(ushort*)(pageDescriptor + 8) = 0x48;
            *(ushort*)(pageDescriptor + 0x0a) = 0x40;
            pageDescriptor[0x23] = 0;
        }
        else
        {
            *(ushort*)(pageDescriptor + 8) = 0xe0;
            *(ushort*)(pageDescriptor + 0x0a) = 0x98;
            pageDescriptor[0x23] = 1;
        }

        byte* rootDescriptor = MenuDescriptor(0);
        if (s_MenuPage_005de6f0 == 4)
        {
            *(ushort*)(rootDescriptor + 6) = 8;
            *(ushort*)(rootDescriptor + 0x0a) = 0xc0;
        }
        else
        {
            *(ushort*)(rootDescriptor + 6) = 0xa0;
            *(ushort*)(rootDescriptor + 0x0a) = 0x28;
        }

        cursor = Ui.RunTextEngine(cursor, rootDescriptor);

        // Ghidra rend le compte de panneaux par deux `neg/sbb`, l'idiome du booleen etendu :
        //   edx = (page != 0) ? -2 : 0, puis +3   ->  (page != 0) ? 1 : 3
        //   ecx = (page != 4) ? edx : 0
        // Soit : la page 4 n'en dessine qu'un, la page 0 en dessine quatre, les autres deux.
        // Ils partent du DERNIER et redescendent — l'ordre compte, chacun s'empile devant le
        // precedent.
        int topDescriptor = s_MenuPage_005de6f0 == 4
            ? 0
            : (s_MenuPage_005de6f0 != 0 ? 1 : 3);

        for (int i = topDescriptor; i >= 0; i--)
        {
            cursor = UiWin.DrawUiPanel(otBase, cursor, MenuDescriptor(i));
        }

        // Le curseur de l'ecran, monte a la main dans l'arene d'entites plutot que pris dans un
        // atlas : huit octets de descripteur, ecrits a chaque image.
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfc) = 0x80;   // Ghidra: DAT_005e8ffc
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xfe) = 0;      // Ghidra: DAT_005e8ffe

        byte* cursorSprite = Mem.Entity(0x005eca88);
        cursorSprite[0] = 0;
        cursorSprite[1] = 0;
        cursorSprite[2] = 0;
        cursorSprite[3] = 0xdf;
        cursorSprite[4] = 0;
        cursorSprite[5] = 1;
        cursorSprite[6] = 0x3f;
        cursorSprite[7] = 0;

        byte* sprite = s_MenuSprite_005de5f8;
        *(short*)(sprite + 8) = 0x80;
        *(short*)(sprite + 0x0a) = 0x70;
        *(short*)(sprite + 0x0c) = 0x1400;
        *(short*)(sprite + 0x0e) = 0x1249;
        *(short*)(sprite + 0x10) = 0;
        *(byte**)sprite = cursorSprite;

        // Les deux derniers appels prennent **otEntry** et non otBase — seul endroit du corps
        // ou la difference se voie, et le binaire est formel (`mov esi,[esp+20h]` en 0x0052f915
        // recharge param_2 juste avant).
        cursor = Prims.DrawSpritePrimitive(otEntry, cursor, (int*)sprite, 0, 0);
        return Prims.DrawFlatPrimitive(otEntry, cursor, (int*)sprite);
    }

    // =====================================================================================
    // LA PAGE 0 — LA FICHE DE PERSONNAGE
    //
    // 0x0052aa24..0x0052d2b9 dans le binaire, 75336..76574 dans Blaze.c. Extraite en methode
    // parce que c'est une unite entiere — un ecran avec sa navigation, son etat et son dessin —
    // et non une etiquette de saut promue mecaniquement.
    // =====================================================================================

    /// <summary>
    /// Ghidra: le <c>case 0</c> de <c>FUN_0052a850</c> — <b>la fiche de personnage</b>, et la page
    /// sur laquelle le menu s'ouvre toujours.
    ///
    /// <para>
    /// La tete choisit le membre (les deux gachettes), recalcule ses statistiques, choisit
    /// l'onglet (gauche/droite), dessine le portrait et la barre d'onglets, puis aiguille sur
    /// l'onglet. <b>Les six onglets ne sont pas les six positions</b> : chaque classe a sa propre
    /// liste, lue dans <see cref="s_ClassTabs_0056a898"/>.
    /// </para>
    /// <para>
    /// <b>Ghidra reutilise <c>iVar29</c> pour deux choses.</b> Dans le test « le membre a change »,
    /// ce n'est PAS la page precedente venue du prologue : le binaire capture l'ancien
    /// <c>DAT_005de708</c> dans <c>edi</c> en 0x0052aa35, juste avant les deux ajustements. La
    /// pseudo-C laisse croire le contraire.
    /// </para>
    /// <para>
    /// <c>DAT_005de708</c> est <b>signe</b> — <c>test eax,eax ; jge</c> en 0x0052aa68 — et c'est ce
    /// qui fait boucler la selection sur le dernier membre.
    /// </para>
    /// </summary>
    private static uint* RunCharacterSheetPage(uint* otBase, uint* cursor, byte* slot, ushort* pad)
    {
        // ---- le choix du membre, tant qu'aucune operation n'est en cours -------------------
        if (s_Unknown_005de718 < 0)
        {
            int previousMember = s_Unknown_005de708;

            // Ghidra: FUN_0040a600() apres chaque pas — corps vide verifie.
            if ((pad[1] & 0x10) != 0) s_Unknown_005de708++;
            if ((pad[1] & 0x40) != 0) s_Unknown_005de708--;

            if (s_Unknown_005de708 < 0) s_Unknown_005de708 = s_PartySize_005e9020 - 1;
            if (s_Unknown_005de708 >= s_PartySize_005e9020) s_Unknown_005de708 = 0;

            if (previousMember != s_Unknown_005de708)
            {
                pad[1] &= 0x3f5f;
                pad[3] &= 0xff5f;
                s_Unknown_005de70c = -1;
                s_Unknown_005de6f8 = 0;
                s_Unknown_005de6fc = 0;
                Aud.PlayUiSound3A();
            }
        }

        int member = s_Unknown_005de708;
        byte* memberSlot = Inp.s_SlotFlags_005e93c0 + member * 0x9c;
        byte characterIndex = memberSlot[8];
        byte* character = GetMainMemPtr() + ((characterIndex + 0x78) << 0xd);

        // Les cinq passes de statistiques, dans cet ordre. Elles sont refaites A CHAQUE IMAGE :
        // la fiche affiche un total recalcule, pas un total range quelque part.
        CommitStagedStatBlock(character);            // Ghidra: FUN_00538ab0
        ScaleStatBlockByDifficulty(character + 0x100);
        ComputeDerivedStats(character);
        ApplyEquipmentStatBonuses(character);
        FinalizeEquipmentEffects(character + 0x100);

        // ---- le choix de l'onglet, et l'action « ce membre prend la tete » ------------------
        if (s_Unknown_005de718 < 0)
        {
            int previousTab = s_Unknown_005de704;

            if ((pad[1] & 0x80) != 0) s_Unknown_005de704--;
            if ((pad[1] & 0x20) != 0) s_Unknown_005de704++;

            if (previousTab != s_Unknown_005de704)
            {
                pad[1] &= 0x3f5f;
                pad[3] &= 0xff5f;
                s_Unknown_005de70c = -1;
                s_Unknown_005de6f8 = 0;
                s_Unknown_005de6fc = 0;
                Aud.PlayUiSound3A();
            }

            if ((pad[1] & 0x8000) != 0)
            {
                pad[1] &= 0x3fff;
                s_Unknown_005de718 = 0;
                Aud.PlayUiSound38();

                // Le bloc entier est garde par le verrou de mise au point : hors « Gurkha », la
                // validation sur le premier onglet ne fait rien de plus que le son ci-dessus.
                // Le `DAT_005de718 == 0` que Ghidra teste vient d'etre pose deux lignes plus
                // haut — toujours vrai, conserve tel quel.
                if (Mem.s_DebugModeLatch_005633a8 == 0
                    && s_Unknown_005de718 == 0
                    && s_Unknown_005de704 == 0)
                {
                    byte* leaderSlot = Inp.s_SlotFlags_005e93c0
                                       + (uint)Mem.s_Unknown_005de538 * 0x9c;

                    if ((*(uint*)memberSlot & 0x40000000) != 0)
                    {
                        uint flags = *(uint*)memberSlot & 0xefdf8ff7;
                        *(uint*)memberSlot = flags;
                        *(short*)(memberSlot + 0x12) = 0;

                        if (Mem.s_Unknown_005de538 != characterIndex)
                        {
                            *(uint*)memberSlot = flags | 0x10000;
                            character[0x2b8] = 1;
                            Inp.s_PlayerSlots_005e9188[characterIndex * 0x4c] = 8;
                        }

                        // La position et le bloc de +0x7c du MENEUR sont recopies vers le membre
                        // choisi — c'est lui qui vient se poser sur le meneur, pas l'inverse.
                        Mem.CopyDwords((uint*)(leaderSlot + 0x1c), (uint*)(memberSlot + 0x1c), 10);
                        Mem.CopyDwords((uint*)(leaderSlot + 0x7c), (uint*)(memberSlot + 0x7c), 6);
                    }

                    *(uint*)memberSlot = (*(uint*)memberSlot & 0xbffffff9) | 0x1000000;
                    *(int*)(memberSlot + 0x18) = 0;
                    *(short*)(character + 0x14c) = *(short*)(character + 0x148);
                    *(short*)(character + 0x14e) = *(short*)(character + 0x14a);
                    *(int*)(character + 0x140) = 0;
                    for (int k = 0; k < 0x20; k++) ((uint*)(character + 0x180))[k] = 0;

                    Aud.PlayUiSound38();
                }
            }
        }

        // ---- la liste des entrees utilisables -----------------------------------------------
        // Dix entrees de 0x280 octets a partir de +0x300. Une entree dont le premier octet n'est
        // pas nul entre dans la liste courte de +0x2c1, dont +0x2c0 tient la longueur.
        character[0x2c0] = 0;
        for (int entry = 0; entry < 10; entry++)
        {
            byte* record = character + 0x300 + entry * 0x280;
            if (record[0] == 0) continue;

            // En mise au point, tout ce qui est listé est aussi marqué disponible.
            if (Mem.s_DebugModeLatch_005633a8 == 0) record[0x13] |= 0x80;

            character[0x2c1 + character[0x2c0]] = (byte)entry;
            character[0x2c0]++;
        }

        byte* sprite = s_MenuSprite_005de5f8;

        // ---- le portrait --------------------------------------------------------------------
        byte* portraitAtlas = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 4);
        *(byte**)sprite = portraitAtlas;
        portraitAtlas[5] = 0;                       // Ghidra: *(DAT_005de5f8 + 5) = 0
        *(ushort*)(sprite + 0x10) = memberSlot[4];
        *(short*)(sprite + 8) = 0x20;
        *(short*)(sprite + 0x0a) = 0x18;
        *(uint*)(sprite + 4) = 0x1000000;

        UiWin.EmitViewportRect(otBase, &cursor);
        cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, character[0x2bd]);
        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        cursor = DrawDecorList_0052FAB0(otBase, cursor, s_MenuDecorHead_0056a8e0);

        // ---- les quatre passes de texte de la fiche ------------------------------------------
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* sheet = MenuDescriptor(3);

        BindMenuLabels(*(byte**)(labelTable + 0x360), (uint)(nint)slot, sheet, 0);
        cursor = Ui.RunTextEngine(cursor, sheet);

        // Le bloc de statistiques du personnage EST un flux d'etiquettes : il part tel quel dans
        // le moteur de texte, sans passer par la table de libelles.
        BindMenuLabels(character + 0x100, (uint)(nint)slot, sheet, 0);
        cursor = Ui.RunTextEngine(cursor, sheet);

        BindMenuLabels(
            *(byte**)(labelTable + 800 + character[0x150] * 4), (uint)(nint)slot, sheet, 0);
        cursor = Ui.RunTextEngine(cursor, sheet);

        // Le pictogramme de classe respire sur trois images, decale par la classe.
        Mem.s_PanelSelection_005e9060[0x3c] = 1;
        Mem.s_PanelSelection_005e9060[0x3d] = 8;
        Mem.s_PanelSelection_005e9060[0x3e] =
            (byte)(((s_FadeCounter_005e9960 >> 2) & 0x1fffffff) % 3
                   + (uint)(sbyte)character[0x151] * 3);

        BindMenuLabels(*(byte**)(labelTable + 0x368), (uint)(nint)slot, sheet, 0);
        cursor = Ui.RunTextEngine(cursor, sheet);

        // ---- la barre d'onglets --------------------------------------------------------------
        int classRow = character[0x150] & 7;
        if (s_Unknown_005de704 < 0)
        {
            s_Unknown_005de704 = (sbyte)s_ClassTabs_0056a898[classRow * 6] - 1;
        }

        if ((sbyte)s_ClassTabs_0056a898[classRow * 6] <= s_Unknown_005de704)
        {
            s_Unknown_005de704 = 0;
        }

        sbyte tab = (sbyte)s_ClassTabs_0056a898[classRow * 6 + 1 + s_Unknown_005de704];
        byte tabIcon = s_TabIcons_0056a8c8[tab];

        UiWin.EmitViewportRect(otBase, &cursor);

        byte* iconAtlas = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        *(byte**)sprite = iconAtlas;
        *(ushort*)(sprite + 0x10) = tabIcon;

        // La barre est centree : (0x98 - largeur du pictogramme - largeur de la fleche) / 2.
        // Les deux largeurs se lisent dans l'atlas lui-meme, a +2 de leur cellule de huit octets.
        *(short*)(sprite + 8) =
            (short)((0x98 - iconAtlas[tabIcon * 8 + 2] - iconAtlas[0x22]) / 2 + 0x58);
        *(short*)(sprite + 0x0a) = 0x14;
        cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);

        *(ushort*)(sprite + 0x10) = 4;
        *(short*)(sprite + 8) += iconAtlas[tabIcon * 8 + 2];
        cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);

        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        // Le binaire relit les deux verrous d'onglet juste avant le saut indexe, et l'onglet 5 se
        // sert de ce temoin : c'est en le comparant a la valeur d'apres ses fleches qu'il sait s'il
        // doit sonner. Rien entre ici et le saut ne touche a DAT_005de6f8, mais la capture est
        // gardee a sa place pour que la comparaison de l'onglet 5 reste lisible.
        int rowBefore = s_Unknown_005de6f8;
        int listBefore = s_Unknown_005de6fc;

        switch (tab)
        {
            case 0:
                return DrawStatsTab(otBase, cursor, slot, character);

            case 1:
                return DrawInventoryTab(otBase, cursor, slot, memberSlot, character, pad);

            case 2:
                return DrawEquipmentTab(otBase, cursor, slot, memberSlot, character, pad);

            case 3:
                return DrawSpellTab(otBase, cursor, slot, character, pad, listBefore);

            case 4:
                return DrawHerbTab(otBase, cursor, slot, character, pad, rowBefore);

            case 5:
                return DrawConfigTab(otBase, cursor, slot, character, pad, rowBefore);

            default:
                throw new NotImplementedException(
                    $"FUN_0052a850, page 0 : l'onglet {tab} sort des six que le binaire aiguille. " +
                    $"Il vient de s_ClassTabs_0056a898, ligne {classRow}, position " +
                    $"{s_Unknown_005de704}.");
        }
    }

    /// <summary>
    /// Ghidra: le <c>case 0</c> du switch d'onglets — <b>le pave de statistiques</b>. Blaze.c
    /// 75488..75674.
    ///
    /// <para>
    /// Il redemande les cinq passes de statistiques — une seconde fois dans la meme image, la tete
    /// de page les ayant deja faites — puis dessine : le niveau, l'or, la jauge de points de vie et
    /// de magie avec leur teinte, sept caracteristiques, quatre resistances, l'experience, et la
    /// rangee de pictogrammes d'etat.
    /// </para>
    /// <para>
    /// <b>La teinte des jauges</b> vient d'un rapport sur 255 : sous la moitie, la composante rouge
    /// est pleine et la verte suit le rapport ; au-dessus, l'inverse. Les deux branches assemblent
    /// leur couleur par des decalages que Ghidra rend en <c>CONCAT31</c> — c'est
    /// <c>(rapport &lt;&lt; 9) | 0xff</c> d'un cote et <c>(rapport + 0x7f80) * 0x20000 | 0xffff</c>
    /// de l'autre.
    /// </para>
    /// <para>
    /// Le bloc garde par <c>DAT_005633a8</c> — les tricheries de mise au point et les quatre rangs
    /// supplementaires — est transcrit avec le reste : il ne s'atteint qu'apres « Gurkha », mais il
    /// fait partie du corps.
    /// </para>
    /// </summary>
    private static uint* DrawStatsTab(uint* otBase, uint* cursor, byte* slot, byte* character)
    {
        byte* sprite = s_MenuSprite_005de5f8;

        // Les cinq passes, une seconde fois. Elles sont dans le binaire aux deux endroits.
        CommitStagedStatBlock(character);
        ScaleStatBlockByDifficulty(character + 0x100);
        ComputeDerivedStats(character);
        ApplyEquipmentStatBonuses(character);
        FinalizeEquipmentEffects(character + 0x100);

        cursor = DrawDecorList_0052FAB0(otBase, cursor, s_MenuDecorStats_0056a8f0);

        // ---- le niveau -----------------------------------------------------------------------
        *(byte**)sprite = SpriteAtlas(0x00566c30);
        *(short*)(sprite + 8) = 0x74;
        *(short*)(sprite + 0x0a) = 0x20;
        *(uint*)(sprite + 4) = 0x1000000;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, *(ushort*)(character + 0x144), 5, 0);

        // ---- la piece de monnaie -------------------------------------------------------------
        UiWin.EmitViewportRect(otBase, &cursor);
        *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        *(ushort*)(sprite + 0x10) = 0x4a;
        *(short*)(sprite + 8) = 0xb4;
        cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        // ---- l'or ------------------------------------------------------------------------------
        *(byte**)sprite = SpriteAtlas(0x00566c30);
        *(short*)(sprite + 8) = 0xbc;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, *(uint*)(character + 0x26c), 0x85, 0);

        // ---- les quatre rangs de mise au point --------------------------------------------------
        if (Mem.s_DebugModeLatch_005633a8 == 0)
        {
            *(short*)(sprite + 8) = 0xd4;
            *(short*)(sprite + 0x0a) = 0xac;

            for (int i = 4; i < 8; i++)
            {
                cursor = Prims.DrawDecimalNumber(
                    otBase, cursor, (int*)sprite,
                    (uint)*(short*)(character + 0x130 + (sbyte)s_StatOrder_0056a8d0[i] * 2),
                    0x83, 0);
                *(short*)(sprite + 0x0a) += 8;
            }
        }

        // ---- les points de vie, puis les points de magie -----------------------------------------
        *(short*)(sprite + 0x0a) = 0x2c;
        *(short*)(sprite + 8) = 0x84;
        SetGaugeTint(sprite, *(short*)(character + 0x14c), *(short*)(character + 0x148));
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, (uint)*(short*)(character + 0x14c), 0x84, 0);

        *(short*)(sprite + 8) += 0x28;
        *(uint*)(sprite + 4) = 0x1000000;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, (uint)*(short*)(character + 0x148), 4, 0);

        *(short*)(sprite + 0x0a) += 8;
        *(short*)(sprite + 8) = 0x84;
        SetGaugeTint(sprite, *(short*)(character + 0x14e), *(short*)(character + 0x14a));
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, (uint)*(short*)(character + 0x14e), 0x84, 0);

        *(short*)(sprite + 8) += 0x28;
        *(uint*)(sprite + 4) = 0x1000000;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, (uint)*(short*)(character + 0x14a), 4, 0);

        // ---- les sept caracteristiques, en deux colonnes ------------------------------------------
        // Ghidra: `CONCAT22(y, x)` sur le dword de +8 — la paire s'ecrit d'un coup.
        short* stats = (short*)(character + 0x120);
        uint dim = *(short*)(character + 0x1ec) != 0 ? 7u : 0u;

        for (int i = 0; i < 7; i++)
        {
            *(short*)(sprite + 8) = (short)((i & 1) * 0x44 + 0x84);
            *(short*)(sprite + 0x0a) = (short)((i >> 1) * 8 + 0x4c);
            cursor = Prims.DrawDecimalNumber(
                otBase, cursor, (int*)sprite, (uint)stats[i], 0x83, dim);
        }

        // ---- les quatre resistances ---------------------------------------------------------------
        for (int i = 0; i < 4; i++)
        {
            *(short*)(sprite + 8) = (short)((i & 1) * 0x44 + 0x84);
            *(short*)(sprite + 0x0a) = (short)((i >> 1) * 8 + 0x70);

            byte state = character[0x208 + (sbyte)s_StatOrder_0056a8d0[i]];
            uint tint = state == 0 ? 0u : ((state & 0x80) != 0 ? 7u : 2u);

            cursor = Prims.DrawDecimalNumber(
                otBase, cursor, (int*)sprite,
                (uint)*(short*)(character + 0x130 + (sbyte)s_StatOrder_0056a8d0[i] * 2),
                0x83, tint);
        }

        // ---- l'experience : acquise, puis ce qui reste --------------------------------------------
        *(short*)(sprite + 8) = 0x7c;
        *(short*)(sprite + 0x0a) = 0x98;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, *(uint*)(character + 0x158), 6, 0);

        uint needed = (uint)((*(ushort*)(character + 0x146) >> 2)
                             + 0x30 + *(ushort*)(character + 0x146) * 2);
        if (needed > 9999) needed = 9999;

        *(short*)(sprite + 8) = 0xc4;
        *(short*)(sprite + 0x0a) = 0x98;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, needed - *(uint*)(character + 0x158), 4, 0);

        // ---- les neuf pictogrammes d'etat -----------------------------------------------------------
        // L'indice avance a chaque rang, dessine ou non : les neuf descripteurs se suivent a partir
        // de 0x005648b8, et c'est le rang qui choisit lequel. Meme regle que dans la carte du monde.
        *(ushort*)(sprite + 0x10) = 0;
        *(short*)(sprite + 8) = 0x7c;
        *(short*)(sprite + 0x0a) = 0x88;
        *(byte**)sprite = SpriteAtlas(0x005648b8);

        UiWin.EmitViewportRect(otBase, &cursor);
        for (int bit = 0; bit < s_StatusBits_00564900.Length; bit++)
        {
            if ((*(uint*)(character + 0x140) & s_StatusBits_00564900[bit]) != 0)
            {
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            }

            *(short*)(sprite + 8) += 8;
            *(ushort*)(sprite + 0x10) += 1;
        }

        *(ushort*)(sprite + 0x10) = 0;
        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        // ---- les deux dernieres liaisons d'etiquettes, sur le descripteur racine -------------------
        // La seconde n'est PAS suivie d'un RunTextEngine : l'epilogue partage s'en charge.
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* root = MenuDescriptor(0);

        BindMenuLabels(*(byte**)(labelTable + 0x2d0), (uint)(nint)slot, root, 0);
        cursor = Ui.RunTextEngine(cursor, root);

        BindMenuLabels(
            *(byte**)(labelTable + 0x500 + *(int*)(character + 0x1ff8) * 4),
            (uint)(nint)slot, root, 0);

        s_Unknown_005de718 = -1;
        return cursor;
    }

    /// <summary>La teinte d'une jauge, celle que le pave de statistiques pose deux fois. Le rapport
    /// est sur 255 ; sous la moitie le rouge est plein, au-dessus c'est le vert.
    /// <para>Ghidra rend les deux assemblages en <c>CONCAT31</c> ; developpes, ce sont
    /// <c>(rapport &lt;&lt; 9) &amp; 0xffffff00 | 0xff</c> et
    /// <c>(rapport + 0x7f80) * 0x20000 | 0xffff</c>.</para></summary>
    private static void SetGaugeTint(byte* sprite, short current, short maximum)
    {
        int ratio = current * 0xff / maximum;

        *(uint*)(sprite + 4) = ratio < 0x80
            ? (uint)((ratio << 9) & 0xffffff00) | 0xff
            : (uint)((ratio + 0x7f80) * 0x20000) | 0xffff;
    }

    /// <summary>
    /// Ghidra: FUN_0052fab0 @ 0x0052fab0 — <b>une liste de decorations</b>. Chaque entree tient sur
    /// un dword : l'indice du descripteur dont la position sert d'origine, l'indice de tuile, puis
    /// deux decalages en octets. La liste s'arrete au premier dword nul.
    ///
    /// <para>Le bloc de sprite est ici <b>local</b>, pas <c>DAT_005de5f8</c> — la fonction s'en
    /// construit un sur sa pile. Sa disposition est la meme : atlas, couleur, position a
    /// <c>+8</c>/<c>+0x0a</c>, indice de tuile a <c>+0x10</c>.</para>
    /// </summary>
    private static uint* DrawDecorList_0052FAB0(uint* otBase, uint* cursor, uint[] list)
    {
        int* sprite = stackalloc int[5];
        for (int i = 0; i < 5; i++) sprite[i] = 0;
        byte* s = (byte*)sprite;

        UiWin.EmitViewportRect(otBase, &cursor);

        *(uint*)(s + 4) = 0x1000000;
        *(byte**)s = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);

        foreach (uint entry in list)
        {
            if (entry == 0) break;

            byte* anchor = MenuDescriptor((sbyte)(byte)entry);
            *(short*)(s + 0x10) = (sbyte)(byte)(entry >> 8);
            *(short*)(s + 0x0a) = (short)(*(short*)(anchor + 6) + (byte)(entry >> 24));
            *(short*)(s + 8) = (short)(*(short*)(anchor + 4) + (byte)(entry >> 16));

            cursor = Prims.DrawSpriteVariant(otBase, cursor, sprite, 0, 0);
        }

        *(short*)(s + 0x10) = 0;
        cursor = Prims.DrawFlatPrimitive(otBase, cursor, sprite);
        UiWin.EmitClipRect(otBase, &cursor);
        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 2</c> du switch d'onglets — <b>l'equipement</b>. Blaze.c 75888..76123.
    ///
    /// <para>
    /// Trois etats, portes par <c>DAT_005de718</c> seul : <c>-1</c> l'onglet n'est pas entre,
    /// <c>0</c> on choisit un emplacement, <c>1</c> on choisit l'objet a y mettre. Les trois
    /// partagent le meme dessin final, ce que Ghidra rend par un <c>goto LAB_0052c017</c>.
    /// </para>
    /// <para>
    /// <b>Le nombre d'emplacements depend de la classe</b> : quatre pour les classes 0 et 6, trois
    /// pour les autres. Et l'emplacement retenu n'est pas la position du curseur — au-dela de la
    /// deuxieme ligne, une classe a trois emplacements saute d'un cran
    /// (<c>DAT_005de710 = DAT_005de6fc + 1</c>). Le meme trou se retrouve dans la boucle de dessin,
    /// qui saute la troisieme rangee et <b>n'avance pas son indice de libelle</b> : les deux se
    /// repondent.
    /// </para>
    /// <para>
    /// <b>Poser un objet est refuse par deux tests, pas un.</b> Le mot de drapeaux de l'objet
    /// (<c>+0x12</c>) doit porter le bit que l'emplacement demande — <c>0x100</c>, <c>0x200</c>,
    /// <c>0x400</c> pour les trois premiers, <c>0x800</c> pour les deux mains, et seulement si
    /// l'autre main ne le porte pas deja — <b>et</b> le personnage doit porter ce meme bit dans son
    /// propre mot de <c>+0x112</c>. Un refus joue le bruitage d'erreur.
    /// </para>
    /// <para>
    /// Poser un objet deja porte a cet emplacement le <b>retire</b> (<c>0xff</c>), ce qui fait de la
    /// validation une bascule. Les cinq passes de statistiques repassent juste apres, puisque le
    /// total change.
    /// </para>
    /// </summary>
    private static uint* DrawEquipmentTab(
        uint* otBase, uint* cursor, byte* slot, byte* memberSlot, byte* character, ushort* pad)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* root = MenuDescriptor(0);
        byte* box = MenuDescriptor(2);
        byte* sprite = s_MenuSprite_005de5f8;
        byte characterIndex = memberSlot[8];

        if (s_Unknown_005de718 == -1)
        {
            BindMenuLabels(*(byte**)(labelTable + 0x2d8), (uint)(nint)slot, root, 0);
        }
        else if (s_Unknown_005de718 == 0)
        {
            // ---- le choix de l'emplacement ----------------------------------------------------
            int classKind = character[0x150] & 7;
            int lastSlot = classKind == 0 || classKind == 6 ? 4 : 3;

            int previousRow = s_Unknown_005de6fc;
            if ((pad[3] & 0x10) != 0) s_Unknown_005de6fc--;
            if ((pad[3] & 0x40) != 0) s_Unknown_005de6fc++;

            if (s_Unknown_005de6fc < 0) s_Unknown_005de6fc = lastSlot;
            if (s_Unknown_005de6fc > lastSlot) s_Unknown_005de6fc = 0;

            if (previousRow != s_Unknown_005de6fc) Aud.PlayUiSound3A();

            if ((pad[1] & 0x8000) == 0)
            {
                if ((pad[1] & 0x4000) != 0)
                {
                    Aud.PlayUiSound39();
                    s_Unknown_005de70c = -1;
                    s_Unknown_005de718 = -1;
                }
            }
            else if (character[0x2c0] == 0)
            {
                Aud.PlayUiSound3B();                     // le sac est vide : refus
            }
            else
            {
                Aud.PlayUiSound38();
                s_Unknown_005de718 = 1;
                s_Unknown_005de710 = s_Unknown_005de6fc;

                // Le trou des classes a trois emplacements.
                if (lastSlot == 3 && s_Unknown_005de6fc > 1)
                {
                    s_Unknown_005de710 = s_Unknown_005de6fc + 1;
                }

                s_Unknown_005de6f8 = 0;
            }

            // Relu : la validation vient peut-etre de le changer, et le curseur ne se dessine que
            // tant qu'on est encore sur le choix d'emplacement.
            if (s_Unknown_005de718 == 0)
            {
                UiWin.EmitViewportRect(otBase, &cursor);
                *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                *(ushort*)(sprite + 0x10) = 0x18;
                *(uint*)(sprite + 4) = 0x1000000;
                *(short*)(sprite + 8) = 0x58;
                *(short*)(sprite + 0x0a) = (short)(s_Unknown_005de6fc * 0x1a + 0x24);
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
                UiWin.EmitClipRect(otBase, &cursor);
            }

            BindMenuLabels(*(byte**)(labelTable + 0x2cc), (uint)(nint)slot, root, 0);
        }
        else if (s_Unknown_005de718 == 1)
        {
            // ---- le choix de l'objet a poser --------------------------------------------------
            int previousPick = s_Unknown_005de6f8;

            if ((pad[3] & 0x80) != 0) s_Unknown_005de6f8--;
            if ((pad[3] & 0x20) != 0) s_Unknown_005de6f8++;

            // Ghidra teste le masque 0x50 d'un coup, et la branche haute ne remonte que de cinq :
            // sous la premiere ligne on descend, sinon on remonte. Conserve tel quel.
            if ((pad[3] & 0x50) != 0)
            {
                if (s_Unknown_005de6f8 < 5)
                {
                    if (character[0x2c0] > 5)
                    {
                        s_Unknown_005de6f8 += 5;
                        if (character[0x2c0] <= s_Unknown_005de6f8)
                        {
                            s_Unknown_005de6f8 = character[0x2c0] - 1;
                        }
                    }
                }
                else
                {
                    s_Unknown_005de6f8 -= 5;
                }
            }

            if (s_Unknown_005de6f8 < 0) s_Unknown_005de6f8 = character[0x2c0] - 1;
            if (character[0x2c0] <= s_Unknown_005de6f8) s_Unknown_005de6f8 = 0;

            if (previousPick != s_Unknown_005de6f8) Aud.PlayUiSound3A();

            byte entry = character[0x2c1 + s_Unknown_005de6f8];

            if ((pad[1] & 0x8000) == 0)
            {
                if ((pad[1] & 0x4000) != 0)
                {
                    Aud.PlayUiSound39();
                    s_Unknown_005de718 = 0;
                }
            }
            else
            {
                // Le bit que l'emplacement exige. Les deux mains partagent 0x800 et se refusent
                // l'une l'autre : le test compare l'objet vise a ce que porte l'autre main.
                uint required = s_Unknown_005de710 switch
                {
                    0 => 0x100u,
                    1 => 0x200u,
                    2 => 0x400u,
                    3 => character[0x2b4] != entry ? 0x800u : 0u,
                    4 => character[0x2b3] != entry ? 0x800u : 0u,

                    // Ghidra n'ecrit pas de defaut : au-dela de 4 le registre garde sa valeur
                    // precedente. DAT_005de710 est borne a 4 par le choix d'emplacement.
                    _ => throw new NotImplementedException(
                        $"FUN_0052a850, onglet 2 : emplacement {s_Unknown_005de710} hors de 0..4. " +
                        "Le binaire n'a pas de cas par defaut ici et retomberait sur la valeur " +
                        "precedente du registre."),
                };

                ushort itemFlags = *(ushort*)(character + 0x312 + entry * 0x280);

                if ((required & itemFlags) == 0 || (*(ushort*)(character + 0x112) & itemFlags) == 0)
                {
                    Aud.PlayUiSound3B();
                }
                else
                {
                    // Bascule : le meme objet deja pose se retire.
                    character[0x2b0 + s_Unknown_005de710] =
                        character[0x2b0 + s_Unknown_005de710] == entry ? (byte)0xff : entry;

                    CommitStagedStatBlock(character);
                    ScaleStatBlockByDifficulty(character + 0x100);
                    ComputeDerivedStats(character);
                    ApplyEquipmentStatBonuses(character);
                    FinalizeEquipmentEffects(character + 0x100);

                    Aud.PlayUiSound38();
                    s_Unknown_005de718 = 0;
                }
            }
        }

        // ================= le dessin, commun aux trois etats — LAB_0052c017 ====================
        if (s_Unknown_005de718 < -1) return cursor;

        if (s_Unknown_005de718 < 1)
        {
            // ---- les cinq rangees d'equipement -------------------------------------------------
            *(short*)(sprite + 8) = 0x60;
            *(short*)(sprite + 0x0a) = 0x20;

            BindMenuLabels(Mem.s_Unknown_005606f8, 0, box, 0);
            cursor = Ui.RunTextEngine(cursor, box);

            int classKind = character[0x150] & 7;
            int labelRow = 0;

            for (int countdown = 5; countdown != 0; countdown--)
            {
                // La troisieme rangee n'existe que pour les classes 0 et 6. Le saut emporte
                // l'avance de l'indice de libelle avec lui — c'est ce qui decale les rangees
                // suivantes d'un cran, exactement comme le fait DAT_005de710.
                if (countdown == 3 && classKind != 0 && classKind != 6) continue;

                byte worn = character[0x2b0 + (5 - countdown)];

                // Ghidra: cinq etiquettes de quatre octets, adressees a l'envers depuis
                // 0x0056ac44. Chacune est un flux `04 2a <y> 00` : un code de placement, ses deux
                // operandes, la fin.
                BindMenuLabels(s_EquipRowLabels_0056ac30 + 0x14 - labelRow * 4, 0, box, 0);
                labelRow++;
                cursor = Ui.RunTextEngine(cursor, box);

                if (worn == 0xff)
                {
                    // Emplacement vide : le tiret, puis le pictogramme de l'emplacement lui-meme.
                    UiWin.EmitViewportRect(otBase, &cursor);
                    *(byte**)sprite = s_ItemSprites_0056aae0 + 0x98;    // Ghidra: &DAT_0056ab78
                    *(short*)(sprite + 8) += 4;
                    *(short*)(sprite + 0x0a) += 4;
                    *(ushort*)(sprite + 0x10) = 0;
                    cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                    cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);

                    *(short*)(sprite + 8) += 0x16;
                    *(short*)(sprite + 0x0a) += 4;

                    // Ghidra: `&DAT_0056abe8 + param_1 * -8` — la planche se remonte a l'envers
                    // depuis sa fin, un cran par rangee.
                    *(byte**)sprite = s_ItemSprites_0056aae0 + 0x108 - countdown * 8;
                    cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                    cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
                    UiWin.EmitClipRect(otBase, &cursor);
                }
                else
                {
                    byte* item = character + 0x300 + worn * 0x280;

                    *(ushort*)(sprite + 0x10) = 0;
                    byte* atlas = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 8);
                    *(byte**)sprite = atlas;
                    atlas[0] = (byte)(worn * 0x18);
                    atlas[1] = (byte)(characterIndex * 0x18);

                    UiWin.EmitViewportRect(otBase, &cursor);
                    cursor = Prims.DrawSpriteVariant(
                        otBase, cursor, (int*)sprite, 0, (uint)((characterIndex << 8) | worn));
                    cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
                    UiWin.EmitClipRect(otBase, &cursor);

                    *(short*)(sprite + 8) += 0x1a;
                    *(short*)(sprite + 0x0a) += 8;

                    BindMenuLabels(
                        (item[0x13] & 0x80) == 0
                            ? s_UnknownItemText_00566b80 + 4        // Ghidra: &DAT_00566b84
                            : ItemDisplayName(item),                // Ghidra: FUN_0051bd30
                        0, box, 0);
                    cursor = Ui.RunTextEngine(cursor, box);
                }

                BindMenuLabels(s_EquipRowLabels_0056ac30, 0, box, 0);   // Ghidra: &DAT_0056ac30
                cursor = Ui.RunTextEngine(cursor, box);

                *(short*)(sprite + 8) -= 0x1a;
                *(short*)(sprite + 0x0a) += 0x12;
            }
        }
        else if (s_Unknown_005de718 == 1)
        {
            // ---- la grille d'objets, empruntee a l'onglet 1 ------------------------------------
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(short*)(sprite + 8) =
                (short)((s_Unknown_005de6f8 % 5 * 8 - s_Unknown_005de6f8 % 5) * 4 + 0x58);
            *(short*)(sprite + 0x0a) = (short)(s_Unknown_005de6f8 / 5 * 0x1c + 0x24);

            UiWin.EmitViewportRect(otBase, &cursor);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            UiWin.EmitClipRect(otBase, &cursor);

            cursor = DrawDecorList_0052FAB0(otBase, cursor, s_MenuDecorItems_0056a948);
            cursor = DrawItemGrid_0052FBA0(otBase, cursor, memberSlot, character);
            cursor = DrawItemDetail_0052FDC0(
                otBase, cursor, memberSlot, slot, character, s_Unknown_005de6f8);
        }

        return cursor;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056ac30</c>..<c>0x0056ac48</c> — <b>six flux d'etiquettes de quatre
    /// octets</b> pour les rangees d'equipement, releves verbatim. Cinq sont
    /// <c>04 2a &lt;y&gt; 00</c> — un code de placement, ses deux operandes, la fin — et le sixieme
    /// (<c>0d 00 00 00</c>) referme la rangee.
    ///
    /// <para>La boucle de dessin les prend <b>a l'envers</b>, depuis <c>0x0056ac44</c> : la rangee 0
    /// est la plus haute (<c>y = 0x1e</c>), la rangee 4 la plus basse (<c>y = 0x54</c>).</para>
    /// <para>Les bornes : au-dessous, deux dwords sans rapport (<c>0x6564</c>, <c>0x6b82</c>) ;
    /// au-dessus, une paire de pointeurs de code (<c>0x005315b0</c>, <c>0x00531b90</c>).</para>
    /// </summary>
    private static readonly byte* s_EquipRowLabels_0056ac30 = AllocBytes([
        0x0d, 0x00, 0x00, 0x00,
        0x04, 0x2a, 0x54, 0x00,
        0x04, 0x2a, 0x46, 0x00,
        0x04, 0x2a, 0x38, 0x00,
        0x04, 0x2a, 0x2a, 0x00,
        0x04, 0x2a, 0x1e, 0x00,
    ]);
    /// <summary>
    /// Ghidra: le <c>case 1</c> du switch d'onglets — <b>l'inventaire</b>. Blaze.c 75675..75887.
    ///
    /// <para>
    /// L'onglet a <b>trois etats</b>, portes par deux verrous. <c>DAT_005de718</c> negatif veut dire
    /// « la grille n'est pas encore entree » — un seul libelle s'affiche et rien ne repond.
    /// Au-dessus, <c>DAT_005de70c</c> mene une petite machine a quatre etats : <c>-1</c> la
    /// selection libre, puis 0 a 3 pour la confirmation d'utilisation d'un objet, chacun avec sa
    /// question et son annulation par Croix.
    /// </para>
    /// <para>
    /// <b>Le curseur vaut 99 quand rien n'est vise.</b> Ce n'est pas une sentinelle inventee : les
    /// quatre directions le remettent a 99 des qu'elles sortent de la liste, et 99 est teste partout
    /// comme « aucun objet ». La grille fait cinq colonnes, d'ou les pas de 1 et de 5.
    /// </para>
    /// <para>
    /// <b>Les six chemins de <c>Blaze.c</c> convergent tous sur le meme appel</b> — Ghidra les rend
    /// par un <c>goto LAB_0052ba95</c> et trois variables recyclees, mais les trois derniers
    /// arguments sont identiques partout (la fiche, le descripteur racine, zero). Seul le flux
    /// d'etiquettes change, ce qui se reconstruit en une variable et un appel.
    /// </para>
    /// </summary>
    private static uint* DrawInventoryTab(
        uint* otBase, uint* cursor, byte* slot, byte* memberSlot, byte* character, ushort* pad)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* root = MenuDescriptor(0);
        byte* labels = null;

        // Un sac vide rend la main tout de suite : les deux verrous repartent a -1.
        if (character[0x2c0] == 0)
        {
            s_Unknown_005de718 = -1;
            s_Unknown_005de70c = -1;
        }

        if (s_Unknown_005de718 < 0)
        {
            labels = *(byte**)(labelTable + 0x2d4);
        }
        else
        {
            // ---- le deplacement du curseur dans la grille -------------------------------------
            if (s_Unknown_005de70c < 1)
            {
                int previousPick = s_Unknown_005de6f8;

                if (s_Unknown_005de6f8 == 99)
                {
                    // Depuis « rien de vise », les quatre directions entrent dans la liste — trois
                    // par la fin, une par le debut. Elles ne s'excluent pas : le binaire les teste
                    // l'une apres l'autre, et une diagonale prend la derniere.
                    if ((pad[3] & 0x80) != 0) s_Unknown_005de6f8 = character[0x2c0] - 1;
                    if ((pad[3] & 0x20) != 0) s_Unknown_005de6f8 = 0;
                    if ((pad[3] & 0x10) != 0) s_Unknown_005de6f8 = character[0x2c0] - 1;
                    if ((pad[3] & 0x40) != 0)
                    {
                        s_Unknown_005de6f8 = character[0x2c0] < 5 ? character[0x2c0] - 1 : 4;
                    }
                }
                else
                {
                    if ((pad[3] & 0x80) != 0) s_Unknown_005de6f8--;
                    if ((pad[3] & 0x20) != 0) s_Unknown_005de6f8++;
                    if ((pad[3] & 0x10) != 0) s_Unknown_005de6f8 -= 5;
                    if ((pad[3] & 0x40) != 0) s_Unknown_005de6f8 += 5;

                    if (s_Unknown_005de6f8 < 0) s_Unknown_005de6f8 = 99;
                    if (character[0x2c0] <= s_Unknown_005de6f8) s_Unknown_005de6f8 = 99;
                }

                if (previousPick != s_Unknown_005de6f8) Aud.PlayUiSound3A();
            }

            byte* item = character + 0x300 + character[0x2c1 + s_Unknown_005de6f8] * 0x280;

            if (s_Unknown_005de70c < 0)
            {
                // ---- la selection libre --------------------------------------------------------
                ushort buttons = pad[1];

                if ((buttons & 0x8000) == 0)
                {
                    if ((buttons & 0x4000) != 0)
                    {
                        s_Unknown_005de718 = -1;
                        Aud.PlayUiSound39();
                    }
                }
                else
                {
                    pad[1] = (ushort)(buttons & 0x7fff);

                    if (s_Unknown_005de6f8 == 99)
                    {
                        s_Unknown_005de70c = 0;
                        Aud.PlayUiSound38();
                    }
                }

                // Ghidra ecrit ce point de chute deux fois (LAB_0052b82d et la suite) ; les deux
                // aboutissent au meme test, et la branche « rien de vise » y arrive avec 99
                // inchange.
                labels = s_Unknown_005de6f8 == 99
                    ? *(byte**)(labelTable + 0x314)
                    : ((item[0x13] & 0x80) == 0
                        ? *(byte**)(labelTable + 0x318)
                        : BuildItemDisplayName_0051BD50(item));
            }
            else
            {
                // ---- la machine de confirmation -------------------------------------------------
                switch (s_Unknown_005de70c)
                {
                    case 0:
                    {
                        ushort buttons = pad[1];

                        if ((buttons & 0x4000) != 0)
                        {
                            s_Unknown_005de70c = -1;
                            Aud.PlayUiSound39();
                            break;
                        }

                        if ((buttons & 0x8000) == 0)
                        {
                            labels = *(byte**)(labelTable + 700);
                            break;
                        }

                        pad[1] = (ushort)(buttons & 0x7fff);
                        Aud.PlayUiSound38();

                        if (s_Unknown_005de6f8 == 99)
                        {
                            s_Unknown_005de70c = -1;
                            break;
                        }

                        // L'objet vise est-il equipe ? Les cinq emplacements de +0x2b0 sont
                        // parcourus a l'envers ; sortir avec le compteur a zero veut dire
                        // « non trouve », et c'est ce zero qui aiguille les deux questions.
                        int remaining = 5;
                        byte* worn = character + 0x2b0;
                        while (remaining != 0)
                        {
                            if (*worn == character[0x2c1 + s_Unknown_005de6f8]) break;
                            remaining--;
                            worn++;
                        }

                        if (remaining == 0)
                        {
                            BindMenuLabels(
                                *(byte**)(labelTable + 0x2c4), (uint)(nint)slot, root, 0);
                            s_Unknown_005de70c = 2;
                        }
                        else
                        {
                            BindMenuLabels(
                                *(byte**)(labelTable + 0x2c0), (uint)(nint)slot, root, 0);
                            s_Unknown_005de70c++;
                        }

                        break;
                    }

                    case 1:
                        if ((((byte*)pad)[3] & 0x40) != 0)
                        {
                            root[0x33] = 0;                      // Ghidra: DAT_00568563
                            root[0x8c7] &= 0xfd;                 // Ghidra: DAT_00568df7
                            s_Unknown_005de70c = 0;
                            Aud.PlayUiSound39();
                        }

                        if ((root[0x8c7] & 2) == 0) s_Unknown_005de70c = 0;
                        break;

                    case 2:
                        if ((((byte*)pad)[3] & 0x40) == 0)
                        {
                            if ((root[0x8c7] & 2) == 0)
                            {
                                BindMenuLabels(
                                    *(byte**)(labelTable + 0x2c8), (uint)(nint)slot, root, 0);
                                s_Unknown_005de70c++;
                            }
                        }
                        else
                        {
                            root[0x8c7] &= 0xfd;
                            root[0x33] = 0;
                            s_Unknown_005de70c = 0;
                            Aud.PlayUiSound39();
                        }

                        break;

                    case 3:
                        if ((((byte*)pad)[3] & 0x40) == 0)
                        {
                            if ((root[0x8c7] & 4) == 0)
                            {
                                // C'est ici que l'objet disparait : son premier octet part a zero.
                                if (Mem.s_Unknown_005e909f == 0) item[0] = 0;
                                s_Unknown_005de70c = 0;
                            }
                        }
                        else
                        {
                            root[0x8c7] &= 0xfb;
                            root[0x3b] = 0;                      // Ghidra: DAT_0056856b
                            s_Unknown_005de70c = 0;
                            Aud.PlayUiSound39();
                        }

                        break;
                }
            }
        }

        if (labels != null) BindMenuLabels(labels, (uint)(nint)slot, root, 0);

        cursor = DrawDecorList_0052FAB0(otBase, cursor, s_MenuDecorItems_0056a948);

        byte* sprite = s_MenuSprite_005de5f8;
        UiWin.EmitViewportRect(otBase, &cursor);

        // ---- les deux fleches, puis le curseur de case ------------------------------------------
        // La premiere n'apparait que quand rien n'est vise et que la grille est entree ; elle laisse
        // derriere elle la teinte clignotante que la seconde reprend.
        if (s_Unknown_005de6f8 == 99 && s_Unknown_005de718 >= 0)
        {
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(short*)(sprite + 8) = 0xcd;
            *(short*)(sprite + 0x0a) = 0x58;
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            *(uint*)(sprite + 4) = s_Unknown_005e9814;
        }
        else
        {
            *(uint*)(sprite + 4) = 0x1000000;
        }

        *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        *(ushort*)(sprite + 0x10) = 0x1a;
        *(short*)(sprite + 8) = 0xd8;
        *(short*)(sprite + 0x0a) = 0x58;
        cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);

        if (s_Unknown_005de6f8 != 99 && s_Unknown_005de718 >= 0)
        {
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(short*)(sprite + 8) = (short)((s_Unknown_005de6f8 % 5 * 8 - s_Unknown_005de6f8 % 5) * 4 + 0x58);
            *(short*)(sprite + 0x0a) = (short)(s_Unknown_005de6f8 / 5 * 0x1c + 0x24);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        }

        UiWin.EmitClipRect(otBase, &cursor);

        cursor = DrawItemGrid_0052FBA0(otBase, cursor, memberSlot, character);

        if (s_Unknown_005de6f8 != 99 && character[0x2c0] != 0)
        {
            cursor = DrawItemDetail_0052FDC0(
                otBase, cursor, memberSlot, slot, character, s_Unknown_005de6f8);
        }

        return cursor;
    }

    /// <summary>
    /// Ghidra: FUN_0051bd50 @ 0x0051bd50 — <b>le nom d'objet, coupe a la barre oblique</b>. Il
    /// recopie le nom de l'objet (a <c>+0x40</c>) dans le tampon <c>DAT_005de0c8</c> jusqu'au
    /// premier <c>'/'</c> ou au terminateur, puis, s'il s'est arrete sur une barre, recopie la
    /// suite.
    ///
    /// <para>
    /// <b>Et la suite repart de l'indice 1, pas de 0 ni de la position de la barre.</b> Le binaire
    /// est formel — <c>mov edx,1</c> en 0x0051bd7c, sans condition. Le format attendu est donc
    /// <c>&lt;un caractere&gt;/&lt;le reste&gt;</c> : le premier caractere survit, la barre
    /// disparait, et le reste se colle derriere. Un nom dont la barre serait ailleurs qu'en
    /// position 1 serait mutile — ce qui n'arrive pas avec les noms du jeu, mais explique la forme
    /// bizarre de la boucle.
    /// </para>
    /// <para>
    /// Ghidra ne lui recupere <b>qu'un</b> parametre alors que le site d'appel en pousse quatre.
    /// Ce n'est pas une signature ratee : la fonction est <c>__cdecl</c>, l'appelant nettoie, et les
    /// trois autres sont simplement ignores — le site les a pousses parce qu'il partage sa forme
    /// avec les cinq autres chemins de l'onglet, qui appellent tous <c>FUN_00544350</c>.
    /// </para>
    /// </summary>
    private static byte* BuildItemDisplayName_0051BD50(byte* item)
    {
        byte* buffer = s_ItemNameScratch_005de0c8;
        int read = 0;

        while (read < 0x40)
        {
            byte c = item[0x40 + read];
            if (c == '/' || c == 0) break;
            buffer[read] = c;
            read++;
        }

        int write = read;

        if (item[0x40 + read] == '/')
        {
            read++;
            write = 1;

            while (read < 0x40)
            {
                byte c = item[0x40 + read];
                if (c == 0) break;
                buffer[write] = c;
                read++;
                write++;
            }
        }

        buffer[write] = 0;
        return buffer;
    }

    /// <summary>
    /// Ghidra: FUN_0052fba0 @ 0x0052fba0 — <b>la grille d'objets</b>, cinq colonnes de cases de
    /// 0x1c. Pour chaque entree listee : sa quantite quand l'objet en porte une, un marqueur par
    /// emplacement d'equipement qui la designe, puis son pictogramme.
    ///
    /// <para>
    /// Ghidra ecrit l'indice de boucle en arithmetique de pointeurs — <c>pbVar8 = pbVar9 + iVar7</c>
    /// avec <c>iVar7 = -0x2c1 - param_4</c> — ce qui n'est que <c>i</c>. Reconstruit en boucle
    /// ordinaire.
    /// </para>
    /// <para>
    /// <b>Le pictogramme se decoupe en ecrivant dans l'atlas.</b> Les deux premiers octets du
    /// descripteur partage recoivent <c>entree * 0x18</c> et <c>personnage * 0x18</c> avant chaque
    /// emission : c'est la case de la planche. Le meme couple repart en <c>extra</c>, empaquete sur
    /// seize bits.
    /// </para>
    /// </summary>
    private static uint* DrawItemGrid_0052FBA0(
        uint* otBase, uint* cursor, byte* memberSlot, byte* character)
    {
        if (character[0x2c0] == 0) return cursor;

        int* sprite = stackalloc int[5];
        for (int i = 0; i < 5; i++) sprite[i] = 0;
        byte* s = (byte*)sprite;

        for (int i = 0; i < character[0x2c0]; i++)
        {
            short cellX = (short)(i % 5 * 0x1c);
            short cellY = (short)(i / 5 * 0x1c);

            *(uint*)(s + 4) = i == s_Unknown_005de6f8 && s_Unknown_005de718 >= 0
                ? s_Unknown_005e9814
                : 0x1000000;

            byte entry = character[0x2c1 + i];
            byte* item = character + 0x300 + entry * 0x280;

            if ((item[0x13] & 0x30) != 0)
            {
                *(short*)(s + 8) = (short)(cellX + 0x68);
                *(short*)(s + 0x0a) = (short)(cellY + 0x30);
                *(byte**)s = SpriteAtlas(0x00566c30);
                cursor = Prims.DrawDecimalNumber(
                    otBase, cursor, sprite, *(ushort*)(item + 0x10), 0x82, 0);
            }

            *(short*)(s + 0x10) = 0;
            UiWin.EmitViewportRect(otBase, &cursor);

            for (int k = 0; k < 5; k++)
            {
                if (character[0x2b0 + k] != entry) continue;

                *(short*)(s + 8) = (short)(cellX + 0x6c);
                *(short*)(s + 0x0a) = (short)(cellY + 0x2c);
                *(byte**)s = s_ItemSprites_0056aae0 + 0x90;      // Ghidra: &DAT_0056ab70
                cursor = Prims.DrawSpriteVariant(otBase, cursor, sprite, 0, 0);
                cursor = Prims.DrawFlatPrimitive(otBase, cursor, sprite);
            }

            byte* atlas = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 8);
            *(byte**)s = atlas;
            atlas[0] = (byte)(entry * 0x18);
            atlas[1] = (byte)(memberSlot[8] * 0x18);
            *(short*)(s + 8) = (short)(cellX + 0x60);
            *(short*)(s + 0x0a) = (short)(cellY + 0x20);
            cursor = Prims.DrawSpriteVariant(
                otBase, cursor, sprite, 0, (uint)((memberSlot[8] << 8) | entry));
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, sprite);
            UiWin.EmitClipRect(otBase, &cursor);
        }

        return cursor;
    }

    /// <summary>
    /// Ghidra: FUN_0052fdc0 @ 0x0052fdc0 — <b>la fiche de l'objet vise</b> : huit rangs de valeurs,
    /// deux rangs de portee, le nom, puis huit pictogrammes d'element.
    ///
    /// <para>
    /// Un objet non identifie (<c>+0x13</c> sans le bit <c>0x80</c>) rend <c>« ??? »</c> a chaque
    /// rang et <c>« ???? »</c> comme nom — les deux chaines sont dans <c>.data</c>, relevees sous
    /// <see cref="s_UnknownItemText_00566b80"/>.
    /// </para>
    /// <para>
    /// <b>Le nom part dans un flux d'etiquettes monte sur la pile</b> : <c>0c 04 &lt;x&gt;
    /// &lt;y&gt;</c> puis le nom recopie octet par octet, le tout lie au descripteur 2 puis passe au
    /// moteur de texte avant le retour. C'est ce que fait l'original, avec la meme duree de vie —
    /// le descripteur garde ensuite un pointeur vers un cadre mort, et personne ne le relit.
    /// </para>
    /// <para>Les deux decalages du flux se prennent <b>par rapport au descripteur 2</b> :
    /// <c>0x60 - x</c> et <c>0x5c - y - 4</c>. Ils suivent donc la boite si elle bouge.</para>
    /// </summary>
    private static uint* DrawItemDetail_0052FDC0(
        uint* otBase, uint* cursor, byte* memberSlot, byte* slot, byte* character, int index)
    {
        int* sprite = stackalloc int[5];
        for (int i = 0; i < 5; i++) sprite[i] = 0;
        byte* s = (byte*)sprite;

        *(uint*)(s + 4) = 0x1000000;

        byte* item = character + 0x300 + character[0x2c1 + index] * 0x280;
        bool identified = (item[0x13] & 0x80) != 0;

        // ---- les huit rangs de valeurs, quatre par ligne ---------------------------------------
        for (int i = 0; i < 8; i++)
        {
            *(short*)(s + 8) = (short)((i & 3) * 0x24 + 0x60 + 8);
            *(short*)(s + 0x0a) = (short)((i >> 2) * 10 + 0x80);

            if (!identified)
            {
                *(byte**)s = SpriteAtlas(0x00566c28);
                cursor = Prims.DrawTextRun(
                    otBase, cursor, sprite, s_UnknownItemText_00566b80, 0, 0, -1);
            }
            else
            {
                *(byte**)s = SpriteAtlas(0x00566c30);
                sbyte field = (sbyte)s_ItemStatOrder_0056a8d8[i];
                cursor = Prims.DrawDecimalNumber(
                    otBase, cursor, sprite,
                    (uint)(sbyte)item[0x18 + field], 3, (uint)(field + 2));
            }
        }

        // ---- les deux rangs de portee ------------------------------------------------------------
        *(short*)(s + 8) = 0x60;
        *(short*)(s + 0x0a) = 0x68;

        for (int i = 0; i < 2; i++)
        {
            *(short*)(s + 8) += 0x14;

            if (!identified)
            {
                *(byte**)s = SpriteAtlas(0x00566c28);
                cursor = Prims.DrawTextRun(
                    otBase, cursor, sprite, s_UnknownItemText_00566b80, 0, 0, -1);
            }
            else
            {
                *(byte**)s = SpriteAtlas(0x00566c30);
                int value = *(short*)(item + 0x30 + i * 2);
                if (value < 0) value = 0;
                cursor = Prims.DrawDecimalNumber(otBase, cursor, sprite, (uint)value, 3, 0);
            }

            *(short*)(s + 8) += 0x28;
        }

        // ---- le nom, en flux d'etiquettes monte sur la pile ---------------------------------------
        *(short*)(s + 0x0a) -= 0xc;

        byte* box = MenuDescriptor(2);
        byte* stream = stackalloc byte[0x48];
        stream[0] = 0x0c;
        stream[1] = 0x04;
        stream[2] = (byte)(0x60 - *(short*)(box + 4));
        stream[3] = (byte)((sbyte)*(short*)(s + 0x0a) - *(short*)(box + 6) - 4);

        *(short*)(s + 8) = 0x60;

        if (!identified)
        {
            // Ghidra: local_38 = DAT_00566b84 ; local_34 = DAT_00566b88 — quatre octets puis un.
            for (int i = 0; i < 5; i++) stream[4 + i] = s_UnknownItemText_00566b80[4 + i];
        }
        else
        {
            byte* name = ItemDisplayName(item);          // Ghidra: FUN_0051bd30
            int length = 0;
            while (name[length] != 0) length++;
            for (int i = 0; i <= length; i++) stream[4 + i] = name[i];
        }

        BindMenuLabels(stream, (uint)(nint)slot, box, 0);
        cursor = Ui.RunTextEngine(cursor, box);

        // ---- les huit pictogrammes de classe --------------------------------------------------------
        // Le bit du mot +0x12 decide de la teinte. Ghidra l'ecrit sans branchement :
        // `(-(bit != 0) & 0xfffffff9) + 9`.
        //
        // **La valeur du cas « bit leve » est 2, pas 0**, et c'est une faute de lecture qui a tenu
        // jusqu'au 2026-08-25 : `0xfffffff9` vaut **-7**, et -7 + 9 fait 2. Avec 0, la pastille de
        // la classe autorisee changeait de page d'atlas — elle sortait en rectangle blanc avec un
        // libelle bariole, dans cette fenetre comme dans celle du coffre, qui porte le meme calcul.
        // Signale en jeu par Vincent sur l'epee du Guerrier.
        //
        // Le reflexe qui manquait : un `& 0xfffffffN` suivi d'un `+ N'` n'est pas un masque, c'est
        // une soustraction deguisee. Calculer les deux cas avant d'ecrire le ternaire.
        *(short*)(s + 0x10) = 0;
        *(short*)(s + 8) = 0x60;
        *(short*)(s + 0x0a) = 0x98;
        *(byte**)s = s_ItemSprites_0056aae0 + 0xa0;      // Ghidra: &DAT_0056ab80

        UiWin.EmitViewportRect(otBase, &cursor);

        for (int i = 0; i < 8; i++)
        {
            uint tint = (*(ushort*)(item + 0x12) & (1u << i)) != 0 ? 2u : 9u;
            cursor = Prims.DrawSpriteVariant(otBase, cursor, sprite, 0, tint);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, sprite);
            *(short*)(s + 8) += 0x10;
            *(short*)(s + 0x10) += 1;
        }

        UiWin.EmitClipRect(otBase, &cursor);
        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 3</c> du switch d'onglets — <b>la liste de sorts</b>. Blaze.c
    /// 76124..76340, 217 lignes.
    ///
    /// <para>
    /// Six pages (quatre pour la classe 5), chacune une tranche du repertoire de l'ecole du
    /// personnage : haut/bas changent de page, gauche/droite parcourent la tranche, le carre retient
    /// le sort vise dans <c>+0x2b6</c>. La tranche se lit dans
    /// <see cref="s_SpellPages_00564640"/> — un couple (premier, compte) par page — et l'etat
    /// « appris » dans le bloc de bits de <c>+0x160 + ecole * 8</c>.
    /// </para>
    /// <para>
    /// <b>La liste courte de <c>DAT_005de6a0</c> encode l'etat dans le signe.</b> Un sort appris y
    /// entre par son numero, un sort inconnu par son <b>oppose</b>, et tout le reste du corps teste
    /// <c>&lt; 0x80</c> pour les distinguer. Le numero 0 est donc ambigu — son oppose vaut aussi
    /// zero — et se lira toujours comme appris ; c'est dans le binaire, note ici sans etre corrige.
    /// </para>
    /// <para>
    /// <b>Le verrou de mise au point apprend tout.</b> Quand <c>DAT_005633a8</c> vaut zero — apres
    /// « Gurkha », il vaut 1 au chargement — la boucle <b>leve</b> le bit de chaque sort qu'elle
    /// parcourt avant de le tester. Ce n'est pas un affichage de triche, c'est une ecriture dans le
    /// personnage.
    /// </para>
    /// <para>
    /// Les huit octets de <c>DAT_005de6a0</c> se referment exactement sur
    /// <see cref="s_KeyItemIndex_005de6a8"/> : la plus grande tranche des trois ecoles compte six
    /// entrees, la place est bornee par le voisin.
    /// </para>
    /// </summary>
    private static uint* DrawSpellTab(
        uint* otBase, uint* cursor, byte* slot, byte* character, ushort* pad, int listBefore)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* root = MenuDescriptor(0);
        byte* sprite = s_MenuSprite_005de5f8;

        // ---- la page --------------------------------------------------------------------
        if (s_Unknown_005de718 < 0)
        {
            BindMenuLabels(*(byte**)(labelTable + 0x2dc), (uint)(nint)slot, root, 0);
            s_Unknown_005de710 = -1;
        }
        else
        {
            // Quatre pages pour la classe 5, six pour les autres.
            int lastPage = ((character[0x150] & 7) != 5 ? 2 : 0) + 3;

            if ((pad[3] & 0x10) != 0 && s_Unknown_005de6fc < lastPage)
            {
                s_Unknown_005de6fc++;
                s_Unknown_005de6f8 = 0;
            }

            if ((pad[3] & 0x40) != 0 && s_Unknown_005de6fc > 0)
            {
                s_Unknown_005de6fc--;
                s_Unknown_005de6f8 = 0;
            }

            if (listBefore != s_Unknown_005de6fc)
            {
                pad[3] &= 0xff5f;
                Aud.PlayUiSound3A();
            }

            if ((((byte*)pad)[3] & 0x40) != 0)
            {
                s_Unknown_005de718 = -1;
                Aud.PlayUiSound39();
            }
        }

        // ---- la tranche de sorts, montee a chaque image ------------------------------------
        byte school = character[0x2b5];
        uint* known = (uint*)(character + 0x160 + school * 8);
        byte* pages = s_SpellPages_00564640[school];

        int id = pages[s_Unknown_005de6fc * 2];
        int listed = 0;

        for (int remaining = pages[s_Unknown_005de6fc * 2 + 1]; remaining != 0; remaining--, id++)
        {
            if (Mem.s_DebugModeLatch_005633a8 == 0) known[id >> 5] |= 1u << (id & 0x1f);

            s_SpellList_005de6a0[listed] = (known[id >> 5] & (1u << (id & 0x1f))) == 0
                ? (byte)-(byte)id
                : (byte)id;

            listed++;
        }

        // ---- le curseur dans la tranche -----------------------------------------------------
        if (s_Unknown_005de718 >= 0)
        {
            int cursorBefore = s_Unknown_005de6f8;

            if ((pad[3] & 0x80) != 0) s_Unknown_005de6f8--;
            if ((pad[3] & 0x20) != 0) s_Unknown_005de6f8++;
            if (s_Unknown_005de6f8 < 0) s_Unknown_005de6f8 = listed - 1;
            if (s_Unknown_005de6f8 >= listed) s_Unknown_005de6f8 = 0;

            if (cursorBefore != s_Unknown_005de6f8) Aud.PlayUiSound3A();

            if ((((byte*)pad)[3] & 0x80) != 0)
            {
                if (s_SpellList_005de6a0[s_Unknown_005de6f8] < 0x80)
                {
                    character[0x2b6] = s_SpellList_005de6a0[s_Unknown_005de6f8];
                    Aud.PlayUiSound38();
                }
                else
                {
                    Aud.PlayUiSound3B();
                }
            }

            byte* label;
            if (s_SpellList_005de6a0[s_Unknown_005de6f8] < 0x80)
            {
                BindMenuLabels(Mem.s_Unknown_005606f8, (uint)(nint)slot, root, 0);
                cursor = Ui.RunTextEngine(cursor, root);

                label = *(byte**)(labelTable
                    + (s_SpellLabelBase_00564608[school]
                       + s_SpellList_005de6a0[s_Unknown_005de6f8]) * 8);
            }
            else
            {
                label = *(byte**)(labelTable + 0x31c);
            }

            BindMenuLabels(label, (uint)(nint)slot, root, 0);
        }

        // ---- le cadre, le numero de page, et le total ---------------------------------------
        cursor = DrawDecorList_0052FAB0(otBase, cursor, s_MenuDecorSpells_0056a978);

        *(byte**)sprite = SpriteAtlas(0x00566c30);
        *(short*)(sprite + 8) = 0xb0;
        *(short*)(sprite + 0x0a) = 0x24;
        *(uint*)(sprite + 4) = 0x1000000;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, (uint)(s_Unknown_005de6fc + 1), 1, 0);

        *(short*)(sprite + 8) += 0x10;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite,
            (uint)(((character[0x150] & 7) != 5 ? 2 : 0) + 4), 1, 0);

        *(short*)(sprite + 0x0a) = 0x32;
        UiWin.EmitViewportRect(otBase, &cursor);

        if (s_Unknown_005de718 >= 0)
        {
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(short*)(sprite + 8) = (short)(s_Unknown_005de6f8 * 0x14 + 0x55);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        }

        // ---- la rangee de pictogrammes -------------------------------------------------------
        *(short*)(sprite + 8) = 0x60;

        for (int i = 0; i < listed; i++)
        {
            *(uint*)(sprite + 4) =
                i == s_Unknown_005de6f8 && s_Unknown_005de718 >= 0
                    ? s_Unknown_005e9814
                    : 0x1000000;

            if (s_SpellList_005de6a0[i] < 0x80)
            {
                // Le sort retenu porte le marqueur, decale de huit dans les deux sens.
                if (character[0x2b6] == s_SpellList_005de6a0[i])
                {
                    *(short*)(sprite + 8) += 8;
                    *(short*)(sprite + 0x0a) += 8;
                    *(byte**)sprite = s_ItemSprites_0056aae0 + 0x90;
                    *(ushort*)(sprite + 0x10) = 0;
                    cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                    cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
                    *(short*)(sprite + 8) -= 8;
                    *(short*)(sprite + 0x0a) -= 8;
                }

                *(byte**)sprite = *(byte**)(
                    *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x10 + school * 4);
                *(ushort*)(sprite + 0x10) = s_SpellList_005de6a0[i];
            }
            else
            {
                *(byte**)sprite = s_ItemSprites_0056aae0 + 0x98;
                *(ushort*)(sprite + 0x10) = 0;
            }

            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            *(short*)(sprite + 8) += 0x14;
        }

        UiWin.EmitClipRect(otBase, &cursor);

        // ---- le nom du sort vise, son cout, ses elements --------------------------------------
        *(byte**)sprite = (byte*)s_GlyphAtlas_00566c28;
        *(short*)(sprite + 8) = 0x64;
        *(short*)(sprite + 0x0a) = 0x48;
        *(uint*)(sprite + 4) = 0x1000000;

        byte* record = null;
        byte* name;

        if (s_SpellList_005de6a0[s_Unknown_005de6f8] < 0x80)
        {
            record = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x9c) + school * 4)
                     + s_SpellList_005de6a0[s_Unknown_005de6f8] * 0x30;

            // Le nom est **reecrit sur place** : les zeros des quatorze premiers octets deviennent
            // des espaces. Une ecriture dans la ressource, pas une copie — transcrite telle quelle.
            for (int i = 0; i < 0x0e; i++) if (record[i] == 0) record[i] = 0x20;

            name = record;
        }
        else
        {
            name = s_UnknownItemText_00566b80 + 4;      // Ghidra: &DAT_00566b84, « ???? »
        }

        cursor = Prims.DrawTextRun(otBase, cursor, (int*)sprite, name, 0, 0, 0x10);

        *(short*)(sprite + 8) = 0xac;
        *(short*)(sprite + 0x0a) = 0x58;
        *(uint*)(sprite + 4) = 0x1000000;

        if (record != null)
        {
            *(byte**)sprite = SpriteAtlas(0x00566c30);
            cursor = Prims.DrawDecimalNumber(
                otBase, cursor, (int*)sprite, *(ushort*)(record + 0x14), 3, 0);
        }
        else
        {
            cursor = Prims.DrawTextRun(
                otBase, cursor, (int*)sprite, s_UnknownItemText_00566b80, 0, 0, 0x10);
        }

        *(short*)(sprite + 8) = 0x64;
        *(short*)(sprite + 0x0a) = 0x88;

        if (record == null)
        {
            *(byte**)sprite = (byte*)s_GlyphAtlas_00566c28;
            return Prims.DrawTextRun(
                otBase, cursor, (int*)sprite, s_UnknownItemText_00566b80 + 4, 0, 0, 0x10);
        }

        // Les pictogrammes d'element, quatre bits par entree, replies quand la ligne est pleine.
        *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        *(uint*)(sprite + 4) = 0x1000000;

        for (int entry = 0; entry < record[0x1f]; entry++)
        {
            uint bits = record[0x20 + entry];

            int set = 0;
            for (int b = 0; b < 4; b++, bits >>= 1) if ((bits & 0x10) != 0) set++;

            if (0xe4 - set * 8 <= *(short*)(sprite + 8))
            {
                *(short*)(sprite + 0x0a) += 0x0c;
                *(short*)(sprite + 8) = 100;
            }

            *(ushort*)(sprite + 0x10) = 0x30;

            for (int b = 0; b < 4; b++, bits >>= 1, (*(ushort*)(sprite + 0x10))++)
            {
                if ((bits & 1) == 0) continue;

                UiWin.EmitViewportRect(otBase, &cursor);
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
                UiWin.EmitClipRect(otBase, &cursor);
                *(short*)(sprite + 8) += 8;
            }

            *(short*)(sprite + 8) += 6;
        }

        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 4</c> du switch d'onglets — <b>les sept herbes</b>. Blaze.c
    /// 76341..76445, 105 lignes. Le seul onglet propre a la classe 4.
    ///
    /// <para>
    /// Sept entrees fixes, sans page ni tranche : haut/bas deplacent le curseur, le carre retient
    /// l'entree visee dans <c>+0x2b6</c> — le meme octet que l'onglet 3. A droite, sept libelles
    /// pris d'un bloc contigu de la table de ressources (<c>+0x398</c> a <c>+0x3b4</c>) et sept
    /// nombres.
    /// </para>
    /// <para>
    /// <b>Les memes huit octets, lus de deux facons.</b> Les nombres viennent de
    /// <c>+0x160 + ecole * 8</c>, un octet par entree — exactement le bloc que l'onglet 3 lit
    /// <b>en bits</b>. Rien ne les reconcilie : c'est la classe qui decide de la lecture, et la
    /// table d'onglets s'arrange pour qu'un personnage n'ait jamais les deux.
    /// </para>
    /// <para>
    /// A l'ecran : « Lavande », « Sauge », « Verveine », « Mercuriale », « Fenouil », « Hysopius »,
    /// « Belladone », avec leur compte et, pour celle qui est visee, sa description en bas.
    /// </para>
    /// <para>
    /// <b>La borne de la boucle de libelles est un decalage, pas un compte</b> : le binaire va de
    /// <c>0x398</c> a <c>0x3b4</c> par pas de quatre, ce qui fait sept entrees et le dit sans avoir
    /// a les compter.
    /// </para>
    /// </summary>
    private static uint* DrawHerbTab(
        uint* otBase, uint* cursor, byte* slot, byte* character, ushort* pad, int rowBefore)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* root = MenuDescriptor(0);
        byte* sprite = s_MenuSprite_005de5f8;
        byte school = character[0x2b5];

        if (s_Unknown_005de718 < 0)
        {
            BindMenuLabels(*(byte**)(labelTable + 0x2dc), (uint)(nint)slot, root, 0);
            s_Unknown_005de710 = -1;
        }
        else
        {
            if ((pad[3] & 0x10) != 0) s_Unknown_005de6f8--;
            if ((pad[3] & 0x40) != 0) s_Unknown_005de6f8++;
            if (s_Unknown_005de6f8 < 0) s_Unknown_005de6f8 = 6;
            if (s_Unknown_005de6f8 > 6) s_Unknown_005de6f8 = 0;

            if (rowBefore != s_Unknown_005de6f8) Aud.PlayUiSound3A();

            if ((pad[1] & 0x8000) == 0)
            {
                if ((pad[1] & 0x4000) != 0)
                {
                    s_Unknown_005de718 = -1;
                    Aud.PlayUiSound39();
                }
            }
            else
            {
                character[0x2b6] = (byte)s_Unknown_005de6f8;
                Aud.PlayUiSound38();
            }

            BindMenuLabels(Mem.s_Unknown_005606f8, (uint)(nint)slot, root, 0);
            cursor = Ui.RunTextEngine(cursor, root);

            BindMenuLabels(
                *(byte**)(labelTable + (s_SpellLabelBase_00564608[school] + s_Unknown_005de6f8) * 8),
                (uint)(nint)slot, root, 0);
        }

        UiWin.EmitViewportRect(otBase, &cursor);

        if (s_Unknown_005de718 >= 0)
        {
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(short*)(sprite + 8) = 0x65;
            *(short*)(sprite + 0x0a) = (short)((s_Unknown_005de6f8 + 2) * 0x12);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        }

        *(short*)(sprite + 8) = 0x5c;
        *(short*)(sprite + 0x0a) = 0x24;

        for (int i = 0; i < 7; i++)
        {
            *(uint*)(sprite + 4) =
                i == s_Unknown_005de6f8 && s_Unknown_005de718 >= 0
                    ? s_Unknown_005e9814
                    : 0x1000000;

            if (character[0x2b6] == i)
            {
                *(short*)(sprite + 8) += 8;
                *(short*)(sprite + 0x0a) += 8;
                *(byte**)sprite = s_ItemSprites_0056aae0 + 0x90;
                *(ushort*)(sprite + 0x10) = 0;
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
                *(short*)(sprite + 8) -= 8;
                *(short*)(sprite + 0x0a) -= 8;
            }

            *(byte**)sprite = *(byte**)(
                *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x10 + school * 4);
            *(ushort*)(sprite + 0x10) = (ushort)i;
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            *(short*)(sprite + 0x0a) += 0x12;
        }

        *(ushort*)(sprite + 0x10) = 0;
        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        // ---- les sept libelles ----------------------------------------------------------------
        *(byte**)sprite = (byte*)s_GlyphAtlas_00566c28;
        *(short*)(sprite + 8) = 0x70;
        *(short*)(sprite + 0x0a) = 0x28;
        *(uint*)(sprite + 4) = 0x1000000;

        for (int offset = 0x398; offset < 0x3b4; offset += 4)
        {
            cursor = Prims.DrawTextRunAlias(
                otBase, cursor, (int*)sprite, *(byte**)(labelTable + offset), 0, 0, -1);
            *(short*)(sprite + 0x0a) += 0x12;
            (*(ushort*)(sprite + 0x10))++;
        }

        // ---- les sept nombres ------------------------------------------------------------------
        *(byte**)sprite = SpriteAtlas(0x00566c30);
        *(short*)(sprite + 8) = 0xc8;
        *(short*)(sprite + 0x0a) = 0x28;
        *(uint*)(sprite + 4) = 0x1000000;

        for (int i = 0; i < 7; i++)
        {
            cursor = Prims.DrawDecimalNumber(
                otBase, cursor, (int*)sprite,
                character[0x160 + school * 8 + i], 0x82, 0);
            *(short*)(sprite + 0x0a) += 0x12;
            (*(ushort*)(sprite + 0x10))++;
        }

        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 5</c> du switch d'onglets — <b>la configuration de la manette</b>.
    /// Blaze.c 76446..76571, binaire 0x0052cf0a..0x0052d2b4.
    ///
    /// <para>
    /// Trois rangees, et le curseur n'est pas celui des grilles : ici <c>DAT_005de6f8</c> EST la
    /// rangee, bornee a 0..2, et chaque rangee agit sur un octet different du personnage —
    /// <c>+0x2b7</c> le type de manette (trois valeurs en cycle), <c>+0x2ba</c> et <c>+0x2b9</c>
    /// deux bascules. Les deux bascules partagent le meme couple d'etiquettes
    /// (<see cref="s_ToggleNames_0056aa30"/>), le type de manette lit
    /// <see cref="s_ControllerTypeNames_0056aa20"/>.
    /// </para>
    /// <para>
    /// <b>Le saut indexe a quatre entrees, la borne n'en laisse que trois.</b> La table de
    /// <c>0x0052fa4c</c> porte bien un <c>case 3</c> (0x0052cffc), mais <c>DAT_005de6f8</c> est
    /// ramene dans 0..2 quelques instructions plus haut : il est inatteignable. Il est transcrit
    /// quand meme — c'est le remappage des quatre boutons d'action, vingt-quatre permutations dans
    /// <see cref="s_ButtonOrder_0056aa38"/> et quatre masques dans
    /// <see cref="s_ButtonMasks_0056aa98"/>, ecrits dans le personnage a <c>+0x1fc0</c>,
    /// <c>+0x1fc2</c>, <c>+0x1fc4</c> et — le quatrieme saute deux mots — <c>+0x1fca</c>.
    /// </para>
    /// <para>
    /// <b>Deux racines de ressources differentes dans le meme corps</b>, et ce n'est pas une
    /// coquille de Ghidra : le curseur de rangee prend son atlas dans <c>DAT_005e97c0</c> (le
    /// tampon), les trois cadres de rangee dans <c>DAT_005e97c4</c> (le repertoire). Verifie au
    /// desassembleur — <c>mov ecx,[5E97C0h]</c> en 0x0052d0a1 contre <c>mov eax,[5E97C4h]</c> en
    /// 0x0052d151.
    /// </para>
    /// <para>
    /// La queue — les trois cadres puis les trois etiquettes — est <b>commune aux deux branches</b> :
    /// onglet entre ou non, elle s'execute. Seule la tete differe, et la branche « pas entre » ne
    /// fait que poser un bloc de libelles sur le descripteur racine.
    /// </para>
    /// </summary>
    private static uint* DrawConfigTab(
        uint* otBase, uint* cursor, byte* slot, byte* character, ushort* pad, int rowBefore)
    {
        byte* sprite = s_MenuSprite_005de5f8;

        if (s_Unknown_005de718 < 0)
        {
            // L'onglet n'est pas entre : rien ne repond, et la seule chose posee est le bloc de
            // libelles de +0x2e0, ancre sur le descripteur racine.
            byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
            BindMenuLabels(*(byte**)(labelTable + 0x2e0), (uint)(nint)slot, MenuDescriptor(0), 0);
        }
        else
        {
            // ---- le curseur de rangee ----------------------------------------------------
            if ((((byte*)pad)[6] & 0x10) != 0) s_Unknown_005de6f8--;
            if ((((byte*)pad)[6] & 0x40) != 0) s_Unknown_005de6f8++;
            if (s_Unknown_005de6f8 < 0) s_Unknown_005de6f8 = 2;
            if (s_Unknown_005de6f8 > 2) s_Unknown_005de6f8 = 0;
            if (rowBefore != s_Unknown_005de6f8) Aud.PlayUiSound3A();   // Ghidra: FUN_0053db60

            // La touche 0x40 qui descend d'une rangee est lue dans le mot 3 ; celle qui ressort de
            // l'onglet porte le meme masque dans le mot 1. Deux mots differents de la manette, ce
            // ne sont pas les memes touches.
            if ((((byte*)pad)[3] & 0x40) != 0)
            {
                s_Unknown_005de718 = -1;
                Aud.PlayUiSound39();                                    // Ghidra: FUN_0053dba0
            }

            switch (s_Unknown_005de6f8)
            {
                case 0:
                {
                    // Le type de manette, en cycle sur trois valeurs. « Type D » existe dans la
                    // table d'etiquettes mais la borne a 2 ne l'atteint jamais.
                    int previous = character[0x2b7];
                    int value = previous;
                    if ((((byte*)pad)[2] & 0x80) != 0) value = previous - 1;
                    if ((((byte*)pad)[6] & 0x20) != 0) value++;
                    if (value < 0) value = 2;
                    if (value > 2) value = 0;
                    character[0x2b7] = (byte)value;
                    if (previous != (value & 0xff)) Aud.PlayUiSound3A();
                    break;
                }

                case 1:
                    // Les deux touches 0x20 et 0x80 du mot 1 basculent, indifferemment.
                    if ((((byte*)pad)[2] & 0xa0) != 0)
                    {
                        character[0x2ba] ^= 1;
                        Aud.PlayUiSound3A();
                    }
                    break;

                case 2:
                    if ((((byte*)pad)[2] & 0xa0) != 0)
                    {
                        character[0x2b9] ^= 1;
                        Aud.PlayUiSound3A();
                    }
                    break;

                case 3:
                {
                    // INATTEIGNABLE — la borne 0..2 ci-dessus l'exclut. Le saut indexe de
                    // 0x0052fa4c le porte quand meme, donc il est transcrit : le remappage des
                    // quatre boutons, une permutation parmi vingt-quatre.
                    int previous = character[0x2bb];
                    int value = previous;
                    if ((((byte*)pad)[2] & 0x80) != 0) value = previous - 1;
                    if ((((byte*)pad)[6] & 0x20) != 0) value++;
                    if (value < 0) value = 0x17;
                    if (value > 0x17) value = 0;
                    character[0x2bb] = (byte)value;
                    value &= 0xff;

                    if (previous != value)
                    {
                        *(ushort*)(character + 0x1fc0) =
                            s_ButtonMasks_0056aa98[s_ButtonOrder_0056aa38[value * 4]];
                        *(ushort*)(character + 0x1fc2) =
                            s_ButtonMasks_0056aa98[s_ButtonOrder_0056aa38[value * 4 + 1]];
                        *(ushort*)(character + 0x1fc4) =
                            s_ButtonMasks_0056aa98[s_ButtonOrder_0056aa38[value * 4 + 2]];
                        // Le quatrieme saute +0x1fc6 et +0x1fc8 — c'est ecrit ainsi dans le
                        // binaire (mov [ebp+1FCAh],ax en 0x0052d090).
                        *(ushort*)(character + 0x1fca) =
                            s_ButtonMasks_0056aa98[s_ButtonOrder_0056aa38[value * 4 + 3]];
                        Aud.PlayUiSound3A();
                    }
                    break;
                }
            }

            // ---- le curseur, pose sur la rangee courante ---------------------------------
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(uint*)(sprite + 4) = 0x1000000;
            *(short*)(sprite + 8) = 0x58;
            *(short*)(sprite + 0x0a) = (short)(s_Unknown_005de6f8 * 0x1a + 0x24);

            UiWin.EmitViewportRect(otBase, &cursor);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            UiWin.EmitClipRect(otBase, &cursor);
        }

        // ---- les trois cadres de rangee --------------------------------------------------
        *(uint*)(sprite + 4) = 0x1000000;
        *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        *(short*)(sprite + 0x0a) = 0x24;
        *(ushort*)(sprite + 0x10) = 0x3a;

        UiWin.EmitViewportRect(otBase, &cursor);
        for (int row = 0; row < 3; row++)
        {
            *(short*)(sprite + 8) = (short)(s_ConfigRowOffsets_0056aa18[row] + 0x60);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            *(short*)(sprite + 0x0a) += 0x1a;
            (*(ushort*)(sprite + 0x10))++;
        }
        UiWin.EmitClipRect(otBase, &cursor);

        // ---- les trois etiquettes ---------------------------------------------------------
        *(byte**)sprite = (byte*)s_GlyphAtlas_00566c28;
        *(short*)(sprite + 8) = 0xb0;
        *(short*)(sprite + 0x0a) = 0x28;
        cursor = Prims.DrawTextRun(
            otBase, cursor, (int*)sprite,
            s_ControllerTypeNames_0056aa20[character[0x2b7]], 0, 0, 0x10);

        *(short*)(sprite + 0x0a) += 0x1a;
        *(short*)(sprite + 8) = 0x98;
        cursor = Prims.DrawTextRun(
            otBase, cursor, (int*)sprite,
            s_ToggleNames_0056aa30[character[0x2ba]], 0, 0, 0x10);

        *(short*)(sprite + 0x0a) += 0x1a;
        *(short*)(sprite + 8) = 0x98;
        cursor = Prims.DrawTextRun(
            otBase, cursor, (int*)sprite,
            s_ToggleNames_0056aa30[character[0x2b9]], 0, 0, 0x10);

        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 1</c> du switch de pages — <b>la page « Objets »</b>, celle des objets
    /// de quete. Blaze.c 76574..76672, binaire 0x0052d2b9..0x0052d50b.
    ///
    /// <para>
    /// Elle parcourt les <b>trente-deux premiers bits</b> de <c>DAT_005e9840</c> — la banque de
    /// drapeaux d'evenement de la partie, celle que les scripts de zone allument — et pour chaque
    /// bit leve prend un enregistrement de huit octets dans <c>DAT_005e97c8 + 0x70</c> : un nom en
    /// <c>+0</c>, une description en <c>+4</c>. C'est ce que le prologue teste avant d'y entrer, et
    /// c'est pourquoi la page se saute tant que le dword est nul.
    /// </para>
    /// <para>
    /// <b>Le role est etabli a l'ecran, pas deduit</b> : une sonde qui leve les quatre premiers
    /// bits fait apparaitre « Clé de l'aubergiste, Pierre triangulaire, Pierre-faucille, Pierre
    /// pentagonale » sous le titre « - Objets - », avec « Pierre étrange qui émet une lumière
    /// blafarde » en description. Ces trente-deux bits sont donc les objets de quete que le groupe
    /// porte — le titre lui-meme vit dans le fichier de ressources, pas dans le binaire, il ne se
    /// lit qu'en jouant.
    /// </para>
    /// <para>
    /// <b>Tout se dessine par une seule chaine.</b> Les noms sont concatenes, separes par
    /// <c>", "</c>, dans un tampon de brouillon a <c>MainMem + 0x1b0000</c>, puis le tampon entier
    /// part dans le moteur de texte. L'entree selectionnee est encadree par deux codes de controle
    /// (<c>0x14 0x06</c> avant, <c>0x14 0x07</c> apres) — c'est-a-dire une couleur, pas un curseur
    /// dessine.
    /// </para>
    /// <para>
    /// <b>Le defilement est un remplacement, pas un cadrage.</b> Au-dela de douze entrees, les
    /// <c>compte - 12</c> premieres sont ecrites <c>"..."</c> au lieu de leur nom — elles gardent
    /// leur place dans la chaine et dans la numerotation. Rien ne bouge, le texte raccourcit.
    /// </para>
    /// <para>
    /// <b>Deux etats, portes par <c>DAT_005de70c</c> seul</b> : negatif, la liste n'est pas entree
    /// et le panneau du bas montre le libelle fixe de <c>+0x2e4</c> ; a zero ou plus, les fleches
    /// deplacent <c>DAT_005de6fc</c> dans la liste et le panneau montre la description de l'entree
    /// visee. Entrer demande le carre <b>et</b> au moins une entree ; sortir est la croix.
    /// </para>
    /// <para>
    /// <b>Une ecriture d'un octet avant le tampon</b> quand la liste est vide : <c>text[-2] = 0</c>
    /// enleve le dernier <c>", "</c>, et sans entree il n'y en a pas eu. Le prologue rend le cas
    /// inatteignable (on n'entre pas sur la page quand le dword est nul) et l'ecriture reste dans
    /// l'arene, donc c'est transcrit tel quel — voir <c>legacy-bug.md</c> si le cas se presente.
    /// </para>
    /// </summary>
    private static uint* RunKeyItemsPage(uint* otBase, uint* cursor, byte* slot, ushort* pad)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* page = MenuDescriptor(1);

        BindMenuLabels(*(byte**)(labelTable + 0x370), (uint)(nint)slot, page, 0);
        cursor = Ui.RunTextEngine(cursor, page);

        // ---- combien de drapeaux sont leves ------------------------------------------------
        // Le binaire indexe la banque par mot (`sar ecx,5`) alors que la borne est 0x20 : l'index
        // vaut donc toujours zero. Ecrit comme le binaire l'ecrit, pas comme il se simplifie.
        uint* flagBank = (uint*)Mem.Arena(0x005e9840);
        int count = 0;
        for (int bit = 0; bit < 0x20; bit++)
        {
            if ((flagBank[bit >> 5] & (1u << (bit & 0x1f))) != 0) count++;
        }

        int elided = count < 0xd ? 0 : count - 0xc;

        // ---- entrer dans la liste, en sortir, s'y deplacer ---------------------------------
        if (s_Unknown_005de70c < 0)
        {
            if ((((byte*)pad)[3] & 0x80) != 0 && count != 0)
            {
                Aud.PlayUiSound38();                                // Ghidra: FUN_0053db80
                s_Unknown_005de70c = 0;
                s_Unknown_005de6fc = 0;
            }
        }
        else
        {
            if ((((byte*)pad)[3] & 0x40) != 0)
            {
                Aud.PlayUiSound39();                                // Ghidra: FUN_0053dba0
                s_Unknown_005de70c = -1;
            }

            int before = s_Unknown_005de6fc;

            // Les deux bornes ne sont pas symetriques : la gauche s'arrete a zero par un test
            // d'egalite, la droite compare a `compte - 1`. C'est ce que fait le binaire.
            if ((((byte*)pad)[6] & 0x80) != 0 && s_Unknown_005de6fc != 0) s_Unknown_005de6fc--;
            if ((((byte*)pad)[6] & 0x20) != 0 && s_Unknown_005de6fc < count - 1) s_Unknown_005de6fc++;

            if (before != s_Unknown_005de6fc) Aud.PlayUiSound3A();  // Ghidra: FUN_0053db60
        }

        // ---- monter la chaine ---------------------------------------------------------------
        byte* text = GetMainMemPtr() + 0x1b0000;
        byte* entries = *(byte**)(s_Unknown_005e97c8 + 0x70);
        int shown = 0;
        int remaining = elided;

        for (int bit = 0; bit < 0x20; bit++)
        {
            if ((flagBank[bit >> 5] & (1u << (bit & 0x1f))) == 0) continue;

            // La correspondance rang affiche -> numero de drapeau est gardee : c'est elle qui
            // retrouve la description de l'entree visee, plus bas.
            s_KeyItemIndex_005de6a8[shown] = (byte)bit;

            bool selected = shown == s_Unknown_005de6fc && s_Unknown_005de70c >= 0;

            if (selected)
            {
                *text++ = 0x14;
                *text++ = 6;
            }

            if (remaining != 0)
            {
                remaining--;
                *text++ = (byte)'.';
                *text++ = (byte)'.';
                *text++ = (byte)'.';
            }
            else
            {
                byte* name = *(byte**)(entries + bit * 8);
                while (*name != 0) *text++ = *name++;
            }

            if (selected)
            {
                *text++ = 0x14;
                *text++ = 7;
            }

            *text++ = (byte)',';
            *text++ = (byte)' ';
            shown++;
        }

        // Le dernier « , » est ecrase par le terminateur.
        text[-2] = 0;

        // ---- le panneau du bas ---------------------------------------------------------------
        byte* detail;
        if (s_Unknown_005de70c < 0)
        {
            detail = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x2e4);
        }
        else
        {
            BindMenuLabels(Mem.s_Unknown_005606f8, (uint)(nint)slot, MenuDescriptor(0), 0);
            cursor = Ui.RunTextEngine(cursor, MenuDescriptor(0));

            detail = *(byte**)(*(byte**)(s_Unknown_005e97c8 + 0x70)
                               + s_KeyItemIndex_005de6a8[s_Unknown_005de6fc] * 8 + 4);
        }

        BindMenuLabels(detail, (uint)(nint)slot, MenuDescriptor(0), 0);

        BindMenuLabels(GetMainMemPtr() + 0x1b0000, (uint)(nint)slot, page, 0);
        cursor = Ui.RunTextEngine(cursor, page);

        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 2</c> du switch de pages — <b>la page « Table », celle des cristaux</b>.
    /// Blaze.c 76672..76697, binaire 0x0052d510..0x0052d60e : <b>vingt-cinq lignes, et aucune
    /// touche lue</b>: c'est une page d'affichage pur, la seule des six.
    ///
    /// <para>
    /// Elle pose dix images aux dix positions de <c>0x00564748</c>, une par bit leve parmi les
    /// <b>dix premiers</b> de <c>DAT_005e9000</c>. La tuile est l'indice du bit, pas le rang de
    /// l'affichage : le compteur avance a chaque tour, meme quand le bit est absent — un trou reste
    /// donc un trou. Le prologue saute la page tant que <c>DAT_005e9001</c> n'a pas son bit
    /// <c>0x10</c>, c'est-a-dire le bit 12 de la meme banque.
    /// </para>
    /// <para>
    /// <b>Le role est etabli a l'ecran</b> : une sonde qui leve les bits 0 a 9 et le bit 12 montre
    /// le titre « - Table - », le panneau « Page Cristaux », et une dalle de pierre gravee portant
    /// neuf cristaux. Les neuf premieres tuiles sont les cristaux (0x20 par 0x20, descripteurs
    /// <c>0x005646f8</c>..<c>0x00564738</c>), la <b>dixieme est la dalle elle-meme</b> — un
    /// descripteur de 0x80 par 0x70 en <c>0x00564740</c>, pose en dernier et donc derriere. Le bit 9
    /// n'est pas un cristal de plus : c'est le support.
    /// </para>
    /// <para>
    /// <c>DAT_005e9000</c> est le cumul par OU des blocs de trente-deux octets de <c>+0x2e0</c> de
    /// chaque membre (voir <c>RefreshPartyAbilityMask</c>) : ces bits-la vivent donc dans les
    /// personnages, pas dans la partie. C'est aussi pourquoi une sonde posee sur la banque ne tient
    /// pas — le prologue rappelle <c>FUN_00535710</c> avant de tester le saut de page, et la
    /// reconstruit.
    /// </para>
    /// <para>
    /// <b>Les deux tables sont deja dans le depot</b>, et voisines : les descripteurs de pastille
    /// commencent a <c>0x005646f8</c>, les positions a <c>0x00564748</c>, toutes dans la plage
    /// d'atlas relevee d'un bloc pour la carte du monde (voir <c>SpriteAtlas</c> dans
    /// <c>WorldMapTick.cs</c>, et la lecon « un descripteur ne se releve pas seul »). La borne de
    /// boucle du binaire est une <b>adresse</b>, <c>0x00564770</c>, pas un compte — elle est gardee
    /// telle quelle ici.
    /// </para>
    /// <para>
    /// <b>Le dword de couleur n'est pas repose.</b> Rien dans ce corps n'ecrit <c>DAT_005de5fc</c> :
    /// la page se sert de ce que la precedente a laisse dans le bloc de brouillon. Verifie au
    /// desassembleur — aucun acces a <c>0x005de5fc</c> entre 0x0052d510 et 0x0052d60e.
    /// </para>
    /// <para>
    /// Ghidra scinde le curseur de primitives en deux (<c>uVar11</c> et <c>param_3</c>) parce que
    /// le binaire le garde a la fois dans <c>eax</c> et dans son emplacement de pile ; les deux
    /// valent toujours la meme chose. Un seul curseur ici.
    /// </para>
    /// </summary>
    private static uint* RunCrystalTablePage(uint* otBase, uint* cursor, byte* slot)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* page = MenuDescriptor(1);

        BindMenuLabels(*(byte**)(labelTable + 0x374), (uint)(nint)slot, page, 0);
        cursor = Ui.RunTextEngine(cursor, page);

        RefreshPartyAbilityMask();                          // Ghidra: FUN_00535710

        byte* sprite = s_MenuSprite_005de5f8;
        *(byte**)sprite = SpriteAtlas(0x005646f8);
        *(ushort*)(sprite + 0x10) = 0;

        UiWin.EmitViewportRect(otBase, &cursor);

        uint* flagBank = (uint*)Mem.Arena(0x005e9000);
        byte* position = SpriteAtlas(0x00564748);
        byte* end = SpriteAtlas(0x00564770);

        for (int bit = 0; position < end; bit++, position += 4)
        {
            if ((flagBank[bit >> 5] & (1u << (bit & 0x1f))) != 0)
            {
                // La position est un dword : X dans le mot bas, Y dans le mot haut.
                *(uint*)(sprite + 8) = *(uint*)position;
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            }

            (*(ushort*)(sprite + 0x10))++;
        }

        *(ushort*)(sprite + 0x10) = 0;
        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        BindMenuLabels(*(byte**)(labelTable + 0x2e8), (uint)(nint)slot, MenuDescriptor(0), 0);

        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 3</c> du switch de pages — <b>la page « Organisation du groupe »</b>.
    /// Blaze.c 76697..76833, binaire 0x0052d613..0x0052db32.
    ///
    /// <para>
    /// Gauche/droite choisissent un membre, haut/bas changent sa <b>couleur</b> (l'octet
    /// <c>+0x2bd</c> du personnage, borne a 0..3 par un simple <c>&amp; 3</c>), le carre lui donne
    /// le role de meneur, et la croix le met de cote. Chaque membre est ensuite dessine avec son
    /// portrait, son numero de couleur, son pictogramme de manette et, pour le meneur, une marque
    /// au-dessus.
    /// </para>
    /// <para>
    /// <b><c>+0x2bd</c> est bien une couleur, pas un rang</b>, et l'ecran le dit deux fois : le
    /// panneau affiche « Couleur 0 », et deux appuis sur haut font passer les cheveux du portrait
    /// de bleu a brun. C'est coherent avec l'usage qu'en fait la fiche de personnage, qui passe ce
    /// meme octet en <c>extra</c> au portrait — le selecteur de palette.
    /// </para>
    /// <para>
    /// <b>Donner le role de meneur echange deux sieges, il ne les copie pas.</b> Si le membre
    /// choisi etait ecarte (bit <c>0x10000</c> de ses drapeaux), il reprend le siege de l'ancien
    /// meneur — les octets <c>+8</c> et <c>+9</c> de l'entree de <c>DAT_005e9180</c>, c'est-a-dire
    /// l'emplacement de manette et l'index de personnage — et <b>c'est l'ancien meneur qui herite
    /// de l'ecart</b>. Le meme geste que <c>PassLeaderToNextEligible</c> fait de son cote.
    /// </para>
    /// <para>
    /// <b>Deux refus, deux raisons.</b> Le carre est refuse si le membre vise est deja meneur ou si
    /// son bloc <c>+0x140</c> porte l'un des bits <c>0x400001c0</c> (mort, petrifie, ce genre
    /// d'etat) ; la croix est refusee tant que <c>DAT_005de034</c> est sous 2, c'est-a-dire quand le
    /// groupe n'a pas de quoi se passer d'un membre. Les deux jouent le meme bip de refus.
    /// </para>
    /// <para>
    /// <b>L'abscisse vient de l'index de personnage, pas du rang de la boucle</b> :
    /// <c>caractere * 0x35 + 0x18</c> pour les pictogrammes, <c>+ 0x1c</c> pour le portrait. Les
    /// deux se lisent dans <c>DAT_005e93c8</c> — l'octet <c>+8</c> de l'entree de drapeaux — et non
    /// dans le compteur. Les enregistrements de manette, eux, s'indexent par ce meme index de
    /// personnage au pas <c>0x4c</c>, tandis que la couleur s'indexe par le rang de la
    /// boucle au pas <c>0x2000</c>. Trois indexations differentes dans une seule boucle : c'est le
    /// piege du corps.
    /// </para>
    /// <para>
    /// Le dernier <c>FUN_00521df0</c> recoit un <c>extra</c> que Ghidra ecrit
    /// <c>CONCAT22((short)(DAT_005de6f8 &gt;&gt; 0x10), couleur)</c> : le binaire fait
    /// <c>movzx ax, byte [ebx]</c> sur un <c>eax</c> qui portait encore <c>DAT_005de6f8</c>, donc
    /// seul le mot bas est remplace. Le mot haut est <b>demontrablement nul</b> — quinze
    /// instructions plus haut, ce meme <c>DAT_005de6f8</c> est borne dans
    /// <c>[0, DAT_005e9020)</c>. Ecrit ici comme la seule couleur.
    /// </para>
    /// </summary>
    private static uint* RunPartyOrderPage(uint* otBase, uint* cursor, byte* slot, ushort* pad)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* page = MenuDescriptor(1);

        BindMenuLabels(*(byte**)(labelTable + 0x378), (uint)(nint)slot, page, 0);
        cursor = Ui.RunTextEngine(cursor, page);

        // ---- le choix du membre ---------------------------------------------------------
        int previousMember = s_Unknown_005de6f8;

        if ((((byte*)pad)[6] & 0x80) != 0) s_Unknown_005de6f8--;
        if ((((byte*)pad)[6] & 0x20) != 0) s_Unknown_005de6f8++;
        if (s_Unknown_005de6f8 < 0) s_Unknown_005de6f8 = s_PartySize_005e9020 - 1;
        if (s_Unknown_005de6f8 >= s_PartySize_005e9020) s_Unknown_005de6f8 = 0;

        if (previousMember != s_Unknown_005de6f8)
        {
            // Les deux touches de direction sont consommees : sans cela le meme appui changerait
            // de membre ET de couleur dans la meme image.
            pad[1] &= 0xffaf;
            Aud.PlayUiSound3A();                                // Ghidra: FUN_0053db60
        }

        int member = s_Unknown_005de6f8;
        byte* character = GetMainMemPtr() + ((member + 0x78) << 0xd);
        byte* memberFlagBytes = Inp.s_SlotFlags_005e93c0 + member * 0x9c;
        uint* memberFlags = (uint*)memberFlagBytes;
        byte* memberSeat = Inp.s_SlotArea_005e9180 + member * 0x4c;

        // ---- la couleur du personnage ----------------------------------------------------
        byte previousColour = character[0x2bd];
        if ((pad[1] & 0x10) != 0) character[0x2bd] = (byte)(previousColour + 1);
        if ((pad[1] & 0x40) != 0) character[0x2bd]--;

        character[0x2bd] &= 3;
        memberFlagBytes[0x0b] = character[0x2bd];

        if (previousColour != character[0x2bd]) Aud.PlayUiSound3A();

        // ---- le carre : ce membre prend la tete -------------------------------------------
        if ((((byte*)pad)[3] & 0x80) != 0)
        {
            int leader = Mem.s_Unknown_005de538 & 0xff;

            if (member == leader || (*(uint*)(character + 0x140) & 0x400001c0) != 0)
            {
                Aud.PlayUiSound3B();                            // Ghidra: FUN_0053dbc0, le refus
            }
            else
            {
                byte* leaderSeat = Inp.s_SlotArea_005e9180 + leader * 0x4c;

                if ((*memberFlags & 0x10000) != 0)
                {
                    // L'echange de sieges : emplacement de manette et index de personnage.
                    byte seatPad = memberSeat[8];
                    byte seatCharacter = memberSeat[9];
                    memberSeat[8] = leaderSeat[8];
                    memberSeat[9] = leaderSeat[9];
                    leaderSeat[8] = seatPad;
                    leaderSeat[9] = seatCharacter;

                    *memberFlags &= 0xfffeffff;
                    *(uint*)(Inp.s_SlotFlags_005e93c0 + leader * 0x9c) |= 0x10000;

                    // Le membre garde en reserve suivait l'ancien meneur : il suit le nouveau.
                    if (s_Unknown_005de540 == leader) s_Unknown_005de540 = (byte)s_Unknown_005de6f8;
                }

                *memberFlags &= 0xf7fffffb;
                Mem.s_Unknown_005de538 = (byte)s_Unknown_005de6f8;
                *(uint*)(Inp.s_SlotFlags_005e93c0 + leader * 0x9c) &= 0xf7fffffb;
                Aud.PlayUiSound38();                            // Ghidra: FUN_0053db80
            }
        }

        // ---- la croix : ce membre est mis de cote ------------------------------------------
        if ((((byte*)pad)[3] & 0x40) != 0)
        {
            if (s_Unknown_005de034 < 2)
            {
                Aud.PlayUiSound3B();
            }
            else
            {
                *memberFlags |= 0x10000;
                PassLeaderToNextEligible(memberFlags, memberSeat, 0x10000);
                s_Unknown_005de540 = (byte)(Mem.s_Unknown_005de538 & 0xff);
                Aud.PlayUiSound39();                            // Ghidra: FUN_0053dba0
            }
        }

        // ---- la colonne de chaque membre ----------------------------------------------------
        byte* sprite = s_MenuSprite_005de5f8;
        byte* colour = GetMainMemPtr() + 0xf02bd;
        byte* seat = Inp.s_SlotFlags_005e93c0 + 8;              // Ghidra: &DAT_005e93c8

        for (int index = 0; index < s_PartySize_005e9020;
             index++, seat += 0x9c, colour += 0x2000)
        {
            byte characterIndex = *seat;
            byte padSlot = Inp.s_PlayerSlots_005e9188[characterIndex * 0x4c];

            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            UiWin.EmitViewportRect(otBase, &cursor);

            *(short*)(sprite + 8) = (short)(characterIndex * 0x35 + 0x18);
            *(short*)(sprite + 0x0a) = 0x30;

            // La marque du meneur, au-dessus de sa colonne.
            if (index == (Mem.s_Unknown_005de538 & 0xff))
            {
                *(ushort*)(sprite + 0x10) = 0x2a;
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            }

            *(short*)(sprite + 8) += 8;
            *(short*)(sprite + 0x0a) += 0x40;

            // Ecarte ou non : le bit 0x10000 des drapeaux choisit entre deux tuiles voisines.
            *(ushort*)(sprite + 0x10) = (ushort)(((*(uint*)(seat - 8) >> 0x10) & 1) + 0x48);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);

            // Manette branchee ou non, meme principe a l'envers : 0x2b si oui, 0x2c si non.
            *(ushort*)(sprite + 0x10) = (ushort)(0x2c - (padSlot < 8 ? 1 : 0));
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);

            *(short*)(sprite + 8) -= 4;
            *(short*)(sprite + 0x0a) += 0x18;
            *(ushort*)(sprite + 0x10) = 0x2d;
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            UiWin.EmitClipRect(otBase, &cursor);

            // ---- le numero de couleur -----------------------------------------------------
            *(short*)(sprite + 8) += 0x20;
            *(byte**)sprite = SpriteAtlas(0x00566c30);
            *(uint*)(sprite + 4) = 0x1000000;
            cursor = Prims.DrawDecimalNumber(otBase, cursor, (int*)sprite, *colour, 1, 0);

            UiWin.EmitViewportRect(otBase, &cursor);

            // ---- le pictogramme de manette, seulement si elle est branchee -----------------
            if (padSlot < 8)
            {
                *(short*)(sprite + 8) -= 0x18;
                *(short*)(sprite + 0x0a) -= 10;
                *(byte**)sprite = SpriteAtlas(0x00564798)
                                  + Inp.s_PlayerSlots_005e9188[characterIndex * 0x4c + 1] * 8;
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            }

            // ---- le portrait ---------------------------------------------------------------
            byte* portraitAtlas =
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 4);
            *(byte**)sprite = portraitAtlas;
            portraitAtlas[5] = 0;                   // Ghidra: DAT_005de5f8[5] = '\0'
            *(ushort*)(sprite + 0x10) = seat[-4];
            *(short*)(sprite + 8) = (short)(characterIndex * 0x35 + 0x1c);
            *(short*)(sprite + 0x0a) = 0x34;

            *(uint*)(sprite + 4) =
                index == s_Unknown_005de6f8 ? s_Unknown_005e9814 : 0x1000000;

            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, *colour);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            UiWin.EmitClipRect(otBase, &cursor);
        }

        BindMenuLabels(*(byte**)(labelTable + 0x2ec), (uint)(nint)slot, MenuDescriptor(0), 0);

        return cursor;
    }

    /// <summary>
    /// Le recadrage de la carte sur le meneur : sa position reduite au quart (<c>sar 0x11</c>),
    /// arrondie au multiple de quatre, bornee a la fenetre de carte.
    ///
    /// <para>Le binaire l'ecrit <b>quatre fois en toute lettre</b> — une dans le prologue au
    /// changement de page, trois dans la page 4 (a l'ouverture de la liste de reperes, a la croix,
    /// et apres avoir pose un repere). Les quatre copies sont identiques instruction pour
    /// instruction ; extraites ici parce que c'est une unite qui se tient seule, pas pour
    /// raccourcir.</para>
    ///
    /// <para>Le masque porte sur les seize bits : <c>and al,0FCh</c> laisse <c>ah</c> intact, ce
    /// que <c>&amp; 0xfffc</c> reproduit puisque les deux bits effaces sont dans l'octet bas.</para>
    /// </summary>
    private static void RecentreMapOnLeader()
    {
        byte* leader = Inp.s_SlotFlags_005e93c0 + (uint)Mem.s_Unknown_005de538 * 0x9c;
        s_Unknown_005e8eda = (short)((*(int*)(leader + 0x24) >> 0x11) & 0xfffc);
        s_Unknown_005e8ed8 = (short)((*(int*)(leader + 0x1c) >> 0x11) & 0xfffc);

        if (s_Unknown_005e8eda > 0xa0) s_Unknown_005e8eda = 0xa0;
        if (s_Unknown_005e8eda < -0xa0) s_Unknown_005e8eda = unchecked((short)0xff60);
        if (s_Unknown_005e8ed8 < -0x90) s_Unknown_005e8ed8 = unchecked((short)0xff70);
        if (s_Unknown_005e8ed8 > 0x90) s_Unknown_005e8ed8 = 0x90;
    }

    /// <summary>
    /// Ghidra: le <c>case 4</c> du switch de pages — <b>la carte du monde et ses reperes</b>.
    /// Blaze.c 76833..77060, binaire 0x0052db5b..0x0052e33e.
    ///
    /// <para>
    /// La plus grosse page apres la fiche, et la seule qui sorte de l'interface : elle fait defiler
    /// l'image de carte, y place <b>huit reperes</b> que le joueur pose lui-meme, et dessine la
    /// position du groupe avec un vrai modele 3d — camera mise de cote, vue fixe a 800, modele
    /// emis, pose restituee. Le meme geste que la rose des vents de <c>WorldMapTick.cs</c>, dont
    /// c'est le jumeau.
    /// </para>
    /// <para>
    /// <b>Deux etats, portes par <c>DAT_005de70c</c>.</b> Negatif, les quatre directions font
    /// defiler la carte de quatre unites par image — lues sur le mot <b>0</b> de la manette, l'etat
    /// <i>tenu</i>, et non sur un front : c'est ce qui rend le defilement continu. Le carre ouvre
    /// la liste des huit reperes, la croix recentre sur le meneur. A zero, gauche/droite parcourent
    /// la liste, la croix la referme, et le carre <b>pose ou retire</b> le repere vise.
    /// </para>
    /// <para>
    /// <b>Un repere fait huit octets</b> : deux <c>short</c> de position monde reduite au quart,
    /// puis un mot d'etiquette qui empaquete <c>DAT_00566c84</c>, <c>DAT_00566c88</c> et la zone
    /// courante. Un repere vide se reconnait a son <c>short</c> de <c>+4</c> negatif — et l'effacer
    /// ecrit <b>quatre</b> octets de <c>0xff</c>, pas deux. Il n'est dessine que si son etiquette
    /// correspond a la zone affichee, et l'octet de zone est <b>ignore</b> quand le terrain courant
    /// n'a pas le bit <c>0x10</c> : les deux cotes de la comparaison sont alors amputes de leur
    /// octet bas.
    /// </para>
    /// <para>
    /// <b>Le defilement de l'image de carte se fait dans le descripteur d'atlas lui-meme</b> : les
    /// deux premiers octets de <c>**(DAT_005e97cc + 0x94)</c> — l'origine u et v — sont reecrits a
    /// chaque image depuis le decalage de camera. Ce n'est pas la position du sprite qui bouge,
    /// c'est la fenetre lue dans la texture.
    /// </para>
    /// <para>
    /// Le bloc de modele de <c>0x005de6c8</c> a exactement la forme du cadre de pile de la rose des
    /// vents : trois entiers de position, deux d'echelle, puis deux mots et un mot d'etiquette. La
    /// seule difference tient a <b>deux ecritures de seize bits</b> la ou la rose en fait de
    /// trente-deux (<c>+0x10</c> et <c>+0x18</c>) : le mot haut garde ce qu'il avait, et c'est ainsi
    /// dans le binaire.
    /// </para>
    /// </summary>
    private static uint* RunWorldMapPage(uint* otBase, uint* cursor, byte* slot, ushort* pad)
    {
        byte* sprite = s_MenuSprite_005de5f8;

        if (s_Unknown_005de70c < 0)
        {
            // ---- le defilement, sur l'etat TENU (mot 0) ----------------------------------
            if ((pad[0] & 0x10) != 0 && s_Unknown_005e8eda < 0xa0) s_Unknown_005e8eda += 4;
            if ((pad[0] & 0x40) != 0 && s_Unknown_005e8eda > -0xa0) s_Unknown_005e8eda -= 4;
            if ((pad[0] & 0x80) != 0 && s_Unknown_005e8ed8 > -0x90) s_Unknown_005e8ed8 -= 4;
            if ((pad[0] & 0x20) != 0 && s_Unknown_005e8ed8 < 0x90) s_Unknown_005e8ed8 += 4;

            if ((((byte*)pad)[3] & 0x80) != 0)
            {
                s_Unknown_005de6fc = 0;
                s_Unknown_005de70c = 0;
                Aud.PlayUiSound38();                            // Ghidra: FUN_0053db80
            }

            if ((((byte*)pad)[3] & 0x40) != 0)
            {
                RecentreMapOnLeader();
                Aud.PlayUiSound39();                            // Ghidra: FUN_0053dba0
            }
        }
        else
        {
            // ---- la liste des huit reperes -----------------------------------------------
            int previousMarker = s_Unknown_005de6fc;

            if ((((byte*)pad)[6] & 0x80) != 0) s_Unknown_005de6fc--;
            if ((((byte*)pad)[6] & 0x20) != 0) s_Unknown_005de6fc++;
            if (s_Unknown_005de6fc < 0) s_Unknown_005de6fc = 7;
            if (s_Unknown_005de6fc > 7) s_Unknown_005de6fc = 0;

            if (previousMarker != s_Unknown_005de6fc) Aud.PlayUiSound3A();

            if ((((byte*)pad)[3] & 0x40) != 0)
            {
                s_Unknown_005de70c = -1;
                Aud.PlayUiSound39();
            }

            if ((((byte*)pad)[3] & 0x80) != 0)
            {
                s_MarkerCursor_005de618 = (short*)Mem.Arena(0x005e9320 + s_Unknown_005de6fc * 8);

                if (*(short*)(s_MarkerCursor_005de618 + 2) >= 0)
                {
                    // Le repere existe : on l'efface. Le test lit DEUX octets, l'effacement en
                    // ecrit QUATRE — c'est ainsi dans le binaire (`mov [eax+4],ebp`, ebp = -1).
                    *(int*)(s_MarkerCursor_005de618 + 2) = -1;
                    Aud.PlayUiSound39();
                }
                else
                {
                    s_MarkerCursor_005de618[0] = (short)(*(int*)Mem.Camera(0x005de068) >> 0x11);
                    s_MarkerCursor_005de618[1] = (short)(*(int*)Mem.Camera(0x005de070) >> 0x11);
                    *(uint*)(s_MarkerCursor_005de618 + 2) = CurrentMarkerTag();

                    RecentreMapOnLeader();

                    s_Unknown_005de70c = -1;
                    Aud.PlayUiSound38();
                }
            }
        }

        // ---- les deux decalages de camera, bornes une seconde fois -------------------------
        // Le binaire refait les bornes ici au lieu de se fier a celles du bloc d'entree, et il les
        // range dans deux entiers de pile : c'est cette copie bornee que le dessin utilise, pas
        // les globales.
        int cameraY = s_Unknown_005e8ed8 < -0x90 ? -0x90
                    : s_Unknown_005e8ed8 > 0x90 ? 0x90
                    : s_Unknown_005e8ed8;

        int cameraX = s_Unknown_005e8eda < -0xa0 ? -0xa0
                    : s_Unknown_005e8eda > 0xa0 ? 0xa0
                    : s_Unknown_005e8eda;

        UiWin.EmitViewportRect(otBase, &cursor);

        // ---- le curseur de la liste, seulement quand elle est ouverte ----------------------
        if (s_Unknown_005de70c == 0)
        {
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(uint*)(sprite + 4) = 0x1000000;
            *(short*)(sprite + 8) = (short)(s_Unknown_005de6fc * 10 + 0xc);
            *(short*)(sprite + 0x0a) = 0xc0;
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        }

        // ---- les huit reperes ---------------------------------------------------------------
        *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        s_MarkerCursor_005de618 = (short*)Mem.Arena(0x005e9320);

        // Le repere clignote sur six images ; le compteur est divise NON SIGNE apres decalage.
        *(ushort*)(sprite + 0x10) = (ushort)((s_FadeCounter_005e9960 >> 1) % 6 + 0x34);

        uint currentTag = CurrentMarkerTag();

        for (int index = 0; index < 8; index++, s_MarkerCursor_005de618 += 4)
        {
            short markerX = (short)(s_MarkerCursor_005de618[0] - cameraY + 0x7c);
            short markerY = (short)(cameraX - s_MarkerCursor_005de618[1] + 0x6c);

            *(uint*)(sprite + 4) = 0x1000000;
            *(short*)(sprite + 8) = markerX;
            *(short*)(sprite + 0x0a) = markerY;

            uint markerTag = *(uint*)(s_MarkerCursor_005de618 + 2);
            uint wanted = currentTag;

            // Sans le bit 0x10 sur le terrain courant, l'octet de zone ne compte pas : les deux
            // cotes perdent leur octet bas avant la comparaison.
            if ((**(byte**)Mem.Entity(0x005eca80) & 0x10) == 0)
            {
                markerTag &= 0xffffff00;
                wanted &= 0xffffff00;
            }

            if (markerTag == wanted
                && markerX > 0x11 && markerX < 0xe9
                && markerY > 0x11 && markerY < 0xc9)
            {
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, (uint)index);
            }

            // La vignette de la liste, dessinee que le repere existe ou non.
            *(short*)(sprite + 8) = (short)(index * 10 + 0x18);
            *(short*)(sprite + 0x0a) = 0xc4;
            *(uint*)(sprite + 4) =
                *(short*)(s_MarkerCursor_005de618 + 2) < 0 ? 0x1000000 : s_Unknown_005e9814;

            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, (uint)index);
        }

        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);

        // ---- le groupe, en modele 3d ---------------------------------------------------------
        short partyX = (short)((*(int*)Mem.Camera(0x005de068) >> 0x11) - cameraY + 0x80);
        short partyY = (short)(cameraX - (*(int*)Mem.Camera(0x005de070) >> 0x11) + 0x70);
        *(short*)(sprite + 8) = partyX;
        *(short*)(sprite + 0x0a) = partyY;

        if (partyX > 0x14 && partyX < 0xec && partyY > 0x14 && partyY < 0xcc)
        {
            Winsys3d.Ordinal_378(partyX, partyY);               // SetGeomOffset

            short* pose = (short*)Mem.s_CameraVector_00604928;

            // Mise de cote du decalage monde, de la pose et de la matrice — a l'identique de
            // DrawCompassRose dans WorldMapTick.cs.
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

            // Le cap est INVERSE le temps du rendu : la carte se lit du dessus, le modele doit
            // tourner dans l'autre sens que la vue du monde.
            pose[1] = (short)-pose[1];
            pose[0] = 0x400;
            RotMatrix(pose, (short*)Mem.s_CameraMatrix_006008e0);

            Mem.CameraTranslation[0] = 0;
            Mem.CameraTranslation[1] = 0;
            Mem.CameraTranslation[2] = 800;

            int* placement = (int*)s_MapModelBlock_005de6c8;
            *(short*)((byte*)placement + 0x1a) = 4;
            placement[7] = 0x1ffffff;
            placement[3] = 0x200;
            *(short*)((byte*)placement + 0x10) = 0x200;
            placement[5] = 0;
            *(short*)((byte*)placement + 0x18) = 0;
            placement[0] = 0;
            placement[1] = 0;
            placement[2] = 0;

            // Element 2 du tableau d'offsets du paquet de modeles — la rose des vents prend le 0.
            cursor = DrawWorldModel_0051C420(
                otBase, cursor, *(byte**)(*(byte**)s_ModelResultB_005f7b40 + 8), placement, 0xc);

            for (int i = 0; i < 8; i++)
            {
                ((uint*)Mem.s_CameraMatrix_006008e0)[i] = ((uint*)Mem.Arena(0x005e9b00))[i];
            }

            Mem.s_Unknown_006008d0 = *(int*)Mem.Entity(0x005fa520);
            Mem.s_Unknown_006008d4 = *(int*)Mem.Entity(0x005fa524);
            Mem.s_Unknown_006008d8 = *(int*)Mem.Entity(0x005fa528);

            pose[0] = *(short*)Mem.Entity(0x005fa534);
            pose[1] = *(short*)Mem.Entity(0x005fa536);
            pose[2] = *(short*)Mem.Entity(0x005fa538);
        }

        // ---- l'image de carte : c'est l'ATLAS qui defile, pas le sprite ----------------------
        int windowV = s_Unknown_005e8ed8 < -0x90 ? 0
                    : s_Unknown_005e8ed8 > 0x90 ? 0x90
                    : (s_Unknown_005e8ed8 >> 1) + 0x80;

        int windowU = s_Unknown_005e8eda < -0xa0 ? 0
                    : s_Unknown_005e8eda > 0xa0 ? 0xa0
                    : 0x80 - (s_Unknown_005e8eda >> 1);

        byte* mapAtlas = *(byte**)*(byte**)(s_Unknown_005e97cc + 0x94);
        *(byte**)sprite = mapAtlas;
        mapAtlas[0] = (byte)(windowV - 0x38);
        mapAtlas[1] = (byte)(windowU - 0x30);

        *(ushort*)(sprite + 0x10) = 0;
        *(short*)(sprite + 8) = 0x80;
        *(short*)(sprite + 0x0a) = 0x70;
        *(uint*)(sprite + 4) = 0x1000000;
        *(short*)(sprite + 0x0c) = 0x2000;
        *(short*)(sprite + 0x0e) = 0x2000;

        cursor = Prims.DrawSpritePrimitive(otBase, cursor, (int*)sprite, 0, 0);
        UiWin.EmitClipRect(otBase, &cursor);

        BindMenuLabels(Mem.s_Unknown_005606f8, (uint)(nint)slot, MenuDescriptor(0), 0);

        return cursor;
    }

    /// <summary>
    /// Ghidra: le <c>case 5</c> du switch de pages — <b>la page « Sauvegarde »</b>. Blaze.c
    /// 77060..77698, binaire 0x0052e343..0x0052f568. <b>638 lignes</b>, la plus grosse du menu.
    ///
    /// <para>
    /// Trois niveaux emboites, portes par trois verrous : <c>DAT_005de710</c> dit quelle entree du
    /// petit menu (Sauvegarde / Quitter) a ete validee, <c>DAT_005de70c</c> quel membre porte la
    /// sauvegarde, <c>DAT_005de714</c> quel emplacement de carte a ete choisi. Puis
    /// <c>DAT_005de71c</c> mene la machine a etats de la carte memoire, du numero 0 au numero 8 et
    /// des numeros negatifs pour les echecs.
    /// </para>
    /// <para>
    /// <b>Le nombre d'emplacements de carte depend des multitaps branches.</b>
    /// <c>DAT_005de6f4</c> vaut 0 a 3 selon que <c>DAT_00604b0a</c> et <c>DAT_00604b2e</c> valent
    /// <c>-0x8000</c>, et indexe deux tables paralleles de <c>0x0056a9a8</c> et <c>0x0056a9e0</c> :
    /// la premiere donne l'identifiant de port, la seconde le compte et les indices de position.
    /// </para>
    /// <para>
    /// <b>La sauvegarde ne se contente pas d'ecrire.</b> Le <c>case 7</c> relit d'abord l'en-tete
    /// du fichier existant, compare sa somme de controle a celle du personnage, <b>fusionne</b> la
    /// table d'icones de la carte avec les dix entrees du personnage, colle le niveau en chiffres a
    /// la fin du titre, recopie huit banques globales et les huit reperes de carte dans le bloc du
    /// personnage, capture la palette depuis la VRAM, recalcule la somme, et n'ecrit qu'ensuite —
    /// en trois morceaux.
    /// </para>
    /// <para>
    /// <b>Et tout cela s'exerce vraiment</b> : ce port remplace la carte memoire par des fichiers
    /// de <c>save\</c>, si bien qu'un aller-retour complet — enumerer, ouvrir, comparer les sommes,
    /// fusionner, ecrire — produit un fichier de <c>0x6000</c> octets sur le disque. Verifie le
    /// 2026-08-25 : « Mémorisation achevée. » a l'ecran et un fichier neuf dans <c>save\</c>.
    /// </para>
    /// </summary>
    private static uint* RunSavePage(uint* otBase, uint* cursor, byte* slot, ushort* pad)
    {
        byte* labelTable = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70);
        byte* root = MenuDescriptor(0);

        BindMenuLabels(*(byte**)(labelTable + 0x37c), (uint)(nint)slot, MenuDescriptor(1), 0);
        cursor = Ui.RunTextEngine(cursor, MenuDescriptor(1));

        int previousSlot = s_CardSlotCursor_005de700;
        int previousMember = s_Unknown_005de6f8;

        if (s_Unknown_005de710 == -1)
        {
            // ---- le petit menu : Sauvegarde ou Quitter --------------------------------------
            // Les deux touches 0x10 et 0x40 du mot repete basculent, indifferemment : c'est un
            // menu a deux entrees, il n'a pas besoin de savoir de quel cote on va.
            if ((pad[3] & 0x50) != 0)
            {
                s_Unknown_005de6fc = ~s_Unknown_005de6fc & 1;
                Aud.PlayUiSound3A();
            }

            if ((((byte*)pad)[3] & 0x80) != 0)
            {
                // « Sauvegarde » est refusee tant que le bit 8 de DAT_005de02c manque — c'est le
                // meme bit que le libelle grise plus bas.
                if ((Mem.s_WorldFlags_005de02c & 8) == 0 && s_Unknown_005de6fc == 0)
                {
                    Aud.PlayUiSound3B();
                }
                else
                {
                    s_Unknown_005de710 = s_Unknown_005de6fc;
                    s_Unknown_005de6f8 = 0;
                    Aud.PlayUiSound38();
                }
            }

            BindMenuLabels(*(byte**)(labelTable + 0x2f0), (uint)(nint)slot, root, 0);
        }
        else if (s_Unknown_005de710 != 0)
        {
            if (s_Unknown_005de710 == 1) StepQuitFlow(labelTable, slot, root, pad);
        }
        else if (s_Unknown_005de70c < 0)
        {
            // ---- le choix du membre qui porte la sauvegarde ---------------------------------
            if ((pad[3] & 0x10) != 0) s_Unknown_005de6f8--;
            if ((pad[3] & 0x40) != 0) s_Unknown_005de6f8++;
            if (s_Unknown_005de6f8 < 0) s_Unknown_005de6f8 = s_PartySize_005e9020 - 1;
            if (s_Unknown_005de6f8 >= s_PartySize_005e9020) s_Unknown_005de6f8 = 0;

            if (previousMember != s_Unknown_005de6f8) Aud.PlayUiSound3A();

            if ((pad[1] & 0x8000) == 0)
            {
                if ((pad[1] & 0x4000) != 0)
                {
                    s_Unknown_005de710 = -1;
                    Aud.PlayUiSound39();
                }
            }
            else if ((*(uint*)(Inp.s_SlotFlags_005e93c0 + s_Unknown_005de6f8 * 0x9c) & 0x40000000) == 0
                     && *(int*)(GetMainMemPtr() + ((s_Unknown_005de6f8 + 0x78) << 0xd)) != 0)
            {
                s_Unknown_005de714 = -1;
                s_CardSlotCursor_005de700 =
                    GetMainMemPtr()[s_Unknown_005de6f8 * 0x2000 + 0xf02bc];
                s_Unknown_005de70c = s_Unknown_005de6f8;
                Aud.PlayUiSound38();
            }
            else
            {
                Aud.PlayUiSound3B();
            }

            BindMenuLabels(*(byte**)(labelTable + 0x3c4), (uint)(nint)slot, root, 0);
        }
        else
        {
            cursor = StepCardFlow(otBase, cursor, labelTable, slot, root, pad, previousSlot);
        }

        return DrawSavePageChrome(otBase, cursor, slot);
    }

    /// <summary>
    /// Ghidra: la branche <c>DAT_005de710 == 1</c> — <b>« Quitter »</b>. Trois etapes portees par
    /// <c>DAT_005de6f8</c> : poser la question (<c>0x2f8</c>), attendre que la boite ait fini de
    /// s'ouvrir (<c>0x3c0</c>), puis armer la sortie.
    ///
    /// <para>La sortie elle-meme n'est pas un appel : elle pose <c>DAT_005645f8</c> a <c>0xfe</c>,
    /// leve le bit 2 de <c>DAT_005de02c</c> et remet l'etat de jeu a zero — la boucle par image
    /// s'en charge a l'image suivante. Et si <c>DAT_005e909f</c> n'est pas nul, la question est
    /// simplement abandonnee.</para>
    /// </summary>
    private static void StepQuitFlow(byte* labelTable, byte* slot, byte* root, ushort* pad)
    {
        if (s_Unknown_005de6f8 == 0)
        {
            BindMenuLabels(*(byte**)(labelTable + 0x2f8), (uint)(nint)slot, root, 0);
            s_Unknown_005de6f8++;
        }
        else if (s_Unknown_005de6f8 == 1)
        {
            if ((((byte*)pad)[3] & 0x40) == 0)
            {
                if ((root[0x8c7] & 2) == 0)
                {
                    BindMenuLabels(*(byte**)(labelTable + 0x3c0), (uint)(nint)slot, root, 0);
                    s_Unknown_005de6f8++;
                }
            }
            else
            {
                CancelConfirmBox(root);
            }
        }
        else if (s_Unknown_005de6f8 == 2)
        {
            if ((((byte*)pad)[3] & 0x40) != 0)
            {
                CancelConfirmBox(root);
                return;
            }

            if ((root[0x8c7] & 4) == 0)
            {
                if (*Mem.Arena(0x005e909f) == 0)
                {
                    // Ghidra ecrit `DAT_005645f8._0_1_ = 0xfe` : un octet dans le pas de la carte
                    // du monde, que le depot porte deja en `sbyte`. 0xfe est -2 signe, la meme
                    // valeur que le pas de sortie pose par WorldMapTick.
                    s_WorldMapStep_005645f8 = -2;
                    Mem.s_WorldFlags_005de02c |= 2;
                    s_GameStateId_005de510 = 0;
                }
                else
                {
                    s_Unknown_005de710 = -1;
                }
            }
        }
    }

    /// <summary>Ghidra: <c>LAB_0052e410</c> — le desarmement de la boite de confirmation, partage
    /// par quatre sites. Il efface les deux drapeaux de <c>+0x8c7</c>, les deux octets de reponse,
    /// joue le bip d'annulation et rend la main au petit menu.</summary>
    private static void CancelConfirmBox(byte* root)
    {
        root[0x8c7] &= 0xf9;
        root[0x3b] = 0;                      // Ghidra: DAT_0056856b
        root[0x33] = 0;                      // Ghidra: DAT_00568563
        Aud.PlayUiSound39();
        s_Unknown_005de710 = -1;
    }

    /// <summary>
    /// Ghidra: la branche <c>DAT_005de70c &gt;= 0</c> — <b>le choix de l'emplacement de carte, puis
    /// la machine a etats de la carte memoire</b>. Blaze.c 77161..77636.
    ///
    /// <para>
    /// Le nombre d'emplacements depend des multitaps : <c>DAT_005de6f4</c> vaut 0 a 3 selon que
    /// <c>DAT_00604b0a</c> et <c>DAT_00604b2e</c> valent <c>-0x8000</c>, et indexe deux tables
    /// paralleles — <c>0x0056a9e0</c> pour le compte et les indices de position,
    /// <c>0x0056a9a8</c> pour l'identifiant de port a poser dans <c>DAT_00604980</c>.
    /// </para>
    /// <para>
    /// L'etat courant est <c>DAT_005de71c</c>. Les numeros positifs sont la marche normale (0 :
    /// enumerer, 1 : ouvrir et lire, 5 a 8 : les confirmations et l'ecriture) ; les negatifs sont
    /// les echecs, et <c>-5</c> est le seul qui repasse par le desarmement du port avant de rendre
    /// la main.
    /// </para>
    /// <para>
    /// <b>Le <c>case -5</c> tombe volontairement dans le <c>case -4</c></b> — le binaire n'a pas de
    /// saut entre les deux, et le desarmement du port n'est donc qu'un prologue au meme
    /// nettoyage. Transcrit par un appel explicite plutot que par un <c>goto</c>.
    /// </para>
    /// </summary>
    private static uint* StepCardFlow(
        uint* otBase, uint* cursor, byte* labelTable, byte* slot, byte* root, ushort* pad,
        int previousSlot)
    {
        // ---- le choix de l'emplacement ----------------------------------------------------
        if (s_Unknown_005de714 < 0)
        {
            if ((pad[3] & 0x80) != 0) s_CardSlotCursor_005de700--;
            if ((pad[3] & 0x20) != 0) s_CardSlotCursor_005de700++;
        }

        s_Unknown_005de6f4 = *(short*)Inp.s_JoyAxis1_00604b0a == unchecked((short)0x8000) ? 1 : 0;
        if (*(short*)Inp.s_JoyAxis2_00604b2e == unchecked((short)0x8000))
        {
            s_Unknown_005de6f4 += 2;
        }

        byte* layout = s_CardSlotLayouts_0056a9e0[s_Unknown_005de6f4];

        if (s_CardSlotCursor_005de700 < 0) s_CardSlotCursor_005de700 = *layout - 1;
        if (s_CardSlotCursor_005de700 >= *layout) s_CardSlotCursor_005de700 = 0;
        if (previousSlot != s_CardSlotCursor_005de700) Aud.PlayUiSound3A();

        if (s_Unknown_005de714 < 0)
        {
            s_Unknown_005e8edc = 0;
            s_Unknown_005de714 = s_CardSlotCursor_005de700;
            s_Unknown_00604960 = 0;
            s_Unknown_005de71c = 0;
            s_CardIconAngle_00604988 = 0;      // Ghidra: _DAT_00604988 &= 0xffff — le mot haut
            Aud.PlayUiSound38();
        }

        int member = s_Unknown_005de6f8;

        // Une image de battement apres chaque changement d'etat : DAT_005de724 fait sauter un tour
        // aux etats 5, 7 et 8, le temps que la boite de dialogue soit a l'ecran.
        if (s_Unknown_005de720 != s_Unknown_005de71c)
        {
            s_Unknown_005de720 = s_Unknown_005de71c;
            s_Unknown_005de724 = 1;
        }

        byte* character = GetMainMemPtr() + ((member + 0x78) << 0xd);

        if (s_Unknown_005de714 >= 0)
        {
            // ---- le sondage du port, entre deux etats -------------------------------------
            if (s_Unknown_005e8edc == 1)
            {
                s_Unknown_005e8ee0 = Saves.PollCardTransfer(CardPort(), false);
                if (s_Unknown_005e8ee0 < -1) s_Unknown_005de71c = -4;
            }
            else if (s_Unknown_005e8edc == 2)
            {
                while (Winsys3d.Ordinal_67(CardPort()) == 0) { }
                s_Unknown_005e8ee0 = Saves.PollCardTransfer(CardPort(), false);
            }

            switch (s_Unknown_005de71c)
            {
                case 0:
                    // Le repertoire de sauvegarde est-il seulement lisible ?
                    if (root[0x8ac] == 0)                    // Ghidra: DAT_00568ddc
                    {
                        s_Unknown_005de71c = 1;
                        s_CardSlotIndex_00604980 =
                            s_CardPortIds_0056a9a8[s_Unknown_005de6f4][s_CardSlotCursor_005de700];
                    }
                    else
                    {
                        root[0x8c7] &= 0xfd;
                        root[0x33] = 0;
                        BindMenuLabels(*(byte**)(labelTable + 0x3cc), (uint)(nint)slot, root, 0);
                    }
                    break;

                case 1:
                    StepCardOpen(labelTable, slot, root, character);
                    break;

                case 5:
                    if (s_Unknown_005de724 == 0)
                    {
                        if ((((byte*)pad)[3] & 0x40) == 0)
                        {
                            if ((root[0x8c7] & 2) == 0)
                            {
                                BindMenuLabels(
                                    *(byte**)(labelTable + 0x3c0), (uint)(nint)slot, root, 0);
                                s_Unknown_005de71c++;
                            }
                        }
                        else
                        {
                            s_Unknown_005de71c = -5;
                        }
                    }
                    else
                    {
                        s_Unknown_005de724 = 0;
                    }
                    break;

                case 6:
                    if ((((byte*)pad)[3] & 0x40) == 0)
                    {
                        if ((root[0x8c7] & 4) == 0)
                        {
                            if (*Mem.Arena(0x005e909f) == 0)
                            {
                                BindMenuLabels(
                                    *(byte**)(labelTable + 0x400), (uint)(nint)slot, root, 0);
                                s_Unknown_005de71c++;
                            }
                            else
                            {
                                s_Unknown_005de71c = -5;
                            }
                        }
                    }
                    else
                    {
                        s_Unknown_005de71c = -5;
                    }
                    break;

                case 7:
                    if (s_Unknown_005de724 == 0) BuildAndWriteSaveBlock(labelTable, slot, root, member);
                    else s_Unknown_005de724 = 0;
                    break;

                case 8:
                    if (s_Unknown_005de724 == 0)
                    {
                        if ((((byte*)pad)[3] & 0x40) == 0)
                        {
                            if ((root[0x8c7] & 2) != 0) break;
                        }
                        else
                        {
                            root[0x8c7] &= 0xf9;
                            root[0x3b] = 0;
                            root[0x33] = 0;
                        }

                        s_Unknown_005de70c = -1;
                    }
                    else
                    {
                        s_Unknown_005de724 = 0;
                    }
                    break;

                case -5:
                    Saves.ResetCardTransferStatus((s_CardSlotIndex_00604980 & 4) << 2);
                    Saves.ResetCardWriteEvents();
                    goto case -4;

                case -4:
                    root[0x3b] = 0;
                    root[0x8c7] &= 0xf9;
                    root[0x33] = 0;
                    s_Unknown_005de714 = -1;
                    Aud.PlayUiSound39();
                    break;

                case -3:
                case -2:
                    if ((((byte*)pad)[3] & 0x40) == 0)
                    {
                        if (s_Unknown_005e8ee0 == -3)
                        {
                            s_Unknown_005e8edc = 0;
                            s_Unknown_005de71c = 0;
                        }
                        else if ((root[0x8c7] & 2) == 0)
                        {
                            Saves.ResetCardTransferStatus((s_CardSlotIndex_00604980 & 4) << 2);
                            Saves.ResetCardWriteEvents();
                            s_Unknown_005de714 = -1;
                        }
                    }
                    else
                    {
                        s_Unknown_005de71c = -5;
                    }
                    break;

                case -1:
                    if ((root[0x8c7] & 2) == 0) s_Unknown_005de714 = -1;
                    break;
            }
        }

        // ---- le curseur d'emplacement, tant qu'aucun n'est retenu --------------------------
        byte* sprite = s_MenuSprite_005de5f8;

        if (s_Unknown_005de714 < 0)
        {
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x19;
            *(uint*)(sprite + 4) = 0x1000000;

            byte place = layout[1 + s_CardSlotCursor_005de700];
            *(short*)(sprite + 8) = (short)(s_CardSlotPositions_0056a9f0[place * 2] + 0x8c);
            *(short*)(sprite + 0x0a) = (short)(s_CardSlotPositions_0056a9f0[place * 2 + 1] + 0x48);

            UiWin.EmitViewportRect(otBase, &cursor);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            UiWin.EmitClipRect(otBase, &cursor);
        }

        if (s_Unknown_005de714 >= 0 && s_Unknown_005de71c > 8 && s_Unknown_005de71c < 0xc)
        {
            cursor = DrawCardIconRing(otBase, cursor);
        }

        return cursor;
    }

    /// <summary>Ghidra: <c>(DAT_00604980 &amp; 3) + (DAT_00604980 &amp; 4) * 4</c> — le numero de
    /// port que les appels de carte attendent. Les deux bits bas donnent l'emplacement dans le
    /// multitap, le bit 2 le port physique et vaut alors seize.</summary>
    private static int CardPort()
        => (s_CardSlotIndex_00604980 & 3) + (s_CardSlotIndex_00604980 & 4) * 4;

    /// <summary>
    /// Ghidra: le <c>case 1</c> de la machine a etats — <b>ouvrir le fichier et decider</b>.
    /// <c>FUN_005126c0</c> enumere le repertoire, puis selon son verdict on ouvre le fichier du
    /// personnage, on annonce un port vide, ou on compose un message d'erreur.
    ///
    /// <para><b>Le message d'erreur est monte en seize bits.</b> Trois chaines — le libelle
    /// <c>0x3f4</c>, le nom du personnage a <c>+0x100</c>, le libelle <c>0x3f8</c> — sont recopiees
    /// dans le tampon de <c>MainMem + 0x1b0000</c> <b>deux octets par caractere</b>, chaque paire
    /// prise telle quelle dans la source. C'est le meme flux d'etiquettes que le moteur de texte
    /// consomme ailleurs, pas une chaine C.</para>
    /// </summary>
    private static void StepCardOpen(byte* labelTable, byte* slot, byte* root, byte* character)
    {
        s_CardCursorSlot_00604984 = 0;

        int verdict = LoadSaveDirectory();                  // Ghidra: FUN_005126c0

        switch (verdict)
        {
            case 0:
            {
                Saves.SetCardFileName((byte*)(character + 0x1fd0));
                Saves.SetCardPathDeviceFields();

                IntPtr handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                Saves.RetryCloseCardFile(handle);

                if ((nint)handle < 0)
                {
                    BuildCardErrorMessage(labelTable, character);
                    BindMenuLabels(GetMainMemPtr() + 0x1b0000, (uint)(nint)slot, root, 0);
                    s_Unknown_005de71c = -1;
                    return;
                }

                BindMenuLabels(*(byte**)(labelTable + 0x404), (uint)(nint)slot, root, 0);
                Saves.ResetCardTransferStatus(CardPort());
                s_Unknown_005e8edc = 1;
                s_Unknown_005de71c = 7;
                break;
            }

            case -3:
                BindMenuLabels(*(byte**)(labelTable + 0x3d0), (uint)(nint)slot, root, 0);
                s_Unknown_005de71c = -1;
                break;

            case -2:
                BindMenuLabels(*(byte**)(labelTable + 0x3d4), (uint)(nint)slot, root, 0);
                Saves.ResetCardTransferStatus(CardPort());
                s_Unknown_005e8edc = 2;
                s_Unknown_005de71c = -3;
                break;

            case -1:
                BuildCardErrorMessage(labelTable, character);
                BindMenuLabels(GetMainMemPtr() + 0x1b0000, (uint)(nint)slot, root, 0);
                s_Unknown_005de71c = -1;
                break;
        }
    }

    /// <summary>Ghidra: les trois boucles de <c>CONCAT11</c> du <c>case 1</c> — le message d'echec
    /// monte dans <c>MainMem + 0x1b0000</c>, deux octets par caractere.</summary>
    private static void BuildCardErrorMessage(byte* labelTable, byte* character)
    {
        ushort* out_ = (ushort*)(GetMainMemPtr() + 0x1b0000);

        for (byte* src = *(byte**)(labelTable + 0x3f4); *src != 0; src += 2)
            *out_++ = (ushort)(src[0] | (src[1] << 8));

        for (byte* src = character + 0x100; *src != 0; src += 2)
            *out_++ = (ushort)(src[0] | (src[1] << 8));

        for (byte* src = *(byte**)(labelTable + 0x3f8); *src != 0; src += 2)
            *out_++ = (ushort)(src[0] | (src[1] << 8));

        *out_ = 0;
    }

    /// <summary>
    /// Ghidra: le <c>case 7</c> de la machine a etats — <b>la sauvegarde elle-meme</b>. Blaze.c
    /// 77354..77503, la partie la plus dense de la page.
    ///
    /// <para>
    /// Elle ne se contente pas d'ecrire : elle relit d'abord les 0x100 octets d'en-tete du fichier
    /// existant et compare sa somme de controle a celle du bloc du personnage. Trois issues — echec
    /// de lecture (<c>0x408</c>), somme differente (<c>0x40c</c>, c'est la sauvegarde d'un autre
    /// personnage), ou la suite.
    /// </para>
    /// <para>
    /// La suite relit la table d'icones de la carte, y <b>fusionne</b> les dix entrees du
    /// personnage — chacune de seize octets, ajoutee seulement si aucune ligne identique n'existe
    /// deja — colle le niveau en chiffres a la fin du titre, recopie huit banques globales et les
    /// huit reperes de carte dans le bloc, capture la palette depuis la VRAM, recalcule la somme
    /// sur 0x1ffc octets, et ecrit en trois morceaux.
    /// </para>
    /// <para>
    /// <b>Le titre se termine par un mot sentinelle.</b> La boucle remonte depuis <c>+0x44</c>
    /// jusqu'a trouver <c>0x6b82</c> — deux octets releves dans le binaire a <c>0x0056ac2c</c>,
    /// juste sous les flux d'etiquettes de <c>0x0056ac30</c> — et ecrit les chiffres apres lui. Les
    /// chiffres ne sont pas de l'ASCII : ce sont les glyphes de seize bits que le moteur de texte
    /// du jeu attend, pris dans la table de la ressource principale.
    /// </para>
    /// </summary>
    private static void BuildAndWriteSaveBlock(byte* labelTable, byte* slot, byte* root, int member)
    {
        byte* character = GetMainMemPtr() + ((member + 0x78) << 0xd);
        byte* scratch = GetMainMemPtr() + 0x1b0000;

        Saves.SetCardFileName(character + 0x1fd0);
        Saves.SetCardPathDeviceFields();
        Saves.ResetCardTransferStatus((s_CardSlotIndex_00604980 & 4) << 2);
        Saves.ResetCardWriteEvents();

        IntPtr handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
        int read = Saves.RetrySeekAndReadCardFile(handle, scratch, 0x3f00, 0x100);
        Saves.RetryCloseCardFile(handle);

        byte* message = *(byte**)(labelTable + 0x408);

        if (read >= 0)
        {
            if (*(int*)(scratch + 0xfc) != *(int*)(character + 0x1ffc))
            {
                message = *(byte**)(labelTable + 0x40c);
            }
            else
            {
                handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 1);
                read = Saves.RetrySeekAndReadCardFile(handle, scratch, 0x4000, 0x2000);
                Saves.RetryCloseCardFile(handle);

                if (read >= 0)
                {
                    MergeSaveIconTable(character, scratch);
                    AppendLevelToSaveTitle(character);
                    CopyGlobalBanksIntoSaveBlock(character, member);

                    int checksum = 0;
                    for (int i = 0; i < 0x1ffc; i++) checksum += (sbyte)character[i];
                    *(int*)(character + 0x1ffc) = checksum;

                    handle = Saves.RetryOpenCardFile(Saves.s_CardPathDescriptor_00604940, 2);
                    int w1 = Saves.RetrySeekAndWriteCardFile(handle, character, 0, 0x2000);
                    int w2 = Saves.RetrySeekAndWriteCardFile(handle, character + 0x1f00, 0x3f00, 0x100);
                    int w3 = Saves.RetrySeekAndWriteCardFile(handle, scratch, 0x4000, 0x2000);
                    Saves.RetryCloseCardFile(handle);

                    if ((w1 | w2 | w3) >= 0) message = *(byte**)(labelTable + 0x3c4);
                }
            }
        }

        BindMenuLabels(message, (uint)(nint)slot, root, 0);
        s_Unknown_005de71c++;
        s_Unknown_005e8edc = 0;
        s_Unknown_005de6fc = 0;
    }

    /// <summary>Ghidra: la fusion de la table d'icones. Dix entrees de <c>0x280</c> octets a partir
    /// de <c>+0x300</c> ; seules celles dont le mot <c>+0x12</c> porte <c>0x8000</c> sans
    /// <c>0x4000</c> entrent, et seulement si leurs seize premiers octets ne figurent pas deja dans
    /// la table de la carte. La table se termine par une ligne dont le premier octet est nul, et
    /// c'est la que la nouvelle entree se pose.</summary>
    private static void MergeSaveIconTable(byte* character, byte* table)
    {
        byte* entry = character + 0x300;

        for (int i = 0; i < 10; i++, entry += 0x280)
        {
            ushort flags = *(ushort*)(entry + 0x12);
            if ((flags & 0x4000) != 0 || (flags & 0x8000) == 0) continue;

            byte* row = table;

            if (*row != 0)
            {
                while (true)
                {
                    int matched = 0;
                    while (matched < 4 && ((uint*)entry)[matched] == ((uint*)row)[matched]) matched++;
                    if (matched == 4) break;

                    row += 0x10;
                    if (*row == 0) break;
                }

                // Deja presente : rien a ajouter.
                if (*row != 0) continue;
            }

            for (int b = 0; b < 0x10; b++) row[b] = entry[b];
        }
    }

    /// <summary>Ghidra: le niveau colle a la fin du titre du fichier. On remonte depuis
    /// <c>+0x44</c> jusqu'au mot sentinelle <c>DAT_0056ac2c</c>, puis on ecrit les chiffres
    /// significatifs — le dernier toujours, meme nul.</summary>
    private static void AppendLevelToSaveTitle(byte* character)
    {
        ushort* tail = (ushort*)(character + 0x44);
        while (*tail != s_TitleSentinel_0056ac2c) tail--;

        byte* digitGlyphs =
            *(byte**)(*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x70) + 4) + 0x20;

        uint value = *(ushort*)(character + 0x144);
        int written = 0;

        for (int remaining = 10; remaining != 0; remaining--)
        {
            uint divisor = Ui.s_DecimalDivisors_00566be8[10 - remaining];
            uint digit = value / divisor;
            value -= divisor * digit;

            if (digit != 0 || remaining == 1) written++;

            if (written != 0)
            {
                tail++;
                *tail = *(ushort*)(digitGlyphs + digit * 2);
            }
        }
    }

    /// <summary>Ghidra: l'en-tete de <c>+0x1f00</c>, les huit banques globales recopiees a
    /// <c>+0x1d00</c>..<c>+0x1ec0</c>, les huit reperes de carte a <c>+0x1f04</c>, puis la capture
    /// de palette depuis la VRAM vers <c>+0x60</c>.</summary>
    private static void CopyGlobalBanksIntoSaveBlock(byte* character, int member)
    {
        byte* header = character + 0x1f00;
        header[0] = 0;
        header[1] = s_Unknown_00566c84;
        header[2] = s_Unknown_00566c88;
        header[3] = *(byte*)Mem.Camera(0x005de044);
        LegacyPlus.Coutures.LieuSauve(header);   // Couture legacy ++ (LP-009)

        // Huit banques de 0x40 octets, dans l'ordre du binaire.
        for (int off = 0; off < 0x40; off += 4)
        {
            *(uint*)(character + 0x1d00 + off) = *(uint*)(Mem.Arena(0x005e96e0) + off);
            *(uint*)(character + 0x1d40 + off) = *(uint*)(Mem.Arena(0x005e9780) + off);
            *(uint*)(character + 0x1d80 + off) = *(uint*)(Mem.Arena(0x005e9060) + off);
            *(uint*)(character + 0x1dc0 + off) = *(uint*)(Mem.Arena(0x005e9840) + off);
            *(uint*)(character + 0x1e00 + off) = *(uint*)(Mem.Arena(0x005e9720) + off);
            *(uint*)(character + 0x1e40 + off) = *(uint*)(Mem.Arena(0x005e92c0) + off);
            *(uint*)(character + 0x1e80 + off) = *(uint*)(Mem.Arena(0x005e9980) + off);
            *(uint*)(character + 0x1ec0 + off) = *(uint*)(Mem.Arena(0x005e9360) + off);
        }

        // Les huit reperes de la page 4 : trois mots chacun, le quatrieme n'est pas sauve.
        for (int i = 0; i < 8; i++)
        {
            ushort* src = (ushort*)Mem.Arena(0x005e9320 + i * 8);
            ushort* dst = (ushort*)(character + 0x1f04 + i * 8);
            dst[0] = src[0];
            dst[1] = src[1];
            dst[2] = src[2];
        }

        byte portrait = Inp.s_SlotFlags_005e93c0[4 + member * 0x9c];
        short* rect = (short*)s_IconRect_005de610;
        rect[0] = (short)((character[0x2bd] + portrait * 4) * 0x10);
        rect[2] = 0x10;
        rect[3] = 1;
        rect[1] = (short)(*(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0x92 + portrait * 4) >> 6);

        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_684(rect, character + 0x60);
        Winsys3d.Ordinal_667(0);
    }

    /// <summary>
    /// Ghidra: le bloc <c>8 &lt; DAT_005de71c &lt; 0xc</c> — <b>l'icone de carte qui tourne</b>
    /// pendant l'ecriture. Blaze.c 77578..77633.
    ///
    /// <para>
    /// Le cadre et les deux chiffres d'abord, puis autant d'icones que le fichier a d'images
    /// (<c>DAT_00604980 &gt;&gt; 8</c>), placees sur un cercle par <c>Ordinal_450</c> /
    /// <c>Ordinal_451</c>. L'angle avance par amortissement vers le douzieme de tour suivant, et le
    /// rayon vers <c>0x1800</c> — les deux moities du meme dword de <c>0x00604988</c>.
    /// </para>
    /// <para>
    /// <b>Celle de devant est dessinee autrement</b> : decalee de huit dans les deux sens, teintee
    /// par <c>DAT_005e9814</c>, et emise par la primitive longue <c>FUN_00521f20</c> avec une
    /// echelle qui bat au rythme du compteur de fondu. Les autres passent par la courte.
    /// </para>
    /// </summary>
    private static uint* DrawCardIconRing(uint* otBase, uint* cursor)
    {
        byte* sprite = s_MenuSprite_005de5f8;

        UiWin.EmitViewportRect(otBase, &cursor);

        *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        *(ushort*)(sprite + 0x10) = 0x2e;
        *(short*)(sprite + 8) = 0xc8;
        *(short*)(sprite + 0x0a) = 0x40;
        cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);

        *(ushort*)(sprite + 0x10) = 0x2f;
        *(short*)(sprite + 8) = 0xcc;
        *(short*)(sprite + 0x0a) = 0x54;
        cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);

        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        *(byte**)sprite = SpriteAtlas(0x00566c30);
        *(short*)(sprite + 8) = 0xd0;
        *(short*)(sprite + 0x0a) = 0x48;
        *(uint*)(sprite + 4) = 0x1000000;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite,
            *(uint*)(s_CardDirectoryEntries_006049bc + 0x18 + s_CardCursorSlot_00604984 * 0x28),
            0x82, 0);

        *(short*)(sprite + 8) = 0xd0;
        *(short*)(sprite + 0x0a) = 0x5c;
        cursor = Prims.DrawDecimalNumber(
            otBase, cursor, (int*)sprite, (uint)(s_CardEntryCount_00604981), 0x82, 0);

        Mem.s_PanelSelection_005e9060[0xf8 - 0x60] = 0x0f;     // Ghidra: _DAT_005e8ff8 = 0xf
        *(ushort*)(Mem.Arena(0x005e8ffa)) = 0x7f80;

        *(ushort*)(sprite + 0x10) = 0;
        *(byte**)sprite = s_RowSpriteRecord_00563148;

        int frames = (s_CardSlotIndex_00604980 >> 8) & 0xff;

        // L'angle courant, ramene dans un tour, puis amorti vers la position de l'image visee.
        uint delta = (uint)(s_CardIconAngle_00604988
                            - (int)(0x1000 / (frames == 0 ? 1 : frames)) * s_CardCursorSlot_00604984)
                     & 0xfff;

        short step = delta < 2000
            ? (short)((int)delta >> 2)
            : (short)-(short)((int)(0x1000 - delta) >> 2);

        s_CardIconAngle_00604988 -= step;
        s_Unknown_0060498a += (short)((0x1800 - s_Unknown_0060498a) >> 2);

        UiWin.EmitViewportRect(otBase, &cursor);

        for (int i = frames; i != 0; i--)
        {
            int place = (i + s_CardCursorSlot_00604984) % frames;
            int angle = (int)(0x1000 / frames) * place - s_CardIconAngle_00604988;

            int sin = (int)Winsys3d.GetSinTableEntry(angle);
            *(short*)(sprite + 8) = (short)((sin * s_Unknown_0060498a >> 0x14) + 0xa2);

            int cos = Winsys3d.GetCosTableEntry(angle);
            s_RowSpriteRecord_00563148[0] = (byte)(place << 4);
            short y = (short)(100 - (cos * s_Unknown_0060498a >> 0x14));
            *(short*)(sprite + 0x0a) = y;

            s_RowSpriteRecord_00563148[1] =
                (byte)((((s_FadeCounter_005e9960 >> 2) % s_CardIconFrameCount_0060498c[place]) + 2) * 0x10);

            if (i == frames)
            {
                // Celle de devant : decalee, teintee, et emise par la primitive longue.
                *(short*)(sprite + 8) += 8;
                *(short*)(sprite + 0x0a) = (short)(y + 8);

                int beat = (int)Winsys3d.GetSinTableEntry((int)((s_FadeCounter_005e9960 & 7) << 9));
                *(short*)(sprite + 0x0c) = (short)((beat >> 2) + 0x1400);
                *(short*)(sprite + 0x0e) = *(short*)(sprite + 0x0c);
                *(uint*)(sprite + 4) = s_Unknown_005e9814;

                cursor = Prims.DrawSpritePrimitive(otBase, cursor, (int*)sprite, 0, (uint)place);
            }
            else
            {
                cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 3, (uint)place);
            }
        }

        cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
        UiWin.EmitClipRect(otBase, &cursor);

        return cursor;
    }

    /// <summary>
    /// Ghidra: <c>LAB_0052f568</c> — <b>la queue commune de la page 5</b> : le curseur du petit
    /// menu, les deux libelles « Sauvegarde » et « Quitter », puis la rangee des portraits du
    /// groupe avec leur nom.
    ///
    /// <para><b>« Sauvegarde » est grisee</b> (<c>0x808080</c> au lieu de <c>0x1000000</c>) tant que
    /// le bit 3 de <c>DAT_005de02c</c> manque — le meme bit qui fait refuser la validation plus
    /// haut. Les deux chaines viennent du binaire et non de la ressource : elles sont ecrites en
    /// dur a <c>0x0056ac20</c> et <c>0x00566b64</c>.</para>
    /// </summary>
    private static uint* DrawSavePageChrome(uint* otBase, uint* cursor, byte* slot)
    {
        byte* sprite = s_MenuSprite_005de5f8;
        *(uint*)(sprite + 4) = 0x1000000;

        if (s_Unknown_005de710 < 0)
        {
            *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
            *(ushort*)(sprite + 0x10) = 0x18;
            *(short*)(sprite + 8) = 0x4c;
            *(short*)(sprite + 0x0a) = (short)(s_Unknown_005de6fc * 0xc + 0x28);

            UiWin.EmitViewportRect(otBase, &cursor);
            cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            UiWin.EmitClipRect(otBase, &cursor);
        }

        *(byte**)sprite = (byte*)s_GlyphAtlas_00566c28;
        *(short*)(sprite + 8) = 0x5c;
        *(short*)(sprite + 0x0a) = 0x2c;

        if ((Mem.s_WorldFlags_005de02c & 8) == 0) *(uint*)(sprite + 4) = 0x808080;

        cursor = Prims.DrawTextRun(
            otBase, cursor, (int*)sprite, s_Sauvegarde_0056ac20, 0, 0, -1);

        *(short*)(sprite + 0x0a) += 0x0c;
        *(uint*)(sprite + 4) = 0x1000000;
        cursor = Prims.DrawTextRun(
            otBase, cursor, (int*)sprite, s_InventoryLabels_00566b58[2], 0, 0, -1);

        // ---- la rangee des membres ----------------------------------------------------------
        byte* seat = Inp.s_SlotFlags_005e93c0 + 4;              // Ghidra: &DAT_005e93c4
        byte* character = GetMainMemPtr() + 0xf0100;

        for (int index = 0; index < s_PartySize_005e9020;
             index++, seat += 0x9c, character += 0x2000)
        {
            int row = seat[4] * 0x14 + 0x4c;

            UiWin.EmitViewportRect(otBase, &cursor);

            if (index == s_Unknown_005de6f8 && s_Unknown_005de710 == 0)
            {
                if (s_Unknown_005de70c < 0)
                {
                    *(byte**)sprite = *(byte**)*(byte**)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
                    *(ushort*)(sprite + 0x10) = 0x18;
                    *(short*)(sprite + 8) = 0x15;
                    *(short*)(sprite + 0x0a) = (short)row;
                    cursor = Prims.DrawSpriteVariant(otBase, cursor, (int*)sprite, 0, 0);
                    cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
                }

                *(uint*)(sprite + 4) = s_Unknown_005e9814;
            }
            else
            {
                *(uint*)(sprite + 4) = 0x1000000;
            }

            byte* portraitAtlas =
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 4);
            *(byte**)sprite = portraitAtlas;
            portraitAtlas[5] = 0;
            *(ushort*)(sprite + 0x10) = (ushort)(*seat + 4);
            *(short*)(sprite + 8) = 0x20;
            *(short*)(sprite + 0x0a) = (short)row;

            cursor = Prims.DrawSpriteVariant(
                otBase, cursor, (int*)sprite, 0, character[0xbd]);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, (int*)sprite);
            UiWin.EmitClipRect(otBase, &cursor);

            *(short*)(sprite + 8) += 0x14;
            *(short*)(sprite + 0x0a) += 4;
            *(byte**)sprite = (byte*)s_GlyphAtlas_00566c28;
            cursor = Prims.DrawTextRunAlias(otBase, cursor, (int*)sprite, character, 0, 0, 8);
        }

        return cursor;
    }

    /// <summary>L'etiquette qu'un repere pose maintenant porterait : <c>DAT_00566c84</c> en octet
    /// haut, <c>DAT_00566c88</c> au milieu, la zone courante en octet bas. Le binaire la recalcule
    /// aux deux endroits ou elle sert.</summary>
    private static uint CurrentMarkerTag()
        => ((uint)s_Unknown_00566c84 << 16) | ((uint)s_Unknown_00566c88 << 8)
           | (uint)*Mem.Camera(0x005de058);

    // =====================================================================================
    // LES DONNEES DU MENU
    // =====================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005de5f8</c> — <b>le bloc de sprite de brouillon</b> que toute la page 0
    /// remplit avant chaque emission. Cinq dwords : l'atlas en <c>+0</c>, la couleur en <c>+4</c>,
    /// la position en deux <c>short</c> a <c>+8</c>, l'indice de tuile a <c>+0x10</c>.
    ///
    /// <para><b>Il ne recouvre pas <c>s_EntityList_005de558</c></b>, qui s'arrete a
    /// <c>0x005de5f4</c> : quatre octets d'ecart, verifies avant de l'allouer a part. Le voisin du
    /// dessus est <c>s_Unknown_005de738</c>, dans <c>Screens/DebugParty.cs</c>.</para>
    /// </summary>
    private static byte* s_MenuSpriteStorage;
    private static byte* s_MenuSprite_005de5f8
        => s_MenuSpriteStorage != null
            ? s_MenuSpriteStorage
            : (s_MenuSpriteStorage = (byte*)NativeMemory.AllocZeroed(0x14));

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005de6a8</c> — <b>rang affiche vers numero d'objet de quete</b>,
    /// trente-deux octets. La page « Objets » le remplit en dessinant sa liste, et s'en ressert
    /// immediatement pour retrouver la description de l'entree visee : sans lui,
    /// <c>DAT_005de6fc</c> (un rang dans la liste courte) ne dirait rien du bit qu'il represente.
    ///
    /// <para>Le voisin du dessous est <c>s_ItemMessageScratch_005de620</c>, qui finit a
    /// <c>0x005de6a0</c> — huit octets d'ecart, verifies ; celui du dessus est
    /// <see cref="s_MenuPage_005de6f0"/>, a <c>0x005de6f0</c>, donc quarante de marge apres la fin
    /// de cette table.</para>
    /// </summary>
    private static byte* s_KeyItemIndexStorage;
    private static byte* s_KeyItemIndex_005de6a8
        => s_KeyItemIndexStorage != null
            ? s_KeyItemIndexStorage
            : (s_KeyItemIndexStorage = (byte*)NativeMemory.AllocZeroed(0x20));

    /// <summary>Ghidra: <c>DAT_005de618</c> — le curseur de la page 4 sur la table de reperes. Il
    /// vit en memoire et non dans un registre : le binaire le relit apres chaque appel de dessin
    /// (<c>mov ebp,[5DE618h]</c>), les appelees pouvant ecrire la meme zone.</summary>
    private static short* s_MarkerCursor_005de618;

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005de6c8</c> — <b>le bloc de placement du modele de la page 4</b>, 0x20
    /// octets. Meme forme que le cadre de pile de la rose des vents : trois entiers de position,
    /// deux d'echelle, un mot de reserve, deux <c>short</c> puis l'etiquette.
    ///
    /// <para>Il commence exactement ou <see cref="s_KeyItemIndex_005de6a8"/> finit
    /// (<c>0x005de6a8 + 0x20</c>) et s'arrete a <c>0x005de6e8</c>, huit octets sous
    /// <see cref="s_MenuPage_005de6f0"/> : les deux bornes se referment sur des symboles connus.
    /// </para>
    /// </summary>
    private static byte* s_MapModelBlockStorage;
    private static byte* s_MapModelBlock_005de6c8
        => s_MapModelBlockStorage != null
            ? s_MapModelBlockStorage
            : (s_MapModelBlockStorage = (byte*)NativeMemory.AllocZeroed(0x20));

    // =====================================================================================
    // LES VERROUS ET LES DONNEES DE LA PAGE 5
    // =====================================================================================

    /// <summary>Ghidra: <c>DAT_005de700</c> — le curseur d'emplacement de carte de la page 5. Il
    /// est initialise depuis l'octet <c>+0x2bc</c> du personnage choisi, c'est-a-dire le dernier
    /// emplacement ou ce personnage a ete sauve.</summary>
    private static int s_CardSlotCursor_005de700;

    /// <summary>Ghidra: <c>DAT_005de6f4</c> — la configuration d'emplacements de carte, 0 a 3
    /// selon les deux multitaps detectes. Elle indexe les deux tables paralleles de
    /// <c>0x0056a9a8</c> et <c>0x0056a9e0</c>.</summary>
    private static int s_Unknown_005de6f4;

    /// <summary>Ghidra: <c>DAT_005de714</c> / <c>DAT_005de71c</c> / <c>DAT_005de720</c> /
    /// <c>DAT_005de724</c> — les quatre verrous de la page 5 : l'emplacement de carte retenu,
    /// l'etat de la machine a etats, sa valeur de l'image precedente, et le battement d'une image
    /// qui laisse la boite de dialogue s'afficher avant que l'etat n'agisse.</summary>
    private static int s_Unknown_005de714, s_Unknown_005de71c;
    private static int s_Unknown_005de720, s_Unknown_005de724;

    /// <summary>Ghidra: <c>_DAT_005de610</c>..<c>_DAT_005de616</c> — le rectangle VRAM de la
    /// capture de palette, quatre <c>short</c>. Il occupe le trou entre
    /// <see cref="s_MenuSprite_005de5f8"/>, qui finit a <c>0x005de60c</c>, et
    /// <see cref="s_MarkerCursor_005de618"/>.</summary>
    private static byte* s_IconRectStorage;
    private static byte* s_IconRect_005de610
        => s_IconRectStorage != null
            ? s_IconRectStorage
            : (s_IconRectStorage = (byte*)NativeMemory.AllocZeroed(8));

    /// <summary>Ghidra: <c>DAT_005e8edc</c> / <c>DAT_005e8ee0</c> — le mode de sondage du port de
    /// carte (0 aucun, 1 simple, 2 avec attente) et le dernier verdict rendu. Voisins immediats de
    /// <c>s_Unknown_005e8ed8</c> et <c>s_Unknown_005e8eda</c>, la position de la carte du
    /// monde.</summary>
    private static int s_Unknown_005e8edc, s_Unknown_005e8ee0;

    /// <summary>
    /// Ghidra: <c>&amp;PTR_DAT_0056a9a8</c> — <b>l'identifiant de port de chaque emplacement</b>,
    /// une rangee par configuration de multitap. Les quatre rangees vivent en
    /// <c>0x0056a98c</c>..<c>0x0056a9a8</c>, contigues, et la table de pointeurs les suit.
    ///
    /// <para>Deux emplacements sans multitap (les deux ports), cinq avec un seul, huit avec les
    /// deux. Le premier octet de chaque rangee est <c>0</c> : le port 1 est toujours la.</para>
    /// </summary>
    private static readonly byte[][] s_CardPortIds_0056a9a8 =
    [
        [0x00, 0x04],                                        // 0x0056a98c
        [0x00, 0x01, 0x02, 0x03, 0x04],                       // 0x0056a990
        [0x00, 0x04, 0x05, 0x06, 0x07],                       // 0x0056a998
        [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07],     // 0x0056a9a0
    ];

    /// <summary>
    /// Ghidra: <c>&amp;PTR_DAT_0056a9e0</c> — <b>le compte d'emplacements et leurs positions</b>,
    /// une rangee par configuration. Le premier octet est le compte, les suivants indexent
    /// <see cref="s_CardSlotPositions_0056a9f0"/>. Rangees en
    /// <c>0x0056a9b8</c>..<c>0x0056a9e0</c>.
    /// </summary>
    private static readonly byte*[] s_CardSlotLayouts_0056a9e0 =
    [
        AllocBytes([0x02, 0x00, 0x01]),
        AllocBytes([0x05, 0x02, 0x03, 0x04, 0x05, 0x01]),
        AllocBytes([0x05, 0x00, 0x06, 0x07, 0x08, 0x09]),
        AllocBytes([0x08, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09]),
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056a9f0</c> — <b>les dix positions d'emplacement de carte</b>, deux
    /// <c>short</c> chacune, relatives a <c>(0x8c, 0x48)</c>. Les deux premieres sont les ports
    /// sans multitap ; les huit suivantes les deux colonnes de quatre qu'un multitap ajoute.
    ///
    /// <para>La plage se referme des deux cotes : <c>0x0056a9e0</c> en dessous est la table de
    /// pointeurs ci-dessus, et <c>0x0056aa18</c> au-dessus est
    /// <see cref="s_ConfigRowOffsets_0056aa18"/>, deja relevee pour l'onglet 5. Dix entrees de
    /// quatre octets tombent exactement entre les deux.</para>
    /// </summary>
    private static readonly short[] s_CardSlotPositions_0056a9f0 =
    [
         0x0011, 0x002c,
         0x001c, 0x002c,
        -0x0011, 0x0043,
        -0x000a, 0x0049,
         0x0004, 0x0049,
         0x000b, 0x0043,
         0x0023, 0x0043,
         0x002a, 0x0049,
         0x0038, 0x0049,
         0x003f, 0x0043,
    ];

    /// <summary>Ghidra: <c>DAT_0056ac2c</c> — le mot sentinelle qui marque la fin du titre d'un
    /// fichier de sauvegarde. Quatre octets entre la chaine « Sauvegarde » et les flux
    /// d'etiquettes de <c>0x0056ac30</c>, dont seul le mot bas est lu.</summary>
    private const ushort s_TitleSentinel_0056ac2c = 0x6b82;

    /// <summary>Ghidra: <c>s_Sauvegarde_0056ac20</c> — la chaine ecrite en dur dans le binaire,
    /// juste au-dessus des six etiquettes de l'onglet 5.</summary>
    private static byte* s_SauvegardeStorage;
    private static byte* s_Sauvegarde_0056ac20
        => s_SauvegardeStorage != null
            ? s_SauvegardeStorage
            : (s_SauvegardeStorage = AllocBytes([
                0x53, 0x61, 0x75, 0x76, 0x65, 0x67, 0x61, 0x72, 0x64, 0x65, 0x00, 0x00,
            ]));

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00568530</c> et ses trois voisins — <b>les quatre descripteurs
    /// d'interface du menu</b>, contigus au pas <c>0x8c8</c> comme la rangee de
    /// <c>0x00560ff0</c>. Leurs <b>0x68 premiers octets</b> sont initialises dans le binaire, la
    /// queue est nulle (verifie a <c>0x00568598</c>, <c>0x00568E60</c>, <c>0x00569728</c>,
    /// <c>0x00569FF0</c>).
    ///
    /// <para>
    /// <b>La rangee compte quatre entrees, pas trois ni cinq</b>, et cela se deduit sans deviner :
    /// <c>FUN_0052fab0</c> indexe depuis <c>0x00568530</c> au pas <c>0x8c8</c>, le plus grand
    /// indice rencontre est 3 (soit <c>0x00569f88</c>), et la premiere donnee qui n'est pas un
    /// descripteur est <c>DAT_0056a898</c> — juste au-dela de <c>0x00568530 + 4 * 0x8c8 =
    /// 0x0056a850</c>.
    /// </para>
    /// <para>
    /// <b>Alloues bas.</b> Comme tous les descripteurs d'interface, ils portent leur propre
    /// ordering table a <c>+0xa4</c>, masquee sur 24 bits : au-dessus de <c>0x01000000</c> elle se
    /// relit sur des adresses tronquees. Paresseux plutot qu'initialiseur de champ, pour qu'un
    /// echec d'allocation basse reste rattrapable par relance.
    /// </para>
    /// </summary>
    private static byte* s_MenuDescriptorsStorage;
    private static byte* s_MenuDescriptors_00568530
        => s_MenuDescriptorsStorage != null
            ? s_MenuDescriptorsStorage
            : (s_MenuDescriptorsStorage = BuildMenuDescriptors());

    private static byte* MenuDescriptor(int index)
        => s_MenuDescriptors_00568530 + index * GameMemory.UiDescriptorStride;

    private static byte* BuildMenuDescriptors()
    {
        byte* block = AllocLowZeroed(4 * GameMemory.UiDescriptorStride);

        // Les 0x68 octets initialises de chacun, releves verbatim dans Legacy/Binaries/Blaze.exe.
        ReadOnlySpan<byte> heads =
        [
            // 0x00568530 — le descripteur racine, celui que l'epilogue dessine en dernier
            0x31, 0x00, 0x00, 0x00, 0x08, 0x00, 0xa0, 0x00, 0xe0, 0x00, 0x28, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x1a, 0x02, 0x0c, 0x00, 0x00, 0x00, 0x08, 0x08, 0x0f, 0x01, 0x50, 0x40, 0xff, 0x02,
            0x10, 0x0c, 0x07, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0xd4, 0x28, 0x0f, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x0c, 0x0f, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x0f, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,

            // 0x00568df8 — celui dont l'epilogue deplace la boite selon la page
            0x31, 0x00, 0x00, 0x00, 0x08, 0x00, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x1a, 0x08, 0x04, 0x00, 0x00, 0x00, 0x08, 0x08, 0x0f, 0x01, 0x50, 0x40, 0xff, 0x02,
            0x10, 0x20, 0x07, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x0f, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,

            // 0x005696c0 — celui dont l'epilogue leve le drapeau de +0x23 sur deux onglets
            0x31, 0x00, 0x00, 0x00, 0x50, 0x00, 0x08, 0x00, 0x98, 0x00, 0x98, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x08, 0x0f, 0x01, 0x50, 0x40, 0xff, 0x02,
            0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x0f, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,

            // 0x00569f88 — la fiche de personnage, celui que la page 0 remplit quatre fois
            0x31, 0x00, 0x00, 0x00, 0x08, 0x00, 0x48, 0x00, 0x48, 0x00, 0x58, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x09, 0x03, 0x09, 0x00, 0x00, 0x00, 0x08, 0x08, 0x0f, 0x01, 0x50, 0x40, 0xff, 0x02,
            0x0c, 0x10, 0x07, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x0f, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01,
        ];

        for (int i = 0; i < 4; i++)
        {
            heads.Slice(i * 0x68, 0x68)
                 .CopyTo(new Span<byte>(block + i * GameMemory.UiDescriptorStride, 0x68));
        }

        return block;
    }

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056a898</c> — <b>les onglets de chaque classe</b>. Huit lignes de six
    /// octets, indexees par <c>caractere[0x150] &amp; 7</c> : le premier octet est le nombre
    /// d'onglets, les suivants leurs identifiants. Relevee verbatim dans le binaire.
    ///
    /// <para>Toutes les lignes commencent par l'onglet 0, ce qui fait que la fiche s'ouvre toujours
    /// sur le pave de statistiques. Trois classes n'ont que quatre onglets et laissent un
    /// <c>0xff</c> de bourrage.</para>
    /// </summary>
    private static readonly byte[] s_ClassTabs_0056a898 =
    [
        0x04, 0x00, 0x01, 0x02, 0x05, 0xff,
        0x05, 0x00, 0x01, 0x02, 0x03, 0x05,
        0x04, 0x00, 0x01, 0x02, 0x05, 0xff,
        0x05, 0x00, 0x01, 0x02, 0x03, 0x05,
        0x05, 0x00, 0x01, 0x02, 0x04, 0x05,
        0x05, 0x00, 0x01, 0x02, 0x03, 0x05,
        0x04, 0x00, 0x01, 0x02, 0x05, 0xff,
        0x05, 0x00, 0x01, 0x02, 0x03, 0x05,
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0056a8c8</c> — l'identifiant d'onglet vers l'indice de
    /// pictogramme dans l'atlas de la barre.</summary>
    private static readonly byte[] s_TabIcons_0056a8c8 = [0x00, 0x01, 0x46, 0x02, 0x03, 0x47, 0x00, 0x00];

    /// <summary>Ghidra: <c>&amp;DAT_0056a8d0</c> — l'ordre d'affichage des huit rangs de
    /// caracteristiques secondaires. Le pave de statistiques le parcourt deux fois : les rangs 0 a 3
    /// pour les resistances, 4 a 7 pour les quatre rangs de mise au point.</summary>
    private static readonly byte[] s_StatOrder_0056a8d0 = [0x00, 0x01, 0x04, 0x05, 0x02, 0x03, 0x06, 0x07];

    /// <summary>Ghidra: <c>&amp;DAT_0056a8e0</c> — les deux decorations de la tete de page, ancrees
    /// sur le descripteur 3.</summary>
    private static readonly uint[] s_MenuDecorHead_0056a8e0 = [0x0c0c0603, 0x280c0703, 0x00000000];

    /// <summary>Ghidra: <c>&amp;DAT_0056a8f0</c> — les vingt decorations du pave de statistiques,
    /// toutes ancrees sur le descripteur 2. Relevees verbatim ; chaque dword est
    /// <c>descripteur | tuile &lt;&lt; 8 | x &lt;&lt; 16 | y &lt;&lt; 24</c>.</summary>
    private static readonly uint[] s_MenuDecorStats_0056a8f0 =
    [
        0x18100802, 0x24200902, 0x24540502, 0x2c200a02,
        0x2c540502, 0x35480b02, 0x44180c02, 0x445c0d02,
        0x4c180e02, 0x4c5c0f02, 0x54181002, 0x545c1102,
        0x5c181202, 0x68181302, 0x685c1402, 0x70181502,
        0x705c1602, 0x7c141702, 0x8c101802, 0x905c1902,
        0x00000000,
    ];

    // =====================================================================================
    // LES DONNEES DE L'ONGLET 1
    // =====================================================================================

    /// <summary>
    /// Ghidra: <c>&amp;DAT_005de0c8</c> — le tampon ou <see cref="BuildItemDisplayName_0051BD50"/>
    /// monte le nom affiche. Quarante-et-un octets : quarante de nom au plus, plus le terminateur.
    ///
    /// <para>
    /// <b>TODO(REVERSE) — recouvrement latent, releve le 2026-08-25, NON corrige.</b> Dans le
    /// binaire, ce tampon finit a <c>0x005de109</c> et mord donc de neuf octets sur
    /// <c>GameMemory.s_ItemMenuArena_005de100</c>. Ici il s'alloue a part, ce qui diverge de
    /// l'original <b>uniquement si un nom atteint 63 caracteres</b> — ce qu'aucun nom du jeu ne
    /// fait. Le corriger demande de trancher l'etendue de l'arene, deja douteuse (voir le meme
    /// TODO dans <c>Engine/UiWindow.cs</c>) : c'est de la reconstruction, pas du refactor.
    /// </para>
    /// </summary>
    private static byte* s_ItemNameScratchStorage;
    private static byte* s_ItemNameScratch_005de0c8
        => s_ItemNameScratchStorage != null
            ? s_ItemNameScratchStorage
            : (s_ItemNameScratchStorage = (byte*)NativeMemory.AllocZeroed(0x41));

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056aae0</c>..<c>0x0056abe8</c> — <b>trente-trois cellules de sprite de
    /// huit octets</b>, relevees d'un bloc. L'onglet 1 en prend deux bases : <c>+0x90</c>
    /// (<c>DAT_0056ab70</c>, le marqueur « equipe ») et <c>+0xa0</c> (<c>DAT_0056ab80</c>, les huit
    /// pictogrammes d'element).
    ///
    /// <para>
    /// <b>Les deux bornes sont mesurees, pas choisies</b> — c'est la lecon que le carrousel de
    /// selection a deja fait payer : un descripteur ne se releve pas seul, parce que
    /// <c>DrawSpriteVariant</c> indexe <b>en avant</b> depuis sa base. En dessous de
    /// <c>0x0056aae0</c> commence une table de mots de seize bits, et a <c>0x0056abe8</c> commence
    /// une chaine cp1252 (« Desactiv… »). Le bloc est donc exactement celui-la.
    /// </para>
    /// </summary>
    private static readonly byte* s_ItemSprites_0056aae0 = AllocBytes([
        0x00, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0x10, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0x20, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0x30, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0b,
        0x40, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0b,
        0x50, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x08,
        0x60, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x08,
        0x70, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0a,
        0x80, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0f,
        0x80, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0f,
        0x00, 0x90, 0x18, 0x18, 0x00, 0x00, 0x11, 0x00,
        0x90, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xa0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xb0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xc0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xd0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xe0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xf0, 0x50, 0x10, 0x10, 0x00, 0x00, 0x11, 0x07,
        0xf8, 0xf8, 0x08, 0x08, 0x00, 0x00, 0x11, 0x07,
        0xe0, 0x80, 0x10, 0x10, 0x00, 0x00, 0x11, 0x0a,
        0x80, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0x90, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xa0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xb0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xc0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xd0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xe0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0xf0, 0xe0, 0x10, 0x08, 0x00, 0x00, 0x30, 0x00,
        0x80, 0xe8, 0x28, 0x08, 0x00, 0x00, 0x30, 0x02,
        0x80, 0xf8, 0x28, 0x08, 0x00, 0x00, 0x30, 0x02,
        0x80, 0xf0, 0x28, 0x08, 0x00, 0x00, 0x30, 0x02,
        0xa8, 0xe8, 0x38, 0x08, 0x00, 0x00, 0x30, 0x02,
        0xa8, 0xf0, 0x38, 0x08, 0x00, 0x00, 0x30, 0x02,
    ]);

    /// <summary>Ghidra: <c>&amp;DAT_0056a8d8</c> — l'ordre des huit rangs de la fiche d'objet. Il
    /// occupe le trou de huit octets entre <see cref="s_StatOrder_0056a8d0"/> et
    /// <see cref="s_MenuDecorHead_0056a8e0"/>, ce qui referme la plage.</summary>
    private static readonly byte[] s_ItemStatOrder_0056a8d8 = [0x02, 0x01, 0x00, 0x03, 0x04, 0x05, 0x06, 0x07];

    // =====================================================================================
    // LES DONNEES DES ONGLETS 3 ET 4
    // =====================================================================================

    /// <summary>Ghidra: <c>&amp;DAT_005de6a0</c> — <b>la tranche de sorts affichee</b>, huit
    /// octets. Un sort appris y entre par son numero, un sort inconnu par son oppose ; tout le
    /// corps de l'onglet 3 teste <c>&lt; 0x80</c> pour les distinguer.
    ///
    /// <para>Les huit octets sont bornes par le voisin du dessus,
    /// <see cref="s_KeyItemIndex_005de6a8"/>, et la plus grande tranche des trois ecoles compte six
    /// entrees : la place suffit et elle est mesuree, pas choisie.</para></summary>
    private static byte* s_SpellListStorage;
    private static byte* s_SpellList_005de6a0
        => s_SpellListStorage != null
            ? s_SpellListStorage
            : (s_SpellListStorage = (byte*)NativeMemory.AllocZeroed(8));

    /// <summary>
    /// Ghidra: <c>&amp;PTR_DAT_00564640</c> — <b>le decoupage en pages du repertoire de chaque
    /// ecole</b>. Chaque rangee donne six couples <c>(premier, compte)</c>, un par page.
    ///
    /// <para>Trois rangees en <c>0x00564610</c>, <c>0x00564620</c> et <c>0x00564630</c>, contigues
    /// et de seize octets chacune ; la table de pointeurs qui les designe porte une <b>quatrieme
    /// entree nulle</b>, ce qui borne le nombre d'ecoles a trois sans avoir a le deviner.</para>
    /// </summary>
    private static readonly byte*[] s_SpellPages_00564640 =
    [
        AllocBytes([0x00, 0x05, 0x05, 0x05, 0x0a, 0x05, 0x0f, 0x05, 0x14, 0x06, 0x1a, 0x03,
                    0x00, 0x00, 0x00, 0x00]),
        AllocBytes([0x00, 0x05, 0x05, 0x06, 0x0b, 0x05, 0x10, 0x03, 0x13, 0x03, 0x16, 0x02,
                    0x00, 0x00, 0x00, 0x00]),
        AllocBytes([0x00, 0x03, 0x03, 0x04, 0x07, 0x02, 0x09, 0x03, 0x0c, 0x04, 0x10, 0x04,
                    0x00, 0x00, 0x00, 0x00]),
    ];

    /// <summary>Ghidra: <c>&amp;DAT_00564608</c> — le rang du premier libelle de chaque ecole dans
    /// la table de ressources. Quatre octets ; le quatrieme borne le troisieme.</summary>
    private static readonly byte[] s_SpellLabelBase_00564608 = [0x00, 0x1d, 0x35, 0x49];

    /// <summary>Ghidra: <c>&amp;DAT_0056a978</c> — les quatre decorations de l'onglet 3, toutes
    /// ancrees sur le descripteur 2. La plage se referme sur <c>0x0056a98c</c>, la table de ports
    /// de carte memoire de la page 5.</summary>
    private static readonly uint[] s_MenuDecorSpells_0056a978 =
    [
        0x1c2c2402, 0x1c680502, 0x50342502, 0x74442602, 0x00000000,
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0056a948</c> — les dix decorations de l'onglet 1, toutes ancrees
    /// sur le descripteur 2.</summary>
    private static readonly uint[] s_MenuDecorItems_0056a948 =
    [
        0x60101302, 0x604c1402, 0x78101c02, 0x78341d02,
        0x78581e02, 0x787c1f02, 0x82102002, 0x82342102,
        0x82582202, 0x827c2302, 0x00000000,
    ];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_00566b80</c> — <b>ce qu'un objet non identifie affiche</b>. Douze octets
    /// relevés verbatim : <c>"???"</c> pour chacun des dix rangs de valeur, puis <c>"????"</c> a
    /// <c>+4</c> pour le nom, avec son terminateur a <c>+8</c>.
    ///
    /// <para>Les deux chaines se lisent a deux endroits du meme bloc — <c>DAT_00566b84</c> et
    /// <c>DAT_00566b88</c> dans la pseudo-C — donc un seul symbole, pas trois.</para>
    /// </summary>
    private static readonly byte* s_UnknownItemText_00566b80 = AllocBytes([
        0x3f, 0x3f, 0x3f, 0x00,   // "???"
        0x3f, 0x3f, 0x3f, 0x3f,   // "????"
        0x00, 0x00, 0x00, 0x00,
    ]);

    // =====================================================================================
    // LES DONNEES DE L'ONGLET 5
    //
    // Elles occupent 0x0056aa18..0x0056aa9f, juste sous la table des reactions par classe
    // (0x0056aaa0, dans WorldMapTick.cs) — la plage se referme donc par le haut. Par le bas,
    // 0x0056aa00..0x0056aa17 est nul dans le binaire. Aucune des trois n'est indexee au-dela
    // de sa propre fin : trois octets pour la premiere, vingt-quatre entrees de quatre pour la
    // deuxieme, et la troisieme n'est atteinte que par des indices 0..3 pris dans la deuxieme.
    // =====================================================================================

    /// <summary>Ghidra: <c>&amp;DAT_0056aa18</c> — le decalage horizontal des trois cadres de
    /// rangee, ajoute a <c>0x60</c>. Quatre octets releves, trois lus.</summary>
    private static readonly byte[] s_ConfigRowOffsets_0056aa18 = [0x04, 0x00, 0x08, 0x04];

    /// <summary>
    /// Ghidra: <c>&amp;DAT_0056aa38</c> — <b>les vingt-quatre permutations de quatre boutons</b>,
    /// quatre octets chacune. C'est la table du <c>case 3</c> inatteignable : chaque octet indexe
    /// <see cref="s_ButtonMasks_0056aa98"/>. Les vingt-quatre lignes sont exactement les vingt-quatre
    /// arrangements de {0,1,2,3}, ce qui confirme la borne <c>0x17</c> du code sans avoir a la
    /// deviner.
    /// </summary>
    private static readonly byte[] s_ButtonOrder_0056aa38 =
    [
        0x03, 0x02, 0x01, 0x00,   0x03, 0x02, 0x00, 0x01,
        0x03, 0x01, 0x02, 0x00,   0x03, 0x01, 0x00, 0x02,
        0x03, 0x00, 0x02, 0x01,   0x03, 0x00, 0x01, 0x02,
        0x02, 0x03, 0x01, 0x00,   0x02, 0x03, 0x00, 0x01,
        0x02, 0x01, 0x03, 0x00,   0x02, 0x01, 0x00, 0x03,
        0x02, 0x00, 0x03, 0x01,   0x02, 0x00, 0x01, 0x03,
        0x01, 0x03, 0x02, 0x00,   0x01, 0x03, 0x00, 0x02,
        0x01, 0x02, 0x03, 0x00,   0x01, 0x02, 0x00, 0x03,
        0x01, 0x00, 0x03, 0x02,   0x01, 0x00, 0x02, 0x03,
        0x00, 0x03, 0x02, 0x01,   0x00, 0x03, 0x01, 0x02,
        0x00, 0x02, 0x03, 0x01,   0x00, 0x02, 0x01, 0x03,
        0x00, 0x01, 0x03, 0x02,   0x00, 0x01, 0x02, 0x03,
    ];

    /// <summary>Ghidra: <c>&amp;DAT_0056aa98</c> — les quatre masques de bouton d'action, quatre
    /// mots de seize bits. Ce sont les bits que le <c>case 3</c> range dans le personnage.</summary>
    private static readonly ushort[] s_ButtonMasks_0056aa98 = [0x1000, 0x2000, 0x4000, 0x8000];

    /// <summary>
    /// Ghidra: <c>0x0056abe8</c>..<c>0x0056ac20</c> — <b>les six etiquettes de l'onglet 5</b>,
    /// cp1252, relevees d'un bloc parce que les deux tables de pointeurs ci-dessous y pointent
    /// toutes les deux. Dans l'ordre : « Désactivé », « &#160;&#160;&#160;Activé », « Type D »,
    /// « Type C », « Type B », « Type A ».
    ///
    /// <para>La borne basse est <see cref="s_ItemSprites_0056aae0"/>, qui s'arrete precisement la ;
    /// la borne haute est <c>0x0056ac20</c>, ou commence « Sauvegar… ».</para>
    /// </summary>
    private static byte* s_ConfigStringsStorage;
    private static byte* s_ConfigStrings_0056abe8
        => s_ConfigStringsStorage != null
            ? s_ConfigStringsStorage
            : (s_ConfigStringsStorage = AllocBytes([
                0x44, 0xe9, 0x73, 0x61, 0x63, 0x74, 0x69, 0x76, 0xe9, 0x00, 0x00, 0x00,  // "Désactivé"
                0x20, 0x20, 0x20, 0x41, 0x63, 0x74, 0x69, 0x76, 0xe9, 0x00, 0x00, 0x00,  // "   Activé"
                0x54, 0x79, 0x70, 0x65, 0x20, 0x44, 0x00, 0x00,                          // "Type D"
                0x54, 0x79, 0x70, 0x65, 0x20, 0x43, 0x00, 0x00,                          // "Type C"
                0x54, 0x79, 0x70, 0x65, 0x20, 0x42, 0x00, 0x00,                          // "Type B"
                0x54, 0x79, 0x70, 0x65, 0x20, 0x41, 0x00, 0x00,                          // "Type A"
            ]));

    /// <summary>Ghidra: <c>&amp;PTR_s_Type_A_0056aa20</c> — les quatre noms de type de manette,
    /// dans l'ordre A, B, C, D. Le quatrieme n'est jamais indexe : le code borne le choix a 2.
    /// </summary>
    private static byte** s_ControllerTypeNamesStorage;
    private static byte** s_ControllerTypeNames_0056aa20
        => s_ControllerTypeNamesStorage != null
            ? s_ControllerTypeNamesStorage
            : (s_ControllerTypeNamesStorage = BuildConfigLabelTable(0x30, 0x28, 0x20, 0x18));

    /// <summary>Ghidra: <c>&amp;PTR_DAT_0056aa30</c> — le couple d'etiquettes des deux bascules :
    /// l'indice 0 est « &#160;&#160;&#160;Activé », l'indice 1 « Désactivé ».</summary>
    private static byte** s_ToggleNamesStorage;
    private static byte** s_ToggleNames_0056aa30
        => s_ToggleNamesStorage != null
            ? s_ToggleNamesStorage
            : (s_ToggleNamesStorage = BuildConfigLabelTable(0x0c, 0x00));

    /// <summary>Une table de pointeurs du binaire, refaite sur les etiquettes reconstruites : les
    /// adresses de 1998 ne survivent pas, les decalages dans le bloc si.</summary>
    private static byte** BuildConfigLabelTable(params int[] offsets)
    {
        byte* block = s_ConfigStrings_0056abe8;
        byte** table = (byte**)NativeMemory.Alloc((nuint)offsets.Length * (nuint)sizeof(byte*));
        for (int i = 0; i < offsets.Length; i++) table[i] = block + offsets[i];
        return table;
    }
}
