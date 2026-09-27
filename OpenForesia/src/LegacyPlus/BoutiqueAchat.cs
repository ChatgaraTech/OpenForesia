// =====================================================================
// LP-008, étape 3c — l'achat : la bourse débitée, l'objet rangé dans l'inventaire avec son icône.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop, design D12).
// Un objet de coffre passe par GrantChestContents_00530F20 (le choix de l'emplacement) puis
// StoreItemInInventory_005312D0 (le rangement) : les deux lisent l'objet dans le CATALOGUE DE LA
// CARTE et son icône dans la MÉMOIRE VIDÉO DE LA CARTE. Un objet acheté vient d'une autre zone et
// n'y est pas ; on a en revanche sa fiche et sa vignette (CatalogueObjets). Les deux règles sont donc
// reprises ici, à l'identique, sur ces données :
//
//   - l'emplacement (GrantChestContents) : empilement sur un emplacement de même nom court si
//     l'objet est empilable (+0x13 & 0x30), sinon le DERNIER emplacement libre ; dix pleins = refus ;
//   - le rangement (StoreItemInInventory) : la fiche recopiée à +0x300 de l'emplacement, le compteur
//     (+0x310) et le bit 0x8000 de +0x312 préservés, compteur + 1 (plafond 99) ; la palette et
//     l'image à +0x440 / +0x460, et ENVOYÉES à la place de mémoire vidéo réservée au personnage —
//     palette (n° × 16, 0x1f1 + joueur), image (0x180 + n° × 6, 0x100 + joueur × 24) — là où le
//     rangement d'origine les recopie ; le son 0x29.
//
// Écart voulu : l'objet acheté est IDENTIFIÉ (bit 0x8000 de +0x12 posé). Un objet de coffre le perd
// (&= 0x3fff) et reste à expertiser ; on sait ce qu'on achète.
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;
using Winsys3d = OpenForesia.Blaze.Winsys3d;

namespace OpenForesia.LegacyPlus;

internal static unsafe class BoutiqueAchat
{
    internal enum Resultat { Achete, PasAssezDOr, Introuvable, InventairePlein }

    private const int Emplacements = 10, Pas = 0x280;
    private const int Identifie = 0x8000;

    /// <summary>L'achat complet ; rien n'est touché s'il est refusé.</summary>
    internal static Resultat Acheter(uint* acheteur, byte* perChar, string nom, int prix)
    {
        if (*(int*)(perChar + 0x26c) < prix) return Resultat.PasAssezDOr;

        var objet = CatalogueObjets.Trouver(nom);
        if (objet == null) return Resultat.Introuvable;

        // La fiche, en copie : on ne touche jamais l'index. L'objet acheté est identifié.
        byte* fiche = stackalloc byte[0x140];
        for (int i = 0; i < 0x140; i++) fiche[i] = objet.Fiche[i];
        *(ushort*)(fiche + 0x12) |= Identifie;

        int? cible = ChoisirEmplacement(perChar, fiche);
        if (cible == null) return Resultat.InventairePlein;

        Ranger(acheteur, perChar, cible.Value, fiche, objet.Vignette);
        *(int*)(perChar + 0x26c) -= prix;
        return Resultat.Achete;
    }

    /// <summary>
    /// GrantChestContents_00530F20, « ou l'objet doit aller » : l'emplacement, ou null si les dix sont
    /// pris. Un emplacement neuf voit son compteur et ses drapeaux remis à zéro, comme à l'origine.
    /// </summary>
    private static int? ChoisirEmplacement(byte* perChar, byte* fiche)
    {
        int occupes = 0, cible = 0;
        byte* stocke = perChar + 0x300;

        for (int i = 0; i < Emplacements; i++, stocke += Pas)
        {
            int choisi = i;
            if (*stocke != 0)
            {
                int k = 0;
                while (k < 0x10 && stocke[k] == fiche[k]) k++;
                if (k == 0x10)
                {
                    if ((*(ushort*)(stocke + 0x12) & 0x8000) != 0) *(ushort*)(fiche + 0x12) |= 0x8000;
                    if ((fiche[0x13] & 0x30) != 0) return i;          // empilable : cet emplacement-ci
                }
                occupes++;
                choisi = cible;                                          // pris : on garde le dernier libre
            }
            cible = choisi;
        }

        if (occupes == Emplacements) return null;

        byte* neuf = perChar + cible * Pas;
        *(ushort*)(neuf + 0x310) = 0;
        *(ushort*)(neuf + 0x312) = 0;
        return cible;
    }

    /// <summary>StoreItemInInventory_005312D0, sur la fiche et la vignette achetées.</summary>
    private static void Ranger(uint* acheteur, byte* perChar, int cible, byte* fiche, byte[] vignette)
    {
        Jeu.Aud.PlayPositionalSound(0x29, 0x400, 0x1300, (int*)((byte*)acheteur + 0x1c));

        // La famille 0x32c9..0x32cf, en posture 4, n'entre pas dans l'inventaire : un compteur monte.
        ushort famille = *(ushort*)(fiche + 0x86);
        if ((perChar[0x150] & 7) == 4 && famille >= 0x32c9 && famille <= 0x32cf)
        {
            perChar[famille - 0x3151]++;
            return;
        }

        byte* emplacement = perChar + cible * Pas;
        ushort drapeauxGardes = *(ushort*)(emplacement + 0x312);
        ushort compteGarde = *(ushort*)(emplacement + 0x310);

        for (int i = 0; i < 0x140; i++) emplacement[0x300 + i] = fiche[i];
        for (int i = 0; i < 0x140; i++) emplacement[0x440 + i] = vignette[i];

        // L'icône à la place réservée au personnage, là où le rangement d'origine la recopie.
        int joueur = ((byte*)acheteur)[8];
        short* rect = stackalloc short[4];
        rect[0] = (short)(cible * 0x10); rect[1] = (short)(0x1f1 + joueur); rect[2] = 0x10; rect[3] = 1;
        Winsys3d.Ordinal_674(rect, emplacement + 0x440);
        rect[0] = (short)(0x180 + cible * 6); rect[1] = (short)(0x100 + joueur * 0x18); rect[2] = 6; rect[3] = 0x18;
        Winsys3d.Ordinal_674(rect, emplacement + 0x460);

        *(ushort*)(emplacement + 0x310) = compteGarde;
        *(ushort*)(emplacement + 0x312) |= (ushort)(drapeauxGardes & 0x8000);
        if (*(ushort*)(emplacement + 0x310) < 99) *(ushort*)(emplacement + 0x310) += 1;
    }
}
