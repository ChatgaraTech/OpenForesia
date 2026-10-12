// =====================================================================
// LP-016 — des raccourcis de sort choisis par le joueur, par personnage.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-10-02-add-legacy-plus-custom-combos).
// Le jeu de 1998 a un mode combinaison : tenir SELECT, taper △ ○ ✕ □, relâcher, et le sort dont la
// fiche porte cette suite part. Les suites sont imposées et longues. Avec `[Magie]
// CombinaisonsLibres`, chaque personnage a SES raccourcis, vides au départ, saisis dans l'onglet
// magie du menu START (SELECT, les coups, SELECT) et rangés dans sa fiche, donc dans sa sauvegarde.
//
// Le rangement (design D1) : +0x1c00 + indice × 5, le trou de 256 octets entre l'inventaire
// (+0x300..+0x1bff) et les banques d'état (+0x1d00) que le jeu de 1998 ne touche pas.
//   [0]     longueur, 0..8 (0 = vide)
//   [1..4]  huit coups, un quartet chacun (le coup k : quartet bas si k est pair, haut sinon) ;
//           un coup = l'octet de saisie du mode combinaison décalé de 4 (△ 1, ✕ 2, ○ 4, □ 8)
// Zéro partout = tous vides : une ancienne sauvegarde se lit telle quelle.
// =====================================================================

using System.Runtime.InteropServices;
using Jeu = OpenForesia.Blaze.Blaze;
using Input = OpenForesia.Blaze.Input;

namespace OpenForesia.LegacyPlus;

internal static unsafe class CombinaisonsLibres
{
    private const int Debut = 0x1c00, TailleEntree = 5, SortsMax = 29, CoupsMax = 8;
    private const ushort Select = 0x0001;

    /// <summary>Deux tampons au format d'une fiche de sort : longueur en <c>+0x1f</c>, coups en <c>+0x20..</c>.</summary>
    private static readonly byte* s_PourLeLancer = (byte*)NativeMemory.AllocZeroed(0x30);
    private static readonly byte* s_PourLeMenu = (byte*)NativeMemory.AllocZeroed(0x30);

    // La saisie en cours (design D3).
    private static bool s_Saisie;
    private static byte* s_PersoSaisi;
    private static int s_IndiceSaisi;
    private static readonly byte[] s_Coups = new byte[CoupsMax];
    private static int s_NombreDeCoups;

    /// <summary>Avance à chaque image dessinée, menu gelé compris ; et la dernière image où l'onglet a été vu.</summary>
    private static long s_Image, s_VuA = -2;

    internal static void Installer()
    {
        if (!ReglagesLegacyPlus.CombinaisonsLibres) return;

        Coutures.Combinaison duSortPrecedent = Coutures.CombinaisonDuSort;
        Coutures.CombinaisonDuSort = (personnage, indice, lue) =>
        {
            duSortPrecedent(personnage, indice, lue);
            Remplir(s_PourLeLancer, personnage, indice);
            return s_PourLeLancer + 0x20;
        };

        Coutures.Combinaison afficheePrecedente = Coutures.CombinaisonAffichee;
        Coutures.CombinaisonAffichee = (personnage, indice, lue) =>
        {
            afficheePrecedente(personnage, indice, lue);
            if (s_Saisie && personnage == s_PersoSaisi && indice == s_IndiceSaisi)
            {
                s_PourLeMenu[0x1f] = (byte)s_NombreDeCoups;
                for (int k = 0; k < s_NombreDeCoups; k++) s_PourLeMenu[0x20 + k] = (byte)(s_Coups[k] << 4);
            }
            else
            {
                Remplir(s_PourLeMenu, personnage, indice);
            }
            return s_PourLeMenu;
        };

        Coutures.Onglet ongletPrecedent = Coutures.OngletSorts;
        Coutures.OngletSorts = (personnage, pad, sortVise) =>
        {
            ongletPrecedent(personnage, pad, sortVise);
            Onglet(personnage, pad, sortVise);
        };

        Coutures.Dessin dessinPrecedent = Coutures.DessinSurImpression;
        Coutures.DessinSurImpression = (otBase, curseur) =>
        {
            s_Image++;
            return dessinPrecedent(otBase, curseur);
        };

        Console.Error.WriteLine("[LEGACY++] LP-016 CombinaisonsLibres installé");
    }

    // ---- le rangement ---------------------------------------------------------------------------

