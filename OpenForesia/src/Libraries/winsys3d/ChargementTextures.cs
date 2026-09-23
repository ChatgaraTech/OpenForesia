// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **transcrite sur le desassemblage** : `Legacy/Raw/Winsys3d/FUN_00a271e0.disasm.txt`,
// `FUN_00a03460.disasm.txt`, `FUN_00a03980.disasm.txt`.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE CHARGEMENT DES TEXTURES — de la VRAM PlayStation a un objet texture du device (2026-09-23)
//
// Quand le cache (`CacheTextures.cs`) place une demande dans un emplacement neuf, il appelle
// `ChargerTexture` : lire le rectangle demande dans la VRAM emulee (1 024 x 512 mots, 0x800 octets
// par ligne), le convertir, et en faire un objet texture par `device[0x10]()`. Deux conversions :
//
//     ConvertirEnDib     FUN_00a03460   une image DIB (BITMAPINFO) : 4 ou 8 bits avec palette de
//                                       256 couleurs, ou 24 bits ; lignes a l'ENVERS (DIB montante).
//                                       Pour les devices de moins de 16 bits, et les pages 8 bits.
//     ConvertirDirect    FUN_00a03980   16 bits bruts, par une table de 65 536 couleurs (1-5-5-5 ou
//                                       4-4-4-4 selon la table) ; lignes a l'endroit.
//
// Les adresses dans la VRAM : la page donne l'origine (x = (page & 0xf) * 64 mots, y = (page &
// 0x10) * 16), la profondeur ((page >> 7) & 3 : 4, 8 ou 16 bits) et la CLUT son emplacement
// (x = (clut & 0x3f) * 16 mots, y = (clut >> 6) & 0x1ff).
//
// L'objet texture est a `SyntheticDevice`/au device : ses methodes sont appelees par sa vtable,
// en `__thiscall` — [0](1) le detruit, [2](bmi, bits) charge une DIB, [3](l, h, bits, format)
// charge des pixels bruts, [6](cle) pose une couleur-cle, [7]() la dit utilisable.
//
// --------------------------------------------------------------------------------------------
// TRANSCRIT TEL QUEL — et ce que le binaire dit que Ghidra ne dit pas
//
// **LB-040 : dans `ConvertirEnDib`, un pixel 16 bits semi-transparent noircit la fin de sa ligne.**
// Quand la CLUT porte 0x8000 (le mode soustractif), un pixel dont le bit 15 est pose est ecrit
// noir — par un MOT de 32 bits sur un pixel de 24 (`mov dword [edi],0`, 0x00a0367b), qui deborde
// d'un octet — et **la source n'avance pas** : seul le chemin opaque fait `add esi,2`. Le pixel
// suivant relit donc le meme, et toute la fin de la ligne sort noire.
//
// **Aucun des deux corps ne teste l'objet que rend `device[0x10]`** avant d'en lire la vtable
// (`mov edi,[esi]`) : un device qui refuserait ferait planter l'original comme la transcription.
//
// **La couleur-cle d'une page 16 bits est 0**, pas « aucune » : `xor ebx,ebx` (0x00a03910). Elle
// est donc posee sur tout device de moins de 16 bits. `ConvertirDirect` pose, lui, une cle `{0, 0}`
// construite sur la pile — l'argument que Ghidra laisse en `&stack0xfffffdc0`.
//
// **Un champ non ecrit dans l'original, mis a zero ici** : `dwFourCC` du format de pixel de
// `ConvertirDirect` (`[esp+28h]`, jamais ecrit entre 0x00a039b6 et 0x00a03a67) garde ce que la
// pile contenait. Le C# y met 0 (`stackalloc`) ; sans effet pour un format `DDPF_RGB`, qui ne le
// lit pas. La palette de `ConvertirEnDib`, elle, est bien remise a zero par l'original
// (`rep stosd`, 0x00a03831).
// ============================================================================================

