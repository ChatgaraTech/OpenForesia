// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur** — `Legacy/Raw/Winsys3d/FUN_00a26f20.disasm.txt` pour le plus gros.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE CACHE DE TEXTURES — la premiere passe de `DrawOTag` (2026-09-23)
//
// La PlayStation lit ses textures dans sa VRAM ; Direct3D veut des surfaces de texture. Entre les
// deux, `winsys3d` tient un cache de 32 pages, et chaque image passe trois fois dessus avant le
// moindre triangle :
//
//     OuvrirReleve       FUN_00a26490   vide les demandes de l'image precedente
//     ReleverNoeud       FUN_00a26b20   sur chaque nœud de l'OT : note les pages que ses
//                                       primitives texturees vont lire
//     SatisfaireDemandes FUN_00a26f20   charge ce qui manque, en evincant les pages les plus
//                                       anciennes tant que la place manque
//
// Ces trois-ci sont portes depuis l'etape 6, et **leurs appelees depuis l'etape 7** — releves par
// genre de primitive, enregistrement des demandes, emplacements, eviction, liaison : voir la
// section « ETAPE 7 » plus bas. Depuis l'etape 8, le chargement lui-meme est dans
// `ChargementTextures.cs` et le `qsort` de la CRT est transcrit ici : **plus rien du cache de
// textures n'est natif**.
//
// --------------------------------------------------------------------------------------------
// CE QUE GHIDRA NE DONNE PAS
//
// **Trois `__thiscall` caches.** `FUN_00a27920`, `FUN_00a27850` et `FUN_00a27820` recoivent tous
// les trois `DAT_012141d4` dans `ecx` (0x00a26f6b, 0x00a26f92, 0x00a270b7), comme
// `FUN_00a27880`. Ghidra n'en montre le `this` a aucun site d'appel.
//
// **Le tri est un `cdecl`** avec une fonction de comparaison passee par adresse
// (`push 0A27100h`, puis `add esp,10h` apres le `call`, 0x00a27067).
//
// **La liste d'eviction deborde sans garde.** Elle fait 1 000 mots — 500 entrees de 8 octets — et
// le releve y range TOUTES les pages plus vieilles que l'image courante, jusqu'a 32 x 2 000. Au-dela
// de 500, les ecritures tombent sur la table de 32 000 mots qui la suit exactement
// (`0x011f3e18 + 0xfa0 = 0x011f4db8`), celle que `OuvrirReleve` remet a -1. Les globales etant
// aliasees sur la memoire de la DLL, le C# ecrit aux memes adresses : le debordement est reproduit
// tel quel, ni plus ni moins.
//
// **La table de sauts du releve** a ete relue dans le binaire (indices en octets a 0x00a26d14,
// cibles a 0x00a26ca4) plutot que prise a Ghidra : les groupes de codes concordent exactement.
// ============================================================================================

