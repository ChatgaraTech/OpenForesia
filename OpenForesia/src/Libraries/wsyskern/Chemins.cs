// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c.
//
// ============================================================================================
// DECOUPER ET RECOMPOSER UN CHEMIN — ordinaux 4 et 5
//
// Ces deux corps ne sont appeles par AUCUNE fonction de `Blaze.exe` : c'est `winsys3d.dll` qui
// les importe, et elle seule (table d'imports de `winsys3d.dll`, section WSYSKERN.dll : les
// ordinaux 1, 2, 3, 4, 5 plus les exports nommes). Ils n'apparaissent donc pas dans
// `DLLAtteignables.md`, dont les racines sont les imports du JEU.
//
// Ils arrivent ici parce que `Winsys3dCarteMemoire.ResoudreChemin` (winsys3d FUN_00a03f80) les
// appelle, et que ce corps-la est le pivot de la composante 2 de `DLLGlobales.md` — la famille
// `FindFirstFile` de la carte memoire.
//
//     ordinal 4   FUN_1000a690  ->  FUN_10013160   `_splitpath`
//     ordinal 5   FUN_1000a6c0  ->  FUN_10012ef0   `_makepath`
//
// Les deux exports sont des passe-plats d'une ligne ; le travail est dans les corps internes,
// transcrits ci-dessous.
//
// ============================================================================================
// CE SONT LES CORPS MBCS DE LA CRT DE MSVC, ET POURQUOI CELA NE CHANGE RIEN ICI
//
// `_splitpath` teste chaque octet avec `_ismbblead` — `(_mbctype + 1)[c] & _M1`, soit
// `*(byte*)(&DAT_10058770 + c + 1) & 4` dans la decompilation —, et `_makepath` recule d'un
// caractere avec `_mbsdec` (`FUN_100173b0`). Les deux dependent de la table `_mbctype`
// (`DAT_10058770`, 257 octets) et du code de page courant (`DAT_10058978`, `__mbcodepage`).
//
// **Aucun des deux ne change le resultat tant que `_mbctype` ne porte aucun octet de tete**, et
// c'est le cas sur toute page de code a un octet. La preuve, lue dans les corps :
//
//   - `FUN_10017e70` (`__initmbctable`, appele par l'amorce de la CRT) fait `_setmbcp(-3)`,
//     c'est-a-dire `GetACP()` ;
//   - `FUN_10017970` (`_setmbcp`) cherche la page dans sa table des quatre pages DBCS connues
//     (`DAT_10054920`). Si elle n'y est pas — 1252 n'y est pas —, il appelle `GetCPInfo`, et sur
//     `MaxCharSize < 2` il **remet les 257 octets de `_mbctype` a zero** avant d'enregistrer la
//     page. `__mbcodepage` vaut alors 1252, non nul, mais la table est vide ;
//   - `FUN_100173b0` (`_mbsdec`) avec une table vide : la branche MBCS calcule
//     `p + (-1 - ((p - (p-2)) & 1))` = `p - 1`, exactement ce que rend la branche SBCS ;
//   - `FUN_10017e80` (`_mbsnbcpy`) avec une table vide : aucun octet n'est reconnu comme tete,
//     la boucle copie octet par octet jusqu'au NUL puis comble de zeros — c'est `strncpy`, le
//     corps meme que prend sa branche SBCS.
//
// Les deux branches coincident donc octet pour octet, et la transcription ci-dessous garde la
// forme SBCS en nommant les deux points ou la distinction existerait.
//
// TODO(REVERSE): sur une page ACP reellement DBCS — 932 (Shift-JIS), 936, 949, 950 —, `_mbctype`
// porterait des octets de tete et les deux corps se comporteraient differemment : `_splitpath`
// sauterait l'octet de queue au lieu d'y voir un `\` ou un `.`, et `_mbsnbcpy` refuserait de
// couper une paire. Ce que cela ferait au jeu n'est pas etabli, et **ne peut pas l'etre par la
// lecture seule** : il faudrait une machine en ACP 932. Le chemin qui y mene est de toute facon
// mort aujourd'hui, voir le pave de tete de `winsys3d/CarteMemoire.cs`.
// ============================================================================================

namespace OpenForesia.Libraries;

