// =====================================================================
// Les réglages d'affichage du joueur — `Affichage.ini`, à côté de l'exécutable.
//
// Hors reconstruction (legacy ++, LB-055, openspec/changes/archive/2026-09-26-simplify-display-settings) : le jeu de
// 1998 n'a pas de résolution interne, pas d'anticrénelage, pas de mode de fenêtre, et son filtrage
// ne se règle que par `Filter=` (F10 est inerte). Ces réglages-ci ne passent PAS par les menus du
// jeu ni par `OpenForesia.cfg`, dont le jeu reste le seul écrivain : ce fichier est **lu une fois,
// jamais écrit**. L'installeur en dépose un exemplaire commenté.
//
// Quatre clés, toutes dans `[Affichage]` : `Rendu`, `Mode`, `Fenetre`, `MiseAEchelle`. Les réglages
// fins de la première version (`ResolutionInterne`, `Anticrenelage`, `Filtrage3D`,
// `FiltrageInterface`) sont retirés : le rendu moderne est fixe, et `Rendu = origine` rend l'image
// d'avant LB-055. Un ancien fichier les porte encore — l'installeur n'écrase jamais celui du
// joueur — : ils sont reconnus comme retirés et ignorés.
//
// Chaque clé est validée seule. Fichier absent, clé absente ou valeur refusée : la valeur par
// défaut, et une ligne de journal par valeur refusée. Rien ici ne peut empêcher le jeu de démarrer.
//
// Analyseur écrit à la main plutôt que GetPrivateProfileStringA : le fichier est en UTF-8 (ses
// commentaires sont accentués), et la release est élaguée.
// =====================================================================

namespace OpenForesia.Blaze;

/// <summary>`[Affichage] Rendu`.</summary>
internal enum RenderStyle
{
    /// <summary>Scène à la taille affichée, MSAA 4, polygones filtrés selon `Filter=`.</summary>
    Modern,
    /// <summary>L'image d'avant LB-055 : taille logique, sans MSAA, au plus proche partout.</summary>
    Original,
}

/// <summary>`[Affichage] Mode` — le mode dans lequel la fenêtre s'ouvre.</summary>
internal enum WindowMode
{
    /// <summary>Fenêtre à cadre et barre de titre, redimensionnable.</summary>
    Windowed,
    /// <summary>Sans cadre, à la taille `Fenetre`, centrée sur le moniteur.</summary>
    Borderless,
    /// <summary>Sans cadre, sur tout le moniteur — aucun changement de mode d'affichage.</summary>
    Fullscreen,
}

/// <summary>`[Affichage] MiseAEchelle` — comment la scène est posée dans la fenêtre.</summary>
internal enum ScalingMode
{
    /// <summary>Plus grand rectangle 4:3 qui tient, rééchantillonné en douceur.</summary>
    Fitted,
    /// <summary>Plus grand facteur entier qui tient, au plus proche : pixels tous de même taille.</summary>
    Integer,
}

internal static class DisplaySettings
{
    internal const string FileName = "Affichage.ini";

    /// <summary>Échantillons du rendu moderne (design D2) ; le repli matériel reste celui du backend.</summary>
    private const int ModernSampleCount = 4;

    internal static RenderStyle Render { get { EnsureLoaded(); return s_Render; } }
    internal static WindowMode Mode { get { EnsureLoaded(); return s_Mode; } }
    internal static ScalingMode Scaling { get { EnsureLoaded(); return s_Scaling; } }

    /// <summary>
    /// `[Affichage] Fenetre` : taille de zone cliente demandée, ou (0, 0) pour `auto` — `auto`
    /// dépend du moniteur et de la taille logique, il se calcule à la création de la fenêtre.
    /// </summary>
    internal static (int Width, int Height) WindowSize { get { EnsureLoaded(); return s_WindowSize; } }

    internal static bool WindowSizeIsAuto => WindowSize.Width == 0;

    // Propriétés dérivées (design D2) : les consommateurs ne lisent jamais `Rendu` directement, pour
    // que ce que `origine` veut dire se décide ici seulement.

