// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et
// **verifiee au desassembleur** : `Legacy/Raw/Winsys3d/FUN_00a049c0.disasm.txt`.
//
// SOURCE DE VERITE : `winsys3d.c`. Base 0x00a00000. Voir src/Libraries/LISEZMOI.md.
//
// ============================================================================================
// LES MANETTES — `FUN_00a049c0`, 2026-09-23
//
// La DLL emule les deux manettes PlayStation avec le clavier et les joysticks de Windows. Chaque
// appel lit les deux, en compose les seize bits de boutons de la `libpad`, et les ecrit INVERSES
// (0 = presse, comme sur la console) aux octets 2 et 3 des deux tampons que le jeu a declares.
// Quatre appelants : `PutDispEnv` (657) en fin d'image, porte ; `PollMessages`, l'ordinal 137
// (qui ne veut que la valeur rendue, sans ecrire les tampons) et `FUN_00a1de50`, encore natifs —
// ils appellent la copie native, sur les memes globales (`Data.cs`).
//
//     bits    0x8000 gauche  0x4000 bas  0x2000 droite  0x1000 haut
//             0x0800, 0x0100, 0x80, 0x40, 0x20, 0x10, 8, 4, 2, 1 : les dix boutons
//
// Chaque bit est vrai si le joystick le dit OU si l'une de deux touches est enfoncee
// (`GetAsyncKeyState < 0`). Les touches sont dans une table de `.data` par manette (38 mots, voir
// `Data.TouchesManette1_00a2c258`) ; les seuils d'axe sont des globales eparses, relevees une a
// une ci-dessous. L'ordre des tests et leurs courts-circuits sont ceux du binaire : un
// `GetAsyncKeyState` qui n'est pas appele ne consomme pas le bit « presse depuis » de sa touche.
//
// Deux touches cachees avant tout : **F10** bascule `TexFilterPossible`, **F8** recharge toutes
// les textures ; chacune attend qu'on la relache, par pas de 100 ms.
//
// --------------------------------------------------------------------------------------------
// DEUX DEFAUTS DU BINAIRE, TRANSCRITS TELS QUELS (LB-043)
//
// **Le test du joystick 2 est inverse.** Pour la manette 1, les valeurs neutres remplacent la
// lecture quand `joyGetPos(0)` ECHOUE ; pour la manette 2, quand `joyGetPos(1)` REUSSIT
// (`jne` a 0x00a04cfd). Un joystick 2 branche est donc ignore ; absent, sa structure garde ce
// que le joystick 1 y a laisse, et la manette 2 le recopie.
//
// **La manette 2 herite des boutons de la manette 1.** Les deux s'accumulent dans le meme
// registre (`bx`), qui n'est pas remis a zero entre les deux : chaque bouton de la manette 1 est
// aussi presse sur la manette 2.
// ============================================================================================

