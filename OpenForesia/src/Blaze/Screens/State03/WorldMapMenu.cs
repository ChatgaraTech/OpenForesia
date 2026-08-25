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
// ETAT : le prologue, l'epilogue partage, la page 0 (la fiche de personnage) et ses onglets 0 (le
// pave de statistiques), 1 (l'inventaire) et 2 (l'equipement) sont transcrits — l'ecran s'ouvre,
// se dessine, change d'onglet, choisit un objet, l'equipe ou le retire, et se referme. Les cinq
// autres pages et les trois autres onglets levent en se nommant, avec leur plage de lignes. Voir
// la carte au-dessus de RunPartyMenu_0052A850.
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
    /// <item><term>1</term><description>76574..76672 — <b>98 l.</b> sautee quand <c>DAT_005e9840</c>
    /// est nul</description></item>
    /// <item><term>2</term><description>76672..76697 — <b>25 l.</b> sautee quand le bit <c>0x10</c>
    /// de <c>DAT_005e9001</c> est absent</description></item>
    /// <item><term>3</term><description>76697..76833 — <b>136 l.</b></description></item>
    /// <item><term>4</term><description>76833..77060 — <b>227 l.</b></description></item>
    /// <item><term>5</term><description>77060..77698 — <b>638 l.</b> la plus grosse apres la 0 ; elle
    /// appelle les quatre corps de carte memoire <c>FUN_00504xxx</c>, donc c'est la sauvegarde</description></item>
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

                // Le meme recadrage que le cas 1 du repartiteur : la position du meneur reduite au
                // quart (`sar 0x11`), arrondie au multiple de quatre, bornee a la fenetre de carte.
                // Le masque porte sur les seize bits (`and al,0FCh` laisse AH intact).
                byte* leader = Inp.s_SlotFlags_005e93c0 + (uint)Mem.s_Unknown_005de538 * 0x9c;
                s_Unknown_005e8eda = (short)((*(int*)(leader + 0x24) >> 0x11) & 0xfffc);
                s_Unknown_005e8ed8 = (short)((*(int*)(leader + 0x1c) >> 0x11) & 0xfffc);

                if (s_Unknown_005e8eda > 0xa0) s_Unknown_005e8eda = 0xa0;
                if (s_Unknown_005e8eda < -0xa0) s_Unknown_005e8eda = unchecked((short)0xff60);
                if (s_Unknown_005e8ed8 < -0x90) s_Unknown_005e8ed8 = unchecked((short)0xff70);
                if (s_Unknown_005e8ed8 > 0x90) s_Unknown_005e8ed8 = 0x90;

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
                throw new NotImplementedException(
                    "FUN_0052a850, page 1 : non transcrite. 98 lignes dans Blaze.c, 76574..76672. " +
                    "Cette page est sautee tant que DAT_005e9840 est nul.");

            case 2:
                throw new NotImplementedException(
                    "FUN_0052a850, page 2 : non transcrite. 25 lignes dans Blaze.c, 76672..76697. " +
                    "Cette page est sautee tant que DAT_005e9001 ne porte pas le bit 0x10.");

            case 3:
                throw new NotImplementedException(
                    "FUN_0052a850, page 3 : non transcrite. 136 lignes dans Blaze.c, 76697..76833.");

            case 4:
                throw new NotImplementedException(
                    "FUN_0052a850, page 4 : non transcrite. 227 lignes dans Blaze.c, 76833..77060.");

            case 5:
                throw new NotImplementedException(
                    "FUN_0052a850, page 5 (la sauvegarde) : non transcrite. 638 lignes dans " +
                    "Blaze.c, 77060..77698. Elle appelle les quatre corps de carte memoire " +
                    "FUN_00504b40 / FUN_00504b70 / FUN_00504bc0 / FUN_00504c20, deja transcrits. " +
                    "Appelees manquantes : FUN_0052fba0 (79 l.), FUN_0052fdc0 (134 l.).");

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

        switch (tab)
        {
            case 0:
                return DrawStatsTab(otBase, cursor, slot, character);

            case 1:
                return DrawInventoryTab(otBase, cursor, slot, memberSlot, character, pad);

            case 2:
                return DrawEquipmentTab(otBase, cursor, slot, memberSlot, character, pad);

            case 3:
                throw new NotImplementedException(
                    "FUN_0052a850, page 0, onglet 3 : non transcrit. 217 lignes dans Blaze.c, " +
                    "76124..76340.");

            case 4:
                throw new NotImplementedException(
                    "FUN_0052a850, page 0, onglet 4 : non transcrit. 105 lignes dans Blaze.c, " +
                    "76341..76445.");

            case 5:
                throw new NotImplementedException(
                    "FUN_0052a850, page 0, onglet 5 : non transcrit. 126 lignes dans Blaze.c, " +
                    "76446..76571.");

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

        // ---- les huit pictogrammes d'element -------------------------------------------------------
        // Le bit du mot +0x12 decide de la teinte : 9 quand il est absent, 0 quand il est la
        // (`-(bit != 0) & 0xfffffff9` puis `+ 9`).
        *(short*)(s + 0x10) = 0;
        *(short*)(s + 8) = 0x60;
        *(short*)(s + 0x0a) = 0x98;
        *(byte**)s = s_ItemSprites_0056aae0 + 0xa0;      // Ghidra: &DAT_0056ab80

        UiWin.EmitViewportRect(otBase, &cursor);

        for (int i = 0; i < 8; i++)
        {
            uint tint = (*(ushort*)(item + 0x12) & (1u << i)) != 0 ? 0u : 9u;
            cursor = Prims.DrawSpriteVariant(otBase, cursor, sprite, 0, tint);
            cursor = Prims.DrawFlatPrimitive(otBase, cursor, sprite);
            *(short*)(s + 8) += 0x10;
            *(short*)(s + 0x10) += 1;
        }

        UiWin.EmitClipRect(otBase, &cursor);
        return cursor;
    }

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
}
