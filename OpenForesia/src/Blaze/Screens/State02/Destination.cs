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
    // Écran de choix de destination — l'état de jeu 2 (Ghidra: FUN_00537da0)
    //
    // Une carte du monde : douze destinations dont seules certaines sont débloquées, une caméra qui
    // glisse vers celle que vise le curseur, le modèle 3D animé du groupe, et 32 particules en fond.
    // Sortie sur START ou CARRÉ, vers l'état de jeu 3.
    //
    // Toutes les tables ci-dessous sont relevées dans `.data` de Legacy/Blaze.exe.
    // =====================================================================

    // Ghidra: &DAT_0056d240 — douze positions d'icône empaquetées (x en mot bas, y en mot haut). La
    // boucle de rendu s'arrête à 0x56d26d, ce qui donne exactement douze entrées.
    private static readonly uint[] s_DestinationIconPos_0056d240 =
    [
        0x00000000, 0x00800078, 0x00000000, 0x00000000,
        0x009800c0, 0x00200058, 0x00400038, 0x00980040,
        0x00000000, 0x00780020, 0x00000000, 0x00000000,
    ];

    // Ghidra: &DAT_0056d270 (x) et &DAT_0056d272 (y) — la pose de caméra visée par zone, deux shorts
    // par entrée, pas de 4.
    private static readonly short[] s_DestinationCamX_0056d270 =
        [0x8a, 0x87, 0xa5, 0xca, 0xd4, 0x64, 0x4b, 0x53, 0x28, 0x2d, 0x87, 0x00];
    private static readonly short[] s_DestinationCamY_0056d272 =
        [0xc3, 0x8c, 0x23, 0x6e, 0xaf, 0x32, 0x55, 0xaa, 0x2d, 0x87, 0x8c, 0x00];

    // Ghidra: &DAT_0056d2a0 et &DAT_0056d2b0 — zone → index de menu, et l'inverse. Vérifiés
    // réciproques sur 0..9 (par exemple zone 6 → index 7 et index 7 → zone 6), ce qui les confirme
    // l'une l'autre ; les entrées 10 et 11 valent 0 des deux côtés.
    private static readonly byte[] s_AreaToMenuIndex_0056d2a0 =
        [0, 1, 4, 3, 2, 5, 7, 9, 6, 8, 0, 0];
    private static readonly byte[] s_MenuIndexToArea_0056d2b0 =
        [0, 1, 4, 3, 2, 5, 8, 6, 9, 7, 0, 0];

    // Ghidra: &DAT_0056c978 — le descripteur d'interface de cet écran, une instance de plus du type
    // à 0x8c8 octets. Ses 26 dwords initialisés relevés dans le binaire ; queue nulle.
    private static byte* s_DestinationDescriptorStorage;
    private static byte* s_DestinationDescriptor_0056c978
        => s_DestinationDescriptorStorage != null
            ? s_DestinationDescriptorStorage
            : (s_DestinationDescriptorStorage = BuildDestinationDescriptor());

    private static byte* BuildDestinationDescriptor()
    {
        byte* block = AllocLowZeroed((uint)UiDescriptorStride);
        uint* f = (uint*)block;
        f[0] = 0x00000031; f[1] = 0x00b00008; f[2] = 0x001800a0; f[3] = 0x00000000;
        f[4] = 0x021a0000; f[5] = 0x0000000c; f[6] = 0x010f0808; f[7] = 0x02cf1020;
        f[8] = 0x01070c10; f[9] = 0x01000000; f[11] = 0x01000000;
        f[12] = 0x000f2844; f[13] = 0x01000000; f[14] = 0x000f0c00; f[15] = 0x01000000;
        f[16] = 0x010f0000; f[17] = 0x01000000; f[19] = 0x01000000; f[21] = 0x01000000;
        f[23] = 0x01000000; f[25] = 0x01000000;
        return block;
    }

    // **Deux champs de ce descripteur, pas des globales isolées** — 0x56c97c est son +4 et 0x56c980
    // son +8, c'est-à-dire le x du panneau et sa largeur, aux mêmes décalages que sur toutes les
    // autres instances. La boucle les lit et les réécrit chaque image pour faire glisser le panneau
    // d'un bord à l'autre selon la position de la caméra.
    private static ref short s_PanelX_0056c97c => ref *(short*)(s_DestinationDescriptor_0056c978 + 4);
    private static ref short s_PanelW_0056c980 => ref *(short*)(s_DestinationDescriptor_0056c978 + 8);

    // Ghidra: &DAT_005de824 — 32 particules de 0x9c octets. Disposition déduite de son
    // initialisation et de ce que <see cref="EmitParticle"/> lit :
    //   +0x00 x   +0x04 y   +0x08 z (avance de 0x4000 par image, resemée passé 0x7fffff)
    //   +0x0c/0x0e/0x10 échelle (0x1000)   +0x12 0   +0x14 et +0x18 nuls
    //   +0x1c couleur   +0x20 descripteur d'atlas (ce que EmitParticle lit en particle[8])
    //   +0x24 variante (0..6)
    //
    // Piège : la boucle d'initialisation part de `&DAT_005de828` (base+4) et celle de mise à jour de
    // `&DAT_005de82c` (base+8). Les mêmes indices `piVar11[n]` y désignent donc des champs
    // différents — c'est ce décalage qui fait passer le compteur avancé chaque image pour une durée
    // de vie alors que c'est le Z, et la couleur pour +0x18 alors qu'elle est en +0x1c.
    private const int ParticleCount = 0x20;
    private const int ParticleStride = 0x9c;
    private static readonly byte* s_Particles_005de824 =
        (byte*)NativeMemory.AllocZeroed(ParticleCount * ParticleStride);

    // Ghidra: DAT_005de52c / DAT_005de530 — les deux octets que la sortie de cet écran prend dans la
    // table +0x74 du répertoire de ressources, à côté de DAT_005de528 (la zone choisie).
    private static byte s_Unknown_005de52c, s_Unknown_005de530;

    /// <summary>
    /// Ghidra: FUN_00537da0 @ 0x00537da0 — l'état de jeu 2 : le choix de la destination sur la carte
    /// du monde. Appelée par <see cref="RunGameStateTick"/> après <see cref="LoadMenuResources"/>
    /// (<c>FUN_00542160</c>) ; c'est l'appelant qui pose l'état de jeu 3 en sortant.
    ///
    /// <para>Le masque des destinations atteignables se construit bit à bit depuis
    /// <c>DAT_005e901e</c> — un seul octet de progression, éclaté en deux masques : celui des
    /// icônes à dessiner et celui des index sur lesquels le curseur a le droit de s'arrêter. Le
    /// curseur saute donc les destinations verrouillées, dans les deux sens.</para>
    ///
    /// <para><b>Un point venait du désassemblage, pas de la pseudo-C</b> : Ghidra rend l'appel au
    /// rendu de modèle <c>unaff_EBP = FUN_0051d1f0(local_40, unaff_EBP, ...)</c>, ce qui suggère un
    /// curseur de primitives distinct qui ne reviendrait jamais dans le vrai. L'assembleur dit
    /// autre chose (<c>Legacy/Raw/Blaze/FUN_00537da0.asm</c>) : les deux derniers <c>PUSH</c> avant
    /// le <c>CALL</c> sont <c>[ESP+0x30]</c> et <c>[ESP+0x24]</c>, soit otBase et primCursor, et le
    /// <c>MOV [ESP+0x14],EAX</c> juste après le <c>ADD ESP,0x1c</c> réécrit bien le résultat dans
    /// primCursor. C'est la convention habituelle, rien de particulier.</para>
    ///
    /// <para>Les deux corrections d'aspect appliquées aux icônes sont des divisions entières exactes
    /// une fois le codage de MSVC démêlé : <c>x = (x - 0x80) * 320 / 256 + 0x80</c> (rendu par
    /// Ghidra avec son correctif de signe <c>+ (v &gt;&gt; 31 &amp; 0xff)</c>) et
    /// <c>y = (y - 0x70) * 242 / 224 + 0x70</c> (rendu par une multiplication magique par
    /// 0x92492493). Elles ramènent la carte d'un cadre 256x224 au 320x240 de l'écran.</para>
    /// </summary>
    private static void RunDestinationSelect()
    {
        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);   // Ghidra: FUN_0052a1e0
        PlayMusicTrack(5);                            // Ghidra: FUN_00529ad0(5)

        int fadeStep = 0;                             // Ghidra: local_30, 0..0x17

        // Les 32 particules, semées au hasard dans un cube.
        byte* particle = s_Particles_005de824;
        for (int i = 0; i < ParticleCount; i++, particle += ParticleStride)
        {
            *(int*)(particle + 0x00) = (int)(((CrtRand() & 0x3ff) - 0x1ff) * 0x1000);
            *(int*)(particle + 0x04) = (int)(((CrtRand() & 0x3ff) - 0x1ff) * 0x1000);
            *(int*)(particle + 0x14) = 0;
            *(int*)(particle + 0x18) = 0;
            *(int*)(particle + 0x08) = (int)(((CrtRand() & 0xfff) - 0x7ff) * 0x1000);
            *(short*)(particle + 0x0c) = 0x1000;
            *(short*)(particle + 0x0e) = 0x1000;
            *(short*)(particle + 0x10) = 0x1000;
            *(short*)(particle + 0x12) = 0;
            *(int*)(particle + 0x1c) = 0;
            particle[0x24] = (byte)(CrtRand() % 7);
            *(int*)(particle + 0x20) = *(int*)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94) + 0xc);
        }

        JoystickResetState(false);                    // Ghidra: FUN_00504b10(0)

        // Ghidra: DAT_005e901e — l'octet de progression, éclaté en deux masques. Transcrit
        // opération par opération : le remaniement bit à bit n'a pas de forme plus courte fidèle.
        uint progress = s_PartyAbilityMask_005e9000[0x1e];
        uint spread = (progress & 0xfc) << 1;
        uint iconMask = (spread | (progress & 3)) << 1 | 9;
        uint navMask = (progress & 1) != 0 ? 3u : 1u;
        int extraIconFlag = (int)((progress & 2) << 1);
        if ((progress & 2) != 0) navMask |= 0x10;
        navMask = (spread & 8) != 0 ? navMask | 0xcu : navMask | 8u;
        if ((spread & 0x10) != 0) navMask |= 0x20;
        if ((spread & 0x20) != 0) navMask |= 0x80;
        if ((spread & 0x40) != 0) navMask |= 0x200;
        if ((spread & 0x80) != 0) navMask |= 0x40;
        if ((spread & 0x100) != 0) navMask |= 0x100;

        uint menuIndex = s_AreaToMenuIndex_0056d2a0[s_Unknown_005de528 & 0xff];
        int camY = s_DestinationCamY_0056d272[s_MenuIndexToArea_0056d2b0[menuIndex]];
        int camX = s_DestinationCamX_0056d270[s_MenuIndexToArea_0056d2b0[menuIndex]];

        // Le bloc de dessin sur pile : le même à sept champs que RunDebugPartySelect.
        int* draw = stackalloc int[8];
        byte* d = (byte*)draw;
        short* projected = stackalloc short[4];        // Ghidra: auStack_1c

        uint cursor = menuIndex;

        while (true)
        {
            uint before = menuIndex;
            s_Unknown_005de15c = 1;

            if (s_SmallTextureFlag == 0 && s_SizeSetting_005de160 > 7)
            {
                s_Unknown_005de154 = 0;
                s_Unknown_005de158 = (s_SizeSetting_005de160 - 0xb) * 0xc;
            }
            else
            {
                s_Unknown_005de154 = 0xa0;
                s_Unknown_005de158 = s_SizeSetting_005de160 * 0xc + 0x76;
            }

            *(short*)(d + 0x0c) = 0x1400;
            *(short*)(d + 0x0e) = 0x1149;

            // Ghidra: FUN_0040a600(5) — corps vide, confirmé ailleurs dans ce fichier. Non transcrit.
            AnimatePalettes(s_MainResourceDirectory_005e97c4);   // Ghidra: FUN_00533000

            byte* pad = CurrentPadState();
            ushort repeat = *(ushort*)(pad + 6);
            cursor = before;

            // Le curseur ne s'arrête que sur un index autorisé par navMask, d'où les deux boucles.
            // Le repli est asymétrique dans l'original — 0xb en montant, 0 en descendant.
            if ((repeat & 0xc0) != 0)
            {
                do
                {
                    menuIndex = cursor - 1;
                    if ((int)menuIndex < 0) menuIndex = 0xb;
                    cursor = menuIndex;
                } while ((navMask & (1u << (int)(menuIndex & 0x1f))) == 0);
            }
            if ((repeat & 0x30) != 0)
            {
                do
                {
                    menuIndex = cursor + 1;
                    if ((int)menuIndex > 10) menuIndex = 0;
                    cursor = menuIndex;
                } while ((navMask & (1u << (int)(menuIndex & 0x1f))) == 0);
            }
            if (before != cursor) PlayUiSound3A();     // Ghidra: FUN_0053db60

            // START (0x0008) ou CARRÉ (0x8000) : valider.
            if ((*(ushort*)(pad + 2) & 0x8008) != 0) break;

            // La caméra glisse par quarts vers la destination visée.
            *(short*)(s_CameraVector_00604928 + 0) = 0x200;
            *(short*)(s_CameraVector_00604928 + 2) = 0x800;
            int targetArea = s_MenuIndexToArea_0056d2b0[cursor];
            int nextX = camX - ((camX - s_DestinationCamX_0056d270[targetArea]) >> 2);
            int nextY = camY - ((camY - s_DestinationCamY_0056d272[targetArea]) >> 2);
            *(short*)(s_CameraVector_00604928 + 4) = 0;
            s_Unknown_006008d0 = 0;
            s_Unknown_006008d4 = 0;
            s_Unknown_006008d8 = 0;
            CameraTranslation[0] = 0;
            CameraTranslation[1] = 0;
            CameraTranslation[2] = 0x5dc;
            camY = nextY;
            camX = nextX;
            Ordinal_379(400);
            Ordinal_378(nextX, nextY);
            RotMatrix((short*)s_CameraVector_00604928, (short*)s_CameraMatrix_006008e0);

            // Le panneau d'information glisse vers le bord opposé à la caméra.
            s_PanelX_0056c97c += nextX < 0x81
                ? (short)(((0xe8 - s_PanelW_0056c980) - s_PanelX_0056c97c) >> 2)
                : (short)((8 - s_PanelX_0056c97c) >> 2);

            // Le modèle 3D animé du groupe, sur l'emplacement d'entité courant.
            uint slot = (uint)(s_Unknown_005de538 & 0xff);
            byte* entity = s_SlotFlags_005e93c0 + slot * 0x9c;
            *(short*)(entity + 0x28) = 0x1000;
            *(short*)(entity + 0x2a) = 0x1000;
            *(short*)(entity + 0x2c) = 0x1000;
            int assetIndex = entity[4] * 4;            // Ghidra: DAT_005e93c4
            *(short*)(entity + 0x36) = 1;
            *(short*)(entity + 0x30) = 0;
            *(short*)(entity + 0x32) = 0;
            *(short*)(entity + 0x34) = 0;
            *(int*)(entity + 0x1c) = 0;
            *(int*)(entity + 0x20) = 0;
            *(int*)(entity + 0x24) = 0;
            *(uint*)(entity + 0x38) = 0x1000000;
            *(short*)(entity + 0x18) = 1;

            byte* animEntry = *(byte**)(*(byte**)(s_PartyAnimationResult_005e9680 + assetIndex) + 4);
            short advanced = (short)(*(short*)(entity + 0x1a) + 1);
            *(short*)(entity + 0x1a) = advanced;
            if (advanced >= *(ushort*)(animEntry + 0x16)) *(short*)(entity + 0x1a) = 0;

            uint modelExtra = (uint)((advanced >> 0xf) << 0x10) | *(ushort*)(entity + 0x0a);
            primCursor = DrawAnimatedModel(
                otBase, primCursor, animEntry,
                *(int**)(s_PartyModelResult_005e9800 + assetIndex),
                (int*)(entity + 0x1c), *(short*)(entity + 0x1a), modelExtra);

            // La caméra revient au cadrage large pour les particules et la carte.
            *(short*)(s_CameraVector_00604928 + 0) = 700;
            *(short*)(s_CameraVector_00604928 + 2) = 500;
            *(short*)(s_CameraVector_00604928 + 4) = 0;
            s_Unknown_006008d0 = 0;
            s_Unknown_006008d4 = 0;
            s_Unknown_006008d8 = 0;
            CameraTranslation[0] = 0;
            CameraTranslation[1] = 0;
            CameraTranslation[2] = 0x5dc;
            Ordinal_379(400);
            Ordinal_378(0xa0, 0x78);
            RotMatrix((short*)s_CameraVector_00604928, (short*)s_CameraMatrix_006008e0);

            // Les 32 particules : celles qui restent à l'écran s'éclaircissent, celles qui sortent
            // du cycle sont resemées ailleurs.
            particle = s_Particles_005de824;
            for (int i = 0; i < ParticleCount; i++, particle += ParticleStride)
            {
                // Une particule visible s'éclaircit jusqu'au gris 0x808080.
                if (ProjectPoint((int*)particle, projected) != 0
                    && *(int*)(particle + 0x1c) != 0x808080)
                {
                    *(int*)(particle + 0x1c) += 0x80808;
                }

                primCursor = EmitParticle(otBase, primCursor, (int*)particle, 0, 0);

                // Elle avance en Z ; passé la limite, elle est resemée en x/y, reculée d'un tour et
                // remise au noir.
                int z = *(int*)(particle + 0x08);
                *(int*)(particle + 0x08) = z + 0x4000;
                if (z + 0x4000 > 0x7fffff)
                {
                    *(int*)(particle + 0x00) = (int)(((CrtRand() & 0x3ff) - 0x1ff) * 0x1000);
                    *(int*)(particle + 0x04) = (int)(((CrtRand() & 0x3ff) - 0x1ff) * 0x1000);
                    *(int*)(particle + 0x08) -= 0x1000000;
                    *(int*)(particle + 0x1c) = 0;
                    particle[0x24] = (byte)(CrtRand() % 7);
                }
            }

            // Les icônes de destination, une par bit posé dans iconMask.
            *(uint*)(d + 4) = 0x1000000;
            byte* atlas = *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94) + 4);
            *(byte**)d = atlas;
            for (short icon = 0; icon < 12; icon++)
            {
                if ((iconMask & (1u << (icon & 0x1f))) == 0) continue;

                byte* record = atlas + icon * 8;
                int iconX = (short)s_DestinationIconPos_0056d240[icon];
                int iconY = (short)(s_DestinationIconPos_0056d240[icon] >> 0x10);

                *(short*)(d + 8) = (short)(((short)(iconX + (record[2] >> 1)) - 0x80) * 0x140 / 0x100 + 0x80);
                *(short*)(d + 0x0c) = 0x1400;
                *(short*)(d + 0x0e) = 0x1149;
                *(short*)(d + 0x0a) = (short)(((short)((record[3] >> 1) - 1 + iconY) - 0x70) * 0xf2 / 0xe0 + 0x70);

                // Trois icônes sont décalées d'un pixel et dessinées un cran plus haut. Ghidra les
                // désigne par comparaison de pointeur (&DAT_0056d250/58/5c) ; ce sont les index 4, 6
                // et 7.
                if (icon == 4 || icon == 6 || icon == 7)
                {
                    *(short*)(d + 0x0a) += 1;
                    *(short*)(d + 0x0e) = 0x116d;
                }

                // Un pixel retiré de chaque côté le temps du tracé, puis rendu.
                bool shrunk = (sbyte)record[2] != 0;
                if (shrunk) { record[2]--; record[3]--; }
                *(short*)(d + 0x10) = icon;
                primCursor = DrawSpritePrimitive(otEntry, primCursor, draw, 0, 0);
                primCursor = DrawFlatPrimitive(otEntry, primCursor, draw);
                if (shrunk) { record[2]++; record[3]++; }
            }

            // L'icône clignotante de la destination courante, quand la progression l'autorise.
            if (extraIconFlag != 0)
            {
                short blink = (short)(fadeStep >> 2);
                byte* blinkAtlas = *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94) + 8);
                *(byte**)d = blinkAtlas;
                byte* record = blinkAtlas + blink * 8;
                *(short*)(d + 8) = (short)(((short)((record[2] >> 1) + 0xa0) - 0x80) * 0x140 / 0x100 + 0x80);
                *(short*)(d + 0x0a) = (short)(((short)((record[3] >> 1) - 1) - 0x70) * 0xf2 / 0xe0 + 0x70);
                record[2]--; record[3]--;
                *(short*)(d + 0x10) = blink;
                primCursor = DrawSpritePrimitive(otEntry, primCursor, draw, 0, 0);
                primCursor = DrawFlatPrimitive(otEntry, primCursor, draw);
                record[2]++; record[3]++;
            }

            // Le fond de carte.
            *(short*)(d + 8) = 0x80;
            *(short*)(d + 0x0a) = 0x70;
            *(short*)(d + 0x10) = 0;
            *(byte**)d = *(byte**)*(byte**)(s_MainResourceDirectory_005e97c4 + 0x94);
            primCursor = DrawSpritePrimitive(otEntry, primCursor, draw, 0, 0);
            primCursor = DrawFlatPrimitive(otEntry, primCursor, draw);

            // Le panneau de description de la destination visée.
            BindMenuLabels(
                *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x70)
                          + s_MenuIndexToArea_0056d2b0[cursor] * 4),
                0, s_DestinationDescriptor_0056c978, 0);
            primCursor = RunTextEngine(primCursor, s_DestinationDescriptor_0056c978);
            primCursor = DrawUiPanel(otBase, primCursor, s_DestinationDescriptor_0056c978);

            // La fenêtre d'affichage rétrécit à 256x224 le temps de l'envoi, puis revient à 320x240
            // — les deux DISPENV à la fois, champs +4 (largeur) et +6 (hauteur).
            *(short*)(s_DispEnv1_005e994c + 4) = 0x100;
            *(short*)(s_DispEnv0_005e98dc + 4) = 0x100;
            *(short*)(s_DispEnv1_005e994c + 6) = 0xe0;
            *(short*)(s_DispEnv0_005e98dc + 6) = 0xe0;
            s_Unknown_005de15c = 0;
            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
            *(short*)(s_DispEnv1_005e994c + 4) = 0x140;
            *(short*)(s_DispEnv0_005e98dc + 4) = 0x140;
            *(short*)(s_DispEnv1_005e994c + 6) = 0xf0;
            *(short*)(s_DispEnv0_005e98dc + 6) = 0xf0;

            fadeStep++;
            camX = nextX;
            if (fadeStep > 0x17) fadeStep = 0;
        }

        PlayUiSound38();                                // Ghidra: FUN_0053db80
        s_Unknown_005de528 = s_MenuIndexToArea_0056d2b0[cursor];
        s_Unknown_00566c84 = 0xff;
        byte* areaInfo = *(byte**)(*(byte**)(s_MainResourceDirectory_005e97c4 + 0x74)
                                   + (uint)s_Unknown_005de528 * 4);
        s_Unknown_00566c88 = 0xff;
        s_Unknown_005de52c = areaInfo[0];
        s_Unknown_005de530 = areaInfo[1];
        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);

        // Quatre des huit banques d'état, plus les paires de 0x5e9320.
        NativeMemory.Clear(s_StateBank3_005e9840, 0x40);
        NativeMemory.Clear(s_StateBank2_005e9060, 0x40);
        NativeMemory.Clear(s_StateBank1_005e9780, 0x40);
        NativeMemory.Clear(s_StateBank0_005e96e0, 0x40);
        ResetScenePairs_005e9320();

        // Retour à la ville (zone 0) : le groupe est soigné. `+0x148`/`+0x14a` sont le maximum et
        // `+0x14c`/`+0x14e` le courant — c'est cette recopie-là qui tranche le sens des deux paires,
        // que FinalizeCharacterStats laissait indécidable en leur donnant la même valeur.
        if (s_Unknown_005de528 == 0 && s_PartySize_005e9020 != 0)
        {
            byte* member = GetMainMemPtr() + 0xf0140;
            for (int i = s_PartySize_005e9020; i != 0; i--, member += 0x2000)
            {
                *(uint*)member &= 0xbfffffff;
                *(short*)(member + 0x0c) = (short)*(int*)(member + 0x08);
                *(ushort*)(member + 0x0e) = *(ushort*)(member + 0x0a);
                NativeMemory.Clear(member + 0x40, 0x80);
            }
        }
    }

    /// <summary>Ghidra: la boucle <c>puVar = &amp;DAT_005e9324; { puVar[-1] = 0; *puVar = -1; }</c>,
    /// huit paires de 0x5e9320 à 0x5e9360. Partagée par <c>FUN_00537da0</c> et la branche « pas
    /// d'état de monde » de <c>FUN_00536ea0</c>.</summary>
    private static void ResetScenePairs_005e9320()
    {
        for (int i = 0; i < 8; i++)
        {
            *(uint*)Arena(0x005e9320 + i * 8) = 0;
            *(uint*)Arena(0x005e9324 + i * 8) = 0xffffffff;
        }
    }
}
