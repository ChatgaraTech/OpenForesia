// =====================================================================
// LP-008 / LP-010 / LP-011 — l'écran des boutiques : le catalogue d'un marchand, ou le rachat.
//
// Hors reconstruction (legacy ++, openspec/changes/archive/2026-09-27-add-legacy-plus-inn-shop, design D6-D9).
// Depuis le 2026-09-28, un usage de l'écran de menu (EcranMenu.cs, openspec/changes/add-legacy-plus-arena,
// design D5) : une page par boutique, la bourse en en-tête, l'objet sous le curseur en aperçu, et la
// question « Ton choix se porte sur cet objet ? » avant d'acheter ou de vendre (demande de Vincent).
//
// Ne tire aucun nombre aléatoire.
// =====================================================================

using System.Globalization;

namespace OpenForesia.LegacyPlus;

internal static unsafe class BoutiqueEcran
{
    private const string Question = "Ton choix se porte sur cet objet ?";

    /// <summary>
    /// Une ligne de boutique : le libellé affiché, le nom exact de l'objet, le prix, et — pour une
    /// vente (LP-011) — la fiche et la vignette de l'exemplaire lui-même, lues dans l'emplacement.
    /// Sans fiche, l'aperçu la cherche dans l'index des objets par le nom.
    /// </summary>
    internal sealed record Ligne(string Affiche, string Nom, int Prix, byte[]? Fiche = null, byte[]? Vignette = null);

    internal static bool Ouvert => EcranMenu.Ouvert;

    internal static void Installer() => EcranMenu.Installer();

    /// <summary>
    /// Ouvre l'écran sur le catalogue d'un marchand (LP-010), déjà filtré par l'appelant. Un
    /// catalogue vide ouvre quand même l'écran, avec un message : le joueur sait pourquoi.
    /// </summary>
    internal static void Ouvrir(uint* acheteur, byte* perChar, string titre, IReadOnlyList<(string Nom, int Prix)> catalogue)
    {
        var lignes = catalogue.Select(o => new Ligne(o.Nom, o.Nom, o.Prix)).ToList();
        EcranMenu.Ouvrir(acheteur, perChar, Page(perChar, titre, () => lignes, "Rien a vendre pour toi ici.",
            ligne => Acheter(acheteur, perChar, ligne)));
        Journal($"{titre} : bourse {Bourse(perChar)}");
    }

    /// <summary>
    /// Ouvre l'écran en mode RACHAT (LP-011) : <paramref name="lister"/> donne ce que le joueur peut
    /// vendre, <paramref name="vendre"/> vend une ligne et rend le message ; la liste est recalculée
    /// après chaque vente.
    /// </summary>
    internal static void OuvrirVente(uint* vendeur, byte* perChar, string titre,
                                     Func<IReadOnlyList<Ligne>> lister, Func<Ligne, string> vendre)
    {
        EcranMenu.Ouvrir(vendeur, perChar, Page(perChar, titre, lister, "Rien que je puisse te racheter.", ligne =>
        {
            int avant = Bourse(perChar);
            string message = vendre(ligne);
            Journal($"vente : {ligne.Affiche} ({ligne.Prix}) — bourse {avant} → {Bourse(perChar)} : {message}");
            return message;
        }));
        Journal($"{titre} : bourse {Bourse(perChar)}");
    }

    private static EcranMenu.Page Page(byte* perChar, string titre, Func<IReadOnlyList<Ligne>> lister, string siVide,
                                       Func<Ligne, string> agir)
    {
        return new EcranMenu.Page
        {
            Titre = titre,
            EnTete = () => $"Bourse : {Bourse(perChar)}",
            Lister = () => lister().Select(l => new EcranMenu.Ligne(l.Affiche, l.Prix.ToString(CultureInfo.InvariantCulture), l)).ToList(),
            SiVide = siVide,
            Question = _ => Question,
            Valider = l => new EcranMenu.Suite(agir((Ligne)l.Donnee!)),
            Objet = l =>
            {
                var ligne = (Ligne)l.Donnee!;
                return ligne.Fiche != null
                    ? new CatalogueObjets.Objet(ligne.Fiche, ligne.Vignette ?? new byte[0x140])
                    : CatalogueObjets.Trouver(ligne.Nom);
            },
        };
    }

    /// <summary>Étape 3c : l'achat réel (BoutiqueAchat), et son message.</summary>
    private static string Acheter(uint* acheteur, byte* perChar, Ligne ligne)
    {
        int avant = Bourse(perChar);
        var resultat = BoutiqueAchat.Acheter(acheteur, perChar, ligne.Nom, ligne.Prix);
        Journal($"{resultat} : {ligne.Nom} ({ligne.Prix}) — bourse {avant} → {Bourse(perChar)}");
        return resultat switch
        {
            BoutiqueAchat.Resultat.Achete => $"Achete : {ligne.Nom}",
            BoutiqueAchat.Resultat.PasAssezDOr => "Pas assez d'or.",
            BoutiqueAchat.Resultat.InventairePlein => "Inventaire plein.",
            _ => "Objet introuvable dans les donnees du jeu.",
        };
    }

    private static int Bourse(byte* perChar) => *(int*)(perChar + 0x26c);

    private static void Journal(string message) => Console.Error.WriteLine($"[BOUTIQUE] {message}");
}
