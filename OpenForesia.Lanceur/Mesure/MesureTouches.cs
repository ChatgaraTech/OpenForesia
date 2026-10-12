// La mesure des touches fantomes (openspec/changes/add-multiplayer-streaming, tache 1.1, design D2).
//
// Les joueurs 2 a 4 seront pilotes par des frappes simulees sur des touches qu'aucun clavier
// francais ne porte et qui ne tapent aucun caractere. Reste a savoir lesquelles traversent Windows :
// chaque code candidat est enfonce puis relache par SendInput dans une fenetre a nous, qui lit les
// deux chemins du jeu — un clavier DirectInput ouvert comme le jeu l'ouvre (ClavierDirectInput), et
// les WM_CHAR, dont la saisie du nom d'un personnage tire ses caracteres (winsys3d, GetKBChar).
//
// Une touche est retenue si DirectInput la voit enfoncee sous un seul code, puis relachee, et
// qu'elle n'a produit aucun caractere. Il en faut 56 (4 joueurs x 14 boutons ; 42 avant que le
// joueur 1 ait les siennes, add-launcher-solo-gamepad).
//
// Surete : les codes etendus qui mettent en veille, eteignent, reveillent, lancent une application
// (navigateur, courrier, calculatrice) ou pilotent le son ne sont jamais envoyes ; la plage n'est
// frappee que tant que notre fenetre est au premier plan — sinon la mesure s'arrete, toutes touches
// relachees.

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using OpenForesia.Lanceur.Natif;

namespace OpenForesia.Lanceur.Mesure;

internal static unsafe class MesureTouches
{
    internal const int ToucheParJoueur = 14, Joueurs = 4;

    /// <summary>Un code de balayage : sur un octet, et étendu (préfixe E0) ou non. Le code DirectInput est l'octet, | 0x80 s'il est étendu.</summary>
    internal readonly record struct Code(byte Octet, bool Etendu)
    {
        public int Dik => Etendu ? 0x80 | Octet : Octet;
        public override string ToString() => Etendu ? $"E0 {Octet:X2}" : $"{Octet:X2}";
    }

    /// <summary>
    /// Les candidats. Simples : 0x55 à 0x7F sans les touches physiques (0x56, la touche « &lt; » de
    /// l'AZERTY ; 0x57 et 0x58, F11 et F12), sans 0x54 (Syst, la capture d'écran) ni 0x5E, 0x5F, 0x63
    /// (les codes d'alimentation, par prudence). Étendus : des codes E0 qu'aucun clavier courant
    /// n'attribue, loin des touches multimédia, d'alimentation et de lancement.
    /// </summary>
    internal static IEnumerable<Code> Candidats()
    {
        foreach (int c in Plage(0x55, 0x55).Concat(Plage(0x59, 0x5D)).Concat(Plage(0x60, 0x62)).Concat(Plage(0x64, 0x7F)))
            yield return new Code((byte)c, false);
        foreach (int c in Plage(0x01, 0x0F).Concat(Plage(0x11, 0x18)).Concat([0x1A, 0x1B, 0x1E, 0x1F, 0x23])
                          .Concat(Plage(0x25, 0x29)).Concat(Plage(0x2B, 0x2D)).Concat([0x2F, 0x31, 0x33, 0x34])
                          .Concat(Plage(0x39, 0x45)))
            yield return new Code((byte)c, true);
    }

    private static IEnumerable<int> Plage(int de, int a) => Enumerable.Range(de, a - de + 1);

    private sealed class Releve
    {
        public required Code Code;
        public bool Vu, Relache;
        public List<int> DikVus = new();
        public List<int> Vk = new();
        public StringBuilder Caracteres = new();
        public bool Retenu => Vu && Relache && DikVus.Count == 1 && Caracteres.Length == 0;
    }

    // Ce que la procédure de fenêtre reçoit pendant la frappe en cours.
    private static Releve? s_Courant;
    private static IntPtr s_Fenetre;

    internal static int Executer(string sortie)
    {
        s_Fenetre = CreerFenetre();
        Win32.ShowWindow(s_Fenetre, Win32.SW_SHOW);
        Win32.SetForegroundWindow(s_Fenetre);
        Attendre(300);
        if (Win32.GetForegroundWindow() != s_Fenetre)
        {
            Console.Error.WriteLine("La fenêtre de mesure n'a pas pu passer au premier plan : mesure annulée, aucune touche envoyée.");
            return 2;
        }

        var releves = new List<Releve>();
        using (var clavier = new ClavierDirectInput(s_Fenetre))
        {
            Attendre(100);
            foreach (Code code in Candidats())
            {
                if (Win32.GetForegroundWindow() != s_Fenetre)
                {
                    Console.Error.WriteLine($"La fenêtre a perdu le premier plan avant {code} : mesure arrêtée ({releves.Count} codes mesurés).");
                    break;
                }
                releves.Add(Mesurer(clavier, code));
            }
        }
        Win32.DestroyWindow(s_Fenetre);

        string rapport = Rapport(releves);
        Console.Out.Write(rapport);
        File.WriteAllText(sortie, rapport, new UTF8Encoding(true));
        Console.Out.WriteLine($"\nRapport écrit dans {Path.GetFullPath(sortie)}");
        return releves.Count(r => r.Retenu) >= ToucheParJoueur * Joueurs ? 0 : 1;
    }

