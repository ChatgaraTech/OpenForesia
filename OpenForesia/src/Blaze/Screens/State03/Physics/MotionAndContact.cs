// OpenForesia - reconstruction of Blaze & Blade (Blaze.exe) from the Ghidra decompilation
// in Blaze.c (Legacy source of truth - never modify it, see Prompt.md / Strategy.md).
//
// Ce fichier est une part de la classe partielle Blaze. La carte des repertoires et la regle
// qui decide ou va une fonction sont dans Documentation/ARCHITECTURE.md. L etat de la
// reconstruction et les questions ouvertes sont dans Documentation/Etat/REVERSE_STATUS.md.
//
// LE MOUVEMENT ET LE CONTACT — ce que TOUTE entite subit, quel que soit son gestionnaire.
//
// `ApplyEntityMotion_00519DF0` avance, `ResolveEntityContact_00534330` heurte,
// `UpdateEntityShadow_00519EE0` pose l'ombre, `GroundSlopeAngle_005335E0` lit la pente,
// `PullEntityTowardCameraTarget_00519F50` rattrape la cible de camera.
//
// AUCUN DE CES CORPS NE SAIT A QUOI IL A AFFAIRE. Ils travaillent sur un enregistrement d'entite
// sans jamais consulter son sous-type ni son gestionnaire — c'est le test d'appartenance de ce
// repertoire.

using System;
using System.Runtime.InteropServices;

namespace OpenForesia.Blaze;

public static unsafe partial class Blaze
{

    /// <summary>Ghidra: FUN_00519df0 @ 0x00519df0 (51 l.) — applique la vitesse a la position et
    /// met a jour les drapeaux de collision de <c>+0x90</c>.</summary>
    /// <summary>
    /// Ghidra: FUN_00519df0 @ 0x00519df0 — <b>la collision contre le voisinage</b> : un balayage
    /// 3x3 des cellules de la <b>premiere</b> grille spatiale (celle de <c>0x1c4000</c>, que
    /// <see cref="TickEntityTable_005fa5e0"/> alimente) autour de l'entite, qui soumet chaque
    /// occupant a <c>FUN_00534330</c>.
    ///
    /// <para>
    /// La constante <c>0x3880</c> de la pseudo-C est la meme que celle de
    /// <see cref="LinkIntoWorldGrid"/> : <c>0x3880 * 0x20 * 4 = 0x1c4000</c>. Le pas de
    /// <c>0x80</c> entre deux cellules avance d'une rangee (0x20 cellules de quatre octets).
    /// </para>
    /// <para>
    /// <b>Ghidra reutilise <c>param_1</c> comme compteur de boucle</b> apres l'avoir sauve dans
    /// <c>puVar3</c> — d'ou un corps ou le meme nom designe tantot l'entite, tantot le nombre 3.
    /// Les deux ont ici des noms distincts.
    /// </para>
    /// <para>
    /// Le porteur de <c>+0x78</c> est <b>remis a zero avant le balayage</b> et relu apres : s'il a
    /// change, la position du nouveau porteur est recopiee dans l'entree d'emplacement. C'est ce
    /// qui permet a <see cref="ComputeCarrierOffset"/> de travailler au tour suivant.
    /// </para>
    /// </summary>
    private static void ApplyEntityMotion_00519DF0(uint* entity, byte* entry)
    {
        byte* e = (byte*)entity;
        byte* mainMem = GetMainMemPtr();

        uint previousCarrier = *(uint*)(e + 0x78);
        *(uint*)(e + 0x78) = 0;

        uint cellX = (uint)((((*(int*)(e + 0x1c) >> 0xc) + 0x2000) >> 9) - 1);
        *entity &= 0xffffffcf;
        uint cellZ = (uint)((((*(int*)(e + 0x24) >> 0xc) + 0x2000) >> 9) - 1);

        for (int column = 3; column != 0; column--)
        {
            int offset = (int)((cellZ + 0x3880) * 0x20 + cellX) * 4;
            uint row = cellZ;

            for (int step = 3; step != 0; step--)
            {
                if (cellX < 0x20 && row < 0x20)
                {
                    for (uint* occupant = *(uint**)(mainMem + offset);
                         occupant != null;
                         occupant = *(uint**)((byte*)occupant + 0x68))
                    {
                        if ((*occupant & 0x2000) == 0)
                        {
                            ResolveEntityContact_00534330(occupant, entity, entry);
                        }
                    }
                }

                offset += 0x80;
                row++;
            }

            cellZ = row - 3;
            cellX++;
        }

        uint carrier = *(uint*)(e + 0x78);
        if (carrier != 0 && carrier != previousCarrier)
        {
            byte* c = (byte*)(nuint)carrier;
            *(uint*)(entry + 0x28) = *(uint*)(c + 0x1c);
            *(uint*)(entry + 0x2c) = *(uint*)(c + 0x20);
            *(uint*)(entry + 0x30) = *(uint*)(c + 0x24);
        }
    }