    /// <summary>La scène est rastérisée à la taille où elle est affichée, sinon à sa taille logique.</summary>
    internal static bool RenderAtDisplaySize => Render == RenderStyle.Modern;

    /// <summary>Échantillons demandés pour la cible de scène.</summary>
    internal static int SampleCount => Render == RenderStyle.Modern ? ModernSampleCount : 1;

    /// <summary>Les polygones que le moteur filtrait le sont ; sinon tout est au plus proche.</summary>
    internal static bool FilterPolygons => Render == RenderStyle.Modern;

    private static RenderStyle s_Render = RenderStyle.Modern;
    private static WindowMode s_Mode = WindowMode.Windowed;
    private static (int Width, int Height) s_WindowSize = (0, 0);
    private static ScalingMode s_Scaling = ScalingMode.Fitted;

    // Clés de la première version d'`Affichage.ini` (enhance-display-resolution-filtering).
    private static readonly string[] s_RetiredKeys =
        ["ResolutionInterne", "Anticrenelage", "Filtrage3D", "FiltrageInterface"];

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
        bool Is(string k) =>
            section.Equals("Affichage", StringComparison.OrdinalIgnoreCase) &&
            key.Equals(k, StringComparison.OrdinalIgnoreCase);
        bool Eq(string v) => value.Equals(v, StringComparison.OrdinalIgnoreCase);

        // Une clé retirée se reconnaît quelle que soit sa section (`Filtrage3D` et
        // `FiltrageInterface` vivaient sous `[Textures]`) : une ligne distincte de « clé inconnue »,
        // pour qu'on ne cherche pas une faute de frappe (design D4).
        if (Array.Exists(s_RetiredKeys, k => k.Equals(key, StringComparison.OrdinalIgnoreCase)))
        {
            Console.Error.WriteLine($"[AFFICHAGE] clé retirée, ignorée : [{section}] {key}");
            return;
        }

        bool accepted;
        if (Is("Rendu"))
        {
            accepted = Eq("moderne") ? Set(ref s_Render, RenderStyle.Modern)
                     : Eq("origine") && Set(ref s_Render, RenderStyle.Original);
        }
        else if (Is("Mode"))
        {
            accepted = Eq("fenetre") ? Set(ref s_Mode, WindowMode.Windowed)
                     : Eq("fenetre-sans-bordure") ? Set(ref s_Mode, WindowMode.Borderless)
                     : Eq("plein-ecran") && Set(ref s_Mode, WindowMode.Fullscreen);
        }
        else if (Is("Fenetre"))
        {
            accepted = Eq("auto") ? Set(ref s_WindowSize, (0, 0))
                     : TryParseSize(value, out var size) && Set(ref s_WindowSize, size);
        }
        else if (Is("MiseAEchelle"))
        {
            accepted = Eq("ajustee") ? Set(ref s_Scaling, ScalingMode.Fitted)
                     : Eq("entiere") && Set(ref s_Scaling, ScalingMode.Integer);
        }
        else
        {
            Console.Error.WriteLine($"[AFFICHAGE] clé inconnue ignorée : [{section}] {key}");
            return;
        }

        if (!accepted)
            Console.Error.WriteLine($"[AFFICHAGE] valeur refusée, défaut conservé : [{section}] {key} = {value}");
    }

    /// <summary>`largeurxhauteur`, deux entiers strictement positifs ; `x` ou `X`, espaces tolérés.</summary>
    private static bool TryParseSize(string value, out (int Width, int Height) size)
    {
        size = default;
        int x = value.IndexOfAny(['x', 'X']);
        if (x <= 0) return false;
        if (!int.TryParse(value[..x].Trim(), out int w) || !int.TryParse(value[(x + 1)..].Trim(), out int h))
            return false;
        if (w <= 0 || h <= 0) return false;
        size = (w, h);
        return true;
    }

    private static bool Set<T>(ref T field, T value)
    {
        field = value;
        return true;
    }

    private static string Describe() =>
        $"Rendu={s_Render} Mode={s_Mode} " +
        $"Fenetre={(s_WindowSize.Width == 0 ? "auto" : $"{s_WindowSize.Width}x{s_WindowSize.Height}")} " +
        $"MiseAEchelle={s_Scaling}";
}
