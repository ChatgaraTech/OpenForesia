// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur**, corps par corps.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES PRIMITIVES GPU — les appelees du repartiteur `FUN_00a13400`
//
// `CheminAffichage.RejouerNoeud` lit le code de primitive a l'octet 7 d'un paquet de l'OT et
// appelle un gestionnaire par genre. Ces codes sont ceux de la `libgpu` PlayStation — 0x20
// POLY_F3, 0x28 POLY_F4, 0x40 LINE_F2, 0x60 TILE, 0x68 TILE_1… — et chaque gestionnaire traduit
// sa primitive en un appel Direct3D. Ce fichier les recoit **par familles de forme**, dans
// l'ordre de `Documentation/Etat/CheminAffichage.md` : une forme comprise vaut pour toute sa
// famille.
//
// Les noms sont ceux de la `libgpu`, et c'est une preuve, pas un habillage : le code de
// l'aiguillage, la disposition des champs (couleur a l'octet 4, coordonnees en shorts a partir
// de l'octet 8, largeur/hauteur de TILE a 0xc/0xe) et le nombre de sommets emis concordent pour
// chacun des neuf.
//
// ============================================================================================
// FAMILLE 1 — `CIVIVCIIIVR`, les primitives PLATES en dessin immediat (9 corps, 2026-09-23)
//
// Pas de texture, une seule couleur, et **pas de tampon de commandes** : chacune ecrit ses
// sommets dans quatre `D3DTLVERTEX` fixes (`&DAT_011cb138`) et appelle `DrawPrimitive` tout de
// suite. Les neuf corps partagent mot pour mot le meme prologue et la meme couleur ; ils ne
// different que par les sommets et le type de primitive :
//
//     code   gestionnaire   primitive     D3D                 sommets
//     0x40   FUN_00a0e580   LINE_F2       2 LINELIST           2
//     0x48   FUN_00a0e6a0   LINE_F3       3 LINESTRIP          3
//     0x4c   FUN_00a0e7f0   LINE_F4       3 LINESTRIP          4
//     0x20   FUN_00a0ef40   POLY_F3       4 TRIANGLELIST       3
//     0x28   FUN_00a0f3f0   POLY_F4       6 TRIANGLEFAN        4   ordre 0, 1, 3, 2
//     0x60   FUN_00a10690   TILE          6 TRIANGLEFAN        4   largeur/hauteur lues
//     0x68   FUN_00a10b60   TILE_1        4 TRIANGLELIST       3   UN seul triangle
//     0x70   FUN_00a10ca0   TILE_8        6 TRIANGLEFAN        4
//     0x78   FUN_00a111c0   TILE_16       6 TRIANGLEFAN        4
//
// Les types et nombres ont ete relus sur les `push` qui precedent `call [reg+0BCh]` dans chacun
// des neuf corps, pas seulement dans Ghidra. Le format de sommet est toujours 3, `D3DVT_TLVERTEX`.
//
// L'original ecrit ce prologue neuf fois en ligne ; ici il est factorise dans
// <see cref="PreparerEtatPlat"/>, et c'est la seule liberte prise — l'ordre des ecritures en
// memoire partagee est celui du binaire.
//
// --------------------------------------------------------------------------------------------
// CE QUE GHIDRA NE DIT PAS, OU DIT MAL
//
// **`POLY_F4` est emis en eventail, sommets 0, 1, 3, 2.** La PlayStation range un quadrilatere
// en « Z » (haut-gauche, haut-droit, bas-gauche, bas-droit) ; un `TRIANGLEFAN` veut le tour du
// contour. Le corps ecrit donc le sommet PS 3 (octets 0x14/0x16) en deuxieme position D3D et le
// sommet PS 2 (0x10/0x12) en troisieme.
//
// **`TILE_1` ne dessine qu'un triangle** : `DrawPrimitive(TRIANGLELIST, 3 sommets)`, le
// triangle haut-droit du pixel. Il ecrit pourtant la couleur du quatrieme sommet. C'est le
// binaire (`push 3 ; push 4` a 0x00a10c26), transcrit tel quel.
//
// **Les temporaires de Ghidra sont des sommets.** Dans `LINE_F3`, `POLY_F3`, etc., Ghidra range
// `(float)DAT_011be770` dans `_DAT_011cb178` avant de l'ecraser par la vraie coordonnee : il a
// pris un emplacement de pile pour une globale voisine. Seule la valeur finale compte ; elle est
// identique.
//
// **L'arithmetique flottante est la meme.** Le binaire additionne en x87 (`fild` du decalage
// entier, `fadd` de l'entree de table) puis arrondit une fois au `fstp` ; le C# convertit le
// decalage en `float` et additionne en simple precision, arrondi une fois. Les deux donnent la
// somme exacte correctement arrondie des que (a) le decalage tient sur 24 bits, ce qu'un decalage
// d'ecran fait toujours, et (b) la somme exacte tient dans la mantisse x87 (53 bits sous le mot
// de controle par defaut de Windows) — faux seulement pour une entree de table non nulle et
// inferieure a 2^-29 en valeur absolue, ce qu'une coordonnee ecran n'est pas.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dPrimitives
{
    private const int Device_LierTexture = 0x64 / 4;
    private const int Device_ShadeMode = 0x8c / 4;
    private const int Device_AlphaBlendEnable = 0x98 / 4;
    private const int Device_Etat9c = 0x9c / 4;
    private const int Device_SrcBlend = 0xa0 / 4;
    private const int Device_DestBlend = 0xa4 / 4;
    private const int Device_DrawPrimitive = 0xbc / 4;

    // Les types de primitive Direct3D que ces corps emploient.
    private const uint D3DPT_LINELIST = 2;
    private const uint D3DPT_LINESTRIP = 3;
    private const uint D3DPT_TRIANGLELIST = 4;
    private const uint D3DPT_TRIANGLEFAN = 6;

    /// <summary>Le format de sommet : toujours 3, `D3DVT_TLVERTEX`.</summary>
    private const uint D3DVT_TLVERTEX = 3;

    /// <summary>Un `D3DTLVERTEX` fait 0x20 octets, soit huit flottants.</summary>
    private const int FlottantsParSommet = 8;

    // ============================================================================================
    // LE MELANGE — `FUN_00a03db0`, appele par toute la famille (et par les producteurs)
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03db0 @ 0x00a03db0 — choisit le mode de melange, en ne touchant
    /// le device que si le memo <c>DAT_00a2c210</c> dit qu'il change.
    ///
    /// Le melange alpha par la source (<c>SRCBLEND</c> 5 SRCALPHA, <c>DESTBLEND</c> 6 INVSRCALPHA)
    /// n'est pose que si le device a **au moins 16 bits** de couleur ET que l'appelant ne force pas
    /// l'opaque. Sinon l'opaque : melange coupe, <c>device[0x9c](0)</c>, <c>SRCBLEND</c> 2 ONE,
    /// <c>DESTBLEND</c> 1 ZERO. Le chemin alpha ne touche pas <c>device[0x9c]</c> — le binaire
    /// n'a que trois appels dans cette branche (0x00a03de3, 0x00a03df3, 0x00a03e03).
    ///
    /// `ret 4`, stdcall, un argument. Verifie au desassembleur.
    /// </summary>
    internal static void ChoisirMelange(int forcerOpaque)
    {
        if (Winsys3dData.ProfondeurCouleur_00b2d07c < 0x10 || forcerOpaque != 0)
        {
            if (Winsys3dData.MemoMelange_00a2c210 != -1)
            {
                Winsys3dData.MemoMelange_00a2c210 = -1;
                AppelDevice(Device_AlphaBlendEnable, 0);
                AppelDevice(Device_Etat9c, 0);
                AppelDevice(Device_SrcBlend, 2);
                AppelDevice(Device_DestBlend, 1);
            }
        }
        else if (Winsys3dData.MemoMelange_00a2c210 != -2)
        {
            Winsys3dData.MemoMelange_00a2c210 = -2;
            AppelDevice(Device_AlphaBlendEnable, 1);
            AppelDevice(Device_SrcBlend, 5);
            AppelDevice(Device_DestBlend, 6);
        }
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03400 @ 0x00a03400 — le mode de melange <paramref name="mode"/>
    /// s'il est disponible sur ce device ; sinon son mode de repli (<c>DAT_00a2c218</c>) s'il l'est ;
    /// sinon -1. `ret 4`, verifie au desassembleur.
    /// </summary>
    internal static int ModeMelangeDisponible(int mode)
    {
        if (Winsys3dData.MelangesDisponibles_00a2f430[mode] != 0) return mode;
        mode = Winsys3dData.MelangesRepli_00a2c218[mode];
        if (Winsys3dData.MelangesDisponibles_00a2f430[mode] != 0) return mode;
        return -1;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03430 @ 0x00a03430 — pour une page de texture : -1 si son mode
    /// <c>abr</c> n'a aucun melange disponible, 2 si le mode retenu est 2, 0 sinon
    /// (`sub eax,2 ; neg ; sbb ; and al,0FEh ; add eax,2`).
    /// </summary>
    internal static int MelangeDePage(uint page)
    {
        int mode = ModeMelangeDisponible((int)page >> 5 & 3);
        if (mode == -1) return -1;
        return mode != 2 ? 0 : 2;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a03c80 @ 0x00a03c80 — pose le melange Direct3D qui imite le mode de
    /// semi-transparence <c>abr</c> de la page. Rend 0 si le device ne sait pas le faire —
    /// l'appelant ne dessine alors rien.
    ///
    /// Mode indisponible : melange opaque ou alpha selon le bit 13, puis le resultat de
    /// <see cref="MelangeDePage"/>, qui refait la meme recherche et rend donc -1 — la fonction rend
    /// 0. Mode deja pose (memo <c>DAT_00a2c210</c>) : 1 sans rien toucher. Sinon, selon les
    /// capacites : 0x1000 -> melange actif et le mode lui-meme ; 0x2000 -> `device[0x9c](1)` et le
    /// mode 0 ; ni l'un ni l'autre -> 0. Puis la table de sauts de 0x00a03d98, relue dans le
    /// binaire : 0 et 2 -> SRCALPHA / INVSRCALPHA ; 1, 3, 4 -> SRCBLEND 2, 5, 3 et DESTBLEND 2 ;
    /// au-dela de 4 -> rien.
    /// </summary>
    internal static int ChoisirMelangePsx(uint page)
    {
        int mode = ModeMelangeDisponible((int)page >> 5 & 3);
        if (mode == -1)
        {
            ChoisirMelange((int)(page >> 13 & 1));
            return MelangeDePage(page) != mode ? 1 : 0;
        }

        if (Winsys3dData.MemoMelange_00a2c210 == mode) return 1;

        int cas;
        if ((Winsys3dData.CapacitesMelange_00a2f454 & 0x1000) != 0)
        {
            AppelDevice(Device_AlphaBlendEnable, 1);
            Winsys3dData.MemoMelange_00a2c210 = mode;
            cas = mode;
        }
        else if ((Winsys3dData.CapacitesMelange_00a2f454 & 0x2000) != 0)
        {
            AppelDevice(Device_Etat9c, 1);
            Winsys3dData.MemoMelange_00a2c210 = mode;
            cas = 0;
        }
        else
            return 0;

        switch (cas)
        {
        case 0:
        case 2:
            AppelDevice(Device_SrcBlend, 5);
            AppelDevice(Device_DestBlend, 6);
            return 1;
        case 1: AppelDevice(Device_SrcBlend, 2); break;
        case 3: AppelDevice(Device_SrcBlend, 5); break;
        case 4: AppelDevice(Device_SrcBlend, 3); break;
        default: return 1;
        }
        AppelDevice(Device_DestBlend, 2);
        return 1;
    }

    // ============================================================================================
    // FAMILLE 1 — les primitives plates en dessin immediat
    // ============================================================================================

    /// <summary>Ghidra: winsys3d.c FUN_00a0e580 @ 0x00a0e580 — `LINE_F2` (0x40..0x47) : un segment.</summary>
    internal static void LineF2(byte* prim)
    {
        PreparerEtatPlat();
        uint couleur = CouleurPlate(prim);
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, couleur);
        PoserCouleur(v, 1, couleur);
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x0c), Y(prim, 0x0e));
        Dessiner(D3DPT_LINELIST, 2);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0e6a0 @ 0x00a0e6a0 — `LINE_F3` (0x48..0x4b) : deux segments chaines.</summary>
    internal static void LineF3(byte* prim)
    {
        PreparerEtatPlat();
        uint couleur = CouleurPlate(prim);
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, couleur);
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x0c), Y(prim, 0x0e));
        PoserSommet(v, 2, X(prim, 0x10), Y(prim, 0x12));
        PoserCouleur(v, 1, couleur);
        PoserCouleur(v, 2, couleur);
        Dessiner(D3DPT_LINESTRIP, 3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0e7f0 @ 0x00a0e7f0 — `LINE_F4` (0x4c..0x4f) : trois segments chaines.</summary>
    internal static void LineF4(byte* prim)
    {
        PreparerEtatPlat();
        uint couleur = CouleurPlate(prim);
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, couleur);
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x0c), Y(prim, 0x0e));
        PoserSommet(v, 2, X(prim, 0x10), Y(prim, 0x12));
        PoserSommet(v, 3, X(prim, 0x14), Y(prim, 0x16));
        PoserCouleur(v, 1, couleur);
        PoserCouleur(v, 2, couleur);
        PoserCouleur(v, 3, couleur);
        Dessiner(D3DPT_LINESTRIP, 4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0ef40 @ 0x00a0ef40 — `POLY_F3` (0x20..0x23) : un triangle plein.</summary>
    internal static void PolyF3(byte* prim)
    {
        PreparerEtatPlat();
        uint couleur = CouleurPlate(prim);
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, couleur);
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x0c), Y(prim, 0x0e));
        PoserSommet(v, 2, X(prim, 0x10), Y(prim, 0x12));
        PoserCouleur(v, 1, couleur);
        PoserCouleur(v, 2, couleur);
        Dessiner(D3DPT_TRIANGLELIST, 3);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0f3f0 @ 0x00a0f3f0 — `POLY_F4` (0x28..0x2b) : un quadrilatere
    /// plein, emis en eventail. **Les sommets PS 2 et 3 sont croises** — voir l'en-tete.
    /// </summary>
    internal static void PolyF4(byte* prim)
    {
        PreparerEtatPlat();
        uint couleur = CouleurPlate(prim);
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, couleur);
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x0c), Y(prim, 0x0e));
        PoserSommet(v, 2, X(prim, 0x14), Y(prim, 0x16));
        PoserSommet(v, 3, X(prim, 0x10), Y(prim, 0x12));
        PoserCouleur(v, 1, couleur);
        PoserCouleur(v, 2, couleur);
        PoserCouleur(v, 3, couleur);
        Dessiner(D3DPT_TRIANGLEFAN, 4);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a10690 @ 0x00a10690 — `TILE` (0x60..0x63) : un rectangle plein
    /// dont la largeur et la hauteur sont dans la primitive (octets 0xc et 0xe).
    /// </summary>
    internal static void Tile(byte* prim)
        => Rectangle(prim, *(short*)(prim + 0x0c), *(short*)(prim + 0x0e));

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a10b60 @ 0x00a10b60 — `TILE_1` (0x68..0x6f) : un pixel. **Un seul
    /// triangle** part au device, le haut-droit ; voir l'en-tete.
    /// </summary>
    internal static void Tile1(byte* prim)
    {
        PreparerEtatPlat();
        uint couleur = CouleurPlate(prim);
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        int x = *(short*)(prim + 0x08);
        int y = *(short*)(prim + 0x0a);

        PoserCouleur(v, 0, couleur);
        float x0 = EcranX(x), y0 = EcranY(y);
        float x1 = EcranX(x + 1), y1 = EcranY(y + 1);
        PoserSommet(v, 0, x0, y0);
        PoserSommet(v, 1, x1, y0);
        PoserSommet(v, 2, x1, y1);
        PoserCouleur(v, 1, couleur);
        PoserCouleur(v, 2, couleur);
        PoserCouleur(v, 3, couleur);    // ecrite, bien que le quatrieme sommet ne parte pas
        Dessiner(D3DPT_TRIANGLELIST, 3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a10ca0 @ 0x00a10ca0 — `TILE_8` (0x70..0x73) : un carre de 8.</summary>
    internal static void Tile8(byte* prim) => Rectangle(prim, 8, 8);

    /// <summary>Ghidra: winsys3d.c FUN_00a111c0 @ 0x00a111c0 — `TILE_16` (0x78..0x7b) : un carre de 16.</summary>
    internal static void Tile16(byte* prim) => Rectangle(prim, 16, 16);

    /// <summary>
    /// Le corps commun de <c>TILE</c>, <c>TILE_8</c> et <c>TILE_16</c> : trois transcriptions de
    /// l'original qui ne different que par la taille — lue dans la primitive, ou constante (le
    /// `+ 0x20` et `+ 0x40` d'octets de Ghidra sont 8 et 16 entrees de table). Eventail de quatre
    /// sommets, dans le sens du contour.
    /// </summary>
    private static void Rectangle(byte* prim, int largeur, int hauteur)
    {
        PreparerEtatPlat();
        uint couleur = CouleurPlate(prim);
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        int x = *(short*)(prim + 0x08);
        int y = *(short*)(prim + 0x0a);

        PoserCouleur(v, 0, couleur);
        float x0 = EcranX(x), y0 = EcranY(y);
        float x1 = EcranX(x + largeur), y1 = EcranY(y + hauteur);
        PoserSommet(v, 0, x0, y0);
        PoserSommet(v, 1, x1, y0);
        PoserSommet(v, 2, x1, y1);
        PoserSommet(v, 3, x0, y1);
        PoserCouleur(v, 1, couleur);
        PoserCouleur(v, 2, couleur);
        PoserCouleur(v, 3, couleur);
        Dessiner(D3DPT_TRIANGLEFAN, 4);
    }

    // ============================================================================================
    // FAMILLE 2 — `CIICRCIIVVR`, les primitives TEXTUREES SEMI-TRANSPARENTES a texture brute
    // (7 corps, 2026-09-23)
    //
    //     code   gestionnaire   primitive   D3D                 sommets   page de texture
    //     0x27   FUN_00a118e0   POLY_FT3    4 TRIANGLELIST       3        octet 0x16 du paquet
    //     0x2f   FUN_00a11cd0   POLY_FT4    6 TRIANGLEFAN        4        octet 0x16
    //     0x37   FUN_00a12190   POLY_GT3    4 TRIANGLELIST       3        octet 0x1a
    //     0x3f   FUN_00a126d0   POLY_GT4    6 TRIANGLEFAN        4        octet 0x1a
    //     0x67   FUN_00a13220   SPRT        6 TRIANGLEFAN        4        DAT_011be6ec (l'opcode 0xe1)
    //     0x77   FUN_00a12ac0   SPRT_8      6 TRIANGLEFAN        4        DAT_011be6ec
    //     0x7f   FUN_00a12e60   SPRT_16     6 TRIANGLEFAN        4        DAT_011be6ec
    //
    // « Brute » : la couleur du paquet est ignoree, chaque sommet part en blanc avec l'alpha du
    // mode de semi-transparence — `AlphaParMode[abr] << 24 | 0xffffff`. Les versions Gouraud ne
    // se distinguent donc des plates que par la disposition du paquet et par l'ombrage (etat 7,
    // `SHADEMODE` 2, au lieu de l'etat 6, `SHADEMODE` 1).
    //
    // Le prologue commun lie la texture si la page, la CLUT ou le genre (0 polygone, 1 sprite)
    // changent — par `FUN_00a27880`, un `__thiscall` sur `DAT_012141d4` — puis choisit le melange
    // par `FUN_00a03c80`. **Chacun des deux peut refuser**, et le corps sort alors sans rien
    // dessiner. Quand la page est en mode `abr` 2 (soustractif), la CLUT est marquee `| 0x8000`
    // avant la comparaison ET avant la liaison.
    //
    // Les sommets sont ceux de `&DAT_011be7e8`. Meme croisement des sommets 2 et 3 que
    // `POLY_F4` pour les quadrilateres ; les sprites se deroulent dans le sens du contour, la
    // texture suivant la geometrie (u + largeur, v + hauteur).
    // ============================================================================================

    // ---- famille 2 : semi-transparentes ------------------------------------------------------

    /// <summary>Ghidra: winsys3d.c FUN_00a118e0 @ 0x00a118e0 — `POLY_FT3` semi-transparent, texture brute (0x27).</summary>
    internal static void PolyFT3SemiBrut(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x16);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbragePlatTexture();
        TriangleTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c, CouleurBrute(page), 4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a11cd0 @ 0x00a11cd0 — `POLY_FT4` semi-transparent, texture brute (0x2f).</summary>
    internal static void PolyFT4SemiBrut(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x16);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbragePlatTexture();
        QuadTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c, 0x20, 0x24, CouleurBrute(page));
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a12190 @ 0x00a12190 — `POLY_GT3` semi-transparent, texture brute (0x37).</summary>
    internal static void PolyGT3SemiBrut(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x1a);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbrageGouraudTexture();
        TriangleTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x14, 0x18, 0x20, 0x24, CouleurBrute(page), 4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a126d0 @ 0x00a126d0 — `POLY_GT4` semi-transparent, texture brute (0x3f).</summary>
    internal static void PolyGT4SemiBrut(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x1a);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbrageGouraudTexture();
        QuadTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x14, 0x18, 0x20, 0x24, 0x2c, 0x30, CouleurBrute(page));
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a13220 @ 0x00a13220 — `SPRT` semi-transparent, texture brute
    /// (0x67, par le sous-switch <c>EmettreSprite</c>). Largeur et hauteur aux octets 0x10/0x12.
    /// </summary>
    internal static void SprtSemiBrut(byte* prim)
        => SpriteSemiBrut(prim, *(short*)(prim + 0x10), *(short*)(prim + 0x12));

    /// <summary>Ghidra: winsys3d.c FUN_00a12ac0 @ 0x00a12ac0 — `SPRT_8` semi-transparent, texture brute (0x77).</summary>
    internal static void Sprt8SemiBrut(byte* prim) => SpriteSemiBrut(prim, 8, 8);

    /// <summary>Ghidra: winsys3d.c FUN_00a12e60 @ 0x00a12e60 — `SPRT_16` semi-transparent, texture brute (0x7f).</summary>
    internal static void Sprt16SemiBrut(byte* prim) => SpriteSemiBrut(prim, 16, 16);

    /// <summary>
    /// Le corps commun des trois sprites semi-transparents bruts : l'original l'ecrit trois fois,
    /// le `+ 0x20` / `+ 0x40` d'octets de Ghidra etant 8 / 16 entrees de table. La page de
    /// texture n'est pas dans le paquet : c'est celle que l'opcode 0xe1 a deposee.
    /// </summary>
    private static void SpriteSemiBrut(byte* prim, int largeur, int hauteur)
    {
        uint page = Winsys3dData.Demi_011be6ec;
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 1)) return;
        OmbragePlatTexture();
        SpriteTexture(Winsys3dData.SommetsTextures_011be7e8, prim, largeur, hauteur, CouleurBrute(page));
    }

    // ============================================================================================
    // FAMILLE 3 — `CICECIVVR`, les memes primitives, OPAQUES a texture brute (7 corps, 2026-09-23)
    //
    //     0x25 FUN_00a0f260 POLY_FT3    0x2d FUN_00a0f780 POLY_FT4    0x75 FUN_00a11000 SPRT_8
    //     0x35 FUN_00a0fdc0 POLY_GT3    0x3d FUN_00a104c0 POLY_GT4    0x7d FUN_00a11520 SPRT_16
    //     0x65 FUN_00a109c0 SPRT        (par le sous-switch `EmettreSprite`)
    //
    // Geometrie et coordonnees de texture identiques a la famille 2, offset pour offset — relu
    // ligne a ligne dans les sept corps. Deux choses changent :
    //
    //   - le prologue **ne refuse jamais** : si page, CLUT et genre sont deja poses, il ne fait
    //     que `ChoisirMelange((page >> 13) & 1)` ; sinon il lie par `FUN_00a27880(page, clut, 0,
    //     genre)` et **ignore son retour** (0x00a0f2a7, pas de `test eax` derriere). Pas de
    //     marquage `| 0x8000` de la CLUT : il n'y a pas de mode soustractif sans semi-transparence ;
    //   - la couleur est **`0xffffffff`**, blanc opaque, sur les quatre sommets.
    // ============================================================================================

    private const uint BlancOpaque = 0xffffffff;

    /// <summary>Ghidra: winsys3d.c FUN_00a0f260 @ 0x00a0f260 — `POLY_FT3` opaque, texture brute (0x25).</summary>
    internal static void PolyFT3Brut(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x16), *(ushort*)(prim + 0x0e), 0);
        OmbragePlatTexture();
        TriangleTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c, BlancOpaque, 4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0f780 @ 0x00a0f780 — `POLY_FT4` opaque, texture brute (0x2d).</summary>
    internal static void PolyFT4Brut(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x16), *(ushort*)(prim + 0x0e), 0);
        OmbragePlatTexture();
        QuadTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c, 0x20, 0x24, BlancOpaque);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0fdc0 @ 0x00a0fdc0 — `POLY_GT3` opaque, texture brute (0x35).</summary>
    internal static void PolyGT3Brut(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x1a), *(ushort*)(prim + 0x0e), 0);
        OmbrageGouraudTexture();
        TriangleTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x14, 0x18, 0x20, 0x24, BlancOpaque, 4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a104c0 @ 0x00a104c0 — `POLY_GT4` opaque, texture brute (0x3d).</summary>
    internal static void PolyGT4Brut(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x1a), *(ushort*)(prim + 0x0e), 0);
        OmbrageGouraudTexture();
        QuadTexture(Winsys3dData.SommetsTextures_011be7e8, prim, 0x08, 0x0c, 0x14, 0x18, 0x20, 0x24, 0x2c, 0x30, BlancOpaque);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a109c0 @ 0x00a109c0 — `SPRT` opaque, texture brute (0x65, par <c>EmettreSprite</c>).</summary>
    internal static void SprtBrut(byte* prim)
        => SpriteBrut(prim, *(short*)(prim + 0x10), *(short*)(prim + 0x12));

    /// <summary>Ghidra: winsys3d.c FUN_00a11000 @ 0x00a11000 — `SPRT_8` opaque, texture brute (0x75).</summary>
    internal static void Sprt8Brut(byte* prim) => SpriteBrut(prim, 8, 8);

    /// <summary>Ghidra: winsys3d.c FUN_00a11520 @ 0x00a11520 — `SPRT_16` opaque, texture brute (0x7d).</summary>
    internal static void Sprt16Brut(byte* prim) => SpriteBrut(prim, 16, 16);

    private static void SpriteBrut(byte* prim, int largeur, int hauteur)
    {
        LierTextureOpaque(Winsys3dData.Demi_011be6ec, *(ushort*)(prim + 0x0e), 1);
        OmbragePlatTexture();
        SpriteTexture(Winsys3dData.SommetsTextures_011be7e8, prim, largeur, hauteur, BlancOpaque);
    }

    // ============================================================================================
    // FAMILLES 4 et 5 — `CIICRCIIVIIIVR` et `CICECIVIIIVR`, les memes primitives texturees, mais
    // MODULEES par la couleur du paquet (10 corps, 2026-09-23)
    //
    //     semi-transparentes (famille 4)          opaques (famille 5)
    //     0x26 FUN_00a116e0 POLY_FT3              0x24 FUN_00a0f090 POLY_FT3
    //     0x2e FUN_00a11a90 POLY_FT4              0x2c FUN_00a0f570 POLY_FT4
    //     0x66 FUN_00a13010 SPRT                  0x64 FUN_00a107e0 SPRT
    //     0x76 FUN_00a128d0 SPRT_8                0x74 FUN_00a10df0 SPRT_8
    //     0x7e FUN_00a12c70 SPRT_16               0x7c FUN_00a11310 SPRT_16
    //
    // Prologues identiques a ceux des familles 2 et 3 (verifies corps par corps). Trois
    // differences, toutes dans le corps :
    //
    //   - **le tampon de sommets est le PREMIER**, `&DAT_011cb138`, celui des primitives plates —
    //     pas `&DAT_011be7e8`. Les `tu`/`tv` y tombent aux memes flottants 6 et 7 ;
    //   - **la couleur est celle du paquet**, doublee et plafonnee comme pour les plates. L'alpha
    //     vaut `AlphaParMode[abr]` en semi-transparence, 0xff en opaque (le `+ 0xff00` de
    //     `CouleurPlate`). Ghidra compose la couleur des sprites semi-transparents dans l'ordre
    //     `uVar2, uVar4, uVar3` : ce n'est qu'un nommage, le desassemblage de `FUN_00a13010` lit
    //     bien les octets 4, 5, 6 dans l'ordre r, g, b ;
    //   - **un `POLY_FT3` module n'ecrit que TROIS couleurs** (`148`, `168`, `188`), la ou son
    //     jumeau brut en ecrit quatre. Le quatrieme sommet du tampon garde la sienne.
    //
    // Les `POLY_GT3`/`GT4` modules (0x34, 0x36, 0x3c, 0x3e) n'en font pas partie : couleur par
    // sommet, autres formes (`FUN_00a0fb40`, `FUN_00a11ec0`, `FUN_00a101c0`, `FUN_00a12350`).
    // ============================================================================================

    // ---- famille 4 : semi-transparentes, modulees ----------------------------------------------

    /// <summary>Ghidra: winsys3d.c FUN_00a116e0 @ 0x00a116e0 — `POLY_FT3` semi-transparent, module (0x26).</summary>
    internal static void PolyFT3Semi(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x16);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbragePlatTexture();
        TriangleTexture(Winsys3dData.SommetsImmediats_011cb138, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c,
                        CouleurModuleeSemi(prim, page), 3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a11a90 @ 0x00a11a90 — `POLY_FT4` semi-transparent, module (0x2e).</summary>
    internal static void PolyFT4Semi(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x16);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbragePlatTexture();
        QuadTexture(Winsys3dData.SommetsImmediats_011cb138, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c, 0x20, 0x24,
                    CouleurModuleeSemi(prim, page));
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a13010 @ 0x00a13010 — `SPRT` semi-transparent, module (0x66, par <c>EmettreSprite</c>).</summary>
    internal static void SprtSemi(byte* prim)
        => SpriteSemi(prim, *(short*)(prim + 0x10), *(short*)(prim + 0x12));

    /// <summary>Ghidra: winsys3d.c FUN_00a128d0 @ 0x00a128d0 — `SPRT_8` semi-transparent, module (0x76).</summary>
    internal static void Sprt8Semi(byte* prim) => SpriteSemi(prim, 8, 8);

    /// <summary>Ghidra: winsys3d.c FUN_00a12c70 @ 0x00a12c70 — `SPRT_16` semi-transparent, module (0x7e).</summary>
    internal static void Sprt16Semi(byte* prim) => SpriteSemi(prim, 16, 16);

    private static void SpriteSemi(byte* prim, int largeur, int hauteur)
    {
        uint page = Winsys3dData.Demi_011be6ec;
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 1)) return;
        OmbragePlatTexture();
        SpriteTexture(Winsys3dData.SommetsImmediats_011cb138, prim, largeur, hauteur, CouleurModuleeSemi(prim, page));
    }

    // ---- famille 5 : opaques, modulees ---------------------------------------------------------

    /// <summary>Ghidra: winsys3d.c FUN_00a0f090 @ 0x00a0f090 — `POLY_FT3` opaque, module (0x24).</summary>
    internal static void PolyFT3(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x16), *(ushort*)(prim + 0x0e), 0);
        OmbragePlatTexture();
        TriangleTexture(Winsys3dData.SommetsImmediats_011cb138, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c,
                        CouleurPlate(prim), 3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0f570 @ 0x00a0f570 — `POLY_FT4` opaque, module (0x2c).</summary>
    internal static void PolyFT4(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x16), *(ushort*)(prim + 0x0e), 0);
        OmbragePlatTexture();
        QuadTexture(Winsys3dData.SommetsImmediats_011cb138, prim, 0x08, 0x0c, 0x10, 0x14, 0x18, 0x1c, 0x20, 0x24,
                    CouleurPlate(prim));
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a107e0 @ 0x00a107e0 — `SPRT` opaque, module (0x64, par <c>EmettreSprite</c>).</summary>
    internal static void Sprt(byte* prim)
        => SpriteModule(prim, *(short*)(prim + 0x10), *(short*)(prim + 0x12));

    /// <summary>Ghidra: winsys3d.c FUN_00a10df0 @ 0x00a10df0 — `SPRT_8` opaque, module (0x74).</summary>
    internal static void Sprt8(byte* prim) => SpriteModule(prim, 8, 8);

    /// <summary>Ghidra: winsys3d.c FUN_00a11310 @ 0x00a11310 — `SPRT_16` opaque, module (0x7c).</summary>
    internal static void Sprt16(byte* prim) => SpriteModule(prim, 16, 16);

    private static void SpriteModule(byte* prim, int largeur, int hauteur)
    {
        LierTextureOpaque(Winsys3dData.Demi_011be6ec, *(ushort*)(prim + 0x0e), 1);
        OmbragePlatTexture();
        SpriteTexture(Winsys3dData.SommetsImmediats_011cb138, prim, largeur, hauteur, CouleurPlate(prim));
    }

    /// <summary>
    /// La couleur d'une primitive semi-transparente modulee : les octets 4, 5, 6 doubles et
    /// plafonnes comme <see cref="CouleurPlate"/>, et l'alpha du mode de la page a la place de
    /// 0xff — `((alpha * 0x100 + r) * 0x100 + g) * 0x100 + b`.
    /// </summary>
    private static uint CouleurModuleeSemi(byte* prim, uint page)
        => CouleurSommet(prim, 4, Winsys3dData.AlphaParMode_00a2c5a8[(page >> 5) & 3]);

    // ============================================================================================
    // FAMILLE 6 — les primitives GOURAUD : une couleur par sommet (9 corps, 2026-09-23)
    //
    //     code        gestionnaire   primitive      D3D                sommets
    //     0x50..0x57  FUN_00a0e970   LINE_G2        2 LINELIST          2
    //     0x58..0x5b  FUN_00a0eaf0   LINE_G3        2 LINELIST          3    ** un seul segment **
    //     0x5c..0x5f  FUN_00a0ecf0   LINE_G4        2 LINELIST          3    ** un seul segment **
    //     0x30..0x33  FUN_00a0f940   POLY_G3        4 TRIANGLELIST      3
    //     0x38..0x3b  FUN_00a0ff50   POLY_G4        6 TRIANGLEFAN       4    ** couleurs 2/3 non croisees **
    //     0x34        FUN_00a0fb40   POLY_GT3       4 TRIANGLELIST      3    opaque
    //     0x3c        FUN_00a101c0   POLY_GT4       6 TRIANGLEFAN       4    opaque
    //     0x36        FUN_00a11ec0   POLY_GT3       4 TRIANGLELIST      3    semi-transparent
    //     0x3e        FUN_00a12350   POLY_GT4       6 TRIANGLEFAN       4    semi-transparent
    //
    // Tous dans le premier tampon, `&DAT_011cb138`. Les non textures prennent le prologue des
    // plates mais passent en ombrage Gouraud, etat 5 ; les textures prennent les prologues des
    // familles 2 et 3 et l'etat 7. Chaque couleur est celle de son sommet, doublee et plafonnee,
    // alpha 0xff ou `AlphaParMode[abr]`.
    //
    // --------------------------------------------------------------------------------------------
    // DEUX DEFAUTS DU BINAIRE, TRANSCRITS TELS QUELS — LB-036 et LB-037
    //
    // **`LINE_G3` et `LINE_G4` ne tracent que leur premier segment.** Les deux passent
    // `DrawPrimitive(2 LINELIST, ..., 3 sommets)` (`push 3 ; push 2` a 0x00a0ec7d et 0x00a0eed1),
    // la ou `LINE_F3`/`LINE_F4` passent `3 LINESTRIP`. Une liste de lignes de trois sommets fait
    // UNE ligne. `LINE_G4` n'ecrit d'ailleurs que trois positions, alors qu'il pose quatre couleurs.
    //
    // **`POLY_G4` croise les positions, pas les couleurs.** Pour l'eventail, le sommet D3D 2 prend
    // la position du sommet PS 3 (octets 0x20/0x22) — mais la couleur du sommet PS 2 (octet 0x14) ;
    // le sommet D3D 3, l'inverse. Le degrade sort avec deux coins echanges. `POLY_GT4`, lui, croise
    // les deux (couleur 0x28 sur le sommet D3D 2) : le defaut est propre a la variante non texturee.
    // Releve sur le binaire (0x00a1009f `mov [11CB188h]` apres les octets 14h-16h, 0x00a10175
    // `fstp [11CB178h]` depuis le mot 20h).
    // ============================================================================================

    /// <summary>Ghidra: winsys3d.c FUN_00a0e970 @ 0x00a0e970 — `LINE_G2` (0x50..0x57) : un segment degrade.</summary>
    internal static void LineG2(byte* prim)
    {
        PreparerEtatGouraud();
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, CouleurSommet(prim, 0x04, 0xff));
        PoserCouleur(v, 1, CouleurSommet(prim, 0x0c, 0xff));
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x10), Y(prim, 0x12));
        Dessiner(D3DPT_LINELIST, 2);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0eaf0 @ 0x00a0eaf0 — `LINE_G3` (0x58..0x5b). **Ne trace que le
    /// premier segment** : `LINELIST` sur trois sommets. LB-036.
    /// </summary>
    internal static void LineG3(byte* prim)
    {
        PreparerEtatGouraud();
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, CouleurSommet(prim, 0x04, 0xff));
        PoserCouleur(v, 1, CouleurSommet(prim, 0x0c, 0xff));
        PoserCouleur(v, 2, CouleurSommet(prim, 0x14, 0xff));
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x10), Y(prim, 0x12));
        PoserSommet(v, 2, X(prim, 0x18), Y(prim, 0x1a));
        Dessiner(D3DPT_LINELIST, 3);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0ecf0 @ 0x00a0ecf0 — `LINE_G4` (0x5c..0x5f). **Ne trace que le
    /// premier segment**, et n'ecrit que trois positions pour quatre couleurs. LB-036.
    /// </summary>
    internal static void LineG4(byte* prim)
    {
        PreparerEtatGouraud();
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, CouleurSommet(prim, 0x04, 0xff));
        PoserCouleur(v, 1, CouleurSommet(prim, 0x0c, 0xff));
        PoserCouleur(v, 2, CouleurSommet(prim, 0x14, 0xff));
        PoserCouleur(v, 3, CouleurSommet(prim, 0x1c, 0xff));
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x10), Y(prim, 0x12));
        PoserSommet(v, 2, X(prim, 0x18), Y(prim, 0x1a));
        Dessiner(D3DPT_LINELIST, 3);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0f940 @ 0x00a0f940 — `POLY_G3` (0x30..0x33) : un triangle degrade.</summary>
    internal static void PolyG3(byte* prim)
    {
        PreparerEtatGouraud();
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, CouleurSommet(prim, 0x04, 0xff));
        PoserCouleur(v, 1, CouleurSommet(prim, 0x0c, 0xff));
        PoserCouleur(v, 2, CouleurSommet(prim, 0x14, 0xff));
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x10), Y(prim, 0x12));
        PoserSommet(v, 2, X(prim, 0x18), Y(prim, 0x1a));
        Dessiner(D3DPT_TRIANGLELIST, 3);
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0ff50 @ 0x00a0ff50 — `POLY_G4` (0x38..0x3b) : un quadrilatere
    /// degrade. **Les couleurs des sommets 2 et 3 ne suivent pas leurs positions** — LB-037.
    /// </summary>
    internal static void PolyG4(byte* prim)
    {
        PreparerEtatGouraud();
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, CouleurSommet(prim, 0x04, 0xff));
        PoserCouleur(v, 1, CouleurSommet(prim, 0x0c, 0xff));
        PoserCouleur(v, 2, CouleurSommet(prim, 0x14, 0xff));    // couleur du sommet PS 2...
        PoserCouleur(v, 3, CouleurSommet(prim, 0x1c, 0xff));
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x10), Y(prim, 0x12));
        PoserSommet(v, 2, X(prim, 0x20), Y(prim, 0x22));        // ...position du sommet PS 3
        PoserSommet(v, 3, X(prim, 0x18), Y(prim, 0x1a));
        Dessiner(D3DPT_TRIANGLEFAN, 4);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a0fb40 @ 0x00a0fb40 — `POLY_GT3` opaque, module (0x34).</summary>
    internal static void PolyGT3(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x1a), *(ushort*)(prim + 0x0e), 0);
        OmbrageGouraudTexture();
        TriangleGouraudTexture(prim, 0xff);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a101c0 @ 0x00a101c0 — `POLY_GT4` opaque, module (0x3c).</summary>
    internal static void PolyGT4(byte* prim)
    {
        LierTextureOpaque(*(ushort*)(prim + 0x1a), *(ushort*)(prim + 0x0e), 0);
        OmbrageGouraudTexture();
        QuadGouraudTexture(prim, 0xff);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a11ec0 @ 0x00a11ec0 — `POLY_GT3` semi-transparent, module (0x36).</summary>
    internal static void PolyGT3Semi(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x1a);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbrageGouraudTexture();
        TriangleGouraudTexture(prim, Winsys3dData.AlphaParMode_00a2c5a8[(page >> 5) & 3]);
    }

    /// <summary>Ghidra: winsys3d.c FUN_00a12350 @ 0x00a12350 — `POLY_GT4` semi-transparent, module (0x3e).</summary>
    internal static void PolyGT4Semi(byte* prim)
    {
        uint page = *(ushort*)(prim + 0x1a);
        if (!LierTextureSemi(page, *(ushort*)(prim + 0x0e), 0)) return;
        OmbrageGouraudTexture();
        QuadGouraudTexture(prim, Winsys3dData.AlphaParMode_00a2c5a8[(page >> 5) & 3]);
    }

    /// <summary>
    /// Le triangle Gouraud texture : couleurs aux octets 4, 0x10, 0x1c ; (u, v) a 0xc, 0x18,
    /// 0x24 ; (x, y) a 8, 0x14, 0x20. Trois couleurs ecrites, pas quatre.
    /// </summary>
    private static void TriangleGouraudTexture(byte* prim, uint alpha)
    {
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, CouleurSommet(prim, 0x04, alpha));
        PoserUvPoly(v, 0, prim, 0x0c);
        PoserCouleur(v, 1, CouleurSommet(prim, 0x10, alpha));
        PoserUvPoly(v, 1, prim, 0x18);
        PoserCouleur(v, 2, CouleurSommet(prim, 0x1c, alpha));
        PoserUvPoly(v, 2, prim, 0x24);
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x14), Y(prim, 0x16));
        PoserSommet(v, 2, X(prim, 0x20), Y(prim, 0x22));
        DessinerDepuis(v, D3DPT_TRIANGLELIST, 3);
    }

    /// <summary>
    /// Le quadrilatere Gouraud texture, en eventail : ici couleurs, (u, v) et positions des
    /// sommets PS 2 et 3 sont croises ENSEMBLE — couleur 0x28, uv 0x30, xy 0x2c sur le sommet D3D 2.
    /// </summary>
    private static void QuadGouraudTexture(byte* prim, uint alpha)
    {
        float* v = Winsys3dData.SommetsImmediats_011cb138;
        PoserCouleur(v, 0, CouleurSommet(prim, 0x04, alpha));
        PoserUvPoly(v, 0, prim, 0x0c);
        PoserCouleur(v, 1, CouleurSommet(prim, 0x10, alpha));
        PoserUvPoly(v, 1, prim, 0x18);
        PoserCouleur(v, 2, CouleurSommet(prim, 0x28, alpha));
        PoserUvPoly(v, 2, prim, 0x30);
        PoserCouleur(v, 3, CouleurSommet(prim, 0x1c, alpha));
        PoserUvPoly(v, 3, prim, 0x24);
        PoserSommet(v, 0, X(prim, 0x08), Y(prim, 0x0a));
        PoserSommet(v, 1, X(prim, 0x14), Y(prim, 0x16));
        PoserSommet(v, 2, X(prim, 0x2c), Y(prim, 0x2e));
        PoserSommet(v, 3, X(prim, 0x20), Y(prim, 0x22));
        DessinerDepuis(v, D3DPT_TRIANGLEFAN, 4);
    }

    /// <summary>
    /// Le prologue des Gouraud non textures : celui des plates (<see cref="PreparerEtatPlat"/>),
    /// sauf l'ombrage — `SHADEMODE` <c>DAT_00a2c5a4</c> (2) si <c>(etat ^ 1) &amp; 9</c>, et etat 5.
    /// </summary>
    private static void PreparerEtatGouraud()
    {
        if (Winsys3dData.PageTexture_00a2c488 != -1)
        {
            ((delegate* unmanaged[Thiscall]<void*, void*, void>)
                Winsys3dData.DeviceVtable[Device_LierTexture])(Winsys3dData.Device_011be760, null);
            Winsys3dData.PageTexture_00a2c488 = -1;
        }
        if (((Winsys3dData.EtatRendu_00a2c480 ^ 1) & 9) != 0)
            AppelDevice(Device_ShadeMode, (uint)Winsys3dData.OmbrageGouraud_00a2c5a4);
        ChoisirMelange(1);
        Winsys3dData.EtatRendu_00a2c480 = 5;
    }

    // ---- la geometrie texturee, commune aux familles 2 a 5 --------------------------------------
    //
    // Chaque primitive est decrite par les octets de ses (x, y) et de ses (u, v) dans le paquet,
    // dans l'ordre PlayStation, et par le tampon qu'elle remplit — `&DAT_011be7e8` pour les
    // brutes, `&DAT_011cb138` pour les modulees. Les quadrilateres partent en eventail avec les
    // sommets PS 2 et 3 croises, comme `POLY_F4`. Quadrilateres et sprites ecrivent toujours
    // quatre couleurs ; un triangle en ecrit quatre s'il est brut, trois s'il est module.

    private static void TriangleTexture(float* v, byte* prim, int xy0, int uv0, int xy1, int uv1, int xy2, int uv2,
                                        uint couleur, int couleurs)
    {
        PoserUvPoly(v, 0, prim, uv0);
        PoserUvPoly(v, 1, prim, uv1);
        PoserUvPoly(v, 2, prim, uv2);
        PoserCouleurs(v, couleurs, couleur);
        PoserSommet(v, 0, X(prim, xy0), Y(prim, xy0 + 2));
        PoserSommet(v, 1, X(prim, xy1), Y(prim, xy1 + 2));
        PoserSommet(v, 2, X(prim, xy2), Y(prim, xy2 + 2));
        DessinerDepuis(v, D3DPT_TRIANGLELIST, 3);
    }

    private static void QuadTexture(float* v, byte* prim, int xy0, int uv0, int xy1, int uv1, int xy2, int uv2,
                                    int xy3, int uv3, uint couleur)
    {
        PoserUvPoly(v, 0, prim, uv0);
        PoserUvPoly(v, 1, prim, uv1);
        PoserUvPoly(v, 2, prim, uv3);       // le sommet PS 3 en troisieme position de l'eventail
        PoserUvPoly(v, 3, prim, uv2);
        PoserCouleurs(v, 4, couleur);
        PoserSommet(v, 0, X(prim, xy0), Y(prim, xy0 + 2));
        PoserSommet(v, 1, X(prim, xy1), Y(prim, xy1 + 2));
        PoserSommet(v, 2, X(prim, xy3), Y(prim, xy3 + 2));
        PoserSommet(v, 3, X(prim, xy2), Y(prim, xy2 + 2));
        DessinerDepuis(v, D3DPT_TRIANGLEFAN, 4);
    }

    /// <summary>
    /// Un sprite : rectangle ecran de <paramref name="largeur"/> x <paramref name="hauteur"/> a
    /// (x, y), texture de meme taille a (u, v). Tables de texture des SPRITES (1c0/1c4), pas
    /// celles des polygones. Deroule dans le sens du contour.
    /// </summary>
    private static void SpriteTexture(float* v, byte* prim, int largeur, int hauteur, uint couleur)
    {
        uint* w = (uint*)v;
        int x = *(short*)(prim + 0x08), y = *(short*)(prim + 0x0a);
        uint u = prim[0x0c], t = prim[0x0d];

        float x0 = EcranX(x), y0 = EcranY(y);
        float x1 = EcranX(x + largeur), y1 = EcranY(y + hauteur);
        uint u0 = Winsys3dData.TexSpriteU_011cb1c0[u];
        uint t0 = Winsys3dData.TexSpriteV_011cb1c4[t];
        uint u1 = Winsys3dData.TexSpriteU_011cb1c0[(int)u + largeur];
        uint t1 = Winsys3dData.TexSpriteV_011cb1c4[(int)t + hauteur];

        PoserSommet(v, 0, x0, y0); w[0 * FlottantsParSommet + 6] = u0; w[0 * FlottantsParSommet + 7] = t0;
        PoserSommet(v, 1, x1, y0); w[1 * FlottantsParSommet + 6] = u1; w[1 * FlottantsParSommet + 7] = t0;
        PoserSommet(v, 2, x1, y1); w[2 * FlottantsParSommet + 6] = u1; w[2 * FlottantsParSommet + 7] = t1;
        PoserSommet(v, 3, x0, y1); w[3 * FlottantsParSommet + 6] = u0; w[3 * FlottantsParSommet + 7] = t1;
        PoserCouleurs(v, 4, couleur);
        DessinerDepuis(v, D3DPT_TRIANGLEFAN, 4);
    }

    // ---- le prologue et l'etat des primitives texturees ----------------------------------------

    /// <summary>
    /// Le prologue SEMI-TRANSPARENT (famille 2) : lie la page de texture <paramref name="page"/>
    /// avec sa CLUT, si l'un des trois memos dit qu'elle a change, puis choisit le melange. Rend
    /// <c>false</c> si l'un ou l'autre refuse — le corps appelant sort alors sans dessiner, c'est
    /// l'original.
    ///
    /// <paramref name="genre"/> vaut 0 pour un polygone, 1 pour un sprite ; il est compare au
    /// memo <c>DAT_00a2c48c</c> et passe en quatrieme argument de <c>FUN_00a27880</c>.
    /// </summary>
    private static bool LierTextureSemi(uint page, uint clut, int genre)
    {
        // Le mode `abr` 2 (soustractif) marque la CLUT : `or ah,80h` a 0x00a118fc.
        if ((page & 0x60) == 0x40) clut |= 0x8000;

        if (((int)page != Winsys3dData.PageTexture_00a2c488
                || (int)clut != Winsys3dData.Memo_00a2c484
                || Winsys3dData.Memo_00a2c48c != genre)
            && Winsys3dCacheTextures.LierTexture(Winsys3dData.Objet_012141d4, page, clut, 1, genre) == 0)
            return false;

        return ChoisirMelangePsx(page) != 0;
    }

    /// <summary>
    /// Le prologue OPAQUE (famille 3). Deja pose : seulement le melange, opaque force si le bit
    /// 13 de la page est pose (`shr eax,0Dh ; and eax,1` a 0x00a0f28d). Sinon la liaison, dont le
    /// retour est ignore — rien n'empeche le dessin. La CLUT n'est pas marquee.
    /// </summary>
    private static void LierTextureOpaque(uint page, uint clut, int genre)
    {
        if ((int)page == Winsys3dData.PageTexture_00a2c488
            && (int)clut == Winsys3dData.Memo_00a2c484
            && Winsys3dData.Memo_00a2c48c == genre)
            ChoisirMelange((int)((page >> 13) & 1));
        else
            Winsys3dCacheTextures.LierTexture(Winsys3dData.Objet_012141d4, page, clut, 0, genre);
    }

    /// <summary>Etat 6 : texture, ombrage plat. `SHADEMODE` 1 si l'etat courant porte un bit de 9.</summary>
    private static void OmbragePlatTexture()
    {
        if ((Winsys3dData.EtatRendu_00a2c480 & 9) != 0) AppelDevice(Device_ShadeMode, 1);
        Winsys3dData.EtatRendu_00a2c480 = 6;
    }

    /// <summary>
    /// Etat 7 : texture, ombrage Gouraud. Le test porte sur <c>etat ^ 1</c> — il ne se declenche
    /// pas quand l'etat courant est deja 1 — et la valeur posee est <c>DAT_00a2c5a4</c>, la
    /// constante 2.
    /// </summary>
    private static void OmbrageGouraudTexture()
    {
        if (((Winsys3dData.EtatRendu_00a2c480 ^ 1) & 9) != 0)
            AppelDevice(Device_ShadeMode, (uint)Winsys3dData.OmbrageGouraud_00a2c5a4);
        Winsys3dData.EtatRendu_00a2c480 = 7;
    }

    /// <summary>Blanc, avec l'alpha du mode de semi-transparence de la page : `AlphaParMode[abr] * 0x1000000 + 0xffffff`.</summary>
    private static uint CouleurBrute(uint page)
        => (uint)Winsys3dData.AlphaParMode_00a2c5a8[(page >> 5) & 3] * 0x1000000 + 0xffffff;

    /// <summary>`tu`/`tv` d'un sommet de polygone, depuis les deux octets u, v a l'octet <paramref name="octet"/> du paquet.</summary>
    private static void PoserUvPoly(float* v, int i, byte* prim, int octet)
    {
        uint* w = (uint*)v;
        w[i * FlottantsParSommet + 6] = Winsys3dData.TexPolyU_011cb1c8[prim[octet]];
        w[i * FlottantsParSommet + 7] = Winsys3dData.TexPolyV_011cb1cc[prim[octet + 1]];
    }

    /// <summary>La meme couleur sur les <paramref name="n"/> premiers sommets.</summary>
    private static void PoserCouleurs(float* v, int n, uint couleur)
    {
        for (int i = 0; i < n; i++) PoserCouleur(v, i, couleur);
    }

    /// <summary>`DrawPrimitive(type, TLVERTEX, v, nombre, 0)` sur l'un des deux tampons fixes.</summary>
    private static void DessinerDepuis(float* v, uint type, uint nombre)
        => ((delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)
            Winsys3dData.DeviceVtable[Device_DrawPrimitive])(
                Winsys3dData.Device_011be760, type, D3DVT_TLVERTEX, v, nombre, 0);

    // ============================================================================================
    // LE SPRITE TOURNE — `FUN_00a0e0c0`, opcode 0xe5 (2026-09-23)
    //
    // Pas une primitive `libgpu` : un sprite a pivot, echelle et angle, propre a ce portage. Il
    // prend le prologue des opaques (famille 3) et dessine un eventail de quatre sommets, mais ses
    // sommets vivent **sur la pile**, pas dans l'un des deux tampons fixes. Transcrit sur le
    // desassemblage (`Legacy/Raw/Winsys3d/FUN_00a0e0c0.disasm.txt`), pile x87 suivie instruction par
    // instruction : Ghidra donne les bonnes formules, mais c'est le binaire qui dit lesquelles sont
    // arrondies a 32 bits et lesquelles restent en precision etendue.
    //
    //     +0x00  mot de drapeaux : bit 31 -> ne rien dessiner ; 0x8000000 tourne ;
    //            0x400000 miroir en u ; 0x800000 miroir en v
    //     +0x04  x, y (shorts)          +0x08  largeur, hauteur (mots, nuls -> rien)
    //     +0x0c  page de texture        +0x0e  u, v (octets)
    //     +0x10  CLUT : (mot >> 5) + (mot suivant << 6)
    //     +0x18  pivot x, y (shorts)    +0x1c  echelle x, y (shorts, virgule fixe 4.12)
    //     +0x20  angle (entier, divise par 360 avant `rsin`/`rcos`)
    //
    // --------------------------------------------------------------------------------------------
    // CE QUI N'EST PAS SANS SURPRISE, ET QUE LE BINAIRE CONFIRME
    //
    // **Sans rotation, les tables d'ecran sont croisees.** Le bord gauche lit la table X pour son
    // x et la table X pour son y ; le bord droit et le bas lisent la table Y
    // (`fld [ebx+ebp*4]` avec `ebx = [0A2C4A4h]` a 0x00a0e40e). `SetShift` pose les deux tables a
    // la meme adresse, donc cela ne se voit que dans le mode ou elles different (ecran large).
    //
    // **La hauteur n'entre jamais dans la texture** : l'amplitude en v est la LARGEUR
    // (`add ecx,eax` a 0x00a0e4f3 et 0x00a0e51e, `eax` = mot +8). Un sprite non carre echantillonne
    // sa texture sur un carre. LB-038.
    //
    // **Avec rotation, les tables d'ecran ne servent pas** : le centre est `x + DecalEcranX` en
    // entier, converti en flottant, et les coins s'en ecartent de (±pivot, ±taille) * echelle
    // tournes. Les huit coordonnees sont calculees en precision etendue et arrondies a 32 bits au
    // `fstp` final ; seuls les facteurs (sinus, cosinus, echelles, et les quatre produits
    // pivot/taille par echelle) passent par la memoire en 32 bits. Le C# le reproduit en `double`
    // entre deux `float` — l'equivalent exact du x87 sous le mot de controle 53 bits de Windows.
    // Si le device Direct3D de 1998 passait le FPU en 24 bits (ce que D3D6 fait sans
    // `DDSCL_FPUSETUP` contraire), les sommes intermediaires y etaient arrondies a 32 bits ; ecart
    // d'un ulp au plus sur une coordonnee ecran.
    //
    // **`z`, `rhw` et la speculaire des quatre sommets ne sont jamais ecrits** : l'original les
    // laisse a ce que la pile contenait. Ce n'est pas reproductible. Le C# les laisse a ZERO (un
    // `stackalloc` est mis a zero), ce que le device traite comme `w = 1`. **C'est le seul ecart
    // assume du fichier** ; s'il se voit, c'est en comparant avec `OPENFORESIA_LEGACY_DRAWOTAG=1`.
    // ============================================================================================

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a0e0c0 @ 0x00a0e0c0 — le sprite a pivot, echelle et angle (opcode
    /// 0xe5). `ret 4`. Voir l'en-tete de section pour la disposition du paquet.
    /// </summary>
    internal static void SpriteTourne(byte* p)
    {
        if (*(short*)(p + 0x08) == 0 || *(short*)(p + 0x0a) == 0 || *(uint*)p >= 0x80000000) return;

        uint page = *(ushort*)(p + 0x0c);
        int clut = (*(short*)(p + 0x10) >> 5) + (*(short*)(p + 0x12) << 6);
        LierTextureOpaque(page, (uint)clut, 0);
        OmbragePlatTexture();

        // Quatre `D3DTLVERTEX` sur la pile, comme l'original — voir l'en-tete pour ce qui n'y est
        // pas ecrit.
        float* v = stackalloc float[4 * FlottantsParSommet];
        uint* w = (uint*)v;

        int largeur = *(ushort*)(p + 0x08);
        int hauteur = *(ushort*)(p + 0x0a);
        int x = *(short*)(p + 0x04), y = *(short*)(p + 0x06);
        int pivotX = *(short*)(p + 0x18), pivotY = *(short*)(p + 0x1a);

        if ((*(uint*)p & 0x8000000) == 0)
        {
            float dx = Winsys3dData.DecalEcranX_011be770;
            float dy = Winsys3dData.DecalEcranY_011be774;
            float* tx = Winsys3dData.TableEcranX_00a2c4a0;
            float* ty = Winsys3dData.TableEcranY_00a2c4a4;

            float gauche = tx[x - pivotX] + dx;
            float droite = ty[x - pivotX + largeur] + dx;     // la table Y, pour un x : le binaire
            float haut = tx[y - pivotY] + dy;                 // la table X, pour un y : le binaire
            float bas = ty[y - pivotY + hauteur] + dy;

            PoserSommet(v, 0, gauche, haut);
            PoserSommet(v, 1, droite, haut);
            PoserSommet(v, 2, droite, bas);
            PoserSommet(v, 3, gauche, bas);
        }
        else
        {
            float k = Winsys3dData.EchelleFixe_00a2a0b8;
            int angle = *(int*)(p + 0x20) / 360;
            float s = (float)((int)Winsys3dTrigonometrie.Rsin((uint)angle) * (double)k);
            float c = (float)(Winsys3dTrigonometrie.Rcos(angle) * (double)k);
            float ex = (float)(*(short*)(p + 0x1c) * (double)k);
            float ey = (float)(*(short*)(p + 0x1e) * (double)k);

            // Les quatre ecarts au centre, arrondis a 32 bits comme dans le binaire.
            float a = (float)(-pivotX * (double)ex);
            float b = (float)(-pivotY * (double)ey);
            float cc = (float)((largeur - pivotX) * (double)ex);
            float d = (float)((hauteur - pivotY) * (double)ey);
            float x0 = x + Winsys3dData.DecalEcranX_011be770;
            float y0 = y + Winsys3dData.DecalEcranY_011be774;

            PoserSommet(v, 0, (float)((double)a * c + (double)b * s + x0), (float)((double)b * c - (double)a * s + y0));
            PoserSommet(v, 1, (float)((double)cc * c + (double)b * s + x0), (float)((double)b * c - (double)cc * s + y0));
            PoserSommet(v, 2, (float)((double)cc * c + (double)d * s + x0), (float)((double)d * c - (double)cc * s + y0));
            PoserSommet(v, 3, (float)((double)a * c + (double)d * s + x0), (float)((double)d * c - (double)a * s + y0));
        }

        // Les coordonnees de texture : tables des SPRITES, miroir possible sur chaque axe, et la
        // largeur pour les deux amplitudes (LB-038).
        uint drapeaux = *(uint*)p;
        uint u = p[0x0e], t = p[0x0f];
        uint* tu = Winsys3dData.TexSpriteU_011cb1c0;
        uint* tv = Winsys3dData.TexSpriteV_011cb1c4;

        uint uNear = tu[u], uFar = tu[u + (uint)largeur];
        if ((drapeaux & 0x400000) != 0) (uNear, uFar) = (uFar, uNear);
        w[0 * FlottantsParSommet + 6] = uNear; w[3 * FlottantsParSommet + 6] = uNear;
        w[1 * FlottantsParSommet + 6] = uFar;  w[2 * FlottantsParSommet + 6] = uFar;

        uint tNear = tv[t], tFar = tv[t + (uint)largeur];
        if ((drapeaux & 0x800000) != 0) (tNear, tFar) = (tFar, tNear);
        w[0 * FlottantsParSommet + 7] = tNear; w[1 * FlottantsParSommet + 7] = tNear;
        w[2 * FlottantsParSommet + 7] = tFar;  w[3 * FlottantsParSommet + 7] = tFar;

        PoserCouleurs(v, 4, BlancOpaque);
        DessinerDepuis(v, D3DPT_TRIANGLEFAN, 4);
    }

    // ============================================================================================
    // Ce que les neuf primitives plates ont en commun
    // ============================================================================================

    /// <summary>
    /// Le prologue des neuf corps plats, ecrit en ligne neuf fois chez l'original : delier la
    /// texture si une est liee, repasser en ombrage plat si l'etat de rendu l'exige, melange
    /// opaque force, puis etat de rendu 4.
    /// </summary>
    private static void PreparerEtatPlat()
    {
        if (Winsys3dData.PageTexture_00a2c488 != -1)
        {
            ((delegate* unmanaged[Thiscall]<void*, void*, void>)
                Winsys3dData.DeviceVtable[Device_LierTexture])(Winsys3dData.Device_011be760, null);
            Winsys3dData.PageTexture_00a2c488 = -1;
        }
        if ((Winsys3dData.EtatRendu_00a2c480 & 9) != 0) AppelDevice(Device_ShadeMode, 1);
        ChoisirMelange(1);
        Winsys3dData.EtatRendu_00a2c480 = 4;
    }

    /// <summary>
    /// La couleur plate de la primitive : les trois octets 4, 5, 6 doubles et plafonnes a 0xff,
    /// alpha 0xff. La PlayStation code le blanc « neutre » a 0x80 ; Direct3D le veut a 0xff, d'ou
    /// le doublement. Le calcul est celui du binaire, `((r + 0xff00) << 8 | g) << 8 | b`.
    /// </summary>
    private static uint CouleurPlate(byte* prim) => CouleurSommet(prim, 4, 0xff);

    /// <summary>
    /// La couleur a l'octet <paramref name="octet"/> du paquet (r, g, b consecutifs), chaque
    /// composante doublee et plafonnee a 0xff (`shl eax,1 ; cmp eax,0FFh ; jl`), et l'alpha donne :
    /// `((alpha * 0x100 + r) * 0x100 + g) * 0x100 + b`. Avec <c>alpha = 0xff</c> c'est exactement
    /// le `(r + 0xff00) * 0x100…` des corps opaques.
    /// </summary>
    private static uint CouleurSommet(byte* prim, int octet, uint alpha)
    {
        uint r = (uint)prim[octet] * 2; if (r >= 0xff) r = 0xff;
        uint g = (uint)prim[octet + 1] * 2; if (g >= 0xff) g = 0xff;
        uint b = (uint)prim[octet + 2] * 2; if (b >= 0xff) b = 0xff;
        return ((alpha * 0x100 + r) * 0x100 + g) * 0x100 + b;
    }

    /// <summary>La coordonnee X du short a l'octet <paramref name="octet"/>, convertie en pixel ecran.</summary>
    private static float X(byte* prim, int octet) => EcranX(*(short*)(prim + octet));

    /// <summary>La coordonnee Y du short a l'octet <paramref name="octet"/>, convertie en pixel ecran.</summary>
    private static float Y(byte* prim, int octet) => EcranY(*(short*)(prim + octet));

    private static float EcranX(int c)
        => Winsys3dData.TableEcranX_00a2c4a0[c] + (float)Winsys3dData.DecalEcranX_011be770;

    private static float EcranY(int c)
        => Winsys3dData.TableEcranY_00a2c4a4[c] + (float)Winsys3dData.DecalEcranY_011be774;

    private static void PoserSommet(float* v, int i, float x, float y)
    {
        v[i * FlottantsParSommet + 0] = x;
        v[i * FlottantsParSommet + 1] = y;
    }

    private static void PoserCouleur(float* v, int i, uint couleur)
        => ((uint*)v)[i * FlottantsParSommet + 4] = couleur;

    private static void Dessiner(uint type, uint nombre)
        => ((delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)
            Winsys3dData.DeviceVtable[Device_DrawPrimitive])(
                Winsys3dData.Device_011be760, type, D3DVT_TLVERTEX,
                Winsys3dData.SommetsImmediats_011cb138, nombre, 0);

    /// <summary>Un appel <c>device[slot](arg)</c> a un argument, relisant le device a chaque fois comme le binaire.</summary>
    private static void AppelDevice(int slot, uint arg)
        => ((delegate* unmanaged[Thiscall]<void*, uint, void>)
            Winsys3dData.DeviceVtable[slot])(Winsys3dData.Device_011be760, arg);
}
