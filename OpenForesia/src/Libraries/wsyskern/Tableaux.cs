// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c et,
// pour six corps, desassemblee dans Legacy/Raw/Wsyskern/.
//
// LA FAMILLE TABLEAU : quatre exports — `CreateArray` (41), `LoadArray` (113), `ArrayToString` (36),
// `DeleteArray` (81) — et la petite hierarchie C++ qui les porte.
//
// ============================================================================================
// L'OBJET, VINGT OCTETS, ET SA VTABLE
//
//     +0x00   le pointeur de vtable
//     +0x04   le nombre magique « ARAY » (0x59415241)
//     +0x08   le GENRE : 0 entiers, 1 chaines, 2 flottants
//     +0x0c   la capacite, en elements
//     +0x10   le tableau de donnees, alloue a part
//
// Trois classes concretes, une par genre, et leurs vtables relevees dans le binaire
// (`.rdata`, huit emplacements chacune) :
//
//     genre 0  PTR_FUN_100499b8   donnees sur 4 octets   un entier long
//     genre 1  PTR_FUN_10049a18   donnees sur 4 octets   un POINTEUR de chaine, possede
//     genre 2  PTR_FUN_100499e8   donnees sur 8 octets   un flottant double
//
// **On repartit sur le GENRE, pas sur la vtable**, et ce n'est pas un raccourci : la
// correspondance genre <-> vtable est bijective — chaque constructeur ecrit les deux ensemble —,
// donc les deux repartitions sont exactement equivalentes. Le mot `+0x00` est tout de meme ecrit
// avec l'adresse d'origine, et le destructeur le remplace par celle de la classe de base comme
// chez l'original : la disposition reste observable a l'identique, seul l'appel indirect
// disparait, faute d'avoir un sens en C#.
//
// QUATRE EMPLACEMENTS SUR HUIT SONT EMPLOYES, et c'est le releve des appelants qui le dit :
//
//     [0]  destructeur         `DeleteArray`
//     [2]  redimensionner      `FUN_10004060`, par `*(this + 8)`
//     [3]  rendre en texte     `ArrayToString`, par `*(this + 0xc)`
//     [6]  poser depuis texte  `FUN_10004060`, par `*(this + 0x18)`
//
// **Aucun de ces douze corps n'etait dans la cloture du recensement**, parce que tous sont
// atteints par POINTEUR : `DLLAtteignables.md` annonce que la cloture est un plancher et non un
// plafond, et c'est ici que cela s'est verifie. Six d'entre eux manquaient meme a `wsyskern.c` —
// Ghidra ne decompile pas du code atteint seulement par son adresse. Ils ont ete transcrits au
// desassembleur ; les relevés sont sous `Legacy/Raw/Wsyskern/`.
// ============================================================================================

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Tableaux
{
    private const uint Magie = 0x59415241;   // « ARAY »

    // Les adresses de vtable, conservees pour que le mot +0x00 porte la meme valeur qu'en 1998.
    private const uint VTableEntiers  = 0x100499b8;
    private const uint VTableChaines  = 0x10049a18;
    private const uint VTableFlottants = 0x100499e8;
    private const uint VTableBase     = 0x10049988;   // PTR_LAB_10049988, posee par le destructeur

    private const int GenreEntier = 0;
    private const int GenreChaine = 1;
    private const int GenreFlottant = 2;

    private static uint* Champ(void* tableau, int decalage) => (uint*)((byte*)tableau + decalage);

    private static int  Genre(void* t)     => (int)*Champ(t, 0x08);
    private static int  Capacite(void* t)  => (int)*Champ(t, 0x0c);
    private static void* Donnees(void* t)  => (void*)*Champ(t, 0x10);

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10003f10 @ 0x10003f10 — `__fastcall`, le controle de validite : non
    /// nul, et le mot `+0x04` vaut « ARAY ». Tous les exports le passent avant de toucher a quoi
    /// que ce soit, et rendent sans bruit s'il echoue.
    /// </summary>
    private static bool EstTableau(void* tableau)
        => tableau != null && *Champ(tableau, 0x04) == Magie;

    // =========================================================================================
    // Construction et destruction
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c CreateArray @ 0x10009f00 (export 41) — alloue 20 octets et appelle le
    /// constructeur du genre demande. Un genre inconnu rend zero.
    ///
    /// **L'argument est le genre, dans l'ordre 0, 1, 2**, mais les trois constructeurs ne sont pas
    /// dans cet ordre dans le binaire : `CreateArray(1)` appelle `FUN_10004a50` et
    /// `CreateArray(2)` appelle `FUN_100046c0`. C'est le mot `+0x08` que chacun ecrit qui fait foi.
    /// </summary>
    internal static void* CreateArray(int genre, int capacite)
    {
        if (genre is not (GenreEntier or GenreChaine or GenreFlottant)) return null;

        void* tableau = Wsyskern.Malloc(0x14);
        if (tableau == null) return null;

        uint vtable = genre switch
        {
            GenreEntier => VTableEntiers,
            GenreChaine => VTableChaines,
            _           => VTableFlottants,
        };

        // Ghidra: wsyskern.c FUN_10004400 / FUN_10004a50 / FUN_100046c0 @ 0x10004400, 0x10004a50,
        // 0x100046c0 — les trois constructeurs, identiques a la taille d'element pres.
        *Champ(tableau, 0x00) = vtable;
        *Champ(tableau, 0x04) = Magie;
        *Champ(tableau, 0x08) = (uint)genre;
        *Champ(tableau, 0x0c) = (uint)capacite;
        *Champ(tableau, 0x10) = (uint)Wsyskern.Malloc((uint)(capacite * TailleElement(genre)));

        return tableau;
    }

    private static int TailleElement(int genre) => genre == GenreFlottant ? 8 : 4;

    /// <summary>
    /// Ghidra: wsyskern.c DeleteArray @ 0x10009fe0 (export 81) — appelle l'emplacement 0 avec le
    /// drapeau 1, celui qui libere aussi l'objet lui-meme.
    /// </summary>
    internal static void DeleteArray(void* tableau)
    {
        if (!EstTableau(tableau) || tableau == null) return;
        Detruire(tableau, liberer: true);
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100043b0 / FUN_10004a30 / FUN_100046a0 (emplacement 0) et leurs
    /// destructeurs reels FUN_10004380 / FUN_100049f0 / FUN_10004670.
    ///
    /// **Le genre 1 possede ses chaines** : son destructeur libere chaque element avant le
    /// tableau. Les deux autres ne liberent que le tableau. Le mot `+0x00` retombe sur la vtable
    /// de la classe de base et `+0x04` — le nombre magique — est remis a zero, ce qui fait
    /// qu'un second `DeleteArray` sur le meme objet ne fait rien au lieu de liberer deux fois.
    /// </summary>
    private static void Detruire(void* tableau, bool liberer)
    {
        if (Genre(tableau) == GenreChaine)
        {
            void** elements = (void**)Donnees(tableau);
            int capacite = Capacite(tableau);
            for (int i = 0; i < capacite; i++) Wsyskern.Free(elements[i]);
        }

        Wsyskern.Free(Donnees(tableau));
        *Champ(tableau, 0x00) = VTableBase;
        *Champ(tableau, 0x04) = 0;

        if (liberer) Wsyskern.Free(tableau);
    }

    // =========================================================================================
    // L'emplacement 2 : redimensionner
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10004af0 @ 0x10004af0 (genres 0 et 1, vtable partagee) et
    /// FUN_10004740 @ 0x10004740 (genre 2) — desassembles, absents de la decompilation.
    ///
    /// **Retrecir ne reallouе rien** : la comparaison est signee et stricte, donc une capacite
    /// inferieure ou egale se contente d'ecrire le nouveau nombre. Le tampon garde sa taille, et
    /// les elements au-dela restent en place — c'est ce qui permet a `FUN_10004060` de gonfler
    /// puis de retrecir sans perdre ce qu'il a ecrit.
    ///
    /// Pour le genre 1, cela veut aussi dire que **retrecir fuit les chaines** des elements
    /// abandonnes : le destructeur ne parcourra plus que la nouvelle capacite. Defaut d'origine,
    /// reproduit tel quel.
    /// </summary>
    private static int Redimensionner(void* tableau, int nouvelle)
    {
        int ancienneCapacite = Capacite(tableau);
        void* anciennes = Donnees(tableau);

        if (nouvelle <= ancienneCapacite)
        {
            *Champ(tableau, 0x0c) = (uint)nouvelle;
            return 1;
        }

        int taille = TailleElement(Genre(tableau));
        byte* neuves = (byte*)Wsyskern.Malloc((uint)(nouvelle * taille));
        *Champ(tableau, 0x10) = (uint)neuves;

        // La copie porte sur l'ANCIENNE capacite, lue avant que le mot ne soit mis a jour.
        int octets = ancienneCapacite * taille;
        for (int i = 0; i < octets; i++) neuves[i] = ((byte*)anciennes)[i];

        *Champ(tableau, 0x0c) = (uint)nouvelle;
        Wsyskern.Free(anciennes);
        return 1;
    }

    // =========================================================================================
    // L'emplacement 3 : rendre en texte
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c ArrayToString @ 0x1000a050 (export 36) — passe par l'emplacement 3.
    /// </summary>
    internal static void ArrayToString(void* tableau, int index, byte* tampon, int taille)
    {
        if (!EstTableau(tableau)) return;
        RendreEnTexte(tableau, index, tampon, taille);
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100044a0 / FUN_10004b90 / FUN_100047e0 (emplacement 3) —
    /// desassembles.
    ///
    /// Les trois partagent la meme forme : index hors capacite rend zero sans rien ecrire, sinon
    /// le tampon est vide d'abord, rempli, puis **termine de force a `taille - 1`**. Les formats
    /// sont ceux du binaire : <c>"%ld"</c> pour un entier (`0x100499e4`), <c>"%g"</c> pour un
    /// flottant (`0x10049a14`). Une chaine nulle du genre 1 laisse le tampon vide et rend 1.
    /// </summary>
    private static int RendreEnTexte(void* tableau, int index, byte* tampon, int taille)
    {
        if ((uint)index >= (uint)Capacite(tableau)) return 0;

        tampon[0] = 0;
        if (taille <= 0) return 1;

        string texte;
        switch (Genre(tableau))
        {
            case GenreEntier:
                texte = ((int*)Donnees(tableau))[index].ToString(System.Globalization.CultureInfo.InvariantCulture);
                break;

            case GenreChaine:
            {
                byte* chaine = ((byte**)Donnees(tableau))[index];
                if (chaine == null) return 1;
                // `_strncpy(tampon, chaine, taille - 1)` puis terminaison forcee.
                int i = 0;
                for (; i < taille - 1 && chaine[i] != 0; i++) tampon[i] = chaine[i];
                for (; i < taille - 1; i++) tampon[i] = 0;
                tampon[taille - 1] = 0;
                return 1;
            }

            default:
                // `%g` : six chiffres significatifs, notation exponentielle quand elle est plus
                // courte. `G6` en est l'equivalent le plus proche, a l'exposant pres — `%g` ecrit
                // `e+06` la ou .NET ecrit `E+06`.
                texte = ((double*)Donnees(tableau))[index]
                    .ToString("G6", System.Globalization.CultureInfo.InvariantCulture)
                    .Replace("E", "e");
                break;
        }

        int n = 0;
        for (; n < taille - 1 && n < texte.Length; n++) tampon[n] = (byte)texte[n];
        for (; n < taille - 1; n++) tampon[n] = 0;
        tampon[taille - 1] = 0;
        return 1;
    }

    // =========================================================================================
    // L'emplacement 6 : poser un element depuis son texte
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10004570 / FUN_10004c60 / FUN_100048d0 (emplacement 6).
    ///
    /// Le genre 1 **libere l'ancienne chaine puis duplique la nouvelle** (`Strdup`, export 141) :
    /// c'est lui qui rend le tableau proprietaire de ses elements.
    /// </summary>
    private static int PoserDepuisTexte(void* tableau, int index, byte* texte)
    {
        if ((uint)index >= (uint)Capacite(tableau)) return 0;

        switch (Genre(tableau))
        {
            case GenreEntier:
                ((int*)Donnees(tableau))[index] = StrToLong(texte);
                return 1;

            case GenreChaine:
            {
                void** elements = (void**)Donnees(tableau);
                Wsyskern.Free(elements[index]);
                elements[index] = Strdup(texte);
                return 1;
            }

            default:
                ((double*)Donnees(tableau))[index] = StrToDouble(texte);
                return 1;
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c strtolong @ 0x10011f50 — **ce n'est pas `strtol`**, et la difference se
    /// voit sur des donnees reelles :
    ///
    ///   - les blancs de tete sont sautes ;
    ///   - <c>$</c> en tete force l'hexadecimal — la notation Pascal, pas la notation C ;
    ///   - <c>+</c> est saute, <c>-</c> nie le resultat du reste, par recursion ;
    ///   - <c>0</c> suivi d'un CHIFFRE est lu en base **dix**, la ou C lirait de l'octal :
    ///     « 010 » vaut dix et non huit ;
    ///   - <c>0b</c> ou <c>0B</c> force le binaire ;
    ///   - sinon la detection habituelle de C, donc <c>0x</c> pour l'hexadecimal.
    /// </summary>
    private static int StrToLong(byte* texte)
    {
        if (texte == null) return 0;

        while (*texte is (byte)' ' or (byte)'\t' or (byte)'\n' or (byte)'\r' or 0x0b or 0x0c) texte++;

        if (*texte == (byte)'$') return (int)LireBase(texte + 1, 16);
        if (*texte == (byte)'+') return StrToLong(texte + 1);
        if (*texte == (byte)'-') return -StrToLong(texte + 1);

        if (*texte == (byte)'0')
        {
            if (texte[1] >= (byte)'0' && texte[1] <= (byte)'9') return (int)LireBase(texte, 10);
            if ((texte[1] & 0xdf) == 0x42) return (int)LireBase(texte + 2, 2);   // 'b' ou 'B'
        }

        return (int)LireBase(texte, 0);
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10013bc0 — la conversion proprement dite. Base zero veut dire
    /// « detecter » : <c>0x</c> pour l'hexadecimal, <c>0</c> pour l'octal, decimal sinon.
    /// </summary>
    private static long LireBase(byte* texte, int baseN)
    {
        if (baseN == 0)
        {
            if (texte[0] == (byte)'0' && (texte[1] & 0xdf) == 0x58) { baseN = 16; texte += 2; }
            else if (texte[0] == (byte)'0') baseN = 8;
            else baseN = 10;
        }

        long valeur = 0;
        while (true)
        {
            int chiffre;
            byte c = *texte;
            if (c >= (byte)'0' && c <= (byte)'9') chiffre = c - '0';
            else if (c >= (byte)'a' && c <= (byte)'z') chiffre = c - 'a' + 10;
            else if (c >= (byte)'A' && c <= (byte)'Z') chiffre = c - 'A' + 10;
            else break;

            if (chiffre >= baseN) break;
            valeur = valeur * baseN + chiffre;
            texte++;
        }
        return valeur;
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10012020 @ 0x10012020 — la conversion en flottant du genre 2.
    ///
    /// TODO(REVERSE): le corps n'a pas ete lu. La forme d'appel (`char*` -&gt; `float10` elargi en
    /// `double`) et son emploi ne laissent guere de place a autre chose qu'un `atof`, mais les
    /// particularites que `strtolong` s'est revele avoir — le <c>$</c>, le zero decimal — commandent
    /// de ne PAS supposer que celui-ci est standard. A lire avant de s'y fier.
    /// </summary>
    private static double StrToDouble(byte* texte)
    {
        if (texte == null) return 0.0;

        int longueur = 0;
        while (texte[longueur] != 0) longueur++;

        string s = System.Text.Encoding.ASCII.GetString(texte, longueur).Trim();
        return double.TryParse(s, System.Globalization.NumberStyles.Float,
                               System.Globalization.CultureInfo.InvariantCulture, out double valeur)
            ? valeur
            : 0.0;
    }

    /// <summary>Ghidra: wsyskern.c Strdup @ 0x1000bc90 (export 141) — sur le tas de la DLL.</summary>
    internal static void* Strdup(byte* texte)
    {
        if (texte == null) return null;
        int longueur = 0;
        while (texte[longueur] != 0) longueur++;

        byte* copie = (byte*)Wsyskern.Malloc((uint)(longueur + 1));
        for (int i = 0; i <= longueur; i++) copie[i] = texte[i];
        return copie;
    }

    // =========================================================================================
    // Le flux de lecture — seize octets : tampon, position, longueur, « je possede le tampon »
    // =========================================================================================

    private const int FluxTampon = 0x00, FluxPosition = 0x04, FluxLongueur = 0x08, FluxPossede = 0x0c;

    /// <summary>Ghidra: wsyskern.c FUN_100224d0 @ 0x100224d0 — ouvre un flux SUR UN FICHIER.</summary>
    private static void* OuvrirFichier(byte* chemin, uint drapeaux)
    {
        void* flux = Wsyskern.Malloc(0x10);
        uint longueur;
        byte* contenu = Fichiers.LoadFile(chemin, drapeaux, &longueur);

        *Champ(flux, FluxTampon) = (uint)contenu;
        *Champ(flux, FluxPosition) = 0;
        *Champ(flux, FluxLongueur) = longueur;
        *Champ(flux, FluxPossede) = 1;

        if (contenu != null) return flux;
        Wsyskern.Free(flux);
        return null;
    }

    /// <summary>Ghidra: wsyskern.c FUN_10022520 @ 0x10022520 — ouvre un flux SUR UN TAMPON.</summary>
    private static void* OuvrirMemoire(byte* tampon, int longueur)
    {
        void* flux = Wsyskern.Malloc(0x10);
        *Champ(flux, FluxTampon) = (uint)tampon;
        *Champ(flux, FluxPosition) = 0;
        *Champ(flux, FluxLongueur) = (uint)longueur;
        *Champ(flux, FluxPossede) = 0;

        if (tampon != null) return flux;
        Wsyskern.Free(flux);
        return null;
    }

    /// <summary>Ghidra: wsyskern.c FUN_10022550 @ 0x10022550 — ferme, et ne rend le tampon que s'il lui appartient.</summary>
    private static void Fermer(void* flux)
    {
        if (*Champ(flux, FluxPossede) != 0) Wsyskern.Free((void*)*Champ(flux, FluxTampon));
        Wsyskern.Free(flux);
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10022570 @ 0x10022570 — lit une ligne. Rend null en fin de flux,
    /// sinon le tampon.
    ///
    /// Trois finesses, toutes dans le corps :
    ///
    ///   - <c>\r</c> est **ignore purement**, jamais range : un fichier en fins de ligne Windows
    ///     sort identique a un fichier Unix ;
    ///   - <c>\n</c> termine la ligne sans y figurer ;
    ///   - a la TRONQUATURE, la ligne est coupee au dernier blanc rencontre et la position du flux
    ///     recule jusqu'a lui. C'est une coupure par mot : la suite de la ligne sera rendue au
    ///     prochain appel, entiere. Sans blanc dans la ligne, la coupure est seche.
    /// </summary>
    private static byte* LireLigne(byte* tampon, int taille, void* flux)
    {
        byte* source = (byte*)*Champ(flux, FluxTampon);
        uint longueur = *Champ(flux, FluxLongueur);
        uint position = *Champ(flux, FluxPosition);

        byte* ecriture = tampon;
        int ecrits = 0;
        byte* dernierBlanc = null;
        uint positionDernierBlanc = 0;

        while (position < longueur)
        {
            byte c = source[position];
            *Champ(flux, FluxPosition) = position + 1;

            if (c == (byte)'\n') { *ecriture = 0; return tampon; }

            if (c != (byte)'\r')
            {
                if (c == (byte)'\t' || c == (byte)' ')
                {
                    positionDernierBlanc = position + 1;
                    dernierBlanc = ecriture;
                }

                *ecriture = c;
                ecriture++;
                ecrits++;

                if (ecrits == taille - 1)
                {
                    if (dernierBlanc != null)
                    {
                        *Champ(flux, FluxPosition) = positionDernierBlanc;
                        *dernierBlanc = 0;
                        *ecriture = 0;
                        return tampon;
                    }
                    *ecriture = 0;
                    return tampon;
                }
            }

            position = *Champ(flux, FluxPosition);
        }

        *ecriture = 0;
        return ecrits == 0 ? null : tampon;
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_1000fe80 @ 0x1000fe80 — remplace les echappements d'une ligne, SUR
    /// PLACE. La sortie est toujours plus courte ou egale, donc la reecriture en place est sure.
    ///
    ///   <c>~</c> devient l'octet 2, <c>^</c> l'octet 1 — deux marqueurs que les donnees du jeu
    ///   emploient et qui ne sont pas des caracteres ;
    ///   le CARACTERE D'ECHAPPEMENT introduit <c>0</c> a <c>9</c> (l'octet de meme valeur),
    ///   <c>n</c>, <c>r</c>, <c>t</c> ; tout autre caractere est garde tel quel.
    ///
    /// **Le caractere d'echappement est configurable** : l'original le lit dans le bloc par thread
    /// a <c>+0x124</c>, a cote des deux crochets de message. Rien dans la cloture ne l'ecrit, donc
    /// il vaut zero et la branche ne se declenche jamais — la boucle s'arrete sur l'octet nul
    /// avant. Le comportement observable est donc : seuls <c>~</c> et <c>^</c> sont traduits.
    ///
    /// TODO(REVERSE): trouver qui ecrit <c>+0x124</c> dans wsyskern.c. C'est le meme bloc que le
    /// `+0x11c`/`+0x120` de `Messages.cs`, et la meme question.
    /// </summary>
    private static void RemplacerEchappements(byte* ligne)
    {
        byte* lecture = ligne;
        byte* ecriture = ligne;
        byte c = *lecture;

        while (c != 0)
        {
            byte* suivant = lecture + 1;

            if (c == (byte)'~') c = 2;
            else if (c == (byte)'^') c = 1;
            else if (c == Wsyskern.CaractereEchappement && Wsyskern.CaractereEchappement != 0)
            {
                c = *suivant;
                suivant = lecture + 2;
                switch (c)
                {
                    case 0: *ecriture = 0; return;
                    case >= (byte)'0' and <= (byte)'9': c -= (byte)'0'; break;
                    case (byte)'n': c = 10; break;
                    case (byte)'r': c = 13; break;
                    case (byte)'t': c = 9; break;
                }
            }

            *ecriture = c;
            ecriture++;
            lecture = suivant;
            c = *suivant;
        }

        *ecriture = 0;
    }

    // =========================================================================================
    // L'export qui les emploie tous
    // =========================================================================================

    /// <summary>
    /// Ghidra: wsyskern.c LoadArray @ 0x1000a190 (export 113) — remplit le tableau depuis un
    /// fichier texte, une ligne par element.
    /// </summary>
    internal static void* LoadArray(void* tableau, byte* chemin, int index, int maxLignes,
                                    int sauter, int drapeaux)
    {
        if (!EstTableau(tableau)) return null;
        Remplir(tableau, chemin, null, 0, index, maxLignes, sauter, (uint)drapeaux);
        return tableau;
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10004060 @ 0x10004060 — le corps de `LoadArray`.
    ///
    /// Les drapeaux : les **trois bits de poids faible** partent tels quels a `LoadFile`, donc y
    /// choisissent le silence, le message fatal ou l'avertissement. Le **bit 3** (`0x08`) interdit
    /// d'agrandir le tableau : sans lui, un index hors capacite le fait croitre de
    /// <c>index + 10</c>, et la boucle l'agrandit encore de dix en dix.
    ///
    /// <paramref name="maxLignes"/> a zero ou moins vaut « sans limite » : l'original pose alors le
    /// compteur a <c>INT_MIN</c> et le decremente jusqu'a zero, ce qui fait deux milliards de tours.
    /// Transcrit comme une absence de borne, ce qui est la meme chose et se lit.
    ///
    /// **La capacite est ramenee a la fin** au plus grand de l'ancienne et du nombre de lignes
    /// lues. Combine au fait que retrecir ne reallouе pas, c'est ce qui rend le gonflement
    /// temporaire sans consequence.
    /// </summary>
    private static int Remplir(void* tableau, byte* chemin, byte* memoire, int longueurMemoire,
                               int index, int maxLignes, int sauter, uint drapeaux)
    {
        int capaciteInitiale = Capacite(tableau);

        if (index < 0) return 0;

        bool sansBorne = maxLignes < 1;

        if (capaciteInitiale <= index)
        {
            if ((drapeaux & 8) != 0) return 0;
            Redimensionner(tableau, index + 10);
        }

        void* flux = memoire == null
            ? OuvrirFichier(chemin, drapeaux & 7)
            : OuvrirMemoire(memoire, longueurMemoire);

        if (flux == null) return -1;

        byte* ligne = stackalloc byte[0x100];

        try
        {
            // Les lignes sautees : `sauter - 1` lectures jetees.
            for (int i = 1; i < sauter; i++)
            {
                if (LireLigne(ligne, 0x100, flux) == null)
                {
                    Redimensionner(tableau, capaciteInitiale);
                    return 0;
                }
            }

            int lues = 0;
            int restant = maxLignes;

            while (sansBorne || restant != 0)
            {
                if (Capacite(tableau) <= index)
                {
                    if ((drapeaux & 8) != 0) break;
                    Redimensionner(tableau, Capacite(tableau) + 10);
                }

                if (LireLigne(ligne, 0x100, flux) == null)
                {
                    Redimensionner(tableau, Math.Max(capaciteInitiale, lues));
                    return lues;
                }

                RemplacerEchappements(ligne);
                PoserDepuisTexte(tableau, index, ligne);

                index++;
                lues++;
                if (!sansBorne) restant--;
            }

            Redimensionner(tableau, Math.Max(capaciteInitiale, lues));
            return lues;
        }
        finally
        {
            Fermer(flux);
        }
    }
}
