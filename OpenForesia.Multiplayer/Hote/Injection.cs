// Du mot de 16 bits d'un invite aux touches fantomes de son joueur (tache 1.6, design D2).
//
// Seuls les boutons qui changent sont frappes : un appui envoie la touche enfoncee, un relachement
// la touche relachee. Le jeu lit l'etat de son clavier a chaque image ; une touche tenue reste donc
// tenue sans rien renvoyer.

using OpenForesia.Multiplayer.Commun;
using OpenForesia.Multiplayer.Natif;

namespace OpenForesia.Multiplayer.Hote;

internal static class Injection
{
    /// <summary>
    /// Amène le joueur <paramref name="joueur"/> (2 à 4) de l'état <paramref name="applique"/> à
    /// l'état <paramref name="cible"/>, et rend l'état désormais appliqué.
    /// </summary>
    internal static ushort Appliquer(int joueur, ushort applique, ushort cible)
    {
        ushort change = (ushort)((applique ^ cible) & Pad.MasqueValide);
        if (change == 0) return applique;

        var journal = new List<string>();
        foreach (Pad.Bouton b in Pad.Boutons)
        {
            if ((change & b.Masque) == 0) continue;
            int code = ConfigTouches.CodePour(joueur, b);
            bool enfonce = (cible & b.Masque) != 0;
            bool frappe = Win32.Frapper((ushort)(code & 0x7f), (code & 0x80) != 0, relache: !enfonce);
            journal.Add($"{b.Nom} {(enfonce ? "enfoncé" : "relâché")} (DIK 0x{code:X2}{(frappe ? "" : ", SendInput REFUSÉ")})");
        }
        // Chaque changement au journal : c'est ce qui dit, à l'essai, si les appuis de l'invité
        // arrivent jusqu'à l'hôte et partent vers le jeu.
        Console.Out.WriteLine($"[HÔTE] J{joueur} : {string.Join(", ", journal)}");
        return (ushort)(cible & Pad.MasqueValide);
    }
}
