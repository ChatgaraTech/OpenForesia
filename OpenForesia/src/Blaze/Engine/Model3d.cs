// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// `Blaze` porte encore ce que cette classe lit : le banc de registres GTE de Platform/Winsys3d.cs
// (voir plus bas) et le compteur d'emission s_Unknown_005e9818 de Screens/State00/MainMenu.cs.
using static OpenForesia.Blaze.Blaze;

namespace OpenForesia.Blaze;

/// <summary>
/// Le rendu de modele anime : la projection d'un personnage a travers le GTE emule et l'emission
/// de ses faces dans la liste d'affichage (Ghidra: 0x0051d).
///
/// <para>
/// <b>Elle ne possede aucune globale.</b> Sept methodes, zero champ - c'est la seule classe de
/// l'etape C dans ce cas. Sa conversion n'est donc pas un demenagement d'etat mais une mise au
/// net de ce qu'elle LIT : 11 symboles de <see cref="GameMemory"/>, 17 registres GTE, et un
/// compteur d'ecran.
/// </para>
/// <para>
/// <b>Les 17 registres GTE restent dans <c>Platform/Winsys3d.cs</c>, et ce n'est pas un
/// compromis.</b> Ce ne sont pas des globales de <c>Blaze.exe</c> : ce sont des pointeurs dans le
/// segment de donnees de <c>winsys3d.dll</c>, resolus par <c>GetProcAddress</c> sur des ordinaux
/// sans nom (voir <c>EnsureGteRegisters</c>). Aucune ancre <c>DAT_</c> ne les designe, ils
/// n'appartiennent donc pas a <see cref="GameMemory"/> ; et trois autres fichiers les lisent, ce
/// qui en fait de l'interop partagee, pas l'etat prive de ce rendu.
/// </para>
/// <para>
/// Ce que la signature ne dit pas encore : <c>s_Unknown_005e9818</c> (<c>DAT_005e9818</c>, le
/// budget de primitives par image) reste chez <c>Screens/State00/MainMenu.cs</c>, expose
/// <c>internal</c>. C'est du <c>.data</c> et il devrait rejoindre <see cref="GameMemory"/> ; ce
/// rapatriement est de l'etape A et se fera separement, comme les cinq de <see cref="Resources"/>.
/// </para>
/// <para>
/// Instanciee UNE FOIS, par <c>Subsystems.cs</c>.
/// </para>
/// </summary>
internal sealed unsafe class Model3d(GameMemory mem)
{
    // =====================================================================
    // FUN_0051d1f0 — le rendu de modèle animé
    //
    // La plus grosse fonction reconstruite à ce jour. C'est elle qui dessine un personnage : elle
    // compose la pose de caméra avec la pose de l'entité, déroule le squelette de l'animation en
    // matrices d'os, transforme et éclaire les sommets par lot côté DLL, puis émet les primitives
    // dans la liste d'affichage.
    //
    // Deux régimes complets, commandés par le bit 0x400 du champ +0x3c du modèle :
    //   dégagé — transformation perspective (ordinal 418, qui produit une profondeur par sommet),
    //            puis **tri en profondeur par polygone** : l'emplacement d'OT est le maximum des
    //            quatre profondeurs, borné par `slot - 0x50 < 0xec0`.
    //   posé   — transformation sans profondeur (ordinal 795) et **un emplacement fixe** pour tout
    //            le modèle. C'est le chemin des géométries plates ou en panneau.
    //
    // C'est ce drapeau qui donne à l'original son air d'avoir quatre boucles d'émission presque
    // identiques : il en a deux, écrites deux fois.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0051d1f0 @ 0x0051d1f0.
    ///
    /// <para>Les sept paramètres, d'après les deux sites d'appel (<c>FUN_00506880</c> et
    /// <c>FUN_00507010</c>) : la base de l'OT, le curseur de primitives, l'entrée d'animation,
    /// l'entrée de la table de modèles, le bloc de transformation de l'entité, l'indice de frame
    /// et le mot d'attributs supplémentaire. Rend le curseur avancé.</para>
    /// </summary>
    internal uint* DrawAnimatedModel(
        uint* otBase, uint* prim, byte* animation, int* modelBundle,
        int* transform, int frameIndex, uint extra)
    {
        EnsureGteRegisters();

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "DrawAnimatedModel: winsys3d.dll's MainMem export resolves to null, so the bone " +
                "matrix arena at MainMem + 0x1ba000 cannot be placed.");
        }

        short* matrix814 = (short*)s_GteMatrix814;
        short* vector802 = (short*)s_GteVector802;
        int* mac801 = (int*)s_GteMac801;
        short* ir813 = (short*)s_GteIr813;

        // Ghidra: la matrice de caméra entre dans le registre MATRIX du GTE — les neuf shorts de
        // rotation puis les trois translations, d'un bloc.
        for (int i = 0; i < 9; i++) matrix814[i] = ((short*)mem.s_CameraMatrix_006008e0)[i];
        *(int*)(s_GteMatrix814 + 0x14) = mem.CameraTranslation[0];
        *(int*)(s_GteMatrix814 + 0x18) = mem.CameraTranslation[1];
        *(int*)(s_GteMatrix814 + 0x1c) = mem.CameraTranslation[2];

        // Ghidra: la position de l'entité, décalée par DAT_006008d0..d8 et ramenée en unités de
        // 1/0x1000. Noter que Y est **soustrait** — l'axe est inversé par rapport aux deux autres.
        vector802[0] = (short)((transform[0] + mem.s_Unknown_006008d0) >> 0xc);
        vector802[1] = (short)((mem.s_Unknown_006008d4 - transform[1]) >> 0xc);
        vector802[2] = (short)((transform[2] + mem.s_Unknown_006008d8) >> 0xc);

        // Ghidra: le produit matrice-vecteur écrit à la main, accumulateurs 32 bits (MAC) puis
        // versions saturées 16 bits (IR) — exactement ce que ferait RotTrans sur la console.
        mac801[0] = (vector802[1] * matrix814[1] + vector802[2] * matrix814[2]
                     + matrix814[0] * vector802[0] >> 0xc) + *(int*)(s_GteMatrix814 + 0x14);
        ir813[0] = (short)mac801[0];
        mac801[1] = (vector802[1] * matrix814[4] + vector802[2] * matrix814[5]
                     + matrix814[3] * vector802[0] >> 0xc) + *(int*)(s_GteMatrix814 + 0x18);
        ir813[1] = (short)mac801[1];
        mac801[2] = (vector802[1] * matrix814[7] + vector802[2] * matrix814[8]
                     + matrix814[6] * vector802[0] >> 0xc) + *(int*)(s_GteMatrix814 + 0x1c);
        ir813[2] = (short)mac801[2];

        // Ghidra: local_3c / sStack_3a / sStack_38 — l'orientation de l'entité, décalée par la pose
        // de caméra DAT_00604928. sStack_38 sert aussi d'emplacement d'OT dans le régime « fixe ».
        short* entityAngles = stackalloc short[3];
        entityAngles[0] = (short)((short)transform[5] + *(short*)(mem.s_CameraVector_00604928 + 0));
        entityAngles[1] = (short)(*(short*)((byte*)transform + 0x16) + *(short*)(mem.s_CameraVector_00604928 + 2));
        entityAngles[2] = (short)((short)transform[6] + *(short*)(mem.s_CameraVector_00604928 + 4));

        // Ghidra: local_bc / local_b8 / local_b4 — la position transformée, mise de côté.
        int translatedX = mac801[0], translatedY = mac801[1], translatedZ = mac801[2];

        // Ghidra: local_d0 — la matrice de l'entité : rotation depuis ses angles, puis mise à
        // l'échelle par transform[3..5] (les trois ushorts à +0xc).
        byte* entityMatrix = stackalloc byte[0x20];
        NativeMemory.Clear(entityMatrix, 0x20);
        RotMatrix(entityAngles, (short*)entityMatrix);
        ScaleMatrixSaturating((short*)entityMatrix, (ushort*)(transform + 3));

        *(int*)(entityMatrix + 0x14) = translatedX;
        *(int*)(entityMatrix + 0x18) = translatedY;
        *(int*)(entityMatrix + 0x1c) = translatedZ;

        // Ghidra: local_68.. — la rotation de scène, celle que le pilotage libre du sous-état 0
        // fait tourner.
        short* sceneAngles = stackalloc short[3];
        sceneAngles[0] = (short)mem.s_ScenePitch_005e9308;
        sceneAngles[1] = (short)mem.s_SceneYaw_005e930a;
        sceneAngles[2] = (short)mem.s_SceneRoll_005e930c;
        short* sceneMatrix = stackalloc short[9];
        RotMatrix(sceneAngles, sceneMatrix);

        // === Les constantes de page de texture, calculées une fois ===
        //
        // Quirk de l'original, transcrit tel quel : elles sont dérivées du **premier** modèle du
        // paquet, alors que la boucle de parties plus bas relit les attributs de chaque partie.
        // Les parties suivantes réutilisent donc la page de la première.
        byte* firstModel = (byte*)(nint)modelBundle[0];
        uint modelAttributes = *(uint*)(firstModel + 0x3c);

        if (mem.s_DeviceCapFlag_00564a48 && (modelAttributes & 0x200) != 0)
        {
            if (mem.s_Unknown_00564a4c == 0) return prim;
            modelAttributes &= 0xfffffdff;
        }

        ushort* skipCounters = (ushort*)(firstModel + 0x10);
        int skipped = skipCounters[0] + skipCounters[1] + skipCounters[2] + skipCounters[3];
        byte* firstRecords = *(byte**)(firstModel + 0x28);

        uint clut = *(uint*)(mem.s_EntityDecodeTable_005e8f00 + ((modelAttributes >> 0x10) & 0xff) * 4);

        uint tpageWord = ((*(ushort*)(firstRecords + skipped * 0x10 + 8) & 0xfffff800u)
                          | ((((modelAttributes & 0xff) | 0x1c0) << 5))
                          | clut) << 0x10;

        uint uv = (clut >> 0x10)
                + (uint)(((short)extra >> 2) & unchecked((int)0xffffffc0))
                + (extra & 0xff);
        if (uv > 0x7ec0) uv = 0x7ec0;
        uint uvWord = uv << 0x10;

        // === La boucle d'os : le squelette de la frame devient des matrices monde ===
        //
        // Chaque os prend sa pose dans la frame courante, la tourne, la met à l'échelle (uniforme)
        // et la compose avec celle de son parent. Le résultat s'empile dans MainMem + 0x1ba000,
        // un slot de 0x20 octets par os — que l'original adresse par `(index + 0xdd00) * 0x20`,
        // ce qui est la même chose.
        int boneCount = *(ushort*)(animation + 0x12);
        byte* poseBase = *(byte**)(animation + 0x20);
        ushort* boneIndices = *(ushort**)(animation + 0x24);
        ushort* poseIndices = (ushort*)(*(byte**)(animation + 0x28) + frameIndex * boneCount * 2);

        byte* boneSlot = mainMem + 0x1ba000;
        byte* parentMatrix = entityMatrix;
        byte* boneMatrix = stackalloc byte[0x20];
        ushort* uniformScale = stackalloc ushort[3];

        // local_50 dans l'original : là où le curseur s'arrête. Ce n'est pas une lecture hors
        // bornes — la table d'animation +0x24 est en deux parties, les parents des os suivis des
        // indices d'os par partie, et la boucle laisse le curseur pile sur la seconde.
        ushort* partBones = boneIndices;

        if (boneCount != 0)
        {
            for (int remaining = boneCount; remaining != 0; remaining--)
            {
                byte* pose = poseBase + *poseIndices * 0x10;

                NativeMemory.Clear(boneMatrix, 0x20);
                RotMatrix((short*)pose, (short*)boneMatrix);

                // Ghidra: local_48 / local_46 / la moitié basse de local_44 reçoivent toutes le
                // même short — l'échelle est uniforme, pas un vecteur.
                ushort scale = *(ushort*)(pose + 0xe);
                uniformScale[0] = scale;
                uniformScale[1] = scale;
                uniformScale[2] = scale;
                Winsys3d.Ordinal_794(boneMatrix, uniformScale);

                *(int*)(boneMatrix + 0x14) = *(short*)(pose + 8);
                *(int*)(boneMatrix + 0x18) = *(short*)(pose + 10);
                *(int*)(boneMatrix + 0x1c) = *(short*)(pose + 0xc);

                Winsys3d.Ordinal_360(parentMatrix, boneMatrix, boneSlot);

                boneIndices++;
                poseIndices++;
                boneSlot += 0x20;
                parentMatrix = mainMem + (*boneIndices + 0xdd00) * 0x20;
            }

            partBones = boneIndices;
        }

        // === La boucle de parties ===
        int transformFlags = *(short*)((byte*)transform + 0x1a);
        int partCount = *(ushort*)(animation + 0x14);
        if (partCount == 0) return prim;

        bool cullParts = (transformFlags & 1) != 0;
        bool skipLighting = (transformFlags & 4) != 0;

        short* composed = stackalloc short[9];
        short otSlotFixed = entityAngles[2];   // sStack_38, réécrit par le cull quand il tourne

        for (; partCount != 0; partCount--, modelBundle++, partBones++)
        {
            short* partMatrix = (short*)(mainMem + (*partBones + 0xdd00) * 0x20);

            // Ghidra: deux chemins sur MMXInstalled_exref — neuf produits scalaires, ou trois
            // triplets de `pmaddwd` sur les mêmes opérandes.
            //
            // DÉVIATION ASSUMÉE : seul le chemin scalaire est transcrit. Le branchement de
            // l'original est une optimisation sur capacité CPU, donc les deux chemins doivent
            // calculer la même chose — mais cette équivalence n'a **pas** été vérifiée opérande
            // par opérande. TODO(REVERSE): la vérifier, ou transcrire le chemin MMX.
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    composed[row * 3 + col] =
                        (short)((sceneMatrix[row * 3 + 0] * partMatrix[0 * 3 + col]
                               + sceneMatrix[row * 3 + 1] * partMatrix[1 * 3 + col]
                               + sceneMatrix[row * 3 + 2] * partMatrix[2 * 3 + col]) >> 0xc);
                }
            }

            // Ghidra: la matrice composée part dans le registre de normales (815), la matrice de
            // l'os — translation comprise — dans le registre de transformation (814).
            //
            // L'original copie 0x20 octets vers 815 depuis local_88, dont la rangée de translation
            // n'est **pas** réécrite par le produit ci-dessus : elle garde ce que la boucle d'os y
            // a laissé au dernier tour. Reproduit tel quel, c'est sans effet — l'éclairage n'a
            // besoin que de la rotation.
            for (int i = 0; i < 9; i++) ((short*)s_GteLightMatrix815)[i] = composed[i];
            *(int*)(s_GteLightMatrix815 + 0x14) = *(int*)(boneMatrix + 0x14);
            *(int*)(s_GteLightMatrix815 + 0x18) = *(int*)(boneMatrix + 0x18);
            *(int*)(s_GteLightMatrix815 + 0x1c) = *(int*)(boneMatrix + 0x1c);

            for (int i = 0; i < 8; i++) ((uint*)s_GteMatrix814)[i] = ((uint*)partMatrix)[i];

            if (cullParts && !PartIsOnScreen(ref otSlotFixed)) continue;

            prim = DrawModelPart(otBase, prim, (byte*)(nint)modelBundle[0], mainMem,
                                 transform, tpageWord, uvWord, skipLighting, otSlotFixed);
        }

        return prim;
    }

    /// <summary>
    /// Ghidra: la branche <c>local_94 != 0</c> de FUN_0051d1f0 — le rejet des parties hors écran.
    ///
    /// <para>Elle projette l'origine de la partie, c'est-à-dire le vecteur nul : tous les produits
    /// par la rotation s'annulent et il ne reste que la translation de la matrice courante, lue en
    /// <c>short</c>. La division perspective passe par la table de réciproques de l'ordinal 821,
    /// indexée par la profondeur, puis le décalage écran des ordinaux 819/820 est ajouté.</para>
    ///
    /// <para>Effet de bord voulu : la profondeur projetée, divisée par quatre, devient
    /// l'emplacement d'OT du régime « fixe ». D'où le <c>ref</c>.</para>
    /// </summary>
    private bool PartIsOnScreen(ref short otSlot)
    {
        ProjectOriginThroughMatrix();

        short x = (short)*(uint*)s_GteSxy809;
        short y = (short)(*(uint*)s_GteSxy809 >> 0x10);
        short z = (short)*(int*)s_GteSz810;

        otSlot = z;

        return (ushort)(x + 0x18) < 0x170
            && (ushort)(y + 0x18) < 0x120
            && (uint)(z - 0x58) < 0x3a0;
    }

    /// <summary>
    /// Ghidra: le cœur de projection partagé par la branche de cull de FUN_0051d1f0 et par
    /// <see cref="ProjectPoint"/> — les deux l'écrivent en toutes lettres, à l'identique.
    ///
    /// <para>Le vecteur d'entrée est mis à zéro, donc tous les produits par la rotation
    /// s'annulent : seule la translation de la matrice courante survit. Ce que l'original écrit
    /// quand même en entier, et qu'on garde ici sous la forme courte — la version longue ne ferait
    /// qu'ajouter des zéros. La division perspective passe par la table de réciproques de
    /// l'ordinal 821, indexée par la profondeur, puis le décalage écran des ordinaux 819/820.</para>
    /// </summary>
    /// <summary>
    /// Charge la matrice de caméra dans le GTE, y transforme une position monde, et **fait de la
    /// position transformée la translation de la matrice** — après quoi projeter l'origine projette
    /// ce point-là.
    ///
    /// <para>Bloc identique, instruction pour instruction, dans <see cref="ProjectPoint"/>
    /// (<c>FUN_0051f650</c>) et dans l'émetteur de particules (<c>FUN_00520ae0</c>) ; la branche de
    /// cull de <c>FUN_0051d1f0</c> fait le même tour. Extrait à ce titre : unité indépendante
    /// répétée telle quelle, pas un découpage d'un corps unique.</para>
    /// </summary>
    internal void LoadPointAsMatrixTranslation(int* position)
    {
        EnsureGteRegisters();

        Winsys3d.Ordinal_366(mem.s_CameraMatrix_006008e0);
        Winsys3d.Ordinal_369(mem.s_CameraMatrix_006008e0);

        short* input = (short*)s_GteVector803;
        input[0] = (short)((position[0] + mem.s_Unknown_006008d0) >> 0xc);
        input[1] = (short)((mem.s_Unknown_006008d4 - position[1]) >> 0xc);
        input[2] = (short)((position[2] + mem.s_Unknown_006008d8) >> 0xc);

        short* m = (short*)s_GteMatrix814;
        int* mac = (int*)s_GteMac801;
        short* ir = (short*)s_GteIr813;

        mac[0] = (input[1] * m[1] + input[2] * m[2] + m[0] * input[0] >> 0xc)
               + *(int*)(s_GteMatrix814 + 0x14);
        mac[1] = (input[1] * m[4] + input[2] * m[5] + m[3] * input[0] >> 0xc)
               + *(int*)(s_GteMatrix814 + 0x18);
        mac[2] = (input[1] * m[7] + input[2] * m[8] + m[6] * input[0] >> 0xc)
               + *(int*)(s_GteMatrix814 + 0x1c);

        ir[0] = (short)mac[0];
        ir[1] = (short)mac[1];
        ir[2] = (short)mac[2];

        *(int*)(s_GteMatrix814 + 0x14) = mac[0];
        *(int*)(s_GteMatrix814 + 0x18) = mac[1];
        *(int*)(s_GteMatrix814 + 0x1c) = mac[2];
    }

    internal void ProjectOriginThroughMatrix()
    {
        short* vector = (short*)s_GteVector802;
        vector[0] = 0;
        vector[1] = 0;
        vector[2] = 0;

        short depth = *(short*)(s_GteMatrix814 + 0x1c);
        *(int*)s_GteSz811 = depth;

        int reciprocal = *(short*)(s_GteRecip821 + (ushort)depth * 2);
        short screenY = *(short*)(s_GteMatrix814 + 0x18);
        short screenX = *(short*)(s_GteMatrix814 + 0x14);

        *(uint*)s_GteSxy809 = (uint)(
              ((reciprocal * screenY & unchecked((int)0xfffff000)) + *(int*)s_GteOfx819 * 0x1000) * 0x10
            + ((reciprocal * screenX) >> 0xc) + *(int*)s_GteOfy820);

        *(int*)s_GteSz810 = *(int*)s_GteSz811 >> 2;
    }

    /// <summary>
    /// Ghidra: FUN_0051f650 @ 0x0051f650 — projette une position monde et dit si elle est à
    /// l'écran. C'est le filtre que le carrousel du sous-état 0 applique à chacun de ses seize
    /// emplacements : un retour nul écarte l'emplacement.
    ///
    /// <para>Elle charge la matrice de caméra dans le registre du GTE par
    /// <c>SetRotMatrix</c>/<c>SetTransMatrix</c> (ordinaux 366 et 369), transforme la position, en
    /// fait la nouvelle translation de la matrice, puis projette l'origine — le même tour que la
    /// branche de cull de FUN_0051d1f0.</para>
    ///
    /// <para>Les deux paramètres viennent de l'assembleur, Ghidra ayant perdu le cadre de pile et
    /// rendu la sortie en <c>unaff_retaddr</c> : arg1 en <c>[ESP+0x1c]</c> (la position), arg2 en
    /// <c>[ESP+0x20]</c> (le tampon de sortie, écrit en <c>0x0051f8e5</c>).</para>
    /// </summary>
    internal int ProjectPoint(int* position, short* outScreen)
    {
        LoadPointAsMatrixTranslation(position);
        ProjectOriginThroughMatrix();

        *(uint*)outScreen = *(uint*)s_GteSxy809;
        *(uint*)(outScreen + 2) = (uint)*(int*)s_GteSz810;

        return (ushort)(outScreen[0] + 0x18) < 0x170
            && (ushort)(outScreen[1] + 0x18) < 0x120
            && (uint)(outScreen[2] - 0x58) < 0x3a0 ? 1 : 0;
    }

    /// <summary>
    /// Ghidra: LAB_0051db61 — le corps d'une partie : transformation et éclairage des sommets côté
    /// DLL, puis émission des primitives.
    ///
    /// <para>Le bit <c>0x400</c> des attributs commande les deux régimes. Dégagé : transformation
    /// perspective par l'ordinal 418, qui produit une profondeur par sommet, et **tri en profondeur
    /// par polygone**. Posé : ordinal 795, sans profondeur, et **un emplacement d'OT fixe** pour
    /// toute la partie.</para>
    /// </summary>
    private uint* DrawModelPart(
        uint* otBase, uint* prim, byte* model, byte* mainMem, int* transform,
        uint tpageWord, uint uvWord, bool skipLighting, short fixedOtSlot)
    {
        uint attributes = *(uint*)(model + 0x3c);

        if (mem.s_DeviceCapFlag_00564a48 && (attributes & 0x2000000) != 0)
        {
            if (mem.s_Unknown_00564a4c == 0) return prim;
            attributes &= 0xfdffffff;
        }

        uint vertexCount = *(uint*)(model + 8) >> 0x10;
        bool depthSorted = (attributes & 0x400) == 0;

        // Ghidra: la transformation par lot. Le chemin avec profondeur remplit aussi le tableau
        // à MainMem + 0x1b4000, que le tri par polygone relit.
        if (depthSorted)
        {
            Winsys3d.Ordinal_418(model + 0x40, mainMem + 0x1b0000, mainMem + 0x1b4000, mainMem,
                        *(uint*)(model + 8) & 0xffff);
        }
        else
        {
            // Même compteur que la branche avec profondeur : les deux ordinaux consomment le
            // champ +8 du modèle sur ses seize bits bas.
            Winsys3d.Ordinal_795(model + 0x40, mainMem + 0x1b0000, *(uint*)(model + 8) & 0xffff);
        }

        // Ghidra: l'éclairage — soit la passe de la DLL, soit une couleur plate recopiée trois fois
        // par triangle quand le bit 4 du bloc de transformation est posé.
        uint* colours = (uint*)(mainMem + 0x1b8000);
        uint flatColour = (uint)transform[7] & 0x3ffffff ^ (attributes & 0x300) << 0x10;

        if (skipLighting)
        {
            // Curseur distinct : `colours` sert encore à l'émission plus bas. L'original a le
            // même découplage (puVar15 est réassigné, pas la base).
            uint* fill = colours;
            for (uint i = vertexCount * 3; i != 0; i--) *fill++ = flatColour;
        }
        else
        {
            *(uint*)s_GteLightColour817 = flatColour;
            Winsys3d.Ordinal_793(*(void**)(model + 0x24), colours, vertexCount);
        }

        byte* records = *(byte**)(model + 0x28);
        ushort* counters = (ushort*)(model + 0x10);
        int skipped = counters[0] + counters[1] + counters[2] + counters[3];
        uint* record = (uint*)(records + skipped * 0x10);

        uint* screen = (uint*)(mainMem + 0x1b0000);
        int* depths = (int*)(mainMem + 0x1b4000);

        // Ghidra: les deux seaux, comptés en +0x1a puis +0x1e. Le premier émet des quads texturés
        // de 10 mots, le second des quads texturés dégradés de 13.
        prim = EmitModelQuads(otBase, prim, ref record, *(ushort*)(model + 0x1a),
                              screen, depths, colours, tpageWord, uvWord,
                              depthSorted, fixedOtSlot, gouraud: false);

        prim = EmitModelQuads(otBase, prim, ref record, *(ushort*)(model + 0x1e),
                              screen, depths, colours, tpageWord, uvWord,
                              depthSorted, fixedOtSlot, gouraud: true);

        return prim;
    }

    /// <summary>
    /// Ghidra: les quatre boucles d'émission de FUN_0051d1f0, qui n'en sont que deux écrites deux
    /// fois — le régime de tri ne change que le calcul de l'emplacement d'OT.
    ///
    /// <para>Chaque enregistrement de 16 octets porte quatre indices de sommet empaquetés dans son
    /// premier mot. Deux garde-fous bornent le travail : un quad n'est considéré que si l'un de ses
    /// quatre X écran tombe sous <c>0x140</c>, et le budget global <c>DAT_005e9818</c> décroît à
    /// chaque primitive émise et arrête la boucle à zéro.</para>
    ///
    /// <para>Le rejet des faces arrière est **inliné** dans l'original — la même expression à six
    /// termes que <see cref="NormalClip"/>, recopiée à la main. Le premier test porte sur les trois
    /// premiers sommets ; s'il passe, le quatrième remplace SXY2 et le test est refait, cette fois
    /// par un vrai appel.</para>
    /// </summary>
    private uint* EmitModelQuads(
        uint* otBase, uint* prim, ref uint* record, int count,
        uint* screen, int* depths, uint* colours,
        uint tpageWord, uint uvWord, bool depthSorted, short fixedOtSlot, bool gouraud)
    {
        int stride = gouraud ? 0xd : 10;

        for (; count != 0 && s_Unknown_005e9818 != 0; count--, record += 4)
        {
            uint packed = record[0];
            int i0 = (int)(packed & 0xff);
            int i1 = (int)(packed >> 8 & 0xff);
            int i2 = (int)(packed >> 0x10 & 0xff);
            int i3 = (int)(packed >> 0x18);

            uint v0 = screen[i0], v1 = screen[i1], v2 = screen[i2], v3 = screen[i3];

            *(uint*)s_GteSxy805 = v0;
            *(uint*)s_GteSxy806 = v1;
            *(uint*)s_GteSxy807 = v2;

            if ((ushort)v0 >= 0x140 && (ushort)v1 >= 0x140
                && (ushort)v2 >= 0x140 && (ushort)v3 >= 0x140) continue;

            // Ghidra: le produit vectoriel écrit à la main, identique à NormalClip.
            int x0 = *(short*)s_GteSxy805, y0 = *(short*)(s_GteSxy805 + 2);
            int x1 = *(short*)s_GteSxy806, y1 = *(short*)(s_GteSxy806 + 2);
            int x2 = *(short*)s_GteSxy807, y2 = *(short*)(s_GteSxy807 + 2);
            *(int*)s_GteMac0808 = x0 * y1 + x1 * y2 + x2 * y0
                                - x0 * y2 - x1 * y0 - x2 * y1;

            if ((v0 & 0xffff0000) >= 0xf00000 && (v1 & 0xffff0000) >= 0xf00000
                && (v2 & 0xffff0000) >= 0xf00000 && (v3 & 0xffff0000) >= 0xf00000) continue;

            if (ReadMac0() >= 0)
            {
                *(uint*)s_GteSxy807 = v3;
                if (NormalClip() >= 0) continue;
            }

            uint* slot;
            if (depthSorted)
            {
                int zmax = depths[i0];
                if (zmax < depths[i1]) zmax = depths[i1];
                if (zmax < depths[i2]) zmax = depths[i2];
                if (zmax < depths[i3]) zmax = depths[i3];

                // Ghidra rend ce test en arithmétique de pointeur (`puVar20 + -0x50` sur un
                // `uint *`). L'assembleur tranche : `LEA ECX,[EAX + 0xfffffec0]` puis
                // `CMP ECX,0xec0` non signé — c'est bien 0x140 octets, pas 0x50.
                if ((uint)(zmax - 0x140) >= 0xec0) continue;

                slot = (uint*)((byte*)otBase + ((uint)zmax & 0xfffc));
            }
            else
            {
                slot = otBase + fixedOtSlot;
            }

            uint previous = *slot;
            *slot = (uint)(nuint)prim;

            if (!gouraud)
            {
                uint colour = colours[record[1] & 0xff];
                if (mem.s_DeviceCapFlag_00564a48) colour &= 0xfdffffff;

                prim[0] = previous | 0x9000000;
                prim[1] = colour | 0x2c000000;
                prim[2] = v1;
                prim[3] = record[2] >> 0x10 | uvWord;
                prim[4] = v0;
                prim[5] = tpageWord | record[2] & 0xffff;
                prim[6] = v2;
                prim[7] = record[3] & 0xffff;
                prim[8] = v3;
                prim[9] = record[3] >> 0x10;
            }
            else
            {
                uint indices = record[1];
                uint c0 = colours[indices & 0xff];
                uint c1 = colours[indices >> 8 & 0xff];
                uint c2 = colours[indices >> 0x10 & 0xff];
                uint c3 = colours[indices >> 0x18];
                if (mem.s_DeviceCapFlag_00564a48) c1 &= 0xfdffffff;

                prim[0] = previous | 0xc000000;
                prim[1] = c1 | 0x3c000000;
                prim[2] = v1;
                prim[3] = record[2] >> 0x10 | uvWord;
                prim[4] = c0;
                prim[5] = v0;
                prim[6] = tpageWord | record[2] & 0xffff;
                prim[7] = c2;
                prim[8] = v2;
                prim[9] = record[3] & 0xffff;
                prim[10] = c3;
                prim[11] = v3;
                prim[12] = record[3] >> 0x10;
            }

            prim += stride;
            s_Unknown_005e9818--;
        }

        return prim;
    }

    // =====================================================================
    // FUN_0051c420 — le rendu de modèle **statique**
    //
    // La sœur de FUN_0051d1f0 juste au-dessus, et elle en éclaire la lecture : même prologue, même
    // banc GTE, mêmes primitives, mais pas de squelette. Un modèle, une pose, quatre seaux de
    // faces. C'est elle qui pose le décor de la carte du monde et la rose des vents.
    //
    // Trois écarts avec la sœur, tous voulus :
    //   * l'emplacement d'OT est **un pointeur unique** passé par l'appelant, avancé une seule fois
    //     par la profondeur du modèle entier — pas un tri par polygone ;
    //   * elle émet **quatre** formes (triangle plat, quad plat, triangle dégradé, quad dégradé)
    //     là où la sœur n'en émet que deux ;
    //   * le mot de page de texture est plus simple : ni le terme `0x1c0`, ni la relecture du
    //     premier enregistrement, ni la borne `0x7ec0` sur l'UV.
    //
    // **Ghidra n'a récupéré que deux des cinq paramètres, et il a coupé chacun des trois autres en
    // deux noms distincts.** Relevé au désassembleur le 2026-08-22 ; le détail est sur la méthode.
    // =====================================================================

    /// <summary>
    /// Ghidra: FUN_0051c420 @ 0x0051c420 — émet un modèle statique dans la liste d'affichage et
    /// rend le curseur avancé.
    ///
    /// <para><b>La signature ne vient pas de la pseudo-C.</b> Ghidra déclare
    /// <c>FUN_0051c420(int param_1, int *param_2)</c> ; les cinq sites d'appel poussent cinq
    /// arguments (<c>add esp,14h</c>). Le prologue tranche : <c>sub esp,50h</c> puis quatre
    /// <c>push</c>, donc les arguments vivent à <c>[esp+64h..74h]</c>. La correspondance, et le
    /// dédoublement qui va avec :</para>
    /// <code>
    /// [esp+64h]  otEntry     Ghidra : puStack_10  ET  iStack_4   (pointeur / entier)
    /// [esp+68h]  prim        Ghidra : unaff_retaddr  ET  puStack_c
    /// [esp+6Ch]  model       Ghidra : param_1  ET  uStack_8
    /// [esp+70h]  placement   Ghidra : param_2
    /// [esp+74h]  flags       Ghidra : perdu — aucun nom
    /// </code>
    ///
    /// <para><b>Le dédoublement est le vrai piège</b>, plus que la perte. Trois paramètres portent
    /// chacun deux noms Ghidra, et rien dans la pseudo-C ne dit qu'il s'agit du même emplacement :
    /// on lit <c>uStack_8 + 0x18</c> à côté de <c>param_1 + 0x3c</c> et on croit à deux structures.
    /// Pire, les emplacements d'arguments servent de brouillon une fois leur valeur consommée, si
    /// bien que le même nom <c>iStack_4</c> désigne <b>l'entrée d'OT</b> au début et un
    /// <b>temporaire du produit vectoriel</b> dans les boucles. Les traduire par une seule variable
    /// détruirait le pointeur d'OT à la première face ; ils sont donc séparés ici.</para>
    ///
    /// <para>Le bloc de placement, tel que les appelants le remplissent :</para>
    /// <code>
    /// +0x00..0x08  position monde X/Y/Z        +0x0c..0x10  echelle X/Y/Z (ushort)
    /// +0x14..0x18  angles X/Y/Z                +0x1a        drapeaux
    /// +0x1c        couleur plate (26 bits)
    /// </code>
    /// <para>Drapeaux : bit 0 rejette hors du cadre écran, bits 0-1 activent le tri en profondeur,
    /// bit 2 remplace l'éclairage par la couleur plate.</para>
    /// </summary>
    internal uint* DrawStaticModel(uint* otEntry, uint* prim, byte* model, int* placement, uint flags)
    {
        EnsureGteRegisters();

        byte* mainMem = GetMainMemPtr();
        if (mainMem == null)
        {
            throw new NotImplementedException(
                "DrawStaticModel: l'export MainMem de winsys3d.dll rend null, donc les tampons de " +
                "sommets a MainMem + 0x1b0000 et de couleurs a + 0x1b8000 ne peuvent pas etre places.");
        }

        // La position du modèle devient la translation de la matrice courante — bloc identique,
        // instruction pour instruction, à celui de FUN_0051f650 et de l'émetteur de particules.
        LoadPointAsMatrixTranslation(placement);

        ushort placementFlags = *(ushort*)((byte*)placement + 0x1a);

        short* vector802 = (short*)s_GteVector802;
        vector802[0] = 0;
        vector802[1] = 0;
        vector802[2] = 0;

        // Le rejet, et le **tri en profondeur du modèle entier**. Ghidra rend l'avance d'OT par
        // `iStack_4 = iStack_4 + sStack_2c * 4`, sur une variable qu'il n'initialise nulle part ;
        // le désassembleur donne `mov edx,[esp+64h] / lea eax,[edx+eax*4] / mov [esp+64h],eax`,
        // soit l'entrée d'OT avancée de la profondeur — en dwords, d'où le `* 4` en octets.
        if ((placementFlags & 3) != 0)
        {
            ProjectOriginThroughMatrix();

            short screenX = (short)*(uint*)s_GteSxy809;
            short screenY = (short)(*(uint*)s_GteSxy809 >> 0x10);
            short depth = (short)*(int*)s_GteSz810;

            if ((placementFlags & 1) != 0)
            {
                if ((ushort)(screenX + 0x18) >= 0x170) return prim;
                if ((ushort)(screenY + 0x18) >= 0x120) return prim;
            }
            if ((uint)(depth - 0x58) >= 0x3a0) return prim;

            otEntry += depth;
        }

        // === La matrice du modèle : rotation depuis ses angles décalés par la pose de caméra,
        // puis mise à l'échelle par les trois ushorts de +0x0c ===
        short* cameraAngles = (short*)mem.s_CameraVector_00604928;
        short* angles = stackalloc short[3];
        angles[0] = (short)(*(short*)((byte*)placement + 0x14) + cameraAngles[0]);
        angles[1] = (short)(*(short*)((byte*)placement + 0x16) + cameraAngles[1]);
        angles[2] = (short)(*(short*)((byte*)placement + 0x18) + cameraAngles[2]);

        byte* matrix = stackalloc byte[0x20];
        NativeMemory.Clear(matrix, 0x20);
        RotMatrix(angles, (short*)matrix);
        ScaleMatrixSaturating((short*)matrix, (ushort*)(placement + 3));

        for (int i = 0; i < 9; i++) ((short*)s_GteMatrix814)[i] = ((short*)matrix)[i];

        // === La matrice d'éclairage : la rotation de scène, composée avec le lacet et le roulis
        // du modèle mais **pas avec son tangage** — l'original écrase l'angle X par
        // DAT_005e9308 au lieu de l'additionner. Transcrit tel quel. ===
        angles[1] += (short)mem.s_SceneYaw_005e930a;
        angles[2] += (short)mem.s_SceneRoll_005e930c;
        angles[0] = (short)mem.s_ScenePitch_005e9308;

        NativeMemory.Clear(matrix, 0x20);
        RotMatrix(angles, (short*)matrix);

        // Huit dwords, soit 0x20 octets, alors que RotMatrix n'en écrit que neuf shorts : les
        // quatorze octets suivants partent aussi dans le registre 815, dont ils écrasent la
        // translation. Dans l'original ce sont des restes de pile ; ici le tampon est remis à zéro
        // avant chaque appel, ce qui est la traduction la plus proche possible.
        for (int i = 0; i < 8; i++) ((uint*)s_GteLightMatrix815)[i] = ((uint*)matrix)[i];

        uint attributes = *(uint*)(model + 0x3c);
        if (mem.s_DeviceCapFlag_00564a48 && (attributes & 0x200) != 0)
        {
            if (mem.s_Unknown_00564a4c == 0) return prim;
            attributes &= 0xfffffdff;
        }

        // Les deux mots constants des primitives. Ghidra ne les montre pas du tout — il les nomme
        // uStack_48 et uStack_44 et ne les affecte jamais. Relevés en 0x0051c82a..0x0051c871.
        uint clut = *(uint*)(mem.s_EntityDecodeTable_005e8f00 + ((attributes >> 0x10) & 0xff) * 4);
        uint tpageWord = (((attributes & 0xff) << 5) | clut) << 0x10;
        uint uvWord = ((uint)(((ushort)((short)flags >> 2)) & 0xffc0) + (flags & 0xff)) << 0x10;
        uvWord += clut & 0xffff0000;

        uint vertexCount = *(uint*)(model + 8) >> 0x10;
        Winsys3d.Ordinal_795(model + 0x40, mainMem + 0x1b0000, *(uint*)(model + 8) & 0xffff);

        uint* colours = (uint*)(mainMem + 0x1b8000);
        uint flatColour = (uint)placement[7] & 0x3ffffff ^ (attributes & 0x300) << 0x10;

        if ((placementFlags & 4) == 0)
        {
            *(uint*)s_GteLightColour817 = flatColour;
            Winsys3d.Ordinal_793(*(void**)(model + 0x24), colours, vertexCount);
        }
        else
        {
            uint* fill = colours;
            for (uint i = vertexCount * 3; i != 0; i--) *fill++ = flatColour;
        }

        ushort* counters = (ushort*)(model + 0x10);
        int skipped = counters[0] + counters[1] + counters[2] + counters[3];
        uint* record = (uint*)(*(byte**)(model + 0x28) + skipped * 0x10);
        uint* screen = (uint*)(mainMem + 0x1b0000);

        prim = EmitStaticTriangles(otEntry, prim, ref record, *(ushort*)(model + 0x18),
                                   screen, colours, tpageWord, uvWord, gouraud: false);
        prim = EmitStaticQuads(otEntry, prim, ref record, *(ushort*)(model + 0x1a),
                               screen, colours, tpageWord, uvWord, gouraud: false);
        prim = EmitStaticTriangles(otEntry, prim, ref record, *(ushort*)(model + 0x1c),
                                   screen, colours, tpageWord, uvWord, gouraud: true);
        prim = EmitStaticQuads(otEntry, prim, ref record, *(ushort*)(model + 0x1e),
                               screen, colours, tpageWord, uvWord, gouraud: true);

        return prim;
    }

    /// <summary>
    /// Ghidra: les seaux <c>+0x18</c> et <c>+0x1c</c> de FUN_0051c420 — les faces à trois sommets.
    /// Le premier émet un triangle texturé plat de 8 mots (marque <c>0x7000000</c>, code
    /// <c>0x24</c>), le second un triangle texturé dégradé de 10 mots (<c>0x9000000</c> /
    /// <c>0x34</c>).
    ///
    /// <para>Le rejet des faces arrière est **inliné** dans l'original, la même expression à six
    /// termes que <see cref="Winsys3d.NormalClip"/> recopiée à la main ; à trois sommets il n'y a
    /// qu'un seul test, contrairement aux quads.</para>
    /// </summary>
    private uint* EmitStaticTriangles(
        uint* otEntry, uint* prim, ref uint* record, int count,
        uint* screen, uint* colours, uint tpageWord, uint uvWord, bool gouraud)
    {
        for (; count != 0 && s_Unknown_005e9818 != 0; count--, record += 4)
        {
            uint packed = record[0];
            uint v0 = screen[packed & 0xff];
            uint v1 = screen[packed >> 8 & 0xff];
            uint v2 = screen[packed >> 0x10 & 0xff];

            *(uint*)s_GteSxy805 = v0;
            *(uint*)s_GteSxy806 = v1;
            *(uint*)s_GteSxy807 = v2;

            if ((ushort)v0 >= 0x140 && (ushort)v1 >= 0x140 && (ushort)v2 >= 0x140) continue;

            // Le produit vectoriel écrit à la main. `iStack_4` chez Ghidra — le même nom que
            // l'entrée d'OT du début de la fonction, et sans rapport avec elle.
            int x0 = *(short*)s_GteSxy805, y0 = *(short*)(s_GteSxy805 + 2);
            int x1 = *(short*)s_GteSxy806, y1 = *(short*)(s_GteSxy806 + 2);
            int x2 = *(short*)s_GteSxy807, y2 = *(short*)(s_GteSxy807 + 2);
            *(int*)s_GteMac0808 = x0 * y1 + x1 * y2 + x2 * y0
                                - x0 * y2 - x1 * y0 - x2 * y1;

            if ((v0 & 0xffff0000) >= 0xf00000 && (v1 & 0xffff0000) >= 0xf00000
                && (v2 & 0xffff0000) >= 0xf00000) continue;

            if (ReadMac0() >= 0) continue;

            uint previous = *otEntry;
            *otEntry = (uint)(nuint)prim;

            if (!gouraud)
            {
                prim[0] = previous | 0x7000000;
                prim[1] = colours[record[1] & 0xff] | 0x24000000;
                prim[2] = v0;
                prim[3] = uvWord | record[2] & 0xffff;
                prim[4] = v2;
                prim[5] = tpageWord | record[3] & 0xffff;
                prim[6] = v1;
                prim[7] = record[2] >> 0x10;
                prim += 8;
            }
            else
            {
                uint indices = record[1];
                uint c0 = colours[indices & 0xff];
                uint c1 = colours[indices >> 8 & 0xff];
                uint c2 = colours[indices >> 0x10 & 0xff];

                prim[0] = previous | 0x9000000;
                prim[1] = c0 | 0x34000000;
                prim[2] = v0;
                prim[3] = uvWord | record[2] & 0xffff;
                prim[4] = c2;
                prim[5] = v2;
                prim[6] = tpageWord | record[3] & 0xffff;
                prim[7] = c1;
                prim[8] = v1;
                prim[9] = record[2] >> 0x10;
                prim += 10;
            }

            s_Unknown_005e9818--;
        }

        return prim;
    }

    /// <summary>
    /// Ghidra: les seaux <c>+0x1a</c> et <c>+0x1e</c> de FUN_0051c420 — les faces à quatre
    /// sommets, quad texturé plat de 10 mots (<c>0x9000000</c> / <c>0x2c</c>) puis quad texturé
    /// dégradé de 13 (<c>0xc000000</c> / <c>0x3c</c>).
    ///
    /// <para>Même disposition de primitive que <see cref="EmitModelQuads"/> de la sœur animée, à
    /// deux détails près : l'emplacement d'OT est le pointeur unique passé par l'appelant, et
    /// <b>la couleur n'est pas masquée par <c>0xfdffffff</c></b> quand le drapeau de capacité est
    /// posé. Cet écart est dans l'original.</para>
    /// </summary>
    private uint* EmitStaticQuads(
        uint* otEntry, uint* prim, ref uint* record, int count,
        uint* screen, uint* colours, uint tpageWord, uint uvWord, bool gouraud)
    {
        for (; count != 0 && s_Unknown_005e9818 != 0; count--, record += 4)
        {
            uint packed = record[0];
            uint v0 = screen[packed & 0xff];
            uint v1 = screen[packed >> 8 & 0xff];
            uint v2 = screen[packed >> 0x10 & 0xff];
            uint v3 = screen[packed >> 0x18];

            *(uint*)s_GteSxy805 = v0;
            *(uint*)s_GteSxy806 = v1;
            *(uint*)s_GteSxy807 = v2;

            if ((ushort)v0 >= 0x140 && (ushort)v1 >= 0x140
                && (ushort)v2 >= 0x140 && (ushort)v3 >= 0x140) continue;

            int x0 = *(short*)s_GteSxy805, y0 = *(short*)(s_GteSxy805 + 2);
            int x1 = *(short*)s_GteSxy806, y1 = *(short*)(s_GteSxy806 + 2);
            int x2 = *(short*)s_GteSxy807, y2 = *(short*)(s_GteSxy807 + 2);
            *(int*)s_GteMac0808 = x0 * y1 + x1 * y2 + x2 * y0
                                - x0 * y2 - x1 * y0 - x2 * y1;

            if ((v0 & 0xffff0000) >= 0xf00000 && (v1 & 0xffff0000) >= 0xf00000
                && (v2 & 0xffff0000) >= 0xf00000 && (v3 & 0xffff0000) >= 0xf00000) continue;

            // Le quatrième sommet remplace SXY2 et le test est refait, cette fois par un vrai
            // appel — mêmes deux passes que chez la sœur animée.
            if (ReadMac0() >= 0)
            {
                *(uint*)s_GteSxy807 = v3;
                if (NormalClip() >= 0) continue;
            }

            uint previous = *otEntry;
            *otEntry = (uint)(nuint)prim;

            if (!gouraud)
            {
                prim[0] = previous | 0x9000000;
                prim[1] = colours[record[1] & 0xff] | 0x2c000000;
                prim[2] = v1;
                prim[3] = record[2] >> 0x10 | uvWord;
                prim[4] = v0;
                prim[5] = tpageWord | record[2] & 0xffff;
                prim[6] = v2;
                prim[7] = record[3] & 0xffff;
                prim[8] = v3;
                prim[9] = record[3] >> 0x10;
                prim += 10;
            }
            else
            {
                uint indices = record[1];
                uint c0 = colours[indices & 0xff];
                uint c1 = colours[indices >> 8 & 0xff];
                uint c2 = colours[indices >> 0x10 & 0xff];
                uint c3 = colours[indices >> 0x18];

                prim[0] = previous | 0xc000000;
                prim[1] = c1 | 0x3c000000;
                prim[2] = v1;
                prim[3] = record[2] >> 0x10 | uvWord;
                prim[4] = c0;
                prim[5] = v0;
                prim[6] = tpageWord | record[2] & 0xffff;
                prim[7] = c2;
                prim[8] = v2;
                prim[9] = record[3] & 0xffff;
                prim[10] = c3;
                prim[11] = v3;
                prim[12] = record[3] >> 0x10;
                prim += 13;
            }

            s_Unknown_005e9818--;
        }

        return prim;
    }
}