    /// <summary>
    /// Ghidra: FUN_00534330 @ 0x00534330 — <b>le contact entre deux entites</b> : le personnage
    /// contre le maillage de collision d'un occupant voisin. Appelee par
    /// <see cref="ApplyEntityMotion_00519DF0"/> pour chaque occupant des neuf cellules de grille.
    /// C'est elle qui pose le porteur de <c>+0x78</c> et les bits de <c>+0x90</c>.
    ///
    /// <para>
    /// <b>2 464 octets</b> (0x00534330..0x00534cd0), 277 lignes de pseudo-C : une boucle sur les
    /// faces du maillage, avec par face un produit vectoriel dans les registres GTE emules puis
    /// jusqu'a trois <c>NormalClip</c> pour le test d'appartenance au triangle.
    /// </para>
    /// <para>
    /// <b>La pseudo-C ne peut pas etre transcrite telle quelle : elle confond les DEUX entites.</b>
    /// Ghidra declare <c>FUN_00534330(uint *param_1)</c> alors que le site d'appel en empile trois
    /// (<c>add esp,0Ch</c> en 0x00519e83), et il fait porter a un meme nom des acces qui visent des
    /// objets differents. La correspondance ci-dessous est <b>etablie au desassembleur</b> et
    /// suffit a rendre la pseudo-C utilisable :
    /// </para>
    /// <list type="table">
    /// <item><term><c>param_1</c>, <c>puStack_18</c></term><description>l'<b>argument 1</b>,
    /// l'occupant — <c>[esp+74h]</c> apres les quatre <c>push</c> du prologue. Les deux noms
    /// designent le meme objet : <c>puStack_18 = param_1</c> en tete de boucle et
    /// <c>param_1 = puStack_18</c> en queue.</description></item>
    /// <item><term><c>local_14</c> <i>vu comme une entite</i></term><description>l'<b>argument
    /// 2</b>, le personnage qui se deplace — <c>[esp+78h]</c>.</description></item>
    /// <item><term><c>local_14</c> <i>passe a FUN_0051c300 / FUN_005327b0 / Ordinal_366</i></term>
    /// <description>un <b>tampon local de neuf shorts</b> a <c>[esp+5Ch]</c>, la matrice de
    /// l'occupant. Rien a voir avec l'entite ci-dessus : Ghidra a fusionne deux emplacements de
    /// pile voisins.</description></item>
    /// <item><term><c>iStack_10</c></term><description>l'<b>argument 3</b>, l'entree
    /// d'emplacement — <c>[esp+7Ch]</c>. Elle recoit le vecteur de contact a <c>+0x1c</c> et la
    /// pose de l'occupant a <c>+0x28</c>..<c>+0x48</c>.</description></item>
    /// <item><term><c>puStack_54</c></term><description>le bloc de modele de l'occupant, mis de
    /// cote en tete et restaure a chaque tour de boucle.</description></item>
    /// </list>
    /// <para>
    /// <b>Le recoupement decisif est 0x00534778</b> : <c>mov [edi+78h],esi</c> puis
    /// <c>mov ecx,[esp+7Ch] ; mov [ecx+1Ch],edx</c> ou <c>edx = [edi+1Ch] - [esi+1Ch]</c>. EDI est
    /// l'argument 2 et ESI l'argument 1, donc <c>entite[0x78] = occupant</c> et
    /// <c>entree[0x1c] = entite.x - occupant.x</c> — ce que la pseudo-C rend en
    /// <c>local_14[0x1e] = puStack_18</c> et
    /// <c>*(uint *)(iStack_10 + 0x1c) = local_14[7] - puStack_18[7]</c>.
    /// </para>
    /// <para>
    /// <b>Le danger de cette fonction est le silence.</b> Intervertir les deux entites compile,
    /// tourne, et rend une collision qui pousse le decor au lieu du personnage. Aucun compteur de
    /// primitives ne le dirait. Verifier chaque acces contre la table ci-dessus avant de
    /// l'ecrire, et ne rien deduire du nom seul.
    /// </para>
    /// <para>
    /// Les six termes du <c>NormalClip</c> sont <b>inlines</b> ici et le depot en porte deja la
    /// version reconstruite (<c>FUN_0051c280</c>, <c>Platform/Winsys3d.cs</c>) : les recouper terme
    /// a terme avant d'appeler, Ghidra les emet dans un autre ordre — precedent deja paye sur
    /// <c>FinalizeEntityPlacement2</c>.
    /// </para>
    /// </summary>
    private static uint ResolveEntityContact_00534330(uint* occupantPtr, uint* entityPtr, byte* entry)
    {
        byte* occupant = (byte*)occupantPtr;
        byte* entity = (byte*)entityPtr;
        byte* mainMem = GetMainMemPtr();

        EnsureGteRegisters();

        // Le maillage de collision de l'occupant, choisi par son type et le decalage de zone.
        byte* model = *(byte**)(Mem.Entity(0x005f7bc0) + (occupant[4] + *Mem.Entity(0x00600838)) * 4);

        // La rangee de translation du registre MATRIX est mise a zero : on travaille en repere
        // local de l'occupant, la translation est faite a la main par les deltas ci-dessous.
        *(int*)(s_GteMatrix814 + 0x1c) = 0;
        *(int*)(s_GteMatrix814 + 0x18) = 0;
        *(int*)(s_GteMatrix814 + 0x14) = 0;

        short* matrix = stackalloc short[9];
        RotMatrix((short*)(occupant + 0x30), matrix);
        ScaleMatrix_005327B0(matrix, (ushort*)(occupant + 0x28));
        Winsys3d.Ordinal_366(matrix);

        // Les deux nappes de sommets transformes : les positions dans MainMem + 0x1b0000, les
        // normales dans + 0x1b8000. Le mot de tete du modele porte les deux comptes.
        uint counts = *(uint*)model;
        Winsys3d.Ordinal_792((short*)(model + 0x40), (short*)(mainMem + 0x1b0000), counts & 0xffff);
        Winsys3d.Ordinal_792(*(short**)(model + 0x24), (short*)(mainMem + 0x1b8000),
            (uint)((int)counts >> 0x10) & 0xffff);

        bool pushed = false;      // Ghidra: bVar6
        bool overFace = false;    // Ghidra: bVar5

        int occupantY = *(int*)(occupant + 0x20);
        int entityY = *(int*)(entity + 0x20);

        short* faceIndex = *(short**)(*(byte**)(model + 0x38) + 4);

        // **L'asymetrie de ces trois deltas est reelle**, verifiee au desassembleur
        // (0x005343ef..0x0053440a) : X et Z sont pris `entite - occupant`, mais Y
        // `occupant - entite`. Ce n'est pas un artefact de Ghidra, et ce n'est pas corrige.
        int deltaX = (*(int*)(entity + 0x1c) - *(int*)(occupant + 0x1c)) >> 0xc;
        int deltaZ = (*(int*)(entity + 0x24) - *(int*)(occupant + 0x24)) >> 0xc;

        *(uint*)s_GteSxy807 = ((uint)deltaZ << 0x10) | ((uint)deltaX & 0xffff);

        int* vec799 = (int*)s_GteVec799;
        int* vec800 = (int*)s_GteVec800;
        int* mac801 = (int*)s_GteMac801;
        short* ir813 = (short*)s_GteIr813;

        while (true)
        {
            short index = *faceIndex;

            if (index < 0)
            {
                if (!overFace) break;

                // L'entite repose sur une face de cet occupant sans le traverser.
                *(uint*)(occupant + 0x70) = (uint)(nuint)entity;
                if ((*entityPtr & 0x10000) == 0)
                {
                    *(uint*)(occupant + 0x6c) = (uint)(nuint)entity;
                    *occupantPtr |= 0x40;
                }

                if ((*entityPtr & 0x40012000) == 0)
                {
                    byte kind = (byte)(occupant[0x0d] & 0xf);
                    if (kind == 0 || (kind == 1 && (occupant[9] & 0xf) == 0))
                    {
                        RunEntityScript(s_Unknown_005e97cc, entity, *(ushort*)(occupant + 0x0e));
                    }
                }

                if (pushed)
                {
                    *(uint*)(entity + 0x90) |= 0x8000800;
                    *(int*)(occupant + 0x50) += *(int*)(entity + 0x50);
                    *(int*)(occupant + 0x58) += *(int*)(entity + 0x58);
                }

                occupant[9] |= (byte)(1 << (int)(*(uint*)(entity + 8) & 0x1f));
                return FinishContact(occupantPtr, entityPtr);
            }

            byte* face = *(byte**)(model + 0x28) + index * 0x10;
            uint corners = *(uint*)face;
            ushort attributes = *(ushort*)(face + 0x0c);
            uint* normal = (uint*)(mainMem + 0x1b8000 + face[4] * 8);

            byte* cache = mainMem + 0x1b0000;
            short* a = (short*)(cache + (corners & 0xff) * 8);
            short* b = (short*)(cache + ((corners >> 8) & 0xff) * 8);
            short* c = (short*)(cache + ((corners >> 0x10) & 0xff) * 8);

            // Le produit vectoriel des deux aretes de la face, depose dans MAC1..3 puis recopie
            // dans IR1..3 — la normale non normalisee.
            vec800[0] = b[0] - a[0];
            vec800[1] = b[1] - a[1];
            vec800[2] = b[2] - a[2];
            vec799[0] = c[0] - a[0];
            vec799[1] = c[1] - a[1];
            vec799[2] = c[2] - a[2];

            mac801[0] = vec799[2] * vec800[1] - vec799[1] * vec800[2];
            mac801[1] = vec800[2] * vec799[0] - vec799[2] * vec800[0];
            mac801[2] = vec799[1] * vec800[0] - vec800[1] * vec799[0];

            ir813[0] = (short)mac801[0];
            ir813[1] = (short)mac801[1];
            ir813[2] = (short)mac801[2];

            int normalY = mac801[1];
            int planar = (deltaZ - a[2]) * mac801[2] + (deltaX - a[0]) * mac801[0];
            int side = (((occupantY - entityY) >> 0xc) - a[1]) * normalY + planar;

            // De quel cote du plan de la face l'entite se trouve-t-elle ? Les faces marquees
            // 0x2000 sont a sens unique et laissent passer par en dessous.
            if ((normalY == 0 && side >= 0)
                || (normalY > 0 && side > 0)
                || (normalY < 0 && side > 0 && (attributes & 0x2000) != 0))
            {
                break;
            }

            if ((attributes & 0x2000) == 0)
            {
                // Le test d'appartenance, en projection sur le plan XZ : trois (ou quatre pour un
                // quad) NormalClip contre les aretes. Les six termes inlines par le compilateur
                // sont **terme a terme** ceux de Winsys3d.NormalClip, deja reconstruit — recoupes
                // avant d'appeler, comme FinalizeEntityPlacement2 l'a exige en aout.
                uint flatA = ((uint)a[2] << 0x10) | (ushort)a[0];
                uint flatB = ((uint)b[2] << 0x10) | (ushort)b[0];
                uint flatC = ((uint)c[2] << 0x10) | (ushort)c[0];

                *(uint*)s_GteSxy805 = flatA;
                *(uint*)s_GteSxy806 = flatB;

                if (NormalClip() >= 0)
                {
                    *(uint*)s_GteSxy805 = flatB;
                    *(uint*)s_GteSxy806 = flatC;

                    if (NormalClip() >= 0)
                    {
                        int last;

                        if ((attributes & 1) == 0)
                        {
                            *(uint*)s_GteSxy805 = flatC;
                            *(uint*)s_GteSxy806 = flatA;
                            last = NormalClip();
                        }
                        else
                        {
                            // Face a quatre sommets : le quatrieme est l'octet haut du mot de
                            // coins, et le test se fait en deux aretes de plus.
                            short* d = (short*)(cache + (corners >> 0x18) * 8);
                            uint flatD = ((uint)d[2] << 0x10) | (ushort)d[0];

                            *(uint*)s_GteSxy805 = flatC;
                            *(uint*)s_GteSxy806 = flatD;
                            if (NormalClip() < 0) goto nextFace;

                            *(uint*)s_GteSxy805 = flatD;
                            *(uint*)s_GteSxy806 = flatA;
                            last = NormalClip();
                        }

                        if (last >= 0)
                        {
                            // L'entite est au-dessus de cette face : on en deduit la hauteur du
                            // plan sous elle.
                            int height = normalY == 0
                                ? occupantY
                                : (planar / normalY - a[1]) * 0x1000 + occupantY;

                            int reach = -0x32000;
                            if (*(int*)(entity + 0x54) < 0) reach = *(int*)(entity + 0x54) - 0x32000;

                            if (height + reach <= entityY)
                            {
                                *occupantPtr |= 0x20;
                                *entityPtr |= 0x20;

                                if ((*(int*)(entity + 0x64) <= height && (attributes & 0x1000) == 0)
                                    || (*(uint*)(entity + 0x90) & 4) != 0)
                                {
                                    *(int*)(entity + 0x64) = height;
                                    *(uint*)(entity + 0x78) = (uint)(nuint)occupant;
                                    PublishContact(entry, entity, occupant, normal);
                                }

                                if (*(int*)(entity + 0x60) <= height
                                    || (*(uint*)(entity + 0x90) & 4) != 0)
                                {
                                    *(int*)(entity + 0x60) = height;
                                    *(uint*)(entity + 0x78) = (uint)(nuint)occupant;

                                    if ((*(uint*)(entity + 0x90) & 0x8800) == 0)
                                    {
                                        *(uint*)(entity + 0x90) = attributes;
                                    }

                                    if (entityY <= height)
                                    {
                                        // Contact franc : l'entite est posee dessus.
                                        (*(short*)(occupant + 0x96))++;
                                        *occupantPtr |= 0x10;
                                        *(int*)(entity + 0x54) = 0;
                                        *entityPtr |= 0x10;
                                        PublishContact(entry, entity, occupant, normal);

                                        uint slotIndex = *(uint*)(entity + 8);
                                        *(uint*)(occupant + 0x70) = (uint)(nuint)entity;
                                        occupant[9] |= (byte)((1 << (int)(slotIndex & 0x1f)) << 4);

                                        if ((*entityPtr & 0x10000) == 0)
                                        {
                                            *(uint*)(occupant + 0x6c) = (uint)(nuint)entity;
                                        }

                                        if ((*entityPtr & 0x40012000) == 0
                                            && (occupant[0x0d] & 0xf) == 4)
                                        {
                                            RunEntityScript(s_Unknown_005e97cc, entity,
                                                *(ushort*)(occupant + 0x0e));
                                        }

                                        return FinishContact(occupantPtr, entityPtr);
                                    }
                                }

                                return 0;
                            }

                            // Trop haut pour se poser : la hauteur est notee et l'entite sera
                            // poussee au tour suivant.
                            *(int*)(entry + 4) = height;
                            pushed = true;
                            overFace = true;
                        }
                    }
                }
            }
            else
            {
                overFace = true;
            }

        nextFace:
            faceIndex++;
        }

        // Ghidra: LAB_0053470b — aucune face ne porte l'entite.
        if ((*entityPtr & 0x40012000) == 0 && (occupant[0x0d] & 0xf) == 2)
        {
            RunEntityScript(s_Unknown_005e97cc, entity, *(ushort*)(occupant + 0x0e));
        }

        occupant[9] &= (byte)~(1 << (int)(*(uint*)(entity + 8) & 0x1f));
        return 0;
    }

