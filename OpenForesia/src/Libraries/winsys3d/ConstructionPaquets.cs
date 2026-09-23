// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur**.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LA CONSTRUCTION DES PAQUETS — ce qui REMPLIT le tampon de commandes (2026-09-23)
//
// `CheminAffichage.cs` REJOUE le tampon de commandes ; ce fichier le REMPLIT. L'entree est
// `ConstruirePaquet` (FUN_00a0dde0, ordinal 752), appele par l'opcode 0xe6 du repartiteur quand le
// bit 31 du descripteur dit « pas encore construit » :
//
//     AllouerPaquet(n)      FUN_00a0c480   Malloc(n * 0x94) ; curseur a 2 ; zone de sommets a
//                                          paquet + n * 0x14 ; tous les memos a -1
//     le mot du descripteur  -> paquet[0]  (le lien d'OT et la longueur)
//     l'adaptateur du genre  (0..15)       un enregistrement de modele -> un paquet de primitive
//                                          sur la pile -> le producteur de ce genre de primitive
//     descripteur = paquet | 0x80000000    « construit »
//
// LES ADAPTATEURS. Le descripteur commence par le nombre d'enregistrements (un mot court) ; chaque
// adaptateur parcourt ces enregistrements a pas fixe et recopie, champ par champ, couleurs,
// coordonnees de texture, page, CLUT et numeros de sommets dans un petit paquet de primitive sur
// sa pile, qu'il passe au PRODUCTEUR avec l'adresse des sommets. Les genres 0 a 7 portent leurs
// sommets dans l'enregistrement meme ; les genres 8 a 15 les prennent dans un tableau externe (le
// second argument de `ConstruirePaquet`), et leurs numeros de sommet sont des octets divises par 8
// (`sar dx,3`). Les genres 12 et 13 ont deux entrees distinctes dans la table de sauts
// (0x00a0dfb4) mais le meme adaptateur, `FUN_00a0dba0`.
//
// Les 15 adaptateurs ont ete ENGENDRES mecaniquement depuis `winsys3d.c` (affectations lues une a
// une, six formes reconnues, aucune laissee de cote), puis verifies au desassembleur sur
// `FUN_00a0d260` et `FUN_00a0d7f0` : decalages du paquet, pas des enregistrements, pointeur des
// sommets, relecture du nombre a chaque tour (`movsx ecx,word [ebx]`).
//
// Les producteurs (huit des dix-huit : `07d80`, `08510`, `08c80`, `09070`, `09e60`, `0a670`,
// `0b000`, `0b930`) sont portes depuis l'etape 10 — plus bas, avec la reconstruction `c4f0` qui
// emploie les dix-huit. Ce sont les primitives du format **TMD** de la libgs PlayStation.
//
// --------------------------------------------------------------------------------------------
// UN ECART ASSUME : LES CHAMPS DU PAQUET QUE L'ADAPTATEUR N'ECRIT PAS
//
// Le paquet de primitive est une variable de pile ; chaque adaptateur n'en ecrit qu'une partie
// (jamais le mot d'en-tete, rarement l'octet de code). L'original y laisse ce que la pile contenait
// — puis, d'un enregistrement a l'autre, ce que le tour precedent y avait mis. Le C# l'alloue une
// fois par appel (`stackalloc`, donc a zero) et le garde d'un tour a l'autre, comme l'original. Si
// un producteur lisait un champ jamais ecrit, il lirait 0 ici et n'importe quoi dans l'original.
// **Verifie a l'etape 10, producteur par producteur : aucun ne le fait.** Chacun des huit ne lit
// que des champs que ses adaptateurs ecrivent (couleurs, u v, CLUT, page, numeros de sommet) ;
// ni le mot d'en-tete ni l'octet de mode ne sont relus. L'ecart est donc sans effet.
// ============================================================================================

