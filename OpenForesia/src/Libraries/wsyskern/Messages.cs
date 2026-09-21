// OpenForesia — reconstruction de wsyskern.dll (1998), lue dans Legacy/Decompiled/wsyskern.c.
//
// LES MESSAGES A L'UTILISATEUR. Sept corps, et c'est le sous-arbre le plus profond de la DLL au
// sens ou tout le reste y aboutit : un fichier manquant, un fichier abime, un tas plein sortent
// tous par ici. Les cinq derniers ne sont que des formats figes.
//
// Le titre de la boite est `s_WSYSKERN_10054058` — la chaine « WSYSKERN », relevee a cette adresse.

using System;
using OpenForesia.Blaze;

namespace OpenForesia.Libraries;

internal static unsafe class Messages
{
    // Le titre, en ANSI termine par zero, alloue une fois pour toutes. `&DAT_10054058`.
    private static readonly byte[] s_Titre = "WSYSKERN\0"u8.ToArray();

    // Les cinq formats, tels quels. `wvsprintfA` les lit, donc ils gardent la syntaxe de
    // `wvsprintf` et non celle de `printf` — voir la note sur l'import dans Interop/Win32.cs.
    private static readonly byte[] s_HorsMemoire  = "Out of memory. %d bytes required\0"u8.ToArray();
    private static readonly byte[] s_Introuvable  = "%s: File not found\0"u8.ToArray();
    private static readonly byte[] s_Corrompu     = "%s: corrupted file\0"u8.ToArray();

    // =====================================================================
    // Le tampon de mise en forme fait 300 octets chez l'original (`CHAR local_12c [300]`), sur la
    // pile. Ici il est statique : `wvsprintfA` n'a aucune borne de sortie, donc un tampon de pile
    // de 300 octets dans du code `unsafe` deborderait sur nos propres variables au lieu de
    // deborder sur les siennes. La taille est conservee a l'identique — la tronquature eventuelle
    // d'un message tres long doit rester celle de 1998.
    //
    // Statique implique non reentrant, exactement comme l'original l'etait par thread : les deux
    // corps ci-dessous s'appellent depuis le meme thread que le chargement de fichiers.
    // =====================================================================
    private static readonly byte[] s_Tampon = new byte[300];

    /// <summary>
    /// Ghidra: wsyskern.c ErrorMessage @ 0x100088d0 (export 85) — met en forme, affiche, puis
    /// SORT. Le retour n'existe pas : `+0x11c` est appele dans les deux branches.
    /// </summary>
    internal static void ErrorMessage(byte* format, void* arglist)
    {
        fixed (byte* tampon = s_Tampon)
        fixed (byte* titre = s_Titre)
        {
            Win32.wvsprintfA(tampon, format, arglist);

            if (Wsyskern.MessageHook == null)
            {
                // 0x2010 = MB_OK | MB_ICONHAND | MB_TASKMODAL
                Win32.MessageBoxA(IntPtr.Zero, tampon, titre, 0x2010);
            }
            else
            {
                Wsyskern.MessageHook(tampon, 1);
            }
        }

        if (Wsyskern.FatalExit != null) Wsyskern.FatalExit();

        // L'original ne revient jamais ici : `+0x11c` est une sortie de processus. Tant que le
        // crochet n'est pas installe — et rien dans la cloture ne l'installe —, ne rien faire
        // rendrait la main a l'appelant, qui continuerait sur un etat que l'original tenait pour
        // perdu. On leve plutot que de poursuivre en silence.
        throw new NotImplementedException(
            "wsyskern ErrorMessage @ 0x100088d0 : l'original sort du processus par le crochet " +
            "+0x11c du bloc par thread, et ce crochet n'est pas installe. " +
            "TODO(REVERSE): trouver qui ecrit +0x11c dans wsyskern.c.");
    }

    /// <summary>
    /// Ghidra: wsyskern.c WarningMessage @ 0x10008950 (export 146) — met en forme et affiche une
    /// boite Abandonner/Recommencer/Ignorer. **Recommencer la reaffiche** (boucle sur `IDRETRY`,
    /// 4) ; Abandonner (`IDABORT`, 3) sort par `+0x11c` ; Ignorer revient a l'appelant.
    /// </summary>
    internal static void WarningMessage(byte* format, void* arglist)
    {
        fixed (byte* tampon = s_Tampon)
        fixed (byte* titre = s_Titre)
        {
            Win32.wvsprintfA(tampon, format, arglist);

            if (Wsyskern.MessageHook != null)
            {
                Wsyskern.MessageHook(tampon, 0);
                return;
            }

            int reponse;
            do
            {
                // 0x2042 = MB_ABORTRETRYIGNORE | MB_ICONHAND | MB_TASKMODAL
                reponse = Win32.MessageBoxA(IntPtr.Zero, tampon, titre, 0x2042);
            }
            while (reponse == 4 /* IDRETRY */);

            if (reponse != 3 /* IDABORT */) return;
        }

        if (Wsyskern.FatalExit != null) Wsyskern.FatalExit();

        throw new NotImplementedException(
            "wsyskern WarningMessage @ 0x10008950 : Abandonner sort du processus par le crochet " +
            "+0x11c du bloc par thread, et ce crochet n'est pas installe.");
    }

    // ---------------------------------------------------------------------------------------
    // Les cinq formats figes. Chacun pousse son unique argument sur la pile et le passe comme
    // liste de va_list — ce que fait l'original, dont le `&stack0x00000008` designe l'argument
    // qui suit le format.
    // ---------------------------------------------------------------------------------------

    /// <summary>Ghidra: wsyskern.c outofmemory @ 0x10008a30 (export 209).</summary>
    internal static void OutOfMemory(uint octets)
    {
        uint argument = octets;
        fixed (byte* format = s_HorsMemoire) ErrorMessage(format, &argument);
    }

    /// <summary>Ghidra: wsyskern.c filenotfound @ 0x10008a50 (export 198).</summary>
    internal static void FileNotFound(byte* chemin)
    {
        byte* argument = chemin;
        fixed (byte* format = s_Introuvable) ErrorMessage(format, &argument);
    }

    /// <summary>Ghidra: wsyskern.c fileerror @ 0x10008a90 (export 197).</summary>
    internal static void FileError(byte* chemin)
    {
        byte* argument = chemin;
        fixed (byte* format = s_Corrompu) ErrorMessage(format, &argument);
    }

    /// <summary>Ghidra: wsyskern.c warnfilenotfound @ 0x10008a70 (export 243).</summary>
    internal static void WarnFileNotFound(byte* chemin)
    {
        byte* argument = chemin;
        fixed (byte* format = s_Introuvable) WarningMessage(format, &argument);
    }

    /// <summary>Ghidra: wsyskern.c warnfileerror @ 0x10008ab0 (export 242).</summary>
    internal static void WarnFileError(byte* chemin)
    {
        byte* argument = chemin;
        fixed (byte* format = s_Corrompu) WarningMessage(format, &argument);
    }
}