    /// <summary>
    /// Ghidra: <c>LAB_005348d5</c> — les deux lignes communes aux deux sorties « contact » de
    /// <see cref="ResolveEntityContact_00534330"/>.
    /// </summary>
    private static uint FinishContact(uint* occupantPtr, uint* entityPtr)
    {
        if ((*occupantPtr & 0x10000000) == 0 && (*entityPtr & 0x10000) == 0)
        {
            *occupantPtr &= 0xbfffffff;
        }

        return 1;
    }

    /// <summary>
    /// Le bloc de treize ecritures que <see cref="ResolveEntityContact_00534330"/> repete
    /// <b>a l'identique</b> a ses deux points de contact : le vecteur qui separe les deux entites,
    /// la pose complete de l'occupant, et les deux mots de normale de la face touchee.
    /// Factorise parce que c'est une unite reellement independante — le meme bloc, deux fois.
    /// </summary>
    private static void PublishContact(byte* entry, byte* entity, byte* occupant, uint* normal)
    {
        *(uint*)(entry + 0x1c) = (uint)(*(int*)(entity + 0x1c) - *(int*)(occupant + 0x1c));
        *(uint*)(entry + 0x20) = (uint)(*(int*)(entity + 0x20) - *(int*)(occupant + 0x20));
        *(uint*)(entry + 0x24) = (uint)(*(int*)(entity + 0x24) - *(int*)(occupant + 0x24));

        *(uint*)(entry + 0x28) = *(uint*)(occupant + 0x1c);
        *(uint*)(entry + 0x2c) = *(uint*)(occupant + 0x20);
        *(uint*)(entry + 0x30) = *(uint*)(occupant + 0x24);
        *(uint*)(entry + 0x34) = *(uint*)(occupant + 0x28);
        *(uint*)(entry + 0x38) = *(uint*)(occupant + 0x2c);
        *(uint*)(entry + 0x3c) = *(uint*)(occupant + 0x30);
        *(uint*)(entry + 0x40) = *(uint*)(occupant + 0x34);

        *(uint*)(entry + 0x44) = normal[0];
        *(uint*)(entry + 0x48) = normal[1];
    }

