// OpenForesia — reconstruction de winsys3d.dll (1998), lue dans Legacy/Decompiled/winsys3d.c et,
// pour les constantes et les deux `fmul` que Ghidra a perdus, desassemblee dans le binaire.
//
// ============================================================================================
// LA TRIGONOMETRIE DE LA PLAYSTATION — `rsin`, `rcos`, `ratan2` de libgte.
//
// Trois exports : 450 (`rcos`), 451 (`rsin`), 457 (`ratan2`). Les ordinaux 452 et 453 sont des
// alias des deux premiers, sur la meme adresse.
//
// **Quatre mille quatre-vingt-seize pas par tour, et 4096 pour 1.0** — la convention PSX de bout
// en bout. Les deux constantes sont relevees dans le binaire et tombent juste au dernier chiffre :
//
//     0x00a2a0d8   0.0015339807878856412   = 2*pi / 4096   le pas d'echantillonnage
//     0x00a2a0e0   4096                    = l'echelle en virgule fixe 1.12
//     0x00a2a0f0   651.89864690440334      = 4096 / (2*pi)  pour ratan2
//
// **Les deux `fmul` n'etaient PAS dans la pseudo-C.** Ghidra rend `fsin(...)` puis `__ftol()` a la
// suite, ce qui donnerait un sinus tronque a zero partout. Le desassemblage montre la mise a
// l'echelle qui manquait :
//
//     fild [esp+10h] ; fmul [0A2A0D8h] ; fsin ; fmul [0A2A0E0h] ; call __ftol
//
// Sans elle, la table entiere serait faite de zeros et de plus ou moins un. C'est la raison pour
// laquelle ce lot est passe par le desassembleur plutot que par `winsys3d.c` seul.
//
// ============================================================================================
// L'ORDINAL 337 — PORTE LE 2026-09-23, AU DERNIER LOT DE `winsys3d`
//
// `InitTrigTables` (337, `FUN_00a1df00`) remplit la table a `DAT_011cb420` — **et quatre autres**.
// Il est reste natif tant que ses lecteurs l'etaient : le porter avec des tables C# aurait laisse
// celles de la DLL a zero. **L'aliasing a leve l'obstacle** : le corps porte ecrit dans la memoire
// de la DLL (`Data.cs`), la ou tout le monde lit. Voir `InitTrigTables` en fin de
// fichier : les « quatre autres » sont les tables de racines carrees de `SquareRoot0`.
//
// **La copie C# de la table de sinus reste**, pour `rsin`/`rcos`, et c'est sans danger :
// **les deux tables coexistent** parce que cette table n'est pas de l'etat
// partage mutable : elle est une FONCTION PURE de deux constantes, ecrite une fois et jamais
// modifiee. Les deux copies portent les memes valeurs par construction. La regle du LISEZMOI —
// « ne pas couper une representation en deux » — vise ce qu'on ECRIT a deux endroits ; une table
// derivee de constantes n'en est pas.
//
// **Le risque residuel est le dernier bit, et il est chiffrable.** Le 387 calcule `fsin` sur 80
// bits, `Math.Sin` sur 64 : un ecart d'un ulp pourrait changer un entier tronque si
// `sin * 4096` tombait a moins de ~1e-13 d'un entier. Les seuls indices ou c'est le cas sont 0,
// 1024, 2048 et 3072 — ou le sinus vaut exactement 0, 1, 0 et -1, donc 0, 4096, 0 et -4096 des
// deux cotes. Pour les 4 092 autres, la probabilite cumulee de tomber dans cette fenetre est de
// l'ordre de 1e-9. Non nulle, donc notee ; pas de quoi renoncer.
// ============================================================================================

using System;

namespace OpenForesia.Libraries;

internal static unsafe class Winsys3dTrigonometrie
{
    /// <summary>Le pas : un tour vaut 0x1000. `0x00a2a0d8`.</summary>
    private const double Pas = 2.0 * Math.PI / 4096.0;

    /// <summary>L'echelle en virgule fixe 1.12 : 4096 vaut 1.0. `0x00a2a0e0`.</summary>
    private const double Echelle = 4096.0;

    /// <summary>4096 / (2*pi), la reciproque du pas. `0x00a2a0f0`.</summary>
    private const double EchelleAngle = 4096.0 / (2.0 * Math.PI);

    /// <summary>
    /// &amp;DAT_011cb420 — la table de sinus.
    ///
    /// **Cinq mille cent vingt entrees, pas 4 096**, et la borne est celle du binaire : la boucle
    /// de `FUN_00a1df00` s'arrete quand le curseur atteint `0x011d0420`, soit `0x5000` octets plus
    /// loin. Les deux lectures ci-dessous masquent pourtant a `0xfff`, donc n'en atteignent que
    /// les 4 096 premieres ; le quart de tour supplementaire sert a du code natif qui indexe sans
    /// masquer. On le construit quand meme — il ne coute que vingt kilo-octets, et le jour ou un
    /// autre ordinal sera porte, la table sera deja complete.
    /// </summary>
    private static readonly int[] s_Sinus = ConstruireTable();