    private static Releve Mesurer(ClavierDirectInput clavier, Code code)
    {
        var r = new Releve { Code = code };
        s_Courant = r;

        Win32.Frapper(code.Octet, code.Etendu, relache: false);
        Attendre(40);
        if (clavier.Lire(out var etat))
        {
            for (int i = 0; i < 256; i++)
                if ((etat[i] & 0x80) != 0) r.DikVus.Add(i);
            r.Vu = r.DikVus.Contains(code.Dik);
        }

        Win32.Frapper(code.Octet, code.Etendu, relache: true);
        Attendre(40);
        r.Relache = clavier.Lire(out etat) && (etat[code.Dik] & 0x80) == 0;

        s_Courant = null;
        return r;
    }

    /// <summary>Laisse passer <paramref name="ms"/> millisecondes en vidant la file de messages de la fenêtre.</summary>
    private static void Attendre(int ms)
    {
        var montre = Stopwatch.StartNew();
        Win32.MSG msg;
        while (montre.ElapsedMilliseconds < ms)
        {
            while (Win32.PeekMessage(&msg, IntPtr.Zero, 0, 0, Win32.PM_REMOVE))
            {
                Win32.TranslateMessage(&msg);
                Win32.DispatchMessage(&msg);
            }
            Thread.Sleep(2);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvStdcall)])]
    private static IntPtr Procedure(IntPtr hwnd, uint msg, IntPtr w, IntPtr l)
    {
        Releve? r = s_Courant;
        if (r != null)
        {
            if (msg is Win32.WM_KEYDOWN or Win32.WM_SYSKEYDOWN) r.Vk.Add((int)w);
            if (msg is Win32.WM_CHAR or Win32.WM_SYSCHAR) r.Caracteres.Append((char)(int)w);
            // Rien ne va à DefWindowProc pendant la frappe : une touche système (Alt, F10) n'ouvre
            // aucun menu de la fenêtre.
            if (msg is Win32.WM_KEYDOWN or Win32.WM_KEYUP or Win32.WM_SYSKEYDOWN or Win32.WM_SYSKEYUP
                    or Win32.WM_CHAR or Win32.WM_SYSCHAR)
                return IntPtr.Zero;
        }
        return Win32.DefWindowProc(hwnd, msg, w, l);
    }

    private static IntPtr CreerFenetre()
    {
        const string classe = "OpenForesiaMesureTouches";
        fixed (char* nom = classe)
        {
            var wc = new Win32.WNDCLASSEXW
            {
                cbSize = (uint)sizeof(Win32.WNDCLASSEXW),
                lpfnWndProc = &Procedure,
                hInstance = Win32.GetModuleHandle(IntPtr.Zero),
                hCursor = Win32.LoadCursor(IntPtr.Zero, 32512),
                hbrBackground = 6,                       // COLOR_WINDOW + 1
                lpszClassName = nom,
            };
            Win32.RegisterClassEx(&wc);
        }
        return Win32.CreateWindowEx(0, classe, "OpenForesia — mesure des touches fantômes (ne touchez pas au clavier)",
            Win32.WS_OVERLAPPEDWINDOW | Win32.WS_VISIBLE, 200, 200, 640, 160, IntPtr.Zero, IntPtr.Zero,
            Win32.GetModuleHandle(IntPtr.Zero), IntPtr.Zero);
    }

    private static string Rapport(List<Releve> releves)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"# Mesure des touches fantômes — {DateTime.Now:yyyy-MM-dd HH:mm}, disposition clavier 0x{(uint)Win32.GetKeyboardLayout(0):X8}");
        sb.AppendLine();
        sb.AppendLine("code   | DIK  | vu | relâché | DIK vus        | VK        | caractères | retenu");
        sb.AppendLine("-------|------|----|---------|----------------|-----------|------------|-------");
        foreach (var r in releves)
        {
            string car = string.Concat(r.Caracteres.ToString().Select(c => c < 0x20 ? $"\\x{(int)c:X2}" : c.ToString()));
            sb.AppendLine($"{r.Code,-6} | 0x{r.Code.Dik:X2} | {(r.Vu ? "oui" : "non"),-2} | {(r.Relache ? "oui" : "non"),-7} | "
                        + $"{string.Join(" ", r.DikVus.Select(d => $"{d:X2}")),-14} | {string.Join(" ", r.Vk.Distinct().Select(v => $"{v:X2}")),-9} | "
                        + $"{car,-10} | {(r.Retenu ? "OUI" : "")}");
        }

        var retenus = releves.Where(r => r.Retenu).ToList();
        sb.AppendLine();
        sb.AppendLine($"{retenus.Count} codes retenus sur {releves.Count} mesurés ; il en faut {ToucheParJoueur * Joueurs}.");
        sb.AppendLine("Retenus (octet, étendu) : " + string.Join(", ", retenus.Select(r => $"({r.Code.Octet:X2},{(r.Code.Etendu ? 1 : 0)})")));
        return sb.ToString();
    }
}