    /// <summary>
    /// Ghidra: FUN_005327b0 @ 0x005327b0 — le <c>ScaleMatrix</c> du SDK : chaque <b>colonne</b> de
    /// la matrice 3x3 est multipliee par un facteur en 1.12 pris dans un triplet de
    /// <c>ushort</c>.
    ///
    /// <para>
    /// L'original passe par les registres GTE emules — le facteur dans 812, la colonne dans
    /// IR1..3 (813), le produit dans MAC1..3 (801) — et non par des variables. Transcrit tel quel :
    /// ces registres sont observables depuis le reste du moteur, et rien ne garantit qu'aucun
    /// lecteur ne les regarde entre deux appels.
    /// </para>
    /// <para>Ce sont bien les <b>colonnes</b> et non les rangees : la fonction touche les indices
    /// 0/3/6, puis 1/4/7, puis 2/5/8.</para>
    /// </summary>
    private static void ScaleMatrix_005327B0(short* matrix, ushort* scale)
    {
        EnsureGteRegisters();

        ushort* factor = (ushort*)s_GteScale812;
        short* ir = (short*)s_GteIr813;
        int* mac = (int*)s_GteMac801;

        for (int column = 0; column < 3; column++)
        {
            *factor = scale[column];
            ir[0] = matrix[column];
            ir[1] = matrix[column + 3];
            ir[2] = matrix[column + 6];

            mac[0] = (int)(*factor * ir[0]) >> 0xc;
            ir[0] = (short)mac[0];
            mac[1] = (int)(ir[1] * *factor) >> 0xc;
            ir[1] = (short)mac[1];
            mac[2] = (int)(ir[2] * *factor) >> 0xc;
            ir[2] = (short)mac[2];

            matrix[column] = ir[0];
            matrix[column + 3] = ir[1];
            matrix[column + 6] = ir[2];
        }
    }