using System;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dChargementTextures
{
    private const int LigneVram = 0x800;
    private const int Device_CreerTexture = 0x10 / 4;

    private const int Texture_Detruire = 0;
    private const int Texture_ChargerDib = 2;
    private const int Texture_ChargerBrut = 3;
    private const int Texture_PoserCle = 6;
    private const int Texture_CleUtilisable = 7;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a271e0 @ 0x00a271e0 — **`__thiscall`** sur un emplacement du cache,
    /// `ret 10h`. Charge la demande dans l'emplacement et rend 1, ou 0.
    ///
    /// Refus d'entree : CLUT -1 sur une page a palette. Le genre 1 retombe au genre 0 si
    /// <c>DAT_00b2d188</c> est nul. Puis la conversion selon le genre : 0 -> DIB si le device a moins
    /// de 16 bits ou la page le bit 0x1000, sinon 16 bits 1-5-5-5 ; 1 -> DIB sous 16 bits, sinon 16
    /// bits par la table du genre 1 ; 2 -> 16 bits par la table du genre 2 ; **tout autre genre ne
    /// convertit rien** et l'emplacement garde un objet nul — donc 0.
    ///
    /// L'emplacement recoit ensuite la CLUT, le rectangle, et ses quatre tables de coordonnees de
    /// texture — choisies par la taille (32, 64, 128) et la position de la page, voir
    /// <see cref="TableUv"/> —, puis `genre &amp; 2`, 0, et `(page &amp; 0x1000) | profondeur`.
    /// </summary>
    internal static int ChargerTexture(int* e, int* demande, uint page, uint clut, int genre)
    {
        int mode = (int)page >> 7 & 3;
        if ((int)clut == -1 && mode < 2) return 0;
        if (genre == 1 && Winsys3dData.Genre1Permis_00b2d188 == 0) genre = 0;

        bool sous16 = Winsys3dData.ProfondeurCouleur_00b2d07c < 0x10;
        switch (genre)
        {
        case 0:
            e[0] = sous16 || (page & 0x1000) != 0
                ? (int)ConvertirEnDib(clut, page, demande)
                : (int)ConvertirDirect(clut, page, demande, Winsys3dData.Couleurs555_00aad078);
            break;
        case 1:
            e[0] = sous16
                ? (int)ConvertirEnDib(clut, page, demande)
                : (int)ConvertirDirect(clut, page, demande, Winsys3dData.CouleursGenre1_00a8d078);
            break;
        case 2:
            e[0] = (int)ConvertirDirect(clut, page, demande, Winsys3dData.CouleursGenre2_00acd078);
            break;
        }

        if (e[0] == 0) return 0;

        e[1] = (int)clut;
        e[2] = demande[0];
        e[3] = demande[1];
        e[4] = demande[2];
        e[5] = demande[3];
        e[6] = (int)TableUv(Winsys3dData.TablesUvSpriteDefaut_011c4cd0, demande[0], demande[2] - demande[0]);
        e[7] = (int)TableUv(Winsys3dData.TablesUvSpriteDefaut_011c4cd0, demande[1], demande[3] - demande[1]);
        e[8] = (int)TableUv(Winsys3dData.TablesUvPolyDefaut_011be868, demande[0], demande[2] - demande[0]);
        e[9] = (int)TableUv(Winsys3dData.TablesUvPolyDefaut_011be868, demande[1], demande[3] - demande[1]);
        e[11] = genre & 2;
        e[10] = 0;
        e[12] = (int)(page & 0x1000) | mode;
        return 1;
    }

    /// <summary>
    /// La table de coordonnees de texture d'un axe : parmi des tables de 0x101 mots rangees a la
    /// suite de <paramref name="base"/>, celle qui correspond a la taille et a la position de la
    /// page — `(pos >> 5) + 0x11` pour 32, `+ 9` pour 64, `+ 1` pour 128 ; la premiere sinon.
    /// </summary>
    private static uint* TableUv(uint* @base, int position, int taille)
    {
        int i = position >> 5;
        switch (taille)
        {
        case 0x20: return @base + (i + 0x11) * 0x101;
        case 0x40: return @base + (i + 9) * 0x101;
        case 0x80: return @base + (i + 1) * 0x101;
        default: return @base;
        }
    }

    /// <summary>La profondeur d'une page : `(page >> 7) &amp; 3` -> 4, 8, ou 16 pour 2 et 3.</summary>
    private static int Profondeur(uint page)
    {
        int mode = (int)page >> 7 & 3;
        return mode == 0 ? 4 : mode != 1 ? 16 : 8;
    }

    /// <summary>L'adresse de la CLUT dans la VRAM.</summary>
    private static byte* AdresseClut(uint clut)
        => Winsys3dData.Vram_00b6dbd0 + (((int)clut >> 6 & 0x1ff) * 0x40 + (int)(clut & 0x3f)) * 0x20;

    /// <summary>L'adresse du coin de la demande dans la VRAM, pour une page et une profondeur.</summary>
    private static byte* AdresseSource(uint page, int bits, int x0, int y0)
    {
        int xOctets = bits * x0 / 8 + (int)(page & 0xf) * 0x80;
        int y = (int)(page & 0x10) * 16 + y0;
        return Winsys3dData.Vram_00b6dbd0 + y * LigneVram + xOctets;
    }

    // ============================================================================================
    // La conversion en DIB
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03460 @ 0x00a03460 — `ret 0Ch`. Construit un `BITMAPINFO` et une
    /// DIB montante dans le tampon de conversion, puis un objet texture par `device[0x10]` et sa
    /// methode [2]. Rend l'objet, ou nul si le chargement echoue.
    ///
    /// Le zoom des pages 8 bits (bit 0x1000, `Zoom8BitTextures` non nul) divise la largeur et la
    /// hauteur par 2^zoom, et la conversion 8 bits ne garde qu'un octet sur 2 ou sur 4 et qu'une
    /// ligne sur 2 ou sur 4 ; un zoom au-dela de 2 ne convertit rien.
    /// </summary>
    internal static void* ConvertirEnDib(uint clut, uint page, int* demande)
    {
        int bits = Profondeur(page);
        int y0 = demande[1];
        int hauteur = demande[3] - y0;
        int x0 = demande[0];
        int largeur = demande[2] - x0;
        int zoom = 0;
        if ((page & 0x1000) != 0 && Winsys3dData.Zoom8BitTextures_011be750 != 0)
        {
            zoom = Winsys3dData.Zoom8BitTextures_011be750;
            largeur >>= zoom;
            hauteur >>= zoom;
        }

        // Le BITMAPINFO : 0x28 octets d'en-tete, puis 256 RGBQUAD.
        byte* bmi = stackalloc byte[0x28 + 0x400];
        ushort bitCount = (ushort)(bits != 0x10 ? bits : 0x18);
        *(int*)(bmi + 0x00) = 0x28;
        *(int*)(bmi + 0x04) = largeur;
        *(int*)(bmi + 0x08) = hauteur;                 // positive : DIB montante
        *(ushort*)(bmi + 0x0c) = 1;
        *(ushort*)(bmi + 0x0e) = bitCount;
        *(int*)(bmi + 0x10) = 0;
        *(int*)(bmi + 0x14) = (bitCount >> 3) * hauteur * largeur;   // 0 pour 4 bits : `4 >> 3`
        *(int*)(bmi + 0x18) = 0;
        *(int*)(bmi + 0x1c) = 0;
        *(int*)(bmi + 0x20) = 0;
        *(int*)(bmi + 0x24) = 0;

        byte* source = AdresseSource(page, bits, x0, y0);
        byte* dib = Winsys3dData.TamponConversion_00aed078;

        if (bits == 0x10)
            Dib24(source, dib, largeur, hauteur, (clut & 0x8000) != 0);
        else if (bits == 8)
            Dib8(source, dib, largeur, hauteur, zoom);
        else
            Dib4(source, dib, largeur, hauteur);

        int cle = bits == 0x10 ? 0 : Palette(bmi + 0x28, clut, 1 << bits);

        void* texture = CreerTexture();
        void** vt = *(void***)texture;
        int hr = ((delegate* unmanaged[Thiscall]<void*, byte*, byte*, int>)vt[Texture_ChargerDib])(texture, bmi, dib);
        if (hr < 0)
        {
            if (texture != null) ((delegate* unmanaged[Thiscall]<void*, int, void>)vt[Texture_Detruire])(texture, 1);
            texture = null;
        }

        if (texture != null && cle != -1 && Winsys3dData.ProfondeurCouleur_00b2d07c < 0x10)
        {
            vt = *(void***)texture;
            if (((delegate* unmanaged[Thiscall]<void*, int>)vt[Texture_CleUtilisable])(texture) == 0) cle = 0;
            int* paire = stackalloc int[2] { cle, cle };
            ((delegate* unmanaged[Thiscall]<void*, int*, void>)vt[Texture_PoserCle])(texture, paire);
        }
        return texture;
    }

    /// <summary>
    /// 16 bits PS -> 24 bits B, G, R, lignes a l'envers. Avec la CLUT marquee 0x8000, un pixel au
    /// bit 15 pose ecrit un MOT nul et laisse la source en place — LB-040, voir l'en-tete.
    /// </summary>
    private static void Dib24(byte* source, byte* dib, int largeur, int hauteur, bool semi)
    {
        int pas = largeur * 3;
        byte* ligne = dib + (hauteur - 1) * pas;
        if (hauteur == 0) return;

        uint rangees = (uint)hauteur;
        do
        {
            ushort* s = (ushort*)source;
            byte* d = ligne;
            uint n = (uint)largeur;
            do
            {
                uint v = *s;
                if (semi && (v & 0x8000) != 0)
                {
                    *(uint*)d = 0;                 // un mot de 32 bits sur un pixel de 24
                    d += 3;                        // ... et `s` n'avance pas
                }
                else
                {
                    uint rb = v & 0x7c1f;
                    d[0] = (byte)((byte)(rb >> 8) << 1);
                    d[1] = (byte)((v & 0x3e0) >> 2);
                    d[2] = (byte)((byte)rb << 3);
                    d += 3;
                    s++;
                }
            } while (--n != 0);

            ligne -= pas;
            source += LigneVram;
        } while (--rangees != 0);
    }

    /// <summary>8 bits : copie des indices, lignes a l'envers ; zoom 1 ou 2, un octet et une ligne sur 2 ou 4.</summary>
    private static void Dib8(byte* source, byte* dib, int largeur, int hauteur, int zoom)
    {
        byte* ligne = dib + (hauteur - 1) * largeur;
        switch (zoom)
        {
        case 0:
            if (hauteur == 0) return;
            uint rangees = (uint)hauteur;
            do
            {
                Buffer.MemoryCopy(source, ligne, (uint)largeur, (uint)largeur);
                ligne -= largeur;
                source += LigneVram;
            } while (--rangees != 0);
            break;

        case 1:
        case 2:
            int pasOctet = zoom == 1 ? 2 : 4;
            int pasLigne = zoom == 1 ? 0x1000 : 0x2000;
            for (int r = hauteur; r > 0; r--)
            {
                for (int i = 0; i < largeur; i++) ligne[i] = source[i * pasOctet];
                ligne -= largeur;
                source += pasLigne;
            }
            break;
        }
    }

    /// <summary>4 bits : les quartets de chaque octet echanges (PS : pixel pair en bas ; DIB : en haut), lignes a l'envers.</summary>
    private static void Dib4(byte* source, byte* dib, int largeur, int hauteur)
    {
        int octets = largeur >> 1;
        byte* ligne = dib + (hauteur - 1) * octets;
        if (hauteur == 0) return;

        uint rangees = (uint)hauteur;
        do
        {
            uint* s = (uint*)source;
            uint* d = (uint*)ligne;
            uint n = (uint)octets >> 2;
            do
            {
                uint v = *s++;
                *d++ = Quartets((byte)v) | (uint)Quartets((byte)(v >> 8)) << 8
                     | (uint)Quartets((byte)(v >> 16)) << 16 | (uint)Quartets((byte)(v >> 24)) << 24;
            } while (--n != 0);

            ligne -= octets;
            source += LigneVram;
        } while (--rangees != 0);
    }

    private static byte Quartets(byte b) => (byte)(b << 4 | b >> 4);

    /// <summary>
    /// La palette DIB : <paramref name="nombre"/> couleurs de la CLUT, chaque composante 5 bits
    /// passee par <c>DAT_00b2d080</c>. Rend l'indice de la premiere couleur nulle (la cle), ou -1.
    /// Avec la CLUT marquee 0x8000, une couleur au bit 15 pose devient noire.
    /// </summary>
    private static int Palette(byte* palette, uint clut, int nombre)
    {
        for (int i = 0; i < 0x100; i++) ((uint*)palette)[i] = 0;

        short* couleurs = (short*)AdresseClut(clut);
        byte* composante = Winsys3dData.Composantes_00b2d080;
        bool semi = (clut & 0x8000) != 0;
        int cle = -1;
        for (int i = 0; i < nombre; i++)
        {
            int v = couleurs[i];
            if (v == 0 && cle == -1) cle = i;

            byte* q = palette + i * 4;
            if (semi && (v & 0x8000) != 0)
            {
                q[0] = 0; q[1] = 0; q[2] = 0;
            }
            else
            {
                q[2] = composante[(v & 0x1f) * 8];
                q[1] = composante[v >> 2 & 0xf8];
                q[0] = composante[v >> 7 & 0xf8];
            }
        }
        return cle;
    }

    // ============================================================================================
    // La conversion directe en 16 bits
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03980 @ 0x00a03980 — `ret 10h`. Convertit la demande en pixels 16
    /// bits par la table <paramref name="table"/> (65 536 couleurs), lignes a l'endroit, puis un
    /// objet texture par `device[0x10]` et sa methode [3]. Les pages a palette passent d'abord leur
    /// CLUT (256 couleurs, meme pour 4 bits) par la table. Le format de pixel annonce 16 bits, et
    /// les masques 1-5-5-5 si la table est celle du genre 0, 4-4-4-4 sinon.
    /// </summary>
    internal static void* ConvertirDirect(uint clut, uint page, int* demande, ushort* table)
    {
        int bits = Profondeur(page);

        int* format = stackalloc int[8];               // DDPIXELFORMAT ; `dwFourCC` reste a 0
        format[0] = 0x20;
        format[1] = 0x41;                              // DDPF_RGB | DDPF_ALPHAPIXELS
        format[3] = 0x10;
        if (table == Winsys3dData.Couleurs555_00aad078)
        {
            format[4] = 0x7c00; format[5] = 0x3e0; format[6] = 0x1f; format[7] = 0x8000;
        }
        else
        {
            format[4] = 0xf00; format[5] = 0xf0; format[6] = 0xf; format[7] = 0xf000;
        }

        int hauteur = demande[3] - demande[1];
        int largeur = demande[2] - demande[0];
        byte* source = AdresseSource(page, bits, demande[0], demande[1]);
        ushort* dest = (ushort*)Winsys3dData.TamponConversion_00aed078;

        if (bits == 0x10)
        {
            if (hauteur != 0)
            {
                uint rangees = (uint)hauteur;
                do
                {
                    ushort* s = (ushort*)source;
                    for (int i = 0; i < largeur; i++) dest[i] = table[s[i]];
                    source += LigneVram;
                    dest += largeur;
                } while (--rangees != 0);
            }
        }
        else
        {
            ushort* palette = stackalloc ushort[0x100];
            ushort* clutVram = (ushort*)AdresseClut(clut);
            for (int i = 0; i < 0x100; i++) palette[i] = table[clutVram[i]];

            if (hauteur != 0)
            {
                uint rangees = (uint)hauteur;
                do
                {
                    if (bits == 8)
                    {
                        for (int i = 0; i < largeur; i++) dest[i] = palette[source[i]];
                    }
                    else
                    {
                        int moitie = largeur >> 1;
                        for (int i = 0; i < moitie; i++)
                        {
                            byte b = source[i];
                            dest[2 * i] = palette[b & 0xf];
                            dest[2 * i + 1] = palette[b >> 4];
                        }
                    }
                    source += LigneVram;
                    dest += largeur;
                } while (--rangees != 0);
            }
        }

        void* texture = CreerTexture();
        void** vt = *(void***)texture;
        int hr = ((delegate* unmanaged[Thiscall]<void*, int, int, byte*, int*, int>)vt[Texture_ChargerBrut])(
            texture, largeur, hauteur, Winsys3dData.TamponConversion_00aed078, format);

        if (texture != null && Winsys3dData.ProfondeurCouleur_00b2d07c < 0x10)
        {
            int* paire = stackalloc int[2];            // la cle {0, 0} construite sur la pile
            ((delegate* unmanaged[Thiscall]<void*, int*, void>)vt[Texture_PoserCle])(texture, paire);
        }
        if (hr < 0)
        {
            if (texture != null) ((delegate* unmanaged[Thiscall]<void*, int, void>)vt[Texture_Detruire])(texture, 1);
            texture = null;
        }
        return texture;
    }

    /// <summary>`device[0x10]()` : un objet texture neuf.</summary>
    private static void* CreerTexture()
        => ((delegate* unmanaged[Thiscall]<void*, void*>)
            Winsys3dData.DeviceVtable[Device_CreerTexture])(Winsys3dData.Device_011be760);

    // ============================================================================================
    // LES TABLES DE COULEURS — ordinal 756, `CreateTranslationTables` (2026-09-23), transcrit sur
    // le desassemblage (`Legacy/Raw/Winsys3d/CreateTranslationTables.disasm.txt`) : Ghidra reutilise
    // le parametre `float` comme compteur d'octets de la boucle, et le rend illisible.
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c CreateTranslationTables @ 0x00a02f50 (ordinal 756) — si le gamma a change,
    /// recalcule les tables que les convertisseurs ci-dessus relisent. `ret 4`.
    ///
    /// D'abord les 256 composantes corrigees (`Gamma` de `wsyskern`, pour `0 .. 255`), puis, pour
    /// chaque couleur PS `i` de 1 a 0xffff — R = bits 0-4, G = 5-9, B = 10-14, chacun lu dans la table
    /// a l'indice `composante * 8` :
    ///
    ///     genre 1 (0x00a8d078)   A = 7 | ~i bit 15..12, puis R, G, B sur 4 bits
    ///     genre 0 (0x00aad078)   1-5-5-5, bit 15 toujours pose
    ///     genre 2 (0x00acd078)   A = 0xf, R, G, B sur 4 bits — pour i &lt;= 0x7fff seulement
    ///     genre 2 haut (0x00add078)   ((R8 + G8 + B8) / 3 &amp; 0xf0) &lt;&lt; 8 + 1, sur les composantes BRUTES
    ///                                 (avant gamma, `add ecx,edx ; add ecx,ebp`) : un noir dont l'alpha
    ///                                 est la luminosite — la semi-transparence PS emulee
    ///
    /// **La comparaison est celle du 387** (`fcomp` puis le bit C3) : un NaN d'un cote ou de
    /// l'autre compte pour « egal », et rien n'est recalcule. L'entree 0 des quatre tables est
    /// remise a 0.
    /// </summary>
    internal static void CreateTranslationTables(float gamma)
    {
        float precedent = Winsys3dData.GammaCourant_00a2c228;
        if (gamma == precedent || float.IsNaN(gamma) || float.IsNaN(precedent)) return;

        Winsys3dData.GammaCourant_00a2c228 = gamma;
        ushort* genre1 = Winsys3dData.CouleursGenre1_00a8d078;
        ushort* genre0 = Winsys3dData.Couleurs555_00aad078;
        ushort* genre2 = Winsys3dData.CouleursGenre2_00acd078;
        ushort* genre2Haute = Winsys3dData.CouleursGenre2Haute_00add078;
        genre1[0] = 0;
        genre0[0] = 0;
        genre2[0] = 0;
        genre2Haute[0] = 0;

        byte* composantes = Winsys3dData.Composantes_00b2d080;
        for (int v = 0; v < 0x100; v++) composantes[v] = Blaze.Wsyskern.Gamma(v, gamma);

        for (int i = 1, octet = 0; octet < 0x1fffe; i++, octet += 2)
        {
            int indiceG = i >> 2 & 0xf8;
            int indiceB = i >> 7 & 0xf8;
            int indiceR = (i & 0x1f) << 3;
            int g = composantes[indiceG];
            int b = composantes[indiceB];
            int r = composantes[indiceR];

            int gb4 = b >> 4 & 0xf | g & 0xf0;
            genre1[i] = (ushort)((0x700 | r & 0xf0) << 4 | ~i & 0xf000 | gb4);
            genre0[i] = (ushort)((g & 0xf8 | (0xff00 | r & 0xf8) << 5) << 2 | b >> 3 & 0x1f);
            if (octet < 0xfffe)
            {
                genre2[i] = (ushort)((0xff00 | r & 0xf0) << 4 | gb4);
                genre2Haute[i] = (ushort)((((indiceB + indiceG + indiceR) / 3 & 0xf0) << 8) + 1);
            }
        }
    }
}
