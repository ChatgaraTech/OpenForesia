// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{
    /// <summary>
    /// Ghidra: DAT_005de728 — le numero du jeu de modeles que la visionneuse charge : l'indice
    /// commun dans les listes <c>+0x10</c> (textures), <c>+0x2c</c> (modeles) et <c>+0x7c</c>
    /// (animations) du repertoire d'etat. Non initialise dans le binaire (zero au chargement), et
    /// lu ou ecrit par les seules fonctions de ce fichier.
    /// </summary>
    private static int s_ViewerModelSet_005de728;

    /// <summary>
    /// Ghidra: FUN_005315d0 @ 0x005315d0 — <b>l'etat de jeu 8, une visionneuse de modeles 3D</b>
    /// laissee dans l'executable. Transcrit contre le desassemblage
    /// (<c>Legacy/Raw/Blaze/FUN_005315D0.asm</c>, les trois fonctions d'affilee).
    ///
    /// <para><b>Inatteignable dans le jeu d'origine.</b> Aucune ecriture de 8 dans
    /// <c>DAT_005de510</c> n'existe : ni dans <c>Blaze.c</c>, ni dans le binaire (releve du
    /// 2026-09-26 sur les trente et un sites qui ecrivent l'adresse — vingt-deux immediats, aucun
    /// ne vaut 8 ; neuf registres, tous dans des fonctions deja transcrites qui ecrivent 0). Seul le
    /// repartiteur <c>FUN_00537ca0</c> la connait. C'est un outil de mise au point dont l'entree a
    /// ete retiree, comme la ligne REBOOT l'a ete des menus ordinaires.</para>
    ///
    /// <para>La boucle : charger le jeu de modeles courant, le montrer jusqu'a ce que
    /// <see cref="RunModelViewerLoop_005316E0"/> rende 0 (SELECT + START) ; elle rend 1 quand on
    /// a choisi un autre jeu, et l'on recharge. Puis l'etat 0.</para>
    /// </summary>
    private static void RunModelViewerState_005315D0()
    {
        int result;
        do
        {
            LoadModelViewerResources_005315F0();
            result = RunModelViewerLoop_005316E0();
        } while (result != 0);
        s_GameStateId_005de510 = result;
    }

    /// <summary>
    /// Ghidra: FUN_005315f0 @ 0x005315f0 — charge le jeu de modeles n° <see cref="s_ViewerModelSet_005de728"/>.
    /// Le meme chargement que <see cref="LoadCharacterSelectResources"/>, a une difference pres :
    /// <b>une seule entree de chaque liste</b>, celle du jeu choisi, et toujours rangee dans
    /// l'emplacement 0 (de la table de pages <c>0x005e8f00</c>, de la table de modeles
    /// <c>0x005e96a0</c>, de la table d'animations <c>0x005e9640</c>). L'animation s'empile a la
    /// suite du modele, a partir de <c>MainMem + 0x138000</c>.
    /// </summary>
    private static void LoadModelViewerResources_005315F0()
    {
        // Ghidra: FUN_0040a600() — corps vide, voir LoadCharacterSelectResources. Non transcrit.

        s_Unknown_005e97c8 = Res.LoadResourceEntry((int*)(nint)s_Unknown_005e92b0, Mem.s_MainResourceDirectory_005e97c4);   // Ghidra: FUN_00529b50
        Res.FixupResourceHeader(Mem.s_MainResourceDirectory_005e97c4);                                                  // Ghidra: FUN_00532070

        byte* directory = Mem.s_MainResourceDirectory_005e97c4;
        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "FUN_005315f0: winsys3d.dll's MainMem export resolves to null, so the model viewer's " +
                "scratch (MainMem + 0x1b0000) and model area (MainMem + 0x138000) have nowhere to live.");
        }

        // La texture du jeu : chargee dans le tampon de travail, decodee dans l'entree 0 des pages.
        int* texture = *(int**)(directory + 0x10) + s_ViewerModelSet_005de728 * 2;
        byte* scratch = mainMem + 0x1b0000;
        Res.LoadResourceEntry((int*)(nint)texture[0], scratch);                                         // Ghidra: FUN_00529b50
        Res.DecodeResourceChunk(scratch, (ushort*)Mem.s_EntityDecodeTable_005e8f00, (byte*)(texture + 1)); // Ghidra: FUN_005322f0

        // Le paquet de modeles, puis celui d'animations a la fin du premier.
        byte* modelArea = mainMem + 0x138000;
        int* model = *(int**)(directory + 0x2c) + s_ViewerModelSet_005de728 * 2;
        byte* next = Res.LoadResourceEntry((int*)(nint)model[0], modelArea);                             // Ghidra: FUN_00529b50
        RelocateModelBundle((int*)modelArea, (byte*)model, (byte**)s_ModelTable_005e96a0);                // Ghidra: FUN_00532640

        int animation = (*(int**)(directory + 0x7c))[s_ViewerModelSet_005de728];
        Res.LoadResourceEntry((int*)(nint)animation, next);                                             // Ghidra: FUN_00529b50
        RelocateAnimationBundle((int*)next, (byte**)Mem.s_AnimationTable_005e9640);                     // Ghidra: FUN_005326d0
    }

    /// <summary>
    /// Ghidra: FUN_005316e0 @ 0x005316e0 — la boucle d'affichage de la visionneuse. Rend 0 pour
    /// quitter, 1 pour recharger un autre jeu de modeles.
    ///
    /// <para><b>Le modele montre</b> est l'enregistrement de groupe 0 (<c>0x005e93c0</c>, pas 0x9c)
    /// utilise comme entite de scene — les memes champs que <c>RunDestinationSelect</c> :
    /// <c>+0x18</c> l'animation, <c>+0x1a</c> l'image, <c>+0x1c..+0x24</c> la position,
    /// <c>+0x28..+0x2c</c> l'echelle (ici 0x1800, 1,5).</para>
    ///
    /// <para><b>Les commandes</b>, lues sur la manette du chef :</para>
    /// <list type="bullet">
    /// <item>SELECT + START (<c>0x0009</c> a l'appui) : quitter ;</item>
    /// <item>SELECT seul (<c>0x0001</c>) : bascule lecture continue / image par image ;</item>
    /// <item>START (<c>0x0008</c>) : camera remise a sa pose de depart ;</item>
    /// <item>CARRE tenu + gauche/droite en repetition : choisir le jeu de modeles, charge au
    /// relachement de CARRE ;</item>
    /// <item>sinon, la croix directionnelle fait tourner la camera (0x40 par image), L1/R1 la
    /// deplacent en X et TRIANGLE/CROIX en Z (0x20), L2/R2 a l'appui changent d'animation, et en
    /// mode image par image ROND avance d'une image.</item>
    /// </list>
    ///
    /// <para><b>Ce que Ghidra rend en <c>unaff_EBX</c> / <c>unaff_ESI</c> / <c>unaff_EBP</c></b> :
    /// trois registres que le prologue initialise bel et bien. <c>ebx</c> est le zero de la
    /// fonction ; la bascule est le local <c>[esp+0x18]</c>, mis a zero avant l'appel a
    /// <c>FUN_0052a1e0</c> ; <c>ebp</c> porte le choix courant, recopie a <c>[esp+0x10]</c>. Le curseur
    /// de primitives passe dans le local <c>[esp+0x14]</c>.</para>
    /// </summary>
    private static int RunModelViewerLoop_005316E0()
    {
        int result = 0;          // Ghidra: [esp+0x28]
        int frameByFrame = 0;    // Ghidra: [esp+0x18]

        uint* otBase, otEntry, primCursor;
        FrameBegin(&otBase, &otEntry, &primCursor);      // Ghidra: FUN_0052a1e0

        // Le nombre de jeux de modeles : la longueur de la liste +0x7c, terminee par zero.
        int setCount = 0;                                // Ghidra: edi / [esp+0x20]
        for (int* entry = *(int**)(Mem.s_MainResourceDirectory_005e97c4 + 0x7c); *entry != 0; entry++)
            setCount++;

        int selection = s_ViewerModelSet_005de728;       // Ghidra: ebp / [esp+0x10]
        Inp.JoystickResetState(false);                   // Ghidra: FUN_00504b10(0)

        byte* entity = Inp.s_SlotFlags_005e93c0;         // l'enregistrement 0, adresses absolues
        *(short*)(entity + 0x28) = 0x1800;               // Ghidra: DAT_005e93e8
        *(short*)(entity + 0x2a) = 0x1800;
        *(short*)(entity + 0x2c) = 0x1800;

        short* camera = (short*)Mem.s_CameraVector_00604928;
        camera[0] = 0x200;
        camera[1] = 0x800;
        camera[2] = 0;
        Mem.s_Unknown_006008d0 = 0;
        Mem.s_Unknown_006008d4 = 0;
        Mem.s_Unknown_006008d8 = 0;
        Mem.CameraTranslation[0] = 0;                    // Ghidra: DAT_006008f4
        Mem.CameraTranslation[1] = 0;
        Mem.CameraTranslation[2] = 0x200;                // Ghidra: DAT_006008fc

        entity[4] = 0;                                   // Ghidra: DAT_005e93c4 & 0xffffff00
        *(short*)(entity + 0x18) = 0;                    // Ghidra: DAT_005e93d8
        *(short*)(entity + 0x1a) = 0;
        *(short*)(entity + 0x36) = 1;
        *(short*)(entity + 0x30) = 0;
        *(short*)(entity + 0x32) = 0;
        *(short*)(entity + 0x34) = 0;
        *(int*)(entity + 0x1c) = 0;
        *(int*)(entity + 0x20) = 0;
        *(int*)(entity + 0x24) = 0;
        *(uint*)(entity + 0x38) = 0xffffff;
        *(short*)(entity + 0x0a) = 0;                    // Ghidra: DAT_005e93ca

        Mem.s_ScenePitch_005e9308 = 0x200;
        Mem.s_SceneYaw_005e930a = 0x380;
        Mem.s_SceneRoll_005e930c = 0;
        Mem.s_BackColourR_005e93a0 = 0x80;
        Mem.s_BackColourG_005e93a2 = 0x80;
        Mem.s_BackColourB_005e93a4 = 0x80;
        Winsys3d.Ordinal_376(0x80, 0x80, 0x80);          // libgte SetBackColor

        while (true)
        {
            AnimatePalettes(Mem.s_MainResourceDirectory_005e97c4);   // Ghidra: FUN_00533000

            byte* leader = Inp.s_SlotFlags_005e93c0 + (byte)Mem.s_Unknown_005de538 * 0x9c;
            byte* pad = Inp.s_InputStateBase_00604a20 + Inp.s_PlayerSlots_005e9188[leader[8] * 0x4c] * 0x10;   // Ghidra: esi

            ushort pressed = *(ushort*)(pad + 2);
            if ((pressed & 9) == 9) break;

            if ((pressed & 1) != 0) frameByFrame ^= 1;
            if ((pressed & 8) != 0)
            {
                camera[0] = 0x200;
                camera[1] = 0x800;
                camera[2] = 0;
                Mem.CameraTranslation[0] = 0;
                Mem.CameraTranslation[1] = 0;
                Mem.CameraTranslation[2] = 0x200;
            }

            ushort held = *(ushort*)pad;
            if ((held & 0x8000) != 0)
            {
                ushort repeat = *(ushort*)(pad + 6);
                if ((repeat & 0x80) != 0) selection--;
                if ((repeat & 0x20) != 0) selection++;
                if (selection < 0) selection = setCount - 1;
                if (selection >= setCount) selection = 0;
            }
            else
            {
                if (s_ViewerModelSet_005de728 != selection)
                {
                    s_ViewerModelSet_005de728 = selection;
                    result = 1;
                    break;
                }

                if ((held & 0x10) != 0) camera[0] -= 0x40;
                if ((pad[0] & 0x40) != 0) camera[0] += 0x40;
                if ((pad[0] & 0x80) != 0) camera[1] += 0x40;
                if ((pad[0] & 0x20) != 0) camera[1] -= 0x40;
                if ((pad[1] & 0x04) != 0) Mem.CameraTranslation[0] -= 0x20;
                if ((pad[1] & 0x08) != 0) Mem.CameraTranslation[0] += 0x20;
                if ((pad[1] & 0x10) != 0) Mem.CameraTranslation[2] -= 0x20;
                if ((pad[1] & 0x40) != 0) Mem.CameraTranslation[2] += 0x20;
                if ((pad[3] & 0x01) != 0)
                {
                    (*(short*)(entity + 0x18))--;
                    *(short*)(entity + 0x1a) = 0;
                }
                if ((*(ushort*)(pad + 2) & 0x200) != 0)
                {
                    (*(short*)(entity + 0x18))++;
                    *(short*)(entity + 0x1a) = 0;
                }
            }

            // L'animation courante, bornee par le compte range a [-4] du tableau d'offsets : lu en
            // mot pour le repli (`mov cx,[edx-4]`), en dword pour la borne, comparee NON SIGNEE (`jb`).
            uint setSlot = (uint)entity[4] * 4;          // Ghidra: DAT_005e93c4 & 0xff
            byte** animations = *(byte***)(Mem.s_AnimationTable_005e9640 + setSlot);
            short clip = *(short*)(entity + 0x18);
            if (clip < 0)
            {
                clip = (short)(*(short*)((byte*)animations - 4) - 1);
                *(short*)(entity + 0x18) = clip;
            }
            if ((uint)(int)clip >= *(uint*)((byte*)animations - 4)) *(short*)(entity + 0x18) = 0;

            Winsys3d.Ordinal_379(400);                   // libgte SetGeomScreen(400)
            Winsys3d.Ordinal_378(0xa0, 0xbe);            // libgte SetGeomOffset(160, 190)
            RotMatrix(camera, (short*)Mem.s_CameraMatrix_006008e0);   // Ghidra: FUN_0051c300

            byte* current = animations[*(short*)(entity + 0x18)];
            if (frameByFrame == 0 || (pad[3] & 0x20) != 0)
            {
                short frame = (short)(*(short*)(entity + 0x1a) + 1);
                *(short*)(entity + 0x1a) = frame;
                if (frame >= *(ushort*)(current + 0x16)) *(short*)(entity + 0x1a) = 0;
            }

            // Le mot d'attributs : l'original pousse edx entier, dont seul le mot bas
            // (DAT_005e93ca) est pose ; le mot haut est un reste de registre (le pointeur
            // d'animation ou l'image sign-etendue selon la branche). DrawAnimatedModel n'en lit que
            // le mot bas (`(short)extra`, `extra & 0xff`), il n'est donc pas reproduit.
            primCursor = Mdl.DrawAnimatedModel(                                     // Ghidra: FUN_0051d1f0
                otBase, primCursor, current,
                *(int**)(s_ModelTable_005e96a0 + setSlot),
                (int*)(entity + 0x1c), *(short*)(entity + 0x1a), *(ushort*)(entity + 0x0a));

            // Deux sommes sur les pieces du modele (mots +0x1a et +0x1e), aussitot abandonnees :
            // les registres esi/edi sont ecrases avant toute lecture. Le reste d'un affichage de
            // mise au point retire a la compilation ; sans effet, transcrit pour la lecture memoire.
            {
                byte** parts = *(byte***)(s_ModelTable_005e96a0 + setSlot);
                int sumA = 0, sumB = 0;
                for (uint n = *(ushort*)(animations[*(short*)(entity + 0x18)] + 0x14); n != 0; n--, parts++)
                {
                    sumA += *(ushort*)(*parts + 0x1a);
                    sumB += *(ushort*)(*parts + 0x1e);
                }
                _ = sumA;
                _ = sumB;
            }

            FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);   // Ghidra: FUN_0052a280
        }

        FrameEnd(&otBase, &otEntry, &primCursor, 3, 1);
        return result;
    }
}
