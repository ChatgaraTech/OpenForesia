// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c.
//
// LA LECTURE DE FICHIERS : ouvrir, decompresser si besoin, dechiffrer si besoin.
//
// Les trois corps d'ici s'appuient sur `Codec.cs`, qui est le vrai morceau. `LoadBitmapFile` y est
// aussi parce qu'il est, litteralement, la meme chaine avec un test de signature BMP en plus.

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Fichiers
{
    /// <summary>
    /// Ghidra: wsyskern.c decrypt @ 0x100077c0 (export 196) — dechiffre sur place, et rend la
    /// taille utile. Un tampon qui ne porte pas le nombre magique ressort intact.
    ///
    /// LE CHIFFRE, tel que le corps le decrit :
    ///
    ///   - les quatre premiers octets valent `0x9DE29BEE`, sinon il n'y a rien a faire ;
    ///   - l'octet 4, ou exclusif `0x16`, donne la longueur de la CLE ;
    ///   - suivent autant d'octets de cle, chacun tourne a droite de son rang modulo 8 puis ou
    ///     exclusif avec la table `DAT_10049b88`, NEUF octets relevés dans le binaire ;
    ///   - le corps du fichier se lit ensuite par tranches de la longueur de la cle : chaque octet
    ///     est tourne de la meme facon, puis ou exclusif avec l'octet de cle correspondant ET avec
    ///     la table, indexee cette fois **modulo neuf**.
    ///
    /// Le 8 et le 9 ne sont pas une coquille de lecture : la rotation prend `rang &amp; 7`, la table
    /// prend `rang % 9`. Les deux periodes different, ce qui allonge le motif a 72 octets.
    /// </summary>
    internal static uint Decrypt(byte* tampon, uint taille)
    {
        if (taille <= 3) return taille;

        // -0x621d6412 en signé, relevé tel quel dans le corps.
        if (*(int*)tampon != unchecked((int)0x9DE29BEE)) return taille;

        fixed (byte* table = WsyskernData.TableDeChiffre)
        {
            byte* lecture = tampon + 5;
            uint longueurCle = (uint)(tampon[4] ^ 0x16);

            byte* cle = stackalloc byte[128];
            for (uint i = 0; i < longueurCle; i++)
            {
                byte octet = *lecture;
                lecture++;
                int rotation = (int)(i & 7);
                cle[i] = (byte)(table[i & 7] ^ (byte)(octet >> rotation | octet << (8 - rotation)));
            }

            uint utile = taille - 5 - longueurCle;
            uint restant = utile;
            uint rang = 0;
            uint parTranche = longueurCle;
            uint aEcrire = utile;

            while (restant != 0)
            {
                uint reste = restant;
                if (restant < longueurCle)
                {
                    // La derniere tranche est plus courte que la cle : on la raccourcit et on sort.
                    aEcrire = 0;
                    parTranche = restant;
                    reste = 0;
                    longueurCle = restant;
                }

                uint dansTranche = 0;
                restant = reste;

                while (dansTranche < parTranche)
                {
                    byte octet = *lecture;
                    lecture++;
                    int rotation = (int)(rang & 7);
                    byte tourne = (byte)(octet >> rotation | octet << (8 - rotation));
                    *tampon = (byte)(table[rang % 9] ^ cle[dansTranche] ^ tourne);
                    tampon++;
                    dansTranche++;
                    rang++;
                    restant = aEcrire;
                }

                if (longueurCle <= restant)
                {
                    restant -= longueurCle;
                    aEcrire = restant;
                }
            }

            return utile;
        }
    }

    /// <summary>
    /// Ghidra: wsyskern.c LoadFile @ 0x10010230 (export 117) — ouvre, lit en entier, decompresse
    /// puis dechiffre, et rend le tampon. L'appelant le rend par <c>Free</c>.
    ///
    /// Les drapeaux, tels que le corps les teste :
    ///
    ///     bits 0-1 == 1   fichier absent : message FATAL
    ///     bits 0-1 == 2   fichier absent : avertissement, et retour nul
    ///     bit 2 (0x04)    ne pas dechiffrer
    ///     bit 4 (0x10)    ne pas decompresser
    ///
    /// **Le tampon est alloue avec deux octets de rabiot** et un `0x0d` est ecrit a la fin, apres
    /// dechiffrement. C'est ce qui permet aux lecteurs de lignes de la DLL de s'arreter sans
    /// connaitre la taille — un retour chariot fait office de sentinelle.
    ///
    /// La detection de compression ne regarde PAS le code de format : elle exige litteralement
    /// « xxxxx » aux octets 2 a 6. Un fichier marque « 4 » ou « 5 », que le codec accepterait, sort
    /// donc d'ici tel quel. L'asymetrie avec `LoadBitmapFile` est dans la DLL, pas dans cette
    /// transcription.
    /// </summary>
    internal static byte* LoadFile(byte* chemin, uint drapeaux, uint* tailleRendue)
    {
        IntPtr fichier = Win32.CreateFileA(chemin, 0x80000000, 1, null, 3, 0x80, IntPtr.Zero);
        if (fichier == new IntPtr(-1))
        {
            if ((drapeaux & 3) == 1) Messages.FileNotFound(chemin);
            if ((drapeaux & 3) == 2) Messages.WarnFileNotFound(chemin);
            return null;
        }

        uint taille = Win32.GetFileSize(fichier, null);
        byte* tampon = (byte*)Wsyskern.Malloc(taille + 2);

        uint lus;
        Win32.ReadFile(fichier, tampon, taille, &lus, null);
        Win32.CloseHandle(fichier);

        if ((drapeaux & 0x10) == 0)
        {
            bool compresse = tampon[2] == 'x' && tampon[3] == 'x' && tampon[4] == 'x'
                          && tampon[5] == 'x' && tampon[6] == 'x';
            if (compresse)
            {
                byte* original = tampon;
                tampon = Codec.Unpack(tampon, (int)taille, &taille);
                Wsyskern.Free(original);   // Ghidra: FUN_10010359, le bloc de nettoyage du cadre SEH
            }
        }

        if ((drapeaux & 4) == 0) taille = Decrypt(tampon, taille);

        tampon[taille] = 0x0d;
        if (tailleRendue != null) *tailleRendue = taille;

        return tampon;
    }

    /// <summary>
    /// Ghidra: wsyskern.c LoadBitmapFile @ 0x10010500 (export 115) — rend le contenu brut d'un
    /// `.BMP`, decompresse au passage s'il l'etait.
    ///
    /// Trois chemins : la signature `BM` en tete se lit telle quelle ; sinon le fichier passe par
    /// le codec, et le resultat doit alors porter `BM` a son tour ; sinon c'est une erreur.
    ///
    /// **Le handle est ferme deux fois** quand le fichier est compresse :
    /// <c>Codec.DecompresserFichier</c> le ferme (c'est dans `FUN_10003260`), puis ce corps-ci le
    /// referme au retour. C'est un defaut de 1998, benin sous Win32 — fermer un handle deja ferme
    /// echoue sans consequence — mais reproduit tel quel, comme tous les autres. Voir
    /// `Documentation/Reference/legacy-bug.md`.
    /// </summary>
    internal static byte* LoadBitmapFile(byte* chemin, uint drapeaux)
    {
        IntPtr fichier = Win32.CreateFileA(chemin, 0x80000000, 1, null, 3, 0x80, IntPtr.Zero);

        if (fichier == new IntPtr(-1))
        {
            if (drapeaux == 1) Messages.FileNotFound(chemin);
            if (drapeaux == 2) Messages.WarnFileNotFound(chemin);
            return null;
        }

        ushort signature = 0;
        uint lus;
        Win32.ReadFile(fichier, &signature, 4, &lus, null);
        Win32.SetFilePointer(fichier, 0, null, 0);

        if (signature == 0x4d42)   // « BM »
        {
            uint taille = Win32.GetFileSize(fichier, null);
            byte* tampon = (byte*)Wsyskern.Malloc(taille);
            Win32.ReadFile(fichier, tampon, taille, &lus, null);
            Win32.CloseHandle(fichier);
            return tampon;
        }

        byte* decompresse = Codec.DecompresserFichier(fichier);
        Win32.CloseHandle(fichier);   // la seconde fermeture — voir le resume

        if (decompresse != null && *(ushort*)decompresse == 0x4d42) return decompresse;

        Wsyskern.Free(decompresse);
        if (drapeaux == 1) Messages.FileError(chemin);
        if (drapeaux == 2) Messages.WarnFileError(chemin);
        return null;
    }
}
