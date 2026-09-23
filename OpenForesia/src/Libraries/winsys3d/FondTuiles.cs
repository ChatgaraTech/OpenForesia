// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **transcrite sur le desassemblage** : `Legacy/Raw/Winsys3d/FUN_00a142a0.disasm.txt`.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LE FOND EN TUILES — `FUN_00a142a0`, opcode 0xe8 du repartiteur (2026-09-23)
//
// Un fond qui defile, dessine cellule par cellule : le paquet designe un JEU DE TUILES (taille de
// cellule, carte de cellules, table d'entrees), une fenetre a l'ecran et un defilement. Chaque
// cellule visible part vers le device en un eventail de quatre sommets ; la premiere et la
// derniere cellule d'une rangee (et la premiere et la derniere rangee) sont TRONQUEES par le
// defilement et la taille de la fenetre.
//
//     paquet (p = nœud + 2)                     jeu de tuiles (*(p + 0x14))
//     +0x00  drapeaux : bit 31 -> rien ;        +0x00  largeur de cellule (0 -> 256)
//            bits 24-25 -> bits 6-7 de la page  +0x01  hauteur de cellule (0 -> 256)
//     +0x04  x, y de la fenetre (shorts)        +0x02  largeur de la carte, en cellules
//     +0x08  largeur, hauteur de la fenetre     +0x04  hauteur de la carte, en cellules
//     +0x0c  defilement x, y (shorts, REECRITS) +0x08  table d'entrees, 8 octets chacune
//     +0x18  pivot x, y (shorts)                +0x0c  carte : un mot par cellule
//
//     entree de cellule : +0 u, +1 v, +2 CLUT, +4 bit 0 miroir u / bit 1 miroir v,
//                         +6 bits 0-4 de la page de texture
//
// Le defilement est **ramene dans la carte et reecrit dans le paquet** a chaque image : un
// defilement negatif devient `taille - (-d % taille)`, puis tout defilement `d % taille`. Les deux
// passent par le mot court du paquet, et c'est ce mot court qui est relu ensuite.
//
// --------------------------------------------------------------------------------------------
// CE QUI EST TRANSCRIT TEL QUEL, ET POURQUOI
//
// **Une cellule `0xffff` fige le jeu.** La lecture de la carte est une boucle
// `do { cellule = carte[i]; } while (cellule == 0xffff)` ou rien ne change entre deux tours :
// `je 00A144F5h` a 0x00a14518 revient relire le meme mot. Ce n'est pas un artefact de Ghidra.
// Une carte qui contiendrait ce code « vide » bloquerait l'image pour toujours. LB-039.
//
// **La derniere rangee avance d'une rangee PLEINE.** A la fin d'une rangee, `y` avance de la
// hauteur de la premiere rangee si elle etait tronquee, sinon d'une hauteur de cellule — jamais
// de la hauteur tronquee de la derniere. Sans effet : c'est la derniere, la boucle sort ensuite.
//
// **La couleur des quatre sommets est `0x00ffffff` : alpha NUL.** Posee une fois avant les
// boucles (`mov eax,0FFFFFFh` a 0x00a1446a). Transcrit tel quel ; son effet depend du melange
// que le prologue a choisi et du mode de modulation de texture du device.
//
// **`z`, `rhw` et la speculaire ne sont jamais ecrits**, comme pour le sprite tourne
// (`Primitives.SpriteTourne`) : les sommets sont sur la pile, et l'original y laisse ce qui s'y
// trouvait. Le C# les laisse a zero (`stackalloc`), que le device lit comme `w = 1`.
//
// **La precision flottante.** Les quatre largeurs et hauteurs tronquees sont
// `(entier / taille) * table[taille]`, calcule en precision etendue puis arrondi a 32 bits ; le
// C# le fait en `double` (l'equivalent exact sous le mot de controle 53 bits). Les avancees de
// `x` et de `y` sont des additions de deux flottants 32 bits, arrondies une fois : identiques.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dFondTuiles
{
    private const int Device_ShadeMode = 0x8c / 4;
    private const int Device_DrawPrimitive = 0xbc / 4;
    private const uint D3DPT_TRIANGLEFAN = 6;
    private const uint D3DVT_TLVERTEX = 3;
    private const int FlottantsParSommet = 8;

    /// <summary>Blanc, alpha nul — la couleur que le corps pose sur ses quatre sommets.</summary>
    private const uint BlancAlphaNul = 0x00ffffff;

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a142a0 @ 0x00a142a0 — dessine un fond en tuiles qui defile
    /// (opcode 0xe8). `ret 4`. Voir l'en-tete pour la disposition du paquet et du jeu de tuiles.
    /// </summary>
    internal static void DessinerFond(byte* p)
    {
        int hauteurPartielle = 0;                                   // [esp+38h], hors des boucles
        if ((*(uint*)p & 0x80000000) != 0) return;

        byte* jeu = *(byte**)(p + 0x14);
        int largeurCellule = jeu[0];
        int hauteurCellule = jeu[1];
        int largeurCarte = *(ushort*)(jeu + 2);
        int hauteurCarte = *(ushort*)(jeu + 4);
        if (hauteurCellule == 0) hauteurCellule = 0x100;
        if (largeurCellule == 0) largeurCellule = 0x100;
        int largeurTotale = largeurCarte * largeurCellule;
        int hauteurTotale = hauteurCarte * hauteurCellule;

        float* tx = Winsys3dData.TableEcranX_00a2c4a0;
        float* ty = Winsys3dData.TableEcranY_00a2c4a4;
        float baseX = tx[*(short*)(p + 0x04) - *(short*)(p + 0x18)];
        float baseY = ty[*(short*)(p + 0x06) - *(short*)(p + 0x1a)];

        // Le defilement, ramene dans la carte et REECRIT dans le paquet.
        short* defX = (short*)(p + 0x0c);
        if (*defX < 0) *defX = (short)(largeurTotale - -*defX % largeurTotale);
        *defX = (short)(*defX % largeurTotale);
        short* defY = (short*)(p + 0x0e);
        if (*defY < 0) *defY = (short)(hauteurTotale - -*defY % hauteurTotale);
        *defY = (short)(*defY % hauteurTotale);

        float largeurCelluleEcran = tx[largeurCellule];
        float hauteurCelluleEcran = ty[hauteurCellule];
        int dx = *defX, dy = *defY;
        int vueLargeur = *(short*)(p + 0x08);
        int vueHauteur = *(short*)(p + 0x0a);

        int premiereLargeur = largeurCellule - dx % largeurCellule;
        int premiereHauteur = hauteurCellule - dy % hauteurCellule;
        float premiereLargeurEcran = (float)((double)premiereLargeur / largeurCellule * largeurCelluleEcran);
        float premiereHauteurEcran = (float)((double)premiereHauteur / hauteurCellule * hauteurCelluleEcran);
        float derniereLargeurEcran = (float)((double)((vueLargeur - premiereLargeur) % largeurCellule)
                                             / largeurCellule * largeurCelluleEcran);
        float derniereHauteurEcran = (float)((double)((vueHauteur - premiereHauteur) % hauteurCellule)
                                             / hauteurCellule * hauteurCelluleEcran);

        int colonneDepart = dx / largeurCellule;
        int decalColonneDepart = dx % largeurCellule;
        int decalLigne = dy % hauteurCellule;
        int ligne = dy / hauteurCellule;

        // Quatre `D3DTLVERTEX` sur la pile, comme l'original — voir l'en-tete.
        float* v = stackalloc float[4 * FlottantsParSommet];
        uint* w = (uint*)v;
        for (int i = 0; i < 4; i++) w[i * FlottantsParSommet + 4] = BlancAlphaNul;

        float y = baseY;
        int resteHauteur = vueHauteur;
        while (true)
        {
            resteHauteur += decalLigne - hauteurCellule;
            if (resteHauteur < hauteurCellule)
            {
                hauteurPartielle = resteHauteur;
                if (resteHauteur <= 0) return;
            }

            float x = baseX;
            int decalColonne = decalColonneDepart;
            int colonne = colonneDepart;
            int resteLargeur = vueLargeur;
            int largeurPartielle = 0;

            while (true)
            {
                // LB-039 : une cellule 0xffff fait tourner cette boucle pour toujours.
                uint cellule;
                do
                {
                    ushort* carte = *(ushort**)(*(byte**)(p + 0x14) + 0x0c);
                    cellule = carte[ligne * largeurCarte + colonne];
                } while (cellule == 0xffff);

                resteLargeur += decalColonne - largeurCellule;
                if (resteLargeur < largeurCellule)
                {
                    largeurPartielle = resteLargeur;
                    if (resteLargeur <= 0) break;
                }

                byte* entree = *(byte**)(*(byte**)(p + 0x14) + 0x08) + cellule * 8;
                LierTexture(entree, p);

                v[0 * FlottantsParSommet + 0] = x;
                v[3 * FlottantsParSommet + 0] = x;
                v[0 * FlottantsParSommet + 1] = y;
                v[1 * FlottantsParSommet + 1] = y;
                Winsys3dData.EtatRendu_00a2c480 = 6;

                // ---- en largeur : premiere cellule tronquee, derniere tronquee, ou pleine ----
                uint* tu = Winsys3dData.TexSpriteU_011cb1c0;
                uint u = entree[0];
                bool miroirU = (entree[4] & 1) != 0;
                uint uGauche, uDroite;
                if (decalColonne != 0)
                {
                    x += premiereLargeurEcran;
                    if (miroirU) { uDroite = tu[u]; uGauche = tu[u + (uint)largeurCellule - (uint)decalColonne]; }
                    else { uGauche = tu[u + (uint)decalColonne]; uDroite = tu[u + (uint)largeurCellule]; }
                }
                else if (largeurPartielle != 0)
                {
                    x += derniereLargeurEcran;
                    if (miroirU) { uDroite = tu[u + (uint)largeurCellule - (uint)largeurPartielle]; uGauche = tu[u + (uint)largeurCellule]; }
                    else { uGauche = tu[u]; uDroite = tu[u + (uint)largeurPartielle]; }
                }
                else
                {
                    x += largeurCelluleEcran;
                    if (miroirU) { uDroite = tu[u]; uGauche = tu[u + (uint)largeurCellule]; }
                    else { uGauche = tu[u]; uDroite = tu[u + (uint)largeurCellule]; }
                }
                v[1 * FlottantsParSommet + 0] = x;
                v[2 * FlottantsParSommet + 0] = x;
                w[0 * FlottantsParSommet + 6] = uGauche; w[3 * FlottantsParSommet + 6] = uGauche;
                w[1 * FlottantsParSommet + 6] = uDroite; w[2 * FlottantsParSommet + 6] = uDroite;

                // ---- en hauteur : premiere rangee tronquee, derniere tronquee, ou pleine ----
                uint* tv = Winsys3dData.TexSpriteV_011cb1c4;
                uint t = entree[1];
                bool miroirV = (entree[4] & 2) != 0;
                float yBas;
                uint tHaut, tBas;
                if (decalLigne != 0)
                {
                    yBas = premiereHauteurEcran + y;
                    if (miroirV) { tBas = tv[t]; tHaut = tv[(uint)hauteurCellule - (uint)decalLigne + t]; }
                    else { tHaut = tv[t + (uint)decalLigne]; tBas = tv[t + (uint)hauteurCellule]; }
                }
                else if (hauteurPartielle != 0)
                {
                    yBas = derniereHauteurEcran + y;
                    if (miroirV) { tBas = tv[(uint)hauteurCellule - (uint)hauteurPartielle + t]; tHaut = tv[(uint)hauteurCellule + t]; }
                    else { tHaut = tv[t]; tBas = tv[t + (uint)hauteurPartielle]; }
                }
                else
                {
                    yBas = hauteurCelluleEcran + y;
                    if (miroirV) { tBas = tv[t]; tHaut = tv[(uint)hauteurCellule + t]; }
                    else { tHaut = tv[t]; tBas = tv[t + (uint)hauteurCellule]; }
                }
                v[2 * FlottantsParSommet + 1] = yBas;
                v[3 * FlottantsParSommet + 1] = yBas;
                w[0 * FlottantsParSommet + 7] = tHaut; w[1 * FlottantsParSommet + 7] = tHaut;
                w[2 * FlottantsParSommet + 7] = tBas;  w[3 * FlottantsParSommet + 7] = tBas;

                ((delegate* unmanaged[Thiscall]<void*, uint, uint, void*, uint, uint, void>)
                    Winsys3dData.DeviceVtable[Device_DrawPrimitive])(
                        Winsys3dData.Device_011be760, D3DPT_TRIANGLEFAN, D3DVT_TLVERTEX, v, 4, 0);

                decalColonne = 0;
                colonne++;
                if (colonne == largeurCarte) colonne = 0;
            }

            // Fin de rangee : `y` avance de la premiere hauteur si elle etait tronquee, sinon
            // d'une hauteur pleine — jamais de la derniere, voir l'en-tete.
            y += decalLigne != 0 ? premiereHauteurEcran : hauteurCelluleEcran;
            decalLigne = 0;
            ligne++;
            if (ligne == hauteurCarte) ligne = 0;
        }
    }

    /// <summary>
    /// Le prologue de chaque cellule — celui des primitives opaques (`Primitives.LierTextureOpaque`
    /// puis l'ombrage plat), a ceci pres que la page de texture est composee : bits 0-4 de
    /// l'entree, bits 6-7 pris aux bits 24-25 des drapeaux du paquet (`shr eax,12h ; and 0C0h`).
    /// Le melange force est donc `(page >> 13) &amp; 1`, toujours nul — Ghidra l'ecrit
    /// `FUN_00a03db0(0)` ; le binaire calcule bien l'expression (0x00a14579).
    /// </summary>
    private static void LierTexture(byte* entree, byte* p)
    {
        uint page = (uint)(entree[6] & 0x1f) | ((*(uint*)p >> 18) & 0xc0);
        uint clut = *(ushort*)(entree + 2);

        if ((int)page == Winsys3dData.PageTexture_00a2c488
            && (int)clut == Winsys3dData.Memo_00a2c484
            && Winsys3dData.Memo_00a2c48c == 0)
            Winsys3dPrimitives.ChoisirMelange((int)((page >> 13) & 1));
        else
            Winsys3dCacheTextures.LierTexture(Winsys3dData.Objet_012141d4, page, clut, 0, 0);

        if ((Winsys3dData.EtatRendu_00a2c480 & 9) != 0)
            ((delegate* unmanaged[Thiscall]<void*, uint, void>)
                Winsys3dData.DeviceVtable[Device_ShadeMode])(Winsys3dData.Device_011be760, 1);
    }
}
