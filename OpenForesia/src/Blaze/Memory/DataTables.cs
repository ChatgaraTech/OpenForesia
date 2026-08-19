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
    // Les huit sous-états de la sélection des personnages
    //
    // Ghidra: PTR_LAB_00560708 @ 0x00560708 — table de huit pointeurs relevée dans
    // Legacy/Blaze.exe au décalage fichier 0x15ED08. FUN_00506880 y répartit chaque frame par
    // DAT_00578de8. Aucun de ces huit corps n'existe dans Blaze.c : seule cette table les désigne,
    // donc Ghidra ne les a jamais désassemblés. Un re-run ciblé sur ces huit adresses les produit
    // d'un coup.
    //
    // Vérifié dans le binaire : les huit sont de vrais prologues de fonction, et **sept sur huit**
    // ouvrent sur la même instruction, `mov eax, [0x00562A48]` — le drapeau « première frame de ce
    // sous-état » que LoadCharacterSelectResources met à 1 et que FUN_00506880 relève à chaque
    // changement de sous-état. Le premier lit `[0x005633A8]` à la place.
    // =====================================================================

    // Ghidra: &DAT_00560708 — la table elle-même, relevée dans Legacy/Blaze.exe au décalage
    // fichier 0x15ED08. Les huit corps sont désormais décompilés et rangés dans
    // Legacy/Raw/Blaze/FUN_<adresse>.c ; il reste à les transcrire.
    private static readonly int[] s_CharacterSelectSubStateAddresses =
    {
        0x00507010, 0x00507cb0, 0x00508410, 0x00508860,
        0x00508e00, 0x00509470, 0x005098f0, 0x0050a910,
    };

    // Ghidra: &DAT_00560ff0 et suivants — la rangée de descripteurs d'interface que la boucle de
    // frame dessine, pas 0x8c8 octets. Trois instances statiques, relevées dans Legacy/Blaze.exe
    // aux décalages 0x15F5F0, 0x15FEB8 et 0x160780 ; FUN_00509470 confirme la troisième en
    // référençant 0x00562180 = 0x00560ff0 + 2 * 0x8c8. Seuls les 0x68 premiers octets de chaque
    // sont initialisés — la queue est nulle dans le fichier, vérifié.
    //
    // Le champ +4 est une paire (x, y) empaquetée : les trois boîtes sont à x = 0x28, 0x78, 0xd0
    // pour un même y = 0x18, et le champ +0x14 vaut leur index 0, 1, 2.
    private const int UiDescriptorStride = 0x8c8;
    private const int UiDescriptorCount = 3;
    // Alloué bas. Ces descripteurs portent leur **propre** ordering table à +0xa4, que
    // ClearOTagR remplit de liens masqués à 0xffffff (voir la marche de liens dans
    // DrawUiPanel) : au-dessus de 0x01000000 cette table se relit sur des adresses tronquées.
    // Le masque de RunTextEngine n'est que le premier symptôme du même problème. Dans
    // l'original la question ne se pose pas, ces structures sont des globales de .data.
    //
    // Paresseux plutôt qu'initialiseur de champ, pour la raison exposée sur
    // s_MenuContext_0056dd58 : un échec d'allocation basse doit rester rattrapable par relance,
    // pas se présenter en TypeInitializationException.
    private static byte* s_UiDescriptorsStorage;
    private static byte* s_UiDescriptors_00560ff0
        => s_UiDescriptorsStorage != null
            ? s_UiDescriptorsStorage
            : (s_UiDescriptorsStorage = BuildUiDescriptors());

    private static byte* BuildUiDescriptors()
    {
        byte* block = AllocLowZeroed((uint)(UiDescriptorCount * UiDescriptorStride));

        // Les 0x68 octets initialisés de chaque instance, relevés dans Legacy/Blaze.exe aux
        // décalages 0x15F5F0, 0x15FEB8 et 0x160780 — 26 dwords, pas 8. Les mots de fonction
        // sont testés par DrawUiPanel en `champ & 0x1000000` : sans f8 (texte) ni f16 (cadre),
        // le panneau se dessine vide, exactement comme le descripteur du menu principal quand il
        // était laissé à zéro.
        //   f5   l'index de la boîte (0, 1, 2)      f12  curseur      — bit 0x1000000 absent
        //   f8   texte        — bit présent         f14  flèches      — bit absent
        //   f16  cadre        — bit présent
        const int UiDescriptorHeaderDwords = 26;
        ReadOnlySpan<uint> headers = new uint[]
        {
            // 0x00560ff0
            0x00000031, 0x00180028, 0x00180048, 0x00000000,
            0x01080000, 0x00000000, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x005618b8
            0x00000031, 0x00180078, 0x00180050, 0x00000000,
            0x01080000, 0x00000001, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x00562180
            0x00000031, 0x001800d0, 0x00180038, 0x00000000,
            0x021a0000, 0x00000002, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f00d0, 0x01000000, 0x000f0a00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
        };

        for (int i = 0; i < UiDescriptorCount; i++)
        {
            uint* dest = (uint*)(block + i * UiDescriptorStride);
            for (int f = 0; f < UiDescriptorHeaderDwords; f++)
            {
                dest[f] = headers[i * UiDescriptorHeaderDwords + f];
            }
        }

        return block;
    }

    // Ghidra: &DAT_00560728 — une neuvième instance du même type, celle que la boucle de frame
    // passe au moteur de texte (FUN_005443a0) puis dessine. Relevée au décalage fichier 0x15ED28.
    //
    // Alloué bas et paresseux pour la même raison que s_UiDescriptors_00560ff0 : il traverse
    // RunTextEngine puis DrawUiPanel, donc il porte une ordering table à +0xa4.
    private static byte* s_StatusDescriptorStorage;
    private static byte* s_StatusDescriptor_00560728
        => s_StatusDescriptorStorage != null
            ? s_StatusDescriptorStorage
            : (s_StatusDescriptorStorage = BuildStatusDescriptor());

    private static byte* BuildStatusDescriptor()
    {
        byte* block = AllocLowZeroed((uint)UiDescriptorStride);
        uint* f = (uint*)block;

        // Les 26 dwords du décalage fichier 0x15ED28, queue comprise : f25 manquait.
        f[0] = 0x00000031; f[1] = 0x00a80028; f[2] = 0x002800e0; f[3] = 0x00000000;
        f[4] = 0x021a0000; f[5] = 0x0000000c; f[6] = 0x010f0808; f[7] = 0x02cf1020;
        f[8] = 0x01070c10; f[9] = 0x01000000; f[10] = 0x00000000; f[11] = 0x01000000;
        f[12] = 0x000f28d4; f[13] = 0x01000000; f[14] = 0x000f0a00; f[15] = 0x01000000;
        f[16] = 0x010f0000; f[17] = 0x01000000; f[19] = 0x01000000; f[21] = 0x01000000;
        f[23] = 0x01000000; f[25] = 0x01000000;

        return block;
    }

    // Ghidra: &DAT_0056b558 et &DAT_0056be20 — **deux** instances du même type, contiguës
    // (0x56b558 + 0x8c8 = 0x56be20), celles de l'écran de chargement de sauvegarde
    // (<c>FUN_00536ea0</c>, l'état de jeu 1 normal). Sa boucle de rendu les parcourt exactement
    // ainsi : <c>do { RunTextEngine; DrawUiPanel; p += 0x8c8; } while (p &lt; 0x56c6e8)</c>, et
    // 0x56c6e8 est bien 0x56b558 + 2 * 0x8c8 — ce qui borne le tableau à deux entrées.
    //
    // Relevées dans Legacy/Blaze.exe : mêmes 26 dwords initialisés que les autres instances, queue
    // nulle. Allouées bas et paresseusement pour la même raison que s_UiDescriptors_00560ff0 : elles
    // traversent RunTextEngine puis DrawUiPanel, donc portent une ordering table à +0xa4.
    private const int LoadScreenDescriptorCount = 2;
    private static byte* s_LoadScreenDescriptorsStorage;
    private static byte* s_LoadScreenDescriptors_0056b558
        => s_LoadScreenDescriptorsStorage != null
            ? s_LoadScreenDescriptorsStorage
            : (s_LoadScreenDescriptorsStorage = BuildLoadScreenDescriptors());

    private static byte* BuildLoadScreenDescriptors()
    {
        byte* block = AllocLowZeroed((uint)(LoadScreenDescriptorCount * UiDescriptorStride));

        ReadOnlySpan<uint> headers = new uint[]
        {
            // 0x0056b558
            0x00000031, 0x00a80028, 0x002800e0, 0x00000000,
            0x021a0000, 0x0000000c, 0x010f0808, 0x02cf1020,
            0x01070c10, 0x01000000, 0x00000000, 0x01000000,
            0x000f28d4, 0x01000000, 0x000f0c00, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
            // 0x0056be20
            0x00000031, 0x00100028, 0x009000e0, 0x00000000,
            0x081a0000, 0x00000004, 0x010f0808, 0x02cf1020,
            0x01071018, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x010f0000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000, 0x00000000, 0x01000000,
            0x00000000, 0x01000000,
        };

        for (int i = 0; i < LoadScreenDescriptorCount; i++)
        {
            uint* dest = (uint*)(block + i * UiDescriptorStride);
            for (int f = 0; f < 26; f++) dest[f] = headers[i * 26 + f];
        }

        return block;
    }

    private static byte* s_LoadScreenDescriptor2_0056be20 => s_LoadScreenDescriptors_0056b558 + UiDescriptorStride;

    // **Huit champs de ces deux descripteurs, jusqu'ici déclarés comme des octets isolés** —
    // la même classe de défaut d'aliasing que les six corrigés le 2026-08-16, trouvée le
    // 2026-08-17 en lisant FUN_00536ea0. Les adresses le disent d'elles-mêmes une fois la taille du
    // descripteur (0x8c8) posée, et la symétrie est parfaite : quatre champs par descripteur, aux
    // mêmes décalages que ceux déjà nommés sur les autres instances
    // (<see cref="s_Unknown_00560763"/> = +0x3b, <see cref="s_MenuSelection_0056e61c"/> = +0x8c4,
    // <see cref="s_Unknown_0056e61f"/> = +0x8c7).
    //
    //   0x56b593 = descripteur 1 + 0x3b     0x56be5b = descripteur 2 + 0x3b
    //   0x56be1c = descripteur 1 + 0x8c4    0x56c6e4 = descripteur 2 + 0x8c4
    //   0x56be1d = descripteur 1 + 0x8c5    0x56c6e5 = descripteur 2 + 0x8c5
    //   0x56be1f = descripteur 1 + 0x8c7    0x56c6e7 = descripteur 2 + 0x8c7
    //
    // Séparés, les huit ne recevaient jamais les écritures réelles de DrawUiPanel/RunTextEngine, et
    // les remises à zéro de RunGameState1Entry tombaient dans le vide (les huit avertissements
    // CS0414 « assigné mais jamais utilisé » qu'ils produisaient en étaient le symptôme visible).
    private static ref byte s_Unknown_0056b593 => ref s_LoadScreenDescriptors_0056b558[0x3b];
    private static ref byte s_Unknown_0056b58b => ref s_LoadScreenDescriptors_0056b558[0x33];
    private static ref byte s_Unknown_0056be1c => ref s_LoadScreenDescriptors_0056b558[0x8c4];
    private static ref byte s_Unknown_0056be1d => ref s_LoadScreenDescriptors_0056b558[0x8c5];
    private static ref byte s_Unknown_0056be1f => ref s_LoadScreenDescriptors_0056b558[0x8c7];
    private static ref byte s_Unknown_0056be5b => ref s_LoadScreenDescriptor2_0056be20[0x3b];
    private static ref byte s_Unknown_0056c6e4 => ref s_LoadScreenDescriptor2_0056be20[0x8c4];
    private static ref byte s_Unknown_0056c6e5 => ref s_LoadScreenDescriptor2_0056be20[0x8c5];
    private static ref byte s_Unknown_0056c6e7 => ref s_LoadScreenDescriptor2_0056be20[0x8c7];

    // Ghidra: &DAT_00562a98 — quatre octets par sous-état, relevés au décalage fichier 0x161098.
    // Seul le troisième varie (0x02 pour les sous-états 0 à 4, 0x03 pour 5 à 7).
    private static readonly byte* s_SelectSubStateBytes_00562a98 = BuildSelectSubStateBytes();

    private static byte* BuildSelectSubStateBytes()
    {
        byte* table = (byte*)NativeMemory.AllocZeroed(8, 4);
        for (int row = 0; row < 8; row++)
        {
            table[row * 4 + 0] = 0x00;
            table[row * 4 + 1] = 0x01;
            table[row * 4 + 2] = (byte)(row < 5 ? 0x02 : 0x03);
            table[row * 4 + 3] = 0x00;
        }
        return table;
    }

    // Ghidra: DAT_00562b68 — le compteur que le test de sortie consulte au sous-état 7. Jamais
    // écrit par le code reconstruit pour l'instant, donc il lit 0.
    private static int s_Unknown_00562b68;

    // Ghidra: &DAT_005606f8 — le descripteur d'étiquette que le carrousel lie avant sa première
    // passe de texte. Seize octets relevés dans Legacy/Blaze.exe au décalage fichier 0x15ECF8 :
    // {0x0000000c, 0, 0x005066c0, 0}. Il précède immédiatement PTR_LAB_00560708.
    //
    // TODO(REVERSE): le troisième champ est un **pointeur de code**, 0x005066c0 — juste avant
    // FUN_005066d0, et absent de Blaze.c comme les huit sous-états l'étaient. Si BindMenuLabels
    // appelle à travers, ça se verra à l'exécution ; il faudra alors un re-run Ghidra ciblé de
    // plus sur cette adresse.
    private static readonly byte* s_Unknown_005606f8 = BuildLabelDescriptor005606f8();

    private static byte* BuildLabelDescriptor005606f8()
    {
        byte* block = (byte*)NativeMemory.AllocZeroed(0x10);
        ((uint*)block)[0] = 0x0000000c;
        ((uint*)block)[1] = 0;
        ((uint*)block)[2] = 0x005066c0;
        ((uint*)block)[3] = 0;
        return block;
    }


    // Ghidra: DAT_005e9308 / 930a / 930c — l'orientation de la scène, en pas de 0x1000 par tour
    // (la convention des tables rsin/rcos de winsys3d, ordinaux 450/451).
    private static int s_ScenePitch_005e9308, s_SceneYaw_005e930a, s_SceneRoll_005e930c;

    // Ghidra: DAT_005e93a0 / 93a2 / 93a4 — la couleur de fond passée à Ordinal_376 (SetBackColor).
    private static int s_BackColourR_005e93a0, s_BackColourG_005e93a2, s_BackColourB_005e93a4;

    // Ghidra: DAT_00604928 — trois shorts contigus, la pose de caméra. Le troisième (+2) est
    // l'angle amorti vers le personnage courant ; FUN_0051c300 consomme le tout.
    private static readonly byte* s_CameraVector_00604928 = (byte*)NativeMemory.AllocZeroed(8);

    // Ghidra: DAT_006008d0 / d4 / d8 — le décalage monde appliqué à la position de l'entité avant
    // projection. Remis à zéro à l'entrée du sous-état 0.
    private static int s_Unknown_006008d0, s_Unknown_006008d4, s_Unknown_006008d8;

    // Ghidra: &DAT_006008e0 — la matrice de caméra, **un seul bloc de 0x20 octets**, exactement la
    // MATRIX du SDK PlayStation : neuf shorts de rotation (0x006008e0..0x006008f1) puis les trois
    // translations à +0x14, +0x18 et +0x1c, que Ghidra nomme DAT_006008f4, DAT_006008f8 et
    // **DAT_006008fc**.
    //
    // Ce dernier point mérite d'être explicite : DAT_006008fc n'est pas une variable isolée. C'est
    // la translation Z de cette matrice, et la boucle de frame l'amortit vers 800 à chaque image
    // (`DAT_006008fc = DAT_006008fc + (800 - DAT_006008fc >> 2)`) — autrement dit elle fait avancer
    // la caméra. Déclarer les trois translations à part de la rotation couperait la structure en
    // deux et FUN_0051d1f0 recopierait une matrice à moitié fausse dans le registre du GTE.
    private static readonly byte* s_CameraMatrix_006008e0 = (byte*)NativeMemory.AllocZeroed(0x20);

    /// <summary>Ghidra: DAT_006008f4 / f8 / fc — la rangée de translation de la matrice ci-dessus.</summary>
    private static int* CameraTranslation => (int*)(s_CameraMatrix_006008e0 + 0x14);

    // Ghidra: &DAT_00578420 — les seize emplacements de personnage du carrousel, pas 0x9c octets
    // (le 0x4e du pas `ushort *` de Ghidra, et le 0x27 de la copie par dwords en case 2). C'est le
    // même pas 0x9c que la table DAT_005e93c8 que CurrentPadState indexe.
    private const int CharacterSlotCount = 16;
    private const int CharacterSlotStride = 0x9c;
    private static readonly byte* s_CharacterSlots_00578420 =
        (byte*)NativeMemory.AllocZeroed(CharacterSlotCount, CharacterSlotStride);

    // Ghidra: &DAT_00572b48 — la destination de la copie faite à la validation : l'emplacement
    // choisi devient l'entité de scène que le sous-état suivant anime.
    //
    // Tout ce que FUN_00506880 nomme DAT_00572b4c / 52 / 60 / 62 / 64..6c / 70..74 tombe dedans :
    // ce sont ses champs +0x04, +0x0a, +0x18, +0x1a, +0x1c..+0x24 et +0x28..+0x2c. Il n'y a donc
    // aucun « bloc de globales 0x00572b40 » à modéliser à côté — c'est le même enregistrement de
    // 0x9c octets qu'un emplacement du carrousel, et les six décalages le confirment un à un.
    private static readonly byte* s_SceneEntity_00572b48 =
        (byte*)NativeMemory.AllocZeroed((nuint)CharacterSlotStride, 1);

    // Ghidra: &DAT_00562a58 — huit teintes de fond, trois shorts et un mot de bourrage chacune,
    // relevées au décalage fichier 0x161058. La table s'arrête net sur DAT_00562a98, huit entrées
    // plus loin, ce qui en donne la taille sans avoir à la deviner.
    private const int SceneToneStride = 8;
    private static readonly byte* s_SceneToneTable_00562a58 = BuildSceneToneTable();

    private static byte* BuildSceneToneTable()
    {
        ReadOnlySpan<short> tones =
        [
            192,  96,   0, 0,      0,  96, 192, 0,
            128,  96,  64, 0,     64, 128,  96, 0,
            192, 192, 192, 0,      0,   0,   0, 0,
            128, 128,  96, 0,     64,  32, 128, 0,
        ];

        byte* block = (byte*)NativeMemory.AllocZeroed(8, SceneToneStride);
        for (int i = 0; i < tones.Length; i++) ((short*)block)[i] = tones[i];
        return block;
    }

    // Ghidra: &DAT_00572be4 — les particules de la scène : quatre enregistrements de 0x9c octets,
    // du même type que l'entité ci-dessus, et **contigus avec elle** (0x00572b48 + 0x9c = 0x00572be4).
    //
    // La base a demandé deux lectures croisées, parce que les deux boucles qui parcourent ce
    // tableau ne le désignent pas au même endroit :
    //   FUN_00506880  part de &DAT_00572c34, pas 0x27 ints, borne 0x572ea4, et travaille sur
    //                 `piVar10 - 0xd` — soit 0x572c00, 0x572c9c, 0x572d38, 0x572dd4 ;
    //   FUN_00508e00  part de &DAT_00572c20, même pas, borne 0x572e90, et recopie 0x27 dwords vers
    //                 `puVar6 - 0xf` — soit 0x572be4, 0x572c80, 0x572d1c, 0x572db8.
    // Les secondes valent les premières moins 0x1c : la boucle de frame manipule le **champ de
    // position** (+0x1c), pas la base. Quatre records dans les deux cas, et la contiguïté avec
    // l'entité confirme le tout.
    private const int SceneParticleCount = 4;
    private const int SceneParticleStride = 0x9c;
    private static readonly byte* s_SceneParticles_00572be4 =
        (byte*)NativeMemory.AllocZeroed(SceneParticleCount, SceneParticleStride);

    // **Corrigé 2026-08-16** : DAT_00560fef, DAT_0056075b, DAT_00560763 ne sont PAS des globales
    // isolées — ce sont des décalages dans le MÊME descripteur que s_StatusDescriptor_00560728
    // (0x560fef-0x560728=0x8c7, 0x56075b-0x560728=0x33, 0x560763-0x560728=0x3b), exactement les
    // champs que BindMenuLabels/RunTextEngine/DrawUiPanel manipulent déjà via ce même pointeur
    // (`descriptor[0x8c7]`, `descriptor[0x33]`, `descriptor[0x3b]`). Déclarées comme trois `byte`
    // séparés, ces trois globales ne partageaient jamais la mémoire du vrai descripteur : chaque
    // appel à BindMenuLabels/DrawUiPanel armait ou effaçait des bits sur une copie fantôme que
    // StepSubState7Phase (et les sous-états 1/3/4/6 sœurs) ne voyait jamais — d'où des boîtes de
    // dialogue dont l'état affiché ne correspondait jamais à ce que la logique de phase lisait.
    // Trouvé par point d'arrêt matériel (x32dbg, Analyse/x32dbg/POINT_ARRET_0x00560758) : voir
    // REVERSE_STATUS.md, section « the generic OK dialog subsystem ».
    private static ref byte s_Unknown_00560fef => ref s_StatusDescriptor_00560728[0x8c7];
    private static ref byte s_Unknown_0056075b => ref s_StatusDescriptor_00560728[0x33];
    private static ref byte s_Unknown_00560763 => ref s_StatusDescriptor_00560728[0x3b];

    // **Corrigé 2026-08-16**, même défaut : DAT_005e909f (confirmGate) est le champ +0x3f d'un
    // tableau, pas une globale isolée — DAT_005e9060 et DAT_005e909f sont la MÊME mémoire
    // contiguë (0x5e909f - 0x5e9060 = 0x3f), confirmé par point d'arrêt matériel
    // (Analyse/x32dbg/POINT_ARRET_0x005E909F) : `DrawUiPanel` (le widget « flèches de défilement »,
    // Blaze.cs plus bas) écrit déjà `s_PanelSelection_005e9060[descriptor[0x8c5]]` au moment de la
    // validation — ce qui, pour tout écran armant `descriptor[0x8c5]=0x3f`, doit retomber
    // exactement sur cet octet. Séparé comme un `byte` isolé, ce champ ne recevait jamais
    // l'écriture réelle du widget.
    private static ref byte s_Unknown_005e909f => ref s_PanelSelection_005e9060[0x3f];

    /// <summary>
    /// Ghidra: FUN_005352e0 @ 0x005352e0 — recopie de <c>count</c> **dwords** de
    /// <c>source</c> vers <c>destination</c>.
    ///
    /// <para>**Confirmé** le 2026-08-17 sur le désassemblage complet
    /// (<c>Legacy/Raw/Blaze/FUN_005352e0.asm</c>, 32 octets en tout) : sortie immédiate si le
    /// compteur est nul, puis une boucle <c>MOV ESI,[EAX] / ADD EAX,4 / MOV [ECX],ESI /
    /// ADD ECX,4 / DEC EDX / JNZ</c>. C'était jusqu'ici une hypothèse déduite des deux sites
    /// d'appel du sous-état 0 (0x27 = 0x9c octets, et 10) ; l'ordre des arguments
    /// (source, destination, nombre) est lui aussi confirmé par les <c>[ESP+4]</c>/<c>[ESP+8]</c>/
    /// <c>[ESP+0xc]</c>.</para>
    /// </summary>
    private static void CopyDwords(uint* source, uint* destination, int count)
    {
        for (int i = 0; i < count; i++) destination[i] = source[i];
    }
}
