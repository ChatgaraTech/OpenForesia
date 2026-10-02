// La qualite de l'image diffusee (tache 5.2) : la taille et le debit, au choix de l'hote selon son
// debit montant — chaque ami recoit le flux entier. Les tailles sont des multiples de 16 (l'encodeur
// n'a jamais a recadrer) et en 4:3, comme le jeu.

namespace OpenForesia.Lanceur.Commun;

internal sealed record Qualite(string Nom, int Largeur, int Hauteur, int Debit)
{
    internal static readonly Qualite[] Toutes =
    [
        new("Haute", 1280, 960, 8_000_000),
        new("Normale", 960, 720, 5_000_000),
        new("Économe", 640, 480, 2_000_000),
    ];

    internal static Qualite ParDefaut => Toutes[0];

    /// <summary>Ce que la liste de la fenêtre de l'hôte affiche.</summary>
    internal string Libelle => $"{Nom} — {Largeur} × {Hauteur}, {Debit / 1_000_000.0:0.#} Mbit/s par ami";
}