internal static unsafe class Chemins
{
    /// <summary>
    /// Ghidra: wsyskern.c FUN_10013160 @ 0x10013160 — `_splitpath`, exporte par l'ordinal 4
    /// (<c>FUN_1000a690</c>, un passe-plat d'une ligne).
    ///
    /// Decoupe <paramref name="chemin"/> en quatre morceaux ; chaque sortie peut etre nulle et
    /// est alors ignoree. Le lecteur ne sort que si le deuxieme octet est <c>':'</c>. Le
    /// repertoire s'arrete APRES le dernier separateur, le nom va de la jusqu'au dernier point,
    /// et l'extension part de ce point, le point compris.
    ///
    /// **Les trois copies sont bornees a 255 octets**, et la borne est ecrite
    /// <c>if (0xfe &lt; n) n = 0xff</c> : une longueur de 255 exactement passe telle quelle, 256
    /// et au-dela sont ramenees a 255. Le NUL est ensuite pose a l'indice <c>n</c>, donc une
    /// sortie peut recevoir jusqu'a 256 octets. C'est la CRT de MSVC, pas une invention du
    /// portage — et c'est ce qui oblige l'appelant a fournir <c>_MAX_DIR</c> et non moins.
    /// </summary>
    internal static void Decouper(byte* chemin, byte* lecteur, byte* repertoire, byte* nom, byte* extension)
    {
        // Le corps commence par un `repne scasb` dont il ne tire qu'une chose : la chaine est-elle
        // vide. `iVar4 == -2` dans la decompilation, c'est-a-dire un seul octet balaye, le NUL.
        bool vide = *chemin == 0;

        byte* dernierPoint = null;

        if (vide || chemin[1] != (byte)':')
        {
            if (lecteur != null) *lecteur = 0;
        }
        else
        {
            if (lecteur != null)
            {
                CopierBorne(lecteur, chemin, 2);
                lecteur[2] = 0;
            }
            chemin += 2;
        }

        // Le balayage. `apresSeparateur` pointe JUSTE APRES le dernier `/` ou `\` rencontre, ce
        // qui fait du repertoire un prefixe separateur compris.
        byte* apresSeparateur = null;
        byte* curseur = chemin;
        byte octet = *chemin;
        while (octet != 0)
        {
            octet = *curseur;
            if (!EstOctetDeTete(octet))
            {
                if (octet == (byte)'/' || octet == (byte)'\\') apresSeparateur = curseur + 1;
                else if (octet == (byte)'.') dernierPoint = curseur;
            }
            else
            {
                // La moitie de queue d'une paire DBCS : sautee sans etre examinee. Jamais prise
                // avec une `_mbctype` vide — voir le pave de tete.
                curseur++;
            }
            curseur++;
            octet = *curseur;
        }
        // `curseur` s'arrete sur le NUL : c'est la fin de chaine dont se servent les deux copies
        // suivantes.

        if (apresSeparateur == null)
        {
            apresSeparateur = chemin;
            if (repertoire != null) *repertoire = 0;
        }
        else if (repertoire != null)
        {
            uint n = (uint)(apresSeparateur - chemin);
            if (0xfe < n) n = 0xff;
            CopierBorne(repertoire, chemin, n);
            repertoire[n] = 0;
        }

        if (dernierPoint == null || dernierPoint < apresSeparateur)
        {
            // Pas d'extension : un point vu AVANT le dernier separateur appartient au repertoire,
            // pas au nom — c'est ce que teste `dernierPoint < apresSeparateur`.
            if (nom != null)
            {
                uint n = (uint)(curseur - apresSeparateur);
                if (0xfe < n) n = 0xff;
                CopierBorne(nom, apresSeparateur, n);
                nom[n] = 0;
            }
            if (extension != null) *extension = 0;
        }
        else
        {
            if (nom != null)
            {
                uint n = (uint)(dernierPoint - apresSeparateur);
                if (0xfe < n) n = 0xff;
                CopierBorne(nom, apresSeparateur, n);
                nom[n] = 0;
            }
            if (extension != null)
            {
                uint n = (uint)(curseur - dernierPoint);
                if (0xfe < n) n = 0xff;
                CopierBorne(extension, dernierPoint, n);
                extension[n] = 0;
            }
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10012ef0 @ 0x10012ef0 — `_makepath`, exporte par l'ordinal 5
    /// (<c>FUN_1000a6c0</c>, un passe-plat d'une ligne).
    ///
    /// Recompose un chemin dans <paramref name="chemin"/>. Chaque morceau peut etre nul ; un
    /// morceau nul ou vide est saute. Trois details que le corps impose et qu'il ne faut pas
    /// « arranger » :
    ///
    ///   - du lecteur, **seule la premiere lettre est prise**, suivie d'un <c>':'</c> ecrit en
    ///     dur : <c>"C:"</c> comme <c>"C"</c> donnent <c>"C:"</c> ;
    ///   - un separateur final est ajoute au repertoire s'il n'en a pas — teste sur le DERNIER
    ///     caractere, donc en reculant avec <c>_mbsdec</c> ;
    ///   - l'extension recoit un <c>'.'</c> si elle n'en porte pas ; une extension pointant sur
    ///     un NUL ne donne rien, pas meme le point.
    ///
    /// **Rien ne borne l'ecriture** : c'est a l'appelant de fournir <c>_MAX_PATH</c>.
    /// </summary>
    internal static void Composer(byte* chemin, byte* lecteur, byte* repertoire, byte* nom, byte* extension)
    {
        if (lecteur != null && *lecteur != 0)
        {
            chemin[0] = *lecteur;
            chemin[1] = (byte)':';
            chemin += 2;
        }

        if (repertoire != null && *repertoire != 0)
        {
            byte* source = repertoire;
            byte* dernierEcrit;
            byte octet = *source;
            do
            {
                dernierEcrit = chemin;
                *dernierEcrit = octet;
                octet = source[1];
                chemin = dernierEcrit + 1;
                source++;
            }
            while (octet != 0);
            // `source` s'arrete sur le NUL du repertoire.

            byte* dernier = CaracterePrecedent(repertoire, source);
            if (*dernier != (byte)'/' && *dernier != (byte)'\\')
            {
                *chemin = (byte)'\\';
                chemin = dernierEcrit + 2;
            }
        }

        if (nom != null)
        {
            byte octet = *nom;
            while (octet != 0)
            {
                *chemin = octet;
                chemin++;
                nom++;
                octet = *nom;
            }
        }

        if (extension == null)
        {
            *chemin = 0;
        }
        else
        {
            if (*extension != 0 && *extension != (byte)'.')
            {
                *chemin = (byte)'.';
                chemin++;
            }
            byte octet = *extension;
            *chemin = octet;
            while (octet != 0)
            {
                chemin++;
                extension++;
                octet = *extension;
                *chemin = octet;
            }
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_10017e80 @ 0x10017e80 — `_mbsnbcpy`. Avec une `_mbctype` sans octet
    /// de tete, ses deux branches se rejoignent sur <c>strncpy</c> : au plus <paramref name="n"/>
    /// octets, arret au NUL de la source, **et le reste comble de zeros**. Ce comblement compte :
    /// <see cref="Decouper"/> pose ensuite son propre NUL a l'indice <c>n</c>, il ne compte pas
    /// dessus.
    /// </summary>
    private static void CopierBorne(byte* destination, byte* source, uint n)
    {
        uint i = 0;
        while (i < n && source[i] != 0)
        {
            destination[i] = source[i];
            i++;
        }
        while (i < n)
        {
            destination[i] = 0;
            i++;
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c FUN_100173b0 @ 0x100173b0 — `_mbsdec`. Recule d'un caractere depuis
    /// <paramref name="position"/>, sans passer avant <paramref name="debut"/>. Avec une
    /// `_mbctype` vide les deux branches rendent <c>position - 1</c> ; la branche MBCS y arrive
    /// par <c>position + (-1 - ((position - (position - 2)) &amp; 1))</c>, ou le <c>&amp; 1</c>
    /// annule la correction. Voir le pave de tete.
    /// </summary>
    private static byte* CaracterePrecedent(byte* debut, byte* position)
    {
        if (position <= debut) return null;
        return position - 1;
    }

    /// <summary>
    /// `_ismbblead` : <c>(_mbctype + 1)[c] &amp; _M1</c>, soit
    /// <c>*(byte*)(&amp;DAT_10058770 + c + 1) &amp; 4</c>. La table est vide sur toute page de code
    /// a un octet — ce corps rend donc toujours <c>false</c>, et le pave de tete dit pourquoi
    /// c'est une propriete etablie et non une commodite.
    /// </summary>
    private static bool EstOctetDeTete(byte octet) => false;
}