    private static int[] ConstruireTable()
    {
        // La premiere boucle de `InitTrigTables` (plus bas), reprise pour la copie C# que lisent
        // `rsin` et `rcos`. Meme formule, meme borne : les deux copies sont identiques par
        // construction (voir le pave de tete).
        int[] table = new int[(0x011d0420 - 0x011cb420) / 4];
        for (int i = 0; i < table.Length; i++)
            table[i] = (int)(Math.Sin(i * Pas) * Echelle);   // `__ftol` tronque vers zero
        return table;
    }

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a25c20 @ 0x00a25c20 (ordinaux 450 et 452) — `rcos`.
    ///
    /// C'est `rsin` decale d'un quart de tour : la meme table, lue a `angle + 0x400`. Le masque
    /// rend l'argument circulaire, donc aucun angle n'est hors bornes.
    /// </summary>
    internal static int Rcos(int angle) => s_Sinus[(angle + 0x400) & 0xfff];

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a25c40 @ 0x00a25c40 (ordinaux 451 et 453) — `rsin`.
    /// </summary>
    internal static uint Rsin(uint angle) => (uint)s_Sinus[angle & 0xfff];

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a25d10 @ 0x00a25d10 (ordinal 457) — `ratan2`.
    ///
    /// Le desassemblage donne l'ordre des operandes sans ambiguite :
    /// <c>fild [esp+4] ; fild [esp+8] ; fpatan</c>. Le 387 calcule <c>atan(st(1) / st(0))</c>,
    /// donc <c>atan2(premier argument, second)</c> — soit `ratan2(y, x)`, ce que la declaration
    /// d'`Interop/` porte deja. **L'inversion de ces deux arguments est un piege connu du depot** :
    /// elle a fait viser cinq corps en miroir de la diagonale, voir `HISTORY.md` au 2026-09-20.
    ///
    /// Le resultat est en pas de tour, pas en radians : 4096 pour un tour complet.
    /// </summary>
    internal static int Ratan2(int y, int x) => (int)(Math.Atan2(y, x) * EchelleAngle);

    /// <summary>
    /// Ghidra: winsys3d.c FUN_00a1df00 @ 0x00a1df00 (ordinal 337) — `InitTrigTables`, sans argument,
    /// transcrit sur le desassemblage (`Legacy/Raw/Winsys3d/FUN_00a1df00.disasm.txt`). Ecrit dans la
    /// memoire de la DLL :
    ///
    /// <list type="number">
    /// <item>le registre de rotation du GTE a zero (huit mots), la pile de matrices vide ;</item>
    /// <item>la table de sinus, 0x1400 entrees : `ftol(sin(i . 2pi/4096) . 4096)` ;</item>
    /// <item>**les quatre tables de racines de <c>SquareRoot0</c>** (442), 256 `short` chacune :
    /// `ftol(sqrt(i))`, `sqrt(i . 0x100)`, `sqrt(i . 0x10000)` et `sqrt(i . 0x1000000)` — la
    /// derniere chargee en 64 bits non signes (`fild qword` sur `[i &lt;&lt; 24 : 0]`). Ce sont ces
    /// quatre `__ftol()` dont Ghidra avait perdu les `fsqrt` ;</item>
    /// <item>la matrice unite (`0x00a2c600`) dans le registre de rotation.</item>
    /// </list>
    ///
    /// Les racines sont exactes au bit pres (`fsqrt` et `Math.Sqrt` sont correctement arrondis,
    /// l'argument est un entier exact). Le sinus porte l'ecart assume du pave de tete.
    /// </summary>
    internal static void InitTrigTables()
    {
        uint* registre = (uint*)Winsys3dData.RegistreRotation_011d0870;
        for (int i = 0; i < 8; i++) registre[i] = 0;
        Winsys3dData.ProfondeurPileMatrices_011f0ecc = 0;

        int* sinus = Winsys3dData.TableSinus_011cb420;
        for (int i = 0; i < (0x011d0420 - 0x011cb420) / 4; i++)
            sinus[i] = (int)(Math.Sin(i * Pas) * Echelle);

        ushort* petites = Winsys3dData.RacinesPetites_011d0620;
        ushort* r8 = Winsys3dData.Racines8_011d0420;
        ushort* r16 = Winsys3dData.Racines16_011f0c90;
        ushort* r24 = Winsys3dData.Racines24_011cb210;
        for (int i = 0; i < 0x100; i++)
        {
            petites[i] = (ushort)(int)Math.Sqrt(i);
            r8[i] = (ushort)(int)Math.Sqrt(i * 0x100);
            r16[i] = (ushort)(int)Math.Sqrt(i * 0x10000);
            r24[i] = (ushort)(int)Math.Sqrt((double)((ulong)(uint)(i << 24)));
        }

        uint* unite = Winsys3dData.MatriceUnite_00a2c600;
        for (int i = 0; i < 8; i++) registre[i] = unite[i];
    }
}