    /// <summary>
    /// Ghidra: FUN_00519f50 @ 0x00519f50 — <b>la laisse</b> : l'entite est ramenee vers le point
    /// que suit la camera des qu'elle s'en eloigne de plus de 400 unites, proportionnellement a
    /// l'exces. En deca, elle se contente de poser le bit <c>0x100</c>.
    ///
    /// <para>
    /// <b>Ghidra a fondu les deux parametres en un.</b> Il declare
    /// <c>void FUN_00519f50(uint *param_1)</c> et rend l'ecriture de la distance comme
    /// <c>*(undefined2 *)param_1 = …</c>, c'est-a-dire dans la moitie basse du mot d'etat de
    /// l'entite — ce qui ecraserait seize drapeaux. Le desassemblage dit autre chose :
    /// <c>0x00519f9b</c> lit <c>[esp+18h]</c>, soit <b>le second argument</b> apres les quatre
    /// <c>push</c> du prologue, et ecrit la distance dans <c>[ecx]</c>. La distance va donc dans
    /// l'<b>entree d'emplacement</b>, pas dans l'entite.
    /// </para>
    /// <para>
    /// C'est le cinquieme parametre perdu de cet arbre d'appel, et le plus dangereux : les quatre
    /// autres se voyaient parce que la pseudo-C lisait une variable jamais ecrite. Celui-ci
    /// <b>compile et tourne</b>, en corrompant seize drapeaux a chaque image.
    /// </para>
    /// </summary>
    private static void PullEntityTowardCameraTarget_00519F50(uint* entity, byte* entry)
    {
        byte* e = (byte*)entity;

        *entity &= 0xfffffeff;
        if ((*(uint*)(e + 0x90) & 0x8000) != 0) return;

        int dx = (*Mem.Camera(0x005de098) - *(int*)(e + 0x1c)) >> 0xc;
        int dz = (*Mem.Camera(0x005de0a0) - *(int*)(e + 0x24)) >> 0xc;
        *(ushort*)entry = (ushort)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz));

        dz = (*Mem.Camera(0x005de070) - *(int*)(e + 0x24)) >> 0xc;
        dx = (*Mem.Camera(0x005de068) - *(int*)(e + 0x1c)) >> 0xc;

        int angle = Winsys3d.Ordinal_457(dx, dz);
        int excess = (int)Winsys3d.Ordinal_442((uint)(dx * dx + dz * dz)) - 400;

        uint flags = *entity;
        if (excess < 1)
        {
            if ((flags & 0x4000) == 0) *entity = flags | 0x100;
        }
        else if ((flags & 0x40000000) == 0)
        {
            *(int*)(e + 0x50) += (int)Winsys3d.GetSinTableEntry(angle) * excess;
            *(int*)(e + 0x58) += Winsys3d.GetCosTableEntry(angle) * excess;
        }
    }

    /// <summary>
    /// Ghidra: FUN_00519ee0 @ 0x00519ee0 — <b>le clignotement d'invulnerabilite</b>. Le compte a
    /// rebours de <c>+0x96</c> descend, et sa parite alterne entre le rendu normal (mode 1, blanc)
    /// et le rendu teinte (mode 5, orange <c>0x4080ff</c>). A zero, le bit <c>0x40</c> tombe.
    ///
    /// <para>
    /// La garde du haut fait retomber trois etats de deplacement (0, 1 et 4) sur l'etat 2 des que
    /// <c>0x40</c> est pose : un personnage touche ne marche plus, il encaisse.
    /// </para>
    /// <para>
    /// Comme <see cref="PostureHandler_0051A670"/>, Ghidra ne lui donne qu'un parametre alors que
    /// le site d'appel en empile deux ; le corps ne lit pas le second.
    /// </para>
    /// </summary>
    private static void UpdateEntityShadow_00519EE0(uint* entity, byte* perChar)
    {
        byte* e = (byte*)entity;

        if ((*entity & 0x40) != 0)
        {
            short state = *(short*)(e + 0x18);
            if (state == 0 || state == 1 || state == 4)
            {
                *(uint*)(e + 0x18) = 2;
                *entity &= 0xfefffff9;
            }
        }

        if (*(short*)(e + 0x96) != 0)
        {
            ushort remaining = (ushort)(*(short*)(e + 0x96) - 1);
            *(ushort*)(e + 0x96) = remaining;

            if ((remaining & 1) == 0)
            {
                *(ushort*)(e + 0x36) = 1;
                *(uint*)(e + 0x38) = 0xffffff;
            }
            else
            {
                *(ushort*)(e + 0x36) = 5;
                *(uint*)(e + 0x38) = 0x4080ff;
            }

            if (remaining == 0) *entity &= 0xffffffbf;
        }
    }

    /// <summary>Ghidra: FUN_005335e0 @ 0x005335e0 (78 l.) — l'angle de la pente sous une
    /// position.</summary>
    private static int GroundSlopeAngle_005335E0(uint* entity, int* position)
        => throw new NotImplementedException(
            "FUN_005335e0 (0x005335e0) : non transcrite. 78 lignes dans Blaze.c — l'angle de la " +
            "face du terrain sous une position, dont la sonde se sert pour recaler le cap contre " +
            "un mur.");
}
