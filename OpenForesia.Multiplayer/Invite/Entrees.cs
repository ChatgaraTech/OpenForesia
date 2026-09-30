// Ce que l'invite peut presser : clavier, manettes XInput, et toute autre manette que Windows
// connait (API joystick de winmm) — tache 1.7, design D7.
//
// Une entree est un triplet (source, appareil, element) ; la configuration associe une entree a
// chaque bouton de la manette PlayStation. On ne lit jamais « une manette » : a chaque instant, on
// releve l'ensemble des entrees actives, et c'est tout ce dont la configuration et le jeu ont besoin.
//
//   Clavier    appareil 0     element = touche virtuelle
//   XInput     appareil 0..3  element 0..15 = bits de wButtons, 16/17 = gachettes gauche/droite,
//                             18..21 = stick gauche haut/bas/gauche/droite, 22..25 = stick droit
//   Joystick   appareil 0..15 element 0..31 = boutons, 32 + 2*axe (+1 si positif) = axes X Y Z R U V,
//                             48..51 = croix directionnelle haut/droite/bas/gauche
//
// XInput passe avant winmm : une manette Xbox paraît dans les deux, et la configuration prend la
// premiere entree qui change.

using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using OpenForesia.Multiplayer.Natif;

namespace OpenForesia.Multiplayer.Invite;

internal enum Source { Clavier = 0, XInput = 1, Joystick = 2 }

internal readonly record struct Entree(Source Source, int Appareil, int Element)
{
    [JsonIgnore]
    public string Libelle => Source switch
    {
        Source.Clavier => $"touche {NomTouche(Element)}",
        Source.XInput => $"manette Xbox {Appareil + 1}, {NomXInput(Element)}",
        _ => $"manette {Appareil + 1}, {NomJoystick(Element)}",
    };

    private static string NomTouche(int vk) => vk switch
    {
        >= 0x30 and <= 0x39 or >= 0x41 and <= 0x5A => ((char)vk).ToString(),
        0x20 => "Espace", 0x0D => "Entrée", 0x08 => "Retour", 0x09 => "Tab",
        0x25 => "Gauche", 0x26 => "Haut", 0x27 => "Droite", 0x28 => "Bas",
        0xA0 => "Maj gauche", 0xA1 => "Maj droite", 0xA2 => "Ctrl gauche", 0xA3 => "Ctrl droit",
        0xA4 => "Alt", 0xA5 => "Alt Gr",
        >= 0x60 and <= 0x69 => $"pavé {vk - 0x60}",
        >= 0x70 and <= 0x87 => $"F{vk - 0x6F}",
        _ => $"0x{vk:X2}",
    };

    private static readonly string[] BoutonsXInput =
        ["haut", "bas", "gauche", "droite", "Start", "Back", "stick gauche", "stick droit",
         "LB", "RB", "?", "?", "A", "B", "X", "Y"];

    private static string NomXInput(int e) => e switch
    {
        < 16 => BoutonsXInput[e],
        16 => "LT", 17 => "RT",
        < 22 => $"stick gauche {Directions[e - 18]}",
        _ => $"stick droit {Directions[e - 22]}",
    };

    private static readonly string[] Directions = ["haut", "bas", "gauche", "droite"];

    private static string NomJoystick(int e) => e switch
    {
        < 32 => $"bouton {e + 1}",
        < 44 => $"axe {"XYZRUV"[(e - 32) / 2]}{((e - 32) % 2 == 1 ? '+' : '-')}",
        _ => $"croix {new[] { "haut", "droite", "bas", "gauche" }[e - 48]}",
    };
}

internal static unsafe partial class Entrees
{
    // --- XInput -----------------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential)]
    private struct XINPUT_STATE { public uint Paquet; public ushort Boutons; public byte GachetteG, GachetteD; public short Lx, Ly, Rx, Ry; }

    [LibraryImport("xinput1_4.dll")] private static partial uint XInputGetState(uint utilisateur, XINPUT_STATE* etat);

    private const int SeuilGachette = 30, SeuilStick = 16000;

