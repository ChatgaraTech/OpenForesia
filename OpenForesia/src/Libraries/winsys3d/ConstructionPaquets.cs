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
// Les producteurs eux-memes (huit corps : `07d80`, `08510`, `08c80`, `09070`, `09e60`, `0a670`,
// `0b000`, `0b930`) restent natifs jusqu'a l'etape 10 — `Natif.cs`, `ret 0Ch`, trois arguments.
//
// --------------------------------------------------------------------------------------------
// UN ECART ASSUME : LES CHAMPS DU PAQUET QUE L'ADAPTATEUR N'ECRIT PAS
//
// Le paquet de primitive est une variable de pile ; chaque adaptateur n'en ecrit qu'une partie
// (jamais le mot d'en-tete, rarement l'octet de code). L'original y laisse ce que la pile contenait
// — puis, d'un enregistrement a l'autre, ce que le tour precedent y avait mis. Le C# l'alloue une
// fois par appel (`stackalloc`, donc a zero) et le garde d'un tour a l'autre, comme l'original. Si
// un producteur lisait un champ jamais ecrit, il lirait 0 ici et n'importe quoi dans l'original :
// a verifier a l'etape 10, producteur par producteur.
// ============================================================================================

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
            Winsys3dNatif.Fun_00a08c80(q, s + 0x2a, 0);
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
            Winsys3dNatif.Fun_00a09070(q, s + 0x3a, 0);
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
            Winsys3dNatif.Fun_00a0a670(q, s + 0x36, 0);
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
            Winsys3dNatif.Fun_00a0b930(q, s + 0x4e, 0);
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
            Winsys3dNatif.Fun_00a07d80(q, s + 0x23, 0);
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
            Winsys3dNatif.Fun_00a08510(q, s + 0x2b, 0);
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
            Winsys3dNatif.Fun_00a09e60(q, s + 0x33, 0);
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
            Winsys3dNatif.Fun_00a0b000(q, s + 0x43, 0);
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
            Winsys3dNatif.Fun_00a08c80(q, sommets, 0);
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
            Winsys3dNatif.Fun_00a09070(q, sommets, 0);
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
            Winsys3dNatif.Fun_00a0a670(q, sommets, 0);
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
            Winsys3dNatif.Fun_00a0b930(q, sommets, 0);
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
            Winsys3dNatif.Fun_00a08510(q, sommets, 0);
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
            Winsys3dNatif.Fun_00a09e60(q, sommets, 0);
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
            Winsys3dNatif.Fun_00a0b000(q, sommets, 0);
        }
    }
}