using System;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dCacheTextures
{
    private const int TailleDemande = 0x18;
    private const int TailleEntreeEviction = 8;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a26490 @ 0x00a26490 — ouvre le releve de l'image : initialise
    /// une fois les quatre octets de <c>DAT_00a2c654</c> (tant que le premier vaut 0xfe), remet la
    /// table de releve a -1 (32 000 mots), vide les demandes (3 000 mots), et prend la page de
    /// texture courante des sprites.
    /// </summary>
    internal static void OuvrirReleve()
    {
        byte* quatre = Winsys3dData.Quatre_00a2c654;
        if (quatre[0] == 0xfe)
            for (int i = 0; i < 4; i++) quatre[i] = (byte)Winsys3dPrimitives.ModeMelangeDisponible(i);

        uint* table = Winsys3dData.TableReleve_011f4db8;
        for (int i = 0; i < 32000; i++) table[i] = 0xffffffff;

        Winsys3dData.NombreDemandes_012141b8 = 0;
        uint* demandes = (uint*)Winsys3dData.Demandes_011f0f38;
        for (int i = 0; i < 3000; i++) demandes[i] = 0;

        Winsys3dData.PageReleve_00a2c650 = Winsys3dData.Demi_011be6ec;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a26b20 @ 0x00a26b20 — deroule le paquet d'un nœud comme le
    /// repartiteur (`CheminAffichage.RejouerNoeud`), mais ne dessine rien : pour chaque primitive
    /// TEXTUREE, il appelle la fonction de releve de son genre avec le bit de semi-transparence
    /// (<c>code &amp; 2</c>). Les non texturees sont sautees de leur taille ; 0xe1 et 0xe2 mettent a
    /// jour la page courante et le pavage **du releve** ; 0xe5 releve le sprite tourne puis sort ;
    /// tout autre code sort.
    ///
    /// Comme dans le repartiteur, les codes 0..3 et 0xf1..0xf8 (sauf 0xf2) ne font que
    /// decompter un mot **sans avancer** : le meme code est relu au tour suivant.
    /// </summary>
    internal static void ReleverNoeud(uint* p)
    {
        int restant = (int)(*p >> 24);
        uint code = ((byte*)p)[7];
        if (code > 0xf8) return;

        while (true)
        {
            uint semi = code & 2;
            switch (code)
            {
            case 0x00: case 0x01: case 0x02: case 0x03:
            case 0xf1: case 0xf3: case 0xf4: case 0xf5: case 0xf6: case 0xf7: case 0xf8:
                restant -= 1; break;

            case 0x20: case 0x21: case 0x22: case 0x23:
            case 0x50: case 0x51: case 0x52: case 0x53: case 0x54: case 0x55: case 0x56: case 0x57:
                p += 4; restant -= 4; break;
            case 0x24: case 0x25: case 0x26: case 0x27:
                RelevePolyFT3((byte*)p, semi); p += 7; restant -= 7; break;
            case 0x28: case 0x29: case 0x2a: case 0x2b:
            case 0x48: case 0x49: case 0x4a: case 0x4b:
                p += 5; restant -= 5; break;
            case 0x2c: case 0x2d: case 0x2e: case 0x2f:
                RelevePolyFT4((byte*)p, semi); p += 9; restant -= 9; break;
            case 0x30: case 0x31: case 0x32: case 0x33:
            case 0x4c: case 0x4d: case 0x4e: case 0x4f:
                p += 6; restant -= 6; break;
            case 0x34: case 0x35: case 0x36: case 0x37:
                RelevePolyGT3((byte*)p, semi); p += 9; restant -= 9; break;
            case 0x38: case 0x39: case 0x3a: case 0x3b:
                p += 8; restant -= 8; break;
            case 0x3c: case 0x3d: case 0x3e: case 0x3f:
                RelevePolyGT4((byte*)p, semi); p += 0xc; restant -= 0xc; break;
            case 0x40: case 0x41: case 0x42: case 0x43: case 0x44: case 0x45: case 0x46: case 0x47:
            case 0x60: case 0x61: case 0x62: case 0x63:
                p += 3; restant -= 3; break;
            case 0x58: case 0x59: case 0x5a: case 0x5b:
                p += 7; restant -= 7; break;
            case 0x5c: case 0x5d: case 0x5e: case 0x5f:
                p += 9; restant -= 9; break;
            case 0x64: case 0x65: case 0x66: case 0x67:
                ReleveSprt((byte*)p, semi); p += 4; restant -= 4; break;
            case 0x68: case 0x69: case 0x6a: case 0x6b: case 0x6c: case 0x6d: case 0x6e: case 0x6f:
            case 0x70: case 0x71: case 0x72: case 0x73:
            case 0x78: case 0x79: case 0x7a: case 0x7b:
                p += 2; restant -= 2; break;
            case 0x74: case 0x75: case 0x76: case 0x77:
                ReleveSprt8((byte*)p, semi); p += 3; restant -= 3; break;
            case 0x7c: case 0x7d: case 0x7e: case 0x7f:
                ReleveSprt16((byte*)p, semi); p += 3; restant -= 3; break;

            case 0xe1:
                Winsys3dData.PageReleve_00a2c650 = (int)(p[1] & 0xffffff);
                p += 1; restant -= 1; break;

            case 0xe2:
                Winsys3dData.PavageReleve_012141cc = (int)(p[1] & 0xffffff);
                if (Winsys3dData.PavageReleve_012141cc != 0)
                {
                    int v = Winsys3dData.PavageReleve_012141cc;
                    Winsys3dData.PavageReleve_012141c4 = v & 0x1f;
                    Winsys3dData.PavageReleve_012141c8 = v >> 5 & 0x1f;
                    Winsys3dData.PavageReleve_012141bc = v >> 10 & 0x1f;
                    Winsys3dData.PavageReleve_012141c0 = v >> 15 & 0x1f;
                }
                p += 1; restant -= 1; break;

            case 0xe5:
                ReleveSpriteTourne((byte*)p);
                return;

            default:
                return;
            }

            if (restant < 1) return;
            code = ((byte*)p)[7];
            if (code > 0xf8) return;
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a26f20 @ 0x00a26f20 — satisfait les demandes relevees.
    ///
    /// Tour apres tour : chaque demande encore ouverte (genre different de 0xff) est preparee
    /// (<c>FUN_00a26e10</c>), puis proposee a deux placements successifs (<c>FUN_00a27920</c>,
    /// <c>FUN_00a27850</c>). Si l'un reussit, la demande est close (genre 0xff) ; si les deux
    /// echouent, sa surface s'ajoute au manque et elle reste ouverte. S'il reste un manque, toutes
    /// les pages plus anciennes que l'image courante sont relevees dans la liste d'eviction,
    /// triees (<c>FUN_00a27e00</c>), et evincees une a une (<c>FUN_00a27820</c>) jusqu'a couvrir
    /// le manque — puis on recommence. On s'arrete quand un tour n'a rien laisse en souffrance, ou
    /// qu'il n'y a plus rien a evincer.
    ///
    /// **Le manque n'est jamais remis a zero entre deux tours** : il est pose a 0 a l'entree, puis
    /// seulement augmente par les echecs et diminue par les evictions (<c>[esp+10h]</c>).
    /// </summary>
    internal static void SatisfaireDemandes()
    {
        int manque = 0;
        bool enSouffrance;
        do
        {
            enSouffrance = false;
            for (int i = 0; i < Winsys3dData.NombreDemandes_012141b8; i++)
            {
                byte* d = Winsys3dData.Demandes_011f0f38 + i * TailleDemande;
                if (d[0x10] == 0xff) continue;

                bool close = true;
                ArrondirDemande((int*)d);

                // Les deux champs sont etendus en signe PUIS combines : `movsx edx,word [esi+10h]`,
                // `movsx eax,byte [esi+0Dh]`, `or edx,eax` (0x00a26f80..0x00a26f8f).
                int mot14 = *(short*)(d + 0x14);
                int octet11 = (sbyte)d[0x11];
                if (DemandeCouverte(Winsys3dData.Objet_012141d4, d,
                        octet11, *(ushort*)(d + 0x12), (sbyte)d[0x10]) == 0
                    && PlacerDemande(Winsys3dData.Objet_012141d4, d,
                        mot14 | octet11, *(ushort*)(d + 0x12), (sbyte)d[0x10]) == 0)
                {
                    close = false;
                    manque += (*(int*)(d + 8) - *(int*)d) * (*(int*)(d + 0x0c) - *(int*)(d + 4));
                    enSouffrance = true;
                }
                if (close) d[0x10] = 0xff;
            }

            if (!enSouffrance) return;

            // La liste d'eviction : toutes les pages plus anciennes que l'image courante.
            uint* liste = (uint*)Winsys3dData.ListeEviction_011f3e18;
            for (int i = 0; i < 1000; i++) liste[i] = 0;

            int nombre = 0;
            byte* entree = Winsys3dData.ListeEviction_011f3e18;
            for (int page = 0; page < 0x20; page++)
            {
                byte** emplacements = *(byte***)((byte*)Winsys3dData.Objet_012141d4 + (page & 0x1f) * 4);
                for (int k = 1; k < 0x7d1; k++)                    // octets 4 .. 0x1f40, pas de 4
                {
                    uint date = *(uint*)(emplacements[k] + 0x28);
                    if (date < (uint)Winsys3dData.CompteurImages_011be75c)
                    {
                        *(short*)(entree + 0) = (short)(k - 1);
                        *(short*)(entree + 2) = (short)page;
                        *(uint*)(entree + 4) = date;
                        nombre++;
                        entree += TailleEntreeEviction;
                    }
                }
            }

            if (nombre == 0) return;

            Trier(Winsys3dData.ListeEviction_011f3e18, (uint)nombre, TailleEntreeEviction);

            byte* e = Winsys3dData.ListeEviction_011f3e18;
            for (int n = 0; n < nombre; n++, e += TailleEntreeEviction)
            {
                if (manque < 1) break;
                int page = *(short*)(e + 2);
                int emplacement = *(short*)(e + 0);
                int* objet = *(int**)(*(byte**)((byte*)Winsys3dData.Objet_012141d4 + (page & 0x1f) * 4)
                                      + 4 + emplacement * 4);
                int surface = objet[0] == 0 ? 0 : (objet[5] - objet[3]) * (objet[4] - objet[2]);
                manque -= surface;
                Evincer(Winsys3dData.Objet_012141d4, page, emplacement);
            }
        } while (true);
    }

    // ============================================================================================
    // ETAPE 7 (2026-09-23) — l'interieur du cache : releve, emplacements, liaison
    //
    // LE MODELE, tel que les corps le manipulent (aucune structure nommee dans Ghidra) :
    //
    //     le cache      (`DAT_012141d4`)  +0x00      32 pointeurs de PAGE, un par page PS
    //                                     +0x80      65 536 mots : CLUT -> numero d'emplacement
    //                                     +0x20080   2 000 mots : emplacement -> CLUT (-1 = libre)
    //     une page                         [0]        nombre d'emplacements occupes
    //                                     [k + 1]    pointeur d'EMPLACEMENT k, ou l'emplacement vide
    //     un emplacement (0x34, `Malloc`)  [0] objet texture du device   [1] CLUT
    //                                     [2..5] rectangle charge (RECT)
    //                                     [6], [7] tables uv des sprites  [8], [9] des polygones
    //                                     [10] date (image du dernier emploi)  [11] ?
    //
    // L'allocation passe par `Malloc`/`Free` de `wsyskern` — les exports eux-memes (la table
    // d'import de winsys3d nomme `Malloc` en 0x012153e4 et `Free` en 0x012153e0), pas la passerelle
    // de `Libraries/wsyskern`, qui ajoute un `OutOfMemory` que l'original n'a pas : lui teste le nul
    // et continue. UN SEUL TAS : du natif libere encore ce que ce fichier alloue.
    //
    // RESTENT NATIFS : le chargement lui-meme (`FUN_00a271e0`, etape 8), et le tri `qsort` de la
    // CRT (`FUN_00a27e00`) avec sa comparaison — le remplacer par `Array.Sort` changerait l'ordre
    // des dates egales, donc l'ordre d'eviction.
    // ============================================================================================

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int UnionRect(int* dst, int* src1, int* src2);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SubtractRect(int* dst, int* src1, int* src2);

    private const int Device_LierTexture = 0x64 / 4;

    // ---- les releves par genre de primitive ----------------------------------------------------
    //
    // Chacun calcule le rectangle de texture que la primitive lit — le plus petit et le plus grand
    // u, le plus petit et le plus grand v — et l'enregistre. La cascade de comparaisons du binaire
    // (verifiee sur `FUN_00a26670`) rend exactement le minimum et le maximum ; elle est ecrite ici
    // comme tels.

    /// <summary>Ghidra: winsys3d.c FUN_00a26670 @ 0x00a26670 — releve `POLY_FT3` (uv 0xc, 0x14, 0x1c ; page 0x16). `ret 8`.</summary>
    internal static void RelevePolyFT3(byte* p, uint semi)
        => ReleverUv(p, 0x16, semi, 0x0c, 0x14, 0x1c);

    /// <summary>Ghidra: winsys3d.c FUN_00a267d0 @ 0x00a267d0 — releve `POLY_FT4` (uv 0xc, 0x14, 0x1c, 0x24 ; page 0x16). `ret 8`.</summary>
    internal static void RelevePolyFT4(byte* p, uint semi)
        => ReleverUv(p, 0x16, semi, 0x0c, 0x14, 0x1c, 0x24);

    /// <summary>Ghidra: winsys3d.c FUN_00a26720 @ 0x00a26720 — releve `POLY_GT3` (uv 0xc, 0x18, 0x24 ; page 0x1a). `ret 8`.</summary>
    internal static void RelevePolyGT3(byte* p, uint semi)
        => ReleverUv(p, 0x1a, semi, 0x0c, 0x18, 0x24);

    /// <summary>Ghidra: winsys3d.c FUN_00a268c0 @ 0x00a268c0 — releve `POLY_GT4` (uv 0xc, 0x18, 0x24, 0x30 ; page 0x1a). `ret 8`.</summary>
    internal static void RelevePolyGT4(byte* p, uint semi)
        => ReleverUv(p, 0x1a, semi, 0x0c, 0x18, 0x24, 0x30);

    private static void ReleverUv(byte* p, int octetPage, uint semi, int a, int b, int c)
    {
        int uMin = p[a], uMax = p[a], vMin = p[a + 1], vMax = p[a + 1];
        Etendre(p, b, ref uMin, ref uMax, ref vMin, ref vMax);
        Etendre(p, c, ref uMin, ref uMax, ref vMin, ref vMax);
        EnregistrerDemande(*(ushort*)(p + octetPage), *(ushort*)(p + 0x0e), uMin, vMin, uMax, vMax, (int)semi);
    }

    private static void ReleverUv(byte* p, int octetPage, uint semi, int a, int b, int c, int d)
    {
        int uMin = p[a], uMax = p[a], vMin = p[a + 1], vMax = p[a + 1];
        Etendre(p, b, ref uMin, ref uMax, ref vMin, ref vMax);
        Etendre(p, c, ref uMin, ref uMax, ref vMin, ref vMax);
        Etendre(p, d, ref uMin, ref uMax, ref vMin, ref vMax);
        EnregistrerDemande(*(ushort*)(p + octetPage), *(ushort*)(p + 0x0e), uMin, vMin, uMax, vMax, (int)semi);
    }

    private static void Etendre(byte* p, int o, ref int uMin, ref int uMax, ref int vMin, ref int vMax)
    {
        int u = p[o], v = p[o + 1];
        if (u < uMin) uMin = u; else if (u > uMax) uMax = u;
        if (v < vMin) vMin = v; else if (v > vMax) vMax = v;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a269b0 @ 0x00a269b0 — releve `SPRT` : de (u, v) a (u + l - 1,
    /// v + h - 1), la taille etant celle du paquet (shorts 0x10/0x12, etendus en signe) ou, si le
    /// pavage du releve est actif, la moitie de la tuile (`sar esi,1`). Page : celle du releve.
    /// </summary>
    internal static void ReleveSprt(byte* p, uint semi)
    {
        int u = p[0x0c], v = p[0x0d];
        int bas, droite;
        if (Winsys3dData.PavageReleve_012141cc == 0)
        {
            bas = *(short*)(p + 0x12) - 1 + v;
            droite = *(short*)(p + 0x10) - 1 + u;
        }
        else
        {
            bas = (Winsys3dData.PavageReleve_012141c8 >> 1) - 1 + v;
            droite = (Winsys3dData.PavageReleve_012141c4 >> 1) - 1 + u;
        }
        EnregistrerDemande((uint)Winsys3dData.PageReleve_00a2c650, *(ushort*)(p + 0x0e), u, v, droite, bas, (int)semi);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a26a30 @ 0x00a26a30 — releve `SPRT_8`.</summary>
    internal static void ReleveSprt8(byte* p, uint semi)
        => EnregistrerDemande((uint)Winsys3dData.PageReleve_00a2c650, *(ushort*)(p + 0x0e),
                              p[0x0c], p[0x0d], p[0x0c] + 7, p[0x0d] + 7, (int)semi);

    /// <summary>Ghidra: winsys3d.c FUN_00a26a70 @ 0x00a26a70 — releve `SPRT_16` (`lea esi,[ecx+0Fh]`).</summary>
    internal static void ReleveSprt16(byte* p, uint semi)
        => EnregistrerDemande((uint)Winsys3dData.PageReleve_00a2c650, *(ushort*)(p + 0x0e),
                              p[0x0c], p[0x0d], p[0x0c] + 0x0f, p[0x0d] + 0x0f, (int)semi);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a26ab0 @ 0x00a26ab0 — releve du sprite tourne (opcode 0xe5), avec
    /// les memes gardes que `Primitives.SpriteTourne` et la meme CLUT composee. Jamais
    /// semi-transparent (`push 0`).
    /// </summary>
    internal static void ReleveSpriteTourne(byte* p)
    {
        ushort largeur = *(ushort*)(p + 0x08), hauteur = *(ushort*)(p + 0x0a);
        if (largeur == 0 || hauteur == 0 || *(uint*)p >= 0x80000000) return;

        int u = p[0x0e], v = p[0x0f];
        int clut = (*(short*)(p + 0x10) >> 5) + (*(short*)(p + 0x12) << 6);
        EnregistrerDemande(*(ushort*)(p + 0x0c), (uint)clut, u, v, largeur + u - 1, hauteur + v - 1, 0);
    }

    // ---- l'enregistrement d'une demande ---------------------------------------------------------

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a264f0 @ 0x00a264f0 — `ret 1Ch`. **Ghidra ne donne pas la branche
    /// semi-transparente** (« bad instruction data », la table de sauts de 0x00a26650 non lue) :
    /// transcrite sur `Legacy/Raw/Winsys3d/FUN_00a264f0.disasm.txt`.
    ///
    /// Le GENRE de la demande : 0 si opaque ; sinon selon <c>DAT_00a2c654[abr]</c> (octet signe),
    /// par la table relue dans le binaire — 0 -> 1, 1 -> 0, 2 -> 2 avec la CLUT marquee
    /// <c>| 0x8000</c>, 3 -> 1, 4 -> 0, au-dela de 4 -> **rien n'est enregistre**. Le genre est
    /// range dans l'emplacement du septieme argument (`[esp+34h]`), d'ou le parametre reutilise.
    ///
    /// Une meme (page, emplacement de CLUT) n'a qu'une demande : la premiere la cree, les suivantes
    /// **etendent son rectangle** (`UnionRect`) et **cumulent le genre par OU** — Ghidra ecrit
    /// `x = x`, le binaire `or cl,al` (0x00a2662f) — et, si la page n'a pas le bit 0x1000, le mot
    /// +0x14 perd tout sauf 0x180.
    /// </summary>
    internal static void EnregistrerDemande(uint page, uint clut, int gauche, int haut, int droite, int bas, int semi)
    {
        int genre = 0;
        if (semi != 0)
        {
            int m = (sbyte)Winsys3dData.Quatre_00a2c654[(int)page >> 5 & 3];
            if ((uint)m > 4) return;
            switch (m)
            {
            case 0: case 3: genre = 1; break;
            case 1: case 4: genre = 0; break;
            case 2: genre = 2; clut |= 0x8000; break;
            }
        }

        int emplacement = EmplacementDeClut(Winsys3dData.Objet_012141d4, clut);
        short* releve = (short*)Winsys3dData.TableReleve_011f4db8 + (int)(page & 0x1f) * 2000 + emplacement;

        if (*releve == -1)
        {
            int nombre = Winsys3dData.NombreDemandes_012141b8;
            *releve = (short)nombre;
            Winsys3dData.NombreDemandes_012141b8 = nombre + 1;
            byte* d = Winsys3dData.Demandes_011f0f38 + (short)nombre * TailleDemande;
            *(int*)(d + 0x04) = haut;
            *(int*)(d + 0x00) = gauche;
            *(int*)(d + 0x0c) = bas;
            *(int*)(d + 0x08) = droite;
            d[0x10] = (byte)genre;
            d[0x11] = (byte)(page & 0x1f);
            *(ushort*)(d + 0x12) = (ushort)clut;
            *(ushort*)(d + 0x14) = (ushort)(page & 0x1180);
            return;
        }

        byte* e = Winsys3dData.Demandes_011f0f38 + *releve * TailleDemande;
        int* rect = stackalloc int[4] { gauche, haut, droite, bas };
        UnionRect((int*)e, rect, (int*)e);
        e[0x10] |= (byte)genre;
        if ((page & 0x1000) == 0) *(ushort*)(e + 0x14) &= 0x180;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a278c0 @ 0x00a278c0 — **`__thiscall`** sur le cache : l'emplacement
    /// de la CLUT, en lui en attribuant un libre au premier emploi. **Quand les 1 999 sont pris,
    /// rend 1999** (`mov eax,7CFh`, 0x00a278f5) — Ghidra ne montre aucune valeur de retour sur ce
    /// chemin.
    /// </summary>
    internal static int EmplacementDeClut(void* cache, uint clut)
    {
        short* parClut = (short*)((byte*)cache + 0x80);
        short* parEmplacement = (short*)((byte*)cache + 0x20080);
        if (parClut[clut] != 0) return parClut[clut];

        for (int k = 1; k < 2000; k++)
        {
            if (parEmplacement[k] == -1)
            {
                parEmplacement[k] = (short)clut;
                parClut[clut] = (short)k;
                return k;
            }
        }
        return 0x7cf;
    }

    // ---- la satisfaction : preparer, chercher, placer, evincer ----------------------------------

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a26e10 @ 0x00a26e10 — arrondit une demande a une taille de
    /// texture : le coin est aligne sur 32, la largeur et la hauteur (plafonnees a 255) deviennent
    /// 32, 64, 128 ou 256 selon leurs trois bits 5-7 (table relue a 0x00a26ef8 : 0 -> 32, 1 -> 64,
    /// 2 et 3 -> 128, au-dela -> 256). Si <c>DAT_00b2d180</c> est pose, un carre du plus grand
    /// cote. Enfin le rectangle est repousse pour ne pas deborder de 256.
    /// </summary>
    internal static void ArrondirDemande(int* d)
    {
        int x0 = d[0] & unchecked((int)0xffffffe0);
        int y0 = d[1] & unchecked((int)0xffffffe0);
        d[1] = y0;
        int largeur = d[2] - x0;
        int hauteur = d[3] - y0;
        d[0] = x0;
        if (largeur > 0xff) largeur = 0xff;
        if (hauteur > 0xff) hauteur = 0xff;

        int l = TailleTexture(largeur >> 5 & 7);
        int h = TailleTexture(hauteur >> 5 & 7);
        if (Winsys3dData.TexturesCarrees_00b2d180 != 0)
        {
            if (l > h) h = l;
            l = h;
        }

        if (h + y0 > 0x100) d[1] = 0x100 - h;
        if (l + x0 > 0x100) d[0] = 0x100 - l;
        d[2] = l + d[0];
        d[3] = h + d[1];
    }

    private static int TailleTexture(int bits) => bits switch
    {
        0 => 0x20,
        1 => 0x40,
        2 or 3 => 0x80,
        _ => 0x100,
    };

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27920 @ 0x00a27920 — **`__thiscall`** sur le cache, `ret 10h`
    /// (le quatrieme argument n'est pas lu). La demande est-elle deja couverte par l'emplacement de
    /// sa CLUT dans sa page ? L'emplacement est au passage date de l'image courante, sauf s'il est
    /// l'emplacement vide. Rend 1 si `SubtractRect` laisse un rectangle vide.
    /// </summary>
    internal static int DemandeCouverte(void* cache, byte* demande, int page, uint clut, int genre)
    {
        int s = *(short*)((byte*)cache + 0x80 + clut * 2);
        if (s == 0) return 0;

        int* e = (int*)Page(cache, page)[s + 1];
        if (e != Winsys3dData.EmplacementVide_012141d0) e[10] = Winsys3dData.CompteurImages_011be75c;

        int* reste = stackalloc int[4];
        return SubtractRect(reste, (int*)demande, e + 2) == 0 ? 1 : 0;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27850 @ 0x00a27850 — **`__thiscall`** sur le cache, `ret 10h` :
    /// place la demande dans un emplacement neuf de sa page. Ghidra confond `this` et les
    /// arguments ; le binaire passe a <see cref="PlacerDansPage"/> la PAGE (`this[page &amp; 0x1f]`)
    /// et cinq arguments.
    /// </summary>
    internal static int PlacerDemande(void* cache, byte* demande, int page, uint clut, int genre)
    {
        int s = EmplacementDeClut(cache, clut);
        return PlacerDansPage(Page(cache, page), demande, page, clut, s, genre);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a275e0 @ 0x00a275e0 — **`__thiscall`** sur une page, `ret 14h` :
    /// libere l'emplacement, en alloue un neuf (`Malloc(0x34)`, construit par
    /// <see cref="ConstruireEmplacement"/> — ou nul si le tas refuse), et y charge la texture
    /// (<c>FUN_00a271e0</c>, encore natif). Si le chargement echoue, l'emplacement est libere.
    /// </summary>
    internal static int PlacerDansPage(int* page, byte* demande, int numeroPage, uint clut, int s, int genre)
    {
        LibererEmplacement(page, s);
        int* e = (int*)Blaze.Wsyskern.Malloc(0x34);
        if (e != null) e = ConstruireEmplacement(e);
        page[s + 1] = (int)e;
        page[0]++;

        int r = Winsys3dChargementTextures.ChargerTexture(e, (int*)demande, (uint)numeroPage, clut, genre);
        if (r == 0) LibererEmplacement(page, s);
        return r;
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a27140 @ 0x00a27140 — `__fastcall` : met un emplacement a son etat initial, et le rend.</summary>
    internal static int* ConstruireEmplacement(int* e)
    {
        e[0] = 0;
        RemettreAZero(e);
        return e;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27180 @ 0x00a27180 — `__fastcall` : detruit l'objet texture d'un
    /// emplacement (son destructeur virtuel, `vtable[0](1)`) et remet les champs a leur etat
    /// initial. **Le pointeur `[0]` n'est pas remis a zero** — l'appelant libere l'emplacement
    /// aussitot. Sans effet sur l'emplacement vide.
    /// </summary>
    internal static void ViderEmplacement(int* e)
    {
        if (e == Winsys3dData.EmplacementVide_012141d0) return;

        void* texture = (void*)e[0];
        if (texture != null)
            ((delegate* unmanaged[Thiscall]<void*, int, void>)(*(void***)texture)[0])(texture, 1);
        RemettreAZero(e);
    }

    /// <summary>Les champs communs a <see cref="ConstruireEmplacement"/> et <see cref="ViderEmplacement"/>.</summary>
    private static void RemettreAZero(int* e)
    {
        e[1] = -1;
        e[5] = 0; e[4] = 0; e[3] = 0; e[2] = 0;
        e[7] = (int)Winsys3dData.TablesUvSpriteDefaut_011c4cd0;
        e[6] = (int)Winsys3dData.TablesUvSpriteDefaut_011c4cd0;
        e[11] = 0;
        e[9] = (int)Winsys3dData.TablesUvPolyDefaut_011be868;
        e[8] = (int)Winsys3dData.TablesUvPolyDefaut_011be868;
        e[10] = 0x7fffffff;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a275a0 @ 0x00a275a0 — **`__thiscall`** sur une page, `ret 4` :
    /// libere l'emplacement <paramref name="s"/> (vide + `Free`) et le remplace par l'emplacement
    /// vide. Rien s'il l'etait deja.
    /// </summary>
    internal static void LibererEmplacement(int* page, int s)
    {
        int* e = (int*)page[s + 1];
        if (e == Winsys3dData.EmplacementVide_012141d0) return;
        if (e != null)
        {
            ViderEmplacement(e);
            Blaze.Wsyskern.WsysFree((byte*)e);
        }
        page[s + 1] = (int)Winsys3dData.EmplacementVide_012141d0;
        page[0]--;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27820 @ 0x00a27820 — **`__thiscall`** sur le cache, `ret 8` :
    /// delie la texture courante, puis libere l'emplacement s'il n'est pas le numero 0.
    /// </summary>
    internal static void Evincer(void* cache, int page, int s)
    {
        DelierTexture();
        if (s != 0) LibererEmplacement(Page(cache, page), s);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27120 @ 0x00a27120 — delie la texture du device et oublie la
    /// page et la CLUT posees. Appele avec le cache dans `ecx`, mais recharge le device.
    /// </summary>
    internal static void DelierTexture()
    {
        ((delegate* unmanaged[Thiscall]<void*, void*, void>)
            Winsys3dData.DeviceVtable[Device_LierTexture])(Winsys3dData.Device_011be760, null);
        Winsys3dData.PageTexture_00a2c488 = -1;
        Winsys3dData.Memo_00a2c484 = -1;
    }

    // ---- la liaison : ce que les primitives appellent -------------------------------------------

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27880 @ 0x00a27880 — **`__thiscall`** sur le cache, `ret 10h` :
    /// lie la texture de (page, CLUT) si elle a un emplacement. Rend 0 sinon. Remplace l'appel par
    /// adresse de `Natif.cs` depuis l'etape 7.
    /// </summary>
    internal static int LierTexture(void* cache, uint page, uint clut, int mode, int genre)
    {
        int s = *(short*)((byte*)cache + 0x80 + clut * 2);
        if (s == 0) return 0;
        return LierEmplacement((int*)Page(cache, (int)page)[s + 1], page, mode, genre);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27430 @ 0x00a27430 — **`__thiscall`** sur un emplacement, `ret 0Ch`.
    ///
    /// Rien sans objet texture. Si <paramref name="mode"/> est non nul : la page doit avoir un
    /// melange disponible (sinon 0), et le mode retenu est <c>ModeMelangeDisponible(mode)</c> —
    /// le binaire passe bien le MODE demande, pas la page (`push ebp`, 0x00a27464). Filtre
    /// bilineaire sauf genre non nul, ou page 8 bits (0x1000) avec `Zoom8BitTextures`. Memos poses,
    /// melange (opaque/alpha selon le bit 13 si aucun mode, sinon celui du mode `abr`), texture
    /// liee, et les quatre tables uv de l'emplacement deviennent les tables courantes. Si le device
    /// refuse la texture : deliee, 0. Sinon l'emplacement est date : 1.
    /// </summary>
    internal static int LierEmplacement(int* e, uint page, int mode, int genre)
    {
        if (e[0] == 0) return 0;

        int retenu = 0;
        if (mode != 0)
        {
            if (Winsys3dPrimitives.MelangeDePage(page) == -1) return 0;
            retenu = Winsys3dPrimitives.ModeMelangeDisponible(mode);
        }

        Blaze.Winsys3d.SetTextureFilterMode(
            genre == 0 && ((page & 0x1000) == 0 || Winsys3dData.Zoom8BitTextures_011be750 == 0) ? 1 : 0);

        Winsys3dData.PageTexture_00a2c488 = (int)page;
        Winsys3dData.Memo_00a2c484 = e[1];
        Winsys3dData.Memo_00a2c48c = genre;

        if (retenu == 0) Winsys3dPrimitives.ChoisirMelange((int)(page >> 13 & 1));
        else Winsys3dPrimitives.ChoisirMelangePsx(page);

        int hr = ((delegate* unmanaged[Thiscall]<void*, void*, int>)
            Winsys3dData.DeviceVtable[Device_LierTexture])(Winsys3dData.Device_011be760, (void*)e[0]);
        Winsys3dData.TexSpriteU_011cb1c0 = (uint*)e[6];
        Winsys3dData.TexSpriteV_011cb1c4 = (uint*)e[7];
        Winsys3dData.TexPolyU_011cb1c8 = (uint*)e[8];
        Winsys3dData.TexPolyV_011cb1cc = (uint*)e[9];
        if (hr < 0)
        {
            ((delegate* unmanaged[Thiscall]<void*, void*, void>)
                Winsys3dData.DeviceVtable[Device_LierTexture])(Winsys3dData.Device_011be760, null);
            return 0;
        }
        e[10] = Winsys3dData.CompteurImages_011be75c;
        return 1;
    }

    // ---- le tri de la liste d'eviction : le `qsort` de la CRT, transcrit (etape 8) ---------------
    //
    // `FUN_00a27e00` est le `qsort` de la CRT de Visual C++ lie statiquement : seuil de 8 sous lequel
    // il passe au tri court `FUN_00a27fb0`, pivot au milieu echange en tete, pile explicite de 30,
    // echange octet par octet `FUN_00a28010`. Il est TRANSCRIT et non remplace par `Array.Sort` :
    // l'ordre relatif des dates egales en sort, et c'est lui qui decide quelles pages sont evincees.

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27e00 @ 0x00a27e00 — `qsort(base, nombre, taille, comparaison)`,
    /// `cdecl`. La comparaison est ici celle de la liste d'eviction (<see cref="ComparerDates"/>) ;
    /// l'original la recoit par adresse (`push 0A27100h`).
    /// </summary>
    internal static void Trier(byte* @base, uint nombre, uint taille)
    {
        if (nombre < 2 || taille == 0) return;

        byte** pileBas = stackalloc byte*[30];
        byte** pileHaut = stackalloc byte*[30];
        int pile = 0;
        byte* lo = @base;
        byte* hi = @base + taille * (nombre - 1);

        while (true)
        {
            uint n = (uint)(hi - lo) / taille + 1;
            if (n <= 8)
            {
                TriCourt(lo, hi, taille);
            }
            else
            {
                Echanger(lo + n / 2 * taille, lo, taille);
                byte* gauche = lo;
                byte* droite = hi + taille;
                while (true)
                {
                    do gauche += taille; while (gauche <= hi && ComparerDates(gauche, lo) <= 0);
                    do droite -= taille; while (droite > lo && ComparerDates(droite, lo) >= 0);
                    if (droite < gauche) break;
                    Echanger(gauche, droite, taille);
                }
                Echanger(lo, droite, taille);

                if ((int)(droite - lo - 1) < (int)(hi - gauche))
                {
                    if (gauche < hi) { pileBas[pile] = gauche; pileHaut[pile] = hi; pile++; }
                    if (lo + taille < droite) { hi = droite - taille; continue; }
                }
                else
                {
                    if (lo + taille < droite) { pileBas[pile] = lo; pileHaut[pile] = droite - taille; pile++; }
                    if (gauche < hi) { lo = gauche; continue; }
                }
            }

            if (--pile < 0) return;
            lo = pileBas[pile];
            hi = pileHaut[pile];
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a27fb0 @ 0x00a27fb0 — le tri court : le plus grand au bout, et on recommence.</summary>
    private static void TriCourt(byte* lo, byte* hi, uint taille)
    {
        while (hi > lo)
        {
            byte* max = lo;
            for (byte* p = lo + taille; p <= hi; p += taille)
                if (ComparerDates(p, max) > 0) max = p;
            Echanger(max, hi, taille);
            hi -= taille;
        }
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a28010 @ 0x00a28010 — echange deux elements octet par octet, rien s'ils sont le meme.</summary>
    private static void Echanger(byte* a, byte* b, uint taille)
    {
        if (a == b) return;
        for (; taille != 0; taille--)
        {
            byte t = *a;
            *a++ = *b;
            *b++ = t;
        }
    }

    /// <summary>
    /// LAB_00a27100 — la comparaison de la liste d'eviction, que Ghidra n'a pas decompilee :
    /// `mov eax,[eax+4] ; sub eax,[ecx+4] ; ret`, la difference des dates, en entier signe.
    /// </summary>
    private static int ComparerDates(byte* a, byte* b) => (int)(*(uint*)(a + 4) - *(uint*)(b + 4));

    /// <summary>La page <paramref name="page"/> &amp; 0x1f du cache : son tableau d'emplacements.</summary>
    private static int* Page(void* cache, int page) => *(int**)((byte*)cache + (page & 0x1f) * 4);

    // ---- l'invalidation : ce que la VRAM et le jeu appellent (`Vram.cs`, 785, 788) ------------
    //
    // Trois `__thiscall` sur le cache, relevees au desassembleur (`Legacy/Raw/Winsys3d/
    // FUN_00a27700`, `27730`, `27770`) : Ghidra les donne sans `this`, `27700` meme sans argument.

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27700 @ 0x00a27700 — **`__thiscall`** sur le cache, `ret 4` :
    /// libere les 2 000 emplacements de la page <paramref name="page"/> &amp; 0x1f — chacun avec la
    /// PAGE pour `this` (`mov ecx,edi`), pas le cache.
    /// </summary>
    internal static void ViderPage(void* cache, int page)
    {
        int* p = Page(cache, page);
        for (int s = 0; s < 2000; s++) LibererEmplacement(p, s);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27730 @ 0x00a27730 — **`__thiscall`** sur le cache, `ret` : delie
    /// la texture, vide les 32 pages, remet a zero la table des CLUT (0x8000 mots a `+0x80`) et a
    /// -1 les 1 000 mots qui la suivent (`+0x20080`).
    /// </summary>
    internal static void Reinitialiser(void* cache)
    {
        DelierTexture();
        for (int page = 0; page < 0x20; page++) ViderPage(cache, page);
        new Span<uint>((byte*)cache + 0x80, 0x8000).Clear();
        new Span<uint>((byte*)cache + 0x20080, 1000).Fill(0xffffffff);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a27770 @ 0x00a27770 — **`__thiscall`** sur le cache, `ret 8` :
    /// libere, dans chacune des 32 pages, l'emplacement que la table des CLUT (`short` a `+0x80`)
    /// associe a chaque tranche a partir de <paramref name="debut"/>.
    ///
    /// **La boucle avance sa BORNE, pas son indice** (`inc eax` sur la fin, `debut` fixe, a
    /// 0x00a277d0) : la condition `debut &lt; fin` reste vraie, et seule une entree nulle de la table
    /// arrete la marche. Le corps libere donc tout ce qui suit jusqu'au premier trou, au-dela des
    /// `taille >> 4` tranches demandees — de l'eviction en trop, invisible a l'ecran. Transcrit tel
    /// quel.
    /// </summary>
    internal static void LibererParClut(void* cache, int debut, int taille)
    {
        DelierTexture();
        int fin = (taille >> 4) + debut;
        if (debut >= fin) return;

        short* entree = (short*)((byte*)cache + 0x80) + debut;
        do
        {
            int s = *entree;
            if (s == 0) return;
            for (int page = 0; page < 0x20; page++) LibererEmplacement(*(int**)((byte*)cache + page * 4), s);
            entree++;
            fin++;
        } while (debut < fin);
    }

    /// <summary>Ghidra: winsys3d.c ReInitializeAllTextures @ 0x00a27980 (ordinal 785) — `Reinitialiser` sur l'objet cache.</summary>
    internal static void ReInitializeAllTextures() => Reinitialiser(Winsys3dData.Objet_012141d4);

    /// <summary>Ghidra: winsys3d.c ReleaseTexture @ 0x00a27990 (ordinal 788) — `ViderPage` sur l'objet cache.</summary>
    internal static void ReleaseTexture(int page) => ViderPage(Winsys3dData.Objet_012141d4, page);
}