    // --- winmm ------------------------------------------------------------------------------------
    [StructLayout(LayoutKind.Sequential)]
    private struct JOYINFOEX
    {
        public uint Taille, Drapeaux, X, Y, Z, R, U, V, Boutons, NumeroBouton, Pov, Reserve1, Reserve2;
    }

    [LibraryImport("winmm.dll")] private static partial uint joyGetNumDevs();
    [LibraryImport("winmm.dll")] private static partial uint joyGetPosEx(uint id, JOYINFOEX* info);

    private const uint JoyReturnAll = 0xFF;
    private const uint Centre = 32767, SeuilAxe = 16000;

    [LibraryImport("user32.dll")] private static partial short GetAsyncKeyState(int vk);

    /// <summary>
    /// Toutes les entrées actives maintenant. Le clavier n'est lu que si <paramref name="fenetre"/>
    /// est au premier plan : un invité qui tape ailleurs ne joue pas.
    /// </summary>
    internal static void Relever(HashSet<Entree> actives, IntPtr fenetre)
    {
        actives.Clear();

        XINPUT_STATE x;
        for (int u = 0; u < 4; u++)
        {
            if (XInputGetState((uint)u, &x) != 0) continue;
            for (int b = 0; b < 16; b++)
                if ((x.Boutons & (1 << b)) != 0) actives.Add(new Entree(Source.XInput, u, b));
            if (x.GachetteG > SeuilGachette) actives.Add(new Entree(Source.XInput, u, 16));
            if (x.GachetteD > SeuilGachette) actives.Add(new Entree(Source.XInput, u, 17));
            Stick(actives, u, 18, x.Lx, x.Ly);
            Stick(actives, u, 22, x.Rx, x.Ry);
        }

        uint n = Math.Min(joyGetNumDevs(), 16u);
        JOYINFOEX j;
        for (uint id = 0; id < n; id++)
        {
            j = default;
            j.Taille = (uint)sizeof(JOYINFOEX);
            j.Drapeaux = JoyReturnAll;
            if (joyGetPosEx(id, &j) != 0) continue;
            for (int b = 0; b < 32; b++)
                if ((j.Boutons & (1u << b)) != 0) actives.Add(new Entree(Source.Joystick, (int)id, b));
            uint* axes = &j.X;
            for (int a = 0; a < 6; a++)
            {
                if (axes[a] + SeuilAxe < Centre) actives.Add(new Entree(Source.Joystick, (int)id, 32 + 2 * a));
                else if (axes[a] > Centre + SeuilAxe) actives.Add(new Entree(Source.Joystick, (int)id, 33 + 2 * a));
            }
            if (j.Pov != 0xFFFF)
            {
                uint p = j.Pov;                                     // centièmes de degré, 0 = haut
                if (p >= 31500 || p <= 4500) actives.Add(new Entree(Source.Joystick, (int)id, 48));
                if (p is >= 4500 and <= 13500) actives.Add(new Entree(Source.Joystick, (int)id, 49));
                if (p is >= 13500 and <= 22500) actives.Add(new Entree(Source.Joystick, (int)id, 50));
                if (p is >= 22500 and <= 31500) actives.Add(new Entree(Source.Joystick, (int)id, 51));
            }
        }

        if (Win32.GetForegroundWindow() != fenetre) return;
        for (int vk = 0x08; vk <= 0xFE; vk++)
        {
            // Maj, Ctrl et Alt génériques doublonnent leurs versions gauche/droite (0xA0..0xA5).
            if (vk is 0x10 or 0x11 or 0x12) continue;
            if ((GetAsyncKeyState(vk) & 0x8000) != 0) actives.Add(new Entree(Source.Clavier, 0, vk));
        }
    }

    private static void Stick(HashSet<Entree> actives, int u, int premier, short x, short y)
    {
        if (y > SeuilStick) actives.Add(new Entree(Source.XInput, u, premier));
        if (y < -SeuilStick) actives.Add(new Entree(Source.XInput, u, premier + 1));
        if (x < -SeuilStick) actives.Add(new Entree(Source.XInput, u, premier + 2));
        if (x > SeuilStick) actives.Add(new Entree(Source.XInput, u, premier + 3));
    }
}
