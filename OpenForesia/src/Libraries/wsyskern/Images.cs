// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c.
//
// LES IMAGES, moins le codec. Quatre corps : la correction gamma, la copie de bloc entre deux DIB,
// la palette tiree d'un DIB, et le chargement d'un bitmap de ressource.
//
// `LoadBitmapFile` (export 115) N'EST PAS ICI, et c'est deliberé : il ouvre le codec de la DLL —
// vingt corps autour de 0x10003480, partages avec `Unpack` et proteges par une section critique.
// C'est un lot a lui seul. Voir LISEZMOI.md, « ne pas couper une representation en deux ».

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Images
{
    /// <summary>
    /// Ghidra: wsyskern.c Gamma @ 0x100061c0 (export 91) — la correction gamma d'une composante.
    ///
    /// Le corps decompile est un `pow` en ligne : `log2`, `f2xm1`, `fscale` x87, autrement dit
    /// <c>2^(log2(x) / gamma)</c>, puis un `__ftol`. Les cinq constantes sont lues dans le binaire
    /// a partir de `_DAT_10049a80` ; la forme qu'elles donnent est
    /// <c>(int)(pow(valeur / 255, 1 / gamma) * 255)</c>, la correction habituelle sur huit bits.
    ///
    /// Les deux gardes de tete ne sont pas du bruit : elles reproduisent ce que le x87 faisait des
    /// cas degeneres. `valeur / 255 >= 1` ou `gamma` superieur a la borne sortent par `__ftol`
    /// sans calcul — le registre porte alors la valeur d'entree, d'ou le retour direct ; une
    /// valeur nulle ou un gamma nul prennent la meme sortie, parce que `log2(0)` vaut moins
    /// l'infini sur le 387 et que la chaine rendait zero.
    /// </summary>
    internal static byte Gamma(int valeur, double gamma)
    {
        // `_DAT_10049a80` = 1/255 ; `_DAT_10049a88` = 1.0 ; `_DAT_10049a90` = la borne de gamma ;
        // `_DAT_10049aa8` = 255 ; `_DAT_10049ab0` = 0. Relevees dans Legacy/Binaries/wsyskern.dll.
        const double Inverse255 = 1.0 / 255.0;
        const double Echelle = 255.0;

        double normalise = valeur * Inverse255;

        // fVar1 >= 1.0 || gamma > borne  ->  __ftol() sans calcul
        if (normalise >= 1.0) return (byte)valeur;

        // !(fVar1 > 0.0 && gamma >= 0.0)  ->  __ftol() sans calcul
        if (normalise <= 0.0 || gamma < 0.0) return (byte)valeur;

        // gamma == 1.0 : la branche qui saute l'exponentielle et applique l'echelle seule.
        double corrige = gamma == 1.0
            ? normalise * Echelle
            : Math.Pow(2.0, Math.Log2(normalise) / gamma) * Echelle;

        // `__ftol` tronque vers zero, ce que fait aussi la conversion C# explicite.
        return (byte)(int)corrige;
    }

    /// <summary>
    /// Ghidra: wsyskern.c DIBCopyBlock @ 0x10006580 (export 50) — copie un rectangle d'un DIB vers
    /// un autre, en respectant le sens des deux.
    ///
    /// Les deux `info` sont des <c>BITMAPINFOHEADER</c> : <c>+4</c> est la largeur, <c>+8</c> la
    /// hauteur. **Une hauteur positive designe un DIB de bas en haut** — la convention Windows —
    /// et le corps s'en sert deux fois : pour retourner l'ordonnee de depart, et pour inverser le
    /// pas de ligne. Un DIB de haut en bas (hauteur negative) est copie tel quel.
    ///
    /// Une largeur NEGATIVE demande un miroir horizontal : la boucle recule alors d'un octet a la
    /// fois cote destination. C'est pour cela que la valeur absolue est prise des le debut et que
    /// le signe est consulte separement.
    /// </summary>
    internal static int DIBCopyBlock(
        void* destinationBits, void* destinationInfo, int destinationX, int destinationY,
        uint largeur, uint hauteur,
        void* sourceBits, void* sourceInfo, int sourceX, int sourceY)
    {
        // Le pas de ligne d'un DIB est la largeur arrondie au multiple de quatre superieur.
        uint pasDestination = (uint)(*(int*)((byte*)destinationInfo + 4) + 3) & 0xfffffffc;
        uint pasSource      = (uint)(*(int*)((byte*)sourceInfo + 4) + 3) & 0xfffffffc;

        int hauteurSource = *(int*)((byte*)sourceInfo + 8);
        int hauteurDestination = *(int*)((byte*)destinationInfo + 8);

        bool destinationInversee = (int)hauteur < 0;

        // Valeurs absolues, le signe ayant ete releve juste au-dessus.
        int lignes = ((int)hauteur ^ (int)hauteur >> 0x1f) - ((int)hauteur >> 0x1f);
        uint colonnes = (uint)(((int)largeur ^ (int)largeur >> 0x1f) - ((int)largeur >> 0x1f));

        if (hauteurDestination > 0)
        {
            destinationInversee = !destinationInversee;
            destinationY = hauteurDestination - destinationY - 1;
        }
        if (hauteurSource > 0)
        {
            sourceY = hauteurSource - sourceY - 1;
        }

        byte* destination = (byte*)destinationBits + pasDestination * (uint)destinationY + destinationX;
        byte* source      = (byte*)sourceBits      + pasSource      * (uint)sourceY      + sourceX;

        if (destinationInversee) pasDestination = (uint)(-(int)pasDestination);
        if (hauteurSource > 0)   pasSource      = (uint)(-(int)pasSource);

        if ((int)largeur < 0)
        {
            // Miroir horizontal : la destination recule, la source avance.
            for (; lignes != 0; lignes--)
            {
                for (uint reste = colonnes; reste != 0; reste--)
                {
                    *destination = *source;
                    destination--;
                    source++;
                }
                destination += (int)pasDestination + (int)colonnes;
                source      += (int)(pasSource - colonnes);
            }
        }
        else
        {
            for (; lignes != 0; lignes--)
            {
                byte* lu = source;
                byte* ecrit = destination;

                // Quatre octets a la fois, puis le reste — la forme du corps d'origine, qui est
                // celle qu'un `memcpy` en ligne produisait.
                for (uint mots = colonnes >> 2; mots != 0; mots--)
                {
                    *(uint*)ecrit = *(uint*)lu;
                    lu += 4;
                    ecrit += 4;
                }
                for (uint octets = colonnes & 3; octets != 0; octets--)
                {
                    *ecrit = *lu;
                    lu++;
                    ecrit++;
                }

                destination += (int)pasDestination;
                source      += (int)pasSource;
            }
        }

        return 1;
    }

    /// <summary>
    /// Ghidra: wsyskern.c PaletteFromDIB @ 0x1000c130 (export 125) — construit un
    /// <c>LOGPALETTE</c> a partir de la table de couleurs d'un DIB.
    ///
    /// Le bloc rendu appartient a l'appelant, qui le rend par <c>Free</c>. Au-dela de huit bits par
    /// pixel il n'y a pas de table de couleurs : le compte tombe a zero et la palette est vide,
    /// pas nulle — `CreatePalette` la refusera, et c'est ce que faisait l'original.
    ///
    /// La conversion elle-meme est un echange : un <c>RGBQUAD</c> est bleu, vert, rouge, reserve ;
    /// une <c>PALETTEENTRY</c> est rouge, vert, bleu, drapeaux.
    /// </summary>
    internal static void* PaletteFromDIB(void* enTete)
    {
        ushort bitsParPixel = *(ushort*)((byte*)enTete + 0x0e);

        int couleurs = 1 << (bitsParPixel & 0x1f);
        if (bitsParPixel > 8) couleurs = 0;

        ushort* palette = (ushort*)Wsyskern.Malloc((uint)(couleurs * 4 + 4));
        palette[0] = 0x300;               // palVersion
        palette[1] = (ushort)couleurs;    // palNumEntries

        // La table de couleurs suit l'en-tete de 40 octets ; `+0x29` vise le vert de la premiere
        // entree, ce dont le corps d'origine se sert comme pivot pour atteindre les trois autres.
        byte* quad = (byte*)enTete + 0x29;
        byte* entree = (byte*)(palette + 2);

        for (int reste = couleurs; reste > 0; reste--)
        {
            entree[0] = quad[1];   // peRed   <- rgbRed
            entree[1] = quad[0];   // peGreen <- rgbGreen
            entree[2] = quad[-1];  // peBlue  <- rgbBlue
            entree[3] = 0;         // peFlags
            entree += 4;
            quad += 4;
        }

        return palette;
    }

    /// <summary>
    /// Ghidra: wsyskern.c LoadBitmapEx @ 0x1000bac0 (export 114) — charge un <c>RT_BITMAP</c> d'un
    /// module et rend le HBITMAP, en posant au passage la palette dans <paramref name="palette"/>.
    ///
    /// Le cadre SEH de l'original (`puStack_c = &DAT_1004a988`) protege exactement deux choses, que
    /// `FUN_1000bbac` libere sur le chemin d'exception comme sur le chemin normal : le DC et la
    /// ressource. D'ou le <c>finally</c> ici — ce n'est pas une precaution ajoutee, c'est le corps
    /// du gestionnaire d'origine, dont Ghidra n'a su rendre que `unaff_EBP` et `unaff_EDI`.
    ///
    /// La ressource introuvable rend zero SANS toucher a <paramref name="palette"/> : le corps
    /// d'origine saute tout le bloc, et l'appelant garde ce qu'il y avait.
    /// </summary>
    internal static IntPtr LoadBitmapEx(IntPtr module, byte* nomOuIdentifiant, IntPtr* palette)
    {
        // RT_BITMAP = 2, passe comme un MAKEINTRESOURCE.
        IntPtr info = Win32.FindResourceA(module, nomOuIdentifiant, (byte*)2);
        if (info == IntPtr.Zero) return IntPtr.Zero;

        IntPtr donnees = Win32.LoadResource(module, info);
        byte* enTete = (byte*)Win32.LockResource(donnees);
        IntPtr dc = Win32.GetDC(IntPtr.Zero);

        try
        {
            void* logique = PaletteFromDIB(enTete);
            *palette = Win32.CreatePalette(logique);
            Wsyskern.Free(logique);

            if (*palette != IntPtr.Zero)
            {
                Win32.SelectPalette(dc, *palette, 0);
                Win32.RealizePalette(dc);
            }

            // `biClrUsed` a zero veut dire « toutes les couleurs que la profondeur permet ».
            uint couleurs = *(uint*)(enTete + 32);
            if (couleurs == 0) couleurs = (uint)(1 << (*(ushort*)(enTete + 0x0e) & 0x1f));

            // Les bits suivent l'en-tete et sa table de couleurs. `biSize` est lu dans l'en-tete
            // plutot que suppose a 40 : un DIB de 1998 pouvait porter un en-tete plus long.
            uint tailleEnTete = *(uint*)enTete;
            void* bits = enTete + tailleEnTete + couleurs * 4;

            // 4 = CBM_INIT, 0 = DIB_RGB_COLORS.
            return Win32.CreateDIBitmap(dc, enTete, 4, bits, enTete, 0);
        }
        finally
        {
            // Ghidra: wsyskern.c FUN_1000bbac @ 0x1000bbac — le bloc de nettoyage du cadre SEH,
            // que Ghidra a sorti en fonction faute de pouvoir rendre ses deux registres.
            Win32.ReleaseDC(IntPtr.Zero, dc);
            Win32.FreeResource(donnees);
        }
    }
}