using System;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dConstructionPaquets
{
    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0dde0 @ 0x00a0dde0 (ordinal 752) — construit le paquet d'un
    /// descripteur de modele, `ret 0Ch`. Rien si le bit 31 est deja pose ; genre au-dela de 15
    /// (compare non signe) : le paquet est alloue et son en-tete ecrit, mais le descripteur n'est
    /// PAS marque construit.
    /// </summary>
    internal static void ConstruirePaquet(uint* descripteur, void* sommets, int genre)
    {
        if ((int)*descripteur < 0) return;

        AllouerPaquet((short)*descripteur);
        *Winsys3dData.PaquetCourant_011be7ac = *descripteur;

        short* p = (short*)descripteur;
        switch ((uint)genre)
        {
        case 0: Adapter_00a0d260(p); break;
        case 1: Adapter_00a0d310(p); break;
        case 2: Adapter_00a0d3d0(p); break;
        case 3: Adapter_00a0d4b0(p); break;
        case 4: Adapter_00a0d5c0(p); break;
        case 5: Adapter_00a0d630(p); break;
        case 6: Adapter_00a0d6a0(p); break;
        case 7: Adapter_00a0d730(p); break;
        case 8: Adapter_00a0d7f0(p, sommets); break;
        case 9: Adapter_00a0d8b0(p, sommets); break;
        case 10: Adapter_00a0d990(p, sommets); break;
        case 11: Adapter_00a0da80(p, sommets); break;
        case 12: Adapter_00a0dba0(p, sommets); break;
        case 13: Adapter_00a0dba0(p, sommets); break;
        case 14: Adapter_00a0dc30(p, sommets); break;
        case 15: Adapter_00a0dce0(p, sommets); break;
        default: return;
        }
        *descripteur = (uint)Winsys3dData.PaquetCourant_011be7ac | 0x80000000;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0c480 @ 0x00a0c480 — alloue un paquet de <paramref name="n"/>
    /// primitives (0x94 octets chacune, dans le tas de `wsyskern` : `call [12153E4h]`, l'export
    /// `Malloc`), et remet tout l'etat du tampon a neuf : curseur a 2 (apres l'en-tete de 8
    /// octets), etat de rendu 8, zone de sommets a `paquet + n * 0x14`, et tous les memos a -1 —
    /// ceux de couleur en 16 bits.
    /// </summary>
    internal static void AllouerPaquet(int n)
    {
        byte* paquet = (byte*)Blaze.Wsyskern.Malloc(n * 0x94);
        Winsys3dData.PaquetCourant_011be7ac = (uint*)paquet;
        Winsys3dData.Curseur_011be7dc = 2;
        Winsys3dData.EtatRendu_00a2c480 = 8;
        Winsys3dData.Sommets_011be7a8 = (float*)(paquet + n * 0x14);
        Winsys3dData.SommetsDebut_011be7a4 = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.DerniereCommande_00a2c578 = -1;
        Winsys3dData.DerniereCommandeArg_00a2c57c = -1;
        Winsys3dData.MemoRouge_011be764 = 0xffff;
        Winsys3dData.MemoVert_011be768 = 0xffff;
        Winsys3dData.MemoBleu_011be76c = 0xffff;
        Winsys3dData.Memo_00a2c484 = -1;
        Winsys3dData.PageTexture_00a2c488 = -1;
        Winsys3dData.Memo_00a2c48c = -1;
    }

    // ============================================================================================
    // Les quinze adaptateurs — engendres depuis `winsys3d.c`, voir l'en-tete.
    // ============================================================================================

    /// <summary>Ghidra: winsys3d.c FUN_00a0d260 @ 0x00a0d260 — genre 0 : paquet de 0x1c octets sur la pile, vers <c>FUN_00a08c80</c>.</summary>
    internal static void Adapter_00a0d260(short* p)
    {
        byte* q = stackalloc byte[0x1c];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1a + i * 0x58;
            *(short*)(q + 0x06) = *(short*)(s - 0x8);
            q[0x04] = (byte)s[-10];
            *(short*)(q + 0x0a) = *(short*)s;
            q[0x05] = (byte)s[-9];
            q[0x09] = (byte)s[-1];
            q[0x0c] = (byte)s[6];
            q[0x08] = (byte)s[-2];
            q[0x10] = (byte)s[-18];
            q[0x11] = (byte)s[-17];
            q[0x0d] = (byte)s[7];
            q[0x12] = (byte)s[-16];
            *(short*)(q + 0x14) = 0;
            *(short*)(q + 0x16) = 1;
            *(short*)(q + 0x18) = 2;
            Ft3SansLumiere_00a08c80(q, s + 0x2a, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d310 @ 0x00a0d310 — genre 1 : paquet de 0x20 octets sur la pile, vers <c>FUN_00a09070</c>.</summary>
    internal static void Adapter_00a0d310(short* p)
    {
        byte* q = stackalloc byte[0x20];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1a + i * 0x70;
            *(short*)(q + 0x0a) = *(short*)s;
            q[0x04] = (byte)s[-10];
            *(short*)(q + 0x06) = *(short*)(s - 0x8);
            q[0x08] = (byte)s[-2];
            q[0x09] = (byte)s[-1];
            q[0x05] = (byte)s[-9];
            q[0x0d] = (byte)s[7];
            q[0x10] = (byte)s[14];
            q[0x0c] = (byte)s[6];
            q[0x14] = (byte)s[-18];
            q[0x15] = (byte)s[-17];
            q[0x11] = (byte)s[15];
            q[0x16] = (byte)s[-16];
            *(short*)(q + 0x18) = 0;
            *(short*)(q + 0x1a) = 1;
            *(short*)(q + 0x1c) = 2;
            *(short*)(q + 0x1e) = 3;
            Ft4SansLumiere_00a09070(q, s + 0x3a, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d3d0 @ 0x00a0d3d0 — genre 2 : paquet de 0x24 octets sur la pile, vers <c>FUN_00a0a670</c>.</summary>
    internal static void Adapter_00a0d3d0(short* p)
    {
        byte* q = stackalloc byte[0x24];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1e + i * 0x68;
            *(short*)(q + 0x06) = *(short*)(s - 0xc);
            q[0x04] = (byte)s[-14];
            *(short*)(q + 0x0a) = *(short*)s;
            q[0x05] = (byte)s[-13];
            q[0x09] = (byte)s[-1];
            q[0x0c] = (byte)s[10];
            q[0x08] = (byte)s[-2];
            q[0x10] = (byte)s[-22];
            q[0x11] = (byte)s[-21];
            q[0x0d] = (byte)s[11];
            q[0x14] = (byte)s[-10];
            q[0x15] = (byte)s[-9];
            q[0x12] = (byte)s[-20];
            q[0x18] = (byte)s[2];
            q[0x19] = (byte)s[3];
            q[0x16] = (byte)s[-8];
            q[0x1a] = (byte)s[4];
            *(short*)(q + 0x1c) = 0;
            *(short*)(q + 0x1e) = 1;
            *(short*)(q + 0x20) = 2;
            Gt3SansLumiere_00a0a670(q, s + 0x36, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d4b0 @ 0x00a0d4b0 — genre 3 : paquet de 0x2c octets sur la pile, vers <c>FUN_00a0b930</c>.</summary>
    internal static void Adapter_00a0d4b0(short* p)
    {
        byte* q = stackalloc byte[0x2c];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1e + i * 0x88;
            *(short*)(q + 0x0a) = *(short*)s;
            q[0x04] = (byte)s[-14];
            *(short*)(q + 0x06) = *(short*)(s - 0xc);
            q[0x08] = (byte)s[-2];
            q[0x09] = (byte)s[-1];
            q[0x05] = (byte)s[-13];
            q[0x0d] = (byte)s[11];
            q[0x10] = (byte)s[22];
            q[0x0c] = (byte)s[10];
            q[0x14] = (byte)s[-22];
            q[0x15] = (byte)s[-21];
            q[0x11] = (byte)s[23];
            q[0x18] = (byte)s[-10];
            q[0x19] = (byte)s[-9];
            q[0x16] = (byte)s[-20];
            q[0x1c] = (byte)s[2];
            q[0x1d] = (byte)s[3];
            q[0x1a] = (byte)s[-8];
            q[0x20] = (byte)s[14];
            q[0x21] = (byte)s[15];
            q[0x1e] = (byte)s[4];
            q[0x22] = (byte)s[16];
            *(short*)(q + 0x24) = 0;
            *(short*)(q + 0x26) = 1;
            *(short*)(q + 0x28) = 2;
            *(short*)(q + 0x2a) = 3;
            Gt4SansLumiere_00a0b930(q, s + 0x4e, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d5c0 @ 0x00a0d5c0 — genre 4 : paquet de 0x10 octets sur la pile, vers <c>FUN_00a07d80</c>.</summary>
    internal static void Adapter_00a0d5c0(short* p)
    {
        byte* q = stackalloc byte[0x10];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x9 + i * 0x40;
            q[0x04] = (byte)s[-1];
            q[0x05] = (byte)s[0];
            q[0x06] = (byte)s[1];
            *(short*)(q + 0x08) = 0;
            *(short*)(q + 0x0a) = 1;
            *(short*)(q + 0x0c) = 2;
            F3SansLumiere_00a07d80(q, s + 0x23, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d630 @ 0x00a0d630 — genre 5 : paquet de 0x10 octets sur la pile, vers <c>FUN_00a08510</c>.</summary>
    internal static void Adapter_00a0d630(short* p)
    {
        byte* q = stackalloc byte[0x10];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x9 + i * 0x50;
            q[0x04] = (byte)s[-1];
            q[0x05] = (byte)s[0];
            q[0x06] = (byte)s[1];
            *(short*)(q + 0x08) = 0;
            *(short*)(q + 0x0a) = 1;
            *(short*)(q + 0x0c) = 2;
            *(short*)(q + 0x0e) = 3;
            F4SansLumiere_00a08510(q, s + 0x2b, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d6a0 @ 0x00a0d6a0 — genre 6 : paquet de 0x18 octets sur la pile, vers <c>FUN_00a09e60</c>.</summary>
    internal static void Adapter_00a0d6a0(short* p)
    {
        byte* q = stackalloc byte[0x18];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x9 + i * 0x50;
            q[0x04] = (byte)s[-1];
            q[0x05] = (byte)s[0];
            q[0x06] = (byte)s[1];
            q[0x08] = (byte)s[7];
            q[0x09] = (byte)s[8];
            q[0x0c] = (byte)s[15];
            q[0x0d] = (byte)s[16];
            q[0x0a] = (byte)s[9];
            q[0x0e] = (byte)s[17];
            *(short*)(q + 0x10) = 0;
            *(short*)(q + 0x12) = 1;
            *(short*)(q + 0x14) = 2;
            G3SansLumiere_00a09e60(q, s + 0x33, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d730 @ 0x00a0d730 — genre 7 : paquet de 0x1c octets sur la pile, vers <c>FUN_00a0b000</c>.</summary>
    internal static void Adapter_00a0d730(short* p)
    {
        byte* q = stackalloc byte[0x1c];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x9 + i * 0x68;
            q[0x04] = (byte)s[-1];
            q[0x05] = (byte)s[0];
            q[0x06] = (byte)s[1];
            q[0x08] = (byte)s[7];
            q[0x09] = (byte)s[8];
            q[0x0c] = (byte)s[15];
            q[0x0d] = (byte)s[16];
            q[0x0a] = (byte)s[9];
            q[0x10] = (byte)s[23];
            q[0x11] = (byte)s[24];
            q[0x0e] = (byte)s[17];
            q[0x12] = (byte)s[25];
            *(short*)(q + 0x14) = 0;
            *(short*)(q + 0x16) = 1;
            *(short*)(q + 0x18) = 2;
            *(short*)(q + 0x1a) = 3;
            G4SansLumiere_00a0b000(q, s + 0x43, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d7f0 @ 0x00a0d7f0 — genre 8 : paquet de 0x1c octets sur la pile, vers <c>FUN_00a08c80</c>.</summary>
    internal static void Adapter_00a0d7f0(short* p, void* sommets)
    {
        byte* q = stackalloc byte[0x1c];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1a + i * 0x4c;
            *(short*)(q + 0x06) = *(short*)(s - 0x8);
            *(short*)(q + 0x0a) = *(short*)s;
            *(short*)(q + 0x14) = (short)(*(short*)(s + 0x2a) >> 3);
            q[0x04] = (byte)s[-10];
            *(short*)(q + 0x16) = (short)(*(short*)(s + 0x2e) >> 3);
            *(short*)(q + 0x18) = (short)(*(short*)(s + 0x32) >> 3);
            q[0x09] = (byte)s[-1];
            q[0x05] = (byte)s[-9];
            q[0x08] = (byte)s[-2];
            q[0x10] = (byte)s[-18];
            q[0x0c] = (byte)s[6];
            q[0x0d] = (byte)s[7];
            q[0x11] = (byte)s[-17];
            q[0x12] = (byte)s[-16];
            Ft3SansLumiere_00a08c80(q, sommets, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d8b0 @ 0x00a0d8b0 — genre 9 : paquet de 0x20 octets sur la pile, vers <c>FUN_00a09070</c>.</summary>
    internal static void Adapter_00a0d8b0(short* p, void* sommets)
    {
        byte* q = stackalloc byte[0x20];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1a + i * 0x60;
            *(short*)(q + 0x06) = *(short*)(s - 0x8);
            *(short*)(q + 0x0a) = *(short*)s;
            *(short*)(q + 0x18) = (short)(*(short*)(s + 0x3a) >> 3);
            *(short*)(q + 0x1e) = (short)(*(short*)(s + 0x46) >> 3);
            *(short*)(q + 0x1a) = (short)(*(short*)(s + 0x3e) >> 3);
            *(short*)(q + 0x1c) = (short)(*(short*)(s + 0x42) >> 3);
            q[0x08] = (byte)s[-2];
            q[0x04] = (byte)s[-10];
            q[0x05] = (byte)s[-9];
            q[0x0d] = (byte)s[7];
            q[0x09] = (byte)s[-1];
            q[0x0c] = (byte)s[6];
            q[0x14] = (byte)s[-18];
            q[0x10] = (byte)s[14];
            q[0x11] = (byte)s[15];
            q[0x15] = (byte)s[-17];
            q[0x16] = (byte)s[-16];
            Ft4SansLumiere_00a09070(q, sommets, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0d990 @ 0x00a0d990 — genre 10 : paquet de 0x24 octets sur la pile, vers <c>FUN_00a0a670</c>.</summary>
    internal static void Adapter_00a0d990(short* p, void* sommets)
    {
        byte* q = stackalloc byte[0x24];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1e + i * 0x5c;
            *(short*)(q + 0x06) = *(short*)(s - 0xc);
            *(short*)(q + 0x0a) = *(short*)s;
            *(short*)(q + 0x1c) = (short)(*(short*)(s + 0x36) >> 3);
            q[0x04] = (byte)s[-14];
            *(short*)(q + 0x1e) = (short)(*(short*)(s + 0x3a) >> 3);
            *(short*)(q + 0x20) = (short)(*(short*)(s + 0x3e) >> 3);
            q[0x09] = (byte)s[-1];
            q[0x05] = (byte)s[-13];
            q[0x08] = (byte)s[-2];
            q[0x10] = (byte)s[-22];
            q[0x0c] = (byte)s[10];
            q[0x0d] = (byte)s[11];
            q[0x14] = (byte)s[-10];
            q[0x11] = (byte)s[-21];
            q[0x12] = (byte)s[-20];
            q[0x18] = (byte)s[2];
            q[0x15] = (byte)s[-9];
            q[0x16] = (byte)s[-8];
            q[0x19] = (byte)s[3];
            q[0x1a] = (byte)s[4];
            Gt3SansLumiere_00a0a670(q, sommets, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0da80 @ 0x00a0da80 — genre 11 : paquet de 0x2c octets sur la pile, vers <c>FUN_00a0b930</c>.</summary>
    internal static void Adapter_00a0da80(short* p, void* sommets)
    {
        byte* q = stackalloc byte[0x2c];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x1e + i * 0x78;
            *(short*)(q + 0x06) = *(short*)(s - 0xc);
            *(short*)(q + 0x0a) = *(short*)s;
            *(short*)(q + 0x24) = (short)(*(short*)(s + 0x4e) >> 3);
            *(short*)(q + 0x2a) = (short)(*(short*)(s + 0x5a) >> 3);
            *(short*)(q + 0x26) = (short)(*(short*)(s + 0x52) >> 3);
            *(short*)(q + 0x28) = (short)(*(short*)(s + 0x56) >> 3);
            q[0x08] = (byte)s[-2];
            q[0x04] = (byte)s[-14];
            q[0x05] = (byte)s[-13];
            q[0x0d] = (byte)s[11];
            q[0x09] = (byte)s[-1];
            q[0x0c] = (byte)s[10];
            q[0x14] = (byte)s[-22];
            q[0x10] = (byte)s[22];
            q[0x11] = (byte)s[23];
            q[0x18] = (byte)s[-10];
            q[0x15] = (byte)s[-21];
            q[0x16] = (byte)s[-20];
            q[0x1c] = (byte)s[2];
            q[0x19] = (byte)s[-9];
            q[0x1a] = (byte)s[-8];
            q[0x20] = (byte)s[14];
            q[0x1d] = (byte)s[3];
            q[0x1e] = (byte)s[4];
            q[0x21] = (byte)s[15];
            q[0x22] = (byte)s[16];
            Gt4SansLumiere_00a0b930(q, sommets, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0dba0 @ 0x00a0dba0 — genres 12 et 13 : paquet de 0x10 octets sur la pile, vers <c>FUN_00a08510</c>.</summary>
    internal static void Adapter_00a0dba0(short* p, void* sommets)
    {
        byte* q = stackalloc byte[0x10];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x38 + i * 0x40;
            *(short*)(q + 0x08) = (short)(*(short*)(s - 0x4) >> 3);
            *(short*)(q + 0x0a) = (short)(*(short*)s >> 3);
            q[0x04] = (byte)s[-48];
            *(short*)(q + 0x0c) = (short)(*(short*)(s + 0x4) >> 3);
            *(short*)(q + 0x0e) = (short)(*(short*)(s + 0x8) >> 3);
            q[0x05] = (byte)s[-47];
            q[0x06] = (byte)s[-46];
            F4SansLumiere_00a08510(q, sommets, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0dc30 @ 0x00a0dc30 — genre 14 : paquet de 0x18 octets sur la pile, vers <c>FUN_00a09e60</c>.</summary>
    internal static void Adapter_00a0dc30(short* p, void* sommets)
    {
        byte* q = stackalloc byte[0x18];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x40 + i * 0x44;
            *(short*)(q + 0x10) = (short)(*(short*)(s - 0x4) >> 3);
            q[0x04] = (byte)s[-56];
            *(short*)(q + 0x12) = (short)(*(short*)s >> 3);
            *(short*)(q + 0x14) = (short)(*(short*)(s + 0x4) >> 3);
            q[0x08] = (byte)s[-48];
            q[0x05] = (byte)s[-55];
            q[0x06] = (byte)s[-54];
            q[0x0c] = (byte)s[-40];
            q[0x09] = (byte)s[-47];
            q[0x0a] = (byte)s[-46];
            q[0x0d] = (byte)s[-39];
            q[0x0e] = (byte)s[-38];
            G3SansLumiere_00a09e60(q, sommets, null);
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0dce0 @ 0x00a0dce0 — genre 15 : paquet de 0x1c octets sur la pile, vers <c>FUN_00a0b000</c>.</summary>
    internal static void Adapter_00a0dce0(short* p, void* sommets)
    {
        byte* q = stackalloc byte[0x1c];
        for (int i = 0; i < *p; i++)
        {
            byte* s = (byte*)p + 0x50 + i * 0x58;
            *(short*)(q + 0x14) = (short)(*(short*)(s - 0x4) >> 3);
            *(short*)(q + 0x16) = (short)(*(short*)s >> 3);
            q[0x04] = (byte)s[-72];
            *(short*)(q + 0x18) = (short)(*(short*)(s + 0x4) >> 3);
            *(short*)(q + 0x1a) = (short)(*(short*)(s + 0x8) >> 3);
            q[0x08] = (byte)s[-64];
            q[0x05] = (byte)s[-71];
            q[0x06] = (byte)s[-70];
            q[0x0c] = (byte)s[-56];
            q[0x09] = (byte)s[-63];
            q[0x0a] = (byte)s[-62];
            q[0x10] = (byte)s[-48];
            q[0x0d] = (byte)s[-55];
            q[0x0e] = (byte)s[-54];
            q[0x11] = (byte)s[-47];
            q[0x12] = (byte)s[-46];
            G4SansLumiere_00a0b000(q, sommets, null);
        }
    }

    // ============================================================================================
    // LA RECONSTRUCTION D'UN OBJET TMD — `FUN_00a0c4f0` (ordinal 755), etape 10, 2026-09-23
    //
    // Le second chemin qui remplit un paquet. `RejouerPaquet` l'appelle quand le mot 1 de l'entree
    // designee par le nœud ne vaut pas -1 : cette entree est alors une **entree de la table d'objets
    // d'un TMD** — le format de modele de la libgs PlayStation (identifiant 0x41, que
    // `FUN_00a06a40` verifie) —, sept mots : sommets, nombre, normales, nombre, primitives,
    // nombre, echelle. Une fois convertie, l'entree devient un descripteur de paquet : mot 0 le
    // paquet, mot 1 a -1, et les deux mots d'origine sont gardes dans l'en-tete du paquet.
    //
    // Le parcours est en DEUX passes. La premiere indexe les primitives (leur taille depend du
    // mode, octet 3, bit 1 — la semi-transparence — masque). La seconde les convertit **en
    // regroupant** : a partir de la premiere non faite, elle balaie tout le reste et convertit
    // chaque primitive qui ne changerait rien a l'etat du tampon (meme etat de rendu, meme couleur
    // plate ou meme page et CLUT). Le premier non fait est toujours converti, donc chaque tour
    // avance ; le cout est quadratique, et le tampon y gagne des enregistrements plus longs.
    //
    // Ghidra en donne un flux illisible (un `goto` vers une etiquette INTERIEURE a la boucle) :
    // le flux ci-dessous est reconstruit sur le desassemblage, `Legacy/Raw/Winsys3d/FUN_00a0c4f0.disasm.txt`,
    // et la table de sauts relue dans le binaire (index a 0x00a0cce0, cibles a 0x00a0cc94) —
    // elle donne exactement le `switch` de Ghidra.
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0c4f0 @ 0x00a0c4f0 (ordinal 755) — convertit en paquet l'objet
    /// TMD que designe le mot 2 de <paramref name="p"/>, `ret 4`. Rien si le mot 1 de l'entree
    /// vaut deja -1.
    /// </summary>
    internal static void ReconstruirePaquet(uint* p)
    {
        uint* objet = (uint*)p[2];
        if ((int)objet[1] == -1) return;

        ChoisirObjetTmd(objet, -1);
        AllouerPaquet(Winsys3dData.TmdNbPrimitives_011be7cc);

        uint* paquet = Winsys3dData.PaquetCourant_011be7ac;
        paquet[0] = objet[0];
        Winsys3dData.PaquetCourant_011be7ac[1] = objet[1];
        objet[1] = 0xffffffff;
        objet[0] = (uint)Winsys3dData.PaquetCourant_011be7ac;

        // Deux tableaux du tas de `wsyskern` (`call [12153E4h]`, l'export `Malloc`) : un drapeau
        // « fait » de 16 bits et l'adresse de chaque primitive. Seul le premier est mis a zero.
        uint n = (uint)Winsys3dData.TmdNbPrimitives_011be7cc;
        ushort* fait = (ushort*)Blaze.Wsyskern.Malloc((int)(n * 2));
        byte** primitives = (byte**)Blaze.Wsyskern.Malloc((int)(n * 4));
        new Span<byte>(fait, (int)(n * 2)).Clear();

        // Premiere passe : l'adresse de chaque primitive. Un mode inconnu n'avance pas le
        // curseur — les suivantes recoivent alors toutes la meme adresse.
        for (Winsys3dData.TmdIndice_011be7d4 = 0;
             (uint)Winsys3dData.TmdIndice_011be7d4 < (uint)Winsys3dData.TmdNbPrimitives_011be7cc;
             Winsys3dData.TmdIndice_011be7d4++)
        {
            primitives[Winsys3dData.TmdIndice_011be7d4] = Winsys3dData.TmdPrimitive_011be7d8;
            int taille = TaillePrimitiveTmd(Winsys3dData.TmdPrimitive_011be7d8[3] & 0xfd);
            if (taille != 0) Winsys3dData.TmdPrimitive_011be7d8 += taille;
        }

        // Seconde passe. `debut` est `esi` : la premiere primitive pas encore faite.
        uint debut = 0;
        do
        {
            Winsys3dData.TmdIndice_011be7d4 = (int)debut;
            for (uint i = debut; i < (uint)Winsys3dData.TmdNbPrimitives_011be7cc; )
            {
                byte* r = primitives[i];
                Winsys3dData.TmdPrimitive_011be7d8 = r;

                if (fait[i] != 0)
                {
                    if (debut == i) debut++;
                }
                else if (ConvertirSiCompatible(r, debut == i))
                {
                    fait[Winsys3dData.TmdIndice_011be7d4] = 1;
                    i = (uint)Winsys3dData.TmdIndice_011be7d4;
                    // Le binaire compare `debut` a `indice - 1`, pas a `indice` (0x00a0cb72) :
                    // la primitive qu'on vient de faire n'avance donc pas `debut` elle-meme ; c'est
                    // l'increment de fin de tour, ou le saut des faites au tour suivant, qui le fait.
                    if (debut == i - 1) debut++;
                }

                i++;
                Winsys3dData.TmdIndice_011be7d4 = (int)i;
            }
            debut++;
        } while (debut < (uint)Winsys3dData.TmdNbPrimitives_011be7cc);

        Blaze.Wsyskern.WsysFree((byte*)fait);
        Blaze.Wsyskern.WsysFree((byte*)primitives);
    }

    /// <summary>
    /// La taille d'une primitive TMD selon son mode (bit 1 masque), telle que la premiere passe de
    /// <see cref="ReconstruirePaquet"/> l'ajoute (table de sauts 0x00a0cc60/0x00a0cc14). 0 pour
    /// un mode inconnu : le curseur n'avance pas.
    /// </summary>
    private static int TaillePrimitiveTmd(int mode) => mode switch
    {
        0x20 or 0x21 or 0x29 or 0x50 => 0x10,
        0x24 or 0x31 or 0x38 => 0x18,
        0x25 or 0x34 or 0x39 => 0x1c,
        0x28 or 0x30 => 0x14,
        0x2c or 0x2d => 0x20,
        0x35 or 0x3c => 0x24,
        0x3d => 0x2c,
        0x40 => 0xc,
        _ => 0,
    };

    /// <summary>
    /// Le `switch` de la seconde passe de <see cref="ReconstruirePaquet"/> : convertit la primitive
    /// si c'est la premiere non faite (<paramref name="premiere"/>) ou si elle ne change pas l'etat
    /// du tampon. Rend vrai si elle a ete convertie. Les producteurs recoivent les sommets et les
    /// normales de l'objet (`DAT_011be7b8`, `DAT_011be7c0`).
    /// </summary>
    private static bool ConvertirSiCompatible(byte* r, bool premiere)
    {
        void* s = Winsys3dData.TmdSommets_011be7b8;
        void* nn = Winsys3dData.TmdNormales_011be7c0;
        int etat = Winsys3dData.EtatRendu_00a2c480;

        switch (r[3] & 0xfd)
        {
        case 0x20: if (!premiere && !MemeCouleurPlate(r, etat, 0)) return false; F3Eclaire_00a078c0(r, s, nn); return true;
        case 0x21: if (!premiere && etat != 4) return false; F3SansLumiere_00a07d80(r, s, nn); return true;
        case 0x24: if (!premiere && !MemeTexture(r, etat, 2)) return false; Ft3Eclaire_00a08850(r, s, nn); return true;
        case 0x25: if (!premiere && !MemeTexture(r, etat, 6)) return false; Ft3SansLumiere_00a08c80(r, s, nn); return true;
        case 0x28: if (!premiere && !MemeCouleurPlate(r, etat, 0)) return false; F4Eclaire_00a08080(r, s, nn); return true;
        case 0x29: if (!premiere && etat != 4) return false; F4SansLumiere_00a08510(r, s, nn); return true;
        case 0x2c: if (!premiere && !MemeTexture(r, etat, 2)) return false; Ft4Eclaire_00a094d0(r, s, nn); return true;
        case 0x2d: if (!premiere && !MemeTexture(r, etat, 6)) return false; Ft4SansLumiere_00a09070(r, s, nn); return true;
        case 0x30: if (!premiere && !MemeCouleurPlate(r, etat, 1)) return false; G3Eclaire_00a09980(r, s, nn); return true;
        case 0x31: if (!premiere && etat != 5) return false; G3SansLumiere_00a09e60(r, s, nn); return true;
        case 0x34: if (!premiere && !MemeTexture(r, etat, 3)) return false; Gt3Eclaire_00a0a1a0(r, s, nn); return true;
        case 0x35: if (!premiere && !MemeTexture(r, etat, 7)) return false; Gt3SansLumiere_00a0a670(r, s, nn); return true;
        case 0x38: if (!premiere && !MemeCouleurPlate(r, etat, 1)) return false; G4Eclaire_00a0aa90(r, s, nn); return true;
        case 0x39: if (!premiere && etat != 5) return false; G4SansLumiere_00a0b000(r, s, nn); return true;
        case 0x3c: if (!premiere && !MemeTexture(r, etat, 3)) return false; Gt4Eclaire_00a0b390(r, s, nn); return true;
        case 0x3d: if (!premiere && !MemeTexture(r, etat, 7)) return false; Gt4SansLumiere_00a0b930(r, s, nn); return true;
        case 0x40: if (!premiere && !MemeCouleurPlate(r, etat, 0)) return false; LigneF2_00a0bdf0(r, s, nn); return true;
        case 0x50: if (!premiere && !MemeCouleurPlate(r, etat, 1)) return false; LigneG2_00a0c110(r, s, nn); return true;
        default: return false;
        }
    }

    /// <summary>Meme etat, et meme couleur que le memo (comparaison en 16 bits : `movzx dx, byte` puis `cmp dx, [memo]`).</summary>
    private static bool MemeCouleurPlate(byte* r, int etat, int attendu)
        => etat == attendu
        && r[4] == Winsys3dData.MemoRouge_011be764
        && r[5] == Winsys3dData.MemoVert_011be768
        && r[6] == Winsys3dData.MemoBleu_011be76c;

    /// <summary>Meme etat, meme CLUT (`+6`) et meme page de texture (`+10`) que les memos.</summary>
    private static bool MemeTexture(byte* r, int etat, int attendu)
        => etat == attendu
        && *(ushort*)(r + 6) == Winsys3dData.Memo_00a2c484
        && *(ushort*)(r + 10) == Winsys3dData.PageTexture_00a2c488;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a06a40 @ 0x00a06a40 (ordinal 678) — choisit l'objet
    /// <paramref name="numero"/> d'un TMD et en pose les sept champs dans `DAT_011be7b0`..`7d8`,
    /// `ret 8`. Un numero negatif prend <paramref name="tmd"/> pour une entree d'objet seule, deja
    /// resolue ; sinon <paramref name="tmd"/> est le fichier, son identifiant doit valoir 0x41
    /// (-1 sinon), et ses pointeurs sont d'abord resolus par <see cref="ResoudreTmd"/>. Rend le
    /// nombre de primitives.
    /// </summary>
    internal static int ChoisirObjetTmd(uint* tmd, int numero)
    {
        int i;
        if (numero < 0)
        {
            Winsys3dData.TmdObjet_011be7b0 = 0;
            Winsys3dData.TmdNbObjets_011be7b4 = 1;
            i = 0;
        }
        else
        {
            if (tmd[0] != 0x41) return -1;
            Winsys3dData.TmdObjet_011be7b0 = numero;
            Winsys3dData.TmdNbObjets_011be7b4 = (int)tmd[2];
            i = numero * 7 + 3;
            ResoudreTmd(tmd + 1);
        }

        Winsys3dData.TmdSommets_011be7b8 = (short*)tmd[i];
        Winsys3dData.TmdNbSommets_011be7bc = (int)tmd[i + 1];
        Winsys3dData.TmdNormales_011be7c0 = (short*)tmd[i + 2];
        Winsys3dData.TmdNbNormales_011be7c4 = (int)tmd[i + 3];
        Winsys3dData.TmdPrimitives_011be7c8 = (byte*)tmd[i + 4];
        Winsys3dData.TmdNbPrimitives_011be7cc = (int)tmd[i + 5];
        Winsys3dData.TmdEchelle_011be7d0 = (int)tmd[i + 6];
        Winsys3dData.TmdIndice_011be7d4 = 0;
        Winsys3dData.TmdPrimitive_011be7d8 = (byte*)tmd[i + 4];
        return (int)tmd[i + 5];
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a1cff0 @ 0x00a1cff0 (ordinal 563) — resout les pointeurs d'un TMD,
    /// une fois, `ret 4`. <paramref name="entete"/> pointe sur le mot de drapeaux : 1 = deja fait.
    /// Sinon les trois decalages de chaque objet (sommets, normales, primitives) deviennent des
    /// adresses, relatives au debut de la table d'objets (`entete + 8`).
    /// </summary>
    internal static void ResoudreTmd(uint* entete)
    {
        if (entete[0] == 1) return;

        entete[0] = 1;
        for (uint k = 0; k < entete[1]; k++)
        {
            uint* objet = entete + 2 + k * 7;
            uint table = (uint)(entete + 2);
            objet[0] += table;
            objet[2] += table;
            objet[4] += table;
        }
    }

    // ============================================================================================
    // LES DIX-HUIT PRODUCTEURS — une primitive TMD -> des sommets Direct3D et une commande
    //
    // Un par mode TMD. Le nom est celui du mode : F plat, G Gouraud, T texture, 3/4 sommets,
    // « Eclaire » quand la primitive porte des normales (sommets `D3DVERTEX`, format 1 : x y z,
    // nx ny nz, u v) et « SansLumiere » quand elle porte ses couleurs (`D3DLVERTEX`, format 2 :
    // x y z, reserve, couleur, speculaire, u v). Tous `ret 0Ch` : (primitive, sommets, normales).
    //
    // Chacun suit le meme squelette : un preambule d'etat (couleur plate, texture deliee, ou page
    // et CLUT), l'ombrage (`SHADEMODE` 1, ou `DAT_00a2c5a4` pour les Gouraud), l'etat de rendu,
    // les sommets, puis la commande de trace (4 liste de triangles, 6 eventail, 2 liste de
    // segments) avec son nombre de sommets. Les triangles ne l'ecrivent que si elle change, et
    // allongent sinon l'enregistrement precedent ; les quadrilateres l'ecrivent toujours.
    //
    // --------------------------------------------------------------------------------------------
    // TRANSCRITS SUR LE DESASSEMBLAGE — deux ecarts systematiques de Ghidra
    //
    // 1. **La couleur est rangee en entier**, pas convertie : Ghidra ecrit
    //    `pf[4] = (float)(r << 16 | g << 8 | b)`, le binaire `mov [ecx+10h],eax` — c'est le DWORD
    //    de couleur du `D3DLVERTEX`, et `(float)` n'est chez Ghidra qu'une reinterpretation.
    //
    // 2. **Les coordonnees de texture sont une division entiere par ~140 millions** — donc
    //    toujours nulles pour un octet. Ghidra replie le calcul et ecrit `0.0` ; le binaire fait
    //    `imul` par une constante magique, `sar`, correction de signe, `fild`, `fstp`. Les
    //    constantes different d'un producteur a l'autre (0x3D2C183D, 0x7A582FCB, 0x1E960961...)
    //    mais donnent toutes un diviseur voisin de 140 422 000. C'est transcrit tel quel, constante
    //    par constante (**LB-042**).
    //
    // Et un defaut du binaire, reproduit : `F4SansLumiere` trace une liste de triangles a quatre
    // sommets — un seul triangle (**LB-041**). Releves sous `Legacy/Raw/Winsys3d/FUN_00a0*.disasm.txt`.
    // ============================================================================================

    /// <summary>
    /// Ecrit un enregistrement `[op][arg0][arg1][0]` au curseur, puis la sentinelle -1 SANS avancer
    /// sur elle, et retient `op`/`arg0` dans les memos `DAT_00a2c578`/`DAT_00a2c57c`. C'est la
    /// sequence que chaque producteur deroule en ligne.
    /// </summary>
    private static void Commande(int op, int arg0, uint arg1)
    {
        Winsys3dData.DerniereCommande_00a2c578 = op;
        Winsys3dData.PaquetCourant_011be7ac[Winsys3dData.Curseur_011be7dc] = (uint)op;
        Winsys3dData.Curseur_011be7dc++;
        Winsys3dData.DerniereCommandeArg_00a2c57c = arg0;
        Winsys3dData.PaquetCourant_011be7ac[Winsys3dData.Curseur_011be7dc] = (uint)arg0;
        Winsys3dData.Curseur_011be7dc++;
        Winsys3dData.PaquetCourant_011be7ac[Winsys3dData.Curseur_011be7dc] = arg1;
        Winsys3dData.Curseur_011be7dc++;
        Winsys3dData.PaquetCourant_011be7ac[Winsys3dData.Curseur_011be7dc] = 0;
        Winsys3dData.Curseur_011be7dc++;
        Winsys3dData.PaquetCourant_011be7ac[Winsys3dData.Curseur_011be7dc] = 0xffffffff;
    }

    /// <summary><see cref="Commande"/>, seulement si `op` ou `arg0` differe des memos.</summary>
    private static void CommandeSiNouvelle(int op, int arg0, uint arg1)
    {
        if (Winsys3dData.DerniereCommande_00a2c578 != op || Winsys3dData.DerniereCommandeArg_00a2c57c != arg0)
            Commande(op, arg0, arg1);
    }

    /// <summary>Ajoute <paramref name="n"/> au dernier mot de l'enregistrement courant — son compte.</summary>
    private static void AjouterAuCompte(int n)
        => Winsys3dData.PaquetCourant_011be7ac[Winsys3dData.Curseur_011be7dc - 1] += (uint)n;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a07ca0 @ 0x00a07ca0 — la meme sequence, en fonction : ecrit
    /// l'enregistrement si `op`/`arg0` changent — ou, pour une commande de trace, si
    /// <paramref name="n"/> vaut 4 —, ajoute <paramref name="n"/> au compte, et avance la zone de
    /// sommets de <paramref name="n"/> sommets pour une commande de trace. `ret 10h`.
    /// </summary>
    internal static void CommandeEtSommets(int op, int arg0, uint arg1, int n)
    {
        if (Winsys3dData.DerniereCommande_00a2c578 != op || Winsys3dData.DerniereCommandeArg_00a2c57c != arg0
            || (op > -1 && n == 4))
            Commande(op, arg0, arg1);
        AjouterAuCompte(n);
        if (op > -1) Winsys3dData.Sommets_011be7a8 += n * 8;
    }

    // --- Les trois preambules -------------------------------------------------------------------

    /// <summary>
    /// La couleur d'une primitive a normales (`+4` r, `+5` g, `+6` b). Si elle vaut le memo, la
    /// texture est deliee (-5) — sauf si elle l'est deja comme primitive plate (page -1). Sinon le
    /// memo change et un enregistrement -3 porte r, g, et b dans son compte ; -3 ne fait rien au
    /// rejeu (`RejouerPaquetCourant`), c'est un trou laisse expres. La page passe a -1.
    /// </summary>
    private static void PoserCouleurPlate(byte* r)
    {
        if (r[4] == Winsys3dData.MemoRouge_011be764 && r[5] == Winsys3dData.MemoVert_011be768
            && r[6] == Winsys3dData.MemoBleu_011be76c)
        {
            if (Winsys3dData.PageTexture_00a2c488 == -1) return;
            CommandeSiNouvelle(-5, 0, 0);
        }
        else
        {
            Winsys3dData.MemoBleu_011be76c = r[6];
            Winsys3dData.MemoVert_011be768 = r[5];
            Winsys3dData.MemoRouge_011be764 = r[4];
            CommandeSiNouvelle(-3, (short)Winsys3dData.MemoRouge_011be764, (uint)(int)(short)Winsys3dData.MemoVert_011be768);
            AjouterAuCompte((short)Winsys3dData.MemoBleu_011be76c);
        }
        Winsys3dData.PageTexture_00a2c488 = -1;
    }

    /// <summary>Une primitive a couleurs sans texture : delie la texture (-5) une fois, page -2.</summary>
    private static void DelierTexture()
    {
        if (Winsys3dData.PageTexture_00a2c488 == -2) return;
        CommandeSiNouvelle(-5, 0, 0);
        Winsys3dData.PageTexture_00a2c488 = -2;
    }

    /// <summary>
    /// Une primitive texturee : si la page (`+10`) ou la CLUT (`+6`) change, les memos suivent et
    /// un enregistrement -4 porte la page, la CLUT dans son compte.
    /// </summary>
    private static void PoserTexture(byte* r)
    {
        int page = *(ushort*)(r + 10);
        int clut = *(ushort*)(r + 6);
        if (page == Winsys3dData.PageTexture_00a2c488 && clut == Winsys3dData.Memo_00a2c484) return;

        Winsys3dData.Memo_00a2c484 = clut;
        Winsys3dData.PageTexture_00a2c488 = page;
        CommandeSiNouvelle(-4, page, 0);
        AjouterAuCompte(clut);
    }

    /// <summary>L'ombrage plat : `SHADEMODE` 1 si l'etat courant a le bit 0 ou 3.</summary>
    private static void OmbragePlat()
    {
        if ((Winsys3dData.EtatRendu_00a2c480 & 9) != 0) CommandeSiNouvelle(-2, 1, 0);
    }

    /// <summary>L'ombrage Gouraud : `SHADEMODE` <c>DAT_00a2c5a4</c> sauf si l'etat est deja Gouraud (bit 0) sans le bit 3.</summary>
    private static void OmbrageGouraud()
    {
        if (((Winsys3dData.EtatRendu_00a2c480 ^ 1) & 9) != 0)
            CommandeSiNouvelle(-2, Winsys3dData.OmbrageGouraud_00a2c5a4, 0);
    }

    // --- Les sommets ----------------------------------------------------------------------------

    /// <summary>x, y, z du sommet numero `*(ushort*)(r + champ)` : quatre `short` par sommet, `fild`.</summary>
    private static void Position(float* v, byte* r, int champ, void* sommets)
    {
        short* s = (short*)sommets + *(ushort*)(r + champ) * 4;
        v[0] = s[0];
        v[1] = s[1];
        v[2] = s[2];
    }

    /// <summary>nx, ny, nz de la normale numero `*(ushort*)(r + champ)`, en 4.12 : `fild` puis `fmul` par 1/4096.</summary>
    private static void Normale(float* v, byte* r, int champ, void* normales)
    {
        short* s = (short*)normales + *(ushort*)(r + champ) * 4;
        v[3] = s[0] * Winsys3dData.EchelleNormale_00a2a0b4;
        v[4] = s[1] * Winsys3dData.EchelleNormale_00a2a0b4;
        v[5] = s[2] * Winsys3dData.EchelleNormale_00a2a0b4;
    }

    /// <summary>La couleur `0x00RRGGBB` des trois octets a <paramref name="champ"/>, rangee en ENTIER dans le DWORD de couleur.</summary>
    private static void Couleur(float* v, byte* r, int champ)
        => *(uint*)(v + 4) = (uint)((r[champ] * 0x100 + r[champ + 1]) * 0x100 + r[champ + 2]);

    /// <summary>
    /// La division entiere signee que le compilateur a faite en multiplication : `imul` par
    /// <paramref name="m"/>, `sar edx,` <paramref name="s"/>, puis le bit de signe ajoute.
    /// Toujours 0 pour un octet (**LB-042**).
    /// </summary>
    private static float QuotientMagique(byte x, int m, int s)
    {
        int q = (int)(((long)m * x) >> 32) >> s;
        return q + (int)((uint)q >> 31);
    }

    /// <summary>u, v d'un sommet : les octets <paramref name="champ"/> et suivant, par les deux divisions du producteur.</summary>
    private static void CoordTexture(float* v, byte* r, int champ, int mu, int su, int mv, int sv)
    {
        v[6] = QuotientMagique(r[champ], mu, su);
        v[7] = QuotientMagique(r[champ + 1], mv, sv);
    }

    /// <summary>Met u, v a zero, `mov` d'un registre nul.</summary>
    private static void SansTexture(float* v)
    {
        v[6] = 0;
        v[7] = 0;
    }

    /// <summary>La fin commune : <paramref name="n"/> sommets au compte, et la zone de sommets avance d'autant.</summary>
    private static void Terminer(int n)
    {
        AjouterAuCompte(n);
        Winsys3dData.Sommets_011be7a8 += n * 8;
    }

    // --- Les triangles plats --------------------------------------------------------------------

    /// <summary>Ghidra: winsys3d.c FUN_00a078c0 @ 0x00a078c0 — F3 eclaire : `+4` rgb, `+8` normale, `+10/12/14` sommets.</summary>
    internal static void F3Eclaire_00a078c0(byte* r, void* sommets, void* normales)
    {
        PoserCouleurPlate(r);
        if ((Winsys3dData.EtatRendu_00a2c480 & 9) != 0) CommandeEtSommets(-2, 1, 0, 0);
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 0;
        Position(v, r, 10, sommets);
        Position(v + 8, r, 12, sommets);
        Position(v + 16, r, 14, sommets);
        Normale(v, r, 8, normales);
        Normale(v + 8, r, 8, normales);
        Normale(v + 16, r, 8, normales);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16);
        CommandeSiNouvelle(4, 1, (uint)v);
        Terminer(3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a07d80 @ 0x00a07d80 — F3 sans lumiere : `+4` rgb, `+8/10/12` sommets.</summary>
    internal static void F3SansLumiere_00a07d80(byte* r, void* sommets, void* normales)
    {
        DelierTexture();
        OmbragePlat();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 4;
        Position(v, r, 8, sommets);
        Position(v + 8, r, 10, sommets);
        Position(v + 16, r, 12, sommets);
        Couleur(v, r, 4); Couleur(v + 8, r, 4); Couleur(v + 16, r, 4);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16);
        CommandeSiNouvelle(4, 2, (uint)v);
        Terminer(3);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a08850 @ 0x00a08850 — FT3 eclaire : `+4/5`, `+8/9`, `+0xc/d` u v,
    /// `+6` CLUT, `+10` page, `+0x10` normale, `+0x12/14/16` sommets.
    /// </summary>
    internal static void Ft3Eclaire_00a08850(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbragePlat();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 2;
        Position(v, r, 0x12, sommets);
        Position(v + 8, r, 0x14, sommets);
        Position(v + 16, r, 0x16, sommets);
        Normale(v, r, 0x10, normales);
        Normale(v + 8, r, 0x10, normales);
        Normale(v + 16, r, 0x10, normales);
        CoordTexture(v, r, 4, 0x3D2C183D, 0x19, 0x7A582FCB, 0x1A);
        CoordTexture(v + 8, r, 8, 0x3D2C183D, 0x19, 0x7A582FCB, 0x1A);
        CoordTexture(v + 16, r, 0xc, 0x3D2C183D, 0x19, 0x7A582FCB, 0x1A);
        CommandeSiNouvelle(4, 1, (uint)v);
        Terminer(3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a08c80 @ 0x00a08c80 — FT3 sans lumiere : u v, CLUT, page, `+0x10` rgb, `+0x14/16/18` sommets.</summary>
    internal static void Ft3SansLumiere_00a08c80(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbragePlat();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 6;
        Position(v, r, 0x14, sommets);
        Position(v + 8, r, 0x16, sommets);
        Position(v + 16, r, 0x18, sommets);
        Couleur(v, r, 0x10); Couleur(v + 8, r, 0x10); Couleur(v + 16, r, 0x10);
        CoordTexture(v, r, 4, 0x7A582AFF, 0x1A, 0x7A582A4F, 0x1A);
        CoordTexture(v + 8, r, 8, 0x7A582AFF, 0x1A, 0x7A582A4F, 0x1A);
        CoordTexture(v + 16, r, 0xc, 0x7A582AFF, 0x1A, 0x7A582A4F, 0x1A);
        CommandeSiNouvelle(4, 2, (uint)v);
        Terminer(3);
    }

    // --- Les quadrilateres plats — eventail 0, 1, 3, 2 -------------------------------------------

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a08080 @ 0x00a08080 — F4 eclaire : `+4` rgb, `+8` normale,
    /// `+10/12/14/16` sommets. Ghidra omet les memos de l'enregistrement -2 : ecrits ou non, la
    /// commande 6 qui suit les recouvre.
    /// </summary>
    internal static void F4Eclaire_00a08080(byte* r, void* sommets, void* normales)
    {
        PoserCouleurPlate(r);
        OmbragePlat();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 0;
        Position(v, r, 10, sommets);
        Position(v + 8, r, 12, sommets);
        Position(v + 16, r, 0x10, sommets);
        Position(v + 24, r, 0xe, sommets);
        Normale(v, r, 8, normales);
        Normale(v + 8, r, 8, normales);
        Normale(v + 16, r, 8, normales);
        Normale(v + 24, r, 8, normales);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16); SansTexture(v + 24);
        Commande(6, 1, (uint)v);
        Terminer(4);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a08510 @ 0x00a08510 — F4 sans lumiere : `+4` rgb, `+8/10/12/14`
    /// sommets. **Trace une LISTE de triangles (4) a quatre sommets**, soit un seul triangle —
    /// `mov [0A2C578h],edi` avec `edi` = 4 (**LB-041**).
    /// </summary>
    internal static void F4SansLumiere_00a08510(byte* r, void* sommets, void* normales)
    {
        DelierTexture();
        OmbragePlat();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 4;
        Position(v, r, 8, sommets);
        Position(v + 8, r, 10, sommets);
        Position(v + 16, r, 14, sommets);
        Position(v + 24, r, 12, sommets);
        Couleur(v, r, 4); Couleur(v + 8, r, 4); Couleur(v + 16, r, 4); Couleur(v + 24, r, 4);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16); SansTexture(v + 24);
        Commande(4, 2, (uint)v);
        Terminer(4);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a094d0 @ 0x00a094d0 — FT4 eclaire : u v a `+4`, `+8`, `+0xc`,
    /// `+0x10`, CLUT, page, `+0x14` normale, `+0x16/18/1a/1c` sommets.
    /// </summary>
    internal static void Ft4Eclaire_00a094d0(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbragePlat();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 2;
        Position(v, r, 0x16, sommets);
        Position(v + 8, r, 0x18, sommets);
        Position(v + 16, r, 0x1c, sommets);
        Position(v + 24, r, 0x1a, sommets);
        Normale(v, r, 0x14, normales);
        Normale(v + 8, r, 0x14, normales);
        Normale(v + 16, r, 0x14, normales);
        Normale(v + 24, r, 0x14, normales);
        CoordTexture(v, r, 4, 0x7A581E6F, 0x1A, 0x01E96077, 0x14);
        CoordTexture(v + 8, r, 8, 0x7A581E6F, 0x1A, 0x01E96077, 0x14);
        CoordTexture(v + 16, r, 0x10, 0x7A581E6F, 0x1A, 0x01E96077, 0x14);
        CoordTexture(v + 24, r, 0xc, 0x7A581E6F, 0x1A, 0x01E96077, 0x14);
        Commande(6, 1, (uint)v);
        Terminer(4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a09070 @ 0x00a09070 — FT4 sans lumiere : u v, CLUT, page, `+0x14` rgb, `+0x18/1a/1c/1e` sommets.</summary>
    internal static void Ft4SansLumiere_00a09070(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbragePlat();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 6;
        Position(v, r, 0x18, sommets);
        Position(v + 8, r, 0x1a, sommets);
        Position(v + 16, r, 0x1e, sommets);
        Position(v + 24, r, 0x1c, sommets);
        Couleur(v, r, 0x14); Couleur(v + 8, r, 0x14); Couleur(v + 16, r, 0x14); Couleur(v + 24, r, 0x14);
        CoordTexture(v, r, 4, 0x1E960961, 0x18, 0x1E960935, 0x18);
        CoordTexture(v + 8, r, 8, 0x1E960961, 0x18, 0x1E960935, 0x18);
        CoordTexture(v + 16, r, 0x10, 0x1E960961, 0x18, 0x1E960935, 0x18);
        CoordTexture(v + 24, r, 0xc, 0x1E960961, 0x18, 0x1E960935, 0x18);
        Commande(6, 2, (uint)v);
        Terminer(4);
    }

    // --- Les triangles Gouraud ------------------------------------------------------------------

    /// <summary>Ghidra: winsys3d.c FUN_00a09980 @ 0x00a09980 — G3 eclaire : `+4` rgb, normale/sommet a `+8/10`, `+0xc/e`, `+0x10/12`.</summary>
    internal static void G3Eclaire_00a09980(byte* r, void* sommets, void* normales)
    {
        PoserCouleurPlate(r);
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 1;
        Position(v, r, 10, sommets);
        Position(v + 8, r, 0xe, sommets);
        Position(v + 16, r, 0x12, sommets);
        Normale(v, r, 8, normales);
        Normale(v + 8, r, 0xc, normales);
        Normale(v + 16, r, 0x10, normales);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16);
        CommandeSiNouvelle(4, 1, (uint)v);
        Terminer(3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a09e60 @ 0x00a09e60 — G3 sans lumiere : rgb a `+4`, `+8`, `+0xc`, `+0x10/12/14` sommets.</summary>
    internal static void G3SansLumiere_00a09e60(byte* r, void* sommets, void* normales)
    {
        DelierTexture();
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 5;
        Position(v, r, 0x10, sommets);
        Position(v + 8, r, 0x12, sommets);
        Position(v + 16, r, 0x14, sommets);
        Couleur(v, r, 4); Couleur(v + 8, r, 8); Couleur(v + 16, r, 0xc);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16);
        CommandeSiNouvelle(4, 2, (uint)v);
        Terminer(3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0a1a0 @ 0x00a0a1a0 — GT3 eclaire : u v, CLUT, page, normale/sommet a `+0x10/12`, `+0x14/16`, `+0x18/1a`.</summary>
    internal static void Gt3Eclaire_00a0a1a0(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 3;
        Position(v, r, 0x12, sommets);
        Position(v + 8, r, 0x16, sommets);
        Position(v + 16, r, 0x1a, sommets);
        Normale(v, r, 0x10, normales);
        Normale(v + 8, r, 0x14, normales);
        Normale(v + 16, r, 0x18, normales);
        CoordTexture(v, r, 4, 0x7A58054F, 0x1A, 0x03D2C025, 0x15);
        CoordTexture(v + 8, r, 8, 0x7A58054F, 0x1A, 0x03D2C025, 0x15);
        CoordTexture(v + 16, r, 0xc, 0x7A58054F, 0x1A, 0x03D2C025, 0x15);
        CommandeSiNouvelle(4, 1, (uint)v);
        Terminer(3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0a670 @ 0x00a0a670 — GT3 sans lumiere : u v, CLUT, page, rgb a `+0x10`, `+0x14`, `+0x18`, `+0x1c/1e/20` sommets.</summary>
    internal static void Gt3SansLumiere_00a0a670(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 7;
        Position(v, r, 0x1c, sommets);
        Position(v + 8, r, 0x1e, sommets);
        Position(v + 16, r, 0x20, sommets);
        Couleur(v, r, 0x10); Couleur(v + 8, r, 0x14); Couleur(v + 16, r, 0x18);
        CoordTexture(v, r, 4, 0x1E95FFF5, 0x18, 0x7A57FF25, 0x1A);
        CoordTexture(v + 8, r, 8, 0x1E95FFF5, 0x18, 0x7A57FF25, 0x1A);
        CoordTexture(v + 16, r, 0xc, 0x1E95FFF5, 0x18, 0x7A57FF25, 0x1A);
        CommandeSiNouvelle(4, 2, (uint)v);
        Terminer(3);
    }

    // --- Les quadrilateres Gouraud — eventail 0, 1, 3, 2 -----------------------------------------

    /// <summary>Ghidra: winsys3d.c FUN_00a0aa90 @ 0x00a0aa90 — G4 eclaire : `+4` rgb, normale/sommet a `+8/10`, `+0xc/e`, `+0x10/12`, `+0x14/16`.</summary>
    internal static void G4Eclaire_00a0aa90(byte* r, void* sommets, void* normales)
    {
        PoserCouleurPlate(r);
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 1;
        Position(v, r, 10, sommets);
        Position(v + 8, r, 0xe, sommets);
        Position(v + 16, r, 0x16, sommets);
        Position(v + 24, r, 0x12, sommets);
        Normale(v, r, 8, normales);
        Normale(v + 8, r, 0xc, normales);
        Normale(v + 16, r, 0x14, normales);
        Normale(v + 24, r, 0x10, normales);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16); SansTexture(v + 24);
        Commande(6, 1, (uint)v);
        Terminer(4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0b000 @ 0x00a0b000 — G4 sans lumiere : rgb a `+4`, `+8`, `+0xc`, `+0x10`, `+0x14/16/18/1a` sommets.</summary>
    internal static void G4SansLumiere_00a0b000(byte* r, void* sommets, void* normales)
    {
        DelierTexture();
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 5;
        Position(v, r, 0x14, sommets);
        Position(v + 8, r, 0x16, sommets);
        Position(v + 16, r, 0x1a, sommets);
        Position(v + 24, r, 0x18, sommets);
        Couleur(v, r, 4); Couleur(v + 8, r, 8); Couleur(v + 16, r, 0x10); Couleur(v + 24, r, 0xc);
        SansTexture(v); SansTexture(v + 8); SansTexture(v + 16); SansTexture(v + 24);
        Commande(6, 2, (uint)v);
        Terminer(4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0b390 @ 0x00a0b390 — GT4 eclaire : u v, CLUT, page, normale/sommet a `+0x14/16`, `+0x18/1a`, `+0x1c/1e`, `+0x20/22`.</summary>
    internal static void Gt4Eclaire_00a0b390(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 3;
        Position(v, r, 0x16, sommets);
        Position(v + 8, r, 0x1a, sommets);
        Position(v + 16, r, 0x22, sommets);
        Position(v + 24, r, 0x1e, sommets);
        Normale(v, r, 0x14, normales);
        Normale(v + 8, r, 0x18, normales);
        Normale(v + 16, r, 0x20, normales);
        Normale(v + 24, r, 0x1c, normales);
        CoordTexture(v, r, 4, 0x0F4AFC53, 0x17, 0x7A57E1E9, 0x1A);
        CoordTexture(v + 8, r, 8, 0x0F4AFC53, 0x17, 0x7A57E1E9, 0x1A);
        CoordTexture(v + 16, r, 0x10, 0x0F4AFC53, 0x17, 0x7A57E1E9, 0x1A);
        CoordTexture(v + 24, r, 0xc, 0x0F4AFC53, 0x17, 0x7A57E1E9, 0x1A);
        Commande(6, 1, (uint)v);
        Terminer(4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0b930 @ 0x00a0b930 — GT4 sans lumiere : u v, CLUT, page, rgb a `+0x14`, `+0x18`, `+0x1c`, `+0x20`, `+0x24/26/28/2a` sommets.</summary>
    internal static void Gt4SansLumiere_00a0b930(byte* r, void* sommets, void* normales)
    {
        PoserTexture(r);
        OmbrageGouraud();
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 7;
        Position(v, r, 0x24, sommets);
        Position(v + 8, r, 0x26, sommets);
        Position(v + 16, r, 0x2a, sommets);
        Position(v + 24, r, 0x28, sommets);
        Couleur(v, r, 0x14); Couleur(v + 8, r, 0x18); Couleur(v + 16, r, 0x20); Couleur(v + 24, r, 0x1c);
        CoordTexture(v, r, 4, 0x7A57DD1D, 0x1A, 0x3D2BEE37, 0x19);
        CoordTexture(v + 8, r, 8, 0x7A57DD1D, 0x1A, 0x3D2BEE37, 0x19);
        CoordTexture(v + 16, r, 0x10, 0x7A57DD1D, 0x1A, 0x3D2BEE37, 0x19);
        CoordTexture(v + 24, r, 0xc, 0x7A57DD1D, 0x1A, 0x3D2BEE37, 0x19);
        Commande(6, 2, (uint)v);
        Terminer(4);
    }

    // --- Les segments ---------------------------------------------------------------------------

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0bdf0 @ 0x00a0bdf0 — LF2, segment plat : `+4` rgb, `+8/10`
    /// sommets. Sommets `D3DVERTEX` sans normale lue : nx, ny, u, v a zero, **nz laisse tel quel**.
    /// </summary>
    internal static void LigneF2_00a0bdf0(byte* r, void* sommets, void* normales)
    {
        PoserCouleurPlate(r);
        if ((Winsys3dData.EtatRendu_00a2c480 & 9) != 0) CommandeEtSommets(-2, 1, 0, 0);
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 0;
        Position(v, r, 8, sommets);
        Position(v + 8, r, 10, sommets);
        v[3] = 0; v[4] = 0; v[11] = 0; v[12] = 0;
        SansTexture(v); SansTexture(v + 8);
        CommandeSiNouvelle(2, 1, (uint)v);
        Terminer(2);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0c110 @ 0x00a0c110 — LG2, segment Gouraud : `+4` rgb, `+0xc/e` sommets ; meme remplissage que LF2.</summary>
    internal static void LigneG2_00a0c110(byte* r, void* sommets, void* normales)
    {
        PoserCouleurPlate(r);
        if (((Winsys3dData.EtatRendu_00a2c480 ^ 1) & 9) != 0)
            CommandeEtSommets(-2, Winsys3dData.OmbrageGouraud_00a2c5a4, 0, 0);
        float* v = Winsys3dData.Sommets_011be7a8;
        Winsys3dData.EtatRendu_00a2c480 = 1;
        Position(v, r, 0xc, sommets);
        Position(v + 8, r, 0xe, sommets);
        v[3] = 0; v[4] = 0; v[11] = 0; v[12] = 0;
        SansTexture(v); SansTexture(v + 8);
        CommandeSiNouvelle(2, 1, (uint)v);
        Terminer(2);
    }
}
