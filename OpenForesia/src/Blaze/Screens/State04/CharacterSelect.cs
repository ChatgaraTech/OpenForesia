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
    // Game state 4 — la sélection des personnages (Ghidra: FUN_005066d0)
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_005066e0 @ 0x005066e0 — le chargement des ressources de l'écran. Même entame que
    /// <see cref="LoadMenuResources"/> — l'entrée de l'état puis la relocalisation de son en-tête —
    /// suivie de **quatre** listes du répertoire au lieu d'une :
    ///
    /// <list type="bullet">
    /// <item><c>+0x10</c> : les TIM, décodées vers la VRAM émulée, comme partout ailleurs.</item>
    /// <item><c>+0x2c</c> : des paquets de modèles, relocalisés puis repaginés.</item>
    /// <item><c>+0x7c</c> : des paquets d'animations, relocalisés.</item>
    /// <item><c>+0xb0</c> : des blocs bruts, chargés bout à bout tous les 0x2000 octets.</item>
    /// </list>
    ///
    /// Écart assumé avec l'original : les gardes <c>null</c> sur le répertoire et sur
    /// <c>MainMem</c> ci-dessous sont un ajout. <c>FUN_005066e0</c> déréférence les deux sans filet.
    ///
    /// Écart assumé n°2 : <c>FUN_0040a600()</c>, appelé entre la mise à 1 des drapeaux et le
    /// chargement de l'en-tête, n'est pas transcrit. Son corps décompilé est **vide**
    /// (<c>Blaze.c:4319</c>), donc l'omission est comportementalement neutre — mais la signature
    /// que Ghidra lui recompose est fausse, puisque d'autres sites l'appellent avec des arguments
    /// (<c>FUN_0040a600(0x25)</c>, <c>FUN_0040a600(4)</c>). Vraisemblablement un stub libgpu vidé
    /// au portage depuis la PlayStation.
    /// </summary>
    private static void LoadCharacterSelectResources()
    {
        for (int i = 0; i < 9; i++) s_SelectState_00578de8[i] = 0;
        for (int i = 0; i < 3; i++) s_SelectFlags_00562a48[i] = 1;

        // ECHAFAUDAGE TEMPORAIRE : OPENFORESIA_FORCE_SUBSTATE=N saute directement au sous-état N
        // de la sélection de personnage, pour tester un sous-état sans traverser ceux qui le
        // précèdent (utile tant qu'un blocage plus tôt dans la chaîne empêche d'y arriver
        // normalement, par ex. les cinq appels non reconstruits de FinalizeCharacterStats).
        if (int.TryParse(Environment.GetEnvironmentVariable("OPENFORESIA_FORCE_SUBSTATE"), out int forced))
        {
            s_SelectState_00578de8[0] = forced;
        }

        s_Unknown_005e97c8 = Res.LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, Mem.s_MainResourceDirectory_005e97c4);
        Res.FixupResourceHeader(Mem.s_MainResourceDirectory_005e97c4);

        byte* directoryBase = Mem.s_MainResourceDirectory_005e97c4;
        byte* mainMem = GetMainMemPtr();
        if (directoryBase == null || mainMem == null) return;

        // Ghidra: la liste +0x10 — chaque entrée est chargée dans le tampon de travail puis décodée
        // dans son emplacement de la table de pages.
        int* textures = *(int**)(directoryBase + 0x10);
        if (textures != null)
        {
            byte* scratch = mainMem + 0x1b0000;
            byte* slot = Mem.s_EntityDecodeTable_005e8f00;
            for (int* entry = textures; *entry != 0; entry += 2, slot += 4)
            {
                Res.LoadResourceEntry((int*)(nint)(*entry), scratch);
                Res.DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
            }
        }

        // Ghidra: la liste +0x2c — les paquets de modèles. Chaque chargement rend la fin du bloc,
        // qui devient la destination du suivant : ils s'empilent sans trou.
        byte* cursor = mainMem + 0x138000;
        int* models = *(int**)(directoryBase + 0x2c);
        if (models != null)
        {
            byte** table = (byte**)s_ModelTable_005e96a0;
            for (int* entry = models; *entry != 0; entry += 2, table++)
            {
                byte* next = Res.LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateModelBundle((int*)cursor, (byte*)entry, table);
                cursor = next;
            }
        }

        // Ghidra: la liste +0x7c — les paquets d'animations, empilés à la suite des précédents.
        // Le curseur est partagé avec la boucle ci-dessus : si la liste +0x2c était vide, la
        // première animation atterrit bien à MainMem + 0x138000.
        int* animations = *(int**)(directoryBase + 0x7c);
        if (animations != null)
        {
            byte** table = (byte**)Mem.s_AnimationTable_005e9640;
            for (int* entry = animations; *entry != 0; entry++, table++)
            {
                byte* next = Res.LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateAnimationBundle((int*)cursor, table);
                cursor = next;
            }
        }

        // Ghidra: la liste +0xb0 — des blocs bruts, un tous les 0x2000 octets, sans relocalisation.
        int* raw = *(int**)(directoryBase + 0xb0);
        if (raw != null)
        {
            byte* destination = mainMem + 0xf8000;
            for (int* entry = raw; *entry != 0; entry++, destination += 0x2000)
                Res.LoadResourceEntry((int*)(nint)(*entry), destination);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00532640 @ 0x00532640 — un paquet de modèles : un compte, puis autant d'offsets
    /// relatifs suivis d'autant de descripteurs de 8 octets. Chaque offset devient un pointeur
    /// absolu, et chaque entrée reçoit ensuite sa relocalisation propre et sa repagination.
    ///
    /// Le pointeur publié dans <paramref name="result"/> vise le **tableau d'offsets**, pas le
    /// compte : c'est ce qui rend le compte lisible à <c>[-4]</c>, comme le fait le code de rendu
    /// en <c>Blaze.c:79042</c>.
    /// </summary>
    private static int RelocateModelBundle(int* bundle, byte* metadata, byte** result)
    {
        uint pageBits = *(uint*)(metadata + 4);
        int count = bundle[0];
        int* offsets = bundle + 1;
        int* descriptors = bundle + count;

        *result = (byte*)offsets;

        for (int i = count; i != 0; i--, offsets++, descriptors++)
        {
            int absolute = (int)(nint)bundle + *offsets + count * 8 + 4;
            *offsets = absolute;
            descriptors[1] |= (int)pageBits;
            RelocateModelEntry((ushort*)(nint)absolute, (byte*)descriptors);
            AdjustModelTexturePages((byte*)(nint)absolute);
        }

        return count;
    }

    /// <summary>
    /// Ghidra: FUN_00532450 @ 0x00532450 — une entrée de modèle. Trois pointeurs simples, deux
    /// tables d'entiers (l'une de taille variable, l'autre de 0x400) et une liste terminée par -1
    /// dont la longueur est rangée dans l'entrée. Les deux derniers champs sont divisés par trois —
    /// la multiplication par 0x55555556 de Ghidra.
    ///
    /// Cette division par trois est l'un des deux indices qui font de cette structure un modèle et
    /// non un paquet de sprites : trois sommets par triangle. L'autre est la repagination de
    /// <see cref="AdjustModelTexturePages"/>.
    ///
    /// Le <c>0x55555556</c> de Ghidra est l'idiome de division signée par 3, dont la correction de
    /// signe est ici inutile : <c>entry[4]</c> et <c>entry[5]</c> sont des <c>ushort</c>, donc
    /// <c>n + 2</c> est toujours positif et l'idiome se réduit exactement à <c>/ 3</c>.
    ///
    /// Attention à l'échelle des indices : Ghidra type ce paramètre <c>ushort*</c> ici et <c>int</c>
    /// dans <c>FUN_00532530</c>, alors que les deux reçoivent le **même** pointeur. Les décalages
    /// ci-dessous sont donc en <c>ushort</c> (<c>entry + 0x12</c> = octet 0x24), ceux de
    /// <see cref="AdjustModelTexturePages"/> en octets.
    /// </summary>
    private static void RelocateModelEntry(ushort* entry, byte* descriptor)
    {
        byte* baseAddress = (byte*)entry;

        *(int*)(entry + 0x12) += (int)(nint)baseAddress;
        *(uint*)(entry + 0x1e) = *(uint*)(descriptor + 4);
        *(int*)(entry + 0x14) += (int)(nint)baseAddress;
        *(int*)(entry + 0x16) += (int)(nint)baseAddress;

        if (*(int*)(entry + 0x18) != 0)
        {
            int* table = (int*)(nint)(*(int*)(entry + 0x18) + (int)(nint)baseAddress);
            *(int**)(entry + 0x18) = table;
            for (uint n = entry[0]; n != 0; n--, table++) *table += *(int*)(entry + 0x18);
        }

        if (*(int*)(entry + 0x1a) != 0)
        {
            int* table = (int*)(nint)(*(int*)(entry + 0x1a) + (int)(nint)baseAddress);
            *(int**)(entry + 0x1a) = table;
            for (int n = 0x400; n != 0; n--, table++) *table += *(int*)(entry + 0x1a);
        }

        ushort length = 0;
        if (*(int*)(entry + 0x1c) != 0)
        {
            int* list = (int*)(nint)((int)(nint)baseAddress + *(int*)(entry + 0x1c));
            *(int**)(entry + 0x1c) = list;
            for (; *list != -1; list++, length++)
                if (*list != 0) *list += *(int*)(entry + 0x1c);
        }

        entry[7] = length;
        entry[4] = (ushort)((entry[4] + 2) / 3);
        entry[5] = (ushort)((entry[5] + 2) / 3);
    }

    /// <summary>
    /// Ghidra: FUN_00532530 @ 0x00532530 — repagine une entrée : le décalage de page, pris dans les
    /// bits 8..15 du champ 0x3c, est ajouté à deux octets de chaque enregistrement de 16 octets. Le
    /// nombre d'enregistrements et leur origine se lisent dans les huit compteurs de l'en-tête —
    /// quatre à sauter (0x10..0x16), quatre à traiter (0x18..0x1e), un par type de primitive.
    ///
    /// Des enregistrements de 16 octets rangés par type de primitive, dont deux octets portent une
    /// page de texture : c'est la disposition d'un modèle PSX, ce qui confirme le rôle de la table
    /// que <see cref="RelocateModelBundle"/> remplit.
    ///
    /// Le champ 0x3c est celui que <see cref="RelocateModelEntry"/> vient d'écrire depuis le
    /// descripteur, après que <see cref="RelocateModelBundle"/> y a fusionné les bits de page de la
    /// liste. Les trois fonctions se referment sur la même convention.
    /// </summary>
    private static void AdjustModelTexturePages(byte* entry)
    {
        uint adjust = (*(uint*)(entry + 0x3c) >> 8) & 0xc0;

        int count = *(ushort*)(entry + 0x1e) + *(ushort*)(entry + 0x1c)
                  + *(ushort*)(entry + 0x1a) + *(ushort*)(entry + 0x18);
        if (count == 0) return;

        int skipped = *(ushort*)(entry + 0x16) + *(ushort*)(entry + 0x14)
                    + *(ushort*)(entry + 0x12) + *(ushort*)(entry + 0x10);

        uint* record = (uint*)(nint)(skipped * 0x10 + *(int*)(entry + 0x28) + 0xc);
        for (; count != 0; count--, record += 4)
        {
            uint high = record[0];
            uint low = record[-1];
            record[-1] = (((((low >> 0x18) + adjust) & 0xff) << 0x10)
                          | (((low >> 8) + adjust) & 0xff)) << 8 | (low & 0xff00ff);
            record[0] = (((((high >> 0x18) + adjust) & 0xff) << 0x10)
                         | ((((high >> 8) & 0xff) + adjust) & 0xff)) << 8 | (high & 0xff00ff);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005326d0 @ 0x005326d0 — un paquet d'animations : même forme que celui des
    /// modèles, sans descripteurs, et huit pointeurs à relocaliser par entrée.
    ///
    /// Comme pour les modèles, le pointeur publié vise le tableau d'offsets et non le compte, que
    /// <c>Blaze.c:79042</c> relit à <c>[-4]</c>.
    /// </summary>
    private static void RelocateAnimationBundle(int* bundle, byte** result)
    {
        int count = bundle[0];
        int stride = count * 4;
        int* offsets = bundle + 1;

        *result = (byte*)offsets;

        for (; count != 0; count--, offsets++)
        {
            int absolute = (int)(nint)bundle + *offsets + stride + 4;
            *offsets = absolute;
            RelocateAnimationEntry((int*)(nint)absolute);
        }
    }

    /// <summary>Ghidra: FUN_005326b0 @ 0x005326b0 — huit pointeurs, tous relatifs à l'entrée.</summary>
    private static void RelocateAnimationEntry(int* entry)
    {
        int origin = entry[0];
        int* slot = entry + 8;
        for (int i = 8; i != 0; i--, slot++) *slot += (int)(nint)entry + origin;
    }

    // Ghidra: DAT_00578de8..DAT_00578e08 — neuf entiers **contigus**, remis à zéro d'un bloc au
    // début de FUN_005066e0 ; et DAT_00562a48..DAT_00562a50, trois autres mis à 1. Déclarés comme
    // les blocs qu'ils sont, plutôt qu'en douze champs isolés.
    private static readonly int* s_SelectState_00578de8 = (int*)NativeMemory.AllocZeroed(9, sizeof(int));
    private static readonly int* s_SelectFlags_00562a48 = (int*)NativeMemory.AllocZeroed(3, sizeof(int));

    // Ghidra: &DAT_005e96a0 et &DAT_005e9640 — les deux tables de pointeurs que les relocalisations
    // ci-dessus remplissent, une entrée par paquet chargé.
    //
    // Ces tables ne sont **pas** propres à la sélection des personnages, malgré le fait que
    // FUN_005066e0 soit le premier site qui les remplisse à être reconstruit. FUN_005069e0 les
    // remplit exactement de la même manière (Blaze.c:78876), et une trentaine de sites les indexent
    // dans tout Blaze.c — 2043, 14900, 19190, 23424, 25455, 52332, 64765, 79042 — le plus souvent
    // par un octet pris dans une entité (`*(byte*)(param_1 + 4) * 4`). D'où le nom générique.
    //
    // Leur rôle respectif, établi par les sites de lecture :
    //   005e9640 — animations. En Blaze.c:55840 un compteur de frame est incrémenté puis comparé au
    //              champ +0x16 de l'entrée et remis à zéro : c'est la longueur de l'animation.
    //   005e96a0 — modèles. FUN_0051d1f0 reçoit les deux ensemble (Blaze.c:55845) : l'entrée
    //              d'animation indexée par la frame, et le paquet de modèles en regard.
    //
    // Capacité : **16** pointeurs chacune, pas 24. La borne 0x5e96a0 - 0x5e9640 = 0x60 laissait
    // croire à 24 pour la table d'animations, mais elle ignorait que `DAT_005e9680` — les quatre
    // pointeurs de sortie de RelocateAnimationBundle — se loge entre les deux. Corrigé le
    // 2026-08-17 en regroupant la région dans une arène : la table d'animations s'arrête à 0x5e9680,
    // celle des modèles à 0x5e96e0 (la première des huit banques d'état). Les deux bornes tombent
    // juste du même coup, ce qui les confirme l'une l'autre.
    //
    // Conséquence assumée : un index ≥ 16 déborde sur le voisin — exactement comme dans l'original,
    // puisque la disposition est désormais la sienne. TODO(REVERSE) : aucun site connu ne dépasse 15,
    // mais rien ne le garantit.
    //
    // Les deux symboles tombent dans la queue non initialisée de .data, donc il n'y a aucun contenu
    // à récupérer du binaire.
    private const int AssetTableCapacity = 16;
    private static byte* s_ModelTable_005e96a0 => Mem.Arena(0x005e96a0);
    // Deplace dans GameMemory le 2026-08-19 : globale .data lue par Engine/Input.cs.

    /// <summary>
    /// Ghidra: FUN_005066d0 @ 0x005066d0 — le point d'entrée de l'état 4, la sélection des
    /// personnages. Deux moitiés, comme <see cref="RunMainMenuState"/> : le chargement puis la
    /// boucle de frame.
    /// </summary>
    private static void RunCharacterSelectState()
    {
        LoadCharacterSelectResources();   // Ghidra: FUN_005066e0()
        RunCharacterSelectFrameLoop();    // Ghidra: FUN_00506880()
    }

    /// <summary>
    /// Ghidra: FUN_00506880 @ 0x00506880 — la boucle de frame de l'écran. Le prologue ci-dessous est
    /// intégralement déterminable et transcrit tel quel ; la boucle elle-même ne l'est pas encore,
    /// pour deux raisons distinctes qu'il faut garder séparées :
    ///
    /// <para><b>1. Les huit sous-états ne sont pas dans <c>Blaze.c</c>.</b> La boucle répartit
    /// chaque frame par <c>(*(code *)(&amp;PTR_LAB_00560708)[DAT_00578de8])(...)</c>. Les huit
    /// cibles ont été relevées dans <c>Legacy/Blaze.exe</c> (décalage fichier <c>0x15ED08</c>) et
    /// sont déclarées en talons ci-dessous — mais aucun de leurs corps n'a été décompilé : la
    /// décompilation saute de <c>FUN_00506880</c> à <c>FUN_00507870</c>. Rien ne les appelle
    /// directement, seule cette table les désigne, donc Ghidra n'a jamais désassemblé ces régions.
    /// C'est la classe « corps uniquement adressés » : il faut un re-run Ghidra ciblé sur les huit
    /// adresses, pas une relecture de <c>Blaze.c</c>.</para>
    ///
    /// <para><b>2. Ghidra a raté les paramètres de <c>FUN_00506880</c> elle-même.</b> Son corps
    /// utilise <c>unaff_EBX</c>, <c>unaff_EBP</c> et <c>unaff_ESI</c>, et les passe tels quels à
    /// <c>FUN_0051d1f0</c>, <c>FUN_0051be20</c>, <c>FUN_00545340</c>, <c>FUN_00521f20</c> et
    /// d'autres. Ces sites d'appel ne sont pas transcriptibles sans re-run : inventer une signature
    /// violerait la règle du projet.</para>
    ///
    /// <para>Reste, indépendamment du re-run, une liste de fonctions de rendu non reconstruites que
    /// la boucle appelle : <c>FUN_0051d1f0</c> (le rendu de modèle animé), <c>FUN_0051be20</c> /
    /// <c>FUN_0051bdb0</c>, <c>FUN_00521df0</c> / <c>FUN_00521f20</c>, <c>FUN_0051f9c0</c>,
    /// <c>FUN_00545340</c>, <c>FUN_00544350</c>, <c>FUN_00520ae0</c>.</para>
    /// </summary>
    private static void RunCharacterSelectFrameLoop()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);   // Ghidra: FUN_0052a1e0(&local_1c,&local_18,&local_20)

        // Ghidra: Ordinal_379(400); Ordinal_378(0xa0,0x78) — libgte SetGeomScreen / SetGeomOffset.
        // (0xa0, 0x78) = (160, 120), le centre d'un écran 320x240 : la projection est recentrée sur
        // l'écran avant que quoi que ce soit ne soit projeté.
        Winsys3d.Ordinal_379(400);
        Winsys3d.Ordinal_378(0xa0, 0x78);

        PlayMusicTrack(4);   // Ghidra: FUN_00529ad0(4)

        InitialiseCharacterSelectSlots();

        // Ghidra: DAT_00573338 = MainMem + 0xf0000, puis 0x2000 octets remis à zéro d'un bloc.
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "RunCharacterSelectFrameLoop: winsys3d.dll's MainMem export resolves to null, so " +
                "the state's 0x2000-byte scratch block at MainMem + 0xf0000 cannot be placed.");
        }
        NativeMemory.Clear(Mem.s_SelectScratch_00573338, 0x2000);

        Inp.JoystickResetState(false);   // Ghidra: FUN_00504b10(0)

        // Ghidra: la sortie normale passe par `DAT_005de510 = 0` avant LAB_00506fe7 ; la sortie par
        // sous-état -1 y saute directement (`goto LAB_00506fe7`) et laisse donc l'état du jeu
        // intact. Le drapeau reproduit cette différence, que le `goto` de l'original masque.
        bool resetGameState = true;

        while (true)
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

            // Ghidra: FUN_0040a600(4) — corps vide, voir LoadCharacterSelectResources.

            byte* pad = Res.CurrentPadState();

            // Ghidra: le test de sortie. Les deux bits 9 = 0b1001 du champ +2, sauf au sous-état 7
            // tant que DAT_00562b68 est positif.
            if ((pad[2] & 9) == 9
                && (s_SelectState_00578de8[0] != 7 || Mem.s_Unknown_00562b68 < 0))
            {
                break;
            }

            if (s_SelectState_00578de8[1] != s_SelectState_00578de8[0])
            {
                s_SelectState_00578de8[1] = s_SelectState_00578de8[0];
                s_SelectFlags_00562a48[0] = 1;
                s_SelectFlags_00562a48[1] = 1;
            }

            primCursor = InvokeCharacterSelectSubState(
                s_SelectState_00578de8[0], otBase, otEntry, primCursor, pad);

            if (s_SelectState_00578de8[0] == -1)
            {
                resetGameState = false;
                break;
            }

            s_SelectFlags_00562a48[1] = 0;

            // =========================================================================
            // Le bloc « un personnage est en scène », sous-états 1 à 6.
            //
            // Les globales que Ghidra nomme DAT_00572b4c, 52, 60, 62, 64..6c et 70..74 ne sont
            // pas un bloc à part : ce sont les champs de s_SceneEntity_00572b48, à +0x04, +0x0a,
            // +0x18, +0x1a, +0x1c..+0x24 et +0x28..+0x2c. Six sur six correspondent au même
            // enregistrement de 0x9c octets qu'un emplacement du carrousel — ce qui est attendu,
            // puisque la validation y recopie l'emplacement choisi.
            // =========================================================================
            if (s_SelectState_00578de8[0] != 0 && s_SelectState_00578de8[0] < 7)
            {
                byte* entity = Mem.s_SceneEntity_00572b48;
                int* scenePos = (int*)(entity + 0x1c);     // DAT_00572b64 / 68 / 6c
                short* sceneGlow = (short*)(entity + 0x28); // DAT_00572b70 / 72 / 74

                // Les particules et la teinte de fond ne concernent que les sous-états 5 et 6.
                if (s_SelectState_00578de8[0] > 4)
                {
                    // Ghidra: &DAT_00562a58 indexé par l'octet +0x151 du bloc de travail.
                    short* tone = (short*)(Mem.s_SceneToneTable_00562a58
                                           + Mem.s_SelectScratch_00573338[0x151] * GameMemory.SceneToneStride);
                    Mem.s_BackColourR_005e93a0 += (short)((tone[0] - Mem.s_BackColourR_005e93a0) >> 2);
                    Mem.s_BackColourG_005e93a2 += (short)((tone[1] - Mem.s_BackColourG_005e93a2) >> 2);
                    Mem.s_BackColourB_005e93a4 += (short)((tone[2] - Mem.s_BackColourB_005e93a4) >> 2);
                    Winsys3d.Ordinal_376((byte)Mem.s_BackColourR_005e93a0,
                                (byte)Mem.s_BackColourG_005e93a2,
                                (byte)Mem.s_BackColourB_005e93a4);

                    primCursor = EmitSceneParticles(otBase, primCursor);

                    // Ghidra: Ordinal_457 est l'atan2 de libgte. La scène se tourne vers la
                    // première particule, l'angle étant pris entre elle et l'entité.
                    Mem.s_ScenePitch_005e9308 = 0;
                    // DAT_00572c00 et DAT_00572c08 sont le x et le z de la **position** du premier
                    // record, donc sa base + 0x1c.
                    int* firstParticle = (int*)(Mem.s_SceneParticles_00572be4 + 0x1c);
                    short dz = (short)((scenePos[2] - firstParticle[2]) >> 0xc);
                    short dx = (short)((scenePos[0] - firstParticle[0]) >> 0xc);
                    Mem.s_SceneYaw_005e930a = -Winsys3d.Ordinal_457(dx, dz);
                    Mem.s_SceneRoll_005e930c = 0;
                }

                // L'éclat de l'entité remonte vers 0x1e00, sa position retombe vers (0, -0x64000, 0)
                // — toujours par quarts. Sous 0x1000 en valeur absolue, x et z sont collés à zéro
                // pour que l'amortissement finisse net plutôt qu'asymptotiquement.
                sceneGlow[0] += (short)((0x1e00 - sceneGlow[0]) >> 2);
                sceneGlow[1] += (short)((0x1e00 - sceneGlow[1]) >> 2);
                sceneGlow[2] += (short)((0x1e00 - sceneGlow[2]) >> 2);

                scenePos[0] += -scenePos[0] >> 2;
                scenePos[1] += -0x64000 - scenePos[1] >> 2;
                scenePos[2] += -scenePos[2] >> 2;

                if ((scenePos[0] < 0 ? -scenePos[0] : scenePos[0]) < 0x1000) scenePos[0] = 0;
                if ((scenePos[2] < 0 ? -scenePos[2] : scenePos[2]) < 0x1000) scenePos[2] = 0;

                // Avance de frame, exactement comme au carrousel : le compteur boucle sur la
                // longueur de l'animation, lue à +0x16 de son entrée.
                byte assetIndex = entity[0x04];                     // DAT_00572b4c
                short* sceneAnimIndex = (short*)(entity + 0x18);    // DAT_00572b60
                short* sceneFrame = (short*)(entity + 0x1a);        // DAT_00572b62

                byte* bundle = ((byte**)Mem.s_AnimationTable_005e9640)[assetIndex];
                byte* animEntry = *(byte**)(bundle + *sceneAnimIndex * 4);

                short advanced = (short)(*sceneFrame + 1);
                *sceneFrame = advanced;
                if (*(ushort*)(animEntry + 0x16) <= advanced) *sceneFrame = 0;

                // Ghidra: CONCAT22(sVar3 >> 0xf, DAT_00572b52) — la moitié haute est le signe du
                // compteur **avant** bouclage, la basse le champ +0x0a. Même panachage qu'au
                // carrousel, transcrit tel quel.
                uint sceneExtra = (uint)((advanced >> 0xf) << 0x10) | *(ushort*)(entity + 0x0a);

                primCursor = Mdl.DrawAnimatedModel(
                    otBase, primCursor, animEntry,
                    (int*)(((byte**)s_ModelTable_005e96a0)[assetIndex]),
                    scenePos, *sceneFrame, sceneExtra);
            }

            // Ghidra: DAT_006008fc — la translation Z de la matrice de caméra, amortie vers 800.
            Mem.CameraTranslation[2] += (800 - Mem.CameraTranslation[2]) >> 2;

            // Ghidra: le descripteur de sprite de 20 octets, monté sur la pile — un seul bloc
            // contigu, comme s_LoadingScreenSprite. Le champ 0 vient du répertoire de ressources.
            byte* sprite = stackalloc byte[20];
            NativeMemory.Clear(sprite, 20);
            *(uint**)sprite = *(uint**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x94) + 0x30);
            *(uint*)(sprite + 4) = 0x1000000;

            // Ghidra: la rangée de descripteurs d'interface — DAT_00560ff0 et ses suivants, pas
            // 0x8c8 octets. Le compte est DAT_00562a50, que les handlers ajustent (3 au maximum
            // observé). Les coordonnées se lisent dans le descripteur et sont décalées de (0xc, 4).
            for (int i = 0; i < s_SelectFlags_00562a48[2]; i++)
            {
                byte* descriptor = Mem.s_UiDescriptors_00560ff0 + i * GameMemory.UiDescriptorStride;

                *(short*)(sprite + 8) = (short)(*(short*)(descriptor + 4) + 0xc);
                *(short*)(sprite + 0xa) = (short)(*(short*)(descriptor + 6) + 4);
                *(short*)(sprite + 0x10) =
                    (sbyte)Mem.s_SelectSubStateBytes_00562a98[s_SelectState_00578de8[0] * 4 + i];

                UiWin.EmitViewportRect(otBase, &primCursor);                              // FUN_0051be20
                primCursor = Prims.DrawSpriteVariant(otBase, primCursor, (int*)sprite, 0, 0);  // FUN_00521df0
                primCursor = Prims.DrawFlatPrimitive(otBase, primCursor, (int*)sprite); // FUN_0051f9c0
                UiWin.EmitClipRect(otBase, &primCursor);                                  // FUN_0051bdb0
                primCursor = UiWin.DrawUiPanel(otBase, primCursor, descriptor);           // FUN_00545340
            }

            if (s_SmallTextureFlag == 0 && Mem.s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de158 = Mem.s_SizeSetting_005de160 * 0xc - 0x70;
            }
            else if (Mem.s_SizeSetting_005de160 < 1)
            {
                s_Unknown_005de158 -= 8;
            }
            else
            {
                s_Unknown_005de158 = (Mem.s_SizeSetting_005de160 + 9) * 0xc;
            }

            if (s_SelectState_00578de8[2] >= 0)
            {
                // Ghidra: FUN_00544350(directory[0x70][DAT_00578df0], 0, &DAT_00560728, 0)
                BindMenuLabels(
                    *(byte**)(*(byte**)(Mem.s_MainResourceDirectory_005e97c4 + 0x70)
                              + s_SelectState_00578de8[2] * 4),
                    0, Mem.s_StatusDescriptor_00560728, 0);
            }

            primCursor = Ui.RunTextEngine(primCursor, Mem.s_StatusDescriptor_00560728);       // FUN_005443a0
            primCursor = UiWin.DrawUiPanel(otBase, primCursor, Mem.s_StatusDescriptor_00560728);

            s_Unknown_005de15c = 0;

            // Ghidra: le plein écran de fin de frame — (0xa0, 0x78) est le centre, (0x1420, 0x1149)
            // les deux mots de texture, et le champ 0 vient cette fois du premier slot de +0x94.
            *(short*)(sprite + 8) = 0xa0;
            *(short*)(sprite + 0xa) = 0x78;
            *(ushort*)(sprite + 0xc) = 0x1420;
            *(ushort*)(sprite + 0xe) = 0x1149;
            *(short*)(sprite + 0x10) = 0;
            *(uint**)sprite = **(uint***)(Mem.s_MainResourceDirectory_005e97c4 + 0x94);
            *(uint*)(sprite + 4) = 0x1000000;

            primCursor = Prims.DrawSpritePrimitive(otEntry, primCursor, (int*)sprite, 0, 0);
            Prims.DrawFlatPrimitive(otEntry, primCursor, (int*)sprite);

            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        }

        if (resetGameState) s_GameStateId_005de510 = 0;

        // Ghidra: LAB_00506fe7 — les deux sorties se rejoignent ici.
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        s_Unknown_005de15c = 0;
    }
}