using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dManettes
{
    private const int VK_F8 = 0x77;
    private const int VK_F10 = 0x79;

    /// <summary>Les dix bits de bouton, dans l'ordre des entrees de la table de touches.</summary>
    private static ushort Bouton(int i) => i switch
    {
        0 => 0x80, 1 => 0x40, 2 => 0x20, 3 => 0x10, 4 => 4,
        5 => 8, 6 => 1, 7 => 2, 8 => 0x800, _ => 0x100,
    };

    /// <summary>
    /// Les seuils d'axe et les valeurs neutres d'une manette, en adresses de `.data`. Les
    /// comparaisons sont non signees (`jb`/`ja`).
    /// </summary>
    private readonly struct Seuils
    {
        public readonly nuint Gauche, Bas, Droite, Haut, NeutreX, NeutreY;
        public Seuils(nuint gauche, nuint bas, nuint droite, nuint haut, nuint neutreX, nuint neutreY)
        { Gauche = gauche; Bas = bas; Droite = droite; Haut = haut; NeutreX = neutreX; NeutreY = neutreY; }
    }

    private static readonly Seuils s_Manette1 = new(0x00b6dbbc, 0x00b6dbb8, 0x00b6d5b4, 0x00b6d5cc, 0x00b6d5b8, 0x00b6d5bc);
    private static readonly Seuils s_Manette2 = new(0x00b6d5c8, 0x00b6dbc4, 0x00b6d5a8, 0x00b6d5d0, 0x00b6d5c0, 0x00b6d5c4);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a049c0 @ 0x00a049c0 — lit les deux manettes, `ret 4`. Avec
    /// <paramref name="tampons"/> nul, rend les bits de la manette 1 sans rien ecrire ; sinon
    /// ecrit les deux tampons et rend 0. Ne fait rien (rend 0) si les manettes sont desactivees et
    /// que les tampons sont demandes.
    /// </summary>
    internal static uint LireManettes(int tampons)
    {
        if (Winsys3dData.ManettesActives_00dedc40 == 0 && tampons != 0) return 0;

        if (Win32.GetAsyncKeyState(VK_F10) != 0)
        {
            Winsys3dData.TexFilterPossible_00a2c504 = Winsys3dData.TexFilterPossible_00a2c504 == 0 ? 1 : 0;
            do Win32.Sleep(100); while (Win32.GetAsyncKeyState(VK_F10) != 0);
        }
        if (Win32.GetAsyncKeyState(VK_F8) != 0)
        {
            Blaze.Winsys3d.ReInitializeAllTextures();
            do Win32.Sleep(100); while (Win32.GetAsyncKeyState(VK_F8) != 0);
        }

        // `JOYINFO` sur la pile, partage par les deux lectures — voir LB-043.
        uint* joy = stackalloc uint[4];
        uint bits = 0;

        if (Win32.joyGetPos(0, joy) != 0) Neutre(joy, s_Manette1);
        if (Winsys3dData.SaisieCoupee_00b6dbcc == 0)
            bits = Lire(joy, Winsys3dData.TouchesManette1_00a2c258, s_Manette1, bits);

        if (tampons == 0) return bits & 0xffff;
        Ecrire(Winsys3dData.TamponManette1_00dedc38, bits);

        // LB-043 : le test est inverse, et `bits` n'est pas remis a zero.
        if (Win32.joyGetPos(1, joy) == 0) Neutre(joy, s_Manette2);
        if (Winsys3dData.SaisieCoupee_00b6dbcc == 0)
            bits = Lire(joy, Winsys3dData.TouchesManette2_00a2c2f0, s_Manette2, bits);

        Ecrire(Winsys3dData.TamponManette2_00dedc3c, bits);
        return 0;
    }

    /// <summary>Les valeurs neutres : x et y de `.data`, aucun bouton. `wZpos` n'est pas touche.</summary>
    private static void Neutre(uint* joy, in Seuils s)
    {
        joy[0] = Winsys3dData.Mot(s.NeutreX);
        joy[1] = Winsys3dData.Mot(s.NeutreY);
        joy[3] = 0;
    }

    /// <summary>
    /// Les quatre directions puis les dix boutons, OU-es dans <paramref name="bits"/>, dans l'ordre
    /// du binaire : gauche, bas, droite, haut, puis les boutons dans l'ordre de la table.
    /// </summary>
    private static uint Lire(uint* joy, uint* t, in Seuils s, uint bits)
    {
        uint x = joy[0], y = joy[1], boutons = joy[3];

        if (x < Winsys3dData.Mot(s.Gauche) || Enfoncee(t[0x78 / 4]) || Enfoncee(t[0x7c / 4])) bits |= 0x8000;
        if (Winsys3dData.Mot(s.Bas) < y || Enfoncee(t[0x90 / 4]) || Enfoncee(t[0x94 / 4])) bits |= 0x4000;
        if (Winsys3dData.Mot(s.Droite) < x || Enfoncee(t[0x80 / 4]) || Enfoncee(t[0x84 / 4])) bits |= 0x2000;
        if (y < Winsys3dData.Mot(s.Haut) || Enfoncee(t[0x88 / 4]) || Enfoncee(t[0x8c / 4])) bits |= 0x1000;

        for (int i = 0; i < 10; i++)
        {
            uint* e = t + i * 3;
            if ((boutons & e[0]) != 0 || Enfoncee(e[1]) || Enfoncee(e[2])) bits |= Bouton(i);
        }
        return bits;
    }

    private static bool Enfoncee(uint touche) => Win32.GetAsyncKeyState((int)touche) < 0;

    /// <summary>Les seize bits, inverses, aux octets 2 (poids fort) et 3 du tampon, s'il existe.</summary>
    private static void Ecrire(byte* tampon, uint bits)
    {
        if (tampon == null) return;
        tampon[2] = (byte)~(bits >> 8);
        tampon[3] = (byte)~bits;
    }
}
