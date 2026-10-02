// La manette PlayStation que le jeu attend, telle que l'invite l'envoie et que l'hote la rejoue.
//
// Le jeu fabrique pour chaque joueur un mot de 16 bits au format du pad PS1 (Engine/Input.cs,
// FoldKeyboardIntoPadState ; masques relus a 0x00564f18, Documentation/Reference/OpenForesia.cfg.md).
// Sur le reseau, le meme agencement de bits, mais ACTIF HAUT (bit pose = bouton enfonce) : un mot
// nul veut dire « rien », ce qui est aussi l'etat d'un invite qui vient d'arriver.

namespace OpenForesia.Lanceur.Commun;

internal static class Pad
{
    /// <summary>Un bouton : son nom pour l'écran de configuration, son bit, et l'action [Controls] qui le pose.</summary>
    internal readonly record struct Bouton(string Nom, ushort Masque, int Action);

    /// <summary>
    /// Les quatorze boutons, dans l'ordre où l'écran de configuration les demande. Les actions 09 et
    /// 0a du jeu ne posent aucun bit, les 10 à 13 sont des combinaisons : aucune n'est un bouton.
    /// </summary>
    internal static readonly Bouton[] Boutons =
    [
        new("CROIX", 0x4000, 0x06),
        new("ROND", 0x2000, 0x05),
        new("CARRE", 0x8000, 0x07),
        new("TRIANGLE", 0x1000, 0x04),
        new("HAUT", 0x0010, 0x0c),
        new("BAS", 0x0040, 0x0e),
        new("GAUCHE", 0x0080, 0x0f),
        new("DROITE", 0x0020, 0x0d),
        new("L1", 0x0400, 0x02),
        new("R1", 0x0800, 0x03),
        new("L2", 0x0100, 0x00),
        new("R2", 0x0200, 0x01),
        new("START", 0x0008, 0x0b),
        new("SELECT", 0x0001, 0x08),
    ];

    /// <summary>Tous les bits d'un bouton : ce qu'un mot reçu peut légitimement porter.</summary>
    internal static readonly ushort MasqueValide = (ushort)Boutons.Aggregate(0, (m, b) => m | b.Masque);
}
