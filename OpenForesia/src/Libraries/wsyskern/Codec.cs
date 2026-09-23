// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c.
//
// LE DECOMPRESSEUR. Dix-huit corps autour de 0x10003400, partages par `Unpack` (export 145) et par
// le chargeur de bitmap, et proteges par une seule section critique.
//
// ============================================================================================
// CE QUE C'EST : LHA `-lh5-`, autrement dit LZHUF.
//
// L'identification n'est pas une supposition de ressemblance, elle se verifie sur les constantes,
// et chacune est celle de la source de LHA :
//
//     NC = 510 (0x1fe)          c_len[NC], et le test `0x1fd < j` de decode_c
//     NT = 19  (0x13)           read_pt_len(19, 5, 3), le pre-arbre
//     NP = 14  (0xe)            read_pt_len(14, 4, -1), l'arbre de positions
//     seuil = 3                 `j = c - 0xfd`, soit c - (255 + 1 - 3)
//     dictionnaire = 8 192      l'anneau masque a 0x1fff, et les blocs de 0x2000
//     left/right = 1 019 mots   0x7f6 octets, soit 2 * NC - 1
//     c_table = 4 096 mots      12 bits de table directe ; pt_table = 256, soit 8 bits
//
// Pourquoi le dire ici : parce que **cela rend la transcription verifiable**. Les corps ci-dessous
// sont transcrits depuis `wsyskern.c` et de nulle part ailleurs — c'est la regle —, mais savoir a
// quoi ils correspondent permet de nommer juste (`FUN_100037b0` est `decode_c`, pas « la fonction
// qui rend un mot ») et de reconnaitre un ecart au lieu de le recopier sans le voir.
//
// UN ECART, justement, et c'est le seul : `fillbuf` consulte DEUX compteurs d'entree la ou LHA
// n'en a qu'un. `CompSizeEnTete` vient de l'en-tete du conteneur, `CompSize` de la taille reelle
// du tampon ; le premier des deux a s'epuiser fait entrer des zeros. Un fichier dont l'en-tete
// ment est donc decode jusqu'au bout avec du remplissage, au lieu de lire au-dela du tampon.
// ============================================================================================

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Codec
{
    private const int NC = 510;          // 0x1fe
    private const int NT = 19;           // 0x13
    private const int NP = 14;           // 0xe
    private const int Seuil = 3;
    private const int TailleDico = 0x2000;
    private const int MasqueAnneau = TailleDico - 1;
    private const int BitsTableC = 12;
    private const int BitsTablePt = 8;

    // =========================================================================================
    // Les tampons du bloc courant — alloues et rendus a chaque decompression.
    // =========================================================================================

    /// <summary>Ghidra: wsyskern.c FUN_10003340 @ 0x10003340 — alloue les quatre tables.</summary>
    private static void AllouerTables()
    {
        WsyskernData.Left   = null;
        WsyskernData.Right  = null;
        WsyskernData.CTable = null;
        WsyskernData.CLen   = null;

        WsyskernData.Left   = (ushort*)Wsyskern.Malloc(0x7f6);
        WsyskernData.Right  = (ushort*)Wsyskern.Malloc(0x7f6);
        WsyskernData.CTable = (ushort*)Wsyskern.Malloc(0x2000);
        WsyskernData.CLen   = (byte*)Wsyskern.Malloc(0x1fe);
    }

    /// <summary>Ghidra: wsyskern.c FUN_100033a0 @ 0x100033a0 — rend les quatre tables.</summary>
    private static void LibererTables()
    {
        Wsyskern.Free(WsyskernData.Left);
        Wsyskern.Free(WsyskernData.Right);
        Wsyskern.Free(WsyskernData.CTable);
        Wsyskern.Free(WsyskernData.CLen);
    }

    // =========================================================================================
    // L'en-tete du conteneur.
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003450 @ 0x10003450 — lit un entier petit-boutien de
    /// <paramref name="octets"/> octets a <paramref name="decalage"/> dans l'en-tete.
    ///
    /// Le corps parcourt a REBOURS, de l'octet de poids fort vers celui de poids faible, en
    /// multipliant par 256 a chaque tour : c'est une lecture petit-boutienne ecrite a l'envers.
    /// </summary>
    private static int LireEntier(short decalage, short octets)
    {
        int valeur = 0;
        octets -= 1;
        if (octets < 0) return 0;

        fixed (byte* enTete = WsyskernData.EnTete)
        {
            byte* p = enTete + decalage + octets;
            for (int reste = octets + 1; reste != 0; reste--)
            {
                valeur = valeur * 0x100 + *p;
                p--;
            }
        }
        return valeur;
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100033d0 @ 0x100033d0 — recopie l'en-tete et en tire les deux
    /// tailles. Rend faux quand le premier octet est nul : le flux n'est alors pas un conteneur.
    ///
    /// Disposition, relevee sur les decalages que le corps emploie — l'en-tete recopie commence a
    /// l'octet 2 du fichier, donc `EnTete[i]` est `fichier[2 + i]` :
    ///
    ///     fichier[0]        la longueur N de l'en-tete
    ///     fichier[1]        inutilise
    ///     EnTete[0..4]      la signature, « xxxxx » — dont EnTete[3] porte le CODE DE FORMAT
    ///     EnTete[5..8]      la taille compressee
    ///     EnTete[9..12]     la taille decompressee
    /// </summary>
    private static bool LireEnTete()
    {
        byte* fichier = WsyskernData.Entree;
        byte longueur = *fichier;
        WsyskernData.Entree = fichier + 1;
        if (longueur == 0) return false;

        int n = (short)(ushort)longueur;
        WsyskernData.Entree = fichier + 2 + n;

        fixed (byte* enTete = WsyskernData.EnTete)
        {
            byte* source = fichier + 2;
            for (int i = 0; i < n; i++) enTete[i] = source[i];
        }

        WsyskernData.CompSize -= 2 + n;
        WsyskernData.CompSizeEnTete = LireEntier(5, 4);
        WsyskernData.OrigSize = (uint)LireEntier(9, 4);
        return true;
    }

    // =========================================================================================
    // Le lecteur de bits.
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003620 @ 0x10003620 — `fillbuf(n)` : decale la fenetre de
    /// <paramref name="n"/> bits et la recharge par l'octet courant.
    ///
    /// **Toute l'arithmetique est sur seize bits**, y compris les debordements : `BitBuf` est un
    /// `ushort` et les decalages a gauche perdent ce qui sort par le haut. Elargir a 32 bits
    /// changerait les codes lus des le premier bloc.
    /// </summary>
    private static void FillBuf(int n)
    {
        WsyskernData.BitBuf = (ushort)(WsyskernData.BitBuf << (n & 0x1f));

        short reste = (short)n;
        if (WsyskernData.BitCount < reste)
        {
            do
            {
                n -= WsyskernData.BitCount;
                reste = (short)n;
                WsyskernData.BitBuf |= (ushort)(WsyskernData.SubBitBuf << (n & 0x1f));

                // Les DEUX compteurs, et le premier epuise gagne — voir le pave de tete.
                if (WsyskernData.CompSizeEnTete == 0)
                {
                    WsyskernData.SubBitBuf = 0;
                }
                else
                {
                    WsyskernData.CompSizeEnTete--;
                    if (WsyskernData.CompSize == 0)
                    {
                        WsyskernData.SubBitBuf = 0;
                    }
                    else
                    {
                        WsyskernData.CompSize--;
                        WsyskernData.SubBitBuf = *WsyskernData.Entree;
                        WsyskernData.Entree++;
                    }
                }

                WsyskernData.BitCount = 8;
            }
            while (reste > 8);
        }

        WsyskernData.BitCount -= reste;
        WsyskernData.BitBuf |= (ushort)(WsyskernData.SubBitBuf >> (WsyskernData.BitCount & 0x1f));
    }

    /// <summary>Ghidra: wsyskern.c FUN_10003e40 @ 0x10003e40 — `getbits(n)`.</summary>
    private static ushort GetBits(int n)
    {
        ushort valeur = WsyskernData.BitBuf;
        FillBuf(n);
        return (ushort)(valeur >> ((0x10 - n) & 0x1f));
    }

    /// <summary>Ghidra: wsyskern.c FUN_10003600 @ 0x10003600 — `init_getbits`.</summary>
    private static void InitGetBits()
    {
        WsyskernData.BitCount = 0;
        WsyskernData.SubBitBuf = 0;
        WsyskernData.BitBuf = 0;
        FillBuf(0x10);
    }

    /// <summary>Ghidra: wsyskern.c FUN_100035f0 @ 0x100035f0 — init + bloc vide.</summary>
    private static void DecodeStart()
    {
        InitGetBits();
        WsyskernData.BlockSize = 0;
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100035e0 @ 0x100035e0 — init complet : le report de recopie
    /// (`RunRestant`) est remis a zero, ce que `DecodeStart` seul ne fait pas.
    /// </summary>
    private static void DecodeStartComplet()
    {
        DecodeStart();
        WsyskernData.RunRestant = 0;
    }

    // =========================================================================================
    // Les arbres de Huffman.
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003bb0 @ 0x10003bb0 — `make_table` : construit la table de
    /// decodage directe, et les branches `left`/`right` pour les codes plus longs que
    /// <paramref name="bitsTable"/>.
    ///
    /// **`compte` et `poids` partagent le meme tableau**, et ce n'est pas une simplification : le
    /// corps d'origine ecrit les poids par-dessus les comptes une fois ceux-ci consommes dans
    /// `depart`. Les separer donnerait le meme resultat ici, mais masquerait que les deux vivent a
    /// la meme adresse — et c'est le genre de detail qui compte quand on relit contre la DLL.
    /// </summary>
    private static void MakeTable(ushort nombreCodes, byte* longueurs, uint bitsTable, ushort* table)
    {
        ushort* compte = stackalloc ushort[17];   // &sStack_48, reutilise ensuite comme `poids`
        ushort* depart = stackalloc ushort[18];   // auStack_24

        for (int i = 0; i < 17; i++) compte[i] = 0;

        for (int i = 0; i < nombreCodes; i++) compte[longueurs[i]]++;

        depart[1] = 0;
        for (int i = 1; i <= 16; i++)
            depart[i + 1] = (ushort)((compte[i] << (16 - i)) + depart[i]);

        byte reserve = (byte)(0x10 - (byte)bitsTable);   // jutbits

        uint suivant = 1;
        if ((ushort)bitsTable != 0)
        {
            uint bits = bitsTable & 0xffff;
            suivant = bits + 1;
            for (int i = 1; i <= (int)bits; i++)
            {
                depart[i] = (ushort)(depart[i] >> (reserve & 0x1f));
                compte[i] = (ushort)(1 << (((int)bits - i) & 0x1f));
            }
        }

        for (uint i = suivant & 0xffff; i <= 16; i++)
            compte[i] = (ushort)(1 << ((0x10 - (int)i) & 0x1f));

        // Le reste de la table directe qu'aucun code n'atteint est mis a zero.
        ushort atteint = (ushort)(depart[(bitsTable & 0xffff) + 1] >> (reserve & 0x1f));
        if (atteint != 0)
        {
            uint taille = (uint)(1 << (int)(bitsTable & 0x1f));
            if (atteint != (ushort)taille)
                for (uint i = atteint; i < (taille & 0xffff); i++) table[i] = 0;
        }

        ushort disponible = nombreCodes;                     // avail
        ushort masqueBranche = (ushort)(1 << ((0xf - (int)(bitsTable & 0xffff)) & 0x1f));

        for (ushort code = 0; code < nombreCodes; code++)
        {
            byte longueur = longueurs[code];
            if (longueur == 0) continue;

            ushort debut = depart[longueur];
            ushort fin = (ushort)(compte[longueur] + debut);

            if ((ushort)bitsTable < longueur)
            {
                // Le code deborde la table directe : on descend en `left`/`right` en creant les
                // noeuds manquants, numerotes a partir de `nombreCodes`.
                ushort* noeud = table + (debut >> (reserve & 0x1f));
                int profondeur = (short)(longueur - bitsTable);

                for (int p = profondeur & 0xffff; p != 0; p--)
                {
                    if (*noeud == 0)
                    {
                        WsyskernData.Left[disponible] = 0;
                        WsyskernData.Right[disponible] = 0;
                        *noeud = disponible;
                        disponible++;
                    }
                    noeud = (masqueBranche & debut) == 0
                        ? WsyskernData.Left + *noeud
                        : WsyskernData.Right + *noeud;
                    debut = (ushort)(debut << 1);
                }
                *noeud = code;
            }
            else if (debut < fin)
            {
                for (uint i = debut; i < fin; i++) table[i] = code;
            }

            depart[longueur] = fin;
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003870 @ 0x10003870 — `read_pt_len` : lit les longueurs de code du
    /// pre-arbre (19 symboles) ou de l'arbre de positions (14).
    ///
    /// Un compte nul est le cas degenere « un seul symbole » : toutes les longueurs a zero, et la
    /// table entiere remplie de ce symbole — pas d'arbre du tout.
    ///
    /// <paramref name="special"/> vaut 3 pour le pre-arbre et -1 pour les positions : arrive au
    /// troisieme symbole, deux bits de plus disent combien de longueurs nulles sautent.
    /// </summary>
    private static void ReadPtLen(int nombre, int bitsCompte, short special)
    {
        short compte = (short)GetBits(bitsCompte);
        short total = (short)nombre;

        if (compte == 0)
        {
            ushort symbole = GetBits(bitsCompte);
            for (int i = 0; i < total; i++) WsyskernData.PtLen[i] = 0;
            for (int i = 0; i < 256; i++) WsyskernData.PtTable[i] = symbole;
            return;
        }

        short ecrits = 0;
        while (ecrits < compte)
        {
            // Les trois bits de tete donnent la longueur ; a 7, les bits suivants la prolongent
            // en unaire — c'est le codage de longueur de LHA.
            ushort longueur = (ushort)(WsyskernData.BitBuf >> 0xd);
            if (longueur == 7)
            {
                ushort masque = 0x1000;
                ushort n = 7;
                if ((WsyskernData.BitBuf & 0x1000) != 0)
                {
                    do
                    {
                        masque = (ushort)(masque >> 1);
                        n++;
                        longueur = n;
                    }
                    while ((masque & WsyskernData.BitBuf) != 0);
                }
            }

            FillBuf((short)longueur < 7 ? 3 : (short)longueur - 3);
            WsyskernData.PtLen[ecrits] = (byte)longueur;
            ecrits++;

            if (ecrits == special)
            {
                short saut = (short)GetBits(2);
                for (int i = 0; i < saut; i++)
                {
                    WsyskernData.PtLen[ecrits] = 0;
                    ecrits++;
                }
            }
        }

        for (int i = ecrits; i < total; i++) WsyskernData.PtLen[i] = 0;

        fixed (byte* longueurs = WsyskernData.PtLen)
        fixed (ushort* table = WsyskernData.PtTable)
            MakeTable((ushort)nombre, longueurs, 8, table);
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100039a0 @ 0x100039a0 — `read_c_len` : lit les longueurs de code des
    /// 510 symboles litteral/longueur, elles-memes codees par le pre-arbre.
    ///
    /// Trois echappements pour les suites de zeros : 0 en saute un, 1 en saute 3 a 18 (quatre bits
    /// de plus), 2 en saute 20 a 531 (neuf bits). Au-dela, la valeur moins deux est la longueur.
    /// </summary>
    private static void ReadCLen()
    {
        short compte = (short)GetBits(9);

        if (compte == 0)
        {
            ushort symbole = GetBits(9);
            for (int i = 0; i < NC; i++) WsyskernData.CLen[i] = 0;
            for (int i = 0; i < (1 << BitsTableC); i++) WsyskernData.CTable[i] = symbole;
            return;
        }

        short ecrits = 0;
        while (ecrits < compte)
        {
            short symbole = (short)WsyskernData.PtTable[(WsyskernData.BitBuf & 0xffff) >> 8];
            if (symbole >= NT)
            {
                ushort masque = 0x80;
                do
                {
                    symbole = (short)((masque & WsyskernData.BitBuf) == 0
                        ? WsyskernData.Left[symbole]
                        : WsyskernData.Right[symbole]);
                    masque = (ushort)(masque >> 1);
                }
                while (symbole >= NT);
            }

            FillBuf(WsyskernData.PtLen[symbole]);

            if (symbole < 3)
            {
                short saut = symbole switch
                {
                    0 => 1,
                    1 => (short)(GetBits(4) + 3),
                    _ => (short)(GetBits(9) + 0x14),
                };
                for (int i = 0; i < saut; i++)
                {
                    WsyskernData.CLen[ecrits] = 0;
                    ecrits++;
                }
            }
            else
            {
                WsyskernData.CLen[ecrits] = (byte)(symbole - 2);
                ecrits++;
            }
        }

        for (int i = ecrits; i < NC; i++) WsyskernData.CLen[i] = 0;

        MakeTable(NC, WsyskernData.CLen, BitsTableC, WsyskernData.CTable);
    }

    // =========================================================================================
    // Le decodage.
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100037b0 @ 0x100037b0 — `decode_c` : rend le prochain symbole
    /// litteral (0..255) ou longueur de recopie (256 et au-dela).
    ///
    /// Le bloc epuise, les trois arbres sont relus. L'ordre — pre-arbre, litteraux, positions —
    /// n'est pas arbitraire : le pre-arbre sert a lire les litteraux.
    /// </summary>
    private static ushort DecodeC()
    {
        if (WsyskernData.BlockSize == 0)
        {
            WsyskernData.BlockSize = GetBits(0x10);
            ReadPtLen(NT, 5, 3);
            ReadCLen();
            ReadPtLen(NP, 4, -1);
        }
        WsyskernData.BlockSize--;

        ushort symbole = WsyskernData.CTable[(WsyskernData.BitBuf & 0xffff) >> (16 - BitsTableC)];
        if (symbole >= NC)
        {
            ushort masque = 1 << (15 - BitsTableC);
            do
            {
                symbole = (masque & WsyskernData.BitBuf) == 0
                    ? WsyskernData.Left[symbole]
                    : WsyskernData.Right[symbole];
                masque = (ushort)(masque >> 1);
            }
            while (symbole >= NC);
        }

        FillBuf(WsyskernData.CLen[symbole]);
        return symbole;
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003b10 @ 0x10003b10 — `decode_p` : rend la distance de recopie.
    ///
    /// Le symbole est un NOMBRE DE BITS, pas une distance : n vaut zero pour la distance zero, et
    /// sinon la distance est <c>(1 &lt;&lt; (n-1)) + les n-1 bits suivants</c>.
    /// </summary>
    private static ushort DecodeP()
    {
        ushort symbole = WsyskernData.PtTable[(WsyskernData.BitBuf & 0xffff) >> BitsTablePt];
        if (symbole >= NP)
        {
            ushort masque = 1 << (15 - BitsTablePt);
            do
            {
                symbole = (masque & WsyskernData.BitBuf) == 0
                    ? WsyskernData.Left[symbole]
                    : WsyskernData.Right[symbole];
                masque = (ushort)(masque >> 1);
            }
            while (symbole >= NP);
        }

        FillBuf(WsyskernData.PtLen[symbole]);

        if (symbole != 0)
            symbole = (ushort)(GetBits(symbole - 1) + (1 << ((symbole - 1) & 0x1f)));

        return symbole;
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100036d0 @ 0x100036d0 — `decode` : remplit
    /// <paramref name="sortie"/> de <paramref name="combien"/> octets.
    ///
    /// **`RunRestant` et `Anneau` survivent a l'appel**, et c'est ce qui permet de decouper la
    /// sortie en blocs de 8 192 : une recopie commencee a la fin d'un bloc se termine au debut du
    /// suivant. Les remettre a zero entre deux blocs couperait toute recopie a cheval.
    ///
    /// L'anneau n'est pas un tampon separe : c'est <paramref name="sortie"/> lui-meme, indexe
    /// modulo 8 192. Une recopie peut donc lire des octets qu'elle vient d'ecrire — c'est voulu,
    /// c'est ainsi que LZ encode les repetitions courtes.
    /// </summary>
    private static void Decode(ushort combien, byte* sortie)
    {
        ushort ecrit = 0;

        // La recopie reportee du bloc precedent.
        while (true)
        {
            WsyskernData.RunRestant--;
            if (WsyskernData.RunRestant < 0) break;

            sortie[ecrit] = sortie[WsyskernData.Anneau];
            ecrit++;
            WsyskernData.Anneau = (ushort)((WsyskernData.Anneau + 1) & MasqueAnneau);
            if (ecrit == combien) return;
        }

        while (true)
        {
            ushort symbole = DecodeC();

            if (symbole > byte.MaxValue)
            {
                WsyskernData.RunRestant = (short)(symbole - (byte.MaxValue + 1 - Seuil));
                WsyskernData.Anneau = (ushort)((ecrit - 1 - DecodeP()) & MasqueAnneau);

                while (true)
                {
                    WsyskernData.RunRestant--;
                    if (WsyskernData.RunRestant < 0) break;

                    sortie[ecrit] = sortie[WsyskernData.Anneau];
                    ecrit++;
                    WsyskernData.Anneau = (ushort)((WsyskernData.Anneau + 1) & MasqueAnneau);
                    if (ecrit == combien) return;
                }
                continue;
            }

            sortie[ecrit] = (byte)symbole;
            ecrit++;
            if (ecrit == combien) return;
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003480 @ 0x10003480 — le pilote : verifie la signature, alloue la
    /// sortie et la remplit par blocs de 8 192 octets. Rend null si le flux n'est pas reconnu.
    ///
    /// **Le code de format est l'octet 5 du fichier**, lu puis ECRASE par « x » avant la
    /// comparaison — c'est ainsi que la signature « xxxxx » vaut pour les trois formats a la fois.
    ///
    /// Le mode « stocke » (code « 0 ») est INATTEIGNABLE : la garde n'accepte que « 4 », « 5 » et
    /// « x ». La branche de recopie directe est conservee telle quelle — c'est ce que la DLL
    /// contient, et ce n'est pas a la transcription de trancher si c'est un oubli de 1998.
    /// </summary>
    private static byte* Decompresser()
    {
        fixed (byte* enTete = WsyskernData.EnTete)
        {
            byte format = enTete[3];
            enTete[3] = 0x78;   // 'x'

            if (format != '4' && format != '5' && format != 'x') return null;

            if (enTete[0] != 'x' || enTete[1] != 'x' || enTete[2] != 'x' ||
                enTete[3] != 'x' || enTete[4] != 'x') return null;
        }

        byte* sortie = (byte*)Wsyskern.Malloc(WsyskernData.OrigSize + 1);
        DecodeStartComplet();

        byte* bloc = (byte*)Wsyskern.Malloc(TailleDico);
        byte* curseur = sortie;

        while (WsyskernData.OrigSize != 0)
        {
            uint taille = WsyskernData.OrigSize < TailleDico + 1 ? WsyskernData.OrigSize : TailleDico;
            ushort combien = (ushort)taille;

            Decode(combien, bloc);
            for (uint i = 0; i < combien; i++) curseur[i] = bloc[i];

            curseur += combien;
            WsyskernData.OrigSize -= combien;
        }

        Wsyskern.Free(bloc);
        return sortie;
    }

    // =========================================================================================
    // Les deux entrees publiques.
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c Unpack @ 0x10003e70 (export 145) — decompresse un tampon deja en
    /// memoire. <paramref name="tailleSortie"/> recoit la taille decompressee, lue dans l'en-tete
    /// AVANT le decodage — elle est donc renseignee meme si le decodage rend null.
    ///
    /// Le bloc de nettoyage d'origine (`FUN_10003efd`) rend les tables et relache le verrou, sur
    /// le chemin d'exception comme sur le chemin normal : d'ou le <c>finally</c>.
    /// </summary>
    internal static byte* Unpack(byte* entree, int taille, uint* tailleSortie)
    {
        lock (WsyskernData.Verrou)
        {
            AllouerTables();
            try
            {
                WsyskernData.CompSize = taille;
                WsyskernData.ALiberer = entree;
                WsyskernData.Entree = entree;

                LireEnTete();
                if (tailleSortie != null) *tailleSortie = WsyskernData.OrigSize;

                return Decompresser();
            }
            finally
            {
                LibererTables();
            }
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003260 @ 0x10003260 — lit un fichier ENTIER puis le decompresse.
    ///
    /// **Le handle est ferme ici**, et c'est ce qui fait que `LoadBitmapFile` le ferme une seconde
    /// fois a son retour. Voir la note dans `Fichiers.cs`.
    ///
    /// Le nettoyage (`FUN_10003316`) rend en plus le tampon d'ENTREE — celui que ce corps vient
    /// d'allouer —, ce que la variante en memoire ne fait pas : la, le tampon appartient a
    /// l'appelant.
    /// </summary>
    internal static byte* DecompresserFichier(IntPtr fichier)
    {
        lock (WsyskernData.Verrou)
        {
            AllouerTables();
            try
            {
                WsyskernData.ALiberer = null;
                WsyskernData.CompSize = (int)Win32.GetFileSize(fichier, null);
                WsyskernData.Entree = (byte*)Wsyskern.Malloc((uint)WsyskernData.CompSize);
                WsyskernData.ALiberer = WsyskernData.Entree;

                uint lus;
                Win32.ReadFile(fichier, WsyskernData.Entree, (uint)WsyskernData.CompSize, &lus, null);
                Win32.CloseHandle(fichier);

                LireEnTete();
                return Decompresser();
            }
            finally
            {
                LibererTables();
                Wsyskern.Free(WsyskernData.ALiberer);
            }
        }
    }
}
