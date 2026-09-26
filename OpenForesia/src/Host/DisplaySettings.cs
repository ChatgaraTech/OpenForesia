// =====================================================================
// Les réglages d'affichage du joueur — `Affichage.ini`, à côté de l'exécutable.
//
// Hors reconstruction (legacy ++, LB-055, openspec/changes/enhance-display-resolution-filtering) :
// le jeu de 1998 n'a pas de résolution interne, pas d'anticrénelage, et son filtrage ne se règle
// que par `Filter=` (F10 est inerte). Ces réglages-ci ne passent PAS par les menus du jeu ni par
// `OpenForesia.cfg`, dont le jeu reste le seul écrivain : ce fichier est **lu une fois, jamais
// écrit**. L'installeur en dépose un exemplaire commenté ; un configurateur l'écrira peut-être un
// jour.
//
// Chaque clé est validée seule. Fichier absent, clé absente ou valeur refusée : la valeur par
// défaut, et une ligne de journal par valeur refusée. Rien ici ne peut empêcher le jeu de démarrer.
//
// Analyseur écrit à la main plutôt que GetPrivateProfileStringA : le fichier est en UTF-8 (ses
// commentaires sont accentués), et la release est élaguée.
// =====================================================================

namespace OpenForesia.Blaze;

/// <summary>`[Affichage] MiseAEchelle` — comment la scène est posée dans la fenêtre.</summary>
internal enum ScalingMode
{
    /// <summary>Plus grand rectangle 4:3 qui tient, rééchantillonné en douceur.</summary>
    Fitted,
    /// <summary>Plus grand facteur entier qui tient, au plus proche : pixels tous de même taille.</summary>
    Integer,
}

/// <summary>`[Textures] Filtrage3D`.</summary>
internal enum Texture3dFilter
{
    /// <summary>Ce que le moteur d'origine demandait : `Filter=` et son choix par texture.</summary>
    Game,
    /// <summary>Toujours au plus proche.</summary>
    Nearest,
    /// <summary>Toujours bilinéaire, quel que soit `Filter=`.</summary>
    Bilinear,
}

/// <summary>`[Textures] FiltrageInterface` — les textures que le moteur ne filtrait jamais.</summary>
internal enum InterfaceFilter
{
    Sharp,
    Smooth,
}

internal static class DisplaySettings
{
    internal const string FileName = "Affichage.ini";

    /// <summary>Valeur de <see cref="InternalResolution"/> qui signifie « la taille affichée ».</summary>
    internal const int AutoResolution = 0;

    /// <summary>`[Affichage] ResolutionInterne` : 1..8, ou <see cref="AutoResolution"/>.</summary>
    internal static int InternalResolution { get { EnsureLoaded(); return s_InternalResolution; } }

    /// <summary>`[Affichage] Anticrenelage` : 1, 2, 4 ou 8 échantillons demandés.</summary>
    internal static int Antialiasing { get { EnsureLoaded(); return s_Antialiasing; } }

    internal static ScalingMode Scaling { get { EnsureLoaded(); return s_Scaling; } }
    internal static Texture3dFilter Filter3d { get { EnsureLoaded(); return s_Filter3d; } }
    internal static InterfaceFilter FilterInterface { get { EnsureLoaded(); return s_FilterInterface; } }

    // Défauts « modernes » (design D1). Le rendu d'avant ce changement :
    // ResolutionInterne = 1, Anticrenelage = 1, Filtrage3D = proche.
    private static int s_InternalResolution = AutoResolution;
    private static int s_Antialiasing = 4;
    private static ScalingMode s_Scaling = ScalingMode.Fitted;
    private static Texture3dFilter s_Filter3d = Texture3dFilter.Game;
    private static InterfaceFilter s_FilterInterface = InterfaceFilter.Sharp;

    private static bool s_Loaded;

    /// <summary>
    /// Lit `Affichage.ini` une fois. Appelée à la création de la fenêtre du jeu, et d'office au
    /// premier accès à un réglage si rien ne l'a fait avant.
    /// </summary>
    internal static void EnsureLoaded()
    {
        if (s_Loaded) return;
        s_Loaded = true;

        string path = Path.Combine(AppContext.BaseDirectory, FileName);
        string[] lines;
        try
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"[AFFICHAGE] {FileName} absent — réglages par défaut ({Describe()})");
                return;
            }
            lines = File.ReadAllLines(path);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[AFFICHAGE] {FileName} illisible ({ex.Message}) — réglages par défaut");
            return;
        }

        string section = "";
        foreach (string raw in lines)
        {
            string line = raw;
            int comment = line.IndexOf(';');
            if (comment >= 0) line = line[..comment];
            line = line.Trim();
            if (line.Length == 0) continue;

            if (line[0] == '[' && line[^1] == ']')
            {
                section = line[1..^1].Trim();
                continue;
            }

            int equals = line.IndexOf('=');
            if (equals <= 0) continue;
            string key = line[..equals].Trim();
            string value = line[(equals + 1)..].Trim();
            Apply(section, key, value);
        }

        Console.Error.WriteLine($"[AFFICHAGE] {FileName} lu — {Describe()}");
    }

    private static void Apply(string section, string key, string value)
    {
        bool Is(string s, string k) =>
            section.Equals(s, StringComparison.OrdinalIgnoreCase) && key.Equals(k, StringComparison.OrdinalIgnoreCase);
        bool Eq(string v) => value.Equals(v, StringComparison.OrdinalIgnoreCase);

        bool accepted;
        if (Is("Affichage", "ResolutionInterne"))
        {
            if (Eq("auto")) { s_InternalResolution = AutoResolution; accepted = true; }
            else accepted = int.TryParse(value, out int n) && n >= 1 && n <= 8 && Set(ref s_InternalResolution, n);
        }
        else if (Is("Affichage", "Anticrenelage"))
        {
            accepted = int.TryParse(value, out int n) && n is 1 or 2 or 4 or 8 && Set(ref s_Antialiasing, n);
        }
        else if (Is("Affichage", "MiseAEchelle"))
        {
            accepted = Eq("ajustee") ? Set(ref s_Scaling, ScalingMode.Fitted)
                     : Eq("entiere") && Set(ref s_Scaling, ScalingMode.Integer);
        }
        else if (Is("Textures", "Filtrage3D"))
        {
            accepted = Eq("jeu") ? Set(ref s_Filter3d, Texture3dFilter.Game)
                     : Eq("proche") ? Set(ref s_Filter3d, Texture3dFilter.Nearest)
                     : Eq("bilineaire") && Set(ref s_Filter3d, Texture3dFilter.Bilinear);
        }
        else if (Is("Textures", "FiltrageInterface"))
        {
            accepted = Eq("net") ? Set(ref s_FilterInterface, InterfaceFilter.Sharp)
                     : Eq("lisse") && Set(ref s_FilterInterface, InterfaceFilter.Smooth);
        }
        else
        {
            Console.Error.WriteLine($"[AFFICHAGE] clé inconnue ignorée : [{section}] {key}");
            return;
        }

        if (!accepted)
            Console.Error.WriteLine($"[AFFICHAGE] valeur refusée, défaut conservé : [{section}] {key} = {value}");
    }

    private static bool Set<T>(ref T field, T value)
    {
        field = value;
        return true;
    }

    private static string Describe() =>
        $"ResolutionInterne={(s_InternalResolution == AutoResolution ? "auto" : s_InternalResolution.ToString())} " +
        $"Anticrenelage={s_Antialiasing} MiseAEchelle={s_Scaling} Filtrage3D={s_Filter3d} " +
        $"FiltrageInterface={s_FilterInterface}";
}