    /// <summary>Le raccourci d'un sort, lu dans la fiche ; une entrée invalide se lit vide.</summary>
    private static int Lire(byte* personnage, int indice, byte* coups)
    {
        if (indice < 0 || indice >= SortsMax) return 0;
        byte* entree = personnage + Debut + indice * TailleEntree;
        int longueur = entree[0];
        if (longueur > CoupsMax) return 0;

        for (int k = 0; k < longueur; k++)
        {
            int quartet = (entree[1 + k / 2] >> ((k & 1) * 4)) & 0xf;
            if (quartet == 0) return 0;
            coups[k] = (byte)quartet;
        }
        return longueur;
    }

    private static void Ecrire(byte* personnage, int indice, byte[] coups, int longueur)
    {
        byte* entree = personnage + Debut + indice * TailleEntree;
        for (int i = 0; i < TailleEntree; i++) entree[i] = 0;
        entree[0] = (byte)longueur;
        for (int k = 0; k < longueur; k++) entree[1 + k / 2] |= (byte)(coups[k] << ((k & 1) * 4));
    }

    /// <summary>Remplit un tampon au format d'une fiche avec le raccourci du personnage.</summary>
    private static void Remplir(byte* tampon, byte* personnage, int indice)
    {
        byte* coups = stackalloc byte[CoupsMax];
        int longueur = Lire(personnage, indice, coups);
        tampon[0x1f] = (byte)longueur;
        for (int k = 0; k < longueur; k++) tampon[0x20 + k] = (byte)(coups[k] << 4);
    }

    // ---- la saisie --------------------------------------------------------------------------------

    private static void Onglet(byte* personnage, ushort* pad, int sortVise)
    {
        // L'onglet n'a pas été dessiné à l'image d'avant (menu fermé, autre onglet) : la saisie
        // en cours est oubliée, rien n'est écrit.
        if (s_Saisie && (s_VuA < s_Image - 1 || personnage != s_PersoSaisi)) s_Saisie = false;
        s_VuA = s_Image;

        ushort appuis = pad[1];

        if (!s_Saisie)
        {
            if ((appuis & Select) == 0) return;
            if (sortVise < 0 || sortVise >= 0x80 || !Appris(personnage, sortVise)) return;

            s_Saisie = true;
            s_PersoSaisi = personnage;
            s_IndiceSaisi = sortVise;
            s_NombreDeCoups = 0;
            pad[1] = 0;
            pad[3] = 0;
            Jeu.Aud.PlayUiSound38();
            return;
        }

        if ((appuis & Select) != 0)
        {
            Valider(personnage);
            s_Saisie = false;
            Jeu.Aud.PlayUiSound38();
        }
        else
        {
            // Le même codage que le mode combinaison au lancer (complément de LB-058).
            int coup = ((Input.ComboButtons(appuis) >> 8) & 0xf0) >> 4;
            if (coup != 0)
            {
                if (s_NombreDeCoups < CoupsMax)
                {
                    s_Coups[s_NombreDeCoups++] = (byte)coup;
                    Jeu.Aud.PlayUiSound3A();
                }
                else
                {
                    Jeu.Aud.PlayUiSound3B();
                }
            }
        }

        // Pendant la saisie, l'onglet ne voit aucun appui : ni validation, ni retour, ni curseur.
        pad[1] = 0;
        pad[3] = 0;
    }

    /// <summary>Écrit la saisie ; un autre sort du même personnage qui avait la même suite est vidé.</summary>
    private static void Valider(byte* personnage)
    {
        if (s_NombreDeCoups > 0)
        {
            byte* autres = stackalloc byte[CoupsMax];
            for (int j = 0; j < SortsMax; j++)
            {
                if (j == s_IndiceSaisi) continue;
                int longueur = Lire(personnage, j, autres);
                if (longueur != s_NombreDeCoups) continue;

                bool pareil = true;
                for (int k = 0; k < longueur && pareil; k++) pareil = autres[k] == s_Coups[k];
                if (pareil) Ecrire(personnage, j, s_Coups, 0);
            }
        }

        Ecrire(personnage, s_IndiceSaisi, s_Coups, s_NombreDeCoups);
    }

    /// <summary>Le sort est appris : son bit dans le masque <c>+0x160 + école × 8</c> du personnage.</summary>
    private static bool Appris(byte* personnage, int indice)
    {
        uint* masque = (uint*)(personnage + 0x160 + personnage[0x2b5] * 8);
        return (masque[indice >> 5] & (1u << (indice & 0x1f))) != 0;
    }
}
