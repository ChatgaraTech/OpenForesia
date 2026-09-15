// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

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
    internal static byte* s_Unknown_005e97cc;                // internal : le code de controle de texte 0x0a le lit
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
    // Recouvrement d'arene corrige le 2026-08-21 : 0x005fa588 et 0x005f7b40 tombent tous deux dans
    // EntityArena. Ce sont des VUES, pas des allocations propres.
    private static byte* s_ModelResultA_005fa588 => Mem.Entity(0x005fa588);
    private static byte* s_ModelResultB_005f7b40 => Mem.Entity(0x005f7b40);

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
    /// chaque image : x 0, y 0x1c0, 0x100 de large, 0x10 de haut. Releve dans Legacy/Binaries/Blaze.exe.</summary>
    private static readonly short* s_WorldMapUploadRect_00566c10 =
        (short*)AllocBytes([0x00, 0x00, 0xc0, 0x01, 0x00, 0x01, 0x10, 0x00]);

    /// <summary>Ghidra: &amp;DAT_005633b0 — le descripteur d'interface de la carte du monde. Ses 26
    /// dwords d'en-tete sont releves dans Legacy/Binaries/Blaze.exe. Alloue bas et paresseusement pour la
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
    /// (<see cref="RunPartyConstitution"/> pose <c>s_GameStateId_005de510 = 3</c>), et aussi depuis l'etat 2
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
            *(short*)(Mem.s_CameraVector_00604928 + 4) = 0;

            // Trois ENTIERS, pas trois shorts — meme relevé qu'au chargement de zone et sur le
            // chemin par image. Voir le commentaire de PlaceWorldMapParty.
            *(int*)(s_WorldMapCamera_00564650 + 8) = 0;
            *(int*)(s_WorldMapCamera_00564650 + 0xc) = 0;
            *(int*)(s_WorldMapCamera_00564650 + 0x10) = 900;

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
        s_Unknown_005e97c8 = Res.LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, Mem.s_MainResourceDirectory_005e97c4);
        Res.FixupResourceHeader(Mem.s_MainResourceDirectory_005e97c4);

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
                Res.LoadResourceEntry((int*)(nint)(*entry), scratch);
                Res.DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
            }
        }

        s_Unknown_005e97e8 = Res.s_Unknown_005e97e4;
        s_Unknown_005e9828 = slotCount;

        byte* cursor = mainMem + 0x138000;
        byte* lastBundle = cursor;

        int* bundlesA = *(int**)(directoryBase + 0xac);
        if (bundlesA != null)
        {
            for (int* entry = bundlesA; *entry != 0; entry += 2)
            {
                byte* start = cursor;
                cursor = Res.LoadResourceEntry((int*)(nint)(*entry), start);
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
                cursor = Res.LoadResourceEntry((int*)(nint)(*entry), start);
                RelocateModelBundle((int*)start, (byte*)entry, (byte**)s_ModelResultB_005f7b40);
            }
        }
        s_Unknown_005fa584 = cursor;

        Res.s_Unknown_005e9810 = *(int*)(directoryBase + 100);

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
        if (s_Unknown_00566c84 == (byte)Res.s_Unknown_005de528) return;

        s_Unknown_00566c84 = (byte)Res.s_Unknown_005de528;
        s_Unknown_00566c94 = 1;
        Res.RunLoadingScreen();          // Ghidra: FUN_0052a440
        // Ghidra: FUN_0040a600() — corps vide.

        byte* directoryBase = Mem.s_MainResourceDirectory_005e97c4;
        byte* mainMem = GetMainMemPtr();
        if (directoryBase == null || mainMem == null) return;

        int areaEntry = ((int*)(*(byte**)(directoryBase + 0x48)))[(uint)Res.s_Unknown_005de528];
        s_Unknown_005e97cc = Res.LoadResourceEntry((int*)(nint)areaEntry, s_Unknown_005e97c8);
        Res.FixupResourceHeader(s_Unknown_005e97c8);

        int* textures = *(int**)(s_Unknown_005e97c8 + 0x10);
        if (textures != null)
        {
            byte* scratch = mainMem + 0x1b0000;
            byte* slot = Mem.s_EntityDecodeTable_005e8f00 + 0x60;   // Ghidra: &DAT_005e8f60
            for (int* entry = textures; *entry != 0; entry += 2, slot += 4)
            {
                Res.LoadResourceEntry((int*)(nint)(*entry), scratch);
                Res.DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
            }
        }

        s_Unknown_005f7b44 = 0;
        int* list = *(int**)(s_Unknown_005e97c8 + 0x48);
        if (list != null)
            for (int* entry = list; *entry != 0; entry++) s_Unknown_005f7b44++;

        // La destination est la **sentinelle 2**, pas un tampon : LoadResourceEntry la reconnait et
        // installe la table de sauts de la zone au lieu de lire des octets.
        Res.LoadResourceEntry(*(int**)(*(byte**)(s_Unknown_005e97c8 + 0x6c)), (byte*)Res.s_OverrideDestB_00604930);

        // Ghidra: `(*(code *)PTR_FUN_0056b4f0)()`. 0x0056b4f0 est l'entree **8** de la table de
        // sauts (0x0056b4d0 + 8 * 4), celle que l'installation vient de renseigner.
        Res.CallAreaHandler(8);
    }

    /// <summary>Ghidra: &amp;DAT_005e8520 — les 0x88 octets d'en-tete du banc de sons de la zone.
    ///
    /// <para>
    /// <b>Ce n'est PAS un tableau distinct, et ce n'en a jamais ete un.</b>
    /// <c>0x005e8520 == 0x005e8340 + 0x1e0</c> : c'est exactement la QUEUE du repertoire de sons
    /// <c>GameMemory.s_SoundDirectory_005e8340</c> (0x268 octets = 0x1e0 pour les ids 0x00..0x3b, puis 0x88
    /// pour les ids 0x3c..0x4c). La reconstruction en faisait une allocation separee jusqu'au
    /// 2026-08-21 ; les 0x88 octets lus ici n'arrivaient donc jamais dans le repertoire que
    /// <c>PlaySoundEffect</c> consulte, et les effets de niveau lisaient un decalage et une
    /// longueur nuls — silencieux.
    /// </para>
    /// <para>
    /// Verifie contre la source de verite, pas deduit : <c>Blaze.c</c> 74824 charge
    /// <c>&amp;DAT_005e8340</c> sur 0x1e0 octets depuis BASE.SND, et 63353 charge
    /// <c>&amp;DAT_005e8520</c> sur 0x88 depuis le fichier de zone. Deux moities d'un seul tableau
    /// de 77 entrees, que <c>PlaySoundEffect</c> indexe depuis la premiere.
    /// </para></summary>
    private static byte* s_SoundBankHeader_005e8520
        => (byte*)(Mem.s_SoundDirectory_005e8340 + GameMemory.SoundDirectoryBaseCount);

    // Ghidra: &DAT_00566c70 — le rectangle de vue passe par adresse a Ordinal_770 et Ordinal_766.
    // Reuni dans GameMemory le 2026-08-21 : MainMenu.cs en allouait un SECOND exemplaire sous le
    // nom s_MenuRenderBlock_00566c70, et les deux chemins ecrivent les memes champs. Une seule
    // memoire dans le binaire, une seule allocation ici.

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

    /// <summary>
    /// Ghidra: FUN_00533450 @ 0x00533450 — la <b>hauteur du sol</b> sous un point (x, z), obtenue
    /// en interpolant le plan de la face passée en paramètre.
    ///
    /// <para>
    /// Même géométrie que <see cref="TryPlaceEntity"/>, dont elle reprend exactement les
    /// conventions : un sommet fait 8 octets (x et y dans les deux moitiés du dword de tête, z dans
    /// le short de +4), les deux arêtes vont dans les vecteurs GTE 799 et 800, leur produit
    /// vectoriel dans 801, et sa troncature en 16 bits dans 813 — le même ordre de facteurs, pas un
    /// équivalent réécrit.
    /// </para>
    /// <para>
    /// La table de sommets vient de <c>DAT_005f7cc0</c>, que <see cref="LoadWorldMapSubArea"/> pose
    /// à <c>premierMorceau + 0x40</c> ; ici elle est déjà chargée, la fonction ne la dérive pas.
    /// </para>
    /// <para>
    /// <b>Le zéro de sortie n'est pas un cas d'erreur mais un cas géométrique</b> : si la composante
    /// Y de la normale est nulle, le plan est vertical et n'a pas de hauteur au-dessus de (x, z).
    /// L'original rend 0 sans le signaler, et c'est transcrit tel quel.
    /// </para>
    /// </summary>
    /// <param name="face">La face du terrain, un enregistrement de 16 octets.</param>
    /// <param name="position">La position, en 20.12 ; seules ses composantes x ([0]) et z ([2]) sont lues.</param>
    private static int GroundHeightOnFace(uint* face, int* position)
    {
        EnsureGteRegisters();

        byte* vertices = *(byte**)Mem.Entity(0x005f7cc0);

        int* v0 = (int*)(vertices + (face[0] & 0xffff) * 8);
        int* v2 = (int*)(vertices + (face[0] >> 0x10) * 8);
        int* v1 = (int*)(vertices + (face[1] & 0xffff) * 8);

        int head0 = *v0;
        int x0 = (short)head0;
        int y0 = head0 >> 0x10;
        int z0 = (short)v0[1];

        int head2 = *v2;
        int head1 = *v1;

        int* vec799 = (int*)s_GteVec799;
        int* vec800 = (int*)s_GteVec800;
        int* mac801 = (int*)s_GteMac801;
        short* ir813 = (short*)s_GteIr813;

        vec800[0] = (short)head2 - x0;
        vec800[1] = (head2 >> 0x10) - y0;
        vec800[2] = (short)v2[1] - z0;

        vec799[0] = (short)head1 - x0;
        vec799[1] = (head1 >> 0x10) - y0;
        vec799[2] = (short)v1[1] - z0;

        mac801[0] = vec799[2] * vec800[1] - vec799[1] * vec800[2];
        mac801[1] = vec800[2] * vec799[0] - vec799[2] * vec800[0];
        mac801[2] = vec799[1] * vec800[0] - vec800[1] * vec799[0];

        ir813[0] = (short)mac801[0];
        ir813[1] = (short)mac801[1];
        ir813[2] = (short)mac801[2];

        // Plan vertical : pas de hauteur au-dessus de (x, z).
        if (mac801[1] == 0) return 0;

        // Les parentheses autour de la soustraction ne sont PAS decoratives : `*` lie plus fort
        // que `-`, donc les omettre calculerait `A/B - (y0 * 0x1000)` au lieu de
        // `(A/B - y0) * 0x1000`. Ghidra ecrit bien la seconde forme.
        int planeHeight = (((position[0] >> 0xc) - x0) * mac801[0]
                         + ((position[2] >> 0xc) - z0) * mac801[2]) / mac801[1];

        return (planeHeight - y0) * 0x1000;
    }

    // Ghidra: DAT_00563c77 et DAT_00563c5c — **ce ne sont pas des globales**, ce sont deux CHAMPS
    // du descripteur d'interface de la carte du monde (0x005633b0) :
    //
    //   0x005633b0 + 0x8ac = 0x00563c5c   le curseur de texte, un POINTEUR
    //   0x005633b0 + 0x8c7 = 0x00563c77   les drapeaux du moteur de texte
    //
    // Ghidra les rend en globales isolees parce qu'il ne sait pas que le bloc fait 0x8c8 octets ;
    // c'est le piege deja documente sous « Un DAT_ cluster dans un enregistrement connu est un
    // champ, pas une globale » (DataSymbols.md), et le meme decalage 0x8c7 que DAT_0056e61f dans
    // le contexte de menu (Engine/Resources.cs).
    //
    // **Corrige le 2026-08-23**, et ce n'etait pas theorique : BindMenuLabels ecrit `descripteur
    // [0x8ac]` et `descripteur[0x8c7]` (Engine/UiText.cs), tandis que le bloc d'affichage lisait
    // deux `static byte` que rien n'atteignait jamais. La boite de dialogue liait donc son texte
    // et ne s'ouvrait pas — « - La reception - » restait invisible a l'entree de l'auberge.
    //
    // Les largeurs viennent du desassembleur, pas de la pseudo-C : `mov [563C77h],bl` est un
    // OCTET, `mov [563C5Ch],ebx` un DWORD (0x00516fa1 et 0x00516fa7).
    private static byte s_Unknown_00563c77
    {
        get => s_WorldMapDescriptor_005633b0[0x8c7];
        set => s_WorldMapDescriptor_005633b0[0x8c7] = value;
    }

    private static uint s_Unknown_00563c5c
    {
        get => *(uint*)(s_WorldMapDescriptor_005633b0 + 0x8ac);
        set => *(uint*)(s_WorldMapDescriptor_005633b0 + 0x8ac) = value;
    }

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
        if (Res.s_Unknown_005de528 == 0
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
        s_Unknown_005e97d0 = Res.LoadResourceEntry((int*)(nint)subEntry, areaDir);
        Res.FixupResourceHeader(areaDir);

        // La sentinelle 3 : sept tables de sauts d'entites installees d'un coup.
        Res.LoadResourceEntry(*(int**)(*(byte**)(areaDir + 0x6c)), (byte*)Res.s_OverrideDestC_00600904);

        // Ghidra: `(*(code *)PTR_FUN_0056afc8)()` — l'entree **0** de la table 0056afc8, qui vaut
        // FUN_00401FA0 pour l'index 0 : un no-op verifie, pas une supposition.
        Res.CallEntityHandler(Res.s_Vt_0056afc8, 0, "PTR_FUN_0056afc8");

        int slotCount = 0;
        int* textures = *(int**)(areaDir + 0x10);
        if (textures != null)
        {
            byte* scratch = mainMem + 0x1b0000;
            byte* slot = Mem.s_EntityDecodeTable_005e8f00;      // Ghidra: &DAT_005e8f00, l'emplacement 0
            for (int* entry = textures; *entry != 0; entry += 2, slot += 4, slotCount++)
            {
                Res.LoadResourceEntry((int*)(nint)(*entry), scratch);
                Res.DecodeResourceChunk(scratch, (ushort*)slot, (byte*)(entry + 1));
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
        int subForName = Res.s_Unknown_005de528 == 0 ? 0 : (byte)s_Unknown_005de52c;
        byte* soundPath = stackalloc byte[100];
        NativeMemory.Clear(soundPath, 100);
        {
            string name = $"q:\\snd\\Q{Res.s_Unknown_005de528 & 0xff:D2}_{subForName:D2}.snd";
            for (int i = 0; i < name.Length; i++) soundPath[i] = (byte)name[i];
            soundPath[0] = Mem.s_CdDataRootPath[0];
        }

        IntPtr sound = OpenExistingFileReadOnly(soundPath);           // Ghidra: Ordinal_331
        if ((int)sound >= 0)
        {
            uint read;
            Win32.ReadFile(sound, s_SoundBankHeader_005e8520, 0x88, &read, null);       // Ordinal_334
            Win32.ReadFile(sound, Mem.s_LevelSoundBank_00600900, 260000, &read, null);               // Ordinal_334
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
                cursor = Res.LoadResourceEntry((int*)(nint)(*entry), cursor);
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

            byte* next = Res.LoadResourceEntry((int*)(nint)(*entry), cursor);
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
                byte* next = Res.LoadResourceEntry((int*)(nint)(*entry), cursor);
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
                byte* next = Res.LoadResourceEntry((int*)(nint)(*entry), cursor);
                RelocateAnimationBundle((int*)cursor, table);
                cursor = next;
            }
        }

        int* extra = *(int**)(areaDir + 0xac);
        for (int* entry = extra; *entry != 0; entry += 2)
        {
            byte* next = Res.LoadResourceEntry((int*)(nint)(*entry), cursor);
            RelocateModelBundle((int*)cursor, (byte*)entry, (byte**)Mem.Entity(0x00600780));
            cursor = next;
        }

        *(byte**)Mem.Entity(0x005f2ce8) = cursor;

        int* raw = *(int**)(areaDir + 0xb0);
        for (int* entry = raw; *entry != 0; entry++)
        {
            Res.LoadResourceEntry((int*)(nint)(*entry), mainMem + 0x1b0000);
            UploadRawAreaBlocks((int*)(mainMem + 0x1b0000));    // Ghidra: FUN_005355f0
        }
    }

    // Ghidra: DAT_005e9adc — le pointeur de resultat que FUN_00532640 remplit, et le compte qu'il
    // rend. Ghidra les lit tous deux en tete de boucle, donc ce sont bien les valeurs du tour
    // precedent qui servent apres la sortie.
    private static byte* s_Unknown_005e9adc;
    private static int s_Unknown_005e9adc_count;
    private static byte* s_Unknown_005e97d0_worldmap;

    /// <summary>
    /// Ghidra: FUN_00533940 @ 0x00533940 — la finalisation du placement d'une entite : partant des
    /// quatre references de face de l'emplacement, elle <b>marche de proche en proche</b> a travers
    /// le maillage du terrain, en franchissant les aretes partagees, jusqu'a trouver la face qui
    /// contient reellement la position — puis en deduit la hauteur du sol et l'etat de vue.
    ///
    /// <para>
    /// <b>Le flot a ete reconstruit, pas translittere.</b> Ghidra en rend sept labels sautes depuis
    /// plusieurs endroits (<c>LAB_005339a8</c>, <c>LAB_005339b6</c>, <c>LAB_00533af7</c>,
    /// <c>LAB_00533b96</c>, <c>LAB_00533c2a</c>, <c>LAB_00533e9f</c>, <c>LAB_0053430a</c>) plus un
    /// <c>joined_r0x005342c7</c>, ce qui donne l'impression d'un graphe irreductible. Il ne l'est
    /// pas : c'est <b>une boucle sur les aretes de la face courante</b>.
    /// </para>
    /// <para>
    /// Les aretes sont, dans l'ordre : <c>(i0,i2)</c>, <c>(i2,i1)</c>, puis <c>(i1,i0)</c> pour un
    /// triangle, ou <c>(i1,i3)</c> et <c>(i3,i0)</c> pour une face a quatre sommets (bit 1 des
    /// attributs). Chaque arete est testee par un <c>NormalClip</c> ; un signe negatif veut dire
    /// « le point est de l'autre cote », donc on franchit CETTE arete vers la face voisine et on
    /// reprend a l'arete suivante. Les trois destinations de saut de Ghidra
    /// (<c>LAB_00533af7</c>, <c>LAB_00533b96</c>, <c>LAB_00533e9f</c>) sont exactement
    /// « reprendre a l'arete 1, 2, ou 2-quad » : le code y compare la cle de l'arete franchie pour
    /// savoir laquelle, ce qui se resume a <c>arete suivante = arete franchie + 1</c>.
    /// </para>
    /// <para>
    /// Si toutes les aretes passent, le point est dans la face et la marche s'arrete. Si la marche
    /// depose la meilleure face dans un AUTRE emplacement que celui qu'on parcourt, le curseur y
    /// demenage et le budget de pas repart a 0x10 — c'est le <c>goto LAB_005339a8</c>. Le budget
    /// vaut seize pas au maximum, et l'epuiser leve le bit 8 du short de <c>+2</c>.
    /// </para>
    /// <para>
    /// <b>Les deux boucles sont dans la MEME methode, et il ne faut pas les separer</b> : la
    /// seconde lit trois locales que la premiere laisse derriere elle (<c>local_30</c>,
    /// <c>local_40</c>, <c>local_1c</c>). Les extraire demanderait de les passer en parametres, ce
    /// qui donnerait une signature mensongere — elles ne sont pas des entrees, ce sont des restes.
    /// </para>
    /// <para>
    /// L'expression a six termes que Ghidra inline six fois est le <c>NormalClip</c> du SDK, deja
    /// reconstruit sous ce nom dans <c>Platform/Winsys3d.cs</c> (FUN_0051c280) : les six termes ont
    /// ete recoupes un a un, c'est la meme expression, sur les memes registres 805/806/807/808.
    /// </para>
    /// </summary>
    private static void FinalizeEntityPlacement2(uint* slot)
    {
        EnsureGteRegisters();

        byte* firstChunk = *(byte**)Mem.Entity(0x005f7bc0);
        byte* faces = *(byte**)(firstChunk + 0x28);            // Ghidra: iVar7
        short** adjacency = *(short***)(firstChunk + 0x30);
        byte* vertices = *(byte**)Mem.Entity(0x005f7cc0);
        byte* materials = *(byte**)Mem.Entity(0x005fa508);

        // La position (x, z) de l'entite, empaquetee comme un SXY : x dans le short bas, z dans le
        // haut. Ghidra: `(int)param_1[7] >> 0xc & 0xffff | (param_1[9] & 0xfffff000) << 4`.
        *(uint*)s_GteSxy807 = ((uint)((int)slot[7] >> 0xc) & 0xffff)
                            | ((slot[9] & 0xfffff000) << 4);

        short* history = Mem.s_FaceWalkHistory_005de730;
        *(int*)history = -1;
        *(int*)(history + 2) = -1;

        uint* cursor = slot + 0x20;          // Ghidra: local_5c
        int remaining = 4;                   // Ghidra: local_4c

        // TODO(REVERSE): la seconde boucle lit `local_1c` et `local_30` sans qu'ils soient
        // forcement ecrits — `local_1c` ne l'est que par la branche « face a quatre sommets » de la
        // premiere boucle, et `local_30` y garde le sommet 2 de la derniere face examinee, ce qui
        // n'a aucun sens comme hauteur. C# exige une affectation definie ; 0 est retenu ici, la ou
        // l'original lirait la pile. Les deux ne sont lus qu'a travers `previousFace != null`,
        // faux au premier tour, donc la valeur n'a pas d'effet observable — mais elle est bien
        // indeterminee dans l'original, et c'est note plutot que masque.
        uint quadPacked = 0;                 // Ghidra: local_1c
        uint carriedPacked = 0;              // Ghidra: local_30

        while (remaining != 0)
        {
            // Ghidra: LAB_005339a8 — tete du corps externe.
            *(ushort*)((byte*)cursor + 2) &= 0xfff7;
            int steps = 0x10;                // Ghidra: local_48
            bool relocated = false;
            bool stop = false;

            while (steps != 0 && !relocated && !stop)
            {
                // Ghidra: LAB_005339b6 — tete de la boucle interne : (re)lecture de la face.
                uint slotRef = *cursor;                    // Ghidra: uVar21
                uint slotAttributes = slotRef >> 0x10;     // Ghidra: uVar19
                if ((slotAttributes & 0x8000) != 0) break;

                uint* face = (uint*)(faces + (slotRef & 0xffff) * 0x10);   // Ghidra: puVar13
                uint f0 = face[0];                          // Ghidra: uVar8
                uint f1 = face[1];                          // Ghidra: uVar17
                uint i0 = f0 & 0xffff;
                uint i2 = f0 >> 0x10;                       // Ghidra: uVar26
                uint i1 = f1 & 0xffff;
                uint i3 = f1 >> 0x10;                       // Ghidra: uVar22
                bool quad = (slotAttributes & 1) != 0;
                int lastEdge = quad ? 3 : 2;

                uint p0 = PackedXz(vertices, i0);           // Ghidra: uVar23
                uint p1 = PackedXz(vertices, i1);           // Ghidra: uVar27
                uint p2 = PackedXz(vertices, i2);           // Ghidra: local_30
                uint p3 = quad ? PackedXz(vertices, i3) : 0;
                carriedPacked = p2;
                if (quad) quadPacked = p3;

                int edge = 0;
                bool reload = false;

                while (!reload && !relocated && !stop)
                {
                    // Les deux extremites de l'arete courante, et sa cle : l'indice de depart dans
                    // le short haut, celui d'arrivee dans le bas.
                    uint a, b, fromIndex, toIndex;
                    switch (edge)
                    {
                        case 0:
                            a = p0; b = p2; fromIndex = i0; toIndex = i2; break;
                        case 1:
                            a = p2; b = p1; fromIndex = i2; toIndex = i1; break;
                        case 2:
                            if (quad) { a = p1; b = p3; fromIndex = i1; toIndex = i3; }
                            else { a = p1; b = p0; fromIndex = i1; toIndex = i0; }
                            break;
                        default:
                            a = p3; b = p0; fromIndex = i3; toIndex = i0; break;
                    }

                    *(uint*)s_GteSxy805 = a;
                    *(uint*)s_GteSxy806 = b;
                    if (NormalClip() >= 0)
                    {
                        // Le point est du bon cote de cette arete. Toutes passees : il est DANS la
                        // face, et la marche s'arrete — Ghidra tombe alors sur LAB_00534010.
                        edge++;
                        if (edge > lastEdge) stop = true;
                        continue;
                    }

                    // Ghidra: LAB_00533c2a — la marche vers la face voisine, a travers l'arete.
                    uint edgeKey = (fromIndex << 0x10) | (toIndex & 0xffff);
                    uint bestKind = (uint)(4 - remaining);   // Ghidra: local_50
                    uint* bestSlot = null;                   // Ghidra: local_2c, usage emplacement

                    short* neighbour = adjacency[fromIndex & 0xffff];
                    for (short other = *neighbour; other >= 0; other = *++neighbour)
                    {
                        uint* candidate = (uint*)(faces + other * 0x10);   // Ghidra: puVar15
                        if (candidate == face) continue;

                        uint c0 = candidate[0];
                        uint c1 = candidate[1];
                        uint cAttributes = candidate[3];
                        bool cQuad = (cAttributes & 1) != 0;

                        // La face voisine porte-t-elle la meme arete ? Quatre dispositions possibles,
                        // selon qu'elle est triangle ou quadrilatere.
                        bool shares = edgeKey == c0
                                   || edgeKey == ((c1 << 0x10) | (c0 >> 0x10))
                                   || (!cQuad && edgeKey == ((c0 << 0x10) | (c1 & 0xffff)))
                                   || (cQuad && (edgeKey == c1 || edgeKey == ((c0 << 0x10) | (c1 >> 0x10))));
                        if (!shares) continue;

                        uint kind = (cAttributes >> 0x10) & 0xff;
                        uint* target = slot + kind + 0x20;
                        uint written = cAttributes;

                        // Le short de +2 de la matiere decide si la face participe au sol.
                        if (*(short*)(materials + 2 + (ushort)candidate[2] * 8) >= 0)
                        {
                            if ((cAttributes & 0x2000) == 0) *(ushort*)((byte*)cursor + 2) &= 0xdfff;
                            else written = cAttributes | 4;
                        }

                        written |= *(ushort*)((byte*)cursor + 2) & 0x2086u;
                        byte zone = *((byte*)candidate + 0xf);
                        if (zone != 0 && zone - 1u != (uint)*Mem.Camera(0x005de058)) written |= 2;
                        if (cursor == target) written |= *(ushort*)((byte*)cursor + 2) & 0x800u;

                        byte kindByte = (byte)(cAttributes >> 0x10);
                        // Ghidra ecrit `uVar20 << 0x10 | (int)sVar4`, une extension de signe. Elle
                        // est inoffensive parce que la boucle garantit `other >= 0` — mais la
                        // laisser telle quelle vaut un CS0675, et un avertissement de signe qu'on
                        // apprend a ignorer est le debut d'un vrai bug de signe. Le cast par
                        // `ushort` calcule exactement la meme chose pour un `other` positif.
                        if ((written & 0x800) == 0 || *((byte*)slot + 0x95) != kindByte)
                            *target = (written << 0x10) | (uint)(ushort)other;
                        else
                            *((byte*)target + 3) |= 8;

                        if (kindByte < (byte)bestKind) { bestKind = kind; bestSlot = target; }
                    }

                    if (slotRef == *cursor)
                    {
                        *(ushort*)((byte*)cursor + 2) =
                            (ushort)((*(ushort*)((byte*)cursor + 2) & 0xdfff) | 0x8000);
                    }

                    // La meilleure face trouvee vit dans un AUTRE emplacement, et ce n'est pas celle
                    // qu'on y avait deja mise : le curseur y demenage et le budget repart.
                    if (bestSlot != null
                        && history[bestKind] != (short)*bestSlot
                        && bestKind != (uint)(4 - remaining))
                    {
                        byte carried = *((byte*)bestSlot + 2);
                        remaining = 4 - (int)bestKind;
                        ushort previous = *(ushort*)((byte*)cursor + 2);
                        history[bestKind] = (short)*bestSlot;
                        ushort fresh = *(ushort*)(faces + (ushort)*bestSlot * 0x10 + 0xc);
                        *(ushort*)((byte*)bestSlot + 2) =
                            (ushort)(fresh | (previous & 0x82) | (carried & 0x82));
                        cursor = bestSlot;
                        if ((fresh & 0x800) != 0) stop = true;
                        else relocated = true;
                        break;
                    }

                    // Ghidra: LAB_0053430a — l'indice de face de l'emplacement a change sous nous :
                    // il faut relire la face et repartir de sa premiere arete.
                    if ((short)slotRef != (short)*cursor)
                    {
                        if ((*cursor & 0x8000000) != 0) { stop = true; break; }
                        steps--;
                        reload = true;
                        break;
                    }

                    // Rien n'a bouge : on reprend a l'arete SUIVANTE. Si on vient de franchir la
                    // derniere, la marche s'arrete.
                    edge++;
                    if (edge > lastEdge) stop = true;
                }
            }

            // Ghidra: LAB_00534010 / LAB_00534018 — le budget epuise leve le bit 8.
            if (relocated) continue;
            if (steps == 0) *((byte*)cursor + 2) |= 8;
            cursor++;
            remaining--;
        }

        // =========================================================================================
        // Seconde boucle : les quatre references sont parcourues A L'ENVERS, et la premiere face
        // dont la hauteur de sol est atteignable fixe l'altitude de l'entite.
        // =========================================================================================
        remaining = 4;                       // Ghidra: local_4c
        uint sinkFlag = 0;                   // Ghidra: local_48, reutilise ici comme drapeau 0x2000
        uint previousFace = 0;               // Ghidra: local_40
        uint refAttributes = 0;              // Ghidra: uVar21 a la sortie de la boucle
        uint* lastRef = null;                // Ghidra: puVar13

        do
        {
            lastRef = cursor - 1;
            refAttributes = cursor[-1] >> 0x10;
            uint faceAddress = (uint)(nuint)(faces + (cursor[-1] & 0xffff) * 0x10);
            slot[0x1f] = faceAddress;

            uint nextQuadPacked = quadPacked;    // Ghidra: uVar19
            uint nextPreviousFace = previousFace; // Ghidra: uVar8
            uint height;                          // Ghidra: uVar17

            if ((refAttributes & 0x8000) == 0)
            {
                height = (uint)GroundHeightOnFace((uint*)(nuint)faceAddress, (int*)(slot + 7));

                if ((int)height <= (int)slot[8])
                {
                    if (previousFace != 0)
                    {
                        refAttributes = quadPacked & 0xffffdfff;
                        slot[0x1f] = previousFace;
                        height = carriedPacked;
                    }
                    slot[0x18] = height;
                    break;
                }

                if ((int)slot[8] < (int)(height - 0x32000))
                {
                    // La face est trop haute : soit on la refuse tout net, soit on la marque comme
                    // franchissable et on continue vers la reference suivante.
                    if ((refAttributes & 0x2000) == 0)
                    {
                        refAttributes |= 0x804;
                        break;
                    }
                    *((byte*)cursor - 1) |= 0x80;
                    uint marked = refAttributes | 0x8000;
                    uint wasStepped = refAttributes & 4;
                    refAttributes = marked;
                    height = carriedPacked;
                    if (wasStepped == 0) sinkFlag = 0x2000;
                }
                else
                {
                    nextQuadPacked = refAttributes;
                    nextPreviousFace = slot[0x1f];
                    if ((*((byte*)(nuint)slot[0x1f] + 0xd) & 0x20) == 0)
                    {
                        if (previousFace != 0 && 0x32000 < (int)(carriedPacked - slot[0x18]))
                        {
                            refAttributes = quadPacked & 0xffffdfff;
                            slot[0x1f] = previousFace;
                            height = carriedPacked;
                        }
                        nextQuadPacked = (height + slot[0x15]) - slot[8];
                        if ((int)nextQuadPacked < 0) slot[0x15] = nextQuadPacked;
                        slot[0x18] = height;
                        slot[8] = height;
                        break;
                    }
                }
            }
            else
            {
                *(ushort*)((byte*)cursor - 2) |= (ushort)sinkFlag;
                refAttributes |= sinkFlag;
                height = carriedPacked;
            }

            carriedPacked = height;
            previousFace = nextPreviousFace;
            remaining--;
            quadPacked = nextQuadPacked;
            cursor = lastRef;
        }
        while (remaining != 0);

        *((sbyte*)slot + 0x95) = (sbyte)(remaining - 1);

        if ((refAttributes & 0x8000) != 0 && (refAttributes & 0x2000) == 0
            && (**(byte**)Mem.Entity(0x005eca80) & 0x20) != 0)
        {
            refAttributes = 0x40000800;
        }
        if ((refAttributes & 0x800) != 0) refAttributes |= 0x10000000;

        if (slot[0x1f] != 0)
        {
            if ((*((byte*)(nuint)slot[0x1f] + 0xd) & 0x20) == 0) refAttributes &= 0xffffdfff;
            else refAttributes |= 0x2000;
        }
        slot[0x24] = refAttributes;

        // Les references restantes recoivent les attributs frais de leur face, et la premiere qui
        // n'est pas masquee par le bit 0x1000 donne la hauteur de reference secondaire.
        bool noneUsable;                     // Ghidra: bVar10
        if ((refAttributes & 0x800) == 0)
        {
            bool wantHeight = true;          // Ghidra: bVar11
            noneUsable = true;
            for (int left = remaining; left != 0; left--)
            {
                if ((*lastRef & 0x80000000) == 0)
                {
                    uint* refFace = (uint*)(faces + (*lastRef & 0xffff) * 0x10);
                    ushort fresh = *(ushort*)((byte*)refFace + 0xc);
                    *(ushort*)((byte*)lastRef + 2) = (ushort)((*((byte*)lastRef + 2) & 8) | fresh);
                    if (wantHeight && (fresh & 0x1000) == 0)
                    {
                        if ((slot[0x24] & 0x800) == 0)
                            slot[0x19] = (uint)GroundHeightOnFace(refFace, (int*)(slot + 7));
                        wantHeight = false;
                    }
                }
                lastRef--;
                noneUsable = wantHeight;
            }
        }
        else
        {
            noneUsable = false;
        }

        if (noneUsable)
        {
            slot[0x19] = 0x80000000;
            slot[0x24] |= 0x20001000;
        }
        else
        {
            slot[0x24] &= 0xffffefff;
        }

        // Le compteur de +0x94 amortit le basculement du bit 0x20000000 de l'etat de l'entite.
        if ((slot[0x24] & 0x8000) == 0)
        {
            *(byte*)(slot + 0x25) = 0x20;
            slot[0] &= 0xdfffffff;
        }
        else if ((sbyte)slot[0x25] != 0)
        {
            *(sbyte*)(slot + 0x25) = (sbyte)((sbyte)slot[0x25] - 1);
        }
        else
        {
            slot[0] |= 0x20000000;
        }

        // Ghidra: LAB_00534272 — tombee sous le plancher, l'entite est reinitialisee.
        if ((int)slot[8] < -0x4e20000)
        {
            *(byte*)(slot + 0x25) = 0;
            *(ushort*)((byte*)slot + 0x82) = 0xa000;
            *(ushort*)((byte*)slot + 0x86) = 0xa000;
            *(ushort*)((byte*)slot + 0x8a) = 0xa000;
            *(ushort*)((byte*)slot + 0x8e) = 0xa000;
            slot[0] |= 0x20000000;
        }
    }

    /// <summary>Ghidra: <c>&amp;DAT_005f7cc0 + i * 8</c> — un sommet fait 8 octets : x dans le short
    /// bas du dword de tete, y dans le haut, z dans le short de <c>+4</c>. Empaquete ici comme un
    /// SXY (x bas, z haut), la forme que les registres 805/806/807 attendent.</summary>
    private static uint PackedXz(byte* vertices, uint index)
    {
        byte* v = vertices + index * 8;
        return (uint)(ushort)*(short*)v | ((uint)(ushort)*(short*)(v + 4) << 0x10);
    }

    /// <summary>
    /// Ghidra: FUN_005350b0 @ 0x005350b0 — <b>le peuplement d'une sous-zone</b> : parcourt la liste
    /// d'apparitions de la sous-zone <paramref name="subArea"/> et remplit un emplacement d'entite
    /// par enregistrement, en placant chacune sur le terrain.
    ///
    /// <para>
    /// <b>Elle s'appelait <c>ApplyAreaPalette</c> jusqu'au 2026-08-21, et ce nom etait faux.</b> Il
    /// datait du moment ou seule sa taille etait connue. Rien ici ne touche a une palette : la
    /// fonction lit une liste d'enregistrements de 0x20 octets et en derive des entites de 0x9c
    /// octets. Renommee des que le corps a ete lu, comme <c>Strategy.md</c> le demande.
    /// </para>
    /// <para>
    /// La liste vient du repertoire de zone, table <c>+0x30</c>, indexee par la sous-zone. Elle est
    /// terminee par <c>0xffffffff</c>, et le remplissage s'arrete aussi quand les 128 emplacements
    /// de <c>DAT_005fa5e0</c> sont pleins.
    /// </para>
    /// <para>
    /// Chaque entite est ensuite posee sur le sol par <see cref="TryPlaceEntity"/> puis
    /// <see cref="FinalizeEntityPlacement2"/> — les deux transcrites plus tot le meme jour, dont
    /// celle que Ghidra n'avait pas decompilee.
    /// </para>
    /// </summary>
    /// <param name="subArea">L'index de sous-zone ; <c>0xff</c> veut dire « aucune ».</param>
    private static void SpawnSubAreaEntities(int subArea)
    {
        uint* slot = (uint*)Mem.Entity(0x005fa5e0) + *(uint*)Mem.Arena(0x005e9ae8) * 0x27;

        // L'original lit la table AVANT de tester `subArea != 0xff` : l'index 0xff provoque donc
        // une lecture de `table[255]` dont le resultat est ensuite jete. Transcrit dans cet ordre.
        byte* spawnLists = *(byte**)(s_Unknown_005e97cc + 0x30);
        uint* record = *(uint**)(spawnLists + subArea * 4);

        int spawned = subArea;                        // Ghidra reutilise param_1 comme compteur

        if (record != null && subArea != 0xff)
        {
            int remaining = 0x80 - (int)*(uint*)Mem.Arena(0x005e9ae8);
            spawned = (int)*(uint*)Mem.Arena(0x005e9ae8);

            byte** chunkTable = (byte**)Mem.Entity(0x005f7bc0);

            for (; remaining > 0; record += 8, remaining--, spawned++, slot += 0x27)
            {
                uint kind = record[0];
                if (kind == 0xffffffff) break;

                uint packedFacing = record[3];        // Ghidra: uVar1
                uint packedX = record[1];             // Ghidra: uVar2
                uint packedZ = record[2];             // Ghidra: uVar3

                *((byte*)slot + 7) = (byte)kind;
                *(byte*)(slot + 2) = (byte)spawned;
                slot[3] = kind;

                *(ushort*)((byte*)slot + 0x96) = 0;
                *((byte*)slot + 9) = 0;
                slot[4] = 0;
                slot[5] = record[6];

                uint model = record[7];               // Ghidra: uVar4
                *(short*)(slot + 1) = (short)model;
                *slot = 0xc0200000;
                *(short*)((byte*)slot + 10) = (short)(model >> 0x10);

                slot[0xe] = 0xffffff;
                slot[8] = (uint)((int)packedX >> 4 & unchecked((int)0xfffff000));
                slot[7] = (uint)((short)packedX << 0xc);
                slot[9] = (uint)((short)packedZ << 0xc);

                ushort facingHigh = (ushort)(packedFacing >> 0x10);   // Ghidra: uVar5

                if ((kind & 3) != 0)
                {
                    // Le morceau de terrain sous l'entite decide si elle est visible d'emblee.
                    byte* chunk = chunkTable[(byte)*Mem.Entity(0x00600838) + (byte)slot[1]];
                    if ((kind & 0x80) == 0
                        || *(ushort*)(chunk + 0xe) < 2
                        || *(int*)(*(int*)(chunk + 0x38) + 4) == 0)
                    {
                        *slot |= 0x2000;
                    }

                    slot[10] = 0x10001000;
                    slot[0xc] = packedFacing << 0x10 | packedZ >> 0x10;
                    slot[0xb] = 0x1000;
                    *(ushort*)(slot + 0xd) = facingHigh;
                }

                switch (kind & 3)
                {
                    case 0:
                        *(ushort*)((byte*)slot + 0x36) = 1;
                        *(byte*)(slot + 0x10) = 0;
                        slot[0xf] = *(uint*)(*(int*)(s_Unknown_005e97cc + 0x38) + (byte)slot[1] * 4);
                        *slot |= 0x2000;
                        slot[10] = packedFacing << 0x10 | packedZ >> 0x10;
                        *(ushort*)((byte*)slot + 0x2e) = facingHigh;
                        break;
                    case 1:
                        *(ushort*)((byte*)slot + 0x36) = 2;
                        break;
                    case 2:
                        *(ushort*)((byte*)slot + 0x36) = 0;
                        break;
                }

                slot[0x14] = 0;
                slot[0x20] = 0xa0000000;
                slot[0x21] = 0xa0000000;
                slot[0x22] = 0xa0000000;
                slot[0x23] = 0xa0000000;
                slot[0x15] = 0;
                slot[0x16] = 0;
                slot[0x1b] = 0;
                slot[0x1c] = 0;
                slot[0x1d] = 0;
                slot[0x1e] = 0;
                slot[0x18] = slot[8];
                slot[0x19] = slot[8];

                TryPlaceEntity(*(byte**)Mem.Entity(0x005f7bc0), slot, 0x8000);
                FinalizeEntityPlacement2(slot);
            }
        }

        // TODO(REVERSE): **defaut probable de l'original, transcrit tel quel.** Cette affectation
        // est HORS du `if`, et `spawned` n'a ete initialise au compte d'entites qu'A L'INTERIEUR.
        // Si la liste est nulle ou si `subArea` vaut 0xff, le compte d'entites recoit donc l'INDEX
        // DE SOUS-ZONE — une valeur qui n'a rien a y faire. Ghidra le rend sans ambiguite (le
        // `param_1 = DAT_005e9ae8` est bien dans le bloc garde), et la reecriture du compte est
        // bien apres l'accolade fermante.
        //
        // Non « corrige » : le projet reproduit les defauts de 1998 plutot que de les reparer. A
        // verser dans legacy-bug.md le jour ou on l'observe en jeu — pour l'instant on ne sait pas
        // si le cas est atteignable.
        *(uint*)Mem.Arena(0x005e9ae8) = (uint)spawned;
    }

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

                // **Largeurs reprises au desassembleur le 2026-08-23** (0x005169b5..0x00516a7d).
                // Le bloc de constantes de camera &DAT_00564650 mele des shorts et des ENTIERS, et
                // la premiere transcription avait lu les trois derniers en shorts :
                //
                //     mov esi,[56465Ch]   dword     mov [564654h],bx   word
                //     mov ebp,[564660h]   dword     mov [60492Ch],bx   word
                //     mov ecx,[564658h]   dword     mov [564660h],edx  dword
                //
                // Les champs +0x08, +0x0c et +0x10 sont les trois decalages de la camera par
                // rapport a la cible, en unites de 1/4096 : les tronquer a seize bits deplacait la
                // camera a l'arrivee dans une zone. C'est l'ecart que Vincent a releve contre le
                // jeu d'origine a l'entree de l'auberge, ou la camera doit se poser juste au-dessus
                // de la porte.
                *(short*)(s_WorldMapCamera_00564650 + 4) = 0;
                *(int*)Mem.s_CameraVector_00604928 = view[0];
                *Mem.Camera(0x005de078) = *(int*)(s_WorldMapCamera_00564650 + 8) + *Mem.Camera(0x005de088);
                *Mem.Camera(0x005de07c) = *Mem.Camera(0x005de08c) + *(int*)(s_WorldMapCamera_00564650 + 0xc);
                *Mem.Camera(0x005de080) = *(int*)(s_WorldMapCamera_00564650 + 0x10) + *Mem.Camera(0x005de090);

                *(short*)(s_WorldMapCamera_00564650 + 0) = (short)first;
                *(short*)Mem.View(0x006067e8) = *(short*)(s_WorldMapCamera_00564650 + 0);
                *(short*)(s_WorldMapCamera_00564650 + 2) = (short)(first >> 0x10);
                *(short*)Mem.View(0x006067ea) = *(short*)(s_WorldMapCamera_00564650 + 2);
                *Mem.View(0x006067c0) = *Mem.Camera(0x005de068);
                *Mem.View(0x006067c4) = *Mem.Camera(0x005de06c);
                *(short*)(Mem.s_CameraVector_00604928 + 4) = 0;
                *(short*)Mem.View(0x006067ec) = 0;
                *Mem.View(0x006067c8) = *Mem.Camera(0x005de070);
                *(int*)(s_WorldMapCamera_00564650 + 0x10) = view[1] - *Mem.Camera(0x005de090);
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

        SpawnSubAreaEntities(sheet[6]);             // Ghidra: FUN_005350b0

        *(ushort*)(Mem.s_ViewRect_00566c70 + 4) = (ushort)(sheet[3] | 0x100);
        *(ushort*)(Mem.s_ViewRect_00566c70 + 8) = (ushort)(sheet[4] << 6);
        *(ushort*)(Mem.s_ViewRect_00566c70 + 0xa) = (ushort)(sheet[4] << 6);

        Winsys3d.Ordinal_770(Mem.s_ViewRect_00566c70);
        Winsys3d.Ordinal_769(1);
        Winsys3d.Ordinal_768(1);
        Winsys3d.Ordinal_767(0, 0xffffff);
        Winsys3d.Ordinal_766(Mem.s_ViewRect_00566c70);
        Winsys3d.Ordinal_765(1, 0xffffff);

        // Les quatre descripteurs d'interface de la carte, montes a l'identique.
        //
        // **Decalages repris au desassembleur le 2026-08-23** (0x00516f25..0x00516f9d), et non a la
        // pseudo-C. Ghidra rend le curseur `puVar15 = &DAT_005dbc72` en `short *` puis melange
        // indices de short et decalages d'octets (`puVar15 + 7` a cote de `(int)puVar15 + 0xf`) ;
        // la premiere transcription en avait tire des decalages faux, decales de deux au debut et
        // de sept a la fin. Le registre de base est `eax = 0x005dbc72`, soit **descripteur + 0x0a**,
        // et tout se lit relativement a lui :
        //
        //     mov [eax-2],bp      -> +0x08 = 0x54     mov [eax-0Ah],si   -> +0x00 = 0x31 (WORD)
        //     mov word [eax],1Ch  -> +0x0a = 0x1c     mov [eax+12h],edi  -> +0x1c (dword)
        //
        // L'erreur n'etait pas benigne : `DrawUiPanel` lit l'etendue de la fenetre en **+0x08**,
        // que l'ancienne version n'ecrivait jamais. Les quatre panneaux de membre sortaient donc
        // sans cadre — les jauges s'affichaient en texte nu la ou l'original les encadre.
        for (int i = 0; i < 4; i++)
        {
            byte* panel = s_WorldPanels_005dbc68 + i * GameMemory.UiDescriptorStride;
            *(short*)(panel + 0x00) = 0x31;
            *(short*)(panel + 0x08) = 0x54;
            *(short*)(panel + 0x0a) = 0x1c;
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
            panel[0x3b] = 0;
            panel[0x33] = 0;
            panel[0x40] = 0;
            panel[0x41] = 0;
            panel[0x42] = 0x0f;
            panel[0x43] = 1;
            *(uint*)(panel + 0x44) = 0x01000000;
            panel[0x4b] = 0;
            panel[0x53] = 0;
            panel[0x5b] = 0;
            panel[0x63] = 0;
        }

        s_Unknown_00563c77 = 0;
        s_Unknown_00563c5c = 0;
        *(short*)(s_WorldMapDescriptor_005633b0 + 6) = unchecked((short)0xffb0);
    }


    /// <summary>Ghidra: DAT_005dbc14, DAT_005dbc18 et DAT_005ddfd0 — trois octets que le prologue
    /// de <c>FUN_00516fc0</c> remplit d'un instantane (zone, sous-zone, taille du groupe) quand le
    /// verrou de mode de mise au point est leve. <b>Role etabli le 2026-09-15</b> par
    /// <see cref="RunDebugMenuFrame_0051AA30"/> : ce sont les trois premieres lignes du menu de mise
    /// au point, que le binaire etiquette lui-meme <c>QUEST</c>, <c>MAP</c> et <c>PLAYER</c> — la
    /// zone et la sous-zone ou se teleporter, et la taille du groupe a reconstituer. L'instantane
    /// sert a ce que le menu s'ouvre sur l'etat courant. Les trois sont des <b>octets</b> — le code machine les charge en <c>al</c>/<c>cl</c>/
    /// <c>dl</c> et les range en <c>mov [.],r8</c>, la ou la pseudo-C laisse croire a des mots.
    /// Ils ne sont pas voisins entre eux : 0x005dbc14 et 0x005dbc18 le sont, 0x005ddfd0 est
    /// ailleurs.</summary>
    private static byte s_DebugQuest_005dbc14, s_DebugPlayer_005dbc18, s_DebugMap_005ddfd0;

    /// <summary>
    /// Ghidra: FUN_00401000 @ 0x00401000 — <b>restitue l'orientation sauvegardee, puis en fait le
    /// but</b>, et marque le bloc comme rafraichi.
    ///
    /// <para>
    /// <b>Ghidra a supprime le premier tiers de cette fonction</b>, et c'est la cause de l'ecart de
    /// camera a l'entree de l'auberge, resolu le 2026-08-23 sur la trace dynamique de Vincent
    /// (<c>Analyse/x32dbg/AUBERGE_CAMERA/openforesia_camera_dynamic_trace.md</c>, point d'arret
    /// materiel en ecriture sur <c>0x006067E8</c>). Le corps reel commence par restituer
    /// <c>ViewAngle</c> depuis <c>SavedViewAngle</c> (<c>0x005712F8</c>) :
    /// </para>
    /// <code>
    /// 00401000  mov ax,[5712F8h]     00401014  mov [6067E8h],ax   <- ViewAngle <- SavedViewAngle
    /// 00401006  mov cx,[5712FAh]     0040101F  mov [6067EAh],cx
    /// 0040100D  mov dx,[5712FCh]     00401026  mov [6067ECh],dx
    /// 0040101A  mov eax,[5E9AE0h]    0040102D  mov cx,[eax]       <- puis le but depuis le pointeur
    /// </code>
    /// <para>
    /// Ghidra tient ces trois ecritures pour <b>mortes</b>, puisque les trois suivantes reecrivent
    /// les memes adresses depuis <c>*DAT_005E9AE0</c>. Elles ne le sont pas : <c>DAT_005E9AE0</c>
    /// <b>pointe sur <c>0x006067E8</c></b>, c'est-a-dire sur la destination elle-meme. Les lectures
    /// `[eax]`, `[eax+2]`, `[eax+4]` relisent donc ce que la premiere passe vient d'y ecrire, et la
    /// chaine reelle est :
    /// </para>
    /// <code>
    /// SavedViewAngle (600, 1024, 0) -> ViewAngle -> ViewAngleGoal
    /// </code>
    /// <para>
    /// Sans ce premier bloc, le but partait a zero, l'elevation derivait vers la borne <b>basse</b>
    /// de <c>DAT_005E9B34</c> (300 au lieu de 600) et le cap restait a 0 au lieu de 1024 — les 90°
    /// d'ecart visibles sur la rose des vents. <b>Une elimination de code mort par le decompilateur
    /// est fausse des qu'un pointeur peut aliaser la destination</b> : c'est le meme piege que les
    /// doubles noms d'adresse, vu du cote du flot au lieu du cote des donnees.
    /// </para>
    /// <para>L'appel final va a <c>FUN_00401fa0</c>, un <c>ret</c> nu : une souche, transcrite
    /// comme telle plutot que passee sous silence.</para>
    /// </summary>
    private static void SnapshotViewVector()
    {
        short* angle = (short*)Mem.View(0x006067e8);
        short* saved = (short*)Mem.ViewParams(0x005712f8);

        angle[0] = saved[0];
        angle[1] = saved[1];
        angle[2] = saved[2];

        // `view` aliase `angle` au repos : les trois lectures ci-dessous relisent ce qui vient
        // d'etre restitue. C'est voulu, et c'est ce que l'original fait.
        short* view = *(short**)Mem.Arena(0x005e9ae0);
        short* goal = (short*)Mem.View(0x006067e0);

        goal[0] = view[0];
        goal[1] = view[1];
        goal[2] = view[2];
        angle[0] = view[0];
        angle[1] = view[1];
        angle[2] = view[2];

        *Mem.ViewParams(0x0057139c) |= 0x20;

        // Ghidra: FUN_00401fa0() — corps `ret` nu (verifie au desassembleur). Sans effet.
    }

    /// <summary>
    /// Ghidra: FUN_00401080 @ 0x00401080 — <b>publie la camera et la vue pour l'image</b> : recopie
    /// les blocs pointes vers le bloc de vue et les parametres de vue, fait glisser le vecteur
    /// courant vers la place du precedent, et pose les valeurs fixes de debut d'image.
    ///
    /// <para>
    /// Le seul calcul de la fonction est <c>DAT_00571304 = DAT_005de06c + 0xc8000</c>, applique deux
    /// fois : la hauteur de camera recoit un decalage constant. Tout le reste est du transport.
    /// </para>
    /// <para>
    /// L'ordre des ecritures est conserve tel quel, y compris la ligne <c>0x0057139c |= 0x40</c>
    /// glissee au milieu du bloc de camera : elle n'a pas de dependance, mais rien ne justifie de
    /// la deplacer.
    /// </para>
    /// </summary>
    private static void PublishFrameView()
    {
        short* view = *(short**)Mem.Arena(0x005e9ae0);
        int* camera = *(int**)Mem.Entity(0x005edebc);
        int* target = *(int**)Mem.Entity(0x005eca94);

        view[0] = *(short*)(*(byte**)Mem.Arena(0x005e9b34) + 2);

        short* current = (short*)Mem.View(0x006067e0);
        short* previous = (short*)Mem.View(0x006067e8);
        short* saved = (short*)Mem.ViewParams(0x005712f8);

        saved[0] = previous[0];
        saved[1] = previous[1];
        saved[2] = previous[2];
        previous[0] = current[0];
        previous[1] = current[1];
        previous[2] = current[2];

        int* cameraParams = (int*)Mem.ViewParams(0x00571360);
        cameraParams[0] = camera[0];
        cameraParams[1] = camera[1];
        cameraParams[2] = camera[2];

        int* targetView = Mem.View(0x006067d0);
        targetView[0] = target[0];
        targetView[1] = target[1];
        targetView[2] = target[2];

        int* targetViewCopy = Mem.View(0x006067f0);
        targetViewCopy[0] = target[0];
        targetViewCopy[1] = target[1];
        targetViewCopy[2] = target[2];

        int* cameraView = Mem.View(0x006067c0);
        cameraView[0] = camera[0];
        cameraView[1] = camera[1];
        *Mem.ViewParams(0x0057139c) |= 0x40;      // Ghidra: pose au milieu du bloc, conserve ici
        cameraView[2] = camera[2];

        // La hauteur de camera (0x005de06c) part decalee de 0xc8000 dans les deux exemplaires.
        int* frameCamera = (int*)Mem.ViewParams(0x00571300);
        frameCamera[0] = *Mem.Camera(0x005de068);
        frameCamera[1] = *Mem.Camera(0x005de06c) + 0xc8000;

        int* frameCameraCopy = (int*)Mem.ViewParams(0x00571370);
        frameCameraCopy[0] = *Mem.Camera(0x005de068);
        frameCameraCopy[1] = *Mem.Camera(0x005de06c) + 0xc8000;

        frameCamera[2] = *Mem.Camera(0x005de070);
        frameCameraCopy[2] = *Mem.Camera(0x005de070);

        *(int*)Mem.ViewParams(0x00571380) = 0;
        *(ushort*)Mem.ViewParams(0x0057128e) = 0xffff;
        *(ushort*)Mem.ViewParams(0x00571276) = 0xffff;
        *(int*)Mem.ViewParams(0x005713a0) = 10;
        *(int*)Mem.ViewParams(0x005713b0) = 0;
        *(int*)Mem.ViewParams(0x005713a8) = 0;
    }

    /// <summary>
    /// Ghidra: <c>thunk_FUN_00536180</c> @ 0x00536260 â€” byte-for-byte identical to
    /// <c>FUN_00536180</c> @ 0x00536180 (the usual linker-folded duplicate; only one body
    /// transcribed). Game state 1's entry point â€” real gameplay, reached once per tick while
    /// <see cref="s_GameStateId_005de510"/> == 1. First area of the call graph reached from the character
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

        Res.ReloadGameplayResources();     // Ghidra: FUN_00504ca0
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
        // directement dans Legacy/Binaries/Blaze.exe. Même correction que dans Inp.PollInputAndAudio.
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

    // =============================================================================================
    // FUN_004747F0 et sa chaine — la transition de sous-zone de la zone 0.
    //
    // CE BLOC EXISTE PARCE QUE GHIDRA N'A PAS DECOMPILE 0x004747F0. Pendant trois jours, la seule
    // issue documentee etait « une re-passe Ghidra ciblee », c'est-a-dire une action manuelle hors
    // du depot. Le verbe `disasm` d'OpenForesia.Tools (ajoute le 2026-08-21) l'a rendue inutile :
    // le binaire est dans le depot, il ne manquait qu'un decodeur.
    //
    // Et ce que le desassemblage a montre contredit l'estimation qui avait fait renoncer. La note
    // disait « 723 octets avec neuf branches convergentes et une logique de drapeaux dense, c'est
    // le travail d'un decompilateur ». En realite c'est une **cascade parfaitement reguliere** :
    // huit blocs identiques au bit pres, chacun « si telle condition, poser un code de destination
    // et sortir ». Les « neuf sorties convergentes » sont neuf `return`, pas un graphe emmele.
    //
    // La lecon, pour la prochaine fois : **une taille en octets ne dit rien de la difficulte**. Ce
    // qui la dit, c'est le nombre de formes distinctes. Ici il y en avait deux.
    // =============================================================================================

    /// <summary>
    /// Ghidra: <c>0x004747F0</c> — l'entree 8 de la table de sauts de la zone 0, appelee par
    /// <see cref="Resources.CallAreaHandler"/>. <b>Non decompilee par Ghidra</b> ; transcrite le
    /// 2026-08-21 depuis le desassemblage (`OpenForesia.Tools disasm Legacy/Binaries/Blaze.exe
    /// 0x004747f0 724`), releve conserve dans <c>Legacy/Raw/Blaze/FUN_004747F0.asm</c>.
    ///
    /// <para>
    /// Ce qu'elle fait : elle choisit la <b>sous-zone suivante</b> de la zone 0 en fonction du
    /// nombre de capacites que le groupe possede en commun. Elle compte les bits leves de
    /// <c>DAT_005e9000</c> (le masque de capacites du groupe, recalcule par
    /// <see cref="RefreshPartyAbilityMask"/>), plus le bit 0 de <c>DAT_005e9001</c>, et selon ce
    /// compte pose un code dans <c>DAT_005de52c</c> : 0, 8, 0x10, 0x18, 0x20, 0x28, 0x30, 0x38,
    /// 0x40, 0x48 ou 0x50.
    /// </para>
    /// <para>
    /// <b>Chaque branche appelle ensuite <c>FUN_00401FA0</c>, qui est un <c>ret</c> nu</b> —
    /// verifie au desassembleur, pas suppose. L'appel est donc sans effet et n'est pas transcrit ;
    /// seul le code de destination compte. C'est coherent avec la table de sauts, que
    /// <c>BuildAreaHandlerTable</c> remplit deja de <c>0x00401FA0</c>.
    /// </para>
    /// <para>
    /// <b>La garde de sortie precoce est la plus importante</b> : si <c>DAT_005de52c</c> ou
    /// <c>DAT_005de530</c> est deja non nul, la fonction ne fait rien du tout. Une transition en
    /// cours n'est jamais ecrasee.
    /// </para>
    /// </summary>
    internal static void RunArea0Handler8()
    {
        // 0x004747f3 : eteint le bit 3 de DAT_005e9841.
        *Mem.Arena(0x005e9841) &= 0xf7;

        byte partyFlags = *Mem.Arena(0x005e9001);      // Ghidra: cl

        // 0x0047480e : si le bit 1 est eteint, le groupe subit d'abord l'operation de script
        // « ET 0xEF » sur l'octet +1 de son bloc de drapeaux — c'est-a-dire l'extinction du
        // bit 0x10. Les quatre octets sont l'operande empilee par 0x0047480f..0x0047481d.
        if ((partyFlags & 2) == 0)
        {
            byte* operand = stackalloc byte[4];
            operand[0] = 0;
            operand[1] = 1;      // decalage de l'octet vise dans le bloc de drapeaux
            operand[2] = 4;      // opcode : ET sur un octet
            operand[3] = 0xef;   // masque : eteint le bit 0x10
            uint discarded;
            ApplyPartyScriptOp(operand, &discarded);
            partyFlags = *Mem.Arena(0x005e9001);       // Ghidra relit cl apres l'appel
        }

        byte stateFlags = *Mem.Arena(0x005e9006);      // Ghidra: dl

        // 0x0047483c : sans le bit 3 de DAT_005e9006, le bit 0 de DAT_005e9840 tombe.
        if ((stateFlags & 8) == 0)
        {
            byte* bank = Mem.Arena(0x005e9840);
            if ((*bank & 1) != 0) *bank &= 0xfe;
        }

        // 0x00474857 : le mode de mise au point remet la transition a zero.
        if (s_Unknown_00566c90 == 1)
        {
            s_Unknown_005de52c = 0;
            s_Unknown_005de530 = 0;
        }

        // 0x0047486c : une transition deja engagee n'est pas touchee.
        if (s_Unknown_005de52c != 0 || s_Unknown_005de530 != 0) return;

        // 0x00474884 : le nombre de capacites communes au groupe — les huit bits de
        // DAT_005e9000, plus le bit 0 de DAT_005e9001.
        byte abilities = *Mem.Arena(0x005e9000);       // Ghidra: al
        int count = 0;                                 // Ghidra: esi
        for (int bit = 0; bit < 8; bit++)
            if ((abilities & (1 << bit)) != 0) count++;
        if ((partyFlags & 1) != 0) count++;

        // 0x004748bd : eax vaut 1 si le bit 1 de DAT_005e9001 est leve, 0 sinon. Toutes les
        // branches ci-dessous exigent qu'il vaille 1 — autrement dit, sans ce bit la fonction
        // s'arrete apres la premiere.
        int stage = (partyFlags & 2) != 0 ? 1 : 0;     // Ghidra: ax

        // 0x004748c9 : sans le bit 3, la transition repart de zero.
        if ((partyFlags & 8) == 0) { s_Unknown_005de52c = 0; return; }

        // 0x004748e3 : premiere sous-zone, tant que le bit 7 n'est pas la.
        if (stage == 0 || (partyFlags & 0x80) == 0) { s_Unknown_005de52c = 0x08; return; }

        if (stage != 1) return;
        if ((partyFlags & 0x10) == 0) { s_Unknown_005de52c = 0x10; return; }

        if ((partyFlags & 0x20) == 0) { s_Unknown_005de52c = 0x18; return; }

        // 0x00474955 et suivants : la cascade par palier de deux capacites.
        if (count >= 0 && count <= stage) { s_Unknown_005de52c = 0x20; return; }
        if (count >= 2 && count <= 3) { s_Unknown_005de52c = 0x28; return; }
        if (count >= 4 && count <= 5) { s_Unknown_005de52c = 0x30; return; }
        if (count >= 6 && count <= 7) { s_Unknown_005de52c = 0x38; return; }
        if (count == 8) { s_Unknown_005de52c = 0x40; return; }

        // 0x00474a60 / 0x00474a92 : a neuf capacites, le bit 7 de DAT_005e9006 depart les deux
        // dernieres destinations.
        if (count == 9)
        {
            s_Unknown_005de52c = (byte)((stateFlags & 0x80) == 0 ? 0x48 : 0x50);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00514000 @ 0x00514000 puis FUN_00515d70 @ 0x00515d70 — applique une operation
    /// de script au bloc de drapeaux de <b>chaque membre du groupe</b>, recalcule le masque commun,
    /// puis applique la meme operation au masque.
    ///
    /// <para>
    /// Les deux fonctions sont fondues : <c>FUN_00514000</c> ne fait que passer
    /// <c>DAT_005e97cc</c> et 0 a <c>FUN_00515d70</c>, et ces deux parametres n'y sont jamais lus.
    /// Les garder aurait donne une signature qui ment sur ce que la fonction touche.
    /// </para>
    /// <para>
    /// L'operande fait quatre octets : <c>[1]</c> le decalage de l'octet vise, <c>[2]</c> l'opcode,
    /// <c>[3]</c> l'operande immediate. <c>[0]</c> n'est pas lu ici.
    /// </para>
    /// </summary>
    private static void ApplyPartyScriptOp(byte* operand, uint* result)
    {
        byte* record = GetMainMemPtr() + 0xf0000;
        for (int remaining = s_PartySize_005e9020; remaining != 0; remaining--)
        {
            ApplyScriptOp(record + 0x2e0 + operand[1], operand[2], operand[3]);
            record += 0x2000;
        }

        RefreshPartyAbilityMask();                     // Ghidra: FUN_00535710

        *result = ApplyScriptOp(Mem.Arena(0x005e9000) + operand[1], operand[2], operand[3])
            ? 1u : 0u;
    }

    /// <summary>
    /// Ghidra: FUN_00514040 @ 0x00514040 — l'unite arithmetique et logique du moteur de script :
    /// une operation entre une cible en memoire, une operande immediate, et la table de variables
    /// <c>DAT_005e9060</c>.
    ///
    /// <para>
    /// L'opcode est structure, pas arbitraire. Trois groupes selon la largeur —
    /// <c>0x00..0x3B</c> octet, <c>0x40..0x7B</c> mot, <c>0x80..0xBB</c> double mot — et dans
    /// chaque groupe six familles de douze :
    /// </para>
    /// <list type="bullet">
    /// <item><c>+0x00</c> : cible <c>op=</c> immediate</item>
    /// <item><c>+0x0C</c> : variable[immediate] <c>op=</c> cible</item>
    /// <item><c>+0x18</c> : cible <c>op=</c> variable[immediate]</item>
    /// <item><c>+0x24</c> : comparaison cible / immediate</item>
    /// <item><c>+0x30</c> : comparaison cible / variable[immediate]</item>
    /// </list>
    /// <para>
    /// <b>La regularite est tentante et il ne faut pas y ceder.</b> Les cas ne sont PAS
    /// interchangeables : Ghidra emet des largeurs et des signes differents d'un cas a l'autre —
    /// <c>0x01</c> additionne en <c>char</c> signe la ou <c>0x07</c> divise en <c>byte</c> non
    /// signe, <c>0x13</c> passe par un 64 bits la ou <c>0x07</c> reste en 32. Les replier en boucle
    /// calculee serait une re-derivation, pas une transcription. Chaque cas est donc ecrit tel
    /// quel, dans l'ordre de Ghidra, pour rester comparable ligne a ligne.
    /// </para>
    /// <para>
    /// <b>Ou cette fonction devra vivre.</b> Le binaire l'appelle depuis <b>17</b> fonctions
    /// distinctes, sur deux bandes d'adresses (<c>0x00515xxx</c> et <c>0x00523xxx</c>) : c'est une
    /// primitive partagee, pas un detail de la carte du monde. Elle reste ici parce que la carte du
    /// monde est aujourd'hui son SEUL appelant reconstruit — regle 4 d'<c>ARCHITECTURE.md</c>. Le
    /// jour ou un second appelant apparait, elle part dans <c>Engine/</c> avec
    /// <c>ApplyPartyScriptOp</c>.
    /// </para>
    /// </summary>
    /// <returns>
    /// Le resultat de la comparaison pour les familles <c>+0x24</c> et <c>+0x30</c> ;
    /// <c>false</c> pour toutes les affectations, et pour tout opcode inconnu.
    /// </returns>
    private static bool ApplyScriptOp(byte* target, byte op, uint operand)
    {
        byte* vars = Mem.Arena(0x005e9060);
        byte immediate = (byte)operand;

        switch (op)
        {
            // ---- octet : cible op= immediate ----------------------------------------------
            case 0x00: *target = immediate; return false;
            case 0x01: *target = (byte)((sbyte)*target + immediate); return false;
            case 0x02: *target = (byte)((sbyte)*target - immediate); return false;
            case 0x03: *target |= immediate; return false;
            case 0x04: *target &= immediate; return false;
            case 0x05: *target ^= immediate; return false;
            case 0x06: *target = (byte)(immediate * (sbyte)*target); return false;
            case 0x07: *target = (byte)(*target / operand); return false;
            case 0x08: *target = (byte)(*target % operand); return false;
            case 0x09: *target = (byte)((sbyte)*target << (immediate & 0x1f)); return false;
            case 0x0a: *target = (byte)(*target >> (immediate & 0x1f)); return false;
            case 0x0b: *target = (byte)((uint)CrtRand() % operand); return false;

            // ---- octet : variable[immediate] op= cible ------------------------------------
            case 0x0c: vars[operand] = *target; return false;
            case 0x0d: vars[operand] = (byte)((sbyte)vars[operand] + (sbyte)*target); return false;
            case 0x0e: vars[operand] = (byte)((sbyte)vars[operand] - (sbyte)*target); return false;
            case 0x0f: vars[operand] |= *target; return false;
            case 0x10: vars[operand] &= *target; return false;
            case 0x11: vars[operand] ^= *target; return false;
            case 0x12: vars[operand] = (byte)((sbyte)*target * (sbyte)vars[operand]); return false;
            case 0x13: vars[operand] = (byte)((ulong)vars[operand] / (ulong)(long)(int)(uint)*target); return false;
            case 0x14: vars[operand] = (byte)((ulong)vars[operand] % (ulong)(long)(int)(uint)*target); return false;
            case 0x15: vars[operand] = (byte)((sbyte)vars[operand] << (*target & 0x1f)); return false;
            case 0x16: vars[operand] = (byte)(vars[operand] >> (*target & 0x1f)); return false;
            case 0x17: vars[operand] = (byte)(CrtRand() % (int)(uint)*target); return false;

            // ---- octet : cible op= variable[immediate] ------------------------------------
            case 0x18: *target = vars[operand]; return false;
            case 0x19: *target = (byte)((sbyte)*target + (sbyte)vars[operand]); return false;
            case 0x1a: *target = (byte)((sbyte)*target - (sbyte)vars[operand]); return false;
            case 0x1b: *target |= vars[operand]; return false;
            case 0x1c: *target &= vars[operand]; return false;
            case 0x1d: *target ^= vars[operand]; return false;
            case 0x1e: *target = (byte)((sbyte)vars[operand] * (sbyte)*target); return false;
            case 0x1f: *target = (byte)((ulong)*target / (ulong)(long)(int)(uint)vars[operand]); return false;
            case 0x20: *target = (byte)((ulong)*target % (ulong)(long)(int)(uint)vars[operand]); return false;
            case 0x21: *target = (byte)((sbyte)*target << (vars[operand] & 0x1f)); return false;
            case 0x22: *target = (byte)(*target >> (vars[operand] & 0x1f)); return false;
            case 0x23: *target = (byte)(CrtRand() % (int)(uint)vars[operand]); return false;

            // ---- octet : comparaison avec l'immediate -------------------------------------
            case 0x24: return *target != operand;
            case 0x25: return *target == operand;
            case 0x26: return operand < *target;
            case 0x27: return *target < operand;
            case 0x28: return operand <= *target;
            case 0x29: return *target <= operand;
            case 0x2a: return (*target & operand) != operand;
            case 0x2b: return (*target & operand) == operand;
            case 0x2c: return (immediate & *target) == 0;
            case 0x2d: return (immediate & *target) != 0;
            case 0x2e: return *target % operand == 0;
            case 0x2f: return *target % operand != 0;

            // ---- octet : comparaison avec variable[immediate] -----------------------------
            case 0x30: return (sbyte)*target != (sbyte)vars[operand];
            case 0x31: return (sbyte)*target == (sbyte)vars[operand];
            case 0x32: return vars[operand] < *target;
            case 0x33: return *target < vars[operand];
            case 0x34: return vars[operand] <= *target;
            case 0x35: return *target <= vars[operand];
            case 0x36: return (*target & vars[operand]) != vars[operand];
            case 0x37: return (*target & vars[operand]) == vars[operand];
            case 0x38: return (vars[operand] & *target) == 0;
            case 0x39: return (vars[operand] & *target) != 0;
            case 0x3a: return (ulong)*target % (ulong)(long)(int)(uint)vars[operand] == 0;
            case 0x3b: return (ulong)*target % (ulong)(long)(int)(uint)vars[operand] != 0;

            // ---- mot : cible op= immediate ------------------------------------------------
            case 0x40: *(ushort*)target = (ushort)operand; return false;
            case 0x41: *(ushort*)target = (ushort)(*(ushort*)target + (ushort)operand); return false;
            case 0x42: *(ushort*)target = (ushort)(*(ushort*)target - (ushort)operand); return false;
            case 0x43: *(ushort*)target = (ushort)(*(ushort*)target | (ushort)operand); return false;
            case 0x44: *(ushort*)target = (ushort)(*(ushort*)target & (ushort)operand); return false;
            case 0x45: *(ushort*)target = (ushort)(*(ushort*)target ^ (ushort)operand); return false;
            case 0x46: *(ushort*)target = (ushort)((ushort)operand * *(ushort*)target); return false;
            case 0x47: *(ushort*)target = (ushort)(*(ushort*)target / operand); return false;
            case 0x48: *(ushort*)target = (ushort)(*(ushort*)target % operand); return false;
            case 0x49: *(ushort*)target = (ushort)(*(ushort*)target << (immediate & 0x1f)); return false;
            case 0x4a: *(ushort*)target = (ushort)(*(ushort*)target >> (immediate & 0x1f)); return false;
            case 0x4b: *(ushort*)target = (ushort)((uint)CrtRand() % operand); return false;

            // ---- mot : variable[immediate] op= cible --------------------------------------
            case 0x4c: *(ushort*)(vars + operand) = *(ushort*)target; return false;
            case 0x4d: *(short*)(vars + operand) = (short)(*(short*)(vars + operand) + *(ushort*)target); return false;
            case 0x4e: *(short*)(vars + operand) = (short)(*(short*)(vars + operand) - *(ushort*)target); return false;
            case 0x4f: *(ushort*)(vars + operand) = (ushort)(*(ushort*)(vars + operand) | *(ushort*)target); return false;
            case 0x50: *(ushort*)(vars + operand) = (ushort)(*(ushort*)(vars + operand) & *(ushort*)target); return false;
            case 0x51: *(ushort*)(vars + operand) = (ushort)(*(ushort*)(vars + operand) ^ *(ushort*)target); return false;
            case 0x52: *(ushort*)(vars + operand) = (ushort)(*(ushort*)target * *(short*)(vars + operand)); return false;
            case 0x53: *(short*)(vars + operand) = (short)((ulong)*(ushort*)(vars + operand) / (ulong)(long)(int)(uint)*(ushort*)target); return false;
            case 0x54: *(short*)(vars + operand) = (short)((ulong)*(ushort*)(vars + operand) % (ulong)(long)(int)(uint)*(ushort*)target); return false;
            case 0x55: *(short*)(vars + operand) = (short)(*(short*)(vars + operand) << ((byte)*(ushort*)target & 0x1f)); return false;
            case 0x56: *(ushort*)(vars + operand) = (ushort)(*(ushort*)(vars + operand) >> ((byte)*(ushort*)target & 0x1f)); return false;
            case 0x57: *(short*)(vars + operand) = (short)(CrtRand() % (int)(uint)*(ushort*)target); return false;

            // ---- mot : cible op= variable[immediate] --------------------------------------
            case 0x58: *(ushort*)target = *(ushort*)(vars + operand); return false;
            case 0x59: *(ushort*)target = (ushort)(*(ushort*)target + *(short*)(vars + operand)); return false;
            case 0x5a: *(ushort*)target = (ushort)(*(ushort*)target - *(short*)(vars + operand)); return false;
            case 0x5b: *(ushort*)target = (ushort)(*(ushort*)target | *(ushort*)(vars + operand)); return false;
            case 0x5c: *(ushort*)target = (ushort)(*(ushort*)target & *(ushort*)(vars + operand)); return false;
            case 0x5d: *(ushort*)target = (ushort)(*(ushort*)target ^ *(ushort*)(vars + operand)); return false;
            case 0x5e: *(ushort*)target = (ushort)(*(short*)(vars + operand) * *(ushort*)target); return false;
            case 0x5f: *(ushort*)target = (ushort)((ulong)*(ushort*)target / (ulong)(long)(int)(uint)*(ushort*)(vars + operand)); return false;
            case 0x60: *(ushort*)target = (ushort)((ulong)*(ushort*)target % (ulong)(long)(int)(uint)*(ushort*)(vars + operand)); return false;
            case 0x61: *(ushort*)target = (ushort)(*(ushort*)target << ((byte)*(ushort*)(vars + operand) & 0x1f)); return false;
            case 0x62: *(ushort*)target = (ushort)(*(ushort*)target >> ((byte)*(ushort*)(vars + operand) & 0x1f)); return false;
            case 0x63: *(ushort*)target = (ushort)(CrtRand() % (int)(uint)*(ushort*)(vars + operand)); return false;

            // ---- mot : comparaison avec l'immediate ---------------------------------------
            case 0x64: return *(ushort*)target != operand;
            case 0x65: return *(ushort*)target == operand;
            case 0x66: return operand < *(ushort*)target;
            case 0x67: return *(ushort*)target < operand;
            case 0x68: return operand <= *(ushort*)target;
            case 0x69: return *(ushort*)target <= operand;
            case 0x6a: return (*(ushort*)target & operand) != operand;
            case 0x6b: return (*(ushort*)target & operand) == operand;
            case 0x6c: return (*(ushort*)target & operand) == 0;
            case 0x6d: return (*(ushort*)target & operand) != 0;
            case 0x6e: return *(ushort*)target % operand == 0;
            case 0x6f: return *(ushort*)target % operand != 0;

            // ---- mot : comparaison avec variable[immediate] -------------------------------
            case 0x70: return *(ushort*)target != *(ushort*)(vars + operand);
            case 0x71: return *(ushort*)target == *(ushort*)(vars + operand);
            case 0x72: return *(ushort*)(vars + operand) < *(ushort*)target;
            case 0x73: return *(ushort*)target < *(ushort*)(vars + operand);
            case 0x74: return *(ushort*)(vars + operand) <= *(ushort*)target;
            case 0x75: return *(ushort*)target <= *(ushort*)(vars + operand);
            case 0x76: return (*(ushort*)target & *(ushort*)(vars + operand)) != *(ushort*)(vars + operand);
            case 0x77: return (*(ushort*)target & *(ushort*)(vars + operand)) == *(ushort*)(vars + operand);
            case 0x78: return (*(ushort*)(vars + operand) & *(ushort*)target) == 0;
            case 0x79: return (*(ushort*)(vars + operand) & *(ushort*)target) != 0;
            case 0x7a: return (ulong)*(ushort*)target % (ulong)(long)(int)(uint)*(ushort*)(vars + operand) == 0;
            case 0x7b: return (ulong)*(ushort*)target % (ulong)(long)(int)(uint)*(ushort*)(vars + operand) != 0;

            // ---- double mot : cible op= immediate -----------------------------------------
            case 0x80: *(uint*)target = operand; return false;
            case 0x81: *(uint*)target += operand; return false;
            case 0x82: *(uint*)target -= operand; return false;
            case 0x83: *(uint*)target |= operand; return false;
            case 0x84: *(uint*)target &= operand; return false;
            case 0x85: *(uint*)target ^= operand; return false;
            case 0x86: *(uint*)target = operand * *(uint*)target; return false;
            case 0x87: *(uint*)target /= operand; return false;
            case 0x88: *(uint*)target %= operand; return false;
            case 0x89: *(uint*)target <<= immediate & 0x1f; return false;
            case 0x8a: *(uint*)target >>= immediate & 0x1f; return false;
            case 0x8b: *(uint*)target = (uint)CrtRand() % operand; return false;

            // ---- double mot : variable[immediate] op= cible -------------------------------
            case 0x8c: *(uint*)(vars + operand) = *(uint*)target; return false;
            case 0x8d: *(uint*)(vars + operand) = (uint)(*(int*)(vars + operand) + *(uint*)target); return false;
            case 0x8e: *(uint*)(vars + operand) = (uint)(*(int*)(vars + operand) - *(uint*)target); return false;
            case 0x8f: *(uint*)(vars + operand) |= *(uint*)target; return false;
            case 0x90: *(uint*)(vars + operand) &= *(uint*)target; return false;
            case 0x91: *(uint*)(vars + operand) ^= *(uint*)target; return false;
            case 0x92: *(uint*)(vars + operand) = (uint)(*(uint*)target * *(int*)(vars + operand)); return false;
            case 0x93: *(uint*)(vars + operand) /= *(uint*)target; return false;
            case 0x94: *(uint*)(vars + operand) %= *(uint*)target; return false;
            case 0x95: *(int*)(vars + operand) = *(int*)(vars + operand) << ((byte)*(uint*)target & 0x1f); return false;
            case 0x96: *(uint*)(vars + operand) = *(uint*)(vars + operand) >> ((byte)*(uint*)target & 0x1f); return false;
            case 0x97: *(uint*)(vars + operand) = (uint)CrtRand() % *(uint*)target; return false;

            // ---- double mot : cible op= variable[immediate] -------------------------------
            case 0x98: *(uint*)target = *(uint*)(vars + operand); return false;
            case 0x99: *(uint*)target = (uint)(*(uint*)target + *(int*)(vars + operand)); return false;
            case 0x9a: *(uint*)target = (uint)(*(uint*)target - *(int*)(vars + operand)); return false;
            case 0x9b: *(uint*)target |= *(uint*)(vars + operand); return false;
            case 0x9c: *(uint*)target &= *(uint*)(vars + operand); return false;
            case 0x9d: *(uint*)target ^= *(uint*)(vars + operand); return false;
            case 0x9e: *(uint*)target = (uint)(*(int*)(vars + operand) * *(uint*)target); return false;
            case 0x9f: *(uint*)target /= *(uint*)(vars + operand); return false;
            case 0xa0: *(uint*)target %= *(uint*)(vars + operand); return false;
            case 0xa1: *(uint*)target <<= (byte)*(uint*)(vars + operand) & 0x1f; return false;
            case 0xa2: *(uint*)target >>= (byte)*(uint*)(vars + operand) & 0x1f; return false;
            case 0xa3: *(uint*)target = (uint)CrtRand() % *(uint*)(vars + operand); return false;

            // ---- double mot : comparaison avec l'immediate --------------------------------
            case 0xa4: return *(uint*)target != operand;
            case 0xa5: return *(uint*)target == operand;
            case 0xa6: return operand < *(uint*)target;
            case 0xa7: return *(uint*)target < operand;
            case 0xa8: return operand <= *(uint*)target;
            case 0xa9: return *(uint*)target <= operand;
            case 0xaa: return (operand & *(uint*)target) != operand;
            case 0xab: return (operand & *(uint*)target) == operand;
            case 0xac: return (operand & *(uint*)target) == 0;
            case 0xad: return (operand & *(uint*)target) != 0;
            case 0xae: return *(uint*)target % operand == 0;
            case 0xaf: return *(uint*)target % operand != 0;

            // ---- double mot : comparaison avec variable[immediate] ------------------------
            case 0xb0: return *(uint*)target != *(uint*)(vars + operand);
            case 0xb1: return *(uint*)target == *(uint*)(vars + operand);
            case 0xb2: return *(uint*)(vars + operand) < *(uint*)target;
            case 0xb3: return *(uint*)target < *(uint*)(vars + operand);
            case 0xb4: return *(uint*)(vars + operand) <= *(uint*)target;
            case 0xb5: return *(uint*)target <= *(uint*)(vars + operand);
            case 0xb6: return (*(uint*)target & *(uint*)(vars + operand)) != *(uint*)(vars + operand);
            case 0xb7: return (*(uint*)target & *(uint*)(vars + operand)) == *(uint*)(vars + operand);
            case 0xb8: return (*(uint*)(vars + operand) & *(uint*)target) == 0;
            case 0xb9: return (*(uint*)(vars + operand) & *(uint*)target) != 0;
            case 0xba: return *(uint*)target % *(uint*)(vars + operand) == 0;
            case 0xbb: return *(uint*)target % *(uint*)(vars + operand) != 0;

            // Ghidra: `default: return false;` — pose entre les cas 0x3b et 0x40, donc il couvre
            // les trous entre les trois groupes de largeur aussi bien que les opcodes hors table.
            default: return false;
        }
    }
}
