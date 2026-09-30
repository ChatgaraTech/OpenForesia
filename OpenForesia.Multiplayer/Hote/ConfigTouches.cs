// Les touches fantomes des joueurs 2 a 4 dans OpenForesia.cfg (tache 1.3, design D2).
//
// Le jeu lit [Controls] une fois, au demarrage : l'hote ecrit donc les touches AVANT de lancer le
// jeu. Pour les joueurs 2 a 4 (groupes 1 a 3), la table 0 recoit une touche fantome par bouton et
// rien ailleurs, la table 1 est videe ; la table 2 (boutons de manette) et tout le joueur 1 ne sont
// jamais ecrits. L'ancien contenu est sauvegarde a cote du cfg (OpenForesia.cfg.multijoueur) et remis
// a la fin de la session — ou au lancement suivant, si l'hote s'est arrete sans le faire.
//
// Les codes viennent de la mesure du 2026-09-30 (OpenForesiaMultiplayer --mesurer-touches) : les
// 42 premiers codes etendus sans touche virtuelle. Voir Documentation/Reference/OpenForesia.cfg.md.

using System.Runtime.InteropServices;
using System.Text;
using OpenForesia.Multiplayer.Commun;

namespace OpenForesia.Multiplayer.Hote;

internal static partial class ConfigTouches
{
    [LibraryImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringW", StringMarshalling = StringMarshalling.Utf16)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool WritePrivateProfileString(string section, string cle, string? valeur, string fichier);

    [LibraryImport("kernel32.dll", EntryPoint = "GetPrivateProfileIntW", StringMarshalling = StringMarshalling.Utf16)]
    private static partial int GetPrivateProfileInt(string section, string cle, int defaut, string fichier);

    private const string Section = "Controls";
    private const int ActionsParTable = 20;
    private const int Absente = int.MinValue;

    /// <summary>
    /// Les codes DirectInput des touches fantômes, par joueur invité (2, 3, 4), dans l'ordre des
    /// actions 00..08 puis 0b..0f. Un code 0x80 | o se frappe comme l'octet o, étendu.
    /// </summary>
    internal static readonly int[][] Codes =
    [
        [0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E],
        [0x8F, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x9A, 0x9B, 0x9E, 0x9F, 0xA3],
        [0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAB, 0xAC, 0xAD, 0xAF, 0xB1, 0xB3, 0xB4, 0xB9, 0xBA],
    ];

    /// <summary>Les actions [Controls] des quatorze boutons, dans l'ordre de <see cref="Codes"/>.</summary>
    private static readonly int[] Actions = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f];

    /// <summary>Le code fantôme du bouton <paramref name="bouton"/> pour le joueur <paramref name="joueur"/> (2 à 4).</summary>
    internal static int CodePour(int joueur, Pad.Bouton bouton) => Codes[joueur - 2][Array.IndexOf(Actions, bouton.Action)];

    private static string Cle(int groupe, int table, int action) => $"K{groupe:x1}{table:x1}{action:x2}";

    private static string Sauvegarde(string cfg) => cfg + ".multijoueur";

    /// <summary>
    /// Écrit les touches fantômes des joueurs 2 à 4, après avoir sauvegardé ce qu'ils avaient. Une
    /// sauvegarde restée d'une session précédente est d'abord remise : c'est elle qui porte le vrai
    /// contenu, pas le cfg, qui porte encore les touches fantômes.
    /// </summary>
    internal static void Ecrire(string cfg)
    {
        Remettre(cfg);

        var sauvegarde = new StringBuilder();
        for (int groupe = 1; groupe <= 3; groupe++)
            for (int table = 0; table <= 1; table++)
                for (int action = 0; action < ActionsParTable; action++)
                {
                    string cle = Cle(groupe, table, action);
                    int valeur = GetPrivateProfileInt(Section, cle, Absente, cfg);
                    sauvegarde.AppendLine(valeur == Absente ? $"{cle}" : $"{cle}={valeur}");
                }
        File.WriteAllText(Sauvegarde(cfg), sauvegarde.ToString(), Encoding.ASCII);

        for (int groupe = 1; groupe <= 3; groupe++)
            for (int action = 0; action < ActionsParTable; action++)
            {
                int i = Array.IndexOf(Actions, action);
                Ecrire(cfg, Cle(groupe, 0, action), i < 0 ? 0 : Codes[groupe - 1][i]);
                Ecrire(cfg, Cle(groupe, 1, action), 0);
            }
        Journal($"touches fantômes des joueurs 2 à 4 écrites dans {cfg} (ancien contenu dans {Sauvegarde(cfg)})");
    }

    /// <summary>Remet le contenu sauvegardé des joueurs 2 à 4 et supprime la sauvegarde ; rien s'il n'y en a pas.</summary>
    internal static void Remettre(string cfg)
    {
        string chemin = Sauvegarde(cfg);
        if (!File.Exists(chemin)) return;

        foreach (string ligne in File.ReadAllLines(chemin))
        {
            if (ligne.Length == 0) continue;
            int egal = ligne.IndexOf('=');
            // Une clé absente avant la session est retirée, pas écrite à 0 : le jeu y prendrait sa
            // valeur par défaut, pas « aucune touche ».
            if (egal < 0) WritePrivateProfileString(Section, ligne, null, cfg);
            else WritePrivateProfileString(Section, ligne[..egal], ligne[(egal + 1)..], cfg);
        }
        File.Delete(chemin);
        Journal($"touches des joueurs 2 à 4 remises dans {cfg}");
    }

    private static void Ecrire(string cfg, string cle, int valeur)
    {
        if (!WritePrivateProfileString(Section, cle, valeur.ToString(), cfg))
            throw new IOException($"écriture de {cle} dans {cfg} impossible");
    }

    private static void Journal(string message) => Console.Out.WriteLine($"[HÔTE] {message}");
}
