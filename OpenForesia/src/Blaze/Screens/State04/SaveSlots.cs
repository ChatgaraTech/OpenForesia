// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans ARCHITECTURE.md, a la racine du depot. L etat de la
// reconstruction et les questions ouvertes sont dans REVERSE_STATUS.md, a la racine egalement.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>
    /// Ghidra: FUN_0050a910 @ 0x0050a910 — <c>PTR_LAB_00560708[7]</c>, l'écran de sauvegarde : neuf
    /// cases de statut fixe (<see cref="s_SelectSlots_00573518"/>+1, réutilisé comme dans
    /// <c>CharacterSelectSubState3</c>) plus, si la phase est en 10..13, un carrousel de cartes
    /// dont chaque icône passe par <see cref="EmitCardGlyphTile"/> (Ghidra: <c>FUN_0051be90</c>,
    /// résolue et transcrite le 2026-08-16 — le rendu de cet écran est complet). Le blocage restant
    /// est dans la logique de phase de <see cref="StepSubState7Phase"/>, pas ici : la sous-couche
    /// carte mémoire (§ « La sous-couche carte mémoire » plus haut dans ce fichier).
    /// </summary>
    private static uint* CharacterSelectSubState7(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = s_SelectScratch_00573338;
        int* cardTransferPhase = (int*)(scratch + 4);   // DAT_0057333c — dans le bloc scratch

        int previousCursor = s_CardCursor_00578e18;

        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;

            byte* box = s_SelectSlots_00573518;
            *(short*)(box + 0x04) = 0xa0;
            *(short*)(box + 0x06) = 0x18;
            *(short*)(box + 0x08) = 0x68;
            *(short*)(box + 0x0a) = 0x88;
            box[0x23] = 0;

            byte* panel = s_SelectSlots_00573518 + CharacterSelectSlotStride;   // DAT_00573de0
            *(short*)(panel + 0x04) = 0x28;
            *(short*)(panel + 0x06) = 0x18;
            *(short*)(panel + 0x08) = 0x78;
            *(short*)(panel + 0x0a) = 0x88;
            panel[0x14] = 3;
            panel[0x12] = 0x1a;
            panel[0x13] = 6;
            panel[0x22] = 7;
            panel[0x23] = 1;

            s_SelectFlags_00562a48[2] = 0;

            // Ghidra: `_DAT_00578df0 = 0xffffffff;` (Legacy/Raw/Blaze/FUN_0050a910.c:59).
            // **Ligne manquante jusqu'au 2026-08-18, et c'est elle qui rendait le choix final
            // invisible.** `state[2]` est l'index de libellé que la boucle d'image relie au
            // descripteur de statut à chaque image, tant qu'il est positif :
            //
            //     if (-1 < DAT_00578df0)
            //         FUN_00544350(directory[0x70][DAT_00578df0 * 4], 0, &DAT_00560728, 0);
            //
            // Le sous-état 6 entre ici avec `state[2] = 0x6e`, soit LabelAt(0x1b8),
            // « Enregistrement achevé. » — donc sans cette remise à -1, la boucle d'image
            // réécrivait ce libellé **par-dessus** tout ce que la machine à phases venait de lier,
            // à chaque image. Le choix à deux lignes de la phase 0x11 était bien lié, puis
            // aussitôt écrasé, et la phase 0x12 trouvait son drapeau de dialogue à zéro : elle
            // partait sur « Définir de nouveaux personnages » sans que rien ne s'affiche.
            s_SelectState_00578de8[2] = -1;

            s_CardPhase_00578e0c = 0;   // Ghidra: `_DAT_00578e0c = 0`, pas -1
            Mem.s_Unknown_00562b68 = -1;
            s_CardPrevPhase_00578e10 = 0;
            s_CardPhaseChanged_00578e14 = 0;
        }

        if (Mem.s_Unknown_00562b68 < 0)
        {
            if ((pad[6] & 0x80) != 0) s_CardCursor_00578e18--;
            if ((pad[6] & 0x20) != 0) s_CardCursor_00578e18++;
            BindMenuLabels(s_EmptyLabel_00572b40, 0, Mem.s_StatusDescriptor_00560728, 0);
        }

        int port1Missing = *(short*)s_JoyAxis1_00604b0a == -0x8000 ? 1 : 0;
        s_CardPort_00578e20 = *(short*)s_JoyAxis2_00604b2e == -0x8000 ? port1Missing + 2 : port1Missing;

        byte[] directory = s_CardDirectorySize_00562b08[s_CardPort_00578e20];
        if (s_CardCursor_00578e18 < 0) s_CardCursor_00578e18 = directory[0] - 1;
        if (directory[0] <= s_CardCursor_00578e18) s_CardCursor_00578e18 = 0;

        if (previousCursor != s_CardCursor_00578e18) PlayUiSound3A();

        if (Mem.s_Unknown_00562b68 < 0)
        {
            *cardTransferPhase = 0;
            s_CardLoadResult_00578de0 = 0;
            s_CardCursorSlot_00604984 = 0;
            s_CardPhase_00578e0c = 0;
            Mem.s_Unknown_00562b68 = s_CardCursor_00578e18;
            PlayUiSound38();
        }

        if (s_CardPrevPhase_00578e10 != s_CardPhase_00578e0c)
        {
            s_CardPrevPhase_00578e10 = s_CardPhase_00578e0c;
            s_CardPhaseChanged_00578e14 = 1;
        }

        if (-1 < Mem.s_Unknown_00562b68 && s_SelectFlags_00562a48[1] == 0)
        {
            StepSubState7Phase(pad, cardTransferPhase);
        }

        return RenderSaveScreen(otBase, primCursor);
    }

    // Ghidra: DAT_00604984 — l'index de carte en surbrillance dans la sous-boîte « quel port ».
    // **Correction 2026-08-16** : le commentaire disait « l'octet de bas de DAT_00604980 » ; c'est
    // faux, ce sont deux adresses distinctes (DAT_00604984 = DAT_00604980 + 4), confirmé en lisant
    // Legacy/Raw/Blaze/FUN_0050a910.c — voir s_CardSlotIndex_00604980 pour l'adresse que le case 1
    // relit réellement.
    private static byte s_CardCursorSlot_00604984;

    // Ghidra: DAT_006049bc — pointeur vers la table des répertoires de sauvegarde. **Corrigé
    // 2026-08-16** : le rédacteur réel est FUN_005126c0 (LoadSaveDirectory ci-dessous), pas
    // FUN_00504820 (qui est PollCardTransfer, sans rapport) comme le disait ce commentaire — trouvé
    // en cherchant le pas 0x28 déjà utilisé par le lecteur (voir RenderSaveScreen). Alloué par
    // LoadSaveDirectory elle-même (0x20 entrées maximum, l'original pointe sur un DAT_005d9e50 fixe
    // que ce port ne peut pas reproduire à la même adresse).
    private static byte* s_CardDirectoryEntries_006049bc;

    // **Corrigé 2026-08-16** : DAT_005e909a..e sont les octets +0x3a..+0x3e du même tableau
    // DAT_005e9060 que confirmGate (+0x3f) — voir le commentaire sur s_Unknown_005e909f plus bas
    // pour le détail de la découverte. Cinq `byte` séparés ici les rendait invisibles au widget de
    // sélection réel.
    private static ref byte s_Unknown_005e909a => ref Mem.s_PanelSelection_005e9060[0x3a];
    private static ref byte s_Unknown_005e909b => ref Mem.s_PanelSelection_005e9060[0x3b];
    private static ref byte s_Unknown_005e909c => ref Mem.s_PanelSelection_005e9060[0x3c];
    private static ref byte s_Unknown_005e909d => ref Mem.s_PanelSelection_005e9060[0x3d];
    private static ref byte s_Unknown_005e909e => ref Mem.s_PanelSelection_005e9060[0x3e];

    // Ghidra: iRam00560fd4 — jamais posé par du code reconstruit, lit 0. La branche `case 0` qui le
    // teste reste donc morte, comme les portes de dialogue.
    private static int s_Unknown_00560fd4;

    // Ghidra: DAT_00604980 — l'octet sélectionnant l'emplacement de carte (posé par le case 0,
    // relu par le case 1 et le sondage de transfert). **Distinct de**
    // <see cref="s_CardCursorSlot_00604984"/>, une adresse différente (+4 octets) malgré la
    // ressemblance des deux noms : le case 0 de la transcription précédente écrivait par erreur
    // dans s_CardCursorSlot_00604984 au lieu de ce champ — trouvé en câblant le case 1, qui a
    // besoin de la vraie valeur de DAT_00604980 pendant que DAT_00604984 est remise à 0 juste
    // avant. Seul l'octet bas compte ; les trois octets suivants (0x604981/82/83, que la pseudo-C
    // de FUN_0050a910 lit comme un `CONCAT31` du même dword) sont en fait des adresses séparées,
    // le même genre de recouvrement que d'autres DAT_ de ce fichier — voir
    // <see cref="s_CardEntryCount_00604981"/>.
    private static byte s_CardSlotIndex_00604980;

    // Ghidra: DAT_00604981 — **corrigé 2026-08-16** : n'est plus « jamais posé ». C'est le compte
    // d'entrées que <see cref="LoadSaveDirectory"/> (FUN_005126c0) remplit en énumérant le
    // répertoire de sauvegarde ; le case 0xa de RenderSaveScreen le relit pour borner le curseur.
    private static byte s_CardEntryCount_00604981;

    // Ghidra: DAT_00604982 — un octet posé à 0xf par FUN_005126c0, jamais relu ailleurs dans le
    // code déjà reconstruit. TODO(REVERSE) : rôle exact non établi.
    private static byte s_Unknown_00604982;

    // Ghidra: DAT_00604960 — un drapeau (dword complet) posé à 0 ou 1 à une bonne dizaine de sites
    // à travers le jeu, dont FUN_005126c0. TODO(REVERSE) : rôle exact non établi ; probablement un
    // état « chargement de carte en cours », mais pas confirmé par la lecture d'un seul site.
    private static int s_Unknown_00604960;

    // Ghidra: DAT_0060498a — remis à zéro à l'entrée du dialogue de carte mémoire de la phase 0xd
    // du sous-écran « Invertaire ». TODO(REVERSE) : aucun lecteur trouvé dans Blaze.c ; le rôle
    // reste à établir, seule l'écriture est attestée.
    private static byte s_Unknown_0060498a;

    // Ghidra: &DAT_0060498c — un octet par entrée de répertoire (jusqu'à 0x20).
    //
    // **Renommé le 2026-08-18** : ce n'est pas un « nombre de personnages » mais le **nombre
    // d'images de l'icône** du fichier de sauvegarde. `meta[2] & 0xf` lit le nibble bas de l'octet
    // +0x02 de la « title frame » PlayStation, dont les seules valeurs légales sont 0x11, 0x12 et
    // 0x13 — une, deux ou trois images. Les `(n - 1) * 0x80` octets lus juste après sont les images
    // 2 et 3 (128 octets chacune, 16x16 en 4 bpp), et la hauteur du rectangle VRAM plus bas vaut
    // `n * 16` : autant de lignes que d'images. Voir « L'héritage PlayStation » plus haut.
    //
    // Utilisé ailleurs (Blaze.c:55417, 77618) comme diviseur d'une animation cyclique — ce qui
    // corrobore la lecture : c'est le cycle d'animation de l'icône, pas un décompte de personnages.
    private static readonly byte* s_CardIconFrameCount_0060498c = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>
    /// Ghidra: FUN_005126c0 @ 0x005126c0 — énumère les fichiers de sauvegarde du dossier
    /// <c>save\</c> correspondant au motif <c>s_SaveDeviceName_00563160</c> (« Blaze-Saving* »),
    /// jusqu'à 0x20 entrées, puis ouvre et lit l'en-tête (0x100 octets) de chacune pour peupler
    /// <see cref="s_CardDirectoryEntries_006049bc"/>, <see cref="s_CardEntryCount_00604981"/> et
    /// <see cref="s_CardIconFrameCount_0060498c"/>.
    /// <para>
    /// Reconstruit depuis le désassemblage brut (<c>Legacy/Raw/Blaze/FUN_005126c0.asm</c>) plutôt
    /// que la pseudo-C Ghidra, qui enveloppe cette fonction dans un habillage SEH (<c>puStack_20</c>,
    /// adresses de continuation littérales <c>0x5126f1</c>...) qui rend l'ordre réel des arguments
    /// ambigu à la lecture. L'assembleur, lui, est direct — pas de gestion d'exception réelle dans
    /// le code machine, juste l'artefact de décompilation habituel pour les fonctions compilées avec
    /// <c>/EHa</c>. Deux confirmations obtenues en le lisant :
    /// </para>
    /// <list type="bullet">
    /// <item><c>PUSH EAX</c> (asm 0x5126df, avant même le prologue standard) pousse la valeur chargée
    /// en tout premier lieu — <c>[0x56315c]</c>, qui pointe directement sur
    /// <see cref="s_SaveDeviceName_00563160"/> (confirmé par lecture directe des octets du binaire :
    /// <c>0x56315c</c> contient la valeur <c>0x563160</c>) — donc le premier appel à
    /// <see cref="SetCardFileName"/> pose bien le motif <c>"Blaze-Saving*"</c>, pas un chemin encore
    /// à déterminer.</item>
    /// <item>Les deux rectangles d'icône en fin de boucle (<c>Ordinal_674</c>) ont chacun leurs 4
    /// champs à des décalages <c>[ESP+n]</c> qui ne semblent PAS contigus avec l'adresse passée au
    /// premier coup d'œil, à cause d'un <c>PUSH</c> intercalé entre l'écriture du premier champ et le
    /// calcul de l'adresse de la structure (<c>LEA ECX,[ESP+0x18]</c> après un <c>PUSH ECX</c> décale
    /// tout de 4 octets). Recalculé précisément : les deux structures sont en fait complètes (4 mots
    /// chacune), rien n'est laissé de côté.</item>
    /// </list>
    /// <para>
    /// TODO(REVERSE) : rôle exact de <see cref="s_Unknown_00604982"/> et <see cref="s_Unknown_00604960"/>
    /// non établi (voir leurs commentaires). Le type de retour Ghidra (<c>undefined4</c>) est
    /// toujours 0 sur tous les chemins observés (trois <c>return 0</c> explicites, dont deux issus
    /// d'un échec précoce) — traduit en <c>void</c>, aucun appelant n'a de raison d'en lire le
    /// résultat.
    /// </para>
    /// </summary>
    private static int LoadSaveDirectory()
    {
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return 0;

        if (s_CardDirectoryEntries_006049bc == null)
            s_CardDirectoryEntries_006049bc = (byte*)NativeMemory.AllocZeroed(0x20 * 0x28);
        s_Unknown_00604982 = 0xf;
        s_Unknown_00604960 = 0;
        s_CardEntryCount_00604981 = 0;

        SetCardFileName(s_SaveDeviceName_00563160);
        SetCardPathDeviceFields();

        byte* entry = s_CardDirectoryEntries_006049bc;
        if (Winsys3d.Ordinal_18(s_CardPathDescriptor_00604940, entry) != null)
        {
            while (true)
            {
                *(int*)(entry + 0x18) = *(int*)(entry + 0x18) >> 0xd;
                entry += 0x28;
                s_CardEntryCount_00604981++;
                if (Winsys3d.Ordinal_19(entry) == 0) break;
                if (s_CardEntryCount_00604981 >= 0x20) break;
            }
        }

        if (s_CardEntryCount_00604981 == 0) return 0;

        byte* cursor = s_CardDirectoryEntries_006049bc;
        for (uint i = 0; i < s_CardEntryCount_00604981; i++, cursor += 0x28)
        {
            SetCardFileName(cursor);

            IntPtr handle = Winsys3d.Ordinal_21(s_CardPathDescriptor_00604940, 1);
            if ((int)handle < 0) return 0;

            Winsys3d.Ordinal_23(handle, 0, 0 /* FILE_BEGIN */);

            uint slotBase = i * 0x100;
            int read = (int)Winsys3d.Ordinal_24(handle, mainMem + 0x1c0000 + slotBase, 0x100);
            if (read >= 0)
            {
                byte frameCount = (byte)(mainMem[0x1c0002 + slotBase] & 0xf);
                s_CardIconFrameCount_0060498c[i] = frameCount;
                if (frameCount != 1)
                {
                    read = (int)Winsys3d.Ordinal_24(handle, mainMem + 0x1c0100 + slotBase, (uint)((frameCount - 1) * 0x80));
                }
            }
            Winsys3d.Ordinal_22(handle);
            if (read < 0) return 0;

            // Ghidra 0x5127f0-0512819 — les seize ushorts de MainMem+0x1c0060+slotBase sont la
            // **CLUT de l'icône** (offset +0x60 de la title frame PlayStation, 16 couleurs en
            // 16 bits). Poser le bit 15 sur les entrées non nulles est le correctif STP habituel du
            // format : dans une couleur PS1 15 bits + STP, 0x0000 vaut « transparent » et toute
            // autre couleur doit avoir son bit 15 levé pour être dessinée opaque. Rôle établi le
            // 2026-08-18 ; ce n'est pas un marqueur d'inventaire comme supposé jusqu'ici.
            for (int j = 0; j < 16; j++)
            {
                ushort* field = (ushort*)(mainMem + 0x1c0060 + slotBase + j * 2);
                if (*field != 0) *field |= 0x8000;
            }

            // Ghidra 0x51281f-0512863 — le téléversement de la CLUT en VRAM : seize mots sur une
            // seule ligne (0x10 x 1), positionnés en grille de 16 colonnes (bas octet de l'index)
            // sur autant de lignes que nécessaire.
            short* rect1 = stackalloc short[4];
            rect1[0] = (short)((i & 0xf) * 0x10);
            rect1[1] = (short)((i >> 4) + 0x1fe);
            rect1[2] = 0x10;
            rect1[3] = 1;
            Winsys3d.Ordinal_674(rect1, mainMem + 0x1c0060 + slotBase);

            // Ghidra 0x51286e-05128ab — le téléversement des images d'icône elles-mêmes, depuis
            // l'offset +0x80 de la title frame : hauteur `nombre d'images * 16` lignes, largeur 4
            // mots de 16 bits (= 16 pixels en 4 bpp), même grille de colonnes que la CLUT.
            short* rect2 = stackalloc short[4];
            rect2[0] = (short)((i & 0xf) * 4 + 0x3c0);
            rect2[1] = (short)(((i >> 4) << 6) + 0x20);
            rect2[2] = 4;
            rect2[3] = (short)(s_CardIconFrameCount_0060498c[i] * 16);
            Winsys3d.Ordinal_674(rect2, mainMem + 0x1c0080 + slotBase);
        }

        return 0;
    }

    /// <summary>Ghidra: `(_DAT_00604980 &amp; 3) + (_DAT_00604980 &amp; 4) * 4`, répété tel quel à
    /// plusieurs sites de <c>FUN_0050a910</c> plutôt que factorisé dans l'original — un seul point
    /// ici, comme pour <see cref="LabelAt"/>.</summary>
    private static int CardSlotSelector() =>
        (s_CardSlotIndex_00604980 & 3) + (s_CardSlotIndex_00604980 & 4) * 4;

    private static void StepSubState7Phase(byte* pad, int* cardTransferPhase)
    {
        // Ghidra: le sondage de transfert en cours — FUN_0050a910.c:100-116. `_DAT_0057333c == 1`
        // un chargement, `== 2` une écriture. Ne remet jamais `_DAT_0057333c` à 0 lui-même (seul
        // le case 5 le fait) : il continue donc de tourner, sans effet une fois le statut retombé
        // à « prêt », tant que la phase 5 n'est pas atteinte — fidèle à l'original, pas un défaut
        // de ce port.
        if (*cardTransferPhase == 1)
        {
            int slot = CardSlotSelector();
            s_CardLoadResult_00578de0 = PollCardTransfer(slot, false);
            if (s_CardLoadResult_00578de0 < -1) s_CardPhase_00578e0c = -4;
        }
        else if (*cardTransferPhase == 2)
        {
            int slot = CardSlotSelector();
            int r = Winsys3d.Ordinal_67(slot);
            while (r == 0) r = Winsys3d.Ordinal_67(slot);
            s_CardLoadResult_00578de0 = PollCardTransfer(slot, false);
        }

        ushort buttons = *(ushort*)(pad + 2);
        byte dialogGate = Mem.s_Unknown_00560fef;

        byte confirmGate = Mem.s_Unknown_005e909f;

        // Ghidra: `uVar17` — quand un cas se contente de `break` plutôt que de sauter directement
        // au rendu, l'original enchaîne sur `func_0x00544350(uVar17,...); DAT_00578e0c++;` juste
        // après le switch. Reproduit ici par ce label différé plutôt que par sept copies du même
        // couple d'appels.
        byte* pendingLabel = null;

        switch (s_CardPhase_00578e0c)
        {
            case 0:
                if (s_Unknown_00560fd4 != 0)
                {
                    // Ghidra: `iRam00560fd4 != 0` — jamais posé par du code reconstruit, lit 0 ;
                    // cette branche reste donc morte.
                    Mem.s_Unknown_00560fef = (byte)(dialogGate & 0xfd);
                    Mem.s_Unknown_0056075b = 0;
                    goto case 1;
                }
                s_CardSlotIndex_00604980 = s_CardSlotFlags_00562adc[s_CardPort_00578e20][s_CardCursor_00578e18];
                s_CardPhase_00578e0c++;
                break;

            case 1:
                // Ghidra: FUN_0050a910.c:136-155 (`case_r0x0050ae60`, aussi la cible du `goto`
                // depuis le case 0). Ouvre le fichier de sauvegarde pour lecture ; échec → tente de
                // le créer vide puis passe en phase 6 (écriture) ; succès → lance le chargement
                // (phase 0xe, déjà câblée) et pose le sondage de transfert ci-dessus.
                s_CardCursorSlot_00604984 = 0;
                // L'échafaudage `OPENFORESIA_FORCE_CARD_NAME` qui injectait ici un nom « TEST » a
                // été retiré le 2026-08-18 : il compensait le gabarit de nom vide (voir
                // s_SaveNameTemplate_00568498), défaut de portage désormais corrigé. Le nom lu ici
                // est celui que la finalisation vient d'écrire, « Blaze-Saving » + les sept
                // caractères de code.
                SetCardFileName(s_SelectScratch_00573338 + 0x1fd0);
                SetCardPathDeviceFields();
                IntPtr fileHandle = RetryOpenCardFile(s_CardPathDescriptor_00604940, 1);
                RetryCloseCardFile(fileHandle);
                if ((long)fileHandle < 0)
                {
                    BindMenuLabels(s_EmptyLabel_00572b40, 0, Mem.s_StatusDescriptor_00560728, 0);
                    IntPtr createHandle = RetryOpenCardFile(s_CardPathDescriptor_00604940, 0x30200);
                    RetryCloseCardFile(createHandle);
                    s_CardPhase_00578e0c = 6;
                }
                else
                {
                    BindMenuLabels(LabelAt(0x1ac), 0, Mem.s_StatusDescriptor_00560728, 0);
                    ResetCardTransferStatus(CardSlotSelector());
                    *cardTransferPhase = 1;
                    s_CardPhase_00578e0c = 0xe;
                }
                break;

            case 2:
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((dialogGate & 2) != 0) break;
                pendingLabel = LabelAt(0x1b0);
                break;

            case 3:
            case 8:
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((dialogGate & 2) != 0) break;
                pendingLabel = LabelAt(400);
                break;

            case 7:
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((dialogGate & 2) != 0) break;
                pendingLabel = LabelAt(0x1bc);
                break;

            case 0xb:
                if ((buttons & 0x4000) != 0)
                {
                    Mem.s_Unknown_00560fef = (byte)(dialogGate & 0xfd);
                    s_CardPhase_00578e0c--;
                    Mem.s_Unknown_0056075b = 0;
                    break;
                }
                if ((dialogGate & 2) != 0) break;
                pendingLabel = LabelAt(400);
                break;

            case 4:
                // Ghidra: Legacy/Raw/Blaze/FUN_0050a910.c:172-187. Transcrit le 2026-08-18.
                // Attend la fin du transfert d'écriture en cours (`WaitEvent` côté PS1), réarme les
                // quatre événements d'écriture, puis se sépare sur la réponse du joueur : sans
                // confirmation, l'intitulé vide (l'écran reste en attente) ; avec, on lève le
                // drapeau de sortie partagé.
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((dialogGate & 4) != 0) break;
                PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                ResetCardWriteEvents();
                if (confirmGate == 0) { pendingLabel = s_EmptyLabel_00572b40; break; }
                Mem.s_Unknown_00562b68 = -1;   // Ghidra: code_r0x0050b711
                break;

            case 5:
                if (s_CardPhaseChanged_00578e14 == 0) { s_CardPhase_00578e0c = 0; }
                else { s_CardPhaseChanged_00578e14 = 0; }
                *cardTransferPhase = 0;
                break;

            case 6:
                // Ghidra: `iVar4 = 0; if (_DAT_00578e14 == 0) { <corps> } else { _DAT_00578e14 = 0; }`
                // — la première image après le changement de phase se contente de purger le
                // drapeau (laisse à l'affichage une image pour se stabiliser) ; la suivante lance
                // vraiment l'écriture.
                if (s_CardPhaseChanged_00578e14 == 0) WriteCardSaveFile(cardTransferPhase);
                else s_CardPhaseChanged_00578e14 = 0;
                break;

            case 9:
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((dialogGate & 4) != 0) break;
                if (confirmGate == 0) { s_CardPhase_00578e0c++; break; }
                s_CardPhase_00578e0c = -5;
                break;

            case 0xa:
            {
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((buttons & 0x8000) != 0)
                {
                    BindMenuLabels(LabelAt(0x1c0), 0, Mem.s_StatusDescriptor_00560728, 0);
                    *(ushort*)(pad + 2) = (ushort)(buttons & 0x7fff);
                    s_CardPhase_00578e0c++;
                    PlayUiSound38();
                    break;
                }
                byte before = s_CardCursorSlot_00604984;
                byte slot = s_CardCursorSlot_00604984;
                if ((pad[6] & 0x80) != 0) slot--;
                if ((pad[6] & 0x20) != 0) slot++;
                // Ghidra: `DAT_00604981` — posé par LoadSaveDirectory (FUN_005126c0, transcrite
                // 2026-08-16), plus jamais figé à 0 depuis.
                byte cardCount = s_CardEntryCount_00604981;
                if (slot == 0xff) slot = (byte)(cardCount - 1);
                if (cardCount <= slot) slot = 0;
                s_CardCursorSlot_00604984 = slot;
                if (before != slot) PlayUiSound3A();

                BindMenuLabels(Mem.s_Unknown_005606f8, 0, Mem.s_StatusDescriptor_00560728, 0);
                // Le reste de la case (deux textes tirés de MainMem+0x1c0004+slot*0x100) touche
                // une zone jamais peuplée par du code reconstruit ; rendu laissé de côté ici.
                break;
            }

            case 0xc:
                if ((buttons & 0x4000) != 0)
                {
                    Mem.s_Unknown_00560fef = (byte)(dialogGate & 0xfb);
                    Mem.s_Unknown_00560763 = 0;
                    s_CardPhase_00578e0c = 10;
                    break;
                }
                if ((dialogGate & 4) != 0) break;
                if (confirmGate != 0)
                {
                    // Ghidra: re-rentre immédiatement dans le switch via `code_r0x0050ad53` avec
                    // la phase 10 — mort tant que confirmGate (toujours à zéro) ne change pas.
                    s_CardPhase_00578e0c = 10;
                    break;
                }
                pendingLabel = s_EmptyLabel_00572b40;
                break;

            case 0xd:
            {
                // Ghidra: Legacy/Raw/Blaze/FUN_0050a910.c:430-455. Transcrit le 2026-08-18. Même
                // suppression que le cas 0x10, mais sur **l'entrée choisie dans le navigateur de
                // sauvegardes** (cas 0xa) et non sur le personnage courant : le nom vient de la
                // table des répertoires, à l'entrée du curseur, pas de DAT_00573338+0x1fd0.
                if (s_CardPhaseChanged_00578e14 != 0) { s_CardPhaseChanged_00578e14 = 0; *cardTransferPhase = 0; break; }

                PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                ResetCardWriteEvents();
                SetCardFileName(s_CardDirectoryEntries_006049bc + s_CardCursorSlot_00604984 * 0x28);
                SetCardPathDeviceFields();

                if (DeleteCardFileRetrying())
                {
                    BindMenuLabels(LabelAt(0x198), 0, Mem.s_StatusDescriptor_00560728, 0);
                    *cardTransferPhase = 0;
                    s_CardPhase_00578e0c = 0;
                }
                else
                {
                    BindMenuLabels(LabelAt(0x1c4), 0, Mem.s_StatusDescriptor_00560728, 0);
                    s_CardPhase_00578e0c = -1;
                }
                break;
            }

            case 0xe:
            case 0xf:
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((dialogGate & 2) != 0) break;
                BindMenuLabels(LabelAt(s_CardPhase_00578e0c == 0xe ? 0x1c8 : 400),
                    0, Mem.s_StatusDescriptor_00560728, 0);
                s_CardPhase_00578e0c++;
                break;

            case 0x10:
            {
                // Ghidra: Legacy/Raw/Blaze/FUN_0050a910.c:487-511. Transcrit le 2026-08-18 — ce
                // n'était pas un « formatage » mais la **suppression de la sauvegarde existante**,
                // dernière étape du parcours 0xe → 0xf → 0x10 qu'emprunte le cas 1 quand le fichier
                // est déjà là. La phase repart ensuite à 0, le cas 1 ne trouve plus de fichier, et
                // l'écriture normale reprend : c'est ainsi que l'original réécrit une sauvegarde.
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if ((dialogGate & 4) != 0) break;
                if (confirmGate != 0) { s_CardPhase_00578e0c = -5; break; }

                BindMenuLabels(LabelAt(0x1cc), 0, Mem.s_StatusDescriptor_00560728, 0);
                PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                ResetCardWriteEvents();
                SetCardFileName(s_SelectScratch_00573338 + 0x1fd0);
                SetCardPathDeviceFields();

                if (!DeleteCardFileRetrying())
                {
                    BindMenuLabels(LabelAt(0x1c4), 0, Mem.s_StatusDescriptor_00560728, 0);
                    s_CardPhase_00578e0c = -1;
                    break;
                }

                // Ghidra: code_r0x0050ad18 → code_r0x0050ad2f — message, puis retour à la phase 0
                // avec le sondage de transfert coupé.
                BindMenuLabels(LabelAt(0x198), 0, Mem.s_StatusDescriptor_00560728, 0);
                *cardTransferPhase = 0;
                s_CardPhase_00578e0c = 0;
                break;
            }

            case 0x11:
                if ((dialogGate & 2) != 0) break;
                pendingLabel = LabelAt(0x1d8);
                break;

            case 0x12:
                if ((dialogGate & 4) == 0)
                {
                    if (confirmGate == 0)
                    {
                        s_SelectState_00578de8[0] = 0;
                        s_SelectState_00578de8[2] = 0;
                    }
                    else
                    {
                        s_Unknown_005de51c = 0;
                        s_SelectState_00578de8[0] = -1;
                        s_GameStateId = 1;
                    }
                }
                break;

            case -5:
                // Ghidra: Legacy/Raw/Blaze/FUN_0050a910.c:537-547. Transcrit le 2026-08-18. Le cas
                // -5 attend la fin du transfert, réarme les événements d'écriture, **relit
                // dialogGate** (l'original recharge `bVar1 = DAT_00560fef` juste avant), puis
                // **enchaîne sans `break`** sur le cas -4 — une chute délibérée dans le `switch`
                // d'origine, pas deux cas indépendants.
                PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                ResetCardWriteEvents();
                dialogGate = Mem.s_Unknown_00560fef;
                goto case -4;

            case -4:
                Mem.s_Unknown_00560fef = (byte)(dialogGate & 0xf9);
                Mem.s_Unknown_00560763 = 0;
                Mem.s_Unknown_0056075b = 0;
                Mem.s_Unknown_00562b68 = -1;
                PlayUiSound39();
                break;

            case -3:
            case -2:
                // Ghidra: Legacy/Raw/Blaze/FUN_0050a910.c:548-563. Transcrit le 2026-08-18. Les
                // deux phases d'erreur partagent un corps : sur un résultat de chargement -3 on
                // repart proprement à la phase 0 ; sinon on attend la fin du transfert, on réarme
                // les événements d'écriture et on lève le drapeau de sortie partagé
                // (code_r0x0050b711), le même que les cas 4, -1 et 99.
                if ((buttons & 0x4000) != 0) { s_CardPhase_00578e0c = -5; break; }
                if (s_CardLoadResult_00578de0 == -3) { *cardTransferPhase = 0; s_CardPhase_00578e0c = 0; break; }
                if ((dialogGate & 2) != 0) break;
                PollCardWriteStatus((s_CardSlotIndex_00604980 & 4) << 2, true);
                ResetCardWriteEvents();
                Mem.s_Unknown_00562b68 = -1;
                break;

            case -1:
            case 99:
                if ((dialogGate & 2) == 0) Mem.s_Unknown_00562b68 = -1;
                break;

            default:
                // Ghidra: toutes les valeurs 19..98 de la phase retombent sur le défaut — table de
                // saut à deux niveaux confirmée par lecture du binaire (&DAT_0050bea8), voir la
                // note sur s_CardPhase_00578e0c plus haut.
                break;
        }

        if (pendingLabel != null)
        {
            BindMenuLabels(pendingLabel, 0, Mem.s_StatusDescriptor_00560728, 0);
            s_CardPhase_00578e0c++;
        }
    }

    /// <summary>Raccourci pour <c>*(byte**)(*(int*)(s_MainResourceDirectory_005e97c4 + 0x70) + offset)</c>,
    /// répété tel quel à chaque case de navigation du sous-état 7.</summary>
    private static byte* LabelAt(int offset) =>
        *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + offset);

    /// <summary>
    /// Ghidra: <c>FUN_0051be90</c> @ 0x0051be90 — dessine un nombre décimal comme une rangée de
    /// tuiles numériques (pas des glyphes du moteur de texte), cinq mots par chiffre, chaînée dans
    /// <c>otEntry</c>. C'est l'affichage des « statistiques actuelles » que <see cref="RenderSaveScreen"/>
    /// utilise. **Corrige une note antérieure** : contrairement à ce qui était noté (« jamais
    /// décompilée, besoin d'un re-run Ghidra »), le corps est bien présent dans <c>Blaze.c</c>
    /// (ligne 66710) — juste assez loin dans les 98 000 lignes du fichier pour qu'une recherche
    /// d'une séance antérieure soit passée à côté. Pas de re-run nécessaire ; simple transcription.
    ///
    /// <para>Le nibble bas de <c>mode</c> est le nombre de chiffres : seules les <c>N</c> dernières
    /// entrées de la table de diviseurs décimaux à dix entrées <see cref="s_DecimalDivisors_00566be8"/>
    /// (la même que <see cref="Handler_005449f0"/>) sont parcourues — donc <c>mode=0x82</c> dessine
    /// exactement deux chiffres (dizaines, unités). Aucune division n'a lieu pour les diviseurs de
    /// tête sautés, donc une valeur atteignant ou dépassant l'ordre de grandeur du nombre de
    /// chiffres demandé tronque plutôt que de déborder proprement sur plus de tuiles ; tous les
    /// sites d'appel de cet écran gardent leurs valeurs sous ce seuil, donc ça ne devrait pas
    /// mordre en pratique. Le bit <c>0x80</c> de <c>mode</c> est un drapeau « dessiner les zéros de
    /// tête » : sans lui, un chiffre de tête à zéro est sauté **et sa largeur de colonne n'avance
    /// pas non plus** la position X ; avec lui, le zéro avance la position sans être dessiné (pas de
    /// tuile zéro visible — juste un espace).</para>
    ///
    /// <para><c>sprite[0]</c> est lui-même un pointeur vers un enregistrement de deux mots
    /// (couleur, attributs), lu **sans décalage de frame** — contrairement aux fonctions sœurs
    /// <see cref="DrawSpriteVariant"/>/<see cref="DrawFlatPrimitive"/>, qui ajoutent toutes deux
    /// <c>(short)sprite[4] * 8</c>. Transcrit exactement comme Ghidra le montre, sans supposer que
    /// ça suit ses sœurs.</para>
    /// </summary>
    // Ghidra: &DAT_00566c30 â€” l'enregistrement (couleur, attributs) partagé que les trois sites
    // d'appel de FUN_0051be90 dans RenderSaveScreen posent dans sprite[0] via l'adresse brute
    // 0x566c30, valable seulement parce que l'exécutable d'origine se charge à cette adresse. Relu
    // directement dans Legacy/Blaze.exe (décalage fichier 0x165230) plutôt que déréférencé tel
    // quel, qui aurait fauté nativement dans ce port. Largeur de colonne ~8 px, page 0x30 â€” des
    // valeurs sensées, cohérentes avec une tuile numérique.
    private static readonly uint* s_NumeralTileRecord_00566c30 = AllocNumeralTileRecord();

    private static uint* AllocNumeralTileRecord()
    {
        uint* record = (uint*)NativeMemory.Alloc(2, sizeof(uint));
        record[0] = 0x08504800;
        record[1] = 0x02300000;
        return record;
    }

    private static uint* EmitCardGlyphTile(uint* otEntry, uint* prim, int* sprite, uint value, uint mode, uint extra)
    {
        EmitViewportRect(otEntry, &prim);   // Ghidra: FUN_0051be20(param_1, &param_2)

        uint packedPos = (uint)sprite[2];        // Ghidra: local_20 = param_3[2]
        uint yPart = packedPos & 0xffff0000;

        uint* record = (uint*)*(int*)sprite;     // Ghidra: *param_3, pas de décalage de frame
        uint colour = record[0];
        uint attrs = record[1];
        if (Mem.s_DeviceCapFlag_00564a48) attrs &= 0xfffffdff;

        uint digitWidth = ((colour >> 0x10) & 0xff) / 10;
        byte digitsStarted = 0;
        short extraShort = (short)extra;
        uint extraByte = extra & 0xff;

        // Ghidra: &DAT_005e8f02 â€” même table que DrawSpriteVariant/DrawFlatPrimitive, décalée de
        // deux octets (mot haut de l'entrée).
        ushort page = *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 2 + ((attrs >> 0x10) & 0xff) * 4);
        uint spriteAttrs = (uint)sprite[1];

        int divisorIndex = 0;
        for (uint remaining = 10; remaining != 0; remaining--, divisorIndex++)
        {
            if (s_Unknown_005e9818 == 0) break;

            if (remaining <= (mode & 0xf))
            {
                uint divisor = Ui.s_DecimalDivisors_00566be8[divisorIndex];
                uint digit = value / divisor;
                value -= digit * divisor;

                if (digit != 0 || remaining == 1) digitsStarted++;

                if (digitsStarted == 0)
                {
                    if ((mode & 0x80) == 0) continue;   // ni dessin, ni avance
                }
                else
                {
                    prim[1] = (((attrs >> 8) & 0xff) << 8 | 0x6400) << 0x10 | (spriteAttrs & 0x3ffffff);
                    prim[2] = yPart | (packedPos & 0xffff);
                    prim[3] = (digit * digitWidth + (colour & 0xff))
                              | (((uint)((ushort)(extraShort >> 2) & 0xffc0) | extraByte) + page + (attrs >> 0x18)) * 0x10000
                              | (colour & 0xff00);
                    prim[4] = ((colour >> 0x18) << 0x10) | digitWidth;

                    prim[0] = *otEntry | 0x4000000;
                    *otEntry = (uint)(nuint)prim;
                    prim += 5;
                    s_Unknown_005e9818--;
                }

                packedPos += digitWidth;
            }
        }

        *(short*)(sprite + 4) = 0;               // Ghidra: *(undefined2*)(param_3 + 4) = 0
        prim = DrawFlatPrimitive(otEntry, prim, sprite);   // Ghidra: FUN_0051f9c0
        EmitClipRect(otEntry, &prim);                       // Ghidra: FUN_0051bdb0
        return prim;
    }

    /// <summary>Le rendu de fin de fonction, toujours exécuté : neuf cases fixes plus, si la phase
    /// est en 10..13, un carrousel de cartes. Ghidra: <c>LAB_0050b71b</c> à la fin de
    /// <c>FUN_0050a910</c>.</summary>
    private static uint* RenderSaveScreen(uint* otBase, uint* primCursor)
    {
        if (-1 < Mem.s_Unknown_00562b68 && s_CardPhase_00578e0c is > 9 and < 0xe)
        {
            // Ghidra: le carrousel de cartes — une icône par port, sur le même cercle que les
            // sous-états 0/6 (Ordinal_450/451). Bloqué par FUN_0051be90 dès la première icône.
            byte* directory = Mem.s_MainResourceDirectory_005e97c4;
            byte* box = stackalloc byte[20];
            NativeMemory.Clear(box, 20);
            *(uint*)(box + 4) = 0x1000000;
            *(uint*)box = *(uint*)(*(byte**)(directory + 0x94) + 0x38);
            *(short*)(box + 0x0a) = 0x30;

            *(short*)(box + 0x10) = 0;
            *(short*)(box + 8) = 0xb4;
            primCursor = DrawSpriteVariant(otBase, primCursor, (int*)box, 0, 0);
            *(short*)(box + 0x10) = 1;
            *(short*)(box + 8) = 0xe8;
            primCursor = DrawSpriteVariant(otBase, primCursor, (int*)box, 0, 0);
            primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)box);

            *(uint*)box = (uint)(nuint)s_NumeralTileRecord_00566c30;
            *(short*)(box + 8) = 0xbc;
            *(short*)(box + 0xa) = 0x38;
            // Ghidra: `*(undefined4 *)(_DAT_006049bc + 0x18 + (_DAT_00604984 & 0xff) * 0x28)` — un
            // enregistrement de la table des répertoires. Son rédacteur est transcrit
            // (<see cref="LoadSaveDirectory"/>, FUN_005126c0), mais **rien ne l'appelle encore** :
            // aucun appelant de FUN_005126c0 n'apparaît dans Blaze.c ni dans les re-décompilations
            // brutes, donc la fonction est probablement prise par adresse depuis du code non
            // reconstruit. C'est le seul point qui reste ouvert sur cet écran depuis que les cases
            // 4/0xd/0x10/-5/-3/-2 sont transcrites (2026-08-18) : la table reste à zéro, donc le
            // navigateur de sauvegardes s'affiche vide au lieu de lister les fichiers présents.
            uint entryValue = s_CardDirectoryEntries_006049bc != null
                ? *(uint*)(s_CardDirectoryEntries_006049bc + 0x18 + s_CardCursorSlot_00604984 * 0x28)
                : 0;
            primCursor = EmitCardGlyphTile(otBase, primCursor, (int*)box, entryValue, 0x82, 0);
        }

        EmitViewportRect(otBase, &primCursor);

        byte* label = stackalloc byte[20];
        NativeMemory.Clear(label, 20);
        *(uint*)(label + 4) = 0x1000000;
        *(uint*)label = *(uint*)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x34);

        if (*(short*)s_JoyAxis1_00604b0a == -0x8000)
        {
            *(short*)(label + 0x10) = 1;
            *(short*)(label + 8) = 0xa9;
            *(short*)(label + 0xa) = 0x6e;
            primCursor = DrawSpriteVariant(otBase, primCursor, (int*)label, 0, 0);
        }
        if (*(short*)s_JoyAxis2_00604b2e == -0x8000)
        {
            *(short*)(label + 0x10) = 2;
            *(short*)(label + 8) = 0xdd;
            *(short*)(label + 0xa) = 0x6e;
            primCursor = DrawSpriteVariant(otBase, primCursor, (int*)label, 0, 0);
        }

        *(short*)(label + 0x10) = 0;
        *(short*)(label + 8) = 0xbd;
        *(short*)(label + 0xa) = 0x48;
        EmitClipRect(otBase, &primCursor);
        primCursor = DrawUiPanel(otBase, primCursor, s_SelectSlots_00573518);

        byte* panel = s_SelectSlots_00573518 + CharacterSelectSlotStride;
        BindMenuLabels(LabelAt(0x158), 0, panel, 0);
        primCursor = Ui.RunTextEngine(primCursor, panel);
        BindMenuLabels(s_SelectScratch_00573338 + 0x100, 0, panel, 0);
        primCursor = Ui.RunTextEngine(primCursor, panel);
        BindMenuLabels(LabelAt(0x15c), 0, panel, 0);
        primCursor = Ui.RunTextEngine(primCursor, panel);
        BindMenuLabels(LabelAt(0xc + (s_SelectScratch_00573338[0x150] & 7) * 4), 0, panel, 0);
        primCursor = Ui.RunTextEngine(primCursor, panel);

        // Ghidra: DAT_005e909a..e — un descripteur de mini-jeu/animation lié au « Bonus » choisi.
        // Jamais lu ailleurs dans le code déjà reconstruit ; transcrit tel quel.
        s_Unknown_005e909a = 1;
        s_Unknown_005e909b = 0xf;
        s_Unknown_005e909d = 0x10;
        s_Unknown_005e909c = (byte)((s_FadeCounter_005e9960 >> 2 & 0x1fffffff) % 3
            + s_SelectScratch_00573338[0x151] * 3);
        s_Unknown_005e909e = s_SelectScratch_00573338[0x150];

        BindMenuLabels(LabelAt(0x160), 0, panel, 0);
        primCursor = Ui.RunTextEngine(primCursor, panel);

        short panelX = *(short*)(panel + 4);
        byte* atlas94 = *(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
        byte* glyph = stackalloc byte[20];
        NativeMemory.Clear(glyph, 20);
        *(uint*)(glyph + 4) = 0x1000000;

        // Ghidra: deux petites cases (le « Bonus » tiré), rangées en damier de deux colonnes.
        short y = (short)(*(short*)(panel + 6) + 0x52);
        for (uint i = 0; i < 2; i++)
        {
            EmitViewportRect(otBase, &primCursor);
            *(short*)(glyph + 0x10) = (short)(i + 0xb);
            *(uint*)glyph = *(uint*)(atlas94 + 0x30);
            short x = (short)(((i & 1) != 0 ? 0x38 : 0) + 0x12 + panelX);
            *(short*)(glyph + 8) = x;
            *(short*)(glyph + 0xa) = y;
            primCursor = DrawSpriteVariant(otBase, primCursor, (int*)glyph, 0, 0);
            primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)glyph);
            EmitClipRect(otBase, &primCursor);

            *(short*)(glyph + 8) = (short)(x + 0x1c);
            *(uint*)glyph = (uint)(nuint)s_NumeralTileRecord_00566c30;
            primCursor = EmitCardGlyphTile(
                otBase, primCursor, (int*)glyph, Mem.s_PanelSelection_005e9060[7 + i], 0x82, 0);
        }

        // Ghidra: les sept statistiques, même disposition en damier, colonne suivante.
        y = (short)(*(short*)(panel + 6) + 0x60);
        for (uint i = 0; i < 7; i++)
        {
            EmitViewportRect(otBase, &primCursor);
            *(short*)(glyph + 0x10) = (short)(i + 4);
            *(uint*)glyph = *(uint*)(atlas94 + 0x30);
            short x = (short)(((i & 1) != 0 ? 0x38 : 0) + 0x12 + panelX);
            *(short*)(glyph + 8) = x;
            *(short*)(glyph + 0xa) = y;
            primCursor = DrawSpriteVariant(otBase, primCursor, (int*)glyph, 0, 0);
            primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)glyph);
            EmitClipRect(otBase, &primCursor);

            *(short*)(glyph + 8) = (short)(x + 0x1c);
            *(uint*)glyph = (uint)(nuint)s_NumeralTileRecord_00566c30;
            primCursor = EmitCardGlyphTile(
                otBase, primCursor, (int*)glyph, Mem.s_PanelSelection_005e9060[i], 0x82, 0);

            y += (short)((i & 1) != 0 ? 9 : 0);
        }

        return DrawUiPanel(otBase, primCursor, panel);
    }

    // Ghidra: &DAT_00572e58 — huit enregistrements de 0x9c octets, du même type que l'entité de
    // scène et que les emplacements du carrousel : les huit icônes que le sous-état 4 dispose en
    // cercle. Ghidra les désigne par DAT_00572e78 (+0x20) et DAT_00572e80 (+0x28) selon la boucle.
    // Elles suivent le tableau de particules (0x00572be4 + 4 * 0x9c = 0x00572e54, puis 0x00572e58).
    private const int SceneIconCount = 8;
    private const int SceneIconStride = 0x9c;
    private static readonly byte* s_SceneIcons_00572e58 =
        (byte*)NativeMemory.AllocZeroed(SceneIconCount, SceneIconStride);

    /// <summary>
    /// Ghidra: FUN_00508e00 @ 0x00508e00 — <c>PTR_LAB_00560708[4]</c>, le carrousel des huit
    /// éléments, disposés sur un cercle de rayon <c>0x78</c> et choisis par <c>scratch[0x151]</c>.
    ///
    /// <para><b>Signature reprise au prologue</b> : <c>SUB ESP,0x44</c> plus quatre <c>PUSH</c> =
    /// <c>0x54</c>, donc les arguments sont en <c>[ESP+0x58..0x64]</c>. <c>EBP</c> reçoit le
    /// curseur de primitives depuis <c>[ESP+0x60]</c> avant la boucle de dessin (d'où le
    /// <c>unaff_retaddr</c> de Ghidra), et l'épilogue renvoie le résultat du dernier
    /// <c>FUN_005443a0</c>.</para>
    ///
    /// <para>Détail à ne pas rater : après la phase d'entrée, l'original **réutilise l'emplacement
    /// de son quatrième argument comme compteur de boucle** (<c>INC EBX ; MOV [ESP+0x64],EBX</c>),
    /// ce que Ghidra rend en <c>uStack00000004</c>. Ici c'est une variable locale.</para>
    /// </summary>
    private static uint* CharacterSelectSubState4(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;

        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;

            // La caméra saute directement à l'angle de l'élément courant, sans amortissement.
            ushort turn = (ushort)((scratch[0x151] - 4) & 7);
            *(short*)(Mem.s_CameraVector_00604928 + 0) = 0x15e;
            *(short*)(Mem.s_CameraVector_00604928 + 2) = (short)(turn * 0x200);
            *(short*)(Mem.s_CameraVector_00604928 + 4) = 0;

            *(short*)(Mem.s_SceneEntity_00572b48 + 0x30) = 0;                          // DAT_00572b78
            *(short*)(Mem.s_SceneEntity_00572b48 + 0x32) = (short)(turn * -0x200 + 0x800); // DAT_00572b7a
            *(short*)(Mem.s_SceneEntity_00572b48 + 0x34) = 0;                          // DAT_00572b7c

            // Les huit icônes sur un cercle : même disposition qu'au carrousel des personnages,
            // avec un pas de 0x200 (huit positions) et un rayon de 0x78 au lieu de 0x96.
            for (int i = 0; i < SceneIconCount; i++)
            {
                byte* icon = s_SceneIcons_00572e58 + i * SceneIconStride;
                *(int*)(icon + 0x1c) = (int)Winsys3d.GetSinTableEntry(i << 9) * -0x78;
                *(int*)(icon + 0x20) = 0;
                *(int*)(icon + 0x24) = Winsys3d.GetCosTableEntry(i << 9) * 0x78;
                *(ushort*)(icon + 0x28) = 0;
                *(ushort*)(icon + 0x2a) = 0;
                *(ushort*)(icon + 0x2c) = 0;
                *(ushort*)(icon + 0x36) = 1;
                *(uint*)(icon + 0x38) = 0x1000000;
            }

            s_SelectFlags_00562a48[2] = 3;
        }

        if (s_SelectFlags_00562a48[1] == 0) StepSubState4Phase(pad, scratch, state);

        // L'amortissement propre à ce sous-état : il vise scratch[0x151] par pas de 0x200 (huit
        // crans), redresse l'orientation de scène vers (0x200, 0x380, 0) et prend sa teinte de fond
        // dans la table des huit tons.
        ushort delta = (ushort)(((scratch[0x151] - 4) * 0x200
                                 - *(short*)(Mem.s_CameraVector_00604928 + 2)) & 0xfff);
        short step = delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-((short)(0x1000 - (short)delta) >> 2);
        *(short*)(Mem.s_CameraVector_00604928 + 2) += step;

        Mem.s_ScenePitch_005e9308 += (short)((0x200 - Mem.s_ScenePitch_005e9308) >> 2);
        Mem.s_SceneYaw_005e930a += (short)((0x380 - Mem.s_SceneYaw_005e930a) >> 2);
        Mem.s_SceneRoll_005e930c += (short)(-Mem.s_SceneRoll_005e930c >> 2);

        short* tone = (short*)(Mem.s_SceneToneTable_00562a58 + scratch[0x151] * GameMemory.SceneToneStride);
        Mem.s_BackColourR_005e93a0 += (short)((tone[0] - Mem.s_BackColourR_005e93a0) >> 2);
        Mem.s_BackColourG_005e93a2 += (short)((tone[1] - Mem.s_BackColourG_005e93a2) >> 2);
        Mem.s_BackColourB_005e93a4 += (short)((tone[2] - Mem.s_BackColourB_005e93a4) >> 2);

        RotMatrix((short*)Mem.s_CameraVector_00604928, (short*)Mem.s_CameraMatrix_006008e0);
        Winsys3d.Ordinal_376((byte)Mem.s_BackColourR_005e93a0,
                    (byte)Mem.s_BackColourG_005e93a2,
                    (byte)Mem.s_BackColourB_005e93a4);

        *(short*)(Mem.s_SceneEntity_00572b48 + 0x32) =
            (short)(0x800 - *(short*)(Mem.s_CameraVector_00604928 + 2));

        return DrawSubState4Icons(otBase, primCursor, scratch);
    }

    /// <summary>
    /// La machine à phases du sous-état 4 : <c>0x2e</c> pendant le choix, <c>0x2f</c> et
    /// <c>0x30</c> le temps de la boîte de confirmation.
    /// </summary>
    private static void StepSubState4Phase(byte* pad, byte* scratch, int* state)
    {
        if (state[2] == 0x2e)
        {
            ushort buttons = *(ushort*)(pad + 2);

            if ((buttons & 0x8000) != 0)          // validation
            {
                *(ushort*)(pad + 2) = (ushort)(buttons & 0x7fff);
                PlayUiSound38();
                state[2]++;
                return;
            }

            if ((buttons & 0x4000) != 0)          // annulation : retour au sous-état 3
            {
                short angle = (short)(scratch[0x150] + 8);
                *(short*)(Mem.s_CameraVector_00604928 + 2) = (short)(angle * 0x100);
                *(short*)(Mem.s_SceneEntity_00572b48 + 0x32) = (short)(angle * -0x100 + 0x800);
                state[2] = 0x5f;
                PlayUiSound39();
                state[0]--;
                return;
            }

            // Le choix tourne en boucle sur huit crans — le masque & 7 s'en charge, sans borne.
            sbyte before = (sbyte)scratch[0x151];
            if ((pad[6] & 0x80) != 0) scratch[0x151] = (byte)(before - 1);
            if ((pad[6] & 0x20) != 0) scratch[0x151]++;
            scratch[0x151] &= 7;
            if (before != (sbyte)scratch[0x151]) PlayUiSound3A();
            return;
        }

        if (state[2] == 0x2f)
        {
            if ((Mem.s_Unknown_00560fef & 2) == 0)
            {
                *(ushort*)(pad + 2) &= 0x7fff;
                state[2]++;
            }
            else if ((pad[3] & 0x40) != 0)
            {
                Mem.s_Unknown_0056075b = 0;
                Mem.s_Unknown_00560fef &= 0xfd;
                PlayUiSound39();
                state[2] = 0x2e;
            }
            return;
        }

        if (state[2] != 0x30) return;

        if ((Mem.s_Unknown_00560fef & 4) != 0)
        {
            if ((pad[3] & 0x40) == 0) return;
            Mem.s_Unknown_00560763 = 0;
            Mem.s_Unknown_00560fef &= 0xfb;
            PlayUiSound39();
            state[2] = 0x2e;
            return;
        }

        if (Mem.s_Unknown_005e909f != 0) { state[2] = 0x2e; return; }

        // Confirmé : l'icône choisie est recopiée dans chacune des quatre particules, qui partent
        // ensuite d'une position et d'une vitesse tirées au sort.
        state[2] = 0x39;
        for (int i = 0; i < GameMemory.SceneParticleCount; i++)
        {
            byte* particle = Mem.s_SceneParticles_00572be4 + i * GameMemory.SceneParticleStride;

            Mem.CopyDwords((uint*)(s_SceneIcons_00572e58 + scratch[0x151] * SceneIconStride),
                       (uint*)particle, 0x27);

            // Ghidra: puVar6 pointe la base + 0x3c, d'où les indices 0, 5, 6, 7 ci-dessous.
            uint* fields = (uint*)(particle + 0x3c);
            fields[0] = *(uint*)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94)
                                 + 8 + scratch[0x151] * 4);

            short spread = (short)CrtRand();
            fields[5] = (uint)(Winsys3d.GetSinTableEntry(spread) * 0x1e);
            fields[6] = (uint)(CrtRand() * 8 - 0x1fff8);
            fields[7] = (uint)(Winsys3d.GetCosTableEntry(spread) * 0x1e);
        }
        state[0]++;
    }

    /// <summary>
    /// Le dessin des huit icônes. Chacune est projetée depuis une copie de dix dwords de sa
    /// position, relevée de <c>0x28000</c>, et n'est dessinée que si la projection tombe à l'écran.
    /// L'emplacement d'OT est la profondeur rendue par <see cref="ProjectPoint"/>.
    /// </summary>
    private static uint* DrawSubState4Icons(uint* otBase, uint* primCursor, byte* scratch)
    {
        for (int i = 0; i < SceneIconCount; i++)
        {
            byte* icon = s_SceneIcons_00572e58 + i * SceneIconStride;
            short* glow = (short*)(icon + 0x28);

            byte* copy = stackalloc byte[40];
            Mem.CopyDwords((uint*)(icon + 0x1c), (uint*)copy, 10);
            *(int*)(copy + 4) += 0x28000;

            // L'icône choisie brille plus fort et tourne moins vite que les autres.
            *(short*)(icon + 0x38) = 0;
            if (i == scratch[0x151])
            {
                *(short*)(icon + 0x3a) = 0x100;
                glow[0] += (short)((0x1800 - glow[0]) >> 2);
                glow[1] += (short)((0x1800 - glow[1]) >> 2);
            }
            else
            {
                *(short*)(icon + 0x3a) = 0x300;
                glow[0] += (short)((0x1000 - glow[0]) >> 2);
                glow[1] += (short)((0x1000 - glow[1]) >> 2);
            }

            short* projected = stackalloc short[4];
            if (ProjectPoint((int*)copy, projected) == 0) continue;

            byte* atlas = ((byte**)*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94))[2 + i];
            short frame = (short)((s_FadeCounter_005e9960 >> 1) % 6);

            byte* sprite = stackalloc byte[20];
            NativeMemory.Clear(sprite, 20);
            *(byte**)sprite = atlas;
            // Ghidra: MOV ECX,[ESI + 0x10] — mais ESI vaut icône + 0x28, donc c'est **icône + 0x38**,
            // le mot d'attribut posé à 0x1000000 à l'initialisation. Le lire à +0x10 donne zéro, et
            // le sprite sort en disque noir : mesuré.
            *(uint*)(sprite + 4) = *(uint*)(icon + 0x38);
            *(short*)(sprite + 8) = projected[0];
            *(short*)(sprite + 0x0a) = projected[1];
            *(short*)(sprite + 0x0c) = glow[0];
            *(short*)(sprite + 0x0e) = glow[1];
            *(short*)(sprite + 0x10) = frame;

            // Le même décalage d'un pixel que le carrousel applique le temps du dessin.
            atlas[frame * 8 + 2]--;
            atlas[frame * 8 + 3]--;
            primCursor = DrawSpritePrimitive(otBase + projected[2], primCursor, (int*)sprite, 0, 0);
            atlas[frame * 8 + 2]++;
            atlas[frame * 8 + 3]++;
        }

        // Le libellé de l'élément courant, dans le troisième descripteur d'interface.
        byte* label = Mem.s_UiDescriptors_00560ff0 + 2 * GameMemory.UiDescriptorStride;   // &DAT_00562180
        BindMenuLabels(
            *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70)
                      + 0xc4 + scratch[0x151] * 4),
            0, label, 0);
        return Ui.RunTextEngine(primCursor, label);
    }

    /// <summary>
    /// Ghidra: FUN_00508860 @ 0x00508860 — <c>PTR_LAB_00560708[3]</c>, le choix sur une grille de
    /// quatre par deux, dont le résultat est empaqueté dans <c>scratch[0x153]</c>.
    ///
    /// <para><b>Signature reprise au prologue</b>, comme les précédentes : <c>SUB ESP,0x14</c> plus
    /// trois <c>PUSH</c> = <c>0x20</c>, donc les arguments sont en <c>[ESP+0x24..0x30]</c> —
    /// <c>[ESP+0x24]</c> otBase (Ghidra: <c>uStack_8</c>), <c>[ESP+0x2C]</c> le curseur de
    /// primitives (<c>unaff_retaddr</c>), <c>[ESP+0x30]</c> la manette (<c>param_1</c>). Le
    /// pseudo-C rend la fonction <c>void</c>, mais l'épilogue laisse dans <c>EAX</c> le résultat du
    /// dernier <c>FUN_00545340</c> : la valeur de retour existe, Ghidra l'a perdue.</para>
    ///
    /// <para><c>DAT_00573de0</c> est <c>DAT_00573518 + 0x8c8</c> : le **deuxième** descripteur du
    /// même tableau, pas un nouveau symbole.</para>
    /// </summary>
    private static uint* CharacterSelectSubState3(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        byte* scratch = s_SelectScratch_00573338;
        int* state = s_SelectState_00578de8;
        byte* box = s_SelectSlots_00573518;                                  // DAT_00573518
        byte* panel = s_SelectSlots_00573518 + CharacterSelectSlotStride;    // DAT_00573de0

        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;

            // Écrit ici dans l'ordre des adresses ; Ghidra les rend dans un ordre arbitraire.
            *(uint*)(box + 0x04) = 0x380028;      // DAT_0057351c — position (0x28, 0x38)
            *(short*)(box + 0x08) = 0xe0;         // DAT_00573520
            *(short*)(box + 0x0a) = 0x28;         // DAT_00573522
            *(uint*)(box + 0x20) = 0x1070c24;     // DAT_00573538 — texte, bit d'activation posé
            *(uint*)(box + 0x24) = 0x1000000;     // DAT_0057353c

            *(short*)(panel + 0x04) = 0x68;       // DAT_00573de4
            *(short*)(panel + 0x06) = 0x68;       // DAT_00573de6
            *(short*)(panel + 0x08) = 0x60;       // DAT_00573de8
            *(short*)(panel + 0x0a) = 0x18;       // DAT_00573dea
            panel[0x12] = 0x0b;                   // DAT_00573df0._2_1_
            panel[0x14] = 0x05;                   // DAT_00573df4._0_1_
            *(uint*)(panel + 0x20) = 0x1070c10;   // DAT_00573e00
            *(uint*)(panel + 0x24) = 0x1000000;   // DAT_00573e04

            state[4] = 0;
            state[5] = 0;
            s_SelectFlags_00562a48[2] = 2;
            scratch[0x153] = s_ClassDefault_00562b70[scratch[0x150]];
        }

        SettleSceneCamera(scratch);

        if (s_SelectFlags_00562a48[1] == 0) StepSubState3Phase(pad, scratch, state);

        return DrawSubState3(otBase, primCursor, state, box, panel);
    }

    /// <summary>
    /// La machine à phases du sous-état 3. <c>state[2]</c> vaut <c>0x5f</c> pendant le choix, puis
    /// <c>0x60</c>, <c>0x61</c>, <c>0x62</c> le temps d'une boîte de dialogue de confirmation.
    /// </summary>
    private static void StepSubState3Phase(byte* pad, byte* scratch, int* state)
    {
        switch (state[2])
        {
            case 0x5f:
            {
                ushort buttons = *(ushort*)(pad + 2);

                if ((buttons & 8) != 0)          // saut direct à la confirmation
                {
                    PlayUiSound38();
                    state[2] = 0x61;
                    break;
                }

                if ((buttons & 0x4000) != 0)     // annulation : retour au sous-état 1
                {
                    PlayUiSound39();
                    state[2] = 0x13;
                    state[0] -= 2;
                    break;
                }

                if ((buttons & 0x8000) != 0)     // validation
                {
                    *(ushort*)(pad + 2) = (ushort)(buttons & 0x7fff);
                    PlayUiSound38();
                    state[2] = 0x61;
                    // Le choix s'empaquette sur les bits 1..3, le bit 0 étant conservé.
                    scratch[0x153] = (byte)((((byte)state[5] << 2 | (byte)state[4]) << 1)
                                            | (scratch[0x153] & 1));
                    break;
                }

                // Déplacement dans la grille. Les quatre bits testés sont ceux de
                // s_PadButtonMasks_00564f18 ; le son ne part que si la position a bougé.
                uint before = (uint)(state[5] << 8 | state[4]);

                if ((pad[6] & 0x10) != 0 && state[5] != 0) state[5]--;
                if ((pad[6] & 0x40) != 0 && state[5] < 1) state[5]++;

                if ((pad[6] & 0x80) != 0)
                {
                    if (state[4] == 0)
                    {
                        if (state[5] != 0) { state[5]--; state[4] = 3; }
                    }
                    else state[4]--;
                }

                if ((pad[6] & 0x20) != 0)
                {
                    if (state[4] < 3) state[4]++;
                    else if (state[5] < 1) { state[5]++; state[4] = 0; }
                }

                if (before != (uint)(state[5] << 8 | state[4])) PlayUiSound3A();
                break;
            }

            case 0x60:
                // TODO(REVERSE): DAT_00560fef et ses voisins sont posés par le code de boîte de
                // dialogue, non reconstruit, donc ils lisent 0 et seule la première branche est
                // atteignable pour l'instant.
                if ((Mem.s_Unknown_00560fef & 4) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2]++;
                    scratch[0x153] = (byte)((((byte)state[5] << 2 | (byte)state[4]) << 1)
                                            | Mem.s_Unknown_005e909f);
                }
                else if ((pad[3] & 0x40) != 0)
                {
                    Mem.s_Unknown_00560763 = 0;
                    Mem.s_Unknown_00560fef &= 0xfb;
                    PlayUiSound39();
                    state[2]--;
                }
                break;

            case 0x61:
                if ((Mem.s_Unknown_00560fef & 2) == 0)
                {
                    *(ushort*)(pad + 2) &= 0x7fff;
                    state[2]++;
                    break;
                }
                if ((pad[3] & 0x40) == 0) break;
                Mem.s_Unknown_0056075b = 0;
                Mem.s_Unknown_00560fef &= 0xfd;
                PlayUiSound39();
                state[2] = 0x5f;
                break;

            case 0x62:
                if ((Mem.s_Unknown_00560fef & 4) != 0)
                {
                    if ((pad[3] & 0x40) == 0) break;
                    Mem.s_Unknown_00560763 = 0;
                    Mem.s_Unknown_00560fef &= 0xfb;
                    PlayUiSound39();
                    state[2] = 0x5f;
                    break;
                }
                if (Mem.s_Unknown_005e909f == 0)
                {
                    // Confirmé : on passe au sous-état suivant.
                    *(byte*)(s_SelectSlots_00573518 + CharacterSelectSlotStride + 0x14) = 4;
                    state[0]++;
                    state[2] = 0x2e;
                    break;
                }
                state[2] = 0x5f;
                break;
        }
    }

    /// <summary>
    /// Le dessin du sous-état 3. Le curseur de grille est relevé dans l'assembleur :
    /// <c>x = boîte+0x04 + colonne * 48 + 0x14</c>, <c>y = boîte+0x06 + ligne * 16 + 0x0a</c>.
    /// <para>Le second panneau reçoit <c>&amp;DAT_00572b48</c> **en guise de drapeaux** — c'est-à-dire
    /// que l'entité de scène devient le « propriétaire » que <see cref="DrawUiPanel"/> relit à
    /// <c>+0x8b0</c>. Son champ 0 valant <c>0x800000</c>, le panneau prend son emplacement joueur
    /// dans <c>owner[2]</c> : c'est le chemin `kind == 0x800000` de DrawUiPanel, jusqu'ici jamais
    /// emprunté.</para>
    /// </summary>
    private static uint* DrawSubState3(uint* otBase, uint* primCursor, int* state, byte* box, byte* panel)
    {
        byte* sprite = stackalloc byte[20];
        NativeMemory.Clear(sprite, 20);
        *(uint**)sprite = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
        *(uint*)(sprite + 4) = 0x1000000;
        *(short*)(sprite + 8) = (short)(*(short*)(box + 0x04) + state[4] * 48 + 0x14);
        *(short*)(sprite + 0x0a) = (short)(*(short*)(box + 0x06) + state[5] * 16 + 0x0a);
        *(short*)(sprite + 0x10) = 0x18;

        EmitViewportRect(otBase, &primCursor);
        primCursor = DrawSpriteVariant(otBase, primCursor, (int*)sprite, 0, 0);
        primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)sprite);
        EmitClipRect(otBase, &primCursor);

        BindMenuLabels(Mem.s_Unknown_005606f8, 0, box, 0);
        primCursor = Ui.RunTextEngine(primCursor, box);

        for (int offset = 0x500; offset < 0x508; offset += 4)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + offset),
                0, box, 0);
            primCursor = Ui.RunTextEngine(primCursor, box);
        }
        primCursor = DrawUiPanel(otBase, primCursor, box);

        if (state[2] != 0x2e)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0x18c),
                (uint)(nuint)Mem.s_SceneEntity_00572b48, panel, 0);
            primCursor = Ui.RunTextEngine(primCursor, panel);
            primCursor = DrawUiPanel(otBase, primCursor, panel);
        }

        return primCursor;
    }

    /// <summary>
    /// Le nom par défaut de la classe choisie, copié dans le tampon de saisie quand le joueur
    /// valide sans avoir rien tapé. Ghidra: <c>directory[0x70][0x208 + scratch[0x150] * 4]</c>.
    /// La longueur retenue est **la moitié** du nombre d'octets : les noms sont en Shift-JIS sur
    /// deux octets, et le compteur compte des caractères.
    /// </summary>
    private static void LoadDefaultName(byte* scratch, int* state)
    {
        byte* source = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70)
                                 + 0x208 + scratch[0x150] * 4);
        int length = 0;
        while (source[length] != 0)
        {
            scratch[0x100 + length] = source[length];
            length++;
        }
        scratch[0x100 + length] = 0;
        state[6] = length >> 1;
    }

    /// <summary>
    /// Le clavier virtuel à la manette. <c>state[3]</c> est la page, <c>state[4]</c> la colonne
    /// (0..9), <c>state[5]</c> la ligne (0..7) — la ligne 7 étant la colonne « fin de saisie » —
    /// et <c>state[6]</c> le nombre de caractères déjà posés.
    /// <para>Retourne <c>true</c> quand le sous-état se termine, ce qui fait désarmer la file de
    /// frappe par l'appelant — l'original y arrive par deux chemins qui se rejoignent avant
    /// <c>EnableKBInput(0)</c>.</para>
    /// </summary>
    private static bool HandlePadNameEntry(byte* pad, byte* scratch, int* state)
    {
        // Ghidra relit la même composition avant et après pour décider du clic de déplacement.
        uint before = (uint)((state[3] << 8 | state[5]) << 8 | state[4]);

        if ((pad[2] & 8) != 0) { state[4] = 0; state[5] = 7; }

        if ((pad[6] & 0x80) != 0)          // haut
        {
            if (state[4] == 0)
            {
                // En haut de colonne : on remonte en bas, mais seulement s'il y a où aller —
                // et l'original **repose le bit gauche** pour que la frappe se propage.
                if (state[5] != 0 || state[3] != 0) { state[4] = 9; pad[6] |= 0x10; }
            }
            else state[4]--;
        }

        if ((pad[6] & 0x20) != 0)          // bas
        {
            if (state[4] < 9) state[4]++;
            else if (state[5] < 7 || state[3] < 7)
            {
                state[4] = 0;
                *(ushort*)(pad + 6) |= 0x40;   // idem, en reposant le bit droit
            }
        }

        if ((pad[6] & 0x10) != 0)          // gauche
        {
            if (state[5] == 0) { if (state[3] != 0) state[3]--; }
            else state[5]--;
        }

        if ((pad[6] & 0x40) != 0 && state[5] < 7) state[5]++;   // droite

        if (state[5] == 7) state[4] = 0;

        if (before != (uint)((state[3] << 8 | state[5]) << 8 | state[4])) PlayUiSound3A();

        if ((*(ushort*)(pad + 2) & 0x8000) == 0)
        {
            if ((*(ushort*)(pad + 2) & 0x4000) == 0) return false;

            // Annulation : efface le dernier caractère, ou quitte si le tampon est déjà vide et
            // que le curseur est sur la case de fin.
            if (state[6] != 0)
            {
                state[6]--;
                scratch[0x100 + state[6]] = 0;
                PlayUiSound39();
                return false;
            }
            if (state[5] != 7 || state[3] != 7) return false;

            PlayUiSound39();
            state[2] = 0;
            state[0]--;
            return true;
        }

        if (state[5] != 7)
        {
            // Validation d'une case : le caractère se lit dans la table de la page courante.
            if (state[6] > 7) return false;

            byte* row = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70)
                                  + 0x50 + (state[5] + state[3]) * 4);
            byte glyph = row[state[4] * 3 + (((state[4] < 5 ? 1 : 0) - 1) & 2)];

            // TODO(REVERSE): le test de l'original compare un **octet** à DAT_00562c9c & 0xffff,
            // soit 0x4081 — le caractère Shift-JIS 0x8140 (espace idéographique) relu en
            // petit-boutien. Un octet ne peut pas valoir 0x4081, donc cette branche ne se
            // déclenche jamais telle qu'écrite ; c'est probablement une lecture sur deux octets
            // que Ghidra a rétrécie. Transcrit tel quel plutôt que corrigé au jugé.
            if (state[6] == 0 && glyph == (s_IdeographicSpace_00562c9c & 0xffff))
            {
                PlayUiSound3B();
                return false;
            }

            PlayUiSound38();
            scratch[0x100 + state[6]] = glyph;
            state[6]++;
            scratch[0x100 + state[6]] = 0;
            if (state[6] == 8) { state[4] = 0; state[5] = 7; }
            return false;
        }

        // Case « fin » : on confirme, en retombant sur le nom par défaut si rien n'a été saisi.
        if (state[6] == 0) LoadDefaultName(scratch, state);
        PlayUiSound38();
        state[2] = 0x5f;
        state[0] += 2;
        return true;
    }

    /// <summary>La frappe réelle, dépilée de la file de winsys3d par <c>GetKBChar</c>.</summary>
    private static bool HandleTypedNameEntry(int typed, byte* scratch, int* state)
    {
        if (typed == 0x0d)                 // Entrée : confirme, comme la case « fin »
        {
            state[5] = 7;
            if (state[6] == 0) LoadDefaultName(scratch, state);
            PlayUiSound38();
            state[0] += 2;
            state[2] = 0x5f;
            EnableKBInput(0);
            EnablePadKeyboard();           // Ghidra: FUN_00523520
            return false;                  // le désarmement est déjà fait ici
        }

        if (typed == 0x08)                 // retour arrière
        {
            if (state[6] != 0)
            {
                state[6]--;
                scratch[0x100 + state[6]] = 0;
                PlayUiSound39();
            }
            return false;
        }

        if (typed != 0x1b)
        {
            // Les codes de contrôle et la plage 0x80..0x9f sont ignorés, et le tampon est plein
            // à huit caractères.
            if (typed < 0x20 || (typed > 0x7f && typed < 0xa0) || state[6] > 7) return false;

            PlayUiSound38();
            scratch[0x100 + state[6]] = (byte)typed;
            state[6]++;
            scratch[0x100 + state[6]] = 0;
            if (state[6] == 8) { state[4] = 0; state[5] = 7; }
            return false;
        }

        PlayUiSound39();                   // Échap : retour au sous-état précédent
        state[2] = 0;
        state[0]--;
        return true;
    }

    /// <summary>
    /// Le dessin de l'écran : la boîte de saisie, le curseur sur la grille, puis les huit lignes
    /// du clavier virtuel.
    /// <para>Coordonnées du curseur, relevées dans l'assembleur plutôt que dans le pseudo-C, qui
    /// avait perdu le cadre de pile : <c>x = 0x40 + colonne * 16 + (colonne &lt; 5 ? 0 : 8)</c> et
    /// <c>y = boîte+0x06 + (ligne + 1) * 12</c>. Le décrochage de 8 pixels au-delà de la
    /// cinquième colonne est le même que celui du choix du caractère, deux lignes plus haut.</para>
    /// </summary>
    private static uint* DrawNameEntryScreen(uint* otBase, uint* primCursor, byte* scratch, int* state)
    {
        byte* box = Mem.s_UiDescriptors_00560ff0 + 1 * GameMemory.UiDescriptorStride;   // &DAT_005618b8

        BindMenuLabels(Mem.s_Unknown_005606f8, 0, box, 0);
        primCursor = Ui.RunTextEngine(primCursor, box);

        // Le nom en cours, recopié sur la pile puis suffixé d'un « _ » tant que le curseur n'est
        // pas sur la case de fin. L'original recopie de la même façon, dans un local de 24 octets.
        byte* typedName = stackalloc byte[24];
        int length = 0;
        while (scratch[0x100 + length] != 0) { typedName[length] = scratch[0x100 + length]; length++; }
        typedName[length] = 0;

        if (state[5] != 7)
        {
            typedName[length] = 0x5f;      // Ghidra: &DAT_00562c98, la chaîne "_"
            typedName[length + 1] = 0;
        }

        BindMenuLabels(typedName, 0, box, 0);
        primCursor = Ui.RunTextEngine(primCursor, box);

        // Le curseur de la grille : un sprite de 20 octets monté sur la pile, comme partout.
        // **Corrigé 2026-08-16** : le désassemblage (FUN_00507cb0.asm:403-408) lit
        // [0x005e97c0] puis +0x94 puis un déréférencement — c'est s_MainResourceBuffer_005e97c0
        // (déjà le bon symbole dans DrawUiPanel pour ce même `+0x94`), pas
        // s_MainResourceDirectory_005e97c4 (décalée de 4 octets). Avec le mauvais symbole, le
        // sprite lisait l'atlas au mauvais décalage et affichait une icône d'élément (terre) à la
        // place du curseur du clavier virtuel, cachant la lettre A — bug purement visuel signalé
        // par l'utilisateur.
        byte* cursor = stackalloc byte[20];
        NativeMemory.Clear(cursor, 20);
        *(uint**)cursor = **(uint***)(Mem.s_MainResourceBuffer_005e97c0 + 0x94);
        *(uint*)(cursor + 4) = 0x1000000;

        byte* grid = s_SelectSlots_00573518;
        *(short*)(cursor + 8) =
            (short)(*(short*)(grid + 0x04) + state[4] * 16 + (state[4] < 5 ? 0 : 8) + 0x18);
        *(short*)(cursor + 0x0a) = (short)(*(short*)(grid + 0x06) + (state[5] + 1) * 12);
        *(short*)(cursor + 0x10) = 0x18;

        EmitViewportRect(otBase, &primCursor);
        primCursor = DrawSpriteVariant(otBase, primCursor, (int*)cursor, 0, 0);
        primCursor = DrawFlatPrimitive(otBase, primCursor, (int*)cursor);
        EmitClipRect(otBase, &primCursor);

        // Les huit lignes du clavier virtuel. La table de largeurs passe en **fixe** le temps de
        // la grille — c'est ce qui aligne les colonnes — et revient au proportionnel après.
        BindMenuLabels(Mem.s_Unknown_005606f8, 0, grid, 0);
        primCursor = Ui.RunTextEngine(primCursor, grid);

        Ui.UseFixedGlyphWidths();             // Ghidra: FUN_00507870
        for (int i = 0; i < 8; i++)
        {
            BindMenuLabels(
                *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70)
                          + 0x50 + (i + state[3]) * 4),
                0, grid, 0);
            primCursor = Ui.RunTextEngine(primCursor, grid);
        }
        primCursor = DrawUiPanel(otBase, primCursor, grid);
        Ui.UseProportionalGlyphWidths();      // Ghidra: FUN_00507890

        return primCursor;
    }

    // Ghidra: DAT_00562c9c — deux octets 81 40 relevés au décalage fichier 0x16129c : le caractère
    // Shift-JIS 0x8140, l'espace idéographique. Le code le relit en dword, d'où le 0x4081.
    private const uint s_IdeographicSpace_00562c9c = 0x00004081;

    private static uint* CharacterSelectSubState0(uint* otBase, uint* otEntry, uint* primCursor, byte* pad)
    {
        ushort* padWords = (ushort*)pad;
        byte* scratch = s_SelectScratch_00573338;

        // Ghidra: le pilotage libre, actif tant que DAT_005633a8 vaut 0. Le bit 1 du premier mot
        // choisit la cible : couleur de fond au pas de 8 (bornée à 8 bits), sinon rotation de la
        // scène au pas de 0x40 (bornée au tour complet de 0x1000).
        if (Mem.s_DebugModeLatch_005633a8 == 0)
        {
            ushort buttons = padWords[0];
            if ((buttons & 1) == 0)
            {
                if ((buttons & 0x400) != 0) Mem.s_SceneYaw_005e930a -= 0x40;
                if ((padWords[0] & 0x800) != 0) Mem.s_SceneYaw_005e930a += 0x40;
                if ((padWords[0] & 0x100) != 0) Mem.s_SceneRoll_005e930c -= 0x40;
                if ((padWords[0] & 0x200) != 0) Mem.s_SceneRoll_005e930c += 0x40;
                if ((padWords[0] & 0x1000) != 0) Mem.s_ScenePitch_005e9308 += 0x40;
                if ((padWords[0] & 0x4000) != 0) Mem.s_ScenePitch_005e9308 -= 0x40;
                Mem.s_ScenePitch_005e9308 &= 0xfff;
                Mem.s_SceneYaw_005e930a &= 0xfff;
                Mem.s_SceneRoll_005e930c &= 0xfff;
            }
            else
            {
                if ((buttons & 0x400) != 0) Mem.s_BackColourG_005e93a2 -= 8;
                if ((padWords[0] & 0x800) != 0) Mem.s_BackColourG_005e93a2 += 8;
                if ((padWords[0] & 0x100) != 0) Mem.s_BackColourB_005e93a4 -= 8;
                if ((padWords[0] & 0x200) != 0) Mem.s_BackColourB_005e93a4 += 8;
                if ((padWords[0] & 0x1000) != 0) Mem.s_BackColourR_005e93a0 += 8;
                if ((padWords[0] & 0x4000) != 0) Mem.s_BackColourR_005e93a0 -= 8;
                Mem.s_BackColourR_005e93a0 &= 0xff;
                Mem.s_BackColourG_005e93a2 &= 0xff;
                Mem.s_BackColourB_005e93a4 &= 0xff;
            }
        }

        // Ghidra: la première frame du sous-état — le drapeau que FUN_00506880 relève.
        if (s_SelectFlags_00562a48[0] != 0)
        {
            s_SelectFlags_00562a48[0] = 0;

            Mem.s_BackColourR_005e93a0 = 0x60;
            Mem.s_BackColourG_005e93a2 = 0x60;
            Mem.s_BackColourB_005e93a4 = 0x60;
            Mem.s_ScenePitch_005e9308 = 0x200;
            Mem.s_SceneYaw_005e930a = 0x380;
            Mem.s_SceneRoll_005e930c = 0;

            // Ghidra: DAT_00604928 — un vecteur de trois shorts. Le troisième cadre la caméra sur
            // le personnage courant : (index - 8) * 0x100, soit un seizième de tour par cran.
            *(short*)(Mem.s_CameraVector_00604928 + 0) = 0x15e;
            *(short*)(Mem.s_CameraVector_00604928 + 4) = 0;
            *(short*)(Mem.s_CameraVector_00604928 + 2) = (short)((scratch[0x150] - 8 & 0xf) << 8);

            Mem.s_Unknown_006008d0 = 0;
            Mem.s_Unknown_006008d4 = 0;
            Mem.s_Unknown_006008d8 = 0;
            Mem.CameraTranslation[0] = 0;
            Mem.CameraTranslation[1] = 0;
            Mem.CameraTranslation[2] = 0;

            // Ghidra: les seize emplacements de personnage, posés sur un cercle. Ordinal_451/450
            // sont rsin/rcos en 0x1000 pas par tour, donc `i << 8` répartit exactement seize
            // positions, et 0x96 = 150 est le rayon.
            for (int i = 0; i < GameMemory.CharacterSlotCount; i++)
            {
                byte* slot = Mem.s_CharacterSlots_00578420 + i * GameMemory.CharacterSlotStride;

                *(uint*)(slot + 0x00) = 0x800000;
                slot[0x04] = (byte)i;
                slot[0x08] = 0;
                slot[0x0a] = 0;
                slot[0x0b] = (byte)(i & 3);
                *(ushort*)(slot + 0x10) = 0;
                *(ushort*)(slot + 0x12) = 0;
                *(ushort*)(slot + 0x18) = 0;
                *(ushort*)(slot + 0x1a) = 0;
                *(int*)(slot + 0x1c) = (int)Winsys3d.GetSinTableEntry(i << 8) * -0x96;
                *(int*)(slot + 0x20) = 0;
                *(int*)(slot + 0x24) = Winsys3d.GetCosTableEntry(i << 8) * 0x96;
                *(ushort*)(slot + 0x28) = 0;
                *(ushort*)(slot + 0x2a) = 0;
                *(ushort*)(slot + 0x2c) = 0;
                *(ushort*)(slot + 0x30) = 0;
                *(ushort*)(slot + 0x34) = 0;
                *(ushort*)(slot + 0x36) = 1;
                *(uint*)(slot + 0x38) = 0xffffff;
            }

            s_SelectFlags_00562a48[2] = 1;
        }

        // Ghidra: la machine à phases sur DAT_00578df0, sautée entièrement quand DAT_00562a4c est
        // encore posé. L'original la referme par un maillage de `goto` (LAB_00507438, LAB_0050743d,
        // joined_r0x00507423) ; il se réduit à deux variantes d'annulation qui ne diffèrent que par
        // l'octet remis à zéro et le bit effacé de DAT_00560fef.
        if (s_SelectFlags_00562a48[1] == 0)
        {
            bool sharedCancel = false;

            switch (s_SelectState_00578de8[2])
            {
                case 0:
                {
                    sbyte before = (sbyte)scratch[0x150];
                    if ((padWords[3] & 0x80) != 0) scratch[0x150] = (byte)(before - 1);
                    if ((padWords[3] & 0x20) != 0) scratch[0x150]++;
                    scratch[0x150] &= 0xf;
                    if (before != (sbyte)scratch[0x150]) PlayUiSound3A();   // FUN_0053db60

                    ushort word = padWords[1];
                    if ((word & 0x8000) == 0)
                    {
                        if ((word & 0x4000) != 0)
                        {
                            padWords[1] = (ushort)(word & 0xbfff);
                            PlayUiSound39();                                // FUN_0053dba0
                            s_SelectState_00578de8[2] = 0x77;
                        }
                    }
                    else
                    {
                        padWords[1] = (ushort)(word & 0x7fff);
                        PlayUiSound38();                                     // FUN_0053db80
                        s_SelectState_00578de8[2]++;
                    }
                    break;
                }

                case 1:
                    if ((Mem.s_Unknown_00560fef & 2) == 0)
                    {
                        padWords[1] &= 0x7fff;
                        s_SelectState_00578de8[2]++;
                    }
                    else sharedCancel = true;
                    break;

                case 2:
                    if ((Mem.s_Unknown_00560fef & 4) == 0)
                    {
                        if (Mem.s_Unknown_005e909f == 0)
                        {
                            // Ghidra: FUN_005352e0(&DAT_00578420 + index * 0x27, &DAT_00572b48, 0x27)
                            // — 0x27 dwords = 0x9c octets, soit l'emplacement entier recopié vers le
                            // bloc de scène. Le sous-état suivant travaille sur cette copie.
                            Mem.CopyDwords(
                                (uint*)(Mem.s_CharacterSlots_00578420 + scratch[0x150] * GameMemory.CharacterSlotStride),
                                (uint*)Mem.s_SceneEntity_00572b48, 0x27);

                            s_SelectState_00578de8[0]++;
                            s_SelectState_00578de8[2] = 0x13;
                            scratch[0x100] = 0;
                            s_SelectState_00578de8[6] = 0;
                        }
                        else s_SelectState_00578de8[2] = 0;
                    }
                    else if ((pad[3] & 0x40) != 0)
                    {
                        Mem.s_Unknown_00560763 = 0;
                        Mem.s_Unknown_00560fef &= 0xfb;
                        PlayUiSound39();
                        s_SelectState_00578de8[2] = 0;
                    }
                    break;

                case 0x59:
                    if ((Mem.s_Unknown_00560fef & 4) != 0)
                    {
                        if ((pad[3] & 0x40) != 0)
                        {
                            Mem.s_Unknown_00560763 = 0;
                            Mem.s_Unknown_00560fef &= 0xfb;
                            PlayUiSound39();
                            s_SelectState_00578de8[2] = 0;
                        }
                    }
                    else if (Mem.s_Unknown_005e909f == 0)
                    {
                        // La sortie de l'écran : le sous-état -1 fait sortir FUN_00506880 par son
                        // `goto`, qui saute la remise à zéro de l'état de jeu — celle-ci est faite
                        // ici à la place.
                        s_SelectState_00578de8[0] = -1;
                        s_GameStateId = 0;
                    }
                    else s_SelectState_00578de8[2] = 0;
                    break;

                case 0x77:
                    if ((Mem.s_Unknown_00560fef & 2) == 0)
                    {
                        padWords[1] &= 0x7fff;
                        s_SelectState_00578de8[2] = 0x59;
                    }
                    else sharedCancel = true;
                    break;
            }

            if (sharedCancel && (pad[3] & 0x40) != 0)
            {
                Mem.s_Unknown_0056075b = 0;
                Mem.s_Unknown_00560fef &= 0xfd;
                PlayUiSound39();
                s_SelectState_00578de8[2] = 0;
            }
        }

        // Ghidra: l'amortissement de la rotation vers le personnage courant, par quarts, sur le
        // chemin le plus court du tour de 0x1000.
        ushort delta = (ushort)(((scratch[0x150] - 8) * 0x100
                                 - *(short*)(Mem.s_CameraVector_00604928 + 2)) & 0xfff);
        short step = delta < 0x800
            ? (short)((short)delta >> 2)
            : (short)-(short)((0x1000 - (short)delta) >> 2);
        *(short*)(Mem.s_CameraVector_00604928 + 2) += step;

        // Ghidra: FUN_0051c300(&DAT_00604928, &DAT_006008e0) — la pose de caméra devient la matrice
        // que FUN_0051d1f0 recopiera ensuite dans le registre MATRIX du GTE.
        RotMatrix((short*)Mem.s_CameraVector_00604928, (short*)Mem.s_CameraMatrix_006008e0);

        // Ghidra: Ordinal_376 — libgte SetBackColor.
        Winsys3d.Ordinal_376((byte)Mem.s_BackColourR_005e93a0,
                    (byte)Mem.s_BackColourG_005e93a2,
                    (byte)Mem.s_BackColourB_005e93a4);

        // === Le carrousel : seize emplacements amortis, dessinés puis étiquetés ===
        //
        // Les décalages ci-dessous sont ceux de l'emplacement (base 0x00578420), pas ceux de
        // `psVar11` : l'original promène un `short *` calé 0x2a octets après la base, ce qui rend
        // tous ses indices négatifs. Recalés ici sur la base, où ils recoupent exactement le bloc
        // d'initialisation de la première frame plus haut.
        short* projected = stackalloc short[4];
        byte* slotCopy = stackalloc byte[40];
        int angleStep = 0;
        int lift = 0x2000;

        for (int i = 0; i < GameMemory.CharacterSlotCount; i++, angleStep += 0x100, lift -= 0x100)
        {
            byte* slot = Mem.s_CharacterSlots_00578420 + i * GameMemory.CharacterSlotStride;

            short* yaw = (short*)(slot + 0x32);
            short* glowR = (short*)(slot + 0x28);
            short* glowG = (short*)(slot + 0x2a);
            short* glowB = (short*)(slot + 0x2c);
            short* tiltX = (short*)(slot + 0x10);
            short* tiltY = (short*)(slot + 0x12);

            // L'emplacement choisi se redresse et s'illumine ; les autres s'inclinent et
            // s'éteignent. Toujours par quarts — l'amortissement standard du jeu.
            if (i == scratch[0x150])
            {
                *yaw += (short)(-(*yaw + angleStep) >> 2);
                *glowR += (short)((0x1800 - *glowR) >> 2);
                *glowG += (short)((0x1800 - *glowG) >> 2);
                *glowB += (short)((0x1800 - *glowB) >> 2);
                *tiltX += (short)(-*tiltX >> 2);
                *tiltY += (short)(-*tiltY >> 2);
            }
            else
            {
                *yaw += (short)((lift - *yaw) >> 2);
                *glowR += (short)(-*glowR >> 2);
                *glowG += (short)(-*glowG >> 2);
                *glowB += (short)(-*glowB >> 2);
                *tiltX += (short)((0x1000 - *tiltX) >> 2);
                *tiltY += (short)((0x1000 - *tiltY) >> 2);
            }

            // Sous 0x100 de luminosité, l'emplacement n'est pas dessiné du tout.
            if (*glowR > 0xff)
            {
                byte assetIndex = slot[0x04];
                short* animIndex = (short*)(slot + 0x18);
                short* frame = (short*)(slot + 0x1a);

                byte* bundle = ((byte**)s_AnimationTable_005e9640)[assetIndex];
                byte* animEntry = *(byte**)(bundle + *animIndex * 4);

                // Avance de frame, bouclée sur la longueur de l'animation (+0x16).
                (*frame)++;
                if (*(ushort*)(animEntry + 0x16) <= *frame) *frame = 0;

                // Ghidra: CONCAT22(psVar11[-9] >> 0xf, psVar11[-0x10]) — la moitié haute est le
                // signe de l'indice d'animation, la basse un champ de l'emplacement.
                // TODO(REVERSE): ce panachage sent l'artefact de reconstruction ; transcrit tel
                // quel faute de mieux.
                uint modelExtra = (uint)((*animIndex >> 0xf) << 0x10)
                                | *(ushort*)(slot + 0x0a);

                primCursor = DrawAnimatedModel(
                    otBase, primCursor, animEntry,
                    (int*)(((byte**)s_ModelTable_005e96a0)[assetIndex]),
                    (int*)(slot + 0x1c), *frame, modelExtra);
            }

            // L'étiquette : on projette la position de l'emplacement, relevée de 0x3c000, et on
            // ne dessine que si elle tombe à l'écran.
            Mem.CopyDwords((uint*)(slot + 0x1c), (uint*)slotCopy, 10);
            *(int*)(slotCopy + 4) += 0x3c000;

            if (ProjectPoint((int*)slotCopy, projected) == 0) continue;

            byte* labelSprite = stackalloc byte[20];
            NativeMemory.Clear(labelSprite, 20);

            byte* atlas = *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x28);
            *(byte**)labelSprite = atlas;
            *(uint*)(labelSprite + 4) = 0x1000000;
            *(short*)(labelSprite + 8) = projected[0];
            *(short*)(labelSprite + 0xa) = projected[1];
            *(short*)(labelSprite + 0xc) = *(short*)(slot + 0x10);
            *(short*)(labelSprite + 0xe) = *(short*)(slot + 0x12);
            *(short*)(labelSprite + 0x10) = (short)i;

            // Ghidra décrémente deux octets de l'entrée d'atlas, dessine, puis les restaure — un
            // décalage d'un pixel appliqué le temps des deux passes.
            atlas[i * 8 + 2]--;
            atlas[i * 8 + 3]--;

            // Deux passes décalées d'un pixel en X : l'ombre puis le texte.
            uint* labelOt = otBase + projected[2] + 0x14;
            primCursor = DrawSpritePrimitive(labelOt, primCursor, (int*)labelSprite, 2, 0);
            *(short*)(labelSprite + 8) -= 1;
            primCursor = DrawSpritePrimitive(labelOt, primCursor, (int*)labelSprite, 2, 0);

            atlas[i * 8 + 2]++;
            atlas[i * 8 + 3]++;
        }

        // Ghidra: les deux passes de texte finales — le nom de la classe puis celui du
        // personnage courant, toutes deux dans le premier descripteur d'interface.
        BindMenuLabels(Mem.s_Unknown_005606f8, 0, Mem.s_UiDescriptors_00560ff0, 0);
        primCursor = Ui.RunTextEngine(primCursor, Mem.s_UiDescriptors_00560ff0);

        BindMenuLabels(
            *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70) + 0xc + scratch[0x150] * 4),
            0, Mem.s_UiDescriptors_00560ff0, 0);
        primCursor = Ui.RunTextEngine(primCursor, Mem.s_UiDescriptors_00560ff0);

        return primCursor;
    }

    /// <summary>
    /// Ghidra: la boucle d'initialisation en tête de FUN_00506880 — neuf structures de 0x8c8 octets
    /// à partir de 0x00573518, dont seuls les 0x68 premiers octets sont écrits.
    ///
    /// <para>Le type est identifiable : <c>DAT_00560728</c> et <c>DAT_00560ff0</c> sont deux
    /// instances **statiques** du même type, lisibles dans <c>Legacy/Blaze.exe</c>, et 14 des 26
    /// premiers dwords y sont identiques à ce que cette boucle écrit — dont le <c>0x31</c> de tête
    /// et l'alternance de <c>0x01000000</c>. <c>FUN_005443a0</c>, le moteur de texte de l'interface,
    /// reçoit <c>DAT_00560728</c> (<c>Blaze.c:55881</c>), donc ces structures sont les descripteurs
    /// que ce moteur consomme. Leur disposition champ par champ reste à établir, d'où les
    /// constantes brutes ci-dessous : les transcrire telles quelles est fidèle, les nommer serait
    /// inventer.</para>
    /// </summary>
    private static void InitialiseCharacterSelectSlots()
    {
        for (int slot = 0; slot < CharacterSelectSlotCount; slot++)
        {
            uint* entry = (uint*)(s_SelectSlots_00573518 + slot * CharacterSelectSlotStride);

            // Ghidra: puVar4 pointe 8 octets après la base, d'où les indices négatifs de l'original.
            // Réécrits ici en décalages depuis la base, dans l'ordre des octets plutôt que dans
            // l'ordre — arbitraire — où Ghidra les a émis.
            entry[0] = 0x31;
            entry[1] = 0;
            entry[2] = 0;
            entry[3] = 0;
            entry[4] = 0x10180000;
            entry[5] = (uint)(slot + 3);
            entry[6] = 0x010f0808;
            entry[7] = 0x02cf1020;
            entry[8] = 0x01070c10;
            entry[9] = 0x01000000;
            entry[10] = 0;
            entry[11] = 0x01000000;
            entry[12] = 0x000f0000;
            entry[13] = 0x01000000;
            entry[14] = 0x000f0c00;
            entry[15] = 0x01000000;
            entry[16] = 0x010f0000;
            entry[17] = 0x01000000;

            // Ghidra: la queue — quatre paires (0x000f0000, 0x01000000) consécutives.
            for (int i = 0; i < 4; i++)
            {
                entry[18 + i * 2] = 0x000f0000;
                entry[19 + i * 2] = 0x01000000;
            }
        }
    }

    // Ghidra: &DAT_00573518 — neuf structures contiguës de 0x8c8 octets (le pas 0x232 dwords de la
    // boucle). L'original les prend dans .data ; ici c'est un bloc alloué, comme les autres.
    private const int CharacterSelectSlotCount = 9;
    private const int CharacterSelectSlotStride = 0x8c8;

    // Alloué bas : ce sont neuf instances du type de s_UiDescriptors_00560ff0, en-tête compris
    // (InitialiseCharacterSelectSlots écrit les mêmes 26 dwords). Aucun des sous-états déjà
    // transcrits ne les passe encore au moteur de texte, mais dès qu'un le fera l'ordering
    // table de +0xa4 y sera masquée à 0xffffff comme sur les autres. Vingt kilo-octets de la
    // fenêtre basse, contre une panne à retrouver plus tard.
    private static byte* s_SelectSlotsStorage;
    private static byte* s_SelectSlots_00573518
        => s_SelectSlotsStorage != null
            ? s_SelectSlotsStorage
            : (s_SelectSlotsStorage =
                   AllocLowZeroed((uint)(CharacterSelectSlotCount * CharacterSelectSlotStride)));

    // Ghidra: DAT_00573338 — le bloc que la boucle place à MainMem + 0xf0000. C'est la même adresse
    // que la destination de la liste +0xb0 de LoadCharacterSelectResources : le chargement y dépose
    // des blocs bruts, la boucle de frame l'efface puis le relit
    // (`*(byte *)(DAT_00573338 + 0x151)` sert d'index dans la table de couleurs DAT_00562a58).
    //
    // **Dérivé de MainMem plutôt qu'affecté une fois (2026-08-18).** C'était un champ posé par le
    // seul LoadCharacterSelectResources, donc encore nul pour qui atteignait l'état de jeu 1 sans
    // être passé par l'écran de sélection des personnages — exactement ce que fait « Début du jeu »
    // au menu principal, et ce qui faisait échouer le `NativeMemory.Clear` de RunGameState1Entry sur
    // un pointeur nul. L'original n'a pas ce problème : il recalcule `*MainMem + 0xf0000` sur place
    // à chaque site (voir FUN_00536ea0, `iVar17 = *(int *)MainMem_exref + 0xf0000;`). On fait pareil.
    private static byte* s_SelectScratch_00573338
    {
        get
        {
            byte* mainMem = GetMainMemPtr();
            return mainMem != null ? mainMem + 0xf0000 : null;
        }
    }

    // Ghidra: DAT_005e9020 â€” read at ~60 sites across the game, essentially always as a party-size
    // bound: loop upper limit over active characters, "current player slot" modulo divisor, and
    // compared/assigned from DAT_005dbc18 (a save-slot-derived count). Declared now because
    // RefreshPartyAbilityMask (FUN_00535710) needs it; the role is inferred from cross-referencing
    // those call sites, not yet confirmed by tracing a single one end to end.
    private static int s_PartySize_005e9020;

    // Ghidra: DAT_005e9000 â€” elsewhere indexed byte-at-a-time (up to `& 0xff`) with individual bits
    // set/cleared via `1 << (someIndex & 0x1f)`, read as "does any party member know skill/spell N"
    // (bit = member slot). RefreshPartyAbilityMask only touches the first 8 dwords (32 bytes) â€” the
    // OR-accumulator this function recomputes from each character's own flag block. Sized at 0x100
    // bytes to safely cover every observed `& 0xff` index; true extent in the original .data section
    // not confirmed.
    // 0x60 utiles, borné par s_PanelSelection_005e9060 — les 0x100 déclarés jusqu'ici débordaient
    // sur lui.
    private static byte* s_PartyAbilityMask_005e9000 => Mem.Arena(0x005e9000);

    /// <summary>
    /// Ghidra: FUN_00535710 @ 0x00535710 â€” recomputes the party-wide combined ability/skill bitmask
    /// (<see cref="s_PartyAbilityMask_005e9000"/>) by OR-ing together the per-character 32-byte flag
    /// block at offset 0x2e0 of each of the <see cref="s_PartySize_005e9020"/> active characters'
    /// 0x2000-byte record in the character-select scratch area
    /// (<see cref="s_SelectScratch_00573338"/> + <c>i * 0x2000</c>; Ghidra reads this relative to
    /// MainMem directly as <c>MainMem + 0xf02e0</c>, which is exactly
    /// <c>s_SelectScratch_00573338 (= MainMem + 0xf0000) + 0x2e0</c>).
    /// </summary>
    private static void RefreshPartyAbilityMask()
    {
        uint* accumulator = (uint*)s_PartyAbilityMask_005e9000;
        for (int i = 0; i < 8; i++) accumulator[i] = 0;

        int partySize = s_PartySize_005e9020;
        if (partySize == 0) return;

        byte* source = s_SelectScratch_00573338 + 0x2e0;
        for (; partySize != 0; partySize--, source += 0x2000)
        {
            uint* src = (uint*)source;
            for (int i = 0; i < 8; i++) accumulator[i] |= src[i];
        }
    }
}
