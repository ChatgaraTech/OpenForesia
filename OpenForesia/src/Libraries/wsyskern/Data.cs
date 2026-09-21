// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c.
//
// LES GLOBALES DE LA DLL, et RIEN D'AUTRE.
//
// Elles vivent dans l'espace d'adressage de `wsyskern.dll` — base 0x10000000 —, pas dans le `.data`
// de `Blaze.exe`. Elles ne rejoignent donc JAMAIS `GameMemory` : ce serait exactement le defaut
// d'aliasing que `OpenForesia.Tests/MemoryLayoutTests.cs` traque, deux noms pour une adresse.
// Voir src/Libraries/LISEZMOI.md, regle 2.
//
// Les adresses sont conservees en commentaire au meme titre que les `// Ghidra:` du reste du
// depot : c'est ce qui permet de relire une ligne contre `wsyskern.c`.

namespace OpenForesia.Libraries;

internal static unsafe class WsyskernData
{
    // =====================================================================
    // L'ETAT DU CODEC.
    //
    // Tout ce qui suit est protege chez l'original par la section critique `DAT_10057e48`, prise
    // par `Unpack` et par le chargeur de bitmap, relachee par leurs blocs de nettoyage. C'est UN
    // SEUL decodeur global, non reentrant : deux decompressions simultanees se marcheraient
    // dessus. Le verrou est reproduit dans `Codec.cs` plutot que supprime — si un jour un second
    // thread decompresse, le comportement doit rester celui de 1998.
    //
    // Les quatre tampons de `left`/`right`/`c_table`/`c_len` sont alloues et liberes a chaque
    // decompression (FUN_10003340 / FUN_100033a0), pas une fois pour toutes. C'est ce que fait la
    // DLL, et c'est ce qui rend la paire `Malloc`/`Free` indispensable ici.
    // =====================================================================

    internal static uint   OrigSize;        // DAT_10057e00 — la taille une fois decompresse
    internal static short  RunRestant;      // _DAT_10057e04 — le `j` de LHA, reporte d'un bloc a l'autre
    internal static ushort* Left;           // DAT_10057e08 — left[2 * NC - 1], 0x7f6 octets
    internal static int    CompSize;        // DAT_10057e0c — ce qui reste du tampon d'entree
    internal static ushort BlockSize;       // DAT_10057e10 — le nombre de symboles du bloc courant
    internal static ushort* CTable;         // DAT_10057e30 — c_table[1 << 12], 0x2000 octets
    internal static ushort* Right;          // DAT_10057e34 — right[2 * NC - 1], 0x7f6 octets
    internal static byte*  Entree;          // DAT_10057e38 — le curseur de lecture
    internal static ushort SubBitBuf;       // DAT_10057e3c — l'octet en cours d'egrenage
    internal static ushort BitBuf;          // DAT_10057e40 — la fenetre de 16 bits
    internal static short  BitCount;        // DAT_10058164 — ce qui reste dans SubBitBuf
    internal static byte*  ALiberer;        // DAT_10058168 — le tampon d'entree, rendu au nettoyage
    internal static byte*  CLen;            // DAT_10058160 — c_len[NC], 0x1fe octets
    internal static ushort Anneau;          // DAT_10058170 — le curseur de l'anneau, masque a 13 bits

    /// <summary>DAT_10057e2c — le second compteur d'entree, lu dans l'en-tete du conteneur.</summary>
    internal static int CompSizeEnTete;

    // `DAT_10057e18` (19 octets utiles) et `DAT_10057f60` (256 mots) sont STATIQUES dans la DLL,
    // pas alloues : d'ou des tableaux ici et non des pointeurs. Les tailles se lisent dans
    // l'espacement des symboles — `DAT_10057e60 - DAT_10057e18` vaut 0x48, et
    // `DAT_10058160 - DAT_10057f60` vaut 0x200, soit 256 mots.
    internal static readonly byte[]   PtLen   = new byte[19];     // &DAT_10057e18
    internal static readonly ushort[] PtTable = new ushort[256];  // &DAT_10057f60

    /// <summary>
    /// &amp;DAT_10057e60 — l'en-tete du conteneur, recopie du fichier. **256 octets exactement** :
    /// `DAT_10057f60` suit immediatement, et la longueur d'en-tete est un octet, donc 255 au plus.
    /// </summary>
    internal static readonly byte[] EnTete = new byte[256];

    /// <summary>
    /// DAT_10057e48 — la section critique du decodeur. Un `object` fait le meme office : le verrou
    /// est reentrant des deux cotes, et rien ici ne traverse de frontiere de processus.
    /// </summary>
    internal static readonly object Verrou = new();

    /// <summary>
    /// &amp;DAT_10049b88 — la table de `decrypt`, relevee dans `Legacy/Binaries/wsyskern.dll`
    /// (`.rdata`, decalage fichier 0x47d88) : <c>ea 7b 22 25 0c 35 2d 1f 47</c>.
    ///
    /// **Neuf octets, pas huit**, et c'est la lecture du binaire qui le montre : le dixieme est
    /// nul. Cela confirme le double indexage du corps — la rotation prend `rang &amp; 7`, donc les
    /// huit premiers, tandis que le ou exclusif prend `rang % 9` et atteint le neuvieme. Une table
    /// transcrite a huit octets aurait dechiffre juste un octet sur neuf.
    /// </summary>
    internal static readonly byte[] TableDeChiffre =
        [0xea, 0x7b, 0x22, 0x25, 0x0c, 0x35, 0x2d, 0x1f, 0x47];
}
