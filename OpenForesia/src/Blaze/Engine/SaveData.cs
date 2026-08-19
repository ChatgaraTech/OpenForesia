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
    // =====================================================================
    // La sous-couche carte mémoire — ouverture/fermeture de fichier, 2026-08-16.
    //
    // De vrais Win32 CreateFileA/CloseHandle/SetFilePointer/ReadFile/WriteFile sur un dossier
    // "save\", à travers winsys3d.dll (FUN_00a04600 et consorts) — pas d'émulation de carte
    // mémoire PS1 — mais l'API imitée en est bien une : voir la note « L'héritage PlayStation »
    // juste en dessous. **Toutes les phases sont transcrites depuis le 2026-08-18** ; il ne reste
    // aucun ThrowSaveCardUnresolved sur cet écran.
    //
    // Le cycle complet de réécriture d'une sauvegarde existante est donc câblé :
    //   case 1 (le fichier existe) → 0xe → 0xf → 0x10 (DeleteFileA) → phase 0
    //   → case 1 (le fichier n'existe plus) → création → case 6 (écriture).
    //
    // ---------------------------------------------------------------------
    // L'héritage PlayStation (établi le 2026-08-18)
    //
    // Cet écran n'« a pas l'air » d'une carte mémoire PS1 : c'en est une, portée telle quelle.
    // winsys3d.dll réimplémente sur Win32 la BIBLIOTHÈQUE PSY-Q (libapi) du BIOS PlayStation, et
    // ses ordinaux 6 à 25 en reprennent l'ordre exact :
    //
    //   6  OpenEvent      7  CloseEvent    8  WaitEvent     9  TestEvent
    //   10 EnableEvent    11 DisableEvent  12 DeliverEvent  13 UndeliverEvent
    //   14 OpenTh         15 CloseTh       16 ChangeTh
    //   18 firstfile      19 nextfile      20 erase         21 open
    //   22 close          23 lseek         24 read          25 write
    //
    // Les constantes le confirment une à une, ce sont celles de libapi :
    //   ids d'événement   0xf4000001 = SwCARD (logiciel), 0xf0000011 = HwCARD (matériel)
    //   spécifications    0x0004 EvSpIOE (fin d'E/S), 0x8000 EvSpERROR,
    //                     0x0100 EvSpTIMOUT, 0x2000 EvSpNEW
    //   mode              0x1000 EvMdINTR (rappel), 0x2000 EvMdNOINTR (drapeau)
    //   états d'un nœud   0x1000 EvStWAIT → 0x2000 EvStACTIVE → 0x4000 EvStALREADY
    //
    // D'où la forme de tout le reste : DAT_00604980 est un `chan` PlayStation, `port * 16 + carte`
    // (CardSlotSelector le décompose en `(x & 3) + (x & 4) * 4`) ; le motif "Blaze-Saving*" est
    // l'argument de `firstfile` ; les huit talons &LAB_00504610..680 sont les gestionnaires
    // d'événement qui publient l'état du transfert.
    //
    // Et le fichier produit est un vrai fichier de sauvegarde PS1, avec sa « title frame »
    // standard en tête de bloc 0 — ce que WriteCardSaveFile compose sans le nommer :
    //   +0x00  "SC"                  la signature
    //   +0x02  0x11                  drapeau d'icône : nibble bas = nombre d'images (1 ici)
    //   +0x03  0x03                  nombre de blocs de 8 Kio → 3 * 0x2000 = 0x6000, la taille
    //   +0x04  titre en Shift-JIS    pleine chasse, deux octets par caractère (NameEncodeChar)
    //   +0x60  CLUT 16 couleurs      capturée en VRAM (Ordinal_684), 16x1 en 16 bits
    //   +0x80  icône 16x16 en 4 bpp  128 octets par image, jusqu'à trois images
    //
    // C'est aussi ce qui explique la lecture de LoadSaveDirectory : le champ qu'elle nomme
    // « nombre de personnages » est en réalité le nombre d'images de l'icône, et les
    // `(n - 1) * 0x80` octets qu'elle lit ensuite sont les images 2 et 3 — voir la note sur
    // s_CardIconFrameCount_0060498c.
    // ---------------------------------------------------------------------
    // =====================================================================

    // Ghidra: DAT_00563158 — deux pointeurs de chaîne ANSI statiques, relus directement dans
    // Legacy/Blaze.exe (décalage fichier 0x161758) plutôt que déréférencés à l'adresse d'origine,
    // qui n'a pas de sens dans ce port — même précédent que le gabarit du fichier de vérification
    // CD plus tôt cette séance.
    private static readonly byte* s_SaveFolderName_00563170 = ToAnsiZ("save\\");
    private static readonly byte* s_SaveDeviceName_00563160 = ToAnsiZ("Blaze-Saving*");

    // Ghidra: s_Veuillez_connecter_la_manette___00568510 @ 0x00568510 (Documentation/DataSymbols.md).
    private static readonly byte* s_ControllerBannerText_00568510 =
        ToAnsiZ("Veuillez connecter la manette !");

    // Ghidra: DAT_00604940.. — le descripteur de chemin qu'Ordinal_21 lit par pointeur (dans
    // winsys3d.dll, FUN_00a03f80, jamais relu en détail ici : ce port ne fait que lui fournir des
    // données réelles à la bonne adresse, comme pour tout appel de DLL). Disposition tassée :
    // [0..3] pointeur du nom de dossier, [4..7] pointeur du nom de « périphérique », le nom de
    // fichier écrit par SetCardFileName commençant à l'octet [5] et chevauchant donc la queue du
    // second pointeur — fidèle à l'original, pas une erreur de recouvrement.
    private static readonly byte* s_CardPathDescriptor_00604940 = (byte*)NativeMemory.AllocZeroed(0x100);

    // Ghidra: DAT_00604ab0 / DAT_00604a00 — les drapeaux d'état de transfert écriture/lecture que
    // FUN_005046c0/FUN_00504770 sondent. 4 = prêt/inactif.
    private static int s_CardWriteStatus_00604ab0 = 4, s_CardReadStatus_00604a00 = 4;

    // Ghidra: DAT_00604b58/5c/60/64 (lecture) et DAT_00604b68/6c/70/74 (écriture) — **les huit
    // poignées que rend Ordinal_6**, pas des blocs à fabriquer. `FUN_00504460` les stocke dans ces
    // huit globales, et `FUN_005047e0`/`FUN_00504730` les repassent telles quelles à Ordinal_9.
    //
    // 2026-08-18 : dix-septième à vingt-quatrième instances du défaut d'aliasing déjà rencontré —
    // ce port allouait ici huit blocs de 12 octets à zéro, sans rapport avec les nœuds que la DLL
    // gère dans son propre tas. Ordinal_9 (winsys3d.c FUN_00a041d0) réarme un nœud en écrivant son
    // champ +4 ; sur un faux bloc il ne faisait rien, et surtout les vrais nœuds ne redevenaient
    // jamais actifs.
    private static readonly void*[] s_CardReadEventHandles_00604b58 = new void*[4];
    private static readonly void*[] s_CardWriteEventHandles_00604b68 = new void*[4];

    // Ghidra: &LAB_00504610..&LAB_00504680 — les huit gestionnaires que `FUN_00504460` enregistre
    // auprès de la DLL. Ghidra n'en donne aucun corps (fonctions uniquement prises par adresse,
    // catégorie « re-run Ghidra » de CLAUDE.md), et ce port passait donc `null` — ce que la DLL
    // appelle vraiment, d'où le 0xC0000005 dès qu'un transfert carte était sondé.
    //
    // Relus directement dans Legacy/Blaze.exe le 2026-08-18 : huit talons de neuf octets, tous de
    // la même forme `C7 05 <adresse> <imm32>` puis `C3`, soit une seule affectation suivie d'un
    // retour.
    //
    //   0x00504610  MOV dword [0x00604AB0],0     0x00504650  MOV dword [0x00604A00],0
    //   0x00504620  MOV dword [0x00604AB0],1     0x00504660  MOV dword [0x00604A00],1
    //   0x00504630  MOV dword [0x00604AB0],2     0x00504670  MOV dword [0x00604A00],2
    //   0x00504640  MOV dword [0x00604AB0],3     0x00504680  MOV dword [0x00604A00],3
    //
    // Autrement dit : les quatre premiers publient l'état d'écriture, les quatre suivants l'état de
    // lecture — exactement les deux globales que `PollCardWriteStatus`/`PollCardReadStatus`
    // sondent, et les valeurs 0..3 exactement celles que `PollCardTransfer` décode. C'est le
    // chaînon qui manquait pour que ces deux fonctions puissent quitter la valeur 4.
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardWriteStatusHandler0() => s_CardWriteStatus_00604ab0 = 0;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardWriteStatusHandler1() => s_CardWriteStatus_00604ab0 = 1;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardWriteStatusHandler2() => s_CardWriteStatus_00604ab0 = 2;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardWriteStatusHandler3() => s_CardWriteStatus_00604ab0 = 3;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardReadStatusHandler0() => s_CardReadStatus_00604a00 = 0;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardReadStatusHandler1() => s_CardReadStatus_00604a00 = 1;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardReadStatusHandler2() => s_CardReadStatus_00604a00 = 2;
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static void CardReadStatusHandler3() => s_CardReadStatus_00604a00 = 3;

    /// <summary>
    /// Ghidra: FUN_005128e0 @ 0x005128e0 — écrit les cinq premiers octets du chemin dans le
    /// descripteur. **Corrigé après désassemblage direct** (<c>Legacy/Blaze.exe</c>, décalage
    /// fichier <c>0x111ce0</c>) : la pseudo-C laissait croire que <c>DAT_00604940</c>/
    /// <c>DAT_00604944</c> recevaient les *pointeurs* de <see cref="s_SaveFolderName_00563170"/>,
    /// mais l'assembleur montre <c>MOV ECX,[EAX]</c> puis <c>MOV [0x604940],ECX</c> — une
    /// déréférence de plus que la pseudo-C ne le montre : ce sont les **quatre octets littéraux**
    /// <c>"save"</c> qui sont copiés dans le descripteur, suivis d'un cinquième octet, <c>'\'</c>
    /// seul (pas deux, contrairement à ce que la pseudo-C `undefined2` suggérait). Le descripteur
    /// est donc simplement la chaîne C <c>"save\"</c> suivie du nom de fichier que
    /// <see cref="SetCardFileName"/> écrit à partir de l'octet 5 — pas de recouvrement, juste une
    /// concaténation directe. <see cref="s_SaveDeviceName_00563160"/> (<c>"Blaze-Saving*"</c>)
    /// n'est **pas** utilisée ici du tout, contrairement à l'hypothèse initiale ; gardée déclarée
    /// au cas où un autre site s'en sert, non encore trouvé.
    /// </summary>
    private static void SetCardPathDeviceFields()
    {
        *(uint*)s_CardPathDescriptor_00604940 = *(uint*)s_SaveFolderName_00563170;   // "save"
        s_CardPathDescriptor_00604940[4] = s_SaveFolderName_00563170[4];             // '\'
    }

    /// <summary>Ghidra: FUN_00512900 @ 0x00512900 — copie une chaîne NUL-terminée dans le champ nom
    /// de fichier du descripteur, à l'octet 5 — juste après le <c>'\'</c> que
    /// <see cref="SetCardPathDeviceFields"/> écrit à l'octet 4, pas un chevauchement.</summary>
    private static void SetCardFileName(byte* name)
    {
        byte* dest = s_CardPathDescriptor_00604940 + 5;
        byte c;
        do { c = *name++; *dest++ = c; } while (c != 0);
    }

    /// <summary>Ghidra: FUN_00504690 @ 0x00504690 — remet les deux drapeaux de statut à 4 puis émet
    /// un signal de complétion Ordinal_67. Confirmé dans le binaire (winsys3d.dll, FUN_00a050f0,
    /// <c>RET 4</c>) qu'Ordinal_67 rend toujours 1 — la boucle ci-dessous ne boucle donc jamais
    /// vraiment dans cette version de la DLL.</summary>
    private static void ResetCardTransferStatus(int slot)
    {
        s_CardWriteStatus_00604ab0 = 4;
        s_CardReadStatus_00604a00 = 4;
        int result = Winsys3d.Ordinal_67(slot);
        while (result == 0) result = Winsys3d.Ordinal_67(slot);
    }

    /// <summary>Ghidra: FUN_005047e0 @ 0x005047e0 — réarme les quatre gestionnaires de lecture
    /// (<c>Ordinal_9(DAT_00604b58/5c/60/64)</c>) puis remet le drapeau de statut lecture à 4.</summary>
    private static void ResetCardReadEvents()
    {
        foreach (void* handle in s_CardReadEventHandles_00604b58)
            if (handle != null) Winsys3d.Ordinal_9((int*)handle);
        s_CardReadStatus_00604a00 = 4;
    }

    /// <summary>Ghidra: FUN_00504730 @ 0x00504730 — la sœur écriture de <see cref="ResetCardReadEvents"/>,
    /// sur <c>DAT_00604b68/6c/70/74</c>.</summary>
    private static void ResetCardWriteEvents()
    {
        foreach (void* handle in s_CardWriteEventHandles_00604b68)
            if (handle != null) Winsys3d.Ordinal_9((int*)handle);
        s_CardWriteStatus_00604ab0 = 4;
    }

    /// <summary>
    /// Ghidra: FUN_005046c0 @ 0x005046c0 — sonde le statut d'écriture. Transcrit tel quel, y
    /// compris son risque de boucle infinie si <paramref name="wait"/> est vrai et que le statut
    /// ne bouge jamais : dans ce port, Ordinal_72 rend toujours 0 (winsys3d.dll, FUN_00a02e20,
    /// talon partagé), donc la branche <c>(uVar1 &amp; 1) != 0</c> ne se déclenche jamais — ce qui
    /// est exactement le cas qui boucle si personne d'autre ne change
    /// <see cref="s_CardWriteStatus_00604ab0"/>. Sans conséquence pour le cas 1 (toujours appelé
    /// avec <paramref name="wait"/> à faux) ; à traiter explicitement plutôt que transcrit tel
    /// quel le jour où un appelant passera vrai.
    ///
    /// <para>2026-08-18 : le cas 0x10 appelle bien avec <paramref name="wait"/> à vrai, et il
    /// n'atteint pourtant pas la garde — depuis que les huit gestionnaires
    /// <c>&amp;LAB_00504610..680</c> sont réels, le statut a déjà quitté 4 quand on arrive là, donc
    /// le premier test rend la valeur publiée. La garde ne subsiste que pour le cas réellement
    /// bloquant (statut resté à 4 <em>et</em> attente demandée), où la transcription littérale
    /// figerait le jeu au lieu de nommer le manque.</para>
    /// </summary>
    private static int PollCardWriteStatus(int slot, bool wait)
    {
        if (s_CardWriteStatus_00604ab0 != 4) return s_CardWriteStatus_00604ab0;
        if (!wait) return 4;

        throw new NotImplementedException(
            "PollCardWriteStatus: appelé avec wait=vrai — dans ce port, Ordinal_72 rend toujours " +
            "0 (winsys3d.dll, FUN_00a02e20), donc la boucle d'attente de l'original ne se termine " +
            "jamais ici. Original : FUN_005046c0, boucle sur Winsys3d.Ordinal_72(slot >> 4) jusqu'à ce que " +
            "DAT_00604ab0 change ou 500000 tentatives s'épuisent.");
    }

    /// <summary>Ghidra: FUN_00504770 @ 0x00504770 — la sœur lecture de <see cref="PollCardWriteStatus"/>,
    /// même structure et même risque de boucle infinie transcrit de la même façon.</summary>
    private static int PollCardReadStatus(int slot, bool wait)
    {
        if (s_CardReadStatus_00604a00 != 4) return s_CardReadStatus_00604a00;
        if (!wait) return 4;

        throw new NotImplementedException(
            "PollCardReadStatus: appelé avec wait=vrai — même risque de boucle infinie que " +
            "PollCardWriteStatus, voir sa note. Original : FUN_00504770.");
    }

    /// <summary>
    /// Ghidra: la boucle de dix tentatives de suppression, écrite deux fois à l'identique dans
    /// <c>FUN_0050a910</c> (cas 0xd sur l'entrée choisie dans le navigateur, cas 0x10 sur le
    /// personnage courant) — un même bloc, comme les <c>Retry…CardFile</c> voisins, donc extrait
    /// ici plutôt que recopié.
    ///
    /// <para><b>La polarité surprend et elle est bien celle de l'original.</b> <c>Ordinal_20</c>
    /// (<c>FUN_00a045e0</c>) rend <b>1 quand DeleteFileA a échoué</b> et 0 quand il a réussi — la
    /// boucle relance donc tant que l'appel rend 0, c'est-à-dire tant qu'il supprime. En pratique
    /// la première tentative supprime (0, on continue) et la seconde échoue sur un fichier
    /// désormais absent (1, on sort) : deux appels, puis la branche « fait ». La branche d'échec
    /// exigerait dix suppressions réussies d'affilée, ce qui ne peut pas arriver.</para>
    /// </summary>
    /// <returns><c>true</c> pour la branche « fait » de l'original (<c>iVar4 != 0</c>).</returns>
    private static bool DeleteCardFileRetrying()
    {
        int result = 0;
        for (int attempts = 10; attempts != 0 && result == 0; attempts--)
            result = Winsys3d.Ordinal_20(s_CardPathDescriptor_00604940);
        return result != 0;
    }

    /// <summary>
    /// Ghidra: FUN_00504820 @ 0x00504820 — attend/relit le statut de transfert d'un créneau et le
    /// traduit en résultat de chargement (-3/-2 erreur, 1 « rien en cours »). Les branches 1/2/3
    /// (via <see cref="ResetCardReadEvents"/>/<see cref="ResetCardWriteEvents"/>) ne sont
    /// atteintes que si <see cref="s_CardWriteStatus_00604ab0"/> quitte 4 — provablement jamais le
    /// cas ici puisque rien dans ce port ne crée le worker asynchrone qui le ferait. Transcrites
    /// quand même plutôt qu'omises ; le <c>case 3</c> tombe dans <c>default</c> sans <c>break</c>
    /// dans l'original (pas une coquille — reproduit tel quel).
    /// </summary>
    private static int PollCardTransfer(int slot, bool startTransfer)
    {
        if (startTransfer) ResetCardTransferStatus(slot);

        int status = PollCardWriteStatus(slot, startTransfer);
        if (status == 1) return -3;
        if (status == 2) return -2;
        if (status == 4) return 1;

        if (status == 3)
        {
            ResetCardReadEvents();
            int r = Winsys3d.Ordinal_68(slot);
            while (r == 0) r = Winsys3d.Ordinal_68(slot);
            PollCardReadStatus(slot, true);   // valeur de retour ignorée, fidèle à l'original
        }
        ResetCardWriteEvents();
        int r2 = Winsys3d.Ordinal_69(slot);
        while (r2 == 0) r2 = Winsys3d.Ordinal_69(slot);

        status = PollCardWriteStatus(slot, startTransfer);
        return status switch { 1 => -3, 2 => -2, 3 => -1, 4 => 1, _ => 0 };
    }

    /// <summary>Ghidra: FUN_00504b40 @ 0x00504b40 — ouvre le fichier décrit par le descripteur de
    /// chemin, dix tentatives. Déclaré <c>void</c> par Ghidra, mais le site d'appel utilise sa
    /// valeur de retour (<c>iVar4 = func_0x00504b40(...)</c>) : EAX porte le dernier résultat
    /// d'Ordinal_21 à la sortie, un simple defaut de récupération de type de retour côté
    /// Ghidra.
    ///
    /// <para><c>flags</c> passe directement à Ordinal_21 (<c>FUN_00a04600</c> dans
    /// <c>winsys3d.c</c>), qui calcule <c>disposition = ((flags &amp; 0x200) != 0) + 3</c> — donc
    /// <c>OPEN_EXISTING</c> (3) pour un <c>flags</c> sans le bit 0x200 (cas 6, écriture — le
    /// fichier doit déjà exister), <c>OPEN_ALWAYS</c> (4) avec ce bit (cas 1, création de
    /// secours). Vérifié par test : Ordinal_21 échoue bien avec <c>OPEN_EXISTING</c> sur un
    /// fichier absent et réussit sur le même chemin une fois créé — comportement correct, pas un
    /// défaut de portage.</para></summary>
    private static IntPtr RetryOpenCardFile(void* pathDescriptor, uint flags)
    {
        IntPtr result = (IntPtr)(-1);
        for (int attempts = 10; attempts != 0 && result == (IntPtr)(-1); attempts--)
            result = Winsys3d.Ordinal_21(pathDescriptor, flags);
        return result;
    }

    /// <summary>Ghidra: FUN_00504b70 @ 0x00504b70 — ferme un handle, dix tentatives. Même défaut de
    /// type de retour que <see cref="RetryOpenCardFile"/> ; la valeur n'est utilisée par aucun site
    /// d'appel connu, donc <c>int</c> suffit.</summary>
    private static int RetryCloseCardFile(IntPtr handle)
    {
        int result = -1;
        for (int attempts = 10; attempts != 0 && result == -1; attempts--)
            result = Winsys3d.Ordinal_22(handle);
        return result;
    }

    /// <summary>Ghidra: FUN_00504b90 @ 0x00504b90 — repositionne le curseur de fichier, dix
    /// tentatives. Même défaut de type de retour <c>void</c> que <see cref="RetryOpenCardFile"/>.</summary>
    private static int RetrySeekCardFile(IntPtr handle, int offset, uint method)
    {
        int result = -1;
        for (int attempts = 10; attempts != 0 && result == -1; attempts--)
            result = Winsys3d.Ordinal_23(handle, offset, method);
        return result;
    }

    /// <summary>Ghidra: FUN_00504bc0 @ 0x00504bc0 — repositionne puis lit, en vérifiant que le
    /// nombre d'octets lus correspond à la taille demandée ; dix tentatives globales (une par
    /// couple seek+lecture, pas dix lectures après un seul seek).</summary>
    private static int RetrySeekAndReadCardFile(IntPtr handle, void* buffer, int offset, int size)
    {
        int result = -1;
        int attempts = 10;
        while (true)
        {
            if (result >= 0) return result;
            int seek = RetrySeekCardFile(handle, offset, 0);
            if (seek < 0) return seek;
            int read = (int)Winsys3d.Ordinal_24(handle, buffer, (uint)size) - 1;
            result = read == size - 1 ? read : -1;
            attempts--;
            if (attempts == 0) return result;
        }
    }

    /// <summary>Ghidra: FUN_00504c20 @ 0x00504c20 — la sœur écriture de
    /// <see cref="RetrySeekAndReadCardFile"/>, même structure avec Ordinal_25.</summary>
    private static int RetrySeekAndWriteCardFile(IntPtr handle, void* buffer, int offset, int size)
    {
        int result = -1;
        int attempts = 10;
        while (true)
        {
            if (result >= 0) return result;
            int seek = RetrySeekCardFile(handle, offset, 0);
            if (seek < 0) return seek;
            int written = (int)Winsys3d.Ordinal_25(handle, buffer, (uint)size) - 1;
            result = written == size - 1 ? written : -1;
            attempts--;
            if (attempts == 0) return result;
        }
    }

    // Ghidra: DAT_00562b52 — 134 octets, relus directement dans Legacy/Blaze.exe (décalage fichier
    // 0x161152). Une seule zone de données que FUN_0050a810 adresse avec des formules différentes
    // selon la classe de caractère : table directe pour la ponctuation/l'espace (indices 0..0x2d),
    // puis à partir de l'indice 0x2e un triplet {code Shift-JIS de base, base ASCII} de 4 octets
    // par catégorie alphanumérique (chiffres/majuscules/minuscules) — Ghidra nomme ces deux champs
    // DAT_00562b80/DAT_00562b82, mais ce sont des décalages dans ce même tableau, pas des globales
    // séparées.
    private static readonly byte[] s_NameEncodeTable_00562b52 =
    [
        0x49,0x00,0x0b,0x00,0x43,0x00,0x23,0x00,0x43,0x00,0x2a,0x00,0x49,0x00,0x38,0x00,
        0x49,0x00,0x3f,0x00,0x43,0x00,0xff,0xff,0xff,0xff,0x00,0x00,0x00,0x00,0x08,0x02,
        0x00,0x04,0x0a,0x02,0x04,0x06,0x09,0x03,0x0f,0x0d,0x01,0x03,0x05,0x07,0x82,0x4f,
        0x30,0x00,0x82,0x60,0x41,0x00,0x82,0x81,0x61,0x00,0x00,0x00,0x00,0x00,0x81,0x40,
        0x81,0x49,0x81,0x68,0x81,0x94,0x81,0x90,0x81,0x93,0x81,0x95,0x81,0x66,0x81,0x69,
        0x81,0x6a,0x81,0x96,0x81,0x7b,0x81,0x43,0x81,0x7c,0x81,0x44,0x81,0x5e,0x81,0x46,
        0x81,0x47,0x81,0x71,0x81,0x81,0x81,0x72,0x81,0x48,0x81,0x97,0x81,0x6d,0x81,0x8f,
        0x81,0x6e,0x81,0x4f,0x81,0x51,0x81,0x65,0x81,0x6f,0x81,0x62,0x81,0x70,0x81,0x50,
        0x00,0x00,0x00,0x00,0x00,0x00,
    ];

    // Ghidra: DAT_00562bd8 — 62 octets étendus (accents latin-1, 0xC0-0xFF), relevés au même
    // décalage. Terminés par '.' dans l'original (utilisé comme sentinelle « table vide » quand le
    // premier octet est déjà '.') ; pas reproduit ici puisque la table est réelle et non vide.
    private static readonly byte[] s_AccentSourceTable_00562bd8 =
    [
        0xc0,0xc1,0xc2,0xc3,0xc4,0xc5,0xc6,0xc7,0xc8,0xc9,0xca,0xcb,0xcc,0xcd,0xce,0xcf,
        0xd1,0xd2,0xd3,0xd4,0xd5,0xd6,0x8c,0xd8,0xd9,0xda,0xdb,0xdc,0xdd,0xdf,0x8a,0xe0,
        0xe1,0xe2,0xe3,0xe4,0xe5,0xe6,0xe7,0xe8,0xe9,0xea,0xeb,0xec,0xed,0xee,0xef,0xf1,
        0xf2,0xf3,0xf4,0xf5,0xf6,0x9c,0xf8,0xf9,0xfa,0xfb,0xfc,0xfd,0xff,0x9a,
    ];

    // Ghidra: DAT_00562c18 — les 62 codes Shift-JIS correspondants, un par entrée de la table
    // ci-dessus, même ordre.
    private static readonly ushort[] s_AccentTargetTable_00562c18 =
    [
        0x6082,0x6082,0x6082,0x6082,0x6082,0x6082,0x6082,0x6282,0x6482,0x6482,0x6482,0x6482,
        0x6882,0x6882,0x6882,0x6882,0x6d82,0x6e82,0x6e82,0x6e82,0x6e82,0x6e82,0x7282,0x6e82,
        0x7482,0x7482,0x7482,0x7482,0x7882,0x9382,0x9382,0x8182,0x8182,0x8182,0x8182,0x8182,
        0x8182,0x8182,0x8382,0x8582,0x8582,0x8582,0x8582,0x8982,0x8982,0x8982,0x8982,0x8e82,
        0x8f82,0x8f82,0x8f82,0x8f82,0x8f82,0x9382,0x8f82,0x9582,0x9582,0x9582,0x9582,0x7882,
        0x7882,0x7282,
    ];

    /// <summary>
    /// Ghidra: FUN_0050a810 @ 0x0050a810 — convertit un octet ASCII/Latin-1 en code Shift-JIS
    /// demi/pleine-largeur pour le champ nom du fichier de sauvegarde. Chiffres/majuscules/
    /// minuscules passent par le triplet {code, base} de <see cref="s_NameEncodeTable_00562b52"/>
    /// indexé par catégorie ; ponctuation/espace par la même table en lecture directe ; les octets
    /// étendus (accents latin-1) par une recherche linéaire dans
    /// <see cref="s_AccentSourceTable_00562bd8"/>/<see cref="s_AccentTargetTable_00562c18"/> ; tout
    /// le reste rend <c>0x8140</c> (l'espace pleine largeur Shift-JIS), en <c>short</c> signé comme
    /// l'original.
    /// </summary>
    private static short NameEncodeChar(byte c)
    {
        int category;
        if (c is >= 0x30 and <= 0x39) category = 0;         // chiffres
        else if (c is >= 0x41 and <= 0x5a) category = 1;    // majuscules
        else if (c is >= 0x61 and <= 0x7a) category = 2;    // minuscules
        else
        {
            int baseOffset;
            if (c is >= 0x7b and <= 0x7e) baseOffset = 0x3f;
            else if (c is >= 0x5b and <= 0x60) baseOffset = 0x25;
            else if (c is >= 0x3a and <= 0x40) baseOffset = 0xb;
            else if (c is >= 0x20 and <= 0x2f) baseOffset = 1;
            else
            {
                for (int i = 0; i < s_AccentSourceTable_00562bd8.Length; i++)
                {
                    if (s_AccentSourceTable_00562bd8[i] == c) return (short)s_AccentTargetTable_00562c18[i];
                }
                return unchecked((short)0x8140);
            }
            return (short)BitConverter.ToUInt16(s_NameEncodeTable_00562b52, (c - baseOffset) * 2);
        }

        // Ghidra: (DAT_00562b82[cat] * 0xff + c) * 0x100 + DAT_00562b80[cat] — DAT_00562b80 à
        // l'indice 0x2e+cat*4, DAT_00562b82 à 0x30+cat*4 (deux octets plus loin).
        int entry = 0x2e + category * 4;
        ushort baseValue = BitConverter.ToUInt16(s_NameEncodeTable_00562b52, entry);
        ushort strideValue = BitConverter.ToUInt16(s_NameEncodeTable_00562b52, entry + 2);
        return (short)((strideValue * 0xff + c) * 0x100 + baseValue);
    }

    /// <summary>
    /// Ghidra: le corps du <c>case 6</c> de <c>FUN_0050a910</c> (construction puis écriture du
    /// fichier de sauvegarde), <c>Legacy/Raw/Blaze/FUN_0050a910.c:198-338</c>. Extrait pour la
    /// lisibilité plutôt que laissé en ligne dans le <c>switch</c> de
    /// <see cref="StepSubState7Phase"/>.
    ///
    /// <para><b>Corrigé le 2026-08-18 — les deux appels à <see cref="Ordinal_684"/> ont bien deux
    /// rectangles différents.</b> La pseudo-C n'en montrait qu'un, réécrit nulle part entre les deux
    /// appels, avec seulement deux adresses de pile distinctes
    /// (<c>&amp;stack0xffffffd4</c> contre <c>&amp;stack0xffffffe0</c>) — et ce port avait choisi de
    /// transcrire la réutilisation, en notant qu'il faudrait y revenir « si l'icône affichée dans un
    /// futur navigateur de sauvegardes sort visiblement fausse ». C'est arrivé : l'écran de
    /// constitution du groupe affichait un aplat à la place de chaque icône.
    ///
    /// <para>Le désassemblage direct (<c>Legacy/Blaze.exe</c>, <c>0x0050B197..0x0050B1E6</c>) montre
    /// la seconde construction, que Ghidra avait entièrement fondue dans la première :</para>
    /// <code>
    /// MOV CL,[0x5E8F14]          ; l'entrée 5 de la table de pages, moitié basse
    /// AND ECX,ESI                ; ESI = 0x10
    /// MOV BL,[EAX+0x150]         ; l'octet d'identité du personnage
    /// AND EDX,0x0F   AND BL,0x03
    /// LEA EDX,[EDI + EDX*4]   SHL EDX,4      MOV [ESP+0x14],DX   ; x
    /// MOV word [ESP+0x18],4                                      ; largeur 4
    /// ADD ECX,EDX             SHL ECX,4      MOV [ESP+0x16],CX   ; y
    /// MOV [ESP+0x1A],SI                                          ; hauteur 0x10
    /// </code>
    /// <para>Largeur 4 et hauteur 0x10 : très exactement les 4 mots de 16 bits sur 16 lignes qu'une
    /// icône 16x16 en 4 bpp occupe, soit les 128 octets que le format de « title frame » attend à
    /// <c>+0x80</c>. L'ancienne transcription n'en écrivait que 32 (un rectangle 0x10x1, la forme de
    /// la CLUT), laissant <c>+0xa0..+0xff</c> à zéro — d'où l'aplat.</para>
    ///
    /// <para><see cref="Ordinal_667"/>, appelé avec <c>0</c> avant chaque capture, est confirmé
    /// no-op dans cette DLL ; quel que soit son rôle dans l'original, il ne change rien ici.</para>
    /// </summary>
    private static void WriteCardSaveFile(int* cardTransferPhase)
    {
        byte* scratch = s_SelectScratch_00573338;
        int count = 0;

        scratch[0] = (byte)'S';
        scratch[1] = (byte)'C';
        scratch[2] = 0x11;
        scratch[3] = 0x03;
        byte* dest = scratch + 4;

        // Ghidra: LabelAt(0x134) — un intitulé fixe copié tel quel dans l'en-tête.
        for (byte* src = LabelAt(0x134); *src != 0; src++, dest++, count++) *dest = *src;

        // Ghidra: jusqu'à 16 caractères du nom du personnage (scratch+0x100), un par un via
        // NameEncodeChar — chacun compte pour 1 dans `count` bien qu'il écrive 2 octets, fidèle à
        // l'original.
        byte* namePtr = scratch + 0x100;
        for (int i = 0; i < 0x10 && *namePtr != 0; i++, namePtr++, dest += 2, count++)
            *(short*)dest = NameEncodeChar(*namePtr);

        // Ghidra: le nom de classe, indexé par scratch[0x150] & 7.
        for (byte* src = LabelAt(0x138 + (scratch[0x150] & 7) * 4); *src != 0; src++, dest++, count++) *dest = *src;

        // Ghidra: complète à 0x40 octets depuis scratch+4 avec des zéros — si les trois chaînes
        // dépassent déjà 0x40 octets, l'original ne tronque pas non plus, transcrit tel quel.
        for (int i = count; i < 0x40; i++) dest[i - count] = 0;

        // Ghidra: 0x1c octets à zéro à partir de +0x44.
        NativeMemory.Clear(scratch + 0x44, 0x1c);

        // Ghidra: les deux captures VRAM→mémoire de la « title frame » PlayStation, avec **deux
        // rectangles distincts** — voir la note de la fonction pour le désassemblage qui l'établit.
        // DAT_005e8f14 et DAT_005e8f16 sont les deux moitiés de l'entrée 5 de
        // s_EntityDecodeTable_005e8f00, pas des globales séparées.
        byte identity = scratch[0x150];

        // La CLUT : seize couleurs sur une ligne, vers +0x60.
        ushort iconPage = *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0x16);
        short* clutRect = stackalloc short[4];
        clutRect[0] = (short)(((uint)identity + (uint)(iconPage & 0x3f)) * 0x10);
        clutRect[1] = (short)(iconPage >> 6);
        clutRect[2] = 0x10;
        clutRect[3] = 1;
        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_684(clutRect, scratch + 0x60);

        // L'icône elle-même : 4 mots de large sur 16 lignes (16x16 en 4 bpp = 128 octets), vers
        // +0x80. La position dépend de l'octet d'identité du personnage : les deux bits bas donnent
        // la colonne, les bits 2-3 la ligne.
        byte iconTile = Mem.s_EntityDecodeTable_005e8f00[0x14];
        short* iconRect = stackalloc short[4];
        iconRect[0] = (short)((((identity & 3) + (iconTile & 0x0f) * 4)) << 4);
        iconRect[1] = (short)((((iconTile & 0x10) + (identity & 0x0c))) << 4);
        iconRect[2] = 4;
        iconRect[3] = 0x10;
        Winsys3d.Ordinal_667(0);
        Winsys3d.Ordinal_684(iconRect, scratch + 0x80);

        // Ghidra: 0x2000 octets à zéro à partir de +0x2000.
        NativeMemory.Clear(scratch + 0x2000, 0x2000);

        // Ghidra: la table de frames d'icône à +0x4000, compactée depuis +0x300 par blocs de
        // 0x280 octets tant que l'octet-drapeau en tête de bloc est non nul ; sinon 16 octets à
        // zéro sans avancer la source (fidèle à l'original — dans ce port, la table source vient du
        // bloc de travail remis à zéro en tête de trame, donc cette boucle dégénère en un simple
        // zérotage de 0x2000 octets tant que rien ne la peuple, ce qui n'est pas encore le cas).
        int sourceOffset = 0;
        byte* frameDest = scratch + 0x4000;
        for (int i = 0; i < 0x200; i++, frameDest += 16)
        {
            if (scratch[sourceOffset + 0x300] == 0)
            {
                NativeMemory.Clear(frameDest, 16);
            }
            else
            {
                Mem.CopyDwords((uint*)(scratch + sourceOffset + 0x300), (uint*)frameDest, 4);
                sourceOffset += 0x280;
            }
        }

        // Ghidra: somme de contrôle signée sur les 0x1ffc premiers octets, stockée juste après.
        int checksum = 0;
        for (int i = 0; i < 0x1ffc; i++) checksum += (sbyte)scratch[i];
        *(int*)(scratch + 0x1ffc) = checksum;

        IntPtr handle = RetryOpenCardFile(s_CardPathDescriptor_00604940, 2);
        int r1 = RetrySeekAndWriteCardFile(handle, scratch, 0, 0x2000);
        int r2 = RetrySeekAndWriteCardFile(handle, scratch + 0x2000, 0x2000, 0x1f00);
        int r3 = RetrySeekAndWriteCardFile(handle, scratch + 0x1f00, 0x3f00, 0x100);
        int r4 = RetrySeekAndWriteCardFile(handle, scratch + 0x4000, 0x4000, 0x2000);
        RetryCloseCardFile(handle);

        if ((r1 | r2 | r3 | r4) < 0)
        {
            BindMenuLabels(LabelAt(0x178), 0, Mem.s_StatusDescriptor_00560728, 0);
            s_CardPhase_00578e0c = 99;
        }
        else
        {
            BindMenuLabels(LabelAt(0x1b8), 0, Mem.s_StatusDescriptor_00560728, 0);
            *cardTransferPhase = 0;
            s_CardPhase_00578e0c = 0x11;
        }
    }
}
