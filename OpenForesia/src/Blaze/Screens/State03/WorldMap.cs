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
    /// Ghidra: FUN_00533000 @ 0x00533000 â€” per-frame **palette animation** (CLUT cycling). Walks a
    /// list of animation descriptors and, for each one whose counter has reached its period,
    /// rotates a run of colour entries by one and resets the counter.
    /// <para>
    /// The CLUTs it rotates are the ones <see cref="DecodeResourceChunk"/> mirrors into main video
    /// memory: both use the same address formula, <c>(bank + 0xe10) * 0x200 + MainMem</c>. That
    /// mirror exists precisely so this can animate them â€” the two functions are two halves of one
    /// mechanism, which is worth knowing before either is changed.
    /// </para>
    /// <para>
    /// Each descriptor is one packed dword:
    /// <c>[7:0]</c> CLUT slot within the bank (32 bytes each), <c>[15:8]</c> how many 16-bit colour
    /// entries take part in the rotation, <c>[23:16]</c> the period in frames, <c>[31:24]</c> the
    /// running frame counter. A descriptor array is terminated by a zero word; the list itself is
    /// (descriptor array, bank index) pairs terminated by a null pointer.
    /// </para>
    /// </summary>
    private static void AnimatePalettes(byte* resourceHeader)
    {
        if (resourceHeader == null) return;

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null) return;

        // Ghidra: puVar12 = *(undefined4 **)(param_1 + 100)
        uint** list = *(uint***)(resourceHeader + 0x64);
        if (list == null) return;

        while (true)
        {
            uint* descriptors = list[0];
            if (descriptors == null) break;

            uint bank = (uint)(nuint)list[1];

            uint descriptor = *descriptors;
            while (descriptor != 0)
            {
                // Ghidra: uVar7 = uVar7 + 0x1000000 â€” tick the counter in the top byte.
                descriptor += 0x1000000;
                uint updated = descriptor;

                // Ghidra: counter byte == period byte â€” this descriptor steps this frame.
                if ((byte)(descriptor >> 0x18) == (byte)(descriptor >> 0x10))
                {
                    updated = descriptor & 0xffffff;   // reset the counter, keep the rest

                    // Same CLUT address formula as DecodeResourceChunk's mirror.
                    ushort* clut = (ushort*)(mainMem
                        + (descriptor & 0xff) * 0x20
                        + ((bank & 0xff) + 0xe10) * 0x200);

                    int count = (byte)(updated >> 8);   // entries taking part
                    ushort first = clut[0];

                    // Rotate left by one: entries [1..count-1] move down, the old first goes last.
                    // The original open-codes this as a dword copy plus a 16-bit tail; the loop
                    // below is the same movement, and the count-1 == 0 case leaves the single
                    // entry untouched exactly as the original's guard does.
                    for (int i = 1; i < count; i++)
                    {
                        clut[i - 1] = clut[i];
                    }
                    if (count >= 1) clut[count - 1] = first;
                }

                *descriptors = updated;
                descriptors++;
                descriptor = *descriptors;
            }

            list += 2;
        }
    }
    // =======================================================================================
    // L'etat de jeu 3 — la carte du monde. Ghidra: thunk_FUN_00516020 @ 0x005160d0.
    // =======================================================================================

    /// <summary>Ghidra: DAT_005e97cc et DAT_005e97e8 — les deux pointeurs de fin que le chargeur
    /// empile apres <see cref="s_Unknown_005e97c8"/> : la zone, puis son ecran.</summary>
    private static byte* s_Unknown_005e97cc;
    private static uint s_Unknown_005e97e8;

    /// <summary>Ghidra: _DAT_005e9828 — le nombre d'emplacements de la table de pages occupes a la
    /// fin du chargement de l'etat 3. Part de 0x10, la ou &amp;DAT_005e8f40 commence.</summary>
    private static int s_Unknown_005e9828;

    /// <summary>Ghidra: DAT_005e9760 et DAT_005f7b44 — les longueurs des deux listes <c>+0x48</c>,
    /// celle du repertoire d'etat et celle de la zone.</summary>
    private static byte s_Unknown_005e9760;
    private static byte s_Unknown_005f7b44;

    /// <summary>Ghidra: DAT_005fa584 — le curseur d'empilement laisse par la seconde serie de
    /// paquets de modeles, et DAT_005edec4, le premier octet du dernier paquet de la premiere
    /// serie. TODO(REVERSE) : leurs lecteurs sont dans FUN_00516350/FUN_00516fc0, non transcrites.</summary>
    private static byte* s_Unknown_005fa584;
    private static byte s_Unknown_005edec4;

    /// <summary>Ghidra: &amp;DAT_005fa588 et &amp;DAT_005f7b40 — les deux emplacements de resultat que
    /// <see cref="RelocateModelBundle"/> remplit. **Ils ne sont pas avances d'un paquet a l'autre**,
    /// contrairement a ceux de <see cref="LoadCharacterSelectResources"/> : chaque paquet ecrase le
    /// precedent, et seul le dernier survit. Transcrit tel quel.</summary>
    private static readonly byte* s_ModelResultA_005fa588 = (byte*)NativeMemory.AllocZeroed(4);
    private static readonly byte* s_ModelResultB_005f7b40 = (byte*)NativeMemory.AllocZeroed(4);

    /// <summary>Ghidra: DAT_005645f8 — l'etat du pas de boucle de la carte du monde. La boucle
    /// interne tourne tant qu'il vaut autre chose que -1, -2 ou -3 ; -1 relance le tour exterieur
    /// (donc reinitialise le pas), -2 et -3 rendent la main a RunGameStateTick.</summary>
    private static sbyte s_WorldMapStep_005645f8 = 2;

    /// <summary>Ghidra: &amp;DAT_00564650 — six champs contigus remis a l'etat neuf a chaque tour
    /// exterieur. Releves a 0x00564650 : 0x017c, 0, 0, 0, 0x50, 0x280. Le code y ecrit des zeros
    /// partout sauf le dernier, qui recoit 900. TODO(REVERSE) : leurs roles ne sont pas etablis,
    /// leurs lecteurs sont dans FUN_00516350/FUN_00516fc0.</summary>
    private static readonly byte* s_WorldMapCamera_00564650 = (byte*)NativeMemory.AllocZeroed(0x14);

    /// <summary>Ghidra: &amp;DAT_00566c10 — le rectangle VRAM ou l'image de la carte est envoyee a
    /// chaque image : x 0, y 0x1c0, 0x100 de large, 0x10 de haut. Releve dans Legacy/Blaze.exe.</summary>
    private static readonly short* s_WorldMapUploadRect_00566c10 =
        (short*)AllocBytes([0x00, 0x00, 0xc0, 0x01, 0x00, 0x01, 0x10, 0x00]);

    /// <summary>Ghidra: &amp;DAT_005633b0 — le descripteur d'interface de la carte du monde. Ses 26
    /// dwords d'en-tete sont releves dans Legacy/Blaze.exe. Alloue bas et paresseusement pour la
    /// meme raison que les autres : il traverse RunTextEngine puis DrawUiPanel.</summary>
    private static byte* s_WorldMapDescriptorStorage;
    private static byte* s_WorldMapDescriptor_005633b0
        => s_WorldMapDescriptorStorage != null
            ? s_WorldMapDescriptorStorage
            : (s_WorldMapDescriptorStorage = BuildWorldMapDescriptor());

    private static byte* BuildWorldMapDescriptor()
    {
        byte* block = AllocLowZeroed(GameMemory.UiDescriptorStride);
        uint* a = (uint*)block;
        a[0] = 0x00000031; a[1] = 0xffb00030; a[2] = 0x004000d0; a[3] = 0x00000000;
        a[4] = 0x04180000; a[5] = 0x00000000; a[6] = 0x010f0808; a[7] = 0x02cf1020;
        a[8] = 0x01071010; a[9] = 0x01000000; a[10] = 0x00000000; a[11] = 0x01000000;
        a[12] = 0x010f40c4; a[13] = 0x01000000; a[14] = 0x000f1000; a[15] = 0x01000000;
        a[16] = 0x010f0000; a[17] = 0x01000000; a[19] = 0x01000000; a[21] = 0x01000000;
        a[23] = 0x01000000; a[25] = 0x01000000;
        return block;
    }

    /// <summary>
    /// Ghidra: FUN_00516020 @ 0x00516020 (atteint par <c>thunk_FUN_00516020</c>) — l'etat de jeu 3,
    /// la carte du monde. Deux boucles imbriquees : la sortie de l'interne par -1 relance
    /// l'externe, qui remet le pas et la camera a neuf ; -2 et -3 rendent la main.
    ///
    /// <para>Atteint depuis « Debut du jeu » → constitution du groupe → lancement
    /// (<see cref="RunPartyConstitution"/> pose <c>s_GameStateId = 3</c>), et aussi depuis l'etat 2
    /// qui y avance de lui-meme.</para>
    /// </summary>
    private static void RunWorldMapState()
    {
        LoadWorldMapResources();     // Ghidra: FUN_005160e0
        BindMenuLabels(Mem.s_Unknown_005606f8, 0, s_WorldMapDescriptor_005633b0, 0);

        while (true)
        {
            s_WorldMapStep_005645f8 = 2;
            s_Unknown_00566c88 = 0xff;
            *(short*)(s_WorldMapCamera_00564650 + 0) = 0;
            *(short*)(s_WorldMapCamera_00564650 + 2) = 0;
            *(short*)(s_WorldMapCamera_00564650 + 4) = 0;
            *(short*)(Mem.s_CameraVector_00604928 + 0) = 0;
            *(short*)(Mem.s_CameraVector_00604928 + 2) = 0;
            *(int*)(Mem.s_CameraVector_00604928 + 4) = 0;
            *(short*)(s_WorldMapCamera_00564650 + 8) = 0;
            *(short*)(s_WorldMapCamera_00564650 + 0xc) = 0;
            *(short*)(s_WorldMapCamera_00564650 + 0x10) = 900;

            while (true)
            {
                LoadWorldMapArea();               // Ghidra: FUN_00516250
                RunWorldMapFrame();               // Ghidra: FUN_00516350
                Winsys3d.ReInitializeAllTextures();
                Winsys3d.Ordinal_674(s_WorldMapUploadRect_00566c10, GetMainMemPtr() + 0x1c2000);
                RunWorldMapTick();                // Ghidra: FUN_00516fc0

                if (s_WorldMapStep_005645f8 == -1) break;
                if (s_WorldMapStep_005645f8 == -2 || s_WorldMapStep_005645f8 == -3) return;
            }
        }
    }

    /// <summary>
    /// Ghidra: FUN_005160e0 @ 0x005160e0 — le chargement des ressources de l'etat 3. La meme forme
    /// que <see cref="LoadCharacterSelectResources"/>, sur d'autres listes : <c>+0x10</c> les
    /// textures (decodees a partir de l'emplacement 0x10 de la table de pages, d'ou le compteur qui
    /// part de 0x10), <c>+0xac</c> puis <c>+0x24</c> deux series de paquets de modeles empilees
    /// bout a bout, et <c>+0x48</c> qui n'est que comptee.
    ///
    /// <para>Ghidra donne a cette fonction un parametre <c>__fastcall</c> qu'elle n'a pas : le
    /// <c>param_1</c> dont il lit <c>DAT_005edec4 = *param_1</c> est en realite la destination du
    /// **dernier** paquet de la serie <c>+0xac</c>, reaffectee dans la boucle. Si la liste est
    /// vide, l'original lit ECX tel quel — un vrai indefini, laisse ici sur le curseur de depart.</para>
    /// </summary>
    private static void LoadWorldMapResources()
    {
        // Ghidra: FUN_0040a600() — corps vide.
        s_Unknown_005e97c8 = LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, Mem.s_MainResourceDirectory_005e97c4);
        FixupResourceHeader(Mem.s_MainResourceDirectory_005e97c4);

        byte* directoryBase = Mem.s_MainResourceDirectory_005e97c4;
        byte* mainMem = GetMainMemPtr();
        if (directoryBase == null || mainMem == null) return;

        int slotCount = 0x10;
        int* textures = *(int**)(directoryBase + 0x10);
        if (textures != null)
        {
            byte* scratch = mainMem + 0x1b0000;
            byte* slot = Mem.s_EntityDecodeTable_005e8f00 + 0x40;   // Ghidra: &DAT_005e8f40
            for (int* entry = textures; *entry != 0; entry += 2, slot += 4, slotCount++)
            {
                LoadResourceEntry((int*)(nint)(*entry), scratch);
                DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
            }
        }

        s_Unknown_005e97e8 = s_Unknown_005e97e4;
        s_Unknown_005e9828 = slotCount;

        byte* cursor = mainMem + 0x138000;
        byte* lastBundle = cursor;

        int* bundlesA = *(int**)(directoryBase + 0xac);
        if (bundlesA != null)
        {
            for (int* entry = bundlesA; *entry != 0; entry += 2)
            {
                byte* start = cursor;
                cursor = LoadResourceEntry((int*)(nint)(*entry), start);
                RelocateModelBundle((int*)start, (byte*)entry, (byte**)s_ModelResultA_005fa588);
                lastBundle = start;
            }
        }
        s_Unknown_005edec4 = *lastBundle;

        int* bundlesB = *(int**)(directoryBase + 0x24);
        if (bundlesB != null)
        {
            for (int* entry = bundlesB; *entry != 0; entry += 2)
            {
                byte* start = cursor;
                cursor = LoadResourceEntry((int*)(nint)(*entry), start);
                RelocateModelBundle((int*)start, (byte*)entry, (byte**)s_ModelResultB_005f7b40);
            }
        }
        s_Unknown_005fa584 = cursor;

        s_Unknown_005e9810 = *(int*)(directoryBase + 100);

        s_Unknown_005e9760 = 0;
        int* areas = *(int**)(directoryBase + 0x48);
        if (areas != null)
            for (int* entry = areas; *entry != 0; entry++) s_Unknown_005e9760++;
    }

    /// <summary>
    /// Ghidra: FUN_00516250 @ 0x00516250 — recharge la zone courante, et **seulement quand elle
    /// change** : la garde compare <c>DAT_00566c84</c> a <c>DAT_005de528</c>. Le repertoire de la
    /// zone est empile juste apres celui de l'etat, et ses textures se decodent a partir de
    /// l'emplacement 0x18 de la table de pages (<c>&amp;DAT_005e8f60</c>).
    /// </summary>
    private static void LoadWorldMapArea()
    {
        if (s_Unknown_00566c84 == (byte)s_Unknown_005de528) return;

        s_Unknown_00566c84 = (byte)s_Unknown_005de528;
        s_Unknown_00566c94 = 1;
        RunLoadingScreen();          // Ghidra: FUN_0052a440
        // Ghidra: FUN_0040a600() — corps vide.

        byte* directoryBase = Mem.s_MainResourceDirectory_005e97c4;
        byte* mainMem = GetMainMemPtr();
        if (directoryBase == null || mainMem == null) return;

        int areaEntry = ((int*)(*(byte**)(directoryBase + 0x48)))[(uint)s_Unknown_005de528];
        s_Unknown_005e97cc = LoadResourceEntry((int*)(nint)areaEntry, s_Unknown_005e97c8);
        FixupResourceHeader(s_Unknown_005e97c8);

        int* textures = *(int**)(s_Unknown_005e97c8 + 0x10);
        if (textures != null)
        {
            byte* scratch = mainMem + 0x1b0000;
            byte* slot = Mem.s_EntityDecodeTable_005e8f00 + 0x60;   // Ghidra: &DAT_005e8f60
            for (int* entry = textures; *entry != 0; entry += 2, slot += 4)
            {
                LoadResourceEntry((int*)(nint)(*entry), scratch);
                DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
            }
        }

        s_Unknown_005f7b44 = 0;
        int* list = *(int**)(s_Unknown_005e97c8 + 0x48);
        if (list != null)
            for (int* entry = list; *entry != 0; entry++) s_Unknown_005f7b44++;

        // La destination est la **sentinelle 2**, pas un tampon : LoadResourceEntry la reconnait et
        // installe la table de sauts de la zone au lieu de lire des octets.
        LoadResourceEntry(*(int**)(*(byte**)(s_Unknown_005e97c8 + 0x6c)), (byte*)s_OverrideDestB_00604930);

        // Ghidra: `(*(code *)PTR_FUN_0056b4f0)()`. 0x0056b4f0 est l'entree **8** de la table de
        // sauts (0x0056b4d0 + 8 * 4), celle que l'installation vient de renseigner.
        CallAreaHandler(8);
    }

    /// <summary>Ghidra: &amp;DAT_005e8520 — les 0x88 octets d'en-tete du banc de sons de la zone.</summary>
    private static readonly byte* s_SoundBankHeader_005e8520 = (byte*)NativeMemory.AllocZeroed(0x88);

    /// <summary>Ghidra: &amp;DAT_00566c70 — le rectangle de vue passe par adresse a Ordinal_770 et
    /// Ordinal_766, d'ou le bloc natif plutot que des champs.</summary>
    private static readonly byte* s_ViewRect_00566c70 = (byte*)NativeMemory.AllocZeroed(0x10);

    /// <summary>Ghidra: &amp;DAT_005dbc68 — **quatre** descripteurs d'interface consecutifs, montes
    /// en fin de FUN_00516350. Le pas de la boucle est 0x464 mots, soit 0x8c8 octets : la meme
    /// taille de descripteur que partout ailleurs, ce qui identifie le bloc sans ambiguite.</summary>
    private static byte* s_WorldPanelsStorage;
    private static byte* s_WorldPanels_005dbc68
        => s_WorldPanelsStorage != null
            ? s_WorldPanelsStorage
            : (s_WorldPanelsStorage = AllocLowZeroed(4 * GameMemory.UiDescriptorStride));

    // Ghidra: les scalaires isoles que FUN_00516350 touche hors arene.
    private static int s_Unknown_005645fc, s_Unknown_00564540;
    private static byte* s_Unknown_005f2ce0;
    private static int s_Unknown_005de534, s_Unknown_005de54c;

    /// <summary>Ghidra: _DAT_005e982c et _DAT_005e97ec — le compte d'emplacements de la table de
    /// pages occupes par la sous-zone, et la copie du pas sauvegarde.</summary>
    private static int s_Unknown_005e982c;
    private static uint s_Unknown_005e97ec;

    /// <summary>Ghidra: FUN_00532770 @ 0x00532770 — remplit une liste d'index identite : un
    /// en-tete de 16 bits valant <c>(compte &amp; 0xff) &lt;&lt; 8</c>, puis les octets 0, 1, 2 …
    /// jusqu'au compte.</summary>
    private static void BuildIdentityIndexList(byte* list, int count)
    {
        *(ushort*)list = (ushort)((count & 0xff) << 8);
        for (int i = 0; i < count; i++) list[2 + i] = (byte)i;
    }

    /// <summary>Ghidra: FUN_005352c0 @ 0x005352c0 — met a zero le premier dword de
    /// <paramref name="count"/> enregistrements consecutifs de 0x9c octets.</summary>
    private static void ClearEntitySlots(byte* first, int count)
    {
        for (int i = 0; i < count; i++) *(uint*)(first + i * 0x9c) = 0;
    }

    /// <summary>Ghidra: FUN_00535050 @ 0x00535050 — recopie les quatre references de
    /// <paramref name="source"/> (+0x10, des shorts) dans <paramref name="slot"/> (+0x80, des
    /// dwords), en y joignant le mot lu dans la table du premier morceau charge. Un index negatif
    /// donne le marqueur <c>0xa0000000</c>.</summary>
    private static void CopyEntityReferences(byte* slot, byte* source)
    {
        byte* table = *(byte**)(Mem.Entity(0x005f7bc0)) + 0x28;
        table = *(byte**)table;
        for (int i = 0; i < 4; i++)
        {
            short reference = *(short*)(source + 0x10 + i * 2);
            *(uint*)(slot + 0x80 + i * 4) = reference < 0
                ? 0xa0000000u
                : (uint)((*(ushort*)(table + reference * 0x10 + 0xc) << 16) | (ushort)reference);
        }
    }

    /// <summary>
    /// Ghidra: FUN_005355f0 @ 0x005355f0 — l'envoi en VRAM des blocs bruts de la liste
    /// <c>+0xb0</c> : par entree, une palette de seize couleurs et une image 24x24, puis 0x50
    /// dwords recopies a la suite dans le tampon de la sous-zone.
    ///
    /// <para><b>Deux defauts de la pseudo-C, tous deux leves au desassemblage.</b> Ghidra rend la
    /// borne de boucle <c>0x180006</c> ; a <c>0x00535619</c> c'est <c>TEST EAX,EAX / JLE</c> sur le
    /// compte lu en tete du bloc — la borne est le compte, la constante est un artefact. Et son
    /// second rectangle, <c>&amp;stack0xfffffff0</c>, est simplement **le meme emplacement de pile
    /// que le premier**, reecrit entre les deux appels : les deux <c>PUSH</c> intercales decalent
    /// les <c>[ESP+n]</c> de huit octets, ce qui a fait croire a deux adresses distinctes. Ses
    /// champs sont montes a <c>0x00535671</c>..<c>0x005356B5</c>.</para>
    /// </summary>
    private static void UploadRawAreaBlocks(int* block)
    {
        byte* dest = s_Unknown_005e97d0;
        *(byte**)Mem.Entity(0x00600824) = dest;

        int count = block[0];
        byte* baseAddress = (byte*)(block + 1) + count * 4;
        int* entry = block + 1;

        short* rect = stackalloc short[4];

        for (int i = 0; i < count; i++, entry++)
        {
            byte* chunk = baseAddress + *entry;
            *entry = (int)(nint)chunk;   // l'offset relatif devient l'adresse absolue

            // La palette : seize couleurs sur une ligne, rangee 0x1dc + i/16.
            rect[0] = (short)((i & 0xf) << 4);
            rect[1] = (short)((i >> 4) + 0x1dc);
            rect[2] = 0x10;
            rect[3] = 1;
            Winsys3d.Ordinal_674(rect, chunk + 0x140);

            // L'image : 24x24 en 4 bits, donc six mots de large, grille de dix colonnes.
            rect[0] = (short)((i % 10) * 6 + 0x180);
            rect[1] = (short)((i / 10) * 0x18 + 0x160);
            rect[2] = 6;
            rect[3] = 0x18;
            Winsys3d.Ordinal_674(rect, chunk + 0x160);

            for (int k = 0; k < 0x50; k++, chunk += 4, dest += 4) *(uint*)dest = *(uint*)chunk;
        }

        s_Unknown_005e97d4 = dest;
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xa4) = 0x216;
        *(ushort*)(Mem.s_EntityDecodeTable_005e8f00 + 0xa6) = 0x7700;
    }

    /// <summary>Ghidra: _DAT_005e97d4 — le curseur laisse par l'envoi des blocs bruts.</summary>
    private static byte* s_Unknown_005e97d4;

    /// <summary>
    /// Ghidra: FUN_005330d0 @ 0x005330d0 — « sur quelle face du terrain cette position tombe-t-elle,
    /// et cette face est-elle franchissable ? ». Elle parcourt la liste de faces du morceau et, pour
    /// chacune, teste le point (x, z) de l'entite par trois produits en croix dans le plan
    /// horizontal ; les faces a quatre sommets ajoutent un quatrieme test.
    ///
    /// <para>Une face retenue voit sa normale calculee dans le banc de registres GTE — les deux
    /// aretes dans les vecteurs 799 et 800, leur produit vectoriel dans 801 — et un dernier produit
    /// scalaire decide de quel cote du plan l'entite se trouve. Le numero de face et ses attributs
    /// sont alors ecrits dans l'entite a <c>+0x80 + type * 4</c>, et le compteur rendu ne compte que
    /// les faces dont les attributs ne rencontrent pas <paramref name="mask"/> — c'est ce compteur
    /// que l'appelant boucle jusqu'a obtenir non nul.</para>
    ///
    /// <para>La disposition d'un sommet, deduite des acces : huit octets, <c>x</c> et <c>y</c> dans
    /// le dword de tete (bas et haut), <c>z</c> dans le short de <c>+4</c>. Une face fait seize
    /// octets : trois index de sommet repartis sur les deux premiers dwords, un index de matiere
    /// au troisieme, et les attributs au quatrieme.</para>
    /// </summary>
    private static int TryPlaceEntity(byte* chunk, uint* slot, ushort mask)
    {
        EnsureGteRegisters();

        byte* materials = *(byte**)(chunk + 0x24);
        uint* face = *(uint**)(chunk + 0x28);
        byte* vertices = chunk + 0x40;

        int px = *(int*)((byte*)slot + 0x1c) >> 0xc;
        int py = *(int*)((byte*)slot + 0x20);
        int pz = *(int*)((byte*)slot + 0x24) >> 0xc;

        int index = 0;
        int accepted = 0;
        int faceCount = *(short*)(chunk + 4);
        if (faceCount == 0) return 0;

        int* vec799 = (int*)s_GteVec799;
        int* vec800 = (int*)s_GteVec800;
        int* mac801 = (int*)s_GteMac801;
        short* ir813 = (short*)s_GteIr813;

        do
        {
            uint attributes = face[3];

            // Seules les faces dont la matiere porte un short negatif participent au sol.
            if (*(short*)(materials + 2 + (ushort)face[2] * 8) < 0)
            {
                uint i2 = face[0] >> 0x10;
                uint i1 = face[1] & 0xffff;
                uint i0 = face[0] & 0xffff;

                int v2 = *(int*)(vertices + i2 * 8);
                int v1 = *(int*)(vertices + i1 * 8);
                int v0 = *(int*)(vertices + i0 * 8);

                int x2 = (short)v2;
                int x0 = (short)v0;
                int z0 = *(short*)(vertices + i0 * 8 + 4);
                int z2 = *(short*)(vertices + i2 * 8 + 4);

                if ((pz - z0) * (x2 - x0) - (px - x0) * (z2 - z0) >= 0)
                {
                    int x1 = (short)v1;
                    int z1 = *(short*)(vertices + i1 * 8 + 4);

                    if ((pz - z2) * (x1 - x2) - (px - x2) * (z1 - z2) >= 0)
                    {
                        int third;
                        if ((attributes & 1) == 0)
                        {
                            third = (pz - z1) * (x0 - x1) - (px - x1) * (z0 - z1);
                        }
                        else
                        {
                            // Face a quatre sommets : le quatrieme index vit dans le haut du
                            // second dword, et il faut deux tests au lieu d'un.
                            short* v3 = (short*)(vertices + (face[1] >> 0x10) * 8);
                            int z3 = v3[2];
                            int x3 = v3[0];
                            if ((pz - z1) * (x3 - x1) - (px - x1) * (z3 - z1) < 0) goto next;
                            third = (pz - z3) * (x0 - x3) - (px - x3) * (z0 - z3);
                        }

                        if (third >= 0)
                        {
                            int y0 = v0 >> 0x10;

                            vec800[0] = x2 - x0;
                            vec800[1] = (v2 >> 0x10) - y0;
                            vec800[2] = z2 - z0;

                            vec799[0] = x1 - x0;
                            vec799[1] = (v1 >> 0x10) - y0;
                            vec799[2] = z1 - z0;

                            mac801[0] = vec799[2] * vec800[1] - vec799[1] * vec800[2];
                            mac801[1] = vec800[2] * vec799[0] - vec799[2] * vec800[0];
                            mac801[2] = vec799[1] * vec800[0] - vec800[1] * vec799[0];

                            ir813[0] = (short)mac801[0];
                            ir813[1] = (short)mac801[1];
                            ir813[2] = (short)mac801[2];

                            if (((-py >> 0xc) - y0) * mac801[1]
                                + (pz - z0) * mac801[2]
                                + (px - x0) * mac801[0] >= 0)
                            {
                                byte kind = *((byte*)face + 0xe);
                                *(ushort*)((byte*)slot + 0x80 + kind * 4) = (ushort)index;
                                *(short*)((byte*)slot + 0x82 + kind * 4) = (short)face[3];
                                if ((mask & (ushort)attributes) == 0) accepted++;
                            }
                        }
                    }
                }
            }

        next:
            index++;
            face += 4;
        }
        while (index < *(ushort*)(chunk + 4));

        return accepted;
    }

    // Ghidra: DAT_00563c77 et DAT_00563c5c — deux octets remis a zero en toute fin de
    // FUN_00516350. TODO(REVERSE) : leurs lecteurs sont dans FUN_00516fc0, non transcrite.
    private static byte s_Unknown_00563c77, s_Unknown_00563c5c;

    /// <summary>
    /// Ghidra: FUN_00516350 @ 0x00516350 — la mise en place d'image de la carte du monde. Trois
    /// temps : le nettoyage des listes d'entites, le rechargement de la sous-zone quand elle change
    /// (textures, morceaux, modeles, animations, banc de sons), puis le placement du groupe et la
    /// derivation de tout l'etat de vue.
    /// </summary>
    private static void RunWorldMapFrame()
    {
        byte* mainMem = GetMainMemPtr();

        // Ghidra: une vidange d'entree de manette, seulement pour certaines combinaisons de
        // sous-zone — la meme forme que les vidanges du menu objets, et le meme besoin de rendre
        // la main a la pompe de messages (voir ItemMenuRootScreen).
        if (s_Unknown_005de528 == 0
            && (((s_Unknown_00566c88 & 7) == 0 && (s_Unknown_005de52c & 7) < 6 && (s_Unknown_005de52c & 7) > 2)
             || ((s_Unknown_00566c88 & 7) != 0 && (s_Unknown_00566c88 & 7) < 5 && (s_Unknown_005de52c & 7) == 0)))
        {
            while (InputDrainStub() != 0) { Winsys3d.Ordinal_132(0); }
        }

        BuildIdentityIndexList(Mem.Entity(0x005f2d00), 0x20);
        BuildIdentityIndexList(Mem.Entity(0x005f7b60), 0x40);
        BuildIdentityIndexList(Mem.Entity(0x005fa5a0), 0x20);
        BuildIdentityIndexList(Mem.Entity(0x006007a0), 0x80);
        BuildIdentityIndexList(Mem.Entity(0x005f7ce0), 0x80);
        BuildIdentityIndexList(Mem.Arena(0x005e99e0), 0x40);

        ClearEntitySlots(Mem.Entity(0x005fa5e0), 0x80);
        ClearEntitySlots(Mem.Entity(0x005ecaa0), 0x20);
        ClearEntitySlots(Mem.Entity(0x005f7de0), 0x40);
        ClearEntitySlots(Mem.Entity(0x005ff400), 0x20);
        ClearEntitySlots(Mem.Entity(0x005edee0), 0x80);
        ClearEntitySlots(Mem.Entity(0x005f2d40), 0x80);

        // Ghidra: 0x40 octets isoles, un tous les 0x28, depuis &DAT_005e9b87.
        for (int i = 0; i < 0x40; i++) *Mem.Arena(0x005e9b87 + i * 0x28) = 0;

        if (s_Unknown_00566c88 != (byte)s_Unknown_005de52c)
        {
            LoadWorldMapSubArea(mainMem);
            s_Unknown_00566c8c = 0xff;
        }

        // Ghidra: 8 dwords a zero depuis &DAT_00604a2c, par pas de 0x10 — un champ par
        // emplacement de manette.
        for (int i = 0; i < 8; i++) *(uint*)(Inp.s_InputStateBase_00604a20 + 0xc + i * 0x10) = 0;

        uint* otBase = null, otEntry = null, primCursor = null;
        FrameBegin(&otBase, &otEntry, &primCursor);   // Ghidra: FUN_0052a1e0

        PlaceWorldMapParty(mainMem);
    }

    /// <summary>
    /// Ghidra: le bloc <c>if (DAT_00566c88 != DAT_005de52c)</c> de <c>FUN_00516350</c> — le
    /// rechargement d'une sous-zone. Il enchaine : le repertoire de la sous-zone, l'installation
    /// des sept tables de sauts d'entites (la sentinelle C), les textures, le **banc de sons** de
    /// la zone, quatre listes de morceaux, les modeles, les animations, et les blocs bruts.
    /// </summary>
    private static void LoadWorldMapSubArea(byte* mainMem)
    {
        s_Unknown_00566c88 = (byte)s_Unknown_005de52c;
        // Ghidra: FUN_0040a600() — corps vide.

        byte* areaDir = s_Unknown_005e97cc;
        int subEntry = ((int*)(*(byte**)(s_Unknown_005e97c8 + 0x48)))[(uint)(byte)s_Unknown_005de52c];
        s_Unknown_005e97d0 = LoadResourceEntry((int*)(nint)subEntry, areaDir);
        FixupResourceHeader(areaDir);

        // La sentinelle 3 : sept tables de sauts d'entites installees d'un coup.
        LoadResourceEntry(*(int**)(*(byte**)(areaDir + 0x6c)), (byte*)s_OverrideDestC_00600904);

        // Ghidra: `(*(code *)PTR_FUN_0056afc8)()` — l'entree **0** de la table 0056afc8, qui vaut
        // FUN_00401FA0 pour l'index 0 : un no-op verifie, pas une supposition.
        CallEntityHandler(s_Vt_0056afc8, 0, "PTR_FUN_0056afc8");

        int slotCount = 0;
        int* textures = *(int**)(areaDir + 0x10);
        if (textures != null)
        {
            byte* scratch = mainMem + 0x1b0000;
            byte* slot = Mem.s_EntityDecodeTable_005e8f00;      // Ghidra: &DAT_005e8f00, l'emplacement 0
            for (int* entry = textures; *entry != 0; entry += 2, slot += 4, slotCount++)
            {
                LoadResourceEntry((int*)(nint)(*entry), scratch);
                DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
            }
        }
        s_Unknown_005e982c = slotCount;
        s_Unknown_005e97ec = s_Unknown_005e97e8;

        // --- Le banc de sons de la zone ---------------------------------------------------
        // Ghidra: FUN_00547960(buf, &DAT_005649f4, zone, sous-zone) — un **sprintf**, pas le
        // FormatDecimal a deux arguments deja transcrit : le site passe quatre arguments et le
        // moteur de format complet est FUN_00549380 (424 lignes, non transcrit). Le seul format
        // utilise ici est "q:\snd\Q%02d_%02d.snd", releve a 0x005649f4 — pose directement plutot
        // que de tirer tout le moteur pour deux entiers. La lettre de lecteur remplace le 'q'
        // juste apres, comme pour tous les chemins CD.
        int subForName = s_Unknown_005de528 == 0 ? 0 : (byte)s_Unknown_005de52c;
        byte* soundPath = stackalloc byte[100];
        NativeMemory.Clear(soundPath, 100);
        {
            string name = $"q:\\snd\\Q{s_Unknown_005de528 & 0xff:D2}_{subForName:D2}.snd";
            for (int i = 0; i < name.Length; i++) soundPath[i] = (byte)name[i];
            soundPath[0] = Mem.s_CdDataRootPath[0];
        }

        IntPtr sound = OpenExistingFileReadOnly(soundPath);           // Ghidra: Ordinal_331
        if ((int)sound >= 0)
        {
            uint read;
            Win32.ReadFile(sound, s_SoundBankHeader_005e8520, 0x88, &read, null);       // Ordinal_334
            Win32.ReadFile(sound, s_LevelSoundBank, 260000, &read, null);               // Ordinal_334
            Win32.CloseHandle(sound);                                                    // Ordinal_336
        }

        // --- Les quatre listes de morceaux, empilees a la suite du curseur de l'etat ------
        byte* cursor = s_Unknown_005fa584;
        byte** chunkTable = (byte**)Mem.Entity(0x005f7bc0);
        ushort* spans = (ushort*)Mem.Entity(0x00600830);
        int start = 0;

        for (int list = 0; list < 4; list++, spans++)
        {
            int count = 0;
            int* entry = *(int**)(areaDir + 0x14 + list * 4);
            while (*entry != 0)
            {
                *chunkTable = cursor;
                cursor = LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateModelEntry((ushort*)(*chunkTable), (byte*)entry);   // Ghidra: FUN_00532450
                count++;
                entry += 2;
                chunkTable++;
            }
            *spans = (ushort)((count << 8) | start);
            start += count;
        }

        // --- Les paquets de modeles de la liste +0x24 -------------------------------------
        int lastCount = 0;
        byte* lastResult = null;
        int* bundles = *(int**)(areaDir + 0x24);
        for (int* entry = bundles; ; entry += 2)
        {
            lastCount = s_Unknown_005e9adc_count;
            lastResult = s_Unknown_005e9adc;
            if (*entry == 0) break;

            byte* next = LoadResourceEntry((int*)(nint)(*entry), cursor);
            byte* result;
            s_Unknown_005e9adc_count = RelocateModelBundle((int*)cursor, (byte*)entry, &result);
            s_Unknown_005e9adc = result;
            cursor = next;
        }
        for (int i = 0; i < lastCount; i++) *chunkTable++ = ((byte**)lastResult)[i];
        *spans = (ushort)(((s_Unknown_005e9adc_count & 0xff) << 8) | start);
        *Mem.Entity(0x005f2cec) = (byte)((s_Unknown_005e9adc_count & 0xff) + (byte)start);

        byte* firstChunk = *(byte**)Mem.Entity(0x005f7bc0);
        *(byte**)Mem.Entity(0x005f7cc0) = firstChunk + 0x40;
        *(uint*)Mem.Entity(0x005fa508) = *(uint*)(firstChunk + 0x24);

        // --- Modeles (+0x2c), animations (+0x7c), seconds modeles (+0xac) ------------------
        int* models = *(int**)(areaDir + 0x2c);
        if (models != null)
        {
            byte** table = (byte**)s_ModelTable_005e96a0;
            for (int* entry = models; *entry != 0; entry += 2, table++)
            {
                byte* next = LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateModelBundle((int*)cursor, (byte*)entry, table);
                cursor = next;
            }
        }

        int* animations = *(int**)(areaDir + 0x7c);
        if (animations != null)
        {
            byte** table = (byte**)Mem.s_AnimationTable_005e9640;
            for (int* entry = animations; *entry != 0; entry++, table++)
            {
                byte* next = LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateAnimationBundle((int*)cursor, table);
                cursor = next;
            }
        }

        int* extra = *(int**)(areaDir + 0xac);
        for (int* entry = extra; *entry != 0; entry += 2)
        {
            byte* next = LoadResourceEntry((int*)(nint)(*entry), cursor);
            RelocateModelBundle((int*)cursor, (byte*)entry, (byte**)Mem.Entity(0x00600780));
            cursor = next;
        }

        *(byte**)Mem.Entity(0x005f2ce8) = cursor;

        int* raw = *(int**)(areaDir + 0xb0);
        for (int* entry = raw; *entry != 0; entry++)
        {
            LoadResourceEntry((int*)(nint)(*entry), mainMem + 0x1b0000);
            UploadRawAreaBlocks((int*)(mainMem + 0x1b0000));    // Ghidra: FUN_005355f0
        }
    }

    // Ghidra: DAT_005e9adc — le pointeur de resultat que FUN_00532640 remplit, et le compte qu'il
    // rend. Ghidra les lit tous deux en tete de boucle, donc ce sont bien les valeurs du tour
    // precedent qui servent apres la sortie.
    private static byte* s_Unknown_005e9adc;
    private static int s_Unknown_005e9adc_count;
    private static byte* s_Unknown_005e97d0_worldmap;

    /// <summary>Ghidra: FUN_00533940 @ 0x00533940 — la finalisation du placement d'une entite.</summary>
    private static void FinalizeEntityPlacement2(uint* slot) =>
        throw new NotImplementedException(
            "FUN_00533940 : la finalisation de placement d'entite (375 lignes). Cinq labels " +
            "entrelaces (LAB_005339a8, LAB_005339b6, LAB_00533af7, LAB_00533c2a, LAB_00534018) " +
            "sautes depuis plusieurs endroits : il faut reconstruire le flot, pas le translitterer. " +
            "Appelle FUN_00533450 (52 lignes). Voir REVERSE_STATUS.md");

    /// <summary>Ghidra: FUN_005350b0 @ 0x005350b0 — 95 lignes, non transcrite.</summary>
    private static void ApplyAreaPalette(byte index) =>
        throw new NotImplementedException("FUN_005350b0 : la mise en place de palette de zone (95 lignes)");

    /// <summary>
    /// Ghidra: la seconde moitie de <c>FUN_00516350</c> — le placement du groupe sur la carte, la
    /// recopie du membre actif vers les quatre emplacements, et la derivation de tout l'etat de vue.
    ///
    /// <para>Un emplacement de personnage fait <b>0x9c octets</b> a partir de 0x005e93c0, et Ghidra
    /// l'adresse de trois facons selon le type du symbole qu'il a choisi : <c>[n * 0x27]</c> sur un
    /// tableau de dwords, <c>[n * 0x4e]</c> sur un tableau de shorts, et <c>+ n * 0x9c</c> en octets.
    /// Les trois designent le meme pas — c'est la source d'erreur principale de ce bloc, et la
    /// raison pour laquelle tout passe ici par un unique pointeur <c>slot</c> et des decalages
    /// explicites.</para>
    /// </summary>
    private static void PlaceWorldMapParty(byte* mainMem)
    {
        byte* areaDir = s_Unknown_005e97cc;
        int member = s_Unknown_005de53c;
        byte* slot = Mem.Arena(0x005e93c0) + member * 0x9c;

        if ((s_WorldMapStep_005645f8 & 0xff) == 2)
        {
            // Le point d'entree de la sous-zone : huit dwords a +0x40 du repertoire.
            uint* spawn = (uint*)(*(byte**)(areaDir + 0x40) + (uint)s_Unknown_005de530 * 0x20);
            uint identity = spawn[0];
            int packedX = (int)spawn[1];
            uint packedZ = spawn[2];
            int packedY = (int)spawn[3];
            short z = (short)packedZ;
            short x = (short)packedX;

            if (spawn[4] == 0xffffffff && spawn[5] == 0xffffffff)
            {
                // Pas de position imposee : on tire au hasard autour du point d'entree jusqu'a
                // tomber sur une face franchissable.
                *(uint*)(slot + 0x30) = 0;
                for (int i = 0; i < 4; i++) *(uint*)(slot + 0x80 + i * 4) = 0xa0000000;
                *(short*)(slot + 0x34) = 0;
                *(uint*)(slot + 0x60) = 0x7fffffff;
                *(uint*)(slot + 0x64) = 0x7fffffff;
                *(uint*)(slot + 0x20) = 0x7fffffff;

                int placed;
                do
                {
                    *(int*)(slot + 0x1c) = (((CrtRand() & 0x7ff) - 0x400) + x) * 0x1000;
                    *(int*)(slot + 0x24) = (((CrtRand() & 0x7ff) - 0x400) + z) * 0x1000;
                    placed = TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), (uint*)slot, 0xa800);
                }
                while (placed == 0);

                FinalizeEntityPlacement2((uint*)slot);
                *(uint*)(slot + 0x20) = *(uint*)(slot + 0x60);
            }
            else
            {
                *(int*)(slot + 0x1c) = x << 0xc;
                *(int*)(slot + 0x24) = z << 0xc;
                uint height = (uint)(packedX >> 4) & 0xfffff000;
                *(uint*)(slot + 0x30) = packedZ >> 0x10 | (uint)(packedY << 0x10);
                *(uint*)(slot + 0x20) = height;
                *(short*)(slot + 0x34) = (short)((uint)packedY >> 0x10);
                *(uint*)(slot + 0x60) = height;
                *(uint*)(slot + 0x64) = height;
                CopyEntityReferences(slot, (byte*)spawn);
                FinalizeEntityPlacement2((uint*)slot);
            }

            slot[0x09] = 0;
            *(byte*)Mem.Camera(0x005de038) = (byte)identity;
            *(short*)Mem.Camera(0x005de048) = (short)(identity >> 0x10);

            byte* views = *(byte**)(areaDir + 0xc0);
            if (views != null && (short)spawn[6] != -1)
            {
                uint index = (spawn[6] * 2) & 0xffff;
                int* view = (int*)(views + index * 4);
                uint first = (uint)view[0];

                *(short*)(s_WorldMapCamera_00564650 + 4) = 0;
                *(int*)Mem.s_CameraVector_00604928 = view[0];
                *Mem.Camera(0x005de078) = *(short*)(s_WorldMapCamera_00564650 + 8) + *Mem.Camera(0x005de088);
                *Mem.Camera(0x005de07c) = *Mem.Camera(0x005de08c) + *(short*)(s_WorldMapCamera_00564650 + 0xc);
                *Mem.Camera(0x005de080) = *(short*)(s_WorldMapCamera_00564650 + 0x10) + *Mem.Camera(0x005de090);

                *(short*)(s_WorldMapCamera_00564650 + 0) = (short)first;
                *(short*)Mem.View(0x006067e8) = *(short*)(s_WorldMapCamera_00564650 + 0);
                *(short*)(s_WorldMapCamera_00564650 + 2) = (short)(first >> 0x10);
                *(short*)Mem.View(0x006067ea) = *(short*)(s_WorldMapCamera_00564650 + 2);
                *Mem.View(0x006067c0) = *Mem.Camera(0x005de068);
                *Mem.View(0x006067c4) = *Mem.Camera(0x005de06c);
                *(int*)(Mem.s_CameraVector_00604928 + 4) = 0;
                *(short*)Mem.View(0x006067ec) = 0;
                *Mem.View(0x006067c8) = *Mem.Camera(0x005de070);
                *(short*)(s_WorldMapCamera_00564650 + 0x10) = (short)(view[1] - *Mem.Camera(0x005de090));
                *(uint*)(s_WorldMapCamera_00564650 + 0) = first;
                *Mem.View(0x006067d0) = *Mem.Camera(0x005de078);
                *Mem.View(0x006067d4) = *Mem.Camera(0x005de07c);
                *Mem.View(0x006067d8) = *Mem.Camera(0x005de080);
            }
        }

        // --- L'emplacement actif est remis a neuf, puis recopie sur les quatre membres ------
        *Mem.Camera(0x005de058) = *Mem.Camera(0x005de038);
        uint flags = *(uint*)slot;

        *(uint*)(slot + 0x74) = 0;
        *(uint*)(slot + 0x78) = 0;
        *(uint*)(slot + 0x5c) = 0;
        *(short*)(slot + 0x98) = 0;
        *(short*)(slot + 0x96) = 0;
        *(uint*)(slot + 0x10) = 0;
        *(uint*)(slot + 0x14) = 0;
        *(uint*)(slot + 0x50) = 0;
        *(uint*)(slot + 0x54) = 0;
        *(uint*)(slot + 0x58) = 0;
        *(short*)(slot + 0x0e) = 0;
        *(uint*)(slot + 0x28) = 0x10001000;
        *(uint*)(slot + 0x2c) = 0x1000;
        *(uint*)(slot + 0x38) = 0xffffff;
        *(short*)(slot + 0x36) = 1;
        *(uint*)(slot + 0x3c) = 0;
        *(uint*)(slot + 0x18) = 0;
        slot[0x0a] = 0;

        *(uint*)slot = flags & 0xe3ff8f71 | 0x80800e00;

        byte* record = mainMem + 0xf0000;
        for (int i = 0; i < 4; i++, record += 0x2000)
        {
            byte* target = Mem.Arena(0x005e93c0) + i * 0x9c;

            if ((*(uint*)target & 0x40000000) != 0)
            {
                *(short*)(record + 0x14c) = 1;
                *(uint*)(record + 0x140) &= 0xbfffffff;
            }

            // Les champs +4 et +8 survivent a la recopie, et le mot d'etat est recombine plutot
            // que remplace : le bit 0x10000 de l'ancien est conserve.
            uint keep4 = *(uint*)(target + 4);
            uint keep8 = *(uint*)(target + 8);
            uint previous = *(uint*)target;

            Mem.CopyDwords((uint*)slot, (uint*)target, 0x27);   // Ghidra: FUN_005352e0

            *(uint*)(target + 4) = keep4;
            *(uint*)(target + 8) = keep8;
            *(uint*)target = *(uint*)slot & *(uint*)target & 0xbffeffff | previous & 0x10000;
            target[9] = 0;
            target[8] = (byte)i;
            target[0xb] = record[0x2bd];

            CommitStagedStatBlock(record);              // Ghidra: FUN_00538ab0
            ScaleStatBlockByDifficulty(record + 0x100); // Ghidra: FUN_00538f50
            ComputeDerivedStats(record);                // Ghidra: FUN_00538c50
            ApplyEquipmentStatBonuses(record);          // Ghidra: FUN_00538e20
            FinalizeEquipmentEffects(record + 0x100);   // Ghidra: FUN_00538fa0
            record[0x206] = 100;

            if (s_PartySize_005e9020 <= i) *(uint*)target &= 0x7fffffff;
        }

        // --- La derivation de l'etat de vue ------------------------------------------------
        for (int i = 0; i < 4; i++) *(uint*)Mem.Entity(0x005f7d70 + i * 4) = *(uint*)(slot + 0x80 + i * 4);

        *(int*)Mem.Arena(0x005e9a30) = *(int*)(slot + 0x1c);
        *(int*)Mem.Arena(0x005e9a34) = *(int*)(slot + 0x20);
        *(int*)Mem.Arena(0x005e9a38) = *(int*)(slot + 0x24);
        *(uint*)Mem.Entity(0x005ff3e4) = *(uint*)slot;
        *(short*)Mem.Arena(0x005e9b38) = *(short*)(slot + 0x90);
        *(uint*)Mem.Entity(0x005f7b48) = *(uint*)(slot + 0x7c);

        *Mem.Camera(0x005de068) = *(int*)(slot + 0x1c);
        *Mem.Camera(0x005de06c) = *(int*)(slot + 0x20);
        *Mem.Camera(0x005de070) = *(int*)(slot + 0x24);
        *Mem.Camera(0x005de098) = *(int*)(slot + 0x1c);
        *Mem.Camera(0x005de09c) = *(int*)(slot + 0x20);
        *Mem.Camera(0x005de0a0) = *(int*)(slot + 0x24);

        *(int*)Mem.Entity(0x006008d0) = -*(int*)(slot + 0x1c);
        *(int*)Mem.Entity(0x006008d4) = *(int*)(slot + 0x20);
        *(ushort*)Mem.View(0x006067ee) = (byte)s_WorldMapStep_005645f8;
        s_Unknown_005de54c = 1;
        *(int*)Mem.Entity(0x006008d8) = -*(int*)(slot + 0x24);

        *(byte**)Mem.Arena(0x005e9ae0) = (byte*)Mem.View(0x006067e8);
        *(byte**)Mem.Entity(0x005edebc) = (byte*)Mem.Camera(0x005de068);
        *(byte**)Mem.Entity(0x005eca94) = (byte*)Mem.View(0x006067d0);

        // La fiche de la sous-zone : une entree de la table +0x74, indexee par l'identite posee
        // plus haut. Tout ce qui suit en decoule.
        byte* sheet = *(byte**)(*(byte**)(areaDir + 0x74) + (*(byte*)Mem.Camera(0x005de038) & 0xff) * 4);
        s_Unknown_005f2ce0 = sheet;

        s_Unknown_005de534 = *sheet;
        *(byte*)Mem.Camera(0x005de03c) = sheet[1];
        s_Unknown_005645fc = sheet[2];

        *Mem.Entity(0x005fa561) = sheet[10];
        *Mem.Entity(0x005fa560) = (byte)(*(sbyte*)Mem.Entity(0x005fa561) != -1 ? 1 : 0);
        *(ushort*)Mem.Camera(0x005de04c) = sheet[0xc] == 0xff ? (ushort)0xffff : sheet[0xc];
        *(ushort*)Mem.Camera(0x005de050) = sheet[0xd] == 0xff ? (ushort)0xffff : sheet[0xd];
        *Mem.Entity(0x00600841) = sheet[0xe];
        *Mem.Entity(0x00600840) = (byte)(*(sbyte*)Mem.Entity(0x00600841) != -1 ? 1 : 0);
        *Mem.Entity(0x00600881) = sheet[0xf];
        *Mem.Entity(0x00600880) = (byte)(*(sbyte*)Mem.Entity(0x00600881) != -1 ? 1 : 0);

        *(byte**)Mem.Arena(0x005e9b34) = *(byte**)(areaDir + 0xa0) + sheet[0xb] * 4;
        byte* terrain = *(byte**)(areaDir + 0x44) + sheet[5];
        *(byte**)Mem.Entity(0x005eca80) = terrain;

        if ((*terrain & 0x80) == 0)
        {
            *(uint*)Mem.Arena(0x005e9898) &= 0xffffff00;
            *(uint*)Mem.Arena(0x005e9908) &= 0xffffff00;
        }
        else
        {
            uint tint = (uint)((*(int*)(*(byte**)(areaDir + 0x78) + sheet[7] * 4) << 8) | 1);
            *(uint*)Mem.Arena(0x005e9898) = tint;
            *(uint*)Mem.Arena(0x005e9908) = tint;
        }

        byte* camera = *(byte**)(areaDir + 0x5c) + sheet[1] * 0x10;
        *(byte**)Mem.Arena(0x005e9af0) = camera;
        *(int*)Mem.Arena(0x005e9028) = *(int*)(camera + 4);
        *(uint*)Mem.Arena(0x005e9164) = *(uint*)(camera + 0xc) >> 0x10;
        *(uint*)Mem.Arena(0x005e902c) = *(uint*)(camera + 8) & 0xffff;
        *(uint*)Mem.Arena(0x005e9160) = *(uint*)(camera + 8) >> 0x10 | *(uint*)(camera + 0xc) << 0x10;

        *(uint*)Mem.Arena(0x005e9308) = 0;
        *(uint*)Mem.Arena(0x005e930c) = 0;
        *(uint*)Mem.Arena(0x005e93a0) = 0;
        *(uint*)Mem.Arena(0x005e93a4) = 0;

        byte* palette = *(byte**)(areaDir + 0x60) + sheet[2] * 4;
        *(byte**)Mem.Entity(0x005eca98) = palette;
        *(uint*)Mem.Arena(0x005e9b3c) = *(uint*)palette;
        *(int*)Mem.Arena(0x005e9b40) = *(int*)(areaDir + 100);
        *(uint*)Mem.Arena(0x005e92b8) = *(uint*)Mem.Arena(0x005e9160);
        *(uint*)Mem.Arena(0x005e92bc) = *(uint*)Mem.Arena(0x005e9164);
        *(int*)Mem.Arena(0x005e9310) = *(int*)Mem.Arena(0x005e9028);
        *(uint*)Mem.Arena(0x005e9314) = *(uint*)Mem.Arena(0x005e902c);

        int roll = CrtRand();                       // Ghidra: FUN_00547140
        *(uint*)Mem.Arena(0x005e9ae8) = 0;
        s_Unknown_00564540 = (roll % 6 + 6) * 0x14;

        ApplyAreaPalette(sheet[6]);                 // Ghidra: FUN_005350b0

        *(ushort*)(s_ViewRect_00566c70 + 4) = (ushort)(sheet[3] | 0x100);
        *(ushort*)(s_ViewRect_00566c70 + 8) = (ushort)(sheet[4] << 6);
        *(ushort*)(s_ViewRect_00566c70 + 0xa) = (ushort)(sheet[4] << 6);

        Winsys3d.Ordinal_770(s_ViewRect_00566c70);
        Winsys3d.Ordinal_769(1);
        Winsys3d.Ordinal_768(1);
        Winsys3d.Ordinal_767(0, 0xffffff);
        Winsys3d.Ordinal_766(s_ViewRect_00566c70);
        Winsys3d.Ordinal_765(1, 0xffffff);

        // Les quatre descripteurs d'interface de la carte, montes a l'identique.
        for (int i = 0; i < 4; i++)
        {
            byte* panel = s_WorldPanels_005dbc68 + i * GameMemory.UiDescriptorStride;
            *(uint*)(panel + 0x00) = 0x31;
            *(short*)(panel + 0x0a) = 0x54;
            *(short*)(panel + 0x0c) = 0x1c;
            panel[0x18] = 8;
            panel[0x19] = 8;
            panel[0x1a] = 0x0f;
            panel[0x1b] = 1;
            *(uint*)(panel + 0x1c) = 0x02cf1020;
            panel[0x20] = 0x0c;
            panel[0x21] = 0x0c;
            panel[0x22] = 7;
            panel[0x23] = 0;
            *(uint*)(panel + 0x24) = 0x01000000;
            panel[0x2c] = 0;
            panel[0x34] = 0;
            panel[0x3c] = 0;
            panel[0x42] = 0;
            panel[0x44] = 0x0f;
            panel[0x45] = 1;
            *(uint*)(panel + 0x48) = 0x01000000;
            panel[0x50] = 0;
            panel[0x58] = 0;
            panel[0x60] = 0;
            panel[0x68] = 0;
        }

        s_Unknown_00563c77 = 0;
        s_Unknown_00563c5c = 0;
        *(short*)(s_WorldMapDescriptor_005633b0 + 6) = unchecked((short)0xffb0);
    }

    /// <summary>Ghidra: FUN_00516fc0 @ 0x00516fc0 — 1226 lignes, la logique par image de la carte
    /// du monde ; c'est elle qui pose DAT_005645f8 et decide de la sortie.</summary>
    private static void RunWorldMapTick() =>
        throw new NotImplementedException(
            "FUN_00516fc0 : la logique par image de la carte du monde (1226 lignes) — voir REVERSE_STATUS.md");

    /// <summary>
    /// Ghidra: <c>thunk_FUN_00536180</c> @ 0x00536260 â€” byte-for-byte identical to
    /// <c>FUN_00536180</c> @ 0x00536180 (the usual linker-folded duplicate; only one body
    /// transcribed). Game state 1's entry point â€” real gameplay, reached once per tick while
    /// <see cref="s_GameStateId"/> == 1. First area of the call graph reached from the character
    /// creation flow this project has not touched before.
    /// </summary>
    private static void RunGameState1Entry()
    {
        // Ghidra: a block of miscellaneous globals reset to idle values, first encountered here â€”
        // roles not yet established. Transcribed as-is rather than guessed at.
        s_Unknown_005de7e8 = 0;
        s_Unknown_005de7ec = 0;
        s_Unknown_005de7f0 = 0;
        s_Unknown_005de7f4 = 0;
        s_Unknown_005de7f8 = 0;
        s_Unknown_005de7fc = 0;
        s_Unknown_0056c778 = -1;
        s_Unknown_005de800 = 0;
        Mem.s_Unknown_0056be1c = 0;
        Mem.s_Unknown_0056be1f = 0;
        Mem.s_Unknown_0056be1d = 0;
        Mem.s_Unknown_0056b593 = 0;
        Mem.s_Unknown_0056c6e4 = 0;
        Mem.s_Unknown_0056c6e7 = 0;
        Mem.s_Unknown_0056c6e5 = 0;
        Mem.s_Unknown_0056be5b = 0;

        ReloadGameplayResources();     // Ghidra: FUN_00504ca0
        SetupMenuScreen();             // Ghidra: FUN_00529a80

        if (s_Unknown_005de51c != 0)
        {
            // Ghidra: FUN_00537a20() â€” not yet read. Currently unreachable in practice: every
            // known writer of s_Unknown_005de51c (case 0x12 of StepSubState7Phase, on confirm)
            // sets it to 0 immediately before entering this state.
            throw new NotImplementedException(
                "thunk_FUN_00536180: FUN_00537a20 (DAT_005de51c != 0 branch) not yet reconstructed");
        }

        // Ghidra: `for (iVar1 = 0x2000; ...) *puVar2++ = 0;` on dwords â€” 0x2000 * 4 = 0x8000 bytes,
        // not the 0x2000 this project has treated Mem.s_SelectScratch_00573338 as elsewhere. The base
        // address is confirmed the same (MainMem + 0xf0000); the true extent of that scratch block
        // is larger than previously assumed. Left as a plain NativeMemory.Clear here rather than
        // retrofitting every earlier reference, since nothing so far has needed past +0x2000.
        byte* mainMem = GetMainMemPtr();
        if (mainMem != null) NativeMemory.Clear(Mem.s_SelectScratch_00573338, 0x8000);

        // Ghidra: the same "current player slot" idiom as Inp.PollInputAndAudio/FUN_00545340 â€” see the
        // comment there for how DAT_005e93c8/DAT_005e9188 were settled as
        // Inp.s_SlotArea_005e9180+0x248 and Inp.s_PlayerSlots_005e9188.
        int slotIndex = Inp.s_PlayerSlots_005e9188[
            Inp.s_SlotArea_005e9180[0x248 + (uint)(Mem.s_Unknown_005de538 & 0xff) * 0x9c] * 0x4c];
        // Pas 4 : `0x0053623f  C1 E0 04  SHL EAX,0x4` puis
        // `0x00536242  F6 80 20 4A 60 00 01  TEST byte [EAX + 0x604a20],0x1` â€” octets lus
        // directement dans Legacy/Blaze.exe. Même correction que dans Inp.PollInputAndAudio.
        // Bit 0 du mot « tenu » = **SELECT** (masque 0x0001, action 8 de
        // Inp.s_PadButtonMasks_00564f18). Confirmé en jeu le 2026-08-17 : avec F1 maintenu de la prise
        // de focus jusqu'ici, la trace posée à ce branchement lisait `pad0=0x01`. Le chemin A veut
        // donc SELECT **maintenu** à l'entrée de l'état de jeu 1, pas un appui.
        bool selectHeld = (Inp.s_InputStateBase_00604a20[slotIndex * 0x10] & 1) != 0;

        // ECHAFAUDAGE : OPENFORESIA_FORCE_DEBUG_PARTY=1 force le chemin A. Les deux conditions
        // réelles sont difficiles à réunir depuis playtest.ps1 : il faut que le moteur de texte ait
        // déjà dessiné "Gurkha" quelque part (donc un personnage portant ce nom) **et** que SELECT
        // soit maintenu à l'entrée de l'état de jeu 1 — or SELECT n'est pas lié au clavier. Sans ce
        // raccourci, RunDebugPartySelect n'a aucun moyen d'être exercée. Voir test-protocol.md.
        bool forceDebugParty =
            Environment.GetEnvironmentVariable("OPENFORESIA_FORCE_DEBUG_PARTY") == "1";

        if (forceDebugParty || (Mem.s_DebugModeLatch_005633a8 == 0 && selectHeld))
        {
            RunDebugPartySelect();     // Ghidra: FUN_00536270
            return;
        }

        RunPartyConstitution();        // Ghidra: FUN_00536ea0
    }
}
