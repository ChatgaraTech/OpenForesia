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
// POURQUOI L'ORDINAL 337 RESTE NATIF, ET POURQUOI CE N'EST PAS UNE MOITIE DE PORTAGE
//
// `InitTrigTables` (337, `FUN_00a1df00`) remplit la table a `DAT_011cb420` — **et quatre autres**,
// que vingt et un endroits encore natifs de `winsys3d.c` relisent. Le porter mettrait la table C#
// a jour et laisserait la table de la DLL a zero : tout le chemin d'affichage natif lirait des
// zeros. Il reste donc appele en natif, et le restera jusqu'a ce que ses consommateurs le soient.
//
// **Les deux tables coexistent, et c'est sans danger**, parce que cette table n'est pas de l'etat
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

internal static class Winsys3dTrigonometrie
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
        // Ghidra: winsys3d.c FUN_00a1df00 @ 0x00a1df00, premiere boucle — la seule des trois que
        // ce fichier reprend. Les deux suivantes remplissent quatre tables que seul du code natif
        // relit, et leurs quatre `__ftol()` ont perdu leurs arguments dans la decompilation.
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
}
