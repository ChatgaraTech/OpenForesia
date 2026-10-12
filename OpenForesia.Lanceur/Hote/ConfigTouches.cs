// Les touches fantomes dans OpenForesia.cfg (add-multiplayer-streaming tache 1.3, design D2 ;
// add-launcher-solo-gamepad, design D2).
//
// Le jeu lit [Controls] une fois, au demarrage : le lanceur ecrit donc les touches AVANT de lancer le
// jeu, et seulement pour les joueurs qui en ont besoin (le joueur 1 en solo, les quatre en
// hebergement). Pour les joueurs 2 a 4 (groupes 1 a 3), la table 0 recoit une touche fantome par
// bouton et rien ailleurs, la table 1 est videe. Pour le joueur 1 (groupe 0), seule la table 1 est
// ecrite : sa table 0 est le clavier du joueur, qui doit rester actif ; il perd son clavier de
// secours pendant la session. La table 2 (boutons de manette) n'est jamais ecrite. L'ancien contenu
// est sauvegarde a cote du cfg (OpenForesia.cfg.lanceur ; OpenForesia.cfg.multijoueur avant le
// renommage, encore relu) et remis apres la fin du jeu — ou au lancement suivant du lanceur, s'il
// s'est arrete sans le faire (design D6).
//
// Les codes viennent de la mesure du 2026-09-30 (OpenForesiaLanceur --mesurer-touches) : les
// 42 premiers codes etendus sans touche virtuelle pour les joueurs 2 a 4, puis les dix codes etendus
// qui restent et quatre codes simples sans touche virtuelle pour le joueur 1. Voir
// Documentation/Reference/OpenForesia.cfg.md.

using System.Runtime.InteropServices;
using System.Text;
using OpenForesia.Lanceur.Commun;

namespace OpenForesia.Lanceur.Hote;

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
    /// Les codes DirectInput des touches fantômes, par joueur (1 à 4), dans l'ordre des actions
    /// 00..08 puis 0b..0f. Un code 0x80 | o se frappe comme l'octet o, étendu.
    /// </summary>
    internal static readonly int[][] Codes =
    [
        [0xBB, 0xBC, 0xBD, 0xBE, 0xBF, 0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0x55, 0x60, 0x61, 0x74],
        [0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E],
        [0x8F, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x9A, 0x9B, 0x9E, 0x9F, 0xA3],
        [0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAB, 0xAC, 0xAD, 0xAF, 0xB1, 0xB3, 0xB4, 0xB9, 0xBA],
    ];

    /// <summary>Les actions [Controls] des quatorze boutons, dans l'ordre de <see cref="Codes"/>.</summary>
    private static readonly int[] Actions = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f];

    /// <summary>Le code fantôme du bouton <paramref name="bouton"/> pour le joueur <paramref name="joueur"/> (1 à 4).</summary>
    internal static int CodePour(int joueur, Pad.Bouton bouton) => Codes[joueur - 1][Array.IndexOf(Actions, bouton.Action)];

    /// <summary>La table [Controls] qui reçoit les touches fantômes du joueur : la 1 pour le joueur 1, la 0 pour les autres.</summary>
    private static int TableFantome(int joueur) => joueur == 1 ? 1 : 0;

    /// <summary>Les tables du joueur que la session écrit, donc sauvegarde : la 1 seule pour le joueur 1, la 0 et la 1 pour les autres.</summary>
    private static int[] TablesEcrites(int joueur) => joueur == 1 ? [1] : [0, 1];

    private static string Cle(int groupe, int table, int action) => $"K{groupe:x1}{table:x1}{action:x2}";

    private static string Sauvegarde(string cfg) => cfg + ".lanceur";

    /// <summary>La sauvegarde du multijoueur d'avant son renommage en lanceur : relue, jamais écrite.</summary>
    private static string AncienneSauvegarde(string cfg) => cfg + ".multijoueur";

    /// <summary>
    /// Écrit les touches fantômes des <paramref name="joueurs"/> (1 à 4), après avoir sauvegardé ce
    /// qu'ils avaient. Une sauvegarde restée d'une session précédente est d'abord remise : c'est elle
    /// qui porte le vrai contenu, pas le cfg, qui porte encore les touches fantômes.
    /// </summary>
    internal static void Ecrire(string cfg, IReadOnlyCollection<int> joueurs)
    {
        Remettre(cfg);

        var sauvegarde = new StringBuilder();
        foreach (int joueur in joueurs)
            foreach (int table in TablesEcrites(joueur))
                for (int action = 0; action < ActionsParTable; action++)
                {
                    string cle = Cle(joueur - 1, table, action);
                    int valeur = GetPrivateProfileInt(Section, cle, Absente, cfg);
                    sauvegarde.AppendLine(valeur == Absente ? $"{cle}" : $"{cle}={valeur}");
                }
        File.WriteAllText(Sauvegarde(cfg), sauvegarde.ToString(), Encoding.ASCII);

        foreach (int joueur in joueurs)
            foreach (int table in TablesEcrites(joueur))
                for (int action = 0; action < ActionsParTable; action++)
                {
                    int i = Array.IndexOf(Actions, action);
                    int code = table == TableFantome(joueur) && i >= 0 ? Codes[joueur - 1][i] : 0;
                    Ecrire(cfg, Cle(joueur - 1, table, action), code);
                }
        Journal($"touches fantômes des joueurs {string.Join(", ", joueurs)} écrites dans {cfg} (ancien contenu dans {Sauvegarde(cfg)})");
    }

    /// <summary>
    /// Remet le contenu sauvegardé et supprime la sauvegarde — celle du lanceur, et celle du
    /// multijoueur d'avant le renommage ; rien s'il n'y en a pas.
    /// </summary>
    internal static void Remettre(string cfg)
    {
        // L'ancienne d'abord : si les deux existent, celle du lanceur, la plus récente, a le dernier mot.
        foreach (string chemin in new[] { AncienneSauvegarde(cfg), Sauvegarde(cfg) })
        {
            if (!File.Exists(chemin)) continue;

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
            Journal($"touches d'avant la session remises dans {cfg} (depuis {Path.GetFileName(chemin)})");
        }
    }

    private static void Ecrire(string cfg, string cle, int valeur)
    {
        if (!WritePrivateProfileString(Section, cle, valeur.ToString(), cfg))
            throw new IOException($"écriture de {cle} dans {cfg} impossible");
    }

    private static void Journal(string message) => Console.Out.WriteLine($"[LANCEUR] {message}");
}
